using System;

public class SystemManager
{
	public SystemManager(){}
    public static void Main(string[] args)
    {
        //TODO: Logger?

        //TODO: Return filelocation from Downloader
        //Creates an instance of Downloader and attempts to download the appropriate nppes files
        Downloader d = new Downloader();
        string updateFileLocation = d.downloadFile("Weekly Update");
        string deactivateFileLocation = d.downloadFile("Monthly Deactivate");

        //TODO: Determine connection string here
        //Connects to the database and applies the downloaded files
        string connectionString; 

        TableReader tr = new TableReader(connectionString);
        tr.readUpdateFile(updateFileLocation);
        tr.readDeactivateFile(deactivateFileLocation);
    }
}
