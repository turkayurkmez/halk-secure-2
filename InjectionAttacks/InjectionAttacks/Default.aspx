<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>ASP.NET</h1>
        <p class="lead">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript.</p>
        <p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-4">

            <asp:TextBox ID="TextBoxUserName" runat="server"  ></asp:TextBox><br />
            <asp:TextBox ID="TextBoxPassword" TextMode="Password" runat="server" ></asp:TextBox><br />

            <asp:Button ID="ButtonLogin" runat="server" Text="Giriş" OnClick="ButtonLogin_Click" />
            <asp:Label ID="LabelResult" runat="server" Text="..."></asp:Label>
        </div>
       
    </div>
</asp:Content>
