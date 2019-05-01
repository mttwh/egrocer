<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="WingtipToys.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Your contact page.</h3>
    <address>
        1200 Baltimore Pike,<br />
        Springfield, PA 19064<br />
        <abbr title="Phone">P:</abbr>
        (800) 932-4600
    </address>

    <address>
        <strong>Support:</strong>   <a href="mailto:Support@example.com">wqk5094@psu.edu</a><br />
        <strong>Marketing:</strong> <a href="mailto:Marketing@example.com">mjw5682@psu.edu</a>
    </address>
</asp:Content>
