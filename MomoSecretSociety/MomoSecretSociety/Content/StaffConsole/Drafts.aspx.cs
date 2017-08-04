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

namespace MomoSecretSociety.Content.StaffConsole
{
    public partial class Drafts : System.Web.UI.Page
    {

        static SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FileDatabaseConnectionString2"].ConnectionString);


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                ((Label)Master.FindControl("lastLoginStaff")).Text = "Your last logged in was <b>"
                            + ActionLogs.getLastLoggedInOf(Context.User.Identity.Name) + "</b>";
            }

            if (IsPostBack)
            {
                errormsgPasswordAuthenticate.Visible = false;
            }

            if (!IsPostBack)
            {
                connection.Open();

                SqlCommand retrieveSubmittedReportsCommand = new SqlCommand("SELECT CaseNumber, Date, Subject, ReportStatus, CreatedDateTime FROM Report " +
                    "WHERE Username = @Username AND ReportStatus = 'drafts' ", connection);

                retrieveSubmittedReportsCommand.Parameters.AddWithValue("@Username", Context.User.Identity.Name);

                SqlDataReader retrieveSubmittedReports = retrieveSubmittedReportsCommand.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(retrieveSubmittedReports);

                connection.Close();


                GridView1.DataSource = dt;
                ViewState["Datable"] = dt;
                GridView1.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FileDatabaseConnectionString2"].ConnectionString);

            connection.Open();
            SqlDataReader dataReader = null;
            SqlCommand dateCommand = new SqlCommand("SELECT * FROM Report WHERE (lower(Username) LIKE @txtSearchValue OR lower(Subject) LIKE @txtSearchValue OR lower(CaseNumber) LIKE @txtSearchValue OR lower(ReportStatus) LIKE @txtSearchValue OR lower(Subject) LIKE @txtSearchValue) AND Username = @AccountUsername AND ReportStatus='drafts'", connection);

            dateCommand.Parameters.AddWithValue("@txtSearchValue", "%" + txtSearchValue.Text.Trim().ToLower() + "%");
            dateCommand.Parameters.AddWithValue("@AccountUsername", Context.User.Identity.Name);

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

        //Joanne test button - View Accepted reports
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~\\Content\\StaffConsole\\ViewAcceptedReport");

        }


        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "DataCommand")
            {
                string commandArgs = e.CommandArgument.ToString();


                Session["caseNumberOfThisSelectedReport"] = commandArgs;

                Response.Redirect("ViewAcceptedReport.aspx");


            }
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

        private string ConvertSortDirectionToSql(SortDirection sortDirection)
        {
            string newSortDirection = String.Empty;

            switch (sortDirection)
            {
                case SortDirection.Ascending:
                    newSortDirection = "ASC";
                    break;

                case SortDirection.Descending:
                    newSortDirection = "DESC";
                    break;
            }

            return newSortDirection;
        }

    }
}