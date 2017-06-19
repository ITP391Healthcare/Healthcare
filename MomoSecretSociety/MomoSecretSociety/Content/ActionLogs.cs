using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MomoSecretSociety.Content
{
    public class ActionLogs
    {

        static SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FileDatabaseConnectionString2"].ConnectionString);

        public enum Action
        {
            Login,
            Logout,
            ReportSubmitted,
        };

        public static void Log(string username, Action action)
        {
            connection.Open();

            SqlCommand insertLogsCommand = new SqlCommand();
            insertLogsCommand.CommandText = "INSERT INTO Logs (username, action, timestamp) VALUES (@Username, @Action, @Timestamp)";
            insertLogsCommand.Parameters.AddWithValue("@Username", username);
            insertLogsCommand.Parameters.AddWithValue("@Action", getActionString(action));
            insertLogsCommand.Parameters.AddWithValue("@Timestamp", DateTime.Now);
            insertLogsCommand.Connection = connection;
            insertLogsCommand.ExecuteNonQuery();

            connection.Close();
        }

        public static string getActionString(Action action)
        {
            String actionString = "";
            
            if (action == Action.Login)
            {
                actionString = "Login";
            }
            else if (action == Action.Logout)
            {
                actionString = "Logout";
            }
            else if (action == Action.ReportSubmitted)
            {
                actionString = "Report was submitted";
            }

            return actionString;
        }


        public static string getLastLoggedInOf(string username)
        {
            String timestamp = "";

            connection.Open();
            SqlCommand getLastLoggedInCommand = new SqlCommand(
            "SELECT TOP 2 * FROM Logs WHERE username = @username AND Action = 'Login' ORDER BY Timestamp DESC;", connection);
            getLastLoggedInCommand.Parameters.AddWithValue("@username", username);

            SqlDataReader getLastLoggedInReader = getLastLoggedInCommand.ExecuteReader();

            getLastLoggedInReader.Read();
            if (getLastLoggedInReader.Read())
            {
                timestamp = getLastLoggedInReader["Timestamp"].ToString();
            }
            connection.Close();

            return timestamp;
        }

        public static DataTable showLogSummary(string username)
        {
            connection.Open();

            SqlCommand getLogSummaryCommand = new SqlCommand(
            "SELECT Timestamp, Action FROM Logs WHERE username = @username AND " +
            "Timestamp >= (SELECT MAX(Timestamp) a FROM Logs WHERE username = @username AND action = 'Login') ", connection);
            getLogSummaryCommand.Parameters.AddWithValue("@username", username);

            SqlDataReader summaryReader = getLogSummaryCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(summaryReader);

            connection.Close();

            return dt;
        }

    }
}