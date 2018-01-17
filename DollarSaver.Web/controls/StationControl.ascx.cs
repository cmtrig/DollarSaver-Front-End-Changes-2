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
using System.Text.RegularExpressions;

using DollarSaver.Core.Common;
using DollarSaver.Core.Data;
using DollarSaver.Core.Data.DollarSaverDBTableAdapters;
using DollarSaver.Core.Encryption;

namespace DollarSaver.Web.controls {
    public partial class StationControl : AdminBaseControl {

        private DollarSaverDB.StationRow _station;

        public DollarSaverDB.StationRow Station {
            get { return _station; }
            set { _station = value; }
        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
        
            saveButton.Click += new EventHandler(saveButton_Click);
            cancelButton.Click += new EventHandler(cancelButton_Click);
            cancelButton.CausesValidation = false;

            if (!Page.IsPostBack) {

                if (IsSuperAdmin) {
                    linkHolder.Visible = false;
                    superAdminHolder.Visible = true;
                } else {
                    linkHolder.Visible = true;
                    superAdminHolder.Visible = false;
                }

                StateTableAdapter stateAdapter = new StateTableAdapter();
                DollarSaverDB.StateDataTable states = stateAdapter.GetStates();

                stateList.DataSource = states.Rows;
                stateList.DataTextField = "Summary";
                stateList.DataValueField = "StateCode";
                stateList.DataBind();

                timeZoneList.Items.Add(new ListItem("Atlantic", "8"));
                timeZoneList.Items.Add(new ListItem("Eastern", "1"));
                timeZoneList.Items.Add(new ListItem("Central", "2"));
                timeZoneList.Items.Add(new ListItem("Mountain", "3"));
                timeZoneList.Items.Add(new ListItem("Mountain - Arizona", "4"));
                timeZoneList.Items.Add(new ListItem("Pacific", "5"));
                timeZoneList.Items.Add(new ListItem("Alaska", "6"));
                timeZoneList.Items.Add(new ListItem("Hawaii", "7"));

                if (Station == null) {
                    actionLabel.Text = "Create";
                    saveButton.Text = "Create";
                } else {
                    actionLabel.Text = "Edit";
                    saveButton.Text = "Save";

                    nameBox.Text = Station.Name;

                    if (IsSuperAdmin) {
                        codeBox.Text = Station.Code;
                    }

                    siteTypeList.SelectedValue = Station.SiteTypeId.ToString();
                    stationTypeList.SelectedValue = Station.StationTypeId.ToString();

                    if (!Station.IsPhoneNumberNull()) {
                        phoneNumberBox.Text = Station.PhoneNumber;
                    }

                    if (!Station.IsAddress1Null()) {
                        address1Box.Text = Station.Address1;
                    }

                    if (!Station.IsAddress2Null()) {
                        address2Box.Text = Station.Address2;
                    }

                    if (!Station.IsCityNull()) {
                        cityBox.Text = Station.City;
                    }

                    if (!Station.IsStateCodeNull()) {
                        stateList.SelectedValue = Station.StateCode;
                    }

                    if (!Station.IsZipCodeNull()) {
                        zipCodeBox.Text = Station.ZipCode;
                    }
                    timeZoneList.SelectedValue = Station.TimeZoneId.ToString();


                    //DateTime test = DateTime.Now;

                    //test = new DateTime(

                    //daylightSavingsBox.Checked = Station.AffectedByDST;

                    string urlDomain = String.Empty;

                    if (!Station.IsStationUrlNull() && Station.StationUrl != String.Empty) {
                        if (Station.StationUrl.StartsWith("https://")) {
                            urlDomain = Station.StationUrl.Substring(8);
                            stationUrlStart.SelectedValue = "https://";
                        } else {
                            urlDomain = Station.StationUrl.Substring(7);
                        }
                    }

                    stationUrlBox.Text = urlDomain;

                    if (!Station.IsSiteNameNull()) {
                        siteNameBox.Text = Station.SiteName;
                    }

                    if (!Station.IsContent1Null()) {
                        content1Box.Text = Station.Content1;
                    }

                    if (!Station.IsContent2Null()) {
                        content2Box.Text = Station.Content2;
                    }

                    //isActiveBox.Checked = Station.IsActive;
                    isSiteActiveBox.Checked = Station.IsSiteActive;
                }


            }
        }


