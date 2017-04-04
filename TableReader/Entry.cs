using System;
using System.Collections.Generic;

public class Entry
{
	public EntryType entryType;
	public List<String> values;

	public Entry (List<String> inValues)
	{
		values = inValues;

		//Gets the second value of input, the Entity type code
		if (string.IsNullOrEmpty (values [1])) {
			entryType = EntryType.Deactivate;
		} else {
			//Converts the recieved string input to an int, then casts to an EntityType
			entryType = (EntryType)Int16.Parse (values [1]);
		}
	}

}

public enum EntryType{
	Deactivate, 
	Provider = 1, 
	Organization = 2
}