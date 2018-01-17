<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" Inherits="DollarSaver.Web.Admin.AdvertiserEdit"
    Title="DollarSaver - Edit Advertiser" CodeBehind="AdvertiserEdit.aspx.cs" ValidateRequest="false" %>

<%@ MasterType VirtualPath="~/admin/admin.master" %>
<%@ Register TagPrefix="tinymce" Namespace="Moxiecode.TinyMCE.Web" Assembly="Moxiecode.TinyMCE" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-6">
            <div class="card">
                <div class="card-header card-header-icon" data-background-color="green">
                    <i class="fa fa-pencil-square-o fa-2x" aria-hidden="true"></i>
                </div>
                <div class="card-content">
                    <h4 class="card-title">
                        <asp:Label ID="createEditLabel" runat="server" />
                        Advertister</h4>
                    <div class="form-horizontal">

                        <div class="row">
                            <label class="col-md-3 label-on-left">Name</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:TextBox ID="nameBox" CssClass="form-control" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="nameBox" Text="*" Display="Static" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">Category</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:DropDownList ID="categoryList" AutoPostBack="true" CssClass="selectpicker" data-style="btn btn-primary" runat="server" />
                                </div>
                            </div>
                        </div>

                        <asp:PlaceHolder ID="subCategory1Holder" runat="server">
                            <div class="row">
                                <label class="col-md-3 label-on-left">Sub Category 1</label>
                                <div class="col-md-9">
                                    <div class="form-group">
                                        <asp:DropDownList ID="subCategoryList1" CssClass="selectpicker" data-style="btn btn-primary" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </asp:PlaceHolder>

                        <asp:PlaceHolder ID="subCategory2Holder" runat="server">
                            <div class="row">
                                <label class="col-md-3 label-on-left">Sub Category 2</label>
                                <div class="col-md-9">
                                    <div class="form-group">
                                        <asp:DropDownList ID="subCategoryList2" CssClass="selectpicker" data-style="btn btn-primary" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </asp:PlaceHolder>

                        <asp:PlaceHolder ID="subCategory3Holder" runat="server">
                            <div class="row">
                                <label class="col-md-3 label-on-left">Sub Category 3</label>
                                <div class="col-md-9">
                                    <div class="form-group">
                                        <asp:DropDownList ID="subCategoryList3" CssClass="selectpicker" data-style="btn btn-primary" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </asp:PlaceHolder>

                        <div class="row">
                            <label class="col-md-3 label-on-left">Address 1</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:TextBox ID="address1Box" CssClass="form-control" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">Address 2</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:TextBox ID="address2Box" CssClass="form-control" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">City</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:TextBox ID="cityBox" CssClass="form-control" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">State</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:DropDownList ID="stateList" CssClass="selectpicker" data-style="btn btn-primary" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">ZipCode</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:TextBox ID="zipCodeBox" CssClass="form-control" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">Display Map Link</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <div class="checkbox">
                                        <label>
                                            <asp:CheckBox ID="isAddressMappableBox" Checked="true" runat="server" />
                                            <span class="checkbox-material"></span>
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">Contact Person</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:TextBox ID="contactPersonBox" CssClass="form-control" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">Phone Number</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:TextBox ID="phoneNumberBox" CssClass="form-control" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">WebSite</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:DropDownList ID="websiteUrlStart" CssClass="selectpicker" data-style="btn btn-primary" runat="server">
                                        <asp:ListItem Text="http://" Value="http://" Selected="true" />
                                        <asp:ListItem Text="https://" Value="https://" />
                                    </asp:DropDownList>
                                    <asp:TextBox ID="websiteBox" CssClass="form-control" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">Logo Image</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:FileUpload ID="logoUpload" CssClass="other_input" runat="server" /><br />
                                    <span class="small_text">(125px X 75px OR 75px X 125px)</span>
                                </div>
                            </div>
                            <div class="col-md-9 col-md-offset-3">
                                <asp:Image ID="logoUrlImage" Visible="false" BorderWidth="1" BorderColor="#404040" runat="server" />
                            </div>
                        </div>


                        <div class="row">
                            <label class="col-md-3 label-on-left">Description</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <span style="font-size: 10px;">(<asp:Label ID="descriptionLengthLabel" Text="0" runat="server" />/2000)</span>
                                    <asp:TextBox ID="descriptionBox" TextMode="MultiLine" Columns="40" Rows="6" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">Sales Person</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:DropDownList ID="salesPersonList" CssClass="selectpicker" data-style="btn btn-primary" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">Active</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <div class="checkbox">
                                        <label>
                                            <asp:CheckBox ID="isActiveBox" Checked="true" runat="server" />
                                            <span class="checkbox-material"></span>
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-9 col-md-offset-3">
                                <div class="form-group">
                                    <asp:Button ID="saveButton" Text="Save" runat="server" CssClass="btn btn-primary" />
                                    <asp:Button ID="cancelButton" Text="Cancel" CausesValidation="false" runat="server" CssClass="btn btn-danger" />
                                    &nbsp; &nbsp;
                                    <asp:Button ID="deleteButton" Text="Delete" CausesValidation="false" runat="server" CssClass="btn btn-warning" />
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>



        <div class="col-md-6">
            <div class="card">
                <div class="card-header card-header-icon" data-background-color="green">
                    <i class="material-icons">gradient</i>
                </div>
                <div class="card-content">
                    <h4 class="card-title">Certificates</h4>
                    <asp:HyperLink ID="newCertificateLink" Text="Add New Certificate" runat="server" />

                    <asp:PlaceHolder ID="certificateHolder" runat="server">

                        <asp:PlaceHolder ID="activeCertHolder" runat="server">
                            <asp:GridView ID="certificateGrid" runat="server" GridLines="none"
                                AutoGenerateColumns="false" BorderWidth="0" CssClass="table table-responsive">
                                
                                <Columns>
                                    <asp:HyperLinkField DataTextField="ShortName" HeaderText="Name" DataNavigateUrlFields="AdvertiserId, CertificateId" DataNavigateUrlFormatString="CertificateEdit.aspx?advertiserId={0}&id={1}" ItemStyle-HorizontalAlign="left" />
                                    <asp:BoundField DataField="QtyRemaining" HeaderText="Available" />
                                    <asp:BoundField DataField="QtyUsed" HeaderText="Used" />
                                </Columns>

                            </asp:GridView>

                        </asp:PlaceHolder>
                        <asp:PlaceHolder ID="noActiveFoundHolder" runat="server">No Certificates Found
                        </asp:PlaceHolder>

                        <asp:PlaceHolder ID="inactiveCertHolder" runat="server">
                            <h4 class="card-title">Inactive Certificates</h4>

                            <asp:GridView ID="inactiveCertificateGrid" runat="server" GridLines="none"
                                AutoGenerateColumns="false" BorderWidth="0" CssClass="table table-responsive">

                                <Columns>
                                    <asp:HyperLinkField DataTextField="ShortName" HeaderText="Name" DataNavigateUrlFields="AdvertiserId, CertificateId" DataNavigateUrlFormatString="CertificateEdit.aspx?advertiserId={0}&id={1}" ItemStyle-HorizontalAlign="left" />
                                    <asp:BoundField DataField="QtyRemaining" HeaderText="Remaining" />
                                    <asp:BoundField DataField="QtyUsed" HeaderText="Used" />
                                </Columns>

                            </asp:GridView>

                        </asp:PlaceHolder>
                    </asp:PlaceHolder>
                </div>
            </div>
        </div>
    </div>


</asp:Content>

<asp:Content ContentPlaceHolderID="ScriptsPlaceHolder" ID="Scripts" runat="Server">
    <script src="assets/lib/tinymce/tinymce.min.js"></script>

    <script type="text/javascript">
        tinymce.init({
            selector: 'textarea',
            plugins: 'textcolor colorpicker',
            toolbar: ['bold italic | forecolor'],
            menubar: 'false',
            setup: function (ed) { ed.on('keyup', function (e) { updateLength(); });}
        });

        function updateLength() {

            var label = document.getElementById('ctl00_ContentPlaceHolder1_descriptionLengthLabel');
            var t = tinyMCE.activeEditor.getContent();

            label.innerHTML = t.length;

            if (t.length > 2000) {
                label.style.color = 'red';
            } else {
                label.style.color = '#404040';
            }
        }

    </script>
</asp:Content>
