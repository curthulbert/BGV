<%@ Control language="c#" %>

<script runat="server">

	private DateTime thedate = DateTime.MinValue;
	private string defaulttext = "Select Date";
	private string linkstyle = String.Empty;


	public DateTime SelectedDate
	{
		get
		{
			DateTime incomingdate = new DateTime();

			//enclose within a try/catch block to keep it from breaking
			//if an invalid date is contained within the hidden field
			try
			{
				//DateValue is the HTML Hidden Input Control
				//where the selected value is stored
				incomingdate = Convert.ToDateTime(DateValue.Value);
			}
			catch
			{
				//set to a DateTime minimum value if not
				//able to assign the given date
				incomingdate = DateTime.MinValue;
			}

			return incomingdate;
		}
		set
		{
			//this sets the date in the private variable and
			//assigns the date to the hidden form field and the label
			thedate = value;
			lblDate.Text = thedate.ToString("d");
			DateValue.Value = thedate.ToString("d");
		}
	}

	public string DefaultText
	{
		get
		{ return defaulttext; }

		set
		{ defaulttext = value; }
	}

	public string LinkStyle
	{
		get
		{ return linkstyle; }

		set
		{
			linkstyle = value;
			lnkDatePick.Attributes["style"] = value;
		}
	}



	void Page_Load()
	{
		//assign the href property to the link of the control
		lnkDatePick.HRef="Javascript:PickDate('" + this.ID + "');";

		//if a date is specified, apply the value to the control
		if (SelectedDate != DateTime.MinValue)
		{
			lblDate.Text = SelectedDate.ToString("d");
		}
		else
		{
			//a date wasn’t specified so assign the default text
			DateValue.Value = DefaultText;
			lblDate.Text = DefaultText;
		}

		//call the function to add the javascript
		RegisterJavaScript();
	}

	void RegisterJavaScript()
	{
		string ScriptString = @"
<script language='javascript'>

function PickDate(ControlName)
{

	var Editor = document.getElementById(ControlName + '_lblDate');
	var SelectedDate = Editor.innerHTML;

	var DateValue = document.getElementById(ControlName + '_DateValue');


	if (!document.all)
	{
		//NS
		window.open('datepicker_calendar.aspx?Frame=1&Date=' + SelectedDate + '&ControlID=' + ControlName, '_blank', 'toolbar=no, location=no, menubar=no, width=270, height=220, modal=yes');
	}
	else
	{
		//IE
		var DateArr = showModalDialog('datepicker_calendar.aspx?Date=' + SelectedDate + '&ControlID=' + ControlName,window,'dialogWidth:300px; dialogHeight:260px;help:0;status:0;resizeable:1;scroll:on;');

		//if there's a return value, assign it to the control
		if (DateArr != null)
		{
			Editor.innerHTML = DateArr[""date""];
			DateValue.setAttribute('value', DateArr[""date""]);
		}
	}
}

//this function is used to handle the value returned from the netscape modal dialog
function NSSetDate(DateArr, ControlName)
{
	if (DateArr != null)
	{
		//strip all leading and trailing spaces
		var thisControl = ControlName.replace(" + "/" + @"^\s+" + "/" + @",'').replace(" + "/" + @"\s+$" + "/" + @",'');

		//get the elements within the DOM
		var Editor = document.getElementById(thisControl + ""_lblDate"");
		var DateValue = document.getElementById(thisControl + ""_DateValue"");

		//set the values
		Editor.innerHTML = DateArr[""date""];
		DateValue.setAttribute('value', DateArr[""date""]);
	}
}
<" + "/" + "script>";


		//See if the client script block has been registered on the page
		//by another instance of the control
		if (!Page.IsClientScriptBlockRegistered("calendarScript"))
		{
			//it hasn't been added, register it to the page now
			Page.RegisterClientScriptBlock("calendarScript", ScriptString);
		}

	}

</script>



<input type="hidden" id="DateValue" runat="server"/>
<a id="lnkDatePick" runat="server"><ASP:Label id="lblDate" runat="server"/></a>
