using System;

public interface IDataManager
{
	string FindExisting(string NPINumber);
	string AddEntity(Entry entry);
	string UpdateEntity(Entry entry);
	string DeactivateEntity(string NPINumber);
}