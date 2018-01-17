<%@ Page Language="C#" MasterPageFile="~/admin/super/super.master" AutoEventWireup="true" Title="DollarSaver - Run Report" Codebehind="RunReport.aspx.cs" Inherits="DollarSaver.Web.Admin.Super.Reports.RunReport" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<input type="hidden" id="idHidden" runat="server" />
<asp:Label id="statusMessageLabel" CssClass="statusMessage" runat="server"/>
<table cellpadding="4" cellspacing="0" border="0" width="100%">
	<tr>
		<td>
			<table cellpadding="3" cellspacing="0" border="0" width="100%">
				<tr>
					<td class="heading_one" align="left">
						<asp:Label id="reportHeaderLabel" runat="server" />
					</td>
				</tr>
				<tr>
					<td align="left">
						<asp:PlaceHolder id="runDateHolder" runat="Server">
							<span style="font-weight: bold;">Report Date:</span> <asp:Label id="runDateLabel" runat="Server" />
						</asp:PlaceHolder>
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<asp:PlaceHolder id="parameterHolder" runat="server" >
	<tr>
				
		<td style="padding: 0px;" valign="top">
		    <br />
			<table cellpadding="10" cellspacing="0" border="0" class="admin_form">
				<tr>
					<td class="form_header" colspan="2" align="left">Report Parameters</td>
				</tr>
		
			<asp:Repeater id="parameterRepeater" runat="server">
				<HeaderTemplate>
				</HeaderTemplate>
				<ItemTemplate>
					<tr>
						<td nowrap style="padding-left: 5px;" class="form_field" valign="top">
							<asp:Label id="paramNameLabel" runat="server" />
						</td>
						<td align="left" style="padding-right: 10px;" class="form_value">
							<table cellpadding="0" cellspacing="0" border="0">
								<tr>
									<td class="form_value">
										<asp:HiddenField  id="paramIdHidden" runat="server" />
										<asp:ListBox id="paramList" SelectionMode="Multiple" rows="5" visible="false" CssClass="other_input" runat="server" />
										<asp:HiddenField ID="paramValueHidden" runat="server" />
										<asp:Label ID="paramValueLabel" Visible="false" runat="server" />
										<asp:TextBox id="paramValueBox" visible="false" runat="server" />
										<asp:CheckBox id="paramCheckBox" visible="false" runat="server" />
										<asp:Image id="calendarImage" visible="false" runat="server" />
										<asp:PlaceHolder id="timeHolder" visible="false" runat="server">
										&nbsp;
										<asp:DropDownList id="hourList" CssClass="other_input" runat="server" /> :
										<asp:DropDownList id="minuteList" CssClass="other_input" runat="server" />
										</asp:PlaceHolder>
									</td>
									<td valign="top" style="padding-left: 10px; padding-top: 4px; font-size: 12px; color: #6060E0;">
										<asp:Label id="paramTypeLabel" runat="server" />
									</td>
									<td valign="top" style="padding-left: 6px;">
										<asp:PlaceHolder id="validatorHolder" runat="server" />
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</ItemTemplate>
				<FooterTemplate>
				</FooterTemplate>
			
			</asp:Repeater>
		
				<tr>
					<td class="form_field">Format</td>
					<td style="padding: 0px;">
						<table cellpadding="4" cellspacing="0" border="0">
							<tr>
								<td>
									<asp:RadioButton checked="true" OnClick="javascript: toggleBox('delimiterDiv', 0)" GroupName="formatRadioButton" id="htmlRadioButton" value="html" runat="server"  /> Web 
								</td>
								<td>
									<asp:RadioButton OnClick="javascript: toggleBox('delimiterDiv', 0)" GroupName="formatRadioButton" id="excelRadioButton"  value="excel" runat="server"  /> Excel
								</td>
								<td>
									<asp:RadioButton OnClick="javascript: toggleBox('delimiterDiv', 1)" GroupName="formatRadioButton" id="delimitedRadioButton"  value="delimited" runat="server"  /> Delimited
								</td>
								<td>
<script type="text/javascript">
<!--
    
function toggleBox(szDivID, iState) // 1 visible, 0 hidden
{
	//try {
		if(document.layers)	   //NN4+
		{
			if(document.layers[szDivID] != null) {
				document.layers[szDivID].visibility = iState ? "show" : "hide";
			}
		}
		else if(document.getElementById)	  //gecko(NN6) + IE 5+
		{
			var obj = document.getElementById(szDivID);
			if(obj != null) {
				obj.style.visibility = iState ? "visible" : "hidden";
			}
		}
		else if(document.all)	// IE 4
		{
			if(document.all[szDivID] != null) {
				document.all[szDivID].style.visibility = iState ? "visible" : "hidden";
			}
		}
    //} catch(e) {}
}
// -->

</script>


									<div id="delimiterDiv" name="delimiterDiv">
									<asp:DropDownList id="delimiterList" runat="server">
										<asp:ListItem value="pipe">Pipe |</asp:ListItem>
										<asp:ListItem value="comma">Comma ,</asp:ListItem>
										<asp:ListItem value="colon">Colon :</asp:ListItem>
										<asp:ListItem value="semicolon">Semicolon ;</asp:ListItem>
										<asp:ListItem value="space">Space</asp:ListItem>
										<asp:ListItem value="tab">Tab</asp:ListItem>
									</asp:DropDownList>
									</div>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="2" class="form_footer" align="right">
						<asp:Button id="runButton" Text="Run Report" runat="server" /> &nbsp;
						<asp:Button id="cancelButton" Text="Cancel" CausesValidation="false" runat="server" />
					</td>
				</tr>
		
		
			</table>
		
		
		</td>
	
	</tr>
	</asp:PlaceHolder>
	<asp:PlaceHolder id="linkHolder" runat="server">
	<tr>
		<td style="padding: 11px;" align="left">
			<span class="noprint">
			<asp:HyperLink id="reRunReportLink" Text="Re-run Report" NavigateUrl="javascript: history.go(-1);" runat="server" /> &nbsp;
			<asp:HyperLink id="reportListLink" Text="Report List" runat="server" />
	        </span>
		</td>
	</tr>
	</asp:PlaceHolder>
	
	<tr>
		<td>
			<asp:PlaceHolder id="resultHolder" runat="server">
				<table cellpadding="5" cellspacing="0" border="0" width="700px">
					<tr>
						<td align="left">
							<asp:Repeater id="runParamRepeater" runat="Server">
								<HeaderTemplate>
									<table cellpadding="2" cellspacing="0" border="0">
								</HeaderTemplate>
								<ItemTemplate>
										<tr>
											<td class="boldText reportCell" valign="top" align="left" nowrap>
												<asp:Label ID="parameterNameLabel" runat="server" />:
											</td>
											<td class="reportCell" align="left">
												<asp:Label ID="parameterValueLabel" runat="server" />
											</td>
										</tr>
								</ItemTemplate>
								<FooterTemplate>
									</table>
								</FooterTemplate>
							</asp:Repeater>
						</td>
					</tr>
					<tr>
						<td>&nbsp;</td>
					</tr>
					<tr>
						<td>
							<asp:Label id="resultsLabel" runat="server" />
						</td>
					</tr>
				</table>
			
			</asp:PlaceHolder>
		</td>
	</tr>
	
</table>


<asp:PlaceHolder id="toggleHolder" runat="server">
<script type="text/javascript">
<!--

//initially hide the div
toggleBox('delimiterDiv', 0)

// -->

</script>
</asp:PlaceHolder>

</asp:Content>