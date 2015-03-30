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

namespace myWeb.App_Control.budget_plan
{
    public partial class budget_plan_control : PageBase
    {
        #region private data
        private string strConn = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"];
        private bool[] blnAccessRight = new bool[5] { false, false, false, false, false };

        private string BudgetType
        {
            get
            {
                if (ViewState["BudgetType"] == null)
                {
                    ViewState["BudgetType"] = Helper.CStr(Request.QueryString["budget_type"]);
                }
                return ViewState["BudgetType"].ToString();
            }
            set
            {
                ViewState["BudgetType"] = value;
            }
        }

        #endregion

        protected void Page_Load(object sender, System.EventArgs e)
        {
            lblError.Text = "";
            if (!IsPostBack)
            {
                imgSaveOnly.Attributes.Add("onMouseOver", "src='../../images/controls/save2.jpg'");
                imgSaveOnly.Attributes.Add("onMouseOut", "src='../../images/controls/save.jpg'");

                #region set QueryString
                if (Request.QueryString["budget_plan_code"] != null)
                {
                    ViewState["budget_plan_code"] = Request.QueryString["budget_plan_code"].ToString();
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
                    InitcboYear();

                    ViewState["page"] = Request.QueryString["page"];
                    txtbudget_plan_code.ReadOnly = true;
                    txtbudget_plan_code.CssClass = "textboxdis";
                    chkStatus.Checked = true;
                    txtbudget_plan_code.CssClass = "textboxdis";
                }
                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    setData();
                    txtbudget_plan_code.ReadOnly = true;
                    txtbudget_plan_code.CssClass = "textboxdis";
                    if (ViewState["PageStatus"] != null)
                    {
                        if (ViewState["PageStatus"].ToString().ToLower().Equals("save"))
                        {
                            txtbudget_plan_code.Text = "";
                            chkStatus.Checked = true;
                            txtbudget_plan_code.Focus();
                            string strScript1 =
                                "self.opener.document.forms[0].ctl00$ASPxRoundPanel1$ContentPlaceHolder2$txthpage.value=" + ViewState["page"].ToString() + ";\n" +
                                "self.opener.document.forms[0].submit();\n" +
                                "self.focus();\n";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
                        }
                    }
                }

                #endregion

                InitcboDirector();
                InitcboBudget();
                InitcboPlan();
                InitcboWork();
                InitcboFund();

                //if (this.BudgetType == "R")
                //{
                //    foreach (Control c in Page.Controls)
                //    {
                //        base.SetLabel(c, "แผนงาน", "งานย่อย");
                //        base.SetLabel(c, "กิจกรรม", "งานรอง");
                //        base.SetLabel(c, "แผนงบประมาณ", "แผนงาน");
                //        base.SetLabel(c, "ผลผลิต", "งานหลัก");
                //    }
                //}


            }
        }

        #region private function

        private void InitcboYear()
        {
            string strYear = string.Empty;
            strYear = cboYear.SelectedValue;
            if (strYear.Equals(""))
            {
                strYear = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["yearnow"].ToString();
            }
            DataTable odt;
            int i;
            cboYear.Items.Clear();
            odt = ((DataSet)Application["xmlconfig"]).Tables["cboYear"];
            for (i = 0; i <= odt.Rows.Count - 1; i++)
            {
                cboYear.Items.Add(new ListItem(odt.Rows[i]["Text"].ToString(), odt.Rows[i]["Value"].ToString()));
            }
            if (cboYear.Items.FindByValue(strYear) != null)
            {
                cboYear.SelectedIndex = -1;
                cboYear.Items.FindByValue(strYear).Selected = true;
            }
        }

        private void InitcboDirector()
        {
            cDirector oDirector = new cDirector();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strDirector_code = string.Empty;
            string strYear = cboYear.SelectedValue;
            strDirector_code = cboDirector.SelectedValue;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and director_year = '" + strYear + "'  and  c_active='Y' ";
            if (this.BudgetType == "R")
            {
                strCriteria = strCriteria + " and budget_type <> 'B' ";
            }
            else
            {
                strCriteria = strCriteria + " and budget_type <> 'R' ";
            }
            if (oDirector.SP_SEL_DIRECTOR(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboDirector.Items.Clear();
                cboDirector.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                int i;
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboDirector.Items.Add(new ListItem(dt.Rows[i]["director_name"].ToString(), dt.Rows[i]["director_code"].ToString()));
                }
                if (cboDirector.Items.FindByValue(strDirector_code) != null)
                {
                    cboDirector.SelectedIndex = -1;
                    cboDirector.Items.FindByValue(strDirector_code).Selected = true;
                }
                InitcboUnit();
            }
        }

