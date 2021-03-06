using System;
using System.Collections.Generic;

public class OrganizationManager : IDataManager
{
	string tableName;

	public OrganizationManager (string tableName){
		this.tableName = tableName;
	}

	public string FindExisting(string NPINumber){
		string query = "SELECT * FROM " + tableName + " WHERE NPI=" + NPINumber + "";
		return query;
	}

	public string AddEntity(Entry entry){
		string command = "INSERT INTO " + tableName + "(NPI, Name, OtherName, OtherNameTypeCode, FirstLineMailingAddress, SecondLineMailingAddress, MailingAddressCity, " +
			"MailingAddressState, MailingAddressPostalCode, MailingAddressCountryCode, MailingAddressTelephone, MailingAddressFax, " +
			"FirstLinePracticeAddress, SecondLinePracticeAddress, PracticeAddressCity, PracticeAddressState, PracticeAddressPostalCode, PracticeAddressCountryCode, " +
			"PracticeAddressTelephone, PracticeAddressFax, AuthorizedOfficialLastName, AuthorizedOfficialFirstName, AuthorizedOfficialTitle, " +
			"AuthorizedOfficialCredential, AuthorizedOfficialTelephone, TaxonomyCode1, LicenseNumber1, LicenseStateCode1, TaxonomySwitch1, " +
			"IsSoleProprietor, IsOrganizationSubpart, DeactivationDate) VALUES (" +
			entry.NPI + ", '" +
			entry.name + "', '" +
			entry.otherName + "', '" +
			entry.otherNameTypeCode + "', '" +
			entry.firstLineMailingAddress + "', '" +
			entry.secondLineMailingAddress + "', '" +
			entry.mailingAddressCity + "', '" +
			entry.mailingAddressState + "', '" +
			entry.mailingAddressPostalCode + "', '" +
			entry.mailingAddressCountryCode + "', '" +
			entry.mailingAddressTelephone + "', '" +
			entry.mailingAddressFax + "', '" +
			entry.firstLinePracticeAddress + "', '" +
			entry.secondLinePracticeAddress+ "', '" +
			entry.practiceAddressCity + "', '" +
			entry.practiceAddressState + "', '" +
			entry.practiceAddressPostalCode + "', '" +
			entry.practiceAddressCountryCode + "', '" +
			entry.practiceAddressTelephone + "', '" +
			entry.practiceAddressFax + "', '" +
			entry.authorizedOfficialLastName + "', '" +
			entry.authorizedOfficialFirstName + "', '" +
			entry.authorizedOfficialTitle + "', '" +
			entry.authorizedOfficialCredential + "', '" +
			entry.authorizedOfficialTelephone + "', '" +
			entry.taxonomyCode1 + "', '" +
			entry.LicenseNumber1 + "', '" +
			entry.LicenseStateCode1 + "', '" +
			entry.TaxonomySwitch1 + "', '" +
			entry.isSoleProprietor + "', '" +
			entry.isOrganizationSubpart + "', '" +
			entry.deactivationDate + "')";

			return command;
	}

	public string UpdateEntity(Entry entry){
		string command = "UPDATE " + tableName + " SET Name = "  + entry.name +
			", OtherName = " + entry.otherName +
			", OtherNameTypeCode = " + entry.otherNameTypeCode +
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
			", PracticeAddressFax = " + entry.practiceAddressFax +
			", AuthorizedOfficialLastName = " + entry.authorizedOfficialLastName +
			", AuthorizedOfficialFirstName = " + entry.authorizedOfficialFirstName +
			", AuthorizedOfficialTitle = " + entry.authorizedOfficialTitle +
			", AuthorizedOfficialCredential = " + entry.authorizedOfficialCredential +
			", AuthorizedOfficialTelephone = " + entry.authorizedOfficialTelephone +
			", TaxonomyCode1 = " + entry.taxonomyCode1 +
			", LicenseNumber1 = " + entry.LicenseNumber1 +
			", LicenseStateCode1 = " + entry.LicenseStateCode1 +
			", TaxonomySwitch1 = " + entry.TaxonomySwitch1 +
			", IsSoleProprietor = " + entry.isSoleProprietor +
			", IsOrganizationSubpart = " + entry.isOrganizationSubpart +
			", DeactivationDate = " + entry.deactivationDate +
			" WHERE NPI=" + entry.NPI +
			"";

		return command;
	}

	public string DeactivateEntity(string NPINumber){
		string command = "DELETE FROM " + tableName + " WHERE NPI = " + NPINumber + "";
		return command;
	}
}
