<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" Inherits="DollarSaver.Web.Admin.OrderEdit" Title="DollarSaver - Edit Order Details" CodeBehind="OrderEdit.aspx.cs" %>

<%@ MasterType VirtualPath="~/admin/admin.master" %>
<%@ Register TagPrefix="DollarSaver" TagName="OrderDetail" Src="~/controls/OrderDetail.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-6">
            <div class="card">
                <div class="card-header card-header-icon" data-background-color="green">
                    <i class="fa fa-pencil-square-o fa-2x" aria-hidden="true"></i>
                </div>
                <div class="card-content">
                    <h4 class="card-title">Edit Order Details</h4>
                    <div class="form-horizontal">

                        <div class="row">
                            <label class="col-md-3 label-on-left">Order ID</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:Label ID="orderIdLabel" runat="server" CssClass="form-control"/>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">Status</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:Label ID="statusLabel" runat="server" CssClass="form-control"/>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">Date</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:TextBox ID="orderDateBox" runat="server" CssClass="form-control datetimepicker"/>
                                    <span class="material-input"></span>
<%--                                    <asp:DropDownList ID="hourList" CssClass="other_input" runat="server" />
                                    :
                                    <asp:DropDownList ID="minuteList" CssClass="other_input" runat="server" />
                                    <asp:DropDownList ID="amPmList" CssClass="other_input" runat="server">
                                        <asp:ListItem Value="AM" />
                                        <asp:ListItem Value="PM" />
                                    </asp:DropDownList>--%>
                                    <asp:RequiredFieldValidator ID="orderDateBoxRFV" ControlToValidate="orderDateBox"
                                        Text="*" Display="Dynamic" runat="server" />

                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">First Name</label>
                            <div class="col-md-9">
                                <div class="form-group label-floating is-empty">
                                    <label class="control-label"></label>
                                    <asp:TextBox ID="firstNameBox" runat="server" CssClass="form-control"/>
                                    <asp:RequiredFieldValidator ID="firstNameBoxRFV" ControlToValidate="firstNameBox"
                                        Text="* Required" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">Last Name</label>
                            <div class="col-md-9">
                                <div class="form-group label-floating is-empty">
                                    <label class="control-label"></label>
                                    <asp:TextBox ID="lastNameBox" runat="server" CssClass="form-control"/>
                                    <asp:RequiredFieldValidator ID="lastNameBoxRFV" ControlToValidate="lastNameBox"
                                        Text="* Required" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">Email</label>
                            <div class="col-md-9">
                                <div class="form-group label-floating is-empty">
                                    <label class="control-label"></label>
                                    <asp:TextBox ID="emailBox" Columns="40" MaxLength="320" runat="server" CssClass="form-control"/>
                                    <asp:RequiredFieldValidator ID="emailBoxRFV" ControlToValidate="emailBox"
                                        Text="* Required" Display="Dynamic" runat="server" /><br />
                                    <asp:RegularExpressionValidator ID="emailBoxREV" ControlToValidate="emailBox"
                                        Text="* Invalid E-mail Address"
                                        ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        Display="Dynamic" runat="server" />
                                </div>
                            </div>
                        </div>

                        <asp:PlaceHolder ID="shippingHolder" runat="server">
                            <h4>Shipping Info</h4>

                            <div class="row">
                                <label class="col-md-3 label-on-left">First Name</label>
                                <div class="col-md-9">
                                    <div class="form-group label-floating is-empty">
                                        <label class="control-label"></label>
                                        <asp:TextBox ID="shippingFirstNameBox" runat="server" CssClass="form-control"/>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="shippingFirstNameBox"
                                            Text="* Required" runat="server" />
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <label class="col-md-3 label-on-left">Last Name</label>
                                <div class="col-md-9">
                                    <div class="form-group label-floating is-empty">
                                        <label class="control-label"></label>
                                        <asp:TextBox ID="shippingLastNameBox" runat="server" CssClass="form-control"/>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="shippingLastNameBox" Text="* Required" runat="server" />
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <label class="col-md-3 label-on-left">Address 1</label>
                                <div class="col-md-9">
                                    <div class="form-group label-floating is-empty">
                                        <label class="control-label"></label>
                                        <asp:TextBox ID="shippingAddress1Box" Columns="35" MaxLength="200" runat="server" CssClass="form-control"/>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="shippingAddress1Box" Text="* Required" runat="server" />
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <label class="col-md-3 label-on-left">Address 2</label>
                                <div class="col-md-9">
                                    <div class="form-group label-floating is-empty">
                                        <label class="control-label"></label>
                                        <asp:TextBox ID="shippingAddress2Box" Columns="35" MaxLength="200" runat="server" CssClass="form-control"/>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <label class="col-md-3 label-on-left">City</label>
                                <div class="col-md-9">
                                    <div class="form-group label-floating is-empty">
                                        <label class="control-label"></label>
                                        <asp:TextBox ID="shippingCityBox" MaxLength="200" runat="server" CssClass="form-control"/>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="shippingCityBox" Text="* Required" runat="server" />
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <label class="col-md-3 label-on-left">State</label>
                                <div class="col-md-9">
                                    <div class="form-group label-floating is-empty">
                                        <label class="control-label"></label>
                                        <asp:DropDownList ID="shippingStateList" CssClass="selectpicker" data-style="btn btn-primary" runat="server" />
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <label class="col-md-3 label-on-left">Zip Code</label>
                                <div class="col-md-9">
                                    <div class="form-group label-floating is-empty">
                                        <label class="control-label"></label>
                                        <asp:TextBox ID="shippingZipCodeBox" Columns="10" MaxLength="12" runat="server" CssClass="form-control"/>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="shippingZipCodeBox" Text="* Required" runat="server" />
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <label class="col-md-3 label-on-left">Phone Number</label>
                                <div class="col-md-9">
                                    <div class="form-group label-floating is-empty">
                                        <label class="control-label"></label>
                                        <asp:TextBox ID="shippingPhoneNumberBox" MaxLength="50" runat="server" CssClass="form-control"/>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="shippingPhoneNumberBox" Text="* Required" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </asp:PlaceHolder>

                        <h4>Billing Address</h4>
                        <div class="row">
                            <label class="col-md-3 label-on-left">Address 1</label>
                            <div class="col-md-9">
                                <div class="form-group label-floating is-empty">
                                    <label class="control-label"></label>
                                    <asp:TextBox ID="address1Box" Columns="35" MaxLength="200" runat="server" CssClass="form-control"/>
                                    <asp:RequiredFieldValidator ID="address1BoxRFV" ControlToValidate="address1Box" Text="* Required" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">Address 2</label>
                            <div class="col-md-9">
                                <div class="form-group label-floating is-empty">
                                    <label class="control-label"></label>
                                    <asp:TextBox ID="address2Box" Columns="35" MaxLength="200" runat="server" CssClass="form-control"/>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">City</label>
                            <div class="col-md-9">
                                <div class="form-group label-floating is-empty">
                                    <label class="control-label"></label>
                                    <asp:TextBox ID="cityBox" MaxLength="200" runat="server" CssClass="form-control"/>
                                    <asp:RequiredFieldValidator ID="cityBoxRFV" ControlToValidate="cityBox" Text="* Required" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">State</label>
                            <div class="col-md-9">
                                <div class="form-group label-floating is-empty">
                                    <label class="control-label"></label>
                                    <asp:DropDownList ID="stateList" CssClass="selectpicker" data-style="btn btn-primary" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">Zip Code</label>
                            <div class="col-md-9">
                                <div class="form-group label-floating is-empty">
                                    <label class="control-label"></label>
                                    <asp:TextBox ID="zipCodeBox" Columns="10" MaxLength="12" runat="server" CssClass="form-control"/>
                                    <asp:RequiredFieldValidator ID="zipCodeBoxRFV" ControlToValidate="zipCodeBox" Text="* Required" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">Phone Number</label>
                            <div class="col-md-9">
                                <div class="form-group label-floating is-empty">
                                    <label class="control-label"></label>
                                    <asp:TextBox ID="phoneNumberBox" MaxLength="50" runat="server" CssClass="form-control"/>
                                    <asp:RequiredFieldValidator ID="phoneNumberBoxRFV" ControlToValidate="phoneNumberBox" Text="* Required" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-9 col-md-offset-3">
                                <asp:Button ID="saveButton" Text="Save" runat="server" CssClass="btn btn-primary" />
                                <asp:Button ID="cancelButton" Text="Cancel" CausesValidation="false" runat="server" CssClass="btn btn-danger" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Scripts1" ContentPlaceHolderID="ScriptsPLaceHolder" runat="Server">
    <script>
        $(document).ready(function () {
            ds.initFormExtendedDatetimepickers();
        });
    </script>
</asp:Content>