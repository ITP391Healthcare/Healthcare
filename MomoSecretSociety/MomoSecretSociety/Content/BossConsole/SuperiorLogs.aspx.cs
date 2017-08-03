using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MomoSecretSociety.Content.BossConsole
{
    public partial class SuperiorLogs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                ((Label)Master.FindControl("lastLoginBoss")).Text = "Your last logged in was <b>"
                            + ActionLogs.getLastLoggedInOf(Context.User.Identity.Name) + "</b>";

                bossUsername.Text = Context.User.Identity.Name;

                if (!IsPostBack)
                {
                    readLogsRespectively();
                }

            }


            if (IsPostBack)
            {
                errormsgPasswordAuthenticate.Visible = false;

                if (txtSearchValue.Text == null && txtSearchValueDate.Text == null)
                {
                    readLogsRespectively();
                }
            }

        }

        protected void btnAuthenticate_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string inputUsername = Context.User.Identity.Name;
                string inputPassword = txtPasswordAuthenticate.Text;

                string dbUsername = "";
                string dbPasswordHash = "";
                string dbSalt = "";

                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FileDatabaseConnectionString2"].ConnectionString);

                connection.Open();
                SqlCommand myCommand = new SqlCommand("SELECT HashedPassword, Salt, Role, Username FROM UserAccount WHERE Username = @AccountUsername", connection);
                myCommand.Parameters.AddWithValue("@AccountUsername", inputUsername);

                SqlDataReader myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    dbPasswordHash = (myReader["HashedPassword"].ToString());
                    dbSalt = (myReader["Salt"].ToString());
                    dbUsername = (myReader["Username"].ToString());
                }
                connection.Close();

                string passwordHash = ComputeHash(inputPassword, new SHA512CryptoServiceProvider(), Convert.FromBase64String(dbSalt));

                if (dbUsername.Equals(inputUsername.Trim()))
                {
                    if (dbPasswordHash.Equals(passwordHash))
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "alert", "$('#myModal').modal('hide')", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "alert", "$('#myModal').modal('show')", true);
                        errormsgPasswordAuthenticate.Visible = true;
                    }

                }
            }
        }

        public static String ComputeHash(string input, HashAlgorithm algorithm, Byte[] salt)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            Byte[] saltedInput = new Byte[salt.Length + inputBytes.Length];
            salt.CopyTo(saltedInput, 0);
            inputBytes.CopyTo(saltedInput, salt.Length);

            Byte[] hashedBytes = algorithm.ComputeHash(saltedInput);

            return BitConverter.ToString(hashedBytes);
        }
        private void readLogsRespectively()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FileDatabaseConnectionString2"].ConnectionString);

            connection.Open();
            SqlDataReader dateReader = null;
            SqlCommand dateCommand = new SqlCommand("SELECT DISTINCT(convert(date, Timestamp)) AS Date FROM Logs WHERE Username = @AccountUsername ORDER BY convert(date,Timestamp) DESC", connection);

            dateCommand.Parameters.AddWithValue("@AccountUsername", Context.User.Identity.Name);
            dateReader = dateCommand.ExecuteReader();

            while (dateReader.Read())
            {
                DateTime date = (DateTime)dateReader["Date"];
                //Response.Write("Date : " + date + "<br>");
                AddDateToPlaceholder(date);

                SqlConnection connection2 = new SqlConnection(ConfigurationManager.ConnectionStrings["FileDatabaseConnectionString2"].ConnectionString);
                connection2.Open();
                SqlDataReader logReader = null;
                SqlCommand logCommand = new SqlCommand("SELECT Action, Timestamp FROM Logs WHERE Username = @AccountUsername AND convert(date, Timestamp) = convert(date,@Date) ORDER BY Timestamp ASC", connection2);

                logCommand.Parameters.AddWithValue("@AccountUsername", Context.User.Identity.Name);
                logCommand.Parameters.AddWithValue("@Date", date);
                logReader = logCommand.ExecuteReader();

                while (logReader.Read())
                {
                    string action = logReader["Action"].ToString();
                    DateTime actionDate = (DateTime)logReader["Timestamp"];
                    //Response.Write("Date : " + actionDate + " Action : " + action + "<br>");
                    AddActionToPlaceholder(action, actionDate);
                }
            }
        }


        private void AddActionToPlaceholder(string action, DateTime actionDate)
        {
            HtmlGenericControl li = new HtmlGenericControl("li");

            HtmlGenericControl icon = new HtmlGenericControl("i");
            string iconStyle = "fa " + GetIconStyle(action);
            icon.Attributes.Add("class", iconStyle);
            li.Controls.Add(icon);


            HtmlGenericControl container = new HtmlGenericControl("div");
            container.Attributes.Add("class", "timeline-item");

            HtmlGenericControl timeholder = new HtmlGenericControl("span");
            timeholder.Attributes.Add("class", "time");

            HtmlGenericControl timeIcon = new HtmlGenericControl("i");
            timeIcon.Attributes.Add("class", "fa fa-clock-o");
            timeholder.Controls.Add(timeIcon);
            timeholder.Controls.Add(new LiteralControl(actionDate.ToString()));
            container.Controls.Add(timeholder);

            HtmlGenericControl header = new HtmlGenericControl("h3");
            header.Attributes.Add("class", "timeline-header no-border");
            header.InnerHtml = action.ToString();
            container.Controls.Add(header);


            li.Controls.Add(container);
            phTimeline.Controls.Add(li);


        }

        private void AddDateToPlaceholder(DateTime date)
        {
            HtmlGenericControl li = new HtmlGenericControl("li");
            li.Attributes.Add("class", "time-label");

            HtmlGenericControl span = new HtmlGenericControl("span");
            span.Attributes.Add("class", "bg-blue");
            span.InnerHtml = date.ToLongDateString();

            li.Controls.Add(span);
            phTimeline.Controls.Add(li);
        }


        private string GetIconStyle(string actionString)
        {
            if (actionString == "Login")
            {
                return "fa-sign-in bg-aqua";
            }
            else if (actionString == "Logout")
            {
                return "fa-sign-out bg-aqua";
            }
            else if (actionString == "Report was submitted")
            {
                return "fa-file-text bg-aqua";
            }
            else if (actionString.Contains("approved"))
            {
                return "fa-check-square-o bg-aqua";
            }
            else if (actionString.Contains("rejected"))
            {
                return "fa-exclamation-triangle bg-aqua";
            }
            else if (actionString == "Report saved to PDF")
            {
                return "fa-file-pdf-o bg-aqua";
            }
            else if (actionString == "Account Lockout")
            {
                return "fa-hourglass-o bg-aqua";
            }
            else if (actionString == "Re-authenticated due to Account Lockout")
            {
                return "fa-handshake-o bg-aqua";
            }
            else if (actionString == "Boss View Pending Report")
            {
                return "fa-edit bg-aqua";
            }

            return "fa-user bg-aqua";

        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bool hasData = false;

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FileDatabaseConnectionString2"].ConnectionString);

            connection.Open();
            SqlDataReader dateReader = null;
            SqlCommand dateCommand = new SqlCommand("SELECT DISTINCT(convert(date, Timestamp)) AS Date FROM Logs WHERE lower(Action) LIKE @Action AND Username = @AccountUsername ORDER BY convert(date,Timestamp) DESC", connection);

            dateCommand.Parameters.AddWithValue("@Action", "%" + txtSearchValue.Text.Trim().ToLower() + "%");
            dateCommand.Parameters.AddWithValue("@AccountUsername", bossUsername.Text);
            dateReader = dateCommand.ExecuteReader();

            while (dateReader.Read())
            {
                DateTime date = (DateTime)dateReader["Date"];
                //Response.Write("Date : " + date + "<br>");
                AddDateToPlaceholder(date);

                SqlConnection connection2 = new SqlConnection(ConfigurationManager.ConnectionStrings["FileDatabaseConnectionString2"].ConnectionString);
                connection2.Open();
                SqlDataReader logReader = null;
                SqlCommand logCommand = new SqlCommand("SELECT Action, Timestamp FROM Logs WHERE lower(Action) LIKE @Action AND Username = @AccountUsername AND convert(date, Timestamp) = convert(date,@Date) ORDER BY Timestamp ASC", connection2);

                logCommand.Parameters.AddWithValue("@Action", "%" + txtSearchValue.Text.Trim().ToLower() + "%");
                logCommand.Parameters.AddWithValue("@AccountUsername", bossUsername.Text);
                logCommand.Parameters.AddWithValue("@Date", date);
                logReader = logCommand.ExecuteReader();

                while (logReader.Read())
                {
                    hasData = true;

                    string action = logReader["Action"].ToString();
                    DateTime actionDate = (DateTime)logReader["Timestamp"];
                    //Response.Write("Date : " + actionDate + " Action : " + action + "<br>");
                    AddActionToPlaceholder(action, actionDate);

                }
            }

            if (hasData == false)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('There is no data found for this search.')", true);
            }

        }


        protected void btnSearchDate_Click(object sender, EventArgs e)
        {
            string s = txtSearchValueDate.Text;

            DateTime dt;
            if (DateTime.TryParse(s, out dt))
            {
                string date = s.ToString().Split(' ')[0];

                date = String.Format("{0:dd/MM/yyyy}", date);
                DateTime InputDate = Convert.ToDateTime(date);

                try
                {

                    SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FileDatabaseConnectionString2"].ConnectionString);

                    connection.Open();

                    SqlCommand dateCommand = new SqlCommand("SELECT DISTINCT(Timestamp) AS [DD/MM/YYYY] FROM LOGS WHERE Username = @AccountUsername AND convert(date, Timestamp, 103) = convert(date, @Timestamp, 103)", connection);

                    dateCommand.Parameters.AddWithValue("@AccountUsername", bossUsername.Text);
                    dateCommand.Parameters.AddWithValue("@Timestamp", InputDate);

                    var dbDate = (DateTime)dateCommand.ExecuteScalar();

                    if (dbDate != null)
                    {
                        AddDateToPlaceholder(dbDate);

                        SqlConnection connection2 = new SqlConnection(ConfigurationManager.ConnectionStrings["FileDatabaseConnectionString2"].ConnectionString);
                        connection2.Open();
                        SqlDataReader logReader = null;
                        SqlCommand logCommand = new SqlCommand("SELECT Action, Timestamp FROM Logs WHERE Username = @AccountUsername AND convert(date, Timestamp, 103) = convert(date, @Timestamp, 103) ORDER BY convert(date,Timestamp,103) ASC", connection2);

                        logCommand.Parameters.AddWithValue("@AccountUsername", bossUsername.Text);
                        logCommand.Parameters.AddWithValue("@Timestamp", dbDate);
                        logReader = logCommand.ExecuteReader();

                        while (logReader.Read())
                        {
                            string action = logReader["Action"].ToString();
                            string actionDate = logReader["Timestamp"].ToString();
                            //Response.Write("Date : " + actionDate + " Action : " + action + "<br>");
                            DateTime actionDateDT = Convert.ToDateTime(actionDate);

                            AddActionToPlaceholder(action, actionDateDT);

                        }

                    }


                }
                catch (System.NullReferenceException exc)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('There is no data found for this search.')", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please check that you have entered a correct format in DD/MM/YYYY.')", true);
            }

        }



        protected void btnSearchBoth_Click(object sender, EventArgs e)
        {

            string s = TextBox2.Text;

            DateTime datetimeDT;
            if (DateTime.TryParse(s, out datetimeDT))
            {
                string date = s.ToString().Split(' ')[0];

                date = String.Format("{0:dd/MM/yyyy}", date);
                DateTime InputDate = Convert.ToDateTime(date);

                try
                {
                    SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FileDatabaseConnectionString2"].ConnectionString);

                    connection.Open();

                    SqlCommand dateCommand = new SqlCommand("SELECT DISTINCT(Timestamp) AS [DD/MM/YYYY] FROM LOGS WHERE (Username = @AccountUsername AND convert(date, Timestamp, 103) = convert(date, @Timestamp, 103))", connection);

                    dateCommand.Parameters.AddWithValue("@AccountUsername", bossUsername.Text);
                    dateCommand.Parameters.AddWithValue("@Timestamp", InputDate);
                    //dateCommand.Parameters.AddWithValue("@txtSearchValue", "%" + TextBox1.Text.Trim().ToLower() + "%");

                    var dbDate = (DateTime)dateCommand.ExecuteScalar();

                    if (dbDate != null)
                    {
                        AddDateToPlaceholder(dbDate);

                        SqlConnection connection2 = new SqlConnection(ConfigurationManager.ConnectionStrings["FileDatabaseConnectionString2"].ConnectionString);
                        connection2.Open();
                        SqlDataReader logReader = null;
                        SqlCommand logCommand = new SqlCommand("SELECT Action, Timestamp FROM Logs WHERE (Username = @AccountUsername AND convert(date, Timestamp, 103) = convert(date, @Timestamp, 103) AND lower(Action) LIKE @txtSearchValue) ORDER BY convert(date, Timestamp, 103) ASC", connection2);

                        logCommand.Parameters.AddWithValue("@AccountUsername", bossUsername.Text);
                        logCommand.Parameters.AddWithValue("@Timestamp", dbDate);

                        logCommand.Parameters.AddWithValue("@txtSearchValue", "%" + TextBox1.Text.Trim().ToLower() + "%");
                        logReader = logCommand.ExecuteReader();

                        while (logReader.Read())
                        {
                            string action = logReader["Action"].ToString();
                            string actionDate = logReader["Timestamp"].ToString();
                            //Response.Write("Date : " + actionDate + " Action : " + action + "<br>");
                            DateTime actionDateDT = Convert.ToDateTime(actionDate);

                            AddActionToPlaceholder(action, actionDateDT);

                        }

                    }


                }
                catch (System.NullReferenceException exc)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('There is no data found for this search.')", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please check that you have entered a correct format in DD/MM/YYYY.')", true);
            }




        }







    }
}