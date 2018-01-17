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

using com.paypal.sdk.core;
using com.paypal.sdk.services;
using com.paypal.sdk.util;

using DollarSaver.Core.Encryption;
using DollarSaver.Core.Data;
using DollarSaver.Core.Data.DollarSaverDBTableAdapters;
using DollarSaver.Web.controls;

namespace DollarSaver.Web {

    public partial class EnterEmail : SecureStationPageBase {

        public override string PageTitle {
            get {
                return base.PageTitle + " - Email Info";
            }
        }

        protected void Page_Load(object sender, EventArgs e) {
            checkoutButton.Click += new EventHandler(checkoutButton_Click);
            paypalButton.Click += new ImageClickEventHandler(paypalButton_Click);

            shippingRepeater.ItemDataBound += new RepeaterItemEventHandler(shippingRepeater_ItemDataBound);
            pickUpRepeater.ItemDataBound += new RepeaterItemEventHandler(pickUpRepeater_ItemDataBound);

            if (Order == null || Order.StationId != StationId || Order.OrderStatusId != (int)OrderStatus.New) {
                OrderId = 0;
                Response.Redirect("~/Cart.aspx");
            }

            if (Order.LineItems.Count == 0) {
                Response.Redirect("~/Cart.aspx");
            }

            if (!Page.IsPostBack) {

                if (!Order.IsShippingEmailNull()) {
                    emailBox.Text = Order.ShippingEmail;
                    confirmEmailBox.Text = Order.ShippingEmail;
                } else if (DsCookie["_c"] == "1") {
                    emailBox.Text = Cipher.Decrypt2(DsCookie["_c_e"]);
                    confirmEmailBox.Text = emailBox.Text;
                }

                if (DsCookie["_c"] == "1") {
                    rememberMeBox.Checked = true;
                }

                if (Order.PrintingRequired) {
                    viewAndPrintHolder.Visible = true;
                } else {
                    viewAndPrintHolder.Visible = false;
                }


                if (Order.PickUpRequired) {
                    pickUpHolder.Visible = true;
                    noDeliveryHolder.Visible = false;

                    pickUpRepeater.DataSource = Order.PickUpItems.Rows;
                    pickUpRepeater.DataBind();

                } else {
                    pickUpHolder.Visible = false;
                }

                if (Order.ShippingRequired) {
                    shippingHolder.Visible = true;
                    noDeliveryHolder.Visible = false;
                    shippingRepeater.DataSource = Order.ShipItems.Rows;
                    shippingRepeater.DataBind();
                    

                    StateTableAdapter stateAdapter = new StateTableAdapter();
                    DollarSaverDB.StateDataTable states = stateAdapter.GetStates();
                    stateList.DataSource = states.Rows;
                    stateList.DataTextField = "Summary";
                    stateList.DataValueField = "StateCode";
                    stateList.DataBind();


                    if (!Order.IsShippingFirstNameNull()) {
                        if (!Order.IsShippingFirstNameNull()) {
                            firstNameBox.Text = Order.ShippingFirstName;
                        }

                        if (!Order.IsShippingLastNameNull()) {
                            lastNameBox.Text = Order.ShippingLastName;
                        }

                        if (!Order.IsShippingAddress1Null()) {
                            address1Box.Text = Order.ShippingAddress1;
                        }

                        if (!Order.IsShippingAddress2Null()) {
                            address2Box.Text = Order.ShippingAddress2;
                        }

                        if (!Order.IsShippingCityNull()) {
                            cityBox.Text = Order.ShippingCity;
                        }

                        if (!Order.IsShippingStateCodeNull()) {
                            stateList.SelectedValue = Order.ShippingStateCode;
                        } else if (!Station.IsStateCodeNull()) {
                            stateList.SelectedValue = Station.StateCode;
                        }

                        if (!Order.IsShippingZipCodeNull()) {
                            zipCodeBox.Text = Order.ShippingZipCode;
                        }

                        if (!Order.IsShippingPhoneNull()) {
                            phoneNumberBox.Text = Order.ShippingPhone;
                        }

                    } else if (DsCookie["_c"] == "1") {

                        firstNameBox.Text = Cipher.Decrypt2(DsCookie["_c_sa"]);
                        lastNameBox.Text = Cipher.Decrypt2(DsCookie["_c_sb"]);
                        address1Box.Text = Cipher.Decrypt2(DsCookie["_c_sc"]);
                        address2Box.Text = Cipher.Decrypt2(DsCookie["_c_sd"]);
                        cityBox.Text = Cipher.Decrypt2(DsCookie["_c_se"]);
                        stateList.SelectedValue = Cipher.Decrypt2(DsCookie["_c_sf"]);
                        zipCodeBox.Text = Cipher.Decrypt2(DsCookie["_c_sg"]);
                        phoneNumberBox.Text = Cipher.Decrypt2(DsCookie["_c_sh"]);

                    } else {
                        stateList.SelectedValue = Station.StateCode;
                    }

                } else {
                    shippingHolder.Visible = false;
                }

            }
        }

