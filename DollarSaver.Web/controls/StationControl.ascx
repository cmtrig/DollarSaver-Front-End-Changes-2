<%@ Control Language="C#" AutoEventWireup="true" Inherits="DollarSaver.Web.controls.StationControl" CodeBehind="StationControl.ascx.cs" %>
<%@ Register TagPrefix="tinymce" Namespace="Moxiecode.TinyMCE.Web" Assembly="Moxiecode.TinyMCE" %>


<div class="row">
    <div class="col-md-6">
        <div class="card">
            <div class="card-header card-header-icon" data-background-color="green">
                <i class="material-icons">settings_input_antenna</i>
            </div>
            <div class="card-content">
                <h4 class="card-title">
                    <asp:Label ID="actionLabel" runat="server" />
                    Station</h4>
                <asp:PlaceHolder ID="linkHolder" runat="server">
                    <asp:HyperLink ID="editContentLink" NavigateUrl="~/admin/StationContent.aspx" runat="server">Edit Content</asp:HyperLink>
                </asp:PlaceHolder>

                <div class="form-horizontal">

                    <div class="row">
                        <label class="col-md-3 label-on-left">Name</label>
                        <div class="col-md-9">
                            <div class="form-group">
                                <asp:TextBox ID="nameBox" runat="server" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="nameBox" Text="*" Display="Static" runat="server" />
                            </div>
                        </div>
                    </div>

                    <asp:PlaceHolder ID="superAdminHolder" runat="server">
                        <div class="row">
                            <label class="col-md-3 label-on-left">Station Login Code</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:TextBox ID="codeBox" CssClass="form-control" runat="server" />
                                    <asp:RequiredFieldValidator ID="codeBoxRFV" ControlToValidate="codeBox" Text="*" Display="Static" runat="server" />
                                </div>
                            </div>
                        </div>
                    </asp:PlaceHolder>

                    <div class="row">
                        <label class="col-md-3 label-on-left">Site Name</label>
                        <div class="col-md-9">
                            <div class="form-group">
                                <asp:TextBox TextMode="MultiLine" ID="siteNameBox" Columns="50" Rows="5" runat="server" />
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <label class="col-md-3 label-on-left">Site Type</label>
                        <div class="col-md-9">
                            <div class="form-group">
                                <asp:DropDownList ID="siteTypeList" CssClass="selectpicker" data-style="btn btn-primary" runat="server">
                                    <asp:ListItem Value="1">Standard</asp:ListItem>
                                    <asp:ListItem Value="2">Deal of the Week</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <label class="col-md-3 label-on-left">Station Type</label>
                        <div class="col-md-9">
                            <div class="form-group">
                                <asp:DropDownList ID="stationTypeList" CssClass="selectpicker" data-style="btn btn-primary" runat="server">
                                    <asp:ListItem Value="1">Radio</asp:ListItem>
                                    <asp:ListItem Value="2">Television</asp:ListItem>
                                    <asp:ListItem Value="3">Other</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <label class="col-md-3 label-on-left">Phone Number</label>
                        <div class="col-md-9">
                            <div class="form-group">
                                <asp:TextBox ID="phoneNumberBox" CssClass="form-control" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="phoneNumberBox" Text="*" Display="Static" runat="server" />
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <label class="col-md-3 label-on-left">Address 1</label>
                        <div class="col-md-9">
                            <div class="form-group">
                                <asp:TextBox ID="address1Box" CssClass="form-control" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="address1Box" Text="*" Display="Static" runat="server" />
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
                                <asp:TextBox ID="cityBox" MaxLength="200" runat="server" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="cityBox" Text="*" Display="Static" runat="server" />
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
                        <label class="col-md-3 label-on-left">Zip Code</label>
                        <div class="col-md-9">
                            <div class="form-group">
                                <asp:TextBox ID="zipCodeBox" Columns="10" MaxLength="12" runat="server" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="zipCodeBox" Text="*" Display="Static" runat="server" />
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <label class="col-md-3 label-on-left">Time Zone</label>
                        <div class="col-md-9">
                            <div class="form-group">
                                <asp:DropDownList ID="timeZoneList" CssClass="selectpicker" data-style="btn btn-primary" runat="server" />
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <label class="col-md-3 label-on-left">Station Website</label>
                        <div class="col-md-9">
                            <div class="form-group">
                                <asp:DropDownList ID="stationUrlStart" CssClass="selectpicker" data-style="btn btn-primary" runat="server">
                                    <asp:ListItem Text="http://" Value="http://" Selected="true" />
                                    <asp:ListItem Text="https://" Value="https://" />
                                </asp:DropDownList>
                                <asp:TextBox ID="stationUrlBox" Columns="50" MaxLength="500" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <label class="col-md-3 label-on-left">Content 1</label>
                        <div class="col-md-9">
                            <div class="form-group">
                                <asp:TextBox ID="content1Box" runat="server" Columns="50" Rows="5" TextMode="MultiLine" />
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <label class="col-md-3 label-on-left">Content 2</label>
                        <div class="col-md-9">
                            <div class="form-group">
                                <asp:TextBox ID="content2Box" runat="server" Columns="50" Rows="5" TextMode="MultiLine" />
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <label class="col-md-3 label-on-left">Consumer Site Active</label>
                        <div class="col-md-9">
                            <div class="form-group">
                                <div class="checkbox">
                                    <label>
                                        <asp:CheckBox ID="isSiteActiveBox" Checked="true" runat="server" />
                                        <span class="checkbox-material">
                                            
                                        </span>                                         
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-9 col-md-offset-3">
                            <div class="form-group">
                                <asp:Button ID="saveButton" Text="Save" runat="server" CssClass="btn btn-primary" />
                                <asp:Button ID="cancelButton" Text="Cancel" runat="server" CssClass="btn btn-danger" />
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
</div>
