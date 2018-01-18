<%@ Page Language="C#" MasterPageFile="~/consumer.master" AutoEventWireup="true" Inherits="DollarSaver.Web.AdvertiserPage" Title="DollarSaver - Advertiser" Codebehind="Advertiser.aspx.cs" %>
<%@ Register TagPrefix="DollarSaver" TagName="CertificateDetail" Src="~/controls/CertificateDetail.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:PlaceHolder ID="advertiserHolder" runat="server">

<div class="uk-grid uk-grid-medium">
 <div class="uk-width-medium-2-10 uk-align-center">
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
 </div>
 <div class="uk-width-medium-8-10 uk-align-center">
    <asp:Label ID="advertiserNameLabel" CssClass="headingOne" runat="server" /> &nbsp; 
    <asp:HyperLink ID="viewWebsiteLink" Text="View Item / Website" CssClass="blue_link" Target="_blank" runat="server" />
    <div class="medium_text">
    <asp:Label ID="addressLabel" runat="server" /> &nbsp;
    <asp:HyperLink ID="mapLink" Text="View Map" CssClass="blue_link" runat="server" />
    </div>
       <div class="medium_text">
        <asp:Label ID="phoneLabel" runat="server" />
      </div>
    <div class="p_text">
    <asp:Label ID="advertiserDescriptionLabel" runat="server" />
    </div>
   </div>
   <div class="uk-width-1-1 uk-align-center">        
    <asp:Repeater ID="certificateRepeater" runat="server">
    <HeaderTemplate>
    </HeaderTemplate>
    <ItemTemplate>
    <div class="uk-panel uk-panel-box uk-panel-box-secondary"><DollarSaver:CertificateDetail ID="certificateDetail" runat="server" /> </div>
    </ItemTemplate>
    <FooterTemplate>
    </FooterTemplate>
    </asp:Repeater>
    <asp:PlaceHolder ID="certNotFoundHolder" Visible="false" runat="server" >
    <div class="bigRed"> Certificate Not Found </div>
    </asp:PlaceHolder>
    </div> 
   </div>               
</asp:PlaceHolder>
</asp:Content>