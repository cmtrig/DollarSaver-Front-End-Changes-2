<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" Inherits="DollarSaver.Web.Admin.AdminList" Title="DollarSaver - Users" CodeBehind="AdminList.aspx.cs" %>

<%@ MasterType VirtualPath="~/admin/admin.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="row">
        <div class="col-md-6">
            <div class="card">
                <div class="card-header card-header-icon" data-background-color="green">
                    <i class="material-icons">assignment</i>
                </div>
                <div class="card-content">
                    <h4 class="card-title">Station Users</h4>
                    <asp:HyperLink ID="newLink" Text="Create New User" NavigateUrl="~/admin/AdminEdit.aspx" runat="server" />

                    <asp:GridView ID="itemGrid" runat="server" CssClass="table table-hover table-responsive"
                        AutoGenerateColumns="false" BorderWidth="0" GridLines="None" EmptyDataText="No Users Found">

                        <Columns>

                            <asp:HyperLinkField DataTextField="Username" DataNavigateUrlFormatString="~/admin/AdminEdit.aspx?id={0}" DataNavigateUrlFields="AdminId" HeaderText="Username" ItemStyle-HorizontalAlign="left" />
                            <asp:BoundField DataField="Username" HeaderText="Username" ItemStyle-HorizontalAlign="left" />
                            <asp:BoundField DataField="RoleName" HeaderText="Role" ItemStyle-HorizontalAlign="left" />
                            <asp:CheckBoxField DataField="IsOrderContact" ItemStyle-HorizontalAlign="center" HeaderText="Contact" />

                            <asp:CheckBoxField DataField="IsActive" ItemStyle-HorizontalAlign="center" HeaderText="Active" />

                        </Columns>

                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
