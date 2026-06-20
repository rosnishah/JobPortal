<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="JobPortal.Register" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row justify-content-center">
        <div class="col-md-6">
            <div class="card">
                <div class="card-header"><h4>Create Account</h4></div>

                <div class="card-body">

                    <asp:Label ID="lblMessage" runat="server"
                        CssClass="alert" Visible="false"></asp:Label>

                    <div class="mb-3">
                        <label>Username</label>
                        <asp:TextBox ID="txtUsername" runat="server"
                            CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="mb-3">
                        <label>Password</label>
                        <asp:TextBox ID="txtPassword" runat="server"
                            TextMode="Password" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="mb-3">
                        <label>Email</label>
                        <asp:TextBox ID="txtEmail" runat="server"
                            CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="mb-3">
                        <label>Role</label>
                        <asp:DropDownList ID="ddlRole" runat="server"
                            CssClass="form-control" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlRole_SelectedIndexChanged">
                            <asp:ListItem Value="">Select Role</asp:ListItem>
                            <asp:ListItem Value="JobSeeker">Job Seeker</asp:ListItem>
                            <asp:ListItem Value="Employer">Employer</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <!-- JobSeeker fields -->
                    <asp:Panel ID="pnlJobSeeker" runat="server" Visible="false">
                        <div class="mb-3">
                            <label>Full Name</label>
                            <asp:TextBox ID="txtFullName" runat="server"
                                CssClass="form-control"></asp:TextBox>
                        </div>
                    </asp:Panel>

                    <!-- Employer fields -->
                    <asp:Panel ID="pnlEmployer" runat="server" Visible="false">
                        <div class="mb-3">
                            <label>Company Name</label>
                            <asp:TextBox ID="txtCompanyName" runat="server"
                                CssClass="form-control"></asp:TextBox>
                        </div>
                    </asp:Panel>

                    <asp:Button ID="btnRegister" runat="server"
                        Text="Register" CssClass="btn btn-primary"
                        OnClick="btnRegister_Click" />

                </div>
            </div>
        </div>
    </div>
</asp:Content>
