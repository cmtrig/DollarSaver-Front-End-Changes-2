<%@ Page Language="C#" MasterPageFile="~/admin/super/super.master" AutoEventWireup="true" Inherits="DollarSaver.Web.Admin.Super.IssueAdmin.IssueView" 
    Title="DollarSaver - View Issue" Codebehind="IssueView.aspx.cs" ValidateRequest="false" %>
<%@ MasterType VirtualPath="~/admin/super/super.master" %>
<%@ Register Src="~/controls/CalendarBox.ascx" TagPrefix="DollarSaver" TagName="CalendarBox" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<table cellpadding="10" cellspacing="0" border="0" class="admin_form" width="500">
    <tr>
        <td class="form_header" colspan="2" align="left">View Issue</td>
    </tr>
    <tr>
        <td class="form_field">Issue ID:</td>
        <td class="form_display">
            <asp:Label ID="issueIdLabel" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="form_field">Station:</td>
        <td class="form_display">
            <asp:Label ID="stationLabel" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="form_field">Status:</td>
        <td class="form_display">
            <asp:Label ID="statusLabel" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="form_field">Date:</td>
        <td class="form_display">
            <asp:Label ID="dateLabel" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="form_field">Advertiser:</td>
        <td class="form_display">
            <asp:HyperLink ID="advertiserLink" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="form_field">Order ID:</td>
        <td class="form_display">
            <asp:HyperLink ID="orderLink" runat="server" />
            <asp:Label ID="orderIdLabel" Style="color: Red;" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="form_field">Name:</td>
        <td class="form_display">
            <asp:Label ID="nameLabel" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="form_field">Email:</td>
        <td class="form_display">
            <asp:Label ID="emailLabel" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="form_field">Message:</td>
        <td class="form_display">
            <asp:Label ID="messageLabel" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="form_field">Notes:</td>
        <td class="form_display">
            <asp:TextBox ID="adminNotesBox" TextMode="MultiLine" Columns="50" Rows="8" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="form_footer" colspan="2">
            <asp:Button ID="toggleButton" runat="server" />
            <asp:Button ID="saveButton" Text="Save Note" runat="server" />
            <asp:Button ID="cancelButton" Text="Cancel" runat="server" />
        </td>
    </tr>

</table>




</asp:Content>

