using System;

namespace myEFrom
{
    public partial class Site_main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Abandon();              
            }
        }


    }
}
