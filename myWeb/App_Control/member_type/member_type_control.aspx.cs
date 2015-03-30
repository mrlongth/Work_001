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

namespace myWeb.App_Control.member_type
{
    public partial class member_type_control : PageBase
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            //if (Session["username"] == null)
            //{
            //    string strScript = "<script language=\"javascript\">\n self.opener.document.location.href=\"../../index.aspx\";\n self.close();\n</script>\n";
            //    this.RegisterStartupScript("close", strScript);
            //    return;
            //}
            lblError.Text = "";
            if (!IsPostBack)
            {
                imgSaveOnly.Attributes.Add("onMouseOver", "src='../../images/controls/save2.jpg'");
                imgSaveOnly.Attributes.Add("onMouseOut", "src='../../images/controls/save.jpg'");

                #region set QueryString
                if (Request.QueryString["member_type_code"] != null)
                {
                    ViewState["member_type_code"] = Request.QueryString["member_type_code"].ToString();
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
                    Session["menupopup_name"] = "เพิ่มข้อมูลประเภทสมาชิก";
                    ViewState["page"] = Request.QueryString["page"];
                    txtmember_type_code.ReadOnly = false;
                    txtmember_type_code.CssClass = "textbox";
                    chkStatus.Checked = true;
                }

                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    Session["menupopup_name"] =  "แก้ไขข้อมูลประเภทสมาชิก";
                    setData();
                    txtmember_type_code.ReadOnly = true;
                    txtmember_type_code.CssClass = "textboxdis";
                    if (ViewState["PageStatus"] != null)
                    {
                        if (ViewState["PageStatus"].ToString().ToLower().Equals("save"))
                        {
                            string strScript1 =
                                "self.opener.document.forms[0].ctl00$ASPxRoundPanel1$ContentPlaceHolder2$txthpage.value=" + ViewState["page"].ToString() + ";\n" +
                                "self.opener.document.forms[0].submit();\n" +
                                "self.focus();\n";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "frMainPage", strScript1, true);
                        }
                    }
                }

                #endregion

                txtmember_type_rate.Attributes.Add("onblur", "chkDecimal(this,2,',')");

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
        }
        #endregion

        private bool saveData()
        {
            bool blnResult = false;
            bool blnDup = false;
            string strMessage = string.Empty;
            string strmember_type_code = string.Empty,
                strmember_type_name = string.Empty,
                strmember_type_rate = string.Empty,
                strcompany_rate = string.Empty,
                strActive = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty;
            string strScript = string.Empty;
            cMember_type oMember_type = new cMember_type();
            DataSet ds = new DataSet();
            try
            {
                #region set Data
                strmember_type_code = txtmember_type_code.Text.Trim();
                strmember_type_name = txtmember_type_name.Text.Trim();
                strmember_type_rate = txtmember_type_rate.Text;
                strcompany_rate = txtcompany_rate.Text;
                if (chkStatus.Checked == true)
                {
                    strActive = "Y";
                }
                else 
                {
                    strActive = "N";
                }
                strCreatedBy = Session["username"].ToString();
                strUpdatedBy = Session["username"].ToString();
                #endregion
                if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    #region edit
                    if (!blnDup)
                    {
                        if (oMember_type.SP_MEMBER_TYPE_UPD(strmember_type_code, strmember_type_name, strmember_type_rate, strcompany_rate,
                                                                                                                    strActive,strUpdatedBy, ref strMessage))
                        {
                            blnResult = true;
                        }
                        else
                        {
                            lblError.Text = strMessage.ToString();
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "frMainPage", strScript, true);
                    }
                    #endregion
                }
                else
                {
                    #region check dup
                    string strCheckDup = string.Empty;
                    strCheckDup = " and member_type_code = '" + strmember_type_code.Trim() + "' ";
                    if (!oMember_type.SP_MEMBER_TYPE_SEL(strCheckDup, ref ds, ref strMessage))
                    {
                        lblError.Text = strMessage;
                    }
                    else
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            strScript = 
                                "alert(\"ไม่สามารถเพิ่มข้อมูล เนื่องจากข้อมูล " + strmember_type_code.Trim() + " : " + strmember_type_name.Trim() + "  ซ้ำ\");\n" ;
                            blnDup = true;
                        }
                    }
                    #endregion
                    #region insert
                    if (!blnDup)
                    {
                        if (oMember_type.SP_MEMBER_TYPE_INS(strmember_type_code, strmember_type_name, strmember_type_rate, strcompany_rate,
                                                                strActive, strCreatedBy, ref strMessage))
                        {
                            ViewState["member_type_code"] = strmember_type_code;
                            blnResult = true;
                        }
                        else
                        {
                            lblError.Text = strMessage.ToString();
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "frMainPage", strScript, true);
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oMember_type.Dispose();
            }
            return blnResult;
        }

        private void imgSaveOnly_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (saveData())
            {
                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    txtmember_type_code.Text = "";
                    txtmember_type_name.Text = "";
                    txtmember_type_rate.Text = "0.00";
                    txtcompany_rate.Text = "0.00";
                    txtmember_type_name.ReadOnly = false;
                    txtmember_type_name.CssClass = "textbox";
                    chkStatus.Checked = true;
                    txtmember_type_name.Focus();
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
            cMember_type oMember_type = new cMember_type();
            DataSet ds = new DataSet();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strmember_type_code = string.Empty,
                strmember_type_name = string.Empty,
                strmember_type_rate = string.Empty,
                strcompany_rate = string.Empty,
                strC_active = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty,
                strCreatedDate = string.Empty,
                strUpdatedDate = string.Empty;
            try
            {
                strCriteria = " and member_type_code = '" + ViewState["member_type_code"].ToString() + "' ";
                if (!oMember_type.SP_MEMBER_TYPE_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region get Data
                        strmember_type_code = ds.Tables[0].Rows[0]["member_type_code"].ToString();
                        strmember_type_name = ds.Tables[0].Rows[0]["member_type_name"].ToString();
                        strmember_type_rate = String.Format("{0:0.00}",float.Parse(ds.Tables[0].Rows[0]["member_type_rate"].ToString()));
                        strcompany_rate = String.Format("{0:0.00}", float.Parse(ds.Tables[0].Rows[0]["company_rate"].ToString()));
                        strC_active = ds.Tables[0].Rows[0]["c_active"].ToString();
                        strCreatedBy = ds.Tables[0].Rows[0]["c_created_by"].ToString();
                        strUpdatedBy = ds.Tables[0].Rows[0]["c_updated_by"].ToString();
                        strCreatedDate = ds.Tables[0].Rows[0]["d_created_date"].ToString();
                        strUpdatedDate = ds.Tables[0].Rows[0]["d_updated_date"].ToString();
                        #endregion

                        #region set Control
                        txtmember_type_code.Text = strmember_type_code;
                        txtmember_type_name.Text = strmember_type_name;
                        txtmember_type_rate.Text = strmember_type_rate;
                        txtcompany_rate.Text = strcompany_rate;
                        if (strC_active.Equals("Y"))
                        {
                            txtmember_type_name.ReadOnly = false;
                            txtmember_type_name.CssClass = "textbox";
                            chkStatus.Checked = true;
                        }
                        else
                        {
                            txtmember_type_name.ReadOnly = true;
                            txtmember_type_name.CssClass = "textboxdis";
                            chkStatus.Checked = false;
                        }
                        txtUpdatedBy.Text = strUpdatedBy;
                        txtUpdatedDate.Text = strUpdatedDate;
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        private void imgSave_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            bool blnResult = false;
            string strScript = string.Empty;
            blnResult = saveData();
            if (blnResult)
            {
                strScript =
                    "self.opener.document.forms[0].ctl00$ASPxRoundPanel1$ContentPlaceHolder2$txthpage.value=" + ViewState["page"].ToString() + ";\n" +
                    "self.opener.document.forms[0].submit();\n" +
                    "self.close();\n" ;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "frMainPage", strScript, true);
            }
        }
    }
}