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

namespace myEFrom.App_Control.doctype
{
    public partial class doctype_control : PageBase
    {

        protected void Page_Init(object sender, EventArgs e)
        {
            base.PermissionURL = "~/App_Control/doctype/doctype_list.aspx";
        }

        
        protected void Page_Load(object sender, System.EventArgs e)
        {
            lblError.Text = "";
            if (!IsPostBack)
            {
                imgSaveOnly.Attributes.Add("onMouseOver", "src='../../images/controls/save2.jpg'");
                imgSaveOnly.Attributes.Add("onMouseOut", "src='../../images/controls/save.jpg'");

                #region set QueryString
                if (Request.QueryString["ef_doctype_code"] != null)
                {
                    ViewState["ef_doctype_code"] = Request.QueryString["ef_doctype_code"].ToString();
                }
                if (Request.QueryString["page"] != null)
                {
                    ViewState["page"] = Request.QueryString["page"].ToString();
                }
                if (Request.QueryString["mode"] != null)
                {
                    ViewState["mode"] = Request.QueryString["mode"].ToString();
                }
                if (Request.QueryString["PageStatus"] != null)
                {
                    ViewState["PageStatus"] = Request.QueryString["PageStatus"].ToString();
                }

                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    Session["menupopup_name"] = "เพิ่มข้อมูลประเภทเอกสาร";
                    ViewState["page"] = Request.QueryString["page"];
                }

                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    Session["menupopup_name"] = "แก้ไขข้อมูลประเภทเอกสาร";
                    setData();
                    txtdoctype_code.ReadOnly = true;
                    txtdoctype_code.CssClass = "textboxdis";
                    //if (ViewState["PageStatus"] != null)
                    //{
                    //    if (ViewState["PageStatus"].ToString().ToLower().Equals("save"))
                    //    {
                    //        string strScript1 =
                    //            "self.opener.document.forms[0].ctl00$ASPxRoundPanel1$ContentPlaceHolder2$txthpage.value=" + ViewState["page"].ToString() + ";\n" +
                    //            "self.opener.document.forms[0].submit();\n" +
                    //            "self.focus();\n";
                    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "frMainPage", strScript1, true);
                    //    }
                    //}
                }

                #endregion
                //imgClose.Attributes.Add("onclick", "ClosePopUp('1');return false;");
            }
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.imgSaveOnly.Click += new System.Web.UI.ImageClickEventHandler(this.imgSaveOnly_Click);
            //this.imgSave.Click += new System.Web.UI.ImageClickEventHandler(this.imgSave_Click);

        }
        #endregion

        private bool saveData()
        {
            bool blnResult = false;
            int intdoctype_code;
            string strdoctype_name = string.Empty,
                strUserName = string.Empty,
                strScript = string.Empty;
            cefDoctype objefDoctype = new cefDoctype();
            DataSet ds = new DataSet();
            try
            {
                #region set Data
                intdoctype_code = Helper.CInt(txtdoctype_code.Text);
                strdoctype_name = txtdoctype_name.Text;
                strUserName = Session["username"].ToString();
                #endregion
                if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    blnResult = objefDoctype.SP_DOCTYPE_UPD(intdoctype_code, strdoctype_name, strUserName);
                }
                else
                {
                    #region insert
                    if (objefDoctype.SP_DOCTYPE_INS(ref intdoctype_code, strdoctype_name, strUserName))
                    {
                        ViewState["ef_doctype_code"] = intdoctype_code;
                        blnResult = true;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate") && ex.Message.Contains("IX_ef_doctype_name"))
                {
                    strScript = "alert(\"ไม่สามารถแก้ไขข้อมูล เนื่องจากข้อมูล " + strdoctype_name.Trim() + "  ซ้ำ\");\n";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "frMainPage", strScript, true);
                }
                else
                {
                    lblError.Text = ex.Message.ToString();
                }
            }
            finally
            {
                objefDoctype.Dispose();
            }
            return blnResult;
        }

        private void imgSaveOnly_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (saveData())
            {
                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    txtdoctype_code.Text = string.Empty;
                    txtdoctype_name.Text = string.Empty;
                    txtdoctype_code.Focus();
                    string strScript1 = "RefreshMain('" + ViewState["page"].ToString() + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
                }
                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    string strScript1 = "ClosePopUpListPost('" + ViewState["page"].ToString() + "','1');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
                }
                MsgBox("บันทึกข้อมูลสมบูรณ์");
            }
        }

        private void setData()
        {
            cefDoctype objECefDoctype = new cefDoctype();
            DataTable dt;
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strdoctype_code = string.Empty,
                strdoctype_name = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty,
                strCreatedDate = string.Empty,
                strUpdatedDate = string.Empty;
            try
            {
                strCriteria = " and ef_doctype_code = '" + ViewState["ef_doctype_code"].ToString() + "' ";
                dt = objECefDoctype.SP_DOCTYPE_SEL(strCriteria);
                if (dt.Rows.Count > 0)
                {
                    #region get Data
                    strdoctype_code = dt.Rows[0]["ef_doctype_code"].ToString();
                    strdoctype_name = dt.Rows[0]["ef_doctype_name"].ToString();
                    strCreatedBy = dt.Rows[0]["c_created_by"].ToString();
                    strUpdatedBy = dt.Rows[0]["c_updated_by"].ToString();
                    strCreatedDate = dt.Rows[0]["d_created_date"].ToString();
                    strUpdatedDate = dt.Rows[0]["d_updated_date"].ToString();
                    #endregion

                    #region set Control
                    txtdoctype_code.Text = strdoctype_code;
                    txtdoctype_name.Text = strdoctype_name;

                    txtUpdatedBy.Text = strUpdatedBy;
                    txtUpdatedDate.Text = strUpdatedDate;
                    #endregion
                }

            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }


    }
}