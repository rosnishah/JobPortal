using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;

namespace JobPortal
{
    public partial class Default : System.Web.UI.Page
    {
        string jobPath;

        protected void Page_Load(object sender, EventArgs e)
        {
            jobPath = Server.MapPath("~/App_Data/jobs.txt");

            if (!IsPostBack)
            {
                LoadRecentJobs();
            }
        }

        // LOAD RECENT JOBS
        private void LoadRecentJobs()
        {
            if (!File.Exists(jobPath))
            {
                lblNoJobs.Visible = true;
                return;
            }

            var jobs = File.ReadAllLines(jobPath)
                           .Where(l => !string.IsNullOrWhiteSpace(l))
                           .Select(l => Job.FromLine(l))
                           .OrderByDescending(j => j.PostedDate)
                           .Take(5)
                           .ToList();

            if (jobs.Count == 0)
            {
                lblNoJobs.Visible = true;
                return;
            }

            rptRecentJobs.DataSource = jobs;
            rptRecentJobs.DataBind();

            hlViewAllJobs.Visible = true;
        }

        // SEARCH BUTTON
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string query = txtSearch.Text.Trim();
            string location = ddlLocation.SelectedValue;

            string url = "~/JobList.aspx?search=" + query + "&location=" + location;

            Response.Redirect(url);
        }

        // HANDLE BUTTONS INSIDE REPEATER
        protected void rptRecentJobs_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item ||
                e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem)
            {
                Job job = (Job)e.Item.DataItem;

                HyperLink hlViewDetails = (HyperLink)e.Item.FindControl("hlViewDetails");
                HyperLink hlApplyNow = (HyperLink)e.Item.FindControl("hlApplyNow");
                HyperLink hlLoginToApply = (HyperLink)e.Item.FindControl("hlLoginToApply");

                // ** FIXED PATH **
                hlViewDetails.NavigateUrl = "~/JobSeeker/JobDetails.aspx?jobId=" + job.JobId;

                // CHECK LOGIN STATUS
                if (Session["JobSeekerId"] != null)
                {
                    hlApplyNow.Visible = true;
                    hlLoginToApply.Visible = false;

                    hlApplyNow.NavigateUrl = "~/JobSeeker/ApplyJob.aspx?jobId=" + job.JobId;
                }
                else
                {
                    hlApplyNow.Visible = false;
                    hlLoginToApply.Visible = true;

                    hlLoginToApply.NavigateUrl = "~/Login.aspx";
                }
            }
        }
    }

    // JOB MODEL
    public class Job
    {
        public string JobId { get; set; }
        public string Company { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public DateTime PostedDate { get; set; }

        public static Job FromLine(string line)
        {
            // Format:
            // JOB101|E101|Frontend Developer|React Dev|IT|Delhi|6 LPA|Full-Time|Active|2025-11-27
            var p = line.Split('|');

            return new Job
            {
                JobId = p[0],
                Company = p[2],
                Title = p[2],
                Location = p[5],
                Category = p[4],
                PostedDate = DateTime.TryParse(p[9], out DateTime date) ? date : DateTime.Now
            };
        }
    }
}
