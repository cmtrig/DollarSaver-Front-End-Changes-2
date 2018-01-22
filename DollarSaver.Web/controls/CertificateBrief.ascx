<%@ Control Language="C#" AutoEventWireup="true" Inherits="DollarSaver.Web.controls.CertificateBrief" Codebehind="CertificateBrief.ascx.cs" %>
<%@ Register Src="~/controls/Name.ascx" TagPrefix="DollarSaver" TagName="Name" %>

<asp:PlaceHolder ID="certHolder" runat="server" >
<div class="uk-margin-large">
     <hr class="uk-grid">
<div class="uk-grid uk-grid-collapse uk-grid-divider">
  <div class="uk-width-medium-1-4 uk-text-center">
    <div class="uk-container uk-container-center">
   
     <div class="uk-margin-top-small">
                    <div class="uk-align-center biggerRed">
                        <asp:Label ID="savingsLabel" runat="server" /> OFF
                    </div>
                    <div style="text-align: center; padding: 5px;">
                            <asp:HyperLink ID="advertiserImageLink" runat="server">
                                <asp:Image ID="advertiserImage" BorderWidth="1" BorderColor="#404040" runat="server" />
                            </asp:HyperLink>
                        </div>

                        <div style="padding: 5px; width:90%; height: 20px;">
                            <fb:like href='<asp:Literal ID="advertiserUrlLiteral" runat="server" />' send="false" layout="button_count" width="120" show_faces="false" action="recommend" font="arial"></fb:like>
                        </div>
                        
                        <div style="padding: 5px; height: 20px;">
                            <a href="https://twitter.com/share" class="twitter-share-button" data-url='<asp:Literal ID="twitterAdvertiserUrlLiteral" runat="server" />' data-text='<asp:Literal ID="twitterTextLiteral" runat="server" />' data-count="none">Tweet</a>
                            <script>!function(d, s, id) { var js, fjs = d.getElementsByTagName(s)[0]; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = "//platform.twitter.com/widgets.js"; fjs.parentNode.insertBefore(js, fjs); } } (document, "script", "twitter-wjs");</script>
                        </div>
        </div>
       </div>
       </div>
        <div class="uk-width-medium-3-4">
              <table cellpadding="0" cellspacing="0" border="0" id="mainTable" width="100%" runat="server">
                <tr>
                    <td align="left" valign="bottom">
                     
                        <table cellpadding="0" cellspacing="0" border="0" runat="server" width="100%">
                            <tr>
                                <td align="left">
                                    <asp:HyperLink ID="advertiserNameLink" runat="server" CssClass="bigger_link" /><br />
                                    <asp:Label ID="cityLabel" runat="server" /> 
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 0px;">
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td>
                                                            Quantity Remaining: <asp:Label ID="quantityLabel" runat="server" /><br />
                                                            Certificate Value: <asp:Label ID="valueLabel" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="font-size: 18px;">
                                                            <DollarSaver:Name ID="Name1" runat="server" /> Price: <asp:Label ID="discountLabel" CssClass="bigRed" Font-Bold="true" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" valign="bottom" nowrap>
                                                            <asp:HyperLink ID="advertiserLink" Font-Bold="true" runat="server">
                                                                More Info
                                                            </asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </table>
                                            
                                            </td>
                                            <td valign="bottom" align="right">
                                            
                                                <table cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td align="left" valign="bottom" style="padding: 0px;">
                                                            <asp:PlaceHolder ID="addToCartHolder" runat="server">
                                                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                <tr>
                                                                    <td align="center" nowrap>
                                                                        Quantity: <asp:DropDownList ID="qtyDropDown" CssClass="other_input" runat="server" />
                                                                        <asp:HiddenField ID="certificateIdHidden" runat="server" />
                                                                        <asp:HiddenField ID="stationIdHidden" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="bottom" >
                                                                    <!--
                                                                        <asp:Button ID="addToCartButtony" Text="ADD TO CART" runat="server" />
                                                                        --> 
                                                                        <asp:ImageButton ID="addToCartButton" ImageUrl="~/images/add-to-cart.png" AlternateText="Add To Cart" runat="server" />
                                                                       
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            </asp:PlaceHolder>
                                                            <asp:PlaceHolder ID="notYetOnSaleHolder" runat="server">
                                                            <table cellpadding="5" cellspacing="0" border="0" width="100%">
                                                                <tr>
                                                                    <td align="center" nowrap>
                                                                        On Sale <asp:Label ID="onSaleDateLabel" runat="server" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            </asp:PlaceHolder>
                                                        </td>
                                                    </tr>
                                                </table>
                                            
                                            </td>
                                        </tr>
                                    </table>
                                
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td align="right" valign="bottom">
                    </td>
                </tr>
            
            </table>

        </div>
  
    </div>
   <hr class="uk-grid">
      </div>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="certNotFoundHolder" Visible="false" runat="server" >
    <div class="bigRed">
            Certificate Not Found
        </div>
    
    </asp:PlaceHolder>
