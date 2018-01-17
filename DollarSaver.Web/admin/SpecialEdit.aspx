<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" Inherits="DollarSaver.Web.Admin.SpecialEdit" Title="DollarSaver - Edit Station Specials" CodeBehind="SpecialEdit.aspx.cs" %>

<%@ MasterType VirtualPath="~/admin/admin.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-header card-header-icon" data-background-color="green">
                    <i class="material-icons">timer</i>
                </div>
                <div class="card-content">
                    <h4 class="card-title">Edit Station Specials</h4>
                    <div class="form-horizontal">
                        <h3 class="text-center">DollarSaver Home Page Layout</h3>

                        <div class="row vertical-align">
                            <div class="col-md-6">
                                <h2>Saving up to 50% is as easy as 1-2-3!</h2>
                            </div>
                            <div class="col-md-6">
                                <div class="card">
                                    <h4 class="text-center">Daily/Weekly Special</h4>

                                    <div class="row">
                                        <label class="col-sm-3 label-on-left">Header</label>
                                        <div class="col-sm-8">
                                            <div class="form-group">
                                                <asp:DropDownList ID="dailyWeeklyList" CssClass="selectpicker" data-style="btn btn-primary" runat="server">
                                                    <asp:ListItem Value="1">Daily Special</asp:ListItem>
                                                    <asp:ListItem Value="2">Weekly Special</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <label class="col-sm-3 label-on-left">Certificate</label>
                                        <div class="col-sm-8">
                                            <div class="form-group">
                                                <asp:DropDownList ID="specialOneCertificateList" CssClass="selectpicker" data-style="btn btn-primary" runat="server" />
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>

                        <h3 class="text-center">Other Great DollarSaver Deals</h3>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="card">
                                    <h4 class="text-center">Special 2</h4>

                                    <div class="row">
                                        <label class="col-sm-3 label-on-left">Random Certificate</label>
                                        <div class="col-sm-9 checkbox-radios">
                                            <div class="radio">
                                                <label>
                                                    <asp:RadioButton ID="specialTwoRandomBox" GroupName="specialTwo" Checked="true" runat="server" />
                                                    <span class="check"></span>
                                                    <span class="circle"></span>

                                                </label>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="row">
                                        <label class="col-sm-3 label-on-left">Specific Certificate</label>
                                        <div class="col-sm-1 checkbox-radios">
                                            <div class="radio">
                                                <label>
                                                    <asp:RadioButton ID="specialTwoCertificateBox" GroupName="specialTwo" runat="server" />
                                                    <span class="check"></span>
                                                    <span class="circle"></span>
                                                </label>

                                            </div>
                                        </div>
                                        <div class="col-sm-7">
                                            <div class="form-group">
                                                <asp:DropDownList ID="specialTwoCertificateList" CssClass="selectpicker" data-style="btn btn-primary" runat="server" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <label class="col-sm-3 label-on-left">Random From Category</label>
                                        <div class="col-sm-1 checkbox-radios">
                                            <div class="radio">
                                                <label>
                                                    <asp:RadioButton ID="specialTwoCategoryBox" GroupName="specialTwo" runat="server" />
                                                    <span class="check"></span>
                                                    <span class="circle"></span>
                                                </label>

                                            </div>
                                        </div>
                                        <div class="col-sm-7">
                                            <div class="form-group">
                                                <asp:DropDownList ID="specialTwoCategoryList" CssClass="selectpicker" data-style="btn btn-primary" runat="server" />
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="card">
                                    <h4 class="text-center">Special 3</h4>

                                    <div class="row">
                                        <label class="col-sm-3 label-on-left">Random Certificate</label>
                                        <div class="col-sm-9 checkbox-radios">
                                            <div class="radio">
                                                <label>
                                                    <asp:RadioButton ID="specialThreeRandomBox" GroupName="specialThree" Checked="true" runat="server" />
                                                    <span class="check"></span>
                                                    <span class="circle"></span>
                                                </label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <label class="col-sm-3 label-on-left">Specific Certificate</label>
                                        <div class="col-sm-1 checkbox-radios">
                                            <div class="radio">
                                                <label>
                                                    <asp:RadioButton ID="specialThreeCertificateBox" GroupName="specialThree" runat="server" />
                                                    <span class="check"></span>
                                                    <span class="circle"></span>
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-sm-7">
                                            <div class="form-group">
                                                <asp:DropDownList ID="specialThreeCertificateList" CssClass="selectpicker" data-style="btn btn-primary" runat="server" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <label class="col-sm-3 label-on-left">Random From Category</label>
                                        <div class="col-sm-1 checkbox-radios">
                                            <div class="radio">
                                                <label>
                                                    <asp:RadioButton ID="specialThreeCategoryBox" GroupName="specialThree" runat="server" />
                                                    <span class="check"></span>
                                                    <span class="circle"></span>
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-sm-7">
                                            <div class="form-group">
                                                <asp:DropDownList ID="specialThreeCategoryList" CssClass="selectpicker" data-style="btn btn-primary" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row vertical-align">
                            <div class="col-md-6">
                                <div class="card">
                                    <h4 class="text-center">Special 4</h4>

                                    <div class="row">
                                        <label class="col-sm-3 label-on-left">Random Certificate</label>
                                        <div class="col-sm-9 checkbox-radios">
                                            <div class="radio">
                                                <label>
                                                    <asp:RadioButton ID="specialFourRandomBox" GroupName="specialFour" Checked="true" runat="server" />
                                                    <span class="check"></span>
                                                    <span class="circle"></span>
                                                </label>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="row">
                                        <label class="col-sm-3 label-on-left">Specific Certificate</label>
                                        <div class="col-sm-1 checkbox-radios">
                                            <div class="radio">
                                                <label>
                                                    <asp:RadioButton ID="specialFourCertificateBox" GroupName="specialFour" runat="server" />
                                                    <span class="check"></span>
                                                    <span class="circle"></span>
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-sm-7">
                                            <div class="form-group">
                                                <asp:DropDownList ID="specialFourCertificateList" CssClass="selectpicker" data-style="btn btn-primary" runat="server" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <label class="col-sm-3 label-on-left">Random From Category</label>
                                        <div class="col-sm-1 checkbox-radios">
                                            <div class="radio">
                                                <label>
                                                    <asp:RadioButton ID="specialFourCategoryBox" GroupName="specialFour" runat="server" />
                                                    <span class="check"></span>
                                                    <span class="circle"></span>
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-sm-7">
                                            <div class="form-group">
                                                <asp:DropDownList ID="specialFourCategoryList" CssClass="selectpicker" data-style="btn btn-primary" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <h2>Station Details</h2>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-4 col-sm-offset-8 text-right">
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
