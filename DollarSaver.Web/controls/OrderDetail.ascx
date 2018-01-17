<%@ Control Language="C#" AutoEventWireup="true" Inherits="DollarSaver.Web.controls.OrderDetail" Codebehind="OrderDetail.ascx.cs" %>
<%@ Register Src="~/controls/Name.ascx" TagPrefix="DollarSaver" TagName="Name" %>
        
<asp:PlaceHolder ID="orderHolder" runat="server">

<table cellpadding="0" cellspacing="0" border="0">
    <tr>
        <td valign="top" style="border: 0px;">
            <table cellpadding="10" cellspacing="0" border="0" class="admin_form">
                <tr>
                <td colspan="2" class="form_header">
                    Order Details
                </td>
                <asp:PlaceHolder ID="editLinkHolder" runat="server">
                <td colspan="2" class="form_header">
                    <asp:HyperLink ID="editLink" Text="Edit Order" runat="server" />
                </td>
                </asp:PlaceHolder>
                <tr>
                    <td class="form_field">
                        Order ID:
                    </td>
                    <td class="form_display">
                        <asp:Label ID="orderIdLabel" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="form_field">
                        Date:
                    </td>
                    <td class="form_display">
                        <asp:Label ID="orderDateLabel" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="form_field">
                        Status:
                    </td>
                    <td class="form_display">
                        <asp:Label ID="statusLabel" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="form_field">
                        Name:
                    </td>
                    <td class="form_display">
                        <asp:Label ID="nameLabel" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="form_field">
                        E-Mail:
                    </td>
                    <td class="form_display">
                        <asp:Label ID="emailLabel" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="form_field">
                        Address:
                    </td>
                    <td class="form_display">
                        <asp:Label ID="addressLabel" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="form_field">
                        Phone:
                    </td>
                    <td class="form_display">
                        <asp:Label ID="phoneLabel" runat="server" />
                    </td>
                </tr>
            </table>
            
        </td>
        <td style="padding: 10px;"></td>
        <td valign="top">
        
            <table cellpadding="8" cellspacing="0" border="0" width="100%" class="admin_form">
                <tr>
                    <td colspan="5" class="form_header" style="padding: 10px;">
                        Line Items
                    </td>
                </tr>
                <tr>
                    <td>Certificates</td>
                    <td>Number</td>
                    <td>Price</td>
                    <td>Qty</td>
                    <td>Total</td>
                </tr>
                <asp:Repeater ID="lineItemRepeater" runat="server">
                <HeaderTemplate>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td valign="top">
                            <asp:HyperLink ID="certificateLink" runat="server" />
                        </td>
                        <td valign="top" style="padding: 6px;">
                            <asp:GridView ID="numberGrid" AutoGenerateColumns="false" CellPadding="2" CellSpacing="0" runat="server"
                                BorderWidth="0" ShowHeader="false" GridLines="None">
                                
                                <Columns>
                                    <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="certificateLink" runat="server" />
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                
                                </Columns>
                                
                            </asp:GridView>
                        </td>
                        <td valign="top"><asp:Label ID="priceLabel" runat="server" /></td>
                        <td valign="top"><asp:Label ID="qtyLabel" runat="server" /></td>
                        <td valign="top"><asp:Label ID="totalLabel" runat="server" /></td>
                    </tr>
                    
                
                </ItemTemplate>
                <FooterTemplate>
                
                
                
                </FooterTemplate>
                    
                </asp:Repeater>
                
                <tr>
                    <td align="right" colspan="4">Sub Total</td>
                    <td><asp:Label ID="subTotalLabel" runat="server" /></td>
                </tr>
                <tr>
                    <td align="right" colspan="4">Shipping</td>
                    <td>Free!</td>
                </tr>
                <asp:PlaceHolder ID="orderTotalHolder" runat="server">
                <tr>
                    <td align="right" colspan="4">Order Total</td>
                    <td><asp:Label ID="orderTotalLabel" runat="server" /></td>
                </tr>
                </asp:PlaceHolder>
            </table>
            
            
        </td>
    </tr>
</table>
     
</asp:PlaceHolder>       
            


<asp:PlaceHolder ID="notFoundHolder" Visible="false" runat="server" >

<table cellpadding="10" cellspacing="0" border="1">
    <tr>
        <td class="bigRed">
            Order Not Found
        </td>
    </tr>
</table>
</asp:PlaceHolder>

