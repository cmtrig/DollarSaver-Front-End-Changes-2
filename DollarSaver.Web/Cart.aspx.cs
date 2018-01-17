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

using com.paypal.sdk.core;
using com.paypal.sdk.services;
using com.paypal.sdk.util;

using DollarSaver.Core.Common;
using DollarSaver.Core.Data;
using DollarSaver.Core.Data.DollarSaverDBTableAdapters;

namespace DollarSaver.Web {
    public partial class CartPage : CookieStationPageBase {

        private bool displayCookieMessage = false;

        public override string PageTitle {
            get {
                return base.PageTitle + " - Your Cart";
            }
        }

        protected override void OnPreInit(EventArgs e) {

            displayCookieMessage = Globals.ConvertToBool(Request.QueryString["no_cookies"]);

            if (!displayCookieMessage) {
                HttpCookie cookie = Request.Cookies.Get(DS_COOKIE_NAME);
                if (cookie == null) {
                    Response.Redirect("~/Cart.aspx?station_id=" + Request.QueryString["station_id"] + "&no_cookies=Y");
                }
            }
            
            base.OnPreInit(e);
        }

        protected void Page_Load(object sender, EventArgs e) {

            lineItemRepeater.ItemDataBound += new RepeaterItemEventHandler(lineItemRepeater_ItemDataBound);
            lineItemRepeater.ItemCommand += new RepeaterCommandEventHandler(lineItemRepeater_ItemCommand);

            updateButton.Click += new EventHandler(updateButton_Click);
            continueButton.Click += new EventHandler(continueButton_Click);

            /*
            if (Order != null && 
                (Order.OrderStatusId != 1 || Order.StationId != StationId)) {
                Order = null;
                Response.Redirect("~/Cart.aspx");
            }*/

            if (displayCookieMessage) {
                itemHolder.Visible = false;
                emptyCartHolder.Visible = false;
                cookieMessageHolder.Visible = true;
                homeLink.NavigateUrl += "?station_id=" + StationId;
                return;
            } else {
                cookieMessageHolder.Visible = false;
            }

            if (!Page.IsPostBack) {


                OrderTableAdapter orderAdapter = new OrderTableAdapter();

                int certificateId = GetCertificateIdFromQueryString();

                if ((Order == null && certificateId > 0) ||
                    (Order != null && Order.OrderStatusId != 1) ||
                    (Order != null && Order.StationId != StationId)) {
                    Order = GenerateOrder();
                }


                if (certificateId > 0) {
                    CertificateTableAdapter certificateAdapter = new CertificateTableAdapter();

                    DollarSaverDB.CertificateDataTable certificateTable = certificateAdapter.GetCertificate(certificateId);

                    if (certificateTable.Count != 1) {
                        Response.Redirect("~/Cart.aspx");
                    }

                    DollarSaverDB.CertificateRow certificate = certificateTable[0];

                    if (certificate.Advertiser.StationId != StationId) {
                        Response.Redirect("~/Cart.aspx");
                    }

                    if (!certificate.IsActive || !certificate.Advertiser.IsActive) {
                        InfoMessage = "Sorry, this certificate is no longer available";
                        Response.Redirect("~/Cart.aspx");
                    }

                    if (certificate.OnSaleDate > DateTime.Now) {
                        InfoMessage = "Sorry, this certificate is not yet available";
                        Response.Redirect("~/Cart.aspx");
                    }

                    int quantity = GetValueFromQueryString("qty");

                    if (quantity == 0) {
                        quantity = 1;
                    }

                    OrderLineItemTableAdapter orderLineItemAdapter = new OrderLineItemTableAdapter();

                    DollarSaverDB.OrderLineItemRow lineItem = Order.LineItems.GetLineItem(certificateId);


                    if (lineItem == null) {

                        quantity = CheckQuantity(quantity, certificate);

                        if (quantity > 0) {

                            Order.LineItems.AddOrderLineItemRow(Order.OrderId, -1, quantity,
                                certificate.CertificateId, certificate.ShortName, certificate.Description,
                                certificate.FaceValue, certificate.Discount, certificate.DiscountTypeId,
                                false, certificate.DeliveryTypeId, certificate.DeliveryNote);

                            orderLineItemAdapter.Update(Order.LineItems);
                        }
                    } else {
                        quantity += lineItem.Quantity;

                        quantity = CheckQuantity(quantity, certificate);

                        if (quantity == 0) {
                            orderLineItemAdapter.Delete(lineItem.OrderLineItemId);
                        } else {
                            lineItem.Quantity = quantity;
                            orderLineItemAdapter.Update(lineItem);
                        }
                    }

                    UpdateLineItemModifiedDate();

                    if (InfoMessage == String.Empty) {
                        InfoMessage = "Certificate(s) added to your cart";
                    }

                    // reload the cart
                    Response.Redirect("~/Cart.aspx");
                }


                if (Order != null && Order.LineItems.Count > 0) {
                    itemHolder.Visible = true;
                    emptyCartHolder.Visible = false;

                    lineItemRepeater.DataSource = Order.LineItems.Rows;
                    lineItemRepeater.DataBind();

                    subTotalLabel.Text = Order.LineItems.SubTotal.ToString("$#,0.00");

                    continueShoppingLink.NavigateUrl = "~/Default.aspx?station_id=" + StationId;
                } else {
                    itemHolder.Visible = false;
                    emptyCartHolder.Visible = true;

                }

            } else {
                // if page is postback, make sure the order is still in the session
                if (Order == null) {
                    InfoMessage = "Sorry, your session has timed out";
                    Response.Redirect("~/Cart.aspx");
                }
            }

        }

