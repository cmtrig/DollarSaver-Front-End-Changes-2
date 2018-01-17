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

using DollarSaver.Core.Common;
using DollarSaver.Core.Data;
using DollarSaver.Core.Data.DollarSaverDBTableAdapters;
using DollarSaver.Web.controls;

namespace DollarSaver.Web.Admin.Super.IssueAdmin {

    public partial class IssueList : SuperAdminPageBase {

        private bool openIssues;

        protected void Page_Load(object sender, EventArgs e) {


            issueGrid.PageIndexChanging += new GridViewPageEventHandler(issueGrid_PageIndexChanging);
            issueGrid.RowDataBound += new GridViewRowEventHandler(issueGrid_RowDataBound);

            String view = Request.QueryString["view"];
            openIssues = view != "closed";

            if (!Page.IsPostBack) {


                if (view == "closed") {
                    headerCell.Attributes["class"] = "inactive_header";
                    headerLabel.Text = "Closed Issues";
                    listLink.Text = "View Open Issues";
                    listLink.NavigateUrl = "~/admin/super/issue/IssueList.aspx";
                    BindIssues();
                } else {
                    headerLabel.Text = "Open Issues";
                    listLink.Text = "View Closed Issues";
                    listLink.NavigateUrl = "~/admin/super/issue/IssueList.aspx?view=closed";
                    BindIssues();
                }
            }

        }

        protected override void OnPreRender(EventArgs e) {
            base.OnPreRender(e);

            if (!Page.IsPostBack) {
            }
        }

        private void BindIssues() {

            IssueTableAdapter issueAdapter = new IssueTableAdapter();
            DollarSaverDB.IssueDataTable issues;

            if (openIssues) {
                issues = issueAdapter.GetOpen();
            } else {
                issues = issueAdapter.GetClosed();
            }

            if (issues.Count > 0) {
                issueHolder.Visible = true;
                noDataFoundHolder.Visible = false;
                
                issueGrid.DataSource = issues.Rows;
                issueGrid.DataBind();
            } else {
                issueHolder.Visible = false;
                noDataFoundHolder.Visible = true;
            }
        }



        void issueGrid_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {
                DollarSaverDB.IssueRow issue = (DollarSaverDB.IssueRow)e.Row.DataItem;

                Label statusLabel = (Label)e.Row.FindControl("statusLabel");

                if (issue.IsOpen) {
                    statusLabel.Text = "Open";
                } else {
                    statusLabel.Text = "Closed";
                }
            }
        }

        void issueGrid_PageIndexChanging(object sender, GridViewPageEventArgs e) {

            issueGrid.PageIndex = e.NewPageIndex;

            BindIssues();
        }

    }
}