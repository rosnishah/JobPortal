<%@ Page Title="Employer Dashboard" Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeFile="EmployerDashboard.aspx.cs"
    Inherits="JobPortal.Employer.EmployerDashboard" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <h2 class="text-center mt-4 mb-4">
        Welcome, <asp:Label ID="lblUsername" runat="server" CssClass="text-primary fw-bold"></asp:Label>
    </h2>

    <div class="text-center mb-4">
        <asp:Button ID="btnPostJob" runat="server" Text="Post New Job"
            CssClass="btn btn-success me-2" OnClick="btnPostJob_Click" />

        <!-- MANAGE JOB BUTTON REMOVED -->

        <asp:Button ID="btnLogout" runat="server" Text="Logout"
            CssClass="btn btn-danger" OnClick="btnLogout_Click" />
    </div>

    <h4 class="mt-4 mb-3 text-center">All Jobs</h4>

    <asp:GridView ID="gvAllJobs" runat="server"
        CssClass="table table-bordered table-striped"
        AutoGenerateColumns="False">

        <Columns>
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="Category" HeaderText="Category" />
            <asp:BoundField DataField="Location" HeaderText="Location" />
            <asp:BoundField DataField="Salary" HeaderText="Salary" />
            <asp:BoundField DataField="Status" HeaderText="Status" />
            <asp:BoundField DataField="PostedOn" HeaderText="Posted On" />
        </Columns>

    </asp:GridView>

    <hr />

    <h4 class="mt-4 mb-3 text-center">Your Posted Jobs</h4>

    <asp:Label ID="lblNoJobs" runat="server" Visible="false"
        CssClass="text-danger fw-bold text-center d-block"></asp:Label>

    <asp:GridView ID="gvMyJobs" runat="server"
        CssClass="table table-bordered table-striped"
        AutoGenerateColumns="False"
        OnRowCommand="gvMyJobs_RowCommand">

        <Columns>

            <asp:BoundField DataField="Title" HeaderText="Job Title" />
            <asp:BoundField DataField="Category" HeaderText="Category" />
            <asp:BoundField DataField="Location" HeaderText="Location" />
            <asp:BoundField DataField="Salary" HeaderText="Salary" />
            <asp:BoundField DataField="Status" HeaderText="Status" />
            <asp:BoundField DataField="PostedOn" HeaderText="Posted On" />

            <asp:TemplateField HeaderText="Edit">
                <ItemTemplate>
                    <asp:LinkButton ID="btnEdit" runat="server" Text="Edit"
                        CssClass="btn btn-primary btn-sm"
                        CommandName="EditJob"
                        CommandArgument='<%# Eval("JobId") %>' />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Delete">
                <ItemTemplate>
                    <asp:LinkButton ID="btnDelete" runat="server" Text="Delete"
                        CssClass="btn btn-danger btn-sm"
                        CommandName="DeleteJob"
                        CommandArgument='<%# Eval("JobId") %>'
                        OnClientClick="return confirm('Delete this job?');" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Toggle Status">
                <ItemTemplate>
                    <asp:LinkButton ID="btnToggle" runat="server"
                        Text='<%# Eval("ToggleText") %>'
                        CssClass="btn btn-warning btn-sm"
                        CommandName="ToggleJob"
                        CommandArgument='<%# Eval("JobId") %>' />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Applicants">
                <ItemTemplate>
                    <asp:LinkButton ID="btnApplicants" runat="server" Text="View"
                        CssClass="btn btn-info btn-sm"
                        CommandName="ViewApplicants"
                        CommandArgument='<%# Eval("JobId") %>' />
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>

</asp:Content>
