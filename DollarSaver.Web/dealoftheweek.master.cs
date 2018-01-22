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
using System.Linq;

using DollarSaver.Core.Data;
using DollarSaver.Core.Data.DollarSaverDBTableAdapters;

namespace DollarSaver.Web {
    public partial class DealOfTheWeekMaster : System.Web.UI.MasterPage {

        private String InfoMessage {
            get { return (String)Session["InfoMessage"] + String.Empty; }
            set { Session["InfoMessage"] = value; }
        }

        private String ErrorMessage {
            get { return (String)Session["ErrorMessage"] + String.Empty; }
            set { Session["ErrorMessage"] = value; }
        }

        private String _onLoadScript = String.Empty;

        public String OnLoadScript {
            get { return _onLoadScript; }
            set { _onLoadScript = value; }
        }

        protected void Page_Load(object sender, EventArgs e) {

            recentDealsRepeater.ItemDataBound += new RepeaterItemEventHandler(recentDealsRepeater_ItemDataBound);

            if (!Page.IsPostBack) {

                StationPageBase parent = (StationPageBase)this.Parent;

                int stationId = parent.StationId;

                headerLink.NavigateUrl = parent.GetUrl("~/Default.aspx");

                DollarSaverDB.StationRow station = parent.Station;

                //stationNameLabel.Text = station.Name;
                content1Label.Text = station.Content1;
                content2Label.Text = station.Content2;

                if (!station.IsStationUrlNull()) {
                    stationUrlHolder.Visible = true;
                    stationLink.Text = "Return to " + station.Name;
                    stationLink.NavigateUrl = station.StationUrl;
                } else {
                    stationUrlHolder.Visible = false;
                }

                if (File.Exists(Request.PhysicalApplicationPath + station.StationDirUrl + "station.css")) {
                    stationStyleSheet.Href = station.StationDirUrl + "station.css";
                } else {
                    stationStyleSheet.Href = "~/styles/station.css";
                }

                String ImageDir = Request.PhysicalApplicationPath + station.ImageDirUrl;
                if (!station.Content.IsTopperImageNull() && File.Exists(ImageDir + station.Content.TopperImage)) {
                    topperImage.ImageUrl = station.ImageDirUrl + station.Content.TopperImage;
                }
                if (!station.Content.IsTopperImageNull() && File.Exists(ImageDir + station.Content.TopperImage))
                {
                    topperImagem.ImageUrl = "~/" + station.ImageDirUrl + station.Content.TopperImage;
                }
                if (!station.Content.IsLocalSavingsImageNull() && File.Exists(ImageDir + station.Content.LocalSavingsImage))
                {
                    localSavingsImagem.ImageUrl = "~/" + station.ImageDirUrl + station.Content.LocalSavingsImage;
                }
                if (!station.Content.IsLocalSavingsImageNull() && File.Exists(ImageDir + station.Content.LocalSavingsImage)) {
                    localSavingsImage.ImageUrl = station.ImageDirUrl + station.Content.LocalSavingsImage;
                }

                if (!station.Content.IsHeaderImageNull() && File.Exists(ImageDir + station.Content.HeaderImage)) {
                    headerImage.ImageUrl = station.ImageDirUrl + station.Content.HeaderImage;
                }

                if (!station.Content.IsLogoImageNull() && File.Exists(ImageDir + station.Content.LogoImage)) {
                    logoImage.ImageUrl = station.ImageDirUrl + station.Content.LogoImage;
                }

                if (File.Exists(Request.PhysicalApplicationPath + station.StationDirUrl + "end_include.html")) {
                    endIncludeLiteral.Text = File.ReadAllText(Request.PhysicalApplicationPath + station.StationDirUrl + "end_include.html");
                }


                signUpTopLink.NavigateUrl = parent.GetUrl(signUpTopLink.NavigateUrl);
                signUpBottomLink.NavigateUrl = parent.GetUrl(signUpBottomLink.NavigateUrl);
                cartLink.NavigateUrl = parent.GetUrl(cartLink.NavigateUrl);
                contactLink.NavigateUrl = parent.GetUrl(contactLink.NavigateUrl);
                privacyLink.NavigateUrl = parent.GetUrl(privacyLink.NavigateUrl);
                termsLink.NavigateUrl = parent.GetUrl(termsLink.NavigateUrl);
				faqLink.NavigateUrl = parent.GetUrl(faqLink.NavigateUrl);
				rssLink.NavigateUrl = parent.GetUrl(rssLink.NavigateUrl);


                CertificateTableAdapter certificateAdapter = new CertificateTableAdapter();
                DollarSaverDB.CertificateDataTable onSaleDeals = certificateAdapter.GetOnSale(station.StationId);

                var latestDeals = 
                    (from DollarSaverDB.CertificateRow cert in onSaleDeals 
                     orderby cert.OnSaleDate descending select cert).Take(10);


                if (latestDeals.Count() > 0) {
                    recentDealsHolder.Visible = true;

                    recentDealsRepeater.DataSource = latestDeals;
                    recentDealsRepeater.DataBind();

                } else {
                    recentDealsHolder.Visible = false;
                }

            }
        }

