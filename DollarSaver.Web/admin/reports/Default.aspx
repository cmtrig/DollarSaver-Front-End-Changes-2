<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" Title="DollarSaver - Reports" CodeBehind="Default.aspx.cs" Inherits="DollarSaver.Web.Admin.Reports.Default" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="row">
        <div class="col-md-8">
            <div class="card">
                <div class="card-header card-header-icon" data-background-color="green">
                    <i class="material-icons">developer_board</i>
                </div>
                <div class="card-content">
                    <h4 class="card-title">
                        <input type="hidden" id="idHidden" runat="server" />
                        Reports
                    </h4>

                        <h5>Stats</h5>

                        <div class="row">
                            <label class="col-md-3">
                                Certificate Total Page Hits</label>
                            <div class="col-md-9">
                                <div>
                                    <asp:HyperLink ID="statsLink" Text="Run" NavigateUrl="~/admin/reports/SiteStats.aspx" runat="server" />
                                </div>
                            </div>
                        </div>
                        
                        <asp:Repeater ID="reportTypeRepeater" runat="server">
                            <HeaderTemplate>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <h5>
                                    <asp:Label ID="reportTypeNameLabel" runat="server" />
                                </h5>
                                <asp:Repeater ID="reportRepeater" runat="server">
                                    <ItemTemplate>

                                        <div class="row">
                                            <label class="col-md-3">
                                                <asp:Label ID="reportLabel" runat="server" /></label>
                                            <div class="col-md-9">
                                                <div>
                                                    <asp:HyperLink ID="runReportLink" Text="Run" runat="server" />
                                                </div>
                                            </div>
                                        </div>

                                    </ItemTemplate>
                                </asp:Repeater>
                            </ItemTemplate>
                            <FooterTemplate>
                            </FooterTemplate>
                        </asp:Repeater>


                    </div>
            </div>
        </div>
    </div>

</asp:Content>
