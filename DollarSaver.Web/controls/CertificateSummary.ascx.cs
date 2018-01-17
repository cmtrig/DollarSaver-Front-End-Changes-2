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

namespace DollarSaver.Web.controls {
    public partial class CertificateSummary : System.Web.UI.UserControl {
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
        
        protected DollarSaverDB.CertificateRow _displayCertificate;

        public DollarSaverDB.CertificateRow DisplayCertificate {
            get { return _displayCertificate; }
            set { _displayCertificate = value; }
        }


        protected void Page_Load(object sender, EventArgs e) {

            addToCartButton.Click += new ImageClickEventHandler(addToCartButton_Click);
            //addToCartButton.Click += new EventHandler(addToCartButton_Click);

            if (!Page.IsPostBack) {
                if (DisplayCertificate != null) {
                    certHolder.Visible = true;
                    certNotFoundHolder.Visible = false;

                    if (DisplayCertificate.Advertiser.LogoUrl != String.Empty) {
                        advertiserImage.ImageUrl = DisplayCertificate.Advertiser.LogoUrl;

						if (DisplayCertificate.Advertiser.IsLogoImageVertical) {
							advertiserImage.Width = 75;
							advertiserImage.Height = 125;
						} else {
							advertiserImage.Width = 125;
							advertiserImage.Height = 75;
						}

                        advertiserImage.AlternateText = DisplayCertificate.AdvertiserName;
                        advertiserImage.ToolTip = DisplayCertificate.AdvertiserName;
                        advertiserImageLink.NavigateUrl = "~/Advertiser.aspx?advertiser_id=" + DisplayCertificate.Advertiser.AdvertiserId;
                    } else {
                        advertiserImage.Visible = false;
                        advertiserImageLink.Visible = false;
                    }

                    //Category category = CategoryController.GetCategory(advertiser.CategoryId);
                    categoryLink.Text = DisplayCertificate.Advertiser.AdvertiserCategory.Name;
                    categoryLink.NavigateUrl = "~/Category.aspx?category_id=" + DisplayCertificate.Advertiser.CategoryId;

                    nameLink.Text = DisplayCertificate.AdvertiserName;
                    nameLink.NavigateUrl = "~/Advertiser.aspx?advertiser_id=" + DisplayCertificate.AdvertiserId;

                    descriptionLabel.Text = DisplayCertificate.Description;
                    qtyRemainingLabel.Text = DisplayCertificate.QtyRemaining.ToString();
                    valueLabel.Text = DisplayCertificate.FaceValue.ToString("$#,0.00");
                    discountLabel.Text = DisplayCertificate.DiscountValue.ToString("$#,0.00");
                    savingsLabel.Text = DisplayCertificate.Savings;

                    advertiserLink.NavigateUrl = "~/Advertiser.aspx?advertiser_id=" + DisplayCertificate.AdvertiserId;

                    if (DisplayCertificate.QtyRemaining == 0) {
                        mainTable.Attributes["class"] = "soldOut";
                        addToCartHolder.Visible = false;
                        notYetOnSaleHolder.Visible = false;
                    } else if (DisplayCertificate.OnSaleDate > DateTime.Now) {
                        mainTable.Attributes["class"] = "comingSoon";
                        addToCartHolder.Visible = false;
                        notYetOnSaleHolder.Visible = true;

                        onSaleDateLabel.Text = DisplayCertificate.AdjustedOnSaleDate.ToString("MM/dd/yyyy");
                        if(DisplayCertificate.AdjustedOnSaleDate.Hour != 0 || DisplayCertificate.AdjustedOnSaleDate.Minute != 0) {
                            onSaleDateLabel.Text += " " + DisplayCertificate.AdjustedOnSaleDate.ToString("hh:mm tt");
                        }

                    } else {
                        addToCartHolder.Visible = true;
                        notYetOnSaleHolder.Visible = false;
                    }

                    // form fields
                    certificateIdHidden.Value = DisplayCertificate.CertificateId.ToString();
                    stationIdHidden.Value = DisplayCertificate.Advertiser.StationId.ToString();

                    int startQty = 1;
                    if (DisplayCertificate.MinPurchaseQty > 1) {
                        startQty = DisplayCertificate.MinPurchaseQty;
                    }

                    if (startQty > DisplayCertificate.QtyRemaining) {
                        startQty = DisplayCertificate.QtyRemaining;
                    }

                    int endQty = 20;
                    if (DisplayCertificate.MaxPurchaseQty > 0 && DisplayCertificate.MaxPurchaseQty < 20) {
                        endQty = DisplayCertificate.MaxPurchaseQty;
                    }

                    if (endQty > DisplayCertificate.QtyRemaining) {
                        endQty = DisplayCertificate.QtyRemaining;
                    }

                    for (int i = startQty; i <= endQty; i++) {
                        qtyDropDown.Items.Add(new ListItem(i.ToString()));
                    }


                    StationPageBase parentPage = (StationPageBase)this.Page;

                    String advertiserUrl = "http://";

                    if (parentPage.UseSubdomain) {
                        advertiserUrl += DisplayCertificate.Advertiser.Station.Subdomain + ".";
                    }

                    advertiserUrl += parentPage.EnvDomain + ResolveUrl("~/Advertiser.aspx?advertiser_id=" + DisplayCertificate.AdvertiserId);

                    advertiserUrlLiteral.Text = advertiserUrl;
               
                    twitterAdvertiserUrlLiteral.Text = advertiserUrl;
                    twitterTextLiteral.Text = Server.HtmlEncode("Save " + DisplayCertificate.Savings + " at " + DisplayCertificate.AdvertiserName);

                } else {
                    certHolder.Visible = false;
                    certNotFoundHolder.Visible = true;
                }
            }
        }


        void addToCartButton_Click(object sender, ImageClickEventArgs e) {
        //void addToCartButton_Click(object sender, EventArgs e) {
            String stationId = stationIdHidden.Value;
            String quantity = qtyDropDown.SelectedValue;
            String certificateId = certificateIdHidden.Value;

            Response.Redirect("~/Cart.aspx?station_id=" + stationId + "&cert_id=" + certificateId + "&qty=" + quantity);
            
        }
    }
}