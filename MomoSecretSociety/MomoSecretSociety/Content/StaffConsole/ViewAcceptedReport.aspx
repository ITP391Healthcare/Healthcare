﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ConsoleStaff.Master" AutoEventWireup="true" CodeBehind="ViewAcceptedReport.aspx.cs" Inherits="MomoSecretSociety.Content.StaffConsole.TestDisplay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron" style="background-color: white;">
        <asp:Label ID="Label1" runat="server" Text="Report #" style="text-decoration:underline"></asp:Label><asp:Label ID="Label2" runat="server" Text="" style="text-decoration:underline"></asp:Label><br />

        <asp:Label ID="Label3" runat="server" Text="Date: "></asp:Label>
        <asp:Label ID="Label4" runat="server" Text=""></asp:Label><br />

        <asp:Label ID="Label5" runat="server" Text="From: "></asp:Label>
        <asp:Label ID="Label6" runat="server" Text=""></asp:Label><br />

        <asp:Label ID="Label7" runat="server" Text="Subject: "></asp:Label>
        <asp:Label ID="Label8" runat="server" Text=""></asp:Label><br />

        <asp:Label ID="Label9" runat="server" Text="Case Description: "></asp:Label><br />
        <asp:Label ID="Label10" runat="server" Text=""></asp:Label><br />

        <asp:Label ID="Label11" runat="server" Text="Remarks: "></asp:Label>
        <asp:Label ID="Label12" runat="server" Text=""></asp:Label><br />
    </div>
</asp:Content>