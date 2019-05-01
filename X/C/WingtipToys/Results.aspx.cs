using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WingtipToys
{
    public partial class Results : System.Web.UI.Page
    {
        public List<String> prodNamesList = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            List<string> searchedProds = new List<string>();

            string prodNames = Session["searched_names"].ToString();

            prodNamesList = prodNames.Split(',').ToList();
            System.Diagnostics.Debug.WriteLine(prodNames);

        }
    }
}