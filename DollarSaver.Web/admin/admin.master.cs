using System;
using System.Web.UI;
using DollarSaver.Core.Data;

namespace DollarSaver.Web.Admin
{

    public partial class admin : MasterPage
    {

        public String InfoMessage {
            get { return (String)Session["Admin_InfoMessage"] + String.Empty; }
            set { Session["Admin_InfoMessage"] = value; }
        }

        public String ErrorMessage {
            get { return (String)Session["Admin_ErrorMessage"] + String.Empty; }
            set { Session["Admin_ErrorMessage"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e) {

        }


        protected override void OnPreRender(EventArgs e) {
            base.OnPreRender(e);

            if (!Page.IsPostBack) {
                AdminPageBase parent = (AdminPageBase)this.Parent;

                if (parent.CurrentUser.Role == AdminRole.Root) {
                    superAdminHolder.Visible = true;
                } else {
                    superAdminHolder.Visible = false;
                }

                int stationId = parent.StationId;

                DollarSaverDB.StationRow station = parent.Station;

                stationLabel.Text = station.Name;
                stationHeaderLabel.Text = station.Name;
                siteTypeLabel.Text = station.SiteTypeId == (int)SiteType.DealOfTheWeek ? "Deal of the Week" : "Standard";
                consumerLink.NavigateUrl = "~/Default.aspx?station_id=" + stationId;

                userIdLabel.Text = parent.CurrentUser.Username;

                endYearLabel.Text = DateTime.Now.Year.ToString();

                if (parent.CurrentUser.Role == AdminRole.Root || parent.CurrentUser.Role == AdminRole.Admin ||
                    parent.CurrentUser.Role == AdminRole.Manager)
                {


                    if (station.SiteTypeId == (int)SiteType.Standard)
                    {
                        menuItemSpecials.Visible = true;
                        //navMenu.Items.AddAt(5, new MenuItem("Specials", "", "", "~/admin/SpecialEdit.aspx", ""));
                    }
                    else
                    {
                        menuItemDealOfTheWeek.Visible = true;
                        //navMenu.Items.AddAt(5, new MenuItem("Deal Of The Week", "", "", "~/admin/DealOfTheWeek.aspx", ""));
                    }


                }
                else
                {
                    menuItemStation.InnerHtml = "";
                    menuItemUser.InnerHtml = "";
                    menuItemOrders.InnerHtml = "";
                    menuItemReports.InnerHtml = "";
                    //navMenu.Items.RemoveAt(6);
                    //navMenu.Items.RemoveAt(5);
                    //navMenu.Items.RemoveAt(2);
                    //navMenu.Items.RemoveAt(1);
                }

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
    }
}
