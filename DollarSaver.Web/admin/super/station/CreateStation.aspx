<%@ Page Language="C#" MasterPageFile="~/admin/super/super.master" AutoEventWireup="true" Inherits="DollarSaver.Web.Admin.Super.StationAdmin.CreateStation" Title="DollarSaver - Create Station" Codebehind="CreateStation.aspx.cs" ValidateRequest="false"  %>
<%@ MasterType VirtualPath="~/admin/admin.master" %>
<%@ Register Src="~/controls/StationControl.ascx" TagPrefix="DollarSaver" TagName="StationControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<DollarSaver:StationControl ID="stationControl" runat="server" />

</asp:Content>

