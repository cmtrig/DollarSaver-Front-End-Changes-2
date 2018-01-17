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

using System.Net.Mail;
using System.Text.RegularExpressions;

using DollarSaver.Core.Common;
using DollarSaver.Core.Data;
using DollarSaver.Core.Data.DollarSaverDBTableAdapters;

namespace DollarSaver.Web {
    public partial class ContactUs : StationPageBase {

        public override string PageTitle {
            get {
                return base.PageTitle + " - Contact Us";
            }
        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);

            submitButton.Click += new EventHandler(submitButton_Click);

            if (!Page.IsPostBack) {

                AdvertiserTableAdapter advertiserAdapter = new AdvertiserTableAdapter();
                DollarSaverDB.AdvertiserDataTable advertisers = advertiserAdapter.GetActiveByStation(StationId);

                advertiserList.DataSource = advertisers.Rows;
                advertiserList.DataTextField = "Name";
                advertiserList.DataValueField = "AdvertiserId";
                advertiserList.DataBind();

                advertiserList.Items.Insert(0, new ListItem("", "0"));
            }
        
        }

        void submitButton_Click(object sender, EventArgs e) {
            if (Page.IsValid) {

                String firstName = firstNameBox.Text.Trim();
                String lastName = lastNameBox.Text.Trim();
                String emailAddress = emailBox.Text.Trim();
                String confirmEmailAddress = confirmEmailBox.Text.Trim();
                int advertiserId = Convert.ToInt32(advertiserList.SelectedValue);
                String orderIdStr = orderIdBox.Text.Trim();
                String message = messageBox.Text.Trim();

                if (firstName == String.Empty) {
                    ErrorMessage = "First Name is required";
                    return;
                } else if (firstName.Length > 50) {
                    firstName = firstName.Substring(0, 50);
                }

                if (lastName == String.Empty) {
                    ErrorMessage = "Last Name is required";
                    return;
                } else if (lastName.Length > 50) {
                    lastName = lastName.Substring(0, 50);
                }

                if (emailAddress == String.Empty) {
                    ErrorMessage = "E-mail is required";
                    return;
                }

                if (emailAddress != confirmEmailAddress) {
                    ErrorMessage = "E-mail address do not match";
                    return;
                }

                if (!Regex.IsMatch(emailAddress, @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$")) {
                    ErrorMessage = "E-mail address is not valid";
                    return;
                }

                int orderId = 0;

                if (orderIdStr != String.Empty) {
                    try {
                        orderId = Convert.ToInt32(orderIdStr);

                        if (orderId <= 0) {
                            throw new Exception();
                        }
                    } catch {
                        ErrorMessage = "Order ID is not valid";
                        return;
                    }
                }

                if (message == String.Empty) {
                    ErrorMessage = "Please enter a message";
                    return;
                } else if (message.Length > 5000) {
                    message = message.Substring(0, 5000);
                }

                IssueTableAdapter issueAdapter = new IssueTableAdapter();

                int issueId = Convert.ToInt32(issueAdapter.InsertPK(StationId, true, DateTime.Now, Globals.ConvertToNull(advertiserId), Globals.ConvertToNull(orderId),
                    message, emailAddress, firstName, lastName, null, null, null, null, null, null, null));


                MailMessage emailMessage = new MailMessage("\"" + firstName + " " + lastName + "\" <" + emailAddress + ">", 
                    "\"DollarSaver Help\" <help@dollarsavershow.com>");

                emailMessage.IsBodyHtml = false;

                emailMessage.Subject = Station.SiteNamePlainText + " DollarSaver Issue #" + issueId;

                String body = Station.SiteNamePlainText + " DollarSaver Issue #" + issueId + Environment.NewLine + Environment.NewLine;

                body += "From: " + firstName + " " + lastName + " - " + emailAddress + Environment.NewLine;

                if (advertiserId > 0) {
                    AdvertiserTableAdapter advertiserAdapter = new AdvertiserTableAdapter();
                    DollarSaverDB.AdvertiserRow advertiser = advertiserAdapter.GetAdvertiser(advertiserId)[0];
                    body += "Advertiser: " + advertiser.Name + Environment.NewLine;
                }

                if (orderId > 0) {
                    body += "Order #: " + orderId + Environment.NewLine;
                }


                body += Environment.NewLine + "Message:" + Environment.NewLine;
                body += message;

                emailMessage.Body = body;

                try {
                    Mailer mailer = new Mailer();
                    mailer.Send(emailMessage);
                } catch { }

                InfoMessage = "Thank you for your message. You should receive a response within 48 hours.";
                DollarSaverRedirect("~/Default.aspx");

            }
        }
    }
}