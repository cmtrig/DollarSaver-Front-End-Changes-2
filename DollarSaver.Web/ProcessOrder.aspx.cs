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

using DollarSaver.Core.Common;
using DollarSaver.Core.Data;
using DollarSaver.Core.Data.DollarSaverDBTableAdapters;
using DollarSaver.Web.controls;

namespace DollarSaver.Web {

    // this page processes an express checkout order
    public partial class ProcessOrder : SecureStationPageBase {

        protected void Page_Load(object sender, EventArgs e) {
           
            if (Order == null || Order.StationId != StationId || Order.OrderStatusId != (int)OrderStatus.New) {
                OrderId = 0;
                Response.Redirect("~/Cart.aspx");
            }

            com.paypal.sdk.services.NVPCallerServices caller = PayPalAPI.PayPalAPIInitialize(IsDev);
            NVPCodec encoder = new NVPCodec();
            encoder["METHOD"] = "GetExpressCheckoutDetails";
            encoder["TOKEN"] = Session["TOKEN"].ToString();

            string paypalRequest = encoder.Encode();
            string paypalResponse = caller.Call(paypalRequest);

            NVPCodec decoder = new NVPCodec();
            decoder.Decode(paypalResponse);

            String billingEmailAddress = String.Empty;
            String firstName = String.Empty;
            String lastName = String.Empty;
            String phoneNumber = String.Empty;
            /*
            String address1 = String.Empty;
            String address2 = String.Empty;
            String city = String.Empty;
            String stateCode = String.Empty;
            String zipCode = String.Empty;
            */

            string strAck = decoder["ACK"];
            if (strAck != null && (strAck == "Success" || strAck == "SuccessWithWarning")) {

                Session["PAYERID"] = decoder["PAYERID"];

                billingEmailAddress = decoder["EMAIL"];
                firstName = decoder["FIRSTNAME"];
                lastName = decoder["LASTNAME"];
                phoneNumber = decoder["PHONENUM"];

            } else {
                /*
                string pStrError =
                    "ErrorCode=" + decoder["L_ERRORCODE0"] + "&" +
                    "Desc=" + decoder["L_SHORTMESSAGE0"] + "&" +
                    "Desc2=" + decoder["L_LONGMESSAGE0"];

                Response.Redirect("APIError.aspx?" + pStrError);
                 * */

                ErrorMessage = decoder["L_LONGMESSAGE0"];
                //Response.Redirect("~/Cart.aspx");
                ResetAndRedirect();

            }


            OrderLineItemTableAdapter orderLineItemAdapter = new OrderLineItemTableAdapter();
            
            OrderTableAdapter orderAdapter = new OrderTableAdapter();
            CertificateNumberTableAdapter numberAdapter = new CertificateNumberTableAdapter();

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

                        ErrorMessage = "We're sorry, " + lineItem.Certificate.AdvertiserName + " is no longer available in the quantity you requested. Please review your updated order and click on the chechout button if you would like to purchase the new quantity";
                    }

                    ResetAndRedirect();

                }


