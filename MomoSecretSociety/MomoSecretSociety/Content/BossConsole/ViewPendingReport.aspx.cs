using Spire.Pdf;
using Spire.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
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
            //To make sure do not allow staff to access boss console through browser
            if (Context.User.Identity.Name != "KaiTatL97")
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Dear " + Session["AccountUsername"].ToString() + ", you are not allowed to access this page.'); window.location = '../../Account/Login.aspx'; ", true);

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

            if (Session["caseNumberOfThisPendingReport"] == null || Session["usernameOfThisPendingReport"] == null)
            {
                return;
            }
            else
            {

                //This should be on click of the particular report then will appear
                string dbCaseNumber = "";
                string dbUsername = "";
                DateTime dbDate = DateTime.Now;
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
                    dbDate = (DateTime)(myReader["Date"]);
                    dbSubject = (myReader["Subject"].ToString());
                    dbDescription = (myReader["Description"].ToString());
                    dbReportStatus = (myReader["ReportStatus"].ToString());
                }

                connection.Close();

                Label2.Text = dbCaseNumber;
                Label4.Text = dbDate.ToString("dd/MM/yyyy");
                Label6.Text = dbUsername;
                Label8.Text = dbSubject;
                Label10.Text = Decrypt(dbDescription);
                //Label10.Text = Decrypt(Label10.Text.Trim());
            }
            
        }

        public static string Decrypt(string cipherText)
        {
            //Label8.Text = this.Decrypt(Label8.Text.Trim());
            //Label10.Text = this.Decrypt(Label10.Text.Trim());
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
            //Response.Redirect("BossAcceptedReports.aspx");
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

                        //Add to logs
                        ActionLogs.Action action = ActionLogs.Action.ReauthenticatedDueToAccountLockout;
                        ActionLogs.Log(Context.User.Identity.Name, action);

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


        //public static string dbCaseNumber = "";
        //public static string dbUsername = "";
        //public static string dbDate = "";
        //public static string dbSubject = "";
        //public static string dbDescription = "";
        //public static string dbRemarks = "";
        //public static string dbCreatedDateTime = "";

        //public static string displayCaseNumber = "";

        protected void Button_Approve_Click(object sender, EventArgs e)
        {
            connection.Open();

            SqlCommand updateReportStatus = new SqlCommand("UPDATE Report SET ReportStatus = @ReportStatus, Remarks = @Remarks WHERE Username = @AccountUsername AND CaseNumber = @CaseNumber", connection);
            //SqlCommand updateReportStatus = new SqlCommand("UPDATE Report SET ReportStatus = @ReportStatus, AlertIsDisplayed = @AlertIsDisplayed WHERE Username = @AccountUsername AND CaseNumber = @CaseNumber", connection);
            updateReportStatus.Parameters.AddWithValue("@ReportStatus", "accepted");
            //updateReportStatus.Parameters.AddWithValue("@AlertIsDisplayed", "1");
            updateReportStatus.Parameters.AddWithValue("@AccountUsername", Label6.Text);
            updateReportStatus.Parameters.AddWithValue("@CaseNumber", Session["caseNumberOfThisPendingReport"].ToString());
            updateReportStatus.Parameters.AddWithValue("@Remarks", Label12_remarks.Text);

            updateReportStatus.ExecuteNonQuery();

            connection.Close();

            caseNumberOfReport = Session["caseNumberOfThisPendingReport"].ToString();


            //Add to logs
            ActionLogs.Action action = ActionLogs.Action.BossApprovedReport;
            ActionLogs.Log(Context.User.Identity.Name, action);

            Session["approvedMsg"] = "Report with the Case Number of <b><u><big>#" + Session["caseNumberOfThisPendingReport"].ToString() + "</b></u></big> has been <b>approved</b>.";
            Response.Redirect("~/Content/BossConsole/PendingReports.aspx");

            //ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Report with the Case Number of #" + Session["caseNumberOfThisPendingReport"].ToString() + " has been approved.'); window.location = 'PendingReports.aspx'; ", true);

            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('This report has been approved.')", true);

        }

        public static string caseNumberOfReport = "";

        protected void Button_Reject_Click(object sender, EventArgs e)
        {
            connection.Open();

            SqlCommand updateReportStatus = new SqlCommand("UPDATE Report SET ReportStatus = @ReportStatus, Remarks = @Remarks WHERE Username = @AccountUsername AND CaseNumber = @CaseNumber", connection);
            updateReportStatus.Parameters.AddWithValue("@ReportStatus", "rejected");
            updateReportStatus.Parameters.AddWithValue("@AccountUsername", Label6.Text);
            updateReportStatus.Parameters.AddWithValue("@CaseNumber", Session["caseNumberOfThisPendingReport"].ToString());
            updateReportStatus.Parameters.AddWithValue("@Remarks", Label12_remarks.Text);
            updateReportStatus.ExecuteNonQuery();

            connection.Close();

            caseNumberOfReport = Session["caseNumberOfThisPendingReport"].ToString();

            //Add to logs
            ActionLogs.Action action = ActionLogs.Action.BossRejectedReport;
            ActionLogs.Log(Context.User.Identity.Name, action);

            Session["rejectedMsg"] = "Report with the Case Number of <b><u><big>#" + Session["caseNumberOfThisPendingReport"].ToString() + "</b></u></big> has been <b>rejected</b>.";
            Response.Redirect("~/Content/BossConsole/PendingReports.aspx");

            //ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Report with the Case Number of #" + Session["caseNumberOfThisPendingReport"].ToString() + " has been rejected.'); window.location = 'PendingReports.aspx'; ", true);

            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('This report has been rejected.')", true);

        }
    }



}