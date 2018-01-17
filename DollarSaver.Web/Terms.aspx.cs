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

namespace DollarSaver.Web {
    public partial class Terms : StationPageBase {

        public override string PageTitle {
            get {
                return base.PageTitle + " - Terms of Agreement";
            }
        }


        protected void Page_Load(object sender, EventArgs e) {

        }
    }
}