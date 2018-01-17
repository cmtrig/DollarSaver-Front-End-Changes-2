using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.IO;

using DollarSaver.Core.Data;
using DollarSaver.Core.Data.DollarSaverDBTableAdapters;

namespace DollarSaver.Web {

/// <summary>
/// Summary description for StationPageBase
/// </summary>
    public class SecureStationPageBase : CookieStationPageBase {


        protected override bool IsSecure {
            get { return true; }
        }

        /*
        override protected void CheckSecurity() {
            if (!IsDev && Request.Url.Scheme.ToLower() != "https") {
                Response.Redirect("https://" + Request.Url.Host + Request.Url.PathAndQuery);
            }
        }
        */

    }
}
