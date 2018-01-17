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
using System.Data.SqlClient;

using DollarSaver.Core.Common;
using DollarSaver.Core.Data;
using DollarSaver.Core.Data.DollarSaverDBTableAdapters;

namespace DollarSaver.Web.Admin {

    public partial class AdvertiserEdit : SalesPersonPageBase {

        private int advertiserId;
        private DollarSaverDB.AdvertiserRow advertiser = null;

        protected void Page_Load(object sender, EventArgs e) {
            saveButton.Click += new EventHandler(saveButton_Click);
            cancelButton.Click += new EventHandler(cancelButton_Click);
            deleteButton.Click += new EventHandler(deleteButton_Click);
            deleteButton.Attributes["onclick"] = "javascript: return confirm('Are you sure want to delete this item?');";

            categoryList.SelectedIndexChanged += new EventHandler(categoryList_SelectedIndexChanged);


            advertiserId = GetIdFromQueryString();
            if (advertiserId > 0) {

                AdvertiserTableAdapter advertiserAdapter = new AdvertiserTableAdapter();

                DollarSaverDB.AdvertiserDataTable advetisers = advertiserAdapter.GetAdvertiser(advertiserId);

                if (advetisers.Count != 1) {
                    Response.Redirect("~/admin/AdvertiserList.aspx");
                }

                advertiser = advetisers[0];

                if (advertiser.StationId != StationId) {
                    Response.Redirect("~/admin/AdvertiserList.aspx");
                }

                if (advertiser.IsDeleted) {
                    InfoMessage = "Sorry, this advertiser has been deleted";
                    Response.Redirect("~/admin/AdvertiserList.aspx");
                }
            }

            if (!Page.IsPostBack) {

                categoryList.DataSource = Station.PrimaryCategories;
                categoryList.DataTextField = "Name";
                categoryList.DataValueField = "CategoryId";
                categoryList.DataBind();

                salesPersonList.DataSource = Station.ActiveSalesPeople.Rows;
                salesPersonList.DataValueField = "SalesPersonId";
                salesPersonList.DataTextField = "FullName";
                salesPersonList.DataBind();
                salesPersonList.Items.Insert(0, new ListItem("", "0"));

                StateTableAdapter stateAdapter = new StateTableAdapter();

                DollarSaverDB.StateDataTable states = stateAdapter.GetStates();

                stateList.DataSource = states.Rows;
                stateList.DataTextField = "Summary";
                stateList.DataValueField = "StateCode";
                stateList.DataBind();

                stateList.Items.Insert(0, new ListItem(String.Empty, String.Empty));

                if (advertiserId > 0) {
                    createEditLabel.Text = "Edit";

                    categoryList.SelectedValue = advertiser.CategoryId.ToString();
                    BindSubCategories(advertiser.CategoryId);

                    nameBox.Text = advertiser.Name;
                    address1Box.Text = advertiser.DisplayAddress1;
                    address2Box.Text = advertiser.DisplayAddress2;
                    cityBox.Text = advertiser.DisplayCity;

                    if (!advertiser.IsStateCodeNull()) {
                        stateList.SelectedValue = advertiser.StateCode;
                    }

                    zipCodeBox.Text = advertiser.DisplayZipCode;
                    isAddressMappableBox.Checked = advertiser.IsAddressMappable;
                    phoneNumberBox.Text = advertiser.DisplayPhoneNumber;

                    if (!advertiser.IsContactPersonNull()) {
                        contactPersonBox.Text = advertiser.ContactPerson;
                    }

                    string urlDomain = String.Empty;

                    if (!advertiser.IsWebsiteUrlNull() && advertiser.WebsiteUrl != String.Empty) {
                        if (advertiser.WebsiteUrl.StartsWith("https://")) {
                            urlDomain = advertiser.WebsiteUrl.Substring(8);
                            websiteUrlStart.SelectedValue = "https://";
                        } else {
                            urlDomain = advertiser.WebsiteUrl.Substring(7);
                        }
                    }

                    websiteBox.Text = urlDomain;

                    descriptionLengthLabel.Text = advertiser.DisplayDescription.Length.ToString();
                    descriptionBox.Text = advertiser.DisplayDescription;
                    isActiveBox.Checked = advertiser.IsActive;

                    if (advertiser.DisplayLogoImage != String.Empty) {
                        logoUrlImage.ImageUrl = advertiser.LogoUrl + "?" + DateTime.Now.ToString("yyyyMMddhhmmss");

                        if (advertiser.IsLogoImageVertical) {
                            logoUrlImage.Width = 75;
                            logoUrlImage.Height = 125;
                        } else {
                             logoUrlImage.Width = 125;
                             logoUrlImage.Height = 75; 
                        }

                        logoUrlImage.Visible = true;
                    }

                    if (!advertiser.IsSalesPersonIdNull() && salesPersonList.Items.FindByValue(advertiser.SalesPersonId.ToString()) == null) {

                        if (advertiser.SalesPerson != null) {
                            salesPersonList.Items.Add(new ListItem(advertiser.SalesPerson.FullName, advertiser.SalesPerson.SalesPersonId.ToString()));
                        }
                    }

                    if (!advertiser.IsSalesPersonIdNull()) {
                        salesPersonList.SelectedValue = advertiser.SalesPersonId.ToString();
                    }

                    if (advertiser.ActiveCertificates.Count > 0) {
                        noActiveFoundHolder.Visible = false;
                        activeCertHolder.Visible = true;

                        certificateGrid.DataSource = advertiser.ActiveCertificates.Rows;
                        certificateGrid.DataBind();
                    } else {
                        noActiveFoundHolder.Visible = true;
                        activeCertHolder.Visible = false;
                    }

                    if (advertiser.InactiveCertificates.Count > 0) {
                        inactiveCertHolder.Visible = true;
                        inactiveCertificateGrid.DataSource = advertiser.InactiveCertificates.Rows;
                        inactiveCertificateGrid.DataBind();
                    } else {
                        inactiveCertHolder.Visible = false;
                    }

                    newCertificateLink.NavigateUrl = "~/admin/CertificateEdit.aspx?advertiserId=" + advertiserId;

                    certificateHolder.Visible = true;

                } else {
                    deleteButton.Visible = false;
                    saveButton.Text = "Create";
                    createEditLabel.Text = "Create";
                    certificateHolder.Visible = false;

                    stateList.SelectedValue = Station.StateCode;
                    
                    BindSubCategories(Convert.ToInt32(categoryList.SelectedValue));
                }

            }

        }

