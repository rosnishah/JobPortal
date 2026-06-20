using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JobPortal.Employer
{
    public partial class ViewApplicants : System.Web.UI.Page
    {
        private string appliedPath => Server.MapPath("~/App_Data/appliedjobs.txt");
        private string jobseekersPath => Server.MapPath("~/App_Data/jobseekers.txt");
        private string jobsPath => Server.MapPath("~/App_Data/jobs.txt");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["EmployerId"] == null) { Response.Redirect("~/Login.aspx"); return; }
            if (!IsPostBack)
            {
                string jobId = Request.QueryString["jobId"];
                if (string.IsNullOrEmpty(jobId)) { lblNoApplicants.Text = "Invalid job."; lblNoApplicants.Visible = true; return; }
                LoadJobTitle(jobId);
                LoadApplicants(jobId);
            }
        }

        private void LoadJobTitle(string jobId)
        {
            if (!File.Exists(jobsPath)) { ltJobTitle.Text = "Unknown"; return; }
            var job = File.ReadAllLines(jobsPath).Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Split('|')).Where(p => p.Length == 10 && p[0] == jobId).FirstOrDefault();
            ltJobTitle.Text = job != null ? job[2] : "Unknown";
        }

        private void LoadApplicants(string jobId)
        {
            if (!File.Exists(appliedPath)) { lblNoApplicants.Text = "No applicants yet."; lblNoApplicants.Visible = true; return; }

            var seekers = new Dictionary<string, (string Name, string Email)>();
            if (File.Exists(jobseekersPath))
            {
                foreach (var l in File.ReadAllLines(jobseekersPath))
                {
                    if (string.IsNullOrWhiteSpace(l)) continue;
                    var p = l.Split('|');
                    if (p.Length >= 4) seekers[p[0]] = (p[2], p[3]);
                }
            }

            var list = new List<object>();
            foreach (var l in File.ReadAllLines(appliedPath))
            {
                if (string.IsNullOrWhiteSpace(l)) continue;
                var p = l.Split('|');
                if (p.Length < 3) continue;
                if (p[0] != jobId) continue;
                string seekerId = p[1];
                string appliedOn = p[2];
                var info = seekers.ContainsKey(seekerId) ? seekers[seekerId] : (Name: seekerId, Email: "N/A");
                list.Add(new { JobSeekerId = seekerId, Name = info.Name, Email = info.Email, AppliedDate = appliedOn });
            }

            if (list.Count == 0) { lblNoApplicants.Text = "No applicants yet."; lblNoApplicants.Visible = true; return; }

            gvApplicants.DataSource = list; gvApplicants.DataBind(); gvApplicants.Visible = true;
        }
    }
}
