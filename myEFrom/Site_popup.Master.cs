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

namespace myEFrom
{
    public partial class Site_popup : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AddJavaScriptInclude();              
            //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th-TH");
        }

        protected void AddJavaScriptInclude()
        {
            HtmlGenericControl script;


            script = new HtmlGenericControl("script");
            script.Attributes["type"] = "text/javascript";
            script.Attributes["src"] = (cCommon.AbsoluteWebRoot + "js/jquery-1.7.2.min.js");
            Page.Header.Controls.Add(script);

            script = new HtmlGenericControl("script");
            script.Attributes["type"] = "text/javascript";
            script.Attributes["src"] = (cCommon.AbsoluteWebRoot + "js/jquery-ui-1.8.22.custom.js");
            Page.Header.Controls.Add(script);


            script = new HtmlGenericControl("script");
            script.Attributes["type"] = "text/javascript";
            script.Attributes["src"] = (cCommon.AbsoluteWebRoot + "scripts/form.js");
            Page.Header.Controls.Add(script);

            script = new HtmlGenericControl("script");
            script.Attributes["type"] = "text/javascript";
            script.Attributes["src"] = (cCommon.AbsoluteWebRoot + "javascript/AwNumeric.js");
            Page.Header.Controls.Add(script);

            script = new HtmlGenericControl("script");
            script.Attributes["type"] = "text/javascript";
            script.Attributes["src"] = (cCommon.AbsoluteWebRoot + "javascript/AwTextBox.js");
            Page.Header.Controls.Add(script);

        }
    
    
    }


}
