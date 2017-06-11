using Microsoft.AspNet.Identity;
using MomoSecretSociety.Content;
using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MomoSecretSociety
{
    public partial class ConsoleBossSiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {

            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;


        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;

            }
            else
            {

                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }

            }




        }


        // public string CustomTitle = "This Is Title";
        protected void Page_Load(object sender, EventArgs e)
        {
            //Page.DataBind();

            //Response.Write("XXX: " + Session["AccountUsername"].ToString());
            //Response.Write("ZZZ: " + ActionLogs.getLastLoggedInOf(Context.User.Identity.Name));

            ////Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+ ActionLogs.getLastLoggedInOf(Context.User.Identity.Name) + "" + "');", true);

            //if (Session["AccountUsername"] != null)
            //{
            //    Response.Write("XXX: " + Session["AccountUsername"].ToString());

            //    ((Label)FindControl("lastLogin")).Text = "Your last logged in was <b>"
            //                + ActionLogs.getLastLoggedInOf(Context.User.Identity.Name) + "</b>";
            //}


            //if (Session["AccountUsername"] != null)
            //{
            //    ((PlaceHolder)FindControl("staffConsoleNavBar")).Visible = true;
            //    ((LinkButton)FindControl("loginNavBar")).Text = "LOG OUT";

            //    Response.Write("XXX: " + Session["AccountUsername"].ToString());
            // }

            //if (IsPostBack)
            //{
            //    if (Request.IsAuthenticated)
            //    {
            //        //    Session["AccountUsername"].ToString() == "boss"
            //        LinkButton Foo = (LinkButton)Master.FindControl("logNavBar");
            //        if (Foo.Visible == false)
            //        {
            //            Foo.Visible = true;
            //        }

            //        Response.Write(Session["AccountUsername"].ToString());

            //    }
            //}
        }


        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            // FormsAuthentication.SignOut();
            Session["AccountUsername"] = Context.User.Identity.Name;

            //Add to logs
             ActionLogs.Action action = ActionLogs.Action.Logout;
             ActionLogs.Log(Session["AccountUsername"].ToString(), action);
            

            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            //Response.Cache.SetNoStore();

            //Clear cookies
            //string[] cookies = Request.Cookies.AllKeys;
            //foreach (string cookie in cookies)
            //{
            //    Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
            //}

            //FormsAuthentication.SignOut();
            //Session.Abandon();
            //FormsAuthentication.RedirectToLoginPage();
            //HttpContext.Current.ApplicationInstance.CompleteRequest();


        }


        //protected void logNavBar_Click(object sender, EventArgs e)
        //{
        //    if (Request.IsAuthenticated)
        //    {
        //        if (Context.User.Identity.Name != "boss")
        //        {
        //            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You are not authorized to do so.');", true);
        //        }
        //    }
        //}

    }

}