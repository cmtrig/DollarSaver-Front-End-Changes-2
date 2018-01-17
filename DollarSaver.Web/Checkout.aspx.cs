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

using System.Text;
using System.Text.RegularExpressions;

using com.paypal.sdk.core;
using com.paypal.sdk.services;
using com.paypal.sdk.util;

using DollarSaver.Core.Common;
using DollarSaver.Core.Encryption;
using DollarSaver.Core.Data;
using DollarSaver.Core.Data.DollarSaverDBTableAdapters;
using DollarSaver.Web.controls;

namespace DollarSaver.Web {

    public partial class CheckoutPage : SecureStationPageBase {

        public override string PageTitle {
            get {
                return base.PageTitle + " - Checkout";
            }
        }

        protected void Page_Load(object sender, EventArgs e) {
            placeOrderButton.Click += new EventHandler(placeOrderButton_Click);

            /*
            OrderId = 10166;
            SendReceipt();
            return;
            */

            if (Order == null || Order.StationId != StationId || Order.OrderStatusId != (int)OrderStatus.New) {
                OrderId = 0;
                Response.Redirect("~/Cart.aspx");
            }

            if (Order.LineItems.Count == 0) {
                Response.Redirect("~/Cart.aspx");
            }

            if (Order.IsShippingEmailNull() || Order.ShippingEmail == String.Empty) {
                Response.Redirect("~/EnterEmail.aspx");
            }


            if (!Page.IsPostBack) {

                StringBuilder disableButtonJS = new StringBuilder();
                disableButtonJS.Append("if (typeof(Page_ClientValidate) == 'function') { ");
                disableButtonJS.Append("if (Page_ClientValidate() == false) { return false; }} ");
                disableButtonJS.Append("this.value = 'Submitting...';");
                disableButtonJS.Append("this.disabled = true;");
                disableButtonJS.Append(this.Page.ClientScript.GetPostBackEventReference(placeOrderButton, ""));
                disableButtonJS.Append(";");
                placeOrderButton.Attributes.Add("onclick", disableButtonJS.ToString());

                OrderTableAdapter orderAdapter = new OrderTableAdapter();

                Order.CheckoutStartDate = DateTime.Now;
                orderAdapter.Update(Order);

                customerEmailLabel.Text = Server.HtmlEncode(Order.ShippingEmail);

                if (Order.ShippingRequired) {
                    shippingHolder.Visible = true;

                    shippingAddressLabel.Text = Server.HtmlEncode(Order.ShippingInfo).Replace(Environment.NewLine, "<BR>");
                } else {
                    shippingHolder.Visible = false;
                }

                StateTableAdapter stateAdapter = new StateTableAdapter();

                DollarSaverDB.StateDataTable states = stateAdapter.GetStates();

                stateList.DataSource = states.Rows;
                stateList.DataTextField = "Summary";
                stateList.DataValueField = "StateCode";
                stateList.DataBind();

                for (int year = DateTime.Now.Year; year <= DateTime.Now.Year + 11; year++) {
                    expirationYearList.Items.Add(new ListItem(year.ToString()));
                }


                if (!Order.IsBillingFirstNameNull()) {
                    if (!Order.IsBillingFirstNameNull()) {
                        firstNameBox.Text = Order.BillingFirstName;
                    }

                    if (!Order.IsBillingLastNameNull()) {
                        lastNameBox.Text = Order.BillingLastName;
                    }

                    if (!Order.IsBillingAddress1Null()) {
                        address1Box.Text = Order.BillingAddress1;
                    }

                    if (!Order.IsBillingAddress2Null()) {
                        address2Box.Text = Order.BillingAddress2;
                    }

                    if (!Order.IsBillingCityNull()) {
                        cityBox.Text = Order.BillingCity;
                    }

                    if (!Order.IsBillingStateCodeNull()) {
                        stateList.SelectedValue = Order.BillingStateCode;
                    } else if (!Station.IsStateCodeNull()) {
                        stateList.SelectedValue = Station.StateCode;
                    }

                    if (!Order.IsBillingZipCodeNull()) {
                        zipCodeBox.Text = Order.BillingZipCode;
                    }

                    if (!Order.IsBillingPhoneNull()) {
                        phoneNumberBox.Text = Order.BillingPhone;
                    }

                } else if (DsCookie["_c"] == "1") {

                    firstNameBox.Text = Cipher.Decrypt2(DsCookie["_c_ba"]);
                    lastNameBox.Text = Cipher.Decrypt2(DsCookie["_c_bb"]);
                    address1Box.Text = Cipher.Decrypt2(DsCookie["_c_bc"]);
                    address2Box.Text = Cipher.Decrypt2(DsCookie["_c_bd"]);
                    cityBox.Text = Cipher.Decrypt2(DsCookie["_c_be"]);

                    String stateCode = Cipher.Decrypt2(DsCookie["_c_bf"]);

                    if (stateCode != String.Empty) {
                        stateList.SelectedValue = stateCode;
                    } else {
                        stateList.SelectedValue = Station.StateCode;
                    }


                    zipCodeBox.Text = Cipher.Decrypt2(DsCookie["_c_bg"]);
                    phoneNumberBox.Text = Cipher.Decrypt2(DsCookie["_c_bh"]);

                } else {
                    stateList.SelectedValue = Station.StateCode;
                }


            } else {
               // postback, reset place order button
                placeOrderButton.Text = "Place Order";
                placeOrderButton.Enabled = true;
            }
        }

