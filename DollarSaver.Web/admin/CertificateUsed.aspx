<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" Inherits="DollarSaver.Web.Admin.CertificateUsed" Title="DollarSaver - Edit Certificate" CodeBehind="CertificateUsed.aspx.cs" %>

<%@ MasterType VirtualPath="~/admin/admin.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-6">
            <div class="card">
                <div class="form-horizontal">
                    <div class="card-header card-header-icon" data-background-color="green">
                        <i class="fa fa-certificate fa-2x" aria-hidden="true"></i>
                    </div>
                    <div class="card-content">
                        <h4 class="card-title">Used Certificates Numbers</h4>
                        <asp:HyperLink ID="returnToCertificateLink" Text="Return to Certificate" runat="server" />

                        <div class="row">
                            <label class="col-md-3 label-on-left">Advertiser</label>
                            <div class="col-md-9">
                                <div class="form-control-static">
                                    <asp:Label ID="advertiserNameLabel" runat="server"/>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">Certificate</label>
                            <div class="col-md-9">
                                <div class="form-control-static">
                                    <asp:Label ID="certificateNameLabel" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-3 label-on-left">Total Used</label>
                            <div class="col-md-9">
                                <div class="form-control-static">
                                    <asp:Label ID="usedCountLabel" runat="server"/>
                                </div>
                            </div>
                        </div>

                        <asp:GridView ID="usedNumberGrid" runat="server" CellPadding="3"
                            AutoGenerateColumns="false" BorderWidth="0" GridLines="None" CssClass="table table-responsive">

                            <Columns>
                                <asp:BoundField DataField="Number" HeaderText="Number" />
                                <asp:BoundField DataField="CreateDate" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Create Date" HtmlEncode="false" />
                                <asp:HyperLinkField DataTextField="OrderId" DataNavigateUrlFormatString="~/admin/OrderView.aspx?id={0}"
                                    DataNavigateUrlFields="OrderId" HeaderText="Order ID" />
                            </Columns>

                        </asp:GridView>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

