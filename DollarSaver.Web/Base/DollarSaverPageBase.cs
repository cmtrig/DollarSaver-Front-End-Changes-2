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

namespace DollarSaver.Web {

    /// <summary>
    /// Summary description for DollarSaverPageBase
    /// </summary>
    public class DollarSaverPageBase : System.Web.UI.Page {


        protected bool IsDev {
            get {
                string environment = Convert.ToString(ConfigurationManager.AppSettings["environment"]);

                return environment == "dev" ? true : false;

            }
        }

        protected bool HitCounterEnabled {
            get {
                string hitCounterEnabled = Convert.ToString(ConfigurationManager.AppSettings["hit_counter_enabled"]);

                return hitCounterEnabled == "true" ? true : false;

            }
        }

        public String DollarSaverAppPath {
            get {

                String url = Request.ApplicationPath;

                if (url.EndsWith("/")) {
                    url = url.Substring(0, url.Length - 1);
                }

                return url;
            }
        }
 
    }
}