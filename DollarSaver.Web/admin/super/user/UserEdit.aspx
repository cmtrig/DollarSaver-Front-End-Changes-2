<%@ Page Language="C#" MasterPageFile="~/admin/super/super.master" AutoEventWireup="true" Inherits="DollarSaver.Web.Admin.Super.UserAdmin.UserEdit" Title="DollarSaver - Edit User" Codebehind="UserEdit.aspx.cs" %>
<%@ MasterType VirtualPath="~/admin/admin.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table cellpadding="10" cellspacing="0" border="0" class="admin_form">
    <tr>
        <td colspan="2" class="form_header" align="left">
            <asp:Label ID="createEditLabel" runat="server" /> User
        </td>
    </tr>
    <tr>
        <td class="form_field">Username:</td>
        <td class="form_value">
            <asp:TextBox ID="usernameBox" runat="server" />
            <asp:RequiredFieldValidator ControlToValidate="usernameBox" Text="* Required" runat="server" />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="usernameBox" Text="* Invalid Username" ValidationExpression="\w+" Display="Dynamic" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="form_field">Role:</td>
        <td class="form_display">Root</td>
    </tr>
    <tr>
        <td class="form_field" nowrap>E-mail Address:</td>
        <td class="form_value">
            <asp:TextBox ID="emailBox" Columns="40" MaxLength="320" runat="server" /><br />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="emailBox" Text="* Invalid E-mail Address" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic" runat="server" />   
        </td>
    </tr>
    <tr>
        <td class="form_field">Active:</td>
        <td class="form_value"><asp:CheckBox ID="isActiveBox" Checked="true" runat="server" /></td>
    </tr>
    <asp:PlaceHolder ID="updateHolder" runat="server">
    <tr>
        <td colspan="2" align="left">
            Leave password field blank if you do not want to change it
            <br />
            <span class="small_text">Password must be at least 6 characters long</span>
        </td>
    </tr>
    </asp:PlaceHolder>
    <tr>
        <td class="form_field">Password:</td>
        <td class="form_value">
            <asp:TextBox TextMode="password" ID="passwordBox" Columns="20" MaxLength="100" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="form_field">Confirm Password:</td>
        <td class="form_value">
            <asp:TextBox TextMode="password" ID="confirmPasswordBox" Columns="20" MaxLength="100" runat="server" />
        </td>
    </tr>

    <tr>
        <td colspan="2" class="form_footer">
            <asp:Button ID="saveButton" Text="Save" runat="server" />
            <asp:Button ID="cancelButton" Text="Cancel" CausesValidation="false" runat="server" /> &nbsp; &nbsp;
            <asp:Button ID="deleteButton" Text="Delete" CausesValidation="false" runat="server" />
        </td>
    </tr>

</table>





</asp:Content>