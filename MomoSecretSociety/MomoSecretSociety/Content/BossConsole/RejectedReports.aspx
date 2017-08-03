<%@ Page Title="" Language="C#" MasterPageFile="~/ConsoleBoss.Master" AutoEventWireup="true" CodeBehind="RejectedReports.aspx.cs" Inherits="MomoSecretSociety.Content.BossConsole.RejectedReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ConsoleBoss_MainContent" runat="server">

    <link href="../../Styles/BossConsole/Reports.css" rel="stylesheet" />

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

    <div>
        <table class="table" style="border-collapse: initial !important; border-spacing: 0;">
            <tr style="height: 5%;">
                <td style="background-color: #146882; color: #b1e3ed; font-family: 'Palatino Linotype'; font-size: 25px; padding: 4% 4%;">Rejected Reports

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

                    <br />

                    <div style="padding: 10px; background-color: #f4f6f5;">

                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ConnectionStrings:FileDatabaseConnectionString2 %>"
                            SelectCommand="SELECT [CaseNumber], [CaseNumber], [Date], [Subject], [ReportStatus], [CreatedDateTime] FROM [Report]
        WHERE (ReportStatus = 'rejected');">
                            <SelectParameters>
                                <asp:SessionParameter Name="Username" SessionField="AccountUsername" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <asp:GridView ID="GridView1" runat="server" BorderColor="#F0F0F0" HeaderStyle-BackColor="#146882" RowStyle-BackColor="#f3f3f3" RowStyle-Font-Size="Small"
                            HeaderStyle-HorizontalAlign="Center" CellPadding="15" Font-Names="Helvetica"
                            HeaderStyle-ForeColor="White" HeaderStyle-Wrap="true" RowStyle-BorderColor="white"
                            RowStyle-HorizontalAlign="Center" PageSize="5" AllowPaging="True" DataSourceID="SqlDataSource1" Width="100%" AutoGenerateColumns="false" OnRowCommand="GridView1_RowCommand">

                            <%-- If There are no reports --%>
                            <EmptyDataTemplate>
                                <label style="color: red; font-weight: bold; font-size: 30px;">There are no reports at the moment</label>
                            </EmptyDataTemplate>
                            <Columns>
                                <%--<asp:HyperLinkField DataTextField="CaseNumber" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="ViewSelectedReport.aspx?Id={0}" />--%>
                                <%--<asp:BoundField DataField="CaseNumber" HeaderText="Case Number" ItemStyle-Width="200" />--%>
                                <asp:TemplateField HeaderText="Case Number">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="link" CommandArgument='<%# Eval("CaseNumber")%>' CommandName="DataCommand" Text='<%# Eval("CaseNumber") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-Width="200" />
                                <asp:BoundField DataField="Subject" HeaderText="Subject" ItemStyle-Width="200" />
                                <asp:BoundField DataField="ReportStatus" HeaderText="Report Status" ItemStyle-Width="200" />
                                <asp:BoundField DataField="CreatedDateTime" HeaderText="Created Date Time" ItemStyle-Width="200" />
                            </Columns>
                        </asp:GridView>
                    </div>

                    <%--  <br>--%>
                </td>
            </tr>


        </table>
    </div>

</asp:Content>
