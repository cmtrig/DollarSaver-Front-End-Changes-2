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
using DollarSaver.Web.controls;

namespace DollarSaver.Web {
    public partial class AdvertiserMapPage : StationPageBase {
        private DollarSaverDB.AdvertiserRow Advertiser;

        public override string PageTitle {
            get {
                String title = base.PageTitle;

                if (Advertiser != null) {
                    title += " - " + Advertiser.Name + " Map";
                }

                return title;
            }
        }

        protected void Page_Load(object sender, EventArgs e) {

            if (!Page.IsPostBack) {

                int advertiserId = GetValueFromQueryString("advertiser_id");

                if (advertiserId > 0) {


                    AdvertiserTableAdapter adapter = new AdvertiserTableAdapter();

                    DollarSaverDB.AdvertiserDataTable data = adapter.GetAdvertiser(advertiserId);

                    if (data.Rows.Count == 1) {
                        advertiserHolder.Visible = true;

                        Advertiser = data[0];

                        if (Advertiser.StationId != StationId || !Advertiser.IsActive) {
                            RedirectToHomePage();
                        }

                        returnLink.NavigateUrl = "~/Advertiser.aspx?advertiser_id=" + Advertiser.AdvertiserId;

                        String displayText = "<b>" + Server.HtmlEncode(Advertiser.Name) + "</b><br>" + Advertiser.FullAddress;
                        displayText = displayText.Replace("'", @"\'");

                        Page.Header.Controls.Add(new LiteralControl("<script src=\"http://maps.google.com/maps?file=api&amp;v=2&amp;key=ABQIAAAA0FhpaLYxjG1W_O_krcsz7xQyXc6TLbTx09-5RFMXJMMBRxWMXRR5ekopx2FaawD8vTFlpkvByI7eQA\" type=\"text/javascript\"></script>"));
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "1", "<script type=\"text/javascript\">showAddress('" + Advertiser.InlineAddress.Replace("'", "\'") + "', '" + displayText + "');</script>");

                        if (Advertiser.LogoUrl != String.Empty) {
                            advertiserImage.ImageUrl = Advertiser.LogoUrl;

							if (Advertiser.IsLogoImageVertical) {
								advertiserImage.Width = 75;
								advertiserImage.Height = 125;
							} else {
								advertiserImage.Width = 125;
								advertiserImage.Height = 75;
							}


                            advertiserImage.AlternateText = Advertiser.Name;
                            advertiserImage.ToolTip = Advertiser.Name;
                        } else {
                            advertiserImage.Visible = false;
                        }

                        advertiserNameLabel.Text = Advertiser.Name;

                        if (!Advertiser.IsWebsiteUrlNull()) {
                            viewWebsiteLink.NavigateUrl = Advertiser.WebsiteUrl;
                        } else {
                            viewWebsiteLink.Visible = false;
                        }

                        if (Advertiser.InlineAddress.Trim() != String.Empty) {
                            addressLabel.Text = "<strong>Address:</strong> " + Advertiser.InlineAddress;
                        }

                        if (Advertiser.DisplayPhoneNumber.Trim() != String.Empty) {
                            phoneLabel.Text = "<strong>Phone:</strong> " + Advertiser.DisplayPhoneNumber;
                        }

                    } else {
                        RedirectToHomePage();
                    }
                } else {
                    RedirectToHomePage();
                }
            }
        }

    }

}
