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
using DollarSaver.Core.Encryption;
using DollarSaver.Web.controls;

namespace DollarSaver.Web {

    public partial class Confirmation : SecureStationPageBase {

        public override string PageTitle {
            get {
                return base.PageTitle + " - Confirmation";
            }
        }

        protected void Page_Load(object sender, EventArgs e) {
            certificateRepeater.ItemDataBound += new RepeaterItemEventHandler(certificateRepeater_ItemDataBound);

            pickUpRepeater.ItemDataBound += new RepeaterItemEventHandler(pickUpRepeater_ItemDataBound);
            shippingRepeater.ItemDataBound += new RepeaterItemEventHandler(shippingRepeater_ItemDataBound);

            if (IsDev && Request.QueryString["order_id"] != null) {
                try {
                    int orderId = Convert.ToInt32(Request.QueryString["order_id"]);
                    if (orderId > 0) {
                        OrderTableAdapter orderAdapter = new OrderTableAdapter();
                        DollarSaverDB.OrderDataTable orderTable = orderAdapter.GetOrder(orderId);
                        if (orderTable.Count == 1) {
                            Order = orderTable[0];
                        }
                    }
                } catch { }
            }



            if (Order == null || Order.StationId != StationId || Order.OrderStatusId != (int)OrderStatus.Complete) {
                OrderId = 0;
                Response.Redirect("~/Default.aspx");
            }

            if (!Page.IsPostBack) {


            }
        }

        protected override void OnPreRender(EventArgs e) {
            base.OnPreRender(e);

            if (Order.PrintItems.Count > 0) {
                printableHolder.Visible = true;
                certificateRepeater.DataSource = Order.PrintItems.Rows;
                certificateRepeater.DataBind();
            } else {
                printableHolder.Visible = false;
            }

            if (Order.PickUpRequired) {
                pickUpHolder.Visible = true;
                pickUpRepeater.DataSource = Order.PickUpItems.Rows;
                pickUpRepeater.DataBind();
            } else {
                pickUpHolder.Visible = false;
            }

            if (Order.ShippingRequired) {
                shippingHolder.Visible = true;
                shippingRepeater.DataSource = Order.ShipItems.Rows;
                shippingRepeater.DataBind();

                shippingInfoLabel.Text = Server.HtmlEncode(Order.ShippingInfo).Replace(Environment.NewLine, "<BR>");
            } else {
                shippingHolder.Visible = false;
            }

            orderNumberLabel.Text = Order.OrderId.ToString();
            lineItemDetail.Order = Order;
        }


        void certificateRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e) {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) {
                DollarSaverDB.OrderLineItemRow lineItem = (DollarSaverDB.OrderLineItemRow)e.Item.DataItem;

                Repeater numberRepeater = (Repeater)e.Item.FindControl("numberRepeater");

                numberRepeater.ItemDataBound += new RepeaterItemEventHandler(numberRepeater_ItemDataBound);

                numberRepeater.DataSource = lineItem.Numbers.Rows;
                numberRepeater.DataBind();
            }

        }

        void numberRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e) {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) {
                DollarSaverDB.CertificateNumberRow certNumber = (DollarSaverDB.CertificateNumberRow)e.Item.DataItem;

                Label certificateNameLabel = (Label)e.Item.FindControl("certificateNameLabel");
                Label numberLabel = (Label) e.Item.FindControl("numberLabel");
                HyperLink certificateLink = (HyperLink) e.Item.FindControl("certificateLink");

                certificateNameLabel.Text = certNumber.LineItem.Certificate.Advertiser.Name;
                numberLabel.Text = certNumber.Number;

                String queryString = "id=" + certNumber.CertificateNumberId + "&order_id=" + Order.OrderId;

                String encryptedString = Server.UrlEncode(Cipher.Encrypt(queryString));


                certificateLink.NavigateUrl = "~/ViewCertificate.aspx?x=" + encryptedString + "&backBtn=Y";
            }

        }


        void pickUpRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e) {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) {

                DollarSaverDB.OrderLineItemRow lineItem = (DollarSaverDB.OrderLineItemRow)e.Item.DataItem;

                Label certificateNameLabel = (Label)e.Item.FindControl("certificateNameLabel");
                Label deliveryNoteLabel = (Label)e.Item.FindControl("deliveryNoteLabel");
                Label qtyLabel = (Label)e.Item.FindControl("qtyLabel");

                certificateNameLabel.Text = lineItem.Certificate.AdvertiserName;
                deliveryNoteLabel.Text = Server.HtmlEncode(lineItem.DeliveryNote).Replace(Environment.NewLine, "<BR>");
                qtyLabel.Text = lineItem.Quantity.ToString();
            }
        }

        void shippingRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e) {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) {

                DollarSaverDB.OrderLineItemRow lineItem = (DollarSaverDB.OrderLineItemRow)e.Item.DataItem;

                Label certificateNameLabel = (Label)e.Item.FindControl("certificateNameLabel");
                Label qtyLabel = (Label)e.Item.FindControl("qtyLabel");

                certificateNameLabel.Text = lineItem.Certificate.AdvertiserName;
                qtyLabel.Text = lineItem.Quantity.ToString();
            }
        }


    }

}
