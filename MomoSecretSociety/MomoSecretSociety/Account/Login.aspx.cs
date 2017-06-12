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

namespace MomoSecretSociety.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Joanne, dont delete these codes
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

            string passwordHash = "";
            string test = "";
            //ComputeHash(inputPassword, "SHA512", dbSalt);
            //ComputeHash (inputPassword, new SHA512CryptoServiceProvider(), Convert.FromBase64String(dbSalt))
            //INPUT PASSWORD + dbSalt --> HASH THE WHOLE THING
            //Compare: passwordHash with dbPasswordHash
            //Compare: dbUsername with username.Text (input username)

            //Joanne, dont delete these codes
            if (IsValid)
            {
                // Validate the user password
                //if (dbUsername.Equals(inputUsername) && dbPasswordHash.Equals(passwordHash) && dbStatus.Equals("staff"))
                //AND CHECK STATUS = staff
                if (username.Text.Equals("staff") && password.Text.Equals("staff"))
                {
                    Session["AccountUsername"] = username.Text;

                    //Add to logs
                    ActionLogs.Action action = ActionLogs.Action.Login;
                    ActionLogs.Log(username.Text, action);

                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, username.Text, DateTime.Now, DateTime.Now.AddMinutes(10), false, username.Text);
                    String encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    Response.Cookies.Add(authCookie);
                    Response.Redirect("~/Content/StaffConsole/SubmittedReports.aspx");
                }
                else
                {
                    IncorrectInputLabel.Text = "incorrect input";
                }

                //if (dbUsername.Equals(inputUsername) && dbPasswordHash.Equals(passwordHash) && dbStatus.Equals("boss"))
                //AND CHECK STATUS = boss
                if (username.Text.Equals("boss") && password.Text.Equals("boss"))
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

        public static String ComputeHash (string input, HashAlgorithm algorithm, Byte[] salt)
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