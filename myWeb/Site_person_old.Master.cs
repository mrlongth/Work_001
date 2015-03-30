using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Threading;
using myDLL;
 

namespace myWeb
{
    public partial class Site_person : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th-TH");
            if (!IsPostBack)
            {
                try
                {
                    UserLabel.Text = Session["PersonFullName"].ToString();
                }
                catch
                {
                    Response.Redirect("~/Default.aspx");
                }     
            }
        }


        public void MsgBox(string strMessage)
        {
            string strScript = string.Empty;
            strScript = "alert('" + strMessage + "');";
            ScriptManager.RegisterClientScriptBlock(updatePanel1, updatePanel1.GetType(), "MessageBox", Helper.ReplaceScript(strScript), true);
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            //Session["person_username"] = "0";
            Response.Redirect("~/Default.aspx");
        }
     
    }
}
