using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.Text.RegularExpressions;

using DollarSaver.Core.Data;
using DollarSaver.Core.Data.DollarSaverDBTableAdapters;

namespace DollarSaver.Web {
    public partial class MailingListSignUp : StationPageBase {

        
        public override string PageTitle {
            get {
                return  base.PageTitle + " - Mailing List Sign Up";
            }
        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);

            signUpButton.Click += new EventHandler(signUpButton_Click);
        
        }

        void signUpButton_Click(object sender, EventArgs e) {

            String emailAddress = emailBox.Text.Trim();
            String confirmEmailAddress = confirmEmailBox.Text.Trim();

            if (emailAddress == String.Empty) {
                ErrorMessage = "E-mail is required";
            }

            if (emailAddress != confirmEmailAddress) {
                ErrorMessage = "E-mail addresses do not match";
            }

            if (!Regex.IsMatch(emailAddress, @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$")) {
                ErrorMessage = "E-mail address is not valid";
            }

            CustomerContactTableAdapter customerContactAdapter = new CustomerContactTableAdapter();

            customerContactAdapter.Insert(StationId, DateTime.Now, emailAddress, null, null);

            InfoMessage = "E-mail address " + emailAddress + " has been added to the mailing list. Thank you!";
            RedirectToHomePage();
        }


    }
}
