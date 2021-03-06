using System;
using System.Collections.Generic;

public class SystemManager
{
	public SystemManager(){}
    public static void Main(string[] args)
    {
        //TODO: Logger?

        //TODO: Return filelocation from Downloader
        //Creates an instance of Downloader and attempts to download the appropriate nppes files
        Downloader d = new Downloader();

        List<string> updateFileLocations = d.checkPath(FileType.Update);
        List<string> deactivateFileLocations = d.checkPath(FileType.Deactivate);

        //TODO: Determine connection string here
        //Connects to the database and applies the downloaded files
        string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=nppes_1;"; 

        TableReader tr = new TableReader(connectionString);
        foreach (string fileLocation in updateFileLocations){
            tr.readUpdateFile(fileLocation);
        }
        foreach (string fileLocation in deactivateFileLocations){
            tr.readDeactivationFile(fileLocation);
        }
    }
}
