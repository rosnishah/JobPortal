using System;
using System.IO;

namespace JobPortal
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) { }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string u = txtUsername.Text.Trim();
            string p = txtPassword.Text.Trim();

            string jsPath = Server.MapPath("~/App_Data/jobseekers.txt");
            string emPath = Server.MapPath("~/App_Data/employers.txt");

            // JOB SEEKER LOGIN
            if (File.Exists(jsPath))
            {
                foreach (var line in File.ReadAllLines(jsPath))
                {
                    var x = line.Split('|');
                    if (x.Length == 4 && x[0] == u && x[1] == p)
                    {
                        Session["JobSeekerId"] = u;
                        Session["JobSeekerName"] = x[2];
                        Response.Redirect("~/JobSeeker/JobSeekerDashboard.aspx");
                        return;
                    }
                }
            }

            // EMPLOYER LOGIN
            if (File.Exists(emPath))
            {
                foreach (var line in File.ReadAllLines(emPath))
                {
                    var x = line.Split('|');
                    if (x.Length == 4 && x[0] == u && x[1] == p)
                    {
                        Session["EmployerId"] = u;
                        Session["EmployerName"] = x[2];
                        Response.Redirect("~/Employer/EmployerDashboard.aspx");
                        return;
                    }
                }
            }

            lblMessage.Visible = true;
            lblMessage.Text = "Invalid username or password.";
        }
    }
}
