using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MomoSecretSociety.Content.BossConsole
{
    public partial class PendingReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //List<x> myList = GetList();

            //GridView1.DataSource = myList;
            //GridView1.DataBind();

            //DataTable dt = showPendingReportsSummary();

            //GridView1.DataSource = dt;
            //GridView1.DataBind();

            //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+ ActionLogs.getLastLoggedInOf(Context.User.Identity.Name) + "" + "');", true);

            if (Request.IsAuthenticated)
            {
                ((Label)Master.FindControl("lastLoginBoss")).Text = "Your last logged in was <b>"
                            + ActionLogs.getLastLoggedInOf(Context.User.Identity.Name) + "</b>";
            }


            //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + ActionLogs.getLastLoggedInOf(Context.User.Identity.Name) + "" + "');", true);

            if (IsPostBack)
            {
                errormsgPasswordAuthenticate.Visible = false;
            }

        }

        public static DataTable showPendingReportsSummary()
        {

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FileDatabaseConnectionString2"].ConnectionString);

            connection.Open();

            SqlCommand getPendingReportsCommand = new SqlCommand(
            "SELECT Username, CaseNumber, Subject FROM Report WHERE ReportStatus = @ReportStatus ", connection);
            getPendingReportsCommand.Parameters.AddWithValue("@ReportStatus", "pending");

            SqlDataReader summaryReader = getPendingReportsCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(summaryReader);

            connection.Close();

            return dt;
        }

        protected void btnAuthenticate_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string inputUsername = Context.User.Identity.Name;
                string inputPassword = txtPasswordAuthenticate.Text;

                string dbUsername = "";
                string dbPasswordHash = "";
                string dbSalt = "";

                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FileDatabaseConnectionString2"].ConnectionString);

                connection.Open();
                SqlCommand myCommand = new SqlCommand("SELECT HashedPassword, Salt, Role, Username FROM UserAccount WHERE Username = @AccountUsername", connection);
                myCommand.Parameters.AddWithValue("@AccountUsername", inputUsername);

                SqlDataReader myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    dbPasswordHash = (myReader["HashedPassword"].ToString());
                    dbSalt = (myReader["Salt"].ToString());
                    dbUsername = (myReader["Username"].ToString());
                }
                connection.Close();

                string passwordHash = ComputeHash(inputPassword, new SHA512CryptoServiceProvider(), Convert.FromBase64String(dbSalt));

                if (dbUsername.Equals(inputUsername.Trim()))
                {
                    if (dbPasswordHash.Equals(passwordHash))
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "alert", "$('#myModal').modal('hide')", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "alert", "$('#myModal').modal('show')", true);
                        errormsgPasswordAuthenticate.Visible = true;
                    }

                }
            }
        }

        public static String ComputeHash(string input, HashAlgorithm algorithm, Byte[] salt)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            Byte[] saltedInput = new Byte[salt.Length + inputBytes.Length];
            salt.CopyTo(saltedInput, 0);
            inputBytes.CopyTo(saltedInput, salt.Length);

            Byte[] hashedBytes = algorithm.ComputeHash(saltedInput);

            return BitConverter.ToString(hashedBytes);
        }

        protected void link_Click(object sender, EventArgs e)
        {
            //Session["usernameOfPendingReport"] = 
            //    Response.Redirect("~/Content/BossConsole/ViewPendingReport.aspx");

            //    < asp:HyperLinkField DataTextField = "CaseNumber" HeaderText = "Case Number" NavigateUrl = "~/Content/BossConsole/ViewPendingReport.aspx" ></ asp:HyperLinkField > --%>

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DataCommand")
            {
                //  var value = e.CommandArgument;

                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                string firstArgVal = commandArgs[0];
                string secondArgVal = commandArgs[1];

                Session["usernameOfThisPendingReport"] = firstArgVal;
                Session["caseNumberOfThisPendingReport"] = secondArgVal;




                Response.Redirect("~/Content/BossConsole/ViewPendingReport.aspx");

            }
        }
    }
}