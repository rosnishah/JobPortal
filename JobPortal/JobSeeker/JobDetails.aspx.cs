using System;
using System.IO;
using System.Linq;

namespace JobPortal.JobSeeker
{
    public partial class JobDetails : System.Web.UI.Page
    {
        private string jobsPath => Server.MapPath("~/App_Data/jobs.txt");
        private string employersPath => Server.MapPath("~/App_Data/employers.txt");
        private string applicationsPath => Server.MapPath("~/App_Data/appliedjobs.txt");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) LoadJobDetails();
        }

        private string[] LoadJobById(string jobId)
        {
            if (!File.Exists(jobsPath)) return null;
            return File.ReadAllLines(jobsPath)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Split('|'))
                .Where(p => p.Length == 10 && p[0] == jobId)
                .FirstOrDefault();
        }

        private void LoadJobDetails()
        {
            string jobId = Request.QueryString["jobId"];
            if (string.IsNullOrWhiteSpace(jobId)) { ShowError("Invalid Job ID."); return; }

            var job = LoadJobById(jobId);
            if (job == null) { ShowError("Job not found."); return; }

            lblJobTitle.Text = job[2];
            lblCompany.Text = GetCompanyName(job[1]);
            lblLocation.Text = job[5];
            lblSalary.Text = job[6];
            lblJobType.Text = job[8];
            lblPostedDate.Text = DateTime.TryParse(job[9], out var d) ? d.ToString("dd MMM yyyy") : job[9];

            lblDescription.Text = Safe(job[3]);
            lblRequirements.Text = Safe(job[7]);
            lblResponsibilities.Text = Safe(job[4]);

            LoadCompanyDetails(job[1]);
            CheckApplyStatus(jobId);
            pnlDetails.Visible = true;
        }

        private void CheckApplyStatus(string jobId)
        {
            if (Session["JobSeekerId"] == null) { pnlLoginRequired.Visible = true; return; }
            string seekerId = Session["JobSeekerId"].ToString();
            if (!File.Exists(applicationsPath)) { pnlApply.Visible = true; return; }

            var applied = File.ReadAllLines(applicationsPath)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Split('|'))
                .Where(p => p.Length >= 3 && p[0] == jobId && p[1] == seekerId)
                .FirstOrDefault();

            if (applied == null) pnlApply.Visible = true;
            else { pnlAlreadyApplied.Visible = true; lblAppliedDate.Text = "You applied on: " + applied[2]; }
        }

        private string GetCompanyName(string employerId)
        {
            if (!File.Exists(employersPath)) return "Unknown Company";
            foreach (var l in File.ReadAllLines(employersPath))
            {
                if (string.IsNullOrWhiteSpace(l)) continue;
                var p = l.Split('|');
                // support EmployerId|Username|Password|CompanyName|Email and fallback Username|Password|Company|Email
                if (p.Length >= 5 && p[0] == employerId) return p[3];
                if (p.Length >= 4 && p[0] == employerId) return p[2];
            }
            return "Unknown Company";
        }

        private void LoadCompanyDetails(string employerId)
        {
            if (!File.Exists(employersPath)) return;
            foreach (var l in File.ReadAllLines(employersPath))
            {
                if (string.IsNullOrWhiteSpace(l)) continue;
                var p = l.Split('|');
                if ((p.Length >= 5 && p[0] == employerId) || (p.Length >= 4 && p[0] == employerId))
                {
                    lblCompanyName.Text = p.Length >= 4 ? p[3] : "Unknown";
                    lblCompanyEmail.Text = p.Length >= 5 ? p[4] : (p.Length >= 4 ? p[3] : "N/A");
                    lblCompanyPhone.Text = "N/A";
                    lblCompanyAddress.Text = "N/A";
                    hlCompanyWebsite.Text = "N/A";
                    lblCompanyDescription.Text = "No additional details available.";
                    return;
                }
            }
        }

        protected void btnApplyNow_Click(object sender, EventArgs e)
        {
            string jobId = Request.QueryString["jobId"];
            Response.Redirect("~/JobSeeker/ApplyJob.aspx?jobId=" + jobId);
        }

        private void ShowError(string msg) { lblMessage.Visible = true; lblMessage.Text = msg; pnlDetails.Visible = false; }

        protected string Safe(string x) { return string.IsNullOrWhiteSpace(x) ? "—" : x.Trim(); }
    }
}
