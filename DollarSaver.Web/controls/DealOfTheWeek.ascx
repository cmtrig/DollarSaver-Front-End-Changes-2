<%@ Control Language="C#" AutoEventWireup="true" Inherits="DollarSaver.Web.controls.DealOfTheWeek" Codebehind="DealOfTheWeek.ascx.cs" %>
<%@ Register Src="~/controls/Name.ascx" TagPrefix="DollarSaver" TagName="Name" %>



<script language="javascript" type="text/javascript">

/*
Count down until any date script-
By JavaScript Kit (www.javascriptkit.com)
Over 200+ free scripts here!
*/

var montharray = new Array("Jan","Feb","Mar","Apr","May","Jun","Jul","Aug","Sep","Oct","Nov","Dec")


var today = Date.parse("<asp:Literal ID="todayLiteral" runat="server" />");
var saleday = Date.parse("<asp:Literal ID="saleDayLiteral" runat="server" />");
    
//function countdown(yr, m, d, hour, minute){
function countdown(){
    
    var dayMillisecs = 1000 * 60 * 60 * 24;
    
    dd = saleday - today
   
    dday = Math.floor(dd / dayMillisecs * 1);
    dhour = Math.floor((dd % dayMillisecs) / (60 * 60 * 1000) * 1);
    dmin = Math.floor(((dd % dayMillisecs) % (60 * 60 * 1000))/(60 * 1000) * 1);
    dsec = Math.floor((((dd % dayMillisecs) % (60 * 60 * 1000)) % (60 * 1000)) / 1000 * 1);
    
    var countDownLabel
    var link_holder;
    var inactive_holder;
    
    if(document.getElementById) {
        countDownLabel = document.getElementById("countDownLabel");
        link_holder = document.getElementById("link_holder");
        inactive_holder = document.getElementById("inactive_holder");
    } else if (document.all) {
        countDownLabel = document.all["countDownLabel"];
        link_holder = document.all["link_holder"];
        inactive_holder = document.all["inactive_holder"];
    } else if (document.layers) {
        countDownLabel = document.layers["countDownLabel"];
        link_holder = document.layers["link_holder"];
        inactive_holder = document.layers["inactive_holder"];
    }
    
    if(countDownLabel != null) {
        if(dday <= 0 && dhour <= 0 && dmin <= 0 && dsec <= 1){
            countDownLabel.innerHTML = "";
            link_holder.style.display = "";
            inactive_holder.style.display = "none";
            return;
        } else {
            countDownLabel.innerHTML = dday + " days, " + dhour + " hours, " + dmin + " minutes, and " + dsec + " seconds";
            link_holder.style.display = "none";
            inactive_holder.style.display = "";
            //setTimeout("countdown(" + yr + ", " + m + ", " + d + ", " + hour + ", " + minute + ")", 1000);
            today = today + 1000;
            setTimeout("countdown()", 1000);
        }
    }
}
</script>