        private void InitcboUnit()
        {
            cUnit oUnit = new cUnit();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strUnit_code = cboUnit.SelectedValue;
            string strDirector_code = cboDirector.SelectedValue;
            string strYear = cboYear.SelectedValue;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and unit.unit_year = '" + strYear + "'  and  unit.c_active='Y' ";
            strCriteria = strCriteria + " and unit.director_code = '" + strDirector_code + "' ";
            if (this.BudgetType == "R")
            {
                strCriteria = strCriteria + " and unit.budget_type <> 'B' ";
            }
            else
            {
                strCriteria = strCriteria + " and unit.budget_type <> 'R' ";
            }
            if (oUnit.SP_SEL_UNIT(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboUnit.Items.Clear();
                cboUnit.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                int i;
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboUnit.Items.Add(new ListItem(dt.Rows[i]["unit_name"].ToString(), dt.Rows[i]["unit_code"].ToString()));
                }
                if (cboUnit.Items.FindByValue(strUnit_code) != null)
                {
                    cboUnit.SelectedIndex = -1;
                    cboUnit.Items.FindByValue(strUnit_code).Selected = true;
                }
            }
        }

        private void InitcboBudget()
        {
            cBudget oBudget = new cBudget();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strYear = cboYear.SelectedValue;
            string strbudget_code = cboBudget.SelectedValue;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and budget_year = '" + strYear + "'  and  c_active='Y' ";
            strCriteria = strCriteria + "  And budget_type ='" + this.BudgetType + "' ";
            if (oBudget.SP_SEL_BUDGET(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboBudget.Items.Clear();
                cboBudget.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                int i;
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboBudget.Items.Add(new ListItem(dt.Rows[i]["budget_name"].ToString(), dt.Rows[i]["budget_code"].ToString()));
                }
                if (cboBudget.Items.FindByValue(strbudget_code) != null)
                {
                    cboBudget.SelectedIndex = -1;
                    cboBudget.Items.FindByValue(strbudget_code).Selected = true;
                }
            }
            InitcboProduce();
        }

        private void InitcboProduce()
        {
            cProduce oProduce = new cProduce();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strbudget_code = string.Empty,
                        strproduce_code = string.Empty,
                        strproduce_name = string.Empty;
            string strYear = cboYear.SelectedValue;
            strbudget_code = cboBudget.SelectedValue;
            strproduce_code = cboProduce.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and  produce.c_active='Y' ";

            strCriteria = strCriteria + " and produce.budget_code= '" + strbudget_code + "' ";
            strCriteria = strCriteria + "  And produce.budget_type ='" + this.BudgetType + "' ";

            if (oProduce.SP_SEL_PRODUCE(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboProduce.Items.Clear();
                cboProduce.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboProduce.Items.Add(new ListItem(dt.Rows[i]["produce_name"].ToString(), dt.Rows[i]["produce_code"].ToString()));
                }
                if (cboProduce.Items.FindByValue(strproduce_code) != null)
                {
                    cboProduce.SelectedIndex = -1;
                    cboProduce.Items.FindByValue(strproduce_code).Selected = true;
                }
            }
            InitcboActivity();
        }

        private void InitcboActivity()
        {
            cActivity oActivity = new cActivity();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        stractivity_code = string.Empty,
                        strbudget_code = string.Empty,
                        strproduce_code = string.Empty;
            stractivity_code = cboActivity.SelectedValue;
            strbudget_code = cboBudget.SelectedValue;
            strproduce_code = cboProduce.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = "  and  activity.c_active='Y' ";

            strCriteria = strCriteria + " and  produce.budget_code= '" + strbudget_code + "' ";
            strCriteria = strCriteria + " and activity.produce_code= '" + strproduce_code + "' ";
            strCriteria = strCriteria + " and activity.budget_type ='" + this.BudgetType + "' ";


            if (oActivity.SP_SEL_ACTIVITY(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboActivity.Items.Clear();
                cboActivity.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboActivity.Items.Add(new ListItem(dt.Rows[i]["activity_name"].ToString(), dt.Rows[i]["activity_code"].ToString()));
                }
                if (cboActivity.Items.FindByValue(stractivity_code) != null)
                {
                    cboActivity.SelectedIndex = -1;
                    cboActivity.Items.FindByValue(stractivity_code).Selected = true;
                }
            }
        }

