<%@ Page Language="C#" MasterPageFile="~/checkout.master" AutoEventWireup="true" Inherits="DollarSaver.Web.CheckoutPage" Title="DollarSaver - Checkout" Codebehind="Checkout.aspx.cs" %>
<%@ Register TagPrefix="DollarSaver" TagName="LineItemDetail" Src="~/controls/LineItemDetail.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div class="uk-align-center">
   <ul class="uk-grid uk-align-center">
	 <li class="uk-width-1-4 heading_two_gray">Your Cart</li>
     <li class="uk-width-1-4 heading_two_gray"> E-mail Info</li>
     <li class="uk-width-1-4 heading_two">Payment Info</li>
     <li class="uk-width-1-4 heading_two_gray">Print Certificates</li>
	</ul>
    <hr />
    <div class="uk-align-center" style="max-width: 600px;">
    <table cellpadding="5" cellspacing="0" border="0" width="100%">
    <tr>
    <td class="heading_two" align="left"> Your Cart
        </td>
         </tr>
         <tr>
       <td align="left" style="padding: 0px;">
       <DollarSaver:LineItemDetail ID="lineItemDetail" runat="server" />
       </td>
       </tr>
    </table>
    </div>
     <div class="uk-align-center" style="max-width: 600px;">      
     <table cellpadding="5" cellspacing="0" border="0" class="ds_table" width="100%">
                            <tr>
                                <td class="heading_two greenback" align="left">
                                    Email Address
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="customerEmailLabel" runat="server" /> 
                                </td>
                            </tr>
            </table>
    </div>
    <div class="uk-align-center" style="max-width: 600px;">
       <asp:PlaceHolder ID="shippingHolder" runat="server">
                        <table cellpadding="5" cellspacing="0" border="0" class="ds_table" width="100%">
                            <tr>
                                <td class="heading_two greenback" align="left">
                                    Shipping Address
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="shippingAddressLabel" runat="server" /> 
                                </td>
                            </tr>
                        </table>
                </asp:PlaceHolder>
    </div>
    <div class="uk-align-center" style="max-width: 600px;">
     <table cellpadding="5" cellspacing="0" border="0" class="ds_table" width="100%">
                            <tr>
                                <td colspan="2" class="heading_two greenback" align="left">
                                    Payment Information
                                </td>
                            </tr>
                            <tr>
                                <td class="form_field" nowrap width="200px;">First Name:</td>
                                <td class="form_value">
                                    <asp:TextBox ID="firstNameBox" MaxLength="50" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="firstNameBox" Text="* Required" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="form_field" nowrap>Last Name:</td>
                                <td class="form_value">
                                    <asp:TextBox ID="lastNameBox" MaxLength="50" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="lastNameBox" Text="* Required" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="form_field" nowrap>Card Type:</td>
                                <td class="form_value">
                                    <asp:DropDownList ID="creditCardList" CssClass="other_input" runat="server">
                                        <asp:ListItem Text="Visa" Value="Visa" Selected />
                                        <asp:ListItem Text="MasterCard" Value="MasterCard" />
                                        <asp:ListItem Text="Discover" Value="Discover" />
                                        <asp:ListItem Text="American Express" Value="Amex" />
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="form_field" nowrap>Card Number:</td>
                                <td class="form_value">
                                    <asp:TextBox ID="creditCardNumberBox" Columns="20" MaxLength="20" runat="server" />
                                    <asp:RequiredFieldValidator ControlToValidate="creditCardNumberBox" Text="* Required" runat="server" />    
                                </td>
                            </tr>
                            <tr>
                                <td class="form_field" nowrap>Expiration Date:</td>
                                <td class="form_value">
                                    <asp:DropDownList ID="expirationMonthList" CssClass="other_input" runat="server">
                                        <asp:ListItem Value="01" Text="JAN (01)" Selected="true" />
                                        <asp:ListItem Value="02" Text="FEB (02)" />
                                        <asp:ListItem Value="03" Text="MAR (03)" />
                                        <asp:ListItem Value="04" Text="APR (04)" />
                                        <asp:ListItem Value="05" Text="MAY (05)" />
                                        <asp:ListItem Value="06" Text="JUN (06)" />
                                        <asp:ListItem Value="07" Text="JUL (07)" />
                                        <asp:ListItem Value="08" Text="AUG (08)" />
                                        <asp:ListItem Value="09" Text="SEP (09)" />
                                        <asp:ListItem Value="10" Text="OCT (10)" />
                                        <asp:ListItem Value="11" Text="NOV (11)" />
                                        <asp:ListItem Value="12" Text="DEC (12)" />
                                    </asp:DropDownList> /
                                    <asp:DropDownList ID="expirationYearList" CssClass="other_input" runat="server" />
                                 </td>
                            </tr>
                            <tr>
                                <td class="form_field" nowrap>Card Verification Number:</td>
                                <td class="form_value">
                                    <asp:TextBox ID="verificationNumberBox" Columns="5" MaxLength="4" runat="server" />
                                    <asp:RequiredFieldValidator ControlToValidate="verificationNumberBox" Text="* Required" runat="server" />    
                                </td>
                            </tr>
                        </table>
    </div>
    <div class="uk-align-center" style="max-width: 600px;">
     <table cellpadding="5" cellspacing="0" border="0" class="ds_table" width="100%">
                            <tr>
                                <td colspan="2" class="heading_two greenback" align="left">
                                    Billing Address
                                </td>
                            </tr>
                            <tr>
                                <td class="form_field" nowrap width="200px;">Address 1:</td>
                                <td class="form_value">
                                    <asp:TextBox ID="address1Box" Columns="35" MaxLength="200" runat="server" />
                                    <asp:RequiredFieldValidator ControlToValidate="address1Box" Text="* Required" runat="server" />    
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
                                    <asp:RequiredFieldValidator ControlToValidate="cityBox" Text="* Required" runat="server" />    
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
                                    <asp:RequiredFieldValidator ControlToValidate="zipCodeBox" Text="* Required" runat="server" />    
                                </td>
                            </tr>
                            <tr>
                                <td class="form_field" nowrap>Phone Number:</td>
                                <td class="form_value">
                                    <asp:TextBox ID="phoneNumberBox" MaxLength="50" runat="server" />
                                    <span class="small_text">(XXX) XXX-XXXX</span>
                                    <asp:RequiredFieldValidator ControlToValidate="phoneNumberBox" Text="* Required" runat="server" />    
                                </td>
                            </tr>
                        </table>
    </div>
    <div class="uk-align-center uk-alert uk-alert-danger" style="max-width: 600px;">                 
      Your purchase will be listed as <strong>"DollarSaver 2072299178"</strong>on your credit card statement
    </div>
    <div class="uk-text-center uk-margin-bottom">
     <asp:Button ID="placeOrderButton" CssClass="uk-button uk-button-primary" Text="Place Order" runat="server" />
    </div>
   </div>
</asp:Content>
