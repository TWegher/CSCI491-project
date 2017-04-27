using System;
using System.IO;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

public class TableReader
{
    //Initialize the connection
    string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=nppes_1;";

    //MySqlDataReader reader;
    MySqlConnection databaseConnection;
    MySqlCommand command = new MySqlCommand();

    OrganizationManager orgManager;
    ProviderManager proManager;
    DeactivationManager deaManager;

    public TableReader(string connectionString)
    {
        if (!string.IsNullOrEmpty(connectionString))
        {
            this.connectionString = connectionString;
        }
        databaseConnection = new MySqlConnection(this.connectionString);
        orgManager = new OrganizationManager("npi_organization_data");
        proManager = new ProviderManager("npi_provider_data");
        deaManager = new DeactivationManager("npi_deactivated");
    }

    private void readUpdateFile(string fileLocation)
    {
        List<Entry> entries = generateEntries(fileLocation);

        foreach (Entry curEntry in entries)
        {
            switch (curEntry.entryType)
            {
                case EntryType.Provider:
                    {
                        updateTable(proManager, curEntry);
                        break;
                    }
                case EntryType.Organization:
                    {
                        updateTable(orgManager, curEntry);
                        break;
                    }

                case EntryType.Deactivate:
                    {
                        deactivateEntity(curEntry);
                        break;
                    }
            }
        }
    }

    private void readDeactivationFile(string fileLocation)
    {
        List<Entry> entries = generateEntries(fileLocation);

        //As each entry in the deactivation list is know to be of EntryType.Deactivate, a switch check is unneccesary
        foreach (Entry curEntry in entries)
        {
            deactivateEntity(curEntry);
        }
    }

    //Populates the database from the full file.  Should be run after the applySchema has been run.
    private void readFullFile(string fileLocation)
    {
        List<Entry> entries = generateEntries(fileLocation);

        foreach (Entry curEntry in entries)
        {
            switch (curEntry.entryType)
            {
                case EntryType.Provider:
                    {
                        addToTable(proManager, curEntry);
                        break;
                    }
                case EntryType.Organization:
                    {
                        addToTable(orgManager, curEntry);
                        break;
                    }
                case EntryType.Deactivate:
                    {
                        break;
                    }
            }
        }
    }

    private void updateTable(IDataManager tableManager, Entry entry)
    {
        //If the database was not able to update the entity, attempt to add it instead
        if (!tryCommand(tableManager.UpdateEntity(entry)))
        {
            tryCommand(tableManager.AddEntity(entry));
        }
    }

    private void addToTable(IDataManager tableManager, Entry entry)
    {
        tryCommand(tableManager.AddEntity(entry));
    }

    private List<Entry> generateEntries(string fileLocation)
    {
        //Initialize the StreamReader
        FileStream fs = File.OpenRead(fileLocation);
        StreamReader sr = new StreamReader(fs);

        //Reads in the CSV update file, line by line
        List<Entry> entries = new List<Entry>();
        while (!sr.EndOfStream)
        {
            //TODO: When generating entries for deactivation files, Determine and utilize the applicable stop date
            string line = sr.ReadLine();
            Entry entry = new Entry(new List<string>(line.Split(',')));
            entries.Add(entry);
        }

        return entries;
    }

    private void deactivateEntity(Entry entry)
    {

        if (!tryCommand(deaManager.UpdateEntity(entry)))
        {
            tryCommand(deaManager.AddEntity(entry));
        }

        tryCommand(proManager.DeactivateEntity(entry.NPI));
        tryCommand(orgManager.DeactivateEntity(entry.NPI));
    }

