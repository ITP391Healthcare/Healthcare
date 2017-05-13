using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MomoSecretSociety.Account
{
    public partial class _2FA : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //CHECK IF ALL DIGITS
            //Verify if 2FA is correct or not 
            //If match, redirect to either staff/boss page depending on the roles
            //If not match, error message
        }

    }
}