        protected override void OnPreRender(EventArgs e) {
            base.OnPreRender(e);



            lineItemDetail.Order = Order;


        }

        void placeOrderButton_Click(object sender, EventArgs e) {

            // Validate fields

            /*
            string patternLenient = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

            string patternStrict = @"^(([^<>()[\]\\.,;:\s@\""]+" 
               + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@" 
               + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}" 
               + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+" 
               + @"[a-zA-Z]{2,}))$";

            */

            String creditCardType = creditCardList.SelectedValue;
            String creditCardNumber = creditCardNumberBox.Text.Trim();
            creditCardNumber = Regex.Replace(creditCardNumber, @"\D", "");
            String verificationNumber = verificationNumberBox.Text.Trim();

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
            if (phoneNumber.Length < 10) {
                ErrorMessage = "Phone Number must include area code plus 7 digit phone number";
                return;
            }

            if (DsCookie["_c"] == "1") {

                DsCookie["_c_ba"] = Cipher.Encrypt2(firstName);
                DsCookie["_c_bb"] = Cipher.Encrypt2(lastName);
                DsCookie["_c_bc"] = Cipher.Encrypt2(address1);
                DsCookie["_c_bd"] = Cipher.Encrypt2(address2);
                DsCookie["_c_be"] = Cipher.Encrypt2(city);
                DsCookie["_c_bf"] = Cipher.Encrypt2(stateCode);
                DsCookie["_c_bg"] = Cipher.Encrypt2(zipCode);
                DsCookie["_c_bh"] = Cipher.Encrypt2(phoneNumber);

            }

            if (creditCardNumber == String.Empty) {
                ErrorMessage = "Credit Card Number is required";
                return;
            }
            if (verificationNumber == String.Empty) {
                ErrorMessage = "Card Verification Number is required";
                return;
            }

            OrderTableAdapter orderAdapter = new OrderTableAdapter();
            CertificateNumberTableAdapter numberAdapter = new CertificateNumberTableAdapter();
            OrderLineItemTableAdapter orderLineItemAdapter = new OrderLineItemTableAdapter();


            decimal subtotal = 0.0m;

            foreach (DollarSaverDB.OrderLineItemRow lineItem in Order.LineItems) {
                int numberAssigned = Convert.ToInt32(numberAdapter.Assign(lineItem.OrderLineItemId));

                if (numberAssigned != lineItem.Quantity) {

                    if (numberAssigned == 0) {
                        ErrorMessage = "We're sorry, " + lineItem.Certificate.AdvertiserName + " is no longer available";
                        orderLineItemAdapter.Delete(lineItem.OrderLineItemId);
                    } else {
                        lineItem.Quantity = numberAssigned;
                        orderLineItemAdapter.Update(lineItem);

                        ErrorMessage = "We're sorry, " + lineItem.Certificate.AdvertiserName + " is no longer available in the quantity you requested. Please review your updated order and click on the checkout button if you would like to purchase the new quantity";
                    }

                    Order.LineItemModifiedDate = DateTime.Now;
                    orderAdapter.Update(Order);

                    ResetOrder();
                    Response.Redirect("~/Cart.aspx");

                }


                subtotal += lineItem.Total;
            }


            switch (creditCardType) {
                case "Visa":
                    Order.PaymentMethodId = (int)PaymentMethod.Visa;
                    break;
                case "MasterCard":
                    Order.PaymentMethodId = (int)PaymentMethod.MasterCard;
                    break;
                case "Discover":
                    Order.PaymentMethodId = (int)PaymentMethod.Discover;
                    break;
                case "Amex":
                    Order.PaymentMethodId = (int)PaymentMethod.Amex;
                    break;
                default:
                    
                    break;
            }


            Order.SubTotal = subtotal;
            Order.GrandTotal = subtotal;
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


            orderAdapter.Update(Order);


            // Check max purchase qty for Deal of the Week
            if (Station.StationSiteType == SiteType.DealOfTheWeek) {
                CertificateTableAdapter certificateAdapter = new CertificateTableAdapter();
                DollarSaverDB.CertificateDataTable certificateTable = certificateAdapter.GetCurrentDeal(StationId);

                if (certificateTable.Count == 1) {
                    DollarSaverDB.CertificateRow deal = certificateTable[0];
                    if (deal.MaxPurchaseQty > 0) {
                        foreach (DollarSaverDB.OrderLineItemRow lineItem in Order.LineItems) {
                            if (lineItem.CertificateId == deal.CertificateId) {
                                int pastQty = Convert.ToInt32(orderLineItemAdapter.GetQtyByConsumer(firstName, lastName, null,
                                    address1, city, stateCode, Order.ShippingEmail, deal.CertificateId));

                                if (pastQty + lineItem.Quantity > deal.MaxPurchaseQty) {
                                    ErrorMessage = "Sorry, the maximum purchase quantity per person for the Deal of the Week is " + deal.MaxPurchaseQty + ".";

                                    if (pastQty >= deal.MaxPurchaseQty) {
                                        ErrorMessage += "<BR>You have already purchased the maximum allowed.";
                                    } else {
                                        int allowedAmount = deal.MaxPurchaseQty - pastQty;
                                        ErrorMessage += "<BR>You may only purchase " + allowedAmount + " more.";
                                    }

                                    ResetOrder();
                                    Response.Redirect("~/Cart.aspx");
                                }

                            }
                        }
                    }
                }
            }
            
            if (Order.CheckoutStartDate < Order.LineItemModifiedDate) {

                ResetOrder();

                ErrorMessage = "Your cart has been updated while checking out, please verify you items and continue the checkout process.";

                Response.Redirect("~/Cart.aspx");
            }

            Order.OrderStatusId = (int)OrderStatus.Processing;
            orderAdapter.Update(Order);


            // charge order...
            NVPCallerServices caller = PayPalAPI.PayPalAPIInitialize(IsDev);
            NVPCodec encoder = new NVPCodec();

            encoder["VERSION"] = "50.0";
            encoder["METHOD"] = "DoDirectPayment";
            encoder["PAYMENTACTION"] = "Sale";
            encoder["AMT"] = subtotal.ToString("0.00");
            encoder["CREDITCARDTYPE"] = creditCardType;
            encoder["ACCT"] = creditCardNumber;
            encoder["EXPDATE"] = expirationMonthList.SelectedValue + expirationYearList.SelectedValue;
            encoder["CVV2"] = verificationNumber;
            encoder["FIRSTNAME"] = firstName;
            encoder["LASTNAME"] = lastName;
            encoder["STREET"] = address1;
            encoder["CITY"] = city;
            encoder["STATE"] = stateCode;
            encoder["ZIP"] = zipCode;
            encoder["COUNTRYCODE"] = "US";
            encoder["CURRENCYCODE"] = "USD";

            /*
            encoder["INVNUM"] = Order.OrderId.ToString();
            encoder["ITEMAMT"] = Order.LineItems.SubTotal.ToString("0.00");
            foreach (DollarSaverDB.OrderLineItemRow lineItem in Order.LineItems.Rows) {
                int itemNumber = lineItem.SeqNo - 1;

                encoder["L_NAME" + itemNumber] = lineItem.ShortName;
                encoder["L_NUMBER" + itemNumber] = lineItem.CertificateId.ToString();
                encoder["L_QTY" + itemNumber] = lineItem.Quantity.ToString();
                encoder["L_AMT" + itemNumber] = lineItem.DiscountValue.ToString("0.00");
            }
            */

            string paypalRequest = encoder.Encode();
            string paypalResponse = String.Empty;

            
            try {
                paypalResponse = caller.Call(paypalRequest);
            } catch {
                ResetOrder();
                ErrorMessage = "An error occurred while processing your order, please try submitting it again.";
                return;
            }
            

            NVPCodec decoder = new NVPCodec();
            decoder.Decode(paypalResponse);

            string strAck = decoder["ACK"];
            if (strAck != null && (strAck == "Success" || strAck == "SuccessWithWarning")) {

                
                string transactionId = decoder["TRANSACTIONID"];
                Order.TransactionId = transactionId;
                
                Order.OrderDate = DateTime.Now;
                Order.OrderStatusId = (int)OrderStatus.Complete;
                orderAdapter.Update(Order);

                InfoMessage = "Successfully processed order";

                if (SendReceipt()) {
                    InfoMessage += "<BR />Receipt sent to " + Order.ShippingEmail;
                }
                
                if (Order.AddToMailingList) {
                    CustomerContactTableAdapter customerContactAdapter = new CustomerContactTableAdapter();
                    customerContactAdapter.Insert(StationId, DateTime.Now, Order.ShippingEmail, Order.BillingFirstName, Order.BillingLastName);
                }

                Response.Redirect("~/Confirmation.aspx", true);
                return;

            } else {

                ResetOrder();

                ErrorMessage = "Error! " + decoder["L_LONGMESSAGE0"] + " (" + decoder["L_ERRORCODE0"] + ")";
                
                return;
            }

        }

        private void ResetOrder() {

            OrderTableAdapter orderAdapter = new OrderTableAdapter();
            CertificateNumberTableAdapter numberAdapter = new CertificateNumberTableAdapter();

            Order.OrderStatusId = (int)OrderStatus.New;
            orderAdapter.Update(Order);

            foreach (DollarSaverDB.OrderLineItemRow releaseItem in Order.LineItems) {
                numberAdapter.Release(releaseItem.OrderLineItemId);
            }

        }



        /*                      
           ID="firstNameBox" Text="John"                        
           ID="lastNameBox" Text="Doe" 
           Text="Visa" Value="Visa" 
           ID="creditCardNumberBox" Text="4059042064101342"
           Text="JAN (01)"
           2010
         */

    }

}
