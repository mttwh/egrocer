<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="WingtipToys.Admin.AdminPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Administration</h1>
    <hr />

    <div class="theRow">
        <div class="addProductTable">
            <h3>Add Product:</h3>
            <table>
                <tr>
                    <td><asp:Label ID="LabelAddCategory" runat="server">Category:</asp:Label></td>
                    <td>
                        <asp:DropDownList ID="DropDownAddCategory" runat="server" 
                            ItemType="WingtipToys.Models.Category" 
                            SelectMethod="GetCategories" DataTextField="CategoryName" 
                            DataValueField="CategoryID" >
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td><asp:Label ID="LabelAddName" runat="server">Name:</asp:Label></td>
                    <td>
                        <asp:TextBox ID="AddProductName" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="* Product name required." ValidationGroup="Add" ControlToValidate="AddProductName" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td><asp:Label ID="LabelAddDescription" runat="server">Description:</asp:Label></td>
                    <td>
                        <asp:TextBox ID="AddProductDescription" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Text="* Description required." ValidationGroup="Add" ControlToValidate="AddProductDescription" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td><asp:Label ID="LabelAddPrice" runat="server">Price:</asp:Label></td>
                    <td>
                        <asp:TextBox ID="AddProductPrice" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Text="* Price required." ValidationGroup="Add" ControlToValidate="AddProductPrice" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Text="* Must be a valid price without $." ValidationGroup="Add" ControlToValidate="AddProductPrice" SetFocusOnError="True" Display="Dynamic" ValidationExpression="^[0-9]*(\.)?[0-9]?[0-9]?$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td><asp:Label ID="LabelAddImageFile" runat="server">Image File:</asp:Label></td>
                    <td>
                        <asp:FileUpload ID="ProductImage" runat="server" />
                    </td>
                </tr>
            </table>
            <p></p>
            <p></p>
            <asp:Button ID="AddProductButton" runat="server" Text="Add Product" OnCommand="AddProductButton_Click" CommandName="Add" ValidationGroup="Add"  CausesValidation="true"/>
            <asp:Label ID="LabelAddStatus" runat="server" Text=""></asp:Label>
        </div>


        <!-- Update Product Table -->
        <div class="updateProductTable">
            <h3>Update Product:</h3>
            <table>
                <tr>
                    <td><asp:Label ID="LabelUpdateCategory" runat="server">Category:</asp:Label></td>
                    <td>
                        <asp:DropDownList ID="DropDownUpdateCategory" runat="server" 
                            ItemType="WingtipToys.Models.Category" 
                            SelectMethod="GetCategories" DataTextField="CategoryName" 
                            DataValueField="CategoryID" >
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Product Name: </td>
                    <td>
                        <asp:DropDownList ID="DropDownUpdateProductName" runat="server" ItemType="WingtipToys.Models.Product" 
                            SelectMethod="GetProducts" AppendDataBoundItems="true" 
                            DataTextField="ProductName" DataValueField="ProductID" >
                        </asp:DropDownList>
                        <asp:CustomValidator ID="UpdateProductValidator" runat="server"  ControloValidate="DropDownUpdateProductName" ValidationGroup="Update" OnServerValidate="updateProductName_ServerValidate" ErrorMessage="Product with this name does not exist!" SetFocusOnError="true" Display="Dynamic"></asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td><asp:Label ID="LabelUpdateDescription" runat="server">Description:</asp:Label></td>
                    <td>
                        <asp:TextBox ID="UpdateProductDescription" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Text="* Description required." ValidationGroup="Update" ControlToValidate="UpdateProductDescription" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td><asp:Label ID="LabelUpdatePrice" runat="server">Price:</asp:Label></td>
                    <td>
                        <asp:TextBox ID="UpdateProductPrice" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Text="* Price required." ValidationGroup="Update" ControlToValidate="UpdateProductPrice" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationGroup="Update" Text="* Must be a valid price without $." ControlToValidate="UpdateProductPrice" SetFocusOnError="True" Display="Dynamic" ValidationExpression="^[0-9]*(\.)?[0-9]?[0-9]?$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td><asp:Label ID="LabelAddImageFile0" runat="server">Image File:</asp:Label></td>
                    <td>
                        <asp:FileUpload ID="ProductImage0" runat="server" />
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Text="* Image required." ValidationGroup="Update" ControlToValidate="ProductImage0" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
            <p></p>
            <p>
                <asp:Button ID="UpdateButton" runat="server" ValidationGroup="Update" OnServerValidate="updateProductName_ServerValidate" CommandName="Update" OnCommand="UpdateButton_Click" CausesValidation="true" 
                    Text="Update Product" />
            </p>
        </div>
    </div>
    
    <div class="removeProductTable">
        <p class="breakText">.</p>
        <h3 class="removeHeader">Remove Product:</h3>
        <table>
            <tr>
                <td><asp:Label ID="LabelRemoveProduct" runat="server">Product:</asp:Label></td>
                <td><asp:DropDownList ID="DropDownRemoveProduct" runat="server" ItemType="WingtipToys.Models.Product" 
                        SelectMethod="GetProducts" AppendDataBoundItems="true" 
                        DataTextField="ProductName" DataValueField="ProductID" >
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <p></p>
        <asp:Button ID="RemoveProductButton" runat="server" Text="Remove Product" OnClick="RemoveProductButton_Click" CausesValidation="false"/>
        <asp:Label ID="LabelRemoveStatus" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>