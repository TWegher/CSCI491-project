using System;
using System.IO;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

public class TableReader
{
    //Initialize the connection
    string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=nppes_1;";

    //MySqlDataReader reader;
    MySqlConnection databaseConnection;
    MySqlCommand command = new MySqlCommand();

    OrganizationManager orgManager;
    ProviderManager proManager;
    DeactivationManager deaManager;

    public TableReader(string connectionString)
    {
        if (!string.IsNullOrEmpty(connectionString))
        {
            this.connectionString = connectionString;
        } 
        databaseConnection = new MySqlConnection(this.connectionString);
        orgManager = new OrganizationManager("npi_organization_data");
        proManager = new ProviderManager("npi_provider_data");
        deaManager = new DeactivationManager("npi_deactivated");
    }

    public void readUpdateFile(string fileLocation)
    {
        List<Entry> entries = generateEntries(fileLocation);

        foreach (Entry curEntry in entries)
        {
			switch (curEntry.entryType) {
			case EntryType.Provider:
				{
					updateTable(proManager, curEntry);
                        break;
				}
			case EntryType.Organization:
				{
					updateTable(orgManager, curEntry);
                        break;
				}

			case EntryType.Deactivate:
				{
					deactivateEntity (curEntry);
                        break;
				}
			}
        }
    }

    public void readDeactivationFile(string fileLocation)
    {
        List<Entry> entries = generateEntries(fileLocation);

        //As each entry in the deactivation list is know to be of EntryType.Deactivate, a switch check is unneccesary
        foreach(Entry curEntry in entries){
            deactivateEntity(curEntry);
        }
    }

	private void updateTable(IDataManager tableManager, Entry entry)
    {
		//If the database was not able to update the entity, attempt to add it instead
		if (!tryCommand(tableManager.UpdateEntity(entry))) {
			tryCommand(tableManager.AddEntity(entry));
		}  
    }

    private List<Entry> generateEntries(string fileLocation)
    {
        //Initialize the StreamReader
        FileStream fs = File.OpenRead(fileLocation);
        StreamReader sr = new StreamReader(fs);

        //Reads in the CSV update file, line by line
        List<Entry> entries = new List<Entry>();
        while (!sr.EndOfStream)
        {
            //TODO: When generating entries for deactivation files, Determine and utilize the applicable stop date
            string line = sr.ReadLine();
            Entry entry = new Entry(new List<string>(line.Split(',')));
            entries.Add(entry);
        }

        return entries;
    }

	private void deactivateEntity(Entry entry){

		if (!tryCommand (deaManager.UpdateEntity(entry))) {
			tryCommand (deaManager.AddEntity (entry));
		}

		tryCommand(proManager.DeactivateEntity (entry.NPI));
		tryCommand(orgManager.DeactivateEntity (entry.NPI));
	}

	private bool tryCommand(string query){

		//Stores the number of effected rows by the executed command
		int numMatched;

		command = new MySqlCommand(query, databaseConnection);
		command.CommandTimeout = 30;

		try{
			databaseConnection.Open();
			//reader = command.ExecuteReader();
			numMatched = command.ExecuteNonQuery();

			databaseConnection.Close();
		}
		catch (Exception ex){
			databaseConnection.Close();
			return false;
		}

		//Only one row should ever be effected by a given command, as NPI is the primary key and passed as the argument
		if (numMatched == 1) {
			return true;
		} else {
			return false;
		}
	}
}
