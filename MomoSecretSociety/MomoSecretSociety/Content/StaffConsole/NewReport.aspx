<%@ Page Title="" Language="C#" MasterPageFile="~/ConsoleStaff.Master" AutoEventWireup="true" CodeBehind="NewReport.aspx.cs" Inherits="MomoSecretSociety.Content.StaffConsole.NewReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3 style= "text-align: center"><b>- NEW REPORT -</b></h3>
    <div class="jumbotron" style="background-color: white;">
        <asp:Label ID="Label1" runat="server" Text="Case Number: "></asp:Label>
        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
        <br />
        
        <asp:Label ID="Label2" runat="server" Text="Date: "></asp:Label>
        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
        <br />
        
        <asp:Label ID="Label3" runat="server" Text="From: "></asp:Label>
        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        <br />
        
        <asp:Label ID="Label4" runat="server" Text="Subject: "></asp:Label>
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <br />
        
        <asp:Label ID="Label5" runat="server" Text="Case Description: "></asp:Label>
        <br />
        <asp:TextBox ID="TextBox1" runat="server" Height="240px" Width="562px"></asp:TextBox>
    </div>

</asp:Content>
