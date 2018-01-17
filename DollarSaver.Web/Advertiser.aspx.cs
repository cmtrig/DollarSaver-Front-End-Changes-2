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
    public partial class AdvertiserPage : StationPageBase {
		/// <summary>
		/// twitterAdvertiserUrlLiteral control.
		/// </summary>
		/// <remarks>
		/// Auto-generated field.
		/// To modify move field declaration from designer file to code-behind file.
		/// </remarks>
		protected global::System.Web.UI.WebControls.Literal twitterAdvertiserUrlLiteral;

		/// <summary>
		/// twitterTextLiteral control.
		/// </summary>
		/// <remarks>
		/// Auto-generated field.
		/// To modify move field declaration from designer file to code-behind file.
		/// </remarks>
		protected global::System.Web.UI.WebControls.Literal twitterTextLiteral;

		/// <summary>
		/// advertiserUrlLiteral control.
		/// </summary>
		/// <remarks>
		/// Auto-generated field.
		/// To modify move field declaration from designer file to code-behind file.
		/// </remarks>
		protected global::System.Web.UI.WebControls.Literal advertiserUrlLiteral;

        private DollarSaverDB.AdvertiserRow Advertiser;

        public override string PageTitle {
            get {
                String title = base.PageTitle;

                if (Advertiser != null) {
                    title += " - " + Advertiser.Name;
                }

                return title;
            }
        }

        protected override int GetPageStationId() {
            return Advertiser != null ? Advertiser.StationId : 0;
        }

        protected override void OnPreInit(EventArgs e) {

            int advertiserId = GetValueFromQueryString("advertiser_id");

            if (advertiserId > 0) {

                AdvertiserTableAdapter adapter = new AdvertiserTableAdapter();

                DollarSaverDB.AdvertiserDataTable advertisers = adapter.GetAdvertiser(advertiserId);

                if (advertisers.Count == 1) {
                    Advertiser = advertisers[0];
                }

                
            }

            base.OnPreInit(e);
        }

        protected void Page_Load(object sender, EventArgs e) {
            certificateRepeater.ItemDataBound += new RepeaterItemEventHandler(certificateRepeater_ItemDataBound);

            if (!Page.IsPostBack) {

                if (Advertiser != null) {
                    advertiserHolder.Visible = true;

                    if (Advertiser.StationId != StationId || !Advertiser.IsActive) {
                        RedirectToHomePage();
                    }

                    // log visit
                    LogPageHit();

                    // Facebook Open Graph fields
                    HtmlMeta metaOgTitle = new HtmlMeta();
                    metaOgTitle.Attributes["property"] = "og:title";
                    metaOgTitle.Content = PageTitle;
                    Page.Header.Controls.Add(metaOgTitle);

                    HtmlMeta metaOgUrl = new HtmlMeta();
                    metaOgUrl.Attributes["property"] = "og:url";
                    metaOgUrl.Content = Request.Url.AbsoluteUri;
                    Page.Header.Controls.Add(metaOgUrl);

                    HtmlMeta metaOgDesc = new HtmlMeta();
                    metaOgDesc.Attributes["property"] = "og:description";
                    metaOgDesc.Content = Advertiser.DescriptionPlainText;
                    Page.Header.Controls.Add(metaOgDesc);


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


                        HtmlMeta metaOgImg = new HtmlMeta();
                        metaOgImg.Attributes["property"] = "og:image";
                        metaOgImg.Content = "http://" + EnvDomain + ResolveUrl(Advertiser.LogoUrl);
                        Page.Header.Controls.Add(metaOgImg);



                    } else {
                        advertiserImage.Visible = false;
                    }

                    if (Advertiser.InlineAddress.Trim() != String.Empty) {
                        addressLabel.Text = "<strong>Address:</strong> " + Advertiser.InlineAddress;
                    }

                    if (Advertiser.IsAddressMappable &&
                        Advertiser.DisplayAddress1 != String.Empty && 
                        Advertiser.DisplayCity != String.Empty &&
                        Advertiser.DisplayStateCode != String.Empty) {
                        mapLink.Visible = true;
                        mapLink.NavigateUrl = "~/AdvertiserMap.aspx?advertiser_id=" + Advertiser.AdvertiserId;
                    } else {
                        mapLink.Visible = false;
                    }

                    if (Advertiser.DisplayPhoneNumber.Trim() != String.Empty) {
                        phoneLabel.Text = "<strong>Phone:</strong> " + Advertiser.DisplayPhoneNumber;
                    }

                    advertiserNameLabel.Text = Advertiser.Name;
                    advertiserDescriptionLabel.Text = Advertiser.DisplayDescription;

                    if (!Advertiser.IsWebsiteUrlNull()) {
                        viewWebsiteLink.NavigateUrl = Advertiser.WebsiteUrl;
                    } else {
                        viewWebsiteLink.Visible = false;
                    }


                    if (Advertiser.ActiveCertificates.Count > 0) {
                        certificateRepeater.DataSource = Advertiser.ActiveCertificates.Rows;
                        certificateRepeater.DataBind();
                    } else {
                        InfoMessage = "No Certificates found for this Advertiser";
                    }

                    String advertiserUrl = "http://";

                    if (UseSubdomain) {
                        advertiserUrl += Advertiser.Station.Subdomain + ".";
                    }

                    advertiserUrl += EnvDomain + ResolveUrl("~/Advertiser.aspx?advertiser_id=" + Advertiser.AdvertiserId);

                    advertiserUrlLiteral.Text = advertiserUrl;

                    twitterAdvertiserUrlLiteral.Text = advertiserUrl;
                    twitterTextLiteral.Text = Server.HtmlEncode("Save at " + Advertiser.Name);

                } else {
                    RedirectToHomePage();
                }
           
            }
        }

        void certificateRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e) {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) {

                CertificateDetail certificateDetail = (CertificateDetail)e.Item.FindControl("certificateDetail");

                certificateDetail.DisplayCertificate = (DollarSaverDB.CertificateRow)e.Item.DataItem;
            }
        }

        private void LogPageHit() {

            if (HitCounterEnabled) {

                PageHitLogTableAdapter pageHitLogAdapter = new PageHitLogTableAdapter();

                DateTime currentDate = DateTime.Now;
                currentDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day);


                foreach (DollarSaverDB.CertificateRow cert in Advertiser.ActiveCertificates) {
                    LogHit(currentDate, cert.CertificateId, PageHitType.AdvertiserPage);
                }
            }
        }
    }

}
