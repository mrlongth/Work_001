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

namespace myWeb.App_Control.user
{
    public partial class user_control : PageBase
    {
        #region private data
        private string strConn = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"];
        private bool[] blnAccessRight = new bool[5] { false, false, false, false, false };
        private string strPrefixCtr_main = "ctl00$ContentPlaceHolder1$TabContainer1$";
        #endregion

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

        protected void Page_Load(object sender, System.EventArgs e)
        {
            lblError.Text = "";
            if (!IsPostBack)
            {

                InitcboUser_group();

                imgSaveOnly.Attributes.Add("onMouseOver", "src='../../images/controls/save2.jpg'");
                imgSaveOnly.Attributes.Add("onMouseOut", "src='../../images/controls/save.jpg'");

                #region set QueryString
                if (Request.QueryString["userID"] != null)
                {
                    ViewState["userID"] = Request.QueryString["userID"].ToString();
                }
                if (Request.QueryString["page"] != null)
                {
                    ViewState["page"] = Request.QueryString["page"].ToString();
                }
                else
                {
                    ViewState["page"] = Request.QueryString["1"].ToString();
                }
                if (Request.QueryString["mode"] != null)
                {
                    ViewState["mode"] = Request.QueryString["mode"].ToString();
                }
                else
                {
                    ViewState["mode"] = string.Empty;
                }
                if (Request.QueryString["PageStatus"] != null)
                {
                    ViewState["PageStatus"] = Request.QueryString["PageStatus"].ToString();
                }

                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    InitcboUser_group();
                    InitcboBudgetType();
                    txtloginname.ReadOnly = false;
                    txtloginname.CssClass = "textbox";
                }
                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    setData();
                    //txtloginname.ReadOnly = true;
                    //txtloginname.CssClass = "textboxdis";
                }

                #endregion

