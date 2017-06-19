using Microsoft.AspNet.Identity;
using MomoSecretSociety.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
//Joanne
namespace MomoSecretSociety.Account
{
    public partial class Login : System.Web.UI.Page
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FileDatabaseConnectionString2"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                string[] cookies = Request.Cookies.AllKeys;
                foreach (string cookie in cookies)
                {
                    Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
                }
                HttpContext.Current.Session.Abandon();
                HttpContext.Current.Response.Cookies.Add(new HttpCookie("__AntiXsrfToken", ""));
            }

            if (!IsPostBack && Request.IsAuthenticated)
            {
                connection.Open();
                SqlCommand updateFirstLoginAccess = new SqlCommand("UPDATE UserAccount SET hasAccessed = @hasAccessed", connection);
                updateFirstLoginAccess.Parameters.AddWithValue("@hasAccessed", "0");
                updateFirstLoginAccess.ExecuteNonQuery();
                connection.Close();

                Response.Redirect(Request.RawUrl);
            }

        }

        protected void LogIn(object sender, EventArgs e)
        {
            string inputUsername = username.Text;
            string inputPassword = password.Text;

            string dbUsername = "";
            string dbPasswordHash = "";
            string dbSalt = "";
            string dbStatus = "";

            connection.Open();
            SqlCommand myCommand = new SqlCommand("SELECT HashedPassword, Salt, Role, Username FROM UserAccount WHERE Username = @AccountUsername", connection);
            myCommand.Parameters.AddWithValue("@AccountUsername", inputUsername);

            SqlDataReader myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                dbPasswordHash = (myReader["HashedPassword"].ToString());
                dbSalt = (myReader["Salt"].ToString());
                dbStatus = (myReader["Role"].ToString());
                dbUsername = (myReader["Username"].ToString());
            }
            connection.Close();

            string passwordHash = ComputeHash(inputPassword, new SHA512CryptoServiceProvider(), Convert.FromBase64String(dbSalt));

            if (IsValid)
            {
                if (dbUsername.Equals(inputUsername.Trim()))
                {
                    if (dbPasswordHash.Equals(passwordHash))
                    {
                        if (dbStatus.Equals("Staff"))
                        {
                            Session["AccountUsername"] = inputUsername;

                            //Add to logs
                            ActionLogs.Action action = ActionLogs.Action.Login;
                            ActionLogs.Log(username.Text, action);

                            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, username.Text, DateTime.Now, DateTime.Now.AddMinutes(10), false, username.Text);
                            String encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                            Response.Cookies.Add(authCookie);

                            Response.Redirect("~/Content/StaffConsole/NewReport.aspx");

                        }
                    }
                    else
                    {
                        IncorrectInputLabel.Text = "Incorrect username/password";
                    }

                }
                else
                {
                    IncorrectInputLabel.Text = "Incorrect username/password";
                }


                if (dbUsername.Equals(inputUsername) && dbPasswordHash.Equals(passwordHash) && dbStatus.Equals("Boss"))
                {
                    Session["AccountUsername"] = username.Text;

                    //Add to logs
                    ActionLogs.Action action = ActionLogs.Action.Login;
                    ActionLogs.Log(username.Text, action);

                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, username.Text, DateTime.Now, DateTime.Now.AddMinutes(10), false, username.Text);
                    String encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    Response.Cookies.Add(authCookie);

                    Response.Redirect("~/Content/BossConsole/PendingReports.aspx");
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