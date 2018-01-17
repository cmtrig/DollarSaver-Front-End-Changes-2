<%@ Page Language="C#" MasterPageFile="~/checkout.master" AutoEventWireup="true" Inherits="DollarSaver.Web.Confirmation" Title="Dollar Saver - Confirmation" Codebehind="Confirmation.aspx.cs" %>
<%@ Register TagPrefix="DollarSaver" TagName="LineItemDetail" Src="~/controls/LineItemDetail.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="uk-align-center">   
    <ul class="uk-grid uk-align-center">
	 <li class="uk-width-1-4 heading_two_gray">Your Cart</li>
     <li class="uk-width-1-4 heading_two_gray"> E-mail Info</li>
     <li class="uk-width-1-4 heading_two_gray">Payment Info</li>
     <li class="uk-width-1-4 heading_two">Print Certificates</li>
	</ul>
    <hr />

 <asp:PlaceHolder ID="printableHolder" runat="server">
<h1 class="uk-h2"> Print Your Certificates</h1>
                <table cellpadding="5" cellspacing="0" border="0" class="cart">
                            <asp:Repeater ID="certificateRepeater" runat="server">
                            <ItemTemplate>

                                <asp:Repeater ID="numberRepeater" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td align="left" width="400px">
                                            <asp:Label ID="certificateNameLabel" runat="server" />
                                            #<asp:Label ID="numberLabel" runat="server" />
                                        </td>
                                        <td align="left">
                                            <asp:HyperLink ID="certificateLink" Text="View & Print" CssClass="certificate_link" runat="server" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                </asp:Repeater>
                                       
                            </ItemTemplate>
                            </asp:Repeater>
                        </table>
          
    </asp:PlaceHolder>
    
     <asp:PlaceHolder ID="pickUpHolder" runat="server">
   
            <div class="uk-width-medium-1-2 uk-container-center">
                <h2 class="uk-h2">Certificates That Must Be Picked Up</h2>
                  <table cellpadding="5" cellspacing="0" border="0" class="ds_table">
                            <asp:Repeater ID="pickUpRepeater" runat="server">
                            <HeaderTemplate>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td align="left" width="400px">
                                        <asp:Label ID="certificateNameLabel" runat="server" /><br />
                                        <asp:Label ID="deliveryNoteLabel" Font-Bold="true" runat="server" />
                                    </td>
                                    <td align="right">Quantity: <asp:Label ID="qtyLabel" runat="server" /></td>
                                </tr>
                                
                            
                            </ItemTemplate>
                            <FooterTemplate>
                            
                            
                            
                            </FooterTemplate>
                                
                            </asp:Repeater>
                  </table>
                    
            </div>
        
    </asp:PlaceHolder>
                
    <asp:PlaceHolder ID="shippingHolder" runat="server">
    
           <div class="uk-width-medium-1-2 uk-container-center">
              <h2 class="uk-h2"> Certificates That Will Be Mailed </h2>
                <table cellpadding="5" cellspacing="0" border="0" class="ds_table">
                            <asp:Repeater ID="shippingRepeater" runat="server">
                            <HeaderTemplate>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td align="left" width="400px">
                                        <asp:Label ID="certificateNameLabel" runat="server" />
                                    </td>
                                    <td align="right">Quantity: <asp:Label ID="qtyLabel" runat="server" /></td>
                                </tr>
                                
                            
                            </ItemTemplate>
                            <FooterTemplate>
                            
                            
                            
                            </FooterTemplate>
                                
                            </asp:Repeater>
                        </table>
           </div>         
             <div class="uk-width-medium-1-2 uk-container-center">
                 <h2 class="uk-h2">
                        Shipping Address
                    </h2>
               
                        <asp:Label ID="shippingInfoLabel" runat="server" />
                    
             </div>
       
    
    </asp:PlaceHolder>
    <div class="uk-width-medium-1-2 uk-container-center">
                <h2 class="uk-h2"> Order Confirmation #<asp:Label ID="orderNumberLabel" runat="server" /></h2>
                      <DollarSaver:LineItemDetail ID="lineItemDetail" runat="server" />
   </div>

 </div>

</asp:Content>
