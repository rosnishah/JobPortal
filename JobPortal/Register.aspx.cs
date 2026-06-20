using System;
using System.IO;

namespace JobPortal
{
    public partial class Register : System.Web.UI.Page
    {
        string jobseekersFile;
        string employersFile;

        protected void Page_Load(object sender, EventArgs e)
        {
            jobseekersFile = Server.MapPath("~/App_Data/jobseekers.txt");
            employersFile = Server.MapPath("~/App_Data/employers.txt");

            if (!IsPostBack)
            {
                pnlJobSeeker.Visible = false;
                pnlEmployer.Visible = false;
            }
        }

        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            string role = ddlRole.SelectedValue;

            pnlJobSeeker.Visible = role == "JobSeeker";
            pnlEmployer.Visible = role == "Employer";
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = false;

            string u = txtUsername.Text.Trim();
            string p = txtPassword.Text.Trim();
            string email = txtEmail.Text.Trim();
            string role = ddlRole.SelectedValue;

            if (u == "" || p == "" || email == "" || role == "")
            {
                ShowMessage("Please fill all required fields.", "danger");
                return;
            }

            if (UserExists(u, role))
            {
                ShowMessage("Username already exists. Try another.", "danger");
                return;
            }

            try
            {
                if (role == "JobSeeker")
                {
                    string fullName = txtFullName.Text.Trim();

                    string line = u + "|" + p + "|" + fullName + "|" + email + Environment.NewLine;
                    File.AppendAllText(jobseekersFile, line);
                }
                else  // Employer
                {
                    string company = txtCompanyName.Text.Trim();

                    string line = u + "|" + p + "|" + company + "|" + email + Environment.NewLine;
                    File.AppendAllText(employersFile, line);
                }

                ShowMessage("Registration successful! You can now log in.", "success");
            }
            catch (Exception ex)
            {
                ShowMessage("Error: " + ex.Message, "danger");
            }
        }

        private bool UserExists(string username, string role)
        {
            string file = role == "JobSeeker" ? jobseekersFile : employersFile;

            if (!File.Exists(file)) return false;

            foreach (string line in File.ReadAllLines(file))
            {
                if (line.StartsWith(username + "|"))
                    return true;
            }

            return false;
        }

        private void ShowMessage(string msg, string type)
        {
            lblMessage.Text = msg;
            lblMessage.CssClass = "alert alert-" + type;
            lblMessage.Visible = true;
        }
    }
}
