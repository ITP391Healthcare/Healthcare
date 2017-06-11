using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MomoSecretSociety.Content.BossConsole
{
    public partial class PendingReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+ ActionLogs.getLastLoggedInOf(Context.User.Identity.Name) + "" + "');", true);

            if (Request.IsAuthenticated)
            {
                ((Label)Master.FindControl("lastLogin")).Text = "Your last logged in was <b>"
                            + ActionLogs.getLastLoggedInOf(Context.User.Identity.Name) + "</b>";
            }


            //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + ActionLogs.getLastLoggedInOf(Context.User.Identity.Name) + "" + "');", true);


        }
    }
}