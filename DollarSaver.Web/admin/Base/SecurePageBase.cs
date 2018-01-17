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

using DollarSaver.Core.Data;
using DollarSaver.Core.Data.DollarSaverDBTableAdapters;

namespace DollarSaver.Web.Admin {

/// <summary>
    /// Summary description for SecurePageBase
/// </summary>
    public class SecurePageBase : DollarSaverPageBase {

        protected const String ADMIN_COOKIE_NAME = "DollarSaver_admin"; 

        // TODO : Fix this redirect loop!
        private void CheckSecurity() {
            //if (UrlSubdomain != String.Empty) {
            //    Response.Redirect("http://" + EnvDomain + Request.Url.PathAndQuery);
            //}

            //if (!IsDev && Request.Url.Scheme.ToLower() != "https") {
            //    Response.Redirect("https://" + Request.Url.Host + Request.Url.PathAndQuery);
            //}

        }

        protected override void OnPreInit(EventArgs e) {

            base.OnPreInit(e);

            CheckSecurity();

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

    }
}