                subtotal += lineItem.Total;
            }

            // Check max purchase qty for Deal of the Week
            if (Station.StationSiteType == SiteType.DealOfTheWeek) {
                CertificateTableAdapter certificateAdapter = new CertificateTableAdapter();
                DollarSaverDB.CertificateDataTable certificateTable = certificateAdapter.GetCurrentDeal(StationId);

                if (certificateTable.Count == 1) {
                    DollarSaverDB.CertificateRow deal = certificateTable[0];

                    if (deal.MaxPurchaseQty > 0) {
                        foreach (DollarSaverDB.OrderLineItemRow lineItem in Order.LineItems) {
                            if (lineItem.CertificateId == deal.CertificateId) {
                                int pastQty = Convert.ToInt32(orderLineItemAdapter.GetQtyByConsumer(firstName, lastName, billingEmailAddress,
                                    null, null, null, Order.ShippingEmail, deal.CertificateId));

                                if (pastQty + lineItem.Quantity > deal.MaxPurchaseQty) {
                                    ErrorMessage = "Sorry, the maximum purchase quantity per person for the Deal of the Week is " + deal.MaxPurchaseQty + ".";

                                    if (pastQty >= deal.MaxPurchaseQty) {
                                        ErrorMessage += "<BR>You have already purchased the maximum allowed.";
                                    } else {
                                        int allowedAmount = deal.MaxPurchaseQty - pastQty;
                                        ErrorMessage += "<BR>You may only purchase " + allowedAmount + " more.";
                                    }

                                    ResetAndRedirect();
                                }

                            }
                        }
                    }
                }
            }

            //charge order here


            Order.SubTotal = subtotal;
            Order.GrandTotal = subtotal;
            Order.BillingFirstName = firstName;
            Order.BillingLastName = lastName;
            Order.BillingEmail = billingEmailAddress;

            /*
            Order.BillingAddress1 = address1;
            if (address2 != String.Empty) {
                Order.BillingAddress2 = address2;
            } else {
                Order.SetBillingAddress2Null();
            }
            Order.BillingCity = city;
            Order.BillingStateCode = stateCode;
            Order.BillingZipCode = zipCode;
             */

            Order.BillingPhone = phoneNumber;

            Order.PaymentMethodId = (int)PaymentMethod.PayPal;

            orderAdapter.Update(Order);


            if (Order.CheckoutStartDate < Order.LineItemModifiedDate) {
                ErrorMessage = "Your cart has been updated while checking out, please verify your items and continue the checkout process.";

                ResetAndRedirect();
            }

            Order.OrderStatusId = (int)OrderStatus.Processing;
            orderAdapter.Update(Order);


            encoder["METHOD"] = "DoExpressCheckoutPayment";
            encoder["TOKEN"] = Session["TOKEN"].ToString();
            encoder["PAYERID"] = Session["PAYERID"].ToString();
            encoder["AMT"] = subtotal.ToString("0.00");
            encoder["PAYMENTACTION"] = "Sale";
            encoder["CURRENCYCODE"] = "USD";

            encoder["INVNUM"] = Order.OrderId.ToString();

            encoder["ITEMAMT"] = Order.LineItems.SubTotal.ToString("0.00");
            foreach (DollarSaverDB.OrderLineItemRow lineItem in Order.LineItems.Rows) {
                int itemNumber = lineItem.SeqNo - 1;

                encoder["L_NAME" + itemNumber] = lineItem.ShortName;
                encoder["L_NUMBER" + itemNumber] = lineItem.CertificateId.ToString();
                encoder["L_QTY" + itemNumber] = lineItem.Quantity.ToString();
                encoder["L_AMT" + itemNumber] = lineItem.DiscountValue.ToString("0.00");
            }

            paypalRequest = encoder.Encode();
            paypalResponse = String.Empty;

            try {
                paypalResponse = caller.Call(paypalRequest);
            } catch {
                ErrorMessage = "An error occurred while processing your order, please try submitting it again.";
                ResetAndRedirect();
            }

            decoder.Decode(paypalResponse);

            strAck = decoder["ACK"];
            if (strAck != null && (strAck == "Success" || strAck == "SuccessWithWarning")) {

                /*
                string pStrResQue = "TRANSACTIONID=" + decoder["TRANSACTIONID"] + "&" +
                    "CURRENCYCODE=" + decoder["CURRENCYCODE"] + "&" +
                    "AMT=" + decoder["AMT"];

                Response.Redirect("DoExpressCheckoutPayment.aspx?" + pStrResQue);
                 * */

                
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

                Response.Redirect("~/Confirmation.aspx");

            } else {

                /*
                string pStrError =
                    "ErrorCode=" + decoder["L_ERRORCODE0"] + "&" +
                    "Desc=" + decoder["L_SHORTMESSAGE0"] + "&" +
                    "Desc2=" + decoder["L_LONGMESSAGE0"];

                Response.Redirect("APIError.aspx?" + pStrError);
                 * */

                ErrorMessage = "An error has occurred while processing your order: " + decoder["L_LONGMESSAGE0"] + " (" + decoder["L_ERRORCODE0"] + ")";
                ResetAndRedirect();
            }

        }


        private void ResetAndRedirect() {

            OrderTableAdapter orderAdapter = new OrderTableAdapter();
            CertificateNumberTableAdapter numberAdapter = new CertificateNumberTableAdapter();

            Order.OrderStatusId = (int)OrderStatus.New;
            orderAdapter.Update(Order);

            foreach (DollarSaverDB.OrderLineItemRow releaseItem in Order.LineItems) {
                numberAdapter.Release(releaseItem.OrderLineItemId);
            }

            Response.Redirect("~/Cart.aspx");

        }


    }

    
}