        private void InitcboPlan()
        {
            cPlan oPlan = new cPlan();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strplan_code = string.Empty;
            string strYear = cboYear.SelectedValue;
            strplan_code = cboPlan_code.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and plan_year = '" + strYear + "'  and  c_active='Y' ";
            strCriteria = strCriteria + " and budget_type ='" + this.BudgetType + "' ";

            if (oPlan.SP_SEL_PLAN(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboPlan_code.Items.Clear();
                cboPlan_code.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboPlan_code.Items.Add(new ListItem(dt.Rows[i]["plan_name"].ToString(), dt.Rows[i]["plan_code"].ToString()));
                }
                if (cboPlan_code.Items.FindByValue(strplan_code) != null)
                {
                    cboPlan_code.SelectedIndex = -1;
                    cboPlan_code.Items.FindByValue(strplan_code).Selected = true;
                }
            }
        }

        private void InitcboWork()
        {
            cWork oWork = new cWork();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
            strwork_code = string.Empty;
            string strYear = cboYear.SelectedValue;
            strwork_code = cboWork.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and work_year = '" + strYear + "'  and  c_active='Y' ";
            strCriteria = strCriteria + " and budget_type ='" + this.BudgetType + "' ";

            if (oWork.SP_SEL_WORK(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboWork.Items.Clear();
                cboWork.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboWork.Items.Add(new ListItem(dt.Rows[i]["work_name"].ToString(), dt.Rows[i]["work_code"].ToString()));
                }
                if (cboWork.Items.FindByValue(strwork_code) != null)
                {
                    cboWork.SelectedIndex = -1;
                    cboWork.Items.FindByValue(strwork_code).Selected = true;
                }
            }
        }

