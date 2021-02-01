<?php
include_once "dbdata.php";
include 'functions.php';
// Script Name
define(SCRIPTNAME, "Mailing List");

// Your sites main URL
define(BASEHREF, "http://www.foxtailpineshoa.org");

// Your sites admin URL
define(ADMINHREF, "http://www.foxtailpineshoa.org");

// path to your base directory
define(BASEPATH, "/home/www/admin/");

// when emails sent this is what shows as the from email field
define(FROMNAME, "Foxtail Pines Hoa");

// email address to show as the from address when email is sent
define(FROMEMAIL, "mailings@foxtailpineshoa.org");

// email address to show as the from address when email is sent
define(LISTNAME, "Foxtail Pines Hoa Mailing List");

// insert unsubscribe link in all emails sent to your mailing list
// any value other than true will cause the link to NOT show
define(INSERTLINK, "true");
                                                   
// Connects to MySQL Database
$conn = mysql_connect($cssrm["mysql_dbhost"],$cssrm["mysql_dbuser"],$cssrm["mysql_dbpass"]) or die("Invalid server or user."); 	
mysql_select_db($cssrm["mysql_dbname"], $conn);

?>