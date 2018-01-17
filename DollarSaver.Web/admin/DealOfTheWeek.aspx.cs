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

using System.Text.RegularExpressions;

using DollarSaver.Core.Data;
using DollarSaver.Core.Data.DollarSaverDBTableAdapters;

namespace DollarSaver.Web.Admin {

    public partial class DealOfTheWeek : ManagerPageBase {

        private int displayDay = 1;
        private int displayHour = 8;
        private int onSaleDay = 4;
        private int onSaleHour = 8;

        private DateTime startDate = DateTime.Now;

        protected override void OnInit(EventArgs e) {
            base.OnInit(e);

            if (ReadOnly) {
                Response.Redirect("~/admin/");
            }
        }

        protected void Page_Load(object sender, EventArgs e) {
            //saveButton.Click += new EventHandler(saveButton_Click);
            saveSettingsButton.Click += new EventHandler(saveSettingsButton_Click);

            dealGrid.PageIndexChanging += new GridViewPageEventHandler(dealGrid_PageIndexChanging);
            dealGrid.RowDataBound += new GridViewRowEventHandler(dealGrid_RowDataBound);
            //dealGrid.RowCommand += new GridViewCommandEventHandler(dealGrid_RowCommand);

            if (Station.SiteTypeId != (int)SiteType.DealOfTheWeek) {
                Response.Redirect("~/admin/Default.aspx");
            }

            if (!Page.IsPostBack) {

                DealSettingsTableAdapter settingsAdapter = new DealSettingsTableAdapter();
                DollarSaverDB.DealSettingsDataTable settingsTable = settingsAdapter.GetDealSettings(StationId);

                if (settingsTable.Count == 1) {
                    DollarSaverDB.DealSettingsRow settings = settingsTable[0];

                    displayDay = settings.DisplayDay;
                    displayHour = settings.DisplayHour;
                    onSaleDay = settings.OnSaleDay;
                    onSaleHour = settings.OnSaleHour;
                }

                displayDayList.SelectedValue = displayDay.ToString();
                displayHourList.SelectedValue = displayHour.ToString();
                onSaleDayList.SelectedValue = onSaleDay.ToString();
                onSaleHourList.SelectedValue = onSaleHour.ToString();


                BindData();


                DateTime dealDate = startDate.AddDays(((7 - (int)startDate.DayOfWeek + onSaleDay) % 7) + 7 * (int)((7 - (int)startDate.DayOfWeek + onSaleDay) / 7));

                // check to make sure deal date's diplay date won't be before latest existing deal's sale date
                DateTime displayDate = dealDate.AddDays(-1 * ((7 + onSaleDay - displayDay) % 7)).AddHours(displayHour - onSaleHour);

                if (displayDate <= startDate) {
                    dealDate = dealDate.AddDays(7);
                }

                for (int i = 1; i <= 10; i++) {
                    //dealDateList.Items.Add(new ListItem(dealDate.ToString("ddd MMM dd, yyyy"), dealDate.ToString("MM/dd/yyyy")));
                    dealDate = dealDate.AddDays(7);
                }

                /*
                CertificateTableAdapter certificateAdapter = new CertificateTableAdapter();
                DollarSaverDB.CertificateDataTable certificates = certificateAdapter.GetActive(StationId);

                certificateList.DataSource = certificates;
                certificateList.DataTextField = "ShortName";
                certificateList.DataValueField = "CertificateId";
                certificateList.DataBind();
                */
            }

        }

        void dealGrid_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            dealGrid.PageIndex = e.NewPageIndex;

            BindData();
        }

