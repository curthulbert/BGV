<?php
session_start();
//Check for login session
if(!session_is_registered(myusername)){
	header("location:index.php");
}else{
	//Check for buttton action
	if(isset($_POST['add'])){
		ob_start();
		header("location:aboutus3.php");
		ob_flush();
	}else if(isset($_POST['finished'])){
		ob_start();
		header("location:index2.php");
		ob_flush();	
	}else{
		include("top.php");		
	}
}
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
                                            <td><h3>Foxtail Pines - About Us PDFs</h3><div align="right"><a href="/admin/index2.php">Admin Home Page</a>&nbsp;-&nbsp;<a href="logout.php">Log out</a></div></td>
                                        </tr>
                                        <tr>
                                            <td><hr /></td>
                                        </tr>	
                                        <tr>
                                            <td>
                                                <form action="aboutus.php" method="post">
                                                <table>
                                                    <tr>
                                                        <td colspan="2" align="center" style="color: red;">
                                                            <label>
                                                                <?php echo $errmsg ?>&nbsp;
                                                            </label><br />
                                                            <input name="infoid" type="hidden" value="<?php echo $row['sectioninfopdfid'] ?>" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="center"><strong>Choose an item below to edit or delete.</strong></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">&nbsp;</td>
                                                    </tr>
                                                    <?php 
                                                    include_once "dbdata.php";
                                                    mysql_connect($cssrm["mysql_dbhost"],$cssrm["mysql_dbuser"],$cssrm["mysql_dbpass"]) or die("Invalid server or user.");
                                                    mysql_select_db($cssrm["mysql_dbname"]);
                                                    $query = "SELECT * FROM sectioninfopdf";
                                                    $result = mysql_query($query);
                                                    while($row= mysql_fetch_array($result)) 
                                                    { 
                                                            
                                                            $id = $row['sectioninfopdfid']; 
                                                               
                                                    ?>                                                    
                                                    <tr>
                                                        <td colspan="2"><a href="aboutus2.php?id=<?php echo $id; ?>">Edit</a>&nbsp;<a href="aboutus4.php?id=<?php echo $id; ?>">Delete</a>&nbsp;|&nbsp;<?php echo trim($row['sectioninfopdfname']) ?></td>                                                        </tr>
                                                    <tr>
                                                        <td colspan="2">&nbsp;</td>
                                                    </tr>                                                    
                                                    <?php                                                    
                                                    }  
                                                    ?> 
                                                    <tr>
                                                        <td colspan="2"><hr /></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">Or...</td>
                                                    </tr>                                                   
                                                    <tr>
                                                        <td align="right">&nbsp;</td>
                                                        <td align="left"><input type="submit" name="add" value="Add New Item" />&nbsp;<input type="submit" name="finished" value="Finished" /></td>
                                                    </tr>
                                                </table>
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