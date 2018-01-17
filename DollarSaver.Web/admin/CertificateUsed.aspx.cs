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

using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;

using DollarSaver.Core.Common;
using DollarSaver.Core.Data;
using DollarSaver.Core.Data.DollarSaverDBTableAdapters;
using DollarSaver.Core.Encryption;

namespace DollarSaver.Web.Admin {

    public partial class CertificateUsed : SalesPersonPageBase {

        private int certificateId = 0;
        private DollarSaverDB.CertificateRow certificate;

        protected void Page_Load(object sender, EventArgs e) {

            certificateId = GetIdFromQueryString();

            CertificateTableAdapter certificateAdapter = new CertificateTableAdapter();


            DollarSaverDB.CertificateDataTable certificateTable = certificateAdapter.GetCertificate(certificateId);

            if (certificateTable.Count != 1) {
                Response.Redirect("~/admin/AdvertiserList.aspx");
            }

            certificate = certificateTable[0];

            if (certificate.Advertiser.StationId != StationId) {
                Response.Redirect("~/admin/AdvertiserList.aspx");
            }

            if (certificate.Advertiser.IsDeleted) {
                InfoMessage = "Sorry, this advertiser has been deleted";
                Response.Redirect("~/admin/AdvertiserList.aspx");
            }

            if (!Page.IsPostBack) {

                returnToCertificateLink.NavigateUrl = "~/admin/CertificateEdit.aspx?id=" + certificate.CertificateId;

                advertiserNameLabel.Text = certificate.Advertiser.Name;
                certificateNameLabel.Text = certificate.ShortName;

                DollarSaverDB.CertificateNumberDataTable usedNumbers = certificate.UsedNumbers;



                usedCountLabel.Text = usedNumbers.Count.ToString();

                usedNumbers.DefaultView.Sort = "CreateDate";

                var sortedNumbers = from DollarSaverDB.CertificateNumberRow cn in usedNumbers 
                                    orderby cn.CreateDate.ToString("yyyyMMdd") descending, cn.OrderId ascending select cn;

                usedNumberGrid.DataSource = sortedNumbers;
                usedNumberGrid.DataBind();

            }

        }
    }
}