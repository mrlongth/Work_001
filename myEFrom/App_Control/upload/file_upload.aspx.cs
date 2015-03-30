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
using myDLL;

namespace myEFrom.App_Control.upload
{
    public partial class file_upload : PageBase
    {



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                imgSaveOnly.Attributes.Add("onMouseOver", "src='../../images/controls/Upload2.jpg'");
                imgSaveOnly.Attributes.Add("onMouseOut", "src='../../images/controls/Upload.jpg'");
                if (Request.QueryString["ctrl_from"] != null)
                {
                    ViewState["ctrl_from"] = Request.QueryString["ctrl_from"].ToString();
                }
                if (Request.QueryString["ctrl1"] != null)
                {
                    ViewState["ctrl1"] = Request.QueryString["ctrl1"].ToString();
                }
                if (Request.QueryString["ctrl2"] != null)
                {
                    ViewState["ctrl2"] = Request.QueryString["ctrl2"].ToString();
                }
                if (Request.QueryString["open_attach_id"] != null)
                {
                    ViewState["open_attach_id"] = Request.QueryString["open_attach_id"].ToString();
                }

                if (Request.QueryString["show"] != null)
                {
                    ViewState["show"] = Request.QueryString["show"].ToString();
                }
                else
                {
                    ViewState["show"] = "1";
                }

            }
        }

        protected void imgSaveOnly_Click(object sender, ImageClickEventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                var extention = System.IO.Path.GetExtension(FileUpload1.FileName);
                var strFilename = System.IO.Path.GetFileName(FileUpload1.FileName);
                var fileSize = FileUpload1.FileBytes;
                if (fileSize.Length <= (1024 * 1024 * 2))
                {
                    string strUrlFilename = "~/temp/attach_" + DateTime.Now.ToString("yyyyMMddHHmmss") + extention;
                    FileUpload1.SaveAs(MapPath(strUrlFilename));
                    string strScript = "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) +
                                    "'].document.getElementById('" + ViewState["ctrl1"] + "').value='" + strUrlFilename +
                                    "';\n " +
                                    "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) +
                                    "'].document.getElementById('" + ViewState["ctrl2"] + "').value='" + strFilename +
                                    "';\n" + "ClosePopUp('" + ViewState["show"] + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "UploadFilePage", strScript, true);
                }
                else
                {
                    MsgBox("ไฟล์มีขนาดใหญ่กว่า 2 MB โปรดตรวจสอบ");
                }
            }
        }
        public void MsgBox(string strMessage)
        {
            string strScript = string.Empty;
            strScript = "alert('" + strMessage + "');";
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "MessageBox", strScript, true);
        }



    }
}
