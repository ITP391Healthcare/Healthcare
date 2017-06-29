<%@ Page Title="" Language="C#" MasterPageFile="~/ConsoleStaff.Master" AutoEventWireup="true" CodeBehind="ViewSelectedReport.aspx.cs" Inherits="MomoSecretSociety.Content.StaffConsole.ViewSelectedReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%-- Report --%>        
    <asp:Label ID="Label1" runat="server" Text="You are currently viewing Report of #" Font-Size="30px" Font-Bold="True"></asp:Label>
    <asp:Label ID="Label2" runat="server" Text="" Font-Size="30px" Font-Bold="True"></asp:Label>
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
                <asp:Label ID="Label12" runat="server" Text="" Font-Bold="true"></asp:Label>
            </td>
        </tr>


    </table>
</asp:Content>
