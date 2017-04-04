using System;

public class ProviderManager
{
	string tableName;

	public ProviderManager (string tableName)
	{
		this.tableName = tableName;
	}

	string UpdateEntity(Entry entry){
		
		//PracticeAddressState (col 31) is absent from the provided schema. Intentional?
		string query = "UPDATE " + tableName + " SET ProviderLastName = " + input[5] +
			", ProviderFirstName = " + input[6] +
			", ProviderNamePrefix = " + input[8] +
			", ProviderNameSuffix = " + input[9] +
			", ProviderCredentialText = " + input[10] +
			", FirstLineMailingAddress = " + input[20] +
			", SecondLineMailingAddress = " + input[21] +
			", MailingAddressCity = " + input[22] +
			", MailingAddressState = " + input[23] +
			", MailingAddressPostalCode = " + input[24] +
			", MailingAddressCountryCode = " + input[25] +
			", MailingAddressTelephone = " + input[26] +
			", MailingAddressFax = " + input[27] +
			", FirstLinePracticeAddress = " + input[28] +
			", SecondLinePracticeAddress = " + input[29] +
			", PracticeAddressCity = " + input[30] +
			", PracticeAddressState = " + entry.values[31] +
			", PracticeAddressPostalCode = " + input[32] +
			", PracticeAddressCountryCode = " + input[33] +
			", PracticeAddressTelephone = " + input[34] +
			", PracticeAddressFaxNumber = " + input[35] +
			", TaxonomyCode1 = " + input[47] +
			", LicenseNumber1 = " + input[48] +
			", LicenseStateCode1 = " + input[49] +
			", TaxonomySwitch1 = " + input[50] +
			", IsSoleProprietor = " + input[307] +
			", DeactivationDate = " + input[39] +
			" WHERE NPI=" + input[0];

		return query;
	}
}