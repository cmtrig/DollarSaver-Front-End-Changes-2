<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" Inherits="DollarSaver.Web.Admin.StationContent" Title="DollarSaver - Edit Custom Images" Codebehind="StationContent.aspx.cs" %>
<%@ MasterType VirtualPath="~/admin/admin.master" %>
<%@ Register Src="~/controls/StationContentControl.ascx" TagPrefix="DollarSaver" TagName="StationControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<DollarSaver:StationControl ID="stationContentControl" runat="server" />


</asp:Content>

