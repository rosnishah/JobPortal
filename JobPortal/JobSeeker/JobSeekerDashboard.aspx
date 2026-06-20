<%@ Page Title="Job Seeker Dashboard" Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeFile="JobSeekerDashboard.aspx.cs"
    Inherits="JobPortal.JobSeeker.JobSeekerDashboard" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">

    <h2 class="text-center mt-4 mb-4">
        Welcome, <asp:Label ID="lblName" runat="server" CssClass="text-primary fw-bold"></asp:Label>
    </h2>

    <div class="text-center mb-4">
        <asp:Button ID="btnLogout" runat="server" Text="Logout"
            CssClass="btn btn-danger" OnClick="btnLogout_Click" />
    </div>

    <ul class="nav nav-tabs mb-3">
        <li class="nav-item">
            <a class="nav-link active" data-bs-toggle="tab" href="#availableTab">Available Jobs</a>
        </li>
        <li class="nav-item">
            <a class="nav-link" data-bs-toggle="tab" href="#appliedTab">Applied Jobs</a>
        </li>
    </ul>

    <div class="tab-content">

        <!-- AVAILABLE JOBS -->
        <div class="tab-pane fade show active" id="availableTab">
            <asp:GridView ID="gvJobs" runat="server"
                AutoGenerateColumns="False" CssClass="table table-bordered"
                OnRowCommand="gvJobs_RowCommand">

                <Columns>
                    <asp:BoundField DataField="JobId" HeaderText="ID" />
                    <asp:BoundField DataField="Title" HeaderText="Title" />
                    <asp:BoundField DataField="Category" HeaderText="Category" />
                    <asp:BoundField DataField="Location" HeaderText="Location" />
                    <asp:BoundField DataField="Salary" HeaderText="Salary" />

                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:Button ID="btnApply" runat="server" Text="Apply"
                                CssClass="btn btn-success btn-sm"
                                CommandName="ApplyJob"
                                CommandArgument='<%# Eval("JobId") %>'
                                Visible='<%# !(bool)Eval("IsApplied") %>' />

                            <asp:Button ID="btnApplied" runat="server" Text="Applied"
                                CssClass="btn btn-secondary btn-sm"
                                Enabled="false"
                                Visible='<%# (bool)Eval("IsApplied") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>

        <!-- APPLIED JOBS -->
        <div class="tab-pane fade" id="appliedTab">

            <asp:GridView ID="gvApplied" runat="server"
                AutoGenerateColumns="False" CssClass="table table-bordered">

                <Columns>
                    <asp:BoundField DataField="JobId" HeaderText="ID" />
                    <asp:BoundField DataField="Title" HeaderText="Title" />
                    <asp:BoundField DataField="Category" HeaderText="Category" />
                    <asp:BoundField DataField="Location" HeaderText="Location" />
                    <asp:BoundField DataField="AppliedDate" HeaderText="Applied On" />
                </Columns>

            </asp:GridView>

        </div>

    </div>

</asp:Content>
