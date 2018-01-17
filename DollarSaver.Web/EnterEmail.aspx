<%@ Page Language="C#" MasterPageFile="~/checkout.master" AutoEventWireup="true" Inherits="DollarSaver.Web.EnterEmail" Title="DollarSaver - Enter Email" Codebehind="EnterEmail.aspx.cs" %>
<%@ Register TagPrefix="DollarSaver" TagName="Name" Src="~/controls/Name.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript" language="javascript">
<!--
function agreeToTermsBoxCV_ClientValidate(sender, args)
{
args.IsValid = (document.getElementById("ctl00_ContentPlaceHolder1_agreeToTermsBox").checked);
}

function popWindow(url) {
	newWindow = window.open(url,'Terms','height=800,width=500,scrollbars=yes,screenX=100,screenY=100,top=100,left=100');
    
	if (window.focus) {
	    newWindow.focus()
	}
	return false;
}

//-->
</script>

<div class="uk-align-center">
   <ul class="uk-grid uk-align-center">
	 <li class="uk-width-1-4 heading_two_gray">Your Cart</li>
     <li class="uk-width-1-4 heading_two"> E-mail Info</li>
     <li class="uk-width-1-4 heading_two_gray">Payment Info</li>
     <li class="uk-width-1-4 heading_two_gray">Print Certificates</li>
	</ul>
    <hr />
  <div class="uk-align-center" style="max-width: 600px;">
       <asp:PlaceHolder ID="viewAndPrintHolder" runat="server">
                <h2 class="uk-h2 uk-text-center">
                        View &amp; Print your certificates on the final checkout page
                    </h2>
               
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="noDeliveryHolder" runat="server">
                <div class="uk-text-center uk-text-success">
                    <em>No postal delivery, print your certificates directly from your computer!</em>
                </div>
                </asp:PlaceHolder>
      </div>
               <div class="uk-align-center" style="max-width: 480px;">
                   <table cellpadding="5" cellspacing="0" border="0" width="100%" class="ds_table">
                     <tr>
                        <td colspan="2" class="heading_two greenback" align="left">
                              Enter E-mail Address
                        </td>
                      </tr>
                    <tr>
                <td class="small_text" colspan="2" align="left">
                     A copy of your certificate(s) will also be sent to the following e-mail address
                   </td>
                </tr>
             <tr>
                 <td class="form_field" nowrap width="110">E-mail:</td>
               <td align="left">
                   <asp:TextBox ID="emailBox" Columns="40" MaxLength="320" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="emailBox" Text="* Required" Display="Dynamic" runat="server" /><br />
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="emailBox" Text="* Invalid E-mail Address" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic" runat="server" />   
           </td>
           </tr>
              <tr>
                                <td class="form_field" nowrap>Confirm E-mail:</td>
                                <td align="left">
                                    <asp:TextBox ID="confirmEmailBox" Columns="40" MaxLength="320" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="confirmEmailBox" Text="* Required" Display="Dynamic" runat="server" /><br />
                                    <asp:CompareValidator ID="CompareValidator1" ControlToValidate="confirmEmailBox" ControlToCompare="emailBox" Operator="Equal" Text="* Does not match E-mail" Display="Dynamic" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
          <asp:PlaceHolder ID="pickUpHolder" runat="server" >
               <div class="uk-align-center" style="max-width: 600px;">
                  <table cellpadding="15" cellspacing="0" border="0" class="message" >
                            <tr>
                                <td>
                                    Your order contains certificates that cannot be printed at your computer and must be picked up.<br />       
                                </td>
                            </tr>
                   </table>
               </div>
                <div class="uk-align-center" style="max-width: 600px;">
                        <table cellpadding="5" cellspacing="0" border="0" class="ds_table">
                        
                            <tr>
                                <td style="font-weight: bold;" class="greenback" width="300px" align="left">Certificates that must be picked up</td>
                                <td style="font-weight: bold;" class="greenback" align="center">Price</td>
                                <td style="font-weight: bold;" class="greenback" align="center">Qty</td>
                                <td style="font-weight: bold;" class="greenback" align="center">Total</td>
                            </tr>
                            <asp:Repeater ID="pickUpRepeater" runat="server">
                            <HeaderTemplate>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="certificateNameLabel" runat="server" /><br />
                                        <asp:Label ID="deliveryNoteLabel" Font-Bold="true" runat="server" />
                                    </td>
                                    <td align="right"><asp:Label ID="priceLabel" runat="server" /></td>
                                    <td align="right"><asp:Label ID="qtyLabel" runat="server" /></td>
                                    <td align="right"><asp:Label ID="totalLabel" runat="server" /></td>
                                </tr>
                                
                            
                            </ItemTemplate>
                            <FooterTemplate>
                            
                            
                            
                            </FooterTemplate>
                                
                            </asp:Repeater>
                        </table>
                </div>
            </asp:PlaceHolder>
                
              <asp:PlaceHolder ID="shippingHolder" runat="server">
               <div class="uk-align-center" style="max-width: 600px;">
                        <table cellpadding="15" cellspacing="0" border="0" class="message" >
                            <tr>
                                <td>
                                    Your order contains certificates that cannot be printed at your computer and must be mailed. Please provide your shipping address below.<br />
                                    <br />
                                    <span style="font-weight: bold;">These are NON-PRINTABLE certificates and will be mailed to you on the first business day following your date of purchase. This program is not responsible for delivery dates and times of the US Postal Service.</span>           
                                </td>
                            </tr>
                        </table>
              </div>
                <div class="uk-align-center" style="max-width: 600px;">
                        <table cellpadding="5" cellspacing="0" border="0" class="ds_table">
                        
                            <tr>
                                <td style="font-weight: bold;" class="greenback" width="300px" align="left">Certificates that will be mailed</td>
                                <td style="font-weight: bold;" class="greenback" align="center">Price</td>
                                <td style="font-weight: bold;" class="greenback" align="center">Qty</td>
                                <td style="font-weight: bold;" class="greenback" align="center">Total</td>
                            </tr>
                            <asp:Repeater ID="shippingRepeater" runat="server">
                            <HeaderTemplate>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="certificateNameLabel" runat="server" />
                                    </td>
                                    <td align="right"><asp:Label ID="priceLabel" runat="server" /></td>
                                    <td align="right"><asp:Label ID="qtyLabel" runat="server" /></td>
                                    <td align="right"><asp:Label ID="totalLabel" runat="server" /></td>
                                </tr>
                                
                            
                            </ItemTemplate>
                            <FooterTemplate>
                            
                            
                            
                            </FooterTemplate>
                                
                            </asp:Repeater>
                        </table>
                </div>
                <div class="uk-align-center" style="max-width: 600px;">
                         <table cellpadding="5" cellspacing="0" border="0" class="ds_table">
                            <tr>
                                <td colspan="2" class="heading_two greenback" align="left">
                                    Shipping Address
                                </td>
                            </tr>
                            <tr>
                                <td class="form_field" nowrap width="150">First Name:</td>
                                <td class="form_value">
                                    <asp:TextBox ID="firstNameBox" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="firstNameBox" Text=" * Required" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="form_field" nowrap>Last Name:</td>
                                <td class="form_value">
                                    <asp:TextBox ID="lastNameBox" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="lastNameBox" Text=" * Required" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="form_field" nowrap>Address 1:</td>
                                <td class="form_value">
                                    <asp:TextBox ID="address1Box" Columns="35" MaxLength="200" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="address1Box" Text=" * Required" runat="server" />    
                                </td>
                            </tr>
                            <tr>
                                <td class="form_field" nowrap>Address 2:</td>
                                <td class="form_value"><asp:TextBox ID="address2Box" Columns="35" MaxLength="200" runat="server" /></td>
                            </tr>
                            <tr>
                                <td class="form_field" nowrap>City:</td>
                                <td class="form_value">
                                    <asp:TextBox ID="cityBox" MaxLength="200" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="cityBox" Text=" * Required" runat="server" />    
                                </td>
                            </tr>
                            <tr>
                                <td class="form_field" nowrap>State:</td>
                                <td class="form_value"><asp:DropDownList ID="stateList" CssClass="other_input" runat="server" /></td>
                            </tr>
                            <tr>
                                <td class="form_field" nowrap>ZIP:</td>
                                <td class="form_value">
                                    <asp:TextBox ID="zipCodeBox" Columns="10" MaxLength="12" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="zipCodeBox" Text=" * Required" runat="server" />    
                                </td>
                            </tr>
                            <tr>
                                <td class="form_field" nowrap>Phone Number:</td>
                                <td class="form_value">
                                    <asp:TextBox ID="phoneNumberBox" MaxLength="50" runat="server" />
                                    <span class="small_text">(XXX) XXX-XXXX</span>  
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="phoneNumberBox" Text=" * Required" runat="server" /> 
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td class="form_value">
                                    <asp:CheckBox ID="sameAddressBox" Checked="true" runat="server" /> Use same address for billing
                                </td>
                            </tr>
                        </table>
                </div>
                </asp:PlaceHolder>

    <div class="uk-width-medium-1-2 uk-container-center">
        <div class="uk-panel uk-panel-box">
               <div class="uk-margin-small">
                                    <asp:CheckBox ID="rememberMeBox" runat="server" /> 
                                    Yes, remember my info on this computer
                 </div>
                 <div class="uk-margin-small">
                                    <asp:CheckBox ID="addToMailingListBox" runat="server" /> 
                                    Yes, I'd like to receive e-mails about future deals
                  </div>
                  <div class="uk-margin-top">
                                    <asp:CheckBox ID="agreeToTermsBox" runat="server" /> 
                                    I agree to the <a href="TermsPopup.aspx" onclick="return popWindow('TermsPopup.aspx')">Terms and Conditions</a>
                                    <asp:CustomValidator ID="agreeToTermsBoxCV" runat="server" Display="static" ClientValidationFunction="agreeToTermsBoxCV_ClientValidate"
                                        Text="* Required" OnServerValidate="agreeToTermsBoxCV_ServerValidate" />
                 </div>
                </div>
            
            
    <div class="uk-margin-top">
                        <table cellpadding="10" cellspacing="0" border="0">
                            <tr>
                                <td align="right" valign="middle" style="border-bottom: solid 1px #D0D0D0;">
                                    To pay by Credit Card:
                                </td>
                                <td align="left" valign="middle" style="border-bottom: solid 1px #D0D0D0;">
                                    <asp:Button ID="checkoutButton" Text="Continue to Checkout" style="font-weight: bold;" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" style="padding-top: 17px;">
						            OR with your PayPal Account:
						        </td>
						        <td align="left" valign="middle">
						            <asp:ImageButton ID="paypalButton" ImageUrl="https://www.paypal.com/en_US/i/btn/btn_xpressCheckout.gif" AlternateText="Check Out With PayPal" runat="server" />
                                </td>
                            </tr>
                        </table>
                </div>
                
                <div class="uk-align-right">
                       <asp:Image ID="Image1" AlternateText="Visa, MasterCard, AmEx, Discover & PayPal" ImageUrl="~/images/payment_logos.gif" BorderWidth="0" runat="server" />
                    </div>
 </div>              
</div>

</asp:Content>