        void categoryList_SelectedIndexChanged(object sender, EventArgs e) {
            int categoryId = Convert.ToInt32(categoryList.SelectedValue);

            BindSubCategories(categoryId);
        }


        private void BindSubCategories(int parentCategoryId) {
            CategoryTableAdapter categoryAdapter = new CategoryTableAdapter();

            DollarSaverDB.CategoryRow category = categoryAdapter.GetCategory(parentCategoryId)[0];

            if (category.SubCategories.Count > 0) {
                subCategory1Holder.Visible = true;

                subCategoryList1.DataSource = category.SubCategories.Rows;
                subCategoryList1.DataTextField = "Name";
                subCategoryList1.DataValueField = "CategoryId";
                subCategoryList1.DataBind();
                subCategoryList1.Items.Insert(0, new ListItem("", "0"));

                if (advertiser != null && advertiser.SubCategories.Count > 0 && 
                    subCategoryList1.Items.FindByValue(advertiser.SubCategories[0].CategoryId.ToString()) != null) {
                    subCategoryList1.SelectedValue = advertiser.SubCategories[0].CategoryId.ToString();
                } else {
                    subCategoryList1.SelectedValue = "0";
                }
            } else {
                subCategory1Holder.Visible = false;
            }


            if (category.SubCategories.Count > 1) {
                subCategory2Holder.Visible = true;

                subCategoryList2.DataSource = category.SubCategories.Rows;
                subCategoryList2.DataTextField = "Name";
                subCategoryList2.DataValueField = "CategoryId";
                subCategoryList2.DataBind();
                subCategoryList2.Items.Insert(0, new ListItem("", "0"));

                if (advertiser != null && advertiser.SubCategories.Count > 1 &&
                    subCategoryList2.Items.FindByValue(advertiser.SubCategories[1].CategoryId.ToString()) != null) {
                    subCategoryList2.SelectedValue = advertiser.SubCategories[1].CategoryId.ToString();
                } else {
                    subCategoryList2.SelectedValue = "0";
                }
            } else {
                subCategory2Holder.Visible = false;
            }


            if (category.SubCategories.Count > 2) {
                subCategory3Holder.Visible = true;

                subCategoryList3.DataSource = category.SubCategories.Rows;
                subCategoryList3.DataTextField = "Name";
                subCategoryList3.DataValueField = "CategoryId";
                subCategoryList3.DataBind();
                subCategoryList3.Items.Insert(0, new ListItem("", "0"));

                if (advertiser != null && advertiser.SubCategories.Count > 2 &&
                    subCategoryList3.Items.FindByValue(advertiser.SubCategories[2].CategoryId.ToString()) != null) {
                    subCategoryList3.SelectedValue = advertiser.SubCategories[2].CategoryId.ToString();
                } else {
                    subCategoryList3.SelectedValue = "0";
                }
            } else {
                subCategory3Holder.Visible = false;
            }

        }


