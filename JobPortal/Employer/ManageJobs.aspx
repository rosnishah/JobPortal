<%@ Page Title="Manage Jobs" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="ManageJobs.aspx.cs"
    Inherits="JobPortal.Employer.ManageJobs" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">

    <h2 class="mb-4">Manage Your Jobs</h2>

    <asp:GridView ID="gvManageJobs" runat="server"
        CssClass="table table-striped table-bordered"
        AutoGenerateColumns="False"
        DataKeyNames="JobId"
        OnRowCommand="gvManageJobs_RowCommand"
        OnRowEditing="gvManageJobs_RowEditing"
        OnRowCancelingEdit="gvManageJobs_RowCancelingEdit"
        OnRowUpdating="gvManageJobs_RowUpdating">

        <Columns>

            <%-- JOB ID COLUMN --%>
            <asp:BoundField DataField="JobId" HeaderText="ID" ReadOnly="True" />

            <%-- EDITABLE FIELDS --%>
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="Category" HeaderText="Category" />
            <asp:BoundField DataField="Location" HeaderText="Location" />
            <asp:BoundField DataField="Salary" HeaderText="Salary" />

            <%-- STATUS COLUMN --%>
            <asp:BoundField DataField="Status" HeaderText="Status" ReadOnly="True" />

            <%-- ACTION BUTTONS --%>
            <asp:TemplateField HeaderText="Actions">
                <ItemTemplate>

                    <asp:LinkButton ID="btnEdit" runat="server"
                        Text="Edit" CssClass="btn btn-sm btn-primary"
                        CommandName="Edit" />

                    <asp:LinkButton ID="btnDelete" runat="server"
                        Text="Delete" CssClass="btn btn-sm btn-danger"
                        CommandName="deleteJob"
                        CommandArgument='<%# Eval("JobId") %>'
                        OnClientClick="return confirm('Delete this job?');" />

                    <asp:LinkButton ID="btnToggle" runat="server"
                        Text='<%# Eval("ToggleText") %>'
                        CssClass="btn btn-sm btn-warning"
                        CommandName="toggleJob"
                        CommandArgument='<%# Eval("JobId") %>' />

                    <asp:LinkButton ID="btnViewApplicants" runat="server"
                        Text="View Applicants"
                        CssClass="btn btn-sm btn-info"
                        CommandName="viewApplicants"
                        CommandArgument='<%# Eval("JobId") %>' />

                </ItemTemplate>

                <EditItemTemplate>

                    <asp:LinkButton runat="server"
                        Text="Save" CssClass="btn btn-sm btn-success"
                        CommandName="Update" />

                    <asp:LinkButton runat="server"
                        Text="Cancel" CssClass="btn btn-sm btn-secondary"
                        CommandName="Cancel" />

                </EditItemTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>

</asp:Content>
