using System;
using System.Web.UI;
using System.Web.UI.WebControls;

using DollarSaver.Core.Common;
using DollarSaver.Core.Data;
using DollarSaver.Core.Data.DollarSaverDBTableAdapters;

namespace DollarSaver.Web.Admin
{

    public partial class OrderList : ManagerPageBase {
        private String searchText = String.Empty;
        private DateTime startDate = new DateTime();
        private DateTime endDate = new DateTime();
        private int advertiserId = 0;
        private int deliveryTypeId = 0;

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
 	
            // fix this mess
            if (ReadOnly) {
                Response.Redirect("~/admin/");
            }

            orderGrid.RowCommand += new GridViewCommandEventHandler(orderGrid_RowCommand);
            //orderGrid.PageIndexChanging += new GridViewPageEventHandler(orderGrid_PageIndexChanging);

            searchButton.Click += new EventHandler(searchButton_Click);

            if (!Page.IsPostBack) {

                advertiserList.DataSource = Station.Advertisers;
                advertiserList.DataTextField = "Name";
                advertiserList.DataValueField = "AdvertiserId";
                advertiserList.DataBind();

                advertiserList.Items.Insert(0, new ListItem("-- All Advertisers --", "0"));
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
                    startDate = TimeZoneInfo.ConvertTime(startDate, Station.StationTimeZoneInfo, TimeZoneInfo.Local);
                } catch {
                    ErrorMessage = "Invalid Start Date";
                }
            }

            if (endDateBox.Text.Trim() != String.Empty) {
                try {
                    endDate = Convert.ToDateTime(endDateBox.Text);
                    endDate = TimeZoneInfo.ConvertTime(endDate, Station.StationTimeZoneInfo, TimeZoneInfo.Local);
                } catch {
                    ErrorMessage = "Invalid End Date";
                }
            }

            advertiserId = Convert.ToInt32(advertiserList.SelectedValue);
            deliveryTypeId = Convert.ToInt32(deliveryTypeList.SelectedValue);

            OrderTableAdapter orderAdapter = new OrderTableAdapter();
            DollarSaverDB.OrderDataTable orders;

            int pageSize = 25;

            int startRow = (OrderGridPageIndex * pageSize) + 1;
            int endRow = (OrderGridPageIndex + 1) * pageSize;

            int? orderCount = 0;

            orders = orderAdapter.Search(StationId, searchText, 
                Globals.ConvertToNull(advertiserId), Globals.ConvertToNull(startDate),
                Globals.ConvertToNull(endDate), Globals.ConvertToNull(deliveryTypeId), 
                startRow, endRow, ref orderCount);

            if (orderCount == 1 && OrderGridPageIndex == 0) {
                Response.Redirect("~/admin/OrderView.aspx?id=" + orders[0].OrderId);
            }

            if (orderCount > 0) {
                orderHolder.Visible = true;
                noDataFoundHolder.Visible = false;
                
                orderGrid.DataSource = orders.Rows;
                orderGrid.DataBind();

                if (orderCount > pageSize) {
                    orderGrid.TopPagerRow.Visible = true;

                    LinkButton previousTopButton = (LinkButton)orderGrid.TopPagerRow.FindControl("previousButton");
                    if (startRow == 1) {
                        previousTopButton.Visible = false;
                    }

                    LinkButton nextTopButton = (LinkButton)orderGrid.TopPagerRow.FindControl("nextButton");
                    if (endRow >= orderCount) {
                        nextTopButton.Visible = false;
                    }

                    DropDownList pageTopList = (DropDownList)orderGrid.TopPagerRow.FindControl("pageList");
                    for (int i = 0; i <= (orderCount / pageSize); i++) {
                        pageTopList.Items.Add(new ListItem((i + 1).ToString(), i.ToString()));
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
        }
         */

        void searchButton_Click(object sender, EventArgs e) {
            OrderGridPageIndex = 0;
            BindOrders();
        }
    }
}