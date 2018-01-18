<%@ Page Language="C#" MasterPageFile="~/consumer.master" AutoEventWireup="true" Inherits="DollarSaver.Web.Terms" Title="DollarSaver - Terms of Agreement" 
    Codebehind="Terms.aspx.cs" EnableViewState="false" %>
<%@ Register TagPrefix="DollarSaver" TagName="TermsText" Src="~/controls/TermsText.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      
<div class="uk-margin-large page-padding">
               <h1 class="uk-h2">Terms of Agreement</h1>
            <DollarSaver:TermsText ID="termsText" runat="server" />
        
</div>



</asp:Content>

