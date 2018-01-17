using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Collections.Specialized;
using System.Text.RegularExpressions;

using DollarSaver.Core.Common;
using DollarSaver.Core.Data;
using DollarSaver.Core.Data.DollarSaverDBTableAdapters;

namespace DollarSaver.Web {

/// <summary>
/// Summary description for StationPageBase
/// </summary>
    public class StationPageBase : ConsumerPageBase {

        protected const String DS_COOKIE_NAME = "DollarSaver";

        private HttpCookie _dsCookie = null;
        
        protected HttpCookie DsCookie {
            get {
                if (_dsCookie == null) {
                    _dsCookie = Request.Cookies.Get(DS_COOKIE_NAME);

                    if (_dsCookie == null) {
                        _dsCookie = new HttpCookie(DS_COOKIE_NAME);
                    }
                }
                return _dsCookie;
            }
        }

        private StationTableAdapter stationAdapter = new StationTableAdapter();
        private bool invalidSubdomain = false;

        public virtual String PageTitle {
            get {
                return Station.SiteNamePlainText; 
            }
        }

        public virtual bool StationActiveRequired {
            get { return true; }
        }


        private int _stationId = 0;

        public int StationId {
            get { return _stationId; }
            set {
                _stationId = value;
                _station = stationAdapter.GetStation(_stationId)[0];
            }
        }

        private DollarSaverDB.StationRow _station;

        public DollarSaverDB.StationRow Station {
            get {
                if (_station == null) {
                    _station = stationAdapter.GetStation(StationId)[0];
                }
                return _station;
            }
            set { 
                _station = value;
                _stationId = value.StationId;
            }
        }


        private String _onLoadScript = String.Empty;

        public String OnLoadScript {
            get { return _onLoadScript; }
            set { _onLoadScript = value; }
        }

        protected virtual bool IsSecure {
            get { return false; }
        }

        protected void CheckSecurity() {

            if (IsSecure) {
                if (!IsDev && Request.Url.Scheme.ToLower() != "https") {
                    Response.Redirect("https://" + Request.Url.Host + Request.Url.PathAndQuery);
                }
            } else {
                if (Request.Url.Scheme.ToLower() != "http") {
                    Response.Redirect("http://" + Request.Url.Host + Request.Url.PathAndQuery);
                }
            }
        }

        protected virtual int GetPageStationId() {
            return _stationId;
        }


        protected override void OnError(EventArgs e) {
            base.OnError(e);

            ErrorMessage = Server.GetLastError().Message;
        }


