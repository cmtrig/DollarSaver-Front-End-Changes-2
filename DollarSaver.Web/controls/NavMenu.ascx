<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NavMenu.ascx.cs" Inherits="DollarSaver.Web.controls.NavMenu" %>
           <nav class="navback uk-flex uk-flex-center nav-text-bold"> 
                <asp:Menu ID="navMenu" Orientation="Horizontal" DynamicVerticalOffset="6" SkipLinkText="" runat="server">
                <StaticMenuStyle VerticalPadding="8" />
                <StaticMenuItemStyle HorizontalPadding="6" CssClass="uk-parent" />
                <DynamicMenuStyle BorderWidth="1" BorderColor="#A0A0A0" CssClass="uk-dropdown uk-dropdown-navbar uk-dropdown-center uk-dropdown-bottom" />
                <DynamicMenuItemStyle VerticalPadding="5" HorizontalPadding="5" />
            </asp:Menu>
            </nav>
