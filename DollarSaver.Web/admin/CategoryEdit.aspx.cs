using System;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using DollarSaver.Core.Data;
using DollarSaver.Core.Data.DollarSaverDBTableAdapters;

namespace DollarSaver.Web.Admin
{

    public partial class CategoryEdit : StationAdminPageBase
    {
        private int categoryId = 0;
        private DollarSaverDB.CategoryRow category;

        private int parentCategoryId = 0;
        private DollarSaverDB.CategoryRow parentCategory;

        private bool isNew = false;
        private bool isSubCategory = false;

        protected int GetParentIdFromQueryString()
        {
            int id = 0;
            try
            {
                id = Int32.Parse(Request.QueryString["parent_id"]);
            }
            catch { }

            return id;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (ReadOnly)
            {
                Response.Redirect("~/admin/Default.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            saveButton.Click += new EventHandler(saveButton_Click);
            cancelButton.Click += new EventHandler(cancelButton_Click);
            deleteButton.Click += new EventHandler(deleteButton_Click);
            deleteButton.Attributes["onclick"] = "javascript: return confirm('Are you sure want to delete this item?');";

            subCategoryGrid.RowCommand += new GridViewCommandEventHandler(subCategoryGrid_RowCommand);

            categoryId = GetIdFromQueryString();

            CategoryTableAdapter categoryAdapter = new CategoryTableAdapter();


            if (categoryId != 0)
            {

                DollarSaverDB.CategoryDataTable categories = categoryAdapter.GetCategory(categoryId);


                if (categories.Rows.Count != 1)
                {
                    Response.Redirect("~/admin/CategoryList.aspx");
                }

                category = categories[0];

                if (category.StationId != StationId)
                {
                    Response.Redirect("~/admin/CategoryList.aspx");
                }

                if (!category.IsParentCategoryIdNull())
                {
                    parentCategoryId = category.ParentCategoryId;

                    parentCategory = categoryAdapter.GetCategory(parentCategoryId)[0];
                }
            }
            else
            {

                parentCategoryId = GetParentIdFromQueryString();

                DollarSaverDB.CategoryDataTable parentCategories = categoryAdapter.GetCategory(parentCategoryId);

                if (parentCategories.Rows.Count != 1)
                {
                    Response.Redirect("~/admin/CategoryList.aspx");
                }

                parentCategory = parentCategories[0];

                if (parentCategory.StationId != StationId)
                {
                    Response.Redirect("~/admin/CategoryList.aspx");
                }

                isNew = true;
            }

            if (isNew || !category.IsParentCategoryIdNull())
            {
                isSubCategory = true;
            }

        }


        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            idHidden.Value = categoryId.ToString();
            parentIdHidden.Value = "0";

            if (!isNew)
            {
                addSubCategoryLink.Visible = true;
                addSubCategoryLink.NavigateUrl = "~/admin/CategoryEdit.aspx?parent_id=" + category.CategoryId;
            }
            else
            {
                addSubCategoryLink.Visible = false;
            }

            if (isSubCategory)
            {
                editSubCatHolder.Visible = true;
                viewSubCatHolder.Visible = false;

                parentIdHidden.Value = parentCategoryId.ToString();

                if (category != null)
                {
                    nameBox.Text = category.Name;
                }
                nameBoxRFV.Enabled = true;
                nameLabel.Text = parentCategory.Name;
            }
            else
            {
                editSubCatHolder.Visible = false;
                viewSubCatHolder.Visible = true;

                nameLabel.Text = category.Name;
            }

            if (!isNew)
            {
                if (category.SubCategories.Count > 0)
                {
                    subCatListHolder.Visible = true;
                    noSubCatHolder.Visible = false;

                    subCategoryGrid.DataSource = category.SubCategories.Rows;
                    subCategoryGrid.DataBind();
                }
                else
                {
                    subCatListHolder.Visible = false;
                    noSubCatHolder.Visible = true;
                }
            }
            else
            {
                subCategoryGrid.Visible = false;
            }

        }

        void saveButton_Click(object sender, EventArgs e)
        {

            categoryId = Int32.Parse(idHidden.Value);

            String name = nameBox.Text.Trim();

            if (name == String.Empty)
            {
                ErrorMessage = "Name cannot be blank";
                return;
            }

            CategoryTableAdapter categoryAdapter = new CategoryTableAdapter();

            if (categoryId > 0)
            {

                category = categoryAdapter.GetCategory(categoryId)[0];


                category.Name = name;

                categoryAdapter.Update(category);

                parentCategoryId = category.ParentCategoryId;

            }
            else if (isNew == true && isSubCategory == false && Request.QueryString["parent_id"] == "NEW")
            {
                categoryAdapter.Insert(StationId, null, name, -1);
            }
            else
            {

                parentCategoryId = Int32.Parse(parentIdHidden.Value);

                // SeqNo of -1 will assign it the next highest SeqNo for the category

                categoryAdapter.Insert(StationId, parentCategoryId, name, -1);

            }

            InfoMessage = "Category Updated";
            if (parentCategoryId > 0)
            {
                Response.Redirect("~/admin/CategoryEdit.aspx?id=" + parentCategoryId);
            }
            else
            {
                Response.Redirect("~/admin/CategoryList.aspx");
            }

        }

        void cancelButton_Click(object sender, EventArgs e)
        {
            parentCategoryId = Int32.Parse(parentIdHidden.Value);

            if (parentCategoryId > 0)
            {
                Response.Redirect("~/admin/CategoryEdit.aspx?id=" + parentCategoryId);
            }
            else
            {
                Response.Redirect("~/admin/CategoryList.aspx");
            }

        }

        void deleteButton_Click(object sender, EventArgs e)
        {
            categoryId = Int32.Parse(idHidden.Value);

            CategoryTableAdapter categoryAdapter = new CategoryTableAdapter();
            try
            {
                int parentCategoryId = category.ParentCategoryId;
                categoryAdapter.Delete(categoryId);

                InfoMessage = "Category Deleted";

                if (parentCategoryId > 0)
                {
                    Response.Redirect("~/admin/CategoryEdit.aspx?id=" + parentCategoryId);
                }
                else
                {
                    Response.Redirect("~/admin/CategoryList.aspx");
                }

            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    ErrorMessage = "Category cannot be deleted because it is assigned to an Advertiser";
                }
                else
                {
                    throw ex;
                }
            }
        }

        void subCategoryGrid_RowCommand(object sender, GridViewCommandEventArgs e)
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