<%@ Page Title="" Language="C#" MasterPageFile="~/ConsoleBoss.Master" AutoEventWireup="true" CodeBehind="ErrorExceptionLogs.aspx.cs" Inherits="MomoSecretSociety.Content.BossConsole.ErrorExceptionLogs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ConsoleBoss_MainContent" runat="server">

    <style>
        /* Tooltip to show password requirements */
        .tooltip .tooltiptext {
            visibility: hidden;
            /*width: 120px;*/
            background-color: black;
            color: #fff;
            text-align: center;
            border-radius: 6px;
            padding: 5px 0;
            /* Position the tooltip */
            position: absolute;
            z-index: 1;
            left: 1%;
        }

        .tooltip:hover .tooltiptext {
            visibility: visible;
        }

        .tooltip {
            position: relative;
            z-index: 1;
            display: inherit;
            font-size: inherit;
            opacity: 1 !important;
        }
    </style>

    <style>
        td {
            text-align: left;
        }

        table tr td:nth-child(3) {
            max-width: 385px;
            overflow-wrap: break-word !important;
            /*overflow: scroll;*/
        }

        table tr td:nth-child(5) {
            max-width: 395px;
            overflow-wrap: break-word !important;
        }


        th:nth-child(2) {
            min-width: 180px;
            text-align: center;
        }

        th:nth-child(3) {
            min-width: 380px;
            text-align: center;
        }



        th {
            /*min-width:185px;*/
            text-align: center;
        }


        /*table {
            margin-left: -4.5%;
        }*/
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


        .btnSearchBoth {
            border-top: thick solid #e5e5e5;
            border-right: thick solid #e5e5e5;
            border-bottom: thick solid #e5e5e5;
            border-left: thick solid #e5e5e5;
            /*box-shadow: 0px 0px 0px 0px #e5e5e5;*/
            box-shadow: 0 0 5px rgba(81, 203, 238, 1);
            border: 1px solid rgba(81, 203, 238, 1);
            margin-left: -1%;
        }
    </style>

    <style>
        .pagerStyle td {
            min-width: 50px;
            padding-right: 5%;
            padding-left: 5%;
        }

            .pagerStyle td a {
                color: black;
            }
    </style>

<%--    <style>
        
    </style>--%>

    <style type="text/css">
        .FixedHeader {
            position: absolute;
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
                    <asp:Button ID="btnAuthenticate" CssClass="btn btn-default" runat="server" Text="Authenticate" OnClick="btnAuthenticate_Click" CausesValidation="false" />
                    <%--  <asp:Button ID="btnLogout" class="btn btn-default" runat="server" Text="Logout" PostBackUrl="~/Account/Login.aspx" />--%>

                    <br />
                </div>
            </div>
        </div>
    </div>


    <div class="jumbotron" style="background-color: white; font-size: 14px; display: inline-block; height: calc(100%); width: calc(100%); box-shadow: 0 0 15px 1px rgba(0, 0, 0, 0.4); text-align: center;">


        <div style="float: right;">

            <div class="tooltip">
                <asp:Label ID="Label2" runat="server" CssClass="toolbox"><span style="font-size: smaller;">Not knowing what exceptions are there &#10067; Hover me!</span>
                </asp:Label>

                <span class="tooltiptext" style="opacity: 0.5; margin-left: -285%; width: 280%; font-size: small; z-index: 1;">

                    <u>List of exception</u><br />
                    <br />
                    SystemException
AccessException
ArgumentException
ArgumentNullException
ArgumentOutOfRangeException
ArithmeticException
ArrayTypeMismatchException
BadImageFormatException
CoreException
DivideByZeroException
FormatException
IndexOutOfRangeException
InvalidCastExpression
InvalidOperationException
MissingMemberException
NotFiniteNumberException
NotSupportedException
NullReferenceException
OutOfMemoryException
StackOverflowException
                </span>
            </div>

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

        <div style="clear: both; padding: 1%;"></div>

        <%-- Search for both keyword + date together --%>
        <div style="float: right; margin-top: 1%;">

            <asp:TextBox ID="TextBox1" runat="server" Width="200" placeholder="Enter a keyword" CssClass="txtSearch" />
            +
            <asp:TextBox ID="TextBox2" runat="server" Width="200" placeholder="Enter a date in DD/MM/YYYY" CssClass="txtSearch" />

            <asp:Button ID="btnSearchBoth" runat="server" Text="Search &#128269;" CssClass="btnSearchBoth" OnClick="btnSearchBoth_Click" />
        </div>

        <br />
        <br />

        <h2 style="text-align: center; text-shadow: 2px 2px beige;">List of Error Exception Logs</h2>

        <asp:Panel ID="tablePanel" runat="server" style="overflow:scroll; width: 100%; height: 1000px;">
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FileDatabaseConnectionString2 %>">
            <%--  SelectCommand="SELECT * FROM [ErrorExceptionLogs]" --%>
            <%--                <SelectParameters>
                    <asp:SessionParameter Name="Username" SessionField="AccountUsername" Type="String" />
                </SelectParameters>--%>

        </asp:SqlDataSource>

        <asp:PlaceHolder ID="placeholder" runat="server">

            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BorderColor="#F0F0F0" HeaderStyle-BackColor="#add8e6" RowStyle-BackColor="#f3f3f3" RowStyle-Font-Size="Small" Font-Names="Helvetica" HeaderStyle-ForeColor="White" RowStyle-BorderColor="white" RowStyle-HorizontalAlign="Center" CellPadding="10" HeaderStyle-Wrap="true" HeaderStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle" RowStyle-Wrap="true" HorizontalAlign="Center" AlternatingRowStyle-BackColor="White" OnSorting="GridView1_Sorting" AllowSorting="true" AllowPaging="false">

                <%--  OnPageIndexChanging="GridView1_PageIndexChanging"   PageSize="30" --%>

                <%-- If There are no reports --%>
                <EmptyDataTemplate>
                    <label style="color: red; font-weight: bold; font-size: 30px;">There are no error exception logs at the moment</label>
                </EmptyDataTemplate>

                <PagerStyle CssClass="pagerStyle" />

                <Columns>
                    <asp:BoundField DataField="Username" HeaderText="Username" SortExpression="Username" ItemStyle-Wrap="true" />
                    <asp:BoundField DataField="ExceptionType" HeaderText="Exception Type" SortExpression="ExceptionType" ItemStyle-Wrap="true" />
                    <asp:BoundField DataField="ErrorMessage" HeaderText="Error Message" SortExpression="ErrorMessage" ItemStyle-Wrap="true" />

                    <asp:BoundField DataField="Timestamp" HeaderText="Timestamp" SortExpression="Timestamp" ItemStyle-Wrap="true" />
                    <asp:BoundField DataField="Location" HeaderText="Location" ItemStyle-Wrap="true" SortExpression="Location" />


                    <%--<asp:ButtonField Text="View Logs" ItemStyle-Width="150px" HeaderStyle-Width="150px" CommandName="view" ButtonType="Button"></asp:ButtonField>--%>
                </Columns>
            </asp:GridView>

        </asp:PlaceHolder>

            </asp:Panel>
    </div>

</asp:Content>
