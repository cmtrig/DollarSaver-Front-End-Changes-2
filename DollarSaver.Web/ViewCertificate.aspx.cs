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

using System.IO;

using DollarSaver.Core.Common;
using DollarSaver.Core.Data;
using DollarSaver.Core.Data.DollarSaverDBTableAdapters;
using DollarSaver.Core.Encryption;

namespace DollarSaver.Web {
    public partial class ViewCertificate : SecureStationPageBase {

        private DollarSaverDB.CertificateNumberRow certNumber = null;
        private DollarSaverDB.CertificateRow certificate = null;

        public override bool StationActiveRequired {
            get { return false; }
        }

        protected override int GetPageStationId() {
            if (certificate != null) {
                return certificate.Advertiser.StationId;
            } else {
                return 0;
            }
        }


        protected override void OnPreInit(EventArgs e) {


            LoadCertificateNumber();
            
            base.OnPreInit(e);
        }

        protected void Page_Load(object sender, EventArgs e) {


            if (Request.RawUrl.StartsWith("/dollarsaver/viewcertificate")) {
                Response.Redirect("~/ViewCertificate.aspx?" + Request.QueryString);
            }


            if (certificate != null) {

                bool backBtn = Globals.ConvertToBool(Request.QueryString["backBtn"]);

                returnToConfirmationLink.Visible = backBtn;

                certificateHolder.Visible = true;
                certNotFoundHolder.Visible = false;

                DollarSaverDB.AdvertiserRow advertiser = certificate.Advertiser;

                if (!advertiser.IsLogoImageNull()) {
                    advertiserImage.ImageUrl = advertiser.LogoUrl;

					if (advertiser.IsLogoImageVertical) {
						advertiserImage.Width = 75;
						advertiserImage.Height = 125;
					} else {
						advertiserImage.Width = 125;
						advertiserImage.Height = 75;
					}


                } else {
                    advertiserImage.Visible = false;
                }

                advertiserNameLabel.Text = advertiser.Name;
                addressLabel.Text = advertiser.FullAddress;
                phoneLabel.Text = advertiser.DisplayPhoneNumber;

                //stationNameLabel.Text = advertiser.Station.Name;

                String ImageDir = Request.PhysicalApplicationPath + advertiser.Station.ImageDirUrl;
                if (!advertiser.Station.Content.IsLogoImageNull() && File.Exists(ImageDir + advertiser.Station.Content.LogoImage)) {
                    logoImage.ImageUrl = "~/" + advertiser.Station.ImageDirUrl + advertiser.Station.Content.LogoImage;
                }

                if (!advertiser.Station.Content.IsCertificateLogoImageNull() && File.Exists(ImageDir + advertiser.Station.Content.CertificateLogoImage)) {
                    certLogoImage.ImageUrl = "~/" + advertiser.Station.ImageDirUrl + advertiser.Station.Content.CertificateLogoImage;
                }

                if (certNumber != null) {
                    voidHolder.Visible = false;
                    
                    certificateNumberLabel.Text = certNumber.Number;
                    amountLabel.Text = certNumber.LineItem.FaceValue.ToString("$#,0.00");
                    certificateDescriptionLabel.Text = certNumber.LineItem.Description;
                    purchaseDateLabel.Text = certNumber.LineItem.Order.OrderDate.ToString("MM/dd/yyyy");

                } else {
                    voidHolder.Visible = true;
                    certDiv.Attributes["style"]= "position:absolute; z-index:2;";

                    certificateNumberLabel.Text = new String('X', certificate.NumberLength);
                    amountLabel.Text = certificate.FaceValue.ToString("$#,0.00");
                    certificateDescriptionLabel.Text = certificate.Description;
                    purchaseDateLabel.Text = DateTime.Now.ToString("MM/dd/yyyy");
                }

                String contactUsUrl = "http://";

                if (UseSubdomain) {
                    contactUsUrl += Station.Subdomain + ".";
                }

                contactUsUrl += EnvDomain + ResolveUrl("~/contact");

                if (!UseSubdomain) {
                    contactUsUrl += "?station_id=" + StationId;
                }

                contactUsLabel1.Text = contactUsUrl;
                contactUsLabel2.Text = contactUsUrl;

            } else {
                certificateHolder.Visible = false;
                certNotFoundHolder.Visible = true;
            }
        }


        private void LoadCertificateNumber() {

            CertificateNumberTableAdapter certificateNumberAdapter = new CertificateNumberTableAdapter();

            int id = 0;
            int orderId = 0;
            int sampleId = 0;

            String cipherText = Request.QueryString["x"];

            if (cipherText != null && cipherText != String.Empty) {
                try {
                    String plainText = Server.UrlDecode(Cipher.Decrypt(cipherText));

                    String[] keysAndValues = plainText.Split('&');

                    foreach (String keyAndValueStr in keysAndValues) {
                        String[] keyAndValue = keyAndValueStr.Split('=');

                        if (keyAndValue.Length == 2) {
                            if (keyAndValue[0] == "id") {
                                try {
                                    id = Convert.ToInt32(keyAndValue[1]);
                                } catch { }
                            } else if (keyAndValue[0] == "order_id") {
                                try {
                                    orderId = Convert.ToInt32(keyAndValue[1]);
                                } catch { }
                            } else if (keyAndValue[0] == "sample_id") {
                                try {
                                    sampleId = Convert.ToInt32(keyAndValue[1]);
                                } catch { }
                            }

                        }

                    }
                } catch { }
            }

            if (sampleId == 0) {

                if (id == 0 || orderId == 0) {

                    // check for old parameters for backward compatibility
                    // viewcertificate?orderNum=13651&certNum=755426&backBtn=no
                    orderId = GetValueFromQueryString("orderNum");
                    id = 0;

                    // cut off backward compatibility after certain order id
                    if (orderId < 17000) {
                        String number = Convert.ToString(Request.QueryString["certNum"]);

                        if (orderId == 0 || number == String.Empty) {
                            return;
                        }

                        DollarSaverDB.CertificateNumberDataTable numberSearch = certificateNumberAdapter.GetBackward(orderId, number);

                        if (numberSearch.Count == 1) {
                            id = numberSearch[0].CertificateNumberId;
                        } else {
                            return;
                        }

                    }

                }

                DollarSaverDB.CertificateNumberDataTable certNumberTable = certificateNumberAdapter.GetCertificateNumber(id);

                if (certNumberTable.Count != 1) {
                    return;
                }

                DollarSaverDB.CertificateNumberRow checkCertNumber = certNumberTable[0];

                OrderTableAdapter orderAdapter = new OrderTableAdapter();

                DollarSaverDB.OrderDataTable orderTable = orderAdapter.GetOrder(orderId);

                if (orderTable.Count != 1) {
                    return;
                }

                DollarSaverDB.OrderRow order = orderTable[0];

                if (order.OrderStatusId != (int)OrderStatus.Complete) {
                    return;
                }

                if (checkCertNumber.IsOrderLineItemIdNull() || !order.LineItems.Contains(checkCertNumber.OrderLineItemId)) {
                    return;
                }

                certNumber = checkCertNumber;
                certificate = certNumber.Certificate;
            } else {

                CertificateTableAdapter certAdapter = new CertificateTableAdapter();
                DollarSaverDB.CertificateDataTable certificateTable = certAdapter.GetCertificate(sampleId);

                if (certificateTable.Count != 1) {
                    return;
                }

                certificate = certificateTable[0];

            }

        }

    }
}