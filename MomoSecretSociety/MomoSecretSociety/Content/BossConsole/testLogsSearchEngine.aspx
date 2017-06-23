<%@ Page Title="" Language="C#" MasterPageFile="~/ConsoleBoss.Master" AutoEventWireup="true" CodeBehind="testLogsSearchEngine.aspx.cs" Inherits="MomoSecretSociety.Content.BossConsole.testLogsSearchEngine" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ConsoleBoss_MainContent" runat="server">

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
            text-align:center;
        }

            .txtSearch:focus, .btnSearch:focus {
                /*box-shadow: 0 0 5px rgba(81, 203, 238, 1);
                border: 1px solid rgba(81, 203, 238, 1);*/
                outline:none;
            }
    </style>



            <div class="jumbotron" style="background-color: white; font-size: 14px; display: inline-block; height: calc(100%); width: calc(100%);">

            <h2>Staff Logs -
                <asp:Label ID="staffUsername" runat="server" Font-Bold="true" Font-Underline="true"></asp:Label>'s</h2>

            <%--    <div style="padding: 10px; background-color: #f4f6f5; width: 50%">--%>

            <div style="float: right; margin-top: -8%">
                <asp:TextBox ID="txtSearchValue" runat="server" Width="200" placeholder="Enter an action..." CssClass="txtSearch" />
                <asp:Button ID="btnSearch" runat="server" Text="Search &#128269;" CssClass="btnSearch"/>
            </div>


                
            <div style="float: right; margin-top: -8%">
                <asp:TextBox ID="txtSearchValueDate" runat="server" Width="200" placeholder="Enter a date in DD/MM/YYYY..." CssClass="txtSearch" />
                <asp:Button ID="btnSearchDate" runat="server" Text="Search &#128269;" CssClass="btnSearch" OnClick="btnSearchDate_Click" />
            </div>

            <%-- </div>--%>

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
    
</asp:Content>
