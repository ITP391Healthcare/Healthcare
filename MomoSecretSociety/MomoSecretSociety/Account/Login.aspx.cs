using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MomoSecretSociety.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Validate the user password
                if (username.Text.Equals("staff") && Password.Text.Equals("staff"))
                {
                    Session["AccountUsername"] = username.Text;

                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, username.Text, DateTime.Now, DateTime.Now.AddMinutes(10), false, username.Text);
                    String encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    Response.Cookies.Add(authCookie);
                    Response.Redirect("~/Content/StaffConsole/SubmittedReports.aspx");

                }

                if (username.Text.Equals("boss") && Password.Text.Equals("boss"))
                {
                    Session["AccountUsername"] = username.Text;

                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, username.Text, DateTime.Now, DateTime.Now.AddMinutes(10), false, username.Text);
                    String encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    Response.Cookies.Add(authCookie);
                    Response.Redirect("~/Content/BossConsole/PendingReports.aspx");
                }
            }
        }

    }
}