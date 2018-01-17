<%@ Page Language="C#" MasterPageFile="~/consumer.master" AutoEventWireup="true" Inherits="DollarSaver.Web.Category" Title="DollarSaver - Category" Codebehind="Category.aspx.cs" %>
<%@ Register TagPrefix="DollarSaver" TagName="CertificateBrief" Src="~/controls/CertificateBrief.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div class="uk-align-center">

            <div class="uk-margin-small">
                
                        <asp:Label ID="categoryLabel" runat="server" CssClass="headingOne" /><br />
                        <p style="font-size: 12px;">Save here on certificates from local businesses. Scroll down to find the business you want then click it to see how much you'll save!</p> 
                    </div>
                 
                <asp:PlaceHolder ID="noAdvertisersFoundHolder" runat="server">
                <div class="uk-margin-small">
                    <em>
                        Sorry, no businesses are currently available in this category.
                    </em>
                </div>
                
                </asp:PlaceHolder>
                    
                <asp:Repeater ID="advertiserRepeater" runat="server">
                <HeaderTemplate>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Repeater ID="certificateRepeater" runat="server">
                        <HeaderTemplate>
                        </HeaderTemplate>
                        <ItemTemplate>
                        <div class="uk-margin-small">
                            <DollarSaver:CertificateBrief ID="certificateBrief" runat="server" />
                        </div>
                        
                        </ItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                    </asp:Repeater>
                </ItemTemplate>
                <FooterTemplate>
                </FooterTemplate>
                    
                </asp:Repeater>
                    
            
</div>

</asp:Content>
