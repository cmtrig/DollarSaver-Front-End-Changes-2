using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DollarSaver.Core.Common;
using DollarSaver.Core.Data;
using DollarSaver.Core.Data.DollarSaverDBTableAdapters;

namespace DollarSaver.Web {
	public partial class TestSoldOut : CookieStationPageBase {


		protected override void OnLoad(EventArgs e) {
			base.OnLoad(e);


			sendSoldoutNoticeButton.Click += sendSoldoutNoticeButton_Click;
		
		}

		void sendSoldoutNoticeButton_Click(object sender, EventArgs e) {

			int certificateId = Convert.ToInt32(certificateIdBox.Text);


			CertificateTableAdapter certificateAdapter = new CertificateTableAdapter();
			DollarSaverDB.CertificateDataTable certificateLookup = certificateAdapter.GetCertificate(certificateId);

			if (certificateLookup.Count == 1) {

				NotifySoldOut(certificateLookup[0]);

				InfoMessage = "Sent Sold Out Notice for Certificate " + certificateLookup[0].ShortName;
			
			} else {

				ErrorMessage = "Certificate Not Found";
			
			}

		}

	}
}