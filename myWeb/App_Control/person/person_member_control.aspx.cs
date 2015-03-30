using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using myDLL;

namespace myWeb.App_Control.person
{
    public partial class person_member_control : PageBase
    {
        #region private data
        private string strConn = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"];
        private bool[] blnAccessRight = new bool[5] { false, false, false, false, false };
        private string strPrefixCtr = "ctl00$ContentPlaceHolder1$";
        #endregion
        protected void Page_Load(object sender, System.EventArgs e)
        {
            lblError.Text = "";
            if (!IsPostBack)
            {
                Session["menupopup_name"] = "";
                #region set QueryString
                if (Request.QueryString["person_code"] != null)
                {
                    ViewState["person_code"] = Request.QueryString["person_code"].ToString();
                    lblperson_code.Text = ViewState["person_code"].ToString();
                }
                if (Request.QueryString["person_name"] != null)
                {
                    ViewState["person_name"] = Request.QueryString["person_name"].ToString();
                    lblperson_name.Text = ViewState["person_name"].ToString();
                }
                if (Request.QueryString["member_code"] != null)
                {
                    ViewState["member_code"] = Request.QueryString["member_code"].ToString();
                    txtmember_code.Text = ViewState["member_code"].ToString();
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
                }
                else
                {
                    setData(); 
                }

                #endregion
                #region Set Image

                imgList_item.Attributes.Add("onclick", "OpenPopUp('800px','400px','93%','ค้นหาข้อมูลสมาชิก/ฌาปนกิจ' ,'../lov/member_lov.aspx?" +
                                                "member_code='+document.forms[0]." + strPrefixCtr + "txtmember_code.value+" +
                                                "'&member_name='+document.forms[0]." + strPrefixCtr + "txtmember_name.value+" +
                                                "'&ctrl1=" + txtmember_code.ClientID + "&ctrl2=" + txtmember_name.ClientID + "&show=3', '3');return false;");

                imgClear_item.Attributes.Add("onclick", "document.forms[0]." + strPrefixCtr + "txtmember_code.value='';" +
                                        "document.forms[0]." + strPrefixCtr + "txtmember_name.value='';" +
                                        "document.forms[0]." + strPrefixCtr + "txtmember_quan.value='0'; return false;");

                #endregion
                txtmember_quan.Attributes.Add("onblur", "checkInt(this,99999)");
                imgSaveOnly.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgSaveOnly"].Rows[0]["title"].ToString());
                chkStatus.Checked = true;
                txtmember_code.Focus();  
            }
        }

        #region private function
        public static string getItemtype(object mData)
        {
            if (mData.Equals("D"))
            {
                return "Debit";
            }
            else
            {
                return "Credit";
            }
        }
        #endregion

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
            string strperson_code = string.Empty,
                strmember_code = string.Empty,
                strmember_name = string.Empty,
                strmember_quan = string.Empty,
                strActive = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty;
            string strScript = string.Empty;
            cPerson oPerson = new cPerson();
            DataSet ds = new DataSet();
            try
            {
                #region set Data
                strperson_code = lblperson_code.Text.Trim();
                strmember_code = txtmember_code.Text.Trim();
                strmember_name = txtmember_name.Text.Trim();
                strmember_quan = txtmember_quan.Text.Trim();
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
                        if (oPerson.SP_PERSON_MEMBER_UPD(strperson_code,strmember_code, strmember_quan  ,strActive, strUpdatedBy, ref strMessage))
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
                    strCheckDup = " and person_code = '" + strperson_code.Trim() + "' " +
                                                  " and member_code = '" + strmember_code.Trim() + "' ";
                    if (!oPerson.SP_PERSON_MEMBER_SEL(strCheckDup, ref ds, ref strMessage))
                    {
                        lblError.Text = strMessage;
                    }
                    else
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            strScript =
                                "alert(\"ไม่สามารถเพิ่มข้อมูล เนื่องจากข้อมูล " + strmember_code.Trim() + " : " + strmember_name.Trim() + "  ซ้ำ\");\n";
                            blnDup = true;
                        }
                    }
                    #endregion
                    #region insert
                    if (!blnDup)
                    {
                        if (oPerson.SP_PERSON_MEMBER_INS(strperson_code, strmember_code, strmember_quan, strActive, strCreatedBy, ref strMessage))
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
                oPerson.Dispose();
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
                    txtmember_quan.Text = "0";
                    txtmember_code.Focus();
                    string strScript1 = "window.parent.frames['iframeShow1'].__doPostBack('ctl00$ContentPlaceHolder1$BtnR2','')";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
                }
                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    string strScript1 = "window.parent.frames['iframeShow1'].__doPostBack('ctl00$ContentPlaceHolder1$BtnR2','');ClosePopUp('2');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
                }
                MsgBox("บันทึกข้อมูลสมบูรณ์");
            }
        }

        private void setData()
        {
            cPerson oPerson = new cPerson();
            DataSet ds = new DataSet();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strmember_code = string.Empty,
                strmember_name = string.Empty,
                strAmount = string.Empty,
                strYear = string.Empty,
                strC_active = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty,
                strCreatedDate = string.Empty,
                strUpdatedDate = string.Empty;

            try
            {
                strCriteria = " and person_code = '" + ViewState["person_code"].ToString() + "' " +
                                        " and member_code = '" + ViewState["member_code"].ToString() + "' ";
                if (!oPerson.SP_PERSON_MEMBER_SEL(strCriteria, ref ds, ref strMessage))
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
                        strAmount = String.Format("{0:0}", int.Parse(ds.Tables[0].Rows[0]["member_quan"].ToString()));
                        strC_active = ds.Tables[0].Rows[0]["c_active"].ToString();
                        strCreatedBy = ds.Tables[0].Rows[0]["c_created_by"].ToString();
                        strUpdatedBy = ds.Tables[0].Rows[0]["c_updated_by"].ToString();
                        strCreatedDate = ds.Tables[0].Rows[0]["d_created_date"].ToString();
                        strUpdatedDate = ds.Tables[0].Rows[0]["d_updated_date"].ToString();
                        #endregion

                        #region set Control
                 
                        imgList_item.Enabled = false;
                        imgClear_item.Enabled = false; 
                        txtmember_code.Text = strmember_code;
                        txtmember_name.Text = strmember_name;
                        txtmember_quan.Text = strAmount;

                        txtmember_code.ReadOnly = true;
                        txtmember_code.CssClass = "textboxdis";
                        txtmember_name.ReadOnly = true;
                        txtmember_name.CssClass = "textboxdis";
                        if (strC_active.Equals("Y"))
                        {
                            txtmember_quan.ReadOnly = false;
                            txtmember_quan.CssClass = "numberbox";
                            chkStatus.Checked = false;
                        }
                        else
                        {
                            txtmember_quan.ReadOnly = true;
                            txtmember_quan.CssClass = "numberdis";
                            chkStatus.Checked = false;
                        }
                        txtUpdatedBy.Text = strUpdatedBy;
                        txtUpdatedDate.Text = strUpdatedDate;
                        txtmember_code.Focus();
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