using System;
using System.Drawing;
using System.Web.UI;
using System.IO;
using myDLL;

namespace myEFrom.App_Control.upload
{
    public partial class person_sign_upload : PageBase
    {



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                imgSaveOnly.Attributes.Add("onMouseOver", "src='../../images/controls/Upload2.jpg'");
                imgSaveOnly.Attributes.Add("onMouseOut", "src='../../images/controls/Upload.jpg'");
                if (Request.QueryString["person_code"] != null)
                {
                    ViewState["person_code"] = Request.QueryString["person_code"].ToString();
                }
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
                cefUser objEfUser = new cefUser();
                //var extention = System.IO.Path.GetExtension(FileUpload1.FileName);
                //var strFilename = System.IO.Path.GetFileName(FileUpload1.FileName);
                var fileSize = FileUpload1.FileBytes;
                if (fileSize.Length <= (1024 * 1024 * 1))
                {
                    if (FileUpload1.PostedFile.ContentType.IndexOf("image", StringComparison.Ordinal) != -1)
                    {
                        if (objEfUser.SP_USER_SIGN_UPDATE(ViewState["person_code"].ToString(), FileUpload1.FileBytes))
                        {
                            string strScript = "alert('บันทึกข้อมูลสมบูรณ์');ClosePopUp('" + ViewState["show"] + "');";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "UploadFilePage", strScript, true);
                        }
                    }
                    else
                    {
                        MsgBox("ไฟล์ที่เลือกไม่ใช่รูปภาพ โปรดตรวจสอบ");                        
                    }
                }
                else
                {
                    MsgBox("ไฟล์มีขนาดใหญ่กว่า 1 MB โปรดตรวจสอบ");
                }
            }
        }
    }
}
