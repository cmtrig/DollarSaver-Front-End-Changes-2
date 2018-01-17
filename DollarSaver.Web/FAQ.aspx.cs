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

namespace DollarSaver.Web {
    public partial class FAQ : StationPageBase {

        public override string PageTitle {
            get {
                return  base.PageTitle + " - F.A.Q.";
            }
        }

        protected void Page_Load(object sender, EventArgs e) {

            if (Station.SiteTypeId == (int)SiteType.Standard) {
                standardHolder.Visible = true;
                dealOfTheWeekHolder.Visible = false;
            } else {
                standardHolder.Visible = false;
                dealOfTheWeekHolder.Visible = true;
            }

            /*
            String contactUsUrl = "http://";

            if (UseSubdomain) {
                contactUsUrl += Station.Subdomain + ".";
            }

            contactUsUrl += EnvDomain + ResolveUrl("~/contact");

            if (!UseSubdomain) {
                contactUsUrl += "?station_id=" + StationId;
            }
            */

        }
    }
}