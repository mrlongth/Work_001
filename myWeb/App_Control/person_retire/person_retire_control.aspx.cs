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

namespace myWeb.App_Control.person_retire
{
    public partial class person_retire_control : PageBase
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
            AjaxPro.Utility.RegisterTypeForAjax(typeof(person_retire_control));
            lblError.Text = "";
            if (!IsPostBack)
            {

                imgSaveOnly.Attributes.Add("onMouseOver", "src='../../images/controls/save2.jpg'");
                imgSaveOnly.Attributes.Add("onMouseOut", "src='../../images/controls/save.jpg'");

                BtnR1.Style.Add("display", "none");
                BtnR2.Style.Add("display", "none");
                BtnR3.Style.Add("display", "none");
                txtperson_id.Attributes.Add("onblur", "checkInt(this,9999999999999)");


                #region set QueryString
                if (Request.QueryString["pr_person_code"] != null)
                {
                    ViewState["pr_person_code"] = Request.QueryString["pr_person_code"].ToString();
                }
                if (Request.QueryString["page"] != null)
                {
                    ViewState["page"] = Request.QueryString["page"].ToString();
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
                if (Request.QueryString["FromPage"] != null)
                {
                    ViewState["FromPage"] = Request.QueryString["FromPage"].ToString();
                }

                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    InitcboTitle();
                    ViewState["page"] = Request.QueryString["page"];
                    txtpr_person_code.ReadOnly = true;
                    txtpr_person_code.CssClass = "textboxdis";
                    txtpr_person_code.CssClass = "textboxdis";


                }
                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    setData();
                    txtpr_person_code.ReadOnly = true;
                    txtpr_person_code.CssClass = "textboxdis";
                }

