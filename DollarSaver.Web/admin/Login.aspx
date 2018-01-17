<%@ Page Language="C#" AutoEventWireup="true" Inherits="DollarSaver.Web.Admin.Login" Title="DollarSaver - Admin Login" CodeBehind="Login.aspx.cs" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta content='width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0' name='viewport' />
    <meta name="viewport" content="width=device-width" />

    <title>DollarSaver - Admin login</title>

    <link href="~/admin/styles/calendar.css" rel="stylesheet" />
    <link href="https://ajax.aspnetcdn.com/ajax/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="assets/css/ds-material-dashboard.css" rel="stylesheet" />
    <link href="assets/css/admin.css" rel="stylesheet" />


    <!--     Fonts and icons     -->
    <link href="http://maxcdn.bootstrapcdn.com/font-awesome/latest/css/font-awesome.min.css" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700|Material+Icons" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
</head>

<body>
    <form id="loginForm" defaultfocus="usernameBox" runat="server">
        <div class="wrapper wrapper-full-page">
            <div class="full-page login-page" filter-color="black">
                <%--you can change the color of the filter page using: data-color="blue | purple | green | orange | red | rose "--%>
                <div class="content">
                    <div class="container">
                        <div class="row">
                            <div class="col-md-4 col-sm-6 col-md-offset-4 col-sm-offset-3">

                                <div class="card card-login card-hidden">
                                    <div class="card-header text-center" data-background-color="green">
                                        <h4 class="card-title">DollarSaver Login</h4>
                                    </div>
                                    <div class="card-content">
                                        <asp:PlaceHolder ID="errorMessageHolder" runat="server">
                                            <asp:Label ID="errorMessageLabel" runat="server" class="text-danger" />
                                        </asp:PlaceHolder>

                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="material-icons">face</i>
                                            </span>
                                            <div class="form-group label-floating">
                                                <label class="control-label">User Name</label>
                                                <asp:TextBox ID="usernameBox" runat="server" CssClass="form-control" />
                                                <asp:RequiredFieldValidator ID="usernameBoxRFV" ControlToValidate="usernameBox" Text="*" runat="server" />
                                            </div>
                                        </div>
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="material-icons">lock_outline</i>
                                            </span>
                                            <div class="form-group label-floating">
                                                <label class="control-label">Password</label>
                                                <asp:TextBox TextMode="Password" ID="passwordBox" runat="server" CssClass="form-control" />
                                                <asp:RequiredFieldValidator ID="passwordBoxRFV" ControlToValidate="passwordBox" Text="*" runat="server" />
                                            </div>
                                        </div>
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="material-icons">settings_input_antenna</i>
                                            </span>
                                            <div class="form-group label-floating">
                                                <label class="control-label">Station</label>
                                                <asp:TextBox ID="stationCodeBox" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="footer text-center">
                                        <asp:Button ID="loginButton" Text="Let's go" runat="server" CssClass="btn btn-primary btn-simple btn-wd btn-lg" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script type="text/javascript" src="<%=ResolveUrl("~/admin/js/calendar.js") %>"></script>

    <script type="text/javascript" src="https://ajax.aspnetcdn.com/ajax/jquery/jquery-3.2.1.min.js"></script>
    <script type="text/javascript" src="https://ajax.aspnetcdn.com/ajax/bootstrap/3.3.7/bootstrap.min.js"></script>

    <script type="text/javascript" src="assets/js/material.min.js"></script>
    <script type="text/javascript" src="assets/js/perfect-scrollbar.jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/core-js/2.4.1/core.js"></script>
    <script type="text/javascript" src="assets/js/arrive.min.js"></script>
    <script type="text/javascript" src="assets/js/jquery-validation/jquery.validate.min.js"></script>
    <script type="text/javascript" src="assets/js/jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js"></script>
    <script type="text/javascript" src="assets/js/moment.min.js"></script>
    <script type="text/javascript" src="assets/js/chartist.min.js"></script>
    <script type="text/javascript" src="assets/js/jquery.bootstrap-wizard.min.js"></script>
    <script type="text/javascript" src="assets/js/bootstrap-notify.min.js"></script>
    <script type="text/javascript" src="assets/js/bootstrap-datetimepicker.min.js"></script>
    <script type="text/javascript" src="assets/js/jquery-jvectormap.min.js"></script>
    <script type="text/javascript" src="assets/js/nouislider.min.js"></script>
    <script type="text/javascript" src="assets/js/jquery.select-bootstrap.min.js"></script>
    <script type="text/javascript" src="assets/js/jquery.datatables.min.js"></script>
    <script type="text/javascript" src="assets/js/sweetalert2.min.js"></script>
    <script type="text/javascript" src="assets/js/jasny-bootstrap.min.js"></script>
    <script type="text/javascript" src="assets/js/fullcalendar.min.js"></script>
    <script type="text/javascript" src="assets/js/jquery.tagsinput.min.js"></script>
    <script type="text/javascript" src="assets/js/material-dashboard.min.js?v=1.2.1"></script>
    <script type="text/javascript" src="assets/js/admin.js"></script>

    <script type="text/javascript">
        $().ready(function () {

            setTimeout(function () {
                // after 1000 ms we add the class animated to the login/register card
                $('.card').removeClass('card-hidden');
            }, 700)
        });
    </script>

</body>

</html>
