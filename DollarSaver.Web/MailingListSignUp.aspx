<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MailingListSignUp.aspx.cs" Inherits="DollarSaver.Web.MailingListSignUp"
 MasterPageFile="~/consumer.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div class="uk-container-center uk-align-center">
     <div class="uk-margin-large page-padding">
            <table cellpadding="5" cellspacing="0" border="0" class="ds_table">
                <tr>
                    <td align="left" class="heading_two greenback" colspan="2">
                        Mailing List Sign Up
                    </td>
                </tr>
                    <tr>
                        <td class="small_text" colspan="2" align="left">
                            Enter your e-mail address to receive information about new deals!
                        </td>
                    </tr>
                <tr>
                    <td class="form_field" nowrap width="100">
                        E-mail:
                   </td>
                   <td align="left">
                        <asp:TextBox ID="emailBox" Columns="26" MaxLength="320" runat="server" />
                        <asp:RequiredFieldValidator ID="emailBoxRFV" ControlToValidate="emailBox" Text="* Required" Display="Dynamic" runat="server" /><br />
                        <asp:RegularExpressionValidator ID="emailBoxREV" ControlToValidate="emailBox" Text="* Invalid E-mail Address" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic" runat="server" />   
                    </td>  
                </tr>
                <tr>
                    <td class="form_field" nowrap>Confirm E-mail:</td>
                    <td align="left">
                        <asp:TextBox ID="confirmEmailBox" Columns="26" MaxLength="320" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="confirmEmailBox" Text="* Required" Display="Dynamic" runat="server" /><br />
                        <asp:CompareValidator ID="CompareValidator1" ControlToValidate="confirmEmailBox" ControlToCompare="emailBox" Operator="Equal" Text="* Does not match E-mail" Display="Dynamic" runat="server" />
                    </td>
                </tr>
                <tr>    
                    <td align="center" colspan="2">
                        <asp:Button ID="signUpButton" CssClass="uk-button uk-button-small" Text="Sign Up" runat="server" />
                    </td> 
                </tr>
            </table>
        
        </div>
     </div>
 </asp:Content>