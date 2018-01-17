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
    public partial class LineItemDetail : System.Web.UI.UserControl {

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
                    lineItemHolder.Visible = true;
                    notFoundHolder.Visible = false;


                    lineItemRepeater.DataSource = Order.LineItems.Rows;
                    lineItemRepeater.DataBind();

                    subTotalLabel.Text = Order.LineItems.SubTotal.ToString("$#,0.00");


                } else {
                    lineItemHolder.Visible = false;
                    notFoundHolder.Visible = true;
                }
            }

        }


        void lineItemRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e) {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) {

                DollarSaverDB.OrderLineItemRow lineItem = (DollarSaverDB.OrderLineItemRow)e.Item.DataItem;

                Label certificateNameLabel = (Label)e.Item.FindControl("certificateNameLabel");
                PlaceHolder nonPrintableHolder = (PlaceHolder)e.Item.FindControl("nonPrintableHolder");
                Label nonPrintableLabel = (Label)e.Item.FindControl("nonPrintableLabel");
                Label priceLabel = (Label)e.Item.FindControl("priceLabel");
                Label qtyLabel = (Label)e.Item.FindControl("qtyLabel");
                Label totalLabel = (Label)e.Item.FindControl("totalLabel");

                certificateNameLabel.Text = lineItem.Certificate.AdvertiserName;
                if (lineItem.DeliveryType != DeliveryType.Print) {
                    nonPrintableHolder.Visible = true;

                    if (lineItem.DeliveryType == DeliveryType.Ship) {
                        nonPrintableLabel.Text = "Certificate(s) will be mailed";
                    } else if (lineItem.DeliveryType == DeliveryType.PickUp) {
                        nonPrintableLabel.Text = Server.HtmlEncode(lineItem.DeliveryNote).Replace(Environment.NewLine, "<BR>");
                    }


                } else {
                    nonPrintableHolder.Visible = false;
                }
                
                priceLabel.Text = lineItem.DiscountValue.ToString("$#,0.00");
                qtyLabel.Text = lineItem.Quantity.ToString();
                totalLabel.Text = lineItem.Total.ToString("$#,0.00");

            }
        }

    }
}