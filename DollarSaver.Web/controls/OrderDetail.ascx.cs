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
using DollarSaver.Core.Encryption;

namespace DollarSaver.Web.controls {
    public partial class OrderDetail : System.Web.UI.UserControl {

        protected DollarSaverDB.OrderRow _order = null;

        public DollarSaverDB.OrderRow Order {
            get { return _order; }
            set { _order = value; }
        }



        protected void Page_Load(object sender, EventArgs e) {
            lineItemRepeater.ItemDataBound += new RepeaterItemEventHandler(lineItemRepeater_ItemDataBound);

        }


        protected override void OnPreRender(EventArgs e) {
            base.OnPreRender(e);


            if (!Page.IsPostBack) {


                if (Order != null) {
                    orderHolder.Visible = true;
                    notFoundHolder.Visible = false;


                    orderIdLabel.Text = Order.OrderId.ToString();
                    orderDateLabel.Text = Order.OrderDate.ToString("ddd MMM dd, yyyy hh:mm tt");
                    statusLabel.Text = Order.Status;
                    nameLabel.Text = Order.BillingName;
                    emailLabel.Text = Order.ShippingEmail;
                    addressLabel.Text = Order.BillingAddress;
                    phoneLabel.Text = Order.BillingPhone;

                    lineItemRepeater.DataSource = Order.LineItems.Rows;
                    lineItemRepeater.DataBind();

                    subTotalLabel.Text = Order.LineItems.SubTotal.ToString("$#,0.00");

                    if (!Order.IsGrandTotalNull()) {
                        orderTotalHolder.Visible = true;
                        orderTotalLabel.Text = Order.GrandTotal.ToString("$#,0.00");
                    } else {
                        orderTotalHolder.Visible = false;
                    }


                } else {
                    orderHolder.Visible = false;
                    notFoundHolder.Visible = true;
                }
            }

        }


        void lineItemRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e) {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) {

                DollarSaverDB.OrderLineItemRow lineItem = (DollarSaverDB.OrderLineItemRow)e.Item.DataItem;

                HyperLink certificateLink = (HyperLink)e.Item.FindControl("certificateLink");
                Label priceLabel = (Label)e.Item.FindControl("priceLabel");
                Label qtyLabel = (Label)e.Item.FindControl("qtyLabel");
                Label totalLabel = (Label)e.Item.FindControl("totalLabel");

                certificateLink.Text = lineItem.ShortName;
                certificateLink.NavigateUrl = "~/admin/CertificateEdit.aspx?id=" + lineItem.CertificateId;
                priceLabel.Text = lineItem.DiscountValue.ToString("$#,0.00");
                qtyLabel.Text = lineItem.Quantity.ToString();
                totalLabel.Text = lineItem.Total.ToString("$#,0.00");

                GridView numberGrid = (GridView)e.Item.FindControl("numberGrid");
                numberGrid.RowDataBound += new GridViewRowEventHandler(numberGrid_RowDataBound);

                numberGrid.DataSource = lineItem.Numbers.Rows;
                numberGrid.DataBind();

            }
        }

        void numberGrid_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {

                DollarSaverDB.CertificateNumberRow certNumber = (DollarSaverDB.CertificateNumberRow)e.Row.DataItem;

                HyperLink certificateLink = (HyperLink)e.Row.FindControl("certificateLink");

                String queryString = "id=" + certNumber.CertificateNumberId + "&order_id=" + Order.OrderId;

                String encryptedString = Server.UrlEncode(Cipher.Encrypt(queryString));

                certificateLink.NavigateUrl = "~/ViewCertificate.aspx?x=" + encryptedString;
                certificateLink.Text = certNumber.Number;
            }
        }

    }
}