        void continueButton_Click(object sender, EventArgs e) {
            //Response.Redirect("~/EnterEmail.aspx");

            String absoluteUrl = String.Empty;

            if (IsDev) {
                absoluteUrl += "http://";
            } else {
                absoluteUrl += "https://";
            } 
            
            absoluteUrl += EnvDomain + DollarSaverAppPath + "/EnterEmail.aspx";

            Response.Redirect(absoluteUrl);

        }


        void lineItemRepeater_ItemCommand(object source, RepeaterCommandEventArgs e) {

            if (e.CommandName == "remove") {
                int lineItemId = Int32.Parse(e.CommandArgument.ToString());

                OrderLineItemTableAdapter lineItemAdapter = new OrderLineItemTableAdapter();
                lineItemAdapter.Delete(lineItemId);
            }

            UpdateLineItemModifiedDate();

            Response.Redirect("~/Cart.aspx");

        }

        void updateButton_Click(object sender, EventArgs e) {

            OrderLineItemTableAdapter lineItemAdapter = new OrderLineItemTableAdapter();

            foreach (RepeaterItem item in lineItemRepeater.Items) {

                HiddenField lineItemIdHidden = (HiddenField)item.FindControl("lineItemIdHidden");
                TextBox qtyBox = (TextBox)item.FindControl("qtyBox");

                int lineItemId = Int32.Parse(lineItemIdHidden.Value);

                DollarSaverDB.OrderLineItemRow lineItem = lineItemAdapter.GetOrderLineItem(lineItemId)[0];

                int newQuantity = lineItem.Quantity;

                try {
                    newQuantity = Int32.Parse(qtyBox.Text.Trim());
                } catch {
                    ErrorMessage = "Invalid quantity entered";
                    return;
                }

                if (newQuantity < 0) {
                    ErrorMessage = "New quantity must be greater than zero";
                    return;
                }


                if (newQuantity > lineItem.Certificate.MaxPurchaseQty && lineItem.Certificate.MaxPurchaseQty > 0) {
                    // set to max purchase qty and continue processing
                    newQuantity = lineItem.Certificate.MaxPurchaseQty;
                    InfoMessage += "Sorry, you can only purchase a maximum of " + lineItem.Certificate.MaxPurchaseQty + " of certifcate " + lineItem.Certificate.AdvertiserName + "<BR />";
                }

                if (newQuantity != lineItem.Quantity && newQuantity >= 0) {

                    if (newQuantity == 0) {
                        lineItemAdapter.Delete(lineItem.OrderLineItemId);
                    } else {
                        newQuantity = CheckQuantity(newQuantity, lineItem.Certificate);

                        if (newQuantity == 0) {
                            lineItemAdapter.Delete(lineItem.OrderLineItemId);
                        } else {
                            lineItem.Quantity = newQuantity;
                            lineItemAdapter.Update(lineItem);
                        }
                    }
                }
            }

            UpdateLineItemModifiedDate();

            if (InfoMessage == String.Empty) {
                InfoMessage = "Cart updated";
            }

            Response.Redirect("~/Cart.aspx");

        }

