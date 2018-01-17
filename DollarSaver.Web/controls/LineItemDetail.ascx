<%@ Control Language="C#" AutoEventWireup="true" Inherits="DollarSaver.Web.controls.LineItemDetail" Codebehind="LineItemDetail.ascx.cs" %>
<%@ Register Src="~/controls/Name.ascx" TagPrefix="DollarSaver" TagName="Name" %>
        
<asp:PlaceHolder ID="lineItemHolder" runat="server" >
    
<table cellpadding="5" cellspacing="0" border="0" class="cart" width="600px">
    <tr>
        <td style="font-weight: bold;" class="greenback" width="300px" align="left">Certificates</td>
        <td style="font-weight: bold;" class="greenback" width="100px" align="center">Price</td>
        <td style="font-weight: bold;" class="greenback" width="100px" align="center">Qty</td>
        <td style="font-weight: bold;" class="greenback" width="100px" align="center">Total</td>
    </tr>
    <asp:Repeater ID="lineItemRepeater" runat="server">
    <HeaderTemplate>
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <td align="left">
                <asp:Label ID="certificateNameLabel" runat="server" />
                <asp:PlaceHolder ID="nonPrintableHolder" runat="server">
                    <br />     
                    <asp:Label ID="nonPrintableLabel" Text="" CssClass="small_text" Style="color: #F02020;" runat="server" />
                </asp:PlaceHolder>
            </td>
            <td align="right"><asp:Label ID="priceLabel" runat="server" /></td>
            <td align="center"><asp:Label ID="qtyLabel" runat="server" /></td>
            <td align="right"><asp:Label ID="totalLabel" runat="server" /></td>
        </tr>
        
    
    </ItemTemplate>
    <FooterTemplate>
    
    
    
    </FooterTemplate>
        
    </asp:Repeater>
    
    <tr>
        <td colspan="4" align="right" style="font-weight: bold;" class="greenback">Order Total: <asp:Label ID="subTotalLabel" runat="server" /></td>
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