                #endregion





            }

        }

        #region private function

        private void InitcboTitle()
        {
            cTitle oTitle = new cTitle();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strtitle_code = string.Empty;
            strtitle_code = cboTitle.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and c_active='Y' ";
            if (oTitle.SP_SEL_TITLE(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboTitle.Items.Clear();
                cboTitle.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboTitle.Items.Add(new ListItem(dt.Rows[i]["title_name"].ToString(), dt.Rows[i]["title_code"].ToString()));
                }
                if (cboTitle.Items.FindByValue(strtitle_code) != null)
                {
                    cboTitle.SelectedIndex = -1;
                    cboTitle.Items.FindByValue(strtitle_code).Selected = true;
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
            string strpr_person_code = string.Empty,
                strtitle_code = string.Empty,
                strperson_thai_name = string.Empty,
                strperson_thai_surname = string.Empty,
                strperson_id = string.Empty,
                strC_active = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty,
                strCreatedDate = string.Empty,
                strUpdatedDate = string.Empty,
                strPerson_password = string.Empty,
                strPerson_email = string.Empty,
                strScript = string.Empty;
            cPerson_retire oPerson_retire = new cPerson_retire();
            DataSet ds = new DataSet();
            try
            {
                #region set Data
                strpr_person_code = txtpr_person_code.Text;
                strtitle_code = cboTitle.SelectedValue;
                if (Request.Form[strPrefixCtr_main + "TabPanel1$cboTitle"] != null)
                {
                    strtitle_code = Request.Form[strPrefixCtr_main + "TabPanel1$cboTitle"].ToString();
                }
                strperson_thai_name = txtperson_thai_name.Text;
                strperson_thai_surname = txtperson_thai_surname.Text;
                strperson_id = txtperson_id.Text;
                if (chkStatus.Checked == true)
                {
                    strC_active = "Y";
                }
                else
                {
                    strC_active = "N";
                }
                strPerson_password = txtperson_password.Text.Trim();
                strPerson_email = txtperson_email.Text.Trim();
                strCreatedBy = Session["username"].ToString();
                strUpdatedBy = Session["username"].ToString();
                #endregion
                if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    #region check dup
                    string strCheckDup = string.Empty;
                    strCheckDup = " and person_thai_name = '" + strperson_thai_name.Trim() + "' and person_thai_surname= '" +
                                                  strperson_thai_surname.Trim() + "' and pr_person_code<>'" + strpr_person_code.Trim() + "' ";
                    if (!oPerson_retire.SP_PERSON_RETIRE_SEL(strCheckDup, ref ds, ref strMessage))
                    {
                        lblError.Text = strMessage;
                    }
                    else
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            strScript =
                                "alert(\"ไม่สามารถแก้ไขข้อมูลได้ เนื่องจาก" +
                                "\\nข้อมูลบุคคลกร : " + strperson_thai_name.Trim() + "  " + strperson_thai_surname.Trim() +
                                "\\nซ้ำ\");\n";
                            blnDup = true;
                        }
                    }
                    #endregion
                    #region edit
                    if (!blnDup)
                    {
                        if (oPerson_retire.SP_PERSON_RETIRE_UPD(strpr_person_code, strtitle_code, strperson_thai_name, strperson_thai_surname, 
                            strperson_id,strC_active, strUpdatedBy, strPerson_password , strPerson_email, ref strMessage))
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
                    strCheckDup = " and person_thai_name = '" + strperson_thai_name.Trim() + "' and person_thai_surname= '" + strperson_thai_surname.Trim() + "' ";
                    if (!oPerson_retire.SP_PERSON_RETIRE_SEL(strCheckDup, ref ds, ref strMessage))
                    {
                        lblError.Text = strMessage;
                    }
                    else
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            strScript =
                                "alert(\"ไม่สามารถแก้ไขข้อมูลได้ เนื่องจาก" +
                                "\\nข้อมูลบุคคลกร : " + strperson_thai_name.Trim() + "  " + strperson_thai_surname.Trim() +
                                "\\nซ้ำ\");\n";
                            blnDup = true;
                        }
                    }
                    #endregion
                    #region insert
                    if (!blnDup)
                    {
                        
                        
                      if (oPerson_retire.SP_PERSON_RETIRE_INS(strpr_person_code, strtitle_code, strperson_thai_name, strperson_thai_surname,
                            strperson_id, strC_active, strUpdatedBy, strPerson_password, strPerson_email, ref strMessage))                                                                     
                        {
                            string strGetcode = " and person_thai_name = '" + strperson_thai_name.Trim() + "' and person_thai_surname = '" + strperson_thai_surname + "' ";
                            if (!oPerson_retire.SP_PERSON_RETIRE_SEL(strGetcode, ref ds, ref strMessage))
                            {
                                lblError.Text = strMessage;
                            }
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                strpr_person_code = ds.Tables[0].Rows[0]["pr_person_code"].ToString();
                            }
                            ViewState["pr_person_code"] = strpr_person_code;
                            txtpr_person_code.Text = ViewState["pr_person_code"].ToString();
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
                oPerson_retire.Dispose();
            }
            return blnResult;
        }

        private void imgSaveOnly_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (saveData())
            {
                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    Response.Redirect("person_retire_control.aspx?mode=edit&pr_person_code=" + ViewState["pr_person_code"].ToString() + "&page=" + ViewState["page"].ToString() + "&PageStatus=save", true);
                }
                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    setData();
                    txtpr_person_code.ReadOnly = true;
                    txtpr_person_code.CssClass = "textboxdis";
                    string strScript1 = "ClosePopUpListPost('" + ViewState["page"].ToString() + "','1');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
                }
                setData();
                txtpr_person_code.ReadOnly = true;
                txtpr_person_code.CssClass = "textboxdis";
                MsgBox("บันทึกข้อมูลสมบูรณ์");
            }
        }

        private void setData()
        {
            cPerson_retire oPerson_retire = new cPerson_retire();
            DataSet ds = new DataSet();
            string strMessage = string.Empty, strCriteria = string.Empty;
            #region clear Data
            //Tab 1 
            string strpr_person_code = string.Empty,
                strtitle_code = string.Empty,
                strperson_thai_name = string.Empty,
                strperson_thai_surname = string.Empty,
                strperson_id = string.Empty,
                strC_active = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty,
                strCreatedDate = string.Empty,
                strUpdatedDate = string.Empty;

            #endregion
            try
            {
                strCriteria = " and pr_person_code = '" + ViewState["pr_person_code"].ToString() + "' ";
                if (!oPerson_retire.SP_PERSON_RETIRE_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region get Data
                        //Tab 1 
                        strpr_person_code = ds.Tables[0].Rows[0]["pr_person_code"].ToString();
                        strtitle_code = ds.Tables[0].Rows[0]["title_code"].ToString();
                        strperson_thai_name = ds.Tables[0].Rows[0]["person_thai_name"].ToString();
                        strperson_thai_surname = ds.Tables[0].Rows[0]["person_thai_surname"].ToString();

                        strperson_id = ds.Tables[0].Rows[0]["person_id"].ToString();

                        strC_active = ds.Tables[0].Rows[0]["c_active"].ToString();
                        strCreatedBy = ds.Tables[0].Rows[0]["c_created_by"].ToString();
                        strUpdatedBy = ds.Tables[0].Rows[0]["c_updated_by"].ToString();
                        strCreatedDate = ds.Tables[0].Rows[0]["d_created_date"].ToString();
                        strUpdatedDate = ds.Tables[0].Rows[0]["d_updated_date"].ToString();


                        #endregion

                        #region set Control

                        txtpr_person_code.Text = strpr_person_code;
                        Session["pr_person_code"] = strpr_person_code;
                        InitcboTitle();
                        if (cboTitle.Items.FindByValue(strtitle_code) != null)
                        {
                            cboTitle.SelectedIndex = -1;
                            cboTitle.Items.FindByValue(strtitle_code).Selected = true;
                        }
                        txtperson_thai_name.Text = strperson_thai_name;
                        txtperson_thai_surname.Text = strperson_thai_surname;

                        txtperson_id.Text = strperson_id;

                        txtUpdatedBy.Text = strUpdatedBy;
                        txtUpdatedDate.Text = strUpdatedDate;

                        txtperson_email.Text = ds.Tables[0].Rows[0]["person_email"].ToString();
                        txtperson_password.Text = ds.Tables[0].Rows[0]["person_password"].ToString();

                        if (strC_active.Equals("Y"))
                        {
                            chkStatus.Checked = true;
                        }
                        else
                        {
                            chkStatus.Checked = false;
                        }

                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        protected void BtnR1_Click(object sender, EventArgs e)
        {
            //BindGridView1();
        }

        protected void BtnR2_Click(object sender, EventArgs e)
        {
            //BindGridView2();
        }

        protected void BtnR3_Click(object sender, EventArgs e)
        {
            //BindGridView3();
        }

        //public string getTitle(object str)
        //{
        //    DataSet ods = new DataSet();
        //    string strError = string.Empty;
        //    string strReturn = string.Empty;
        //    cTitle oTitle = new cTitle();
        //    str = ChkStrNull(str);
        //    try
        //    {
        //        oTitle.SP_SEL_TITLE(" And  title_name='" + str.ToString().Trim() + "'", ref ods, ref strError);
        //        if (ods.Tables[0].Rows.Count > 0)
        //        {
        //            strReturn = ods.Tables[0].Rows[0][0].ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblError.Text = "ไม่สามารถจัดการข้อมูล เนื่องจาก " + ex.Message;
        //    }
        //    return strReturn;
        //}



    }
}