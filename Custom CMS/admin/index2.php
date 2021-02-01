<?php 
session_start();
if(!session_is_registered(myusername)){
	header("location:index.php");
}else{
	include("top.php");
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
                                            <td><h3>Foxtail Pines HOA Web Admin Menu</h3><div align="right"><a href= logout.php>Log out</a></div></td>
                                        </tr>
                                        <tr>
                                            <td><hr></td>
                                        </tr>	
                                        <tr>
                                            <td align="center">
                                                <table width="50%">
                                                    <tr>
                                                        <td><strong>Content Menu</strong></td>
                                                    </tr>
                                                    <tr>
                                                        <td><hr /></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left"><a href="homecontent.php">Edit Home Page</a></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left"><a href="aboutuscontent.php">Edit About Us Page</a></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left"><a href="resourcescontent.php">Edit Resources Page</a></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left"><a href="documentscontent.php">Edit Documents Page</a></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left"><a href="faqscontent.php">Edit FAQs Page</a></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left"><a href="contactuscontent.php">Edit Contact Us Page</a></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left"><a href="feedback.php">Edit Feedback</a></td>
                                                    </tr>								                
                                                    <tr>
                                                        <td><hr /></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left"><a href="adminsetup.php">Add, Edit or Delete an Administrator or Member</a></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left"><a href="homenews.php">Add, Edit or Delete the Home Page News</a></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left"><a href="aboutus.php">Add, Edit or Delete the About Us PDFs</a></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left"><a href="resources.php">Add, Edit or Delete the Resources PDFs</a></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left"><a href="documents.php">Add, Edit or Delete the Document PDFs</a></td>
                                                    </tr>	
                                                    <tr>
                                                        <td align="left"><a href="documentstype.php">Add, Edit or Delete a Document type</a></td>
                                                    </tr>									
                                                    <tr>
                                                        <td align="left"><a href="faqs.php">Add, Edit or Delete the FAQ's Links</a></td>
                                                    </tr>										
                                                     <tr>
                                                        <td><hr /></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left"><a href="sendmailing.php">Send Mass Email</a></td>
                                                    </tr>
                                                    <tr>
                                                        <td><hr /></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left"><a href="uploadimages.php">Upload Images</a></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left"><a href="uploadpdfs.php">Upload PDFs</a></td>
                                                    </tr><tr>
                                                        <td><hr /></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left"><a href="exportmemberinfo.php">Export Members List</a></td>
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