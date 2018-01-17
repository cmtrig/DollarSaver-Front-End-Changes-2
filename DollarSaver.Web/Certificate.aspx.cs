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
    public partial class Certificate : StationPageBase {

        protected void Page_Load(object sender, EventArgs e) {

            int certificateId = GetIdFromQueryString();

            if (certificateId > 0) {

                CertificateTableAdapter certificateAdapter = new CertificateTableAdapter();

                DollarSaverDB.CertificateDataTable certificateTable = certificateAdapter.GetCertificate(certificateId);

                if (certificateTable.Rows.Count == 1) {

                    DollarSaverDB.CertificateRow certificate = certificateTable[0];

                    if (certificate.Advertiser.StationId != StationId) {
                        RedirectToHomePage();
                    }

                    certificateDetail.DisplayCertificate = certificate;
                } else {
                    RedirectToHomePage();
                }
            } else {
                RedirectToHomePage();
            }
        }

    }

}
