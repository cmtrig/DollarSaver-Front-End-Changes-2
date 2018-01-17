using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using System.Linq;

using System.Data.SqlClient;

using DollarSaver.Core.Common;
using DollarSaver.Core.Data;
using DollarSaver.Core.Data.DollarSaverDBTableAdapters;

namespace DollarSaver.Web.Admin.Reports {

    public partial class RunReport : ManagerPageBase {

        private int id = 0;
        private DollarSaverDB.ReportRow reportToRun;

        protected override void OnInit(EventArgs e) {
            base.OnInit(e);

            if (ReadOnly) {
                Response.Redirect("~/admin/Default.aspx");
            }
        }



        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);

            //this.Header.Controls.Add(new LiteralControl("<script language=\"javascript\" type=\"text/javascript\" src=\"/admin/js/calendar.js\"></script>"));

            runButton.Click += new EventHandler(runButton_Click);

            cancelButton.Click += new EventHandler(cancelButton_Click);
            cancelButton.CausesValidation = false;

            reportListLink.NavigateUrl = "~/admin/reports/";

            parameterRepeater.ItemDataBound += new RepeaterItemEventHandler(parameterRepeater_ItemDataBound);
            runParamRepeater.ItemDataBound += new RepeaterItemEventHandler(runParamRepeater_ItemDataBound);

        }


        protected override void OnPreRender(EventArgs e) {
            base.OnPreRender(e);



            if (!Page.IsPostBack) {

                parameterHolder.Visible = true;
                toggleHolder.Visible = true;
                resultHolder.Visible = false;
                linkHolder.Visible = false;
                runDateHolder.Visible = false;

                id = GetIdFromQueryString();

                ReportTableAdapter reportAdapter = new ReportTableAdapter();
                DollarSaverDB.ReportDataTable reportTable = reportAdapter.GetReport(id);

                if (reportTable.Count == 1) {

                    reportToRun = reportTable[0];

					var roleCheck = from DollarSaverDB.AdminRoleRow role in reportToRun.TypeOfReport.Roles
									where role.AdminRoleId == (int) AdminRole.Admin
									select role;

					if (!roleCheck.Any()) {
						Response.Redirect("default.aspx");
					}


                    idHidden.Value = reportToRun.ReportId.ToString();
                    reportHeaderLabel.Text = reportToRun.Name;

                    parameterRepeater.DataSource = reportToRun.Parameters.Rows;
                    parameterRepeater.DataBind();
                } else {
                    Response.Redirect("default.aspx");
                }
            }
        }


        private void parameterRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e) {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) {
                DollarSaverDB.ParameterRow parameter = (DollarSaverDB.ParameterRow)e.Item.DataItem;

                HiddenField paramIdHidden = (HiddenField)e.Item.FindControl("paramIdHidden");
                paramIdHidden.Value = parameter.ParameterId.ToString();

                Label paramNameLabel = (Label)e.Item.FindControl("paramNameLabel");
                paramNameLabel.Text = parameter.Name;

                Label paramTypeLabel = (Label)e.Item.FindControl("paramTypeLabel");
                //paramTypeLabel.Text = parameter.ParameterType.Name;



                PlaceHolder validatorHolder = (PlaceHolder)e.Item.FindControl("validatorHolder");

                RegularExpressionValidator rev;
                RequiredFieldValidator rfv;



                paramNameLabel.Text = parameter.Name;
                HiddenField paramValueHidden = (HiddenField)e.Item.FindControl("paramValueHidden");
                Label paramValueLabel = (Label)e.Item.FindControl("paramValueLabel");
                TextBox paramValueBox = (TextBox)e.Item.FindControl("paramValueBox");
                CheckBox paramCheckBox = (CheckBox)e.Item.FindControl("paramCheckBox");
                ListBox paramList = (ListBox)e.Item.FindControl("paramList");
                PlaceHolder timeHolder = (PlaceHolder)e.Item.FindControl("timeHolder");
                DropDownList hourList = (DropDownList)e.Item.FindControl("hourList");
                DropDownList minuteList = (DropDownList)e.Item.FindControl("minuteList");


                switch (parameter.ParameterTypeId) {

                    case 1:
                        // bit
                        paramValueLabel.Visible = false;
                        paramValueBox.Visible = false;
                        paramCheckBox.Visible = true;
                        paramList.Visible = false;
                        timeHolder.Visible = false;

                        paramCheckBox.Checked = true;

                        if (parameter.Description != string.Empty) {
                            paramTypeLabel.Text = " " + parameter.Description;
                        }

                        break;

                    case 2:
                        // date
                        paramValueLabel.Visible = false;
                        paramValueBox.Visible = true;
                        paramCheckBox.Visible = false;
                        paramList.Visible = false;
                        timeHolder.Visible = false;

                        paramValueBox.Columns = 10;
                        paramValueBox.MaxLength = 12;



                        // default value hacking..
                        if (parameter.Name.ToUpper().Trim() == "START DATE") {
                            DateTime startDate = DateTime.Now;
                            startDate = startDate.AddMonths(-1);
                            startDate = new DateTime(startDate.Year, startDate.Month, 1);

							paramValueBox.Text = startDate.ToString("MM/dd/yyyy");
                        } else if (parameter.Name.ToUpper().Trim() == "END DATE") {
                            DateTime startDate = DateTime.Now;
                            startDate = startDate.AddMonths(-1);
                            startDate = new DateTime(startDate.Year, startDate.Month, 1);
                            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

                            paramValueBox.Text = endDate.ToString("MM/dd/yyyy");
						}


                        Image calendarImage = (Image)e.Item.FindControl("calendarImage");
                        calendarImage.Visible = true;
                        calendarImage.ImageUrl = "~/images/calendar_button.gif";
                        calendarImage.Attributes["OnClick"] = "showCalendarControl('" + paramValueBox.ClientID + "');";
                        //paramValueBox.Attributes["onblur"] = "hideCalendarControl();";

                        rev = GenerateDateREV(paramValueBox);
                        validatorHolder.Controls.Add(rev);

                        rfv = GenerateRFV(paramValueBox);
                        validatorHolder.Controls.Add(rfv);

                        paramTypeLabel.Text = " (MM/DD/YYYY)";


                        break;
                    case 3:
                        // decimal

                        paramValueLabel.Visible = false;
                        paramValueBox.Visible = true;
                        paramCheckBox.Visible = false;
                        paramList.Visible = false;
                        timeHolder.Visible = false;

                        paramValueBox.Columns = 12;
                        paramValueBox.MaxLength = 20;

                        rev = GenerateDecimalREV(paramValueBox);
                        validatorHolder.Controls.Add(rev);

                        rfv = GenerateRFV(paramValueBox);
                        validatorHolder.Controls.Add(rfv);


                        if (parameter.Description != string.Empty) {
                            paramTypeLabel.Text = " " + parameter.Description;
                        }

                        break;
                    case 4:
                        // int

                        paramValueLabel.Visible = false;
                        paramValueBox.Visible = true;
                        paramCheckBox.Visible = false;
                        paramList.Visible = false;
                        timeHolder.Visible = false;

                        paramValueBox.Columns = 12;
                        paramValueBox.MaxLength = 20;

                        rev = GenerateIntREV(paramValueBox);
                        validatorHolder.Controls.Add(rev);

                        rfv = GenerateRFV(paramValueBox);
                        validatorHolder.Controls.Add(rfv);

                        if (parameter.Description != string.Empty) {
                            paramTypeLabel.Text = " " + parameter.Description;
                        }

                        break;
                    case 5:
                        // varchar
                        paramValueLabel.Visible = false;
                        paramValueBox.Visible = true;
                        paramCheckBox.Visible = false;
                        paramList.Visible = false;
                        timeHolder.Visible = false;

                        paramValueBox.Columns = 20;
                        paramValueBox.MaxLength = 50;

                        rfv = GenerateRFV(paramValueBox);
                        validatorHolder.Controls.Add(rfv);

                        if (parameter.Description != string.Empty) {
                            paramTypeLabel.Text = " " + parameter.Description;
                        }

                        break;
                    case 6:
                        // Station
                        paramValueLabel.Visible = true;
                        paramValueBox.Visible = false;
                        paramCheckBox.Visible = false;
                        paramList.Visible = false;
                        timeHolder.Visible = false;

                        paramValueLabel.Text = Station.Name;
                        paramValueHidden.Value = StationId.ToString();

                        break;
                    case 7:
                        // Char
                        paramValueLabel.Visible = false;
                        paramValueBox.Visible = true;
                        paramCheckBox.Visible = false;
                        paramList.Visible = false;
                        timeHolder.Visible = false;

                        paramValueBox.Columns = 20;
                        paramValueBox.MaxLength = 50;

                        rfv = GenerateRFV(paramValueBox);
                        validatorHolder.Controls.Add(rfv);

                        if (parameter.Description != string.Empty) {
                            paramTypeLabel.Text = " " + parameter.Description;
                        }

                        break;
                    case 8:
                        // Date and Time
                        paramValueLabel.Visible = false;
                        paramValueBox.Visible = true;
                        paramCheckBox.Visible = false;
                        paramList.Visible = false;
                        timeHolder.Visible = true;

                        for (int i = 0; i < 24; i++) {
                            hourList.Items.Add(i.ToString("00"));
                        }

                        for (int i = 0; i < 60; i++) {
                            minuteList.Items.Add(i.ToString("00"));
                        }

                        paramValueBox.Columns = 10;
                        paramValueBox.MaxLength = 12;

                        /*
                        if(reportToRun.TypeId == 2) { // MAC reports only
                            if(parameter.Name.ToUpper().Trim() == "START DATE") {
                                DateTime startDate = DateTime.Now;
                                while(startDate.DayOfWeek != DayOfWeek.Saturday) {
                                    startDate = startDate.AddDays(-1);
                                }
                                paramValueBox.Text = startDate.ToString("MM/dd/yyyy");
                            } else if (parameter.Name.ToUpper().Trim() == "END DATE") {
                                paramValueBox.Text = DateTime.Now.ToString("MM/dd/yyyy");
                            }
                        }
                         * */

                        Image calendarImage2 = (Image)e.Item.FindControl("calendarImage");
                        calendarImage2.Visible = true;
                        calendarImage2.ImageUrl = "~/images/calendar_button.gif";
                        calendarImage2.Attributes["OnClick"] = "showCalendarControl('" + paramValueBox.ClientID + "');";
                        //paramValueBox.Attributes["onblur"] = "hideCalendarControl();";

                        rev = GenerateDateREV(paramValueBox);
                        validatorHolder.Controls.Add(rev);

                        rfv = GenerateRFV(paramValueBox);
                        validatorHolder.Controls.Add(rfv);

                        paramTypeLabel.Text = " (MM/DD/YYYY HH:MM)";


                        break;

                    default:
                        throw new Exception("ERROR: Unknown Parameter Type");

                }



            }
        }


        void runParamRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e) {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) {
                DollarSaverDB.ParameterRow parameter = (DollarSaverDB.ParameterRow)e.Item.DataItem;

                Label parameterNameLabel = (Label)e.Item.FindControl("parameterNameLabel");
                Label parameterValueLabel = (Label)e.Item.FindControl("parameterValueLabel");

                parameterNameLabel.Text = parameter.Name;

                switch (parameter.ParameterTypeId) {

					case 2:
						parameterValueLabel.Text = ((DateTime)parameter.RunParameter.Value).ToString("MM/dd/yyyy");
						break;
                    case 6:
                        parameterValueLabel.Text = Station.Name;
                        break;
                    default :
                        parameterValueLabel.Text = parameter.RunParameter.Value.ToString();
                        break;
                }

            }
        }


        private RequiredFieldValidator GenerateRFV(Control control) {
            RequiredFieldValidator rev = new RequiredFieldValidator();

            rev.ControlToValidate = control.ID;
            rev.Display = ValidatorDisplay.Dynamic;
            rev.Text = "* Field is required";

            return rev;
        }

        private RegularExpressionValidator GenerateDateREV(Control control) {
            RegularExpressionValidator rev = new RegularExpressionValidator();

            rev.ControlToValidate = control.ID;
            rev.Display = ValidatorDisplay.Dynamic;
            rev.ValidationExpression = @"(0?[1-9]|1[012])[- /.](0?[1-9]|[12][0-9]|3[01])[- /.](19|20|21)\d\d";

            rev.Text = "* Date is in the wrong format";

            return rev;
        }

        private RegularExpressionValidator GenerateIntREV(Control control) {
            RegularExpressionValidator rev = new RegularExpressionValidator();

            rev.ControlToValidate = control.ID;
            rev.Display = ValidatorDisplay.Dynamic;
            rev.ValidationExpression = @"-?[1-9]+\d*";
            rev.Text = "* Integer is in the wrong format";

            return rev;
        }

        private RegularExpressionValidator GenerateDecimalREV(Control control) {
            RegularExpressionValidator rev = new RegularExpressionValidator();

            rev.ControlToValidate = control.ID;
            rev.Display = ValidatorDisplay.Dynamic;
            rev.ValidationExpression = @"-?\d*(.?\d+)?";
            rev.Text = "* Decimal is in the wrong format";

            return rev;
        }


        private void cancelButton_Click(object sender, EventArgs e) {
            Response.Redirect("default.aspx");
        }





        private void runButton_Click(object sender, EventArgs e) {


            id = Convert.ToInt32(idHidden.Value);

            ReportTableAdapter reportAdapter = new ReportTableAdapter();
            reportToRun = reportAdapter.GetReport(id)[0];

            ArrayList sqlParameters = new ArrayList();

            ParameterTableAdapter parameterAdapter = new ParameterTableAdapter();
            ArrayList parameters = new ArrayList();

            foreach (RepeaterItem item in parameterRepeater.Items) {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem) {

                    HiddenField paramIdHidden = (HiddenField)item.FindControl("paramIdHidden");
                    DollarSaverDB.ParameterRow parameter = parameterAdapter.GetParameter(Convert.ToInt32(paramIdHidden.Value))[0];

                    SqlParameter sqlParameter = new SqlParameter();

                    sqlParameter.ParameterName = parameter.VariableName;
                    sqlParameter.SqlDbType = (SqlDbType)parameter.ParameterType.SqlDbTypeId;

                    TextBox paramValueBox = (TextBox)item.FindControl("paramValueBox");

                    switch (parameter.ParameterTypeId) {

                        case 1:
                            CheckBox paramCheckBox = (CheckBox)item.FindControl("paramCheckBox");
                            sqlParameter.Value = paramCheckBox.Checked;
                            break;

                        case 2:

                            DateTime paramDate;
                            try {
                                paramDate = Convert.ToDateTime(paramValueBox.Text.Trim());
                            } catch {
                                ErrorMessage = parameter.Name + " is not in the proper format";
                                return;
                            }

                            sqlParameter.Value = paramDate;
                            break;
                        case 3:
                        case 4:
                        case 5:
                        case 7:
                            sqlParameter.Value = paramValueBox.Text.Trim();
                            break;
                        case 8:

                            DropDownList hourList = (DropDownList)item.FindControl("hourList");
                            DropDownList minuteList = (DropDownList)item.FindControl("minuteList");

                            string paramValue = paramValueBox.Text.Trim() + " " + hourList.SelectedValue + ":" + minuteList.SelectedValue + ":00";

                            DateTime paramDateTime;
                            try {
                                paramDateTime = Convert.ToDateTime(paramValue);
                            } catch {
                                ErrorMessage = parameter.Name + " is not in the proper format";
                                return;
                            }


                            sqlParameter.Value = paramDateTime;
                            break;

                        case 6:

                            HiddenField paramValueHidden = (HiddenField)item.FindControl("paramValueHidden");

                            sqlParameter.Value = Convert.ToInt32(paramValueHidden.Value);

                            break;

                        /* case 9 :
                        case 10 :
						
                        */
                        default:
                            throw new Exception("ERROR: Unknown Parameter Type");
                    }

                    sqlParameters.Add(sqlParameter);
                    parameter.RunParameter = sqlParameter;
                    parameters.Add(parameter);
                }

            }

            reportHeaderLabel.Text = reportHeaderLabel.Text;

            parameterHolder.Visible = false;
            toggleHolder.Visible = false;
            resultHolder.Visible = true;
            runDateHolder.Visible = true;

            bool textFile = false;
            bool excelFile = false;
            if (excelRadioButton.Checked) {
                excelFile = true;
            } else if (delimitedRadioButton.Checked) {
                textFile = true;
            } else {
                linkHolder.Visible = true;
            }

            DataTable results = new DataTable();

            try {
                results = reportToRun.Execute(sqlParameters);
            }
            catch (Exception) {
                ErrorMessage = "An error occurred while executing the report";
                return;
            }
            

            if (!textFile) {

                String header = string.Empty;
                foreach (DataColumn column in results.Columns) {
                    header += "<th class=\"reportHeader\" align=\"center\">" + column.ColumnName + "</th>";
                }

                StringBuilder val = new StringBuilder("");
                string rowClass;
                int count = 0;

                if (results.Rows.Count > 0) {

                    String cellValue = String.Empty;
                    String align = String.Empty;

                    Type intType = typeof(System.Int32);
                    Type smallIntType = typeof(System.Int16);
                    Type decimalType = typeof(System.Decimal);

                    foreach (DataRow row in results.Rows) {
                        count++;

                        if (count % 2 == 0) {
                            rowClass = "reportRow";
                        } else {
                            rowClass = "reportAlternatingRow";
                        }


                        val.Append("<tr class=\"" + rowClass + "\";>");
                        for (int i = 0; i < row.ItemArray.Length; i++) {

                            cellValue = row.ItemArray[i].ToString().Trim();

                            align = "left";
                            if (row.ItemArray[i].GetType() == intType ||
                                row.ItemArray[i].GetType() == decimalType ||
                                row.ItemArray[i].GetType() == smallIntType) {
                                align = "center";
                            }

                            if (!excelRadioButton.Checked && cellValue == String.Empty) {
                                cellValue = "&nbsp;";
                            }


                            val.Append("<td align=\"").Append(align).Append("\">").Append(cellValue).Append("</td>");

                        }
                        val.Append("</tr>\n");
                    }
                } else {
                    val.Append("<tr><td colspan=").Append(results.Columns.Count.ToString()).Append(" style=\" padding: 15px;\">No Data Found</td></tr>\n");
                }


                if (excelFile) {

                    String resultTable =
                            "<table border=\"1\" cellpadding=\"5\" cellspacing=\"0\">\n" +
                            "<thead><tr>" + header + "</tr></thead>\n" +
                            "<tbody>" + val + "</tbody>\n" +
                            "</table>\n";

                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + reportToRun.Name + ".xls");
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.Write(resultTable);
                    Response.Flush();
                    Response.Close();
                } else {
                    String resultTable =
                            "<table class=\"table table-responsive\">\n" +
                            "<thead><tr>" + header + "</tr></thead>\n" +
                            "<tbody>" + val + "</tbody>\n" +
                            "</table>\n";

                    DateTime runTime = DateTime.Now;

                    runParamRepeater.DataSource = parameters;
                    runParamRepeater.DataBind();

                    Page.EnableViewState = false;

                    runDateLabel.Text = runTime.ToShortDateString() + " " + runTime.ToShortTimeString();
                    resultsLabel.Text = resultTable;
                }

            } else {

                StringBuilder val = new StringBuilder("");

                char delimiter = ' ';
                switch (delimiterList.SelectedValue) {
                    case "pipe":
                        delimiter = '|';
                        break;
                    case "comma":
                        delimiter = ',';
                        break;
                    case "colon":
                        delimiter = ':';
                        break;
                    case "semicolon":
                        delimiter = ';';
                        break;
                    case "space":
                        delimiter = ' ';
                        break;
                    case "tab":
                        delimiter = '\t';
                        break;

                    default:
                        throw new Exception("ERROR: Unknown Delimter Type");

                }

                if (results.Rows.Count > 0) {
                    String cellValue = String.Empty;
                    foreach (DataRow row in results.Rows) {

                        for (int i = 0; i < row.ItemArray.Length; i++) {

                            cellValue = row.ItemArray[i].ToString().Trim();

                            val.Append(cellValue).Append(delimiter);
                        }
                        val.Append(Environment.NewLine);
                    }
                } else {
                    // No data found

                }

                string fileName = DateTime.Now.ToString("yyyy-MM-dd") + " " + reportToRun.Name + ".txt";

                Response.ClearContent();
                Response.ClearHeaders();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
                Response.Buffer = true;
                Response.ContentType = "text/plain";
                Response.Write(val.ToString());
                Response.Flush();
                Response.Close();

            }

        }


    }



}
