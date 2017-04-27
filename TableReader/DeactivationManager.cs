using System;

public class DeactivationManager : IDataManager
{
    string tableName;

    public DeactivationManager(string tableName)
    {
        this.tableName = tableName;
    }

    public string FindExisting(string NPINumber)
    {
        string query = "SELECT * FROM " + tableName + " WHERE NPI=" + NPINumber + "";
        return query;
    }

    public string DeactivateEntity(string NPINumber)
    {
        string command = "DELETE FROM " + tableName + " WHERE NPI = " + NPINumber + ";";
        return command;
    }

    public string AddEntity(Entry entry)
    {
        string command = "INSERT INTO " + tableName + "(NPI, DeactivationDate) VALUES (" +
            entry.NPI + ", '" +
            entry.deactivationDate + "')";

        return command;
    }

    public string UpdateEntity(Entry entry)
    {
        string command = "UPDATE " + tableName + " SET DeactivationDate = " + entry.deactivationDate + " WHERE NPI=" + entry.NPI + "";
        return command;
    }
}
