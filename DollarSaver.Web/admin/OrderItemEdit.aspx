<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" Inherits="DollarSaver.Web.Admin.OrderItemEdit" Title="DollarSaver - Edit Order Items" CodeBehind="OrderItemEdit.aspx.cs" %>

<%@ MasterType VirtualPath="~/admin/admin.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:PlaceHolder ID="orderHolder" runat="server">

        <div class="row">
            <div class="col-md-6">
                <div class="card">
                    <div class="card-header card-header-icon" data-background-color="green">
                        <i class="material-icons">assignment</i>
                    </div>
                    <div class="card-content">
                        <h4 class="card-title">Order Details</h4>
                        <asp:PlaceHolder ID="editLinkHolder" runat="server">
                            <asp:HyperLink ID="editLink" Text="Edit Details" runat="server" />
                        </asp:PlaceHolder>
                        <table class="table table-hover table-responsive">
                            <tr>
                                <td class="form_field">Order ID:
                                </td>
                                <td class="form_display">
                                    <asp:Label ID="orderIdLabel" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="form_field">Status:
                                </td>
                                <td class="form_display">
                                    <asp:Label ID="statusLabel" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="form_field">Date:
                                </td>
                                <td class="form_display">
                                    <asp:Label ID="orderDateLabel" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="form_field">Name:
                                </td>
                                <td class="form_display">
                                    <asp:Label ID="nameLabel" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="form_field">E-Mail:
                                </td>
                                <td class="form_display">
                                    <asp:Label ID="emailLabel" runat="server" />
                                </td>
                            </tr>
                            <asp:PlaceHolder ID="shippingHolder" runat="server">
                                <tr>
                                    <td class="form_field">Shipping Info:
                                    </td>
                                    <td class="form_display">
                                        <asp:Label ID="shippingInfoLabel" runat="server" />
                                    </td>
                                </tr>
                            </asp:PlaceHolder>
                            <tr>
                                <td class="form_field">Billing Address:
                                </td>
                                <td class="form_display">
                                    <asp:Label ID="addressLabel" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="form_field">Phone:
                                </td>
                                <td class="form_display">
                                    <asp:Label ID="phoneLabel" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>

            <div class="col-md-6">
                <div class="card">
                    <div class="card-header card-header-icon" data-background-color="green">
                        <i class="material-icons">assignment</i>
                    </div>
                    <div class="card-content">
                        <h4 class="card-title">Order Line Items</h4>
                        <asp:PlaceHolder ID="editItemHolder" runat="server">
                            <asp:HyperLink ID="editItemLink" Text="Edit Order Items" runat="server" />
                        </asp:PlaceHolder>
                        <table class="table table-hover table-responsive">
                            <tr>
                                <td>Certificates</td>
                                <td>Number</td>
                                <td>Price</td>
                                <td>Qty</td>
                                <td>Total</td>
                            </tr>
                            <asp:Repeater ID="lineItemRepeater" runat="server">

                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:HyperLink ID="certificateLink" runat="server" />
                                        </td>
                                        <td>
                                            <asp:GridView ID="numberGrid" AutoGenerateColumns="false" CellPadding="2" CellSpacing="0" runat="server"
                                                BorderWidth="0" ShowHeader="false" GridLines="None">

                                                <Columns>

                                                    <asp:TemplateField>
                                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="certificateLink" runat="server" />
                                                            <asp:Label ID="nonPrintableLabel" Text="Not Printable" runat="server" />
                                                            <asp:HiddenField ID="certNumberIdHidden" runat="server" />
                                                            <asp:CheckBox ID="deleteBox" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>

                                            </asp:GridView>
                                        </td>
                                        <td>
                                            <asp:Label ID="priceLabel" runat="server" /></td>
                                        <td>
                                            <asp:HiddenField ID="lineItemIdHidden" runat="server" />
                                            <asp:Label ID="qtyLabel" runat="server" /></td>
                                        <td>
                                            <asp:Label ID="totalLabel" runat="server" /></td>
                                    </tr>
                                </ItemTemplate>


                            </asp:Repeater>

                            <asp:PlaceHolder ID="orderTotalHolder" runat="server">
                                <tr>
                                    <td class="text-right" colspan="4">Order Total</td>
                                    <td>
                                        <asp:Label ID="orderTotalLabel" runat="server" /></td>
                                </tr>
                            </asp:PlaceHolder>
                        </table>

                        <div class="row">
                            <div class="col-md-7 col-md-offset-5 text-right">
                                <asp:Button ID="saveButton" Text="Return Selected Items" runat="server" CssClass="btn btn-primary" />&nbsp;
                                <asp:Button ID="cancelButton" Text="Cancel" CausesValidation="false" runat="server" CssClass="btn btn-danger" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>


    </asp:PlaceHolder>



    <asp:PlaceHolder ID="notFoundHolder" Visible="false" runat="server">
        <div class="row">
            <div class="col-md-6">
                <div class="card">
                    <div class="card-content">
                        <div class="alert alert-danger">
                            <span>
                                <b>Info - </b>Order was not found</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:PlaceHolder>



</asp:Content>

