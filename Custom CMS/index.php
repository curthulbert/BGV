<?php 
include("top.php"); 
include_once "dbdata.php";
mysql_connect($cssrm["mysql_dbhost"],$cssrm["mysql_dbuser"],$cssrm["mysql_dbpass"]) or die("Invalid server or user.");
mysql_select_db($cssrm["mysql_dbname"]);
$result = mysql_query("select * from homepageinfo");

while ($row = mysql_fetch_object($result)) {
    $welcomeheader=$row->homepageinfowelcomeheader;
    $welcomecontent=$row->homepageinfowelcomecontent;
    $newsmainheader=$row->homepageinfonewsheader;
    $visionheader=$row->homepageinfovisionheader;
    $visioncontent=$row->homepageinfovisioncontent;
	$visionpdf=$row->homepageinfovisionpdf;
    $visionimage=$row->homepageinfovisionimagename;
    $missionheader=$row->homepageinfomissionheader;
    $missioncontent=$row->homepageinfomissioncontent;
    $missionimage=$row->homepageinfomissionimagename;
    $bottomheader=$row->homepageinfobottomheader;
    $bottomcontent=$row->homepageinfobottomcontent;
    $bottomimage=$row->homepageinfobottomimagename;
    $bottomcontent2=$row->homepageinfobottomcontent2;
    $bottomimage2=$row->homepageinfobottomimagename2;
}
mysql_free_result($result);
?>	
    <tr>
        <td>
            <table cellpadding="0" cellspacing="0" border="0" width="715">
                <tr>
                    <td class="newsevents" valign="top">
                        <table cellpadding="0" cellspacing="5" border="0" width="186">
                            <tr>
                                <td><h3><?php echo $newsmainheader; ?></h3></td>
                            </tr>
                            <tr>
                                <td><hr /></td>
                            </tr>
                            <tr>
                                <td>
                                    <?php
                                        $connection = mysql_connect($cssrm["mysql_dbhost"],$cssrm["mysql_dbuser"],$cssrm["mysql_dbpass"]) or die("Invalid server or user.");
                                        mysql_select_db("news", $connection);
                                        $result = mysql_query("SELECT * FROM news ORDER BY newsdate, newsid desc", $connection) or die("error querying database");
                                        $i = 0;
                                        while($result_ar = mysql_fetch_assoc($result)){
                                    ?>
                                    <table width="100%">
                                        <tr>
                                            <td><strong><?php echo $result_ar['newsheader']; ?></strong></td>							
                                        </tr>
                                        
                                        <tr>
                                            <td><i>[<?php echo date("m/d/Y", strtotime($result_ar['newsdate'])); ?>]</i></td>
                                        </tr>
                                        <tr>
                                            <td><?php echo $result_ar['newscontent']; ?></td>
                                        </tr>
            <tr>
                                            <td><hr class="hrline" /></td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                    <?php
										$i+=1;
										}
                                    ?>
                                
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="maincontent" valign="top">
                        <table cellpadding="0" cellspacing="10" border="0">
                            <tr>
                                <td width="60%" valign="top">
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td><h3><?php echo $welcomeheader; ?></h3></td>
                                        </tr>
                                        <tr>
                                            <td><hr></td>
                                        </tr>	
                                        <tr>
                                            <td><?php echo $welcomecontent; ?></td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="3%">&nbsp;</td>
                                <td width="37%" valign="top">
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td><h3><?php echo $visionheader; ?></h3></td>
                                        </tr>
                                        <tr>
                                            <td><hr></td>
                                        </tr>	
                                        <tr>
                                            <td><?php echo $visioncontent; ?></td>
                                        </tr>
                                        <?php if($visionpdf){ ?>
                                        <tr>
                                            <td><a href="/admin/pdfs/<?php echo $visionpdf; ?>">Read More</a></td>
                                        </tr>
                                        <?php } ?>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3" valign="top">
                                    <table cellpadding="0" cellspacing="0" border="0" width="459">
                                        <tr>
                                            <td colspan="4" valign="top"><h3><?php echo $bottomheader; ?></h3></td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" valign="top"><hr></td>
                                        </tr>	
                                        <tr>
                                            <td valign="top"><?php echo $bottomcontent; ?></td>
                                            <td>&nbsp;</td>
    <td>&nbsp;</td>
                                            <td valign="top" align="right"><img class="imageborder" src="/admin/images/new/<?php echo $bottomimage; ?>" width="105" height="79" alt="" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3" valign="top">
                                    <table cellpadding="0" cellspacing="0" border="0" width="459">
                                        <tr>
                                            <td valign="top"><?php echo $bottomcontent2; ?></td>
                                            <td>&nbsp;</td>
    <td>&nbsp;</td>
                                            <td valign="top" align="right"><img class="imageborder" src="/admin/images/new/<?php echo $bottomimage2; ?>" width="105" height="79" alt="" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" valign="top" align="center"><a href="http://www.freebloghitcounter.com/" target="_blank"><img src="http://www.freebloghitcounter.com//counter.php?page=www.foxtailpineshoa.org&digits=6&unique=0" alt="free counter" border="0;"></a></td>
                                </tr>
                        </table>
                    </td>
                    
                </tr>
            </table>
        </td>
    </tr>
<?php include("bottom.php"); ?>