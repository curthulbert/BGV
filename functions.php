<?php
function my_curl_function($w) {
	//use curl to grab all html
	$curl_handle = curl_init();
	curl_setopt($curl_handle, CURLOPT_URL, $w);
	curl_setopt($curl_handle, CURLOPT_CONNECTTIMEOUT, 2);
	curl_setopt($curl_handle, CURLOPT_RETURNTRANSFER, 1);
	curl_setopt($curl_handle, CURLOPT_USERAGENT, 'name');
	$htmlhandoff = curl_exec($curl_handle);
	curl_close($curl_handle);
	return $htmlhandoff;
}

function get_user_ip($w) {
	//get client ip
	$w = '';
	if (getenv('HTTP_CLIENT_IP'))
		$w = getenv('HTTP_CLIENT_IP');
	else if(getenv('HTTP_X_FORWARDED_FOR'))
		$w = getenv('HTTP_X_FORWARDED_FOR');
	else if(getenv('HTTP_X_FORWARDED'))
		$w = getenv('HTTP_X_FORWARDED');
	else if(getenv('HTTP_FORWARDED_FOR'))
		$w = getenv('HTTP_FORWARDED_FOR');
	else if(getenv('HTTP_FORWARDED'))
	   $w = getenv('HTTP_FORWARDED');
	else if(getenv('REMOTE_ADDR'))
		$w = getenv('REMOTE_ADDR');
	else
		$w = 'UNKNOWN';
	return $w;
}

function get_host_ip($w) {
	//get host ip
	$hosts = gethostbynamel($w);
	if (is_array($hosts)) {
		 foreach ($hosts as $ip) {
			  return $ip;
		 }
	} else {
		 return false;
	}
}

function get_title($w) {
	//pull title out of website
    $sData = file_get_contents($w);
    if(preg_match('/<head.[^>]*>.*<\/head>/is', $sData, $aHead)){
        $sDataHtml = preg_replace('/<(.[^>]*)>/i', strtolower('<$1>'), $aHead[0]);
        $xTitle = simplexml_import_dom(DomDocument::LoadHtml($sDataHtml));
        return (string)$xTitle->head->title;
    }
    return null;
}

function valid_website($w) {
	//validate website
		if (!preg_match("/(http|https|ftp|ftps)\:\/\/[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(\/\S*)?/",$w)) {
		return FALSE;
	} else{
		return TRUE;
	}
}

function valid_email($w) {
	//validate email
	if (!preg_match("/^[^@]+@[^@]+\.[a-z]{2,6}$/i",$w)) {
		return FALSE;
	} else{
		return TRUE;
	}
}

function remove_http($u){
	//Set new link for testing against original url
	//remove http
	if(substr($u,0,7)=="http://"){
		//Create original url for testing
		$u = substr($u, 7);
		//find length to /
		$urltoremove = strstr($u, '/');
		//remove http://
		$u = str_replace($urltoremove, '', $u);
	//remove https
	}elseif(substr($u,0,8)=="https://"){
		//Create original url for testing
		$u = substr($u, 8);
		//find length to /
		$urltoremove = strstr($u, '/');
		//remove http://
		$u = str_replace($urltoremove, '', $u);
	}
	return $u;
}

function remove_www($u){
	//Set new link for testing against original url
	//remove http://
	if(substr($u,0,4)=="www."){
		//Create original url for testing
		$u = substr($u, 4);
		//find length to /
		$urltoremove = strstr($u, 'www.');
		//remove http://
		$u = str_replace($urltoremove, '', $u);
	//remove https://
	}
	return $u;
}

function remove_tld($u){
	//Set new link for testing against original url
	//remove http://
	if(substr($u,0,1)=="."){
		//Create original url for testing
		$u = substr($u, 10);
		//find length to /
		$urltoremove = strstr($u, '.');
		//remove http://
		$u = str_replace($urltoremove, '', $u);
	//remove https://
	}
	return $u;
}

function check_if_https($w){
	$options = array(
        CURLOPT_RETURNTRANSFER => true,     // return web page
        CURLOPT_HEADER         => false,    // don't return headers
        CURLOPT_FOLLOWLOCATION => true,     // follow redirects
        CURLOPT_ENCODING       => "",       // handle all encodings
        CURLOPT_USERAGENT      => "spider", // who am i
        CURLOPT_AUTOREFERER    => true,     // set referer on redirect
        CURLOPT_CONNECTTIMEOUT => 120,      // timeout on connect
        CURLOPT_TIMEOUT        => 120,      // timeout on response
        CURLOPT_MAXREDIRS      => 10,       // stop after 10 redirects
        CURLOPT_SSL_VERIFYPEER => false     // Disabled SSL Cert checks
    );

    $ci      = curl_init( $w );
    curl_setopt_array( $ci, $options );
    $content = curl_exec( $ci );
    $err     = curl_errno( $ci );
    $errmsg  = curl_error( $ci );
    $header  = curl_getinfo( $ci );
	$header2 = curl_getinfo($ci,CURLINFO_HTTP_CODE);
    curl_close( $ci );

    if(!$err){
		return true;
	}
	return false;
}

