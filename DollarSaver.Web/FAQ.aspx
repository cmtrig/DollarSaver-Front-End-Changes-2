<%@ Page Language="C#" MasterPageFile="~/consumer.master" AutoEventWireup="true" Inherits="DollarSaver.Web.FAQ" Title="DollarSaver - F.A.Q." 
    Codebehind="FAQ.aspx.cs" EnableViewState="false" %>
<%@ Register TagPrefix="DollarSaver" TagName="Name" Src="~/controls/Name.ascx" %>
<%@ Register TagPrefix="DollarSaver" TagName="DealOfTheWeekName" Src="~/controls/DealOfTheWeekName.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="page-padding">                                   
                <asp:PlaceHolder ID="standardHolder" runat="server">

                <h1 class="uk-h2">Frequently Asked Questions</h1>

                <p class="heading_two">What is <DollarSaver:Name runat="server" />?</p>
                <p class="p_text">
                 <DollarSaver:Name runat="server" /> is a website operated by RWR, LLC and its network of radio stations, TV stations, newspaper and websites (hereinafter referred to as <DollarSaver:Name runat="server" />). The purpose of <DollarSaver:Name runat="server" /> is to offer local consumers information and savings from local businesses. <DollarSaver:Name runat="server" /> offers discounted certificates from local businesses. <DollarSaver:Name runat="server" /> may also provide content relating to local businesses, including tips, recipes, suggestions and ratings.
                </p>
                 
                <p class="heading_two">Why shop <DollarSaver:Name runat="server" />?</p>
                <p class="p_text">
                Every certificate for sale through <DollarSaver:Name runat="server" /> is discounted 30% or more. Our inventory of local certificates changes frequently and when offered items are gone, there is no guarantee that they will be available in the future. From time to time we vary the discount level (usually from 30%-50%) so you may see the same certificate offered at different prices at different times.
                </p>
                 
                <p class="heading_two">How do I purchase a <DollarSaver:Name runat="server" /> certificate?</p>
                <p class="p_text">
                Browse through the <DollarSaver:Name runat="server" /> site. There are several categories and there may be sub-categories to help you locate the products and services. Local businesses are highlighted and offer their products and services for sale via certificates that you purchase on <DollarSaver:Name runat="server" />. When you find a certificate that you would like to purchase, simply click "Add to Cart" and it will be placed in your shopping cart. You will then be taken through the checkout process. If at any time you wish to remove your selection, you may do so by clicking on "remove" next to the selection.
                </p>
                <p class="p_text">
                Your selections are always visible in your shopping cart as you continue to browse through the various goods and service offers. Simply click on "View Cart". Once you've completed your purchase you have two options to print your certificates. Either one will work. 1) After completing the checkout process click view/ print and your certificate will appear. Click "Print Your Certificate" to print at your computer! Repeat process for each certificate purchased. 2) Your certificates will also be emailed to the email address you entered in the checkout process should you wish to send them to someone or print them later.  
                </p>
                 
                <p class="heading_two">What certificate amounts are available?</p>
                <p class="p_text">
                Certificate values vary by merchant. If the value of a certificate is less than $25.00, there may be a minimum purchase required. For example certificates worth $5.00 each would be offered with a minimum purchase of five for $25.00, less the applicable <DollarSaver:Name runat="server" /> discount.
                </p>
                 
                <p class="heading_two">I'm sending a <DollarSaver:Name runat="server" /> certificate as a gift. What gift options do you offer?</p>
                <p class="p_text">
                You can elect to email the certificate to the recipient, rather than yourself (making sure that you know their correct email address and are confident that a firewall or spam filter will not reject the incoming email from <DollarSaver:Name runat="server" />!). You can print the certificate, put it in an envelope or a gift box and send or deliver it to the recipient.
                </p>
                 
                <p class="heading_two">Can I send a <DollarSaver:Name runat="server" /> certificate to someone other than myself?</p>
                <p class="p_text">
                Yes. See above.
                </p>
                 
                <p class="heading_two">How long does the process take?</p>
                <p class="p_text">
                It takes one to two minutes to complete the purchase process, and then after checkout has been completed you will have two options for receiving your certificate(s). Both work just fine...
                </p>
                <p class="p_text">
                         1. Click on the view/print link(s) that appear on the screen after completing the checkout process. Each click of a link will make a certificate appear on your screen. Click the print button to print your certificate. Repeat process for each certificate purchased.
                 </p>
                 <p class="p_text">

                         2. Your certificates will also be emailed to the email address you entered in the checkout process. This is so you can print your certificate(s) out at a later time if you so desire or email them as a gift to another person.
                </p>
                 
                <p class="heading_two">Can I order a <DollarSaver:Name runat="server" /> certificate that is no longer shown on the website?</p>
                <p class="p_text">
                If you don’t see it on the site, you can’t count on buying it. It may or may not return. If the "quantity remaining" is "0" then you should assume that there is no inventory left and that it cannot be purchased at that time.
                </p>
                 
                <p class="heading_two">How do I use a <DollarSaver:Name runat="server" /> certificate?</p> 
                <p class="p_text">
                To Redeem:
                </p><p class="p_text">
                All product-guarantees and warranties are the responsibility of the business whose certificate you purchased. Neither <DollarSaver:Name runat="server" /> nor the merchant is responsible for lost or stolen certificates. There is no cash back upon redemption or at any other time. Print your certificate, take it to the local business that provided it, and present it to the business when you arrive. It will be accepted for the face value, as cash, subject to any restrictions that are included. For example, if you purchase a $50.00 certificate that is discounted 50%, you will pay $25.00 for the certificate but will be able to purchase $50.00  worth of goods or services (subject to applicable restrictions) at the issuing business. Please note you will not receive "change" in the form of cash for any unused portion of the certificate. In most cases you have to use the full amount at the time you present it. For instance, using the example above, if you present your $50.00 certificate (which you paid $25.00 for) to pay for a $45.00 item at the issuing business, you will leave $5.00 "on the table." You would NOT receive $5.00 in cash and possibly not in credit. Some businesses may elect to return the unused portion of the certificate’s value in credit. That decision is at the discretion of the business specified on the certificate and not a policy of the <DollarSaver:Name runat="server" />. If your purchase exceeds the value of the <DollarSaver:Name runat="server" /> certificate then you are required to pay the remainder with a valid method of payment accepted by the business specified on the certificate.
                </p>
                 
                <p class="heading_two">When does a <DollarSaver:Name runat="server" /> certificate expire?</p>
                <p class="p_text">
                 <DollarSaver:Name runat="server" /> certificates may be valid for up to one year from the date of purchase indicated on the certificate unless (a) otherwise indicated on the certificate itself or (b) where otherwise prohibited by law.
                </p>
                 
                <p class="heading_two">Is there any shipping/handling charges?</p>
                <p class="p_text">
                All <DollarSaver:Name runat="server" /> certificates are delivered electronically and therefore there are no shipping/handling charges.
                </p>
                 

                 
                <p class="heading_two">What do I do if I damage or lose my <DollarSaver:Name runat="server" /> Certificate?</p>
                <p class="p_text">
                If you lose or damage your certificate, print it again. Remember, each certificate has a unique certificate number and the issuing business will honor each unique certificate number once…the first time it is presented. If you lose your certificate and someone else finds they could redeem it. <DollarSaver:Name runat="server" /> certificates are like cash. Guard them as you would cash.
                </p>
                 
                 
                 
                <p class="heading_two">May I cancel or change an order?</p>
                <p class="p_text">
                You may change an order at any time during the shopping process. An order may not be cancelled after the checkout process is completed.
                </p>
                 
                <p class="heading_two">Is there a policy on returning a <DollarSaver:Name runat="server" /> certificate?</p>
                <p class="p_text">
                <span style="font-weight: bold; text-decoration: underline;">ALL SALES ARE FINAL.</span> Absolutely no refunds, or exchanges for any reason whatsoever. No refunds will be issued for the business indicated on the face of the certificate if it has closed or gone out of business regardless of reason.
                </p>
                
                 
                <p class="heading_two">Paying By Credit Card</p>
                <p class="p_text">
                <DollarSaver:Name runat="server" /> accepts Visa, MasterCard, American Express and Discover. Your purchase will be listed as "DollarSaver 2072299178" on your credit card statement.
                </p>
                 

                </asp:PlaceHolder>
                 
                 
                <asp:PlaceHolder ID="dealOfTheWeekHolder" runat="server">


                <p class="heading_two">What is <DollarSaver:DealOfTheWeekName runat="server" />?</p>
                <p class="p_text">
                <DollarSaver:DealOfTheWeekName runat="server" /> is a website operated by RWR, LLC, and its network of radio stations, TV stations, newspaper and websites (hereinafter referred to as <DollarSaver:DealOfTheWeekName runat="server" />). The purpose of <DollarSaver:DealOfTheWeekName runat="server" /> is to offer local consumers information and savings from local businesses. <DollarSaver:DealOfTheWeekName runat="server" /> offers discounted certificates from local businesses. <DollarSaver:DealOfTheWeekName runat="server" /> may also provide content relating to local businesses, including tips, recipes, suggestions and ratings.
                </p>
                 
                <p class="heading_two">Why shop <DollarSaver:DealOfTheWeekName runat="server" />?</p>
                <p class="p_text">
                Every certificate for sale through <DollarSaver:DealOfTheWeekName runat="server" /> is discounted 30% or more. Our inventory of local certificates changes frequently and when offered items are gone, there is no guarantee that they will be available in the future. From time to time we vary the discount level (usually from 30%-50%) so you may see the same certificate offered at different prices at different times.
                </p>
                 
                <p class="heading_two">How do I purchase a <DollarSaver:DealOfTheWeekName runat="server" /> certificate?</p>
                <p class="p_text">
                Go to The Deal of Week website. Then at the designated "On Sale" time, or anytime after simply click "Buy Now" and your purchase will be placed in your shopping cart. You will then be taken through the checkout process. If at any time you wish to remove your selection, you may do so by clicking on "remove" next to the selection.
                </p>
                <p class="p_text">If you continue to browse, and shop you can view the contents of your shopping cart anytime by simply clicking on "View Cart". Once you've completed the checkout process you have two options to print your certificates. Either one will work just fine.</p>
                <p class="p_text">
                         1. After completing the checkout process click view/ print and your certificate will appear. Click "Print Your Certificate" to print at your computer! Repeat process for each certificate purchased.
                        </p>
                         <p class="p_text">
                         2. Your certificates will also be emailed to the email address you entered in the checkout process. This is so you can print your certificate(s) out at a later time if you so desire. Or possibly you would like to enter the e-mail address of a person you wished to gift.
                </p>
                 
                <p class="heading_two">What certificate amounts are available?</p>
                <p class="p_text">
                Certificate values vary by merchant. If the value of a certificate is less than $25.00, there may be a minimum purchase required.
                </p>
                 
                <p class="heading_two">I'm sending a <DollarSaver:DealOfTheWeekName runat="server" /> certificate as a gift. What gift options do you offer?</p>
                <p class="p_text">
                You can elect to email the certificate to the recipient, rather than yourself (making sure that you know their correct email address and are confident that a firewall or spam filter will not reject the incoming email from <DollarSaver:DealOfTheWeekName runat="server" />!). You can print the certificate, put it in an envelope or a gift box and send or deliver it to the recipient.
                </p>
                 
                <p class="heading_two">Can I send a <DollarSaver:DealOfTheWeekName runat="server" /> certificate to someone other than myself?</p>
                <p class="p_text">
                Yes. See above.
                </p>
                 
                <p class="heading_two">How long does the process take?</p>
                <p class="p_text">
                It takes one to two minutes to complete the purchase process, and then after checkout has been completed you will have two options for receiving your certificate(s) (see above).
                </p>
                 
                <p class="heading_two">Can I order a <DollarSaver:DealOfTheWeekName runat="server" /> certificate that is no longer shown on the website?</p>
                <p class="p_text">
                If you don’t see it on the site, you can’t count on buying it. Check site periodically because it may or may not return. If the "quantity remaining" is "0" then you should assume that there is no inventory left and it cannot be purchased at that time.
                </p>
                 
                <p class="heading_two">How do I use a <DollarSaver:DealOfTheWeekName runat="server" /> certificate? </p>
                <p class="p_text"> 
                To Redeem:
                </p><p class="p_text">
                All product-guarantees and warranties are the responsibility of the business whose certificate you purchased. Neither <DollarSaver:DealOfTheWeekName runat="server" /> nor the merchant is responsible for lost or stolen certificates. There is no cash back upon redemption or at any other time. Print your certificate, take it to the local business that provided it, and present it to the business when you arrive. It will be accepted for the face value, as cash, subject to any restrictions that are included. For example, if you purchase a $50.00 certificate that is discounted 50%, you will pay $25.00 for the certificate but will be able to purchase $50.00  worth of goods or services (subject to applicable restrictions) at the issuing business. Please note you will not receive "change" in the form of cash for any unused portion of the certificate. In most cases you have to use the full amount at the time you present it. For instance, using the example above, if you present your $50.00 certificate (which you paid $25.00 for) to pay for a $45.00 item at the issuing business, you will leave $5.00 "on the table." You would NOT receive $5.00 in cash and possibly not in credit. Some businesses may elect to return the unused portion of the certificate’s value in credit. That decision is at the discretion of the business specified on the certificate and not a policy of the <DollarSaver:DealOfTheWeekName runat="server" />. If your purchase exceeds the value of the <DollarSaver:DealOfTheWeekName runat="server" /> certificate then you are required to pay the remainder with a valid method of payment accepted by the business specified on the certificate.
                </p>
                 
                <p class="heading_two">When does a <DollarSaver:DealOfTheWeekName runat="server" /> certificate expire?</p>
                <p class="p_text">
                <DollarSaver:DealOfTheWeekName runat="server" /> certificates may be valid for up to one year from the date of purchase indicated on the certificate unless (a) otherwise indicated on the certificate itself or (b) where otherwise prohibited by law.
                </p>
                 
                <p class="heading_two">Is there any shipping/handling charges?  </p>
                <p class="p_text">
                All <DollarSaver:DealOfTheWeekName runat="server" /> certificates are delivered electronically and therefore there are no shipping/handling charges.
                </p>
                 

                 
                <p class="heading_two">What do I do if I damage or lose my <DollarSaver:DealOfTheWeekName runat="server" /> Certificate?</p>
                <p>
                If you lose or damage your certificate, print it again. Remember, each certificate has a unique certificate number and the issuing business will honor each unique certificate number on...the first time it is presented. If you lose your certificate and someone else finds it they could redeem it. <DollarSaver:DealOfTheWeekName runat="server" /> certificates are as good as cash. Guard them as you would cash. If you have a problem with a <DollarSaver:DealOfTheWeekName runat="server" /> certificate, please contact us.
                </p>
                 
                 
                 
                <p class="heading_two">May I cancel or change an order?</p>
                <p class="p_text">
                You may change an order at any time during the shopping process. An order may not be cancelled after the checkout process is completed.
                </p>
                 
                <p class="heading_two">Is there a policy on returning a <DollarSaver:DealOfTheWeekName runat="server" /> certificate? </p>
                <p class="p_text">
                <span style="font-weight: bold; text-decoration: underline;">ALL SALES ARE FINAL.</span> Absolutely no refunds, or exchanges for any reason whatsoever. No refunds will be issued for the business indicated on the face of the certificate if it has closed or gone out of business regardless of reason.
                </p>
                 
                <p class="heading_two">Paying By Credit Card</p>
                <p class="p_text">
                <DollarSaver:DealOfTheWeekName ID="DealOfTheWeekName1" runat="server" /> accepts Visa, MasterCard, American Express and Discover. Your purchase will be listed as "DollarSaver 2072299178" on your credit card statement.
                </p>

</div>

                </asp:PlaceHolder> 


</asp:Content>

