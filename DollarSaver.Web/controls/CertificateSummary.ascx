<%@ Control Language="C#" AutoEventWireup="true" Inherits="DollarSaver.Web.controls.CertificateSummary" Codebehind="CertificateSummary.ascx.cs" %>
<%@ Register Src="~/controls/Name.ascx" TagPrefix="DollarSaver" TagName="Name" %>
<asp:PlaceHolder ID="certHolder" runat="server" >
<table class="uk-align-center sumtable" cellpadding="5" cellspacing="0" border="0" style="border: solid 5px #F0F0F0; width: 93%;" id="mainTable" runat="server" >
    <tr>
        <td valign="top">
            <table cellpadding="5" cellspacing="0" border="0">
                <tr>
                    <td style="padding: 0px;">
                        <table cellpadding="5" cellspacing="0" border="0" style="width: 100%;">
                            <tr>
                                <td style="width: 135px; text-align: center;">
                                    <asp:HyperLink ID="advertiserImageLink" runat="server" >
                                    <asp:Image ID="advertiserImage" BorderWidth="1" BorderColor="#404040" runat="server" />
                                    </asp:HyperLink>
                                </td>
                                <td align="left" valign="middle">
                                    <asp:HyperLink ID="nameLink" CssClass="big_link" runat="server" /><br />
                                    <asp:HyperLink ID="categoryLink" CssClass="small_link" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="medium_text">
                        <asp:Label ID="descriptionLabel" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right" style="padding: 0px;">
                        <table cellpadding="3" cellspacing="0" border="0" style="width: 100%;">
                            <tr>
                                <td align="center" class="biggerRed">
                                    <asp:Label ID="savingsLabel" runat="server" /><br />OFF
                                </td>
                                <td style="padding: 0px;">
                                <table cellpadding="3" cellspacing="0" border="0">
                                    <tr>
                                        <td align="right" class="medium_text">
                                            Quantity Remaining: 
                                        </td>
                                        <td align="right" class="medium_text">
                                            <asp:Label ID="qtyRemainingLabel" runat="server" />
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td align="right" class="medium_text">
                                            Certificate Value:
                                        </td>
                                        <td align="right" class="medium_text">
                                            <asp:Label ID="valueLabel" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <DollarSaver:Name ID="Name1" runat="Server" /> Price:
                                        </td>
                                        <td align="right" class="biggerRed">
                                            <asp:Label ID="discountLabel" Font-Size="20px" Font-Bold="true" ForeColor="Red" runat="server" />
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
    </tr>
    <tr>
        <td align="right" style="padding-right: 10px; padding-bottom: 10px; padding-top: 0px;">
            <table cellpadding="5" cellspacing="0" border="0" width="100%">
                <tr>
                    <td align="left" nowrap>
                        <asp:HyperLink ID="advertiserLink" runat="server">
                            More Info
                        </asp:HyperLink>
                    </td>
                    <asp:PlaceHolder ID="addToCartHolder" runat="server">
                    <td align="right" style="padding: 0px;">
                        <table cellpadding="5" cellspacing="0" border="0">
                            <tr>
                                <td align="right">
                                    Quantity: <asp:DropDownList ID="qtyDropDown" CssClass="other_input" runat="server" />
                                    <asp:HiddenField ID="certificateIdHidden" runat="server" />
                                    <asp:HiddenField ID="stationIdHidden" runat="server" />
                                </td>
                                <td align="left">
                                <!--
                                    <asp:Button ID="addToCartButtonz" Text="ADD TO CART" runat="server" />
                                -->
                                    <asp:ImageButton ID="addToCartButton" ImageUrl="~/images/BUTTON_AddToCart.gif" AlternateText="Add To Cart" runat="server" />
                                    
                                </td>
                            </tr>
                        </table>
                    </td>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="notYetOnSaleHolder" runat="server">
                    <td align="right">
                        On Sale <asp:Label ID="onSaleDateLabel" runat="server" />
                    </td>
                    </asp:PlaceHolder>
                </tr>
                <tr>
                    <td colspan="2">
                    
                        <div style="position: relative; float: left; padding: 2px;">
                            <a href="https://twitter.com/share" class="twitter-share-button" data-url='<asp:Literal ID="twitterAdvertiserUrlLiteral" runat="server" />' data-text='<asp:Literal ID="twitterTextLiteral" runat="server" />' data-count="none">Tweet</a>
                            <script>!function(d, s, id) { var js, fjs = d.getElementsByTagName(s)[0]; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = "//platform.twitter.com/widgets.js"; fjs.parentNode.insertBefore(js, fjs); } } (document, "script", "twitter-wjs");</script>
                        </div>
                        <div style="position: relative; float: left; padding: 2px;">
                            <fb:like href='<asp:Literal ID="advertiserUrlLiteral" runat="server" />' send="false" layout="button_count" width="120" show_faces="false" action="recommend" font="arial"></fb:like>
                        </div>
                        
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>

</asp:PlaceHolder>
<asp:PlaceHolder ID="certNotFoundHolder" Visible="false" runat="server" >
<table cellpadding="5" cellspacing="0" border="0" width="370px" style="border: solid 5px #F0F0F0;" >
    <tr>
        <td class="bigRed" align="left">
            Certificate Not Found
        </td>
    </tr>
</table>
    
</asp:PlaceHolder>