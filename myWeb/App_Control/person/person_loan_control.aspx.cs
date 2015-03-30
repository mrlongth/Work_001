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
    public partial class person_loan_control : PageBase
    {
        #region private data
        private string strConn = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"];
        private bool[] blnAccessRight = new bool[5] { false, false, false, false, false };
        private string strPrefixCtr = "ctl00$ContentPlaceHolder1$";
        private string strPrefixCtr_main = "ctl00$ContentPlaceHolder1$TabContainer1$";

        #endregion
        protected void Page_Load(object sender, System.EventArgs e)
        {
            lblError.Text = "";
            if (!IsPostBack)
            {
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
                if (Request.QueryString["loan_code"] != null)
                {
                    ViewState["loan_code"] = Request.QueryString["loan_code"].ToString();
                }

                if (Request.QueryString["loan_acc"] != null)
                {
                    ViewState["loan_acc"] = Request.QueryString["loan_acc"].ToString();
                }

                ViewState["mode"] = Request.QueryString["mode"].ToString();
                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    ViewState["page"] = Request.QueryString["page"];
                    InitcboLoan();
                }
                else
                {
                    setData();
                }

                #endregion
      
                imgSaveOnly.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgSaveOnly"].Rows[0]["title"].ToString());
              
            }
        }

        #region private function
       
        private void InitcboLoan()
        {
            cCommon oCommon = new cCommon();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strCode = cboLoan_code.SelectedValue;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " Select * from  loan order by loan_name ";
            if (oCommon.SEL_SQL(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboLoan_code.Items.Clear();
                int i;
                cboLoan_code.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboLoan_code.Items.Add(new ListItem(dt.Rows[i]["loan_name"].ToString(), dt.Rows[i]["loan_code"].ToString()));
                }
                if (cboLoan_code.Items.FindByValue(strCode) != null)
                {
                    cboLoan_code.SelectedIndex = -1;
                    cboLoan_code.Items.FindByValue(strCode).Selected = true;
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
        }
        #endregion

        private bool saveData()
        {
            bool blnResult = false;
            bool blnDup = false;
            string strMessage = string.Empty;
            string strperson_code = string.Empty,
                strloan_code = string.Empty,
                strloan_acc = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty;
            string strScript = string.Empty;
            cPerson oPerson = new cPerson();
            DataSet ds = new DataSet();
            try
            {
                #region set Data
                strperson_code = lblperson_code.Text.Trim();
                strloan_code = cboLoan_code.SelectedValue;
                strloan_acc = txtLoan_acc.Text.Trim();
               
                strCreatedBy = Session["username"].ToString();
                strUpdatedBy = Session["username"].ToString();
                #endregion
                if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    #region edit
                    if (!blnDup)
                    {
                        if (oPerson.SP_PERSON_LOAN_UPD(strperson_code, strloan_code, strloan_acc , txtLoan_acc_name.Text.Trim() , txtLoan_remark.Text.Trim(),  strUpdatedBy, ref strMessage))
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
                                                  " and loan_code = '" + strloan_code.Trim() + "' " + 
                                                  " and loan_acc = '" + strloan_acc.Trim() + "' ";
                    if (!oPerson.SP_PERSON_LOAN_SEL(strCheckDup, ref ds, ref strMessage))
                    {
                        lblError.Text = strMessage;
                    }
                    else
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            strScript =
                                "alert(\"ไม่สามารถเพิ่มข้อมูล เนื่องจากข้อมูล " + cboLoan_code.SelectedItem.Text + " : เลขที่บัญชี   " + txtLoan_acc.Text + "  ซ้ำ\");\n";
                            blnDup = true;
                        }
                    }
                    #endregion
                    #region insert
                    if (!blnDup)
                    {
                        if (oPerson.SP_PERSON_LOAN_INS(strperson_code, strloan_code, strloan_acc, txtLoan_acc_name.Text.Trim(), txtLoan_remark.Text.Trim(), strCreatedBy, ref strMessage))
                        {
                            ViewState["loan_code"] = strloan_code;
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
                    InitcboLoan();
                    txtLoan_acc.Text = string.Empty;
                    cboLoan_code.Focus();
                    string strScript1 = "window.parent.frames['iframeShow1'].__doPostBack('ctl00$ContentPlaceHolder1$BtnR4','');ClosePopUp('2');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
                }
                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    string strScript1 = "window.parent.frames['iframeShow1'].__doPostBack('ctl00$ContentPlaceHolder1$BtnR4','');ClosePopUp('2');";
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
            string strloan_code = string.Empty,
                strloan_name = string.Empty,
                strloan_acc = string.Empty,
                strloan_acc_name = string.Empty,
                strloan_remark = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty,
                strCreatedDate = string.Empty,               
                strUpdatedDate = string.Empty;
            try
            {
                strCriteria = " and person_code = '" + ViewState["person_code"].ToString() + "' " +
                                " and loan_code = '" + ViewState["loan_code"].ToString() + "' " + 
                                " and loan_acc = '" + ViewState["loan_acc"].ToString() + "' " ;
                if (!oPerson.SP_PERSON_LOAN_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region get Data
                        strloan_code = ds.Tables[0].Rows[0]["loan_code"].ToString();
                        strloan_name = ds.Tables[0].Rows[0]["loan_name"].ToString();
                        strloan_acc = ds.Tables[0].Rows[0]["loan_acc"].ToString();
                        strloan_acc_name = ds.Tables[0].Rows[0]["loan_acc_name"].ToString();
                        strloan_remark = ds.Tables[0].Rows[0]["loan_remark"].ToString();
                       
                        strCreatedBy = ds.Tables[0].Rows[0]["c_created_by"].ToString();
                        strUpdatedBy = ds.Tables[0].Rows[0]["c_updated_by"].ToString();
                        strCreatedDate = ds.Tables[0].Rows[0]["d_created_date"].ToString();
                        strUpdatedDate = ds.Tables[0].Rows[0]["d_updated_date"].ToString();

                        #endregion

                        #region set Control

                        InitcboLoan();
                        if (cboLoan_code.Items.FindByValue(strloan_code) != null)
                        {
                            cboLoan_code.SelectedIndex = -1;
                            cboLoan_code.Items.FindByValue(strloan_code).Selected = true;
                        }
                        cboLoan_code.Enabled = false;
                        txtLoan_acc.Text = strloan_acc;
                        txtLoan_acc.Enabled = false;
                        txtLoan_acc_name.Text = strloan_acc_name;
                        txtLoan_remark.Text = strloan_remark;
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