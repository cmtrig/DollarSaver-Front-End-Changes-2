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

    public partial class SalesPersonList : ManagerPageBase {


        protected void Page_Load(object sender, EventArgs e) {

            salesPeopleGrid.RowDataBound += new GridViewRowEventHandler(salesPeopleGrid_RowDataBound);

            SalesPersonTableAdapter salesPersonAdapter = new SalesPersonTableAdapter();
            DollarSaverDB.SalesPersonDataTable salesPeople = salesPersonAdapter.GetByStation(StationId);


            if (salesPeople.Count > 0) {
                salesPeopleHolder.Visible = true;
                noDataFoundHolder.Visible = false;

                salesPeopleGrid.DataSource = salesPeople.Rows;
                salesPeopleGrid.DataBind();

                if (ReadOnly) {
                    newLink.Visible = false;

                    salesPeopleGrid.Columns[0].Visible = false;
                    salesPeopleGrid.Columns[1].Visible = true;
                } else {
                    newLink.Visible = true;

                    salesPeopleGrid.Columns[0].Visible = true;
                    salesPeopleGrid.Columns[1].Visible = false;
                }
            } else {
                salesPeopleHolder.Visible = false;
                noDataFoundHolder.Visible = true;
            }


        }

        void salesPeopleGrid_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {
                DollarSaverDB.SalesPersonRow salesPerson = (DollarSaverDB.SalesPersonRow)e.Row.DataItem;
                Label emailLabel = (Label)e.Row.FindControl("emailLabel");

                if (!salesPerson.IsEmailAddressNull()) {
                    emailLabel.Text = salesPerson.EmailAddress;
                }
            }
        }
    }
}