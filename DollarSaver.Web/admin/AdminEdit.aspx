<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" Inherits="DollarSaver.Web.Admin.AdminEdit" Title="DollarSaver - Edit User" CodeBehind="AdminEdit.aspx.cs" %>

<%@ MasterType VirtualPath="~/admin/admin.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-6">
            <div class="card">
                <div class="card-header card-header-icon" data-background-color="green">
                    <i class="fa fa-pencil-square-o fa-2x" aria-hidden="true"></i>
                </div>
                <div class="card-content">
                    <h4 class="card-title"><asp:Label ID="createEditLabel" runat="server" /> Station User</h4>
                    

                    <div class="form-horizontal">

                        <div class="row">
                            <label class="col-md-3 label-on-left">Username</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:TextBox ID="usernameBox" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ControlToValidate="usernameBox" Text="* Required" runat="server" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="usernameBox" Text="* Invalid Username" ValidationExpression="\w+" Display="Dynamic" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">Role</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:DropDownList ID="roleList" CssClass="selectpicker" data-style="btn btn-primary" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">Email Address</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:TextBox ID="emailBox" CssClass="form-control" runat="server" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="emailBox" Text="* Invalid E-mail Address" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">Is Contact</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <div class="checkbox">
                                        <label>
                                            <asp:CheckBox ID="isOrderContactBox" Checked="true" runat="server" />
                                            <span class="checkbox-material"></span>
                                        </label>
                                    </div>
                                    <span class="small_text">User will be e-mailed when non-printable certificates are purchased</span>
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
                            <label class="col-md-3 label-on-left">Password</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:TextBox TextMode="password" ID="passwordBox" CssClass="form-control" runat="server" />
                                    <p>Leave password field blank if you do not want to change it</p>
                                    <span class="small_text">Password must be at least 6 characters long</span>
                                </div>
                            </div>
                        </div>


                        <div class="row">
                            <label class="col-md-3 label-on-left">Confirm Password</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:TextBox TextMode="password" ID="confirmPasswordBox" CssClass="form-control" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:Button ID="saveButton" Text="Save" runat="server" CssClass="btn btn-primary"/>
                                    <asp:Button ID="cancelButton" Text="Cancel" CausesValidation="false" runat="server" CssClass="btn btn-danger"/>
                                    &nbsp; &nbsp;
                                    <asp:Button ID="deleteButton" Text="Delete" CausesValidation="false" runat="server" CssClass="btn btn-warning"/>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
