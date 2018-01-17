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

using System.Data.SqlClient;
using System.Text.RegularExpressions;

using DollarSaver.Core.Common;
using DollarSaver.Core.Data;
using DollarSaver.Core.Data.DollarSaverDBTableAdapters;

namespace DollarSaver.Web.Admin.Super.UserAdmin {

    public partial class UserEdit : SuperAdminPageBase {

        private int adminId;

        protected void Page_Load(object sender, EventArgs e) {
            saveButton.Click += new EventHandler(saveButton_Click);
            cancelButton.Click += new EventHandler(cancelButton_Click);
            deleteButton.Click += new EventHandler(deleteButton_Click);
            deleteButton.Attributes["onclick"] = "javascript: return confirm('Are you sure want to delete this item?');";

            adminId = GetIdFromQueryString();

            if (!Page.IsPostBack) {

                if (adminId > 0) {
                    createEditLabel.Text = "Edit";
                    updateHolder.Visible = true;

                    AdminTableAdapter adminAdapter = new AdminTableAdapter();

                    DollarSaverDB.AdminDataTable admins = adminAdapter.GetAdmin(adminId);

                    if (admins.Rows.Count == 1) {
                        DollarSaverDB.AdminRow admin = admins[0];

                        if (admin.Role != AdminRole.Root || !admin.IsStationIdNull()) {
                            RedirectToUserList();
                        }

                        usernameBox.Text = admin.Username;
                        if (!admin.IsEmailAddressNull()) {
                            emailBox.Text = admin.EmailAddress;
                        }
                        isActiveBox.Checked = admin.IsActive;

                        if (adminId == CurrentUser.AdminId) {
                            deleteButton.Visible = false;
                        }

                    } else {
                        RedirectToUserList();
                    }
                } else {
                    updateHolder.Visible = false;
                    deleteButton.Visible = false;
                    saveButton.Text = "Create";
                    createEditLabel.Text = "Create";
                }

            }

        }

        void saveButton_Click(object sender, EventArgs e) {

            if (Page.IsValid) {

                String username = usernameBox.Text.Trim().ToLower();
                String emailAddress = emailBox.Text.Trim();
                String password = passwordBox.Text;
                String confirmPassword = confirmPasswordBox.Text;
                bool isActive = isActiveBox.Checked;


                if (username == String.Empty) {
                    ErrorMessage = "Username is required";
                    return;
                }

                if (emailAddress != String.Empty && !Regex.IsMatch(emailAddress, @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$")) {
                    ErrorMessage = "E-mail address is not valid";
                    return;
                }

                if (emailAddress == String.Empty) {
                    emailAddress = null;
                }

                if (password != confirmPassword) {
                    ErrorMessage = "Password and Confirmation must be the same.";
                    return;
                }

                if (password != String.Empty && password.Length < 6) {
                    ErrorMessage = "Password must be at least 6 characters long";
                    return;
                }

                if (!Regex.IsMatch(username, @"^\w+$")) {
                    ErrorMessage = "Username can only contain numbers, letters or underscores";
                    return;
                }


                AdminTableAdapter adminAdapter = new AdminTableAdapter();
                DollarSaverDB.AdminDataTable checkAdmins = adminAdapter.GetByUsername(0, username);

                if (checkAdmins.Count == 1 && checkAdmins[0].AdminId != adminId) {
                    ErrorMessage = "Username is already in use";
                    return;
                }

                if (adminId > 0) {

                    DollarSaverDB.AdminRow admin = adminAdapter.GetAdmin(adminId)[0];

                    admin.Username = username;
                    admin.IsActive = isActive;

                    if (emailAddress != null) {
                        admin.EmailAddress = emailAddress;
                    } else {
                        admin.SetEmailAddressNull();
                    }

                    if (password != String.Empty) {
                        admin.Password = password;
                    }

                    adminAdapter.Update(admin);

                    InfoMessage = "Root User updated";

                } else {

                    if (password == String.Empty) {
                        ErrorMessage = "Password is required";
                        return;
                    }
                    adminAdapter.Insert(null, (int)AdminRole.Root, username, password, emailAddress, DateTime.Now, null, isActive, false);

                    InfoMessage = "Root User created";
                }

                RedirectToUserList();
            }

        }

        void cancelButton_Click(object sender, EventArgs e) {
            RedirectToUserList();
        }

        void deleteButton_Click(object sender, EventArgs e) {


            if (adminId > 0) {

                AdminTableAdapter adminAdapter = new AdminTableAdapter();
                DollarSaverDB.AdminRow admin = adminAdapter.GetAdmin(adminId)[0];

                if (admin.Role == AdminRole.Root && admin.IsStationIdNull()) {

                    try {
                        adminAdapter.Delete(admin.AdminId);
                        InfoMessage = "Root User deleted";
                    } catch (SqlException ex) {
                        if (ex.Number == 547) {
                            ErrorMessage = "Root User cannot be deleted due to database constraints.";
                        } else {
                            throw ex;
                        }
                    }

                }

            }

            RedirectToUserList();

        }

        private void RedirectToUserList() {
            Response.Redirect("~/admin/super/user/UserList.aspx");
        }
    }
}
