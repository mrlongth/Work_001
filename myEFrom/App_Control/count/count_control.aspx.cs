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

namespace myEFrom.App_Control.count
{
    public partial class count_control : PageBase
    {

        protected void Page_Init(object sender, EventArgs e)
        {
            base.PermissionURL = "~/App_Control/count/count_list.aspx";
        }

        
        protected void Page_Load(object sender, System.EventArgs e)
        {
            lblError.Text = "";
            if (!IsPostBack)
            {
                imgSaveOnly.Attributes.Add("onMouseOver", "src='../../images/controls/save2.jpg'");
                imgSaveOnly.Attributes.Add("onMouseOut", "src='../../images/controls/save.jpg'");

                #region set QueryString
                if (Request.QueryString["count_id"] != null)
                {
                    ViewState["count_id"] = Request.QueryString["count_id"].ToString();
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
                    Session["menupopup_name"] = "เพิ่มข้อมูลหน่วยนับ";
                    ViewState["page"] = Request.QueryString["page"];
                }

                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    Session["menupopup_name"] = "แก้ไขข้อมูลหน่วยนับ";
                    setData();
                    txtcount_id.ReadOnly = true;
                    txtcount_id.CssClass = "textboxdis";
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
            int intcount_id;
            string strcount_name = string.Empty,
                strUserName = string.Empty,
                strScript = string.Empty;
            c3dCount obj3dCount = new c3dCount();
            try
            {
                #region set Data
                intcount_id = Helper.CInt(txtcount_id.Text);
                strcount_name = txtcount_name.Text;
                strUserName = Session["username"].ToString();
                #endregion
                if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    blnResult = obj3dCount.SP_COUNT_UPD(intcount_id, strcount_name, strUserName);
                }
                else
                {
                    #region insert
                    if (obj3dCount.SP_COUNT_INS(ref intcount_id, strcount_name, strUserName))
                    {
                        ViewState["count_id"] = intcount_id;
                        blnResult = true;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate") && ex.Message.Contains("IX_count_name"))
                {
                    strScript = "alert(\"ไม่สามารถแก้ไขข้อมูล เนื่องจากข้อมูล " + strcount_name.Trim() + "  ซ้ำ\");\n";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "frMainPage", strScript, true);
                }
                else
                {
                    lblError.Text = ex.Message.ToString();
                }
            }
            finally
            {
                obj3dCount.Dispose();
            }
            return blnResult;
        }

        private void imgSaveOnly_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (saveData())
            {
                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    txtcount_id.Text = string.Empty;
                    txtcount_name.Text = string.Empty;
                    txtcount_id.Focus();
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
            c3dCount objECefDoctype = new c3dCount();
            DataTable dt;
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strcount_id = string.Empty,
                strcount_name = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty,
                strCreatedDate = string.Empty,
                strUpdatedDate = string.Empty;
            try
            {
                strCriteria = " and count_id = '" + ViewState["count_id"].ToString() + "' ";
                dt = objECefDoctype.SP_COUNT_SEL(strCriteria);
                if (dt.Rows.Count > 0)
                {
                    #region get Data
                    strcount_id = dt.Rows[0]["count_id"].ToString();
                    strcount_name = dt.Rows[0]["count_name"].ToString();
                    strCreatedBy = dt.Rows[0]["c_created_by"].ToString();
                    strUpdatedBy = dt.Rows[0]["c_updated_by"].ToString();
                    strCreatedDate = dt.Rows[0]["d_created_date"].ToString();
                    strUpdatedDate = dt.Rows[0]["d_updated_date"].ToString();
                    #endregion

                    #region set Control
                    txtcount_id.Text = strcount_id;
                    txtcount_name.Text = strcount_name;

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