<table cellpadding="10" cellspacing="0" border="0" style="width: 100%;" class="dotw_box">
    <asp:PlaceHolder ID="dealHolder" runat="server" >
    <tr>
        <td valign="top" align="left" class="dotw_bg">
            <table cellpadding="5" cellspacing="0" border="0">
                <tr>
                    <td valign="top" align="left">
                        <table cellpadding="5" cellspacing="0" border="0" style="width: 170px;">
                            <tr>
                                <td align="center">
                                    <asp:Image ID="advertiserImage" BorderWidth="1" BorderColor="#404040" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="medium_text">
                                    <asp:Label ID="addressLabel" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="medium_text" nowrap>
                                    <asp:Label ID="phoneLabel" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                
                                    <div style="padding: 5px; width:90%;">
                                        <fb:like href='<asp:Literal ID="advertiserUrlLiteral" runat="server" />' send="false" layout="button_count" width="120" show_faces="false" action="recommend" font="arial"></fb:like>
                                    </div>
                                
                                    <div style="padding: 5px;">
                                        <a href="https://twitter.com/share" class="twitter-share-button" data-url='<asp:Literal ID="twitterAdvertiserUrlLiteral" runat="server" />' data-text='<asp:Literal ID="twitterTextLiteral" runat="server" />' data-count="none">Tweet</a>
                                        <script>                                            !function(d, s, id) { var js, fjs = d.getElementsByTagName(s)[0]; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = "//platform.twitter.com/widgets.js"; fjs.parentNode.insertBefore(js, fjs); } } (document, "script", "twitter-wjs");</script>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="top" align="left">
                        <table cellpadding="5" cellspacing="0" border="0" style="width: 100%;">
                            <tr>
                                <td align="left">
                                    <asp:Label ID="advertiserNameLabel" CssClass="headingOne" runat="server" /> &nbsp; <asp:HyperLink ID="viewWebsiteLink" Text="View Item / Website" Target="_blank" CssClass="blue_link" runat="server" />
                                    <p class="p_text">
                                    <asp:Label ID="advertiserDescriptionLabel" runat="server" />
                                    </p>
                                    <!-- <asp:Label ID="nameLabel" runat="server" /> -->
                                    <hr style="width: 95%; color: #606060; height: 1px;" />
                                    <p class="p_text">
                                    <asp:Label ID="descriptionLabel" runat="server" />
                                    </p>
                                </td>
                            </tr>
                        </table>
                    </td>
                    
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td valign="top" align="left" style="padding: 0px;">
            <table cellpadding="5" cellspacing="0" border="0" width="100%" id="mainTable" runat="server">
                <tr>
                    <td valign="top" align="left" style="width: 40%;">
                        <p align="center" style="font-size: 16px; font-weight: bold;"><strong>How It Works</strong></p>

                        <p class="medium_text">The <DollarSaver:Name runat="server" /> 
                        is a great way to purchase certificates to local businesses with savings of at least 
                        <span style="color: #FF2020; font-weight: bold;">50%</span> on the things you shop for everyday.
                        
                        <p class="medium_text; font-weight: bold;">
                        TO MAKE A PURCHASE:
                        </p>
                        <h1 style="font-size: 12px; font-weight: bold; font-style: italic;">When the countdown clocks hits zero&hellip;</h1>

                        <table cellpadding="2" cellspacing="0" border="0">
                            <tr>
                                <td class="medium_text" align="left" valign="top" nowrap>
                                    <strong>Step 1:</strong>
                                </td>
                                <td class="medium_text" align="left" valign="top">
                                    Click the green &ldquo;BUY NOW&rdquo; button.
                                </td>
                            </tr>
                            <tr>
                                <td class="medium_text" align="left" valign="top" nowrap>
                                    <strong>Step 2:</strong>
                                </td>
                                <td class="medium_text" align="left" valign="top">
                                    Complete the checkout process by filling out all the necessary information.<br />
                                </td>
                            </tr>
                            <tr>
                                <td class="medium_text" align="left" valign="top" nowrap>
                                    <strong>Step 3:</strong>
                                </td>
                                <td class="medium_text" align="left" valign="top">
                                    Get your certificate(s) one of two ways:<br />
                                    1) Print your certificate(s) at the end of the checkout process.<br />
                                    2) They will also be sent to your email address via an email from<br />
                                    <strong>auto-confirm@dollarsavershow.com</strong>.<br />
                                    Open email and print your certificate(s).
                        
                                </td>
                            </tr>
                        </table>
                         <br />
                         
                        
                           
                    </td>
                    <td valign="top" style="width: 60%;">
                        <p></p>
                        <table cellpadding="5" cellspacing="0" border="0" style="width: 100%">
                            <tr>
                                <td colspan="2" style="color: #FF4000; font-size: 18px; font-weight: bold;" align="center">
                                    <asp:Label ID="onSaleDateLabel" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <span id="countDownLabel"></span>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center" style="padding: 0px;">
                                    
                                    <table cellpadding="5" cellspacing="0" border="0">
                                        <tr>
                                            <td align="center" style="font-size: 12px;">
                                                <p>CERTIFICATE VALUE:<br />
                                                <asp:Label ID="valueLabel" CssClass="headingOne" runat="server" /></p>
                                            </td>
                                            <td align="center" style="font-size: 12px;">
                                                <p><DollarSaver:Name ID="Name1" runat="server" /> PRICE:<br />
                                                <asp:Label ID="priceLabel" CssClass="headingOne" runat="server" /></p>
                                            </td>
                                            <td align="center" style="font-size: 12px;">
                                                <p><b>YOUR SAVINGS:</b><br />
                                                <b><asp:Label ID="savingsLabel" CssClass="headingOne" style="color: #FF2020;" runat="server" /></b></p> 
                                            </td>
                                        </tr>
                                    
                                    </table>
                                    
                                              

                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <div id="link_holder" style="display: none;">
                                    <table cellpadding="5" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="qtyLabel" Text="Quantity:" runat="server" /> <asp:DropDownList ID="qtyDropDown" CssClass="other_input" runat="server" />
                                                <asp:HiddenField ID="certificateIdHidden" runat="server" />
                                                <asp:HiddenField ID="stationIdHidden" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" valign="bottom" >
                                                <asp:ImageButton ID="addToCartButton" ImageUrl="~/images/BUTTON_buynow.gif" AlternateText="Buy Now" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                    </div>
                                    <div id="inactive_holder" style="display: none;">
                                        <img src="/images/BUTTON_buynow_inactive.gif" alt="Coming Soon" />
                                    </div>
                                </td>
                                <td align="center" style="font-size: 12px;">
                                    QUANTITY REMAINING:
                                    <asp:Label ID="quantityLabel" CssClass="headingOne" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:PlaceHolder ID="upcomingDealsHolder" runat="server">
                                    <table cellpadding="5" cellspacing="0" border="0" style="margin-top: 10px;">
                                        <tr>
                                            <td colspan="2" align="center" style="font-weight: bold;">
                                                Upcoming Deals
                                            </td>
                                        </tr>
                                    <asp:Repeater ID="upcomingDealsRepeater" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td style="font-size: 12px; font-weight: bold;" valign="top" align="left"><asp:HyperLink ID="advertiserLink" runat="server" /></td>
                                            <td style="font-size: 12px;" nowrap valign="top" align="left"><asp:Label ID="dealDateLabel" runat="server" /></td>
                                        </tr>
                                    </ItemTemplate>
                                    </asp:Repeater>
                                    </table>
                                    </asp:PlaceHolder>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
       
    </tr>
    
            
    </asp:PlaceHolder>
    
    <asp:PlaceHolder ID="dealNotFoundHolder" Visible="false" runat="server" >
    <tr>
        <td class="bigRed">
            Deal Not Found
        </td>
    </tr>
    </asp:PlaceHolder>

</table>



