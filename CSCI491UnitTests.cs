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
    MySqlConnection conn = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=test_database;");
    OrganizationManager orgManager = new OrganizationManager("npi_organization_data");
    ProviderManager proManager = new ProviderManager("npi_provider_data");
    DeactivationManager deaManager = new DeactivationManager("npi_deactivated");
    TableReader tableReader = new TableReader("datasource=127.0.0.1;port=3306;username=root;password=;database=test_database;");
    //Entry testEntry = new Entry(new List<string>(new string[]{"123456", "1"}));
    Entry testOrgEntry;
    Entry testProvEntry;
    Entry testDeaEntry;
    string updateFileLoc = "../../TestFiles/UpdateTestSnippit1.csv";
    string deactivateFileLoc = "../../TestFiles/UpdateTestSnippit1.csv";
    //tests for add
    public UnitTest1()
    {
        List<string> list = new List<string>();
        list.Add("123456");
        for(int i=0; i < 313; i++)
        {
            list.Add("0");
        }
        testDeaEntry = new Entry(list);
        list.Insert(1, "2");
        testOrgEntry = new Entry(list);
        list.Insert(1, "1");
        testProvEntry = new Entry(list);
        initConn.Open();
        //testCom.Connection = initConn;
        //testCom = new MySqlCommand("mysqldump -u root –p -d nppes_1 | mysql -u root -p test_database;", initConn);
        //testCom.ExecuteNonQuery();
        initConn.Close();
        conn = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=test_database;");
        com.Connection = conn;
    }
    [TestMethod]
    public void testAddOrg()
    {
        clearDatabase();
        conn.Open();
        testCom =  new MySqlCommand(orgManager.AddEntity(testOrgEntry), conn);
        testCom.ExecuteNonQuery();
        com.CommandText = "SELECT npi FROM npi_organization_data WHERE npi = 123456";
        int result = int.Parse(com.ExecuteScalar().ToString());
        conn.Close();
        Assert.AreEqual(123456, result, "testing add organizer to an empty db");
    }

    [TestMethod]
    public void testAddDupOrg()
    {
        clearDatabase();
        conn.Open();
        testCom = new MySqlCommand(orgManager.AddEntity(testOrgEntry), conn);
        testCom.ExecuteNonQuery();
        try
        {
            testCom.ExecuteNonQuery();
        }
        catch (MySqlException e)
        {
            conn.Close();
            Assert.AreEqual(1062, e.Number, "adding a duplicate entry should fail with error code 1062");
        }
    }


    [TestMethod]
    public void testAddProv()
    {
        clearDatabase();
        conn.Open();
        testCom = new MySqlCommand(proManager.AddEntity(testProvEntry),conn);
        testCom.ExecuteNonQuery();
        com.CommandText = "SELECT npi FROM npi_provider_data WHERE npi = 123456";
        int result = int.Parse(com.ExecuteScalar().ToString());
        conn.Close();
        Assert.AreEqual(123456, result, "testing adding provider to an empty db");
    }

    [TestMethod]
    public void testAddDupProv()
    {
        clearDatabase();
        conn.Open();
        testCom = new MySqlCommand(proManager.AddEntity(testProvEntry), conn);
        testCom.ExecuteNonQuery();
        try
        {
            testCom.ExecuteNonQuery();
        }
        catch (MySqlException e)
        {
            conn.Close();
            Assert.AreEqual(1062, e.Number, "adding a duplicate entry should fail with error code 1062");
        }
    }

    //tests for find

    [TestMethod]
    public void testFindOrg()
    {
        clearDatabase();
        conn.Open();
        testCom = new MySqlCommand(orgManager.AddEntity(testOrgEntry), conn);
        testCom.ExecuteNonQuery();
        testCom = new MySqlCommand(orgManager.FindExisting("123456"), conn);
        int result = int.Parse(testCom.ExecuteScalar().ToString());
        conn.Close();
        Assert.AreEqual(123456, result, "testing adding andfinding an organization on an empty db");
    }

    [TestMethod]
    public void testFindProv()
    {
        clearDatabase();
        conn.Open();
        testCom = new MySqlCommand(proManager.AddEntity(testProvEntry), conn);
        testCom.ExecuteNonQuery();
        testCom = new MySqlCommand(proManager.FindExisting("123456"), conn);
        int result = int.Parse(testCom.ExecuteScalar().ToString());
        conn.Close();
        Assert.AreEqual(123456, result, "testing adding andfinding an organization on an empty db");
    }

    [TestMethod]
    public void testFindDeavctivate()
    {
        clearDatabase();
        conn.Open();
        testCom = new MySqlCommand(deaManager.AddEntity(testDeaEntry), conn);
        testCom.ExecuteNonQuery();
        testCom = new MySqlCommand(deaManager.FindExisting("123456"), conn);
        int result = int.Parse(testCom.ExecuteScalar().ToString());
        conn.Close();
        Assert.AreEqual(123456, result, "testing adding and finding an organization on an empty db");
    }


    //tests for update

    [TestMethod]
    public void testUpdate()
    {
        clearDatabase();
        tableReader.readUpdateFile(updateFileLoc);
        conn.Open();
        testCom = new MySqlCommand(proManager.FindExisting("1275857534"), conn);
        string result = testCom.ExecuteScalar().ToString();
        conn.Close();
        Assert.AreEqual("1275857534", result, "testing applying an update file on an empty db");
    }


    //tests for deactivate

    [TestMethod]
    public void testDeactivate()
    {
        clearDatabase();
        tableReader.readUpdateFile(updateFileLoc);
        //use the same file that was used to update, should cause the table to be empty
        tableReader.readDeactivationFile(deactivateFileLoc);
        conn.Open();
        com.CommandText = "SELECT COUNT(*) FROM npi_organization_data";
        int result = int.Parse(com.ExecuteScalar().ToString());
        conn.Close();
        Assert.AreEqual(0, result, "testing applying deactivate that should remove the only row causing it to be empty");
    }

    [TestMethod]
    public void testRemoveFromDeavctivate()
    {
        clearDatabase();
        conn.Open();
        testCom = new MySqlCommand(deaManager.AddEntity(testDeaEntry), conn);
        testCom.ExecuteNonQuery();
        testCom = new MySqlCommand(deaManager.DeactivateEntity("123456"), conn);
        testCom.ExecuteNonQuery();
        com.CommandText = "SELECT COUNT(*) FROM npi_deactivated";
        int result = int.Parse(com.ExecuteScalar().ToString());
        conn.Close();
        Assert.AreEqual(0, result, "testing removing entity from deactivated table");
    }

    private void clearDatabase()
    {
        conn.Open();
        testCom = new MySqlCommand("truncate npi_organization_data",conn);
        testCom.ExecuteNonQuery();
        testCom = new MySqlCommand("truncate npi_provider_data",conn);
        testCom.ExecuteNonQuery();
        testCom = new MySqlCommand("truncate npi_deactivated",conn);
        testCom.ExecuteNonQuery();
        conn.Close();
    }
}
