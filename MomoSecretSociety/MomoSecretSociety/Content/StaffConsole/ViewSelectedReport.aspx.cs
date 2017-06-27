﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MomoSecretSociety.Content.StaffConsole
{
    public partial class ViewSelectedReport : System.Web.UI.Page
    {
        static SqlConnection viewSelectedReportsDetailsConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["FileDatabaseConnectionString2"].ConnectionString);


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string abc = "";

                viewSelectedReportsDetailsConnection.Open();
                SqlCommand selectedDetailsCommand = new SqlCommand("SELECT * FROM Report WHERE CaseNumber = @CaseNumber", viewSelectedReportsDetailsConnection);
                selectedDetailsCommand.Parameters.AddWithValue("@CaseNumber", Session["caseNumberOfThisSelectedReport"].ToString());


                SqlDataReader selectedReportsDetailsReader = selectedDetailsCommand.ExecuteReader();

                while (selectedReportsDetailsReader.Read())
                {
                    //let say i retrieve username
                    CaseNumber.Text = selectedReportsDetailsReader["CaseNumber"].ToString();
                    Username.Text = "From: <b>" + selectedReportsDetailsReader["Date"].ToString() + "</b>";
                    Date.Text = "Date: <b>" + selectedReportsDetailsReader["Username"].ToString() + "</b>";
                    Subject.Text = "Subject: <b>" + selectedReportsDetailsReader["Subject"].ToString() + "</b>";
                    Description.Text = selectedReportsDetailsReader["Description"].ToString();
                    Remarks.Text = "Remarks: <b>" + selectedReportsDetailsReader["Remarks"].ToString() + "</b>";

                }
                viewSelectedReportsDetailsConnection.Close();

                Response.Write(abc);
            }
        }



    }
}