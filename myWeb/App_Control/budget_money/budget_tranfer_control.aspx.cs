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

namespace myWeb.App_Control.budget_money
{
    public partial class budget_tranfer_control : PageBase
    {
        #region private data
        private bool[] blnAccessRight = new bool[5] { false, false, false, false, false };
        private string strPrefixCtr = "ctl00$ContentPlaceHolder1$";
        private string strPrefixCtr_main = "ctl00$ContentPlaceHolder1";
        #endregion

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

        protected void Page_Load(object sender, System.EventArgs e)
        {
            lblError.Text = "";
            if (!IsPostBack)
            {
                #region set QueryString

                if (Request.QueryString["show"] != null)
                {
                    ViewState["show"] = Request.QueryString["show"].ToString();
                }
                else
                {
                    ViewState["show"] = "1";
                }

                if (Request.QueryString["year"] != null)
                {
                    ViewState["year"] = Request.QueryString["year"].ToString();
                    txtyear.Text = ViewState["year"].ToString();
                }

                if (Request.QueryString["pay_year"] != null)
                {
                    ViewState["pay_year"] = Request.QueryString["pay_year"].ToString();
                    txtPay_year.Text = ViewState["pay_year"].ToString();
                }

                if (Request.QueryString["pay_month"] != null)
                {
                    ViewState["pay_month"] = Request.QueryString["pay_month"].ToString();
                    InitcboPay_Month();
                    if (cboPay_Month.Items.FindByValue(ViewState["pay_month"].ToString()) != null)
                    {
                        cboPay_Month.SelectedIndex = -1;
                        cboPay_Month.Items.FindByValue(ViewState["pay_month"].ToString()).Selected = true;
                    }
                }

                if (Request.QueryString["budget_plan_code"] != null)
                {
                    ViewState["budget_plan_code"] = Request.QueryString["budget_plan_code"].ToString();
                    txtbudget_plan_code_ds.Text = ViewState["budget_plan_code"].ToString();
                }

                if (Request.QueryString["lot_code"] != null)
                {
                    ViewState["lot_code"] = Request.QueryString["lot_code"].ToString();
                    InitcboLot();
                    if (cboLot.Items.FindByValue(ViewState["lot_code"].ToString()) != null)
                    {
                        cboLot.SelectedIndex = -1;
                        cboLot.Items.FindByValue(ViewState["lot_code"].ToString()).Selected = true;
                    }
                }

                if (Request.QueryString["item_group_code"] != null)
                {
                    ViewState["item_group_code"] = Request.QueryString["item_group_code"].ToString();
                }

                if (Request.QueryString["budget_item_group_code"] != null)
                {
                    ViewState["budget_item_group_code"] = Request.QueryString["budget_item_group_code"].ToString();
                    InitcboItem_group();
                    if (cboItem_group.Items.FindByValue(ViewState["budget_item_group_code"].ToString()) != null)
                    {
                        cboItem_group.SelectedIndex = -1;
                        cboItem_group.Items.FindByValue(ViewState["budget_item_group_code"].ToString()).Selected = true;
                    }
                }


                if (Request.QueryString["mode"] != null)
                {
                    ViewState["mode"] = Request.QueryString["mode"].ToString();
                }

                if (Request.QueryString["page"] != null)
                {
                    ViewState["page"] = Request.QueryString["page"].ToString();
                }

                if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    //setData();
                }
                else
                {


                }


                #endregion

                imgList_budget_plan.Attributes.Add("onclick", "OpenPopUp('950px','500px','94%','ค้นหาข้อมูลผังงบประมาณประจำปี' ,'../lov/budget_plan_lov.aspx?" +
                                        "budget_plan_code='+document.forms[0]." + strPrefixCtr_main + "$txtbudget_plan_code.value+'" +
                                        "&activity_name='+document.forms[0]." + strPrefixCtr_main + "$txtactivity_name.value+'" +
                                        "&plan_name='+document.forms[0]." + strPrefixCtr_main + "$txtplan_name.value+'" +
                                        "&work_name='+document.forms[0]." + strPrefixCtr_main + "$txtwork_name.value+'" +
                                        "&fund_name='+document.forms[0]." + strPrefixCtr_main + "$txtfund_name.value+'" +
                                        "&unit_name='+document.forms[0]." + strPrefixCtr_main + "$txtunit_name.value+'" +
                                        "&ctrl1=" + txtbudget_plan_code.ClientID +
                                        "&ctrl4=" + txtactivity_name.ClientID +
                                        "&ctrl5=" + txtplan_name.ClientID +
                                        "&ctrl6=" + txtwork_name.ClientID +
                                        "&ctrl7=" + txtfund_name.ClientID +
                                        "&ctrl10=" + txtunit_name.ClientID +
                                        "&budget_type=" + this.BudgetType +
                                        "&from_page=budget_tranfer_control&show=3', '3');return false;");

                imgClear_budget_plan.Attributes.Add("onclick",
                                                                "document.forms[0]." + strPrefixCtr_main + "$txtbudget_plan_code.value='';" +
                                                                "document.forms[0]." + strPrefixCtr_main + "$txtactivity_name.value='';" +
                                                                "document.forms[0]." + strPrefixCtr_main + "$txtplan_name.value='';" +
                                                                "document.forms[0]." + strPrefixCtr_main + "$txtwork_name.value='';" +
                                                                "document.forms[0]." + strPrefixCtr_main + "$txtfund_name.value='';" +
                                                                "document.forms[0]." + strPrefixCtr_main + "$txtunit_name.value='';" +
                                                                 "window.__doPostBack('ctl00$ContentPlaceHolder1$BtnR1','');" +
                                                                "return false;");

                #region check dup
                string strCheckDup = string.Empty;
                string strMessage = string.Empty;
                cBudget_money oBudget_money = new cBudget_money();
                DataSet ds = new DataSet();
                strCheckDup = " and budget_year = '" + ViewState["year"].ToString() + "' " +
                                              " and budget_tranfer_year = '" + ViewState["pay_year"].ToString() + "' " +
                                              " and budget_tranfer_month = '" + ViewState["pay_month"].ToString() + "' " +
                                              " and budget_plan_code_ds = '" + ViewState["budget_plan_code"].ToString() + "' " +
                                              " and lot_code_ds = '" + ViewState["lot_code"].ToString() + "' " +
                                              " and budget_item_group_code_ds = '" + ViewState["budget_item_group_code"].ToString() + "' ";
                if (!oBudget_money.SP_BUDGET_TRANFER_SEL(strCheckDup, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ViewState["mode"] = "edit";
                        txtbudget_tranfer_doc.Text = ds.Tables[0].Rows[0]["budget_tranfer_doc"].ToString();
                        setData();
                    }
                    else
                    {
                        ViewState["mode"] = "add";
                        setDS_Data();
                        txtbudget_tranfer_date.Text = cCommon.CheckDate(DateTime.Now.ToShortDateString());
                    }
                }
                #endregion

                imgSaveOnly.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgSaveOnly"].Rows[0]["title"].ToString());


                BtnR1.Style.Add("display", "none");
            }
        }

