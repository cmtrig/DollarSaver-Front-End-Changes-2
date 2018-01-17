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
    /// Summary description for LoggedInPageBase
/// </summary>
    public class LoggedInPageBase : SecurePageBase {

        public String InfoMessage {
            get { return (String)Session["Admin_InfoMessage"] + String.Empty; }
            set { Session["Admin_InfoMessage"] = value; }
        }

        public String ErrorMessage {
            get { return (String)Session["Admin_ErrorMessage"] + String.Empty; }
            set { Session["Admin_ErrorMessage"] = value; }
        }


        private DollarSaverDB.AdminRow _currentUser;

        public DollarSaverDB.AdminRow CurrentUser {
            get {
                return _currentUser;
            }
        }


        protected override void OnPreInit(EventArgs e) {
            base.OnPreInit(e);

            Page.Response.Buffer = true;

            int adminId = Convert.ToInt32(Context.User.Identity.Name);

            AdminTableAdapter adminAdapter = new AdminTableAdapter();

            DollarSaverDB.AdminDataTable adminTable = adminAdapter.GetAdmin(adminId);

            if (adminTable.Count != 1) {
                FormsAuthentication.RedirectToLoginPage();
            }


            _currentUser = adminTable[0];
        }


        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);

            if (ScriptManager.GetCurrent(this.Page) == null || !ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack) {

                HttpCookie authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];

                if (null == authCookie) {
                    FormsAuthentication.RedirectToLoginPage();
                }

                FormsAuthenticationTicket authTicket = null;
                try {
                    //assign the ticket to the authentication ticket
                    authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                } catch {
                    FormsAuthentication.RedirectToLoginPage();
                }

                //if ticket is expired or not found terminate the execution and return to the login page to authenticate
                if (authTicket == null || authTicket.Expired) {
                    FormsAuthentication.RedirectToLoginPage();
                }

                //to store the renewed ticket
                FormsAuthenticationTicket newAuthTicket = FormsAuthentication.RenewTicketIfOld(authTicket);

                if (newAuthTicket.Expiration > authTicket.Expiration) {
                    //create new principal and set the user context
                    Context.User = new System.Security.Principal.GenericPrincipal(new FormsIdentity(newAuthTicket), null);

                    string hash = FormsAuthentication.Encrypt(newAuthTicket);
                    if (newAuthTicket.IsPersistent) {
                        authCookie.Expires = newAuthTicket.Expiration;
                    }

                    authCookie.Value = hash;
                    //authCookie.HttpOnly = true;
                    authCookie.Secure = FormsAuthentication.RequireSSL;
                    if (FormsAuthentication.CookieDomain != null) {
                        authCookie.Domain = FormsAuthentication.CookieDomain;
                    }

                    //store the cookie back into the client machine with latest expiration time
                    Response.Cookies.Add(authCookie);
                }
            }
        
        }


        protected int GetIdFromQueryString() {
            return GetValueFromQueryString("id");
        }

        protected int GetValueFromQueryString(String key) {

            String idString = Request.QueryString[key];

            int id = 0;

            try {
                id = Int32.Parse(idString);
            } catch { }

            return id;

        }



        protected override void OnError(EventArgs e) {
            base.OnError(e);

            Session["ErrorMessage"] = Server.GetLastError().Message;
        }

    }
}

