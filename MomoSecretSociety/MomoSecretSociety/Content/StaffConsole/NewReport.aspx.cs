using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MomoSecretSociety.Content.StaffConsole
{
    public partial class NewReport : System.Web.UI.Page
    {
        int cNumber;
        static SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FileDatabaseConnectionString1"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            //At page load, the name of the person who sign in will fill in the FROM input box automatically
            //Unable to edit
            TextBox3.Text = Session["AccountUsername"].ToString();
            TextBox3.ReadOnly = true;
        }
            int CaseNumber = 201700000;

        protected void SubmitButton_Click(object sender, EventArgs e)
        {   //Case Number Created +1 
            //Retrieve the latest case number and +1
            string dbCaseNumber="";
            connection.Open();
            SqlCommand myCommand = new SqlCommand("SELECT CaseNumber FROM Report", connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                dbCaseNumber = (myReader["CaseNumber"].ToString());
            }

            cNumber = int.Parse(dbCaseNumber);
            connection.Close();
            cNumber++;

            DateTime DateInput = new DateTime();
            DateInput = Convert.ToDateTime(TextBox4.Text);

            string NameInput = TextBox3.Text;
            string SubjectInput = TextBox2.Text;
            string CaseDesInput = TextBox1.Text;
            string status = "pending";

            //Add the details into database (done)
            //Report inserted into database, with ReportStatus = Pending (done)
            //Report details encrypted (not done)

            connection.Open();

            SqlCommand insertReportCommand = new SqlCommand();
            insertReportCommand.CommandText = "INSERT INTO Report (CaseNumber, Username, Date, Subject, Description, Remarks, ReportStatus)" + 
                " VALUES (@caseNumber, @username, @date, @subject, @description, @remarks, @status)";
            insertReportCommand.Parameters.AddWithValue("@caseNumber", cNumber);
            insertReportCommand.Parameters.AddWithValue("@username", NameInput);
            insertReportCommand.Parameters.AddWithValue("@date", DateInput);
            insertReportCommand.Parameters.AddWithValue("@subject", SubjectInput);
            insertReportCommand.Parameters.AddWithValue("@description", CaseDesInput);
            insertReportCommand.Parameters.AddWithValue("@Remarks", "");
            insertReportCommand.Parameters.AddWithValue("@status", status);

            insertReportCommand.Connection = connection;
            insertReportCommand.ExecuteNonQuery();
            connection.Close();

            Response.Redirect("~/Content/StaffConsole/SubmittedReports.aspx");
            //Show a line: Case #___ is created
            //string script = "alert('Case #');" + cNumber + "alert(' has been created.');";
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script, true);

            //Redirect to the submitted reports page
        }

        protected void SaveAsDraftsButton_Click(object sender, EventArgs e)
        {
            //string script = "alert('abc');";
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script, true);
        }

        //public static bool IsDate(string date)
        //{
        //    bool valid = true;
        //    //check in dd/mm/yyyy format
        //    valid = Regex.IsMatch(date, "^[0-9]{2}/[0-9]{2}/[0-9]{4}$");

        //    if (valid == true)
        //    {
        //        return true;
        //    }
        //    else
        //        return false;
        //}
    }

}