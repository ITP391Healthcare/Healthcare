using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MomoSecretSociety.Content
{
    public partial class LogoutSummary : System.Web.UI.Page
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FileDatabaseConnectionString2"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AccountUsername"] != null)
            {
                Label_username.Text = Session["AccountUsername"].ToString();

                DataTable dt = ActionLogs.showLogSummary(Session["AccountUsername"].ToString());

                GridView1.DataSource = dt;
                GridView1.DataBind();
                
                Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

                connection.Open();
                SqlCommand updateFirstLoginAccess = new SqlCommand("UPDATE UserAccount SET isFirstTimeAccessed = @isFirstTimeAccessed WHERE Username = @AccountUsername", connection);
                updateFirstLoginAccess.Parameters.AddWithValue("@isFirstTimeAccessed", "0");
                updateFirstLoginAccess.Parameters.AddWithValue("@AccountUsername", Session["AccountUsername"].ToString());
                updateFirstLoginAccess.ExecuteNonQuery();
                connection.Close();
            }

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }
    }
}