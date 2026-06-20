using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JobPortal.JobSeeker
{
    public partial class JobSeekerDashboard : System.Web.UI.Page
    {
        private string GetJobsFile()
        {
            return Server.MapPath("~/App_Data/jobs.txt");
        }

        private string GetAppliedFile()
        {
            return Server.MapPath("~/App_Data/appliedjobs.txt");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["JobSeekerId"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            lblName.Text = Session["JobSeekerName"].ToString();

            if (!IsPostBack)
            {
                LoadAvailableJobs();
                LoadAppliedJobs();
            }
        }

        private List<string[]> LoadJobsRaw()
        {
            string path = GetJobsFile();
            List<string[]> list = new List<string[]>();

            if (!File.Exists(path)) return list;

            foreach (string line in File.ReadAllLines(path))
            {
                if (line.Trim() == "") continue;

                string[] p = line.Split('|');
                if (p.Length == 10)
                    list.Add(p);
            }

            return list;
        }

        private void LoadAvailableJobs()
        {
            string jsId = Session["JobSeekerId"].ToString();
            string appliedPath = GetAppliedFile();

            HashSet<string> applied = new HashSet<string>();

            if (File.Exists(appliedPath))
            {
                foreach (string line in File.ReadAllLines(appliedPath))
                {
                    if (line.Trim() == "") continue;

                    string[] p = line.Split('|');
                    if (p.Length >= 3 && p[1] == jsId)
                        applied.Add(p[0]);
                }
            }

            List<object> jobs = new List<object>();

            foreach (string[] p in LoadJobsRaw())
            {
                jobs.Add(new
                {
                    JobId = p[0],
                    Title = p[2],
                    Category = p[4],
                    Location = p[5],
                    Salary = p[6],
                    IsApplied = applied.Contains(p[0])
                });
            }

            gvJobs.DataSource = jobs;
            gvJobs.DataBind();
        }

        private void LoadAppliedJobs()
        {
            string appliedPath = GetAppliedFile();
            string jsId = Session["JobSeekerId"].ToString();

            List<object> list = new List<object>();
            Dictionary<string[], string> jobDict = new Dictionary<string[], string>();

            List<string[]> jobs = LoadJobsRaw();

            if (!File.Exists(appliedPath))
            {
                gvApplied.DataSource = null;
                gvApplied.DataBind();
                return;
            }

            foreach (string line in File.ReadAllLines(appliedPath))
            {
                if (line.Trim() == "") continue;

                string[] p = line.Split('|');
                if (p.Length < 3) continue;

                if (p[1] == jsId)
                {
                    foreach (string[] job in jobs)
                    {
                        if (job[0] == p[0])
                        {
                            list.Add(new
                            {
                                JobId = job[0],
                                Title = job[2],
                                Category = job[4],
                                Location = job[5],
                                AppliedDate = p[2]
                            });
                        }
                    }
                }
            }

            gvApplied.DataSource = list;
            gvApplied.DataBind();
        }

        protected void gvJobs_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ApplyJob")
            {
                string jobId = e.CommandArgument.ToString();
                string jsId = Session["JobSeekerId"].ToString();
                string path = GetAppliedFile();

                if (!File.Exists(path))
                    File.WriteAllText(path, "");

                // prevent duplicate apply
                foreach (string line in File.ReadAllLines(path))
                {
                    if (line.StartsWith(jobId + "|" + jsId + "|"))
                        return;
                }

                File.AppendAllText(path,
                    jobId + "|" + jsId + "|" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + Environment.NewLine);

                LoadAvailableJobs();
                LoadAppliedJobs();
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Default.aspx");
        }
    }
}
