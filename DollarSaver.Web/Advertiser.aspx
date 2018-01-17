<%@ Page Language="C#" MasterPageFile="~/consumer.master" AutoEventWireup="true" Inherits="DollarSaver.Web.AdvertiserPage" Title="DollarSaver - Advertiser" Codebehind="Advertiser.aspx.cs" %>
<%@ Register TagPrefix="DollarSaver" TagName="CertificateDetail" Src="~/controls/CertificateDetail.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



<table cellpadding="0" cellspacing="0" border="0" width="100%">
    <tr>
        <td align="center">

            <asp:PlaceHolder ID="advertiserHolder" runat="server">

            <table cellpadding="10" cellspacing="0" border="0" width="95%">
                    
                <tr>
                    <td valign="top" align="left">
                        <table cellpadding="5" cellspacing="0" border="0">
                            <tr>
                                <td valign="top" align="left" width="135">
                                    
                                    <div style=" text-align: center; padding-bottom: 5px;">
                                        <asp:Image ID="advertiserImage" BorderWidth="1" BorderColor="#404040" runat="server" />
                                    </div>

                                    <div style="padding: 5px; width:90%;">
                                        <fb:like href='<asp:Literal ID="advertiserUrlLiteral" runat="server" />' send="false" layout="button_count" width="120" show_faces="false" action="recommend" font="arial"></fb:like>
                                    </div>
                                    <div style="padding: 5px;">
                                        <a href="https://twitter.com/share" class="twitter-share-button" data-url='<asp:Literal ID="twitterAdvertiserUrlLiteral" runat="server" />' data-text='<asp:Literal ID="twitterTextLiteral" runat="server" />' data-count="none">Tweet</a>
                                        <script>!function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0]; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = "//platform.twitter.com/widgets.js"; fjs.parentNode.insertBefore(js, fjs); } }(document, "script", "twitter-wjs");</script>
                                    </div>
                                </td>
                                <td valign="top" align="left" style="padding: 2px;">
                                    <table cellpadding="3" cellspacing="0" border="0">
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="advertiserNameLabel" CssClass="headingOne" runat="server" /> &nbsp; 
                                                <asp:HyperLink ID="viewWebsiteLink" Text="View Item / Website" CssClass="blue_link" Target="_blank" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="medium_text">
                                                <asp:Label ID="addressLabel" runat="server" /> &nbsp;
                                                <asp:HyperLink ID="mapLink" Text="View Map" CssClass="blue_link" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="medium_text">
                                                <asp:Label ID="phoneLabel" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" class="p_text">
                                                <asp:Label ID="advertiserDescriptionLabel" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    
                    </td>
                
                </tr>
                <asp:Repeater ID="certificateRepeater" runat="server">
                    <HeaderTemplate>

                    </HeaderTemplate>
                    <ItemTemplate>
                            <tr>
                                <td>
                                    <DollarSaver:CertificateDetail ID="certificateDetail" runat="server" /> 
                                </td>
                            
                            </tr>

                    </ItemTemplate>

                    <FooterTemplate>
                    </FooterTemplate>
                </asp:Repeater>

                <asp:PlaceHolder ID="certNotFoundHolder" Visible="false" runat="server" >
                <tr>
                    <td class="bigRed">
                        Certificate Not Found
                    </td>
                </tr>
                </asp:PlaceHolder>
                    
            </table>
            </asp:PlaceHolder>

        </td>
    </tr>
</table>


</asp:Content>