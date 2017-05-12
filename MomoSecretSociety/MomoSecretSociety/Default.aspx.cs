using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MomoSecretSociety
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Context.User.Identity.IsAuthenticated)
            //{
            //    hihihi.Text = "in le";
            //}

            //if (Session["AccountUsername"] != null)
            //{
            //    ((PlaceHolder)FindControl("staffConsoleNavBar")).Visible = true;
            //    ((LinkButton)FindControl("loginNavBar")).Text = "LOG OUT";
            //    hihihi.Text = "in le";
            //    Response.Write("XXX: " + Session["AccountUsername"].ToString());
            //}
        }
    }
}