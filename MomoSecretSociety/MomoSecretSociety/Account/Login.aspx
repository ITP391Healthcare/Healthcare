<%@ Page Title="" Language="C#" MasterPageFile="~/ConsoleStaff.Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MomoSecretSociety.Account.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div class="jumbotron" style="background-color: white;">
        <h1>PLEASE LOGIN AS A STAFF/BOSS</h1>
        <div class="row">
            <div class="col-md-8">
                <section id="loginForm">
                    <div class="form-horizontal">
                        <h4>Use a local account to log in.</h4>
                        <hr />
                        <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                            <p class="text-danger">
                                <asp:Literal runat="server" ID="FailureText" />
                            </p>
                        </asp:PlaceHolder>
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="username" CssClass="col-md-2 control-label">Username</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="username" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="username"
                                    CssClass="text-danger" ErrorMessage="The text field is required." />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label">Password</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="The password field is required." />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-offset-2 col-md-10">
                                <asp:Button runat="server" OnClick="LogIn" Text="Log in" CssClass="btn btn-default" />
                            </div>
                        </div>
                    </div>
                </section>
            </div>

        </div>
    </div>


</asp:Content>
