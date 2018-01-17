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

namespace DollarSaver.Web.Admin {

    public partial class SpecialEdit : ManagerPageBase {


        StationSpecialTableAdapter specialAdapter = new StationSpecialTableAdapter();
        DollarSaverDB.StationSpecialDataTable specialTable = new DollarSaverDB.StationSpecialDataTable();
        DollarSaverDB.StationSpecialRow specialOne;
        DollarSaverDB.StationSpecialRow specialTwo;
        DollarSaverDB.StationSpecialRow specialThree;
        DollarSaverDB.StationSpecialRow specialFour;

        protected override void OnInit(EventArgs e) {
            base.OnInit(e);

            if (ReadOnly) {
                Response.Redirect("~/admin/");
            }
        }

        protected void Page_Load(object sender, EventArgs e) {
            saveButton.Click += new EventHandler(saveButton_Click);
            cancelButton.Click += new EventHandler(cancelButton_Click);


            if (Station.SiteTypeId != (int)SiteType.Standard) {
                Response.Redirect("~/admin/Default.aspx");
            }

            specialTable = specialAdapter.GetStationSpecial(StationId, 1);
            if (specialTable.Count == 1) {
                specialOne = specialTable[0];
            }
            specialTable = specialAdapter.GetStationSpecial(StationId, 2);
            if (specialTable.Count == 1) {
                specialTwo = specialTable[0];
            }
            specialTable = specialAdapter.GetStationSpecial(StationId, 3);
            if (specialTable.Count == 1) {
                specialThree = specialTable[0];
            }
            specialTable = specialAdapter.GetStationSpecial(StationId, 4);
            if (specialTable.Count == 1) {
                specialFour = specialTable[0];
            }

            if (!Page.IsPostBack) {


                CategoryTableAdapter categoryAdapter = new CategoryTableAdapter();
                DollarSaverDB.CategoryDataTable categories = categoryAdapter.GetPrimaryCategoriesByStation(StationId);

                CertificateTableAdapter certificateAdapter = new CertificateTableAdapter();
                DollarSaverDB.CertificateDataTable certificates = certificateAdapter.GetActive(StationId);

                //certificates.DefaultView.Sort 

                SpecialSettingsTableAdapter specialSettingsAdapter = new SpecialSettingsTableAdapter();
                DollarSaverDB.SpecialSettingsDataTable specialSettingsTable = specialSettingsAdapter.GetSpecialSettings(StationId);

                if (specialSettingsTable.Count == 1) {
                    if (specialSettingsTable[0].DailyHeader) {
                        dailyWeeklyList.SelectedValue = "1";
                    } else {
                        dailyWeeklyList.SelectedValue = "2";
                    }
                }

                // Daily/Weekly Special 1
                /*
                specialOneCategoryList.DataSource = categories;
                specialOneCategoryList.DataTextField = "Name";
                specialOneCategoryList.DataValueField = "CategoryId";
                specialOneCategoryList.DataBind();
                */

                specialOneCertificateList.DataSource = certificates.Rows;
                specialOneCertificateList.DataTextField = "AdvertiserAndCertificate";
                specialOneCertificateList.DataValueField = "CertificateId";
                specialOneCertificateList.DataBind();


                if (specialOne != null) {

                    if (!specialOne.IsCertificateIdNull()) {
                        //specialOneCertificateBox.Checked = true;

                        if (certificates.FindByCertificateId(specialOne.CertificateId) != null) {
                            specialOneCertificateList.SelectedValue = specialOne.CertificateId.ToString();
                        }

                    }
                    /*
                    else if (!specialOne.IsCategoryIdNull()) {
                        specialOneCategoryBox.Checked = true;

                        specialOneCategoryList.SelectedValue = specialOne.CategoryId.ToString();
                    } else {
                        specialOneRandomBox.Checked = true;
                    }
                     */
                }

                // Special 2
                specialTwoCategoryList.DataSource = categories;
                specialTwoCategoryList.DataTextField = "Name";
                specialTwoCategoryList.DataValueField = "CategoryId";
                specialTwoCategoryList.DataBind();

                specialTwoCertificateList.DataSource = certificates.Rows;
                specialTwoCertificateList.DataTextField = "AdvertiserAndCertificate";
                specialTwoCertificateList.DataValueField = "CertificateId";
                specialTwoCertificateList.DataBind();

                if (specialTwo != null) {

                    if (!specialTwo.IsCertificateIdNull()) {
                        specialTwoCertificateBox.Checked = true;

                        if (certificates.FindByCertificateId(specialTwo.CertificateId) != null) {
                            specialTwoCertificateList.SelectedValue = specialTwo.CertificateId.ToString();
                        }

                    } else if (!specialTwo.IsCategoryIdNull()) {
                        specialTwoCategoryBox.Checked = true;

                        specialTwoCategoryList.SelectedValue = specialTwo.CategoryId.ToString();
                    } else {
                        specialTwoRandomBox.Checked = true;
                    }
                }


                // Special 3
                specialThreeCategoryList.DataSource = categories;
                specialThreeCategoryList.DataTextField = "Name";
                specialThreeCategoryList.DataValueField = "CategoryId";
                specialThreeCategoryList.DataBind();

                specialThreeCertificateList.DataSource = certificates.Rows;
                specialThreeCertificateList.DataTextField = "AdvertiserAndCertificate";
                specialThreeCertificateList.DataValueField = "CertificateId";
                specialThreeCertificateList.DataBind();

                if (specialThree != null) {

                    if (!specialThree.IsCertificateIdNull()) {
                        specialThreeCertificateBox.Checked = true;

                        if (certificates.FindByCertificateId(specialThree.CertificateId) != null) {
                            specialThreeCertificateList.SelectedValue = specialThree.CertificateId.ToString();
                        }

                    } else if (!specialThree.IsCategoryIdNull()) {
                        specialThreeCategoryBox.Checked = true;

                        specialThreeCategoryList.SelectedValue = specialThree.CategoryId.ToString();
                    } else {
                        specialThreeRandomBox.Checked = true;
                    }
                }

                // Special 4
                specialFourCategoryList.DataSource = categories;
                specialFourCategoryList.DataTextField = "Name";
                specialFourCategoryList.DataValueField = "CategoryId";
                specialFourCategoryList.DataBind();

                specialFourCertificateList.DataSource = certificates.Rows;
                specialFourCertificateList.DataTextField = "AdvertiserAndCertificate";
                specialFourCertificateList.DataValueField = "CertificateId";
                specialFourCertificateList.DataBind();

                if (specialFour != null) {

                    if (!specialFour.IsCertificateIdNull()) {
                        specialFourCertificateBox.Checked = true;

                        if (certificates.FindByCertificateId(specialFour.CertificateId) != null) {
                            specialFourCertificateList.SelectedValue = specialFour.CertificateId.ToString();
                        }

                    } else if (!specialFour.IsCategoryIdNull()) {
                        specialFourCategoryBox.Checked = true;

                        specialFourCategoryList.SelectedValue = specialFour.CategoryId.ToString();
                    } else {
                        specialFourRandomBox.Checked = true;
                    }
                }

            }

        }

