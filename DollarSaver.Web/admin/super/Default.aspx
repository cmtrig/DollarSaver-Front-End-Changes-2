<%@ Page Language="C#" MasterPageFile="~/admin/super/super.master" AutoEventWireup="true" Inherits="DollarSaver.Web.Admin.Super.Default" Title="DollarSaver - Super Admin" Codebehind="Default.aspx.cs" %>
<%@ MasterType VirtualPath="~/admin/super/super.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">                           
                


<asp:Timer ID="mainTimer" runat="server" Interval="10000">
</asp:Timer>

<asp:UpdatePanel ID="mainPanel" UpdateMode="Conditional" runat="server">
<Triggers>
<asp:AsyncPostBackTrigger ControlID="mainTimer" EventName="Tick" />
</Triggers>
<ContentTemplate>
<table cellpadding="10" cellspacing="0" border="0">
    <tr>
        <td class="small_text" align="left" colspan="2">
            Updated: <asp:Label ID="timerBottomLabel" runat="server" />
        </td>
    </tr>
    <tr>
        <td valign="top" align="left">
            <table cellpadding="10" cellspacing="0" border="0">
                <tr>
                    <td class="form_header" id="headerCell" runat="server" align="left">
                        Stations
                    </td>
                
                </tr> 
                <tr>
                    <td class="cap" align="left">
                  
                        <asp:PlaceHolder ID="inactiveLinkHolder" runat="server">
                        <asp:HyperLink ID="addNewStationLink" Text="Create New Station" NavigateUrl="~/admin/super/Station/CreateStation.aspx" runat="server" /> |
                        <asp:HyperLink ID="inactiveLink" Text="Inactive Stations" NavigateUrl="~/admin/super/Default.aspx?inactive=1" runat="server" />
                        </asp:PlaceHolder>
                        <asp:PlaceHolder ID="activeLinkHolder" runat="server">
                        <asp:HyperLink ID="activeLink" Text="Active Stations" NavigateUrl="~/admin/super/Default.aspx" runat="server" />
                        </asp:PlaceHolder>
                    </td>
                </tr>
              
                <asp:PlaceHolder ID="stationHolder" runat="server">
                <tr>
                    <td style="padding: 0px;">
                        <asp:GridView ID="stationGrid" runat="server" CellPadding="5" AutoGenerateColumns="false" 
                            CssClass="admin_form" ShowFooter="true" ShowHeader="true" Width="100%" EnableViewState="false" >
                        
                            <RowStyle VerticalAlign="top" />
                            <AlternatingRowStyle VerticalAlign="top" />
                            <FooterStyle BackColor="#E6F6E9" />
                            <Columns>
                            
                                <asp:HyperLinkField Text="Admin" DataNavigateUrlFormatString="~/admin/Default.aspx?station_id={0}" DataNavigateUrlFields="StationId" ItemStyle-HorizontalAlign="Center" />
                                <asp:HyperLinkField Text="Edit" DataNavigateUrlFormatString="~/admin/super/station/StationEdit.aspx?station_id={0}" DataNavigateUrlFields="StationId" ItemStyle-HorizontalAlign="Center" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                     <asp:HyperLink ID="stationLink" Text="Site" Target="_blank" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Station" DataField="ShortName" FooterText="Total Sales" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="right" />
                                <asp:TemplateField>
                                    <HeaderStyle Wrap="false" />
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="right" Font-Bold="true" />
                                    <HeaderTemplate>
                                        Today
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="total1Label" runat="server" />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="sumTotal1Label" runat="server" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderStyle Wrap="false" />
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="right" Font-Bold="true" />
                                    <HeaderTemplate>
                                        This Month
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="total2Label" runat="server" />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="sumTotal2Label" runat="server" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderStyle Wrap="false" />
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="right" Font-Bold="true" />
                                    <HeaderTemplate>
                                        This Year
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="total3Label" runat="server" />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="sumTotal3Label" runat="server" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                
                            </Columns>

                        </asp:GridView>  
                    </td>
                
                </tr>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="noStationsHolder" runat="server">
                <tr>
                    <td class="outlined" align="left">No Stations Found</td>
                </tr>
                </asp:PlaceHolder>
            </table>
        
        </td>
        <td valign="top">
            <asp:PlaceHolder ID="recentOrderHolder" runat="server">
            <table cellpadding="10" cellspacing="0" border="0">
                <tr>
                    <td class="form_header" runat="server" align="left">
                        Recent Orders
                    </td>
                </tr>
                <tr>
                    <td class="cap" align="left">
                        <asp:HyperLink ID="viewAllOrdersLink" NavigateUrl="~/admin/super/order/OrderList.aspx" Text="View All" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="padding: 0px;">
                        <asp:GridView ID="recentOrderGrid" runat="server" CellPadding="5" AutoGenerateColumns="false" 
                            CssClass="admin_form" ShowHeader="true" Width="100%" EnableViewState="false">
                            
                            <Columns>
                            
                            <asp:HyperLinkField HeaderText="Order #" DataTextField="OrderId" DataNavigateUrlFormatString="~/admin/OrderView.aspx?id={0}&station_id={1}" DataNavigateUrlFields="OrderId, StationId" ItemStyle-HorizontalAlign="Center" ControlStyle-CssClass="order_link" />
                            <asp:BoundField HeaderText="Total" DataField="DisplayTotal" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="small_text" />
                            <asp:BoundField HeaderText="Date" DataField="AdjustedOrderDate" DataFormatString="{0:MM/dd hh:mm tt}" ItemStyle-CssClass="small_text" HtmlEncode="false" />
                            
                            </Columns>
                            
                        </asp:GridView>
                            
                            
                            
                
                </tr>
            </table>    
            </asp:PlaceHolder>
        </td>
    </tr>
</table>

</ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>

