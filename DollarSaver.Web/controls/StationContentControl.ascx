<%@ Control Language="C#" AutoEventWireup="true" Inherits="DollarSaver.Web.controls.StationContentControl" CodeBehind="StationContentControl.ascx.cs" %>


<div class="row">
    <div class="col-md-12">
        <div class="card">
            <div class="card-header card-header-icon" data-background-color="green">
                <i class="fa fa-pencil-square-o fa-2x" aria-hidden="true"></i>
            </div>
            <div class="card-content">
                <h4 class="card-title">Edit Custom Images</h4>


                <table border="0" class="table table-responsive">
                    <tr>
                        <td class="form_field">Header Image:</td>
                        <td class="form_value">
                            <table cellpadding="2" cellspacing="0" border="0">
                                <tr>
                                    <td align="left">
                                        <asp:FileUpload ID="headerUpload" Width="450" runat="server" />
                                    </td>
                                </tr>
                                <asp:PlaceHolder ID="resetHeaderHolder" runat="server">
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="resetHeaderBox" runat="server" />
                                            Reset to default image
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <tr>
                                    <td align="left">
                                        <asp:Image ID="headerImage" ImageUrl="~/images/header.jpg" Width="500" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="small_text">(1000px X 80px OR 800px X 200px) <i>Size reduced to show on this page.</i> This image appears at the top of every page.
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="form_field">Logo Image:</td>
                        <td class="form_value">
                            <table cellpadding="2" cellspacing="0" border="0">
                                <tr>
                                    <td align="left">
                                        <asp:FileUpload ID="logoUpload" Width="450" runat="server" />
                                    </td>
                                </tr>
                                <asp:PlaceHolder ID="resetLogoHolder" runat="server">
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="resetLogoBox" runat="server" />
                                            Reset to default image
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <tr>
                                    <td align="left">
                                        <asp:Image ID="logoImage" ImageUrl="~/images/ds_small.gif" Width="125" Height="75" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="small_text">
                                        <span class="small_text">(Exactly 125px X 75px)</span> This image appears at the bottom of the home page and on each certificate.
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="form_field">Top Image:</td>
                        <td class="form_value">
                            <table cellpadding="2" cellspacing="0" border="0">
                                <tr>
                                    <td align="left">
                                        <asp:FileUpload ID="topperUpload" Width="450" runat="server" />
                                    </td>
                                </tr>
                                <asp:PlaceHolder ID="resetTopperHolder" runat="server">
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="resetTopperBox" runat="server" />
                                            Reset to default image
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <tr>
                                    <td align="left">
                                        <asp:Image ID="topperImage" ImageUrl="~/images/ds_logo_header.gif" runat="server" style="width:225px;"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="small_text">
                                        <span>(Approximately 225px X 50px)</span> This image appears at the top left above the header image on every page.
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="form_field">Local Savings Image:</td>
                        <td class="form_value">
                            <table cellpadding="2" cellspacing="0" border="0">
                                <tr>
                                    <td align="left">
                                        <asp:FileUpload ID="localSavingsUpload" Width="450" runat="server" />
                                    </td>
                                </tr>
                                <asp:PlaceHolder ID="resetLocalSavingsHolder" runat="server">
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="resetLocalSavingsBox" runat="server" />
                                            Reset to default image
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <tr>
                                    <td align="left">
                                        <asp:Image ID="localSavingsImage" ImageUrl="~/images/local.gif" runat="server" style="width:250px;"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="small_text">
                                        <span class="small_text">(Approximately 250px X 30px)</span> This image appears to the right of the Top Image.
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="form_field">Certificate Logo Image:</td>
                        <td class="form_value">
                            <table cellpadding="2" cellspacing="0" border="0">
                                <tr>
                                    <td align="left">
                                        <asp:FileUpload ID="certificateLogoUpload" Width="450" runat="server" />
                                    </td>
                                </tr>
                                <asp:PlaceHolder ID="resetCertificateLogoHolder" runat="server">
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="resetCertificateLogoBox" runat="server" />
                                            Reset to default image
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <tr>
                                    <td align="left">
                                        <asp:Image ID="certificateLogoImage" ImageUrl="~/images/ds_logo_small.gif" runat="server" style="width:90px;"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="small_text">
                                        <span class="small_text">(Approximately 90px X 100px) This image appears in the top right corner of the certificate.</span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <asp:MultiView ID="homePageImageMultiView" runat="server">
                        <asp:View ID="standardView" runat="server">

                            <tr>
                                <td class="form_field">Weekly Deal Image:</td>
                                <td class="form_value">
                                    <table cellpadding="2" cellspacing="0" border="0">
                                        <tr>
                                            <td align="left">
                                                <asp:FileUpload ID="weeklyDealUpload" Width="450" runat="server" />
                                            </td>
                                        </tr>
                                        <asp:PlaceHolder ID="resetWeeklyDealHolder" runat="server">
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="resetWeeklyDealBox" runat="server" />
                                                    Reset to default image
                                                </td>
                                            </tr>
                                        </asp:PlaceHolder>
                                        <tr>
                                            <td align="left">
                                                <asp:Image ID="weeklyDealImage" ImageUrl="~/images/boxtop_weekly.gif" runat="server" style="width:462px;"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" class="small_text">
                                                <span class="small_text">(Approximately 462px X 35px)</span> This image appears on the home page when a Weekly Deal is set.
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="form_field">Daily Deal Image:</td>
                                <td class="form_value">
                                    <table cellpadding="2" cellspacing="0" border="0">
                                        <tr>
                                            <td align="left">
                                                <asp:FileUpload ID="dailyDealUpload" Width="450" runat="server" />
                                            </td>
                                        </tr>
                                        <asp:PlaceHolder ID="resetDailyDealHolder" runat="server">
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="resetDailyDealBox" runat="server" />
                                                    Reset to default image
                                                </td>
                                            </tr>
                                        </asp:PlaceHolder>
                                        <tr>
                                            <td align="left">
                                                <asp:Image ID="dailyDealImage" ImageUrl="~/images/boxtop_daily.gif" runat="server" style="width:462px;"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" class="small_text">
                                                <span class="small_text">(Approximately 462px X 35px)</span> This image appears on the home page when a Daily Deal is set.
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="form_field">Other Deals Image:</td>
                                <td class="form_value">
                                    <table cellpadding="2" cellspacing="0" border="0">
                                        <tr>
                                            <td align="left">
                                                <asp:FileUpload ID="otherDealsUpload" Width="450" runat="server" />
                                            </td>
                                        </tr>
                                        <asp:PlaceHolder ID="resetOtherDealsHolder" runat="server">
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="resetOtherDealsBox" runat="server" />
                                                    Reset to default image
                                                </td>
                                            </tr>
                                        </asp:PlaceHolder>
                                        <tr>
                                            <td align="left">
                                                <asp:Image ID="otherDealsImage" ImageUrl="~/images/bar_othergreat.gif" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" class="small_text">
                                                <span class="small_text">(Approximately 950px X 20px)</span> This image appears on the home page.
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </asp:View>
                        <asp:View ID="dealOfTheWeekView" runat="server">

                            <tr>
                                <td class="form_field">Subheader Image:</td>
                                <td class="form_value">
                                    <table cellpadding="2" cellspacing="0" border="0">
                                        <tr>
                                            <td align="left">
                                                <asp:FileUpload ID="dotwSubheaderUpload" Width="450" runat="server" />
                                            </td>
                                        </tr>
                                        <asp:PlaceHolder ID="resetDotwSubheaderHolder" runat="server">
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="resetDotwSubheaderBox" runat="server" />
                                                    Reset to default image
                                                </td>
                                            </tr>
                                        </asp:PlaceHolder>
                                        <tr>
                                            <td align="left">
                                                <asp:Image ID="dotwSubheaderImage" ImageUrl="~/images/dotw_background.gif" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" class="small_text">(Approximately 750px X 40px) This image appears at the top of the home page and will be overlaid by the site name.
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                        </asp:View>
                    </asp:MultiView>
                    <tr>
                    <td></td>
                        <td class="form_footer" colspan="2">
                            <asp:Button ID="saveButton" Text="Save" runat="server" CssClass="btn btn-primary"/>
                            <asp:Button ID="cancelButton" Text="Cancel" runat="server" CssClass="btn btn-danger"/>
                        </td>
                    </tr>

                </table>
            </div>
        </div>
    </div>
</div>
