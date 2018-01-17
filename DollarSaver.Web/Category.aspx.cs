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


using DollarSaver.Core.Data;
using DollarSaver.Core.Data.DollarSaverDBTableAdapters;
using DollarSaver.Web.controls;

namespace DollarSaver.Web {
    public partial class Category : StationPageBase {
        private DollarSaverDB.CategoryRow category;

        public override string PageTitle {
            get {
                String title = base.PageTitle;

                if (category != null) {
                    title += " - " + category.Name;
                }

                return title;
            }
        }

        protected override int GetPageStationId() {
            return category != null ? category.StationId : 0;
        }

        protected override void OnPreInit(EventArgs e) {

            int categoryId = GetValueFromQueryString("category_id");

            if (categoryId > 0) {

                CategoryTableAdapter categoryAdapter = new CategoryTableAdapter();

                DollarSaverDB.CategoryDataTable categories = categoryAdapter.GetCategory(categoryId);

                if (categories.Count == 1) {
                    category = categories[0];
                }

            }

            base.OnPreInit(e);
        }

        protected void Page_Load(object sender, EventArgs e) {
            advertiserRepeater.ItemDataBound += new RepeaterItemEventHandler(advertiserRepeater_ItemDataBound);

            if (!Page.IsPostBack) {

                if (category != null) {

                    if (category.StationId != StationId) {
                        RedirectToHomePage();
                    }

                    LogPageHit();

                    categoryLabel.Text = category.Name;

                    if (category.ActiveAdvertisers.Count > 0) {
                        noAdvertisersFoundHolder.Visible = false;
                        advertiserRepeater.Visible = true;
                        advertiserRepeater.DataSource = category.ActiveAdvertisers.Rows;
                        advertiserRepeater.DataBind();
                    } else {
                        noAdvertisersFoundHolder.Visible = true;
                        advertiserRepeater.Visible = false;
                    }

                } else {


                    LogPageHit();

                    categoryLabel.Text = "All Deals";

                    if (Station.ActiveAdvertisers.Count > 0) {
                        noAdvertisersFoundHolder.Visible = false;
                        advertiserRepeater.Visible = true;
                        advertiserRepeater.DataSource = Station.ActiveAdvertisers.Rows;
                        advertiserRepeater.DataBind();
                    } else {
                        noAdvertisersFoundHolder.Visible = true;
                        advertiserRepeater.Visible = false;
                    }


                    //RedirectToHomePage();
                }

            }

        }

        void advertiserRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e) {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) {
                DollarSaverDB.AdvertiserRow advertiser = (DollarSaverDB.AdvertiserRow)e.Item.DataItem;


                Repeater certificateRepeater = (Repeater)e.Item.FindControl("certificateRepeater");
                certificateRepeater.ItemDataBound += new RepeaterItemEventHandler(certificateRepeater_ItemDataBound);

                certificateRepeater.DataSource = advertiser.ActiveCertificates.Rows;
                certificateRepeater.DataBind();

            }

        }

        void certificateRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e) {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) {
                DollarSaverDB.CertificateRow certificate = (DollarSaverDB.CertificateRow)e.Item.DataItem;

                CertificateBrief certificateBrief = (CertificateBrief)e.Item.FindControl("certificateBrief");

                certificateBrief.DisplayCertificate = certificate;
            }
        }

        private void LogPageHit() {

            if (HitCounterEnabled) {
                PageHitLogTableAdapter pageHitLogAdapter = new PageHitLogTableAdapter();

                DateTime currentDate = DateTime.Now;
                currentDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day);


                DollarSaverDB.AdvertiserDataTable activeAdvertisers;
                if (category != null) {
                    activeAdvertisers = category.ActiveAdvertisers;
                } else {
                    activeAdvertisers = Station.ActiveAdvertisers;
                }


                foreach (DollarSaverDB.AdvertiserRow advertiser in activeAdvertisers) {
                    foreach (DollarSaverDB.CertificateRow cert in advertiser.ActiveCertificates) {
                        LogHit(currentDate, cert.CertificateId, PageHitType.CategoryPage);
                    }
                }
            }
        }
    }
}