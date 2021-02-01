<?php
session_start();
session_destroy();
include("top.php");
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
                                            <td><h3>Foxtail Pines - Website Logoff</h3><div align="right"><a href= logout.php>Log out</a></div></td>
                                        </tr>
                                        <tr>
                                            <td><hr></td>
                                        </tr>	
                                        <tr>
                                            <td>
                                                You have been logged out!<br /><br />
                                                <a href="aboutus.php">Log in Again</a><br /><br />
                                                or
                                                <br /><br />
                                                <a href="/">Got to the Home Page</a>
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