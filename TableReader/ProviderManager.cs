using System;

public class ProviderManager : IDataManager
{
	string tableName;

	public ProviderManager (string tableName)
	{
		this.tableName = tableName;
	}

	public string FindExisting(string NPINumber){
		string query = "SELECT * FROM " + tableName + " WHERE NPI=" + NPINumber + "";
		return query;
	}

	public string AddEntity(Entry entry){
		string command = "INSERT INTO " + tableName + "(NPI, ProviderLastName, ProviderFirstName, ProviderNamePrefix, ProviderNameSuffix, ProviderCredentialText, " +
			"FirstLineMailingAddress, SecondLineMailingAddress, MailingAddressCity, MailingAddressState, MailingAddressPostalCode, MailingAddressCountryCode, MailingAddressTelephone, MailingAddressFax, " +
			"FirstLinePracticeAddress, SecondLinePracticeAddress, PracticeAddressCity, PracticeAddressState, PracticeAddressPostalCode, PracticeAddressCountryCode, " +
			"PracticeAddressTelephone, PracticeAddressFaxNumber, TaxonomyCode1, LicenseNumber1, LicenseStateCode1, TaxonomySwitch1, " +
			"IsSoleProprietor, DeactivationDate) VALUES (" +
			entry.NPI + ", '" +
			entry.providerLastName + "', '" +
			entry.providerFirstName + "', '" +
			entry.providerNamePrefix + "', '" +
			entry.providerNameSufix + "', '" +
			entry.providerCredentialText + "', '" +
			entry.firstLineMailingAddress + "', '" +
			entry.secondLineMailingAddress + "', '" +
			entry.mailingAddressCity + "', '" +
			entry.mailingAddressState + "', '" +
			entry.mailingAddressPostalCode + "', '" +
			entry.mailingAddressCountryCode + "', '" +
			entry.mailingAddressTelephone + "', '" +
			entry.mailingAddressFax + "', '" +
			entry.firstLinePracticeAddress+ "', '" +
			entry.secondLinePracticeAddress + "', '" +
			entry.practiceAddressCity + "', '" +
			entry.practiceAddressState + "', '" +
			entry.practiceAddressPostalCode + "', '" +
			entry.practiceAddressCountryCode + "', '" +
			entry.practiceAddressTelephone + "', '" +
			entry.practiceAddressFax + "', '" +
			entry.taxonomyCode1 + "', '" +
			entry.LicenseNumber1 + "', '" +
			entry.LicenseStateCode1 + "', '" +
			entry.TaxonomySwitch1 + "', '" +
			entry.isSoleProprietor + "', '" +
			entry.deactivationDate + "')";


		return command;
	}

	public string UpdateEntity(Entry entry){
		//PracticeAddressState (col 31) is absent from the provided schema. Intentional?
		string command = "UPDATE " + tableName + " SET ProviderLastName = " + entry.providerLastName +
			", ProviderFirstName = " + entry.providerFirstName +
			", ProviderNamePrefix = " + entry.providerNamePrefix +
			", ProviderNameSuffix = " + entry.providerNameSufix +
			", ProviderCredentialText = " + entry.providerCredentialText +
			", FirstLineMailingAddress = " + entry.firstLineMailingAddress +
			", SecondLineMailingAddress = " + entry.secondLineMailingAddress +
			", MailingAddressCity = " + entry.mailingAddressCity +
			", MailingAddressState = " + entry.mailingAddressState +
			", MailingAddressPostalCode = " + entry.mailingAddressPostalCode +
			", MailingAddressCountryCode = " + entry.mailingAddressCountryCode +
			", MailingAddressTelephone = " + entry.mailingAddressTelephone +
			", MailingAddressFax = " + entry.mailingAddressFax +
			", FirstLinePracticeAddress = " + entry.firstLinePracticeAddress +
			", SecondLinePracticeAddress = " + entry.secondLinePracticeAddress +
			", PracticeAddressCity = " + entry.practiceAddressCity +
			", PracticeAddressState = " + entry.practiceAddressState +
			", PracticeAddressPostalCode = " + entry.practiceAddressPostalCode +
			", PracticeAddressCountryCode = " + entry.practiceAddressCountryCode +
			", PracticeAddressTelephone = " + entry.practiceAddressTelephone +
			", PracticeAddressFaxNumber = " + entry.practiceAddressFax +
			", TaxonomyCode1 = " + entry.taxonomyCode1 +
			", LicenseNumber1 = " + entry.LicenseNumber1 +
			", LicenseStateCode1 = " + entry.LicenseStateCode1 +
			", TaxonomySwitch1 = " + entry.TaxonomySwitch1 +
			", IsSoleProprietor = " + entry.isSoleProprietor +
			", DeactivationDate = " + entry.deactivationDate +
			" WHERE NPI=" + entry.NPI;

		return command;
	}

	public string DeactivateEntity(string NPINumber){
		string command = "DELETE FROM " + tableName + " WHERE NPI = " + NPINumber + "";
		return command;
	}
}