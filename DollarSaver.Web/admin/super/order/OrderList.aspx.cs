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

namespace DollarSaver.Web.Admin.Super.OrderAdmin {

    public partial class OrderList : SuperAdminPageBase {
        private String searchText = String.Empty;
        private DateTime startDate = new DateTime();
        private DateTime endDate = new DateTime();
        private int stationId = 0;

        private int OrderGridPageIndex {
            get {
                if (ViewState["OrderGridPageIndex"] != null) {
                    return (int)ViewState["OrderGridPageIndex"];
                } else {
                    return 0;
                }
            }

            set {
                ViewState["OrderGridPageIndex"] = value;
            }
        }

        protected override void  OnLoad(EventArgs e) {
 	        base.OnLoad(e);

            orderGrid.RowCommand += new GridViewCommandEventHandler(orderGrid_RowCommand);
            //orderGrid.PageIndexChanging += new GridViewPageEventHandler(orderGrid_PageIndexChanging);

            searchButton.Click += new EventHandler(searchButton_Click);

            if (!Page.IsPostBack) {

                StationTableAdapter stationAdapter = new StationTableAdapter();
                DollarSaverDB.StationDataTable stations = stationAdapter.GetActive();

                stationList.DataSource = stations.Rows;
                stationList.DataTextField = "ShortName";
                stationList.DataValueField = "Stationid";
                stationList.DataBind();

                stationList.Items.Insert(0, new ListItem("-- All Stations --", "0"));
            }

        }

        protected override void OnPreRender(EventArgs e) {
            base.OnPreRender(e);

            if (!Page.IsPostBack) {
                BindOrders();
            }
        }

        private void BindOrders() {

            searchText = searchBox.Text.Trim();
            if (searchText.Length > 50) {
                searchText = searchText.Substring(0, 50);
            }

            if (startDateBox.Text.Trim() != String.Empty) {
                try {
                    startDate = Convert.ToDateTime(startDateBox.Text);
                } catch {
                    ErrorMessage = "Invalid Start Date";
                }
            }

            if (endDateBox.Text.Trim() != String.Empty) {
                try {
                    endDate = Convert.ToDateTime(endDateBox.Text);
                } catch {
                    ErrorMessage = "Invalid End Date";
                }
            }

            stationId = Convert.ToInt32(stationList.SelectedValue);

            OrderTableAdapter orderAdapter = new OrderTableAdapter();
            DollarSaverDB.OrderDataTable orders;

            int pageSize = 25;

            int startRow = (OrderGridPageIndex * pageSize) + 1;
            int endRow = (OrderGridPageIndex + 1) * pageSize;

            int? orderCount = 0;

            orders = orderAdapter.SuperSearch(Globals.ConvertToNull(stationId), searchText, 
                Globals.ConvertToNull(startDate), Globals.ConvertToNull(endDate), startRow, endRow, ref orderCount);


            // DataView orderView = new DataView(orders);

            if (orderCount > 0) {
                orderHolder.Visible = true;
                noDataFoundHolder.Visible = false;
                
                orderGrid.DataSource = orders.Rows;
                orderGrid.DataBind();

                if (orderCount > pageSize) {
                    orderGrid.TopPagerRow.Visible = true;
                    //orderGrid.BottomPagerRow.Visible = true;

                    LinkButton previousTopButton = (LinkButton)orderGrid.TopPagerRow.FindControl("previousButton");
                    if (startRow == 1) {
                        previousTopButton.Visible = false;
                    }

                    LinkButton nextTopButton = (LinkButton)orderGrid.TopPagerRow.FindControl("nextButton");
                    if (endRow >= orderCount) {
                        nextTopButton.Visible = false;
                    }

                    DropDownList pageTopList = (DropDownList) orderGrid.TopPagerRow.FindControl("pageList");
                    for (int i = 0; i <= (orderCount / pageSize); i++) {
                        pageTopList.Items.Add(new ListItem((i+1).ToString(), i.ToString()));
                    }

                    pageTopList.SelectedValue = OrderGridPageIndex.ToString();

                } else {
                    orderGrid.TopPagerRow.Visible = false;
                }
            } else {
                orderHolder.Visible = false;
                noDataFoundHolder.Visible = true;
            }
        }

        protected void pageList_SelectedIndexChanged(object sender, EventArgs e) {

            DropDownList pageList = (DropDownList)orderGrid.TopPagerRow.FindControl("pageList");

            OrderGridPageIndex = Convert.ToInt32(pageList.SelectedValue);

            BindOrders();
        }


        void orderGrid_RowCommand(object sender, GridViewCommandEventArgs e) {
            if (e.CommandName == "next") {
                OrderGridPageIndex++;
            } else if (e.CommandName == "previous") {
                OrderGridPageIndex--;
            }

            BindOrders();
        }

        /*
        void orderGrid_PageIndexChanging(object sender, GridViewPageEventArgs e) {

            orderGrid.PageIndex = e.NewPageIndex;

            BindOrders();
        }*/

        void searchButton_Click(object sender, EventArgs e) {
            OrderGridPageIndex = 0;
            BindOrders();
        }
    }
}