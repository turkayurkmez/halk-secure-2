<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="About.aspx.cs" Inherits="About" ValidateRequest="true"  %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Your application description page.</h3>
    <p>Use this area to provide additional information.</p>

    <asp:TextBox ID="TextBoxComment" runat="server" TextMode="MultiLine" Height="188px" Width="463px"></asp:TextBox>
    <asp:Button ID="ButtonAddComment" runat="server" Text="Yorum Ekle" OnClick="ButtonAddComment_Click" />
    <asp:Label ID="LabelComments" runat="server" Text=""></asp:Label>
</asp:Content>