        protected override void OnPreRender(EventArgs e) {
            base.OnPreRender(e);

            if (ErrorMessage != String.Empty) {
                errorMessageHolder.Visible = true;
                errorMessageLabel.Text = ErrorMessage;
                ErrorMessage = String.Empty;
            } else {
                errorMessageHolder.Visible = false;

                if (InfoMessage != String.Empty) {
                    messageHolder.Visible = true;
                    messageLabel.Text = InfoMessage;
                    InfoMessage = String.Empty;
                } else {
                    messageHolder.Visible = false;
                }
            }

            if (OnLoadScript != null && OnLoadScript != String.Empty) {
                body.Attributes["onload"] = OnLoadScript;
            }
        }


        void recentDealsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e) {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item) {
                DollarSaverDB.CertificateRow deal = (DollarSaverDB.CertificateRow)e.Item.DataItem;

                HyperLink advertiserImageLink = (HyperLink)e.Item.FindControl("advertiserImageLink");
                Image advertiserImage = (Image)e.Item.FindControl("advertiserImage");
                Label soldOutLabel = (Label)e.Item.FindControl("soldOutLabel");
                HyperLink advertiserLink = (HyperLink)e.Item.FindControl("advertiserLink");
                Label summaryLabel = (Label)e.Item.FindControl("summaryLabel");
                Label savingsLabel = (Label)e.Item.FindControl("savingsLabel");
                PlaceHolder newRowHolder = (PlaceHolder)e.Item.FindControl("newRowHolder");

                advertiserImage.ImageUrl = deal.Advertiser.LogoUrl;

				if (deal.Advertiser.IsLogoImageVertical) {
					advertiserImage.Width = 75;
					advertiserImage.Height = 125;
				} else {
					advertiserImage.Width = 125;
					advertiserImage.Height = 75;
				}

                advertiserImage.AlternateText = deal.AdvertiserName;
                advertiserImageLink.ToolTip = deal.AdvertiserName;
                advertiserImageLink.NavigateUrl = "~/Advertiser.aspx?advertiser_id=" + deal.Advertiser.AdvertiserId;
                 

                if (deal.QtyRemaining == 0) {
                    soldOutLabel.Visible = true;
                } else {
                    soldOutLabel.Visible = false;
                }

                advertiserLink.Text = deal.Advertiser.Name;
                advertiserLink.NavigateUrl = "~/Advertiser.aspx?advertiser_id=" + deal.AdvertiserId;
                
                //valueLabel.Text = deal.FaceValue.ToString("$0.00");
                //priceLabel.Text = deal.DiscountValue.ToString("$0.00");


                summaryLabel.Text = "Price: " + deal.DiscountValue.ToString("$#,0.00");


                String savings = "";

                if (deal.DiscountTypeId == 1) {
                    savings += deal.Discount.ToString("0") + "%";
                } else {
                    savings += deal.Discount.ToString("$0.00");
                }

                savingsLabel.Text = savings;

                if ((e.Item.ItemIndex + 1) % 2 == 0) {
                    newRowHolder.Visible = true;
                } else {
                    newRowHolder.Visible = false;
                }
            }
        }

    }
}