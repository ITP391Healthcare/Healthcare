using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MomoSecretSociety.Content.BossConsole
{
    public partial class ErrorExceptionLogs : System.Web.UI.Page
    {
        static SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FileDatabaseConnectionString2"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            //To make sure do not allow staff to access boss console through browser
            if (Context.User.Identity.Name != "KaiTatL97")
            {
                Response.Redirect("../../Account/Login.aspx");
                return;

                //ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Dear " + Session["AccountUsername"].ToString() + ", you are not allowed to access this page.'); window.location = '../../Account/Login.aspx'; ", true);

                //return;
            }


            DataTable dt = showErrorLogsSummary();

            GridView1.DataSource = dt;
            ViewState["Datable"] = dt;
            GridView1.DataBind();


            if (IsPostBack)
            {
                errormsgPasswordAuthenticate.Visible = false;
            }
        }

        public static DataTable showErrorLogsSummary()
        {
            connection.Open();

            SqlCommand retrieveErrorsDetailsCommand = new SqlCommand("SELECT * FROM ErrorExceptionLogs ORDER BY convert(datetime,Timestamp) DESC", connection);


            SqlDataReader summaryReader = retrieveErrorsDetailsCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(summaryReader);

            connection.Close();

            return dt;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FileDatabaseConnectionString2"].ConnectionString);

            connection.Open();
            SqlDataReader dataReader = null;
            SqlCommand dateCommand = new SqlCommand("SELECT * FROM ErrorExceptionLogs WHERE (lower(Username) LIKE @txtSearchValue OR lower(ExceptionType) LIKE @txtSearchValue OR lower(ErrorMessage) LIKE @txtSearchValue OR lower(ErrorSource) LIKE @txtSearchValue OR lower(Location) LIKE @txtSearchValue) ORDER BY convert(datetime,Timestamp) DESC", connection);

            dateCommand.Parameters.AddWithValue("@txtSearchValue", "%" + txtSearchValue.Text.Trim().ToLower() + "%");
            dataReader = dateCommand.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(dataReader);

            GridView1.DataSource = dt;
            ViewState["Datable"] = dt;
            GridView1.DataBind();

            if (dt.Rows.Count == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('There is no data found for this search.')", true);
            }

            connection.Close();


            searchValue = txtSearchValue.Text;
            url = System.Web.HttpContext.Current.Request.Url.ToString();

            //Add to logs
            ActionLogs.Action actionLog = ActionLogs.Action.SearchErrorLogs;
            ActionLogs.Log(Context.User.Identity.Name, actionLog);

        }

        public static string searchValue = "";
        public static string url = "";


        protected void btnSearchDate_Click(object sender, EventArgs e)
        {
            string s = txtSearchValueDate.Text;

            DateTime dt;
            if (DateTime.TryParse(s, out dt))
            {
                string date = s.ToString().Split(' ')[0];

                date = String.Format("{0:dd/MM/yyyy}", date);
                DateTime InputDate = Convert.ToDateTime(date);


                //String hour = s.ToString().Split(' ')[1].Split(':')[0];
                //Response.Write(hour + "\n");
                //String min = s.ToString().Split(' ')[1].Split(':')[1];
                //Response.Write(min + "\n");
                //String sec = s.ToString().Split(' ')[1].Split(':')[2];
                //Response.Write(sec + "\n");

                //string time = hour + ":" + min + ":" + sec;

                //if (txtSearchValueDate.Text.Contains(time))
                //{
                //    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please check that you have entered a correct format in DD/MM/YYYY!')", true);
                //}

                try
                {

                    SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FileDatabaseConnectionString2"].ConnectionString);

                    connection.Open();

                    SqlCommand dateCommand = new SqlCommand("SELECT * FROM ErrorExceptionLogs WHERE convert(date, Timestamp, 103) = convert(date,@Timestamp,103)", connection);
                    //OR convert(time(0), Timestamp) = @Time)
                    //SELECT (convert(varchar(15), Timestamp, 108)) FROM ErrorExceptionLogs

                    dateCommand.Parameters.AddWithValue("@Timestamp", InputDate);
                    //dateCommand.Parameters.AddWithValue("@Time", time);

                    SqlDataReader dataReader = dateCommand.ExecuteReader();

                    DataTable dt2 = new DataTable();
                    dt2.Load(dataReader);

                    GridView1.DataSource = dt2;
                    ViewState["Datable"] = dt;
                    GridView1.DataBind();

                    if (dt2.Rows.Count == 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('There is no data found for this search.')", true);
                    }
                    connection.Close();

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
            
            searchValue = txtSearchValueDate.Text;
            url = System.Web.HttpContext.Current.Request.Url.ToString();

            //Add to logs
            ActionLogs.Action actionLog = ActionLogs.Action.SearchErrorLogs;
            ActionLogs.Log(Context.User.Identity.Name, actionLog);
            
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


                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FileDatabaseConnectionString2"].ConnectionString);

                connection.Open();
                SqlDataReader dataReader = null;
                SqlCommand dateCommand = new SqlCommand("SELECT * FROM ErrorExceptionLogs WHERE ((lower(Username) LIKE @txtSearchValue OR lower(ExceptionType) LIKE @txtSearchValue OR lower(ErrorMessage) LIKE @txtSearchValue OR lower(ErrorSource) LIKE @txtSearchValue OR lower(Location) LIKE @txtSearchValue) AND convert(date, Timestamp, 103) = convert(date,@Timestamp,103)) ORDER BY convert(date,Timestamp) DESC", connection);

                dateCommand.Parameters.AddWithValue("@txtSearchValue", "%" + TextBox1.Text.Trim().ToLower() + "%");
                dateCommand.Parameters.AddWithValue("@Timestamp", InputDate);

                dataReader = dateCommand.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(dataReader);

                GridView1.DataSource = dt;
                ViewState["Datable"] = dt;
                GridView1.DataBind();

                if (dt.Rows.Count == 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('There is no data found for this search.')", true);
                }

                connection.Close();


            }

            searchValue = TextBox1.Text + " " + TextBox2.Text;
            url = System.Web.HttpContext.Current.Request.Url.ToString();

            //Add to logs
            ActionLogs.Action actionLog = ActionLogs.Action.SearchErrorLogs;
            ActionLogs.Log(Context.User.Identity.Name, actionLog);
            
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

                        //Add to logs
                        ActionLogs.Action action = ActionLogs.Action.ReauthenticatedDueToAccountLockout;
                        ActionLogs.Log(Context.User.Identity.Name, action);

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

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(ViewState["Datable"]);
            DataTable dataTable = ViewState["Datable"] as DataTable;
            if (dataTable != null)
            {
                string SortDirection = "DESC";
                if (ViewState["SortExpression"] != null)
                {
                    if (ViewState["SortExpression"].ToString() == e.SortExpression)
                    {
                        ViewState["SortExpression"] = null;
                        SortDirection = "ASC";
                    }
                    else
                    {
                        ViewState["SortExpression"] = e.SortExpression;
                    }
                }
                else
                {
                    ViewState["SortExpression"] = e.SortExpression;
                }

                DataView dataView = new DataView(dataTable);
                dataView.Sort = e.SortExpression + " " + SortDirection;
                System.Diagnostics.Debug.WriteLine(e.SortDirection);
                GridView1.DataSource = dataView;
                GridView1.DataBind();

            }
        }

        //private string ConvertSortDirectionToSql(SortDirection sortDirection)
        //{
        //    string newSortDirection = String.Empty;

        //    switch (sortDirection)
        //    {
        //        case SortDirection.Ascending:
        //            newSortDirection = "ASC";
        //            break;

        //        case SortDirection.Descending:
        //            newSortDirection = "DESC";
        //            break;
        //    }

        //    return newSortDirection;
        //}
    }
}