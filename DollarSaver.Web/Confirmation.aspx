<%@ Page Language="C#" MasterPageFile="~/checkout.master" AutoEventWireup="true" Inherits="DollarSaver.Web.Confirmation" Title="Dollar Saver - Confirmation" Codebehind="Confirmation.aspx.cs" %>
<%@ Register TagPrefix="DollarSaver" TagName="LineItemDetail" Src="~/controls/LineItemDetail.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="uk-align-center">   
    <div class="uk-grid">
	 <div class="uk-width-1-4 heading_two uk-text-center">&#8226; Your Cart</div>
     <div class="uk-width-1-4 heading_two_gray uk-text-center">&#8226; E-mail Info</div>
     <div class="uk-width-1-4 heading_two_gray uk-text-center">&#8226; Payment Info</div>
     <div class="uk-width-1-4 heading_two_gray uk-text-center">&#8226; Print Your Certificates</div>
	</div>

 <asp:PlaceHolder ID="printableHolder" runat="server">

        <table cellpadding="5" cellspacing="0" border="0" class="uk-width-medium-1-2 uk-container-center">
                <tr>
                    <td class="headingOne" align="left">
                        Print Your Certificates
                    </td>
                </tr>
                <tr>
                    <td align="left">
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
   
            <table cellpadding="5" cellspacing="0" border="0" class="uk-width-medium-1-2 uk-container-center">
                <tr>
                    <td class="headingOne" align="left">
                        Certificates That Must Be Picked Up
                    </td>
                </tr>
                <tr>
                    <td align="left">
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
                    </td>
                </tr>
            </table>
        
    </asp:PlaceHolder>
                
    <asp:PlaceHolder ID="shippingHolder" runat="server">
    
            <table cellpadding="5" cellspacing="0" border="0" class="uk-width-medium-1-2 uk-container-center">
                <tr>
                    <td class="headingOne" align="left">
                        Certificates That Will Be Mailed
                    </td>
                </tr>
                <tr>
                    <td align="left">
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
                    </td>
                </tr>
                <tr>
                    <td class="headingOne" align="left">
                        Shipping Address
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="shippingInfoLabel" runat="server" />
                    </td>
                </tr>
            </table>
            
       
    
    </asp:PlaceHolder>
                <table cellpadding="5" cellspacing="0" border="0" class="uk-width-medium-1-2 uk-container-center">
                <tr>
                    <td class="headingOne" align="left">
                        Order Confirmation #<asp:Label ID="orderNumberLabel" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <DollarSaver:LineItemDetail ID="lineItemDetail" runat="server" />
                    </td>
                
                </tr>
                
            </table>

 </div>

</asp:Content>