        void saveButton_Click(object sender, EventArgs e) {

            if (Page.IsValid) {

                //String imageDir = Request.PhysicalApplicationPath + "/station/" + StationId + "/images/";
                Globals.CheckDirectory(ImageDir);

                int categoryId = Int32.Parse(categoryList.SelectedValue);
                String name = nameBox.Text.Trim();
                String address1 = address1Box.Text.Trim();
                String address2 = address2Box.Text.Trim();
                String city = cityBox.Text.Trim();
                String stateCode = stateList.SelectedValue;
                String zipCode = zipCodeBox.Text.Trim();
                bool isAddressMappable = isAddressMappableBox.Checked;
                String contactPerson = contactPersonBox.Text.Trim();
                String phoneNumber = phoneNumberBox.Text.Trim();
                String websiteUrl = websiteBox.Text.Trim();
                String description = descriptionBox.Text.Trim();
                int salesPersonId = Int32.Parse(salesPersonList.SelectedValue);
                bool isActive = isActiveBox.Checked;



                if (name == String.Empty) {
                    ErrorMessage = "Name cannot be blank";
                    return;
                }

                if (websiteUrl != String.Empty) {
                    websiteUrl = websiteUrlStart.SelectedValue + websiteUrl;

                    // come up with a better validation...
                    if (!Uri.IsWellFormedUriString(websiteUrl, UriKind.Absolute)) {
                        ErrorMessage = "Please enter a valid Advertiser Website";
                        return;
                    }
                } else {
                    websiteUrl = null;
                }


                if (address1 == String.Empty) {
                    address1 = null;
                }

                if (address2 == String.Empty) {
                    address2 = null;
                }

                if (city == String.Empty) {
                    city = null;
                }

                if (stateCode == String.Empty) {
                    stateCode = null;
                }

                if (zipCode == String.Empty) {
                    zipCode = null;
                }

                if (contactPerson == String.Empty) {
                    contactPerson = null;
                }
                
                if (phoneNumber == String.Empty) {
                    phoneNumber = null;
                }

                if (description == String.Empty) {
                    description = null;
                }

                if (description != null && description.Length > 3000) {
                    description = description.Substring(0, 3000);
                }

                int subCat1Id = 0;
                int subCat2Id = 0;
                int subCat3Id = 0;


                if (subCategory1Holder.Visible && subCategoryList1.SelectedValue != "0") {
                    subCat1Id = Int32.Parse(subCategoryList1.SelectedValue);
                }

                if (subCategory2Holder.Visible && subCategoryList2.SelectedValue != "0") {
                    subCat2Id = Int32.Parse(subCategoryList2.SelectedValue);

                    if (subCat2Id == subCat1Id) {
                        subCat2Id = 0;
                    }
                }

                if (subCategory3Holder.Visible && subCategoryList3.SelectedValue != "0") {
                    subCat3Id = Int32.Parse(subCategoryList3.SelectedValue);

                    if (subCat3Id == subCat1Id || subCat3Id == subCat2Id) {
                        subCat3Id = 0;
                    }
                }

                AdvertiserTableAdapter advertiserAdapter = new AdvertiserTableAdapter();

                if (advertiserId > 0) {

                    advertiser.CategoryId = categoryId;
                    advertiser.Name = name;

                    if (address1 == null) {
                        advertiser.SetAddress1Null();
                    } else {
                        advertiser.Address1 = address1;
                    }

                    if (address2 == null) {
                        advertiser.SetAddress2Null();
                    } else {
                        advertiser.Address2 = address2;
                    }

                    if (city == null) {
                        advertiser.SetCityNull();
                    } else {
                        advertiser.City = city;
                    }

                    if (zipCode == null) {
                        advertiser.SetZipCodeNull();
                    } else {
                        advertiser.ZipCode = zipCode;
                    }

                    advertiser.StateCode = stateCode;

                    advertiser.IsAddressMappable = isAddressMappable;

                    if (contactPerson == null) {
                        advertiser.SetContactPersonNull();
                    } else {
                        advertiser.ContactPerson = contactPerson;
                    }

                    if (phoneNumber == null) {
                        advertiser.SetPhoneNumberNull();
                    } else {
                        advertiser.PhoneNumber = phoneNumber;
                    }

                    if (websiteUrl == null) {
                        advertiser.SetWebsiteUrlNull();
                    } else {
                        advertiser.WebsiteUrl = websiteUrl;
                    }

                    String logoImage = String.Empty;
                    String fileType = String.Empty;
                    if (logoUpload.HasFile) {
                        logoImage = logoUpload.FileName.ToUpper(); ;

                        if (logoImage.EndsWith(".GIF")) {
                            fileType = ".gif";
                        } else if (logoImage.EndsWith(".JPG")) {
                            fileType = ".jpg";
                        } else if (logoImage.EndsWith(".JPEG")) {
                            fileType = ".jpeg";
                        } else {
                            ErrorMessage = "Image must be of type .gif or .jpg";
                            return;
                        }

                        System.Drawing.Image logoImageFile = System.Drawing.Image.FromStream(logoUpload.FileContent);

                        if (!(logoImageFile.Width == 125 && logoImageFile.Height == 75) &&
                            !(logoImageFile.Width == 75 && logoImageFile.Height == 125)) {

                            ErrorMessage = "Image must be 125px X 75px OR 75px X 125px";
                            return;
                        }

                        Boolean isLogoImageVertical = false;
                        if (logoImageFile.Width == 75) {
                            isLogoImageVertical = true;
                        }

                        logoImage = "advertiser_" + advertiserId + fileType;

                        advertiser.LogoImage = logoImage;
                        advertiser.IsLogoImageVertical = isLogoImageVertical;
                        logoUpload.SaveAs(ImageDir + logoImage);
                    }


                    if (description == null) {
                        advertiser.SetDescriptionNull();
                    } else {
                        advertiser.Description = description;
                    }

                    if (salesPersonId == 0) {
                        advertiser.SetSalesPersonIdNull();
                    } else {
                        advertiser.SalesPersonId = salesPersonId;
                    }

                    advertiser.IsActive = isActive;

                    advertiserAdapter.Update(advertiser);

                    CategoryTableAdapter categoryAdapter = new CategoryTableAdapter();
                    categoryAdapter.ClearSubCategoriesByAdvertiser(advertiserId);

                    if (subCat1Id > 0) {
                        categoryAdapter.AddSubCategoryToAdvertiser(advertiserId, subCat1Id);
                    }
                    if (subCat2Id > 0) {
                        categoryAdapter.AddSubCategoryToAdvertiser(advertiserId, subCat2Id);
                    }
                    if (subCat3Id > 0) {
                        categoryAdapter.AddSubCategoryToAdvertiser(advertiserId, subCat3Id);
                    }


                    InfoMessage = "Advertiser updated";

                    Response.Redirect("~/admin/AdvertiserList.aspx");
                } else {

                    advertiserId = Convert.ToInt32(advertiserAdapter.InsertPK(StationId, name, categoryId,
                        Globals.ConvertToNull(salesPersonId), address1, address2, city, stateCode, zipCode, 
                        contactPerson, phoneNumber, websiteUrl, null, description, isActive, false, isAddressMappable, false));

                    String logoImage = String.Empty;
                    String fileType = String.Empty;
                    if (logoUpload.HasFile) {
                        logoImage = logoUpload.FileName.ToUpper(); ;

                        if (logoImage.EndsWith(".GIF")) {
                            fileType = ".gif";
                        } else if (logoImage.EndsWith(".JPG")) {
                            fileType = ".jpg";
                        } else if (logoImage.EndsWith(".JPEG")) {
                            fileType = ".jpeg";
                        } else {
                            ErrorMessage = "Image must be of type .gif or .jpg";
                            return;
                        }

                        System.Drawing.Image logoImageFile = System.Drawing.Image.FromStream(logoUpload.FileContent);

                        if(!(logoImageFile.Width == 125 && logoImageFile.Height == 75) && 
                            !(logoImageFile.Width == 75 && logoImageFile.Height == 125)) {

                                ErrorMessage = "Image must be 125px X 75px OR 75px X 125px";
                                return;
                        }

                        Boolean isLogoImageVertical = false;
                        if (logoImageFile.Width == 75) {
                            isLogoImageVertical = true;
                        }

                        logoImage = "advertiser_" + advertiserId + fileType;

                        advertiser = advertiserAdapter.GetAdvertiser(advertiserId)[0];
                        advertiser.LogoImage = logoImage;
                        advertiser.IsLogoImageVertical = isLogoImageVertical;

                        logoUpload.SaveAs(ImageDir + logoImage);

                        advertiserAdapter.Update(advertiser);
                    }

                    CategoryTableAdapter categoryAdapter = new CategoryTableAdapter();
                    categoryAdapter.ClearSubCategoriesByAdvertiser(advertiserId);

                    if (subCat1Id > 0) {
                        categoryAdapter.AddSubCategoryToAdvertiser(advertiserId, subCat1Id);
                    }
                    if (subCat2Id > 0) {
                        categoryAdapter.AddSubCategoryToAdvertiser(advertiserId, subCat2Id);
                    }
                    if (subCat3Id > 0) {
                        categoryAdapter.AddSubCategoryToAdvertiser(advertiserId, subCat3Id);
                    }

                    
                    InfoMessage = "Advertiser created, please create a certificate for the advertiser";

                    Response.Redirect("~/admin/CertificateEdit.aspx?advertiserId=" + advertiserId);
                }
            }

        }


        void cancelButton_Click(object sender, EventArgs e) {
            Response.Redirect("~/admin/AdvertiserList.aspx");
        }

        void deleteButton_Click(object sender, EventArgs e) {

            /*
            try {
                AdvertiserTableAdapter advertiserAdapter = new AdvertiserTableAdapter();

                advertiserAdapter.Delete(advertiserId);

            } catch (SqlException ex) {
                if (ex.Number == 547) {
                    ErrorMessage = "Advertiser cannot be deleted due to database constraints. De-activate it instead.";
                    return;
                } else {
                    throw ex;
                }
            }
             * */
            if (advertiserId > 0) {
                AdvertiserTableAdapter advertiserAdapter = new AdvertiserTableAdapter();
                advertiser.IsDeleted = true;

                advertiserAdapter.Update(advertiser);
                InfoMessage = "Advertiser deleted";
                Response.Redirect("~/admin/AdvertiserList.aspx");
            }
        }
    }
}
