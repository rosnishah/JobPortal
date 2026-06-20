<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs"
    Inherits="JobPortal.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <!-- HERO SECTION -->
    <div class="hero-section text-center text-white"
         style="background: linear-gradient(135deg,#667eea,#764ba2); padding:70px 20px;">
        <h1 class="display-4 fw-bold">Find Your Dream Job</h1>
        <p class="lead">Search thousands of jobs and start your career journey</p>
    </div>

    <!-- SEARCH BAR -->
    <div class="container mt-4">
        <div class="row g-2">
            <div class="col-md-6">
                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control form-control-lg"
                    placeholder="Search by job title or keyword"></asp:TextBox>
            </div>

            <div class="col-md-4">
                <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control form-control-lg">
                    <asp:ListItem Text="Select Location" Value="" />
                    <asp:ListItem Text="Delhi" Value="Delhi" />
                    <asp:ListItem Text="Mumbai" Value="Mumbai" />
                    <asp:ListItem Text="Hyderabad" Value="Hyderabad" />
                    <asp:ListItem Text="Bangalore" Value="Bangalore" />
                    <asp:ListItem Text="Chennai" Value="Chennai" />
                    <asp:ListItem Text="Kolkata" Value="Kolkata" />
                </asp:DropDownList>
            </div>

            <div class="col-md-2">
                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-lg w-100"
                    Text="Search" OnClick="btnSearch_Click" />
            </div>
        </div>
    </div>

    <!-- RECENT JOBS SECTION -->
    <div class="container mt-5">
        <h3 class="fw-bold mb-3">Recent Jobs</h3>

        <asp:Label ID="lblNoJobs" runat="server"
            Text="No jobs available at the moment." CssClass="text-danger fw-bold"
            Visible="false"></asp:Label>

        <asp:Repeater ID="rptRecentJobs" runat="server" OnItemDataBound="rptRecentJobs_ItemDataBound">
            <ItemTemplate>
                <div class="card mb-3 shadow-sm">
                    <div class="card-body">
                        <h5 class="card-title"><%# Eval("Title") %></h5>

                        <p class="mb-1">
                            <strong>Company:</strong> <%# Eval("Company") %>
                        </p>

                        <p class="mb-1">
                            <strong>Location:</strong> <%# Eval("Location") %>
                        </p>

                        <p class="mb-1">
                            <strong>Category:</strong> <%# Eval("Category") %>
                        </p>

                        <p class="text-muted small">Posted: <%# Eval("PostedDate", "{0:dd MMM yyyy}") %></p>

                        <asp:HyperLink ID="hlViewDetails" runat="server"
                            Text="View Details"
                            CssClass="btn btn-outline-primary btn-sm me-2" />

                        <asp:HyperLink ID="hlApplyNow" runat="server"
                            Text="Apply Now"
                            Visible="false"
                            CssClass="btn btn-success btn-sm me-2" />

                        <asp:HyperLink ID="hlLoginToApply" runat="server"
                            Text="Login to Apply"
                            Visible="false"
                            CssClass="btn btn-warning btn-sm" />
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <asp:HyperLink ID="hlViewAllJobs" runat="server" Text="View All Jobs →"
            NavigateUrl="~/JobList.aspx"
            CssClass="btn btn-outline-secondary mt-3"
            Visible="false" />
    </div>

</asp:Content>
