using System;
using System.Collections.Generic;

class FileManager
{
    TableReader tr;
    public FileManager()
    {
        string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=nppes_1;"
        tr = new TableReader(connectionString);

        //tr.readUpdateFile(updateFileLocation);
        //tr.readUpdateFile(deactivationFileLocation);
    }
}
