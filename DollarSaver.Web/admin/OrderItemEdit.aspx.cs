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

    public partial class OrderItemEdit : ManagerPageBase {


        private int orderId = 0;
        protected DollarSaverDB.OrderRow order = null;

        protected void Page_Load(object sender, EventArgs e) {

            // fix this mess
            if (ReadOnly) {
                Response.Redirect("~/admin/");
            }

            saveButton.Click += new EventHandler(saveButton_Click);
            saveButton.Attributes["onclick"] = "javascript: return confirm('Are you sure want to return selected items?');";
            cancelButton.Click += new EventHandler(cancelButton_Click);

            lineItemRepeater.ItemDataBound += new RepeaterItemEventHandler(lineItemRepeater_ItemDataBound);

            orderId = GetIdFromQueryString();

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


                    orderIdLabel.Text = order.OrderId.ToString();
                    orderDateLabel.Text = order.AdjustedOrderDate.ToString("ddd MMM dd, yyyy hh:mm tt zzz");
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

                    if (!order.IsGrandTotalNull()) {
                        orderTotalHolder.Visible = true;
                        orderTotalLabel.Text = order.GrandTotal.ToString("$#,0.00");
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

                HiddenField lineItemIdHidden = (HiddenField)e.Item.FindControl("lineItemIdHidden");
                //DropDownList qtyList = (DropDownList)e.Item.FindControl("qtyList");
                Label qtyLabel = (Label)e.Item.FindControl("qtyLabel");
                
                Label totalLabel = (Label)e.Item.FindControl("totalLabel");

                certificateLink.Text = lineItem.ShortName;
                certificateLink.NavigateUrl = "~/admin/CertificateEdit.aspx?id=" + lineItem.CertificateId;
                priceLabel.Text = lineItem.DiscountValue.ToString("$#,0.00");

                lineItemIdHidden.Value = lineItem.OrderLineItemId.ToString();

                /*
                for (int qty=0; qty <= lineItem.Quantity; qty++) {
                    qtyList.Items.Add(qty.ToString());
                }
                qtyList.SelectedValue = lineItem.Quantity.ToString();
                */
                qtyLabel.Text = lineItem.Quantity.ToString();

                totalLabel.Text = lineItem.Total.ToString("$#,0.00");

                GridView numberGrid = (GridView)e.Item.FindControl("numberGrid");

                numberGrid.Visible = true;

                numberGrid.RowDataBound += new GridViewRowEventHandler(numberGrid_RowDataBound);

                numberGrid.DataSource = lineItem.Numbers.Rows;
                numberGrid.DataBind();



            }
        }

        void numberGrid_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {

                DollarSaverDB.CertificateNumberRow certNumber = (DollarSaverDB.CertificateNumberRow)e.Row.DataItem;

                Label nonPrintableLabel = (Label)e.Row.FindControl("nonPrintableLabel");
                HyperLink certificateLink = (HyperLink)e.Row.FindControl("certificateLink");
                HiddenField certNumberIdHidden = (HiddenField)e.Row.FindControl("certNumberIdHidden");

                certNumberIdHidden.Value = certNumber.CertificateNumberId.ToString();

                if (certNumber.LineItem.DeliveryType == DeliveryType.Print) {
                    certificateLink.Visible = true;
                    nonPrintableLabel.Visible = false;

                    String queryString = "id=" + certNumber.CertificateNumberId + "&order_id=" + order.OrderId;

                    String encryptedString = Server.UrlEncode(Cipher.Encrypt(queryString));

                    certificateLink.NavigateUrl = "~/ViewCertificate.aspx?x=" + encryptedString;
                    certificateLink.Text = certNumber.Number;
                } else {
                    certificateLink.Visible = false;
                    nonPrintableLabel.Visible = true;

                    nonPrintableLabel.Text = "Not Printable";

                    if (certNumber.LineItem.DeliveryType == DeliveryType.Ship) {
                        nonPrintableLabel.Text += " - Mail to Customer";
                    } else if (certNumber.LineItem.DeliveryType == DeliveryType.PickUp) {
                        nonPrintableLabel.Text += " - Pick Up";
                    }

                }
            }
        }




        void cancelButton_Click(object sender, EventArgs e) {
            Response.Redirect("~/admin/OrderView.aspx?id=" + orderId);
        }

        void saveButton_Click(object sender, EventArgs e) {

            OrderLineItemTableAdapter lineItemAdapter = new OrderLineItemTableAdapter();
            CertificateNumberTableAdapter certificateNumberAdapter = new CertificateNumberTableAdapter();

            decimal subtotal = 0.0m;

            foreach(RepeaterItem item in lineItemRepeater.Items) {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem) {

                    HiddenField lineItemIdHidden = (HiddenField)item.FindControl("lineItemIdHidden");
                    int lineItemId = Convert.ToInt32(lineItemIdHidden.Value);
                    DollarSaverDB.OrderLineItemRow lineItem = lineItemAdapter.GetOrderLineItem(lineItemId)[0];


                    //DropDownList qtyList = (DropDownList)item.FindControl("qtyList");
                    //int qty = Convert.ToInt32(qtyList.SelectedValue);


                    /*
                    if (lineItem.Quantity != qty) {


                        int returnedQty = lineItem.Quantity - qty;
                        certificateNumberAdapter.Return(lineItem.OrderLineItemId, returnedQty);

                        if (qty == 0) {
                            lineItem.Returned = true;
                            lineItemAdapter.Update(lineItem);
                        } else {
                            lineItem.Quantity = qty;
                            lineItemAdapter.Update(lineItem);

                            DollarSaverDB.OrderLineItemRow returnedItem = order.ReturnedLineItems.GetLineItem(lineItem.CertificateId);

                            if (returnedItem != null) {
                                returnedItem.Quantity += returnedQty;
                                lineItemAdapter.Update(returnedItem);
                            } else {
                                lineItemAdapter.Insert(lineItem.OrderId, -1, returnedQty, lineItem.CertificateId,
                                    lineItem.ShortName, lineItem.Description, lineItem.FaceValue, lineItem.Discount, lineItem.DiscountTypeId, lineItem.Printable, true);
                            }
                        }

                    }
                     */

                    GridView numberGrid = (GridView)item.FindControl("numberGrid");

                    int returnedQty = 0;
                    foreach (GridViewRow certNumberRow in numberGrid.Rows) {

                        CheckBox deleteBox = (CheckBox)certNumberRow.FindControl("deleteBox");

                        if (deleteBox.Checked) {
                            returnedQty++;

                            HiddenField certNumberIdHidden = (HiddenField)certNumberRow.FindControl("certNumberIdHidden");
                            int certNumberId = Convert.ToInt32(certNumberIdHidden.Value);

                            DollarSaverDB.CertificateNumberRow certNumber = 
                                certificateNumberAdapter.GetCertificateNumber(certNumberId)[0];

                            certNumber.SetOrderLineItemIdNull();
                            certificateNumberAdapter.Update(certNumber);

                        }

                    }


                    if(returnedQty > 0) {

                        DollarSaverDB.OrderLineItemRow returnedItem = order.ReturnedLineItems.GetLineItem(lineItem.CertificateId);

                        if (returnedItem != null) {
                            returnedItem.Quantity += returnedQty;
                            lineItemAdapter.Update(returnedItem);

                            if (returnedQty == lineItem.Quantity) {
                                lineItemAdapter.Delete(lineItem.OrderLineItemId);
                            } else {
                                lineItem.Quantity -= returnedQty;
                                lineItemAdapter.Update(lineItem);
                                subtotal += lineItem.Total;
                            }
                        } else {

                            if (returnedQty == lineItem.Quantity) {
                                lineItem.Returned = true;
                                lineItemAdapter.Update(lineItem);
                            } else {
                                lineItemAdapter.Insert(lineItem.OrderId, -1, returnedQty, lineItem.CertificateId,
                                    lineItem.ShortName, lineItem.Description, lineItem.FaceValue, lineItem.Discount, lineItem.DiscountTypeId, lineItem.DeliveryTypeId, lineItem.DeliveryNote, true);

                                lineItem.Quantity -= returnedQty;
                                lineItemAdapter.Update(lineItem);
                                subtotal += lineItem.Total;
                            }
                        }

                    } else {
                        subtotal += lineItem.Total;
                    }


                }
            }

            order.SubTotal = subtotal;
            order.GrandTotal = subtotal;

            OrderTableAdapter orderAdapter = new OrderTableAdapter();
            orderAdapter.Update(order);

            InfoMessage = "Order updated";
            Response.Redirect("~/admin/OrderView.aspx?id=" + orderId);

        }
    }
}