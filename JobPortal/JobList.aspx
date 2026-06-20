<%@ Page Title="Job List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="JobList.aspx.cs" Inherits="JobPortal.JobList" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">

        <!-- PAGE HEADER -->
        <div class="row mb-4">
            <div class="col-md-8">
                <h2>Search Results</h2>
                <p class="text-muted">Showing jobs based on your search criteria</p>
            </div>

            <div class="col-md-4 text-end">
                <asp:Button ID="btnBack" runat="server"
                    Text="← Back to Home"
                    CssClass="btn btn-outline-secondary"
                    OnClick="btnBack_Click" />
            </div>
        </div>

        <!-- JOB LIST -->
        <div class="card">
            <div class="card-header">
                <h5 class="mb-0">Available Jobs</h5>
            </div>

            <div class="card-body">

                <!-- Repeater -->
                <asp:Repeater ID="rptJobs" runat="server">
                    <ItemTemplate>
                        <div class="job-item card mb-3">
                            <div class="card-body">

                                <h5 class="card-title"><%# Eval("Title") %></h5>

                                <p class="mb-1"><strong>Company:</strong> <%# Eval("Company") %></p>
                                <p class="mb-1"><strong>Location:</strong> <%# Eval("Location") %></p>
                                <p class="mb-1"><strong>Salary:</strong> <%# Eval("Salary") %></p>
                                <p class="mb-1"><strong>Type:</strong> <%# Eval("Type") %></p>

                                <div class="mt-3">

                                    <!-- VIEW DETAILS -->
                                    <asp:HyperLink 
                                        ID="hlViewDetails"
                                        runat="server"
                                        CssClass="btn btn-primary btn-sm"
                                        NavigateUrl='<%# "JobSeeker/JobDetails.aspx?JobId=" + Eval("JobId") %>'
                                        Text="View Details" />

                                    <!-- APPLY NOW -->
                                    <asp:HyperLink 
                                        ID="hlApply"
                                        runat="server"
                                        CssClass="btn btn-success btn-sm ms-2"
                                        NavigateUrl='<%# "JobSeeker/ApplyJob.aspx?JobId=" + Eval("JobId") %>'
                                        Text="Apply Now" />

                                </div>

                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

                <asp:Label ID="lblNoJobs" runat="server"
                    CssClass="text-danger fw-bold"
                    Visible="false"></asp:Label>

            </div>
        </div>

    </div>
</asp:Content>
