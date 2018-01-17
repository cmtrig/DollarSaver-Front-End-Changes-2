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

using System.Collections.Generic;
using Terradue.ServiceModel.Syndication;
using System.Text;
using System.Xml;

using DollarSaver.Core.Common;
using DollarSaver.Core.Data;
using DollarSaver.Core.Data.DollarSaverDBTableAdapters;

namespace DollarSaver.Web {
    public partial class Rss : StationPageBase {

        public override string PageTitle {
            get {
                return "";
            }
        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);

            if (!Page.IsPostBack) {

                GenerateFeed();
            
            }
        
        }

        private void GenerateFeed() {

            CertificateTableAdapter certificateAdapter = new CertificateTableAdapter();
            

            String description = "";

            String siteUrl = "http://" + EnvDomain;
            String homePageUrl = siteUrl + ResolveUrl("~/Default.aspx") + "?station_id=" + StationId;

            //mainFeed.Title = TextSyndicationContent.CreatePlaintextContent(Station.SiteNamePlainText);
            //mainFeed.Links.Add(SyndicationLink.CreateSelfLink(new Uri(homePageUrl)));
            

            List<DollarSaverDB.CertificateRow> listCertificates = new List<DollarSaverDB.CertificateRow>();

            if (Station.StationSiteType == SiteType.Standard) {

                SpecialSettingsTableAdapter specialSettingsAdapter = new SpecialSettingsTableAdapter();
                DollarSaverDB.SpecialSettingsDataTable specialSettingsTable = specialSettingsAdapter.GetSpecialSettings(StationId);

                bool dailyHeader = false;
                if (specialSettingsTable.Count == 1) {
                    dailyHeader = specialSettingsTable[0].DailyHeader;
                }

                if (dailyHeader) {
                    description = "Daily Deal";
                } else {
                    description = "Weekly Deal";
                }

                DollarSaverDB.CertificateDataTable daily = certificateAdapter.GetSpecial(StationId, 1);
                if (daily.Count == 1) {
                    listCertificates.Add(daily[0]);
                }

            } else {

                description = "Deal of the Week";

                DollarSaverDB.CertificateDataTable certificateTable = certificateAdapter.GetCurrentDeal(StationId);

                if (certificateTable.Count == 1) {
                    listCertificates.Add(certificateTable[0]);
                }

            }

            List<SyndicationItem> items = new List<SyndicationItem>();

            foreach (DollarSaverDB.CertificateRow certificate in listCertificates) {
                SyndicationItem item = new SyndicationItem();

                item.Title = TextSyndicationContent.CreatePlaintextContent(certificate.AdvertiserName);

                String advertiserUrl = siteUrl + ResolveUrl("~/Advertiser.aspx?advertiser_id=" + certificate.AdvertiserId);

                item.Links.Add(SyndicationLink.CreateAlternateLink(new Uri(advertiserUrl)));


                StringBuilder content = new StringBuilder();


                if (certificate.Advertiser.LogoUrl != String.Empty) {

                    String logoUrl = siteUrl + ResolveUrl(certificate.Advertiser.LogoUrl);

                    int logoWidth = 125;
                    int logoHeight = 75;

                    if (certificate.Advertiser.IsLogoImageVertical) {
                        logoWidth = 75;
                        logoHeight = 125;
                    }

                    content.Append("<div>");
                    content.Append("<a href=\"" + advertiserUrl + "\">");
                    content.Append("<img src=\"" + logoUrl + "\"  alt=\"" + Server.HtmlEncode(certificate.AdvertiserName) + "\" style=\"border-color:#404040;border-width:1px;border-style:solid;height:" + logoHeight + "px;width:" + logoWidth + "px;\">");
                    content.Append("</a>");
                    content.Append("</div>");
                

                }

                content.Append("<div style=\"font-family: Arial;\">");
                
                //content.Append("<a href=\"" + advertiserUrl + "\" style=\"font-weight: bold; font-size: 1.3em; line-height: 1.7em;\">" + Server.HtmlEncode(certificate.AdvertiserName) + "</a>");
                
                content.Append("<p>");
                content.Append(certificate.Advertiser.Description);
                content.Append("</p>");
                content.Append("<a href=\"" + advertiserUrl + "\" style=\"font-weight: bold; font-size: 1.1em; line-height: 1.5em;\">" + Server.HtmlEncode(certificate.ShortName) + "<a>");
                content.Append("<p> ");
                content.Append(certificate.Description);
                content.Append("</p>");
                content.Append("<p style=\"font-weight: bold;\"> ");
                content.Append("Certificate Value: " + certificate.FaceValue.ToString("$0.00") + "<br />");
                content.Append("Price: <span style=\"color: red;\">" + certificate.DiscountValue.ToString("$0.00") + "</span><br />");
                content.Append("Your Savings: " + certificate.Savings + "<br />");
                content.Append("</p>");
                content.Append("<a href=\"" + advertiserUrl + "\" style=\"font-weight: bold; font-size: 1.1em; line-height: 1.5em;\">BUY NOW</a>");
                content.Append("</div>");
                content.Append("<div style=\"clear: both;\"></div>");


                item.Content = SyndicationContent.CreateHtmlContent(content.ToString());

                items.Add(item);
            }


            SyndicationFeed mainFeed = new SyndicationFeed(Station.SiteNamePlainText, description, new Uri(homePageUrl));

            mainFeed.Description = TextSyndicationContent.CreatePlaintextContent(description);

            mainFeed.Items = items;

            Response.ContentType = "application/rss+xml";

            using (XmlWriter xmlWriter = XmlWriter.Create(Response.OutputStream)) {

                Rss20FeedFormatter rssFeed = new Rss20FeedFormatter(mainFeed);
                rssFeed.WriteTo(xmlWriter);

            }

        }

    }
}