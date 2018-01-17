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
    public partial class Error : ConsumerPageBase {



        protected void Page_Load(object sender, EventArgs e) {

            if (ErrorMessage != String.Empty) {
                errorLabel.Text = ErrorMessage;
                ErrorMessage = String.Empty;
            } 

        }
    }
}