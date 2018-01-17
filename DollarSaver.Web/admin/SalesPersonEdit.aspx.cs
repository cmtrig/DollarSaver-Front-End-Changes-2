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

namespace DollarSaver.Web.Admin {

    public partial class SalesPersonEdit : ManagerPageBase {

        protected override void OnInit(EventArgs e) {
            base.OnInit(e);

            if (ReadOnly) {
                Response.Redirect("~/admin/Default.aspx");
            }
        }


        protected void Page_Load(object sender, EventArgs e) {
            saveButton.Click += new EventHandler(saveButton_Click);
            cancelButton.Click += new EventHandler(cancelButton_Click);
            deleteButton.Click += new EventHandler(deleteButton_Click);
            deleteButton.Attributes["onclick"] = "javascript: return confirm('Are you sure want to delete this item?');";

            int salesPersonId = GetIdFromQueryString();

            if (!Page.IsPostBack) {

                if (salesPersonId > 0) {
                    createEditLabel.Text = "Edit";

                    SalesPersonTableAdapter salesPersonAdapter = new SalesPersonTableAdapter();

                    DollarSaverDB.SalesPersonDataTable salesPeople = salesPersonAdapter.GetSalesPerson(salesPersonId);

                    if (salesPeople.Count != 1) {
                        Response.Redirect("~/admin/SalesPersonList.aspx");
                    }

                    DollarSaverDB.SalesPersonRow salesPerson = salesPeople[0];

                    if (salesPerson.StationId != StationId) {
                        Response.Redirect("~/admin/SalesPersonList.aspx");
                    }


                    idHidden.Value = salesPersonId.ToString();
                    firstNameBox.Text = salesPerson.FirstName;
                    lastNameBox.Text = salesPerson.LastName;
                    isActiveBox.Checked = salesPerson.IsActive;

                    if (!salesPerson.IsEmailAddressNull()) {
                        emailBox.Text = salesPerson.EmailAddress;
                    }

                    if (!salesPerson.IsMobilePhoneNull()) {
                        mobilePhoneBox.Text = salesPerson.MobilePhone;
                    }

                    if (!salesPerson.IsWorkPhoneNull()) {
                        workPhoneBox.Text = salesPerson.WorkPhone;
                    }

                } else {
                    deleteButton.Visible = false;
                    saveButton.Text = "Create";
                    createEditLabel.Text = "Create";
                }

            }

        }

        void saveButton_Click(object sender, EventArgs e) {

            if (Page.IsValid) {

                //SalesPersonController sc = new SalesPersonController();

                String firstName = firstNameBox.Text.Trim();
                if (firstName == String.Empty) {
                    ErrorMessage = "First Name is required";
                    return;
                }


                String lastName = lastNameBox.Text.Trim();
                if (lastName == String.Empty) {
                    ErrorMessage = "Last Name is required";
                    return;
                }
                
                
                bool isActive = isActiveBox.Checked;

                String emailAddress = emailBox.Text.Trim();

                emailAddress = Globals.ConvertToNull(emailAddress);
                if (emailAddress != null && !Regex.IsMatch(emailAddress, @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$")) {
                    ErrorMessage = "E-mail address is not valid";
                    return;
                }

                String mobilePhone = mobilePhoneBox.Text.Trim();
                String workPhone = workPhoneBox.Text.Trim();

                mobilePhone = Globals.ConvertToNull(mobilePhone);
                workPhone = Globals.ConvertToNull(workPhone);


                SalesPersonTableAdapter salesPersonAdapter = new SalesPersonTableAdapter();

                if (idHidden.Value != String.Empty) {

                    DollarSaverDB.SalesPersonRow salesPerson = salesPersonAdapter.GetSalesPerson(Int32.Parse(idHidden.Value))[0];

                    salesPerson.FirstName = firstName;
                    salesPerson.LastName = lastName;
                    salesPerson.IsActive = isActive;

                    if (emailAddress == null) {
                        salesPerson.SetEmailAddressNull();
                    } else {
                        salesPerson.EmailAddress = emailAddress;
                    }

                    if (mobilePhone == null) {
                        salesPerson.SetMobilePhoneNull();
                    } else {
                        salesPerson.MobilePhone = mobilePhone;
                    }

                    if (mobilePhone == null) {
                        salesPerson.SetMobilePhoneNull();
                    } else {
                        salesPerson.MobilePhone = mobilePhone;
                    }

                    if (workPhone == null) {
                        salesPerson.SetWorkPhoneNull();
                    } else {
                        salesPerson.WorkPhone = workPhone;
                    }


                    salesPersonAdapter.Update(salesPerson);

                } else {
                    //sc.Insert(StationId, firstName, lastName, isActive);

                    salesPersonAdapter.Insert(StationId, firstName, lastName, mobilePhone, workPhone, emailAddress, isActive);
                }

                Response.Redirect("~/admin/SalesPersonList.aspx");
            }

        }

        void cancelButton_Click(object sender, EventArgs e) {
            Response.Redirect("~/admin/SalesPersonList.aspx");
        }

        void deleteButton_Click(object sender, EventArgs e) {

            SalesPersonTableAdapter salesPersonAdapter = new SalesPersonTableAdapter();

            if (idHidden.Value != String.Empty) {

                //SalesPersonController sc = new SalesPersonController();
                //SalesPerson salesPerson = sc.GetSalesPerson(Int32.Parse(idHidden.Value));

                try {
                    salesPersonAdapter.Delete(Int32.Parse(idHidden.Value));
                } catch (SqlException ex) {
                    if (ex.Number == 547) {
                        ErrorMessage = "Sales Person cannot be deleted because they are assigned to an advertiser";
                    } else {
                        throw ex;
                    }
                }
            }

            Response.Redirect("~/admin/SalesPersonList.aspx");



        }


    }
}