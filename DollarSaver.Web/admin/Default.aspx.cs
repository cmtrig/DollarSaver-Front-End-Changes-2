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

namespace DollarSaver.Web.Admin
{

    public partial class Default : AdminPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            runningLowGrid.RowDataBound += new GridViewRowEventHandler(runningLowGrid_RowDataBound);

            if (!Page.IsPostBack)
            {
                if (CurrentUser.Role == AdminRole.SalesRep)
                {
                    managerHolder.Visible = false;
                }
                else
                {
                    managerHolder.Visible = true;

                    OrderTableAdapter orderAdapter = new OrderTableAdapter();
                    DollarSaverDB.OrderDataTable recentOrders = orderAdapter.GetRecent(StationId, 10);

                    DollarSaverDB.OrderDataTable nonPrintableOrders = orderAdapter.GetRecentUnprintable(StationId, 10);

                    nonPrintableOrderGrid.DataSource = nonPrintableOrders.Rows;
                    nonPrintableOrderGrid.DataBind();

                    recentOrderGrid.DataSource = recentOrders.Rows;
                    recentOrderGrid.DataBind();
                }

                CertificateTableAdapter certificateAdapter = new CertificateTableAdapter();
                DollarSaverDB.CertificateDataTable runningLowCertificates = certificateAdapter.GetRunningLow(StationId);

                runningLowGrid.DataSource = runningLowCertificates.Rows;
                runningLowGrid.DataBind();
            }

        }

        void runningLowGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DollarSaverDB.CertificateRow certificate = (DollarSaverDB.CertificateRow)e.Row.DataItem;
                Label qtyLabel = (Label)e.Row.FindControl("qtyLabel");

                if (certificate.QtyRemaining == 0)
                {
                    qtyLabel.Style["color"] = "red";
                    qtyLabel.Text = "Sold Out";
                }
                else
                {
                    qtyLabel.Text = certificate.QtyRemaining.ToString();
                }

                Label salesPersonLabel = (Label)e.Row.FindControl("salesPersonLabel");

                salesPersonLabel.Text = certificate.Advertiser.IsSalesPersonIdNull() ? String.Empty : certificate.Advertiser.SalesPerson.FullName;
            }
        }
    }
}