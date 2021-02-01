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

To test this feature make sure you are either a memer or administrator in the system first so that you can recieve the test email.
<BR><BR>
Send an email to your list.

<BR><BR>


	<TABLE BORDER=0 align=center>
	<form action="send.php" method="post">
		<TR>
			<TD VALIGN=top>Subject:</TD>
         <TD VALIGN=top><input type=text name="subject"></TD>
		</TR>
		<TR>

			<TD VALIGN=top>Message:</TD>
			<TD VALIGN=top><textarea name="message" rows=10 cols=40></textarea></TD>
		</TR>
		<TR>
		   <TD VALIGN=top>&nbsp;</TD>
		   <TD VALIGN=top><input type=submit name=submit value=submit></TD>
		</TR>
	</form>

	</TABLE>
</td>


							        </tr>
						        </table>
					        </td>					
				        </tr>
				        <tr>
					        <td>&nbsp;</td>
				        </tr>
					<tr>
					        <td>&nbsp;</td>
				        </tr>
					<tr>
					        <td>&nbsp;</td>
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