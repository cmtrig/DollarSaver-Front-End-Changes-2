<%@ Page Language="C#" MasterPageFile="~/consumer.master" AutoEventWireup="true" Inherits="DollarSaver.Web.ContactUs" Title="DollarSaver - Contact Us" Codebehind="ContactUs.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div class="uk-align-center">  
            <table cellpadding="10" cellspacing="0" border="0" class="uk-width-medium-1-2 uk-container-center">
                <tr>
                    <td align="left">
                        Please submit the form below to contact us regarding refunds, questions or comments. You will receive a response within 48 hours.
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="5" cellspacing="0" border="0" class="ds_table">
                            <tr>
                                <td colspan="2" class="heading_two greenback" align="left">
                                    Contact Us
                                </td>
                            </tr>
                            <tr>
                                <td class="form_field" nowrap width="200px;">First Name:</td>
                                <td class="form_value">
                                    <asp:TextBox ID="firstNameBox" MaxLength="50" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="firstNameBox" Text="* Required" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="form_field" nowrap>Last Name:</td>
                                <td class="form_value">
                                    <asp:TextBox ID="lastNameBox" MaxLength="50" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="lastNameBox" Text="* Required" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="form_field" nowrap width="150">E-mail:</td>
                                <td class="form_value">
                                    <asp:TextBox ID="emailBox" Columns="40" MaxLength="320" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="emailBox" Text="* Required" Display="Dynamic" runat="server" /><br />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="emailBox" Text="* Invalid E-mail Address" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic" runat="server" />   
                                </td>
                            </tr>
                            <tr>
                                <td class="form_field" nowrap>Confirm E-mail:</td>
                                <td class="form_value">
                                    <asp:TextBox ID="confirmEmailBox" Columns="40" MaxLength="320" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="confirmEmailBox" Text="* Required" Display="Dynamic" runat="server" /><br />
                                    <asp:CompareValidator ID="CompareValidator1" ControlToValidate="confirmEmailBox" ControlToCompare="emailBox" Operator="Equal" Text="* Does not match E-mail" Display="Dynamic" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="form_field">
                                    Advertiser:
                                </td>
                                <td class="form_value">
                                    <asp:DropDownList ID="advertiserList" CssClass="other_input" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="form_field">
                                    Order ID:
                                </td>
                                <td class="form_value">
                                    <asp:TextBox ID="orderIdBox" MaxLength="20" Columns="8" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="form_field">
                                    Message:    
                                </td>   
                                <td class="form_value">
                                    <asp:TextBox ID="messageBox" TextMode="MultiLine" Columns="40" Rows="8" runat="server" /> 
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="right" class="greenback">
                                    <asp:Button ID="submitButton" Text="Send" runat="server" />
                                </td>
                            </tr>
                        </table>
                    
              
    
                    </td>
                </tr>
            </table>
    </div>

</asp:Content>

