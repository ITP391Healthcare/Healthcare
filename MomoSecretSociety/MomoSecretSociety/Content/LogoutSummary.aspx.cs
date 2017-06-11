using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MomoSecretSociety.Content
{
    public partial class LogoutSummary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AccountUsername"] != null)
            {
                Label_username.Text = Session["AccountUsername"].ToString();

                DataTable dt = ActionLogs.showLogSummary(Session["AccountUsername"].ToString());

                GridView1.DataSource = dt;
                GridView1.DataBind();
                
                Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            }

        }
    }
}