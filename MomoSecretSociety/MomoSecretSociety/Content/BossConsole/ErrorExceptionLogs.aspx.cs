using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
            DataTable dt = showErrorLogsSummary();

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        public static DataTable showErrorLogsSummary()
        {
            connection.Open();

            SqlCommand retrieveErrorsDetailsCommand = new SqlCommand("SELECT * FROM ErrorExceptionLogs ORDER BY convert(date,Timestamp) DESC", connection);


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
            SqlCommand dateCommand = new SqlCommand("SELECT * FROM ErrorExceptionLogs WHERE (lower(Username) LIKE @txtSearchValue OR lower(ExceptionType) LIKE @txtSearchValue OR lower(ErrorMessage) LIKE @txtSearchValue OR lower(ErrorSource) LIKE @txtSearchValue OR lower(Location) LIKE @txtSearchValue) ORDER BY convert(date,Timestamp) DESC", connection);

            dateCommand.Parameters.AddWithValue("@txtSearchValue", "%" + txtSearchValue.Text.Trim().ToLower() + "%");
            dataReader = dateCommand.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(dataReader);

            GridView1.DataSource = dt;
            GridView1.DataBind();

            if (dt.Rows.Count == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('There is no data found for this search.')", true);
            }

            connection.Close();

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

                    SqlCommand dateCommand = new SqlCommand("SELECT * FROM ErrorExceptionLogs WHERE convert(date, Timestamp, 103) = @Timestamp", connection);
                    //OR convert(time(0), Timestamp) = @Time)
                    //SELECT (convert(varchar(15), Timestamp, 108)) FROM ErrorExceptionLogs

                    dateCommand.Parameters.AddWithValue("@Timestamp", InputDate);
                    //dateCommand.Parameters.AddWithValue("@Time", time);

                    SqlDataReader dataReader = dateCommand.ExecuteReader();

                    DataTable dt2 = new DataTable();
                    dt2.Load(dataReader);

                    GridView1.DataSource = dt2;
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

        }

        //protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{

        //    //DataTable dt = showErrorLogsSummary();

        //    //GridView1.DataSource = dt;
        //    GridView1.DataBind();

        //}
    }
}