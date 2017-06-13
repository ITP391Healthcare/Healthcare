<%@ Page Title="" Language="C#" MasterPageFile="~/ConsoleStaff.Master" AutoEventWireup="true" CodeBehind="NewReport.aspx.cs" Inherits="MomoSecretSociety.Content.StaffConsole.NewReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3 style="text-align: center"><b>- NEW REPORT -</b></h3>
    <div class="jumbotron" style="background-color: white;">
<%--    <asp:Label ID="Label1" runat="server" Text="Case Number: "></asp:Label>
        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
        <br />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="TextBox5" CssClass="text-danger" ErrorMessage="The text field is required." />
        <br />--%>

        <asp:Label ID="Label2" runat="server" Text="Date: "></asp:Label>
        <asp:TextBox ID="TextBox4" runat="server" TextMode="Date"></asp:TextBox>
        <br />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="TextBox4" CssClass="text-danger" ErrorMessage="The text field is required." />
        <br />

        <asp:Label ID="Label3" runat="server" Text="From: "></asp:Label>
        <asp:TextBox ID="TextBox3" runat="server" ReadOnly="True"></asp:TextBox>
        <br />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="TextBox3" CssClass="text-danger" ErrorMessage="The text field is required." />
        <br />

        <asp:Label ID="Label4" runat="server" Text="Subject: "></asp:Label>
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <br />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="TextBox2" CssClass="text-danger" ErrorMessage="The text field is required." />
        <br />

        <asp:Label ID="Label5" runat="server" Text="Case Description: "></asp:Label>
        <br />
        <asp:TextBox ID="TextBox1" runat="server" Height="240px" Width="562px" TextMode="MultiLine"></asp:TextBox>
        <br />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="TextBox1" CssClass="text-danger" ErrorMessage="The text field is required." />
        <br />
        <asp:Label ID="Label6" runat="server" Text="Remarks (for higherups)"></asp:Label>
        <br />
        <asp:TextBox ID="TextBox5" runat="server" Height="90px" ReadOnly="True" TextMode="MultiLine" Width="558px"></asp:TextBox>
        <br />

        <asp:Button ID="Button1" runat="server" Text="Save as drafts" OnClick="SaveAsDraftsButton_Click" />
        <asp:Button ID="Button2" runat="server" OnClick="SubmitButton_Click" Text="Submit" />
    </div>

</asp:Content>
