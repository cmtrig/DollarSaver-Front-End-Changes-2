<%@ Page Language="C#" MasterPageFile="~/admin/super/super.master" AutoEventWireup="true" Inherits="DollarSaver.Web.Admin.Super.StationAdmin.StationEdit" Title="DollarSaver - Edit Station" Codebehind="StationEdit.aspx.cs" %>
<%@ MasterType VirtualPath="~/admin/admin.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<table cellpadding="10" cellspacing="0" border="0" class="admin_form">
    <tr>
        <td class="form_header" colspan="2" align="left">Edit Station</td>
    </tr>
    <tr>
        <td class="form_field">Name:</td>
        <td class="form_display">
            <asp:Label ID="nameLabel" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="form_field">Station Login Code:</td>
        <td class="form_value">
            <asp:TextBox ID="codeBox" Columns="12" MaxLength="20" runat="server" />
            <asp:RequiredFieldValidator ID="codeBoxRFV" ControlToValidate="codeBox" Text="*" Display="Static" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="form_field">Subdomain:</td>
        <td class="form_value">
            http://<asp:TextBox ID="subdomainBox" Columns="10" MaxLength="50" runat="server" />.dollarsavershow.com
            <asp:RegularExpressionValidator ID="subdomainBoxREV" ControlToValidate="subdomainBox" Text="* Invalid" ValidationExpression="\w+([-]\w+)*" Display="Static" runat="server" />  
        </td>
    </tr>
    <tr>
        <td class="form_field">Is Active:</td>
        <td class="form_display"><asp:CheckBox ID="isActiveBox" runat="server" /></td>
    </tr>
    <tr>
        <td class="form_footer" colspan="2">
            <asp:Button ID="saveButton" Text="Save" runat="server" />
            <asp:Button ID="cancelButton" Text="Cancel" CausesValidation="false" runat="server" />
        </td>
    </tr>

</table>


</asp:Content>

