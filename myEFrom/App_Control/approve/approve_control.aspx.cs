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

namespace myEFrom.App_Control.approve
{
    public partial class approve_control : PageBase
    {

        protected void Page_Init(object sender, EventArgs e)
        {
            base.PermissionURL = "~/App_Control/approve/approve_list.aspx";
        }
        
        protected void Page_Load(object sender, System.EventArgs e)
        {
            lblError.Text = "";
            if (!IsPostBack)
            {
                imgSaveOnly.Attributes.Add("onMouseOver", "src='../../images/controls/save2.jpg'");
                imgSaveOnly.Attributes.Add("onMouseOut", "src='../../images/controls/save.jpg'");

                #region set QueryString
                if (Request.QueryString["approve_code"] != null)
                {
                    ViewState["approve_code"] = Request.QueryString["approve_code"].ToString();
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
                    ViewState["page"] = Request.QueryString["page"];
                    txtapprove_code.ReadOnly = true;
                    txtapprove_code.CssClass = "textboxdis";
                }

                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    setData();
                    txtapprove_code.ReadOnly = true;
                    txtapprove_code.CssClass = "textboxdis";                  
                }

                #endregion
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
            int intapprove_code, intapprove_level= 0;
            string strapprove_name = string.Empty,
                strUserName = string.Empty,
                strScript = string.Empty;
            cefApprove objefApprove = new cefApprove();
            DataSet ds = new DataSet();
            try
            {
                #region set Data
                intapprove_code = Helper.CInt(txtapprove_code.Text);
                strapprove_name = txtapprove_name.Text;
                intapprove_level = Helper.CInt(txtapprove_level.Value);
                strUserName = Session["username"].ToString();
                #endregion
                if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    blnResult = objefApprove.SP_APPROVE_UPD(intapprove_code, strapprove_name, intapprove_level, strUserName);
                }
                else
                {
                    #region insert
                    if (objefApprove.SP_APPROVE_INS(ref intapprove_code, strapprove_name, intapprove_level, strUserName))
                    {
                        ViewState["approve_code"] = intapprove_code;
                        blnResult = true;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate") && ex.Message.Contains("IX_approve_name"))
                {
                    strScript = "alert(\"ไม่สามารถบันทึกข้อมูล เนื่องจากข้อมูลรายละเอียดระดับการอนุมัติ : " + strapprove_name.Trim() + "  ซ้ำ\");\n";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "frMainPage", strScript, true);
                }
                else if (ex.Message.Contains("duplicate") && ex.Message.Contains("IX_approve_level"))
                {
                    strScript = "alert(\"ไม่สามารถบันทึกข้อมูล เนื่องจากข้อมูลลำดับการอนุมัติ : " + intapprove_level.ToString() + "  ซ้ำ\");\n";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "frMainPage", strScript, true);
                }
                else
                {
                    lblError.Text = ex.Message.ToString();
                }
            }
            finally
            {
                objefApprove.Dispose();
            }
            return blnResult;
        }

        private void imgSaveOnly_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (saveData())
            {
                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    txtapprove_code.Text = string.Empty;
                    txtapprove_name.Text = string.Empty;
                    txtapprove_code.Focus();
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
            cefApprove objECefDoctype = new cefApprove();
            DataTable dt;
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strapprove_code = string.Empty,
                strapprove_name = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty,
                strCreatedDate = string.Empty,
                strUpdatedDate = string.Empty,
                strapprove_level = "0";
            try
            {
                strCriteria = " and approve_code = '" + ViewState["approve_code"].ToString() + "' ";
                dt = objECefDoctype.SP_APPROVE_SEL(strCriteria);
                if (dt.Rows.Count > 0)
                {
                    #region get Data
                    strapprove_code = dt.Rows[0]["approve_code"].ToString();
                    strapprove_name = dt.Rows[0]["approve_name"].ToString();
                    strapprove_level = dt.Rows[0]["approve_level"].ToString();
                    strCreatedBy = dt.Rows[0]["c_created_by"].ToString();
                    strUpdatedBy = dt.Rows[0]["c_updated_by"].ToString();
                    strCreatedDate = dt.Rows[0]["d_created_date"].ToString();
                    strUpdatedDate = dt.Rows[0]["d_updated_date"].ToString();
                    #endregion

                    #region set Control
                    txtapprove_code.Text = strapprove_code;
                    txtapprove_name.Text = strapprove_name;
                    txtapprove_level.Value = strapprove_level;

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