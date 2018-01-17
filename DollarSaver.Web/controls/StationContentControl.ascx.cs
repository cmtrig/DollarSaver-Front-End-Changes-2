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

using DollarSaver.Core.Common;
using DollarSaver.Core.Data;
using DollarSaver.Core.Data.DollarSaverDBTableAdapters;
using DollarSaver.Core.Encryption;

namespace DollarSaver.Web.controls {
    public partial class StationContentControl : AdminBaseControl {

        private String ImageDir = String.Empty;

        private DollarSaverDB.StationRow _station;

        public DollarSaverDB.StationRow Station {
            get { return _station; }
            set { _station = value; }
        }

        DollarSaverDB.StationContentRow stationContent;

        protected override void OnLoad(EventArgs e) {

            ImageDir = Request.PhysicalApplicationPath + Station.ImageDirUrl;

            saveButton.Click += new EventHandler(saveButton_Click);
            cancelButton.Click += new EventHandler(cancelButton_Click);

            StationContentTableAdapter stationContentAdapter = new StationContentTableAdapter();

            DollarSaverDB.StationContentDataTable stationContentTable = stationContentAdapter.GetStationContent(Station.StationId);

            if (stationContentTable.Count == 1) {
                stationContent = stationContentTable[0];
            }

            if (!Page.IsPostBack) {

                if (stationContent != null) {

                    if (!stationContent.IsHeaderImageNull() && File.Exists(ImageDir + stationContent.HeaderImage)) {
                        headerImage.ImageUrl = "~/" + Station.ImageDirUrl + stationContent.HeaderImage + "?" + DateTime.Now.ToString("yyyyMMddhhmmss");
                    } else {
                        resetHeaderHolder.Visible = false;
                    }

                    if (!stationContent.IsLogoImageNull() && File.Exists(ImageDir + stationContent.LogoImage)) {
                        logoImage.ImageUrl = "~/" + Station.ImageDirUrl + stationContent.LogoImage + "?" + DateTime.Now.ToString("yyyyMMddhhmmss");
                    } else {
                        resetLogoHolder.Visible = false;
                    }

                    if (!stationContent.IsTopperImageNull() && File.Exists(ImageDir + stationContent.TopperImage)) {
                        topperImage.ImageUrl = "~/" + Station.ImageDirUrl + stationContent.TopperImage + "?" + DateTime.Now.ToString("yyyyMMddhhmmss");
                    } else {
                        resetTopperHolder.Visible = false;
                    }

                    if (!stationContent.IsLocalSavingsImageNull() && File.Exists(ImageDir + stationContent.LocalSavingsImage)) {
                        localSavingsImage.ImageUrl = "~/" + Station.ImageDirUrl + stationContent.LocalSavingsImage + "?" + DateTime.Now.ToString("yyyyMMddhhmmss");
                    } else {
                        resetLocalSavingsHolder.Visible = false;
                    }

                    if (!stationContent.IsCertificateLogoImageNull() && File.Exists(ImageDir + stationContent.CertificateLogoImage)) {
                        certificateLogoImage.ImageUrl = "~/" + Station.ImageDirUrl + stationContent.CertificateLogoImage + "?" + DateTime.Now.ToString("yyyyMMddhhmmss");
                    } else {
                        resetCertificateLogoHolder.Visible = false;
                    }

                    if (Station.StationSiteType == SiteType.Standard) {

                        homePageImageMultiView.SetActiveView(standardView);

                        if (!stationContent.IsWeeklyDealImageNull() && File.Exists(ImageDir + stationContent.WeeklyDealImage)) {
                            weeklyDealImage.ImageUrl = "~/" + Station.ImageDirUrl + stationContent.WeeklyDealImage + "?" + DateTime.Now.ToString("yyyyMMddhhmmss");
                        } else {
                            resetWeeklyDealHolder.Visible = false;
                        }

                        if (!stationContent.IsDailyDealImageNull() && File.Exists(ImageDir + stationContent.DailyDealImage)) {
                            dailyDealImage.ImageUrl = "~/" + Station.ImageDirUrl + stationContent.DailyDealImage + "?" + DateTime.Now.ToString("yyyyMMddhhmmss");
                        } else {
                            resetDailyDealHolder.Visible = false;
                        }

                        if (!stationContent.IsOtherDealsImageNull() && File.Exists(ImageDir + stationContent.OtherDealsImage)) {
                            otherDealsImage.ImageUrl = "~/" + Station.ImageDirUrl + stationContent.OtherDealsImage + "?" + DateTime.Now.ToString("yyyyMMddhhmmss");
                        } else {
                            resetOtherDealsHolder.Visible = false;
                        }

                    } else {

                        homePageImageMultiView.SetActiveView(dealOfTheWeekView);


                        if (!stationContent.IsDotwSubheaderImageNull() && File.Exists(ImageDir + stationContent.DotwSubheaderImage)) {
                            dotwSubheaderImage.ImageUrl = "~/" + Station.ImageDirUrl + stationContent.DotwSubheaderImage + "?" + DateTime.Now.ToString("yyyyMMddhhmmss");
                        } else {
                            resetDotwSubheaderHolder.Visible = false;
                        }

                    }


                }

            }
        }


