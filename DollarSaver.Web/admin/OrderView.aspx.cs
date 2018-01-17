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

namespace DollarSaver.Web.Admin {

    public partial class OrderView : ManagerPageBase {

        
        protected DollarSaverDB.OrderRow order = null;

        protected void Page_Load(object sender, EventArgs e) {

            // fix this mess
            if (ReadOnly) {
                Response.Redirect("~/admin/");
            }

            lineItemRepeater.ItemDataBound += new RepeaterItemEventHandler(lineItemRepeater_ItemDataBound);
            returnedItemRepeater.ItemDataBound += new RepeaterItemEventHandler(returnedItemRepeater_ItemDataBound);

            int orderId = GetIdFromQueryString();

            if (orderId > 0) {

                OrderTableAdapter orderAdapter = new OrderTableAdapter();

                DollarSaverDB.OrderDataTable orderTable = orderAdapter.GetOrder(orderId);

                if (orderTable.Count == 1) {
                    order = orderTable[0];

                } else {
                    Response.Redirect("~/admin/OrderList.aspx");
                }

                if (order.StationId != StationId) {
                    Response.Redirect("~/admin/OrderList.aspx");
                }


            } else {
                Response.Redirect("~/admin/OrderList.aspx");
            }
        }



        protected override void OnPreRender(EventArgs e) {
            base.OnPreRender(e);


            if (!Page.IsPostBack) {


                if (order != null) {
                    orderHolder.Visible = true;
                    notFoundHolder.Visible = false;

                    if(!ReadOnly) {
                        editLinkHolder.Visible = true;
                        editItemHolder.Visible = true;
                        editLink.NavigateUrl = "~/admin/OrderEdit.aspx?id=" + order.OrderId;
                        editItemLink.NavigateUrl = "~/admin/OrderItemEdit.aspx?id=" + order.OrderId;
                    } else {
                        editLinkHolder.Visible = false;
                        editItemHolder.Visible = false;
                    }

                    orderIdLabel.Text = order.OrderId.ToString();
                    orderDateLabel.Text = order.AdjustedOrderDate.ToString("ddd MMM dd, yyyy hh:mm tt");
                    statusLabel.Text = order.Status;
                    nameLabel.Text = Server.HtmlEncode(order.BillingName);
                    emailLabel.Text = Server.HtmlEncode(order.ShippingEmail);
                    addressLabel.Text = Server.HtmlEncode(order.BillingAddress).Replace(Environment.NewLine, "<BR>");
                    phoneLabel.Text = Server.HtmlEncode(order.BillingPhone);

                    if (order.ShippingRequired) {
                        shippingHolder.Visible = true;
                        shippingInfoLabel.Text = Server.HtmlEncode(order.ShippingInfo).Replace(Environment.NewLine, "<BR>");
                    } else {
                        shippingHolder.Visible = false;
                    }

                    lineItemRepeater.DataSource = order.LineItems.Rows;
                    lineItemRepeater.DataBind();

                    if (order.ReturnedLineItems.Count > 0) {
                        returnedItemHolder.Visible = true;
                        returnedItemRepeater.DataSource = order.ReturnedLineItems.Rows;
                        returnedItemRepeater.DataBind();
                    } else {
                        returnedItemHolder.Visible = false;
                    }

                    if (!order.IsGrandTotalNull()) {
                        orderTotalHolder.Visible = true;
                        orderTotalLabel.Text = "$" + order.GrandTotal.ToString("#,0.00");
                    } else {
                        orderTotalHolder.Visible = false;
                    }


                } else {
                    orderHolder.Visible = false;
                    notFoundHolder.Visible = true;
                }
            }

        }


        void lineItemRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e) {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) {

                DollarSaverDB.OrderLineItemRow lineItem = (DollarSaverDB.OrderLineItemRow)e.Item.DataItem;

                HyperLink certificateLink = (HyperLink)e.Item.FindControl("certificateLink");
                Label priceLabel = (Label)e.Item.FindControl("priceLabel");
                Label qtyLabel = (Label)e.Item.FindControl("qtyLabel");
                Label totalLabel = (Label)e.Item.FindControl("totalLabel");

                certificateLink.Text = lineItem.ShortName;
                certificateLink.NavigateUrl = "~/admin/CertificateEdit.aspx?id=" + lineItem.CertificateId;
                priceLabel.Text = lineItem.DiscountValue.ToString("$#,0.00");
                qtyLabel.Text = lineItem.Quantity.ToString();
                totalLabel.Text = lineItem.Total.ToString("$#,0.00");

                GridView numberGrid = (GridView)e.Item.FindControl("numberGrid");
                Label nonPrintableLabel = (Label)e.Item.FindControl("nonPrintableLabel");
                if (lineItem.DeliveryType == DeliveryType.Print) {
                    nonPrintableLabel.Visible = false;
                    numberGrid.Visible = true;

                    numberGrid.RowDataBound += new GridViewRowEventHandler(numberGrid_RowDataBound);

                    numberGrid.DataSource = lineItem.Numbers.Rows;
                    numberGrid.DataBind();
                } else {
                    nonPrintableLabel.Visible = true;

                    nonPrintableLabel.Text = "Not Printable";

                    if (lineItem.DeliveryType == DeliveryType.Ship) {
                        nonPrintableLabel.Text += " - Mail to Customer";
                    } else if (lineItem.DeliveryType == DeliveryType.PickUp) {
                        nonPrintableLabel.Text += " - Pick Up<BR>" + Server.HtmlEncode(lineItem.DeliveryNote).Replace(Environment.NewLine, "<BR>");
                    }
                    
                    numberGrid.Visible = false;
                }

            }
        }

        void numberGrid_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {

                DollarSaverDB.CertificateNumberRow certNumber = (DollarSaverDB.CertificateNumberRow)e.Row.DataItem;

                HyperLink certificateLink = (HyperLink)e.Row.FindControl("certificateLink");

                String queryString = "id=" + certNumber.CertificateNumberId + "&order_id=" + order.OrderId;

                String encryptedString = Server.UrlEncode(Cipher.Encrypt(queryString));

                certificateLink.NavigateUrl = "~/ViewCertificate.aspx?x=" + encryptedString;
                certificateLink.Text = certNumber.Number;
            }
        }


        void returnedItemRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e) {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) {

                DollarSaverDB.OrderLineItemRow returnedItem = (DollarSaverDB.OrderLineItemRow)e.Item.DataItem;

                HyperLink certificateLink = (HyperLink)e.Item.FindControl("certificateLink");
                Label priceLabel = (Label)e.Item.FindControl("priceLabel");
                Label qtyLabel = (Label)e.Item.FindControl("qtyLabel");
                Label totalLabel = (Label)e.Item.FindControl("totalLabel");

                certificateLink.Text = returnedItem.ShortName;
                certificateLink.NavigateUrl = "~/admin/CertificateEdit.aspx?id=" + returnedItem.CertificateId;
                priceLabel.Text = returnedItem.DiscountValue.ToString("$#,0.00");
                qtyLabel.Text = returnedItem.Quantity.ToString();
                totalLabel.Text = returnedItem.Total.ToString("$#,0.00");

            }
        }


    }
}