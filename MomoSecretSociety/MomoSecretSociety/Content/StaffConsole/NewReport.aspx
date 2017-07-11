<%@ Page Title="" Language="C#" MasterPageFile="~/ConsoleStaff.Master" AutoEventWireup="true" CodeBehind="NewReport.aspx.cs" Inherits="MomoSecretSociety.Content.StaffConsole.NewReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

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

      <!-- Pop up Modal -->
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
                    <asp:Button ID="btnAuthenticate" class="btn btn-default" runat="server" Text="Authenticate" OnClick="btnAuthenticate_Click" CausesValidation="false" />
                    <%--  <asp:Button ID="btnLogout" class="btn btn-default" runat="server" Text="Logout" PostBackUrl="~/Account/Login.aspx" />--%>

                    <br />
                </div>
            </div>
        </div>
    </div>



    <h3 style="text-align: center"><b>- NEW REPORT -</b></h3>
    <table class="jumbotron" style="background-color: white; border-collapse: initial; border-spacing: 0; padding-left: 12%; padding-right: 12%; font-size: medium">
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Date: " Font-Bold="true"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox4" runat="server" TextMode="Date" Font-Bold="true"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="TextBox4" CssClass="text-danger" ErrorMessage="The text field is required." />
            </td>
        </tr>

        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="From: " Font-Bold="true"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox3" runat="server" ReadOnly="True" Font-Bold="true"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="TextBox3" CssClass="text-danger" ErrorMessage="The text field is required." />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Text="Subject: " Font-Bold="true"></asp:Label>
            </td>
            <td style="padding-bottom: 2%;">
                <asp:TextBox ID="TextBox2" runat="server" Font-Bold="true"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="TextBox2" CssClass="text-danger" ErrorMessage="The text field is required." />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="Case Description: " Font-Bold="true"></asp:Label>
            </td>
            <td style="min-width: 700px !important; line-height: 120% !important;">
                <asp:TextBox ID="TextBox1" runat="server" Height="240px" Width="562px" TextMode="MultiLine" Font-Bold="true"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="TextBox1" CssClass="text-danger" ErrorMessage="The text field is required." />
            </td>
        </tr>
        <tr>
            <td style="padding-top: 2%; vertical-align: top;">
                <asp:Label ID="Label6" runat="server" Text="Remarks (for higherups)" Font-Bold="true"></asp:Label>
            </td>
            <td style="padding-top: 2%;">
                <asp:TextBox ID="TextBox5" runat="server" Height="90px" ReadOnly="True" TextMode="MultiLine" Width="558px"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td></td>
            <td>
                <asp:Button ID="Button1" runat="server" CssClass="button-linkReject" Text="Save as drafts" OnClick="SaveAsDraftsButton_Click" />
                <asp:Button ID="Button2" runat="server" CssClass="button-linkApprove" Text="Submit" OnClick="SubmitButton_Click" />
            </td>
        </tr>

    </table>

</asp:Content>
