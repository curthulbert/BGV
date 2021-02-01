<%@ Page Language="C#" %>

<script runat="server">

	void Page_Load() 
	{
		//if the frame querystring is specified, hide the frame panel
		// and display the calendar panel
		if (Request.QueryString["frame"] != null)
		{
			pnlCalendar.Visible = true;
			pnlIFrame.Visible = false;

			//if the page isn’t posting back to it’s self, set date to the one
			//specified in the querystring
			if (!Page.IsPostBack)
			{

				//set the specified date within a try/catch block
                                //to make sure that it’s a valid date.
				try
				{
					Calendar1.SelectedDate = Convert.ToDateTime(Request.QueryString["date"]);
				}
				catch
				{
					//if the date passed to the application
					//isn't valid, set the date to today.
					Calendar1.SelectedDate = DateTime.Now;

				}


				//set the date being displayed to the selected date
				//by default, the visible date is today’s date
				Calendar1.VisibleDate = Calendar1.SelectedDate;

			}
		}
	}



	//the function to render the days of the calendar as specified by us
	private void Calendar1_DayRender(object sender, System.Web.UI.WebControls.DayRenderEventArgs e)
	{
		//clear the controls that are rendered automatically
		//by the calendar control

		e.Cell.Controls.Clear();

		//Create a new link control
		System.Web.UI.HtmlControls.HtmlGenericControl Link = new System.Web.UI.HtmlControls.HtmlGenericControl();

		Link.TagName = "a";
		Link.InnerText = e.Day.DayNumberText;

		//set the href value to the javascript function to select the date
		Link.Attributes.Add("href", "Javascript:SelectDate('" + e.Day.Date.ToString("d") + "');");

		//set the color of the link
		Link.Attributes.Add("style", "color:#000000;");

		//add our link control to the day
		e.Cell.Controls.Add(Link);
	}


</script>

<ASP:Panel id="pnlCalendar" visible="false" runat="server">
<html>
<head>
<title>Select Date</title>

<script language="javascript">

 	// this will determine the value and return it to the opening page
	function SelectDate(SelectedDate)
	{
		var RetVal = { date: "" };
		RetVal["date"] = SelectedDate;

		if (document.all)
		{
			//IE
			self.returnValue = RetVal;
			self.close();
		}
		else
		{
			//Netscape
			var ControlName = "<%=Request.QueryString["ControlID"].Trim() %>";

			//determine the method of opening the window
			if (window.opener)
			{
				//pass the value back to the opening window’s function
				window.opener.NSSetDate(RetVal, ControlName);
				self.close();
			}
			else
			{
				//pass the value back to the opening window’s function
				window.parent.opener.NSSetDate(RetVal, ControlName);
				window.parent.close();

			}

		}
	} 
</script>



</head>
<body>


	<form id="Form1" method="post" runat="server">

		<center>

			<asp:calendar id="Calendar1" runat="server" showgridlines="True" OnDayRender="Calendar1_DayRender" bordercolor="Black">
				<selectorstyle backcolor="#FFCC66"/>
				<nextprevstyle font-size="9pt" forecolor="#FFFFFF"/>
				<dayheaderstyle height="1px" backcolor="#ADB09F"/>
				<daystyle forecolor="#000000"/>
				<selecteddaystyle font-bold="True" BackColor="#99CCFF" ForeColor="#000000"/>
				<OtherMonthDayStyle BackColor="#CCCCCC"/>
				<titlestyle font-size="9pt" font-bold="True" forecolor="#FFFFFF" backcolor="#354E6E"/>

			</asp:calendar>
		</center>

	</form>
</body>
</html>
</ASP:Panel>
<ASP:Panel id="pnlIFrame" runat="server">
<html>
<head>
<title>Select Date</title>
</head>
<body>
<iframe style="width:100%;height:100%;border:1;" border=0 frameborder=0" src="datepicker_calendar.aspx?frame=1&<%=Request.QueryString%>"></iframe>
</body>
</html>
</ASP:Panel>