        protected override void OnPreInit(EventArgs e) {

            base.OnPreInit(e);

            CheckSecurity();

            int subdomainStationId = CheckSubdomain();

            int queryStringStationId = GetStationIdFromQueryString();

            if (queryStringStationId == 0) {
                // old parameter for backwards compatibility
                queryStringStationId = GetValueFromQueryString("stationId");
            }


            int stationId = 0;

            if (subdomainStationId > 0) {

                stationId = subdomainStationId;

            } else if (queryStringStationId > 0) {

                stationId = queryStringStationId;

            } else if (GetPageStationId() > 0) {

                stationId = GetPageStationId();

            } else if (DsCookie["station_id"] != null) {

                try {
                    stationId = Int32.Parse(DsCookie["station_id"]);
                } catch { }
            }

            if (stationId == 0 || invalidSubdomain) {

                String url = "http://" + EnvDomain + ResolveUrl("~/Sites.aspx");

                Response.Redirect(url);
            }

            DollarSaverDB.StationDataTable stationTable = stationAdapter.GetStation(stationId);

            if (stationTable.Count != 1) {
                ErrorMessage = "Unkown Station: " + stationId;

                String url = "http://" + EnvDomain + ResolveUrl("~/Sites.aspx");

                Response.Redirect(url);
            }

            DollarSaverDB.StationRow station = stationTable[0];

            if (StationActiveRequired && (!station.IsActive || !station.IsSiteActive)) {
                ErrorMessage = "Sorry, this station is not currently active";

                String url = "http://" + EnvDomain + ResolveUrl("~/Sites.aspx");

                Response.Redirect(url);
            }

            Station = station;


            // set cookie

            DsCookie["station_id"] = stationId.ToString();
            DsCookie.Expires = DateTime.Now.AddYears(10);
            if (IsDev) {
                DsCookie.Domain = EnvDomain;
            } else {
                DsCookie.Domain = ".dollarsavershow.com";
            }

            if (UseSubdomain && UrlSubdomain == String.Empty && !IsSecure) {
                // redirect to absolute URL
                String absoluteUrl = String.Empty;

                if (IsSecure) {
                    absoluteUrl = "https://";
                } else {
                    absoluteUrl = "http://";
                }

                absoluteUrl += Station.Subdomain + "." + EnvDomain;

                //absoluteUrl += Request.RawUrl;

                String queryString = String.Empty;

                foreach(String key in Request.QueryString.Keys) {
                    if (key.ToLower() != "station_id" && key.ToLower() != "stationid") {
                        queryString += key + "=" + Request.QueryString[key];
                    }
                }

                if (queryString.EndsWith("&")) {
                    queryString = queryString.Substring(0, queryString.Length - 1);
                }

                if (queryString != String.Empty) {
                    queryString = "?" + queryString;
                }

                absoluteUrl += Request.Url.LocalPath + queryString;


                Response.Redirect(absoluteUrl);

            }

        }

        
        protected override void OnPreRender(EventArgs e) {
            base.OnPreRender(e);

			if (PageTitle != "") {
				Title = Server.HtmlEncode(PageTitle);
			}

            HttpContext.Current.Response.Cookies.Add(DsCookie);


        }

        public String EnvDomain {
            get {

                if (IsDev) {
                    //return "localhost" + (Request.Url.Port != 80 ? ":" + Request.Url.Port : String.Empty);
                    string port = (Request.Url.Port != 80 ? ":" + Request.Url.Port : String.Empty);
                    return $"{Request.Url.Host}{port}";
                } else {
                    return "dollarsavershow.com";
                }
            }
        }


        protected void RedirectToHomePage() {
            DollarSaverRedirect("~/Default.aspx");
        }

        private bool? _useSubdomain = null;

        public bool UseSubdomain {
            get {
                if (_useSubdomain == null) {
                    if (Station != null && !Station.IsSubdomainNull() && Station.Subdomain != String.Empty) {
                        _useSubdomain = true;
                    } else {
                        _useSubdomain = false;
                    }
                }
                return (bool) _useSubdomain;
            }
        }

        private String _urlSubdomain = null;

        private String UrlSubdomain {
            get {

                if (_urlSubdomain == null) {
                    string[] url = Request.Url.Host.Split('.');
                    if (url.Length == 3 && url[0] != String.Empty && url[0].ToLower() != "www") {
                        _urlSubdomain = url[0].ToLower();
                    } else {
                        _urlSubdomain = String.Empty;
                    }
                }

                return _urlSubdomain;
            }
        }

        private int CheckSubdomain() {

            int subdomainStationId = 0;

            if (UrlSubdomain != String.Empty) {
                DollarSaverDB.StationDataTable stationLookup = stationAdapter.GetBySubdomain(UrlSubdomain);

                if (stationLookup.Count == 1) {
                    subdomainStationId = stationLookup[0].StationId;
                } else {
                    invalidSubdomain = true;
                }
            }

            return subdomainStationId;
        }

