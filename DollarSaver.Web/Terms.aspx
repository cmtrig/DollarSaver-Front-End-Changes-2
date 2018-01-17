<%@ Page Language="C#" MasterPageFile="~/consumer.master" AutoEventWireup="true" Inherits="DollarSaver.Web.Terms" Title="DollarSaver - Terms of Agreement" 
    Codebehind="Terms.aspx.cs" EnableViewState="false" %>
<%@ Register TagPrefix="DollarSaver" TagName="TermsText" Src="~/controls/TermsText.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      
<table cellpadding="0" cellspacing="0" border="0" width="100%">
    <tr>
        <td align="center">
            <DollarSaver:TermsText ID="termsText" runat="server" />
        </td>
    </tr>
</table>



</asp:Content>

