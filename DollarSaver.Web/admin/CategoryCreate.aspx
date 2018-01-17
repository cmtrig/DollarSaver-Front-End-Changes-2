<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CategoryCreate.aspx.cs" Inherits="DollarSaver.Web.Admin.CategoryCreate"
    Title="DollarSaver - Create Category" MasterPageFile="~/admin/admin.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="row">
        <div class="col-md-4">
            <div class="card">
                <div class="card-header card-header-icon" data-background-color="green">
                    <i class="material-icons">folder_special</i>
                </div>
                <div class="card-content">
                    <h4 class="card-title">
                        <asp:Label ID="headerCell" runat="server">Categories</asp:Label>
                    </h4>
                    <div class="form-horizontal">

                        <div class="row">
                            <label class="col-md-3 label-on-left">Name</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:TextBox ID="txtCategory" CssClass="form-control" runat="server" />
                                    <asp:RequiredFieldValidator ID="txtCategoryValidator" ControlToValidate="txtCategory" Text="Category cannot be blank" Display="Dynamic" runat="server"/>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-9 col-md-offset-3">
                                <div class="form-group">
                                    <asp:Button ID="saveButton" Text="Save" runat="server" CssClass="btn btn-primary" />
                                    <asp:Button ID="cancelButton" Text="Cancel" CausesValidation="false" runat="server" CssClass="btn btn-danger" />
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
