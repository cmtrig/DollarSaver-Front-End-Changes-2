<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TermsPopup.aspx.cs" Inherits="DollarSaver.Web.TermsPopup" %>
<%@ Register TagPrefix="DollarSaver" TagName="TermsText" Src="~/controls/TermsText.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Terms of Agreement</title>
<link rel="Stylesheet" type="text/css" href="/styles/theme.css.css" />
<link rel="Stylesheet" type="text/css" href="/styles/custom.css" />
<link rel="Stylesheet" type="text/css" href="/styles/station.css" />

</head>
<body>
    <form id="form1" runat="server">

        <div class="uk-margin-large page-padding">
               <h1 class="uk-h2">Terms of Agreement</h1>
            <DollarSaver:TermsText ID="termsText" runat="server" />
        
        </div>


    </form>
</body>
</html>
