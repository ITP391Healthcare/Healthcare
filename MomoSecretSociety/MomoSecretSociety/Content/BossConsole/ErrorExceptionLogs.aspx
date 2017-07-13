<%@ Page Title="" Language="C#" MasterPageFile="~/ConsoleBoss.Master" AutoEventWireup="true" CodeBehind="ErrorExceptionLogs.aspx.cs" Inherits="MomoSecretSociety.Content.BossConsole.ErrorExceptionLogs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ConsoleBoss_MainContent" runat="server">

    <style>
        td {
            text-align: left;
        }

        th {
            text-align: center;
        }

        .pagerStyle td {
            padding: 10px;
        }
    </style>

    <style>
        .btnSearch {
            border-top: thick solid #e5e5e5;
            border-right: thick solid #e5e5e5;
            border-bottom: thick solid #e5e5e5;
            border-left: thick solid #e5e5e5;
            margin-left: -2%;
            /*box-shadow: 0px 0px 0px 0px #e5e5e5;*/
            box-shadow: 0 0 5px rgba(81, 203, 238, 1);
            border: 1px solid rgba(81, 203, 238, 1);
        }

        .txtSearch {
            border-top: thick solid #e5e5e5;
            border-left: thick solid #e5e5e5;
            border-bottom: thick solid #e5e5e5;
            box-shadow: 0 0 5px rgba(81, 203, 238, 1);
            border: 1px solid rgba(81, 203, 238, 1);
            text-align: center;
        }

            .txtSearch:focus, .btnSearch:focus {
                /*box-shadow: 0 0 5px rgba(81, 203, 238, 1);
                border: 1px solid rgba(81, 203, 238, 1);*/
                outline: none;
            }

            .txtSearch:focus {
                border-bottom: 2px solid black;
            }
    </style>

    <div class="jumbotron" style="background-color: white; font-size: 14px; display: inline-block; height: calc(100%); width: calc(100%); box-shadow: 0 0 15px 1px rgba(0, 0, 0, 0.4); text-align: center;">


        <div style="float: right;">
            <abbr title="Enter a Username/Exception Type/Error Message/Location">
                <asp:TextBox ID="txtSearchValue" runat="server" Width="200" placeholder="Enter a keyword" CssClass="txtSearch" />
            </abbr>
            <asp:Button ID="btnSearch" runat="server" Text="Search &#128269;" CssClass="btnSearch" OnClick="btnSearch_Click" />
        </div>

        <div style="clear: both;"></div>

        <div style="float: right; margin-top: 1%">
            <abbr title="Enter a Date">
                <asp:TextBox ID="txtSearchValueDate" runat="server" Width="200" placeholder="Enter a date in DD/MM/YYYY" CssClass="txtSearch" />
            </abbr>
            <asp:Button ID="btnSearchDate" runat="server" Text="Search &#128269;" CssClass="btnSearch" OnClick="btnSearchDate_Click" />
        </div>

        <br />
        <br />

        <h2 style="text-align: center; text-shadow: 2px 2px beige;">List of Error Exception Logs</h2>

        <%--    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FileDatabaseConnectionString2 %>" SelectCommand="SELECT * FROM [ErrorExceptionLogs]">--%>

        <%--                <SelectParameters>
                    <asp:SessionParameter Name="Username" SessionField="AccountUsername" Type="String" />
                </SelectParameters>--%>

        <%--  </asp:SqlDataSource>--%>

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BorderColor="#F0F0F0" HeaderStyle-BackColor="#add8e6" RowStyle-BackColor="#f3f3f3" RowStyle-Font-Size="Small" Font-Names="Helvetica" HeaderStyle-ForeColor="White" RowStyle-BorderColor="white" RowStyle-HorizontalAlign="Center" AllowPaging="False" CellPadding="10" HeaderStyle-Wrap="true" HeaderStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle" RowStyle-Wrap="true" HorizontalAlign="Center">


            <%--<PagerStyle CssClass="pagerStyle" />--%>

            <Columns>
                <asp:BoundField DataField="Username" HeaderText="Username" />
                <asp:BoundField DataField="ExceptionType" HeaderText="Exception Type" />
                <asp:BoundField DataField="ErrorMessage" HeaderText="Error Message" />

                <asp:BoundField DataField="Timestamp" HeaderText="Timestamp" />
                <asp:BoundField DataField="Location" HeaderText="Location" ItemStyle-Wrap="true" />


                <%--<asp:ButtonField Text="View Logs" ItemStyle-Width="150px" HeaderStyle-Width="150px" CommandName="view" ButtonType="Button"></asp:ButtonField>--%>
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>
