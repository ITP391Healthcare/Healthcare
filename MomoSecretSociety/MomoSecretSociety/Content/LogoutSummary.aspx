<%@ Page Title="" Language="C#" MasterPageFile="~/ConsoleStaff.Master" AutoEventWireup="true" CodeBehind="LogoutSummary.aspx.cs" Inherits="MomoSecretSociety.Content.LogoutSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <style>
        th {
            text-align: center;
        }
    </style>

    <style>
        .modal {
            position: relative;
            overflow: hidden;
        }

        .modal-backdrop.in {
            opacity: 0;
        }

        .fade {
            opacity: 1;
        }
    </style>

    <style>
        .pagerStyle td {
            min-width: 50px;
        }

            .pagerStyle td a {
                color: black;
            }
    </style>

    <div style="text-align: center;">

        <div class="modal fade" id="myModal" role="dialog" style="padding-top: 3%; display: inline-block;">
            <div class="modal-content" style="display: block;">

                <div class="modal-header">
                    <h4 class="modal-title" style="text-align: center;">Successful Logout</h4>
                </div>

                <div class="modal-body" style="text-align: center;">

                    <p>
                        Dear <b><u><asp:Label ID="Label_username" runat="server"></asp:Label></u></b>, 
                        this is a summary of what you have did in this session.
                    </p>
                    <p>Thank you for using our service.</p>

                    <asp:GridView ID="GridView1" runat="server" BorderColor="#F0f0f0" HeaderStyle-BackColor="#2eb3ed" RowStyle-BackColor="#f3f3f3" RowStyle-Font-Size="Medium"
                        HeaderStyle-HorizontalAlign="Center" CellPadding="15" Font-Names="Helvetica"
                        HeaderStyle-ForeColor="White" HeaderStyle-Wrap="true" RowStyle-BorderColor="white" HorizontalAlign="Center"
                        RowStyle-HorizontalAlign="Center" PageSize="5" AllowPaging="true">

                        <PagerStyle CssClass="pagerStyle" />
                    </asp:GridView>

                </div>

            </div>
        </div>

    </div>

</asp:Content>
