using DollarSaver.Core.Data.DollarSaverDBTableAdapters;
using System;
using System.Web.UI.WebControls;

namespace DollarSaver.Web.Admin
{
    public partial class CategoryCreate : StationAdminPageBase
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            saveButton.Click += new EventHandler(saveButton_Click);
            cancelButton.Click += new EventHandler(cancelButton_Click);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/admin/CategoryList.aspx");
        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            string categoryName = txtCategory.Text.Trim();

            if (string.IsNullOrEmpty(categoryName))
            {
                txtCategoryValidator.ErrorMessage = "Category cannot be blank";
                return;
            }

            CategoryTableAdapter categoryAdapter = new CategoryTableAdapter();

            int categoryMatches = (int)categoryAdapter.ValidateCategoryName(categoryName);

            if (categoryMatches != 0)
            {
                ErrorMessage = "There's already a category created with that name";
                return;
            }

            categoryAdapter.Insert(StationId, null, categoryName, -1);

            InfoMessage = "Category created";

            Response.Redirect("~/admin/CategoryList.aspx");

        }
    }
}