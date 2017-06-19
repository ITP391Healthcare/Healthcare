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

            //This should be on click of the particular report then will appear
            string dbCaseNumber = "";
            string dbUsername = "";
            string dbDate = "";
            string dbSubject = "";
            string dbDescription = "";
            string dbRemarks = "";
            string dbReportStatus = "";

            connection.Open();
            SqlCommand myCommand = new SqlCommand("SELECT CaseNumber, Username, Date, Subject, Description, Remarks, ReportStatus FROM Report WHERE CaseNumber = @caseNo", connection);
            myCommand.Parameters.AddWithValue("@caseNo", 201700001); //Hardcoded the case number - next time change to auto input when onclick of the particular report
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
            string inputUsername = Session["AccountUsername"].ToString();
            string rStatus = "accepted";

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FileDatabaseConnectionString2"].ConnectionString);
            connection.Open();
            SqlCommand myCommand = new SqlCommand("SELECT CaseNumber, Username, Date, Subject, Description, Remarks, CreatedDateTime FROM Report WHERE Username = @AccountUsername AND ReportStatus = @reportStatus" , connection);
            myCommand.Parameters.AddWithValue("@AccountUsername", inputUsername); //Taking the latest report of that user only. //Should be click on a particular report number - thats the report that we should take
            myCommand.Parameters.AddWithValue("@reportStatus", rStatus);

            SqlDataReader myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                dbCaseNumber = (myReader["CaseNumber"].ToString());
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
            DrawPage(page);

            string wmText = "Report #" + dbCaseNumber + " by " + dbUsername;

            // + Watermark (text) -> DrawString(string s, PdfFontBase font, PdfBrush brush, float x, float y, PdfStringFormat format)
            PdfTilingBrush brush = new PdfTilingBrush(new SizeF(page.Canvas.ClientSize.Width / 2, page.Canvas.ClientSize.Height / 3));
            brush.Graphics.SetTransparency(0.3f);
            brush.Graphics.Save();
            brush.Graphics.TranslateTransform(brush.Size.Width / 2, brush.Size.Height / 2);
            brush.Graphics.RotateTransform(-45);
            brush.Graphics.DrawString(wmText, new PdfFont(PdfFontFamily.Helvetica, 24), PdfBrushes.Black, 0, 0, new PdfStringFormat(PdfTextAlignment.Center));
            brush.Graphics.Restore();
            brush.Graphics.SetTransparency(1);
            page.Canvas.DrawRectangle(brush, new RectangleF(new PointF(1, 1), page.Canvas.ClientSize));

            //Save pdf to a location
            doc.SaveToFile("C:\\Users\\User\\Desktop\\CreatePDFTest" + dbCaseNumber + ".pdf");


        }

        private static void DrawPage(PdfPageBase page)
        {
            float pageWidth = page.Canvas.ClientSize.Width;
            float y = 0;

            //.DrawString(string s, PdfFontBase font, PdfBrush brush, float x, float y, PdfStringFormat format);
            //.DrawLine(PdfPen pen, float x1, float y1, float x2, float y2);

            //page header
            PdfPen pen1 = new PdfPen(Color.LightGray, 1f);
            PdfBrush brush1 = new PdfSolidBrush(Color.LightGray);
            PdfTrueTypeFont font1 = new PdfTrueTypeFont(new Font("Arial", 8f, FontStyle.Italic));
            PdfStringFormat format1 = new PdfStringFormat(PdfTextAlignment.Right);
            String text = "Report Case Number #" + dbCaseNumber +".pdf";
            page.Canvas.DrawString(text, font1, brush1, pageWidth, y, format1);
            SizeF size = font1.MeasureString(text, format1);
            y = y + size.Height + 1;
            page.Canvas.DrawLine(pen1, 0, y, pageWidth, y);

            //page footer
            /*
            PdfPen footerpen = new PdfPen(Color.LightGray, 1f);
            PdfBrush footerbrush = new PdfSolidBrush(Color.LightGray);
            PdfTrueTypeFont footerfont = new PdfTrueTypeFont(new Font("Arial", 8f, FontStyle.Italic));
            PdfStringFormat footerformat = new PdfStringFormat(PdfTextAlignment.Right);
            String footertext = "Created on: " + dbCreatedDateTime;
            page.Canvas.DrawString(footertext, footerfont, footerbrush, pageWidth, y, footerformat);
            SizeF footersize = font1.MeasureString(footertext, footerformat);
            y = y + footersize.Height + 1;
            page.Canvas.DrawLine(footerpen, 0, y, pageWidth, y);
            */

            //title
            y = y + 5;
            PdfBrush brush2 = new PdfSolidBrush(Color.Black);
            PdfTrueTypeFont font2 = new PdfTrueTypeFont(new Font("Arial", 16f, FontStyle.Bold));
            PdfStringFormat format2 = new PdfStringFormat(PdfTextAlignment.Center);
            //format2.CharacterSpacing = 1f;
            text = "Report Case #" + dbCaseNumber;
            page.Canvas.DrawString(text, font2, brush2, pageWidth / 2, y, format2);
            size = font2.MeasureString(text, format2);
            y = y + size.Height + 6;

            //icon
            PdfImage image = PdfImage.FromFile(@"C:\\Users\\User\\Desktop\\vntest.jpg");
            float width = image.Width * 0.1f;
            float height = image.Height * 0.1f;
            float x = (page.Canvas.ClientSize.Width - width) / 2;

            page.Canvas.DrawImage(image, x, 60, width, height);
            //page.Canvas.DrawImage(image, new PointF(pageWidth - image.PhysicalDimension.Width, y));
            float imageLeftSpace = pageWidth - image.PhysicalDimension.Width - 2;
            float imageBottom = image.PhysicalDimension.Height + y;


            //reference content
            PdfTrueTypeFont font3 = new PdfTrueTypeFont(new Font("Arial", 9f));
            PdfStringFormat format3 = new PdfStringFormat();
            format3.ParagraphIndent = font3.Size * 2;
            format3.MeasureTrailingSpaces = true;
            format3.LineSpacing = font3.Size * 1.5f;
            String text1 = "(All contents are strictly confidential, do not distribute)";
            page.Canvas.DrawString(text1, font3, brush2, 0, y, format3);
            //-
            size = font3.MeasureString(text1, format3);
            float x1 = size.Width;
            format3.ParagraphIndent = 0;
            PdfTrueTypeFont font4 = new PdfTrueTypeFont(new Font("Arial", 9f, FontStyle.Underline));
            PdfBrush brush3 = PdfBrushes.Blue;
            x1 = x1 + size.Width;

            //page.Canvas.DrawString(text1, font3, brush2, x1, y, format3);
            y = y + size.Height;

            //content
            PdfStringFormat format4 = new PdfStringFormat();
            text = "Date: " + dbDate + "\n" + "From: " + dbUsername + "\n" + "Subject: " + dbSubject + "\n" + "Case Description: " + dbDescription + "\n" + "Remarks: " + "\n" + dbRemarks; //testing
            PdfTrueTypeFont font5 = new PdfTrueTypeFont(new Font("Arial", 10f));
            format4.LineSpacing = font5.Size * 1.5f;
            PdfStringLayouter textLayouter = new PdfStringLayouter();
            float imageLeftBlockHeight = imageBottom - y;
            PdfStringLayoutResult result = textLayouter.Layout(text, font5, format4, new SizeF(imageLeftSpace, imageLeftBlockHeight));
            if (result.ActualSize.Height < imageBottom - y)
            {
                imageLeftBlockHeight = imageLeftBlockHeight + result.LineHeight;
                result = textLayouter.Layout(text, font5, format4, new SizeF(imageLeftSpace, imageLeftBlockHeight));
            }
            foreach (LineInfo line in result.Lines)
            {
                page.Canvas.DrawString(line.Text, font5, brush2, 0, y, format4);
                y = y + result.LineHeight;
            }

            //PdfTextWidget textWidget = new PdfTextWidget(result.Remainder, font5, brush2);
            PdfTextWidget textWidget = new PdfTextWidget("", font5, brush2);

            PdfTextLayout textLayout = new PdfTextLayout();
            textLayout.Break = PdfLayoutBreakType.FitPage;
            textLayout.Layout = PdfLayoutType.Paginate;
            RectangleF bounds = new RectangleF(new PointF(0, y), page.Canvas.ClientSize);
            textWidget.StringFormat = format4;
            textWidget.Draw(page, bounds, textLayout);
        }
    }
}