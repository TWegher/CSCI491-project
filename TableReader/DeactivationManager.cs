using System;

public class DeactivationManager : IDataManager
{
    string tableName;

	public DeactivationManager(string tableName)
	{
        this.tableName = tableName;
	}

    string FindExisting(string NPINumber){
        string query = "SELECT * FROM " + tableName + " WHERE NPI=" + NPINumber + " ;";
        return query;
    }

    string DeactivateEntity(string NPINumber){
 
    }
}
