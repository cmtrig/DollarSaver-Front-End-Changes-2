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

    public partial class CertificateDetail : System.Web.UI.UserControl {
        protected DollarSaverDB.CertificateRow _displayCertificate;


        public DollarSaverDB.CertificateRow DisplayCertificate {
            get { return _displayCertificate; }
            set { _displayCertificate = value; }
        }


        protected void Page_Load(object sender, EventArgs e) {

            //addToCartButton.Click += new EventHandler(addToCartButton_Click); 
            addToCartButton.Click += new ImageClickEventHandler(addToCartButton_Click);

            if (!Page.IsPostBack) {
                if (DisplayCertificate != null) {
                    certHolder.Visible = true;
                    certNotFoundHolder.Visible = false;

                    nameLabel.Text = DisplayCertificate.ShortName;
                    descriptionLabel.Text = DisplayCertificate.Description;

                    if (DisplayCertificate.MinPurchaseQty > 1) {
                        minPurchaseQtyHolder.Visible = true;
                        minPurchaseQtyLabel.Text = DisplayCertificate.MinPurchaseQty.ToString();
                    } else {
                        minPurchaseQtyHolder.Visible = false;
                    }


                    quantityLabel.Text = DisplayCertificate.QtyRemaining.ToString();
                    valueLabel.Text = DisplayCertificate.FaceValue.ToString("$#,0.00");
                    priceLabel.Text = DisplayCertificate.DiscountValue.ToString("$#,0.00");


                    if (DisplayCertificate.DeliveryType == DeliveryType.Ship) {
                        shippingHolder.Visible = true;
                        pickUpHolder.Visible = false;
                    } else if (DisplayCertificate.DeliveryType == DeliveryType.PickUp) {
                        shippingHolder.Visible = false;
                        pickUpHolder.Visible = true;

                        deliveryNoteLabel.Text = Server.HtmlEncode(DisplayCertificate.DeliveryNote).Replace(Environment.NewLine, "<BR>");

                    } else {
                        shippingHolder.Visible = false;
                        pickUpHolder.Visible = false;
                    }

                    //if (DisplayCertificate.DiscountTypeId == 1) {
                    //    savingsLabel.Text = DisplayCertificate.Discount.ToString("0") + "%";
                    //} else {
                    //    savingsLabel.Text = DisplayCertificate.Discount.ToString("$0.00");
                    //}

                    topSavingsLabel.Text = DisplayCertificate.Savings;
                    savingsLabel.Text = DisplayCertificate.Savings;

                    if (DisplayCertificate.QtyRemaining == 0) {
                        mainTable.Attributes["class"] = "soldOut";
                        addToCartHolder.Visible = false;
                        notYetOnSaleHolder.Visible = false;
                    } else if (DisplayCertificate.OnSaleDate > DateTime.Now) {
                        mainTable.Attributes["class"] = "comingSoon";
                        addToCartHolder.Visible = false;
                        notYetOnSaleHolder.Visible = true;

                        onSaleDateLabel.Text = DisplayCertificate.AdjustedOnSaleDate.ToString("MM/dd/yyyy");
                        if (DisplayCertificate.AdjustedOnSaleDate.Hour != 0 || DisplayCertificate.AdjustedOnSaleDate.Minute != 0) {
                            onSaleDateLabel.Text += " " + DisplayCertificate.AdjustedOnSaleDate.ToString("hh:mm tt");
                        }
                    }  else {
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
            //DollarSaverRedirect("~/Cart.aspx?cert_id=" + certificateId + "&qty=" + quantity);
        }

    }

}