        public String GetUrl(String url) {

            
            String dollarSaverUrl = url;

            if (!UseSubdomain) {
                if (url.Contains("?")) {
                    dollarSaverUrl += "&station_id=" + StationId;
                } else {
                    dollarSaverUrl += "?station_id=" + StationId;
                }
            } else if (IsSecure) {

                /*
                String absoluteUrl = "http://" + Station.Subdomain + "." + EnvDomain + DollarSaverAppPath;

                if (url.StartsWith("~")) {
                    absoluteUrl += url.Substring(1, url.Length - 1);
                } else if (url.StartsWith("/")) {
                    absoluteUrl += url;
                } else {
                    //absoluteUrl += Request.Url.
                    absoluteUrl += Request.FilePath.Substring(0, Request.FilePath.LastIndexOf("/")) + url;
                }
                */

                String absoluteUrl = "http://" + Station.Subdomain + "." + EnvDomain + ResolveUrl(url);

                dollarSaverUrl = absoluteUrl;

            }

            return dollarSaverUrl;

            /*
            if (UseSubdomain) {
                //Request.Url.Host
                if (UrlSubdomain == String.Empty) {
                    // redirect to absolute URL
                    String absoluteUrl = String.Empty;

                    if(IsSecure) {
                        absoluteUrl = "https://";
                    } else {
                        absoluteUrl = "http://";
                    }

                    if(IsDev) {
                        absoluteUrl += Station.Subdomain + ".localhost" + (Request.Url.Port != 80 ? ":" + Request.Url.Port : String.Empty);
                    } else {
                        absoluteUrl += Station.Subdomain + ".dollarsavershow.com";
                    }


                    if (url.StartsWith("~")) {
                        absoluteUrl += url.Substring(1, url.Length - 1);

                        absoluteUrl += DollarSaverAppPath;
                    } else if (url.StartsWith("/")) {
                        absoluteUrl += url;
                    } else {
                        //absoluteUrl += Request.Url.
                        absoluteUrl += Request.FilePath.Substring(0, Request.FilePath.LastIndexOf("/")) + url;
                    }

                    dollarSaverUrl = absoluteUrl;
                }

            } else {
                dollarSaverUrl = url;
                if (url.Contains("?")) {
                    dollarSaverUrl += "&station_id=" + StationId;
                } else {
                    dollarSaverUrl += "?station_id=" + StationId;
                }

                if (UrlSubdomain != String.Empty) {
                    // redirect to absolute URL
                    // redirect to absolute URL
                    String absoluteUrl = String.Empty;

                    if (IsSecure) {
                        absoluteUrl = "https://";
                    } else {
                        absoluteUrl = "http://";
                    }

                    if (IsDev) {
                        absoluteUrl += "localhost" + (Request.Url.Port != 80 ? ":" + Request.Url.Port : String.Empty);
                    } else {
                        absoluteUrl += "dollarsavershow.com";
                    }

                    if (dollarSaverUrl.StartsWith("~")) {
                        absoluteUrl += dollarSaverUrl.Substring(1, url.Length - 1);
                        absoluteUrl += DollarSaverAppPath;
                    } else if (dollarSaverUrl.StartsWith("/")) {
                        absoluteUrl += dollarSaverUrl;
                    } else {
                        //absoluteUrl += Request.Url.
                        absoluteUrl += Request.FilePath.Substring(0, Request.FilePath.LastIndexOf("/")) + dollarSaverUrl;
                    }

                    dollarSaverUrl = absoluteUrl;

                }

            }

            return dollarSaverUrl;
            */
        }

        protected void DollarSaverRedirect(String url) {
            String dollarSaverUrl = GetUrl(url);
            Response.Redirect(dollarSaverUrl);
        }

        protected void LogHit(DateTime hitDate, int certificateId, PageHitType pageHitType) {

            PageHitLogTableAdapter pageHitLogAdapter = new PageHitLogTableAdapter();

            bool isUnique = false;

            switch (pageHitType) {
                case PageHitType.HomePage :

                    if (Session["visited_homepage_" + certificateId] == null) {
                        isUnique = true;
                        Session["visited_homepage_" + certificateId] = 1;
                    }

                    break;
                case PageHitType.CategoryPage :

                    if (Session["visited_category_" + certificateId] == null) {
                        isUnique = true;
                        Session["visited_category_" + certificateId] = 1;
                    }

                    break;

                case PageHitType.AdvertiserPage :

                    if (Session["visited_advertiser_" + certificateId] == null) {
                        isUnique = true;
                        Session["visited_advertiser_" + certificateId] = 1;
                    }

                    break;
            }


            pageHitLogAdapter.Ping(hitDate, certificateId, (int)pageHitType, isUnique);

        }

    }

}
