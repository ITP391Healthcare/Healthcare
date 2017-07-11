<%@ Page Title="" Language="C#" MasterPageFile="~/ConsoleStaff.Master" AutoEventWireup="true" CodeBehind="ViewAcceptedReport.aspx.cs" Inherits="MomoSecretSociety.Content.StaffConsole.TestDisplay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    
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

    <div class="jumbotron" style="background-color: white;">

        <asp:Label ID="Label3" runat="server" Text="Date: "></asp:Label>
        <asp:Label ID="Label4" runat="server" Text="" Font-Bold="true"></asp:Label>
        <asp:TextBox ID="TextBox3" runat="server" Font-Bold="true" ReadOnly="true" Visible="false"></asp:TextBox>
        <br />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="TextBox3" CssClass="text-danger" ErrorMessage="The text field is required." />
        <br />

        <asp:Label ID="Label5" runat="server" Text="From: "></asp:Label>
        <asp:Label ID="Label6" runat="server" Text="" Font-Bold="true"></asp:Label>
        <asp:TextBox ID="TextBox5" runat="server" Font-Bold="true" ReadOnly="true" Visible="false"></asp:TextBox>
        <br />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="TextBox5" CssClass="text-danger" ErrorMessage="The text field is required." />
        <br />

        <asp:Label ID="Label7" runat="server" Text="Subject: "></asp:Label>
        <asp:Label ID="Label8" runat="server" Text="" Font-Bold="true"></asp:Label>
        <asp:TextBox ID="TextBox7" runat="server" Font-Bold="true" ReadOnly="true" Visible="false"></asp:TextBox>
        <br />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="TextBox7" CssClass="text-danger" ErrorMessage="The text field is required." />
        <br />


        <asp:Label ID="Label9" runat="server" Text="Case Description: "></asp:Label><br />
        <asp:Label ID="Label10" runat="server" Text="" Font-Bold="true"></asp:Label>
        <asp:TextBox ID="TextBox9" runat="server" Font-Bold="true" ReadOnly="true" Visible="false" Height="240px" Width="562px" TextMode="MultiLine"></asp:TextBox><br />
        <br />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="TextBox9" CssClass="text-danger" ErrorMessage="The text field is required." />
        <br />
        <asp:Label ID="Label11" runat="server" Text="Remarks: "></asp:Label><br />
        <asp:Label ID="Label12" runat="server" Text="" Font-Bold="true"></asp:Label><br />
    </div>

    <div>
        <asp:Button ID="btnSaveAsPDF" runat="server" Text="Save Report" OnClick="btnSaveAsPDF_Click" />
        <asp:Button ID="Button2" runat="server" Text="Submit" />
    </div>
</asp:Content>
