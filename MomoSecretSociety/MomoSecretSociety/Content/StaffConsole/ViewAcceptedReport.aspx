<%@ Page Title="" Language="C#" MasterPageFile="~/ConsoleStaff.Master" AutoEventWireup="true" CodeBehind="ViewAcceptedReport.aspx.cs" Inherits="MomoSecretSociety.Content.StaffConsole.TestDisplay" %>

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



    <br />
    <asp:Label ID="Label1" runat="server" Text="- REPORT #" Font-Size="30px" Font-Bold="True"></asp:Label>
    <asp:Label ID="Label2" runat="server" Text="" Font-Size="30px" Font-Bold="True"></asp:Label>

    <table class="jumbotron" style="background-color: white; border-collapse: initial; border-spacing: 0; padding-left: 12%; padding-right: 12%; font-size: medium">
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Date: "></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label4" runat="server" Text="" Font-Bold="true"></asp:Label>
           
                <asp:TextBox ID="TextBox3" runat="server" Font-Bold="true" ReadOnly="true" Visible="false" ></asp:TextBox><br />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="TextBox3" CssClass="text-danger" ErrorMessage="The text field is required." Display="Dynamic" />
            </td>
        </tr>

        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="From: "></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label6" runat="server" Text="" Font-Bold="true"></asp:Label><%--<br />--%>
                <asp:TextBox ID="TextBox5" runat="server" Font-Bold="true" ReadOnly="true" Visible="false"></asp:TextBox>
                <%--<br />--%>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="TextBox5" CssClass="text-danger" ErrorMessage="The text field is required." Display="Dynamic"  />
                <%--<br />--%>
            </td>
        </tr>

        <tr>
            <td>
                <asp:Label ID="Label7" runat="server" Text="Subject: "></asp:Label>
            </td>
            <td style="padding-bottom: 2%;">
                <asp:Label ID="Label8" runat="server" Text="" Font-Bold="true"></asp:Label>
                <asp:TextBox ID="TextBox7" runat="server" Font-Bold="true" ReadOnly="true" Visible="false"></asp:TextBox>
                <%--<br />--%>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="TextBox7" CssClass="text-danger" ErrorMessage="The text field is required." Display="Dynamic"  />
                <%--<br />--%>
            </td>
        </tr>

        <tr style="vertical-align: top;">
            <td>
                <asp:Label ID="Label9" runat="server" Text="Case Description: "></asp:Label><%--<br />--%>
            </td>
            <td style="min-width: 700px !important; line-height: 120% !important;">
                <asp:Label ID="Label10" runat="server" Text="" Font-Bold="true"></asp:Label>
                <asp:TextBox ID="TextBox9" runat="server" Font-Bold="true" ReadOnly="true" Visible="false" Height="240px" Width="562px" TextMode="MultiLine"></asp:TextBox><br />
                <%--<br />--%>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="TextBox9" CssClass="text-danger" ErrorMessage="The text field is required." Display="Dynamic" />
                <%--<br />--%>
            </td>
        </tr>
        <tr>
            <td style="padding-top: 2%; vertical-align: top;">
                <asp:Label ID="Label11" runat="server" Text="Remarks: "></asp:Label><br />
            </td>
            <td style="padding-top: 2%;">
                <asp:Label ID="Label12" runat="server" Text="" Font-Bold="true"></asp:Label><br />
            </td>
        </tr>
        <tr>
            <td style="padding-top: 2%; vertical-align: top;">
                <asp:Label ID="Label13" runat="server" Text="Password for PDF: "></asp:Label>
            </td>
            <td style="padding-top: 2%; vertical-align: top;">
                <asp:TextBox ID="PasswordTxt" runat="server" TextMode="Password"></asp:TextBox><br />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="PasswordTxt" CssClass="text-danger" ErrorMessage="Password is required for your saved PDF." Display="Dynamic" />
            </td>
        </tr>

        <tr>
            <td></td>
            <td>
                <asp:Button ID="btnSaveAsPDF" runat="server" Text="Save Report" OnClick="btnSaveAsPDF_Click" />
                <asp:Button ID="btnReSubmitRpt" runat="server" Text="Submit" OnClick="btnReSubmitRpt_Click" />
            </td>
        </tr>

    </table>
</asp:Content>
