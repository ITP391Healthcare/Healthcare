<%@ Page Title="" Language="C#" MasterPageFile="~/ConsoleBoss.Master" AutoEventWireup="true" CodeBehind="PendingReports.aspx.cs" Inherits="MomoSecretSociety.Content.BossConsole.PendingReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ConsoleBoss_MainContent" runat="server">

    <link href="../../Styles/BossConsole/Reports.css" rel="stylesheet" />

    <style>
        th, tr, td {
            padding-left: 8px;
            padding-right: 8px;
            text-align: center;
        }
    </style>


    <!-- Pop up Modal -->
    <div class="modal fade" id="myModal" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <h3 class="modal-title" style="text-align: center; font-weight: bold;">Session Timeout</h3>
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
                    <asp:Button ID="btnAuthenticate" CssClass="btn btn-default" runat="server" Text="Authenticate" OnClick="btnAuthenticate_Click" CausesValidation="false" />
                    <%--  <asp:Button ID="btnLogout" class="btn btn-default" runat="server" Text="Logout" PostBackUrl="~/Account/Login.aspx" />--%>

                    <br />
                </div>
            </div>
        </div>
    </div>


    <div>
        <table class="table" style="border-collapse: initial !important; border-spacing: 0;">
            <tr style="height: 5%;">
                <td style="background-color: #146882; color: #b1e3ed; font-family: 'Palatino Linotype'; font-size: 25px; padding: 4% 4%;">Pending Reports
                          
                    <br />
                </td>

                <%-- Bookmark --%>
                <div style="z-index: 5; position: absolute; margin-left: 60%; padding-top: 1.3%;">
                    <div id="ribbon-head">
                    </div>
                    <div id="ribbon-tail">
                        <div id="left"></div>
                        <div id="right"></div>
                    </div>
                </div>
            </tr>

            <tr>
                <td>

                    <br>

                    <div style="padding: 10px; background-color: #f4f6f5;">

                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ConnectionStrings:FileDatabaseConnectionString2 %>"
                            SelectCommand="SELECT [Username], [CaseNumber], [Subject] FROM [Report]
        WHERE (ReportStatus = 'pending');"></asp:SqlDataSource>

                        <asp:GridView ID="GridView1" runat="server" BorderColor="#F0F0F0" HeaderStyle-BackColor="#146882" RowStyle-BackColor="#f3f3f3" RowStyle-Font-Size="Medium"
                            HeaderStyle-HorizontalAlign="Center" CellPadding="15" Font-Names="Helvetica"
                            HeaderStyle-ForeColor="White" HeaderStyle-Wrap="true" RowStyle-BorderColor="white"
                            RowStyle-HorizontalAlign="Center" PageSize="5" AllowPaging="True" DataSourceID="SqlDataSource1" Width="100%" AutoGenerateColumns="false" OnRowCommand="GridView1_RowCommand" DataKeyNames="Username">

                            <PagerStyle CssClass="pagerStyle" />

                            <Columns>
                                <asp:TemplateField HeaderText="Case Number">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="link" OnClick="link_Click"
                                            CommandArgument='<%# Eval("Username") + "," + Eval("CaseNumber")%>' CommandName="DataCommand"
                                            Text='<%# Eval("CaseNumber") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="Username" HeaderText="Username" />
                                <asp:BoundField DataField="Subject" HeaderText="Subject" />
                            </Columns>


                        </asp:GridView>

                    </div>
                </td>
            </tr>
        </table>
    </div>




</asp:Content>
