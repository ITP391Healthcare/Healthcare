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
    public partial class testPageWithErrors : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string dbPasswordHash = "";
            string dbSalt = "";
            string dbUsername = "";

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FileDatabaseConnectionString2"].ConnectionString);
            SqlConnection connection2 = new SqlConnection(ConfigurationManager.ConnectionStrings["FileDatabaseConnectionString2"].ConnectionString);

            connection.Open();
            SqlCommand myCommand = new SqlCommand("SELECT HashedPassword, Salt, Role, Username FROM UserAccount WHERE Username = @AccountUsername", connection);
            myCommand.Parameters.AddWithValue("@AccountUsername", Context.User.Identity.Name);

            SqlDataReader myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                dbPasswordHash = (myReader["HashedPassword"].ToString());
                dbSalt = (myReader["Salt"].ToString());
                dbUsername = (myReader["Username"].ToString());
            }

            connection2.Open();
            SqlCommand myCommand2 = new SqlCommand("SELECT HashedPassword, Salt, Role, Username FROM UserAccount WHERE Username = @AccountUsername", connection2);
            myCommand.Parameters.AddWithValue("@AccountUsername", Context.User.Identity.Name);

            SqlDataReader myReader2 = myCommand2.ExecuteReader();
            while (myReader2.Read())
            {
                dbPasswordHash = (myReader2["HashedPassword"].ToString());
                dbSalt = (myReader2["Salt"].ToString());
                dbUsername = (myReader2["Username"].ToString());
            }

            Response.Write(dbPasswordHash + dbSalt + dbUsername);


        }
    }
}