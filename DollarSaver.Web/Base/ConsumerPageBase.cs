using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.IO;

using DollarSaver.Core.Data;
using DollarSaver.Core.Data.DollarSaverDBTableAdapters;

namespace DollarSaver.Web {

    /// <summary>
    /// Summary description for ConsumerPageBase
    /// </summary>
    public class ConsumerPageBase : DollarSaverPageBase {


        public String InfoMessage {
            get { return (String)Session["InfoMessage"] + String.Empty; }
            set { Session["InfoMessage"] = value; }
        }

        public String ErrorMessage {
            get { return (String)Session["ErrorMessage"] + String.Empty; }
            set { Session["ErrorMessage"] = value; }
        }

        protected override void OnPreInit(EventArgs e) {
            base.OnPreInit(e);

            EnableEventValidation = false;
            Response.Buffer = true;
        }


        protected int GetStationIdFromQueryString() {
            return GetValueFromQueryString("station_id");
        }

        protected int GetIdFromQueryString() {
            return GetValueFromQueryString("id");
        }

        protected int GetOrderIdFromQueryString() {
            return GetValueFromQueryString("order_id");
        }

        protected int GetValueFromQueryString(String key) {

            String idString = Request.QueryString[key];

            int id = 0;

            try {
                id = Int32.Parse(idString);
            } catch { }

            return id;

        }

        


    }
}