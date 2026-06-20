using System;

namespace JobPortal
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserType"] == null) return;

            string type = Session["UserType"].ToString();

            var phJobSeeker = LoginView1.FindControl("phJobSeeker");
            var phEmployer = LoginView1.FindControl("phEmployer");

            if (phJobSeeker != null) phJobSeeker.Visible = false;
            if (phEmployer != null) phEmployer.Visible = false;

            if (type == "JobSeeker" && phJobSeeker != null)
                phJobSeeker.Visible = true;

            if (type == "Employer" && phEmployer != null)
                phEmployer.Visible = true;
        }
    }
}
