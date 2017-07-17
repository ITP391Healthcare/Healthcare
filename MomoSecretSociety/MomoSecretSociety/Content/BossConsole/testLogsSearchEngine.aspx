<%@ Page Title="" Language="C#" MasterPageFile="~/ConsoleBoss.Master" AutoEventWireup="true" CodeBehind="testLogsSearchEngine.aspx.cs" Inherits="MomoSecretSociety.Content.BossConsole.testLogsSearchEngine" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
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
 <%-- asd --%>
    
    <asp:Chart ID="Chart1" runat="server" DataSourceID="SqlDataSource1">
        <Series>
            <asp:Series Name="Series1" ChartType="Pie" YValuesPerPoint="4">
                  <Points>
                    <asp:DataPoint YValues="45" />
                    <asp:DataPoint YValues="15" />
                    <asp:DataPoint YValues="70" />
                    <asp:DataPoint YValues="50" />
                    <asp:DataPoint YValues="30" />
                    <asp:DataPoint YValues="10" />
                </Points>
            </asp:Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="ChartArea1">
                 <AxisY Title="Frequency" TitleForeColor="#ff0000" Interval="20">
                    <MajorGrid Enabled ="true" />
                </AxisY>
               <AxisX Title="Activity" IsLabelAutoFit="True" TitleForeColor="#ff0000">
            <%--<LabelStyle Format="dddd, dd-MM-yy" />--%>
            <MajorGrid Enabled ="true" />
        </AxisX> 
            </asp:ChartArea>
        </ChartAreas>
    </asp:Chart>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FileDatabaseConnectionString2 %>" 
        SelectCommand="SELECT Username, COUNT(Action) AS TotalNumberOfActivity, ROUND(COUNT(Action) * 100 / (SELECT COUNT(*) AS Expr1 FROM Logs), 2) AS PercentageOfActivity FROM Logs AS Logs_1 GROUP BY Username">
    </asp:SqlDataSource>

<%--    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <shield:ShieldChart ID="ShieldChart1" runat="server" AutoPostBack="true" OnSelectionChanged="ShieldChart1_SelectionChanged" Width="320px" Height="330px" 
                            OnTakeDataSource="ShieldChart1_TakeDataSource"> 
                            <PrimaryHeader Text="Quarterly Sales"> 
                            </PrimaryHeader> 
                            <ExportOptions AllowExportToImage="false" AllowPrint="false" /> 
                            <TooltipSettings CustomPointText="Sales Volume: <b>{point.y}</b>"> 
                            </TooltipSettings> 
                            <Axes> 
                                <shield:ChartAxisX CategoricalValuesField="Quarter"> 
                                </shield:ChartAxisX> 
                                <shield:ChartAxisY> 
                                    <Title Text="Quarter Overview"></Title> 
                                </shield:ChartAxisY> 
                            </Axes> 
                            <DataSeries> 
                                <shield:ChartBarSeries DataFieldY="Sales"> 
                                    <Settings EnablePointSelection="true" EnableAnimation="true"> 
                                        <DataPointText BorderWidth=""> 
                                        </DataPointText> 
                                    </Settings> 
                                </shield:ChartBarSeries> 
                            </DataSeries> 
                            <Legend Align="Center" BorderWidth=""></Legend> 
                        </shield:ShieldChart> 
                    </ContentTemplate> 
                </asp:UpdatePanel>--%>


    <br /><br /><br /><br /><br />






</asp:Content>
