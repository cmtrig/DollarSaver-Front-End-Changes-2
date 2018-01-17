using System;

using DollarSaver.Core.Data;
using DollarSaver.Core.Data.DollarSaverDBTableAdapters;

namespace DollarSaver.Web.Admin
{

    public partial class AdminList : ManagerPageBase {

        protected override void OnInit(EventArgs e) {
            base.OnInit(e);

            if (ReadOnly) {
                Response.Redirect("~/admin/");
            }
        }

        protected void Page_Load(object sender, EventArgs e) {

            AdminTableAdapter adapter = new AdminTableAdapter();
            DollarSaverDB.AdminDataTable admins = adapter.GetByRole(StationId, CurrentUser.AdminRoleId);

            if (admins.Count > 0) {
                itemGrid.DataSource = admins.Rows;
                itemGrid.DataBind();
            }

            if (ReadOnly) {
                newLink.Visible = false;

                itemGrid.Columns[0].Visible = false;
                itemGrid.Columns[1].Visible = true;
            } else {
                newLink.Visible = true;

                itemGrid.Columns[0].Visible = true;
                itemGrid.Columns[1].Visible = false;
            }
        }
    }
}
