using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MomoSecretSociety.Content.StaffConsole
{
    public partial class Drafts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                ((Label)Master.FindControl("lastLoginStaff")).Text = "Your last logged in was <b>"
                            + ActionLogs.getLastLoggedInOf(Context.User.Identity.Name) + "</b>";
            }

        }
    }
}