using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MomoSecretSociety.Content.StaffConsole
{
    public partial class testRedirectStaff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Request.Url.ToString() == "http://localhost:53380/Content/BossConsole/PendingReports")
            {
                if (Context.User.Identity.Name != "KaiTatL97")
                {

                    Page.ClientScript.RegisterStartupScript(GetType(), "alert", "cannot go boss la ", true);
                    
                }
            }
        }
    }
}