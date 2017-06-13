using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MomoSecretSociety.Content.StaffConsole
{
    public partial class SubmittedReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Content/StaffConsole/ViewAcceptedReport");
        }

        //protected void Button1_Click(object sender, System.EventArgs e)
        //{
        //    // Total number of rows.
        //    int rowCnt;
        //    // Current row count.
        //    int rowCtr;
        //    // Total number of cells per row (columns).
        //    int cellCtr;
        //    // Current cell counter
        //    int cellCnt;

        //    rowCnt = int.Parse(TextBox1.Text);
        //    cellCnt = int.Parse(TextBox2.Text);

        //    for (rowCtr = 1; rowCtr <= rowCnt; rowCtr++)
        //    {
        //        // Create new row and add it to the table.
        //        TableRow tRow = new TableRow();
        //        Table1.Rows.Add(tRow);
        //        for (cellCtr = 1; cellCtr <= cellCnt; cellCtr++)
        //        {
        //            // Create a new cell and add it to the row.
        //            TableCell tCell = new TableCell();
        //            tCell.Text = "Row " + rowCtr + ", Cell " + cellCtr;
        //            tRow.Cells.Add(tCell);
        //        }
        //    }
        //}

    }
}