        void saveButton_Click(object sender, EventArgs e) {

            if (Page.IsValid) {

                specialTable = new DollarSaverDB.StationSpecialDataTable();

                bool specialOneNew = false;
                if (specialOne == null) {
                    specialOneNew = true;
                    specialOne = specialTable.NewStationSpecialRow();
                    specialOne.StationId = StationId;
                    specialOne.SeqNo = 1;
                }

                bool specialTwoNew = false;
                if (specialTwo == null) {
                    specialTwoNew = true;
                    specialTwo = specialTable.NewStationSpecialRow();
                    specialTwo.StationId = StationId;
                    specialTwo.SeqNo = 2;
                }

                bool specialThreeNew = false;
                if (specialThree == null) {
                    specialThreeNew = true;
                    specialThree = specialTable.NewStationSpecialRow();
                    specialThree.StationId = StationId;
                    specialThree.SeqNo = 3;
                }

                bool specialFourNew = false;
                if (specialFour == null) {
                    specialFourNew = true;
                    specialFour = specialTable.NewStationSpecialRow();
                    specialFour.StationId = StationId;
                    specialFour.SeqNo = 4;
                }

                // Special One
                //if (specialOneCertificateBox.Checked) {
                bool dailyHeader = false;
                if (dailyWeeklyList.SelectedValue == "1") {
                    dailyHeader = true;
                }

                SpecialSettingsTableAdapter specialSettingsAdapter = new SpecialSettingsTableAdapter();
                DollarSaverDB.SpecialSettingsDataTable specialSettingsTable = specialSettingsAdapter.GetSpecialSettings(StationId);

                if (specialSettingsTable.Count == 1) {
                    specialSettingsTable[0].DailyHeader = dailyHeader;
                    specialSettingsAdapter.Update(specialSettingsTable[0]);
                } else {
                    specialSettingsAdapter.Insert(StationId, dailyHeader);
                }

                specialOne.CertificateId = Convert.ToInt32(specialOneCertificateList.SelectedValue);
                specialOne.SetCategoryIdNull();

                /*
                } else if (specialOneCategoryBox.Checked) {
                    specialOne.CategoryId = Convert.ToInt32(specialOneCategoryList.SelectedValue);
                    specialOne.SetCertificateIdNull();
                } else {
                    specialOne.SetCertificateIdNull();
                    specialOne.SetCategoryIdNull();
                }
                 * */

                if (specialOneNew) {
                    specialTable.AddStationSpecialRow(specialOne);
                } else {
                    specialTable.ImportRow(specialOne);
                }

                // Special Two
                if (specialTwoCertificateBox.Checked) {
                    specialTwo.CertificateId = Convert.ToInt32(specialTwoCertificateList.SelectedValue);
                    specialTwo.SetCategoryIdNull();

                } else if (specialTwoCategoryBox.Checked) {
                    specialTwo.CategoryId = Convert.ToInt32(specialTwoCategoryList.SelectedValue);
                    specialTwo.SetCertificateIdNull();
                } else {
                    specialTwo.SetCertificateIdNull();
                    specialTwo.SetCategoryIdNull();
                }

                if (specialTwoNew) {
                    specialTable.AddStationSpecialRow(specialTwo);
                } else {
                    specialTable.ImportRow(specialTwo);
                }

                // Special Three
                if (specialThreeCertificateBox.Checked) {
                    specialThree.CertificateId = Convert.ToInt32(specialThreeCertificateList.SelectedValue);
                    specialThree.SetCategoryIdNull();

                } else if (specialThreeCategoryBox.Checked) {
                    specialThree.CategoryId = Convert.ToInt32(specialThreeCategoryList.SelectedValue);
                    specialThree.SetCertificateIdNull();
                } else {
                    specialThree.SetCertificateIdNull();
                    specialThree.SetCategoryIdNull();
                }

                if (specialThreeNew) {
                    specialTable.AddStationSpecialRow(specialThree);
                } else {
                    specialTable.ImportRow(specialThree);
                }

                // Special Four
                if (specialFourCertificateBox.Checked) {
                    specialFour.CertificateId = Convert.ToInt32(specialFourCertificateList.SelectedValue);
                    specialFour.SetCategoryIdNull();

                } else if (specialFourCategoryBox.Checked) {
                    specialFour.CategoryId = Convert.ToInt32(specialFourCategoryList.SelectedValue);
                    specialFour.SetCertificateIdNull();
                } else {
                    specialFour.SetCertificateIdNull();
                    specialFour.SetCategoryIdNull();
                }

                if (specialFourNew) {
                    specialTable.AddStationSpecialRow(specialFour);
                } else {
                    specialTable.ImportRow(specialFour);
                }



                specialAdapter.Update(specialTable);

                InfoMessage = "Specials updated";
                Response.Redirect("~/admin/Default.aspx");
            }

        }

        void cancelButton_Click(object sender, EventArgs e) {
            Response.Redirect("~/admin/Default.aspx");
        }

    }
}