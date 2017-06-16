<%@ Page Title="" Language="C#" MasterPageFile="~/ConsoleBoss.Master" AutoEventWireup="true" CodeBehind="SuperiorLogs.aspx.cs" Inherits="MomoSecretSociety.Content.BossConsole.SuperiorLogs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ConsoleBoss_MainContent" runat="server">

         <!-- Pop up Modal -->
    <div class="modal fade" id="myModal" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <h3 class="modal-title" style="text-align: center; font-weight: bold;">Session Timeout</h3>
                </div>
                <div class="modal-body">
                    <h4>To prove that you are the user <b><%: Context.User.Identity.GetUserName()  %> </b>...</h4>
                    <p style="text-align: center;">Please enter your password in order to continue:</p>
                    <asp:TextBox ID="txtPasswordAuthenticate" runat="server" TextMode="Password" CssClass="textbox" Text=""></asp:TextBox>
                    <br />
                    <p style="text-align: center;">
                        <asp:Label ID="errormsgPasswordAuthenticate" runat="server" Text="Password is incorrect." Visible="false" ForeColor="#da337a"></asp:Label>
                    </p>
                </div>
                <div class="modal-footer" style="text-align: center;">
                    <asp:Button ID="btnAuthenticate" class="btn btn-default" runat="server" Text="Authenticate" OnClick="btnAuthenticate_Click" CausesValidation="false" />
                    <%--  <asp:Button ID="btnLogout" class="btn btn-default" runat="server" Text="Logout" PostBackUrl="~/Account/Login.aspx" />--%>

                    <br />
                </div>
            </div>
        </div>
    </div>

     <asp:Panel runat="server" ID="panel2">

        <div class="jumbotron" style="background-color: white; font-size: 14px; display: inline-block; height: calc(100%); width: calc(100%);">

            <h2>Boss's Logs</h2>

            <div class="col-md-9" style="height: calc(100%); width: calc(100%);">

                <div class="box box-primary">
                    <!-- The timeline -->
                    <div class="box-body">
                        <ul class="timeline timeline-inverse">
                            <asp:PlaceHolder ID="phTimeline" runat="server"></asp:PlaceHolder>

                            <li>
                                <i class="fa fa-clock-o bg-gray"></i>
                            </li>
                        </ul>
                    </div>
                </div>

            </div>
        </div>

    </asp:Panel>

</asp:Content>