        private void BindData() {

            //DealTableAdapter dealAdapter = new DealTableAdapter();
            //DollarSaverDB.DealDataTable deals = dealAdapter.GetByStation(StationId);

            CertificateTableAdapter certificateAdapter = new CertificateTableAdapter();
            DollarSaverDB.CertificateDataTable certificates = certificateAdapter.GetActive(StationId);

            certificates.DefaultView.Sort = "OnSaleDate DESC";

            //certificates = (DollarSaverDB.CertificateDataTable) certificates.DefaultView.ToTable();

            if (certificates.Count > 0) {
                //startDate = deals[0].StartDate;

                DollarSaverDB.CertificateDataTable c = new DollarSaverDB.CertificateDataTable();

                foreach (DataRow row in certificates.DefaultView.ToTable().Rows) {
                    c.ImportRow(row);
                }

                dealHolder.Visible = true;
                noDealHolder.Visible = false;
                //dealGrid.DataSource = ((DollarSaverDB.CertificateDataTable)certificates.DefaultView.ToTable()).Rows;
                dealGrid.DataSource = c.Rows;
                
                dealGrid.DataBind();
            } else {
                dealHolder.Visible = false;
                noDealHolder.Visible = true;
            }

        }

        void dealGrid_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {
                //DollarSaverDB.DealRow deal = (DollarSaverDB.DealRow)e.Row.DataItem;
                //LinkButton deleteButton = (LinkButton)e.Row.FindControl("deleteButton");

                //DollarSaverDB.CertificateRow certificate = (DollarSaverDB.CertificateRow)e.Row.DataItem;

                /*
                if (deal.StartDate > DateTime.Now) {
                    deleteButton.CommandArgument = deal.DealId.ToString();
                    deleteButton.Attributes["onclick"] = "javascript: return confirm('Are you sure want to delete this item?');";
                } else {
                    deleteButton.Visible = false;
                }
                 */
            }
        }


        void saveSettingsButton_Click(object sender, EventArgs e) {

            displayDay = Convert.ToInt32(displayDayList.SelectedValue);
            displayHour = Convert.ToInt32(displayHourList.SelectedValue);
            onSaleDay = Convert.ToInt32(onSaleDayList.SelectedValue);
            onSaleHour = Convert.ToInt32(onSaleHourList.SelectedValue);

            if (displayDay == onSaleDay) {
                ErrorMessage = "Cannot set deal to display and go on sale on the same day";
                return;
            }


            DealSettingsTableAdapter settingsAdapter = new DealSettingsTableAdapter();
            DollarSaverDB.DealSettingsDataTable settingsTable = settingsAdapter.GetDealSettings(StationId);

            if (settingsTable.Count == 1) {
                DollarSaverDB.DealSettingsRow settings = settingsTable[0];

                settings.DisplayDay = displayDay;
                settings.DisplayHour = displayHour;
                settings.OnSaleDay = onSaleDay;
                settings.OnSaleHour = onSaleHour;

                settingsAdapter.Update(settings);

            } else {
                settingsAdapter.Insert(StationId, displayDay, displayHour, onSaleDay, onSaleHour);
            }

            InfoMessage = "Settings Updated";
            Response.Redirect("~/admin/DealOfTheWeek.aspx");
        }

        /*
        void saveButton_Click(object sender, EventArgs e) {

            if (Page.IsValid) {

                DealSettingsTableAdapter settingsAdapter = new DealSettingsTableAdapter();
                DollarSaverDB.DealSettingsDataTable settingsTable = settingsAdapter.GetDealSettings(StationId);

                if (settingsTable.Count == 1) {
                    DollarSaverDB.DealSettingsRow settings = settingsTable[0];

                    displayDay = settings.DisplayDay;
                    displayHour = settings.DisplayHour;
                    onSaleDay = settings.OnSaleDay;
                    onSaleHour = settings.OnSaleHour;
                }

                int certificateId = Convert.ToInt32(certificateList.SelectedValue);
                DateTime startDate = Convert.ToDateTime(dealDateList.SelectedValue + " " + onSaleHour + ":00:00");
                DateTime displayDate = startDate.AddDays(-1 * ((7 + onSaleDay - displayDay) % 7)).AddHours(displayHour - onSaleHour);


                DealTableAdapter dealAdapter = new DealTableAdapter();

                dealAdapter.Insert(StationId, displayDate, startDate, certificateId);

                InfoMessage = "Deal of the Week Added";
                Response.Redirect("~/admin/DealOfTheWeek.aspx");
            }

        }
        */

        void cancelButton_Click(object sender, EventArgs e) {
            Response.Redirect("~/admin/Default.aspx");
        }
    }
}
