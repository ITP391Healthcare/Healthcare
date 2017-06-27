<%@ Page Title="" Language="C#" MasterPageFile="~/ConsoleStaff.Master" AutoEventWireup="true" CodeBehind="ViewSelectedReport.aspx.cs" Inherits="MomoSecretSociety.Content.StaffConsole.ViewSelectedReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%-- Report --%>
    <div class ="Report">
        <asp:Label ID="CaseNumber" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="Date" runat="server" Text=""></asp:Label>
        <br />  
        <asp:Label ID="Username" runat="server" Text=""></asp:Label>    
        <asp:Label ID="Subject" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="Status" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="Description" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="Remarks" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>
