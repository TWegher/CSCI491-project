using System;
using System.IO;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

public class TableReader
{
    //Initialize the connection
    string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=nppes_1;";


    MySqlConnection databaseConnection = new MySqlConnection(connectionString);
    MySqlDataReader reader;
    MySqlCommand command = new MySqlCommand();

	OrganizationManager orgManager = new OrganizationManager ("npi_organization_data");
	ProviderManager proManager = new ProviderManager ("npi_provider_data");
	DeactivationManager deaManager = new DeactivationManager ("npi_deactivated");

    private void readUpdateFile(string fileLocation)
    {
        //Initialize the StreamReader
        FileStream fs = File.OpenRead(fileLocation);
        StreamReader sr = new StreamReader(fs);

        //Reads in the CSV update file, line by line
        List<Entry> entries = new List<Entry>();
        while (!sr.EndOfStream)
        {
            string line = sr.ReadLine();
			Entry entry = new Entry (line.Split (','));
			entries.Add(entry);
        }

        foreach (Entry curEntry in entries)
        {
			switch (curEntry.entryType) {
			case EntryType.Provider:
				{
					updateTable(proManager, curEntry);
				}
			case EntryType.Organization:
				{
					updateTable(orgManager, curEntry);
				}
			//TODO: Ask client what should be done if the entry is not currently found
			case EntryType.Deactivate:
				{
					deactivateEntity (curEntry);
				}
			}
        }
    }

    private void readDeactivationFile()
    {

    }

	private void updateTable(IDataManager tableManager, Entry entry)
    {
		string query = tableManager.UpdateEntity (entry);

		//If the database was not able to update the entity, attempt to add it instead
		if (!tryCommand(query)) {
			tryCommand(tableManager.AddEntity(entry));
		}  
    }

	private void deactivateEntity(Entry entry){
		
		string query = deaManager.UpdateEntity (entry);

		if (!tryCommand (query)) {
			tryCommand (deaManager.AddEntity (entry.values [0]));
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
