<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/consumer.master" CodeBehind="TestSoldOut.aspx.cs" Inherits="DollarSaver.Web.TestSoldOut" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:TextBox ID="certificateIdBox" runat="server" />

    <asp:Button ID="sendSoldoutNoticeButton" Text="Send Soldout Notice" runat="server" />

</asp:Content>