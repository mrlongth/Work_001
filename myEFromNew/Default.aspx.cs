using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;


namespace myEFrom
{
    public partial class Default : Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("http://www.eform.mju.ac.th");
        }
    }
}
