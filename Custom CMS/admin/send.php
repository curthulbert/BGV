<?
require('config.php');
include('top.php');
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
                                            <td><h3>Foxtail Pines - Send Mailing List</h3><div align="right"><a href="/admin/index2.php">Admin Home Page</a>&nbsp;-&nbsp;<a href="logout.php">Log out</a></div></td>
                                        </tr>
                                        <tr>
                                            <td><hr></td>
                                        </tr>	
                                        <tr>
                                            <td>
                                                <table border="0" width="100%">
                                                    <tr>
                                                        <td valign="top">
                                                        <?
                                                            // Grab member info			
                                                            $sql2 = "select email from members";
                                                            $res2 = mysql_query($sql2) or die("Invalid server or user.");
                                                            $headers = "From: \"".FROMNAME."\" <".FROMEMAIL.">\n";
                                                            while ($row = mysql_fetch_array($res2)) {													
                                                                $email_addr = $row[0];																																																							
                                                                $fullmessage = $_POST[message];
                                                                //Send mail
                                                                mail("$email_addr", "$_POST[subject]", $fullmessage, $headers);
                                                            }
                                                            echo "<center><strong>This was sent to all administrators and members:<br /><br /></strong></center>";
                                                            echo $fullmessage;
                                                        ?>
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
            </table>
        </td>
    </tr>
<?php 
include("bottom.php");
?>