using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MomoSecretSociety.Content.BossConsole
{
    public partial class testLogsSearchEngine : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            staffUsername.Text = Context.User.Identity.Name;
        }


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