using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.IO;
using System.Net.Mail;

using DollarSaver.Core.Common;
using DollarSaver.Core.Data;
using DollarSaver.Core.Data.DollarSaverDBTableAdapters;
using DollarSaver.Core.Encryption;

namespace DollarSaver.Web {

    /// <summary>
    /// Summary description for StationPageBase
    /// </summary>
    public class CookieStationPageBase : StationPageBase {


        protected override int GetPageStationId() {

            HttpCookie cookie = Request.Cookies.Get(DS_COOKIE_NAME);

            if (Order != null) {
                return Order.StationId;
            } else if (cookie != null) {
                return Int32.Parse(cookie["station_id"]);
            } else {
                return 0;
            }

        }

        private DollarSaverDB.OrderRow _order = null;

        public DollarSaverDB.OrderRow Order {
            get {
                if (_order == null || _order.OrderId != OrderId) {
                    _order = null;
                    if (OrderId > 0) {
                        OrderTableAdapter orderAdapter = new OrderTableAdapter();
                        DollarSaverDB.OrderDataTable orderTable = orderAdapter.GetOrder(OrderId);

                        if (orderTable.Count == 1) {
                            _order = orderTable[0];
                            Session["order_id"] = _order.OrderId;
                        }

                        // reset OrderId if order not found?
                    }
                }

                return _order;
            }

            set {
                _order = value;
                if (value != null) {
                    Session["order_id"] = value.OrderId;
                } else {
                    Session["order_id"] = 0;
                }
            }
        }


        public int OrderId {
            get {
                if (Session["order_id"] != null) {
                    return (int)Session["order_id"];
                } else {
                    return 0;
                }
            }
            set {
                Session["order_id"] = value;
                _order = null;
            }
        }

        protected override void OnPreInit(EventArgs e) {

            base.OnPreInit(e);
        }


        public bool SendReceipt() {

            bool receiptSent = false;

            try {
                //String siteName = "DollarSaver";

                //if (!Station.IsSiteNameNull() && Station.SiteName != String.Empty) {
                //    siteName = Station.SiteName;
                //}

                // Send certificate e-mail
                MailMessage message = new MailMessage("\"" + Station.SiteNamePlainText.Replace("\"", "") + "\" <auto-confirm@dollarsavershow.com>", Order.ShippingEmail);

                message.IsBodyHtml = true;


                message.Subject = Station.SiteNamePlainText + " Order #" + Order.OrderId + " Receipt";

                String body = "<html><body><span style='font-family: Verdana; font-size: 10pt;'>" + Environment.NewLine;

                body += "Thank you for your purchase from " + Station.SiteNamePlainText + "!<BR><BR>" + Environment.NewLine;

                body += "Order #" + Order.OrderId + " Receipt<BR>" + Environment.NewLine;


                String printableLinks = String.Empty;
                foreach (DollarSaverDB.OrderLineItemRow lineItem in Order.PrintItems) {
                    foreach (DollarSaverDB.CertificateNumberRow certNumber in lineItem.Numbers) {

                        String certificateName = lineItem.Certificate.Advertiser.Name;
                        String number = certNumber.Number;

                        String queryString = "id=" + certNumber.CertificateNumberId + "&order_id=" + Order.OrderId;

                        String encryptedString = Server.UrlEncode(Cipher.Encrypt(queryString));


                        String certificateLink = "https://" + EnvDomain + ResolveUrl("~/ViewCertificate.aspx?x=" + encryptedString);

                        printableLinks += "<a href=\"" + certificateLink + "\">" + certificateName + " - " + number + "</a><BR>" + Environment.NewLine;

                    }

                }

                if (printableLinks != String.Empty) {
                    body += "<BR>Click the links below to view and print your certificates:<BR><BR>" + Environment.NewLine;
                    body += printableLinks;
                }

                if (Order.ShippingRequired) {

                    body += "<BR>The following certificates will be mailed:<BR>" + Environment.NewLine;

                    foreach (DollarSaverDB.OrderLineItemRow lineItem in Order.ShipItems) {
                        String certificateName = lineItem.Certificate.Advertiser.Name;

                        body += certificateName + "<BR>Quantity: " + lineItem.Quantity + "<BR><BR>" + Environment.NewLine;

                    }

                    body += "Shipping Address:<BR>" + Order.ShippingInfo.Replace(Environment.NewLine, "<BR>") + "<BR><BR>" + Environment.NewLine;
                }


                if (Order.PickUpRequired) {

                    body += "<BR>The following certificates must be picked up:<BR>" + Environment.NewLine;

                    foreach (DollarSaverDB.OrderLineItemRow lineItem in Order.PickUpItems) {
                        String certificateName = lineItem.Certificate.Advertiser.Name;

                        body += certificateName + "<BR>Quantity: " + lineItem.Quantity + "<BR><BR>" + Environment.NewLine;

                        body += "Pick up instructions:<BR>";
                        body += Server.HtmlEncode(lineItem.DeliveryNote).Replace(Environment.NewLine, "<BR>");

                        body += "<BR><BR>" + Environment.NewLine;
                    }


                }


                body += "</span></body></html>";

                message.Body = body;

                Mailer mailer = new Mailer();
                mailer.Send(message);

                receiptSent = true;

            } catch { }


            try {

                if (Order.ShippingRequired || Order.PickUpRequired) {
                    NotifyStation();
                }

                foreach (DollarSaverDB.OrderLineItemRow lineItem in Order.LineItems) {
                    if (lineItem.Certificate.AvailableNumbers.Count == 0) { // OR (lineItem.Certificate.AvailableNumbers + lineItem.Numbers.Count > lineItem.Certificate.LowStockAmount && lineItem.Certificate.AvailableNUmbers.Count <= lineItem.Certificate.LowStockAmount)
                        NotifySoldOut(lineItem.Certificate);
                    }
                }

            } catch { }


            return receiptSent;

        }