function lastcrawldate_lessthan_today($w){
	if (date("Y-m-d") > date('Y-m-d', strtotime($w))){
		return true;
	}
	return false;
}

function grab_website_url($w){
//grab info from attributes table
	$query = "SELECT websiteurl FROM cannawys_rep.websites where websiteid = ". $w;
	$result = mysqli_query($_SESSION["conn"], $query);
	while($row = mysqli_fetch_array($result)) {
		return $row['websiteurl'];
	}
	return null;
}

function which_state($a,$b,$c){
	if ($a>0&&$b>0&&$c>0){
		if(( $a === $b) && ( $b === $c ) ){
			return $a;
		}else{
			return 0;
		}
	}elseif($a>0&&$b>0&&$c==0){
		if( $a === $b){
			return $a;
		}else{
			return 0;
		}
	}elseif($a>0&&$b==0&&$c==0){
			return $a;
	}elseif($a==0&&$b==0&&$c==0){
			return 0;
	}elseif($a>0&&$b==0&&$c>0){
		if( $a === $c){
			return $a;
		}else{
			return 0;
		}
	}elseif($a==0&&$b==0&&$c>0){
		return $c;
	}elseif($a==0&&$b>0&&$c>0){
		if( $b === $c){
			return $b;
		}else{
			return 0;
		}
	}elseif($a==0&&$b>0&&$c==0){
		return $b;
	}
}

function grab_website_crawldate_last($w){
//grab info from attributes table
	$query = "SELECT websitecrawldatelast FROM cannawys_rep.websites where websiteid = ". $w;
	$result = mysqli_query($_SESSION["conn"], $query);
	while($row = mysqli_fetch_array($result)) {
		return $row['websitecrawldatelast'];
	}
	return null;
}

function grab_website_crawldate_first($w){
//grab info from attributes table
	$query = "SELECT websitecrawldatefirst FROM cannawys_rep.websites where websiteid = ". $w;
	$result = mysqli_query($_SESSION["conn"], $query);
	while($row = mysqli_fetch_array($result)) {
		return $row['websitecrawldatefirst'];
	}
	return null;
}

function add_http($w){
	//add the prefix to the url otherwise it will break the search
	if(substr($w,0,4)!="http"){
		$w="http://".$w;
		return $w;
	}
}

function checkpass($pwd) {
		$error = 0;
    if (strlen($pwd) < 8) {
        $error = 1; 
    } elseif (!preg_match("#[0-9]+#", $pwd)) {
        $error = 2; 
    } elseif (!preg_match("#[a-zA-Z]+#", $pwd)) {
        $error = 3; 
    }
    return $error;
}

function rel2abs($rel, $base){
	if (parse_url($rel, PHP_URL_SCHEME) != '') return $rel;
	if ($rel[0]=='#' || $rel[0]=='?') return $base.$rel;
	extract(parse_url($base));
	$path = preg_replace('#/[^/]*$#', '', $path);
	if ($rel[0] == '/') $path = '';
	$abs = "$host$path/$rel";
	$re = array('#(/\.?/)#', '#/(?!\.\.)[^/]+/\.\./#');
	for($n=1; $n>0;$abs=preg_replace($re,'/', $abs,-1,$n)){}
	$abs=str_replace("../","",$abs);
	return $scheme.'://'.$abs;
}

function perfect_url($u,$b){
	// format url
	$bp=parse_url($b);
	if(($bp['path']!="/" && $bp['path']!="") || $bp['path']==''){
		if($bp['scheme']==""){
			$scheme="http";
		}else{
			$scheme=$bp['scheme'];
		}
		$b=$scheme."://".$bp['host']."/";
	}
	if(substr($u,0,2)=="//"){
		$u="http:".$u;
	}
	if(substr($u,0,4)!="http"){
		$u=rel2abs($u,$b);
	}
	return $u;
}

function replace_apostrophe($w){
	//look for an ' in url otherwise this will break mysql
	if (strpos($w,"'") !== false) {
		//find the position of the ' and replace with ascii value change ' to %27
		$w=str_replace("'",'%27',$w);
	} 
	return $w;
}

?>
