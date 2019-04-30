using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WingtipToys.Models;
using WingtipToys.Logic;
using System.Data.SqlClient;

namespace WingtipToys.Admin
{
    public partial class AdminPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string productAction = Request.QueryString["ProductAction"];
            if (productAction == "add")
            {
                LabelAddStatus.Text = "Product added!";
            }

            if (productAction == "remove")
            {
                LabelRemoveStatus.Text = "Product removed!";
            }
        }

        protected void AddProductButton_Click(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                Page.Validate("Add");
            }

            Boolean fileOK = false;
            String path = Server.MapPath("~/Catalog/Images/");
            if (ProductImage.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(ProductImage.FileName).ToLower();
                String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOK = true;
                    }
                }
            }

            if (fileOK)
            {
                try
                {
                    // Save to Images folder.
                    ProductImage.PostedFile.SaveAs(path + ProductImage.FileName);
                    // Save to Images/Thumbs folder.
                    ProductImage.PostedFile.SaveAs(path + "Thumbs/" + ProductImage.FileName);
                }
                catch (Exception ex)
                {
                    LabelAddStatus.Text = ex.Message;
                }

                // Add product data to DB.
                AddProducts products = new AddProducts();
                bool addSuccess = products.AddProduct(AddProductName.Text, AddProductDescription.Text,
                    AddProductPrice.Text, DropDownAddCategory.SelectedValue, ProductImage.FileName);
                if (addSuccess)
                {
                    // Reload the page.
                    string pageUrl = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.Count() - Request.Url.Query.Count());
                    Response.Redirect(pageUrl + "?ProductAction=add");
                }
                else
                {
                    LabelAddStatus.Text = "Unable to add new product to database.";
                }
            }
            else
            {
                LabelAddStatus.Text = "Unable to accept file type.";
            }
        }

        public IQueryable GetCategories()
        {
            var _db = new WingtipToys.Models.ProductContext();
            IQueryable query = _db.Categories;
            return query;
        }

        public IQueryable GetProducts()
        {
            var _db = new WingtipToys.Models.ProductContext();
            IQueryable query = _db.Products;
            return query;
        }

        protected void RemoveProductButton_Click(object sender, EventArgs e)
        {
            using (var _db = new WingtipToys.Models.ProductContext())
            {
                int productId = Convert.ToInt16(DropDownRemoveProduct.SelectedValue);
                var myItem = (from c in _db.Products where c.ProductID == productId select c).FirstOrDefault();
                if (myItem != null)
                {
                    _db.Products.Remove(myItem);
                    _db.SaveChanges();

                    // Reload the page.
                    string pageUrl = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.Count() - Request.Url.Query.Count());
                    Response.Redirect(pageUrl + "?ProductAction=remove");
                }
                else
                {
                    LabelRemoveStatus.Text = "Unable to locate product.";
                }
            }
        }

        protected void UpdateButton_Click(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Update")
            {
                Page.Validate("Update");
            }
        }

        protected void updateProductName_ServerValidate(object sender, ServerValidateEventArgs e)
        {

            //String connectionString = @"Data Source=DESKTOP-S3P3TBM\SQLEXPRESS;Initial Catalog=wingtiptoys.mdf;Integrated Security=True";
            string connectionString = "Data Source=MYLAPTOP\\SQLEXPRESS;Database=wingtiptoys.mdf;Integrated Security=True";
            string queryString = "SELECT * FROM dbo.Products WHERE ProductName = '" + DropDownUpdateProductName.SelectedItem.Text + "'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    if (reader.Read())
                    {
                        //update product record in database
                        e.IsValid = true;
                        System.Diagnostics.Debug.WriteLine(e.Value.ToString() + " Exists");
                        //update product in database
                        using (var _db = new WingtipToys.Models.ProductContext())
                        {
                            var myItem = (from c in _db.Products where c.ProductName == DropDownUpdateProductName.SelectedItem.Text select c).FirstOrDefault();
                            if (myItem != null)
                            {
                                System.Diagnostics.Debug.WriteLine(myItem.Description);
                                //remove product from database
                                _db.Products.Remove(myItem);
                                _db.SaveChanges();

                                // Add product data to DB.
                                AddProducts products = new AddProducts();
                                var product = new WingtipToys.Models.Product();
                                //update name, description, and price, but keep same category id and image file path for now
                                bool addSuccess = products.AddProduct(DropDownUpdateProductName.SelectedItem.Text, UpdateProductDescription.Text,
                                    UpdateProductPrice.Text, DropDownUpdateCategory.SelectedValue, myItem.ImagePath);
                                if (addSuccess)
                                {
                                    // Reload the page.
                                    string pageUrl = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.Count() - Request.Url.Query.Count());
                                    Response.Redirect(pageUrl + "?ProductAction=add");
                                }
                                else
                                {
                                    System.Diagnostics.Debug.WriteLine("Unable to add new product to database.");
                                }

                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine("Unable to locate product.");
                            }

                        }
                    }
                    else
                    {
                        //Throw validation error because product does not exist and cannot be updated
                        e.IsValid = false;
                        System.Diagnostics.Debug.WriteLine(e.Value.ToString() + " Does not exist");
                    }
                }
                finally
                {
                    reader.Close();
                }
            }


        }
    }
}