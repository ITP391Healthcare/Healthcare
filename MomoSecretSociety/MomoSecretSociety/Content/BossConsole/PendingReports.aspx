<%@ Page Title="" Language="C#" MasterPageFile="~/ConsoleBoss.Master" AutoEventWireup="true" CodeBehind="PendingReports.aspx.cs" Inherits="MomoSecretSociety.Content.BossConsole.PendingReports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ConsoleBoss_MainContent" runat="server">

    <style>
        body {
    font-size: 14px;
    color: #333333;
}

h4 {
    font-size: 18px;
}

h4, h5, h6 {
    margin-top: 10px;
    margin-bottom: 10px;
}

h1, h2, h3, h4, h5, h6, .h1, .h2, .h3, .h4, .h5, .h6 {
    font-family: "Helvetica Neue", Helvetica, Arial, sans-serif;
    font-weight: 500;
    line-height: 1.1;
}


/* Table */
td {
    width: 97.8%;
    display: inline-block !important;
    border-top: none !important;
    padding: 8%;
}

tr {
    display: inline;
}

.table {
    width: 700px;
    height: 575px;
    margin-right: auto;
    margin-left: auto;
    padding: 17px 25px 17px 25px;
    /*background-color: #ffffff;
    border-radius: 10px;
    -moz-box-shadow: 0 0 15px 1px #bbb;
    -webkit-box-shadow: 0 0 15px 1px #bbb;
    box-shadow: 0 0 15px 1px #bbb;*/
    border: 1px solid lightblue;
    font-family: Helvetica;
    text-align: center;
    background-color: white;
}


/* Input labels etc */
input[type="text"], input[type="password"], input[type="email"], input[type="tel"], input[type="select"] {
    max-width: none !important;
}

input {
    border: 1px solid #c4c4c4;
    width: 88.5%;
    font-size: 13px;
    padding: 4px 4px 4px 4px;
    border-radius: 4px;
    -moz-border-radius: 4px;
    -webkit-border-radius: 4px;
    box-shadow: 0px 0px 8px #d9d9d9;
    -moz-box-shadow: 0px 0px 8px #d9d9d9;
    -webkit-box-shadow: 0px 0px 8px #d9d9d9;
}

    input:focus {
        outline: none;
        border: 1px solid #da337a;
        box-shadow: 0px 0px 8px #da337a;
        -moz-box-shadow: 0px 0px 8px #da337a;
        -webkit-box-shadow: 0px 0px 8px #da337a;
    }

.toolbox {
    float: left;
    margin-left: 6%;
}

.contactNo {
    float: left;
    margin-left: 2%;
}

.countryCode {
    width: 30px;
    text-align: center;
    float: left;
    margin-left: 6%;
}


/* Numbers for "Header" 1, 2, 3, 4*/
.numberCircle {
    display: inline-block;
    line-height: 0px;
    border-radius: 50%;
    border: 2px solid;
    font-size: 25px;
    background-color: #4ea9da;
    border-color: #4ea9da;
    color: white;
    float: left;
    margin-right: 2%;
}

    .numberCircle span {
        display: inline-block;
        padding-top: 50%;
        padding-bottom: 50%;
        margin-left: 8px;
        margin-right: 8px;
        background-color: #4ea9da;
        border-color: #4ea9da;
        color: white;
        float: left;
    }


/* Bookmark */
#ribbon-head {
    display: block;
    background-color: #90bc2b;
    width: 60px;
    height: 60px;
}

h1 {
    font-family: 'Impact';
    color: #fff;
    text-align: center;
    font-size: 12px;
}

#ribbon-tail {
    display: table-row;
}

#left, #right {
    display: table-cell;
    border-top: 30px solid #90bc2b;
    border-bottom: 30px solid transparent;
    height: 0px;
    width: 0px;
}

#left {
    border-left: 30px solid #90bc2b;
}

#right {
    border-right: 30px solid #90bc2b;
}


/* Captcha */
.imgCaptcha {
    float: left;
    margin-left: 6%;
}

.msgCaptcha {
    float: right;
    margin-right: 6%;
}


/* Tooltip to show password requirements */
.tooltip .tooltiptext {
    visibility: hidden;
    width: 120px;
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

      <div>
        <table class="table" style="border-collapse: initial !important; border-spacing: 0;">
            <tr style="height: 5%;">
                <td style="background-color: #146882; color: #b1e3ed; font-family: 'Palatino Linotype'; font-size: 25px; padding: 4% 4%;">Pending Reports
                          
                    <br />
                 <%--   <span style="font-size: 12px; font-family: Arial; color: #5fa6c2;">unleash your power</span>--%>
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
          <%--  <tr>
                <td>
                    <span class="numberCircle">
                        <span style="background-color: #4ea9da; color: white;">1</span>
                    </span>
                    <h4 style="float: left; color: #626b6e; font-family: Cambria;">Firstly, name your portfolio</h4>
                </td>
            </tr>--%>

            <tr>
                <td>
                    
            <br>
                    <div style="padding: 10px; background-color: #f4f6f5; height: 250px;">

                        </div>
                    </td>
            </tr>

       <%--     <tr>
                <td colspan="2">
                    <asp:Button ID="btnRegister" runat="server" Text="Register" BackColor="#90bc2b" Width="15%" />
                </td>
            </tr>--%>

        </table>
    </div>




</asp:Content>
