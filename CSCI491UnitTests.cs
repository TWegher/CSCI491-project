//CSCI491 unit tests

@Before
//set up a conncetion and a command for MySql
MySql.Data.MySqlClient.MySqlCommand com = new MySql.Data.MySqlClient.MySqlCommand();

//create an empty database for testing that is an exact copy but empty
mysqldump -u username –p  -d database_name|mysql -u username -p new_database

//establish connection with test database
conn = new MySql.Data.MySqlClient.MySqlConnection("CONNECTION");
com.Connection = conn;


//tests for add

@Test 
public void testAddOrg()
{
	add(123456, "tester");
	com.CommandText = "SELECT npi FROM organization WHERE npi == 123456";
	int result = int.Parse(com.ExecuteScalar().ToString());
	assertEqual(result, 123456, "testing add organizer to an empty db");
}

@Test
public void testAddDupOrg()
{
	add(123456, "tester");
	add(123456, "tester");
	com.CommandText = "SELECT COUNT(*) FROM organization";
	int result = int.Parse(com.ExecuteScalar().ToString());
	assertEqual(result,1, "testing applying duplicate organization");
}


@Test
public void testAddProv()
{
	add(123456);
	com.CommandText = "SELECT npi FROM provider WHERE npi == 123456";
	int result = int.Parse(com.ExecuteScalar().ToString());
	assertEqual(result, 123456, "testing adding provider to an empty db");
}

@Test
public void testAddDupProv()
{
	add(123456);
	add(123456);
	com.CommandText = "SELECT COUNT(*) FROM provider";
	int result = int.Parse(com.ExecuteScalar().ToString());
	assertEqual(result,1, "testing applying duplicate provider");
}

//tests for find

@Test
public void testFindOrg()
{
	add(123456,"Tester");
	Organization testOrg = new Organization();
	testOrg = find(123456, "Tester");
	assertEqual(testOrg.npi, 123456, "testing finding an organization on an empty db");
}

@Test
public void testFindProv()
{
	add(123456);
	Provider testProv = new Provider();
	testProv = find(123456);
	assertEqual(testProv.npi, 123456, "testing finding a Provider on an empty db");
}


//tests for update

@Test
public void testUpdate()
{
	update();
	Provider testProv = new Provider();
	testProv = find(123456);
	assertEqual(testProv.npi, 123456, "testing applying an update file on an empty db");
}


//tests for deactivate

@Test 
public void testUpdate()
{
	update();
	//use the same file that was used to update, should cause the table to be empty
	deactivate();
	com.CommandText = "SELECT COUNT(*) FROM organization";
	int result = int.Parse(com.ExecuteScalar().ToString());
	assertEqual(result,0, "testing applying deactivate that should remove the only row causing it to be empty");
}