        #region private function

        private void InitcboLot()
        {
            cLot oLot = new cLot();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strLot_code = string.Empty;
            string strLot = cboLot.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and c_active='Y' " +
                                    " and lot_code in (Select lot_code from dbo.view_budget_money Where budget_plan_code='" + txtbudget_plan_code.Text + "')";
            if (oLot.SP_SEL_LOT(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboLot.Items.Clear();
                cboLot.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboLot.Items.Add(new ListItem(dt.Rows[i]["lot_name"].ToString(), dt.Rows[i]["lot_code"].ToString()));
                }
                if (cboLot.Items.FindByValue(strLot) != null)
                {
                    cboLot.SelectedIndex = -1;
                    cboLot.Items.FindByValue(strLot).Selected = true;
                }
                InitcboItem_group();
            }
        }

        private void InitcboItem_group()
        {
            cItem_group oItem_group = new cItem_group();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strItem_group_code = string.Empty;
            string strlot_code = cboLot.SelectedValue;
            strItem_group_code = cboItem_group.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and lot_code='" + strlot_code + "' and lot_code <> '' and budget_plan_code='" + txtbudget_plan_code.Text + "' ";

            if (oItem_group.SP_BUDGET_MONEY_ITEM_GROUP_SEL(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboItem_group.Items.Clear();
                cboItem_group.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboItem_group.Items.Add(new ListItem(dt.Rows[i]["item_group_name"].ToString(), dt.Rows[i]["item_group_code"].ToString()));
                }
                if (cboItem_group.Items.FindByValue(strItem_group_code) != null)
                {
                    cboItem_group.SelectedIndex = -1;
                    cboItem_group.Items.FindByValue(strItem_group_code).Selected = true;
                }
            }
        }

