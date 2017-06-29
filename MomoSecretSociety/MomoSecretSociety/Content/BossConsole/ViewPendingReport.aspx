<%@ Page Title="" Language="C#" MasterPageFile="~/ConsoleBoss.Master" AutoEventWireup="true" CodeBehind="ViewPendingReport.aspx.cs" Inherits="MomoSecretSociety.Content.BossConsole.ViewPendingReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ConsoleBoss_MainContent" runat="server">

    <style>
        .button-linkReject, .button-linkApprove {
            text-shadow: none;
            border-radius: 3px;
            border-color: #19c589;
            color: #fff;
            font: 13px/16px "lft-etica-n4", "lft-etica", Arial, sans-serif;
            padding: 8px 17px;
            background-color: #19c589;
            float: right;
        }

            .button-linkReject:hover, .button-linkApprove:hover {
                opacity: 0.7;
                text-decoration: none;
            }

        .button-linkReject {
            margin-top: 1%;
        }

        .button-linkApprove {
            margin-top: 1%;
        }
    </style>


    <style>
        .table tbody > tr > td {
            border-top: none;
        }

        td {
            min-width: 200px;
        }

        textarea.textbox_remarks {
            border: 1px solid #c4c4c4;
            /*width: 180px;*/
            width: 700px;
            font-size: 13px;
            padding: 3px;
            border-radius: 4px;
            -moz-border-radius: 4px;
            -webkit-border-radius: 4px;
            box-shadow: 0px 0px 8px #d9d9d9;
            -moz-box-shadow: 0px 0px 8px #d9d9d9;
            -webkit-box-shadow: 0px 0px 8px #d9d9d9;
        }

            textarea.textbox_remarks:focus {
                outline: none;
                border: 1px solid lightblue;
                box-shadow: 0px 0px 8px lightblue;
                -moz-box-shadow: 0px 0px 8px lightblue;
                -webkit-box-shadow: 0px 0px 8px lightblue;
            }


        .label_caseDesc {
            width: 500px !important;
        }

        .label_subject {
            border-bottom: 2px solid lightblue;
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

    <%--    <marquee hspace="40%" style="text-align: center;">--%>
    <div class="header" style="text-align: center; margin-top: 2%; text-shadow: 2px 2px beige;">
        <asp:Label ID="Label1" runat="server" Text="You are currently viewing Report of #" Font-Size="30px" Font-Bold="True"></asp:Label>
        <asp:Label ID="Label2" runat="server" Text="" Font-Size="30px" Font-Bold="True"></asp:Label>
        <%--<asp:Label ID="Label2_2" runat="server" Text=" -" Font-Size="30px" Font-Bold="true"></asp:Label>--%>
    </div>
    <%--    </marquee>--%>

    <table class="jumbotron" style="background-color: white; border-collapse: initial; border-spacing: 0; padding-left: 12%; padding-right: 12%; font-size: medium">
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Date: "></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label4" runat="server" Text="" Font-Bold="true"></asp:Label><br />
            </td>
        </tr>

        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="From: "></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label6" runat="server" Text="" Font-Bold="true"></asp:Label><br />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label7" runat="server" Text="Subject: "></asp:Label>
            </td>
            <td style="padding-bottom: 2%;">
                <asp:Label ID="Label8" runat="server" Text="" Font-Bold="true" CssClass="label_subject"></asp:Label><br />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label9" runat="server" Text="Case Description: "></asp:Label><br />
            </td>
            <td style="min-width: 700px !important; line-height: 120% !important;">

                <asp:Label ID="Label10" runat="server" Text="" CssClass="label_caseDesc" Font-Bold="true"></asp:Label><br />

            </td>
        </tr>
        <tr>
            <td style="padding-top: 2%; vertical-align: top;">
                <asp:Label ID="Label11" runat="server" Text="Remarks: "></asp:Label><br />
                <%--<asp:Label ID="Label12" runat="server" Text="" Font-Bold="true"></asp:Label><br />--%>
            </td>
            <td style="padding-top: 2%;">
                <asp:TextBox TextMode="MultiLine" ID="Label12_remarks" runat="server" CssClass="textbox_remarks"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td></td>
            <td>
                <asp:Button ID="Button_Reject" runat="server" CssClass="button-linkReject" Text="Reject" OnClick="Button_Reject_Click" />

                <asp:Button ID="Button_Approve" runat="server" CssClass="button-linkApprove" Text="Approve" OnClick="Button_Approve_Click" />
            </td>
        </tr>

    </table>




</asp:Content>
