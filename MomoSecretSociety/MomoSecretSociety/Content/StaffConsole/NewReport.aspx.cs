﻿using System;
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
        static SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FileDatabaseConnectionString1"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            //At page load, the name of the person who sign in will fill in the FROM input box automatically
            //Unable to edit
            TextBox3.Text = Session["AccountUsername"].ToString();
            TextBox3.ReadOnly = true;
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {   //Case Number Created +1 
            int CaseNumber = 201700000;
            CaseNumber++;

            DateTime DateInput = new DateTime();
            DateInput = Convert.ToDateTime(TextBox4.Text);

            string NameInput = TextBox3.Text;
            string SubjectInput = TextBox2.Text;
            string CaseDesInput = TextBox1.Text;
            string status = "pending";

            //Add the details into database
            connection.Open();

            SqlCommand insertReportCommand = new SqlCommand();
            insertReportCommand.CommandText = "INSERT INTO Report (CaseNumber, Username, Date, Subject, Description, Remarks, Status)" + 
                " VALUES (@caseNumber, @username, @date, @subject, @description, @status)";
            insertReportCommand.Parameters.AddWithValue("@caseNumber", CaseNumber);
            insertReportCommand.Parameters.AddWithValue("@username", NameInput);
            insertReportCommand.Parameters.AddWithValue("@date", DateInput);
            insertReportCommand.Parameters.AddWithValue("@subject", SubjectInput);
            insertReportCommand.Parameters.AddWithValue("@description", CaseDesInput);
            insertReportCommand.Parameters.AddWithValue("@Remarks", null);
            insertReportCommand.Parameters.AddWithValue("@status", status);

            insertReportCommand.Connection = connection;
            insertReportCommand.ExecuteNonQuery();
            connection.Close();




            //Show a line: Case #___ is created
            //Report inserted into database, with ReportStatus = Pending
            //Report details encrypted
            //Check that fields are not empty
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