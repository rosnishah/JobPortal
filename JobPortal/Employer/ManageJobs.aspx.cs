using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;

namespace JobPortal.Employer
{
    public partial class ManageJobs : System.Web.UI.Page
    {
        private string JobsFile => Server.MapPath("~/App_Data/jobs.txt");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["EmployerId"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }
            if (!IsPostBack) LoadJobs();
        }

        private List<string[]> LoadJobsRaw()
        {
            if (!File.Exists(JobsFile)) return new List<string[]>();
            return File.ReadAllLines(JobsFile)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Split('|'))
                .Where(p => p.Length == 10)
                .ToList();
        }

        private void LoadJobs()
        {
            string empId = Session["EmployerId"].ToString();

            var jobs = LoadJobsRaw()
                .Where(p => p[1] == empId)
                .Select(p => new
                {
                    JobId = p[0],
                    Title = p[2],
                    Category = p[4],
                    Location = p[5],
                    Salary = p[6],
                    Status = p[8],
                    ToggleText = p[8] == "Active" ? "Deactivate" : "Activate"
                })
                .ToList();

            gvManageJobs.DataSource = jobs;
            gvManageJobs.DataBind();
        }

        protected void gvManageJobs_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvManageJobs.EditIndex = e.NewEditIndex;
            LoadJobs();
        }

        protected void gvManageJobs_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvManageJobs.EditIndex = -1;
            LoadJobs();
        }

        protected void gvManageJobs_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string jobId = gvManageJobs.DataKeys[e.RowIndex].Value.ToString();
            GridViewRow row = gvManageJobs.Rows[e.RowIndex];

            // BoundFields render TextBox as first Control in cell when in edit mode.
            string newTitle = ((TextBox)row.Cells[1].Controls[0]).Text.Trim();
            string newCategory = ((TextBox)row.Cells[2].Controls[0]).Text.Trim();
            string newLocation = ((TextBox)row.Cells[3].Controls[0]).Text.Trim();
            string newSalary = ((TextBox)row.Cells[4].Controls[0]).Text.Trim();

            var lines = File.ReadAllLines(JobsFile).ToList();
            for (int i = 0; i < lines.Count; i++)
            {
                var p = lines[i].Split('|');
                if (p[0] == jobId)
                {
                    p[2] = newTitle;
                    p[4] = newCategory;
                    p[5] = newLocation;
                    p[6] = newSalary;
                    lines[i] = string.Join("|", p);
                    break;
                }
            }
            File.WriteAllLines(JobsFile, lines);

            gvManageJobs.EditIndex = -1;
            LoadJobs();
        }

        protected void gvManageJobs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string jobId = e.CommandArgument?.ToString();
            if (e.CommandName == "deleteJob") { DeleteJob(jobId); return; }
            if (e.CommandName == "toggleJob") { ToggleStatus(jobId); return; }
            if (e.CommandName == "viewApplicants") { Response.Redirect($"~/Employer/ViewApplicants.aspx?jobId={jobId}"); return; }
        }

        private void DeleteJob(string id)
        {
            if (!File.Exists(JobsFile)) return;
            var lines = File.ReadAllLines(JobsFile).ToList();
            lines.RemoveAll(x => x.StartsWith(id + "|"));
            File.WriteAllLines(JobsFile, lines);
            LoadJobs();
        }

        private void ToggleStatus(string id)
        {
            if (!File.Exists(JobsFile)) return;
            var lines = File.ReadAllLines(JobsFile).ToList();
            for (int i = 0; i < lines.Count; i++)
            {
                var p = lines[i].Split('|');
                if (p[0] == id)
                {
                    p[8] = p[8] == "Active" ? "Inactive" : "Active";
                    lines[i] = string.Join("|", p);
                    break;
                }
            }
            File.WriteAllLines(JobsFile, lines);
            LoadJobs();
        }
    }
}
