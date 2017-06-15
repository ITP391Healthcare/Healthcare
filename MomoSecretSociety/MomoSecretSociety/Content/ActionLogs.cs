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
            Register,
            Login,
            Logout,
            ChangeProfilePicture,
            ChangePassword,
            ResetPassword,
            ChangeEmail,
            ChangeContactNo,
            ChallengeCompleted,
            WriteUpSubmitted,
            FeedbackSubmitted,
            Disable2FA,
            Enable2FA,
            DisableAccount,
            EnableAccount,
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

            if (action == Action.Register)
            {
                actionString = "Register";
            }
            else if (action == Action.Login)
            {
                actionString = "Login";
            }
            else if (action == Action.Logout)
            {
                actionString = "Logout";
            }
            else if (action == Action.ChangeProfilePicture)
            {
                actionString = "Profile Picture was changed";
            }
            else if (action == Action.ChangePassword)
            {
                actionString = "Password was changed";
            }
            else if (action == Action.ResetPassword)
            {
                actionString = "Password was reset";
            }
            else if (action == Action.ChangeEmail)
            {
                actionString = "Email was changed";
            }
            else if (action == Action.ChangeContactNo)
            {
                actionString = "Contact No was changed";
            }
            else if (action == Action.ChallengeCompleted)
            {
                actionString = "Challenge was completed";
            }
            else if (action == Action.WriteUpSubmitted)
            {
                actionString = "Write up was submitted";
            }
            else if (action == Action.FeedbackSubmitted)
            {
                actionString = "Feedback was submitted";
            }
            else if (action == Action.Disable2FA)
            {
                actionString = "2FA was disabled";
            }
            else if (action == Action.Enable2FA)
            {
                actionString = "2FA was enabled";
            }
            else if (action == Action.DisableAccount)
            {
                actionString = "Account was disabled";
            }
            else if (action == Action.EnableAccount)
            {
                actionString = "Account was enabled";
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