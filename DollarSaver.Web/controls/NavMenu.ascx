<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NavMenu.ascx.cs" Inherits="DollarSaver.Web.controls.NavMenu" %>
           <nav class="navback uk-flex uk-flex-center"> 
           <asp:Menu ID="navMenu" Orientation="Horizontal" DynamicVerticalOffset="6" SkipLinkText="" runat="server">
                <StaticMenuStyle VerticalPadding="6" />
                <StaticMenuItemStyle HorizontalPadding="6" CssClass="nav_menu"  />
                <DynamicMenuStyle BorderWidth="1" BorderColor="#A0A0A0" CssClass="dynamic_menu" />
                <DynamicMenuItemStyle VerticalPadding="4" HorizontalPadding="5" CssClass="nav_menu"  />
               
            </asp:Menu> 
            </nav>


