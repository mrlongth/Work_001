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

namespace myWeb.App_Control.Person_Manage
{
    public partial class global_budget_plan_report : GlobalPageBase
    {
        #region private data

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
                InitcboBudgetType();
                this.BudgetType = cboBudget_type.SelectedValue;
                InitcboLot();
                InitcboBudget();
                InitcboProduce();
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

        private void InitcboBudgetType()
        {

            cCommon oCommon = new cCommon();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strCode = this.BudgetType;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " Select * from  general where g_type = 'budget_type' and g_code <> 'M' Order by g_sort ";
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
            //if (strCode != "M") cboBudget_type.Enabled = false;
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

            string strScript = string.Empty;
            string strPay_Month = string.Empty;
            string strPay_Year = string.Empty;
            string strReport_code = string.Empty;
            string strLot = string.Empty;
            strYear = cboYear.SelectedValue;

            strdirector_code = cboDirector.SelectedValue;
            strunit_code = cboUnit.SelectedValue;
            strbudget_code = cboBudget.SelectedValue;
            strproduce_code = cboProduce.SelectedValue;
            strdirector_code = cboDirector.SelectedValue;
            strunit_code = cboUnit.SelectedValue;

            strPay_Month = cboPay_Month.SelectedValue;
            strPay_Year = cboPay_Year.SelectedValue;
            strLot = cboLot.SelectedValue;
            this.BudgetType = cboBudget_type.SelectedValue;


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


            strCriteria3 = strCriteria2;
            strCriteria2 += " and pd.pay_year+'/'+pd.pay_month < '" + strPay_Year + "/" + strPay_Month + "'";
            strCriteria3 += " and pd.pay_year+'/'+pd.pay_month = '" + strPay_Year + "/" + strPay_Month + "'";

            Session["criteria2"] = strCriteria2.Replace("bm.", "pd.");
            Session["criteria3"] = strCriteria3.Replace("bm.", "pd.");
            strReport_code = "Rep_payment_openbysum";


            Session["criteria"] = strCriteria;
            strScript = "windowOpenMaximize(\"global_payment_report.aspx?report_code=" + strReport_code +
                                                         "&months=" + cboPay_Month.Text + "&year=" + cboPay_Year.Text + "\", \"_blank\");\n";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript, true);

        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboDirector();
            InitcboBudget();
            InitcboLot();
        }

        protected void cboDirector_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboUnit();
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void cboBudget_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboProduce();
        }

        protected void cboUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboBudget();
        }

        protected void cboBudget_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BudgetType = cboBudget_type.SelectedValue;
            InitcboDirector();
            InitcboLot();
            InitcboBudget();
            InitcboProduce();
        }

      

    }
}
