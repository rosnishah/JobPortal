using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace JobPortal.Shared
{
    public static class DataAccess
    {
        // ---------------------------------------------------------
        // SAFE BASE PATH LOAD (Fixes: HttpContext null issue)
        // ---------------------------------------------------------
        private static string BasePath
        {
            get
            {
                var context = HttpContext.Current;
                if (context == null) return AppDomain.CurrentDomain.BaseDirectory + "App_Data/";

                return context.Server.MapPath("~/App_Data/");
            }
        }

        private static string FilePath(string fileName) =>
            Path.Combine(BasePath, fileName);


        // ---------------------------------------------------------
        // FILE READ / WRITE HELPERS (Fully Safe)
        // ---------------------------------------------------------
        private static List<Dictionary<string, string>> ReadFile(string fileName)
        {
            var list = new List<Dictionary<string, string>>();
            string path = FilePath(fileName);

            if (!File.Exists(path))
                return list;

            foreach (var line in File.ReadAllLines(path))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                Dictionary<string, string> dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                foreach (var pair in line.Split('|'))
                {
                    var kv = pair.Split('=');
                    if (kv.Length != 2) continue;

                    string key = kv[0].Trim();
                    string value = kv[1].Trim();

                    if (!dict.ContainsKey(key))
                        dict[key] = value;
                }

                list.Add(dict);
            }

            return list;
        }

        private static void WriteFile(string fileName, List<Dictionary<string, string>> data)
        {
            string path = FilePath(fileName);

            var lines = data.Select(d =>
                string.Join("|", d.Select(p => $"{p.Key}={p.Value}"))
            );

            File.WriteAllLines(path, lines);
        }

        private static void AppendLine(string fileName, Dictionary<string, string> data)
        {
            string path = FilePath(fileName);

            string line = string.Join("|", data.Select(p => $"{p.Key}={p.Value}"));

            File.AppendAllLines(path, new[] { line });
        }


        // ---------------------------------------------------------
        // USER AUTH
        // ---------------------------------------------------------
        public static string GetUserRole(string username)
        {
            if (string.IsNullOrEmpty(username)) return "";

            var jobseekers = ReadFile("jobseekers.txt");
            if (jobseekers.Any(u => u.ContainsKey("Username") && u["Username"] == username))
                return "JobSeeker";

            var employers = ReadFile("employers.txt");
            if (employers.Any(u => u.ContainsKey("Username") && u["Username"] == username))
                return "Employer";

            return "";
        }

        public static Dictionary<string, string> LoginUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var jobseeker = ReadFile("jobseekers.txt")
                .FirstOrDefault(u =>
                    u.ContainsKey("Username") &&
                    u.ContainsKey("Password") &&
                    u["Username"] == username &&
                    u["Password"] == password
                );

            if (jobseeker != null)
                return jobseeker;

            var employer = ReadFile("employers.txt")
                .FirstOrDefault(u =>
                    u.ContainsKey("Username") &&
                    u.ContainsKey("Password") &&
                    u["Username"] == username &&
                    u["Password"] == password
                );

            return employer;
        }


        // ---------------------------------------------------------
        // JOB SEEKER FUNCTIONS
        // ---------------------------------------------------------
        public static void AddJobSeeker(Dictionary<string, string> user) =>
            AppendLine("jobseekers.txt", user);

        public static Dictionary<string, string> GetJobSeeker(string username)
        {
            if (string.IsNullOrEmpty(username)) return null;

            return ReadFile("jobseekers.txt")
                .FirstOrDefault(u =>
                    u.ContainsKey("Username") &&
                    u["Username"] == username
                );
        }


        // ---------------------------------------------------------
        // EMPLOYER FUNCTIONS
        // ---------------------------------------------------------
        public static void AddEmployer(Dictionary<string, string> data) =>
            AppendLine("employers.txt", data);

        public static Dictionary<string, string> GetEmployer(string employerId)
        {
            if (string.IsNullOrEmpty(employerId)) return null;

            return ReadFile("employers.txt")
                .FirstOrDefault(e =>
                    e.ContainsKey("EmployerId") &&
                    e["EmployerId"] == employerId
                );
        }


        // ---------------------------------------------------------
        // JOB FUNCTIONS
        // ---------------------------------------------------------
        public static void AddJob(Dictionary<string, string> job) =>
            AppendLine("jobs.txt", job);

        public static Dictionary<string, string> GetJob(string jobId)
        {
            if (string.IsNullOrEmpty(jobId)) return null;

            return ReadFile("jobs.txt")
                .FirstOrDefault(j =>
                    j.ContainsKey("JobId") &&
                    j["JobId"] == jobId
                );
        }

        public static List<Dictionary<string, string>> GetJobs() =>
            ReadFile("jobs.txt");

        public static void UpdateJob(Dictionary<string, string> updatedJob)
        {
            if (!updatedJob.ContainsKey("JobId"))
                return;

            string id = updatedJob["JobId"];
            var all = ReadFile("jobs.txt");

            for (int i = 0; i < all.Count; i++)
            {
                if (all[i].ContainsKey("JobId") && all[i]["JobId"] == id)
                {
                    all[i] = updatedJob;
                    break;
                }
            }

            WriteFile("jobs.txt", all);
        }


        // ---------------------------------------------------------
        // APPLICATION FUNCTIONS
        // ---------------------------------------------------------
        public static void AddApplication(Dictionary<string, string> data) =>
            AppendLine("applications.txt", data);

        public static List<Dictionary<string, string>> GetApplicationsByUser(string username)
        {
            if (string.IsNullOrEmpty(username)) return new List<Dictionary<string, string>>();

            return ReadFile("applications.txt")
                .Where(a =>
                    a.ContainsKey("Username") &&
                    a["Username"] == username
                ).ToList();
        }

        public static List<Dictionary<string, string>> GetApplicationsByEmployer(string employerId)
        {
            if (string.IsNullOrEmpty(employerId)) return new List<Dictionary<string, string>>();

            return ReadFile("applications.txt")
                .Where(a =>
                    a.ContainsKey("EmployerId") &&
                    a["EmployerId"] == employerId
                ).ToList();
        }

        public static void UpdateApplicationStatus(string appId, string newStatus)
        {
            var apps = ReadFile("applications.txt");
            bool changed = false;

            foreach (var app in apps)
            {
                if (app.ContainsKey("ApplicationId") && app["ApplicationId"] == appId)
                {
                    app["Status"] = newStatus;
                    changed = true;
                    break;
                }
            }

            if (changed)
                WriteFile("applications.txt", apps);
        }

        public static bool HasApplied(string jobId, string username)
        {
            if (string.IsNullOrEmpty(jobId) || string.IsNullOrEmpty(username))
                return false;

            return ReadFile("applications.txt")
                .Any(a =>
                    a.ContainsKey("JobId") &&
                    a.ContainsKey("Username") &&
                    a["JobId"] == jobId &&
                    a["Username"] == username
                );
        }
    }
}
