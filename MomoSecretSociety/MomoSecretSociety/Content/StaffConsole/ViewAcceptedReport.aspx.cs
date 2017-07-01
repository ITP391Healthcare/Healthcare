using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Spire.Pdf;
using Spire.Pdf.Graphics;
using System.Drawing;
using Spire.Pdf.Security;
using Spire.Pdf.Exporting.XPS.Schema;
using Spire.Pdf.Widget;
using System.Security.Cryptography.X509Certificates;
using Spire.Pdf.Fields;

namespace MomoSecretSociety.Content.StaffConsole
{
    public partial class TestDisplay : System.Web.UI.Page
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

            string dbCaseNumber = "";
            string dbUsername = "";
            string dbDate = "";
            string dbSubject = "";
            string dbDescription = "";
            string dbRemarks = "";
            string dbReportStatus = "";

            connection.Open();
            SqlCommand myCommand = new SqlCommand("SELECT CaseNumber, Username, Date, Subject, Description, Remarks, ReportStatus FROM Report WHERE CaseNumber = @caseNo", connection);
            myCommand.Parameters.AddWithValue("@caseNo", Session["caseNumberOfThisSelectedReport"].ToString()); 
            SqlDataReader myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                dbCaseNumber = (myReader["CaseNumber"].ToString());
                dbUsername = (myReader["Username"].ToString());
                dbDate = (myReader["Date"].ToString());
                dbSubject = (myReader["Subject"].ToString());
                dbDescription = (myReader["Description"].ToString());
                dbRemarks = (myReader["Remarks"].ToString());
                dbReportStatus = (myReader["ReportStatus"].ToString());

            }

            connection.Close();

            Label2.Text = dbCaseNumber + " -";
            Label4.Text = dbDate;
            Label6.Text = dbUsername;
            Label8.Text = dbSubject;
            Label10.Text = dbDescription;
            Label12.Text = dbRemarks;
            
            //means if pending/drafts/rejected cannot save pdf until the report has been approved
            if (dbReportStatus != "accepted")
            {
                btnSaveAsPDF.Enabled = false;
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


        public static string dbCaseNumber = "";
        public static string dbUsername = "";
        public static string dbDate = "";
        public static string dbSubject = "";
        public static string dbDescription = "";
        public static string dbRemarks = "";
        public static string dbCreatedDateTime = "";

        protected void btnSaveAsPDF_Click(object sender, EventArgs e)
        {
            string inputUsername = Context.User.Identity.Name;
            //string inputUsername = Session["AccountUsername"].ToString();
            string rStatus = "accepted";
            dbCaseNumber = Session["caseNumberOfThisSelectedReport"].ToString();

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FileDatabaseConnectionString2"].ConnectionString);
            connection.Open();
            SqlCommand myCommand = new SqlCommand("SELECT CaseNumber, Username, Date, Subject, Description, Remarks, CreatedDateTime FROM Report WHERE Username = @AccountUsername AND ReportStatus = @reportStatus AND CaseNumber = @cNum" , connection);
            myCommand.Parameters.AddWithValue("@AccountUsername", inputUsername); //Taking the latest report of that user only. //Should be click on a particular report number - thats the report that we should take
            myCommand.Parameters.AddWithValue("@reportStatus", rStatus);
            myCommand.Parameters.AddWithValue("@cNum", dbCaseNumber);


            SqlDataReader myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                //dbCaseNumber = (myReader["CaseNumber"].ToString());
                dbUsername = (myReader["Username"].ToString());
                dbDate = (myReader["Date"].ToString());
                dbSubject = (myReader["Subject"].ToString());
                dbDescription = (myReader["Description"].ToString());
                dbRemarks = (myReader["Remarks"].ToString());
                dbCreatedDateTime = (myReader["CreatedDateTime"].ToString());

            }
            connection.Close();

            //Creating a pdf document
            PdfDocument doc = new PdfDocument();

            //Create a page
            PdfPageBase page = doc.Pages.Add();

            //Draw the contents of page
            AlignText(page);

            // + Encryption (Joanne) 
            doc.Security.KeySize = PdfEncryptionKeySize.Key128Bit;
            doc.Security.OwnerPassword = "e-iceblue";
            doc.Security.UserPassword = "test";
            doc.Security.Permissions = PdfPermissionsFlags.Print | PdfPermissionsFlags.FillFields;

            //// + DigitalSignature Method 1 (KaiTat)
            //String pfxPath = @"C:\\Program Files (x86)\\e-iceblue\\Spire.pdf\\Demos\\Data\\Demo.pfx";
            //PdfCertificate digi = new PdfCertificate(pfxPath, "e-iceblue");
            //PdfSignature signature = new PdfSignature(doc, page, digi, "demo");
            //signature.ContactInfo = "Harry Hu";
            //signature.Certificated = true;
            //signature.DocumentPermissions = PdfCertificationFlags.AllowFormFill;

            //KT Digital Signature Method 2
            PdfSignatureField signaturefield = new PdfSignatureField(page, "Signature");
            signaturefield.BorderWidth = 1.0f;
            signaturefield.BorderStyle = PdfBorderStyle.Solid;
            signaturefield.BorderColor = new PdfRGBColor(System.Drawing.Color.Black);
            signaturefield.HighlightMode = PdfHighlightMode.Outline;
            signaturefield.Bounds = new RectangleF(320, 40, 200, 150);
            

            doc.Form.Fields.Add(signaturefield);



            // + Watermark - Text (Joanne)
            string wmText = "Report #" + dbCaseNumber + " by " + dbUsername;

            PdfTilingBrush brush = new PdfTilingBrush(new SizeF(page.Canvas.ClientSize.Width / 2, page.Canvas.ClientSize.Height / 3));
            brush.Graphics.SetTransparency(0.3f);
            brush.Graphics.Save();
            brush.Graphics.TranslateTransform(brush.Size.Width / 2, brush.Size.Height / 2);
            brush.Graphics.RotateTransform(-45);
            brush.Graphics.DrawString(wmText, new PdfFont(PdfFontFamily.Helvetica, 20), PdfBrushes.Black, 0, 0, new PdfStringFormat(PdfTextAlignment.Center));
            brush.Graphics.Restore();
            brush.Graphics.SetTransparency(1);
            page.Canvas.DrawRectangle(brush, new RectangleF(new PointF(1, 1), page.Canvas.ClientSize));

            

            //Save pdf to a location
            doc.SaveToFile("C:\\Users\\User\\Desktop\\CreatePDFTest" + dbCaseNumber + ".pdf");
            //Kt testing
            //doc.SaveToFile("C:\\Users\\Kai Tat\\Desktop\\CreatePDFTest" + dbCaseNumber + ".pdf");


            //Launching the PDF File
            System.Diagnostics.Process.Start("C:\\Users\\User\\Desktop\\CreatePDFTest" + dbCaseNumber + ".pdf");
            //Kt testing
            //System.Diagnostics.Process.Start("C:\\Users\\Kai Tat\\Desktop\\CreatePDFTest" + dbCaseNumber + ".pdf");

        }

        private static void AlignText(PdfPageBase page)
        {
            float x1 = 20;
            float y1 = 50;
            float x2 = 90;
            string text = "";
            float pageWidth = page.Canvas.ClientSize.Width;

            //Title
            PdfBrush brush1 = new PdfSolidBrush(Color.Black);
            PdfTrueTypeFont font1 = new PdfTrueTypeFont(new Font("Arial", 16f, FontStyle.Bold));
            PdfStringFormat format1 = new PdfStringFormat(PdfTextAlignment.Center);
            //format2.CharacterSpacing = 1f;
            text = "Report Case #" + dbCaseNumber;
            page.Canvas.DrawString(text, font1, brush1, pageWidth / 2, 10, format1);
            
            //Draw the text - alignment
            PdfFont font2 = new PdfFont(PdfFontFamily.Helvetica, 10f);
            PdfTrueTypeFont font3 = new PdfTrueTypeFont(new Font("Helvetica", 10f, FontStyle.Bold));
            PdfSolidBrush brush = new PdfSolidBrush(Color.Black);
            PdfStringFormat leftAlignment = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);

            //DATE
            page.Canvas.DrawString("Date: ", font2, brush, x1, y1, leftAlignment);
            page.Canvas.DrawString(dbDate, font3, brush, x2, y1, leftAlignment);
            y1 = y1 + 30;

            //FROM
            page.Canvas.DrawString("From: ", font2, brush, x1, y1, leftAlignment);
            page.Canvas.DrawString(dbUsername, font3, brush, x2, y1, leftAlignment);
            y1 = y1 + 30;

            //SUBJECT
            page.Canvas.DrawString("Subject: ", font2, brush, x1, y1, leftAlignment);
            page.Canvas.DrawString(dbSubject, font3, brush, x2, y1, leftAlignment);
            y1 = y1 + 30;

            //CASE DESCRIPTION
            page.Canvas.DrawString("Case Description: ", font2, brush, x1, y1, leftAlignment);
            y1 = y1 + 30;

            string[] delimiter = new string[] { " " };
            string[] result;

            string finalResult = "";
            int counter = 0;

            result = dbDescription.Split(delimiter, StringSplitOptions.None);

            foreach (string s in result)
            {
                int charCount = s.Length;
                counter += charCount;

                if (counter < 70)
                {
                    finalResult += s + " ";
                    //add the word to the final result with a space
                }
                else if (counter >= 70)
                {
                    counter = 0;
                    counter += charCount;
                    finalResult += "\n" + s + " ";
                    //y1 += 10;
                }
            }

            page.Canvas.DrawString(finalResult, font3, brush, x2, y1, leftAlignment);
            y1 = y1 + 30;

            //page.Canvas.DrawString(dbDescription, font3, brush, x2, y1, leftAlignment); //Characters count: 76
            //y1 = y1 + 30;

            //REMARKS
            page.Canvas.DrawString("Remarks: ", font2, brush, x1, y1, leftAlignment);
            y1 = y1 + 30;
            page.Canvas.DrawString(dbRemarks, font3, brush, x2, y1, leftAlignment);


            //WIDTH 515 HEIGHT 762
            //To print out the size of the whole page
            /*
            y1 = y1 + 30;
            SizeF size = page.Canvas.ClientSize;
            string sizeText = size.ToString();
            page.Canvas.DrawString(sizeText, font3, brush, x2, y1, leftAlignment);
            */

                string w = page.Canvas.Size.ToString();
                y1 = y1 + 30;
                page.Canvas.DrawString(w, font3, brush, x2, y1, leftAlignment);
                y1 = y1 + 30;
                SizeF size = page.Canvas.ClientSize;
                string sizeText = size.ToString();
                page.Canvas.DrawString(sizeText, font3, brush, x2, y1, leftAlignment);
  
        }
    }
}