        void lineItemRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e) {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) {

                DollarSaverDB.OrderLineItemRow lineItem = (DollarSaverDB.OrderLineItemRow)e.Item.DataItem;

                LinkButton removeButton = (LinkButton)e.Item.FindControl("removeButton");
                Label certificateNameLabel = (Label)e.Item.FindControl("certificateNameLabel");
                HiddenField lineItemIdHidden = (HiddenField)e.Item.FindControl("lineItemIdHidden");
                Label priceLabel = (Label)e.Item.FindControl("priceLabel");
                TextBox qtyBox = (TextBox)e.Item.FindControl("qtyBox");
                Label totalLabel = (Label)e.Item.FindControl("totalLabel");

                removeButton.CommandArgument = lineItem.OrderLineItemId.ToString();
                certificateNameLabel.Text = lineItem.Certificate.AdvertiserName;
                lineItemIdHidden.Value = lineItem.OrderLineItemId.ToString();
                priceLabel.Text = lineItem.DiscountValue.ToString("$#,0.00");
                qtyBox.Text = lineItem.Quantity.ToString();
                totalLabel.Text = lineItem.Total.ToString("$#,0.00");

            }
        }

        private void UpdateLineItemModifiedDate() {
            OrderTableAdapter orderAdapter = new OrderTableAdapter();

            Order.LineItemModifiedDate = DateTime.Now;
            orderAdapter.Update(Order);
        }


        private DollarSaverDB.OrderRow GenerateOrder() {

            OrderTableAdapter orderAdapter = new OrderTableAdapter();
            DollarSaverDB.OrderRow newOrder = orderAdapter.GenerateOrder(StationId)[0];

            String customerIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (customerIpAddress == null || customerIpAddress == String.Empty) {
                customerIpAddress = Request.ServerVariables["REMOTE_ADDR"];
            }

            if (customerIpAddress != null) {

                if (customerIpAddress.Contains(",")) {
                    char[] seperator = { ',' };
                    String[] ipAddresses = customerIpAddress.Split(seperator);
                    customerIpAddress = ipAddresses[0];
                }

                newOrder.IPAddress = customerIpAddress;

                orderAdapter.Update(newOrder);
            }

            return newOrder;
        }



        protected int GetCertificateIdFromQueryString() {

            String idString = Request.QueryString["cert_id"];

            int id = 0;

            try {
                id = Int32.Parse(idString);
            } catch { }

            return id;
        }

        private int CheckQuantity(int quantity, DollarSaverDB.CertificateRow certificate) {

            if (certificate.MinPurchaseQty > 1) {

                if (quantity < certificate.MinPurchaseQty) {
                    quantity = certificate.MinPurchaseQty;
                    InfoMessage = certificate.AdvertiserName + " has a minimum purchase quantity of " + certificate.MinPurchaseQty;
                }

            }

            if (certificate.MaxPurchaseQty > 0) {

                if (quantity > certificate.MaxPurchaseQty) {
                    quantity = certificate.MaxPurchaseQty;
                    InfoMessage = certificate.AdvertiserName + " has a maximum purchase quantity of " + certificate.MaxPurchaseQty;
                }
            }


            if (quantity > certificate.QtyRemaining) {
                quantity = certificate.QtyRemaining;

                if (certificate.QtyRemaining == 0) {
                    InfoMessage = "Sorry, " + certificate.AdvertiserName + " is sold out";
                } else if (certificate.QtyRemaining == 1) {
                    InfoMessage = "Sorry, there is only 1 of " + certificate.AdvertiserName + " remaining";
                } else {
                    InfoMessage = "Sorry, there are only " + certificate.QtyRemaining + " of " + certificate.AdvertiserName + " remaining";
                }
            }

            return quantity;
        }
    }
}
