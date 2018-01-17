<%@ Page Language="C#" MasterPageFile="~/admin/super/super.master" AutoEventWireup="true" Inherits="DollarSaver.Web.Admin.Super.OrderAdmin.OrderList" Title="DollarSaver - Orders" Codebehind="OrderList.aspx.cs" %>
<%@ MasterType VirtualPath="~/admin/super/super.master" %>
<%@ Register Src="~/controls/CalendarBox.ascx" TagPrefix="DollarSaver" TagName="CalendarBox" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table cellpadding="10" cellspacing="0" border="0">
    <tr>
        <td align="center">
            <table cellpadding="5" cellspacing="0" border="0" class="admin_form">
                <tr>
                    <td colspan="2" class="form_header">Order Search</td>
                </tr>
                <tr>
                    <td class="form_field">Search:</td>
                    <td class="form_value"><asp:TextBox ID="searchBox" Columns="20" MaxLength="50" runat="server" /></td>
                </tr>
                <tr>
                    <td class="form_field">Station:</td>
                    <td class="form_value"><asp:DropDownList ID="stationList" CssClass="other_input" runat="server" /></td>
                </tr>
                <tr>
                    <td class="form_field">Dates:</td>
                    <td class="form_value"><DollarSaver:CalendarBox ID="startDateBox" Required="false" runat="server" /> to <DollarSaver:CalendarBox ID="endDateBox" Required="false" runat="server" /></td>
                </tr>
                <tr>
                    <td colspan="2" align="right" class="form_footer">
                        <asp:Button ID="searchButton" Text="Search" runat="server" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <asp:PlaceHolder ID="orderHolder" runat="server">
    <tr>
        <td style="padding: 0px;" align="center">
      

            <asp:GridView ID="orderGrid" runat="server" CellPadding="5" AutoGenerateColumns="false"
                AllowPaging="true" PageSize="25" PagerSettings-Position="Top" PagerStyle-HorizontalAlign="right"
                CssClass="admin_form" PagerStyle-CssClass="pager">

                <PagerTemplate>
                    <asp:LinkButton ID="previousButton" CausesValidation="false" CommandName="previous" Text="Previous" runat="server" />
                    <asp:DropDownList ID="pageList" AutoPostBack="true" OnSelectedIndexChanged="pageList_SelectedIndexChanged" CssClass="other_input" Style="font-size: 10pt;" runat="server" />
                    <asp:LinkButton ID="nextButton" CausesValidation="false" CommandName="next" Text="Next" runat="server" />
                </PagerTemplate>

                <RowStyle VerticalAlign="top" />
                <AlternatingRowStyle VerticalAlign="top" />
                <Columns>
                
                    <asp:HyperLinkField HeaderText="Order #" DataTextField="OrderId" DataNavigateUrlFormatString="~/admin/OrderView.aspx?id={0}&station_id={1}" DataNavigateUrlFields="OrderId, StationId" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center" ControlStyle-CssClass="order_link" />
                    <asp:BoundField HeaderText="Station" DataField="StationShortName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField HeaderText="Total" DataField="DisplayTotal" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="100" HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Customer" DataField="BillingName" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200" ItemStyle-Wrap="false" />
                    <asp:BoundField HeaderText="Date" DataField="AdjustedOrderDate" DataFormatString="{0:MM/dd/yyyy hh:mm:ss tt}" HtmlEncode="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                    
                
                </Columns>


            </asp:GridView>  
            
        </td>
    </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="noDataFoundHolder" runat="server">
    <tr>
        <td align="left" class="outlined">
            No Orders Found
        </td>
    </tr>
    </asp:PlaceHolder>
</table>






</asp:Content>

