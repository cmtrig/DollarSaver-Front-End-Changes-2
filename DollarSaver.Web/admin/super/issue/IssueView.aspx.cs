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

    public partial class IssueView : SuperAdminPageBase {
        
        private int issueId = 0;
        private DollarSaverDB.IssueRow issue;
        private IssueTableAdapter issueAdapter = new IssueTableAdapter();

        protected void Page_Load(object sender, EventArgs e) {

            toggleButton.Click += new EventHandler(toggleButton_Click);
            saveButton.Click += new EventHandler(saveButton_Click);
            cancelButton.Click += new EventHandler(cancelButton_Click);

            issueId = GetIdFromQueryString();

            if (issueId <= 0) {
                RedirectToIssueList();
            }

            DollarSaverDB.IssueDataTable issueSearch = issueAdapter.GetIssue(issueId);

            if (issueSearch.Count != 1) {
                RedirectToIssueList();
            }

            issue = issueSearch[0];

            if (!Page.IsPostBack) {

                toggleButton.Text = issue.IsOpen ? "Close Issue" : "Reopen Issue";

                issueIdLabel.Text = issue.IssueId.ToString();
                stationLabel.Text = issue.Station.Name;
                statusLabel.Text = issue.IsOpen ? "Open" : "Closed";
                dateLabel.Text = issue.IssueDate.ToString("MM/dd/yyyy hh:mm:ss tt");

                if (!issue.IsAdvertiserIdNull()) {
                    advertiserLink.Text = issue.Advertiser.Name;
                    advertiserLink.NavigateUrl = "~/admin/AdvertiserEdit.aspx?station_id=" + issue.StationId + "&id=" + issue.AdvertiserId;
                } else {
                    advertiserLink.Visible = false;
                }

                if (!issue.IsOrderIdNull()) {

                    OrderTableAdapter orderAdpater = new OrderTableAdapter();

                    DollarSaverDB.OrderDataTable orderSearch = orderAdpater.GetOrder(issue.OrderId);

                    if (orderSearch.Count == 0) {
                        orderLink.Visible = false;
                        orderIdLabel.Visible = true;
                        orderIdLabel.Text = issue.OrderId.ToString() + " - Order not found";
                    } else {
                        DollarSaverDB.OrderRow order = orderSearch[0];

                        orderLink.Visible = true;
                        orderLink.Text = order.OrderId.ToString();
                        orderLink.NavigateUrl = "~/admin/OrderView.aspx?station_id=" + order.StationId + "&id=" + order.OrderId;

                        if (order.StationId != issue.StationId) {
                            orderIdLabel.Visible = true;
                            orderIdLabel.Text = " - Order station does not match issue station!";
                        } else {
                            orderIdLabel.Visible = false;
                        }

                    }

                }

                nameLabel.Text = Server.HtmlEncode(issue.FullName);
                emailLabel.Text = Server.HtmlEncode(issue.Email);
                messageLabel.Text = Server.HtmlEncode(issue.Message).Replace(Environment.NewLine, "<BR>");

                if (!issue.IsAdminNotesNull()) {
                    adminNotesBox.Text = issue.AdminNotes;
                }

            }

        }

        void toggleButton_Click(object sender, EventArgs e) {

            issue.IsOpen = !issue.IsOpen;
            if (Save()) {

                if (issue.IsOpen) {
                    InfoMessage = "Issue Reopened";
                } else {
                    InfoMessage = "Issue Closed";
                }

                RedirectToIssueList();
            }
        }


        void saveButton_Click(object sender, EventArgs e) {

            if (Save()) {
                InfoMessage = "Issue Saved";
                RedirectToIssueList();
            }
        }

        private bool Save() {

            if (Page.IsValid) {

                String adminNotes = adminNotesBox.Text.Trim();

                if (adminNotes.Length > 5000) {
                    adminNotes = adminNotes.Substring(0, 5000);
                }

                issue.AdminNotes = adminNotes;

                issueAdapter.Update(issue);
                return true;
            } else {
                return false;
            }
        }

        void cancelButton_Click(object sender, EventArgs e) {
            RedirectToIssueList();
        }


        private void RedirectToIssueList() {
            Response.Redirect("~/admin/super/issue/IssueList.aspx");
        }

    }
}