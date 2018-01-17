<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" Inherits="DollarSaver.Web.Admin.StationEdit" Title="DollarSaver - Edit Station" CodeBehind="StationEdit.aspx.cs" ValidateRequest="false" %>

<%@ MasterType VirtualPath="~/admin/admin.master" %>
<%@ Register Src="~/controls/StationControl.ascx" TagPrefix="DollarSaver" TagName="StationControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <DollarSaver:StationControl ID="stationControl" runat="server" />

</asp:Content>

<asp:Content ID="ScriptsContent" ContentPlaceHolderID="ScriptsPlaceHolder" runat="server">
    <script src="assets/lib/tinymce/tinymce.min.js"></script>

    <script>
        tinymce.init({
            selector: 'textarea',
            plugins: 'textcolor colorpicker',
            toolbar: ['bold italic | forecolor'],
            menubar: 'false'
        });
    </script>
</asp:Content>
