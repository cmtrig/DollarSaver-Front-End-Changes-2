<%@ Page Language="C#" MasterPageFile="~/consumer.master" AutoEventWireup="true" Inherits="DollarSaver.Web.AdvertiserMapPage" Title="DollarSaver - Advertiser" Codebehind="AdvertiserMap.aspx.cs" %>
<%@ Register TagPrefix="DollarSaver" TagName="CertificateDetail" Src="~/controls/CertificateDetail.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:PlaceHolder ID="advertiserHolder" runat="server">

<div class="uk-panel uk-align-center">
   <div class="uk-grid uk-grid-medium">
    <div class="uk-width-medium-2-10 uk-align-center">
     <div style=" text-align: center; padding-bottom: 5px;">
    <asp:Image ID="advertiserImage" BorderWidth="1" BorderColor="#404040" runat="server" />
     </div>
    </div>

  <div class="uk-width-medium-8-10 uk-align-center">
     
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
   </div>
    <div class="uk-panel uk-align-center">
         <div id="map" style="max-width: 750px; height: 450px; border: solid 1px #404040;"></div>
    </div>
  </div>   
                    
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

