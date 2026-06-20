<%@ Page Title="Apply Job" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="ApplyJob.aspx.cs"
    Inherits="JobPortal.JobSeeker.ApplyJob" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="mb-4">Apply for Job</h2>

    <!-- JOB DETAILS -->
    <div class="card p-3 mb-4">
        <h4>Job Details</h4>

        <p><strong>Title:</strong> <asp:Label ID="lblJobTitle" runat="server" /></p>
        <p><strong>Company:</strong> <asp:Label ID="lblCompany" runat="server" /></p>
    </div>

    <!-- JOB PREVIEW -->
    <asp:Panel ID="pnlPreview" runat="server" CssClass="card p-3 mb-4">
        <h4>Job Preview</h4>

        <p><strong>Title:</strong> <asp:Label ID="lblPreviewTitle" runat="server" /></p>
        <p><strong>Company:</strong> <asp:Label ID="lblPreviewCompany" runat="server" /></p>
        <p><strong>Location:</strong> <asp:Label ID="lblPreviewLocation" runat="server" /></p>
        <p><strong>Salary:</strong> <asp:Label ID="lblPreviewSalary" runat="server" /></p>
        <p><strong>Type:</strong> <asp:Label ID="lblPreviewJobType" runat="server" /></p>
        <p><strong>Posted:</strong> <asp:Label ID="lblPreviewPosted" runat="server" /></p>
    </asp:Panel>

    <!-- ALREADY APPLIED -->
    <asp:Panel ID="pnlAlreadyApplied" runat="server" Visible="false" CssClass="alert alert-info">
        You have already applied for this job.
        <br /><br />
        <asp:Label ID="lblApplicationStatus" runat="server" />
        <br /><br />
        <asp:Button ID="BtnViewApplication" runat="server" CssClass="btn btn-primary"
            Text="View My Applications" OnClick="BtnViewApplication_Click" />
    </asp:Panel>

    <!-- APPLY FORM -->
    <asp:Panel ID="pnlApplyForm" runat="server">

        <div class="card p-3 mb-4">
            <h4>Your Details</h4>

            Full Name:
            <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control mb-2" />

            Email:
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control mb-2" />

            Phone:
            <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control mb-2" />

            <h5 class="mt-4">Resume</h5>
            <asp:HyperLink ID="hlResume" runat="server" 
                Text="View Current Resume" Target="_blank"
                CssClass="text-primary" Visible="false" />

            <asp:Label ID="lblNoResume" runat="server" 
                Text="No Resume Uploaded" CssClass="text-danger" />

            <br /><br />
            Upload New Resume:
            <asp:FileUpload ID="fuNewResume" runat="server" CssClass="form-control mb-3" />

            <asp:Label ID="lblValidationMessage" runat="server" CssClass="text-danger mb-3" Visible="false" />

            <h5>Cover Letter</h5>
            <asp:TextBox ID="txtCoverLetter" runat="server" TextMode="MultiLine"
                CssClass="form-control mb-3" Rows="6" />

            <asp:Button ID="BtnTemplateProfessional" runat="server" Text="Professional"
                CssClass="btn btn-secondary me-2" OnClick="BtnTemplateProfessional_Click" />

            <asp:Button ID="BtnTemplateEnthusiastic" runat="server" Text="Enthusiastic"
                CssClass="btn btn-secondary me-2" OnClick="BtnTemplateEnthusiastic_Click" />

            <asp:Button ID="BtnTemplateConcise" runat="server" Text="Concise"
                CssClass="btn btn-secondary me-2" OnClick="BtnTemplateConcise_Click" />

            <asp:Button ID="BtnClearCoverLetter" runat="server" Text="Clear"
                CssClass="btn btn-danger" OnClick="BtnClearCoverLetter_Click" />

            <hr />

            <asp:Button ID="BtnPreview" runat="server" Text="Preview"
                CssClass="btn btn-info me-2" OnClick="BtnPreview_Click" />

            <asp:Button ID="BtnSubmit" runat="server" Text="Submit Application"
                CssClass="btn btn-success me-2" OnClick="BtnSubmit_Click" />

            <asp:Button ID="BtnCancel" runat="server" Text="Cancel"
                CssClass="btn btn-warning" OnClick="BtnCancel_Click" />
        </div>

        <!-- COVER LETTER PREVIEW -->
        <asp:Panel ID="pnlPreviewCoverLetter" runat="server" CssClass="card p-3">
            <h4>Cover Letter Preview</h4>
            <asp:Label ID="lblPreviewCoverLetter" runat="server" />
        </asp:Panel>

    </asp:Panel>

</asp:Content>
