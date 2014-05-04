<?php
	// ========================================================================
	// Functions
	// ========================================================================

	function FileExists($name)
	{
		// Create the sql string
		$sql = "SELECT name FROM lisimba where name='" . $name . "';";
		
		// Execute the query
		$rs = mysql_query($sql);
		
		if (!$rs)
			die("Error: " . mysql_error());
		
		$count = mysql_num_rows($rs);
		return $count;
	}

	// ========================================================================
	// Functions
	// ========================================================================
	
	// Import config file
	include("config.cfg");
	
	// Get the file name.
	$name = $_POST["name"];
	if ($name == null || $name == "")
		die("name required.");
		
	// Get the content.
	$lsb = $_POST["content"];
	if ($lsb == null || $lsb == "")
		die("content required.");
	
	// Connect
	$conn = mysql_connect(DB_HOST, DB_USER, DB_PASS);
	if (!$conn)
	{
		die("Could not connect to the database: " . mysql_error());
	}
	
	// Set the database
	mysql_select_db(DB_NAME);
	
	// Create the sql string
	$sql = "";
	if (FileExists($name))
	{
		$sql = "update lisimba set lsb='" . $lsb . "' where name='" . $name . "';";
	}
	else
	{
		$sql = "insert into lisimba(name, lsb) values('" . $name . "', '" . $lsb . "');";
	}
	
	//die ($sql);
	
	// Execute the sql string
	$rs = mysql_query($sql);
	
	if (!$rs)
		die("Error: " . mysql_error());
	
	// Free the result
	//mysql_free_result($rs);
	
	// Disconnect
	mysql_close($conn);
?>