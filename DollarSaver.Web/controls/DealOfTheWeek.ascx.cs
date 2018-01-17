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

    public partial class DealOfTheWeek : System.Web.UI.UserControl {
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

        protected DollarSaverDB.CertificateRow _deal;

        public DollarSaverDB.CertificateRow Deal {
            get { return _deal; }
            set { _deal = value; }
        }

        private String _onLoadScript = String.Empty;

        public String OnLoadScript {
            get { return _onLoadScript; }
            set { _onLoadScript = value; }
        }

        protected void Page_Load(object sender, EventArgs e) {

            addToCartButton.Click += new ImageClickEventHandler(addToCartButton_Click);
            upcomingDealsRepeater.ItemDataBound += new RepeaterItemEventHandler(upcomingDealsRepeater_ItemDataBound);

            if (!Page.IsPostBack) {

                StationPageBase parent = (StationPageBase)this.Page;

                if (Deal != null) {
                    dealHolder.Visible = true;
                    dealNotFoundHolder.Visible = false;

                    DateTime now = DateTime.Now;
                    now = TimeZoneInfo.ConvertTime(now, TimeZoneInfo.Local, Deal.Advertiser.Station.StationTimeZoneInfo);


                    todayLiteral.Text = now.ToString("MMM dd, yyyy HH:mm:ss");
                    saleDayLiteral.Text = Deal.AdjustedOnSaleDate.ToString("MMM dd, yyyy HH:mm:ss");

                    //stationName1Label.Text = Deal.Advertiser.Station.Name;
                    //stationName2Label.Text = Deal.Advertiser.Station.Name;
                    //stationName3Label.Text = Deal.Advertiser.Station.Name;

                    if (Deal.Advertiser.LogoUrl != String.Empty) {
                        advertiserImage.Visible = true;

						if (Deal.Advertiser.IsLogoImageVertical) {
							advertiserImage.Width = 75;
							advertiserImage.Height = 125;
						} else {
							advertiserImage.Width = 125;
							advertiserImage.Height = 75;
						}

                        advertiserImage.AlternateText = Deal.AdvertiserName;
                        advertiserImage.ToolTip = Deal.AdvertiserName;
                        advertiserImage.ImageUrl = Deal.Advertiser.LogoUrl;
                    } else {
                        advertiserImage.Visible = false;
                    }

                    if (Deal.Advertiser.FullAddress != String.Empty) {
                        addressLabel.Text = "<strong>Address</strong><br />" + Deal.Advertiser.FullAddress;
                    }

                    if (Deal.Advertiser.DisplayPhoneNumber != String.Empty) {
                        phoneLabel.Text = "<strong>Phone</strong><br />" + Deal.Advertiser.DisplayPhoneNumber;
                    }

                    advertiserNameLabel.Text = Deal.Advertiser.Name;
                    advertiserDescriptionLabel.Text = Deal.Advertiser.DisplayDescription;

                    if (!Deal.Advertiser.IsWebsiteUrlNull()) {
                        viewWebsiteLink.NavigateUrl = Deal.Advertiser.WebsiteUrl;
                    } else {
                        viewWebsiteLink.Visible = false;
                    }

                    nameLabel.Text = Deal.ShortName;
                    descriptionLabel.Text = Deal.Description;

                    quantityLabel.Text = Deal.QtyRemaining.ToString();
                    valueLabel.Text = Deal.FaceValue.ToString("$#,0.00");
                    priceLabel.Text = Deal.DiscountValue.ToString("$#,0.00");

                    if (Deal.DiscountTypeId == 1) {
                        savingsLabel.Text = Deal.Discount.ToString("0") + "%";
                    } else {
                        savingsLabel.Text = Deal.Discount.ToString("$#,0.00");
                    }

                    OnLoadScript = "countdown();";
                    if (Deal.AdjustedOnSaleDate > now) {
                        onSaleDateLabel.Text = "ON SALE " + Deal.AdjustedOnSaleDate.ToString("dddd").ToUpper() + " AT " + Deal.AdjustedOnSaleDate.ToString("hh:mm tt");
                        //OnLoadScript = "countdown(" + Deal.StartDate.Year + ", " + Deal.StartDate.Month + ", " + Deal.StartDate.Day + ", " + Deal.StartDate.Hour + ", " + Deal.StartDate.Minute + ");";
                        
                    } else {
                        onSaleDateLabel.Text = "ON SALE NOW";
                        onSaleDateLabel.Style["font-size"] =  "20px";
                    }

                    if (Deal.QtyRemaining == 0) {
                        mainTable.Attributes["class"] = "soldOut";
                        addToCartButton.Visible = false;
                        qtyDropDown.Visible = false;
                        qtyLabel.Visible = false;
                    } else {
                        addToCartButton.Visible = true;
                        qtyDropDown.Visible = true;
                        qtyLabel.Visible = true;

                        // form fields
                        certificateIdHidden.Value = Deal.CertificateId.ToString();
                        stationIdHidden.Value = Deal.Advertiser.StationId.ToString();

                        int startQty = 1;
                        if (Deal.MinPurchaseQty > 1) {
                            startQty = Deal.MinPurchaseQty;
                        }

                        if (startQty > Deal.QtyRemaining) {
                            startQty = Deal.QtyRemaining;
                        }

                        int endQty = 20;
                        if (Deal.MaxPurchaseQty > 0 && Deal.MaxPurchaseQty < 20) {
                            endQty = Deal.MaxPurchaseQty;
                        }

                        if (endQty > Deal.QtyRemaining) {
                            endQty = Deal.QtyRemaining;
                        }

                        for (int i = startQty; i <= endQty; i++) {
                            qtyDropDown.Items.Add(new ListItem(i.ToString()));
                        }

                    }


                    CertificateTableAdapter certificateAdapter = new CertificateTableAdapter();

                    DollarSaverDB.CertificateDataTable upcomingDeals = certificateAdapter.GetUpcomingDeals(Deal.Advertiser.StationId);

                    if (upcomingDeals.Count > 0) {
                        upcomingDealsHolder.Visible = true;

                        upcomingDealsRepeater.DataSource = upcomingDeals.Rows;
                        upcomingDealsRepeater.DataBind();

                    } else {
                        upcomingDealsHolder.Visible = false;
                    }

                    StationPageBase parentPage = (StationPageBase)this.Page;

                    String advertiserUrl = "http://";

                    if (parentPage.UseSubdomain) {
                        advertiserUrl += Deal.Advertiser.Station.Subdomain + ".";
                    }

                    advertiserUrl += parentPage.EnvDomain + ResolveUrl("~/Advertiser.aspx?advertiser_id=" + Deal.AdvertiserId);

                    advertiserUrlLiteral.Text = advertiserUrl;

                    twitterAdvertiserUrlLiteral.Text = advertiserUrl;
                    twitterTextLiteral.Text = Server.HtmlEncode("Save " + Deal.Savings + " at " + Deal.AdvertiserName);

                } else {
                    dealHolder.Visible = false;
                    dealNotFoundHolder.Visible = true;
                }
            }
        }


        void addToCartButton_Click(object sender, ImageClickEventArgs e) {
            String stationId = stationIdHidden.Value;
            String quantity = qtyDropDown.SelectedValue;
            String certificateId = certificateIdHidden.Value;

            Response.Redirect("~/Cart.aspx?station_id=" + stationId + "&cert_id=" + certificateId + "&qty=" + quantity);
            //DollarSaverRedirect("~/Cart.aspx?cert_id=" + certificateId + "&qty=" + quantity);
            
        }

        void upcomingDealsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e) {
            if(e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item) {
                DollarSaverDB.CertificateRow deal = (DollarSaverDB.CertificateRow)e.Item.DataItem;

                HyperLink advertiserLink = (HyperLink)e.Item.FindControl("advertiserLink");
                Label dealDateLabel = (Label) e.Item.FindControl("dealDateLabel");


                advertiserLink.Text = deal.Advertiser.Name;
                advertiserLink.NavigateUrl = "~/Advertiser.aspx?advertiser_id=" + deal.AdvertiserId;
                dealDateLabel.Text = deal.AdjustedOnSaleDate.ToString("dddd, MM/dd/yyyy");
            }
        }

    }

}
