using System;
using System.Collections.Generic;

public class Entry
{
	public EntryType entryType;
    public readonly string NPI, firstLineMailingAddress, secondLineMailingAddress, mailingAddressCity, mailingAddressState, mailingAddressPostalCode, mailingAddressCountryCode,
        mailingAddressTelephone, mailingAddressFax, firstLinePracticeAddress, secondLinePracticeAddress, practiceAddressCity, practiceAddressState, practiceAddressPostalCode, 
        practiceAddressCountryCode, practiceAddressTelephone, practiceAddressFax, taxonomyCode1, LicenseNumber1, LicenseStateCode1, TaxonomySwitch1, isSoleProprietor, deactivationDate,
        providerLastName, providerFirstName, providerNamePrefix, providerNameSufix, providerCredentialText, name, otherName, otherNameTypeCode, authorizedOfficialLastName, 
        authorizedOfficialFirstName, authorizedOfficialTitle, authorizedOfficialCredential, authorizedOfficialTelephone, isOrganizationSubpart;

	public Entry (List<String> inValues)
	{
        List<String> values = inValues;

		//Gets the second value of input, the Entity type code
		if (string.IsNullOrEmpty (values [1])) {
			entryType = EntryType.Deactivate;
		} else {
			//Converts the recieved string input to an int, then casts to an EntityType
			entryType = (EntryType)Int16.Parse (values [1]);

            name = values[4];
            providerLastName = values[5];
            providerFirstName = values[6];
            providerNamePrefix = values[8];
            providerNameSufix = values[9];
            providerCredentialText = values[10];
            otherName = values[11];
            otherNameTypeCode = values[12];
            firstLineMailingAddress = values[20];
            secondLineMailingAddress = values[21];
            mailingAddressCity = values[22];
            mailingAddressState = values[23];
            mailingAddressPostalCode = values[24];
            mailingAddressCountryCode = values[25];
            mailingAddressTelephone = values[26];
            mailingAddressFax = values[27];
            firstLinePracticeAddress = values[28];
            secondLinePracticeAddress = values[29];
            practiceAddressCity = values[30];
            practiceAddressState = values[31];
            practiceAddressPostalCode = values[32];
            practiceAddressCountryCode = values[33];
            practiceAddressTelephone = values[34];
            practiceAddressFax = values[35];
            authorizedOfficialLastName = values[42];
            authorizedOfficialFirstName = values[43];
            authorizedOfficialTitle = values[45];
            authorizedOfficialTelephone = values[46];
            taxonomyCode1 = values[47];
            LicenseNumber1 = values[48];
            LicenseStateCode1 = values[49];
            TaxonomySwitch1 = values[50];
            isSoleProprietor = values[307];
            isOrganizationSubpart = values[308];
            authorizedOfficialCredential = values[313];
        }
        NPI = values[0];
        deactivationDate = values[39];
	}
}

public enum EntryType{
	Deactivate, 
	Provider = 1, 
	Organization = 2
}