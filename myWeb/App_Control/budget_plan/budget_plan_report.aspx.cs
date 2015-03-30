using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using myDLL;

namespace myWeb.App_Control.budget_plan
{
    public partial class budget_plan_report : PageBase
    {
        #region private data
        private bool[] blnAccessRight = new bool[5] { false, false, false, false, false };
        private string strPrefixCtr = "ctl00$ASPxRoundPanel1$ASPxRoundPanel2$ContentPlaceHolder1$";

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitcboRound();
                cboDirector.Visible = true;
                cboUnit.Visible = true;
                lblProduce.Visible = false;
                cboProduce.Visible = false;
                lblLot.Visible = false;
                cboLot.Visible = false;
                lblItem_group.Visible = false;
                cboItem_group.Visible = false;

                lblBudget.Visible = false;
                cboBudget.Visible = false;
                lblProduce.Visible = false;
                cboProduce.Visible = false;
                lblActivity.Visible = false;
                cboActivity.Visible = false;

                imgList_item.Visible = false;
                imgClear_item.Visible = false;
                txtitem_code.Visible = false;
                txtitem_name.Visible = false;

                lblFund.Visible = false;
                cboFund.Visible = false;

                imgList_item.Attributes.Add("onclick", "OpenPopUp('800px','400px','93%','ค้นหาข้อมูลรายได้/ค่าใช้จ่าย' ,'../lov/item_lov.aspx?year='+document.forms[0]." + strPrefixCtr +
                                                           "cboYear.options[document.forms[0]." + strPrefixCtr + "cboYear.selectedIndex].value+" +
                                                           "'&item_code='+document.forms[0]." + strPrefixCtr + "txtitem_code.value+" +
                                                           "'&item_name='+document.forms[0]." + strPrefixCtr + "txtitem_name.value+" +
                                                           "'&ctrl1=" + txtitem_code.ClientID + "&ctrl2=" + txtitem_name.ClientID + "&from=report', '1');return false;");

                imgClear_item.Attributes.Add("onclick", "document.forms[0]." + strPrefixCtr + "txtitem_code.value='';" +
                                        "document.forms[0]." + strPrefixCtr + "txtitem_name.value='';return false;");

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
            InitcboDirector();
            InitcboBudget();
            InitcboLot();
            InitcboItem_group();
            InitcboFund();
        }

