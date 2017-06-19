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
        static SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FileDatabaseConnectionString2"].ConnectionString);
        static SqlConnection connection2 = new SqlConnection(ConfigurationManager.ConnectionStrings["FileDatabaseConnectionString2"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {



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

            //Pop up for new pending reports

            connection2.Open();
            SqlCommand myCommand2 = new SqlCommand("SELECT count(CaseNumber) FROM Report WHERE(ReportStatus = @ReportStatus AND isNew = @isNew)", connection2);
            myCommand2.Parameters.AddWithValue("@ReportStatus", "pending");
            myCommand2.Parameters.AddWithValue("@isNew", "0");

            SqlDataReader myReader2 = myCommand2.ExecuteReader();
            myReader2.Close();
            noOfNewPendingReports.Text = myCommand2.ExecuteScalar().ToString();
            //while (myReader2.Read())
            //{
            //}
            connection2.Close();

            connection.Open();
            SqlCommand myCommand = new SqlCommand("SELECT Username, CaseNumber, Subject FROM Report WHERE(ReportStatus = @ReportStatus AND isNew = @isNew)", connection);
            myCommand.Parameters.AddWithValue("@ReportStatus", "pending");
            myCommand.Parameters.AddWithValue("@isNew", "0");

            SqlDataReader myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "alert", "$('#myModal2').modal('show')", true);

            }
            connection.Close();

        }

        //public static DataTable showPendingReportsSummary()
        //{

        //    SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FileDatabaseConnectionString2"].ConnectionString);

        //    connection.Open();

        //    SqlCommand getPendingReportsCommand = new SqlCommand(
        //    "SELECT Username, CaseNumber, Subject FROM Report WHERE ReportStatus = @ReportStatus ", connection);
        //    getPendingReportsCommand.Parameters.AddWithValue("@ReportStatus", "pending");

        //    SqlDataReader summaryReader = getPendingReportsCommand.ExecuteReader();
        //    DataTable dt = new DataTable();
        //    dt.Load(summaryReader);

        //    connection.Close();

        //    return dt;
        //}

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


        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            

            if (e.CommandName == "DataCommand")
            {

                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                string firstArgVal = commandArgs[0];
                string secondArgVal = commandArgs[1];

                Session["usernameOfThisPendingReport"] = firstArgVal;
                Session["caseNumberOfThisPendingReport"] = secondArgVal;


                connection.Open();

                SqlCommand updateOldOrNew = new SqlCommand("UPDATE Report SET isNew = @isNew WHERE Username = @AccountUsername AND CaseNumber = @CaseNumber", connection);
                updateOldOrNew.Parameters.AddWithValue("@isNew", "1");
                updateOldOrNew.Parameters.AddWithValue("@AccountUsername", Session["usernameOfThisPendingReport"].ToString());
                updateOldOrNew.Parameters.AddWithValue("@CaseNumber", Session["caseNumberOfThisPendingReport"].ToString());

                updateOldOrNew.ExecuteNonQuery();

                connection.Close();




                Response.Redirect("~/Content/BossConsole/ViewPendingReport.aspx");

            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string isNew = e.Row.Cells[3].Text;
                if (isNew == "False")
                {
                    e.Row.Cells[3].Text = "New";
                    e.Row.BackColor = System.Drawing.Color.FromArgb(200, 224, 233);
                    e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    e.Row.Cells[3].Text = "Old";
                }
            }
        }
    }
}