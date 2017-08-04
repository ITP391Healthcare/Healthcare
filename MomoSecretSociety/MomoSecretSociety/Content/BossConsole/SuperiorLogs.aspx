<%@ Page Title="" Language="C#" MasterPageFile="~/ConsoleBoss.Master" AutoEventWireup="true" CodeBehind="SuperiorLogs.aspx.cs" Inherits="MomoSecretSociety.Content.BossConsole.SuperiorLogs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ConsoleBoss_MainContent" runat="server">

        <style>
        .timeline-inverse>li>.timeline-item>.timeline-header 
        {
            font-size: small;
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
        .ribbon {
            position: relative;
            box-shadow: 0px 1px 3px lightblue;
            clear: both;
        }

        div.both_ribbon {
            text-align: center;
            color: #000;
            z-index: 1;
            box-shadow: 0 0 15px lightblue;
            border-radius: 3px;
            width: 100%;
        }

            div.both_ribbon::before {
                display: block;
                width: 10px;
                height: 0px;
                position: absolute;
                bottom: -10px;
                left: -11px;
                content: "";
                border-bottom: 10px solid transparent;
                border-right: 10px solid rgb(0, 80, 116);
            }

            div.both_ribbon::after {
                display: block;
                width: 10px;
                height: 0px;
                position: absolute;
                bottom: -10px;
                right: -10px;
                content: "";
                border-bottom: 10px solid transparent;
                border-left: 10px solid rgb(0, 80, 116);
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


    <asp:Panel runat="server" ID="panel2">

        <div class="jumbotron" style="background-color: white; font-size: 14px; display: inline-block; height: calc(100%); width: calc(100%);">

            
            <div style="float: right;">
                <asp:TextBox ID="txtSearchValue" runat="server" Width="200" placeholder="Enter a keyword of action" CssClass="txtSearch" />
                <asp:Button ID="btnSearch" runat="server" Text="Search &#128269;" CssClass="btnSearch" OnClick="btnSearch_Click" />
            </div>

            <div style="clear: both;"></div>

            <div style="float: right; margin-top: 1%">
                <asp:TextBox ID="txtSearchValueDate" runat="server" Width="200" placeholder="Enter a date in DD/MM/YYYY" CssClass="txtSearch" />
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

            <div class="ribbon both_ribbon">
                <h2 style="padding: 0.5%; text-shadow: 1px 1px black;">Boss Logs of 
                <asp:Label ID="bossUsername" runat="server" Font-Bold="true" Font-Underline="true"></asp:Label>
                </h2>
            </div>

            <div class="col-md-9" style="height: calc(100%); width: calc(100%); box-shadow: 0 0 10px lightblue">

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
