<?php
	// Import config file
	include("config.cfg");
	
	// Get the file name.
	$name = $_GET["name"];
	if ($name == null || $name == "")
		die("name required.");
	
	// Connect
	$conn = mysql_connect(DB_HOST, DB_USER, DB_PASS);
	if (!$conn)
	{
		die("Could not connect to the database: " . mysql_error());
	}
	
	// Set the database
	mysql_select_db(DB_NAME);
	
	// Create the query
	$sql = "SELECT lsb FROM lisimba where name='" . $name . "';";
	
	// Execute the query
	$rs = mysql_query($sql);
	
	if (!$rs)
		die("Error: " . mysql_error());
	
	if ($row = mysql_fetch_array($rs, MYSQL_NUM))
	{
		header("content-type: text/xml");
		echo $row[0];
	}

	// Free the result
	mysql_free_result($rs);
	
	// Disconnect
	mysql_close($conn);
?>