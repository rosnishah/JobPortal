<%@ Page Title="Post Job" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="PostJob.aspx.cs"
    Inherits="JobPortal.Employer.PostJob" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="mb-4">Post a New Job</h2>

    <div class="card p-4">

        <div class="mb-3">
            <label>Job Title</label>
            <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" />
        </div>

        <div class="mb-3">
            <label>Category</label>
            <asp:TextBox ID="txtCategory" runat="server" CssClass="form-control" />
        </div>

        <div class="mb-3">
            <label>Location</label>
            <asp:TextBox ID="txtLocation" runat="server" CssClass="form-control" />
        </div>

        <div class="mb-3">
            <label>Salary</label>
            <asp:TextBox ID="txtSalary" runat="server" CssClass="form-control" />
        </div>

        <div class="mb-3">
            <label>Description</label>
            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control" />
        </div>

        <asp:Button ID="btnSubmit" runat="server" Text="Post Job"
            CssClass="btn btn-primary px-4" OnClick="btnSubmit_Click" />

    </div>

</asp:Content>
