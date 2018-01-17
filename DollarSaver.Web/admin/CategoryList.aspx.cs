using System;
using System.Web.UI.WebControls;

using DollarSaver.Core.Data;
using DollarSaver.Core.Data.DollarSaverDBTableAdapters;

namespace DollarSaver.Web.Admin
{

    public partial class CategoryList : StationAdminPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            categoryGrid.RowCommand += new GridViewCommandEventHandler(categoryGrid_RowCommand);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            CategoryTableAdapter categoryAdapter = new CategoryTableAdapter();

            DollarSaverDB.CategoryDataTable categories = categoryAdapter.GetPrimaryCategoriesByStation(StationId);

            categoryGrid.DataSource = categories.Rows;
            categoryGrid.DataBind();
        }

        void categoryGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            CategoryTableAdapter categoryAdapter = new CategoryTableAdapter();

            int categoryId = Int32.Parse(e.CommandArgument.ToString());

            if (e.CommandName == "up")
            {

                categoryAdapter.MoveUp(categoryId);

            }
            else if (e.CommandName == "down")
            {

                categoryAdapter.MoveDown(categoryId);
            }
        }
    }
}