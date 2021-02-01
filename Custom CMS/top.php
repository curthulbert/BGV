<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>Foxtail Pines HOA</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link rel="shortcut icon" href="/favicon.ico" />
<link rel="stylesheet" href="/styles/main.css">
<link type="text/css" rel="stylesheet" href="/styles/ie7.css" />
    <!--[if lt IE 7]>
	<link type="text/css" rel="stylesheet" href="/styles/ie6.css" />	
	<script src="/scripts/ie6/warning.js"></script><script>window.onload=function(){iewarning("/scripts/ie6/")}</script>
	<![endif]-->
<script language="JavaScript1.2" type="text/javascript">
<!--
function MM_findObj(n, d) { //v4.01
  var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
    d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
  if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
  for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
  if(!x && d.getElementById) x=d.getElementById(n); return x;
}
function MM_swapImage() { //v3.0
  var i,j=0,x,a=MM_swapImage.arguments; document.MM_sr=new Array; for(i=0;i<(a.length-2);i+=3)
   if ((x=MM_findObj(a[i]))!=null){document.MM_sr[j++]=x; if(!x.oSrc) x.oSrc=x.src; x.src=a[i+2];}
}
function MM_swapImgRestore() { //v3.0
  var i,x,a=document.MM_sr; for(i=0;a&&i<a.length&&(x=a[i])&&x.oSrc;i++) x.src=x.oSrc;
}

function MM_preloadImages() { //v3.0
  var d=document; if(d.images){ if(!d.MM_p) d.MM_p=new Array();
    var i,j=d.MM_p.length,a=MM_preloadImages.arguments; for(i=0; i<a.length; i++)
    if (a[i].indexOf("#")!=0){ d.MM_p[j]=new Image; d.MM_p[j++].src=a[i];}}
}

//-->
</script>
</head>
<body bgcolor="#ffffff" onload="MM_preloadImages('/images/youth1_f2.jpg','/images/section4_f2.jpg','/images/member1_f2.jpg','/images/newsletter1_f2.jpg','/images/contactus1_f2.jpg','/images/helpful1_f2.jpg','/images/photo1_f2.jpg');">
<?php
include_once "dbdata.php";
mysql_connect($cssrm["mysql_dbhost"],$cssrm["mysql_dbuser"],$cssrm["mysql_dbpass"]) or die("Invalid server or user.");
mysql_select_db($cssrm["mysql_dbname"]);
//get homepage contentin info
$result = mysql_query("select * from homepageinfo");
while ($row = mysql_fetch_object($result)) {
    $image3=$row->homepageinfologoimagename;
    $image4=$row->homepageinfomainimagename;
    $image5=$row->homepageinfotoprightimagename;
    $image6=$row->homepageinfobottomrightimagename;
}
mysql_free_result($result);
//get member info
if (isset($_SESSION['currentmemberid'])){
	mysql_connect($cssrm["mysql_dbhost"],$cssrm["mysql_dbuser"],$cssrm["mysql_dbpass"]) or die("Invalid server or user.");
	mysql_select_db($cssrm["mysql_dbname"]);
	$resultmember = mysql_query("select * from members where id =" . $_SESSION['currentmemberid']);
	while ($rowmember = mysql_fetch_object($resultmember)) {
		$_SESSION['currentmemberfullname']=$rowmember->fullname;
		$_SESSION['currentmemberemail']=$rowmember->email;
	}
	mysql_free_result($resultmember);
 }
?>	
<div id="pageShadow">
    <div align="center">
        <table cellpadding="0" cellspacing="0" border="0" width="715">
            <tr>
                <td><!--Menu-->
                    <table cellpadding="0" cellspacing="0" border="0" width="715">
                        <tr>
                            <td><a href="/" onmouseout="MM_swapImgRestore();" onmouseover="MM_swapImage('home1','','/images/new/h_f2.jpg',1);"><img name="home1" src="/images/new/h.jpg" width="75" height="60" border="0" id="home1" title="Home" alt="Home" /></a></td>
                            <td><a href="/aboutus.php" onmouseout="MM_swapImgRestore();" onmouseover="MM_swapImage('aboutus','','/images/new/au_f2.jpg',1);"><img name="aboutus" src="/images/new/au.jpg" width="97" height="60" border="0" id="aboutus" title="About Us" alt="About Us" /></a></td>
                            <td><a href="/resources.php" onmouseout="MM_swapImgRestore();" onmouseover="MM_swapImage('resources','','/images/new/re_f2.jpg',1);"><img name="resources" src="/images/new/re.jpg" width="111" height="60" border="0" id="resources" title="Resources" alt="Resources" /></a></td>
                            <td><a href="/documents.php" onmouseout="MM_swapImgRestore();" onmouseover="MM_swapImage('documents','','/images/new/docs_f2.jpg',1);"><img name="documents" src="/images/new/docs.jpg" width="117" height="60" border="0" id="documents" title="Documents" alt="Documents" /></a></td>
                            <td><a href="/faqs.php" onmouseout="MM_swapImgRestore();" onmouseover="MM_swapImage('faqs','','/images/new/ffl_f2.jpg',1);"><img name="faqs" src="/images/new/ffl.jpg" width="202" height="60" border="0" id="faqs/links" title="Feedback - FAQs - Links" alt="Feedback - FAQs - Links" /></a></td>
                            <td><a href="/contactus.php" onmouseout="MM_swapImgRestore();" onmouseover="MM_swapImage('contactus','','/images/new/cu_f2.jpg',1);"><img name="home" src="/images/new/cu.jpg" width="113" height="60" border="0" id="contactus" title="Contact Us" alt="Contact Us" /></a></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td><!--Line Under Menu--><img src="/images/new/lineundermenu.jpg" height="5" width="715" alt="" /></td>
            </tr>
            <tr>
                <td><!--Logo and Pics-->
                    <table cellpadding="0" cellspacing="0" border="0" height="215" width="715">
                        <tr>
                            <td><a href="/login.php"><img src="/admin/images/new/<?php echo $image3; ?>" width="200" height="215" alt="" /></a></td>
                            <td><!--spacer--><img src="/images/new/spcerleft.jpg" width="3" height="215" alt="" /></td>
                            <td><img src="/admin/images/new/<?php echo $image4; ?>" width="322" height="215" alt="" /></td>
                            <td><!--spacer--><img src="/images/new/spcerright.jpg" width="3" height="215" alt="" /></td>
                            <td>
                                <table cellpadding="0" cellspacing="0" border="0" height="215" width="187">
                                    <tr>
                                        <td><img src="/admin/images/new/<?php echo $image5; ?>" width="187" height="107" alt="" /></td>
                                    </tr>
                                    <tr>
                                        <td><!--spacer--><img src="/images/new/rightimagesspcer.jpg" width="187" height="1" alt="" /></td>
                                    </tr>
                                    <tr>
                                        <td><img src="/admin/images/new/<?php echo $image6; ?>" width="187" height="107" alt="" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td><!--news header line--><img src="/images/new/undermain.jpg" width="715" height="18" alt="" /></td>
            </tr>
            
