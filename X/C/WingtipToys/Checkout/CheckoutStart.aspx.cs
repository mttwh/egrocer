using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WingtipToys.Models;

namespace WingtipToys.Checkout
{
    public partial class CheckoutStart : System.Web.UI.Page
    {
        public List<CartItem> prods = new List<CartItem>{};

        public void Page_Load(object sender, EventArgs e)
        {
            //pull out session objects
            var total = Session["payment_amt"].ToString();
            string names = Session["product_names"].ToString();
            string prices = Session["product_prices"].ToString();
            string quantities = Session["product_quantities"].ToString();
            LabelOrderTotal.DataBind();

            List<String> productNames = names.Split(' ').ToList();
            List<String> productPrices = prices.Split(' ').ToList();
            List<String> productQuantities = quantities.Split(' ').ToList();
            
            foreach(var i in productNames)
            {
                System.Diagnostics.Debug.WriteLine(i);
            }
            
        }
    }
}