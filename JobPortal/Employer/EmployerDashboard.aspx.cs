using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace JobPortal.Employer
{
    public partial class EmployerDashboard : System.Web.UI.Page
    {
        private string GetJobsFile()
        {
            return Server.MapPath("~/App_Data/jobs.txt");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["EmployerId"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                lblUsername.Text = Session["EmployerName"].ToString();
                LoadAllJobs();
                LoadMyJobs();
            }
        }

        private List<string[]> LoadJobsRaw()
        {
            List<string[]> list = new List<string[]>();
            string path = GetJobsFile();

            if (!File.Exists(path)) return list;

            foreach (string line in File.ReadAllLines(path))
            {
                if (line.Trim() == "") continue;

                string clean = line.Trim();
                if (clean.EndsWith("|"))
                    clean = clean.Substring(0, clean.Length - 1);

                string[] p = clean.Split('|');

                if (p.Length == 10)
                {
                    list.Add(p);
                }
                else if (p.Length == 11 && p[10].Trim() == "")
                {
                    string[] corrected = new string[10];
                    Array.Copy(p, corrected, 10);
                    list.Add(corrected);
                }
            }

            return list;
        }

        private void LoadAllJobs()
        {
            List<string[]> jobs = LoadJobsRaw();
            List<object> data = new List<object>();

            foreach (string[] p in jobs)
            {
                data.Add(new
                {
                    Title = p[2],
                    Category = p[4],
                    Location = p[5],
                    Salary = p[6],
                    Status = p[8],
                    PostedOn = p[9]
                });
            }

            gvAllJobs.DataSource = data;
            gvAllJobs.DataBind();
        }

        private void LoadMyJobs()
        {
            string empId = Session["EmployerId"].ToString();
            List<string[]> jobs = LoadJobsRaw();
            List<object> mine = new List<object>();

            foreach (string[] p in jobs)
            {
                if (p[1] == empId)
                {
                    mine.Add(new
                    {
                        JobId = p[0],
                        Title = p[2],
                        Category = p[4],
                        Location = p[5],
                        Salary = p[6],
                        Status = p[8],
                        PostedOn = p[9],
                        ToggleText = (p[8] == "Active" ? "Deactivate" : "Activate")
                    });
                }
            }

            lblNoJobs.Visible = (mine.Count == 0);

            gvMyJobs.DataSource = mine;
            gvMyJobs.DataBind();
        }

        protected void gvMyJobs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string jobId = e.CommandArgument.ToString();

            if (e.CommandName == "EditJob")
            {
                Response.Redirect("~/Employer/ManageJobs.aspx?id=" + jobId);
            }
            else if (e.CommandName == "DeleteJob")
            {
                DeleteJob(jobId);
            }
            else if (e.CommandName == "ToggleJob")
            {
                ToggleJob(jobId);
            }
            else if (e.CommandName == "ViewApplicants")
            {
                Response.Redirect("~/Employer/ViewApplicants.aspx?jobId=" + jobId);
            }
        }

        private void DeleteJob(string id)
        {
            string path = GetJobsFile();
            List<string> lines = new List<string>(File.ReadAllLines(path));

            lines.RemoveAll(l => l.StartsWith(id + "|"));
            File.WriteAllLines(path, lines.ToArray());

            LoadMyJobs();
            LoadAllJobs();
        }

        private void ToggleJob(string id)
        {
            string path = GetJobsFile();
            List<string> lines = new List<string>(File.ReadAllLines(path));

            for (int i = 0; i < lines.Count; i++)
            {
                string clean = lines[i];
                if (clean.EndsWith("|"))
                    clean = clean.Substring(0, clean.Length - 1);

                string[] p = clean.Split('|');
                if (p.Length < 9) continue;

                if (p[0] == id)
                {
                    if (p[8] == "Active")
                        p[8] = "Inactive";
                    else
                        p[8] = "Active";

                    lines[i] = string.Join("|", p);
                }
            }

            File.WriteAllLines(path, lines.ToArray());
            LoadMyJobs();
            LoadAllJobs();
        }

        protected void btnPostJob_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Employer/PostJob.aspx");
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Default.aspx");
        }
    }
}
