<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" Inherits="DollarSaver.Web.Admin.AdvertiserList" Title="DollarSaver - Advertisers" CodeBehind="AdvertiserList.aspx.cs" %>

<%@ MasterType VirtualPath="~/admin/admin.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-header card-header-icon" data-background-color="green">
                    <i class="material-icons">gradient</i>
                </div>
                <div class="card-content">
                    <h4 class="card-title">
                        <asp:Label ID="headerCell" runat="server">Advertisers</asp:Label></h4>
                    <div class="btn-group">
                        <asp:PlaceHolder ID="inactiveLinkHolder" runat="server">
                            <asp:HyperLink ID="addNewAdvertiserLink" Text="Create New Advertiser" NavigateUrl="~/admin/AdvertiserEdit.aspx" runat="server" CssClass="btn btn-primary" />
                            <asp:HyperLink ID="inactiveLink" Text="Inactive Advertisers" NavigateUrl="~/admin/AdvertiserList.aspx?inactive=1" runat="server" CssClass="btn btn-primary" />
                            <asp:PlaceHolder ID="categoryHolder" runat="server">
                                <asp:HyperLink ID="categoryLink" NavigateUrl="~/admin/CategoryList.aspx" Text="Edit Categories" runat="server" CssClass="btn btn-primary" />
                            </asp:PlaceHolder>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder ID="activeLinkHolder" runat="server">
                            <asp:HyperLink ID="activeLink" Text="Active Advertisers" NavigateUrl="~/admin/AdvertiserList.aspx" runat="server" CssClass="btn btn-primary" />
                        </asp:PlaceHolder>
                    </div>
                    <br />

                    <div class="btn-group btn-group-sm">
                        <asp:Repeater ID="categoryMenuRepeater" runat="Server">
                            <ItemTemplate>
                                <a href="<%# DataBinder.Eval(Container.DataItem, "NavigateUrl") %>" class="btn"><%# DataBinder.Eval(Container.DataItem, "Text") %></a>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <asp:PlaceHolder ID="advertiserHolder" runat="server">
                        <asp:GridView ID="advertiserGrid" runat="server" CellPadding="5" Width="100%" GridLines="None"
                            AutoGenerateColumns="false" BorderWidth="0" CssClass="table table-responsive">

                            <RowStyle VerticalAlign="top" />
                            <AlternatingRowStyle VerticalAlign="top" />

                            <Columns>

                                <asp:TemplateField>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Image ID="logoUrlImage" BorderColor="#404040" BorderWidth="1" runat="server" />
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:HyperLinkField DataTextField="Name" DataNavigateUrlFormatString="~/admin/AdvertiserEdit.aspx?id={0}" DataNavigateUrlFields="AdvertiserId" HeaderText="Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" />
                                <asp:BoundField DataField="CategoryName" HeaderText="Category" />
                                <asp:BoundField HeaderText="Address" DataField="FullAddress" HtmlEncode="false" ItemStyle-HorizontalAlign="Left" />

                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Certificates
                                    </HeaderTemplate>
                                    <ItemStyle />
                                    <ItemTemplate>
                                        <asp:GridView ID="certificateGrid" runat="server" CellPadding="2" Width="100%"
                                            AutoGenerateColumns="false" BorderWidth="0" GridLines="none" ShowHeader="false">
                                            <Columns>

                                                <asp:TemplateField>
                                                    <ItemStyle Width="250px" />
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="certificateLink" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>

                                        </asp:GridView>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>


                        </asp:GridView>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="noAdvertiserHolder" runat="server">
                        <div class="row">
                            <div class=" col-md-4">
                                <div class="alert alert-danger">
                                    <span>
                                        <b>Info - </b>No Advertisers Found</span>
                                </div>
                            </div>
                        </div>
                    </asp:PlaceHolder>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

