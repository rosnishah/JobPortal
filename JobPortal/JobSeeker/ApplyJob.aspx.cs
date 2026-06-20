using System;
using System.IO;
using System.Web.UI;

namespace JobPortal.JobSeeker
{
    public partial class ApplyJob : Page
    {
        protected string ResumeFolder = "~/Resumes/";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null || Session["Role"]?.ToString() != "JobSeeker")
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadJobDetails();
                LoadApplicantInfo();
            }
        }

        private void LoadJobDetails()
        {
            lblJobTitle.Text = "Software Developer";
            lblCompany.Text = "Tech Solutions Ltd.";

            lblPreviewTitle.Text = lblJobTitle.Text;
            lblPreviewCompany.Text = lblCompany.Text;
            lblPreviewLocation.Text = "Bangalore";
            lblPreviewSalary.Text = "₹50,000 - ₹70,000";
            lblPreviewJobType.Text = "Full-Time";
            lblPreviewPosted.Text = DateTime.Now.AddDays(-5).ToShortDateString();
        }

        private void LoadApplicantInfo()
        {
            txtFullName.Text = Session["Username"].ToString();
            txtEmail.Text = "john@example.com";
            txtPhone.Text = "9876543210";

            string resumePath = Server.MapPath(ResumeFolder + "resume_john.pdf");

            if (File.Exists(resumePath))
            {
                hlResume.NavigateUrl = ResumeFolder + "resume_john.pdf";
                hlResume.Visible = true;
                lblNoResume.Visible = false;
            }
            else
            {
                hlResume.Visible = false;
                lblNoResume.Visible = true;
            }

            bool alreadyApplied = false;

            pnlAlreadyApplied.Visible = alreadyApplied;
            pnlApplyForm.Visible = !alreadyApplied;
            lblApplicationStatus.Visible = alreadyApplied;
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (fuNewResume.HasFile)
            {
                string ext = Path.GetExtension(fuNewResume.FileName).ToLower();
                string[] allowed = { ".pdf", ".doc", ".docx", ".txt" };

                if (Array.IndexOf(allowed, ext) < 0)
                {
                    lblValidationMessage.Text = "Invalid file format! Only PDF/DOC/DOCX/TXT allowed.";
                    lblValidationMessage.Visible = true;
                    return;
                }

                if (fuNewResume.PostedFile.ContentLength > 5 * 1024 * 1024)
                {
                    lblValidationMessage.Text = "File too large! Max allowed size is 5MB.";
                    lblValidationMessage.Visible = true;
                    return;
                }

                string fileName = Guid.NewGuid() + ext;
                string fullPath = Server.MapPath(ResumeFolder + fileName);
                fuNewResume.SaveAs(fullPath);
            }

            pnlApplyForm.Visible = false;
            pnlAlreadyApplied.Visible = true;
            lblApplicationStatus.Visible = true;
            lblApplicationStatus.Text = "Your application has been submitted.";
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
            => Response.Redirect("~/JobSeeker/JobDetails.aspx");

        protected void BtnPreview_Click(object sender, EventArgs e)
            => lblPreviewCoverLetter.Text = txtCoverLetter.Text;

        protected void BtnTemplateProfessional_Click(object sender, EventArgs e)
            => txtCoverLetter.Text = "Dear Hiring Manager,\nI am applying for [Position]...";

        protected void BtnTemplateEnthusiastic_Click(object sender, EventArgs e)
            => txtCoverLetter.Text = "Hello Team!\nI am excited to apply for [Position]...";

        protected void BtnTemplateConcise_Click(object sender, EventArgs e)
            => txtCoverLetter.Text = "Dear Sir/Madam,\nPlease find my application for [Position].";

        protected void BtnClearCoverLetter_Click(object sender, EventArgs e)
            => txtCoverLetter.Text = "";

        protected void BtnViewApplication_Click(object sender, EventArgs e)
            => Response.Redirect("~/JobSeeker/MyApplications.aspx");
    }
}
