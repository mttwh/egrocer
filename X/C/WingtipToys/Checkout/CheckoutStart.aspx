<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CheckoutStart.aspx.cs" Inherits="WingtipToys.Checkout.CheckoutStart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Order Summary</h1>
     <asp:Label ID="LabelOrderTotal" runat="server" Text='<%# Session["payment_amt"].ToString() %>'></asp:Label>
</asp:Content>
