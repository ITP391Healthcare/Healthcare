<%@ Page Title="" Language="C#" MasterPageFile="~/ConsoleStaff.Master" AutoEventWireup="true" CodeBehind="RejectedReports.aspx.cs" Inherits="MomoSecretSociety.Content.StaffConsole.RejectedReports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <style>

        .myDataGrid {
             border: 2px solid black;
             width: 80%;
         }
 
         th, td{
             text-align:center;
             height:40px;
         }
 
         .header{
             background-color:#535360;
             font-family: Arial;
             color: white;
             border: none;
         }

    </style>
    
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

        <asp:SqlDataSource ID ="SqlDataSource1" runat="server" ConnectionString="<%$ConnectionStrings:FileDatabaseConnectionString2 %>"
        SelectCommand="SELECT [CaseNumber], [CaseNumber], [Date], [Subject], [ReportStatus], [CreatedDateTime] FROM [Report]
        WHERE ([Username] = @Username AND ( ReportStatus = 'rejected' ));">
        <SelectParameters>
            <asp:SessionParameter Name="Username" SessionField="AccountUsername" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:GridView ID ="GridView1" CssClass="myDataGrid" HeaderStyle-CssClass="header" runat="server" DataSourceID ="SqlDataSource1" 
        AutoGenerateColumns="false" OnRowCommand="GridView1_RowCommand" AlternatingRowStyle-BackColor="#adadad" RowStyle-Height="40" RowStyle-BackColor="#c5c5c5">
        
        <%-- If There are no reports --%>
        <EmptyDataTemplate>
            <label style ="color: red; font-weight: bold; font-size: 30px;"> There are no reports at the moment</label>
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



</asp:Content>
