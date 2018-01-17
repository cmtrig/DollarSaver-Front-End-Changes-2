using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace DollarSaver.Web.controls {

    public partial class DealOfTheWeekName : System.Web.UI.UserControl {

        private const String DOLLARSAVERNAME = "<span style=\"color: #557700\"><b>Dollar</b><span style=\"font-weight: normal\">Saver</span> <b>Deal of the Week</b></span>";

        protected override void OnPreRender(EventArgs e) {
            base.OnPreRender(e);

            if (this.Page is StationPageBase) {

                StationPageBase parentPage = (StationPageBase)this.Page;

                if (!parentPage.Station.IsSiteNameNull() && parentPage.Station.SiteName != String.Empty) {
                    nameLabel.Text = parentPage.Station.SiteName;
                } else {
                    nameLabel.Text = DOLLARSAVERNAME;
                }
            } else {
                nameLabel.Text = DOLLARSAVERNAME;
            }


        }
    }
}
