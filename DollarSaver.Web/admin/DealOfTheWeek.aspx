<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" Inherits="DollarSaver.Web.Admin.DealOfTheWeek" Title="DollarSaver - Deal of the Week" CodeBehind="DealOfTheWeek.aspx.cs" %>

<%@ MasterType VirtualPath="~/admin/admin.master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="row">
        <div class="col-md-6">
            <div class="card">
                <div class="card-header card-header-icon" data-background-color="green">
                    <i class="material-icons">timer</i>
                </div>
                <div class="card-content">
                    <h4 class="card-title">Deal of the Week Settings</h4>
                    <div class="form-horizontal">

                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <label class="col-md-3 label-on-left">Display</label>
                                    <div class="col-md-9">
                                        <div class="form-group">
                                            <asp:DropDownList ID="displayDayList" CssClass="selectpicker" data-style="btn btn-primary" runat="server">
                                                <asp:ListItem Value="0" Text="Sunday" />
                                                <asp:ListItem Value="1" Text="Monday" />
                                                <asp:ListItem Value="2" Text="Tuesday" />
                                                <asp:ListItem Value="3" Text="Wednesday" />
                                                <asp:ListItem Value="4" Text="Thursday" />
                                                <asp:ListItem Value="5" Text="Friday" />
                                                <asp:ListItem Value="6" Text="Saturday" />
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="displayHourList" CssClass="selectpicker" data-style="btn btn-primary" runat="server">
                                                <asp:ListItem Value="6" Text="06:00 AM" />
                                                <asp:ListItem Value="7" Text="07:00 AM" />
                                                <asp:ListItem Value="8" Text="08:00 AM" />
                                                <asp:ListItem Value="9" Text="09:00 AM" />
                                                <asp:ListItem Value="10" Text="10:00 AM" />
                                                <asp:ListItem Value="11" Text="11:00 AM" />
                                                <asp:ListItem Value="12" Text="12:00 PM" />
                                                <asp:ListItem Value="13" Text="01:00 PM" />
                                                <asp:ListItem Value="14" Text="02:00 PM" />
                                                <asp:ListItem Value="15" Text="03:00 PM" />
                                                <asp:ListItem Value="16" Text="04:00 PM" />
                                                <asp:ListItem Value="17" Text="05:00 PM" />
                                                <asp:ListItem Value="18" Text="06:00 PM" />
                                                <asp:ListItem Value="19" Text="07:00 PM" />
                                                <asp:ListItem Value="20" Text="08:00 PM" />
                                                <asp:ListItem Value="21" Text="09:00 PM" />
                                                <asp:ListItem Value="22" Text="10:00 PM" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6">
                                <div class="row">
                                    <label class="col-md-3 label-on-left">On Sale</label>
                                    <div class="col-md-9">
                                        <div class="form-group">
                                            <asp:DropDownList ID="onSaleDayList" CssClass="selectpicker" data-style="btn btn-primary" runat="server">
                                                <asp:ListItem Value="0" Text="Sunday" />
                                                <asp:ListItem Value="1" Text="Monday" />
                                                <asp:ListItem Value="2" Text="Tuesday" />
                                                <asp:ListItem Value="3" Text="Wednesday" />
                                                <asp:ListItem Value="4" Text="Thursday" />
                                                <asp:ListItem Value="5" Text="Friday" />
                                                <asp:ListItem Value="6" Text="Saturday" />
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="onSaleHourList" CssClass="selectpicker" data-style="btn btn-primary" runat="server">
                                                <asp:ListItem Value="6" Text="06:00 AM" />
                                                <asp:ListItem Value="7" Text="07:00 AM" />
                                                <asp:ListItem Value="8" Text="08:00 AM" />
                                                <asp:ListItem Value="9" Text="09:00 AM" />
                                                <asp:ListItem Value="10" Text="10:00 AM" />
                                                <asp:ListItem Value="11" Text="11:00 AM" />
                                                <asp:ListItem Value="12" Text="12:00 PM" />
                                                <asp:ListItem Value="13" Text="01:00 PM" />
                                                <asp:ListItem Value="14" Text="02:00 PM" />
                                                <asp:ListItem Value="15" Text="03:00 PM" />
                                                <asp:ListItem Value="16" Text="04:00 PM" />
                                                <asp:ListItem Value="17" Text="05:00 PM" />
                                                <asp:ListItem Value="18" Text="06:00 PM" />
                                                <asp:ListItem Value="19" Text="07:00 PM" />
                                                <asp:ListItem Value="20" Text="08:00 PM" />
                                                <asp:ListItem Value="21" Text="09:00 PM" />
                                                <asp:ListItem Value="22" Text="10:00 PM" />
                                            </asp:DropDownList>
                                            <asp:Button ID="saveSettingsButton" Text="Save" runat="server" CssClass="btn btn-success" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <h4>Deals</h4>
                        <asp:PlaceHolder ID="dealHolder" runat="server">
                            <asp:GridView ID="dealGrid" runat="server" CellPadding="5" AutoGenerateColumns="false"
                                AllowPaging="true" PageSize="10" PagerSettings-Mode="NumericFirstLast" PagerSettings-Position="TopAndBottom" PagerStyle-HorizontalAlign="right"
                                BorderWidth="0" GridLines="none" CssClass="table table-responsive">

                                <RowStyle VerticalAlign="top" />
                                <AlternatingRowStyle VerticalAlign="top" />
                                <Columns>
                                    <asp:BoundField HeaderText="On Sale" DataField="AdjustedOnSaleDate" DataFormatString="{0:ddd MMM dd, yyyy hh:mm tt}" HtmlEncode="false" />
                                    <asp:HyperLinkField HeaderText="Certificate" DataTextField="ShortName" DataNavigateUrlFormatString="~/admin/CertificateEdit.aspx?id={0}" DataNavigateUrlFields="CertificateId" />
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" CssClass="GridPager" />
                            </asp:GridView>
                        </asp:PlaceHolder>

                        <asp:PlaceHolder ID="noDealHolder" runat="server">
                            <div class="alert alert-danger">
                                <span>
                                    <b>Info - </b>No Deals Found</span>
                            </div>
                        </asp:PlaceHolder>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
