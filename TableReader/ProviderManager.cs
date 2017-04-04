using System;

public class ProviderManager : IDataManager
{
	string tableName;

	public ProviderManager (string tableName)
	{
		this.tableName = tableName;
	}

	public string FindExisting(string NPINumber){
		string query = "SELECT * FROM " + tableName + " WHERE NPI=" + NPINumber + " ;";
		return query;
	}

	public string AddEntity(Entry entry){
		string command = "INSERT INTO " + tableName + "(NPI, ProviderLastName, ProviderFirstName, ProviderNamePrefix, ProviderNameSuffix, ProviderCredentialText, " +
			"FirstLineMailingAddress, SecondLineMailingAddress, MailingAddressCity, MailingAddressState, MailingAddressPostalCode, MailingAddressCountryCode, MailingAddressTelephone, MailingAddressFax, " +
			"FirstLinePracticeAddress, SecondLinePracticeAddress, PracticeAddressCity, PracticeAddressState, PracticeAddressPostalCode, PracticeAddressCountryCode, " +
			"PracticeAddressTelephone, PracticeAddressFaxNumber, TaxonomyCode1, LicenseNumber1, LicenseStateCode1, TaxonomySwitch1, " +
			"IsSoleProprietor, DeactivationDate) VALUES (" +
			entry.values[0] + ", '" +
			entry.values[5] + "', '" +
			entry.values[6] + "', '" +
			entry.values[8] + "', '" +
			entry.values[9] + "', '" +
			entry.values[10] + "', '" +
			entry.values[20] + "', '" +
			entry.values[21] + "', '" +
			entry.values[22] + "', '" +
			entry.values[23] + "', '" +
			entry.values[24] + "', '" +
			entry.values[25] + "', '" +
			entry.values[26] + "', '" +
			entry.values[27] + "', '" +
			entry.values[28] + "', '" +
			entry.values[29] + "', '" +
			entry.values[30] + "', '" +
			entry.values[31] + "', '" +
			entry.values[32] + "', '" +
			entry.values[33] + "', '" +
			entry.values[34] + "', '" +
			entry.values[35] + "', '" +
			entry.values[47] + "', '" +
			entry.values[48] + "', '" +
			entry.values[49] + "', '" +
			entry.values[50] + "', '" +
			entry.values[307] + "', '" +
			entry.values[39] + "');";


		return command;
	}

	public string UpdateEntity(Entry entry){
		//PracticeAddressState (col 31) is absent from the provided schema. Intentional?
		string command = "UPDATE " + tableName + " SET ProviderLastName = " + entry.values[5] +
			", ProviderFirstName = " + entry.values[6] +
			", ProviderNamePrefix = " + entry.values[8] +
			", ProviderNameSuffix = " + entry.values[9] +
			", ProviderCredentialText = " + entry.values[10] +
			", FirstLineMailingAddress = " + entry.values[20] +
			", SecondLineMailingAddress = " + entry.values[21] +
			", MailingAddressCity = " + entry.values[22] +
			", MailingAddressState = " + entry.values[23] +
			", MailingAddressPostalCode = " + entry.values[24] +
			", MailingAddressCountryCode = " + entry.values[25] +
			", MailingAddressTelephone = " + entry.values[26] +
			", MailingAddressFax = " + entry.values[27] +
			", FirstLinePracticeAddress = " + entry.values[28] +
			", SecondLinePracticeAddress = " + entry.values[29] +
			", PracticeAddressCity = " + entry.values[30] +
			", PracticeAddressState = " + entry.values[31] +
			", PracticeAddressPostalCode = " + entry.values[32] +
			", PracticeAddressCountryCode = " + entry.values[33] +
			", PracticeAddressTelephone = " + entry.values[34] +
			", PracticeAddressFaxNumber = " + entry.values[35] +
			", TaxonomyCode1 = " + entry.values[47] +
			", LicenseNumber1 = " + entry.values[48] +
			", LicenseStateCode1 = " + entry.values[49] +
			", TaxonomySwitch1 = " + entry.values[50] +
			", IsSoleProprietor = " + entry.values[307] +
			", DeactivationDate = " + entry.values[39] +
			" WHERE NPI=" + entry.values[0];

		return command;
	}

	public string DeactivateEntity(string NPINumber){
		string command = "DELETE FROM " + tableName + " WHERE NPI = " + NPINumber + ";";
		return command;
	}
}