        void shippingRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e) {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) {

                DollarSaverDB.OrderLineItemRow lineItem = (DollarSaverDB.OrderLineItemRow)e.Item.DataItem;

                Label certificateNameLabel = (Label)e.Item.FindControl("certificateNameLabel");
                Label priceLabel = (Label)e.Item.FindControl("priceLabel");
                Label qtyLabel = (Label)e.Item.FindControl("qtyLabel");
                Label totalLabel = (Label)e.Item.FindControl("totalLabel");

                certificateNameLabel.Text = lineItem.Certificate.AdvertiserName;
                priceLabel.Text = lineItem.DiscountValue.ToString("$0.00");
                qtyLabel.Text = lineItem.Quantity.ToString();
                totalLabel.Text = lineItem.Total.ToString("$0.00");
            }
        }


        void pickUpRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e) {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) {

                DollarSaverDB.OrderLineItemRow lineItem = (DollarSaverDB.OrderLineItemRow)e.Item.DataItem;

                Label certificateNameLabel = (Label)e.Item.FindControl("certificateNameLabel");
                Label deliveryNoteLabel = (Label)e.Item.FindControl("deliveryNoteLabel");
                Label priceLabel = (Label)e.Item.FindControl("priceLabel");
                Label qtyLabel = (Label)e.Item.FindControl("qtyLabel");
                Label totalLabel = (Label)e.Item.FindControl("totalLabel");

                certificateNameLabel.Text = lineItem.Certificate.AdvertiserName;
                deliveryNoteLabel.Text = Server.HtmlEncode(lineItem.DeliveryNote).Replace(Environment.NewLine, "<BR>");
                priceLabel.Text = lineItem.DiscountValue.ToString("$0.00");
                qtyLabel.Text = lineItem.Quantity.ToString();
                totalLabel.Text = lineItem.Total.ToString("$0.00");
            }
        }


        protected override void OnPreRender(EventArgs e) {
            base.OnPreRender(e);

        }

        protected void agreeToTermsBoxCV_ServerValidate(object source, ServerValidateEventArgs args) {
            args.IsValid = agreeToTermsBox.Checked;
        }

        void paypalButton_Click(object sender, ImageClickEventArgs e) {

            if (SaveEmail()) {

                string url = String.Empty;
                string host = String.Empty;

                if (IsDev) {
                    url = Request.Url.Scheme + "://" + Request.Url.Host + ":" + Request.Url.Port;
                    host = "www.sandbox.paypal.com";
                } else {
                    url = "https://dollarsavershow.com";
                    host = "www.paypal.com";
                }

                string returnURL = url + ResolveUrl("ProcessOrder.aspx");
                string cancelURL = url + ResolveUrl("Cart.aspx");

                com.paypal.sdk.services.NVPCallerServices caller = PayPalAPI.PayPalAPIInitialize(IsDev);
                NVPCodec encoder = new NVPCodec();
                encoder["METHOD"] = "SetExpressCheckout";
                encoder["RETURNURL"] = returnURL;
                encoder["CANCELURL"] = cancelURL;
                encoder["AMT"] = Order.LineItems.SubTotal.ToString("0.00");
                encoder["PAYMENTACTION"] = "Sale";
                encoder["CURRENCYCODE"] = "USD";

                encoder["INVNUM"] = Order.OrderId.ToString();
                encoder["NOSHIPPING"] = "1";
                encoder["EMAIL"] = Order.ShippingEmail;
                encoder["HDRIMG"] = "https://dollarsavershow.com/images/ds_banner.gif";
                encoder["HDRBORDERCOLOR"] = "404040";
                encoder["PAYFLOWCOLOR"] = "C0E0A0";

                string paypalRequest = encoder.Encode();
                string paypalResponse = caller.Call(paypalRequest);

                NVPCodec decoder = new NVPCodec();
                decoder.Decode(paypalResponse);

                string strAck = decoder["ACK"];
                if (strAck != null && (strAck == "Success" || strAck == "SuccessWithWarning")) {

                    OrderTableAdapter orderAdapter = new OrderTableAdapter();
                    Order.CheckoutStartDate = DateTime.Now;
                    orderAdapter.Update(Order);

                    Session["TOKEN"] = decoder["TOKEN"];
                    
                    //string host = "www.sandbox.paypal.com";
                    //string host = "www.paypal.com";

                    string paypalUrl = "https://" + host + "/cgi-bin/webscr?cmd=_express-checkout&useraction=commit&noshipping=1&token=" + decoder["TOKEN"];
                    
                    Response.Redirect(paypalUrl, false);
                    return;

                } else {
                    /*
                    string pStrError =
                        "ErrorCode=" + decoder["L_ERRORCODE0"] + "&" +
                        "Desc=" + decoder["L_SHORTMESSAGE0"] + "&" +
                        "Desc2=" + decoder["L_LONGMESSAGE0"];

                    Response.Redirect("APIError.aspx?" + pStrError);
                     */

                    //ErrorMessage = decoder["L_LONGMESSAGE0"];
                    ErrorMessage = "Error! " + decoder["L_LONGMESSAGE0"] + " (" + decoder["L_ERRORCODE0"] + ")";
                }
            }
        }

        void checkoutButton_Click(object sender, EventArgs e) {

            if(SaveEmail()) {

                Response.Redirect("~/Checkout.aspx", false);
                return;
            }
        }
 


        private bool SaveEmail() {
            // Validate email

            String emailAddress = emailBox.Text.Trim();
            String confirmEmailAddress = confirmEmailBox.Text.Trim();

            if (emailAddress == String.Empty) {
                ErrorMessage = "E-mail is required";
                return false;
            }

            if (emailAddress != confirmEmailAddress) {
                ErrorMessage = "E-mail addresse do not match";
                return false;
            }

            if (!Regex.IsMatch(emailAddress, @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$")) {
                ErrorMessage = "E-mail address is not valid";
                return false;
            }

            OrderTableAdapter orderAdapter = new OrderTableAdapter();

            Order.ShippingEmail = emailAddress;
            Order.AgreeToTerms = agreeToTermsBox.Checked;
            Order.AddToMailingList = addToMailingListBox.Checked;


            if (rememberMeBox.Checked) {
                DsCookie["_c"] = "1";
                DsCookie["_c_e"] = Cipher.Encrypt2(emailAddress);
            } else {
                DsCookie["_c"] = "0";
                DsCookie["_c_e"] = String.Empty;
                DsCookie["_c_sa"] = String.Empty;
                DsCookie["_c_sb"] = String.Empty;
                DsCookie["_c_sc"] = String.Empty;
                DsCookie["_c_sd"] = String.Empty;
                DsCookie["_c_se"] = String.Empty;
                DsCookie["_c_sf"] = String.Empty;
                DsCookie["_c_sg"] = String.Empty;
                DsCookie["_c_sh"] = String.Empty;


                DsCookie["_c_ba"] = String.Empty;
                DsCookie["_c_bb"] = String.Empty;
                DsCookie["_c_bc"] = String.Empty;
                DsCookie["_c_bd"] = String.Empty;
                DsCookie["_c_be"] = String.Empty;
                DsCookie["_c_bf"] = String.Empty;
                DsCookie["_c_bg"] = String.Empty;
                DsCookie["_c_bh"] = String.Empty;

            }

            if (Order.ShippingRequired) {

                String firstName = firstNameBox.Text.Trim();
                String lastName = lastNameBox.Text.Trim();
                String address1 = address1Box.Text.Trim();
                String address2 = address2Box.Text.Trim();
                String city = cityBox.Text.Trim();
                String stateCode = stateList.SelectedValue;
                String zipCode = zipCodeBox.Text.Trim();
                String phoneNumber = phoneNumberBox.Text.Trim();

                if (firstName == String.Empty) {
                    ErrorMessage = "First Name is required";
                    return false;
                }
                if (lastName == String.Empty) {
                    ErrorMessage = "Last Name is required";
                    return false;
                }
                if (address1 == String.Empty) {
                    ErrorMessage = "Address is required";
                    return false;
                }
                if (city == String.Empty) {
                    ErrorMessage = "City is required";
                    return false;
                }
                if (stateCode == String.Empty) {
                    ErrorMessage = "State is required";
                    return false;
                }
                if (zipCode == String.Empty) {
                    ErrorMessage = "Zip Code is required";
                    return false;
                }
                if (phoneNumber == String.Empty) {
                    ErrorMessage = "Phone Number is required";
                    return false;
                }
                if (phoneNumber.Length < 10) {
                    ErrorMessage = "Phone Number must include area code plus 7 digit phone number";
                    return false;
                }

                Order.ShippingFirstName = firstName;
                Order.ShippingLastName = lastName;
                Order.ShippingAddress1 = address1;
                if (address2 != String.Empty) {
                    Order.ShippingAddress2 = address2;
                } else {
                    Order.SetShippingAddress2Null();
                }
                Order.ShippingCity = city;
                Order.ShippingStateCode = stateCode;
                Order.ShippingZipCode = zipCode;
                Order.ShippingPhone = phoneNumber;

                if (sameAddressBox.Checked) {
                    Order.BillingFirstName = firstName;
                    Order.BillingLastName = lastName;
                    Order.BillingAddress1 = address1;
                    if (address2 != String.Empty) {
                        Order.BillingAddress2 = address2;
                    } else {
                        Order.SetBillingAddress2Null();
                    }
                    Order.BillingCity = city;
                    Order.BillingStateCode = stateCode;
                    Order.BillingZipCode = zipCode;
                    Order.BillingPhone = phoneNumber;
                }

                if (rememberMeBox.Checked) {

                    DsCookie["_c_sa"] = Cipher.Encrypt2(firstName);
                    DsCookie["_c_sb"] = Cipher.Encrypt2(lastName);
                    DsCookie["_c_sc"] = Cipher.Encrypt2(address1);
                    DsCookie["_c_sd"] = Cipher.Encrypt2(address2);
                    DsCookie["_c_se"] = Cipher.Encrypt2(city);
                    DsCookie["_c_sf"] = Cipher.Encrypt2(stateCode);
                    DsCookie["_c_sg"] = Cipher.Encrypt2(zipCode);
                    DsCookie["_c_sh"] = Cipher.Encrypt2(phoneNumber);

                }
            }


            orderAdapter.Update(Order);

            return true;
        }

    }

}
