<%@ Page Language="C#"  AutoEventWireup="true" Inherits="DollarSaver.Web.Sites" Title="DollarSaver - Sites" Codebehind="Sites.aspx.cs" %>
<%@ Register TagPrefix="DollarSaver" TagName="Name" Src="~/controls/Name.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>DollarSaver - Sites</title>
<link id="CSS3StyleSheet" rel="stylesheet" href="~/styles/theme.css" type="text/css" runat="server" />
<link id="dsStyleSheet" rel="stylesheet" type="text/css" href="~/styles/custom.css" runat="server" />
<link id="stationStyleSheet" rel="stylesheet" type="text/css" href="~/styles/station.css" runat="server" />
</head>
<body>
<form id="mainForm" runat="server">

    <div class="tm-block-toolbar" data-uk-sticky="{media: 767}">
       <div class="uk-container uk-container-center">
         <div class="tm-toolbar-container">
             <div class="uk-hidden-small">
             <asp:Image ID="Image5" ImageUrl="~/images/ds_logo_header.gif" AlternateText="DollarSaver" runat="server" /> <asp:Image ID="Image6" ImageUrl="~/images/local.gif" AlternateText="DollarSaver" runat="server"  />
             </div>
             <div class="uk-align-center uk-visible-small">
             <asp:Image ID="Image3" ImageUrl="~/images/ds_logo_header.gif" AlternateText="DollarSaver" runat="server" />
             <asp:Image ID="Image4" ImageUrl="~/images/local.gif" AlternateText="DollarSaver" runat="server"  />
            </div> 
         </div>
     </div>
   </div>

   <div class="uk-container uk-container-center">
    <div id="tm-main" class="tm-block-main">
    <div class="uk-margin-large">
      
     <main id="tm-content" class="tm-content">
        <h1 class="uk-h1">Please select your local <DollarSaver:Name runat="server" /> site</h1>
       <div class="uk-margin-left">
            <asp:PlaceHolder ID="messageHolder" runat="server">
             <div class="uk-panel uk-align-center">
                <table width="80%" cellpadding="5" cellspacing="0" class="message">
                 <tr>
                 <td align="center"><asp:Label ID="messageLabel" runat="server" /></td>
                 </tr>
                 </table>
       </div>
       </asp:PlaceHolder>
       <asp:PlaceHolder ID="errorMessageHolder" runat="server">
       <div class="uk-panel uk-align-center">
       <table width="80%" cellpadding="5" cellspacing="0" class="error_message">
       <tr>
       <td align="center"><asp:Label ID="errorMessageLabel" runat="server" /></td>
       </tr>
      </table>
      </div>
       </asp:PlaceHolder>
     </div>

    
       <div class="uk-container uk-container-center">
        <div class="uk-column-medium-1-2">
        <asp:Repeater ID="stationRepeater" runat="server">
        <HeaderTemplate>
        </HeaderTemplate>
            
         <ItemTemplate>
        <div class="uk-margin">
	      <table cellpadding="5" cellspacing="0" border="0">
          <tr>
            <td valign="middle" align="center" style="width: 130px;">
           <asp:HyperLink ID="logoLink" runat="server">
            <asp:Image ID="logoImage" Width="125" Height="75" BorderWidth="1" BorderColor="#404040" runat="server" /> 
            </asp:HyperLink>
            </td> 
            <td valign="middle" align="left">
          <asp:HyperLink ID="stationLink" CssClass="uk-h3" runat="server" /><br />
          <asp:Label ID="addressLabel" runat="server" />
           </td>
           </tr>
         </table>
         </div>
        </ItemTemplate>
        
          <FooterTemplate>
          </FooterTemplate>
          </asp:Repeater>
        </div>
        </div>
      <div class="uk-container uk-container-center uk-text-center uk-text-small uk-margin-top">
      &copy; <DollarSaver:Name ID="Name1" runat="server" /> 2006 - <asp:Label ID="endYearLabel" runat="server" /> Rights Reserved
      </div>
    </main> 
    </div>
   </div>
  </div>
</form>
</body>
</html>
