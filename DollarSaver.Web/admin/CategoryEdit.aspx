<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" Inherits="DollarSaver.Web.Admin.CategoryEdit" Title="DollarSaver - Edit Category" CodeBehind="CategoryEdit.aspx.cs" %>

<%@ MasterType VirtualPath="~/admin/admin.master" %>
<%@ Import Namespace="DollarSaver.Core.Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-4">
            <div class="card">
                <div class="card-header card-header-icon" data-background-color="green">
                    <i class="material-icons">folder_special</i>
                </div>
                <div class="card-content">
                    <h4 class="card-title">
                        <asp:Label ID="nameLabel" runat="server" />
                        Sub-categories
                    </h4>

                    <div class="form-horizontal">
                        <asp:PlaceHolder ID="editSubCatHolder" runat="server">

                            <div class="row">
                                <label class="col-md-3 label-on-left">Name</label>
                                <div class="col-md-9">
                                    <div class="form-group">
                                        <asp:TextBox ID="nameBox" runat="server" CssClass="form-control" />
                                        <asp:RequiredFieldValidator ID="nameBoxRFV" ControlToValidate="nameBox" Text="*" Display="Static" runat="server" />
                                        <asp:HiddenField ID="idHidden" runat="server" />
                                        <asp:HiddenField ID="parentIdHidden" runat="server" />
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

                        </asp:PlaceHolder>
                    </div>

                    <asp:PlaceHolder ID="viewSubCatHolder" runat="server">

                        <div class="btn-group">
                            <asp:HyperLink ID="returnToCategoriesLink" Text="Return to Categories" NavigateUrl="~/admin/CategoryList.aspx" runat="server" CssClass="btn btn-primary" />
                            <asp:HyperLink ID="addSubCategoryLink" Text="Add Sub Category" runat="server" CssClass="btn btn-primary" />
                        </div>
                        <asp:PlaceHolder ID="subCatListHolder" runat="server">
                            <asp:GridView ID="subCategoryGrid" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive"
                                BorderWidth="0" GridLines="None">
                                <Columns>
                                    <asp:BoundField HeaderText="Order" DataField="DisplaySeqNo" ItemStyle-HorizontalAlign="center" />

                                    <asp:HyperLinkField DataTextField="Name" DataNavigateUrlFormatString="~/admin/CategoryEdit.aspx?id={0}" DataNavigateUrlFields="CategoryId" 
                                                        HeaderText="Name" ItemStyle-HorizontalAlign="left" />

                                    <asp:TemplateField HeaderText="Order">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="moveUpButton" Text="Up" CommandName="up" 
                                                            CommandArgument="<%#((DollarSaverDB.CategoryRow) Container.DataItem).CategoryId %>" runat="server" >
                                                                <i class="material-icons">arrow_upward</i> </asp:LinkButton>
                                            <asp:LinkButton ID="moveDownButton" Text="Down" CommandName="down" 
                                                            CommandArgument="<%#((DollarSaverDB.CategoryRow) Container.DataItem).CategoryId %>" runat="server" >
                                                                <i class="material-icons">arrow_downward</i> </asp:LinkButton>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </asp:PlaceHolder>

                        <asp:PlaceHolder ID="noSubCatHolder" runat="server">No Sub Categories found 
                        </asp:PlaceHolder>
                    </asp:PlaceHolder>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

