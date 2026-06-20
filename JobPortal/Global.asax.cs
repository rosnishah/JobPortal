using System;
using System.IO;
using System.Web;
using System.Collections.Generic;

namespace JobPortal
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            InitializeDataFiles();
            InitializeApplicationSettings();
        }

        void Application_End(object sender, EventArgs e)
        {
            // Code that runs on application shutdown
            CleanupResources();
        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            Exception ex = Server.GetLastError();
            LogError(ex);

            // IMPORTANT: Clear the error first
            Server.ClearError();

            // Safe redirect - check if error page exists
            string errorPage = "~/ErrorPage.aspx";
            if (File.Exists(Server.MapPath(errorPage)))
            {
                Response.Redirect(errorPage + "?error=" + HttpUtility.UrlEncode(ex.Message));
            }
            else
            {
                // If error page doesn't exist, redirect to home
                Response.Redirect("~/Default.aspx?error=unexpected");
            }
        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
            Session["UserSessionStart"] = DateTime.Now;
            Session["SessionID"] = Guid.NewGuid().ToString();
            Session["JobsViewed"] = new List<int>();

            // Track active sessions
            Application.Lock();
            Application["OnlineUsers"] = Convert.ToInt32(Application["OnlineUsers"] ?? 0) + 1;
            Application["TotalUsers"] = Convert.ToInt32(Application["TotalUsers"] ?? 0) + 1;
            Application.UnLock();
        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends
            Application.Lock();
            int onlineUsers = Convert.ToInt32(Application["OnlineUsers"] ?? 0);
            if (onlineUsers > 0)
            {
                Application["OnlineUsers"] = onlineUsers - 1;
            }
            Application.UnLock();
        }

        // Custom Methods
        private void InitializeDataFiles()
        {
            try
            {
                // Ensure data directory exists
                string dataPath = Server.MapPath("~/App_Data/");
                if (!Directory.Exists(dataPath))
                {
                    Directory.CreateDirectory(dataPath);
                }

                // Ensure resume directory exists
                string resumePath = Server.MapPath("~/Resumes/");
                if (!Directory.Exists(resumePath))
                {
                    Directory.CreateDirectory(resumePath);
                }

                // Initialize data files if they don't exist
                EnsureDataFiles();

                // Log application start
                LogMessage("Application started successfully at " + DateTime.Now);
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void EnsureDataFiles()
        {
            try
            {
                string dataPath = Server.MapPath("~/App_Data/");

                // Create users file if it doesn't exist
                string usersFile = Path.Combine(dataPath, "users.txt");
                if (!File.Exists(usersFile))
                {
                    File.WriteAllText(usersFile, "user_id,username,email,password,user_type,registration_date,is_active" + Environment.NewLine);
                }

                // Create jobs file if it doesn't exist
                string jobsFile = Path.Combine(dataPath, "jobs.txt");
                if (!File.Exists(jobsFile))
                {
                    File.WriteAllText(jobsFile, "job_id,employer_id,title,description,company,location,salary,job_type,posted_date,expiry_date,is_active,category" + Environment.NewLine);
                }

                // Create applications file if it doesn't exist
                string applicationsFile = Path.Combine(dataPath, "applications.txt");
                if (!File.Exists(applicationsFile))
                {
                    File.WriteAllText(applicationsFile, "application_id,job_id,user_id,apply_date,cover_letter,resume_path,status,application_date" + Environment.NewLine);
                }

                LogMessage("Data files initialized successfully");
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void InitializeApplicationSettings()
        {
            // Set application-wide settings
            Application.Lock();
            Application["AppStartTime"] = DateTime.Now;
            Application["OnlineUsers"] = 0;
            Application["TotalUsers"] = 0;
            Application["TotalJobs"] = 0;
            Application["SiteStartTime"] = DateTime.Now;
            Application.UnLock();
        }

        private void CleanupResources()
        {
            try
            {
                LogMessage("Application shutting down at " + DateTime.Now);
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void LogError(Exception ex)
        {
            try
            {
                string logPath = Server.MapPath("~/App_Data/error_log.txt");
                string errorMessage = $"[{DateTime.Now}] ERROR: {ex.Message}\nStackTrace: {ex.StackTrace}\n\n";
                File.AppendAllText(logPath, errorMessage);
            }
            catch
            {
                // If logging fails, there's not much we can do
            }
        }

        private void LogMessage(string message)
        {
            try
            {
                string logPath = Server.MapPath("~/App_Data/app_log.txt");
                string logMessage = $"[{DateTime.Now}] INFO: {message}\n";
                File.AppendAllText(logPath, logMessage);
            }
            catch
            {
                // Ignore logging errors
            }
        }
    }
}