    private bool tryCommand(string query)
    {

        //Stores the number of effected rows by the executed command
        int numMatched;

        command = new MySqlCommand(query, databaseConnection);
        command.CommandTimeout = 30;

        try
        {
            databaseConnection.Open();
            //reader = command.ExecuteReader();
            numMatched = command.ExecuteNonQuery();

            databaseConnection.Close();
        }
        catch (Exception ex)
        {
            databaseConnection.Close();
            return false;
        }

        //Only one row should ever be effected by a given command, as NPI is the primary key and passed as the argument
        if (numMatched == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Deletes rows that don't belong in their respective tables, ie. rows with provider data don't belong in the organization table.
    private void removeDuplicates()
    {
        MySqlCommand commandProvider = new MySqlCommand();
        MySqlCommand commandOrganization = new MySqlCommand();

        string providerQuery = "DELETE FROM npi_provider_data WHERE ProviderLastName = '';";
        string organizationQuery = "DELETE FROM npi_organization_data WHERE Name = '';";

        commandProvider = new MySqlQuery(providerQuery, databaseConnection);
        commandProvider.CommandTimeout = Int32.MaxValue;

        commandOrganization = new MySqlQuery(organizationQuery, databaseConnection);
        commandOrganization.CommandTimeout = Int32.MaxValue;

        try
        {
            databaseConnection.Open();
            commandProvider.ExecuteNonQuery();
            commandOrganization.ExecuteNonQuery();

            databaseConnection.Close();
        }
        catch (Exception ex)
        {
            databaseConnection.Close();
        }
    }

    //Deletes the entire databse and reapplies the schema.
    private void applySchema()
    {
        string deleteDatabase = "DROP DATABASE nppes_1";
        string schema = "CREATE DATABASE  IF NOT EXISTS `nppes_1` /*!40100 DEFAULT CHARACTER SET utf8 COLLATE utf8_unicode_ci */;" +
            "\nUSE `nppes_1`;\n-- MySQL dump 10.13  Distrib 5.7.9, for Win64 (x86_64)\n--\n-- Host: 192.168.1.151    Database: nppe" +
            "s\n-- ------------------------------------------------------\n-- Server version\t5.6.26-log\n\n/*!40101 SET @OLD_CHARA" +
            "CTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;\n/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;\n/*!40" +
            "101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;\n/*!40101 SET NAMES utf8 */;\n/*!40103 SET @OLD_TIME_ZONE" +
            "=@@TIME_ZONE */;\n/*!40103 SET TIME_ZONE='+00:00' */;\n/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=" +
            "0 */;\n/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;\n/*!40101 SET @OLD_SQL_MODE" +
            "=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;\n/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;\n\n--\n-- " +
            "Table structure for table `npi_organization_data`\n--\n\nDROP TABLE IF EXISTS `npi_organization_data`;\n/*!40101 SET @" +
            "saved_cs_client     = @@character_set_client */;\n/*!40101 SET character_set_client = utf8 */;\nCREATE TABLE `npi_organ" +
            "ization_data` (\n  `NPI` int(10) unsigned NOT NULL,\n  `Name` varchar(70) COLLATE utf8_unicode_ci NOT NULL,\n  `OtherNa" +
            "me` varchar(70) COLLATE utf8_unicode_ci DEFAULT NULL,\n  `OtherNameTypeCode` varchar(1) COLLATE utf8_unicode_ci DEFAULT" +
            " NULL,\n  `FirstLineMailingAddress` varchar(55) COLLATE utf8_unicode_ci DEFAULT NULL,\n  `SecondLineMailingAddress` var" +
            "char(55) COLLATE utf8_unicode_ci DEFAULT NULL,\n  `MailingAddressCity` varchar(40) COLLATE utf8_unicode_ci DEFAULT NULL," +
            "\n  `MailingAddressState` varchar(40) COLLATE utf8_unicode_ci DEFAULT NULL,\n  `MailingAddressPostalCode` varchar(20) CO" +
            "LLATE utf8_unicode_ci DEFAULT NULL,\n  `MailingAddressCountryCode` varchar(2) COLLATE utf8_unicode_ci DEFAULT NULL,\n  " +
            "`MailingAddressTelephone` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,\n  `MailingAddressFax` varchar(20) COLLATE u" +
            "tf8_unicode_ci DEFAULT NULL,\n  `FirstLinePracticeAddress` varchar(55) COLLATE utf8_unicode_ci DEFAULT NULL,\n  `SecondLi" +
            "nePracticeAddress` varchar(55) COLLATE utf8_unicode_ci DEFAULT NULL,\n  `PracticeAddressCity` varchar(40) COLLATE utf8_un" +
            "icode_ci DEFAULT NULL,\n  `PracticeAddressState` varchar(40) COLLATE utf8_unicode_ci DEFAULT NULL,\n  `PracticeAddressP" +
            "ostalCode` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,\n  `PracticeAddressCountryCode` varchar(2) COLLATE utf8_u" +
            "nicode_ci DEFAULT NULL,\n  `PracticeAddressTelephone` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,\n  `PracticeAdd" +
            "ressFax` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,\n  `AuthorizedOfficialLastName` varchar(35) COLLATE utf8_un" +
            "icode_ci DEFAULT NULL,\n  `AuthorizedOfficialFirstName` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,\n  `Authoriz" +
            "edOfficialTitle` varchar(35) COLLATE utf8_unicode_ci DEFAULT NULL,\n  `AuthorizedOfficialCredential` varchar(20) COLLA" +
            "TE utf8_unicode_ci DEFAULT NULL,\n  `AuthorizedOfficialTelephone` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,\n " +
            " `TaxonomyCode1` varchar(10) COLLATE utf8_unicode_ci DEFAULT NULL,\n  `LicenseNumber1` varchar(20) COLLATE utf8_unicode" +
            "_ci DEFAULT NULL,\n  `LicenseStateCode1` varchar(2) COLLATE utf8_unicode_ci DEFAULT NULL,\n  `TaxonomySwitch1` varcha" +
            "r(1) COLLATE utf8_unicode_ci DEFAULT NULL,\n  `IsSoleProprietor` varchar(1) COLLATE utf8_unicode_ci DEFAULT NULL,\n  " +
            "`IsOrganizationSubpart` varchar(1) COLLATE utf8_unicode_ci DEFAULT NULL,\n  `DeactivationDate` datetime DEFAULT NULL,\n" + 
            "  PRIMARY KEY (`NPI`)\n) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT;\n/*!40101 SET" +
            " character_set_client = @saved_cs_client */;\n\n--\n-- Table structure for table `npi_provider_data`\n--\n\nDROP TABLE" +
            " IF EXISTS `npi_provider_data`;\n/*!40101 SET @saved_cs_client     = @@character_set_client */;\n/*!40101 SET character" +
            "_set_client = utf8 */;\nCREATE TABLE `npi_provider_data` (\n  `NPI` int(10) unsigned NOT NULL,\n  `ProviderLastName` va" +
            "rchar(45) COLLATE utf8_unicode_ci DEFAULT NULL,\n  `ProviderFirstName` varchar(45) COLLATE utf8_unicode_ci DEFAULT NULL," +
            "\n  `ProviderNamePrefix` varchar(5) COLLATE utf8_unicode_ci DEFAULT NULL,\n  `ProviderNameSuffix` varchar(5) COLLATE ut" +
            "f8_unicode_ci DEFAULT NULL,\n  `ProviderCredentialText` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,\n  `FirstLin" +
            "eMailingAddress` varchar(55) COLLATE utf8_unicode_ci DEFAULT NULL,\n  `SecondLineMailingAddress` varchar(55) COLLATE ut" +
            "f8_unicode_ci DEFAULT NULL,\n  `MailingAddressCity` varchar(45) COLLATE utf8_unicode_ci DEFAULT NULL,\n  `MailingAddre" +
            "ssState` varchar(45) COLLATE utf8_unicode_ci DEFAULT NULL,\n  `MailingAddressPostalCode` varchar(20) COLLATE utf8_uni" +
            "code_ci DEFAULT NULL,\n  `MailingAddressCountryCode` varchar(2) COLLATE utf8_unicode_ci DEFAULT NULL,\n  `MailingAddr" +
            "essTelephone` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,\n  `MailingAddressFax` varchar(20) COLLATE utf8_unic" +
            "ode_ci DEFAULT NULL,\n  `FirstLinePracticeAddress` varchar(55) COLLATE utf8_unicode_ci DEFAULT NULL,\n  `SecondLineP" +
            "racticeAddress` varchar(55) COLLATE utf8_unicode_ci DEFAULT NULL,\n  `PracticeAddressCity` varchar(45) COLLATE utf8" +
            "_unicode_ci DEFAULT NULL,\n  `PracticeAddressState` varchar(40) COLLATE utf8_unicode_ci DEFAULT NULL,\n  `PracticeA" +
            "ddressPostalCode` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,\n  `PracticeAddressCountryCode` varchar(2) COL" +
            "LATE utf8_unicode_ci DEFAULT NULL,\n  `PracticeAddressTelephone` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL," +
            "\n  `PracticeAddressFaxNumber` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,\n  `TaxonomyCode1` varchar(10)" +
            " COLLATE utf8_unicode_ci DEFAULT NULL,\n  `LicenseNumber1` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,\n  " +
            "`LicenseStateCode1` varchar(2) COLLATE utf8_unicode_ci DEFAULT NULL,\n  `TaxonomySwitch1` varchar(1) COLLATE utf8_u" +
            "nicode_ci DEFAULT NULL,\n  `IsSoleProprietor` varchar(1) COLLATE utf8_unicode_ci DEFAULT NULL,\n  `DeactivationDate`" +
            " datetime DEFAULT NULL,\n  PRIMARY KEY (`NPI`)\n) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;\n/*!401" +
            "01 SET character_set_client = @saved_cs_client */;\n\n--\n-- Table structure for table `npi_deactivated`\n--\n\nDRO" +
            "P TABLE IF EXISTS `npi_deactivated`;\n/*!40101 SET @saved_cs_client     = @@character_set_client */;\n/*!40101 SET cha" +
            "racter_set_client = utf8 */;\nCREATE TABLE `npi_deactivated` (\n  `NPI` int(10) unsigned NOT NULL,\n  `DeactivationD" +
            "ate` datetime DEFAULT NULL,\n  PRIMARY KEY (`NPI`)\n) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;\n/*!" +
            "40101 SET character_set_client = @saved_cs_client */;\n\n\n/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;\n\n/*!40101 SET " +
            "SQL_MODE=@OLD_SQL_MODE */;\n/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;\n/*!40014 SET UNIQUE_CHECKS=" +
            "@OLD_UNIQUE_CHECKS */;\n/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;\n/*!40101 SET CHARACTER_SET_RE" +
            "SULTS=@OLD_CHARACTER_SET_RESULTS */;\n/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;\n/*!40111 SET SQ" +
            "L_NOTES=@OLD_SQL_NOTES */;";

        MySqlCommand commandDump = new MySqlCommand();
        commandDump = new MySqlQuery(deleteDatabase, databaseConnection);
        commandDump.CommandTimeout = Int32.MaxValue;

        command = new MySqlQuery(schema, databaseConnection);
        command.CommandTimeout = Int32.MaxValue;

        try
        {
            databaseConnection.Open();
            commandDump.ExecuteNonQuery();
            command.ExecuteNonQuery();
            databaseConnection.Close();
        }
        catch (Exception ex)
        {
            databaseConnection.Close();
        }
    }
}
