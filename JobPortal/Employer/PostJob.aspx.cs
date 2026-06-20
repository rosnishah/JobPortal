using System;
using System.IO;

namespace JobPortal.Employer
{
    public partial class PostJob : System.Web.UI.Page
    {
        string JobsPath => Server.MapPath("~/App_Data/jobs.txt");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["EmployerId"] == null)
                Response.Redirect("~/Login.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string id = "J" + DateTime.Now.Ticks;
            string employerId = Session["EmployerId"].ToString();
            string title = txtTitle.Text.Trim();
            string category = txtCategory.Text.Trim();
            string location = txtLocation.Text.Trim();
            string salary = txtSalary.Text.Trim();
            string desc = txtDescription.Text.Trim();

            string status = "Active";
            string created = DateTime.Now.ToString("yyyy-MM-dd");

            string line = $"{id}|{employerId}|{title}|{desc}|{category}|{location}|{salary}|-|{status}|{created}|";

            File.AppendAllText(JobsPath, line + Environment.NewLine);

            Response.Redirect("~/Employer/EmployerDashboard.aspx");
        }
    }
}
