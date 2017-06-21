using Spire.Pdf;
using Spire.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MomoSecretSociety.Content.BossConsole
{
    public partial class ViewPendingReport : System.Web.UI.Page
    {
        static SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FileDatabaseConnectionString2"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["caseNumberOfThisPendingReport"] == null || Session["caseNumberOfThisPendingReport"] == null)
            {
                return;
            }


            if (Request.IsAuthenticated)
            {
                ((Label)Master.FindControl("lastLoginBoss")).Text = "Your last logged in was <b>"
                            + ActionLogs.getLastLoggedInOf(Context.User.Identity.Name) + "</b>";
            }

            if (IsPostBack)
            {
                errormsgPasswordAuthenticate.Visible = false;
            }

            //This should be on click of the particular report then will appear
            string dbCaseNumber = "";
            string dbUsername = "";
            string dbDate = "";
            string dbSubject = "";
            string dbDescription = "";
            string dbRemarks = "";
            string dbReportStatus = "";

            connection.Open();
            SqlCommand myCommand = new SqlCommand("SELECT CaseNumber, Username, Date, Subject, Description, ReportStatus FROM Report WHERE CaseNumber = @caseNo AND Username = @AccountUsername", connection);
            myCommand.Parameters.AddWithValue("@caseNo", Session["caseNumberOfThisPendingReport"].ToString());
            myCommand.Parameters.AddWithValue("@AccountUsername", Session["usernameOfThisPendingReport"].ToString());

            //Hardcoded the case number - next time change to auto input when onclick of the particular report
            SqlDataReader myReader = myCommand.ExecuteReader();


            while (myReader.Read())
            {
                dbCaseNumber = (myReader["CaseNumber"].ToString());
                dbUsername = (myReader["Username"].ToString());
                dbDate = (myReader["Date"].ToString());
                dbSubject = (myReader["Subject"].ToString());
                dbDescription = (myReader["Description"].ToString());
                dbReportStatus = (myReader["ReportStatus"].ToString());
            }

            connection.Close();

            Label2.Text = dbCaseNumber;
            Label4.Text = dbDate;
            Label6.Text = dbUsername;
            Label8.Text = dbSubject;
            Label10.Text = dbDescription;
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


        public static string dbCaseNumber = "";
        public static string dbUsername = "";
        public static string dbDate = "";
        public static string dbSubject = "";
        public static string dbDescription = "";
        public static string dbRemarks = "";
        public static string dbCreatedDateTime = "";

        protected void Button_Approve_Click(object sender, EventArgs e)
        {
            connection.Open();

            SqlCommand updateReportStatus = new SqlCommand("UPDATE Report SET ReportStatus = @ReportStatus WHERE Username = @AccountUsername AND CaseNumber = @CaseNumber", connection);
            updateReportStatus.Parameters.AddWithValue("@ReportStatus", "accepted");
            updateReportStatus.Parameters.AddWithValue("@AccountUsername", Session["usernameOfThisPendingReport"].ToString());
            updateReportStatus.Parameters.AddWithValue("@CaseNumber", Session["caseNumberOfThisPendingReport"].ToString());

            updateReportStatus.ExecuteNonQuery();

            connection.Close();

            //Add to logs
            ActionLogs.Action action = ActionLogs.Action.BossApprovedReport;
            ActionLogs.Log(Context.User.Identity.Name, action);

            Response.Redirect("~/Content/BossConsole/PendingReports.aspx");
        }

        protected void Button_Reject_Click(object sender, EventArgs e)
        {
            connection.Open();

            SqlCommand updateReportStatus = new SqlCommand("UPDATE Report SET ReportStatus = @ReportStatus WHERE Username = @AccountUsername AND CaseNumber = @CaseNumber", connection);
            updateReportStatus.Parameters.AddWithValue("@ReportStatus", "rejected");
            updateReportStatus.Parameters.AddWithValue("@AccountUsername", Session["usernameOfThisPendingReport"].ToString());
            updateReportStatus.Parameters.AddWithValue("@CaseNumber", Session["caseNumberOfThisPendingReport"].ToString());
            updateReportStatus.ExecuteNonQuery();

            connection.Close();

            //Add to logs
            ActionLogs.Action action = ActionLogs.Action.BossRejectedReport;
            ActionLogs.Log(Context.User.Identity.Name, action);

            Response.Redirect("~/Content/BossConsole/PendingReports.aspx");
        }
    }



}