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

using System.Text.RegularExpressions;

using DollarSaver.Core.Data;
using DollarSaver.Core.Data.DollarSaverDBTableAdapters;

namespace DollarSaver.Web.Admin.Super.StationAdmin {

    public partial class StationEdit : SuperAdminPageBase {

        protected DollarSaverDB.StationRow Station;
        protected StationTableAdapter stationAdapter = new StationTableAdapter();
        
        protected void Page_Load(object sender, EventArgs e) {
            saveButton.Click += new EventHandler(saveButton_Click);
            cancelButton.Click += new EventHandler(cancelButton_Click);


            int stationId = GetValueFromQueryString("station_id");

            if (stationId == 0) {
                RedirectToSuperAdminHome();
            }

            DollarSaverDB.StationDataTable stationTable = stationAdapter.GetStation(stationId);

            if (stationTable.Count == 0) {
                RedirectToSuperAdminHome();
            }

            Station = stationTable[0];

            if (!Page.IsPostBack) {


                nameLabel.Text = Station.Name;
                codeBox.Text = Station.Code;
                isActiveBox.Checked = Station.IsActive;

                if (!Station.IsSubdomainNull()) {
                    subdomainBox.Text = Station.Subdomain;
                }

            }
        }


        void saveButton_Click(object sender, EventArgs e) {

            if (Page.IsValid) {

                Station.IsActive = isActiveBox.Checked;

                String code = codeBox.Text.Trim().ToUpper();

                if (code == String.Empty) {
                    ErrorMessage = "Code is required";
                    return;
                }

                if (code.Length < 3 || code.Length > 20) {
                    ErrorMessage = "Code must be between 3 and 20 characters";
                    return;
                }

                DollarSaverDB.StationDataTable stationCodeLookup = stationAdapter.GetByCode(code);

                if (stationCodeLookup.Count == 1 && stationCodeLookup[0].StationId != Station.StationId) {
                    InfoMessage = "Code is already in use";
                    return;
                }

                Station.Code = code;

                String subdomain = subdomainBox.Text.Trim().ToLower();

                if (subdomain != String.Empty) {

                    if (!Regex.IsMatch(subdomain, @"\w+([-]\w+)*")) {
                        ErrorMessage = "Subdomain can only contain letters, numbers and dashes";
                        return;
                    }


                    DollarSaverDB.StationDataTable stationLookup = stationAdapter.GetBySubdomain(subdomain);

                    if (stationLookup.Count == 1 && stationLookup[0].StationId != Station.StationId) {
                        InfoMessage = "Subdomain is already in use";
                        return;
                    }

                    Station.Subdomain = subdomain;

                } else {
                    Station.SetSubdomainNull();
                }

                stationAdapter.Update(Station);
                InfoMessage = "Station Updated";


                RedirectToSuperAdminHome();
            }

        }

        void cancelButton_Click(object sender, EventArgs e) {
            RedirectToSuperAdminHome();
        }


    }
}