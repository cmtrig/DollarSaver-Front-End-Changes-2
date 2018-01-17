<%@ Page Language="C#" MasterPageFile="~/consumer.master" AutoEventWireup="true" Inherits="DollarSaver.Web.CartPage" Title="DollarSaver - Cart" Codebehind="Cart.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<div class="uk-align-center">
   <div class="uk-grid">
	 <div class="uk-width-1-4 heading_two">&#8226; Your Cart</div>
     <div class="uk-width-1-4 heading_two_gray">&#8226; E-mail Info</div>
     <div class="uk-width-1-4 heading_two_gray">&#8226; Payment Info</div>
     <div class="uk-width-1-4 heading_two_gray">&#8226; Print Your Certificates</div>
	</div>
           
    <asp:PlaceHolder ID="cookieMessageHolder" runat="server">
      <table cellpadding="7" width="90%" border="0" style="border: solid 1px #FF4040; background-color: #FFFE90;">
	            <tr>
	                <td style="font-size: 14px; color: #DF4040;" align="left">
	                    Please enable cookies in your web browser to ensure the site will work properly.
	                </td>
	            </tr>
	            <tr>
	                <td style="font-size: 14px; color: #DF4040;" align="left">
	                    Refer to you web browsers help section for instructions on enabling cookies
	                </td>
	            </tr>
	            <tr>
	                <td style="font-size: 14px; color: #DF4040;" align="left">
	                    Once you have enabled cookies, <asp:HyperLink ID="homeLink" NavigateUrl="~/Default.aspx" runat="server" style="color: #DF4040; text-decoration: underline;">please click here to continue shopping</asp:HyperLink>
	                </td>
	            </tr>
	        </table>
       
    </asp:PlaceHolder>
   
	<div class="uk-panel">
                <asp:PlaceHolder ID="itemHolder" runat="server" >
               
                        <table cellpadding="5" cellspacing="0" border="0" width="100%" class="cart">
                            <tr>
                                <td class="greenback">&nbsp;</td>
                                <td align="left" style="font-weight: bold;" class="greenback">Certificates</td>
                                <td align="center" style="font-weight: bold;" class="greenback">Price</td>
                                <td align="center" style="font-weight: bold;" class="greenback">Qty</td>
                                <td align="center" style="font-weight: bold;" class="greenback">Total</td>
                            </tr>
                            <asp:Repeater ID="lineItemRepeater" runat="server">
                            <HeaderTemplate>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr> <!-- E8F0E0; -->
                                    <td align="center">
                                        <asp:LinkButton ID="removeButton" CommandName="remove" Text="Remove" Style="font-size: 12px;" runat="server" />
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="certificateNameLabel" runat="server" />
                                        <asp:HiddenField ID="lineItemIdHidden" runat="server" />
                                    </td>
                                    <td align="right"><asp:Label ID="priceLabel" runat="server" /></td>
                                    <td align="center"><asp:TextBox ID="qtyBox" Columns="2" style="text-align: right" runat="server" /></td>
                                    <td align="right"><asp:Label ID="totalLabel" runat="server" /></td>
                                </tr>
                                
                            
                            </ItemTemplate>
                            <FooterTemplate>
                            
                            
                            
                            </FooterTemplate>
                                
                            </asp:Repeater>
                            
                            <tr>
                                <td colspan="3" align="right"></td>
                                <td align="center"><asp:Button ID="updateButton" Text="Update" CssClass="small_button" runat="server" /></td>
                                <td></td>
                            </tr>
                            <!--
                            <tr>
                                <td colspan="4" align="right" style="font-weight: bold;">Estimated Shipping</td>
                                <td align="right">Free!</td>
                            </tr>
                            -->
                            <tr>
                                <td colspan="5" align="right" style="font-weight: bold;" class="greenback">Order Total: <asp:Label ID="subTotalLabel" runat="server" /></td>
                            </tr>
                        </table>
                    </div>
                
                    <div class="uk-align-right uk-margin-top">
                        <table cellpadding="5" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td align="left" valign="bottom">
                                    <asp:HyperLink ID="continueShoppingLink" Text="Continue Shopping" runat="server" />
                                </td>
                                <td align="right" valign="top">
                                    <asp:Button ID="continueButton" Text="Proceed to Checkout" style="font-weight: bold;" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td align="right" style="padding-top: 50px;">
                                    <asp:Image AlternateText="Visa, MasterCard, AmEx, Discover & PayPal" ImageUrl="~/images/payment_logos.gif" BorderWidth="0" runat="server" />
                                </td>
                            </tr>
                        </table>
		</div>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="emptyCartHolder" runat="server">
                <div class="uk-margin-large-bottom">
                        <em>Your cart is empty. Start shopping!</em>
                    </div>    
                
                
                </asp:PlaceHolder> 
            </div>

</asp:Content>
