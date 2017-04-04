//CSCI491 unit tests
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

[TestClass]
public class UnitTest1
{

    //set up a conncetion and a command for MySql
    MySqlCommand com = new MySqlCommand();

    //create an empty database for testing that is an exact copy but empty
    //"mysqldump -u username –p  -d database_name|mysql -u username -p new_database";

    //establish connection with test database
    MySqlConnection conn = new MySqlConnection("CONNECTION");
    //com.Connection = conn;
    OrganizationManager orgManager = new OrganizationManager("npi_organization_data");
    ProviderManager proManager = new ProviderManager("npi_provider_data");
    DeactivationManager deaManager = new DeactivationManager("npi_deactivated");

    Entry testEntryOrg = new Entry(new List<string>(new string[]{ "123456", "tester" }));
    Entry testEntryPro = new Entry(new List<string>(new string[] { "123456" }));
    //tests for add

    [TestMethod]
    public void testAddOrg()
    {
        orgManager.AddEntity(testEntryOrg);
        com.CommandText = "SELECT npi FROM organization WHERE npi == 123456";
        int result = int.Parse(com.ExecuteScalar().ToString());
        Assert.AreEqual(result, 123456, "testing add organizer to an empty db");
    }

    [TestMethod]
    public void testAddDupOrg()
    {
        orgManager.AddEntity(testEntryOrg);
        orgManager.AddEntity(testEntryOrg);
        com.CommandText = "SELECT COUNT(*) FROM organization";
        int result = int.Parse(com.ExecuteScalar().ToString());
        Assert.AreEqual(result, 1, "testing applying duplicate organization");
    }


    [TestMethod]
    public void testAddProv()
    {
        proManager.AddEntity(testEntryPro);
        com.CommandText = "SELECT npi FROM provider WHERE npi == 123456";
        int result = int.Parse(com.ExecuteScalar().ToString());
        Assert.AreEqual(result, 123456, "testing adding provider to an empty db");
    }

    [TestMethod]
    public void testAddDupProv()
    {
        proManager.AddEntity(testEntryPro);
        proManager.AddEntity(testEntryPro);
        com.CommandText = "SELECT COUNT(*) FROM provider";
        int result = int.Parse(com.ExecuteScalar().ToString());
        Assert.AreEqual(result, 1, "testing applying duplicate provider");
    }

    //tests for find

    [TestMethod]
    public void testFindOrg()
    {
        orgManager.AddEntity(testEntryOrg);
        Organization testOrg = new Organization();
        testOrg = find(123456, "Tester");
        Assert.AreEqual(testOrg.npi, 123456, "testing finding an organization on an empty db");
    }

    [TestMethod]
    public void testFindProv()
    {
        proManager.AddEntity(testEntryPro);
        Provider testProv = new Provider();
        testProv = find(123456);
        Assert.AreEqual(testProv.npi, 123456, "testing finding a Provider on an empty db");
    }


    //tests for update

    [TestMethod]
    public void testUpdate()
    {
        update();
        Provider testProv = new Provider();
        testProv = find(123456);
        Assert.AreEqual(testProv.npi, 123456, "testing applying an update file on an empty db");
    }


    //tests for deactivate

    [TestMethod]
    public void testDeactivate()
    {
        update();
        //use the same file that was used to update, should cause the table to be empty
        deactivate();
        com.CommandText = "SELECT COUNT(*) FROM organization";
        int result = int.Parse(com.ExecuteScalar().ToString());
        Assert.AreEqual(result, 0, "testing applying deactivate that should remove the only row causing it to be empty");
    }
}










