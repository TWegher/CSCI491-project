//CSCI491 unit tests
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

[TestClass]
public class UnitTest1
{

    //set up a conncetion and a command for MySql
    MySqlCommand com = new MySqlCommand();
    MySqlCommand testCom;

    //create an empty database for testing that is an exact copy but empty
    //"mysqldump -u username –p  -d nppes_1|mysql -u username -p test_database";

    //establish connection with test database
    MySqlConnection initConn = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=nppes_1;");
    MySqlConnection conn;
    OrganizationManager orgManager = new OrganizationManager("npi_organization_data");
    ProviderManager proManager = new ProviderManager("npi_provider_data");
    DeactivationManager deaManager = new DeactivationManager("npi_deactivated");
    TableReader tableReader = new TableReader("datasource=127.0.0.1;port=3306;username=root;password=;database=test_database;");
    Entry testEntry = new Entry(new List<string>(new string[]{"123456"}));
    string updateFileLoc = "";
    string deactivateFileLoc = "";
    //tests for add
    public UnitTest1()
    {
        initConn.Open();
        testCom = new MySqlCommand("mysqldump -u root –p  -d nppes_1|mysql -u root -p test_database;", initConn);
        testCom.ExecuteNonQuery();
        initConn.Close();
        conn = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=test_database;");
        com.Connection = conn;
    }
    [TestMethod]
    public void testAddOrg()
    {
        conn.Open();
        testCom =  new MySqlCommand(orgManager.AddEntity(testEntry), conn);
        testCom.ExecuteNonQuery();
        com.CommandText = "SELECT npi FROM organization WHERE npi == 123456";
        int result = int.Parse(com.ExecuteScalar().ToString());
        conn.Close();
        Assert.AreEqual(result, 123456, "testing add organizer to an empty db");
    }

    [TestMethod]
    public void testAddDupOrg()
    {
        conn.Open();
        testCom = new MySqlCommand(orgManager.AddEntity(testEntry), conn);
        testCom.ExecuteNonQuery();
        testCom.ExecuteNonQuery();
        com.CommandText = "SELECT COUNT(*) FROM organization";
        int result = int.Parse(com.ExecuteScalar().ToString());
        conn.Close();
        Assert.AreEqual(result, 1, "testing applying duplicate organization");
    }


    [TestMethod]
    public void testAddProv()
    {
        conn.Open();
        testCom = new MySqlCommand(proManager.AddEntity(testEntry),conn);
        testCom.ExecuteNonQuery();
        com.CommandText = "SELECT npi FROM provider WHERE npi == 123456";
        int result = int.Parse(com.ExecuteScalar().ToString());
        conn.Close();
        Assert.AreEqual(result, 123456, "testing adding provider to an empty db");
    }

    [TestMethod]
    public void testAddDupProv()
    {
        conn.Open();
        testCom = new MySqlCommand(proManager.AddEntity(testEntry), conn);
        testCom.ExecuteNonQuery();
        testCom.ExecuteNonQuery();
        com.CommandText = "SELECT COUNT(*) FROM provider";
        int result = int.Parse(com.ExecuteScalar().ToString());
        conn.Close();
        Assert.AreEqual(result, 1, "testing applying duplicate provider");
    }

    //tests for find

    [TestMethod]
    public void testFindOrg()
    {
        conn.Open();
        testCom = new MySqlCommand(orgManager.AddEntity(testEntry), conn);
        testCom.ExecuteNonQuery();
        testCom = new MySqlCommand(orgManager.FindExisting("123456"), conn);
        int result = int.Parse(testCom.ExecuteScalar().ToString());
        conn.Close();
        Assert.AreEqual(result, 123456, "testing finding an organization on an empty db");
    }

    [TestMethod]
    public void testFindProv()
    {
        conn.Open();
        testCom = new MySqlCommand(proManager.AddEntity(testEntry), conn);
        testCom.ExecuteNonQuery();
        testCom = new MySqlCommand(proManager.FindExisting("123456"), conn);
        int result = int.Parse(testCom.ExecuteScalar().ToString());
        conn.Close();
        Assert.AreEqual(result, 123456, "testing finding an organization on an empty db");
    }


    //tests for update

    [TestMethod]
    public void testUpdate()
    {
        tableReader.readUpdateFile(updateFileLoc);
        conn.Open();
        testCom = new MySqlCommand(proManager.FindExisting("123456"), conn);
        int result = int.Parse(testCom.ExecuteScalar().ToString());
        conn.Close();
        Assert.AreEqual(result, 123456, "testing applying an update file on an empty db");
    }


    //tests for deactivate

    [TestMethod]
    public void testDeactivate()
    {
        tableReader.readUpdateFile(updateFileLoc);
        //use the same file that was used to update, should cause the table to be empty
        tableReader.readDeactivateFile(updateFileLoc);
        conn.Open();
        com.CommandText = "SELECT COUNT(*) FROM organization";
        int result = int.Parse(com.ExecuteScalar().ToString());
        conn.Close();
        Assert.AreEqual(result, 0, "testing applying deactivate that should remove the only row causing it to be empty");
    }
}










