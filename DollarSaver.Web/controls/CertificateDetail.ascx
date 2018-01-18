<%@ Control Language="C#" AutoEventWireup="true" Inherits="DollarSaver.Web.controls.CertificateDetail" Codebehind="CertificateDetail.ascx.cs" %>
<%@ Register Src="~/controls/Name.ascx" TagPrefix="DollarSaver" TagName="Name" %>
 <asp:PlaceHolder ID="certHolder" runat="server">
 <div class="uk-grid uk-grid-divider">
  <div class="uk-width-medium-1-2 uk-align-center">
        <div class="bigRed">
         <asp:Label ID="topSavingsLabel" runat="server" /> OFF
         </div>
        <asp:Label ID="nameLabel" CssClass="heading_two" runat="server" />
          <div class="p_text">
     <asp:Label ID="descriptionLabel" runat="server" />
    </div>
    <asp:PlaceHolder ID="shippingHolder" runat="server">
   <div class="uk-margin-small">
       <table cellpadding="10" cellspacing="0" border="0" class="message" style="width: 100%;" >
       <tr>
      <td>
          Certificate cannot be printed at your computer and will be mailed. <br />
         <br />
        <span style="font-weight: bold;">These are NON-PRINTABLE certificates and will be mailed to you on the first business day following your date of purchase. This program is not responsible for delivery dates and times of the US Postal Service.</span>           
        </td>
     </tr>
   </table>
   </div>
  </asp:PlaceHolder>
  <asp:PlaceHolder ID="pickUpHolder" runat="server">
                               <div class="uk-align-center">
                                    <table cellpadding="10" cellspacing="0" border="0" class="message" style="width: 100%;" >
                                        <tr>
                                            <td align="left">
                                                Certificate cannot be printed at your computer and must be picked up.<br />
                                                <br />
                                                <asp:Label ID="deliveryNoteLabel" Font-Bold="true" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
  </asp:PlaceHolder>
   </div>
   <div class="uk-width-medium-1-2 uk-align-center">
      <div class="uk-margin-top uk-align-right">
                        <asp:PlaceHolder ID="minPurchaseQtyHolder" runat="server">
                          Minimum Purchase Quantity: <asp:Label ID="minPurchaseQtyLabel" runat="server" />
                        </asp:PlaceHolder>
                              <div class="uk-align-right">
                                                Quantity Remaining: <asp:Label ID="quantityLabel" runat="server" />
                                            <br />
                                                Certificate Value: <asp:Label ID="valueLabel" runat="server" />
                                            <br />
                                            <span class="uk-font-large">
                                                <DollarSaver:Name ID="Name1" runat="server" /> Price: <asp:Label ID="priceLabel" CssClass="bigRed" Font-Bold="true" runat="server" />
                                            </span>
                                            <br />
                                                <b>Your Savings: <asp:Label ID="savingsLabel" Font-Bold="true" runat="server" /></b>
                                            </div>
                                            <div class="uk-align-right">
                                            <asp:PlaceHolder ID="addToCartHolder" runat="server" >
                                             <div class="uk-margin-small uk-align-center">
                                                Quantity: <asp:DropDownList ID="qtyDropDown" CssClass="other_input" runat="server" />
                                                <asp:HiddenField ID="certificateIdHidden" runat="server" />
                                                <asp:HiddenField ID="stationIdHidden" runat="server" />
                                             </div>
                                            <div class="uk-align-center">
                                            <!--
                                                <asp:Button ID="addToCartButtonx" Text="ADD TO CART" runat="server" />
                                                -->
                                                <asp:ImageButton ID="addToCartButton" ImageUrl="~/images/button_addtocart.gif" AlternateText="Add To Cart" runat="server" />
                                            </div>
                                         </asp:PlaceHolder>
                                            </div>
       </div> 
   </div>
   <div class="uk-width-1-1"> 
                    <asp:PlaceHolder ID="notYetOnSaleHolder" runat="server">
                                <div class="uk-align-center">
                                    On Sale <asp:Label ID="onSaleDateLabel" runat="server" />
                               </div>
                        </asp:PlaceHolder>
    </div>
 </div>
    <asp:PlaceHolder ID="certNotFoundHolder" Visible="false" runat="server" >
    <div class="uk-align-center">
        <div class="bigRed">
            Certificate Not Found
        </div>
    </div>
   </asp:PlaceHolder>  
    </asp:PlaceHolder>