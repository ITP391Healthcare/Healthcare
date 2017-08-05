using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MomoSecretSociety.Content.BossConsole.ErrorPages
{
    public partial class demoPageWithError3SQL : System.Web.UI.Page
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FileDatabaseConnectionString2"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            string abc;

            SqlCommand myCommand = new SqlCommand("SELECT HashedPassword, Salt, Role, Username FROM UserAccount WHERE Username = @AccountUsername", connection);
            myCommand.Parameters.AddWithValue("@AccountUsername", Context.User.Identity.Name);

            SqlDataReader myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                abc = (myReader["HashedPassword"].ToString());
            }
            
        }
    }
}