﻿using System;

public class DeactivationManager : IDataManager
{
    string tableName;

	public DeactivationManager(string tableName)
	{
        this.tableName = tableName;
	}

    public string FindExisting(string NPINumber){
        string query = "SELECT * FROM " + tableName + " WHERE NPI=" + NPINumber + " ;";
        return query;
    }

    public string DeactivateEntity(string NPINumber){
        throw new NotImplementedException();
    }

    public string AddEntity(Entry entry)
    {
        string command = "INSERT INTO " + tableName + "(NPI, DeactivaitionDate) VALUES (" +
            entry.NPI + ", '" +
            entry.deactivationDate + "');";

        return command;
    }

    public string UpdateEntity(Entry entry)
    {
        throw new NotImplementedException();
    }
}