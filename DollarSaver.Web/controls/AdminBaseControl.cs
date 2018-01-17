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
    public partial class AdminBaseControl : System.Web.UI.UserControl {

        private bool _isSuperAdmin = false;

        public bool IsSuperAdmin {
            get { return _isSuperAdmin; }
            set { _isSuperAdmin = value; }
        }


        protected String InfoMessage {
            get { return (String)Session["Admin_InfoMessage"] + String.Empty; }
            set { Session["Admin_InfoMessage"] = value; }
        }

        protected String ErrorMessage {
            get { return (String)Session["Admin_ErrorMessage"] + String.Empty; }
            set { Session["Admin_ErrorMessage"] = value; }
        }


        protected void RedirectToHomePage() {
            if (IsSuperAdmin) {
                Response.Redirect("~/admin/super/Default.aspx");
            } else {
                Response.Redirect("~/admin/Default.aspx");
            }
        }

    }

}