        void saveButton_Click(object sender, EventArgs e) {

            if (Page.IsValid) {
                Globals.CheckDirectory(ImageDir);

                String headerImageName = null;
                if (headerUpload.HasFile && !resetHeaderBox.Checked) {
                    headerImageName = headerUpload.FileName.ToLower(); ;

                    String fileType = String.Empty;

                    if (headerImageName.EndsWith(".gif")) {
                        fileType = ".gif";
                    } else if (headerImageName.EndsWith(".jpg")) {
                        fileType = ".jpg";
                    } else if (headerImageName.EndsWith(".jpeg")) {
                        fileType = ".jpeg";
                    } else {
                        ErrorMessage = "Header image must be of type .gif or .jpg";
                        return;
                    }

                    headerImageName = "header" + fileType;
                }


                String logoImageName = null;
                if (logoUpload.HasFile && !resetLogoBox.Checked) {
                    logoImageName = logoUpload.FileName.ToLower(); ;

                    String fileType = String.Empty;

                    if (logoImageName.EndsWith(".gif")) {
                        fileType = ".gif";
                    } else if (logoImageName.EndsWith(".jpg")) {
                        fileType = ".jpg";
                    } else if (logoImageName.EndsWith(".jpeg")) {
                        fileType = ".jpeg";
                    } else {
                        ErrorMessage = "Logo image must be of type .gif or .jpg";
                        return;
                    }

                    logoImageName = "logo" + fileType;
                }

                String topperImageName = null;
                if (topperUpload.HasFile && !resetTopperBox.Checked) {
                    topperImageName = topperUpload.FileName.ToLower(); ;

                    String fileType = String.Empty;

                    if (topperImageName.EndsWith(".gif")) {
                        fileType = ".gif";
                    } else if (topperImageName.EndsWith(".jpg")) {
                        fileType = ".jpg";
                    } else if (topperImageName.EndsWith(".jpeg")) {
                        fileType = ".jpeg";
                    } else {
                        ErrorMessage = "Top image must be of type .gif or .jpg";
                        return;
                    }

                    topperImageName = "topper" + fileType;
                }

                String localSavingsImageName = null;
                if (localSavingsUpload.HasFile && !resetLocalSavingsBox.Checked) {
                    localSavingsImageName = localSavingsUpload.FileName.ToLower(); ;

                    String fileType = String.Empty;

                    if (localSavingsImageName.EndsWith(".gif")) {
                        fileType = ".gif";
                    } else if (localSavingsImageName.EndsWith(".jpg")) {
                        fileType = ".jpg";
                    } else if (localSavingsImageName.EndsWith(".jpeg")) {
                        fileType = ".jpeg";
                    } else {
                        ErrorMessage = "Local Savings image must be of type .gif or .jpg";
                        return;
                    }

                    localSavingsImageName = "local_savings" + fileType;
                }

                String certificateLogoImageName = null;
                if (certificateLogoUpload.HasFile && !resetCertificateLogoBox.Checked) {
                    certificateLogoImageName = certificateLogoUpload.FileName.ToLower(); ;

                    String fileType = String.Empty;

                    if (certificateLogoImageName.EndsWith(".gif")) {
                        fileType = ".gif";
                    } else if (certificateLogoImageName.EndsWith(".jpg")) {
                        fileType = ".jpg";
                    } else if (certificateLogoImageName.EndsWith(".jpeg")) {
                        fileType = ".jpeg";
                    } else {
                        ErrorMessage = "Certificate Logo image must be of type .gif or .jpg";
                        return;
                    }

                    certificateLogoImageName = "certificate_logo" + fileType;
                }


                String weeklyDealImageName = null;
                String dailyDealImageName = null;
                String otherDealsImageName = null;
                String dotwSubheaderImageName = null;

                if (Station.StationSiteType == SiteType.Standard) {

                    if (weeklyDealUpload.HasFile && !resetWeeklyDealBox.Checked) {
                        weeklyDealImageName = weeklyDealUpload.FileName.ToLower(); ;

                        String fileType = String.Empty;

                        if (weeklyDealImageName.EndsWith(".gif")) {
                            fileType = ".gif";
                        } else if (weeklyDealImageName.EndsWith(".jpg")) {
                            fileType = ".jpg";
                        } else if (weeklyDealImageName.EndsWith(".jpeg")) {
                            fileType = ".jpeg";
                        } else {
                            ErrorMessage = "Weekly Deal image must be of type .gif or .jpg";
                            return;
                        }

                        weeklyDealImageName = "weekly_deal" + fileType;
                    }

                    if (dailyDealUpload.HasFile && !resetDailyDealBox.Checked) {
                        dailyDealImageName = dailyDealUpload.FileName.ToLower(); ;

                        String fileType = String.Empty;

                        if (dailyDealImageName.EndsWith(".gif")) {
                            fileType = ".gif";
                        } else if (dailyDealImageName.EndsWith(".jpg")) {
                            fileType = ".jpg";
                        } else if (dailyDealImageName.EndsWith(".jpeg")) {
                            fileType = ".jpeg";
                        } else {
                            ErrorMessage = "Daily Deal image must be of type .gif or .jpg";
                            return;
                        }

                        dailyDealImageName = "daily_deal" + fileType;
                    }

                    if (otherDealsUpload.HasFile && !resetOtherDealsBox.Checked) {
                        otherDealsImageName = otherDealsUpload.FileName.ToLower(); ;

                        String fileType = String.Empty;

                        if (otherDealsImageName.EndsWith(".gif")) {
                            fileType = ".gif";
                        } else if (otherDealsImageName.EndsWith(".jpg")) {
                            fileType = ".jpg";
                        } else if (otherDealsImageName.EndsWith(".jpeg")) {
                            fileType = ".jpeg";
                        } else {
                            ErrorMessage = "Other Deals image must be of type .gif or .jpg";
                            return;
                        }

                        otherDealsImageName = "other_deal" + fileType;
                    }

                } else {


                    if (dotwSubheaderUpload.HasFile && !resetDotwSubheaderBox.Checked) {
                        dotwSubheaderImageName = dotwSubheaderUpload.FileName.ToLower(); ;

                        String fileType = String.Empty;

                        if (dotwSubheaderImageName.EndsWith(".gif")) {
                            fileType = ".gif";
                        } else if (dotwSubheaderImageName.EndsWith(".jpg")) {
                            fileType = ".jpg";
                        } else if (dotwSubheaderImageName.EndsWith(".jpeg")) {
                            fileType = ".jpeg";
                        } else {
                            ErrorMessage = "Subheader image must be of type .gif or .jpg";
                            return;
                        }

                        dotwSubheaderImageName = "dotw_subheader" + fileType;
                    }
                }


                StationContentTableAdapter stationContentAdapter = new StationContentTableAdapter();

                if (stationContent != null) {

                    if (headerImageName != null) {
                        stationContent.HeaderImage = headerImageName;
                        headerUpload.SaveAs(ImageDir + headerImageName);
                    } else if (resetHeaderBox.Checked) {
                        stationContent.SetHeaderImageNull();
                    }

                    if (logoImageName != null) {
                        stationContent.LogoImage = logoImageName;
                        logoUpload.SaveAs(ImageDir + logoImageName);
                    } else if (resetLogoBox.Checked) {
                        stationContent.SetLogoImageNull();
                    }

                    if (topperImageName != null) {
                        stationContent.TopperImage = topperImageName;
                        topperUpload.SaveAs(ImageDir + topperImageName);
                    } else if (resetTopperBox.Checked) {
                        stationContent.SetTopperImageNull();
                    }

                    if (localSavingsImageName != null) {
                        stationContent.LocalSavingsImage = localSavingsImageName;
                        localSavingsUpload.SaveAs(ImageDir + localSavingsImageName);
                    } else if (resetLocalSavingsBox.Checked) {
                        stationContent.SetLocalSavingsImageNull();
                    }

                    if (certificateLogoImageName != null) {
                        stationContent.CertificateLogoImage = certificateLogoImageName;
                        certificateLogoUpload.SaveAs(ImageDir + certificateLogoImageName);
                    } else if (resetCertificateLogoBox.Checked) {
                        stationContent.SetCertificateLogoImageNull();
                    }

                    if (Station.StationSiteType == SiteType.Standard) {

                        if (weeklyDealImageName != null) {
                            stationContent.WeeklyDealImage = weeklyDealImageName;
                            weeklyDealUpload.SaveAs(ImageDir + weeklyDealImageName);
                        } else if (resetWeeklyDealBox.Checked) {
                            stationContent.SetWeeklyDealImageNull();
                        }

                        if (dailyDealImageName != null) {
                            stationContent.DailyDealImage = dailyDealImageName;
                            dailyDealUpload.SaveAs(ImageDir + dailyDealImageName);
                        } else if (resetDailyDealBox.Checked) {
                            stationContent.SetDailyDealImageNull();
                        }

                        if (otherDealsImageName != null) {
                            stationContent.OtherDealsImage = otherDealsImageName;
                            otherDealsUpload.SaveAs(ImageDir + otherDealsImageName);
                        } else if (resetOtherDealsBox.Checked) {
                            stationContent.SetOtherDealsImageNull();
                        }

                    } else {

                        if (dotwSubheaderImageName != null) {
                            stationContent.DotwSubheaderImage = dotwSubheaderImageName;
                            dotwSubheaderUpload.SaveAs(ImageDir + dotwSubheaderImageName);
                        } else if (resetDotwSubheaderBox.Checked) {
                            stationContent.SetDotwSubheaderImageNull();
                        }
                    }

                    stationContentAdapter.Update(stationContent);

                } else {

                    if (headerImageName != null) {
                        headerUpload.SaveAs(ImageDir + headerImageName);
                    }

                    if (logoImageName != null) {
                        logoUpload.SaveAs(ImageDir + logoImageName);
                    }

                    if (topperImageName != null) {
                        topperUpload.SaveAs(ImageDir + topperImageName);
                    }

                    if (localSavingsImageName != null) {
                        localSavingsUpload.SaveAs(ImageDir + localSavingsImageName);
                    }

                    if (certificateLogoImageName != null) {
                        certificateLogoUpload.SaveAs(ImageDir + certificateLogoImageName);
                    }

                    if (Station.StationSiteType == SiteType.Standard) {
                        if (weeklyDealImageName != null) {
                            weeklyDealUpload.SaveAs(ImageDir + weeklyDealImageName);
                        }

                        if (dailyDealImageName != null) {
                            dailyDealUpload.SaveAs(ImageDir + dailyDealImageName);
                        }

                        if (otherDealsImageName != null) {
                            otherDealsUpload.SaveAs(ImageDir + otherDealsImageName);
                        }
                    } else {
                        if (dotwSubheaderImageName != null) {
                            dotwSubheaderUpload.SaveAs(ImageDir + dotwSubheaderImageName);
                        }
                    }

                    stationContentAdapter.Insert(Station.StationId, headerImageName, logoImageName,
                        topperImageName, localSavingsImageName, weeklyDealImageName, dailyDealImageName, otherDealsImageName,
                        dotwSubheaderImageName, certificateLogoImageName);

                }


                InfoMessage = "Station Content Updated";


                RedirectPage();
            }

        }

        void cancelButton_Click(object sender, EventArgs e) {
            RedirectPage();
        }

        private void RedirectPage() {
            Response.Redirect("~/admin/StationContent.aspx");
        }

    }
}