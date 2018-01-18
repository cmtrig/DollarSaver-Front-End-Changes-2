<%@ Page Language="C#" MasterPageFile="~/consumer.master" AutoEventWireup="true" Inherits="DollarSaver.Web.Privacy" Title="DollarSaver - Privacy Policy" 
    Codebehind="Privacy.aspx.cs" EnableViewState="false" %>
<%@ Register TagPrefix="DollarSaver" TagName="Name" Src="~/controls/Name.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="uk-margin-large page-padding">
               <h1 class="uk-h2">Privacy Policy</h1>
                <p>
                <DollarSaver:Name runat="server" /> is committed to protecting your privacy online. We also want to provide you with the best possible experience on our site. In order to do this, we do gather some personal information about you that helps us to make this site everything our guests desire. Please read the following policy to understand how your personal information will be treated as you make full use of our website. This policy is subject to change, so please be sure to check back periodically.
                </p>
                 
                <p class="heading_two">What are cookies and how do we use them?</p>
                <p>
                Cookies are small bits of data that are sent to your browser and stored on your computer. Cookies are generally used to record the preferences of users and to facilitate a more personalized web experience.
                </p>
                <p class="heading_two">What do we do with the information we collect?</p>
                <p>
                Our primary purpose in collecting personal information is to better understand the demographics and collective makeup of our guests to allow us to make <DollarSaver:Name runat="server" /> everything our guests desire.
                </p>

                <p class="heading_two">Third-Party Warrantees</p>
                <p>
                Due to the nature of <DollarSaver:Name runat="server" />, we have many links to retailers and sites other than <DollarSaver:Name runat="server" />. Although we do our best to ensure that all sites listed here are reputable, inclusion on <DollarSaver:Name runat="server" /> does not imply an endorsement or approval of a site, and we make no warranties or guarantees, implicit or implied, to the services provided or the products sold on those third-party sites. If you have a dispute with a third-party site that was linked to <DollarSaver:Name runat="server" /> you need to contact that site direct to resolve that dispute.
                </p>
                <p>
                We are, however, interested in knowing if our guests have a problem with a specific site. If you should have a problem with a site (even if it's just a broken link) please let us know by using our <asp:HyperLink ID="contactUsLink" NavigateUrl="~/ContactUs.aspx" runat="server">contact form</asp:HyperLink>.
                </p>
                 
                <p class="heading_two">Your Consent</p>
                <p>
                By using our web site you consent to the collection of your personal information as outlined in this privacy policy.
                </p>
                 
                <p class="heading_two">Changes to this policy</p>
                <p>
                This policy is subject to change and all changes will be posted here. It is our goal to allow our guests to know at all times what information we collect, whom we share it with and how we use it. We take your privacy seriously.  
                </p>
        </div>
</asp:Content>

