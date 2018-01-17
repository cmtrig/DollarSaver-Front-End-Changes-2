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
/// Summary description for AdminPageBase
/// </summary>
    public class AdminPageBase : LoggedInPageBase {

        public String ImageDirUrl = String.Empty;
        public String ImageDir = String.Empty;

        private bool _readOnly = false;

        protected bool ReadOnly {
            get { return _readOnly; }
            set { _readOnly = value; }
        }


        private DollarSaverDB.StationRow _station = null;

        public int StationId {
            get {
                if (Session["admin_station_id"] == null) {
                    return 0;
                } else {
                    return (int)Session["admin_station_id"];
                }
            }
            set { Session["admin_station_id"] = value; }
        }

        public DollarSaverDB.StationRow Station {
            get {
                if (_station == null) {
                    if (StationId > 0) {
                        StationTableAdapter stationAdapter = new StationTableAdapter();
                        _station = stationAdapter.GetStation(StationId)[0];
                    }
                }

                return _station;
            }
        }



        protected override void OnPreInit(EventArgs e) {
            base.OnPreInit(e);


            if (CurrentUser.Role == AdminRole.Root) {
                int rootStationId = GetValueFromQueryString("station_id");

                if (rootStationId > 0 && rootStationId != StationId) {
                    StationId = rootStationId;
                }

                if (StationId == 0) {
                    Response.Redirect("~/admin/super/Default.aspx");
                }

            } else if (StationId == 0) {
                HttpCookie cookie = Request.Cookies.Get(ADMIN_COOKIE_NAME);

                if (cookie != null) {
                    try {
                        StationId = Int32.Parse(cookie["station_id"]);
                    } catch {}
                }
            }

            ImageDirUrl = "/station/" + StationId + "/images/";
            ImageDir = Request.PhysicalApplicationPath + ImageDirUrl;

            if (StationId == 0) {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
                Response.End();
            }

        }

        protected override void OnInit(EventArgs e) {
            base.OnInit(e);


            if (CurrentUser == null || 
                (CurrentUser.Role != AdminRole.Root && CurrentUser.StationId != StationId)) {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);        
        }

    }
}

