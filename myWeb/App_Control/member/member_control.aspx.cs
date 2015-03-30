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

namespace myWeb.App_Control.member
{
    public partial class member_control : PageBase
    {
        private string strPrefixCtr = "ctl00$ContentPlaceHolder1$";
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

                #region Set Image
                string     strYear = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["yearnow"].ToString();
                imgList_item.Attributes.Add("onclick", "OpenPopUp('750px','400px','93%','ค้นหาข้อมูลรายได้/ค่าใช้จ่าย' ,'../lov/item_lov.aspx?" +
                                                "year=" + strYear +"&item_code='+document.forms[0]." + strPrefixCtr + "txtitem_code.value+'" +
                                                "&item_name='+document.forms[0]." + strPrefixCtr + "txtitem_name.value+'" +
                                                "&ctrl1=" + txtitem_code.ClientID + "&ctrl2=" + txtitem_name.ClientID +
                                                "&show=2&from=member&item_type=C', '2');return false;");

                imgClear_item.Attributes.Add("onclick", "document.forms[0]." + strPrefixCtr + "txtitem_code.value='';" +
                                        "document.forms[0]." + strPrefixCtr + "txtitem_name.value=''; return false;");

                #endregion


                #region set QueryString
                if (Request.QueryString["member_code"] != null)
                {
                    ViewState["member_code"] = Request.QueryString["member_code"].ToString();
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
                    Session["menupopup_name"] = "เพิ่มข้อมูลเงินสมาชิก";
                    ViewState["page"] = Request.QueryString["page"];
                    txtmember_code.ReadOnly = false;
                    txtmember_code.CssClass = "textbox";
                    chkStatus.Checked = true;
                }

                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    Session["menupopup_name"] =  "แก้ไขข้อมูลเงินสมาชิก";
                    setData();
                    txtmember_code.ReadOnly = true;
                    txtmember_code.CssClass = "textboxdis";
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
            string strmember_code = string.Empty,
                strmember_name = string.Empty,
                stritem_code = string.Empty,
                strActive = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty;
            string strScript = string.Empty;
            cMember oMember = new cMember();
            DataSet ds = new DataSet();
            try
            {
                #region set Data
                strmember_code = txtmember_code.Text.Trim();
                strmember_name = txtmember_name.Text;
                stritem_code = txtitem_code.Text.Trim();    
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
                        if (oMember.SP_MEMBER_UPD(strmember_code, strmember_name,stritem_code, strActive,strUpdatedBy, ref strMessage))
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
                    strCheckDup = " and member_code = '" + strmember_code.Trim() + "' ";
                    if (!oMember.SP_MEMBER_SEL(strCheckDup, ref ds, ref strMessage))
                    {
                        lblError.Text = strMessage;
                    }
                    else
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            strScript = 
                                "alert(\"ไม่สามารถเพิ่มข้อมูล เนื่องจากข้อมูล " + strmember_code.Trim() + " : " + strmember_name.Trim() + "  ซ้ำ\");\n" ;
                            blnDup = true;
                        }
                    }
                    #endregion
                    #region insert
                    if (!blnDup)
                    {
                        if (oMember.SP_MEMBER_INS(strmember_code, strmember_name,stritem_code, strActive, strCreatedBy, ref strMessage))
                        {
                            ViewState["member_code"] = strmember_code;
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
                oMember.Dispose();
            }
            return blnResult;
        }

        private void imgSaveOnly_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (saveData())
            {
                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    txtmember_code.Text = string.Empty;
                    txtmember_name.Text = string.Empty;
                    txtmember_code.Focus();
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
            cMember oMember = new cMember();
            DataSet ds = new DataSet();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strmember_code = string.Empty,
                strmember_name = string.Empty,
                stritem_code = string.Empty,
                stritem_name = string.Empty,
                strC_active = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty,
                strCreatedDate = string.Empty,
                strUpdatedDate = string.Empty;
            try
            {
                strCriteria = " and member_code = '" + ViewState["member_code"].ToString() + "' ";
                if (!oMember.SP_MEMBER_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region get Data
                        strmember_code = ds.Tables[0].Rows[0]["member_code"].ToString();
                        strmember_name = ds.Tables[0].Rows[0]["member_name"].ToString();
                        stritem_code = ds.Tables[0].Rows[0]["item_code"].ToString();
                        stritem_name = ds.Tables[0].Rows[0]["item_name"].ToString();
                        strC_active = ds.Tables[0].Rows[0]["c_active"].ToString();
                        strCreatedBy = ds.Tables[0].Rows[0]["c_created_by"].ToString();
                        strUpdatedBy = ds.Tables[0].Rows[0]["c_updated_by"].ToString();
                        strCreatedDate = ds.Tables[0].Rows[0]["d_created_date"].ToString();
                        strUpdatedDate = ds.Tables[0].Rows[0]["d_updated_date"].ToString();
                        #endregion

                        #region set Control
                        txtmember_code.Text = strmember_code;
                        txtmember_name.Text = strmember_name;
                        txtitem_code.Text = stritem_code;
                        txtitem_name.Text = stritem_name;
                        if (strC_active.Equals("Y"))
                        {
                            txtmember_name.ReadOnly = false;
                            txtmember_name.CssClass = "textbox";
                            chkStatus.Checked = true;
                        }
                        else
                        {
                            txtmember_name.ReadOnly = true;
                            txtmember_name.CssClass = "textboxdis";
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

    }
}