        private void InitcboRound()
        {
            cPayment_round oPayment_round = new cPayment_round();
            DataSet ds = new DataSet();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strYear = string.Empty;
            string strPay_Month = string.Empty;
            string strPay_Year = string.Empty;
            try
            {
                strCriteria = " and round_status= 'O' ";
                if (!oPayment_round.SP_PAYMENT_ROUND_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region get Data
                        strYear = ds.Tables[0].Rows[0]["payment_year"].ToString();
                        strPay_Month = ds.Tables[0].Rows[0]["pay_month"].ToString();
                        strPay_Year = ds.Tables[0].Rows[0]["pay_year"].ToString();
                        #endregion
                    }
                    else
                    {
                        #region get Data
                        strYear = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["yearnow"].ToString();
                        if (DateTime.Now.Year < 2200)
                        {
                            strPay_Year = (DateTime.Now.Year + 543).ToString();
                        }
                        if (DateTime.Now.Month < 10)
                            strPay_Month = "0" + DateTime.Now.Month;
                        else
                            strPay_Month = DateTime.Now.Month.ToString();
                        #endregion
                    }

                    #region set Control
                    InitcboYear();
                    if (cboYear.Items.FindByValue(strYear) != null)
                    {
                        cboYear.SelectedIndex = -1;
                        cboYear.Items.FindByValue(strYear).Selected = true;
                    }

                    InitcboPay_Month();
                    if (cboPay_Month.Items.FindByValue(strPay_Month) != null)
                    {
                        cboPay_Month.SelectedIndex = -1;
                        cboPay_Month.Items.FindByValue(strPay_Month).Selected = true;
                    }

                    InitcboPay_Year();
                    if (cboPay_Year.Items.FindByValue(strPay_Year) != null)
                    {
                        cboPay_Year.SelectedIndex = -1;
                        cboPay_Year.Items.FindByValue(strPay_Year).Selected = true;
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
                oPayment_round.Dispose();
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

        private void InitcboPay_Year()
        {
            string strYear = string.Empty;
            strYear = cboPay_Year.SelectedValue;
            if (strYear.Equals(""))
            {
                if (DateTime.Now.Year < 2200)
                {
                    strYear = (DateTime.Now.Year + 543).ToString();
                }
                else
                {
                    strYear = DateTime.Now.Year.ToString();
                }
            }
            DataTable odt;
            int i;
            cboPay_Year.Items.Clear();
            odt = ((DataSet)Application["xmlconfig"]).Tables["cboYear"];
            for (i = 0; i <= odt.Rows.Count - 1; i++)
            {
                cboPay_Year.Items.Add(new ListItem(odt.Rows[i]["Text"].ToString(), odt.Rows[i]["Value"].ToString()));
            }
            if (cboPay_Year.Items.FindByValue(strYear) != null)
            {
                cboPay_Year.SelectedIndex = -1;
                cboPay_Year.Items.FindByValue(strYear).Selected = true;
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
            if (DirectorLock == "Y")
            {
                strCriteria += " and substring(director_code,4,2) = substring('" + DirectorCode + "',4,2) ";
            }
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
                cboDirector.Items.Add(new ListItem("---- เลือกข้อมูลทั้งหมด ----", ""));
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
                cboUnit.Items.Add(new ListItem("---- เลือกข้อมูลทั้งหมด ----", ""));
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
                cboBudget.Items.Add(new ListItem("---- เลือกข้อมูลทั้งหมด ----", ""));
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
            strCriteria = " and  produce.produce_year='" + cboYear.SelectedItem.Value + "' ";
            strCriteria = " and  produce.c_active='Y' ";
            strCriteria = strCriteria + "  And produce.budget_type ='" + this.BudgetType + "' ";

            if (!strbudget_code.Equals(""))
            {
                strCriteria = strCriteria + " and produce.budget_code= '" + strbudget_code + "' ";
            }

            if (oProduce.SP_SEL_PRODUCE(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboProduce.Items.Clear();
                cboProduce.Items.Add(new ListItem("---- เลือกข้อมูลทั้งหมด ----", ""));
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
            strCriteria += "  and  activity.activity_year='" + cboYear.SelectedItem.Value + "' ";
            strCriteria += "  And activity.budget_type ='" + this.BudgetType + "' ";
            if (!strbudget_code.Equals(""))
            {
                strCriteria = strCriteria + " and  produce.budget_code= '" + strbudget_code + "' ";
            }

            if (!strproduce_code.Equals(""))
            {
                strCriteria = strCriteria + " and activity.produce_code= '" + strproduce_code + "' ";
            }


            if (oActivity.SP_SEL_ACTIVITY(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboActivity.Items.Clear();
                cboActivity.Items.Add(new ListItem("---- เลือกข้อมูลทั้งหมด ----", ""));
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
            strCriteria = " and c_active='Y' ";
            strCriteria += " and lot_year='" + cboYear.SelectedValue + "' ";
            strCriteria += "  And budget_type ='" + this.BudgetType + "' ";

            if (oLot.SP_SEL_LOT(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboLot.Items.Clear();
                cboLot.Items.Add(new ListItem("---- เลือกข้อมูลทั้งหมด ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboLot.Items.Add(new ListItem(dt.Rows[i]["lot_name"].ToString(), dt.Rows[i]["lot_code"].ToString()));
                }
                if (cboLot.Items.FindByValue(strLot) != null)
                {
                    cboLot.SelectedIndex = -1;
                    cboLot.Items.FindByValue(strLot).Selected = true;
                }
            }
        }

        //private void InitcboItem_group()
        //{
        //    cItem_group oItem_group = new cItem_group();
        //    string strMessage = string.Empty,
        //                strCriteria = string.Empty,
        //                strItem_group_code = string.Empty;
        //    strItem_group_code = cboItem_group.SelectedValue;
        //    int i;
        //    DataSet ds = new DataSet();
        //    DataTable dt = new DataTable();
        //    strCriteria = " and c_active='Y' ";
        //    strCriteria += " and item_group_year = '" + cboYear.SelectedValue + "' ";
        //    strCriteria += " and budget_type = '"  + this.BudgetType + "' ";
        //    if (oItem_group.SP_SEL_item_group(strCriteria, ref ds, ref strMessage))
        //    {
        //        dt = ds.Tables[0];
        //        cboItem_group.Items.Clear();
        //        cboItem_group.Items.Add(new ListItem("---- เลือกข้อมูลทั้งหมด ----", ""));
        //        for (i = 0; i <= dt.Rows.Count - 1; i++)
        //        {
        //            cboItem_group.Items.Add(new ListItem(dt.Rows[i]["Item_group_name"].ToString(), dt.Rows[i]["Item_group_code"].ToString()));
        //        }
        //        if (cboItem_group.Items.FindByValue(strItem_group_code) != null)
        //        {
        //            cboItem_group.SelectedIndex = -1;
        //            cboItem_group.Items.FindByValue(strItem_group_code).Selected = true;
        //        }
        //    }
        //}

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
            //strCriteria = " and lot_code='" + strlot_code + "' and lot_code <> '' and budget_plan_code='" + txtbudget_plan_code.Text + "' ";
            strCriteria = "   And  lot_code='" + strlot_code + "' and lot_code <> '' and item_group_code <> '' ";
            strCriteria += "  And  item_year = '" + cboYear.SelectedValue + "' ";
            strCriteria += "  And  budget_type ='" + this.BudgetType + "' ";
            if (oItem_group.SP_BUDGET_ITEM_GROUP_SEL(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboItem_group.Items.Clear();
                cboItem_group.Items.Add(new ListItem("---- เลือกข้อมูลทั้งหมด ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboItem_group.Items.Add(new ListItem(dt.Rows[i]["budget_item_group_name"].ToString(), dt.Rows[i]["budget_item_group_code"].ToString()));
                }
                if (cboItem_group.Items.FindByValue(strItem_group_code) != null)
                {
                    cboItem_group.SelectedIndex = -1;
                    cboItem_group.Items.FindByValue(strItem_group_code).Selected = true;
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

        protected void imgPrint_Click(object sender, ImageClickEventArgs e)
        {
            string strCriteria = string.Empty;
            string strCriteria2 = string.Empty;
            string strCriteria3 = string.Empty;
            string strYear = string.Empty;
            string strActive = string.Empty;
            string strperson_group_code = string.Empty;

            string strbudget_code = string.Empty;
            string strproduce_code = string.Empty;
            string strunit_code = string.Empty;
            string strdirector_code = string.Empty;
            string stractivity_code = string.Empty;

            string strScript = string.Empty;
            string strPay_Month = string.Empty;
            string strPay_Year = string.Empty;
            string strReport_code = string.Empty;
            string strLot = string.Empty;
            string strItem_group_code = string.Empty;
            string strFund = string.Empty;
            strYear = cboYear.SelectedValue;

            strdirector_code = cboDirector.SelectedValue;
            strunit_code = cboUnit.SelectedValue;
            strbudget_code = cboBudget.SelectedValue;
            strproduce_code = cboProduce.SelectedValue;
            strdirector_code = cboDirector.SelectedValue;
            strunit_code = cboUnit.SelectedValue;
            stractivity_code = cboActivity.SelectedValue;

            strPay_Month = cboPay_Month.SelectedValue;
            strPay_Year = cboPay_Year.SelectedValue;
            strLot = cboLot.SelectedValue;
            strItem_group_code = cboItem_group.SelectedValue;
            strFund = cboFund.SelectedValue;

            if (RadioButtonList1.SelectedValue.Equals("A05") | 
                RadioButtonList1.SelectedValue.Equals("A06") |
                RadioButtonList1.SelectedValue.Equals("A08") |
                RadioButtonList1.SelectedValue.Equals("A06-2") |
                 RadioButtonList1.SelectedValue.Equals("A06-3"))
            {

                strCriteria2 = string.Empty;
                if (!strYear.Equals(""))
                {
                    strCriteria = strCriteria + "  And  [view_payment_all].payment_year = '" + strYear + "'  ";
                }

                if (!strdirector_code.Equals(""))
                {
                    strCriteria = strCriteria + "  And  [view_payment_all].payment_detail_director_code = '" + strdirector_code + "'  ";
                }

                if (!strunit_code.Equals(""))
                {
                    strCriteria = strCriteria + "  And  [view_payment_all].payment_detail_unit_code = '" + strunit_code + "'  ";
                }


                if (!strproduce_code.Equals(""))
                {
                    strCriteria = strCriteria + "  And  ([view_payment_all].payment_detail_produce_code ='" + strproduce_code + "') ";
                }

                if (!strbudget_code.Equals(""))
                {
                    strCriteria = strCriteria + "  And  ([view_payment_all].payment_detail_budget_code ='" + strbudget_code + "') ";
                }

                if (!stractivity_code.Equals(""))
                {
                    strCriteria = strCriteria + "  And  ([view_payment_all].payment_detail_activity_code = '" + stractivity_code + "') ";
                }


                if (cboFund.Visible)
                {
                    if (!strFund.Equals(""))
                    {
                        strCriteria = strCriteria + "  And  [view_payment_all].payment_detail_fund_code = '" + strFund + "'  ";
                    }
                }

                if (cboLot.Visible)
                {
                    if (!strLot.Equals(""))
                    {
                        strCriteria = strCriteria + "  And  [view_payment_all].payment_detail_lot_code = '" + strLot + "'  ";
                    }
                }
                

                string stritem_code = txtitem_code.Text;
                if (!stritem_code.Equals(""))
                {
                    strCriteria = strCriteria + "  And  [view_payment_all].item_code= '" + stritem_code + "' ";
                }

              
                strCriteria = strCriteria + "  And  [view_payment_all].item_type = 'D'  ";
                strCriteria = strCriteria + "  And  [view_payment_all].payment_detail_budget_type = '" + this.BudgetType + "'  ";
                strCriteria2 = strCriteria.Replace("[view_payment_all]", "pay_all");
                if (DirectorLock == "Y")
                {
                    strCriteria += " and substring([view_payment_all].payment_detail_director_code,4,2) = substring('" + DirectorCode + "',4,2) ";
                }

            }
            else
            {


                if (!strYear.Equals(""))
                {
                    strCriteria = strCriteria + "  And  bm.budget_money_year = '" + strYear + "'  ";
                }

                if (!strdirector_code.Equals(""))
                {
                    strCriteria = strCriteria + "  And  bm.director_code = '" + strdirector_code + "'  ";
                }

                if (!strunit_code.Equals(""))
                {
                    strCriteria = strCriteria + "  And  bm.unit_code = '" + strunit_code + "'  ";
                }

                if (cboLot.Visible)
                {
                    if (!strLot.Equals(""))
                    {
                        strCriteria = strCriteria + "  And  bm.lot_code = '" + strLot + "'  ";
                    }
                }

                if (cboItem_group.Visible)
                {
                    if (!strItem_group_code.Equals(""))
                    {
                        if (RadioButtonList1.SelectedValue.Equals("A02") | RadioButtonList1.SelectedValue.Equals("A03") | RadioButtonList1.SelectedValue.Equals("A04"))
                        {
                            strCriteria = strCriteria + "  And  budget_item_group_code = '" + strItem_group_code + "'  ";
                        }
                        else
                        {
                            strCriteria = strCriteria + "  And  bm.item_group_code = '" + strItem_group_code + "'  ";
                        }
                    }
                }
                if (cboBudget.Visible)
                {
                    if (!strbudget_code.Equals(""))
                    {
                        strCriteria = strCriteria + "  And  (bm.budget_code ='" + strbudget_code + "') ";
                    }
                }

                if (cboProduce.Visible)
                {
                    if (!strproduce_code.Equals(""))
                    {
                        strCriteria = strCriteria + "  And  bm.produce_code = '" + strproduce_code + "'  ";
                    }
                }


                strCriteria = strCriteria + "  And  bm.budget_type = '" + this.BudgetType + "'  ";

                if (DirectorLock == "Y")
                {
                    strCriteria += " and substring(bm.director_code,4,2) = substring('" + DirectorCode + "',4,2) ";
                }

                if (RadioButtonList1.SelectedValue.Equals("A07"))
                {
                    strCriteria3 = strCriteria2;
                    strCriteria2 += " and (pd.pay_year+'/'+pd.pay_month) < '" + strPay_Year + "/" + strPay_Month + "' and pd.payment_year='" + strYear + "' ";
                    strCriteria3 += " and (pd.pay_year+'/'+pd.pay_month) = '" + strPay_Year + "/" + strPay_Month + "' and pd.payment_year='" + strYear + "' ";
                }
            }

            if (RadioButtonList1.SelectedValue.Equals("A01"))
            {
                strCriteria = strCriteria.Replace("bm.", "");
                strReport_code = "Rep_budget_plan_cumulative";
            }
            else if (RadioButtonList1.SelectedValue.Equals("A02"))
            {
                strCriteria = strCriteria.Replace("bm.", "");
                strReport_code = "Rep_budget_plan_item_group";
            }
            else if (RadioButtonList1.SelectedValue.Equals("A03"))
            {
                strCriteria = strCriteria.Replace("bm.", "");
                strReport_code = "Rep_budget_plan_lot";
            }
            else if (RadioButtonList1.SelectedValue.Equals("A04"))
            {
                strCriteria = strCriteria.Replace("bm.", "");
                strReport_code = "Rep_budget_plan_produce";
            }
            else if (RadioButtonList1.SelectedValue.Equals("A05"))
            {
                Session["criteria2"] = strCriteria2;
                strReport_code = "Rep_paymentyearbybudgetall";
            }

            else if (RadioButtonList1.SelectedValue.Equals("A06"))
            {
                Session["criteria2"] = strCriteria2;
                strReport_code = "Rep_paymentyearbyproduceall";
            }


            else if (RadioButtonList1.SelectedValue.Equals("A06-2"))
            {
                Session["criteria2"] = strCriteria2;
                strReport_code = "Rep_paymentyearbyquarter";
            }

            else if (RadioButtonList1.SelectedValue.Equals("A06-3"))
            {
                Session["criteria2"] = strCriteria2;
                strReport_code = "Rep_paymentyearbyperson";
            }

            else if (RadioButtonList1.SelectedValue.Equals("A07"))
            {
                Session["criteria2"] = strCriteria2.Replace("bm.", "pd.");
                Session["criteria3"] = strCriteria3.Replace("bm.", "pd.");
                strReport_code = "Rep_payment_openbysum";
            }

            else if (RadioButtonList1.SelectedValue.Equals("A08"))
            {
                Session["criteria2"] = strCriteria2;
                strReport_code = "Rep_paymentyearbyitem_all";
            }


            Session["criteria"] = strCriteria;
            strScript = "windowOpenMaximize(\"../../App_Control/reportsparameter/payment_report_show.aspx?report_code=" + strReport_code +
                                                         "&months=" + cboPay_Month.Text + "&year=" + cboPay_Year.Text + "\", \"_blank\");\n";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript, true);

        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboDirector();
            InitcboBudget();
            InitcboLot();
            InitcboItem_group();
            InitcboProduce();
            InitcboFund();
        }

        protected void cboDirector_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboUnit();
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {

            InitcboYear();

            lblBudget.Visible = false;
            cboBudget.Visible = false;
            lblProduce.Visible = false;
            cboProduce.Visible = false;
            lblActivity.Visible = false;
            cboActivity.Visible = false;

            imgList_item.Visible = false;
            imgClear_item.Visible = false;

            lblitem.Visible = false;
            txtitem_code.Visible = false;
            txtitem_name.Visible = false;

            lblFund.Visible = false;
            cboFund.Visible = false;

            if (RadioButtonList1.SelectedValue == "A01")
            {
                cboDirector.Visible = true;
                cboUnit.Visible = true;
                lblProduce.Visible = false;
                cboProduce.Visible = false;
                lblLot.Visible = false;
                cboLot.Visible = false;
                lblItem_group.Visible = false;
                cboItem_group.Visible = false;
            }
            else if (RadioButtonList1.SelectedValue == "A02")
            {
                cboDirector.Visible = true;
                cboUnit.Visible = true;
                lblProduce.Visible = false;
                cboProduce.Visible = false;
                lblLot.Visible = true;
                cboLot.Visible = true;
                lblItem_group.Visible = true;
                cboItem_group.Visible = true;
            }
            else if (RadioButtonList1.SelectedValue == "A03")
            {
                cboDirector.Visible = true;
                cboUnit.Visible = true;
                lblProduce.Visible = false;
                cboProduce.Visible = false;
                lblLot.Visible = true;
                cboLot.Visible = true;
                lblItem_group.Visible = true;
                cboItem_group.Visible = true;
            }
            else if (RadioButtonList1.SelectedValue == "A04")
            {
                cboDirector.Visible = true;
                cboUnit.Visible = true;
                lblProduce.Visible = true;
                cboProduce.Visible = true;
                lblLot.Visible = true;
                cboLot.Visible = true; ;
                lblItem_group.Visible = true;
                cboItem_group.Visible = true;
            }
            else if (RadioButtonList1.SelectedValue == "A05")
            {
                cboDirector.Visible = true;
                cboUnit.Visible = true;
                lblProduce.Visible = true;
                cboProduce.Visible = true;


                lblLot.Visible = false;
                cboLot.Visible = false;
                lblItem_group.Visible = false;
                cboItem_group.Visible = false;

                lblBudget.Visible = true;
                cboBudget.Visible = true;
                lblProduce.Visible = true;
                cboProduce.Visible = true;
                lblActivity.Visible = true;
                cboActivity.Visible = true;

                imgList_item.Visible = true;
                imgClear_item.Visible = true;
                txtitem_code.Visible = true;
                txtitem_name.Visible = true;

            }
            else if (RadioButtonList1.SelectedValue == "A06" || RadioButtonList1.SelectedValue == "A08")
            {
                cboDirector.Visible = true;
                cboUnit.Visible = true;
                lblProduce.Visible = true;
                cboProduce.Visible = true;


                lblLot.Visible = false;
                cboLot.Visible = false;
                lblItem_group.Visible = false;
                cboItem_group.Visible = false;

                lblBudget.Visible = true;
                cboBudget.Visible = true;
                lblProduce.Visible = true;
                cboProduce.Visible = true;
                lblActivity.Visible = true;
                cboActivity.Visible = true;

                imgList_item.Visible = true;
                imgClear_item.Visible = true;

                lblitem.Visible = true;
                txtitem_code.Visible = true;
                txtitem_name.Visible = true;

            }
            else if (RadioButtonList1.SelectedValue == "A06-2" || RadioButtonList1.SelectedValue == "A06-3")
            {
                cboDirector.Visible = true;
                cboUnit.Visible = true;
                lblProduce.Visible = true;
                cboProduce.Visible = true;


                lblLot.Visible = true;
                cboLot.Visible = true;
                lblItem_group.Visible = false;
                cboItem_group.Visible = false;

                lblBudget.Visible = true;
                cboBudget.Visible = true;
                lblProduce.Visible = true;
                cboProduce.Visible = true;
                lblActivity.Visible = true;
                cboActivity.Visible = true;

                imgList_item.Visible = true;
                imgClear_item.Visible = true;

                lblitem.Visible = true;
                txtitem_code.Visible = true;
                txtitem_name.Visible = true;

                lblFund.Visible = true;
                cboFund.Visible = true;

            }
            else if (RadioButtonList1.SelectedValue == "A07")
            {
                cboDirector.Visible = true;
                cboUnit.Visible = true;
                lblBudget.Visible = true;
                cboBudget.Visible = true;
                lblProduce.Visible = true;
                cboProduce.Visible = true;
                lblLot.Visible = true;
                cboLot.Visible = true; ;
                lblItem_group.Visible = false;
                cboItem_group.Visible = false;
            }
            else
            {
                lblBudget.Visible = false;
                cboBudget.Visible = false;
                lblProduce.Visible = false;
                cboProduce.Visible = false;
                lblActivity.Visible = false;
                cboActivity.Visible = false;

                imgList_item.Visible = false;
                imgClear_item.Visible = false;
                txtitem_code.Visible = false;
                txtitem_name.Visible = false;
            }


        }

        protected void cboBudget_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboProduce();
        }

        protected void cboProduce_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboActivity();
        }

        protected void cboUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboBudget();
        }

        protected void cboLot_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboItem_group();
        }


    }
}
