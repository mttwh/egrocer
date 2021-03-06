﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WingtipToys.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace WingtipToys.Checkout
{
    public partial class CheckoutStart : System.Web.UI.Page
    {
        public List<String> productNames = new List<string>();
        public List<String> productPrices = new List<string>();
        public List<String> productQuantities = new List<string>();
        public List<List<string>> productInfoList = new List<List<string>>();

        public void Page_Load(object sender, EventArgs e)
        {
            //pull out session objects
            var total = Session["payment_amt"].ToString();
            string names = Session["product_names"].ToString();
            string prices = Session["product_prices"].ToString();
            string quantities = Session["product_quantities"].ToString();
            LabelOrderTotal.DataBind();

            productNames = names.Split(',').ToList();
            productPrices = prices.Split(',').ToList();
            productQuantities = quantities.Split(',').ToList();


            for (int i = 0; i < productNames.Count; i++)
            {
                List<String> tempList = new List<string>();
                tempList.Add(productNames[i]);
                tempList.Add(productQuantities[i]);
                tempList.Add(productPrices[i]);
                productInfoList.Add(tempList);
            }


            string messageBody = "\n\nOrder Summary\n";
            for (int i = 0; i < productInfoList.Count - 1; i++)
            {

                string tempString = productInfoList[i][1] + " " + productInfoList[i][0] + "(s) at $" + productInfoList[i][2] + " each.\n";
                System.Diagnostics.Debug.WriteLine(tempString);
                messageBody = messageBody + tempString;



            }
            messageBody = messageBody + "Order total: $" + total + ".";
            string currentDate = DateTime.Now.ToString("MM/dd/yyyy");
            string currentTime = DateTime.Now.ToString("h:mm tt");

            messageBody = messageBody + "                                    You placed your oder on " + currentDate.ToString() + " at " + currentTime + "\n";
            messageBody = messageBody + "\nPlease bring your receipt with you (printed or on phone) to pay for your order onsite." +
                " As for now, we don't take online payments. ";

            //send user order confirmation email

            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();

            string username = System.Web.HttpContext.Current.User.Identity.Name;
            System.Diagnostics.Debug.WriteLine(username);
            ApplicationUser user = manager.FindByName(username);
            string code = manager.GeneratePasswordResetToken(user.Id);
            string callbackUrl = IdentityHelper.GetResetPasswordRedirectUrl(code, Request);
            manager.SendEmail(user.Id, "Order Confirmation", messageBody);


            System.Diagnostics.Debug.WriteLine(messageBody);
        }
    }
}
