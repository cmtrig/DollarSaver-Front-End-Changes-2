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

using System.IO;

using DollarSaver.Core.Data;
using DollarSaver.Core.Data.DollarSaverDBTableAdapters;

namespace DollarSaver.Web {
    public partial class ConsumerMaster : System.Web.UI.MasterPage {

        private String InfoMessage {
            get { return (String)Session["InfoMessage"] + String.Empty; }
            set { Session["InfoMessage"] = value; }
        }

        private String ErrorMessage {
            get { return (String)Session["ErrorMessage"] + String.Empty; }
            set { Session["ErrorMessage"] = value; }
        }

        private String _onLoadScript = String.Empty;

        public String OnLoadScript {
            get { return _onLoadScript; }
            set { _onLoadScript = value; }
        }

        protected void Page_Load(object sender, EventArgs e) {

            if (!Page.IsPostBack) {

                StationPageBase parent = (StationPageBase)this.Page;

                int stationId = parent.StationId;

                DollarSaverDB.StationRow station = parent.Station;


                headerLink.NavigateUrl = parent.GetUrl("~/Default.aspx");

                if (!station.IsStationUrlNull()) {
                    stationUrlHolder.Visible = true;
                    stationLink.Text = "Return to " + station.Name;
                    stationLink.NavigateUrl = station.StationUrl;
                } else {
                    stationUrlHolder.Visible = false;
                }


                if (File.Exists(Request.PhysicalApplicationPath + station.StationDirUrl + "station.css")) {
                    stationStyleSheet.Href = "~/" + station.StationDirUrl + "station.css";
                } else {
                    stationStyleSheet.Href = "~/styles/station.css";
                }
                
                String ImageDir = Request.PhysicalApplicationPath + station.ImageDirUrl;
                if (!station.Content.IsTopperImageNull() && File.Exists(ImageDir + station.Content.TopperImage)) {
                    topperImage.ImageUrl = "~/" + station.ImageDirUrl + station.Content.TopperImage;
                }

                if (!station.Content.IsLocalSavingsImageNull() && File.Exists(ImageDir + station.Content.LocalSavingsImage)) {
                    localSavingsImage.ImageUrl = "~/" + station.ImageDirUrl + station.Content.LocalSavingsImage;
                }

                if (!station.Content.IsHeaderImageNull() && File.Exists(ImageDir + station.Content.HeaderImage)) {
                    headerImage.ImageUrl = "~/" + station.ImageDirUrl + station.Content.HeaderImage;
                }


                if (File.Exists(Request.PhysicalApplicationPath + station.StationDirUrl + "end_include.html")) {
                    endIncludeLiteral.Text = File.ReadAllText(Request.PhysicalApplicationPath + station.StationDirUrl + "end_include.html");
                }


                //topMenu.Station = station;

                endYearLabel.Text = DateTime.Now.ToString("yyyy");

                signUpBottomLink.NavigateUrl = parent.GetUrl(signUpBottomLink.NavigateUrl);
                cartLink.NavigateUrl = parent.GetUrl(cartLink.NavigateUrl);
                contactLink.NavigateUrl = parent.GetUrl(contactLink.NavigateUrl);
                privacyLink.NavigateUrl = parent.GetUrl(privacyLink.NavigateUrl);
				termsLink.NavigateUrl = parent.GetUrl(termsLink.NavigateUrl);
				faqLink.NavigateUrl = parent.GetUrl(faqLink.NavigateUrl);
				rssLink.NavigateUrl = parent.GetUrl(rssLink.NavigateUrl);
            }
        }

        protected override void OnPreRender(EventArgs e) {
            base.OnPreRender(e);

            errorMessageHolder.Visible = false;
            messageHolder.Visible = false;

            if (ErrorMessage != String.Empty) {
                errorMessageHolder.Visible = true;
                errorMessageLabel.Text = ErrorMessage;
                ErrorMessage = String.Empty;
                InfoMessage = String.Empty;
            } else if (InfoMessage != String.Empty) {
                messageHolder.Visible = true;
                messageLabel.Text = InfoMessage;
                InfoMessage = String.Empty;
            }

            if (OnLoadScript != null && OnLoadScript != String.Empty) {
                body.Attributes["onload"] = OnLoadScript;
            }
        }
    }
}