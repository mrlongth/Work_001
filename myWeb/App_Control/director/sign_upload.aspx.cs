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

namespace myWeb.App_Control.director
{
    public partial class sign_upload : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                imgSaveOnly.Attributes.Add("onMouseOver", "src='../../images/controls/Upload2.jpg'");
                imgSaveOnly.Attributes.Add("onMouseOut", "src='../../images/controls/Upload.jpg'");
                if (Request.QueryString["ctrl1"] != null)
                {
                    ViewState["ctrl1"] = Request.QueryString["ctrl1"].ToString();
                }
                if (Request.QueryString["ctrl2"] != null)
                {
                    ViewState["ctrl2"] = Request.QueryString["ctrl2"].ToString();
                }
            }
        }

        protected void imgSaveOnly_Click(object sender, ImageClickEventArgs e)
        {
            if (FileUpload1.HasFile) 
            {
                FileUpload1.SaveAs(MapPath("~/person_pic/" + FileUpload1.FileName));
                string strScript1 = "window.parent.frames['iframeShow1'].document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + FileUpload1.FileName + "';" +
                                                   "window.parent.frames['iframeShow1'].document.getElementById('" + ViewState["ctrl2"].ToString() + "').src='../../person_pic/" + FileUpload1.FileName + "';" +
                                                   "ClosePopUp('2');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
            }
        }
    }
}
