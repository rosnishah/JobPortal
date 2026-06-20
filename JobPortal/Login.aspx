<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="JobPortal.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row justify-content-center mt-5">
        <div class="col-md-5">

            <div class="card shadow-sm">
                <div class="card-header text-center bg-primary text-white">
                    <h4 class="mb-0">Login to WorkOwl</h4>
                </div>

                <div class="card-body">

                    <!-- Error Message -->
                    <asp:Label ID="lblMessage" runat="server"
                        CssClass="alert alert-danger d-block text-center"
                        Visible="false"></asp:Label>

                    <!-- Username -->
                    <div class="mb-3">
                        <label class="form-label">Username</label>
                        <asp:TextBox ID="txtUsername" runat="server"
                            CssClass="form-control" Placeholder="Enter Username"></asp:TextBox>
                    </div>

                    <!-- Password -->
                    <div class="mb-3">
                        <label class="form-label">Password</label>
                        <asp:TextBox ID="txtPassword" runat="server"
                            TextMode="Password" CssClass="form-control"
                            Placeholder="Enter Password"></asp:TextBox>
                    </div>

                    <!-- Button -->
                    <div class="d-grid mb-3">
                        <asp:Button ID="btnLogin" runat="server" Text="Login"
                            CssClass="btn btn-primary" OnClick="btnLogin_Click" />
                    </div>

                    <div class="text-center">
                        <a href="Register.aspx">Don't have an account? Register here</a>
                    </div>

                </div>
            </div>

        </div>
    </div>

</asp:Content>
