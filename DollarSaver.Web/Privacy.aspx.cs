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
    public partial class Privacy : StationPageBase {

        public override string PageTitle {
            get {
                return base.PageTitle + " - Privacy Policy";
            }
        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);


            if (!Page.IsPostBack) {

                contactUsLink.NavigateUrl = GetUrl(contactUsLink.NavigateUrl);

            }
            
        }
    
    
    }


}