        public void NotifyStation() {

            try {
                //String siteName = "DollarSaver";

                //if (!Station.IsSiteNameNull() && Station.SiteName != String.Empty) {
                //    siteName = Station.SiteNamePlainText;
                //}

                if (Order.ShippingRequired) {
                    Mailer mailer = new Mailer();
                    String subject = Station.SiteNamePlainText + " Order #" + Order.OrderId + " Needs To Be Shipped";


                    String body = "<html><body><span style='font-family: Verdana; font-size: 10pt;'>" + Environment.NewLine;

                    body += "Order #" + Order.OrderId + " <BR><BR>" + Environment.NewLine;

                    body += "The following certificates need to be mailed:<BR>" + Environment.NewLine;

                    foreach (DollarSaverDB.OrderLineItemRow lineItem in Order.ShipItems) {
                        String certificateName = lineItem.Certificate.Advertiser.Name;

                        body += certificateName + "<BR>Quantity: " + lineItem.Quantity + "<BR><BR>" + Environment.NewLine;

                    }

                    body += "Shipping Address:<BR>" + Order.ShippingInfo.Replace(Environment.NewLine, "<BR>") + "<BR><BR>" + Environment.NewLine;

                    body += "Email Address: " + Order.ShippingEmail + "<BR><BR>" + Environment.NewLine;

                    body += "</span></body></html>";


                    foreach (DollarSaverDB.AdminRow contact in Station.Contacts) {

                        if (!contact.IsEmailAddressNull()) {
                            // Send certificate e-mail
                            MailMessage message = new MailMessage("\"" + Station.SiteNamePlainText.Replace("\"", "") + "\" <auto-confirm@dollarsavershow.com>", contact.EmailAddress);

                            message.IsBodyHtml = true;

                            message.Subject = subject;
                            message.Body = body;


                            try {
                                mailer.Send(message);
                            } catch {
                            }
                        }
                    }
                }


                if (Order.PickUpRequired) {
                    Mailer mailer = new Mailer();
                    String subject = Station.SiteNamePlainText + " Order #" + Order.OrderId + " Will Be Picked Up";


                    String body = "<html><body><span style='font-family: Verdana; font-size: 10pt;'>" + Environment.NewLine;

                    body += "Order #" + Order.OrderId + " <BR><BR>" + Environment.NewLine;

                    body += "The following certificates will be picked up:<BR>" + Environment.NewLine;

                    foreach (DollarSaverDB.OrderLineItemRow lineItem in Order.PickUpItems) {
                        String certificateName = lineItem.Certificate.Advertiser.Name;

                        body += certificateName + "<BR>Quantity: " + lineItem.Quantity + "<BR><BR>" + Environment.NewLine;

                    }

                    body += "Customer Name:<BR>" + Order.BillingName;



                    body += "</span></body></html>";


                    foreach (DollarSaverDB.AdminRow contact in Station.Contacts) {

                        if (!contact.IsEmailAddressNull()) {
                            // Send certificate e-mail
                            MailMessage message = new MailMessage("\"" + Station.SiteNamePlainText.Replace("\"", "") + "\" <auto-confirm@dollarsavershow.com>", contact.EmailAddress);

                            message.IsBodyHtml = true;

                            message.Subject = subject;
                            message.Body = body;


                            try {
                                mailer.Send(message);
                            } catch {
                            }
                        }
                    }
                }


            } catch { }

        }


        public void NotifySoldOut(DollarSaverDB.CertificateRow soldOutCertificate) {

            try {


                String subject = Station.SiteNamePlainText + " - " + soldOutCertificate.AdvertiserName + " - " + soldOutCertificate.ShortName + " has sold out!";


                String body = "<html><body><span style='font-family: Verdana; font-size: 10pt;'>" + Environment.NewLine;

                body += "<span style='font-weight: bold;'>SOLD OUT NOTICE!</span><br><br>" + Environment.NewLine;

                body += soldOutCertificate.AdvertiserName + " - " + soldOutCertificate.ShortName + " has sold out on your e-commerce program. You must do the following...<BR><BR>" + Environment.NewLine;

                body += "1. Stop playing any " + soldOutCertificate.AdvertiserName + " spots.<BR>" + Environment.NewLine;

                body += "2. Notify " + soldOutCertificate.AdvertiserName + " that they are sold out asking them if they want to put more certificates up for sale.  If " + soldOutCertificate.AdvertiserName + " is owed any commercials discuss producing, and airing those commercials.<BR><BR>" + Environment.NewLine;

                body += "Thanks!<BR>";

                body += "</span></body></html>";


                // Send certificate e-mail
                MailMessage message = new MailMessage();
                message.From = new MailAddress("\"" + Station.SiteNamePlainText.Replace("\"", "") + "\" <auto-confirm@dollarsavershow.com>");


                message.IsBodyHtml = true;

                message.Subject = subject;
                message.Body = body;


                if (soldOutCertificate.Advertiser.SalesPerson != null && !soldOutCertificate.Advertiser.SalesPerson.IsEmailAddressNull()) {
                    message.To.Add(soldOutCertificate.Advertiser.SalesPerson.EmailAddress);
                }
               

                foreach (DollarSaverDB.AdminRow contact in Station.Contacts) {

                    if (!contact.IsEmailAddressNull()) {
                          message.To.Add(contact.EmailAddress);

                    }
                }

                
                if(message.To.Count > 0) {

                    try {
                        Mailer mailer = new Mailer();
                        mailer.Send(message);
                    } catch {
                    }

                }


            } catch { }

        }


    }
}
