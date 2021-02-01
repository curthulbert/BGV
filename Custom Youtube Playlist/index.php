<?php include 'functions.php'; ?>
<link href="youtubeplaylist.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.3.2/jquery.min.js"></script>
<script type="text/javascript" src="jquery.youtubeplaylist.js"></script>
<script type="text/ecmascript">
		$(function() {
			$("ul.playlist").ytplaylist({addThumbs:true, autoPlay: true, holderId: 'youtubevideo', playerHeight: '200', playerWidth: '350', thumbSize: 'small', showInline: false, showRelated: true, allowFullScreen: true});
		});
</script>
<form id="youtubeform" name="youtubeform" action="index.php" method="post" >
	<div>
		<div class="youtubeholder">
			<div id="youtubevideo"></div>
			<div class="scroller">				
				<?php
				//Set Video id from query string var
				if ($_GET["v"]) {
				$v=$_GET["v"];
				} else {
				$v="";
				}
				//set video playlist id from query string var
				if ($_GET["vpid"]) {
					$vpid=$_GET["vpid"];
				} else {
					$vpid="";
				}
				//  set video name from query string var
				if ($_GET["vn"]) {
				 	$vn=$_GET["vn"]; 
				} else {
					$vn="";
				}				
				// set video playlist name from query string var
				if ($_GET["vpn"]) {
				 	$vpn=$_GET["vpn"]; 
				} else {
					$vpn="";
				}							
				// set feed URL 
				$allplaylistbychannelURL = 'https://gdata.youtube.com/feeds/api/users/advwebsolutions/playlists?v=2&orderby=published'; 
				// read feed into SimpleXML object
				$sxml = simplexml_load_file($allplaylistbychannelURL);
				?>
                <!-- drop down with choices of Youtube Playlists -->
				<p><h2><?=$sxml->title; ?></h2>
				<select name="selectplaylist" id="selectplaylist">
					<option value="0">--Please Select--</option>
				<?php	
					foreach ($sxml->entry as $entry) {
						// get nodes in media: namespace for media information
						$xmlcontent = explode(":", $entry->id );
						echo "<option value='".$xmlcontent[5].":".$entry->title."'>".$entry->title."</option>";
					}				
				?>
				</select>
				<input type="submit" name="view" value="View">
				<br />				
				<ul class="playlist">
				<?php if ($v) { ?>			
                    <li class="smaller"><a href="index.php?v=<?=$v; ?>&vpid=<?=$vpid; ?>&vn=<?=$vn; ?>&vpn=<?php echo $vpn; ?>"><strong>Viewing:</strong>&nbsp;<?=$vn; ?></a></li>	
				<?php } //This is for easy read ability of testing for the Video ID being returned from a query string var ?>			
                <?php
                $playlistpieces = explode(":", $_REQUEST['selectplaylist']);
                $videoplaylistid = $playlistpieces[0];
                $videoplaylistname = $playlistpieces[1];
                echo "<h3>".$videoplaylistname."</h3>";
                if ($videoplaylistid != undefined){
                    if ($videoplaylistid != "" and $videoplaylistid != '0') {
                        $feedURL = 'https://gdata.youtube.com/feeds/api/playlists/'.$videoplaylistid.'?v=2';
                        // read feed into SimpleXML object
                        $selectedplaylist = simplexml_load_file($feedURL);
                        foreach ($selectedplaylist->entry as $selectedentry) {
                            $media = $selectedentry->children('http://search.yahoo.com/mrss/');      
                            // get video player URL
                            $attrs = $media->group->player->attributes();
                            $watchvideo = $attrs['url'];      
                            // get video thumbnail
                            $attrs = $media->group->thumbnail[0]->attributes();
                            $thumbnail = $attrs['url'];             
                            // get <yt:duration> node for video length
                            $yt = $media->children('http://gdata.youtube.com/schemas/2007');
                            $attrs = $yt->duration->attributes();
                            $length = $attrs['seconds']; 
                            // get <yt:stats> node for viewer statistics
                            $yt = $selectedentry->children('http://gdata.youtube.com/schemas/2007');
                            $attrs = $yt->statistics->attributes();
                            if ($yt->statistics){
                                $viewCount = $attrs['viewCount'];
                            }else{
                                $viewCount = 0;	   
                            }
                            // get <gd:rating> node for video ratings
                            $gd = $selectedentry->children('http://schemas.google.com/g/2005'); 
                            if ($gd->rating) {
                                $attrs = $gd->rating->attributes();
                                $rating = $attrs['average']; 
                            } else {
                                $rating = 0; 
                            } 
                            //$get videoid;
                            $youtubevideoid = getYouTubeIdFromURL($watchvideo);
                ?>
                    <li class="smaller">							
                        <a href="index.php?v=<?=$youtubevideoid; ?>&vpid=<?=$videoplaylistid; ?>&vn=<?=$media->group->title; ?>&vpn=<?=$videoplaylistname; ?>"><strong><?=$media->group->title; ?></strong></a>        
                        <p><a href="index.php?v=<?=$youtubevideoid; ?>&vpid=<?=$videoplaylistid; ?>&vn=<?=$media->group->title; ?>&vpn=<?=$videoplaylistname; ?>"><img src="<?=$thumbnail;?>" align="left" /></a><?=$media->group->description; ?>
                            <p>&nbsp;</p> 
                            Duration: <?php printf('%0.2f', $length/60); ?> min. <br/>
                            Views: <?=$viewCount; ?> <br/>
                            Rating: <?=$rating; ?> <br />
                            <hr style="width:350px;" /> 
                        </p>						
                    </li>
			    <?php      
                        }// End of For Each
				    }// End of 2nd If
			    }// End of 1st If
               ?>
				</ul>			   
			</div>
		</div>
	</div>
</form>

