<%@ Page Title="Job Details" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="JobDetails.aspx.cs"
    Inherits="JobPortal.JobSeeker.JobDetails" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">

    <div class="container mt-4">

        <!-- ERROR MESSAGE -->
        <asp:Label ID="lblMessage" runat="server" CssClass="alert alert-danger d-block" Visible="false"></asp:Label>

        <!-- MAIN JOB DETAILS -->
        <asp:Panel ID="pnlDetails" runat="server" Visible="false">

            <h2 class="mb-3"><asp:Label ID="lblJobTitle" runat="server" /></h2>

            <p class="text-muted mb-4">
                <strong>Company:</strong> <asp:Label ID="lblCompany" runat="server" /><br />
                <strong>Location:</strong> <asp:Label ID="lblLocation" runat="server" /><br />
                <strong>Salary:</strong> <asp:Label ID="lblSalary" runat="server" /><br />
                <strong>Status:</strong> <asp:Label ID="lblJobType" runat="server" /><br />
                <strong>Posted:</strong> <asp:Label ID="lblPostedDate" runat="server" />
            </p>

            <hr />

            <h4>Description</h4>
            <p><asp:Label ID="lblDescription" runat="server" /></p>

            <h4>Requirements</h4>
            <p><asp:Label ID="lblRequirements" runat="server" /></p>

            <h4>Responsibilities</h4>
            <p><asp:Label ID="lblResponsibilities" runat="server" /></p>

            <hr />

            <h4>Company Info</h4>
            <p>
                <strong>Name:</strong> <asp:Label ID="lblCompanyName" runat="server" /><br />
                <strong>Email:</strong> <asp:Label ID="lblCompanyEmail" runat="server" /><br />
                <strong>Phone:</strong> <asp:Label ID="lblCompanyPhone" runat="server" /><br />
                <strong>Address:</strong> <asp:Label ID="lblCompanyAddress" runat="server" /><br />
                <strong>Website:</strong>
                <asp:HyperLink ID="hlCompanyWebsite" runat="server" Target="_blank" /><br />
                <strong>Description:</strong> <asp:Label ID="lblCompanyDescription" runat="server" />
            </p>

            <hr />

            <!-- NOT LOGGED IN -->
<asp:Panel ID="pnlLoginRequired" runat="server" Visible="false" CssClass="alert alert-warning">
    Please <a href="~/Login.aspx" runat="server">login</a> to apply.
</asp:Panel>


            <!-- APPLY BUTTON -->
            <asp:Panel ID="pnlApply" runat="server" Visible="false">
                <asp:Button ID="btnApplyNow" runat="server"
                    CssClass="btn btn-primary btn-lg"
                    Text="Apply Now" OnClick="btnApplyNow_Click" />
            </asp:Panel>

            <!-- ALREADY APPLIED -->
            <asp:Panel ID="pnlAlreadyApplied" runat="server" Visible="false" CssClass="alert alert-info mt-3">
                <asp:Label ID="lblAppliedDate" runat="server" CssClass="fw-bold"></asp:Label>
            </asp:Panel>

        </asp:Panel>

    </div>

</asp:Content>
