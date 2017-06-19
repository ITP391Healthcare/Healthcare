<%@ Page Title="" Language="C#" MasterPageFile="~/ConsoleBoss.Master" AutoEventWireup="true" CodeBehind="StaffLogs.aspx.cs" Inherits="MomoSecretSociety.Content.BossConsole.StaffLogs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ConsoleBoss_MainContent" runat="server">

    <style>
        .circleIcon {
            width: 30px;
            height: 30px;
            font-size: 15px;
            line-height: 30px;
            position: absolute;
            color: #666;
            /*background: #0073b7;*/
            background: #d2d6de;
            border-radius: 50%;
            text-align: center;
            left: 18px;
            top: 0;
            margin-left: -9.5%;
        }

        th {
            text-align: center;
        }

        html input[type="button"] {
            text-shadow: none;
            border-radius: 3px;
            border-color: lightblue;
            color: #fff;
            font: 13px/16px "lft-etica-n4", "lft-etica", Arial, sans-serif;
            padding: 8px 17px;
            background-color: lightblue;
        }

            html input[type="button"]:hover {
                opacity: 0.7;
                text-decoration: none;
            }

        table {
            display: inline-block;
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

    <asp:Panel runat="server" ID="panel1">

        <div class="jumbotron" style="background-color: white; font-size: 14px; display: inline-block; height: calc(100%); width: calc(100%); box-shadow: 0 0 15px 1px rgba(0, 0, 0, 0.4); text-align: center;">

            <h2 style="text-align: center;">List of Staff Members</h2>

            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FileDatabaseConnectionString2 %>" SelectCommand="SELECT DISTINCT(Username) FROM [Logs]"></asp:SqlDataSource>

            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" BorderColor="#F0F0F0" HeaderStyle-BackColor="#add8e6" RowStyle-BackColor="#f3f3f3" RowStyle-Font-Size="Medium" Font-Names="Helvetica" HeaderStyle-ForeColor="White" RowStyle-BorderColor="white" RowStyle-HorizontalAlign="Center" PageSize="5" AllowPaging="true" CellPadding="15" OnRowCommand="GridView1_RowCommand" GridLines="None" HeaderStyle-Wrap="true" HeaderStyle-HorizontalAlign="Center" OnRowCreated="grid_RowCreated" RowStyle-VerticalAlign="Middle">


                <PagerStyle CssClass="pagerStyle" />

                <Columns>
                    <asp:BoundField DataField="Username" HeaderText="Staff Name" SortExpression="Username" />

                    <asp:ButtonField Text="View Logs" ItemStyle-Width="150px" HeaderStyle-Width="150px" CommandName="view" ButtonType="Button"></asp:ButtonField>

                </Columns>
            </asp:GridView>
        </div>

    </asp:Panel>

    <asp:Panel runat="server" Visible="false" ID="panel2">

        <div class="jumbotron" style="background-color: white; font-size: 14px; display: inline-block; height: calc(100%); width: calc(100%);">

            <h2>Staff Logs -
                <asp:Label ID="staffUsername" runat="server" Font-Bold="true" Font-Underline="true"></asp:Label>'s</h2>
            <div class="col-md-9" style="height: calc(100%); width: calc(100%);">

                <div class="box box-primary">
                    <!-- The timeline -->
                    <div class="box-body">
                        <ul class="timeline timeline-inverse">
                            <asp:PlaceHolder ID="phTimeline" runat="server"></asp:PlaceHolder>

                            <li>
                                <i class="fa fa-clock-o bg-gray"></i>
                            </li>
                        </ul>
                    </div>
                </div>

            </div>
        </div>

    </asp:Panel>

</asp:Content>
