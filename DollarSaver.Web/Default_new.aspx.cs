using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.IO;
using System.Text.RegularExpressions;

using DollarSaver.Core.Data;
using DollarSaver.Core.Data.DollarSaverDBTableAdapters;

namespace DollarSaver.Web {
    public partial class Default_new : StationPageBase {

        private SiteType siteType;


        protected override void OnPreInit(EventArgs e) {
            base.OnPreInit(e);
            
            if (Station.SiteTypeId == (int)SiteType.DealOfTheWeek) {
                Page.MasterPageFile = "~/dealoftheweek.master";
            } else {
                if (StationId == 1) {
                    Page.MasterPageFile = "~/consumer_new.master";
                } else {
                    Page.MasterPageFile = "~/consumer.master";
                }
            }

        }


        protected void Page_Load(object sender, EventArgs e) {


            if (!Page.IsPostBack) {
                if (Request.RawUrl.StartsWith("/dollarsaver/index")) {
                    //Response.Redirect("~/Default.aspx?station_id=" + StationId);
                    DollarSaverRedirect("~/Default.aspx");
                }
                

                DateTime currentDate = DateTime.Now;
                currentDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day);

                StationTableAdapter stationAdapter = new StationTableAdapter();

                DollarSaverDB.StationRow station = stationAdapter.GetStation(StationId)[0];
                siteType = (SiteType)station.SiteTypeId;

                CertificateTableAdapter certificateAdapter = new CertificateTableAdapter();


                //stationNameLabel.Text = station.Name;
                if (!station.IsContent1Null()) {
                    content1Label.Text = station.Content1;
                }

                if (!station.IsContent2Null()) {
                    content2Label.Text = station.Content2;
                }

                String ImageDir = Request.PhysicalApplicationPath + station.ImageDirUrl;
                if (station.StationSiteType == SiteType.Standard) {
                    standardHolder.Visible = true;
                    dealHolder.Visible = false;

                    signUpTopLink.NavigateUrl = GetUrl(signUpTopLink.NavigateUrl);

                    SpecialSettingsTableAdapter specialSettingsAdapter = new SpecialSettingsTableAdapter();
                    DollarSaverDB.SpecialSettingsDataTable specialSettingsTable = specialSettingsAdapter.GetSpecialSettings(StationId);

                    bool dailyHeader = false;
                    if (specialSettingsTable.Count == 1) {
                        dailyHeader = specialSettingsTable[0].DailyHeader;
                    }

                    Image dailyWeeklyImage = new Image();

                    if (dailyHeader) {
                        //dailyWeeklyImage.SkinID = "dailyImage";

                        if (!station.Content.IsDailyDealImageNull() && File.Exists(ImageDir + station.Content.DailyDealImage)) {
                            dailyWeeklyImage.ImageUrl = station.ImageDirUrl + station.Content.DailyDealImage;
                        } else {
                            dailyWeeklyImage.ImageUrl = "~/images/boxtop_daily.gif";
                        }
                    } else {
                        //dailyWeeklyImage.SkinID = "weeklyImage";
                        if (!station.Content.IsWeeklyDealImageNull() && File.Exists(ImageDir + station.Content.WeeklyDealImage)) {
                            dailyWeeklyImage.ImageUrl = station.ImageDirUrl + station.Content.WeeklyDealImage;
                        } else {
                            dailyWeeklyImage.ImageUrl = "~/images/boxtop_weekly.gif";
                        }
                    }

                    dailyWeeklyImageHolder.Controls.Add(dailyWeeklyImage);                    

                    DollarSaverDB.CertificateDataTable daily = certificateAdapter.GetSpecial(StationId, 1);
                    if (daily.Count == 1) {
                        SpecialCert1.DisplayCertificate = daily[0];
                        if (HitCounterEnabled) {
                            LogHit(currentDate, daily[0].CertificateId, PageHitType.HomePage);
                        }
                    }

                    DollarSaverDB.CertificateDataTable cert2 = certificateAdapter.GetSpecial(StationId, 2);
                    if (cert2.Count == 1) {
                        SpecialCert2.DisplayCertificate = cert2[0];
                        if (HitCounterEnabled) {
                            LogHit(currentDate, cert2[0].CertificateId, PageHitType.HomePage);
                        }
                    }

                    DollarSaverDB.CertificateDataTable cert3 = certificateAdapter.GetSpecial(StationId, 3);
                    if (cert3.Count == 1) {
                        SpecialCert3.DisplayCertificate = cert3[0];
                        if (HitCounterEnabled) {
                            LogHit(currentDate, cert3[0].CertificateId, PageHitType.HomePage);
                        }
                    }

                    DollarSaverDB.CertificateDataTable cert4 = certificateAdapter.GetSpecial(StationId, 4);
                    if (cert4.Count == 1) {
                        SpecialCert4.DisplayCertificate = cert4[0];
                        if (HitCounterEnabled) {
                            LogHit(currentDate, cert4[0].CertificateId, PageHitType.HomePage);
                        }
                    }

                    if (!station.Content.IsOtherDealsImageNull() && File.Exists(ImageDir + station.Content.OtherDealsImage)) {
                        otherDealsImage.ImageUrl = station.ImageDirUrl + station.Content.OtherDealsImage;
                    }

                    if (!station.Content.IsLogoImageNull() && File.Exists(ImageDir + station.Content.LogoImage)) {
                        logoImage.ImageUrl = station.ImageDirUrl + station.Content.LogoImage;
                    }

                } else { // Deal of the Week
                    standardHolder.Visible = false;
                    dealHolder.Visible = true;

                    if (!Station.IsSiteNameNull() && Station.SiteName != String.Empty) {
                        dotwStationNameLabel.Text = Regex.Replace(Station.SiteName, "<[^>]+>", "");
                    } else {
                        dotwStationNameLabel.Text = "DollarSaver Deal of the Week";
                    }

                    if (!Station.Content.IsDotwSubheaderImageNull() && File.Exists(ImageDir + station.Content.DotwSubheaderImage)) {
                        subheaderCell.Style["background-image"] = "url(" + station.ImageDirUrl + station.Content.DotwSubheaderImage + ")";
                    } else {
                        subheaderCell.Style["background-image"] = "url(" + ResolveUrl("~/images/dotw_background.gif") + ")";
                    }

                    DollarSaverDB.CertificateDataTable certificateTable = certificateAdapter.GetCurrentDeal(StationId);

                    if (certificateTable.Count == 1) {
                        dealOfTheWeek.Deal = certificateTable[0];
                        if (HitCounterEnabled) {
                            LogHit(currentDate, certificateTable[0].CertificateId, PageHitType.HomePage);
                        }
                    }

                }
            }
        }

        protected override void OnPreRender(EventArgs e) {
            base.OnPreRender(e);

            if (!Page.IsPostBack) {
                if (siteType == SiteType.DealOfTheWeek) {
                    ((DealOfTheWeekMaster)Master).OnLoadScript = dealOfTheWeek.OnLoadScript;
                }
            }
        }

    }
}