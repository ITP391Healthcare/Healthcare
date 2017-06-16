<%@ Page Title="" Language="C#" MasterPageFile="~/ConsoleBoss.Master" AutoEventWireup="true" CodeBehind="PendingReports.aspx.cs" Inherits="MomoSecretSociety.Content.BossConsole.PendingReports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ConsoleBoss_MainContent" runat="server">

    <link href="../../Styles/BossConsole/Reports.css" rel="stylesheet" />


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
                    <asp:Button ID="btnAuthenticate" CssClass="btn btn-default" runat="server" Text="Authenticate" OnClick="btnAuthenticate_Click" CausesValidation="false" />
                    <%--  <asp:Button ID="btnLogout" class="btn btn-default" runat="server" Text="Logout" PostBackUrl="~/Account/Login.aspx" />--%>

                    <br />
                </div>
            </div>
        </div>
    </div>


      <div>
        <table class="table" style="border-collapse: initial !important; border-spacing: 0;">
            <tr style="height: 5%;">
                <td style="background-color: #146882; color: #b1e3ed; font-family: 'Palatino Linotype'; font-size: 25px; padding: 4% 4%;">Pending Reports
                          
                    <br />
                </td>

                <%-- Bookmark --%>
                <div style="z-index: 5; position: absolute; margin-left: 60%; padding-top: 1.3%;">
                    <div id="ribbon-head">
                    </div>
                    <div id="ribbon-tail">
                        <div id="left"></div>
                        <div id="right"></div>
                    </div>
                </div>
            </tr>

            <tr>
                <td>
                    
            <br>
                    <div style="padding: 10px; background-color: #f4f6f5; height: 250px;">

                        </div>
                    </td>
            </tr>
        </table>
    </div>




</asp:Content>