                #region Set Image
                string strYear = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["yearnow"].ToString();
                imgList_person.Attributes.Add("onclick", "OpenPopUp('900px','500px','94%','ค้นหาข้อมูลบุคคลากร' ,'../lov/person_lov.aspx?year=" + strYear +
                                                                              "&person_code='+document.getElementById('" + txtperson_code.ClientID + "').value+'" +
                                                                              "&person_name='+document.getElementById('" + txtperson_name.ClientID + "').value+'" +
                                                                              "&ctrl1=" + txtperson_code.ClientID + "&ctrl2=" + txtperson_name.ClientID +
                                                                              "&ctrl3=" + txtperson_group_name.ClientID + "&ctrl4=" + txtdirector_name.ClientID +
                                                                              "&ctrl5=" + txtunit_name.ClientID + "&show=2&from=user', '2');return false;");
                imgClear_person.Attributes.Add("onclick", "document.getElementById('" + txtperson_code.ClientID + "').value='';" +
                                                                                                    "document.getElementById('" + txtperson_name.ClientID + "').value=''; " +
                                                                                                    "document.getElementById('" + txtperson_group_name.ClientID + "').value=''; " +
                                                                                                    "document.getElementById('" + txtdirector_name.ClientID + "').value=''; " +
                                                                                                    "document.getElementById('" + txtunit_name.ClientID + "').value=''; " +
                                                                                                    "return false;");
                #endregion

            }
        }

        #region private function
        
        private void InitcboUser_group()
        {
            cUser_group oUser_group = new cUser_group();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        struser_group_code = string.Empty;
            struser_group_code = cbouser_group.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and c_active='Y' ";
            if (oUser_group.SP_User_Group_SEL(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cbouser_group.Items.Clear();
                cbouser_group.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cbouser_group.Items.Add(new ListItem(dt.Rows[i]["user_group_name"].ToString(), dt.Rows[i]["user_group_code"].ToString()));
                }
                if (cbouser_group.Items.FindByValue(struser_group_code) != null)
                {
                    cbouser_group.SelectedIndex = -1;
                    cbouser_group.Items.FindByValue(struser_group_code).Selected = true;
                }
            }
        }


        private void InitcboBudgetType()
        {
            cCommon oCommon = new cCommon();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strCode = cboBudget_type.SelectedValue;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " Select * from  general where g_type = 'budget_type'  Order by g_sort ";
            if (oCommon.SEL_SQL(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboBudget_type.Items.Clear();
                int i;
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboBudget_type.Items.Add(new ListItem(dt.Rows[i]["g_name"].ToString(), dt.Rows[i]["g_code"].ToString()));
                }
                if (cboBudget_type.Items.FindByValue(strCode) != null)
                {
                    cboBudget_type.SelectedIndex = -1;
                    cboBudget_type.Items.FindByValue(strCode).Selected = true;
                }
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
        /// 
        private void InitializeComponent()
        {
            this.imgSaveOnly.Click += new System.Web.UI.ImageClickEventHandler(this.imgSaveOnly_Click);
            //this.imgSave.Click += new System.Web.UI.ImageClickEventHandler(this.imgSave_Click);

        }
        #endregion

        private bool saveData()
        {
            bool blnResult = false;
            bool blnDup = false;
            string strMessage = string.Empty;
            //Tab 1 
            string
                strUserID = Helper.CStr(Helper.CInt(hddUserID.Value)),
                strperson_code = string.Empty,
                strloginname = string.Empty,
                strpassword = string.Empty,
                stremail = string.Empty,
                struser_group_code = string.Empty,
                strStatus = string.Empty,
                strRemark = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty;
            string strScript = string.Empty;
            cUser oUser = new cUser();
            DataSet ds = new DataSet();
            try
            {
                #region set Data
                strperson_code = txtperson_code.Text;
                strloginname = txtloginname.Text;
                strpassword = txtpassword.Text;
                stremail = txtemail.Text;
                struser_group_code = cbouser_group.SelectedValue;
                if (chkStatus.Checked == true)
                {
                    strStatus = "Y";
                }
                else
                {
                    strStatus = "N";
                }
                strCreatedBy = Session["username"].ToString();
                strUpdatedBy = Session["username"].ToString();
                #endregion
                if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    #region edit

                    #region check dup
                    string strCheckDup = string.Empty;
                    strCheckDup = " and [loginname] = '" + strloginname + "'  and [loginname] <> '" + hddusername.Value.ToString() + "' ";
                    if (!oUser.SP_USER_SEL(strCheckDup, ref ds, ref strMessage))
                    {
                        lblError.Text = strMessage;
                    }
                    else
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            strScript =
                                "alert(\"ไม่สามารถแก้ไขข้อมูลได้ เนื่องจาก" +
                                "\\nข้อมูล Username : " + strloginname.Trim() +
                                "\\nซ้ำ\");\n";
                            blnDup = true;
                        }
                    }
                    #endregion
                    if (!blnDup)
                    {
                        if (oUser.SP_USER_UPD(strUserID, strperson_code, strloginname, strpassword, stremail, struser_group_code, strStatus, strRemark, strUpdatedBy, cboBudget_type.SelectedValue, ref strMessage))
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
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "chkdup", strScript, true);
                    }
                    #endregion
                }
                else
                {
                    #region check dup
                    string strCheckDup = string.Empty;
                    strCheckDup = " and [loginname] = '" + strloginname + "' ";
                    if (!oUser.SP_USER_SEL(strCheckDup, ref ds, ref strMessage))
                    {
                        lblError.Text = strMessage;
                    }
                    else
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            strScript =
                                "alert(\"ไม่สามารถเพิ่มข้อมูลได้ เนื่องจาก" +
                                "\\nข้อมูล Username : " + strloginname.Trim() +
                                "\\nซ้ำ\");\n";
                            blnDup = true;
                        }
                    }
                    #endregion

                    #region insert
                    if (!blnDup)
                    {
                        if (oUser.SP_USER_INS(ref strUserID, strperson_code, strloginname, strpassword, stremail, struser_group_code, strStatus, strRemark, strCreatedBy, cboBudget_type.SelectedValue, ref strMessage))
                        {

                            string strCode = " and [loginname] = '" + strloginname + "' ";
                            if (!oUser.SP_USER_SEL(strCheckDup, ref ds, ref strMessage))
                            {
                                lblError.Text = strMessage;
                            }
                            else
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    ViewState["userID"] = ds.Tables[0].Rows[0]["userID"].ToString();
                                }
                            }
                            blnResult = true;
                        }
                        else
                        {
                            lblError.Text = strMessage.ToString();
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "close", strScript, true);
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
                oUser.Dispose();
            }
            return blnResult;
        }

        private void imgSaveOnly_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (saveData())
            {
                MsgBox("บันทึกข้อมูลสมบูรณ์");
                string strScript1 = "ClosePopUpListPost('" + ViewState["page"].ToString() + "','1');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
            }
        }

        private void setData()
        {
            cUser oUser = new cUser();
            DataSet ds = new DataSet();
            string strMessage = string.Empty, strCriteria = string.Empty;

            try
            {
                strCriteria = " and UserID = '" + ViewState["userID"].ToString() + "' ";
                if (!oUser.SP_USER_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region get Data
                        hddUserID.Value = ds.Tables[0].Rows[0]["UserID"].ToString();
                        txtperson_code.Text = ds.Tables[0].Rows[0]["person_code"].ToString();
                        txtperson_name.Text = ds.Tables[0].Rows[0]["person_thai_name"].ToString() + "  " + ds.Tables[0].Rows[0]["person_thai_surname"].ToString();
                        txtperson_group_name.Text = ds.Tables[0].Rows[0]["person_group_name"].ToString();
                        txtdirector_name.Text = ds.Tables[0].Rows[0]["director_name"].ToString();
                        txtunit_name.Text = ds.Tables[0].Rows[0]["unit_name"].ToString();
                        txtemail.Text = ds.Tables[0].Rows[0]["email"].ToString();
                        txtloginname.Text = ds.Tables[0].Rows[0]["loginname"].ToString();
                        hddusername.Value = ds.Tables[0].Rows[0]["loginname"].ToString();
                        txtpassword.Text = Cryptorengine.Decrypt(ds.Tables[0].Rows[0]["password"].ToString(), true);
                        string strBudget_type = ds.Tables[0].Rows[0]["budget_type"].ToString();
                        InitcboBudgetType();
                        if (cboBudget_type.Items.FindByValue(strBudget_type) != null)
                        {
                            cboBudget_type.SelectedIndex = -1;
                            cboBudget_type.Items.FindByValue(strBudget_type).Selected = true;
                        }
                        if (ds.Tables[0].Rows[0]["status"].ToString() == "Y")
                        {
                            chkStatus.Checked = true;
                        }
                        else
                        {
                            chkStatus.Checked = false;
                        }
                        InitcboUser_group();
                        cbouser_group.SelectedValue = ds.Tables[0].Rows[0]["user_group_code"].ToString();
                        txtUpdatedBy.Text = ds.Tables[0].Rows[0]["UpdatedBy"].ToString(); ;
                        txtUpdatedDate.Text = cCommon.CheckDate(ds.Tables[0].Rows[0]["UpdatedDate"].ToString()) ;

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