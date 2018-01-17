<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" Inherits="DollarSaver.Web.Admin.OrderList" Title="DollarSaver - Orders" CodeBehind="OrderList.aspx.cs" %>

<%@ MasterType VirtualPath="~/admin/admin.master" %>
<%@ Import Namespace="DollarSaver.Core.Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-6">
            <div class="card">
                <div class="card-header card-header-icon" data-background-color="green">
                    <i class="material-icons">search</i>
                </div>
                <div class="card-content">
                    <h4 class="card-title">Order Search</h4>
                    <div class="form-horizontal">

                        <div class="row">
                            <label class="col-md-3 label-on-left">Search</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:TextBox ID="searchBox" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">Advertiser</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:DropDownList ID="advertiserList" CssClass="selectpicker" data-style="btn btn-primary" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">Date From</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:TextBox ID="startDateBox" Required="false" runat="server" CssClass="form-control datepicker" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">Date To</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:TextBox ID="endDateBox" Required="false" runat="server" CssClass="form-control datepicker" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">Delivery</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:DropDownList ID="deliveryTypeList" CssClass="selectpicker" data-style="btn btn-primary" runat="server">
                                        <asp:ListItem Text="-- All --" Value="0" Selected="True" />
                                        <asp:ListItem Text="Printable" Value="1" />
                                        <asp:ListItem Text="Not Printable - Mail to Customer" Value="2" />
                                        <asp:ListItem Text="Not Printable - Pick Up" Value="3" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-9 col-md-offset-3">
                                <div class="form-group">
                                    <asp:Button ID="searchButton" Text="Search" runat="server" CssClass="btn btn-primary" />
                                </div>
                            </div>
                        </div>


                        <asp:PlaceHolder ID="orderHolder" runat="server">

                            <asp:GridView ID="orderGrid" runat="server" CellPadding="5" AutoGenerateColumns="false"
                                AllowPaging="true" PageSize="25" PagerSettings-Position="Top" PagerStyle-HorizontalAlign="right"
                                class="table table-hover table-responsive" PagerStyle-CssClass="pager" GridLines="none">

                                <PagerTemplate>
                                    <asp:LinkButton ID="previousButton" CausesValidation="false" CommandName="previous" Text="Previous" runat="server" />
                                        <asp:DropDownList ID="pageList" AutoPostBack="true" OnSelectedIndexChanged="pageList_SelectedIndexChanged" runat="server" />
                                    <asp:LinkButton ID="nextButton" CausesValidation="false" CommandName="next" Text="Next" runat="server" />
                                </PagerTemplate>

                                <RowStyle VerticalAlign="top" />
                                <AlternatingRowStyle VerticalAlign="top" />
                                <Columns>

                                    <asp:HyperLinkField HeaderText="Order #" DataTextField="OrderId" DataNavigateUrlFormatString="~/admin/OrderView.aspx?id={0}" DataNavigateUrlFields="OrderId" ItemStyle-HorizontalAlign="Center" ControlStyle-CssClass="order_link" />
                                    <asp:BoundField HeaderText="Total" DataField="DisplayTotal" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="100" />
                                    <asp:BoundField HeaderText="Customer" DataField="BillingName" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200" ItemStyle-Wrap="false" />
                                    <asp:BoundField HeaderText="Date" DataField="AdjustedOrderDate" DataFormatString="{0:MM/dd/yyyy hh:mm:ss tt}" HtmlEncode="false" />

                                </Columns>

                            </asp:GridView>

                        </asp:PlaceHolder>
                        <asp:PlaceHolder ID="noDataFoundHolder" runat="server">
                            <div class="alert alert-danger">
                                <span>
                                    <b>Info - </b>No Orders Found</span>
                            </div>
                        </asp:PlaceHolder>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Scripts1" ContentPlaceHolderID="ScriptsPLaceHolder" runat="Server">
    <script>
        $(document).ready(function () {
            ds.initFormExtendedDatetimepickers();
        });
    </script>
</asp:Content>
