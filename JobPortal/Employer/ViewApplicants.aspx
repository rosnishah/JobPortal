<%@ Page Title="View Applicants" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="ViewApplicants.aspx.cs"
    Inherits="JobPortal.Employer.ViewApplicants" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4">

        <h2 class="mb-3">Applicants for: 
            <asp:Literal ID="ltJobTitle" runat="server" />
        </h2>

        <asp:Label ID="lblNoApplicants" runat="server"
            CssClass="alert alert-info" Visible="false" />

        <asp:GridView ID="gvApplicants" runat="server"
            CssClass="table table-bordered table-hover"
            AutoGenerateColumns="False" Visible="false">

            <Columns>

                <asp:BoundField DataField="JobSeekerId" HeaderText="Applicant ID" />

                <asp:BoundField DataField="Name" HeaderText="Name" />

                <asp:BoundField DataField="Email" HeaderText="Email" />

                <asp:BoundField DataField="AppliedDate" HeaderText="Applied On" />

            </Columns>

        </asp:GridView>

    </div>

</asp:Content>
