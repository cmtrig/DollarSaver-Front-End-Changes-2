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
using DollarSaver.Core.Encryption;

namespace DollarSaver.Web.Admin {

    public partial class OrderEdit : ManagerPageBase {

        private int orderId = 0;
        protected DollarSaverDB.OrderRow order = null;


        protected void Page_Load(object sender, EventArgs e) {

            // fix this mess
            if (ReadOnly) {
                Response.Redirect("~/admin/");
            }

            saveButton.Click += new EventHandler(saveButton_Click);
            cancelButton.Click += new EventHandler(cancelButton_Click);

            //lineItemRepeater.ItemDataBound += new RepeaterItemEventHandler(lineItemRepeater_ItemDataBound);

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

                StateTableAdapter stateAdapter = new StateTableAdapter();

                DollarSaverDB.StateDataTable states = stateAdapter.GetStates();


                stateList.DataSource = states.Rows;
                stateList.DataTextField = "Summary";
                stateList.DataValueField = "StateCode";
                stateList.DataBind();

                //for (int hour = 1; hour <= 12; hour++) {
                //    hourList.Items.Add(new ListItem(hour.ToString("00")));
                //}

                //for (int minute = 0; minute <= 59; minute++) {
                //    minuteList.Items.Add(new ListItem(minute.ToString("00")));
                //}

                if (order != null) {

                    orderIdLabel.Text = order.OrderId.ToString();
                    statusLabel.Text = order.Status;

                    if (!order.IsOrderDateNull()) {
                        orderDateBox.Text = order.AdjustedOrderDate.ToString();
                        //hourList.SelectedValue = order.AdjustedOrderDate.Hour.ToString("00");
                        //minuteList.SelectedValue = order.AdjustedOrderDate.Minute.ToString("00");
                        //amPmList.SelectedValue = order.AdjustedOrderDate.ToString("tt");
                    }

                    if (!order.IsShippingEmailNull()) {
                        emailBox.Text = order.ShippingEmail;
                    }

                    if (!order.IsBillingFirstNameNull()) {
                        firstNameBox.Text = order.BillingFirstName;
                    }

                    if (!order.IsBillingLastNameNull()) {
                        lastNameBox.Text = order.BillingLastName;
                    }

                    if (!order.IsBillingAddress1Null()) {
                        address1Box.Text = order.BillingAddress1;
                    }

                    if (!order.IsBillingAddress2Null()) {
                        address2Box.Text = order.BillingAddress2;
                    }

                    if (!order.IsBillingCityNull()) {
                        cityBox.Text = order.BillingCity;
                    }

                    if (!order.IsBillingStateCodeNull()) {
                        stateList.SelectedValue = order.BillingStateCode;
                    }

                    if (!order.IsBillingZipCodeNull()) {
                        zipCodeBox.Text = order.BillingZipCode;
                    }
                    
                    if (!order.IsBillingPhoneNull()) {
                        phoneNumberBox.Text = order.BillingPhone;
                    }

                    if (order.ShippingRequired) {
                        shippingHolder.Visible = true;

                        shippingStateList.DataSource = states.Rows;
                        shippingStateList.DataTextField = "StateCode";
                        shippingStateList.DataValueField = "StateCode";
                        shippingStateList.DataBind();

                        if (!order.IsShippingFirstNameNull()) {
                            shippingFirstNameBox.Text = order.ShippingFirstName;
                        }

                        if (!order.IsShippingLastNameNull()) {
                            shippingLastNameBox.Text = order.ShippingLastName;
                        }

                        if (!order.IsShippingAddress1Null()) {
                            shippingAddress1Box.Text = order.ShippingAddress1;
                        }

                        if (!order.IsShippingAddress2Null()) {
                            shippingAddress2Box.Text = order.ShippingAddress2;
                        }

                        if (!order.IsShippingCityNull()) {
                            shippingCityBox.Text = order.ShippingCity;
                        }

                        if (!order.IsShippingStateCodeNull()) {
                            shippingStateList.SelectedValue = order.ShippingStateCode;
                        }

                        if (!order.IsShippingZipCodeNull()) {
                            shippingZipCodeBox.Text = order.ShippingZipCode;
                        }

                        if (!order.IsShippingPhoneNull()) {
                            shippingPhoneNumberBox.Text = order.ShippingPhone;
                        }
                    } else {
                        shippingHolder.Visible = false;
                    }
                     

                    //lineItemRepeater.DataSource = order.LineItems.Rows;
                    //lineItemRepeater.DataBind();

                    //if (!order.IsGrandTotalNull()) {
                    //    orderTotalHolder.Visible = true;
                    //    orderTotalLabel.Text = order.GrandTotal.ToString("$#,0.00");
                    //} else {
                    //    orderTotalHolder.Visible = false;
                    //}


                } else {
                    Response.Redirect("~/admin/");
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
                    numberGrid.Visible = false;

                    nonPrintableLabel.Text = "Not Printable";

                    if (lineItem.DeliveryType == DeliveryType.Ship) {
                        nonPrintableLabel.Text += " - Mail to Customer";
                    } else if (lineItem.DeliveryType == DeliveryType.PickUp) {
                        nonPrintableLabel.Text += " - Pick Up<BR>" + Server.HtmlEncode(lineItem.DeliveryNote).Replace(Environment.NewLine, "<BR>");
                    }
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


        void saveButton_Click(object sender, EventArgs e) {

            if (Page.IsValid) {

                String orderDateStr = orderDateBox.Text.Trim();
                //String hour = hourList.SelectedValue;
                //String minute = minuteList.SelectedValue;
                //String amPm = amPmList.SelectedValue;

                String emailAddress = emailBox.Text.Trim();
                String firstName = firstNameBox.Text.Trim();
                String lastName = lastNameBox.Text.Trim();
                String address1 = address1Box.Text.Trim();
                String address2 = address2Box.Text.Trim();
                String city = cityBox.Text.Trim();
                String stateCode = stateList.SelectedValue;
                String zipCode = zipCodeBox.Text.Trim();
                String phoneNumber = phoneNumberBox.Text.Trim();

                DateTime orderDate = DateTime.Now;
                try {
                    orderDate = Convert.ToDateTime(orderDateStr);
                } catch {
                    ErrorMessage = "On Sale Date must by in the format MM/DD/YYYY";
                    return;
                }

                orderDate = TimeZoneInfo.ConvertTime(orderDate, Station.StationTimeZoneInfo, TimeZoneInfo.Local);

                if (emailAddress == String.Empty) {
                    ErrorMessage = "E-mail is required";
                    return;
                }

                if (!Regex.IsMatch(emailAddress, @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$")) {
                    ErrorMessage = "E-mail address is not valid";
                    return;
                }

                if (firstName == String.Empty) {
                    ErrorMessage = "First Name is required";
                    return;
                }
                if (lastName == String.Empty) {
                    ErrorMessage = "Last Name is required";
                    return;
                }
                if (address1 == String.Empty) {
                    ErrorMessage = "Address is required";
                    return;
                }
                if (city == String.Empty) {
                    ErrorMessage = "City is required";
                    return;
                }
                if (stateCode == String.Empty) {
                    ErrorMessage = "State is required";
                    return;
                }
                if (zipCode == String.Empty) {
                    ErrorMessage = "Zip Code is required";
                    return;
                }
                if (phoneNumber == String.Empty) {
                    ErrorMessage = "Phone Number is required";
                    return;
                }

                if (order.ShippingRequired) {

                    String shippingFirstName = shippingFirstNameBox.Text.Trim();
                    String shippingLastName = shippingLastNameBox.Text.Trim();
                    String shippingAddress1 = shippingAddress1Box.Text.Trim();
                    String shippingAddress2 = shippingAddress2Box.Text.Trim();
                    String shippingCity = shippingCityBox.Text.Trim();
                    String shippingStateCode = shippingStateList.SelectedValue;
                    String shippingZipCode = shippingZipCodeBox.Text.Trim();
                    String shippingPhoneNumber = shippingPhoneNumberBox.Text.Trim();

                    if (shippingFirstName == String.Empty) {
                        ErrorMessage = "Shipping First Name is required";
                        return;
                    }
                    if (shippingLastName == String.Empty) {
                        ErrorMessage = "Shipping Last Name is required";
                        return;
                    }
                    if (shippingAddress1 == String.Empty) {
                        ErrorMessage = "Shipping Address is required";
                        return;
                    }
                    if (shippingCity == String.Empty) {
                        ErrorMessage = "Shipping City is required";
                        return;
                    }
                    if (shippingStateCode == String.Empty) {
                        ErrorMessage = "Shipping State is required";
                        return;
                    }
                    if (shippingZipCode == String.Empty) {
                        ErrorMessage = "Shipping Zip Code is required";
                        return;
                    }
                    if (shippingPhoneNumber == String.Empty) {
                        ErrorMessage = "Shipping Phone Number is required";
                        return;
                    }

                    order.ShippingFirstName = shippingFirstName;
                    order.ShippingLastName = shippingLastName;
                    order.ShippingAddress1 = shippingAddress1;
                    if (shippingAddress2 != String.Empty) {
                        order.ShippingAddress2 = shippingAddress2;
                    } else {
                        order.SetShippingAddress2Null();
                    }
                    order.ShippingCity = shippingCity;
                    order.ShippingStateCode = shippingStateCode;
                    order.ShippingZipCode = shippingZipCode;
                    order.ShippingPhone = shippingPhoneNumber;
                }

                order.OrderDate = orderDate;
                order.BillingFirstName = firstName;
                order.BillingLastName = lastName;
                order.BillingAddress1 = address1;
                if (address2 != String.Empty) {
                    order.BillingAddress2 = address2;
                } else {
                    order.SetBillingAddress2Null();
                }
                order.BillingCity = city;
                order.BillingStateCode = stateCode;
                order.BillingZipCode = zipCode;
                order.BillingPhone = phoneNumber;

                order.ShippingEmail = emailAddress;





                OrderTableAdapter orderAdapter = new OrderTableAdapter();
                orderAdapter.Update(order);

                InfoMessage = "Order updated";
                Response.Redirect("~/admin/OrderView.aspx?id=" + orderId);
            }
        }

        void cancelButton_Click(object sender, EventArgs e) {
            Response.Redirect("~/admin/OrderView.aspx?id=" + orderId);
        }


    }




}