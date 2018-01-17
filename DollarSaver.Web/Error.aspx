<%@ Page Language="C#" AutoEventWireup="true" Inherits="DollarSaver.Web.Error" Title="DollarSaver - Error" Codebehind="Error.aspx.cs" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title>DollarSaver - Error!</title>
<link rel="Stylesheet" type="text/css" href="/styles/theme.css.css" />
<link rel="Stylesheet" type="text/css" href="/styles/custom.css" />
<link rel="Stylesheet" type="text/css" href="/styles/station.css" />

</head>

<body>

<form id="mainForm" runat="server">

<table cellpadding="0" cellspacing="0" border="0" width="100%">
    <tr>
        <td align="center">
        
            <table cellpadding="20" cellspacing="0" border="0" style="background-color: #FEFEFA; border-left: solid 1px #A0A0A0; border-right: solid 1px #A0A0A0; border-bottom: solid 1px #A0A0A0;" width="600">
                <tr>
                    <td align="center">
                    
                        <img src="/images/ds_logo_header.gif" alt="Dollar Saver - Error!" />
                    
                    </td>
                
                </tr>
                <tr>
                    <td align="center" style="padding: 10px;">
                        <table cellpadding="10" cellspacing="0" border="0" width="480">
                            <tr>
                                <td align="left" style="font-size: 18px;">That's an error.</td>
                            </tr>
                            <tr>
                                <td align="left"><asp:Label id="errorLabel" Text="Its not the end of the world, though. Try finding what you need on the hoem page." CssClass="bigRed" runat="server" /></td>
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

