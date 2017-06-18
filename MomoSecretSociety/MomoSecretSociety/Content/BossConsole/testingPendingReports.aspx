<%@ Page Title="" Language="C#" MasterPageFile="~/ConsoleBoss.Master" AutoEventWireup="true" CodeBehind="testingPendingReports.aspx.cs" Inherits="MomoSecretSociety.Content.BossConsole.testingPendingReports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ConsoleBoss_MainContent" runat="server">

                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ConnectionStrings:FileDatabaseConnectionString2 %>"
                            SelectCommand="SELECT [Username], [CaseNumber], [Subject] FROM [Report]
        WHERE (ReportStatus = 'pending');">
                        </asp:SqlDataSource>

                        <asp:GridView ID="GridView1" runat="server" BorderColor="#F0F0F0" HeaderStyle-BackColor="#2eb3ed" RowStyle-BackColor="#f3f3f3" RowStyle-Font-Size="Medium"
                            HeaderStyle-HorizontalAlign="Center" CellPadding="15" Font-Names="Helvetica"
                            HeaderStyle-ForeColor="White" HeaderStyle-Wrap="true" RowStyle-BorderColor="white" HorizontalAlign="Center"
                            RowStyle-HorizontalAlign="Center" PageSize="5" AllowPaging="True" DataSourceID="SqlDataSource1">

                            <PagerStyle CssClass="pagerStyle" />

<%--                            <Columns>
                                <asp:BoundField DataField="Username" HeaderText="Username"/>
                                <asp:BoundField DataField="CaseNumber" HeaderText="Case Number"/>
                                <asp:BoundField DataField="Subject" HeaderText="Subject"/>
                            </Columns>--%>


                        </asp:GridView>


</asp:Content>
