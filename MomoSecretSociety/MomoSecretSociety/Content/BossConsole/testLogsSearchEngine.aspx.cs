using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace MomoSecretSociety.Content.BossConsole
{
    public partial class testLogsSearchEngine : System.Web.UI.Page
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FileDatabaseConnectionString2"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            List<double> usernameArray = null;
                        List<double> actionArray = null;

            staffUsername.Text = Context.User.Identity.Name;

            //double total = 0;

            //connection.Open();
            //SqlCommand myCommand = new SqlCommand("SELECT Username, COUNT(Action) AS NumberOfActivity FROM Logs GROUP BY Username", connection);

            //SqlDataReader myReader = myCommand.ExecuteReader();
            //while (myReader.Read())
            //{
            //    actionArray.Add(Convert.ToDouble(myReader["NumberOfActivity"]));
            //    //actionArray.Add(myReader["NumberOfActivity"].ToString());
                
            //}
            //connection.Close();


            Series S = Chart1.Series["Series1"];
            S.ChartType = SeriesChartType.Pie;
            S.IsValueShownAsLabel = true;
            S["PieLabelStyle"] = "Outside";

            //DataPoint p = new DataPoint(usernameArray, actionArray);
            //// check your data type for the calculation!
            //p.Label = p.YValues[0] + "h =\n" +
            //         (100d * p.YValues[0] / total).ToString("00.00") + "%\n"; // my format

            //for (int i = 0; i <= index - 1; i++)
            //{
            //    S.Points.AddXY(project[i], projTime[i]);
            //}

            // calculate the total:
            double total = S.Points.Sum(dp => dp.YValues[0]);

            // now we can set the percentages
            foreach (DataPoint p in S.Points)
            {
                p.Label = p.YValues[0] + "h =\n" +
                          (100d * p.YValues[0] / total).ToString("00.00") + "%\n"; // my format
            }

            Chart1.Titles.Add(S.Points.Count + " projects, total " +
                              total.ToString("###,##0") + " hours");
            Chart1.Titles[0].Font = new System.Drawing.Font("Arial", 14f);

        }

        //protected void ShieldChart1_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e)
        //{
        //    ShieldChart1.DataSource = new object[]
        //    {
        //        new {Quarter = "Q1", Sales = 312 },
        //        new {Quarter = "Q2", Sales = 212 },
        //        new {Quarter = "Q3", Sales = 322 },
        //        new {Quarter = "Q4", Sales = 128 }
        //    };
        //}

        protected void btnSearchDate_Click(object sender, EventArgs e)
        {
            string s = txtSearchValueDate.Text;

            DateTime dt;
            if (DateTime.TryParse(s, out dt))
            {
                string date = s.ToString().Split(' ')[0];

                date = String.Format("{0:dd/MM/yyyy}", date);

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Date: " + date + "')", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please check that you have entered a correct format in DD/MM/YYYY.')", true);
            }



            ////YEAR
            //if (DateTime.TryParse((String.Format("{0:YYYY}", s.ToString())), out dt))
            //{
            //    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('YEAR-IF: " + dt + "')", true);

            //}
            //else
            //{
            //    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('YEAR-Else: " + dt + "')", true);

            //}

            ////MONTH
            //if (DateTime.TryParse((String.Format("{0:MMM MMMM}", s.ToString())), out dt))
            //{
            //    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('MONTH-IF: " + dt + "')", true);

            //}
            //else
            //{
            //    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('MONTH-Else: " + dt + "')", true);

            //}



        }
    }
}