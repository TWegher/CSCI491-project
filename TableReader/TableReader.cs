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

    public TableReader()
    {
        databaseConnection = new MySqlConnection(connectionString);
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
			//TODO: Ask client what should be done if the entry is not currently found
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

        //TODO: finish implementation of Method
    }

	private void updateTable(IDataManager tableManager, Entry entry)
    {
		string query = tableManager.UpdateEntity (entry);

		//If the database was not able to update the entity, attempt to add it instead
		if (!tryCommand(query)) {
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
            string line = sr.ReadLine();
            Entry entry = new Entry(new List<string>(line.Split(',')));
            entries.Add(entry);
        }

        return entries;
    }

	private void deactivateEntity(Entry entry){
		
		string query = deaManager.UpdateEntity (entry);

		if (!tryCommand (query)) {
			tryCommand (deaManager.AddEntity (entry));
		}
		tryCommand(proManager.DeactivateEntity (entry.values [0]));
		tryCommand(orgManager.DeactivateEntity (entry.values [0]));
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
			return false;
		}

		//Only one row should ever be effected by a given command
		if (numMatched == 1) {
			return true;
		} else {
			return false;
		}
	}
	
}
