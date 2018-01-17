<%@ Page Language="C#" MasterPageFile="~/consumer.master" AutoEventWireup="true" Inherits="DollarSaver.Web.Certificate" Title="DollarSaver - Advertiser" Codebehind="Certificate.aspx.cs" %>
<%@ Register TagPrefix="DollarSaver" TagName="CertificateDetail" Src="~/controls/CertificateDetail.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">







<div class="uk-panel">
    
            <DollarSaver:CertificateDetail ID="certificateDetail" runat="server" /> 
        
</div>





</asp:Content>

