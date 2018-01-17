<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" Title="DollarSaver - Run Report" CodeBehind="RunReport.aspx.cs" Inherits="DollarSaver.Web.Admin.Reports.RunReport" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="row">
        <div class="col-md-8">
            <div class="card">
                <div class="card-header card-header-icon" data-background-color="green">
                    <i class="material-icons">developer_board</i>
                </div>
                <div class="card-content">
                    <h4 class="card-title">
                        <input type="hidden" id="idHidden" runat="server" />
                        <asp:Label ID="statusMessageLabel" CssClass="statusMessage" runat="server" />
                    </h4>

                    <div class="form-horizontal">

                        <h2>
                            <asp:Label ID="reportHeaderLabel" runat="server" /></h2>

                        <div class="row">
                            <div class="col-md-6">
                                <asp:PlaceHolder ID="runDateHolder" runat="Server">
                                    <span style="font-weight: bold;">Report Date:</span>
                                    <asp:Label ID="runDateLabel" runat="Server" />
                                </asp:PlaceHolder>
                            </div>
                        </div>


                        <table border="0">

                            <asp:PlaceHolder ID="parameterHolder" runat="server">
                                <tr>

                                    <td style="padding: 0px;">
                                        <br />
                                        <table border="0" class="table no-border">
                                            <tr>
                                                <td class="form_header" colspan="2">
                                                    <h4>Report Parameters</h4>
                                                </td>
                                            </tr>

                                            <asp:Repeater ID="parameterRepeater" runat="server">
                                                <HeaderTemplate>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="padding: 0 5px;" class="form_field">
                                                            <asp:Label ID="paramNameLabel" runat="server" />
                                                        </td>
                                                        <td style="padding-right: 10px;" class="form_value">
                                                            <table border="0">
                                                                <tr>
                                                                    <td class="form_value">
                                                                        <asp:HiddenField ID="paramIdHidden" runat="server" />
                                                                        <asp:ListBox ID="paramList" SelectionMode="Multiple" Rows="5" Visible="false" CssClass="other_input" runat="server" />
                                                                        <asp:HiddenField ID="paramValueHidden" runat="server" />
                                                                        <asp:Label ID="paramValueLabel" Visible="false" runat="server" />
                                                                        <asp:TextBox ID="paramValueBox" Visible="false" runat="server" />
                                                                        <asp:CheckBox ID="paramCheckBox" Visible="false" runat="server" />
                                                                        <asp:Image ID="calendarImage" Visible="false" runat="server" Style="max-width: 16px" />
                                                                        <asp:PlaceHolder ID="timeHolder" Visible="false" runat="server">&nbsp;
                                        <asp:DropDownList ID="hourList" CssClass="other_input" runat="server" />
                                                                            :
                                        <asp:DropDownList ID="minuteList" CssClass="other_input" runat="server" />
                                                                        </asp:PlaceHolder>
                                                                    </td>
                                                                    <td style="padding-left: 10px; padding-top: 4px; font-size: 12px; color: #6060E0;">
                                                                        <asp:Label ID="paramTypeLabel" runat="server" />
                                                                    </td>
                                                                    <td style="padding-left: 6px;">
                                                                        <asp:PlaceHolder ID="validatorHolder" runat="server" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>

                                            </asp:Repeater>

                                            <tr>
                                                <td style="padding: 0 5px;">Format</td>
                                                <td>
                                                    <table border="0">
                                                        <tr>
                                                            <td style="padding-right: 10px">
                                                                <asp:RadioButton Checked="true" OnClick="javascript: toggleBox('delimiterDiv', 0)" GroupName="formatRadioButton" ID="htmlRadioButton" value="html" runat="server" />
                                                                Web 
                                                            </td>
                                                            <td style="padding-right: 10px">
                                                                <asp:RadioButton OnClick="javascript: toggleBox('delimiterDiv', 0)" GroupName="formatRadioButton" ID="excelRadioButton" value="excel" runat="server" />
                                                                Excel
                                                            </td>
                                                            <td style="padding-right: 10px">
                                                                <asp:RadioButton OnClick="javascript: toggleBox('delimiterDiv', 1)" GroupName="formatRadioButton" ID="delimitedRadioButton" value="delimited" runat="server" />
                                                                Delimited
                                                            </td>
                                                            <td>
                                                            <script type="text/javascript">
<!--

    function toggleBox(szDivID, iState) // 1 visible, 0 hidden
    {
        //try {
        if (document.layers)	   //NN4+
        {
            if (document.layers[szDivID] != null) {
                document.layers[szDivID].visibility = iState ? "show" : "hide";
            }
        }
        else if (document.getElementById)	  //gecko(NN6) + IE 5+
        {
            var obj = document.getElementById(szDivID);
            if (obj != null) {
                obj.style.visibility = iState ? "visible" : "hidden";
            }
        }
        else if (document.all)	// IE 4
        {
            if (document.all[szDivID] != null) {
                document.all[szDivID].style.visibility = iState ? "visible" : "hidden";
            }
        }
        //} catch(e) {}
    }
// -->

                                                                </script>


                                                                <div id="delimiterDiv" name="delimiterDiv">
                                                                    <asp:DropDownList ID="delimiterList" runat="server">
                                                                        <asp:ListItem Value="pipe">Pipe |</asp:ListItem>
                                                                        <asp:ListItem Value="comma">Comma ,</asp:ListItem>
                                                                        <asp:ListItem Value="colon">Colon :</asp:ListItem>
                                                                        <asp:ListItem Value="semicolon">Semicolon ;</asp:ListItem>
                                                                        <asp:ListItem Value="space">Space</asp:ListItem>
                                                                        <asp:ListItem Value="tab">Tab</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="form_footer">
                                                    <asp:Button ID="runButton" Text="Run Report" runat="server" CssClass="btn btn-primary" />
                                                    &nbsp;
                        <asp:Button ID="cancelButton" Text="Cancel" CausesValidation="false" runat="server" CssClass="btn btn-danger" />
                                                </td>
                                            </tr>


                                        </table>


                                    </td>

                                </tr>
                            </asp:PlaceHolder>
                            <asp:PlaceHolder ID="linkHolder" runat="server">
                                <tr>
                                    <td style="padding: 11px;">
                                        <span class="noprint">
                                            <asp:HyperLink ID="reRunReportLink" Text="Re-run Report" NavigateUrl="javascript: history.go(-1);" runat="server" />
                                            &nbsp;
            <asp:HyperLink ID="reportListLink" Text="Report List" runat="server" />
                                        </span>
                                    </td>
                                </tr>
                            </asp:PlaceHolder>

                            <tr>
                                <td>
                                    <asp:PlaceHolder ID="resultHolder" runat="server">
                                        <table border="0">
                                            <tr>
                                                <td>
                                                    <asp:Repeater ID="runParamRepeater" runat="Server">
                                                        <HeaderTemplate>
                                                            <table class="table">
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="parameterNameLabel" runat="server" />:
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="parameterValueLabel" runat="server" />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </table>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </td>
                                            </tr>
                                        </table>                                       

                                    </asp:PlaceHolder>
                                </td>
                            </tr>

                        </table>

                        <asp:Label ID="resultsLabel" runat="server" />

                        <asp:PlaceHolder ID="toggleHolder" runat="server">
                            <script type="text/javascript">
<!--

    //initially hide the div
    toggleBox('delimiterDiv', 0)

// -->

                            </script>
                        </asp:PlaceHolder>

                    </div>
                </div>
            </div>
        </div>
    </div>



</asp:Content>
