<%@ Page Language="C#" AutoEventWireup="true" Inherits="DollarSaver.Web.ViewCertificate" Codebehind="ViewCertificate.aspx.cs" %>
<%@ Register TagPrefix="DollarSaver" TagName="Name" Src="~/controls/Name.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<title>Print Certificate</title>
    <link rel="stylesheet" href="~/styles/certificate.css" type="text/css" runat="server" />
    <link rel="stylesheet" href="~/styles/theme.css" type="text/css" runat="server" />
	<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <style type="text/css">
        .auto-style1 {
            width: 74px;
            height: 75px;
        }
    </style>
</head> 
<body>
   <div class="uk-visible-small uk-text-center uk-text-warning">
       This page best viewed in Landscape Mode 

   <img alt="Best viewed in Landscape mode" class="auto-style1" src="images/mobile-landscape_orientation-128.png" /></div>   
<table cellpadding="10" cellspacing="0" border="0" style="background-color: #FFFFFF;" width="100%">
    <tr>
        <td align="center">
        <asp:PlaceHolder ID="certificateHolder" runat="server">
            <table cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td style="padding-bottom: 10px;" align="left">
                    
                        <table cellpadding="10" cellspacing="0" class="noprint">
                          <tr>
                            <td>
                                <a href="#" onclick="javascript:window.print()">Print Certificate</a>
                            
                            </td>
                            <td>
                                <asp:HyperLink ID="returnToConfirmationLink" NavigateUrl="~/Confirmation.aspx" Text="Return to Confirmation Page" runat="server" />
                            </td>
                          </tr>
                        </table>
            
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <div id="certDiv" runat="server">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <table cellpadding="10" cellspacing="0" border="0" width="650" style="border: solid 5px #707070;">
                                    <tr>
                                        <td width="35%" align="center">
                                    		
                                            <asp:Image ID="advertiserImage" BorderWidth="1" BorderColor="#404040" runat="server" />

                                        </td>
                                        <td width="30%" align="center">
                         
                                            <asp:Label ID="amountLabel" style="font-size: 36pt; font-family: Verdana, Sans-Serif, Arial; font-weight: bold;" runat="server" />
                                    		
                                        </td>
                                        <td width="35%" align="center">
                                            <asp:Image ID="certLogoImage" ImageUrl="~/images/ds_logo_small.gif" BorderWidth="0" runat="server" />
                                        </td>

                                    </tr>
                                    <tr>
                                        <td valign="top" align="left" style="padding-right: 0px;">
                                            
                                            <asp:Label ID="advertiserNameLabel" runat="server" style="font-family: Arial; font-size: 12pt; font-weight: bold;" /><br />
                                            <asp:Label ID="addressLabel" CssClass="certsmall" runat="server" /><br />
                                            <asp:Label ID="phoneLabel" CssClass="certsmall" style="font-weight: bold;" runat="server" /><br />
                                   
                                        </td>
                                        <td align="center" nowrap>
                                            <span style="font-family: Verdana, Sans-Serif; font-weight: bold;">Certificate Number</span>
                                            <br />
                                     		
                                            <asp:Label ID="certificateNumberLabel" style="color: Red; font-family: Arial; font-size: 40pt; font-weight:bold;" runat="server" />
                                    		
                                        </td>

                                        <td style="font-family: Arial; font-size: 12pt; font-weight: bold;" align="center" valign="top">

                                            <asp:Image ID="logoImage" Width="125" Height="75" BorderWidth="1" BorderColor="#404040" runat="server" /><br />
                                            <DollarSaver:Name runat="server" />
                                        </td>
                                    </tr>
                                    <tr>

                                        <td class="certinfo" align="center" colspan="3" style="border-top: solid 5px #707070;">
                                            <B>THIS CERTIFICATE IS GOOD FOR:</B><br />

                                            <asp:Label ID="certificateDescriptionLabel" runat="server" /><br />
                                            <b>Purchase Date:</b>  <asp:Label ID="purchaseDateLabel" runat="server" />
                                        </td>
                                    </tr>


                                    </table>
                                        
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                    <table cellpadding="3" cellspacing="0" width="650">
                                    
	                                    <tr>
		                                    <td align="left">
                                    TERMS AND CONDITIONS OF USE: 
		                                    </td>
	                                    </tr>
	                                    <tr>
		                                    <td class="certsmall" align="left">

			                                    <ul>
                                        <li>To enjoy your certificate just visit the business specified on the certificate. Present the certificate when you are placing an order or checking out.</li>
                                        <li>Each certificate has a unique certificate number that must be legible at the time of redemption. When the certificate is presented the business will check off the unique certificate number. Each certificate is valid for one time use and once it is used it is null and void and cannot be used again. Any attempt at re-use, sale, trade or duplication of this certificate is prohibitive by law.</li>
                                        <li>This certificate can only be redeemed by the business indicated on the face of the certificate. It cannot be redeemed for cash and can only be used once. Certificates will not be replaced if lost, stolen or redeemed by an unauthorized user.</li>

                                        <li>This certificate will expire one year from the purchase date indicated or unless otherwise indicated.</li>
                                        <li>Tax and gratuities are the responsibility of the bearer of this certificate.</li>
                                        <li>This certificate must be used in full at the time of redemption unless the business specified on the certificate chooses to give credit for the unused portion of the certificate. If your purchase exceeds the amount of the certificate you are required to pay the remainder with a valid method of payment accepted by the business specified on the certificate.</li>
                                        
                                        <li>Return and Exchange Policy: <span style="font-weight: bold; text-decoration: underline;">ALL SALES ARE FINAL.</span>  Absolutely no refunds, or exchanges for any reason whatsoever. No refunds will be issued for the business indicated on the face of the certificate if it has closed or gone out of business regardless of reason. </li>
                                        
                                        <li>The seller of this certificate is not liable for faulty goods /services provided by this business.</li>
                                        <li>The seller of this certificate makes no warranties expressed or implied with respect to any products purchased with a certificate.</li>

                                        <li>Any questions regarding the product or service being purchased from the business specified on this certificate should be directed to that business.</li>
                                       
                                        <li>Purchaser of a certificate has read and agrees to all conditions stated in the Privacy Policy and Terms of Agreement posted on the website.</li>

			                                    </ul>
		                                    </td>

 	                                    </tr>
 	                                    <tr>
 		                                    <td class="certplain" align="left">
			                                    <b>Participating business:</b> Any questions regarding the use of this certificate or validation thereof please contact your sales representative at your partnering media outlet or contact us using the contact form on our website: <asp:Label Id="contactUsLabel1" runat="server" />.
		                                    </td>

	                                    </tr>
	                                    <tr>
		                                    <td class="certplain" align="left">
			                                    <b>Consumers:</b> Any questions regarding the use of the certificate please contact us using the contact form on our website: <asp:Label Id="contactUsLabel2" runat="server" />
		                                    </td>

	                                    </tr>
                                    </table>
                                    
                                </td>
                            </tr>
                        </table>
                    
                        </div>
                        
                        <div style="max-width: 650px;"></div>
                        <asp:PlaceHolder ID="voidHolder" Visible="false" runat="server">
                        <div style="z-index:0">
                            <asp:Image ImageUrl="~/images/void.gif" AlternateText="Void" runat="server" />
                        </div>
                        </asp:PlaceHolder>
                    </td>
                </tr>
            </table>  
            
            
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="certNotFoundHolder" runat="server">
            <div style="font-size: 22px; color: Red; font-family: Verdana, Sans-Serif; padding: 50px;">
            Error: The certificate was not found
            </div>
             
            </asp:PlaceHolder>


        </td>
    </tr>
</table>
              
</body>
</html>

