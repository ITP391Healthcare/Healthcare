<%@ Page Title="" Language="C#" MasterPageFile="~/ConsoleStaff.Site.Master" AutoEventWireup="true" CodeBehind="2FA.aspx.cs" Inherits="MomoSecretSociety.Account._2FA" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron" style="background-color: white;">
        <h1>ENTER THE CODE</h1>
        <p>Enter the code that was sent to your phone number.</p>
        <div class="form-group">

                Code: <asp:TextBox ID="TextBox1" runat="server" Height="40px" MaxLength="1" Width="35px"></asp:TextBox>
                <asp:TextBox ID="TextBox2" runat="server" Height="40px" MaxLength="1" Width="35px"></asp:TextBox>
                <asp:TextBox ID="TextBox3" runat="server" Height="40px" MaxLength="1" Width="35px"></asp:TextBox>
                <asp:TextBox ID="TextBox4" runat="server" Height="40px" MaxLength="1" Width="35px"></asp:TextBox>
                <br /><br /> 
                <asp:Button ID="Button1" runat="server" Text="Continue" Width="203px" />
        </div>
    </div>
</asp:Content>
