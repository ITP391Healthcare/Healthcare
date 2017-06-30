using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MomoSecretSociety.Content.BossConsole
{
    public partial class testPageWithErrors2NULL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Error 2: Divide By Zero Exception
             
            int result = 15 / int.Parse("0");
            Response.Write(result);
            

        }
    }
}