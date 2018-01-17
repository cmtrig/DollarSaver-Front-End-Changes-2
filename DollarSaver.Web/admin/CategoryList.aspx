<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" Inherits="DollarSaver.Web.Admin.CategoryList" Title="Categories" CodeBehind="CategoryList.aspx.cs" %>

<%@ MasterType VirtualPath="~/admin/admin.master" %>
<%@ Import Namespace="DollarSaver.Core.Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-4">
            <div class="card">
                <div class="card-header card-header-icon" data-background-color="green">
                    <i class="material-icons">folder_special</i>
                </div>
                <div class="card-content">
                    <h4 class="card-title">
                        <asp:Label ID="headerCell" runat="server">Categories</asp:Label>
                    </h4>

                    <asp:HyperLink ID="addNewCategoryLink" NavigateUrl="~/admin/CategoryCreate.aspx" Text="Create New Category" runat="server" CssClass="btn btn-primary" />

                    <asp:GridView ID="categoryGrid" runat="server" AutoGenerateColumns="false" BorderWidth="0" CssClass="table table-responsive" GridLines="None">

                        <RowStyle VerticalAlign="top" />
                        <AlternatingRowStyle VerticalAlign="top" />

                        <Columns>

                            <asp:BoundField HeaderText="Order" DataField="DisplaySeqNo" ItemStyle-HorizontalAlign="center" />

                            <asp:HyperLinkField DataTextField="Name" DataNavigateUrlFormatString="~/admin/CategoryEdit.aspx?id={0}"
                                DataNavigateUrlFields="CategoryId" HeaderText="Name" />

                            <asp:TemplateField HeaderText="Order">
                                <ItemTemplate>
                                    <asp:LinkButton ID="moveUpButton" CommandName="up" CommandArgument="<%#((DollarSaverDB.CategoryRow) Container.DataItem).CategoryId %>" runat="server"><i class="material-icons">arrow_upward</i> </asp:LinkButton>
                                    <asp:LinkButton ID="moveDownButton" Text="Down" CommandName="down" CommandArgument="<%#((DollarSaverDB.CategoryRow) Container.DataItem).CategoryId %>" runat="server"><i class="material-icons">arrow_downward</i> </asp:LinkButton>
                                </ItemTemplate>

                            </asp:TemplateField>

                        </Columns>

                    </asp:GridView>

                </div>
            </div>
        </div>
    </div>


</asp:Content>

