<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SiteStats.aspx.cs" Inherits="DollarSaver.Web.Admin.Reports.SiteStats"
    MasterPageFile="~/admin/admin.master" %>

<%@ Register TagPrefix="zed" Namespace="ZedGraph.Web" Assembly="ZedGraph.Web" %>




<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link rel="stylesheet" href="//cdn.jsdelivr.net/chartist.js/latest/chartist.min.css">
    <script src="//cdn.jsdelivr.net/chartist.js/latest/chartist.min.js"></script>

    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-header card-header-icon" data-background-color="green">
                    <i class="material-icons">developer_board</i>
                </div>
                <div class="card-content">
                    <h4 class="card-title">Certificate Total Page Hits
                    </h4>


                    <table border="0">
                        <tr>

                            <td style="padding: 0px;">
                                <br />
                                <table class="table">
                                    <tr>
                                        <td colspan="2">Report Parameters</td>
                                    </tr>

                                    <tr>
                                        <td style="padding-left: 5px;">Start Date
                                        </td>
                                        <td style="padding-right: 10px;" class="form_value">
                                            <table border="0">
                                                <tr>
                                                    <td class="form_value">
                                                        <asp:TextBox ID="startDateBox" Columns="10" MaxLength="12" runat="server" />
                                                        <asp:Image ID="startDateCalImage" runat="server" Style="width: 16px" />
                                                    </td>
                                                    <td style="padding-left: 10px; padding-top: 4px; font-size: 12px; color: #6060E0;">(MM/DD/YYYY)
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="startDateBoxRFV" ControlToValidate="startDateBox" Display="Dynamic" Text="* Field is required" runat="server" />
                                                        <asp:RegularExpressionValidator ID="startDateBoxREV" ControlToValidate="startDateBox" Display="Dynamic" Text="* Date is in the wrong format"
                                                            ValidationExpression="(0?[1-9]|1[012])[- /.](0?[1-9]|[12][0-9]|3[01])[- /.](19|20|21)\d\d" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 5px;" class="form_field">End Date
                                        </td>
                                        <td style="padding-right: 10px;" class="form_value">
                                            <table border="0">
                                                <tr>
                                                    <td class="form_value">
                                                        <asp:TextBox ID="endDateBox" Columns="10" MaxLength="12" runat="server" />
                                                        <asp:Image ID="endDateCalImage" runat="server" Style="width: 16px" />
                                                    </td>
                                                    <td style="padding-left: 10px; padding-top: 4px; font-size: 12px; color: #6060E0;">(MM/DD/YYYY)
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="endDateBoxRFV" ControlToValidate="endDateBox" Display="Dynamic" Text="* Field is required" runat="server" />
                                                        <asp:RegularExpressionValidator ID="endDateBoxREV" ControlToValidate="endDateBox" Display="Dynamic" Text="* Date is in the wrong format"
                                                            ValidationExpression="(0?[1-9]|1[012])[- /.](0?[1-9]|[12][0-9]|3[01])[- /.](19|20|21)\d\d" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="form_footer">
                                            <asp:Button ID="runButton" Text="Run Report" runat="server" CssClass="btn btn-primary" />
                                            &nbsp;
                                            <asp:Button ID="cancelButton" Text="Cancel" runat="server" CssClass="btn btn-danger" />
                                        </td>
                                    </tr>


                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-top: 15px;"></td>
                        </tr>
                    </table>

                    <div class="ct-chart ct-perfect-fourth"></div>

                    <script>
                        new Chartist.Bar('.ct-chart', {
                            labels: <%= labelsJson %>,
                            series: <%= seriesJson %>
                        },
                        {
                            stackBars: true,
                            axisY: {
                                labelInterpolationFnc: function (value) {
                                    return value;
                                }
                            }
                        }).on('draw', function (data) {
                            if (data.type === 'bar') {
                                data.element.attr({
                                    style: 'stroke-width: 30px'
                                });
                            }
                        });
                    </script>


                </div>
            </div>
        </div>
    </div>
</asp:Content>
