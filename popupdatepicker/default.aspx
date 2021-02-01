<%@ Page Language="C#" %>
<%@ Register tagprefix="DPS" tagname="DatePicker" Src="datepicker.ascx" %>

<script runat="server">

	void Page_Load()
	{
		if (!Page.IsPostBack)
		{
			dpDate.SelectedDate = Convert.ToDateTime("12/15/1978");
		}
	}


	void GetDate_OnClick(object sender, EventArgs e)
	{
		lblTheDate.Text = "Date 1: " + dpDate.SelectedDate.ToString("d") + "<br>Date 2: " + dpDate2.SelectedDate.ToString("d");
	}

</script>
<html>
<head>
<title>Dot Pitch Studios - Pop-up Date Picker</title>
</head>
<body>

<form runat="server">
<DPS:DatePicker id="dpDate" runat="server"/>
<br><br>
<DPS:DatePicker id="dpDate2" runat="server"/>
<br><br>
<ASP:Button id="btnGetDate" OnClick="GetDate_OnClick" text="Get Date" runat="server"/>
<br>
<br>
<ASP:Label id="lblTheDate" runat="server"/>
</form>

<br>
<br>
<a href="http://www.dotpitchstudios.com/default.aspx?pageid=7">View the article about how to build this Pop-up Date Picker control</a><br>
<a href="http://www.dotpitchstudios.com/articles/popupdatepicker/popupdatepicker.zip">Download Source Code</a><br>
<a href="http://www.dotpitchstudios.com">Dot Pitch Studios, LLC</a><br>



</body>
</html>