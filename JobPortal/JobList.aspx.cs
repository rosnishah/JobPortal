using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace JobPortal
{
    public partial class JobList : System.Web.UI.Page
    {
        private string DataFolder => Server.MapPath("~/App_Data/");
        private string JobsFile => Path.Combine(DataFolder, "jobs.txt");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadJobs();
            }
        }

        private void LoadJobs()
        {
            if (!File.Exists(JobsFile))
            {
                lblNoJobs.Text = "Jobs file missing!";
                lblNoJobs.Visible = true;
                return;
            }

            var jobs = File.ReadAllLines(JobsFile)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line => line.Split('|'))
                .Where(x => x.Length >= 9)
                .Select(x => new
                {
                    JobId = x[0],
                    Title = x[1],
                    Company = x[2],
                    Location = x[3],
                    Salary = x[4],
                    Type = x[5]
                }).ToList();

            if (jobs.Count == 0)
            {
                lblNoJobs.Text = "No jobs found!";
                lblNoJobs.Visible = true;
                return;
            }

            rptJobs.DataSource = jobs;
            rptJobs.DataBind();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}