        void saveButton_Click(object sender, EventArgs e) {

            if (Page.IsValid) {

                StationTableAdapter stationAdapter = new StationTableAdapter();

                String name = nameBox.Text.Trim();
                int siteTypeId = Int32.Parse(siteTypeList.SelectedValue);
                int stationTypeId = Int32.Parse(stationTypeList.SelectedValue);
                String phoneNumber = phoneNumberBox.Text.Trim();
                String address1 = address1Box.Text.Trim();
                String address2 = address2Box.Text.Trim();
                String city = cityBox.Text.Trim();
                String stateCode = stateList.SelectedValue;
                String zipCode = zipCodeBox.Text.Trim();
                int timeZoneId = Int32.Parse(timeZoneList.SelectedValue);
                //bool affectedByDST = daylightSavingsBox.Checked;
                String stationUrl = stationUrlBox.Text.Trim();
                String siteName = siteNameBox.Text.Trim();
                String content1 = content1Box.Text.Trim();
                String content2 = content2Box.Text.Trim();
                //bool isActive = isActiveBox.Checked;

                if (name == String.Empty) {
                    ErrorMessage = "Name is required";
                    return;
                }


                if (phoneNumber == String.Empty) {
                    ErrorMessage = "Phone Number is required";
                    return;
                }

                if (address1 == String.Empty) {
                    ErrorMessage = "Address 1 is required";
                    return;
                }

                if (city == String.Empty) {
                    ErrorMessage = "City is required";
                    return;
                }

                if (zipCode == String.Empty) {
                    ErrorMessage = "Zip Code is required";
                    return;
                }

                if (stationUrl != String.Empty) {
                    stationUrl = stationUrlStart.SelectedValue + stationUrl;

                    // come up with a better validation...
                    if (!Uri.IsWellFormedUriString(stationUrl, UriKind.Absolute)) {
                        ErrorMessage = "Please enter a valid Station Website";
                        return;
                    }
                }

                if (stationUrl.Length > 500) {
                    stationUrl = stationUrl.Substring(0, 500);
                }

                /*
                if (content1 == String.Empty) {
                    ErrorMessage = "Content 1 is required";
                    return;
                }*/

                if (content1.Length > 1000) {
                    content1 = content1.Substring(0, 1000);
                }
                

                /*
                if (content2 == String.Empty) {
                    ErrorMessage = "Content 2 is required";
                    return;
                }*/

                if (content2.Length > 1000) {
                    content2 = content2.Substring(0, 1000);
                }

                siteName = siteName.Replace("<br />", "");

                if (siteName.Length > 500) {
                    siteName = siteName.Substring(0, 500);
                }

                String siteNameCheck = Regex.Replace(siteName, "<[^>]+>", "").Replace("&nbsp;", "").Trim();

                if (siteNameCheck == String.Empty) { // only leftover formatting in site name 
                    siteName = String.Empty;
                }

                if (Station != null) {

                    Station.Name = name;
                    Station.SiteTypeId = Int32.Parse(siteTypeList.SelectedValue);
                    Station.StationTypeId = Int32.Parse(stationTypeList.SelectedValue);
                    Station.PhoneNumber = phoneNumber;
                    Station.Address1 = address1;
                    Station.Address2 = address2;
                    Station.City = city;
                    Station.StateCode = stateList.SelectedValue;
                    Station.ZipCode = zipCode;
                    Station.TimeZoneId = Int32.Parse(timeZoneList.SelectedValue);
                    //Station.AffectedByDST = daylightSavingsBox.Checked;
                    if (stationUrl != String.Empty) {
                        Station.StationUrl = stationUrl;
                    } else {
                        Station.SetStationUrlNull();
                    }

                    if (siteName != String.Empty) {
                        Station.SiteName = siteName;
                    } else {
                        Station.SetSiteNameNull();
                    }

                    Station.Content1 = content1;
                    Station.Content2 = content2;
                    //Station.IsActive = isActiveBox.Checked;
                    Station.IsSiteActive = isSiteActiveBox.Checked;

                    stationAdapter.Update(Station);
                    InfoMessage = "Station Updated";
                } else {


                    if (IsSuperAdmin) {


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

                        if (stationCodeLookup.Count == 1 && (Station == null || stationCodeLookup[0].StationId != Station.StationId)) {
                            InfoMessage = "Code is already in use";
                            return;
                        }

                        int stationId = Convert.ToInt32(stationAdapter.InsertPK(name, code, stationTypeId, siteTypeId, phoneNumber, address1, address2,
                           city, stateCode, zipCode, timeZoneId, false, DateTime.Now, DateTime.Now, true,
                           content1, content2, Globals.ConvertToNull(stationUrl), Globals.ConvertToNull(siteName), isSiteActiveBox.Checked, null));

                        Station = stationAdapter.GetStation(stationId)[0];

                        CategoryTableAdapter categoryAdapter = new CategoryTableAdapter();
                        categoryAdapter.Insert(stationId, null, "Restaurants & Food", 1);
                        categoryAdapter.Insert(stationId, null, "Things To Do", 2);
                        categoryAdapter.Insert(stationId, null, "Home & Garden", 3);
                        categoryAdapter.Insert(stationId, null, "Health & Beauty", 4);
                        categoryAdapter.Insert(stationId, null, "Retail", 5);
                        categoryAdapter.Insert(stationId, null, "Automotive", 6);

                        StationContentTableAdapter stationContentTableAdapter = new StationContentTableAdapter();
                        stationContentTableAdapter.Insert(stationId, null, null, null, null, null, null, null, null, null);

                        DealSettingsTableAdapter dealSettingsAdapter = new DealSettingsTableAdapter();
                        dealSettingsAdapter.Insert(stationId, 1, 8, 4, 10);

                        SpecialSettingsTableAdapter specialSettingAdapter = new SpecialSettingsTableAdapter();
                        specialSettingAdapter.Insert(stationId, true);

                        if (!Directory.Exists(Request.PhysicalApplicationPath + Station.StationDirUrl)) {
                            Directory.CreateDirectory(Request.PhysicalApplicationPath + Station.StationDirUrl);
                        }

                        if (!Directory.Exists(Request.PhysicalApplicationPath + Station.ImageDirUrl)) {
                            Directory.CreateDirectory(Request.PhysicalApplicationPath + Station.ImageDirUrl);
                        }
                    }
                }

                RedirectToHomePage();
            }

        }

        void cancelButton_Click(object sender, EventArgs e) {
            RedirectToHomePage();
        }



    }
}