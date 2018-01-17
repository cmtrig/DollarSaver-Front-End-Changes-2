<%@ Page Language="C#" MasterPageFile="~/admin/super/super.master" AutoEventWireup="true" Title="DollarSaver - Reports" Codebehind="Default.aspx.cs" Inherits="DollarSaver.Web.Admin.Super.Reports.Default" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table cellpadding="10" cellspacing="0" border="0" class="admin_form">
	<tr>
		<td colspan="2" class="form_header" align="left">
			Reports
		</td>
	</tr>
	<tr>
		<td align="left">
			<table cellpadding="4" cellspacing="0" border="0">

			<asp:Repeater id="reportTypeRepeater" runat="server">
			<HeaderTemplate>
				<tr>
					<td valign="top">
				        <table cellpadding="5" cellspacing="0" border="0">
			</HeaderTemplate>
			<ItemTemplate>
					        <tr>
						        <td colspan="2" class="heading_three"><asp:Label ID="reportTypeNameLabel" runat="server" /></td>
					        </tr>
        						
				            <asp:Repeater id="reportRepeater" runat="server">
				            <ItemTemplate>
					        <tr>
						        <td valign="top" style="padding-left: 15px;"><b><asp:HyperLink id="runReportLink" Text="Run" runat="server" /></b></td>
						        <td valign="top"><asp:Label id="reportLabel" runat="server" />
					        </tr>
				            </ItemTemplate>
				            </asp:Repeater>
			</ItemTemplate>
			<FooterTemplate>
				        </table>
					</td>
				</tr>
			</FooterTemplate>
			</asp:Repeater>
					

			</table>
		</td>
	</tr>
</table>


</asp:Content>