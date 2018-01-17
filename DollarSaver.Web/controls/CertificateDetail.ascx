<%@ Control Language="C#" AutoEventWireup="true" Inherits="DollarSaver.Web.controls.CertificateDetail" Codebehind="CertificateDetail.ascx.cs" %>
<%@ Register Src="~/controls/Name.ascx" TagPrefix="DollarSaver" TagName="Name" %>

<table cellpadding="5" cellspacing="0" border="0" width="100%" style="border-top: solid 2px #D0D0D0; border-bottom: solid 2px #D0D0D0;">
    <asp:PlaceHolder ID="certHolder" runat="server" >
    <tr>
        <td valign="top" align="left">
            <table cellpadding="0" cellspacing="0" border="0" width="100%" id="mainTable" runat="server">
                <tr>
                    <td valign="top" align="left" style="width: 50%;">
                        <table cellpadding="5" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td align="left">
                                    <table cellpadding="5" cellspacing="0" border="0">
                                        <tr>
                                            <td class="bigRed">
                                                <asp:Label ID="topSavingsLabel" runat="server" /><br />OFF
                                            </td>
                                            <td>
                                                <asp:Label ID="nameLabel" CssClass="heading_two" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="p_text">
                                    <asp:Label ID="descriptionLabel" runat="server" />
                                </td>
                            </tr>
                            <asp:PlaceHolder ID="shippingHolder" runat="server">
                            <tr>
                                <td align="center">
                                    <table cellpadding="10" cellspacing="0" border="0" class="message" style="width: 100%;" >
                                        <tr>
                                            <td align="left">
                                                Certificate cannot be printed at your computer and will be mailed. <br />
                                                <br />
                                                <span style="font-weight: bold;">These are NON-PRINTABLE certificates and will be mailed to you on the first business day following your date of purchase. This program is not responsible for delivery dates and times of the US Postal Service.</span>           
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            </asp:PlaceHolder>
                            <asp:PlaceHolder ID="pickUpHolder" runat="server">
                            <tr>
                                <td align="center">
                                    <table cellpadding="10" cellspacing="0" border="0" class="message" style="width: 100%;" >
                                        <tr>
                                            <td align="left">
                                                Certificate cannot be printed at your computer and must be picked up.<br />
                                                <br />
                                                <asp:Label ID="deliveryNoteLabel" Font-Bold="true" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            </asp:PlaceHolder>
                           
                        </table>
                    </td>
                    <td align="right" valign="top" style="width: 50%;">
                        <table cellpadding="5" cellspacing="0" border="0" style="margin-top: 30px;">
                            <tr>
                                <td style="padding: 0px;" align="right">
                                    <table cellpadding="3" cellspacing="0" border="0">
                                        <asp:PlaceHolder ID="minPurchaseQtyHolder" runat="server">
                                        <tr>
                                            <td align="right">
                                                Minimum Purchase Quantity:
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="minPurchaseQtyLabel" runat="server" />
                                            </td>
                                        </tr>
                                        </asp:PlaceHolder>
                                        <tr>
                                            <td align="right">
                                                Quantity Remaining:
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="quantityLabel" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Certificate Value: 
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="valueLabel" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="font-size: 18px;">
                                                <DollarSaver:Name ID="Name1" runat="server" /> Price: 
                                            </td>
                                            <td align="right" style="font-size: 18px;">
                                                <asp:Label ID="priceLabel" CssClass="bigRed" Font-Bold="true" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <b>Your Savings:</b>
                                            </td>
                                            <td align="right">
                                                <b><asp:Label ID="savingsLabel" Font-Bold="true" runat="server" /></b>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        
                        <asp:PlaceHolder ID="addToCartHolder" runat="server">
                            <tr>
                                <td align="right">
                                    <table cellpadding="5" cellspacing="0" border="0">
                                        <tr>
                                            <td nowrap align="center">
                                                Quantity: <asp:DropDownList ID="qtyDropDown" CssClass="other_input" runat="server" />
                                                <asp:HiddenField ID="certificateIdHidden" runat="server" />
                                                <asp:HiddenField ID="stationIdHidden" runat="server" />
                                            </td>
                                            <td align="center">
                                            <!--
                                                <asp:Button ID="addToCartButtonx" Text="ADD TO CART" runat="server" />
                                                -->
                                                <asp:ImageButton ID="addToCartButton" ImageUrl="~/images/button_addtocart.gif" AlternateText="Add To Cart" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                
                            </tr>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder ID="notYetOnSaleHolder" runat="server">
                            <tr>
                                <td align="center" nowrap>
                                    On Sale <asp:Label ID="onSaleDateLabel" runat="server" />
                                </td>
                            </tr>
                        </asp:PlaceHolder>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    
            
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="certNotFoundHolder" Visible="false" runat="server" >
    <tr>
        <td class="bigRed">
            Certificate Not Found
        </td>
    </tr>
    </asp:PlaceHolder>

</table>