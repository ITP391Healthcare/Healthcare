<%@ Page Title="" Language="C#" MasterPageFile="~/ConsoleStaff.Master" AutoEventWireup="true" CodeBehind="SubmittedReports.aspx.cs" Inherits="MomoSecretSociety.Content.StaffConsole.SubmittedReports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%--    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><br />
    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox><br />
    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" style="height: 26px" /><br />
    <asp:Table ID="Table1" runat="server">
    </asp:Table>--%>

    <asp:SqlDataSource ID ="SqlDataSource1" runat="server" ConnectionString="<%$ConnectionStrings:FileDatabaseConnectionString2 %>"
        SelectCommand="SELECT [CaseNumber], [CaseNumber], [Date], [Subject], [ReportStatus], [CreatedDateTime] FROM [Report]
        WHERE ([Username] = @Username);">
        <SelectParameters>
            <asp:SessionParameter Name="Username" SessionField="AccountUsername" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:GridView ID ="GridView1" CssClass="myDataGrid" HeaderStyle-CssClass="header" runat="server" DataSourceID ="SqlDataSource1" 
        AutoGenerateColumns="false">
        
        <%-- If There are no reports --%>
        <EmptyDataTemplate>
            <label style ="color: red; font-weight: bold; font-size: 30px;"> There are no reports at the moment</label>
        </EmptyDataTemplate>
        <Columns>
            <asp:BoundField DataField="CaseNumber" HeaderText="Case Number" ItemStyle-Width="200" />
            <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-Width="200" />
            <asp:BoundField DataField="Subject" HeaderText="Subject" ItemStyle-Width="200" />
            <asp:BoundField DataField="ReportStatus" HeaderText="Report Status" ItemStyle-Width="200" />
            <asp:BoundField DataField="CreatedDateTime" HeaderText="Created Date Time" ItemStyle-Width="200" />
        </Columns>
    </asp:GridView>

</asp:Content>
