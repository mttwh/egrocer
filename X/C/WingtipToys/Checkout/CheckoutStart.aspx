<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CheckoutStart.aspx.cs" Inherits="WingtipToys.Checkout.CheckoutStart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Order Summary</h1>
     

    <table class="table table-striped table-bordered">
        <tr>
            <th>ProductvName</th>
            <th>Product Quantity</th>
            <th>Product Price</th>
        </tr>
        <% for (int i = 0; i < productInfoList.Count; i++) { %>
            <tr><td><%= productInfoList[i][0] %></td>
                <td><%= productInfoList[i][1] %></td>
                <td><%= productInfoList[i][2] %></td></tr>
        <% } %>
    </table>
    <div>
        <p></p>
        <strong>
            <asp:Label ID="LabelOrderTotalText" runat="server" Text="Order Total: "></asp:Label>
            <asp:Label ID="LabelOrderTotal" runat="server" Text='<%# Session["payment_amt"].ToString() %>'></asp:Label>
        </strong> 
    </div>
    
</asp:Content>
