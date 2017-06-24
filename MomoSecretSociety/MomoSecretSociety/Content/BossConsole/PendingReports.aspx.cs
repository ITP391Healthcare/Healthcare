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
        static SqlConnection firstLoginAccessConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["FileDatabaseConnectionString2"].ConnectionString);

        static SqlConnection caseNumberQtyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["FileDatabaseConnectionString2"].ConnectionString);

        static SqlConnection pendingReportsDetailsConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["FileDatabaseConnectionString2"].ConnectionString);

        static SqlConnection connection4 = new SqlConnection(ConfigurationManager.ConnectionStrings["FileDatabaseConnectionString2"].ConnectionString);

        string dbIsFirstTimeAccessed = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated && (IsPostBack || !IsPostBack))
            {
                ((Label)Master.FindControl("lastLoginBoss")).Text = "Your last logged in was <b>"
                            + ActionLogs.getLastLoggedInOf(Context.User.Identity.Name) + "</b>";

                showNewPendingReports();
            }

            if (IsPostBack)
            {
                errormsgPasswordAuthenticate.Visible = false;
            }



        }

        private void showNewPendingReports()
        {
            //Check if is 1st time login. If 1st time login, then only pop up summary of NEW pending reports

            firstLoginAccessConnection.Open();
            SqlCommand firstLoginAccessCommand = new SqlCommand("SELECT isFirstTimeAccessed FROM UserAccount WHERE Username = @AccountUsername", firstLoginAccessConnection);
            firstLoginAccessCommand.Parameters.AddWithValue("@AccountUsername", Context.User.Identity.Name);

            SqlDataReader firstLoginAccessReader = firstLoginAccessCommand.ExecuteReader();
            while (firstLoginAccessReader.Read())
            {
                dbIsFirstTimeAccessed = (firstLoginAccessReader["isFirstTimeAccessed"].ToString());
            }
            firstLoginAccessReader.Close();
            firstLoginAccessConnection.Close();

            //Pop up for NEW pending reports for 1st Login Access 
            if (dbIsFirstTimeAccessed == "False")
            {
                //Retrieve amount of NEW pending reports
                caseNumberQtyConnection.Open();
                SqlCommand caseNumberQtyCommand = new SqlCommand("SELECT count(CaseNumber) FROM Report WHERE(ReportStatus = @ReportStatus AND isNew = @isNew)", caseNumberQtyConnection);
                caseNumberQtyCommand.Parameters.AddWithValue("@ReportStatus", "pending");
                caseNumberQtyCommand.Parameters.AddWithValue("@isNew", "0");

                SqlDataReader caseNumberQtyReader = caseNumberQtyCommand.ExecuteReader();
                caseNumberQtyReader.Close();
                noOfNewPendingReports.Text = caseNumberQtyCommand.ExecuteScalar().ToString();
                caseNumberQtyConnection.Close();

                //Retrieve NEW pending reports' details
                pendingReportsDetailsConnection.Open();
                SqlCommand pendingReportsDetailsCommand = new SqlCommand("SELECT Username, CaseNumber, Subject FROM Report WHERE(ReportStatus = @ReportStatus AND isNew = @isNew)", pendingReportsDetailsConnection);
                pendingReportsDetailsCommand.Parameters.AddWithValue("@ReportStatus", "pending");
                pendingReportsDetailsCommand.Parameters.AddWithValue("@isNew", "0");

                SqlDataReader pendingReportsDetailsReader = pendingReportsDetailsCommand.ExecuteReader();
                while (pendingReportsDetailsReader.Read())
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "alert", "$('#myModal2').modal('show')", true);

                    //Update 1st Login Access to TRUE to make sure it doesn't pop up again within this session
                    firstLoginAccessConnection.Open();
                    SqlCommand firstLoginAccessCommandUpdate = new SqlCommand("UPDATE UserAccount SET isFirstTimeAccessed = @isFirstTimeAccessed WHERE Username = @AccountUsername", firstLoginAccessConnection);
                    firstLoginAccessCommandUpdate.Parameters.AddWithValue("@isFirstTimeAccessed", "1");
                    firstLoginAccessCommandUpdate.Parameters.AddWithValue("@AccountUsername", Session["AccountUsername"].ToString());
                    firstLoginAccessCommandUpdate.ExecuteNonQuery();
                    firstLoginAccessConnection.Close();


                }
                pendingReportsDetailsConnection.Close();


            }
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

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DataCommand")
            {
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                string firstArgVal = commandArgs[0];
                string secondArgVal = commandArgs[1];

                Session["usernameOfThisPendingReport"] = firstArgVal;
                Session["caseNumberOfThisPendingReport"] = secondArgVal;

                SqlConnection updateReportStatusToOldConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["FileDatabaseConnectionString2"].ConnectionString);

                updateReportStatusToOldConnection.Open();

                SqlCommand updateOldOrNewCommand = new SqlCommand("UPDATE Report SET isNew = @isNew WHERE Username = @AccountUsername AND CaseNumber = @CaseNumber", updateReportStatusToOldConnection);
                updateOldOrNewCommand.Parameters.AddWithValue("@isNew", "1");
                updateOldOrNewCommand.Parameters.AddWithValue("@AccountUsername", Session["usernameOfThisPendingReport"].ToString());
                updateOldOrNewCommand.Parameters.AddWithValue("@CaseNumber", Session["caseNumberOfThisPendingReport"].ToString());

                updateOldOrNewCommand.ExecuteNonQuery();
                updateReportStatusToOldConnection.Close();

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

        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (dbIsFirstTimeAccessed == "True")
            {
                //Retrieve NEW pending reports' details
                pendingReportsDetailsConnection.Open();
                SqlCommand pendingReportsDetailsCommand = new SqlCommand("SELECT Username, CaseNumber, Subject FROM Report WHERE(ReportStatus = @ReportStatus AND isNew = @isNew)", pendingReportsDetailsConnection);
                pendingReportsDetailsCommand.Parameters.AddWithValue("@ReportStatus", "pending");
                pendingReportsDetailsCommand.Parameters.AddWithValue("@isNew", "0");

                SqlDataReader pendingReportsDetailsReader = pendingReportsDetailsCommand.ExecuteReader();
                while (pendingReportsDetailsReader.Read())
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "alert", "$('#myModal2').modal('show')", true);
                }
                pendingReportsDetailsConnection.Close();
            }

        }
    }
}