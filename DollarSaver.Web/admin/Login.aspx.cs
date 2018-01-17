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

namespace DollarSaver.Web.Admin {

    public partial class Login : SecurePageBase {

        private String errorMessage = String.Empty;

        protected void Page_Load(object sender, EventArgs e) {

            loginButton.Click += new EventHandler(loginButton_Click);

            if (Request.QueryString["logout"] == "Y") {
                FormsAuthentication.SignOut();
            }

            if (!Page.IsPostBack) {
                if (Request.RawUrl.StartsWith("/dollarsaver/admin/loginForm") ||
                    Request.RawUrl.StartsWith("/dollarsaver/admin/logoutForm")) {
                    Response.Redirect("~/admin/Login.aspx");
                }

                /*
                StationTableAdapter stationAdapter = new StationTableAdapter();

                DollarSaverDB.StationDataTable stations = stationAdapter.GetActive();

                stationList.DataSource = stations;
                stationList.DataTextField = "Name";
                stationList.DataValueField = "StationId";
                stationList.DataBind();

                stationList.Items.Insert(0, new ListItem("", "0"));
                */

                HttpCookie cookie = Request.Cookies.Get(ADMIN_COOKIE_NAME);

                if (cookie != null) {

                    /*
                    String stationIdStr = cookie["station_id"];

                    if (stationIdStr != null && stationIdStr != String.Empty) {
                        ListItem item = stationList.Items.FindByValue(stationIdStr);

                        if (item != null) {
                            stationList.SelectedValue = stationIdStr;
                        }
                    }
                     */

                    String stationCode = cookie["station_code"];

                    if (stationCode != null) {
                        stationCodeBox.Text = stationCode;
                    }
                }
                else {
                    HttpCookie newCookie = new HttpCookie(ADMIN_COOKIE_NAME);
                    newCookie.Expires = DateTime.Now.AddDays(1d);
                    Response.Cookies.Add(newCookie);
                }

            }

        }

        protected override void OnPreRender(EventArgs e) {
            base.OnPreRender(e);

            errorMessageHolder.Visible = false;
            if (errorMessage != String.Empty) {
                errorMessageHolder.Visible = true;
                errorMessageLabel.Text = errorMessage;
                errorMessage = String.Empty;
            }
        
        }

        void loginButton_Click(object sender, EventArgs e) {

            String username = usernameBox.Text.Trim();
            String password = passwordBox.Text;

            //int stationId = Int32.Parse(stationList.SelectedValue);

            
            StationTableAdapter stationAdapter = new StationTableAdapter();

            String stationCode = stationCodeBox.Text.Trim().ToUpper();

            int stationId = 0;
            if (stationCode != String.Empty) {

                DollarSaverDB.StationDataTable stationLookup = stationAdapter.GetByCode(stationCode);

                if (stationLookup.Count != 1 || !stationLookup[0].IsActive) {
                    errorMessage = "Incorrect username, password or station";
                    return;
                }

                stationId = stationLookup[0].StationId;
            }
            

            AdminTableAdapter adminAdapter = new AdminTableAdapter();

            if ((int)adminAdapter.Authenticate(stationId, username, password) == 1) {

                DollarSaverDB.AdminRow user = adminAdapter.GetByUsername(stationId, username)[0];

                user.LastAccessDate = DateTime.Now;
                adminAdapter.Update(user);

                int userStationId;
                if (user.Role == AdminRole.Root) {
                    userStationId = 0;
                } else {
                    userStationId = stationId;
                }
                Session["admin_station_id"] = userStationId;

                HttpCookie cookie = Request.Cookies.Get(ADMIN_COOKIE_NAME);

                if (cookie == null) {
                    cookie = new HttpCookie(ADMIN_COOKIE_NAME);
                }

                cookie.Expires = DateTime.Now.AddYears(10);
                if (IsDev) {
                    cookie.Domain = EnvDomain;
                } else {
                    cookie.Domain = ".dollarsavershow.com";
                }

                cookie["station_id"] = userStationId.ToString();
                cookie["station_code"] = stationCode;

                HttpContext.Current.Response.Cookies.Add(cookie);

                FormsAuthentication.SetAuthCookie(user.AdminId.ToString(), true);

                if (user.Role == AdminRole.Root) {
                    if (stationId == 0) {
                        Response.Redirect("~/admin/super/", false);
                    } else {
                        Response.Redirect("~/admin/Default.aspx?station_id=" + stationId, false);
                    }
                } else {
                    Response.Redirect("~/admin/", false);
                }

            } else {
                errorMessage = "Incorrect username, password or station";
            }

        }
    }
}