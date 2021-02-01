<?php
session_start();

if(!session_is_registered(myusername)){
	header("location:index.php");
}else{
$directory_self = str_replace(basename($_SERVER['PHP_SELF']), '', $_SERVER['PHP_SELF']); 
define ("MAX_SIZE","100");
function getExtension($str) {         
			$i = strrpos($str,".");         
			if (!$i) { return ""; }         
			$l = strlen($str) - $i;         
			$ext = substr($str,$i+1,$l);         
			return $ext; 
		}
$errors=0;
$errmsg  = ''; // error message

	if(isset($_POST['upload']))
	{
		
		include("top.php");
		//reads the name of the file the user submitted for uploading 	
		$image=$_FILES['image']['name']; 	
		//if it is not empty 	
		if ($image) 
		{ 	
			//get the original name of the file from the clients machine 		
			$filename = stripslashes($_FILES['image']['name']); 	
			//get the extension of the file in a lower case format  		
			$extension = getExtension($filename); 		
			$extension = strtolower($extension); 	
			//if it is not a known extension, we will suppose it is an error and will not  upload the file,  	
			//otherwise we will do more tests 
			if (($extension != "jpg") && ($extension != "jpeg") && ($extension != "png") && ($extension != "gif")) 
			{		
				//print error message 			
				$errmsg =  "Unknown extension!"; 			
				$errors=1; 
			} 		
			else 		
			{
				//get the size of the image in bytes 
				//$_FILES['image']['tmp_name'] is the temporary filename of the file 
				//in which the uploaded file was stored on the server 
				$size=filesize($_FILES['image']['tmp_name']);
				//compare the size with the maxim size we defined and print error if bigger
				if ($size > MAX_SIZE*10024)
				{	
					$errmsg =  "You have exceeded the size limit!";	
					$errors=1;
				}
				//we will give an unique name, for example the time in unix time format
				//$image_name=$filename.'.'.$extension;
				//the new name will be containing the full path where will be stored (images folder)
				$newname="images/new/".$filename;
				//we verify if the image has been uploaded, and print error instead
				$copied = copy($_FILES['image']['tmp_name'], $newname);
				if (!$copied) 
				{	
					$errmsg =  "Copy unsuccessfull!";	
					$errors=1;
				}
			}
		}
		//If no errors registred, print the success message 
		if(!$errors)  
		{ 	
			$errmsg =  "File Uploaded Successfully!"; 
		}		

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
								        <td><h3>Foxtail Pines - Upload Images</h3><div align="right"><a href="/admin/index2.php">Admin Home Page</a>&nbsp;-&nbsp;<a href="logout.php">Log out</a></div></td>
							        </tr>
							        <tr>
								        <td><hr></td>
							        </tr>	
							        <tr>
								        <td>
 									        <form name="newad" method="post" enctype="multipart/form-data" action=""> 
										        <table>
											        <tr>
												        <td align="center" style="color: red;">
													        <label>
														        <?php echo $errmsg ?>&nbsp;
													        </label>			
												        </td>
											        </tr> 
											        <tr>
											            <td>Please be aware that the filesize to be uploaded cannot exceed 10Mbs.</td>
											        </tr>
											        <tr>
											            <td>&nbsp;</td>
											        </tr> 		
											        <tr>
												        <td><input type="file" name="image" /></td>
											        </tr> 	
											        <tr>
												        <td><input name="upload" type="submit" value="Upload Image" />&nbsp;<input type="submit" name="finished" value="Finished" /></td>
											        </tr> 
										        </table>	 
									        </form>
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