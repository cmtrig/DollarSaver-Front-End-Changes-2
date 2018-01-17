<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CalendarBox.ascx.cs" Inherits="DollarSaver.Web.controls.CalendarBox" %>
<asp:TextBox ID="dateBox" Columns="12" MaxLength="10" runat="server" />
<asp:Image id="calendarImage" ImageUrl="~/images/calendar_button.gif" runat="server" />
<asp:RequiredFieldValidator ID="dateBoxRFV" ControlToValidate="dateBox" Text="*" runat="server" />
<asp:RegularExpressionValidator ID="dateBoxREV" ControlToValidate="dateBox" Text="*"
    ValidationExpression="(0?[1-9]|1[012])[- /.](0?[1-9]|[12][0-9]|3[01])[- /.](19|20|21)\d\d" runat="server" />