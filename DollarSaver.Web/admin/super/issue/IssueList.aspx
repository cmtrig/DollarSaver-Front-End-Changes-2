<%@ Page Language="C#" MasterPageFile="~/admin/super/super.master" AutoEventWireup="true" Inherits="DollarSaver.Web.Admin.Super.IssueAdmin.IssueList" Title="DollarSaver - Issues" Codebehind="IssueList.aspx.cs" %>
<%@ MasterType VirtualPath="~/admin/super/super.master" %>
<%@ Register Src="~/controls/CalendarBox.ascx" TagPrefix="DollarSaver" TagName="CalendarBox" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table cellpadding="10" cellspacing="0" border="0">
    <tr>
        <td class="form_header" align="left" id="headerCell" runat="server">
            <asp:Label ID="headerLabel" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="cap" align="left">
            <asp:HyperLink ID="listLink" runat="server" />
        </td>
    </tr>
    <asp:PlaceHolder ID="issueHolder" runat="server">
    <tr>
        <td style="padding: 0px;">
      

            <asp:GridView ID="issueGrid" runat="server" CellPadding="5" AutoGenerateColumns="false" EnableViewState="false"
                AllowPaging="true" PageSize="25" PagerSettings-Mode="NumericFirstLast" PagerSettings-Position="TopAndBottom" PagerStyle-HorizontalAlign="right"
                CssClass="admin_form" PagerStyle-CssClass="pager_alt">

                <RowStyle VerticalAlign="top" />
                <AlternatingRowStyle VerticalAlign="top" />
                <Columns>
                
                    <asp:HyperLinkField HeaderText="Issue #" DataTextField="IssueId" DataNavigateUrlFormatString="~/admin/super/issue/IssueView.aspx?id={0}" DataNavigateUrlFields="IssueId, StationId" ItemStyle-HorizontalAlign="Center" ControlStyle-CssClass="order_link" HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Station" DataField="StationShortName" ItemStyle-HorizontalAlign="Left"  HeaderStyle-HorizontalAlign="Left"/>
                    <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="statusLabel" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Date" DataField="IssueDate" DataFormatString="{0:MM/dd/yyyy hh:mm:ss tt}" HtmlEncode="false"  ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField HeaderText="Customer" DataField="FullName" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" />
                    
                </Columns>

            </asp:GridView>  
            
        </td>
    </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="noDataFoundHolder" runat="server">
    <tr>
        <td class="outlined" align="left">
            No Issues Found
        </td>
    </tr>
    </asp:PlaceHolder>
</table>






</asp:Content>

