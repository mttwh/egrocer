using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WingtipToys
{
    public partial class Results : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["srch"]))
            {
                //perform search and display results
                var searchText = Request.QueryString["srch"];

                string connectionString = "Data Source=MYLAPTOP\\SQLEXPRESS;Database=wingtiptoys.mdf;Integrated Security=True";
                string queryString = "SELECT * FROM dbo.Products WHERE ProductName = '" + searchText + "'";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    try
                    {
                        while(reader.Read())
                        {
                            //deleteLabel.Text = queryString;
                            deleteLabel.Text = String.Format("{0}, {1}, {2}", reader[0], reader[1], reader[2]);
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
}