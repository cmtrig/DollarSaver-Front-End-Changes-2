using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

using DollarSaver.Core.Data;
using DollarSaver.Core.Data.DollarSaverDBTableAdapters;

namespace DollarSaver.Web
{
    public partial class Sites : ConsumerPageBase {


        protected void Page_Load(object sender, EventArgs e) {
            stationRepeater.ItemDataBound += new RepeaterItemEventHandler(stationRepeater_ItemDataBound);

            if (!Page.IsPostBack) {


                StationTableAdapter stationAdapter = new StationTableAdapter();

                DollarSaverDB.StationDataTable stations = stationAdapter.GetActive();

                DollarSaverDB.StationDataTable activeSiteStations = new DollarSaverDB.StationDataTable();
                foreach (DollarSaverDB.StationRow station in stations) {
                    if (station.StationId != 1 && station.IsSiteActive) {
                        activeSiteStations.ImportRow(station);
                    }
                }


                stationRepeater.DataSource = activeSiteStations.Rows;
                stationRepeater.DataBind();

                endYearLabel.Text = DateTime.Now.ToString("yyyy");

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
        }

        void stationRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e) {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) {

                DollarSaverDB.StationRow station = (DollarSaverDB.StationRow)e.Item.DataItem;

                HyperLink logoLink = (HyperLink)e.Item.FindControl("logoLink");
                Image logoImage = (Image)e.Item.FindControl("logoImage");
                HyperLink stationLink = (HyperLink)e.Item.FindControl("stationLink");
                Label addressLabel = (Label)e.Item.FindControl("addressLabel");
            
                String ImageDir = Request.PhysicalApplicationPath + station.ImageDirUrl;
                if (!station.Content.IsLogoImageNull() && File.Exists(ImageDir + station.Content.LogoImage)) {
                    logoImage.ImageUrl = "~/" + station.ImageDirUrl + station.Content.LogoImage;
                    logoLink.NavigateUrl = "~/Default.aspx?station_id=" + station.StationId;
                } else {
                    logoLink.Visible = false;
                }

                stationLink.Text = station.SiteNamePlainText;
                stationLink.NavigateUrl = "~/Default.aspx?station_id=" + station.StationId;

                String address = String.Empty;

                if(!station.IsCityNull()) {
                    address += station.City;
                }

                if(!station.IsStateCodeNull()) {
                    if(address != String.Empty) {
                        address += ", ";
                    }

                    address += station.StateCode;
                }

                addressLabel.Text = address;
            }

        }


    }
}