<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="" Inherits="DollarSaver.Web.Default" Title="DollarSaver" Codebehind="Default.aspx.cs" %>
<%@ Register TagPrefix="DollarSaver" TagName="CertificateSummary" Src="~/controls/CertificateSummary.ascx" %>
<%@ Register TagPrefix="DollarSaver" TagName="DealOfTheWeek" Src="~/controls/DealOfTheWeek.ascx" %>
<%@ Register TagPrefix="DollarSaver" TagName="Name" Src="~/controls/Name.ascx" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:PlaceHolder ID="standardHolder" runat="server">
   
<div class="uk-grid uk-grid-medium">
	<div class="uk-width-medium-1-2 uk-align-center">
	<div class="uk-margin-large">
   <div class="signup_table">
       <div class="uk-text-center" style="background-image:url(<%=ResolveUrl("~/images/topSignupBG.gif")%>);">
                                    Be the first to know about new deals -<br />
                                    <asp:HyperLink ID="signUpTopLink" NavigateUrl="~/MailingListSignUp.aspx" Text="Sign Up To Our E-Mail List!" runat="server"/>
        </div>
   </div>  
                          
   <table cellpadding="10" cellspacing="0" border="0" style="padding-left: 25px; width: 100%; background-image:url(<%=ResolveUrl("~/images/1-2-3-BG.gif")%>); background-repeat:no-repeat">
                            <tr>
                                <td colspan="2" align="left" style="font-family: Georgia, Serif; font-size: 20px; font-weight: bold; padding-top: 35px; padding-bottom: 25px;" class="station_text">Saving Up to 50% is as easy as 1-2-3!</td>
                            </tr>
                            <tr>
                                <td align="left" style="font-family: Trebuchet MS; font-size: 24px; font-weight: bold;" class="station_text">1</td>
                                <td align="left" style="font-family: Trebuchet MS; font-size: 16px; border-top: solid 1px #D0D0D0;" class="station_text">Pick and click a category!</td>
                            </tr>
                            <tr>
                                <td align="left" style="font-family: Trebuchet MS; font-size: 24px; font-weight: bold;" class="station_text">2</td>
                                <td align="left" style="font-family: Trebuchet MS; font-size: 16px; border-top: solid 1px #D0D0D0;" class="station_text">Browse the category and click the business where you want to save!</td>
                            </tr>
                            <tr>
                                <td align="left" style="font-family: Trebuchet MS; font-size: 24px; font-weight: bold;" class="station_text">3</td>
                                <td align="left" style="font-family: Trebuchet MS; font-size: 16px; border-top: solid 1px #D0D0D0;" class="station_text">Purchase certificates and<br />print them instantly at your printer!</td>
                            </tr>
                            <tr>
                                <td align="left" style="font-family: Trebuchet MS; font-size: 24px; font-weight: bold;" class="station_text">&nbsp;</td>
                                <td align="left" style="font-family: Trebuchet MS; font-size: 16px; border-top: solid 1px #D0D0D0;" class="station_text">&nbsp;</td>
                            </tr>
                        
   </table>
        </div>
	</div>
    <div class="uk-width-medium-1-2 uk-align-center">
        
   <table cellpadding="0" cellspacing="0" border="0" style="width: 105%;">
                            <tr>
                                <td align="center">
                                    <asp:PlaceHolder ID="dailyWeeklyImageHolder" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <DollarSaver:CertificateSummary ID="SpecialCert1" runat="server" />
                                </td>
                            </tr>
    </table>
	</div>
 </div>   
   
 <div class="uk-grid uk-margin-top-remove">
	<div class="uk-width-1-1">
       <asp:Image ID="otherDealsImage" ImageUrl="~/images/b/other_deal.jpg" CssClass="uk-align-center" AlternateText="Other Great DollarSaver Deals" runat="server" />
    </div>
    <div class="uk-width-medium-1-2 uk-align-center">
       <DollarSaver:CertificateSummary ID="SpecialCert2" runat="server" />
    </div>
    <div class="uk-width-medium-1-2 uk-align-center">
      <DollarSaver:CertificateSummary ID="SpecialCert3" runat="server" />
     </div>
     <div class="uk-width-medium-1-2 uk-align-center">
       <DollarSaver:CertificateSummary ID="SpecialCert4" runat="server" />
    </div>
	<div class="uk-width-medium-1-2 uk-align-center">
     <div class="heading_two">
        <DollarSaver:Name ID="Name1" runat="server" />
     </div>
     <div class="uk-align-left">
        <asp:Image ID="logoImage" ImageAlign="Left" ImageUrl="~/images/ds_small.gif" Width="125" Height="75" BorderWidth="1" BorderColor="#404040" style="float:left; margin-top:8px;margin-right:8px;" runat="server" />
        <asp:Label ID="content1Label" CssClass="p_text" runat="server" />
      </div>
    <div class="p_text">
       <asp:Label ID="content2Label" runat="server" />
    </div>
	</div>
</div>

</asp:PlaceHolder>

<asp:PlaceHolder ID="dealHolder" runat="server">
 <div class="uk-align-center uk-margin">
            <table cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td align="center" id="subheaderCell" runat="server" style="height: 40px; font-size: 20pt; font-style: italic; font-family: Verdana, Arial; font-weight: bold; background-repeat: no-repeat; background-position: center; color: #FEFEFE;">
                        <asp:Label ID="dotwStationNameLabel" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <DollarSaver:DealOfTheWeek ID="dealOfTheWeek" runat="server" />
                    </td>
                </tr>
            </table>
      </div>

</asp:PlaceHolder>

</asp:Content>