        private void InitcboFund()
        {
            cFund oFund = new cFund();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
            strfund_code = string.Empty;
            string strYear = cboYear.SelectedValue;
            strfund_code = cboFund.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and fund_year = '" + strYear + "'  and  c_active='Y' ";
            strCriteria = strCriteria + " and budget_type ='" + this.BudgetType + "' ";

            if (oFund.SP_SEL_FUND(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboFund.Items.Clear();
                cboFund.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboFund.Items.Add(new ListItem(dt.Rows[i]["fund_name"].ToString(), dt.Rows[i]["fund_code"].ToString()));
                }
                if (cboFund.Items.FindByValue(strfund_code) != null)
                {
                    cboFund.SelectedIndex = -1;
                    cboFund.Items.FindByValue(strfund_code).Selected = true;
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
            //  InitcboYear();
            bool blnResult = false;
            bool blnDup = false;
            string strMessage = string.Empty;
            string strbudget_plan_code = string.Empty,
                strbudget_plan_year = string.Empty,
                strunit_code = string.Empty,
                stractivity_code = string.Empty,
                strplan_code = string.Empty,
                strwork_code = string.Empty,
                strfund_code = string.Empty,
                strActive = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty;
            string strScript = string.Empty;
            cBudget_plan oBudget_plan = new cBudget_plan();
            DataSet ds = new DataSet();
            try
            {

                #region set Data
                strbudget_plan_code = txtbudget_plan_code.Text.Trim();
                strbudget_plan_year = cboYear.SelectedValue;
                strunit_code = cboUnit.SelectedValue;
                stractivity_code = cboActivity.SelectedValue;
                strplan_code = cboPlan_code.SelectedValue;
                strwork_code = cboWork.SelectedValue;
                strfund_code = cboFund.SelectedValue;
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

                string strCheckAdd = " and budget_plan_code = '" + strbudget_plan_code.Trim() + "' ";
                if (!oBudget_plan.SP_BUDGET_PLAN_SEL(strCheckAdd, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region check dup
                        string strCheckDup = string.Empty;
                        strCheckDup = " and unit_code='" + strunit_code + "' " +
                                                      " and activity_code='" + stractivity_code + "' " +
                                                      " and plan_code='" + strplan_code + "' " +
                                                      " and work_code='" + strwork_code + "' " +
                                                      " and fund_code='" + strfund_code + "' " +
                                                      " and budget_plan_code <>'" + strbudget_plan_code + "' ";
                        if (!oBudget_plan.SP_BUDGET_PLAN_SEL(strCheckDup, ref ds, ref strMessage))
                        {
                            lblError.Text = strMessage;
                        }
                        else
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                strScript =
                                    "alert(\"ไม่สามารถแก้ไขข้อมูลได้ เนื่องจาก ข้อมูลผังงบประมาณซ้ำ\");\n";
                                blnDup = true;
                            }
                        }
                        #endregion
                        #region edit
                        if (!blnDup)
                        {
                            if (oBudget_plan.SP_BUDGET_PLAN_UPD(strbudget_plan_code, strbudget_plan_year, 
                                strunit_code, stractivity_code,strplan_code, strwork_code, strfund_code,
                                strActive, strUpdatedBy, this.BudgetType, ref strMessage))
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
                        strCheckDup = " and unit_code='" + strunit_code + "' " +
                                                      " and activity_code='" + stractivity_code + "' " +
                                                      " and plan_code='" + strplan_code + "' " +
                                                      " and work_code='" + strwork_code + "' " +
                                                      " and fund_code='" + strfund_code + "' ";
                        if (!oBudget_plan.SP_BUDGET_PLAN_SEL(strCheckDup, ref ds, ref strMessage))
                        {
                            lblError.Text = strMessage;
                        }
                        else
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                strScript =
                                    "alert(\"ไม่สามารถแก้ไขข้อมูลได้ เนื่องจาก ข้อมูลผังงบประมาณซ้ำ\");\n";
                                blnDup = true;
                            }
                        }
                        #endregion
                        #region insert
                        if (!blnDup)
                        {
                            if (oBudget_plan.SP_BUDGET_PLAN_INS(strbudget_plan_year, strunit_code, stractivity_code,strplan_code,
                                strwork_code, strfund_code, strActive, strCreatedBy, this.BudgetType, ref strMessage))
                            {
                                string strGetcode = " and unit_code='" + strunit_code + "' " +
                              " and activity_code='" + stractivity_code + "' " +
                              " and plan_code='" + strplan_code + "' " +
                              " and work_code='" + strwork_code + "' " +
                              " and fund_code='" + strfund_code + "' ";

                                if (!oBudget_plan.SP_BUDGET_PLAN_SEL(strGetcode, ref ds, ref strMessage))
                                {
                                    lblError.Text = strMessage;
                                }
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    strbudget_plan_code = ds.Tables[0].Rows[0]["budget_plan_code"].ToString();
                                }
                                ViewState["budget_plan_code"] = strbudget_plan_code;
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
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oBudget_plan.Dispose();
            }
            return blnResult;
        }

        private void imgSaveOnly_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (saveData())
            {
                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    ClearText();
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
            //if (saveData())
            //{
            //    ClearText();
            //    string strScript1 = "RefreshMain('" + ViewState["page"].ToString() + "');";
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
            //}
        }

        private void setData()
        {
            cBudget_plan oBudget_plan = new cBudget_plan();
            DataSet ds = new DataSet();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strbudget_plan_code = string.Empty,
                strdirector_code = string.Empty,
                strunit_code = string.Empty,
                strbudget_code = string.Empty,
                strproduce_code = string.Empty,
                stractivity_code = string.Empty,
                strplan_code = string.Empty,
                strwork_code = string.Empty,
                strfund_code = string.Empty,
                strYear = string.Empty,
                strC_active = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty,
                strCreatedDate = string.Empty,
                strUpdatedDate = string.Empty;

            try
            {
                strCriteria = " and budget_plan_code = '" + ViewState["budget_plan_code"].ToString() + "' ";
                if (!oBudget_plan.SP_BUDGET_PLAN_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region get Data
                        strbudget_plan_code = ds.Tables[0].Rows[0]["budget_plan_code"].ToString();
                        strdirector_code = ds.Tables[0].Rows[0]["director_code"].ToString();
                        strunit_code = ds.Tables[0].Rows[0]["unit_code"].ToString();
                        strbudget_code = ds.Tables[0].Rows[0]["budget_code"].ToString();
                        strproduce_code = ds.Tables[0].Rows[0]["produce_code"].ToString();
                        stractivity_code = ds.Tables[0].Rows[0]["activity_code"].ToString();
                        strplan_code = ds.Tables[0].Rows[0]["plan_code"].ToString();
                        strwork_code = ds.Tables[0].Rows[0]["work_code"].ToString();
                        strfund_code = ds.Tables[0].Rows[0]["fund_code"].ToString();
                        strYear = ds.Tables[0].Rows[0]["budget_plan_year"].ToString();
                        strC_active = ds.Tables[0].Rows[0]["c_active"].ToString();
                        strCreatedBy = ds.Tables[0].Rows[0]["c_created_by"].ToString();
                        strUpdatedBy = ds.Tables[0].Rows[0]["c_updated_by"].ToString();
                        strCreatedDate = ds.Tables[0].Rows[0]["d_created_date"].ToString();
                        strUpdatedDate = ds.Tables[0].Rows[0]["d_updated_date"].ToString();
                        #endregion

                        #region set Control

                        InitcboYear();
                        if (cboYear.Items.FindByValue(strYear) != null)
                        {
                            cboYear.SelectedIndex = -1;
                            cboYear.Items.FindByValue(strYear).Selected = true;
                        }

                        InitcboDirector();
                        if (cboDirector.Items.FindByValue(strdirector_code) != null)
                        {
                            cboDirector.SelectedIndex = -1;
                            cboDirector.Items.FindByValue(strdirector_code).Selected = true;
                        }

                        InitcboUnit();
                        if (cboUnit.Items.FindByValue(strunit_code) != null)
                        {
                            cboUnit.SelectedIndex = -1;
                            cboUnit.Items.FindByValue(strunit_code).Selected = true;
                        }

                        InitcboBudget();
                        if (cboBudget.Items.FindByValue(strbudget_code) != null)
                        {
                            cboBudget.SelectedIndex = -1;
                            cboBudget.Items.FindByValue(strbudget_code).Selected = true;
                        }

                        InitcboProduce();
                        if (cboProduce.Items.FindByValue(strproduce_code) != null)
                        {
                            cboProduce.SelectedIndex = -1;
                            cboProduce.Items.FindByValue(strproduce_code).Selected = true;
                        }

                        InitcboActivity();
                        if (cboActivity.Items.FindByValue(stractivity_code) != null)
                        {
                            cboActivity.SelectedIndex = -1;
                            cboActivity.Items.FindByValue(stractivity_code).Selected = true;
                        }

                        InitcboPlan();
                        if (cboPlan_code.Items.FindByValue(strplan_code) != null)
                        {
                            cboPlan_code.SelectedIndex = -1;
                            cboPlan_code.Items.FindByValue(strplan_code).Selected = true;
                        }

                        InitcboWork();
                        if (cboWork.Items.FindByValue(strwork_code) != null)
                        {
                            cboWork.SelectedIndex = -1;
                            cboWork.Items.FindByValue(strwork_code).Selected = true;
                        }

                        InitcboFund();
                        if (cboFund.Items.FindByValue(strfund_code) != null)
                        {
                            cboFund.SelectedIndex = -1;
                            cboFund.Items.FindByValue(strfund_code).Selected = true;
                        }

                        cboYear.Enabled = false;

                        if (strC_active.Equals("Y"))
                        {
                            txtbudget_plan_code.ReadOnly = true;
                            txtbudget_plan_code.Text = strbudget_plan_code;
                            txtbudget_plan_code.CssClass = "textboxdis";
                            chkStatus.Checked = true;
                        }
                        else
                        {
                            txtbudget_plan_code.ReadOnly = true;
                            txtbudget_plan_code.Text = strbudget_plan_code;
                            txtbudget_plan_code.CssClass = "textboxdis";
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

        private void ClearText()
        {
            txtbudget_plan_code.Text = string.Empty;
            txtbudget_plan_code.ReadOnly = true;
            txtbudget_plan_code.CssClass = "textboxdis";
            chkStatus.Checked = true;
        }

        protected void cboDirector_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboUnit();
        }

        protected void cboBudget_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboProduce();
        }

        protected void cboProduce_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboActivity();
        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboDirector();
        }

    }
}