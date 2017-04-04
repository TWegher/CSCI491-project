using System;
using System.Collections.Generic;

public class OrganizationManager : IDataManager
{
	string tableName;

	public OrganizationManager (string tableName){
		this.tableName = tableName;
	}

	Entity FindExisting(int NPINumber){

	}

	bool AddEntity(Entry inEntry){
		string query = "INSERT INTO " + tableName "(NPI, Name, OtherName, OtherNameTypeCode, FirstLineMailingAddress, SecondLineMailingAddress, MailingAddressCity, " +
			"MailingAddressState, MailingAddressPostalCode, MailingAddressCountryCode, MailingAddressTelephone, MailingAddressFax, " +
			"FirstLinePracticeAddress, secondLinePracticeAddress, PracticeAddressCity, PracticeAddressState, PracticeAddressPostalCode, PracticeAddressCountryCode, " +
			"PracticeAddressTelephone, PracticeAddressFax, AuthorizedOfficialLastName, AuthorizedOfficialFirstName, AuthorizedOfficialTitle, " +
			"AuthorizedOfficialCredential, AuthorizedOfficialTelephone, TaxonomyCode1, LicenseNumber1, LicenseStateCode1, TaxonomySwitch1, " +
			"IsSoleProprietor, IsOrganizationSubpart, DeactivationDate) VALUES (" +
			entry.values[0] + ", '" +
			entry.values[4] + "', '" +
			entry.values[11] + "', '" +
			entry.values[12] + "', '" +
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
			entry.values[42] + "', '" +
			entry.values[43] + "', '" +
			entry.values[45] + "', '" +
			entry.values[313] + "', '" +
			entry.values[46] + "', '" +
			entry.values[47] + "', '" +
			entry.values[48] + "', '" +
			entry.values[49] + "', '" +
			entry.values[50] + "', '" +
			entry.values[307] + "', '" +
			entry.values[308] + "', '" +
			entry.values[39] + "');" +
	}

	string UpdateEntity(Entry entry){
		string query = "UPDATE " + tableName + " SET Name = "  + entry.values[4] +
			", OtherName = " + entry.values[11] +
			", OtherNameTypeCode = " + entry.values[12] +
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
			", PracticeAddressFax = " + entry.values[35] +
			", AuthorizedOfficialLastName = " + entry.values[42] +
			", AuthorizedOfficialFirstName = " + entry.values[43] +
			", AuthorizedOfficialTitle = " + entry.values[45] +
			", AuthorizedOfficialCredential = " + entry.values[313] +
			", AuthorizedOfficialTelephone = " + entry.values[46] +
			", TaxonomyCode1 = " + entry.values[47] +
			", LicenseNumber1 = " + entry.values[48] +
			", LicenseStateCode1 = " + entry.values[49] +
			", TaxonomySwitch1 = " + entry.values[50] +
			", IsSoleProprietor = " + entry.values[307] +
			", IsOrganizationSubpart = " + entry.values[308] +
			", DeactivationDate = " + entry.values[39] +
			" WHERE NPI=" + entry.values[0] +
			";";

		return query;
	}

	bool DeactivateEntity(string NPINumber){
		string query = "DELETE FROM " + tableName + " WHERE NPI = " + NPINumber + ";";
		return query;
	}
}
