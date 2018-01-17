<%@ Page Language="C#" MasterPageFile="~/consumer.master" AutoEventWireup="true" Inherits="DollarSaver.Web.AdvertiserMapPage" Title="DollarSaver - Advertiser" Codebehind="AdvertiserMap.aspx.cs" %>
<%@ Register TagPrefix="DollarSaver" TagName="CertificateDetail" Src="~/controls/CertificateDetail.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:PlaceHolder ID="advertiserHolder" runat="server">

<div class="uk-panel uk-align-center">
   <table cellpadding="5" cellspacing="0" border="0">
    <tr>
    <td valign="top" align="center" width="135">
     <asp:Image ID="advertiserImage" BorderWidth="1" BorderColor="#404040" runat="server" />
    </td>
     <td valign="top" align="left" style="padding: 2px;">
     
	<table cellpadding="3" cellspacing="0" border="0">
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="advertiserNameLabel" CssClass="headingOne" runat="server" /> &nbsp; 
                                                <asp:HyperLink ID="viewWebsiteLink" Text="View Item / Website" CssClass="blue_link" Target="_blank" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="medium_text">
                                                <asp:Label ID="addressLabel" runat="server" /> &nbsp;
                                                <asp:HyperLink ID="returnLink" Text="Return to Certificate Page" CssClass="blue_link" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="medium_text">
                                                <asp:Label ID="phoneLabel" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                
                                    <div id="map" style="width: 750px; height: 450px; border: solid 1px #404040;"></div>
                                
                                </td>
                            </tr>
                        </table>
                    
</div>

</asp:PlaceHolder>

   <script type="text/javascript">

    //<![CDATA[
 
function showAddress(address, displayText) {
    var map = null;
    var geocoder = null;
    
    map = new GMap2(document.getElementById("map"));
    map.addControl(new GSmallMapControl());
    geocoder = new GClientGeocoder();
    
    if (GBrowserIsCompatible()) {
      geocoder.getLatLng(
        address,
        function(point) {
          if (!point) {
            alert(address + " not found");
          } else {
            map.setCenter(point, 15);
            var marker = new GMarker(point);
            map.addOverlay(marker);
            marker.openInfoWindowHtml('<span style="font: Tahoma;">' + displayText + '</span>');
          }
        }
      );
    }
}

//]]>
</script>


</asp:Content>

