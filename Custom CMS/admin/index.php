<?php 
include("top.php"); 
include_once "dbdata.php"; 
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
                                            <td><h3>Foxtail Pines HOA Website Admin Login</h3></td>
                                        </tr>
                                        <tr>
                                            <td><hr></td>
                                        </tr>	
                                        <tr>
                                            <td>
                                                <table width="300" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#CCCCCC">
                                                    <tr>
                                                        <form name="form1" method="post" action="checklogin.php">
                                                            <td>
                                                                <table width="100%" border="0" cellpadding="3" cellspacing="1" bgcolor="#FFFFFF">
                                                                    <tr>
                                                                        <td colspan="2"><strong>Foxtail Pines Admin Login </strong></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="78">Username:</td>
                                                                        <td width="294"><input name="myusername" type="text" id="myusername" /></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Password:</td>
                                                                        <td><input name="mypassword" type="password" id="mypassword" /></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>&nbsp;</td>
                                                                        <td><input type="submit" name="Submit" value="Login"></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </form>
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