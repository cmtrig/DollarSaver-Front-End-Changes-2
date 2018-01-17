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

namespace DollarSaver.Web.Admin.Super {

    public partial class Default : SuperAdminPageBase {


        private DateTime now = DateTime.Now;
        private decimal sumTotal1 = 0.0m;
        private decimal sumTotal2 = 0.0m;
        private decimal sumTotal3 = 0.0m;

        protected void Page_Load(object sender, EventArgs e) {

            //this.Header.Controls.Add(new LiteralControl("<meta http-equiv=\"refresh\" content=\"60\" />"));

            stationGrid.RowDataBound += new GridViewRowEventHandler(stationGrid_RowDataBound);
            mainTimer.Tick += new EventHandler<EventArgs>(mainTimer_Tick);

            if (!Page.IsPostBack) { // &&
                //!(ScriptManager.GetCurrent(this.Page) != null && ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)) {
                //AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();

                // associate the trigger with the drop down
                //trigger.ControlID = this.Master.FindControl("mainTimer").UniqueID;
                //trigger.EventName = "Tick";
                //mainPanel.Triggers.Add(trigger);

                BindData();
            }

        }

        private void BindData() {
            timerBottomLabel.Text = now.ToString("MM/dd/yyyy hh:mm:ss tt");

            bool inactive = Globals.ConvertToBool(Request.QueryString["inactive"]);
            DollarSaverDB.StationDataTable stations;

            if (inactive) {
                mainTimer.Enabled = false;

                activeLinkHolder.Visible = true;
                inactiveLinkHolder.Visible = false;
                headerCell.Attributes["class"] = "inactive_header";
                headerCell.InnerText = "Inactive Stations";

                recentOrderHolder.Visible = false;

                StationTableAdapter stationAdapter = new StationTableAdapter();
                stations = stationAdapter.GetInactive();

            } else {
                activeLinkHolder.Visible = false;
                inactiveLinkHolder.Visible = true;

                StationTableAdapter stationAdapter = new StationTableAdapter();
                stations = stationAdapter.GetActive();

                recentOrderHolder.Visible = true;
                OrderTableAdapter orderAdapter = new OrderTableAdapter();
                DollarSaverDB.OrderDataTable recentOrders = orderAdapter.GetSuperRecent(15);
                recentOrderGrid.DataSource = recentOrders.Rows;
                recentOrderGrid.DataBind();
            }

            if (stations.Count > 0) {
                stationHolder.Visible = true;
                noStationsHolder.Visible = false;

                stationGrid.DataSource = stations.Rows;
                stationGrid.DataBind();
            } else {
                stationHolder.Visible = false;
                noStationsHolder.Visible = true;
            }

        }

        void mainTimer_Tick(object sender, EventArgs e) {

            now = DateTime.Now;
            sumTotal1 = 0.0m;
            sumTotal2 = 0.0m;
            sumTotal3 = 0.0m;

            BindData();
        }

        void stationGrid_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {
                DollarSaverDB.StationRow station = (DollarSaverDB.StationRow)e.Row.DataItem;

                HyperLink stationLink = (HyperLink)e.Row.FindControl("stationLink");


                String stationUrl = "http://";
                
                String EnvDomain;
                if (IsDev) {
                    EnvDomain = "localhost" + (Request.Url.Port != 80 ? ":" + Request.Url.Port : String.Empty);
                } else {
                    EnvDomain = "dollarsavershow.com";
                }

                if (station != null && !station.IsSubdomainNull() && station.Subdomain != String.Empty) {
                    stationUrl += station.Subdomain + "." + EnvDomain + ResolveUrl("~/Default.aspx");
                } else {
                    stationUrl += EnvDomain + ResolveUrl("~/Default.aspx?station_id=" + station.StationId);
                }

                stationLink.NavigateUrl = stationUrl;

                OrderTableAdapter orderAdapter = new OrderTableAdapter();

                Label total1Label = (Label)e.Row.FindControl("total1Label");
                Label total2Label = (Label)e.Row.FindControl("total2Label");
                Label total3Label = (Label)e.Row.FindControl("total3Label");

                // total 1
                DateTime end1Date = now;
                DateTime start1Date = new DateTime(end1Date.Year, end1Date.Month, end1Date.Day, 0, 0, 0, 0);

                decimal total1 = Convert.ToDecimal(orderAdapter.GetSumTotal(station.StationId, start1Date, end1Date));
                sumTotal1 += total1;
                total1Label.Text = total1.ToString("c");

                // total 2
                DateTime end2Date = now;
                DateTime start2Date = new DateTime(end2Date.Year, end2Date.Month, 1, 0, 0, 0);

                decimal total2 = Convert.ToDecimal(orderAdapter.GetSumTotal(station.StationId, start2Date, end2Date));
                sumTotal2 += total2;
                total2Label.Text = total2.ToString("c");

                // total 3
                DateTime end3Date = now;
                DateTime start3Date = new DateTime(end3Date.Year, 1, 1, 0, 0, 0);

                decimal total3 = Convert.ToDecimal(orderAdapter.GetSumTotal(station.StationId, start3Date, end3Date));
                sumTotal3 += total3;
                total3Label.Text = total3.ToString("c");
            } else if (e.Row.RowType == DataControlRowType.Footer) {

                Label sumTotal1Label = (Label)e.Row.FindControl("sumTotal1Label");
                Label sumTotal2Label = (Label)e.Row.FindControl("sumTotal2Label");
                Label sumTotal3Label = (Label)e.Row.FindControl("sumTotal3Label");

                sumTotal1Label.Text = sumTotal1.ToString("c");
                sumTotal2Label.Text = sumTotal2.ToString("c");
                sumTotal3Label.Text = sumTotal3.ToString("c");

            }
        }

    }
}