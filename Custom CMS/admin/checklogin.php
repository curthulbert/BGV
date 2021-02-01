<?php

ob_start();
include_once "dbdata.php";
$tbl_name="members"; // Table name 

// Connect to server and select databse.
mysql_connect($cssrm["mysql_dbhost"], $cssrm["mysql_dbuser"], $cssrm["mysql_dbpass"]) or die("Invalid server or user."); 
mysql_select_db($cssrm["mysql_dbname"])or die("cannot select DB");

// Define $myusername and $mypassword 
$myusername=$_POST['myusername']; 
$mypassword=$_POST['mypassword']; 

// To protect MySQL injection (more detail about MySQL injection)
$myusername = stripslashes($myusername);
$mypassword = stripslashes($mypassword);
$myusername = mysql_real_escape_string($myusername);
$mypassword = mysql_real_escape_string($mypassword);
$sql="SELECT * FROM $tbl_name WHERE username='$myusername' and password='$mypassword'";
$result=mysql_query($sql);
$count=mysql_num_rows($result);
//Test for result count
if($count==1){
	$nt=mysql_fetch_array($result);
	$typeid = $nt[membertypeid];
	$memid = $nt[id];
	$lastlogin = $nt[currentlogindate];
	//Find usertypeid and test for member or anonymous users
	if($typeid == 2){	
		session_register("myusername");
		session_register("mypassword");
		$_SESSION['currentdate'] = date("m.d.y");
		//set last date in admin section for admins
		mysql_connect($cssrm["mysql_dbhost"],$cssrm["mysql_dbuser"],$cssrm["mysql_dbpass"]) or die("Invalid server or user.");			
		mysql_select_db($cssrm["mysql_dbname"]);
		$result34 = mysql_query("UPDATE members SET lastlogindate = '".$lastlogin."', currentlogindate = '".date("y.m.d")."' WHERE id = ".$memid) or die(mysql_error());
		mysql_close();
		header("location:index2.php");
	}else {
		$errmsg = "You are not authorized to acces this section";
		include("top.php");
	}// End of find usertypeid and test for member or anonymous users
}else {
	$errmsg = "Wrong Username or Password";
	include("top.php");
}// End of test for result count
?>
    <tr>
        <td>
            <table cellpadding="0" cellspacing="0" border="0" width="715">
                <tr>
                    <td class="innercontent" valign="top">
                        <table cellpadding="0" cellspacing="10" border="0">
                            <tr>
                                <td valign="top">
                                    <table cellpadding="0" cellspacing="0" border="0" width="690">
                                        <tr>
                                            <td><h3>Foxtail Pines - Login</h3><div align="right"><a href="/admin/index2.php">Admin Home Page</a>&nbsp;-&nbsp;<a href="logout.php">Log out</a></div></td>
                                        </tr>
                                        <tr>
                                            <td><hr /></td>							        	
										</tr>	
                                        <tr>
                                            <td>
                                                <?php
												    // Throw error message            
                                                    echo $errmsg;
                                                    ob_end_flush();
                                                ?>
                                                <br /><br />
                                                <form>
                                                    <input type=button value="Try Again" onCLick="history.back()">
                                                </form>
                                            </td>
                                        </tr>
                                    </table>
								</td>					
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</td>
	</tr>
<?php 
include("bottom.php");
?>