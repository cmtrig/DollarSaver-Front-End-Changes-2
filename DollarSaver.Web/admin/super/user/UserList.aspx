<%@ Page Language="C#" MasterPageFile="~/admin/super/super.master" AutoEventWireup="true" Inherits="DollarSaver.Web.Admin.Super.UserAdmin.UserList" Title="DollarSaver - Root Users" Codebehind="UserList.aspx.cs" %>
<%@ MasterType VirtualPath="~/admin/admin.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table cellpadding="10" cellspacing="0" border="0">
    <tr>
        <td class="form_header" align="left">
            Root Users
        </td>
    </tr>
    <tr>
        <td class="cap" align="left">
      
            <asp:HyperLink ID="newLink" Text="Create New Root User" NavigateUrl="~/admin/super/user/UserEdit.aspx" runat="server" />
        </td>
    </tr>
    <asp:PlaceHolder ID="userHolder" runat="server">
    <tr>
        <td style="padding: 0px;">
      

            <asp:GridView ID="itemGrid" runat="server" CellPadding="5" Width="100%" CssClass="admin_grid"
                AutoGenerateColumns="false" BorderWidth="0">

                <RowStyle VerticalAlign="top" />
                <AlternatingRowStyle VerticalAlign="top" />

                <Columns>
               
                   
                    <asp:HyperLinkField DataTextField="Username" DataNavigateUrlFormatString="~/admin/super/user/UserEdit.aspx?id={0}" DataNavigateUrlFields="AdminId" HeaderText="Username" ItemStyle-HorizontalAlign="left" />
                    <asp:BoundField DataField="Username" HeaderText="Username" ItemStyle-HorizontalAlign="left" />
                    <asp:BoundField DataField="RoleName" HeaderText="Role" ItemStyle-HorizontalAlign="left" />
                
                    <asp:CheckBoxField DataField="IsActive" ItemStyle-HorizontalAlign="center" HeaderText="Active" />
                
                </Columns>


            </asp:GridView>  
            
        </td>
    </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="noUserHolder" runat="server">
    <tr>
        <td class="outlined" align="left">
            No Root Users Found
        </td>
    </tr>
    </asp:PlaceHolder>
</table>



</asp:Content>
