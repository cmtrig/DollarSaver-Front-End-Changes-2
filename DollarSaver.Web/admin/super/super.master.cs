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

using DollarSaver.Core.Data;
using DollarSaver.Core.Data.DollarSaverDBTableAdapters;

namespace DollarSaver.Web.Admin.Super {

    public partial class super : System.Web.UI.MasterPage {

        public String InfoMessage {
            get { return (String)Session["Admin_InfoMessage"] + String.Empty; }
            set { Session["Admin_InfoMessage"] = value; }
        }

        public String ErrorMessage {
            get { return (String)Session["Admin_ErrorMessage"] + String.Empty; }
            set { Session["Admin_ErrorMessage"] = value; }
        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);

            mainTimer.Tick += new EventHandler<EventArgs>(mainTimer_Tick);
        
        }

        protected override void OnPreRender(EventArgs e) {
            base.OnPreRender(e);

            if (!Page.IsPostBack) {
                LoggedInPageBase parent = (LoggedInPageBase)this.Parent;

                userIdLabel.Text = parent.CurrentUser.Username;
                endYearLabel.Text = DateTime.Now.ToString("yyyy");

                BindData();
            }

            if (InfoMessage != String.Empty) {
                messageHolder.Visible = true;
                messageLabel.Text = InfoMessage;
                InfoMessage = String.Empty;
            } else {
                messageHolder.Visible = false;
            }

            if (ErrorMessage != String.Empty) {
                errorMessageHolder.Visible = true;
                errorMessageLabel.Text = ErrorMessage;
                ErrorMessage = String.Empty;
            } else {
                errorMessageHolder.Visible = false;
            }

        }


        void mainTimer_Tick(object sender, EventArgs e) {
            BindData();
        }

        private void BindData() {

            IssueTableAdapter issueAdapter = new IssueTableAdapter();
            DollarSaverDB.IssueDataTable openIssues = issueAdapter.GetOpen();

            if (openIssues.Count > 0) {
                issueLink.Text = "Issues (" + openIssues.Count.ToString() + ")";
            } else {
                issueLink.Text = "Issues";
            }
        }
    
    }
}
