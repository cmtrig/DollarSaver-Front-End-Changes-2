<%@ Page Language="C#"  AutoEventWireup="true" Inherits="DollarSaver.Web.Sites" Title="DollarSaver - Sites" Codebehind="Sites.aspx.cs" %>
<%@ Register TagPrefix="DollarSaver" TagName="Name" Src="~/controls/Name.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>DollarSaver - Sites</title>

<link id="dsStyleSheet" rel="stylesheet" type="text/css" href="~/styles/dollarsaver.css" runat="server" />
<link id="stationStyleSheet" rel="stylesheet" type="text/css" href="~/styles/station.css" runat="server" />

</head>

<body>

<form id="mainForm" runat="server">

<table cellpadding="0" cellspacing="0" border="0" width="100%">
    <tr>
        <td align="center">
        
            <table cellpadding="0" cellspacing="0" border="0" style="background-color: #FEFEFA; border-left: solid 1px #A0A0A0; border-right: solid 1px #A0A0A0; border-bottom: solid 1px #A0A0A0;" width="800">
                <tr>
                    <td align="center">

                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td>
                                    <table cellpadding="10" cellspacing="0" border="0">
                                        <tr>
                                            <td valign="middle" align="left">
                                                <asp:Image ID="topperImage" ImageUrl="~/images/ds_logo_header.gif" AlternateText="DollarSaver" runat="server" />
                                            </td>
                                            <td valign="middle">
                                                 <asp:Image ID="localSavingsImage" ImageUrl="~/images/local.gif" AlternateText="DollarSaver" runat="server"  />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="padding: 10px; border-top: solid 1px #808080;">

                                    <table cellpadding="5" cellspacing="0" border="0" width="100%">
                                        <asp:PlaceHolder ID="messageHolder" runat="server">
                                        <tr>
                                            <td style="10px" align="center">
                                                <table width="80%" cellpadding="5" cellspacing="0" class="message">
                                                    <tr>
                                                        <td align="center"><asp:Label ID="messageLabel" runat="server" /></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        </asp:PlaceHolder>
                                        <asp:PlaceHolder ID="errorMessageHolder" runat="server">
                                        <tr>
                                            <td style="10px" align="center">
                                                <table width="80%" cellpadding="5" cellspacing="0" class="error_message">
                                                    <tr>
                                                        <td align="center"><asp:Label ID="errorMessageLabel" runat="server" /></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        </asp:PlaceHolder>
                                        <tr>
                                            <td align="left">
                                                <span class="headingOne">Please select your local <DollarSaver:Name runat="server" /> site</span>
                                            </td>
                                        </tr>
                                            
                                        <asp:Repeater ID="stationRepeater" runat="server">
                                        <HeaderTemplate>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td align="left">
                                                    <table cellpadding="5" cellspacing="0" border="0">
                                                        <tr>
                                                            <td valign="middle" align="center" style="width: 130px;">
                                                                <asp:HyperLink ID="logoLink" runat="server">
                                                                <asp:Image ID="logoImage" Width="125" Height="75" BorderWidth="1" BorderColor="#404040" runat="server" /> 
                                                                </asp:HyperLink>
                                                            </td>
                                                            <td valign="middle" align="left">
                                                                <asp:HyperLink ID="stationLink" Style="font-size: 18px;" runat="server" /><br />
                                                                <asp:Label ID="addressLabel" runat="server" />
                                                            </td>
                                                        </tr>
                                                    
                                                    </table>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        </FooterTemplate>
                                            
                                        </asp:Repeater>
                                            
                                        <tr>
                                            <td align="center" style="padding: 10px; font-size: 10px; color: #6A9BE0;">
                                                &copy; <DollarSaver:Name ID="Name1" runat="server" /> 2006 - <asp:Label ID="endYearLabel" runat="server" /> Rights Reserved
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
</table>

</form>

</body>

</html>
