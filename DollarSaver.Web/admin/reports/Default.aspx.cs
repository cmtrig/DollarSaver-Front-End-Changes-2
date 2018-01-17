using System;
using System.Collections;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using DollarSaver.Core.Data;
using DollarSaver.Core.Data.DollarSaverDBTableAdapters;

namespace DollarSaver.Web.Admin.Reports {

    /// <summary>
    /// Summary description for Default.
    /// </summary>
    public partial class Default : ManagerPageBase {
        protected Repeater reportTypeRepeater;

        protected override void OnInit(EventArgs e) {
            base.OnInit(e);

            if (ReadOnly) {
                Response.Redirect("~/admin/Default.aspx");
            }
        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);

            reportTypeRepeater.ItemDataBound += new RepeaterItemEventHandler(reportTypeRepeater_ItemDataBound);


        }

        protected override void OnPreRender(EventArgs e) {
            base.OnPreRender(e);


            ReportTypeTableAdapter reportTypeAdapter = new ReportTypeTableAdapter();
            DollarSaverDB.ReportTypeDataTable reportTypes = reportTypeAdapter.GetByRole((int) AdminRole.Admin);

            reportTypeRepeater.DataSource = reportTypes.Rows;
            reportTypeRepeater.DataBind();


        }


        private void reportTypeRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e) {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) {
                DollarSaverDB.ReportTypeRow reportType = (DollarSaverDB.ReportTypeRow)e.Item.DataItem;

                Label reportTypeNameLabel = (Label)e.Item.FindControl("reportTypeNameLabel");

                reportTypeNameLabel.Text = reportType.Name;

                Repeater reportRepeater = (Repeater)e.Item.FindControl("reportRepeater");

                reportRepeater.ItemDataBound += new RepeaterItemEventHandler(reportRepeater_ItemDataBound);

                ReportTableAdapter reportAdapter = new ReportTableAdapter();
                DollarSaverDB.ReportDataTable reports = reportAdapter.GetByType(reportType.ReportTypeId);

                reportRepeater.DataSource = reports.Rows;
                reportRepeater.DataBind();


            }


        }

        private void reportRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e) {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) {

                DollarSaverDB.ReportRow report = (DollarSaverDB.ReportRow)e.Item.DataItem;

                HyperLink runReportLink = (HyperLink)e.Item.FindControl("runReportLink");
                Label reportLabel = (Label)e.Item.FindControl("reportLabel");

                runReportLink.NavigateUrl = "RunReport.aspx?id=" + report.ReportId;

                reportLabel.Text = report.Name;

            }
        }
    }
}