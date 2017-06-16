using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//Joanne
namespace MomoSecretSociety.Content.StaffConsole
{
    public partial class NewReport : System.Web.UI.Page
    {
        int cNumber;
        static SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FileDatabaseConnectionString2"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                ((Label)Master.FindControl("lastLoginStaff")).Text = "Your last logged in was <b>"
                            + ActionLogs.getLastLoggedInOf(Context.User.Identity.Name) + "</b>";
            }

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

            //Converting input date into datetime type input
            DateTime DateInput = new DateTime();
            DateInput = Convert.ToDateTime(TextBox4.Text);

            //Getting the date time when submit drafts/save reports as drafts
            DateTime createdDateTime = new DateTime();
            createdDateTime = DateTime.Now;

            string NameInput = TextBox3.Text;
            string SubjectInput = TextBox2.Text;
            string CaseDesInput = TextBox1.Text;
            string status = "pending";
            

            //Add the details into database (done)
            //Report inserted into database, with ReportStatus = Pending (done)
            //Report details encrypted (not done)

            connection.Open();

            SqlCommand insertReportCommand = new SqlCommand();
            insertReportCommand.CommandText = "INSERT INTO Report (CaseNumber, Username, Date, Subject, Description, Remarks, ReportStatus, CreatedDateTime)" + 
                " VALUES (@caseNumber, @username, @date, @subject, @description, @remarks, @status, @createdDT)";
            insertReportCommand.Parameters.AddWithValue("@caseNumber", cNumber);
            insertReportCommand.Parameters.AddWithValue("@username", NameInput);
            insertReportCommand.Parameters.AddWithValue("@date", DateInput);
            insertReportCommand.Parameters.AddWithValue("@subject", SubjectInput);
            insertReportCommand.Parameters.AddWithValue("@description", CaseDesInput);
            insertReportCommand.Parameters.AddWithValue("@Remarks", "");
            insertReportCommand.Parameters.AddWithValue("@status", status);
            insertReportCommand.Parameters.AddWithValue("@createdDT", createdDateTime);


            insertReportCommand.Connection = connection;
            insertReportCommand.ExecuteNonQuery();
            connection.Close();

            //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert",
            //"alert('Case #'" + cNumber + "' has been created.');" + "window.location = 'SubmittedReports.aspx'; ", true);

            string message = "Case #" + cNumber + " has been created.";
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + message + "'); window.location = 'SubmittedReports.aspx'; ", true);
        }

        protected void SaveAsDraftsButton_Click(object sender, EventArgs e)
        {
            //COPY PASTED FROM SUBMIT BUTTON
            //Flow: create a case record in the reports db

            //Case Number Created +1 
            //Retrieve the latest case number and +1
            string dbCaseNumber = "";
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

            //Converting input date into datetime type input
            DateTime DateInput = new DateTime();
            DateInput = Convert.ToDateTime(TextBox4.Text);

            //Getting the date time when submit drafts/save reports as drafts
            DateTime createdDateTime = new DateTime();
            createdDateTime = DateTime.Now;

            string NameInput = TextBox3.Text;
            string SubjectInput = TextBox2.Text;
            string CaseDesInput = TextBox1.Text;
            string status = "drafts";

            //Add the details into database (done)
            //Report inserted into database, with ReportStatus = Pending (done)
            //Report details encrypted (not done)

            connection.Open();

            SqlCommand insertReportCommand = new SqlCommand();
            insertReportCommand.CommandText = "INSERT INTO Report (CaseNumber, Username, Date, Subject, Description, Remarks, ReportStatus, CreatedDateTime)" +
                " VALUES (@caseNumber, @username, @date, @subject, @description, @remarks, @status, @createdDT)";
            insertReportCommand.Parameters.AddWithValue("@caseNumber", cNumber);
            insertReportCommand.Parameters.AddWithValue("@username", NameInput);
            insertReportCommand.Parameters.AddWithValue("@date", DateInput);
            insertReportCommand.Parameters.AddWithValue("@subject", SubjectInput);
            insertReportCommand.Parameters.AddWithValue("@description", CaseDesInput);
            insertReportCommand.Parameters.AddWithValue("@Remarks", "");
            insertReportCommand.Parameters.AddWithValue("@status", status);
            insertReportCommand.Parameters.AddWithValue("@createdDT", createdDateTime);


            insertReportCommand.Connection = connection;
            insertReportCommand.ExecuteNonQuery();
            connection.Close();

            //alert
            string message = "Your report has been saved in drafts!";
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + message + "'); window.location = 'SubmittedReports.aspx'; ", true);
        }

    }

}