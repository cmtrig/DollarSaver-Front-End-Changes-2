<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" Inherits="DollarSaver.Web.Admin.SalesPersonList" Title="DollarSaver - Sales People" CodeBehind="SalesPersonList.aspx.cs" %>

<%@ MasterType VirtualPath="~/admin/admin.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="row">
        <div class="col-md-6">
            <div class="card">
                <div class="card-header card-header-icon" data-background-color="green">
                    <i class="material-icons">attach_money</i>
                </div>
                <div class="card-content">
                    <h4 class="card-title">Sales People</h4>
                    <asp:HyperLink ID="newLink" Text="Create New Sales Person" NavigateUrl="~/admin/SalesPersonEdit.aspx" runat="server" />

                    <asp:PlaceHolder ID="salesPeopleHolder" runat="server">
                        <asp:GridView ID="salesPeopleGrid" runat="server" CellPadding="5" CssClass="table table-responsive"
                            AutoGenerateColumns="false" BorderWidth="0" GridLines="none">
                            <Columns>
                                <asp:HyperLinkField DataTextField="FullName" DataNavigateUrlFormatString="~/admin/SalesPersonEdit.aspx?id={0}"
                                    DataNavigateUrlFields="SalesPersonId" HeaderText="Name" />
                                <asp:BoundField DataField="FullName" HeaderText="Name" />
                                <asp:TemplateField HeaderText="E-mail">
                                    <ItemTemplate>
                                        <asp:Label ID="emailLabel" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CheckBoxField HeaderText="Active" DataField="IsActive" />
                            </Columns>
                        </asp:GridView>
                    </asp:PlaceHolder>

                    <asp:PlaceHolder ID="noDataFoundHolder" runat="server">
                        <div class="alert alert-danger">
                            <span>
                                <b>Info - </b>No Sales People Found</span>
                        </div>
                    </asp:PlaceHolder>

                </div>
            </div>
        </div>
    </div>

</asp:Content>

