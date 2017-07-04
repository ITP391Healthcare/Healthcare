using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace MomoSecretSociety.Content.StaffConsole.ErrorPages
{
    public partial class demoPageWithError2DivideByZero : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Error 2: Divide By Zero Exception

            int result = 15 / int.Parse("0");
            Response.Write(result);
        }
    }
}