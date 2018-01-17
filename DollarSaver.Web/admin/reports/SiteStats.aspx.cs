using System;
using System.Collections;
using System.Web.UI;

using System.Collections.Generic;

//using ZedGraph;
//using ZedGraph.Web;

using DollarSaver.Core.Data;
using DollarSaver.Core.Data.DollarSaverDBTableAdapters;
using Newtonsoft.Json;

namespace DollarSaver.Web.Admin.Reports
{

    internal class PageHitSummary
    {

        public String Name = String.Empty;
        public int HomeUniqueHits = 0;
        public int HomeTotalHits = 0;
        public int CategoryUniqueHits = 0;
        public int CategoryTotalHits = 0;
        public int AdvertiserUniqueHits = 0;
        public int AdvertiserTotalHits = 0;

        public int TotalHits
        {
            get
            {
                return HomeTotalHits + CategoryTotalHits + AdvertiserTotalHits;
            }
        }
    }

    internal class PageHitSummaryComparer : IComparer<PageHitSummary>
    {
        public int Compare(PageHitSummary x, PageHitSummary y)
        {
            return x.TotalHits.CompareTo(y.TotalHits);
        }
    }


    public partial class SiteStats : ManagerPageBase
    {
        PageHitLogTableAdapter pageHitLogAdapter = new PageHitLogTableAdapter();

        int certCount = 0;
        ArrayList certNames = new ArrayList();

        List<PageHitSummary> certSummaries = new List<PageHitSummary>();

        public string seriesJson = string.Empty;
        public string labelsJson = string.Empty;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (ReadOnly)
            {
                Response.Redirect("~/admin/Default.aspx");
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            runButton.Click += new EventHandler(runButton_Click);
            cancelButton.Click += new EventHandler(cancelButton_Click);


            if (!Page.IsPostBack)
            {
                DateTime startDate = DateTime.Now;
                startDate = new DateTime(startDate.Year, startDate.Month, 1);
                startDateBox.Text = startDate.ToString("MM/dd/yyyy");

                startDateCalImage.ImageUrl = "~/images/calendar_button.gif";
                startDateCalImage.Attributes["OnClick"] = "showCalendarControl('" + startDateBox.ClientID + "');";

                DateTime endDate = startDate.AddMonths(1).AddDays(-1);
                endDateBox.Text = endDate.ToString("MM/dd/yyyy");

                endDateCalImage.ImageUrl = "~/images/calendar_button.gif";
                endDateCalImage.Attributes["OnClick"] = "showCalendarControl('" + endDateBox.ClientID + "');";
            }
        }


        void runButton_Click(object sender, EventArgs e)
        {

            DateTime startDate;
            DateTime endDate;

            try
            {
                startDate = Convert.ToDateTime(startDateBox.Text.Trim());
            }
            catch
            {
                InfoMessage = "Start Date is in the wrong format";
                return;
            }

            try
            {
                endDate = Convert.ToDateTime(endDateBox.Text.Trim());
            }
            catch
            {
                InfoMessage = "End Date is in the wrong format";
                return;
            }

            if (startDate > endDate)
            {
                InfoMessage = "Start Date must be before End Date";
                return;
            }


            foreach (DollarSaverDB.AdvertiserRow advertiser in Station.Advertisers)
            {

                foreach (DollarSaverDB.CertificateRow cert in advertiser.AllCertificates)
                {


                    int? homePageUniqueHitTotal = 0;
                    int? homePageTotalHitTotal = 0;

                    pageHitLogAdapter.Sum(startDate, endDate, cert.CertificateId, (int)PageHitType.HomePage, ref homePageUniqueHitTotal, ref homePageTotalHitTotal);


                    int? categoryPageUniqueHitTotal = 0;
                    int? categoryPageTotalHitTotal = 0;

                    pageHitLogAdapter.Sum(startDate, endDate, cert.CertificateId, (int)PageHitType.CategoryPage, ref categoryPageUniqueHitTotal, ref categoryPageTotalHitTotal);


                    int? advertiserPageUniqueHitTotal = 0;
                    int? advertiserPageTotalHitTotal = 0;

                    pageHitLogAdapter.Sum(startDate, endDate, cert.CertificateId, (int)PageHitType.AdvertiserPage, ref advertiserPageUniqueHitTotal, ref advertiserPageTotalHitTotal);

                    if (homePageUniqueHitTotal + homePageTotalHitTotal + categoryPageUniqueHitTotal + categoryPageTotalHitTotal +
                        advertiserPageUniqueHitTotal + advertiserPageTotalHitTotal > 0)
                    {

                        certCount++;

                        PageHitSummary certSummary = new PageHitSummary();
                        certSummary.Name = advertiser.Name + " - " + cert.ShortName;

                        if (certSummary.Name.Length > 30)
                        {
                            certSummary.Name = certSummary.Name.Substring(0, 27) + "...";
                        }

                        certSummary.HomeTotalHits = (int)homePageTotalHitTotal;
                        certSummary.HomeUniqueHits = (int)homePageUniqueHitTotal;
                        certSummary.CategoryTotalHits = (int)categoryPageTotalHitTotal;
                        certSummary.CategoryUniqueHits = (int)categoryPageUniqueHitTotal;
                        certSummary.AdvertiserTotalHits = (int)advertiserPageTotalHitTotal;
                        certSummary.AdvertiserUniqueHits = (int)advertiserPageUniqueHitTotal;

                        certSummaries.Add(certSummary);

                    }
                }
            }

            certSummaries.Sort(new PageHitSummaryComparer());

            CreateChartistObject(certSummaries);

        }

        private void CreateChartistObject(List<PageHitSummary> certSummaries)
        {
            List<string> labels = new List<string>();
            List<List<int>> series = new List<List<int>>();

            List<int> homeTotalHits = new List<int>();
            List<int> categoryTotalHits = new List<int>();
            List<int> advertiserTotalHits = new List<int>();

            foreach (var item in certSummaries)
            {
                labels.Add(item.Name);
                homeTotalHits.Add(item.HomeTotalHits);
                categoryTotalHits.Add(item.CategoryTotalHits);
                advertiserTotalHits.Add(item.AdvertiserTotalHits);
            }

            series.Add(homeTotalHits);
            series.Add(categoryTotalHits);
            series.Add(advertiserTotalHits);

            labelsJson = JsonConvert.SerializeObject(labels);
            seriesJson = JsonConvert.SerializeObject(series);
        }

        void cancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/admin/reports/Default.aspx");
        }
    }

}
