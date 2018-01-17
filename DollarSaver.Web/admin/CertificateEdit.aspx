<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" Inherits="DollarSaver.Web.Admin.CertificateEdit" Title="DollarSaver - Edit Certificate" CodeBehind="CertificateEdit.aspx.cs" %>

<%@ MasterType VirtualPath="~/admin/admin.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(function () {
            $('#<%=deliveryList.ClientID%>').change(function () { setDeliveryVisibility(); });
            setDeliveryVisibility();
        }
        )

        function setDeliveryVisibility() {
            $('#<%=deliveryList.ClientID%> input:checked').val() == "3" ? $("#delivery_note").show() : $("#delivery_note").hide();
        }

    </script>
    <script type="text/javascript">

        function updateLength() {

            var textbox = document.getElementById('ctl00_ContentPlaceHolder1_descriptionBox');
            var label = document.getElementById('ctl00_ContentPlaceHolder1_descriptionLengthLabel');

            label.innerHTML = textbox.value.length;

            if (textbox.value.length > 500) {
                label.style.color = 'red';
            } else {
                label.style.color = '#404040';
            }
        }

    </script>


    <asp:HiddenField ID="idHidden" runat="server" />
    <asp:HiddenField ID="advertiserIdHidden" runat="server" />


    <div class="row">
        <div class="col-md-6">
            <div class="card">
                <div class="form-horizontal">
                    <div class="card-header card-header-icon" data-background-color="green">
                        <i class="fa fa-certificate fa-2x" aria-hidden="true"></i>
                    </div>
                    <div class="card-content">
                        <h4 class="card-title">
                            <asp:Label ID="createEditLabel" runat="server" />
                            Certificate</h4>

                        <div class="row">
                            <label class="col-md-3 label-on-left">Advertiser</label>
                            <div class="col-md-9">
                                <div class="form-control-static">
                                    <asp:Label ID="advertiserNameLabel" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">Certificate Name</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:TextBox ID="nameBox" Columns="40" MaxLength="100" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ControlToValidate="nameBox" Text="*" Display="Dynamic" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">
                                Description<br />
                                <span style="font-size: 10px;">(<asp:Label ID="descriptionLengthLabel" Text="0" runat="server" />/500)</span>
                            </label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:TextBox ID="descriptionBox" TextMode="MultiLine" Columns="40" Rows="6" Wrap="true" onKeyUp="updateLength();" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ControlToValidate="descriptionBox" Text="*" Display="Static" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">Minimum Purchase Qty</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:TextBox ID="minPurchaseQtyBox" Text="1" Columns="2" MaxLength="10" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="minPurchaseQtyBox" Text="*" Display="Dynamic" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">Maximum Purchase Qty</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:TextBox ID="maxPurchaseQtyBox" Text="0" Columns="2" MaxLength="10" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="maxPurchaseQtyBox" Text="*" Display="Dynamic" runat="server" />
                                    <span class="small_text">Enter 0 for no limit</span>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">Face Value</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:TextBox ID="faceValueBox" Columns="8" MaxLength="10" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ControlToValidate="faceValueBox" Text="*" Display="Dynamic" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">Discount</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:TextBox ID="discountBox" Columns="5" MaxLength="10" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ControlToValidate="discountBox" Text="*" Display="Dynamic" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">Discount Type</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:DropDownList ID="discountList" runat="server" CssClass="selectpicker" data-style="btn btn-primary" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">Low Stock Qty</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:TextBox ID="lowStockAmountBox" Text="10" Columns="2" MaxLength="10" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="lowStockAmountRFV" ControlToValidate="lowStockAmountBox" Text="*" Display="Dynamic" runat="server" />
                                    <span class="small_text">Enter 0 for no notification</span>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">On Sale Date</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:PlaceHolder ID="standardDateHolder" runat="server">
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="onSaleDateBox" Columns="12" MaxLength="10" runat="server" CssClass="form-control" />
                                                <asp:Label ID="onSaleDateFormatLabel" CssClass="small_text" runat="server">(MM/DD/YYYY)</asp:Label>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:DropDownList ID="onSaleHourList" CssClass="selectpicker" data-style="btn btn-primary" runat="server" />
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:DropDownList ID="onSaleMinuteList" CssClass="selectpicker" data-style="btn btn-primary" runat="server" />
                                            </div>

                                            <asp:RequiredFieldValidator ID="onSaleDateBoxRFV" ControlToValidate="onSaleDateBox" Text="*" Display="Dynamic" runat="server" />
                                        </div>
                                    </asp:PlaceHolder>
                                    <asp:DropDownList ID="onSaleDateList" Visible="false" CssClass="selectpicker" data-style="btn btn-primary" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">Delivery</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:RadioButtonList ID="deliveryList" CssClass="radioButtonList" runat="server">
                                        <asp:ListItem Text="Printable" Value="1" Selected="True" />
                                        <asp:ListItem Text="Not Printable - Mail to Customer" Value="2" />
                                        <asp:ListItem Text="Not Printable - Pick Up" Value="3" />
                                    </asp:RadioButtonList>
                                    <div id="delivery_note">
                                        <h5>Pick Up Instructions</h5>
                                        <asp:TextBox ID="deliveryNoteBox" TextMode="MultiLine" Columns="35" Rows="5" Wrap="true" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">Number Length</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:DropDownList ID="numberLengthList" CssClass="selectpicker" data-style="btn btn-primary" runat="server">
                                        <asp:ListItem Value="5" Selected="true" />
                                        <asp:ListItem Value="6" />
                                        <asp:ListItem Value="7" />
                                        <asp:ListItem Value="8" />
                                        <asp:ListItem Value="9" />
                                        <asp:ListItem Value="10" />
                                    </asp:DropDownList>
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
                            <label class="col-md-3 label-on-left">Active</label>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:TextBox ID="qtyBox" Columns="5" MaxLength="10" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ControlToValidate="qtyBox" Text="*" Display="Dynamic" runat="server" />
                                </div>
                            </div>
                        </div>

                        <asp:PlaceHolder ID="newCertHolder" runat="server">
                            <div class="row">
                                <label class="col-md-3 label-on-left">Confirm Quantity</label>
                                <div class="col-md-9">
                                    <div class="form-group">
                                        <asp:TextBox ID="confirmQtyBox" Columns="5" MaxLength="10" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="confirmQtyBox" Text="*" Display="Dynamic" runat="server" />
                                        <asp:CompareValidator ControlToValidate="confirmQtyBox" ControlToCompare="qtyBox" Operator="Equal" Text="*" ErrorMessage="Quantity values must be equal" Display="Static" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </asp:PlaceHolder>

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

        <asp:PlaceHolder ID="certificateNumberHolder" runat="server">
            <div class="col-md-6">
                <div class="card">
                    <div class="form-horizontal">
                        <div class="card-content">
                            <h4 class="card-title">Certificate Numbers</h4>

                            <div class="row">
                                <label class="col-md-3 label-on-left">Qty</label>
                                <div class="col-md-9">
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="newQtyBox" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:Button ID="addNumbersButton" Text="Add" CausesValidation="false" runat="server" CssClass="btn btn-sm btn-success" />
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:Button ID="removeNumbersButton" Text="Remove" CausesValidation="false" runat="server" CssClass="btn btn-sm btn-danger" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <h5>Number History</h5>

                            <table class="table table-responsive table-hover">
                                <asp:Repeater ID="createHistoryRepeater" runat="server">
                                    <HeaderTemplate>
                                        <thead>
                                            <tr>
                                                <th>Create Date</th>
                                                <th>Qty</th>
                                            </tr>
                                        </thead>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="createDateLabel" runat="server" /></td>
                                            <td>
                                                <asp:Label ID="quantityLabel" runat="server" /></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>

                            <br />
                            <asp:PlaceHolder ID="printableHolder" runat="server">
                                <h5>Email Certificate Numbers</h5>
                                <div class="row">
                                    <label class="col-md-3 label-on-left">Email</label>
                                    <div class="col-md-9">
                                        <div class="form-group">
                                            <asp:TextBox ID="emailBox" Columns="30" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <label class="col-md-3 label-on-left">Create Date</label>
                                    <div class="col-md-9">
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="createDateList" CssClass="selectpicker" data-style="btn btn-primary" runat="server" />
                                                </div>
                                                <div class="col-sm-6">
                                                    <asp:Button ID="emailButton" Text="Send" CausesValidation="false" runat="server" CssClass="btn btn-primary" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-9 col-md-offset-3">
                                        <asp:HyperLink ID="viewSampleLink" Text="View Sample Certificate" runat="server" />
                                    </div>
                                </div>
                                <br />
                                <br />
                                <asp:GridView ID="availableNumberGrid" runat="server" AutoGenerateColumns="false"
                                    BorderWidth="0" GridLines="none" CssClass="table table-responsive">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Available
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="certificateNumberLink" Visible="false" runat="server" />
                                                <asp:Label ID="certificateNumberLabel" Visible="false" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CreateDate" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Create Date"
                                            HtmlEncode="false" />
                                    </Columns>
                                </asp:GridView>

                                <asp:PlaceHolder ID="noAvailableHolder" runat="server">
                                    <div class="row">
                                        <div class="col-md-9 col-md-offset-3">
                                            Available
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-9 col-md-offset-3">None Found</div>
                                    </div>
                                </asp:PlaceHolder>

                                <br />
                                <br />
                                <div class="row">
                                <label class="col-md-3"><strong>Used</strong></label>
                                    <div class="col-md-9">
                                        <asp:Label ID="noneUsedLabel" runat="server">None Found</asp:Label>
                                        <asp:HyperLink ID="usedLink" runat="server" />
                                    </div>
                                </div>

                            </asp:PlaceHolder>

                            <asp:PlaceHolder ID="notPrintableHolder" runat="server">
                                <div class="row">
                                    <label class="col-md-3 label-on-left">Available</label>
                                    <div class="col-md-9">
                                        <div class="form-group">
                                            <asp:Label ID="availableCountLabel" runat="server" CssClass="form-control"/>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <label class="col-md-3 label-on-left">Used</label>
                                    <div class="col-md-9">
                                        <div class="form-group">
                                            <asp:Label ID="usedCountLabel" runat="server" CssClass="form-control"/>
                                        </div>
                                    </div>
                                </div>
                            </asp:PlaceHolder>
                        </div>
                    </div>
                </div>
            </div>
        </asp:PlaceHolder>
    </div>
</asp:Content>

