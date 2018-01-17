<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TermsPopup.aspx.cs" Inherits="DollarSaver.Web.TermsPopup" %>
<%@ Register TagPrefix="DollarSaver" TagName="TermsText" Src="~/controls/TermsText.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Terms of Agreement</title>
    
<style type="text/css">

.heading_two
{
    font-family: Verdana, Sans-Serif;
    font-size: 12px;
    font-weight: bold;
    color: #404040;
}

.p_text 
{
    font-family: Verdana, Sans-Serif;
  font-size: 10px; 
  line-height: 16px;   
}
</style>

</head>
<body>
    <form id="form1" runat="server">

        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td align="center">
                    <DollarSaver:TermsText ID="termsText" runat="server" />
                </td>
            </tr>
        </table>


    </form>
</body>
</html>
