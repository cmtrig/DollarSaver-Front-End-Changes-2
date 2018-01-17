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

using DollarSaver.Core.Common;
using DollarSaver.Core.Data;
using DollarSaver.Core.Data.DollarSaverDBTableAdapters;
using System.Collections.Generic;

namespace DollarSaver.Web.Admin {

    public partial class AdvertiserList : SalesPersonPageBase {
        protected void Page_Load(object sender, EventArgs e) {
            List<MenuItem> categoryMenu = new List<MenuItem>();
            
            advertiserGrid.RowDataBound += new GridViewRowEventHandler(advertiserGrid_RowDataBound);

            MenuItem allAdvertisers = new MenuItem("View All", "0", null, "/admin/AdvertiserList.aspx");
            categoryMenu.Add(allAdvertisers);

            bool inactive = Globals.ConvertToBool(Request.QueryString["inactive"]);

            String inactiveLinkAppender = String.Empty;
            if (inactive) {
                inactiveLinkAppender = "&inactive=1";
            }

            foreach (DollarSaverDB.CategoryRow category in Station.PrimaryCategories) {
                MenuItem item = new MenuItem(category.Name, category.CategoryId.ToString(), null, "/admin/AdvertiserList.aspx?id=" + category.CategoryId + inactiveLinkAppender);

                categoryMenu.Add(item);

            }

            categoryMenuRepeater.DataSource = categoryMenu;
            categoryMenuRepeater.DataBind();


            int categoryId = GetIdFromQueryString();

            DollarSaverDB.AdvertiserDataTable advertisers;


            if (categoryId > 0) {

                CategoryTableAdapter categoryAdapter = new CategoryTableAdapter();

                DollarSaverDB.CategoryDataTable categories = categoryAdapter.GetCategory(categoryId);

                if (categories.Count != 1) {
                    Response.Redirect("~/admin/AdvertiserList.aspx");
                }

                DollarSaverDB.CategoryRow category = categories[0];

                if (category.StationId != StationId) {
                    Response.Redirect("~/admin/AdvertiserList.aspx");
                }

                if (inactive) {
                    advertisers = category.InactiveAdvertisers;
                } else {
                    advertisers = category.ActiveAdvertisers;
                }
            } else {
                if (inactive) {
                    advertisers = Station.InactiveAdvertisers;
                } else {
                    advertisers = Station.ActiveAdvertisers;
                }
            }

            if (advertisers.Count > 0) {
                advertiserHolder.Visible = true;
                noAdvertiserHolder.Visible = false;
                advertiserGrid.DataSource = advertisers.Rows;
                advertiserGrid.DataBind();
            } else {
                advertiserHolder.Visible = false;
                noAdvertiserHolder.Visible = true;
            }

            if (inactive) {
                activeLinkHolder.Visible = true;
                inactiveLinkHolder.Visible = false;
                headerCell.Text = "Inactive Advertisers";
            } else {
                activeLinkHolder.Visible = false;
                inactiveLinkHolder.Visible = true;
            }

            if (CurrentUser.Role == AdminRole.Root || CurrentUser.Role == AdminRole.Admin) {
                categoryHolder.Visible = true;
            } else {
                categoryHolder.Visible = false;
            }


        }

        void advertiserGrid_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {
                DollarSaverDB.AdvertiserRow advertiser = (DollarSaverDB.AdvertiserRow) e.Row.DataItem;


                Image logoUrlImage = (Image)e.Row.FindControl("logoUrlImage");

                if (advertiser.IsLogoImageNull()) {
                    logoUrlImage.Visible = false;
                } else {
                    logoUrlImage.Visible = true;
                    logoUrlImage.ImageUrl = advertiser.LogoUrl;

                    if (advertiser.IsLogoImageVertical) {
                        logoUrlImage.Width = 30;
                        logoUrlImage.Height = 50;
                    } else {
                        logoUrlImage.Width = 50;
                        logoUrlImage.Height = 30;
                    }

                }

                GridView certificateGrid = (GridView)e.Row.FindControl("certificateGrid");
                certificateGrid.RowDataBound += new GridViewRowEventHandler(certificateGrid_RowDataBound);

                certificateGrid.DataSource = advertiser.ActiveCertificates.Rows;
                certificateGrid.DataBind();
            }
        }

        void certificateGrid_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {
                DollarSaverDB.CertificateRow certificate = (DollarSaverDB.CertificateRow)e.Row.DataItem;
                
                HyperLink certificateLink = (HyperLink)e.Row.FindControl("certificateLink");
                certificateLink.NavigateUrl = "CertificateEdit.aspx?id=" + certificate.CertificateId;
                certificateLink.Text = certificate.ShortName;

                if (certificate.QtyRemaining == 0) {
                    certificateLink.Text += " (Sold Out)";
                } else {
                    certificateLink.Text += " (" + certificate.QtyRemaining.ToString() + ")";
                }
            }
        }
    }
}