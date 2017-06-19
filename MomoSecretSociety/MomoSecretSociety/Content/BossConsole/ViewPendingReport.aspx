<%@ Page Title="" Language="C#" MasterPageFile="~/ConsoleBoss.Master" AutoEventWireup="true" CodeBehind="ViewPendingReport.aspx.cs" Inherits="MomoSecretSociety.Content.BossConsole.ViewPendingReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ConsoleBoss_MainContent" runat="server">

    <style>
        .button-link {
            text-shadow: none;
            border-radius: 3px;
            border-color: #19c589;
            color: #fff;
            font: 13px/16px "lft-etica-n4", "lft-etica", Arial, sans-serif;
            padding: 8px 17px;
            background-color: #19c589;
        }

            .button-link:hover {
                opacity: 0.7;
                text-decoration: none;
            }
    </style>

    <div class="modal fade" id="myModal" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <h3 class="modal-title" style="text-align: center; font-weight: bold;">Your account has been locked</h3>
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


    <asp:Label ID="Label1" runat="server" Text="- REPORT #" Font-Size="30px" Font-Bold="True"></asp:Label>
    <asp:Label ID="Label2" runat="server" Text="" Font-Size="30px" Font-Bold="True"></asp:Label>
    <asp:Label ID="Label2_2" runat="server" Text=" -" Font-Size="30px" Font-Bold="true"></asp:Label>

    <div class="jumbotron" style="background-color: white;">

        <asp:Label ID="Label3" runat="server" Text="Date: "></asp:Label>
        <asp:Label ID="Label4" runat="server" Text="" Font-Bold="true"></asp:Label><br />

        <asp:Label ID="Label5" runat="server" Text="From: "></asp:Label>
        <asp:Label ID="Label6" runat="server" Text="" Font-Bold="true"></asp:Label><br />

        <asp:Label ID="Label7" runat="server" Text="Subject: "></asp:Label>
        <asp:Label ID="Label8" runat="server" Text="" Font-Bold="true"></asp:Label><br />

        <asp:Label ID="Label9" runat="server" Text="Case Description: "></asp:Label><br />
        <asp:Label ID="Label10" runat="server" Text="" Font-Bold="true"></asp:Label><br />
        <br />

        <asp:Label ID="Label11" runat="server" Text="Remarks: "></asp:Label><br />
        <%--<asp:Label ID="Label12" runat="server" Text="" Font-Bold="true"></asp:Label><br />--%>
        <asp:TextBox TextMode="MultiLine" ID="Label12_remarks" runat="server"></asp:TextBox>

        <br />

        <asp:Button ID="Button_Approve" runat="server" CssClass="button-link" Text="Approve" OnClick="Button_Approve_Click"/>

        <asp:Button ID="Button_Reject" runat="server" CssClass="button-link" Text="Reject" OnClick="Button_Reject_Click" />

    </div>




</asp:Content>
