<link rel="stylesheet" type="text/css" href="css/styles.css" />
<div id="page">
	<div id="wall" class="facebookWall"></div>
</div>

<!-- jQuery templates. Not rendered by the browser. Notice the type attributes -->

<script id="headingTpl" type="text/x-jquery-tmpl">
<!--<h1>${name}<span>on Facebook</span></h1>-->
</script>

<script id="feedTpl" type="text/x-jquery-tmpl">
<li>
	<img src="${from.picture}" class="avatar" width="30" />	
	<div class="status">
		<h2><a href="http://www.facebook.com/profile.php?id=${from.id}" target="_blank">${from.name}</a></h2>
		<p class="message"><br />{{html message}}</p>
		{{if type == "link" }}
			<div class="attachment">
				{{if picture}}
					<img class="picture" src="${picture}" width="35" />
				{{/if}}
				<div class="attachment-data">
					<p class="name"><a href="${link}" target="_blank">${name}</a></p>
					<p class="caption">${caption}</p>
					<p class="description">${description}</p>
				</div>
			</div>
		{{/if}}
	</div>
	
	<p class="meta">${created_time}<br />
	{{if comments}}
		${comments.count} Comment{{if comments.count!=1}}s{{/if}}
	{{else}}
		0 Comments
	{{/if}} <br />
	{{if likes}}
		${likes.count} Like{{if likes.count!=1}}s{{/if}}
	{{else}}
		0 Likes
	{{/if}}
	</p>
	
</li>
</script>

<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.5.1/jquery.min.js"></script>
<script src="js/jquery.tmpl.min.js"></script>
<script src="js/script.js"></script>

