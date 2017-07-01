using System;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//Joanne

namespace MomoSecretSociety.Content.StaffConsole
{
    public partial class NewReport : System.Web.UI.Page
    {
        int cNumber;
        static SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FileDatabaseConnectionString2"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                ((Label)Master.FindControl("lastLoginStaff")).Text = "Your last logged in was <b>"
                            + ActionLogs.getLastLoggedInOf(Context.User.Identity.Name) + "</b>";
            }

            if (IsPostBack)
            {
                errormsgPasswordAuthenticate.Visible = false;
            }


            //At page load, the name of the person who sign in will fill in the FROM input box automatically
            //Unable to edit
            //TextBox3.Text = Context.User.Identity.Name;
            TextBox3.Text = Session["AccountUsername"].ToString();
            TextBox3.ReadOnly = true;
        }
            int CaseNumber = 201700000;

        //Encryption of subject and description

        private string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        private string Decrypt(string cipherText)
        {
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
        }


        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            //string uname = Context.User.Identity.Name;
            string uname = Session["AccountUsername"].ToString();
            //Case Number Created +1 
            //Retrieve the latest case number and +1
            string dbCaseNumber="";
            connection.Open();
            SqlCommand myCommand = new SqlCommand("SELECT CaseNumber FROM Report", connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                dbCaseNumber = (myReader["CaseNumber"].ToString());
            }

            cNumber = int.Parse(dbCaseNumber);
            cNumber++;
            connection.Close();

            //Converting input date into datetime type input
            DateTime DateInput = new DateTime();
            DateInput = Convert.ToDateTime(TextBox4.Text);

            //Getting the date time when submit drafts/save reports as drafts
            DateTime createdDateTime = new DateTime();
            createdDateTime = DateTime.Now;

            string NameInput = TextBox3.Text;
            string SubjectInput = TextBox2.Text;
            string CaseDesInput = TextBox1.Text;
            string status = "pending";


            connection.Open();

            
            //(KT)
            string constr = ConfigurationManager.ConnectionStrings["FileDatabaseConnectionString2"].ConnectionString;
            //using (SqlConnection con = new SqlConnection(constr))
            //{
            //    con.Open();
            //    using (SqlCommand cmd = new SqlCommand("INSERT INTO Report (Subject, Description) VALUES(@Subject, @Description)"))
            //    {
            //        cmd.CommandType = CommandType.Text;
            //        cmd.Parameters.AddWithValue("@Subject", Encrypt(TextBox2.Text.Trim()));
            //        cmd.Parameters.AddWithValue("@Description", Encrypt(TextBox1.Text.Trim()));
            //        cmd.Connection = con;
                    
            //        cmd.ExecuteNonQuery();
            //        con.Close();
            //    }
            //}
            //Response.Redirect(Request.Url.AbsoluteUri);

            SqlCommand insertReportCommand = new SqlCommand();
            insertReportCommand.CommandText = "INSERT INTO Report (CaseNumber, Username, Date, Subject, Description, Remarks, ReportStatus, CreatedDateTime)" + 
                " VALUES (@caseNumber, @username, @date, @subject, @description, @remarks, @status, @createdDT)";
            insertReportCommand.Parameters.AddWithValue("@caseNumber", cNumber);
            insertReportCommand.Parameters.AddWithValue("@username", NameInput);
            insertReportCommand.Parameters.AddWithValue("@date", DateInput);
            insertReportCommand.Parameters.AddWithValue("@subject", SubjectInput);
            insertReportCommand.Parameters.AddWithValue("@description", CaseDesInput);
            //insertReportCommand.Parameters.AddWithValue("@subject", Encrypt(TextBox2.Text.Trim()));
            //insertReportCommand.Parameters.AddWithValue("@description", Encrypt(TextBox1.Text.Trim()));
            insertReportCommand.Parameters.AddWithValue("@Remarks", "");
            insertReportCommand.Parameters.AddWithValue("@status", status);
            insertReportCommand.Parameters.AddWithValue("@createdDT", createdDateTime);


            insertReportCommand.Connection = connection;
            insertReportCommand.ExecuteNonQuery();
            connection.Close();

            //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert",
            //"alert('Case #'" + cNumber + "' has been created.');" + "window.location = 'SubmittedReports.aspx'; ", true);

            string message = "Case #" + cNumber + " has been created.";
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + message + "'); window.location = 'SubmittedReports.aspx'; ", true);
        }

        protected void SaveAsDraftsButton_Click(object sender, EventArgs e)
        {
            //COPY PASTED FROM SUBMIT BUTTON
            //Flow: create a case record in the reports db

            //Case Number Created +1 
            //Retrieve the latest case number and +1
            string dbCaseNumber = "";
            connection.Open();
            SqlCommand myCommand = new SqlCommand("SELECT CaseNumber FROM Report", connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                dbCaseNumber = (myReader["CaseNumber"].ToString());
            }

            cNumber = int.Parse(dbCaseNumber);
            connection.Close();
            cNumber++;

            //Converting input date into datetime type input
            DateTime DateInput = new DateTime();
            DateInput = Convert.ToDateTime(TextBox4.Text);

            //Getting the date time when submit drafts/save reports as drafts
            DateTime createdDateTime = new DateTime();
            createdDateTime = DateTime.Now;

            string NameInput = TextBox3.Text;
            string SubjectInput = TextBox2.Text;
            string CaseDesInput = TextBox1.Text;
            string status = "drafts";

            //Add the details into database (done)
            //Report inserted into database, with ReportStatus = drafts (done)
            //Report details encrypted (not done)

            connection.Open();

            SqlCommand insertReportCommand = new SqlCommand();
            insertReportCommand.CommandText = "INSERT INTO Report (CaseNumber, Username, Date, Subject, Description, Remarks, ReportStatus, CreatedDateTime)" +
                " VALUES (@caseNumber, @username, @date, @subject, @description, @remarks, @status, @createdDT)";
            insertReportCommand.Parameters.AddWithValue("@caseNumber", cNumber);
            insertReportCommand.Parameters.AddWithValue("@username", NameInput);
            insertReportCommand.Parameters.AddWithValue("@date", DateInput);
            insertReportCommand.Parameters.AddWithValue("@subject", SubjectInput);
            insertReportCommand.Parameters.AddWithValue("@description", CaseDesInput);
            insertReportCommand.Parameters.AddWithValue("@Remarks", "");
            insertReportCommand.Parameters.AddWithValue("@status", status);
            insertReportCommand.Parameters.AddWithValue("@createdDT", createdDateTime);


            insertReportCommand.Connection = connection;
            insertReportCommand.ExecuteNonQuery();
            connection.Close();

            //alert
            string message = "Your report has been saved in drafts!";
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + message + "'); window.location = 'SubmittedReports.aspx'; ", true);
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
    }

}