        private void InitcboPay_Month()
        {
            string strMonth = string.Empty;
            strMonth = cboPay_Month.SelectedValue;
            if (strMonth.Equals(""))
            {
                if (DateTime.Now.Month < 10)
                {
                    strMonth = "0" + DateTime.Now.Month.ToString();
                }
                else
                {
                    strMonth = DateTime.Now.Month.ToString();
                }
            }
            DataTable odt;
            int i;
            cboPay_Month.Items.Clear();
            odt = ((DataSet)Application["xmlconfig"]).Tables["cboMonth"];
            for (i = 0; i <= odt.Rows.Count - 1; i++)
            {
                cboPay_Month.Items.Add(new ListItem(odt.Rows[i]["Text"].ToString(), odt.Rows[i]["Value"].ToString()));
            }
            if (cboPay_Month.Items.FindByValue(strMonth) != null)
            {
                cboPay_Month.SelectedIndex = -1;
                cboPay_Month.Items.FindByValue(strMonth).Selected = true;
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
            #region declare Variable
            bool blnResult = false;
            string strMessage = string.Empty;
            string pbudget_tranfer_doc;
            string pbudget_tranfer_date;
            string pbudget_year;
            string pbudget_tranfer_year;
            string pbudget_tranfer_month;
            string pbudget_plan_code_sr;
            string plot_code_sr;
            string pitem_group_code_sr;
            string pbudget_tranfer_money_sr;
            string pbudget_plan_code_ds;
            string plot_code_ds;
            string pitem_group_code_ds;
            string pbudget_tranfer_money_ds;
            string pbudget_tranfer_money;
            string pc_active;
            string strCreatedBy;
            string strUpdatedBy;
            string strScript = string.Empty;
            string pbudget_item_group_code_sr = string.Empty;
            string pbudget_item_group_code_ds = string.Empty;
            cBudget_money oBudget_money = new cBudget_money();
            cItem oItem = new cItem();
            DataSet ds = new DataSet();
            DataSet dsItem = new DataSet();
            #endregion
            try
            {
                #region set Data
                strCreatedBy = Session["username"].ToString();
                strUpdatedBy = Session["username"].ToString();
                pbudget_tranfer_doc = txtbudget_tranfer_doc.Text;
                pbudget_tranfer_date = txtbudget_tranfer_date.Text;
                pbudget_year = txtyear.Text;
                pbudget_tranfer_year = txtPay_year.Text;
                pbudget_tranfer_month = cboPay_Month.SelectedValue;
                pbudget_plan_code_sr = txtbudget_plan_code.Text;
                plot_code_sr = cboLot.SelectedValue;

                pbudget_item_group_code_sr = cboItem_group.SelectedValue;
                pitem_group_code_sr = pbudget_item_group_code_sr;
                string strCriteria = " and item_code = '" + pbudget_item_group_code_sr + "' ";
                if (oItem.SP_ITEM_SEL(strCriteria, ref ds, ref strMessage))
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        pitem_group_code_sr = ds.Tables[0].Rows[0]["item_group_code"].ToString();
                    }
                }
                oItem.Dispose();

                pbudget_tranfer_money_sr = txtbudget_tranfer_money_sr.Value.ToString();
                pbudget_plan_code_ds = txtbudget_plan_code_ds.Text;
                plot_code_ds = ViewState["lot_code"].ToString();
                pitem_group_code_ds = ViewState["item_group_code"].ToString();
                pbudget_item_group_code_ds = ViewState["budget_item_group_code"].ToString();

                pbudget_tranfer_money_ds = txtbudget_tranfer_money_ds.Value.ToString();
                pbudget_tranfer_money = txtbudget_tranfer_money.Value.ToString();



                pc_active = "Y";
                #endregion
                if ((pbudget_plan_code_sr.Equals(pbudget_plan_code_ds)) & 
                    (plot_code_sr.Equals(plot_code_ds)) & 
                    (pitem_group_code_sr.Equals(pitem_group_code_ds)) & 
                    (pbudget_item_group_code_sr.Equals(pbudget_item_group_code_ds)))
                {
                    string strScript1 = "alert('ข้อมูลผังงบประมาณซ้ำโปรดตรวจสอบ!!');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
                }
                else
                {
                    if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                    {
                        #region edit
                        if (oBudget_money.SP_BUDGET_TRANFER_UPD(pbudget_tranfer_doc, pbudget_tranfer_date, pbudget_year, pbudget_tranfer_year, pbudget_tranfer_month, pbudget_plan_code_sr, plot_code_sr,
                                            pitem_group_code_sr, pbudget_tranfer_money_sr, pbudget_plan_code_ds, plot_code_ds, pitem_group_code_ds, pbudget_tranfer_money_ds,
                                            pbudget_tranfer_money, pc_active, strUpdatedBy, pbudget_item_group_code_sr , pbudget_item_group_code_ds,  ref strMessage))
                        {
                            blnResult = true;
                        }
                        else
                        {
                            lblError.Text = strMessage.ToString();
                        }

                        #endregion
                    }
                    else
                    {
                        #region insert
                        if (oBudget_money.SP_BUDGET_TRANFER_INS(pbudget_tranfer_date, pbudget_year, pbudget_tranfer_year, pbudget_tranfer_month, pbudget_plan_code_sr, plot_code_sr,
                                            pitem_group_code_sr, pbudget_tranfer_money_sr, pbudget_plan_code_ds, plot_code_ds, pitem_group_code_ds, pbudget_tranfer_money_ds,
                                            pbudget_tranfer_money, pc_active, strCreatedBy, pbudget_item_group_code_sr, pbudget_item_group_code_ds, ref strMessage))
                        {
                            blnResult = true;
                        }
                        else
                        {
                            lblError.Text = strMessage.ToString();
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
                oBudget_money.Dispose();
            }
            return blnResult;
        }

        private void imgSaveOnly_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (saveData())
            {
                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    string strScript1 = "window.parent.frames['iframeShow1'].__doPostBack('ctl00$ContentPlaceHolder1$BtnR1','');ClosePopUp('2');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
                    string strScript = "window.parent.top.__doPostBack('ctl00$ContentPlaceHolder1$BtnR1','');ClosePopUp('1');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript, true);
                }
                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    string strScript1 = "window.parent.frames['iframeShow1'].__doPostBack('ctl00$ContentPlaceHolder1$BtnR1','');ClosePopUp('2');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
                }
                MsgBox("บันทึกข้อมูลสมบูรณ์");
            }
        }

        private void setData()
        {
            string strCriteria = string.Empty;
            string strMessage = string.Empty;
            cBudget_money oBudget_money = new cBudget_money();
            DataSet ds = new DataSet();
            try
            {
                strCriteria = " and budget_tranfer_doc = '" + txtbudget_tranfer_doc.Text + "' ";
                if (!oBudget_money.SP_BUDGET_TRANFER_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region set Control
                        txtbudget_tranfer_doc.Text = ds.Tables[0].Rows[0]["budget_tranfer_doc"].ToString();
                        txtbudget_tranfer_date.Text = cCommon.CheckDate(ds.Tables[0].Rows[0]["budget_tranfer_date"].ToString());
                        txtbudget_plan_code_ds.Text = ds.Tables[0].Rows[0]["budget_plan_code_ds"].ToString();
                        txtunit_name_ds.Text = ds.Tables[0].Rows[0]["unit_name_ds"].ToString();
                        txtactivity_name_ds.Text = ds.Tables[0].Rows[0]["activity_name_ds"].ToString();
                        txtplan_name_ds.Text = ds.Tables[0].Rows[0]["plan_name_ds"].ToString();
                        txtwork_name_ds.Text = ds.Tables[0].Rows[0]["work_name_ds"].ToString();
                        txtfund_name_ds.Text = ds.Tables[0].Rows[0]["fund_name_ds"].ToString();
                        txtlot_name_ds.Text = ds.Tables[0].Rows[0]["lot_name_ds"].ToString();
                        txtitem_group_name_ds.Text = ds.Tables[0].Rows[0]["budget_item_group_name_ds"].ToString();
                        txtbudget_tranfer_money_ds.Value = ds.Tables[0].Rows[0]["budget_tranfer_money_ds"].ToString();
                        //- - - - - - - - - - -
                        txtbudget_plan_code.Text = ds.Tables[0].Rows[0]["budget_plan_code_sr"].ToString();
                        txtunit_name.Text = ds.Tables[0].Rows[0]["unit_name_sr"].ToString();
                        txtactivity_name.Text = ds.Tables[0].Rows[0]["activity_name_sr"].ToString();
                        txtplan_name.Text = ds.Tables[0].Rows[0]["plan_name_sr"].ToString();
                        txtwork_name.Text = ds.Tables[0].Rows[0]["work_name_sr"].ToString();
                        txtfund_name.Text = ds.Tables[0].Rows[0]["fund_name_sr"].ToString();
                        string lot_code_sr = ds.Tables[0].Rows[0]["lot_code_sr"].ToString();
                        InitcboLot();
                        if (cboLot.Items.FindByValue(lot_code_sr) != null)
                        {
                            cboLot.SelectedIndex = -1;
                            cboLot.Items.FindByValue(lot_code_sr).Selected = true;
                        }
                        string strbudget_item_group_code_sr = ds.Tables[0].Rows[0]["budget_item_group_code_sr"].ToString();
                        InitcboItem_group();
                        if (cboItem_group.Items.FindByValue(strbudget_item_group_code_sr) != null)
                        {
                            cboItem_group.SelectedIndex = -1;
                            cboItem_group.Items.FindByValue(strbudget_item_group_code_sr).Selected = true;
                        }

                        txtbudget_tranfer_money_sr.Value = ds.Tables[0].Rows[0]["budget_tranfer_money_sr"].ToString();
                        txtbudget_tranfer_money.Value = ds.Tables[0].Rows[0]["budget_tranfer_money"].ToString();

                        txtUpdatedBy.Text = ds.Tables[0].Rows[0]["c_updated_by"].ToString();
                        txtUpdatedDate.Text = ds.Tables[0].Rows[0]["d_updated_date"].ToString();
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        private void setDS_Data()
        {
            cBudget_money oBudget_money = new cBudget_money();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string strbudget_plan_code = txtbudget_plan_code_ds.Text.Trim(),
            strYear = txtyear.Text;
            string strlot_code = ViewState["lot_code"].ToString();
            string stritem_group_code = ViewState["item_group_code"].ToString();
            string strbudget_item_group_code = ViewState["budget_item_group_code"].ToString();
            try
            {
                strCriteria = " and budget_plan_code = '" + strbudget_plan_code + "' " +
                                        " and budget_money_year = '" + strYear + "' " +
                                        " and lot_code = '" + strlot_code + "' " +
                                        " and item_group_code = '" + stritem_group_code + "' " +
                                        " and budget_item_group_code = '" + strbudget_item_group_code + "' ";
                if (!oBudget_money.SP_BUDGET_MONEY_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region get Data
                        txtunit_name_ds.Text = ds.Tables[0].Rows[0]["unit_name"].ToString();
                        txtactivity_name_ds.Text = ds.Tables[0].Rows[0]["activity_name"].ToString();
                        txtplan_name_ds.Text = ds.Tables[0].Rows[0]["plan_name"].ToString();
                        txtwork_name_ds.Text = ds.Tables[0].Rows[0]["work_name"].ToString();
                        txtfund_name_ds.Text = ds.Tables[0].Rows[0]["fund_name"].ToString();
                        txtlot_name_ds.Text = ds.Tables[0].Rows[0]["lot_name"].ToString();
                        txtitem_group_name_ds.Text = ds.Tables[0].Rows[0]["budget_item_group_name"].ToString();
                        txtbudget_tranfer_money_ds.Value = ds.Tables[0].Rows[0]["budget_money_subremain"].ToString();
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        protected void cboLot_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboItem_group();
            setSR_Data();
        }

        protected void BtnR1_Click(object sender, EventArgs e)
        {
            InitcboLot();
            txtbudget_tranfer_money_sr.Value = 0;
            txtbudget_tranfer_money.Value = 0;
        }

        protected void cboItem_group_SelectedIndexChanged(object sender, EventArgs e)
        {
            setSR_Data();
        }

        private void setSR_Data()
        {
            cBudget_money oBudget_money = new cBudget_money();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string strbudget_plan_code = txtbudget_plan_code.Text.Trim(),
            strYear = txtyear.Text;
            string strlot_code = cboLot.SelectedValue;
            string stritem_group_code = cboItem_group.SelectedValue;
            try
            {
                strCriteria = " and budget_plan_code = '" + strbudget_plan_code + "' " +
                                        " and budget_money_year = '" + strYear + "' " +
                                        " and lot_code = '" + strlot_code + "' " +
                                        " and budget_item_group_code = '" + stritem_group_code + "' ";

                if (!oBudget_money.SP_BUDGET_MONEY_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtbudget_tranfer_money_sr.Value = ds.Tables[0].Rows[0]["budget_money_subremain"].ToString();
                    }
                    else
                    {
                        txtbudget_tranfer_money_sr.Value = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ViewState["mode"] = "add";
            txtbudget_plan_code.Text = string.Empty;
            txtunit_name.Text = string.Empty;
            txtactivity_name.Text = string.Empty;
            txtplan_name.Text = string.Empty;
            txtwork_name.Text = string.Empty;
            txtfund_name.Text = string.Empty;
            string lot_code_sr = string.Empty;
            InitcboLot();
            if (cboLot.Items.FindByValue(lot_code_sr) != null)
            {
                cboLot.SelectedIndex = -1;
                cboLot.Items.FindByValue(lot_code_sr).Selected = true;
            }
            string item_group_code_sr = string.Empty;
            InitcboItem_group();
            if (cboItem_group.Items.FindByValue(item_group_code_sr) != null)
            {
                cboItem_group.SelectedIndex = -1;
                cboItem_group.Items.FindByValue(item_group_code_sr).Selected = true;
            }

            txtbudget_tranfer_money_sr.Value = 0;

        }



    }
}