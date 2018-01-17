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

namespace DollarSaver.Web.Admin.Super.UserAdmin {

    public partial class UserList : SuperAdminPageBase {


        protected void Page_Load(object sender, EventArgs e) {

            AdminTableAdapter adapter = new AdminTableAdapter();
            DollarSaverDB.AdminDataTable admins = adapter.GetRootUsers();

            if (admins.Count > 0) {
                userHolder.Visible = true;
                noUserHolder.Visible = false;

                itemGrid.DataSource = admins.Rows;
                itemGrid.DataBind();
            } else {
                userHolder.Visible = false;
                noUserHolder.Visible = true;
            }


        }

    }
}
