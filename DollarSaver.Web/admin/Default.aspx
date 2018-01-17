<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true"
    Inherits="DollarSaver.Web.Admin.Default" Title="DollarSaver - Admin" CodeBehind="Default.aspx.cs" %>

<%@ MasterType VirtualPath="~/admin/admin.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="row">
        <asp:PlaceHolder ID="managerHolder" runat="server">
            <div class="col-md-6">
                <div class="card">
                    <div class="card-header card-header-icon" data-background-color="green">
                        <i class="material-icons">assignment</i>
                    </div>
                    <div class="card-content">
                        <h4 class="card-title">Recent Non-Printable Orders</h4>

                        <asp:GridView ID="nonPrintableOrderGrid" runat="server" AutoGenerateColumns="false" GridLines="None" BorderStyle="None"
                            CssClass="table table-hover table-responsive" PagerStyle-CssClass="pager" Width="100%">

                            <Columns>
                                <asp:HyperLinkField HeaderText="Order #" DataTextField="OrderId"
                                    DataNavigateUrlFormatString="~/admin/OrderView.aspx?id={0}"
                                    DataNavigateUrlFields="OrderId" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="Customer" DataField="BillingName" />
                                <asp:BoundField HeaderText="Date" DataField="AdjustedOrderDate" DataFormatString="{0:MM/dd/yyyy hh:mm:ss tt}" HtmlEncode="false" />
                            </Columns>

                        </asp:GridView>
                    </div>
                </div>
            </div>

            <div class="col-md-6">
                <div class="card">
                    <div class="card-header card-header-icon" data-background-color="green">
                        <i class="material-icons">monetization_on</i>
                    </div>
                    <div class="card-content table-full-width">
                        <h4 class="card-title">Recent Orders</h4>
                        <asp:HyperLink ID="ordersLink" NavigateUrl="~/admin/OrderList.aspx" Text="View All Orders" runat="server" />

                        <asp:GridView ID="recentOrderGrid" runat="server" CellPadding="5" AutoGenerateColumns="false" GridLines="None" BorderStyle="None"
                            CssClass="table table-hover table-responsive" PagerStyle-CssClass="pager" Width="100%" EmptyDataText="No recent orders found">

                            <Columns>
                                <asp:HyperLinkField HeaderText="Order #" DataTextField="OrderId"
                                    DataNavigateUrlFormatString="~/admin/OrderView.aspx?id={0}"
                                    DataNavigateUrlFields="OrderId" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="Customer" DataField="BillingName" />
                                <asp:BoundField HeaderText="Date" DataField="AdjustedOrderDate" DataFormatString="{0:MM/dd/yyyy hh:mm:ss tt}" HtmlEncode="false" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </asp:PlaceHolder>

        <div class="col-md-6">
            <div class="card">
                <div class="card-header card-header-icon" data-background-color="red">
                    <i class="material-icons">assignment_late</i>
                </div>
                <div class="card-content">
                    <h4 class="card-title">Low Stock Items</h4>

                    <asp:GridView ID="runningLowGrid" runat="server" Width="100%" AutoGenerateColumns="false" GridLines="None" BorderStyle="None"
                        CssClass="table table-hover table-responsive" UseAccessibleHeader="true" EmptyDataText="No certificates are running low">

                        <Columns>

                            <asp:HyperLinkField DataTextField="ShortName" HeaderText="Name"
                                DataNavigateUrlFields="CertificateId" DataNavigateUrlFormatString="CertificateEdit.aspx?id={0}" />

                            <asp:TemplateField>
                                <HeaderTemplate>Qty Remaining</HeaderTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="qtyLabel" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>Sales Person</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="salesPersonLabel" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>

                </div>
            </div>
        </div>

    </div>
</asp:Content>

