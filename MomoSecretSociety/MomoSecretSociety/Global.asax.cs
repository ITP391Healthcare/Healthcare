using Haxriderz;
using MomoSecretSociety.Content;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace MomoSecretSociety
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }


        //protected void Page_Error(object sender, EventArgs e)
        //{
        //    Exception Ex = Server.GetLastError().GetBaseException();

        //    Server.ClearError();


        //    string dbRole = "";
        //    SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FileDatabaseConnectionString2"].ConnectionString);

        //    connection.Open();
        //    SqlCommand myCommand = new SqlCommand("SELECT Role FROM UserAccount WHERE Username = @AccountUsername", connection);
        //    myCommand.Parameters.AddWithValue("@AccountUsername", Context.User.Identity.Name);

        //    SqlDataReader myReader = myCommand.ExecuteReader();
        //    while (myReader.Read())
        //    {
        //        dbRole = (myReader["Role"].ToString());
        //    }
        //    connection.Close();


        //    if (dbRole.Equals("Boss"))
        //    {
        //        Response.Redirect("~/Content/BossConsole/ErrorPages/Error.aspx");
        //    }
        //    else
        //    {
        //        Response.Redirect("~/Content/StaffConsole/ErrorPages/Error.aspx");
        //    }


        //}

        //protected void Application_Error(object sender, EventArgs e)
        //{

        //    Exception Ex = Server.GetLastError().GetBaseException();

        //    Server.ClearError();

        //    ActionLogs.LogExceptionError(Context.User.Identity.Name, Ex, System.Web.HttpContext.Current.Request.Url.ToString());


        //    string dbRole = "";
        //    SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FileDatabaseConnectionString2"].ConnectionString);

        //    connection.Open();
        //    SqlCommand myCommand = new SqlCommand("SELECT Role FROM UserAccount WHERE Username = @AccountUsername", connection);
        //    myCommand.Parameters.AddWithValue("@AccountUsername", Context.User.Identity.Name);

        //    SqlDataReader myReader = myCommand.ExecuteReader();
        //    while (myReader.Read())
        //    {
        //        dbRole = (myReader["Role"].ToString());
        //    }
        //    connection.Close();


        //    if (dbRole.Equals("Boss"))
        //    {
        //        Response.Redirect("~/Content/BossConsole/ErrorPages/Error.aspx");
        //       // Server.Transfer("~/Content/BossConsole/ErrorPages/Error.aspx");
        //    }
        //    else
        //    {
        //        Response.Redirect("~/Content/StaffConsole/ErrorPages/Error.aspx");
        //        // Server.Transfer("~/Content/StaffConsole/ErrorPages/Error.aspx");
        //    }


        //}

    }
}