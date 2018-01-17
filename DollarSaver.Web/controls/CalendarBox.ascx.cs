
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace DollarSaver.Web.controls {
    public partial class CalendarBox : System.Web.UI.UserControl {

        public String Text {
            get { return dateBox.Text; }
            set { dateBox.Text = value; }
        }

        public bool Required {
            get { return dateBoxRFV.Enabled; }
            set { dateBoxRFV.Enabled = value; }
        }


        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);


            calendarImage.Attributes["OnClick"] = "javascript: showCalendarControl('" + dateBox.ClientID + "');";


        }
    }
}
