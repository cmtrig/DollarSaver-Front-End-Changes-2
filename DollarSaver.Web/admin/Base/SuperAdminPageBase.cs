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
    public class SuperAdminPageBase : LoggedInPageBase {


        protected override void OnInit(EventArgs e) {
            base.OnInit(e);


            if (CurrentUser == null || CurrentUser.Role != AdminRole.Root) {
                Response.Write("You are not allowed to access this page");
                Response.End();
            }

        }

        protected void RedirectToSuperAdminHome() {
            Response.Redirect("~/admin/super/Default.aspx");
        }



    }
}

