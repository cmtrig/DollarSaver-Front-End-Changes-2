<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" Inherits="DollarSaver.Web.Admin.SalesPersonEdit" Title="DollarSaver - Edit Sales Person" CodeBehind="SalesPersonEdit.aspx.cs" %>

<%@ MasterType VirtualPath="~/admin/admin.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="idHidden" runat="server" />



    <div class="row">
        <div class="col-md-6">
            <div class="card">
                <div class="card-header card-header-icon" data-background-color="green">
                    <i class="material-icons">attach_money</i>
                </div>
                <div class="card-content">
                    <h4 class="card-title">
                        <asp:Label ID="createEditLabel" runat="server" />
                        Sales Person</h4>
                    <div class="form-horizontal">

                        <div class="row">
                            <label class="col-md-3 label-on-left">First Name</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:TextBox ID="firstNameBox" MaxLength="50" runat="server" CssClass="form-control"/>
                                    <asp:RequiredFieldValidator ControlToValidate="firstNameBox" Text="*" Display="Static" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">Last Name</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:TextBox ID="lastNameBox" MaxLength="50" runat="server" CssClass="form-control"/>
                                    <asp:RequiredFieldValidator ControlToValidate="lastNameBox" Text="*" Display="Static" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">Email</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:TextBox ID="emailBox" Columns="40" MaxLength="320" runat="server" CssClass="form-control"/>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="emailBox" Text="* Invalid E-mail Address" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Static" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">Mobile Phone</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:TextBox ID="mobilePhoneBox" Columns="25" MaxLength="25" runat="server" CssClass="form-control"/>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">Work Phone</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:TextBox ID="workPhoneBox" Columns="25" MaxLength="50" runat="server" CssClass="form-control"/>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">Is Active</label>
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
                            <div class="col-md-9">
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
    </div>

</asp:Content>

