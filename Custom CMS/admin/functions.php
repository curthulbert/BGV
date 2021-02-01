<?php
function insert_mail() {
	$fname = $_POST['fname'];
	$lname = $_POST['lname'];
	$email = $_POST['email'];
	$fullname = $fname . " " . $lname;
	$sql2="select * from members where email='$email'";
	$result2=mysql_query($sql2) or die("select  fails");
	$no=mysql_num_rows($result2);
	if ($no==0) {
		$sql = "insert into members(fullname,email) values(NULL,'$fullname','$email')";
		$result = mysql_query($sql) or die("insert fails");
		echo "Email added to list: " . LISTNAME;
	} else {
		echo "Email Address Already Exists in List: " . LISTNAME;
	}
}

function delete_mail() {
    $email = $_POST['email'];
    if ($email == "") {
       $email = $_GET['email'];
    }
    $sql2="select * from members where email='$email'";
    $result2=mysql_query($sql2) or die("select  fails");
    $no=mysql_num_rows($result2);
    if ($no==0) {
       echo "Your email was not found in the list: " . LISTNAME;
    } else {
       echo "Your email was unsubscribed from the list: " . LISTNAME;
    }
    $sql2="delete from members where email='$email'";
    $result2=mysql_query($sql2) or die("unsubscribe failed, please try again");
}
?>