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

namespace myWeb.App_Control.payment
{
    public partial class payment_report : PageBase
    {
        private string strPrefixCtr = "ctl00$ASPxRoundPanel1$ASPxRoundPanel2$ContentPlaceHolder1$";
     
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(payment_report));
            
            //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th-TH");
        
            if (!IsPostBack)
            {
                imgList_person.Attributes.Add("onclick", "OpenPopUp('900px','500px','94%','ค้นหาข้อมูลบุคคลากร' ,'../lov/person_lov.aspx?year='+document.forms[0]." + strPrefixCtr +
               "cboYear.options[document.forms[0]." + strPrefixCtr + "cboYear.selectedIndex].value+" +
               "'&person_code='+document.forms[0]." + strPrefixCtr + "txtperson_code.value+" +
               "'&person_name='+document.forms[0]." + strPrefixCtr + "txtperson_name.value+" +
               "'&ctrl1=" + txtperson_code.ClientID + "&ctrl2=" + txtperson_name.ClientID + "', '1');return false;");
                imgClear_person.Attributes.Add("onclick", "document.getElementById('" + txtperson_code.ClientID + "').value='';document.getElementById('" + txtperson_name.ClientID + "').value=''; return false;");


                //imgList_item.Attributes.Add("onclick", "PaymentPopUp('800px','400px','93%','ค้นหาข้อมูลรายได้/ค่าใช้จ่าย' ,'../lov/direct_pay_item_lov.aspx?year='+document.forms[0]." + strPrefixCtr +
                //                                             "cboYear.options[document.forms[0]." + strPrefixCtr + "cboYear.selectedIndex].value+" +
                //                                             "'&item_code='+document.forms[0]." + strPrefixCtr + "txtitem_code.value+" +
                //                                             "'&item_name='+document.forms[0]." + strPrefixCtr + "txtitem_name.value+" +
                //                                             "'&ctrl1=" + txtitem_code.ClientID + "&ctrl2=" + txtitem_name.ClientID + "&from=report', '1');return false;");

                //imgClear_item.Attributes.Add("onclick", "document.forms[0]." + strPrefixCtr + "txtitem_code.value='';" +
                //                        "document.forms[0]." + strPrefixCtr + "txtitem_name.value='';return false;");
                InitcboRound();
                //InitcboPay_Month();
                //InitcboPay_Year();
                InitcboPerson_group_all();
                InitRadioList();
                InitcboDocType();
                txtpayment_date.Text = DateTime.Now.ToString("dd/MM/yyyy");

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
            else
            {

                if (Request.Form[txtpayment_date.UniqueID] != null)
                {
                    txtpayment_date.Text = Request.Form[txtpayment_date.UniqueID].ToString();
                }
                else
                {
                    txtpayment_date.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
            }

        }

        #region private function

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
            InitcboProduce();
            if (IsPostBack)
            {
                InitRadioList();
            }
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

        private void InitcboProduce()
        {
            cProduce oProduce = new cProduce();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strproduce_code = string.Empty;
            strproduce_code = cboProduce.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and  produce.produce_year='" + cboYear.SelectedValue + "' " +
                             " and  produce.c_active='Y' ";
            strCriteria += " and  produce.budget_type='" + this.BudgetType + "' ";
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

        //private void InitcboPerson_group()
        //{
        //    cPerson_group oPerson_group = new cPerson_group();
        //    string strMessage = string.Empty,
        //                strCriteria = string.Empty,
        //                strperson_group_code = string.Empty;
        //    strperson_group_code = cboPerson_group.SelectedValue;
        //    int i;
        //    DataSet ds = new DataSet();
        //    DataTable dt = new DataTable();
        //    strCriteria = " and c_active='Y'  " +
        //                            " and person_group_code in " +
        //                            " (Select person_group_code from dbo.view_payment_report " +
        //                            " where report_group_code = '" + cboDoctype.SelectedValue + "' " +
        //                            " and person_group_code IN (" + PersonGroupList + ") " +
        //                            " group by person_group_code) ";
        //    if (oPerson_group.SP_PERSON_GROUP_SEL(strCriteria, ref ds, ref strMessage))
        //    {
        //        dt = ds.Tables[0];
        //        cboPerson_group.Items.Clear();
        //        cboPerson_group.Items.Add(new ListItem("---- เลือกข้อมูลทั้งหมด ----", ""));
        //        for (i = 0; i <= dt.Rows.Count - 1; i++)
        //        {
        //            cboPerson_group.Items.Add(new ListItem(dt.Rows[i]["person_group_name"].ToString(), dt.Rows[i]["person_group_code"].ToString()));
        //        }
        //        if (dt.Rows.Count == 1)
        //        {
        //            cboPerson_group.SelectedIndex = 1;
        //            //cboPerson_group.Enabled = false;
        //        }
        //        else if (dt.Rows.Count == 0)
        //        {
        //            InitcboPerson_group_all();
        //        }
        //        else
        //        {
        //            //cboPerson_group.Enabled = true;
        //        }
        //    }
        //}


        private void InitcboPerson_group()
        {
            cPerson_group oPerson_group = new cPerson_group();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strperson_group_code = string.Empty;
            strperson_group_code = cboPerson_group.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and c_active='Y'  " +
                                    " and person_group_code in " +
                                    " (select distinct isnull(nullif(pd.person_group_code,''),ph.person_group_code) " +
                                    " from payment_detail pd " +
                                    " inner join payment_head ph " +
                                    " 	on ph.payment_doc = pd.payment_doc " +
                                    " left join  payment_acc_twin pt " +
                                    " 	on pt.payment_doc = pd.payment_doc " +
                                    " inner join payment_report pr " +
                                    " 	on ph.payment_year = pr.report_year	 " +
                                    " 	and  (pt.item_code = pr.item_code or pd.item_code = pr.item_code)" +
                                    " where ph.payment_year = '" + cboYear.SelectedValue + "' " +
                                    " and	ph.pay_year = '" + cboPay_Year.SelectedValue + "' " +
                                    " and   ph.pay_month = '" + cboPay_Month.SelectedValue + "' " +
                                    " and   pr.report_group_code='" + cboDoctype.SelectedValue + "' " +
                                    " and   pr.report_group_show = '" + RadioButtonList1.SelectedValue + "' " +
                                    " and   pd.budget_type = 'B') order by person_group_name ";
            if (oPerson_group.SP_PERSON_GROUP_SEL(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboPerson_group.Items.Clear();
                cboPerson_group.Items.Add(new ListItem("---- เลือกข้อมูลทั้งหมด ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboPerson_group.Items.Add(new ListItem(dt.Rows[i]["person_group_name"].ToString(), dt.Rows[i]["person_group_code"].ToString()));
                }
                if (dt.Rows.Count == 1)
                {
                    cboPerson_group.SelectedIndex = 1;
                    //cboPerson_group.Enabled = false;
                }
                else if (dt.Rows.Count == 0)
                {
                    InitcboPerson_group_all();
                }
            }
        }

        private void InitcboPerson_group_all()
        {
            cPerson_group oPerson_group = new cPerson_group();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strperson_group_code = string.Empty;
            strperson_group_code = cboPerson_group.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and c_active='Y'  ";
            strCriteria += " and person_group_code IN (" + PersonGroupList + ") ";
            strCriteria += " order by person_group_name";

            if (oPerson_group.SP_PERSON_GROUP_SEL(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboPerson_group.Items.Clear();
                cboPerson_group.Items.Add(new ListItem("---- เลือกข้อมูลทั้งหมด ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboPerson_group.Items.Add(new ListItem(dt.Rows[i]["person_group_name"].ToString(), dt.Rows[i]["person_group_code"].ToString()));
                }
                if (dt.Rows.Count == 1)
                {
                    cboPerson_group.SelectedIndex = 0;
                }
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

        private void InitRadioList()
        {
            cPayment oPayment = new cPayment();
            string strMessage = string.Empty;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string strCriteria = " and c_active='A' ";
            strCriteria += " and  report_year = '" + cboYear.SelectedValue + "' ";
            strCriteria += " and  report_group_show in (Select report_group_show from view_payment_report  where  person_group_code IN (" + PersonGroupList + ",'') and  report_year = '" + cboYear.SelectedValue + "' ";
            strCriteria += " and  budget_type IN ('B','M') ";


            //if (DirectorLock == "N")
            //{
            //    strCriteria += " and lot_code in (''," + LotCodeList + ") ";
            //}
            strCriteria += ")";
            RadioButtonList1.Items.Clear();
            if (oPayment.SP_PAYMENT_REPORT_SEL(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];

                if (dt.Rows.Count > 0)
                {
                    for (i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        RadioButtonList1.Items.Add(new ListItem(dt.Rows[i]["report_name"].ToString(), dt.Rows[i]["report_group_show"].ToString()));
                    }
                    if (RadioButtonList1.Items.Count > 0)
                    {
                        RadioButtonList1.SelectedIndex = 0;
                    }
                }
            }
        }
        //private void InitRadioList()
        //{
        //    cPayment oPayment = new cPayment();
        //    string strMessage = string.Empty;
        //    int i;
        //    DataSet ds = new DataSet();
        //    DataTable dt = new DataTable();
        //    string strCriteria = " and c_active='A' ";
        //    strCriteria += " and  report_year = '" + cboYear.SelectedValue + "' ";
        //    strCriteria += " and  report_group_show in (Select report_group_show from view_payment_report  where  person_group_code IN (" + PersonGroupList + ",'') and  report_year = '" + cboYear.SelectedValue + "' ";
        //    strCriteria += " and  budget_type IN ('B','M')) ";
        //    //strCriteria += " and  item_code in (" +
        //    //                        " select distinct substring(item_code,1,10) " +
        //    //                        " from view_payment_item_all " +
        //    //                        " where payment_year = '" + cboYear.SelectedValue + "' " +
        //    //                        " and	pay_year = '" + cboPay_Year.SelectedValue + "' " +
        //    //                        " and   pay_month = '" + cboPay_Month.SelectedValue + "' " +
        //    //                        " and   payment_detail_budget_type = 'B' ";
        //    //if (base.DirectorLock == "Y")
        //    //{
        //    //    strCriteria += " and substring(payment_detail_director_code,4,2) = substring('" + base.DirectorCode + "',4,2) ";
        //    //}
        //    strCriteria += " and  (item_code in (" +
        //        " select distinct substring(pd.item_code,1,10) " +
        //        " from payment_detail pd  " +
        //        " inner join payment_head ph " +
        //        " 	on ph.payment_doc = pd.payment_doc  " +
        //        " inner join budget_plan " +
        //        " 	on isnull(pd.budget_plan_code,ph.budget_plan_code) = budget_plan.budget_plan_code " +
        //        " 	and	 ph.payment_year = budget_plan.budget_plan_year " +
        //        " inner join unit " +
        //        " 	on budget_plan.unit_code = unit.unit_code " +
        //        " where ph.payment_year = '" + cboYear.SelectedValue + "'  and	ph.pay_year =  '" + cboPay_Year.SelectedValue + "'   " +
        //        " and   ph.pay_month = '" + cboPay_Month.SelectedValue + "'   " +
        //        " and   budget_plan.budget_type = 'B' ";
        //        if (base.DirectorLock == "Y")
        //        {
        //            strCriteria += " and substring(unit.director_code,4,2) = substring('" + base.DirectorCode + "',4,2) ";
        //        }
        //       strCriteria += " union   select distinct substring(pd.item_code,1,10) " +
        //        " from payment_acc_twin pd  " +
        //        " inner join payment_head ph  	" +
        //        " 	on ph.payment_doc = pd.payment_doc  " +
        //        " inner join budget_plan " +
        //        " 	on isnull(pd.budget_plan_code,ph.budget_plan_code) = budget_plan.budget_plan_code " +
        //        " 	and	 ph.payment_year = budget_plan.budget_plan_year " +
        //        " inner join unit " +
        //        " 	on budget_plan.unit_code = unit.unit_code " +
        //        " where ph.payment_year = '" + cboYear.SelectedValue + "'  and	ph.pay_year =  '" + cboPay_Year.SelectedValue + "'  " +
        //        " and   ph.pay_month = '" + cboPay_Month.SelectedValue + "'  " +
        //        " and   budget_plan.budget_type = 'B' ";
        //        if (base.DirectorLock == "Y")
        //        {
        //            strCriteria += " and substring(unit.director_code,4,2) = substring('" + base.DirectorCode + "',4,2) ";
        //        }

        //    strCriteria += ") or c_active='A')";
        //    RadioButtonList1.Items.Clear();
        //    if (oPayment.SP_PAYMENT_REPORT_SEL(strCriteria, ref ds, ref strMessage))
        //    {
        //        dt = ds.Tables[0];

        //        if (dt.Rows.Count > 0)
        //        {
        //            for (i = 0; i <= dt.Rows.Count - 1; i++)
        //            {
        //                RadioButtonList1.Items.Add(new ListItem(dt.Rows[i]["report_name"].ToString(), dt.Rows[i]["report_group_show"].ToString()));
        //            }
        //            if (RadioButtonList1.Items.Count > 0)
        //            {
        //                RadioButtonList1.SelectedIndex = 0;
        //            }
        //        }
        //    }
        //}


        private void InitcboDocType()
        {
            cPayment oPayment = new cPayment();
            string strMessage = string.Empty;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            string strCriteria = " and  report_year = '" + cboYear.SelectedValue + "' ";
            strCriteria += " and c_active in ('A' ,'Y')   and  report_group_show='" + RadioButtonList1.SelectedValue + "' ";

            if (oPayment.SP_PAYMENT_REPORT_SEL(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboDoctype.Items.Clear();
                cboDoctype.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboDoctype.Items.Add(new ListItem(dt.Rows[i]["report_name"].ToString(), dt.Rows[i]["report_group_code"].ToString()));
                }
                if (dt.Rows.Count == 1)
                {
                    cboDoctype.SelectedIndex = 1;
                }

            }
        }

        #endregion

        protected void imgPrint_Click(object sender, ImageClickEventArgs e)
        {
            string strCriteria = string.Empty;
            string strCriteria2 = string.Empty;
            string strYear = string.Empty;
            string strActive = string.Empty;
            string strperson_group_code = string.Empty;
            string strdirector_code = string.Empty;
            string strunit_code = string.Empty;
            string stritem_code = string.Empty;
            string strproduce_code = string.Empty;
            string strScript = string.Empty;
            string strPay_Month = string.Empty;
            string strPay_Year = string.Empty;

            strYear = cboYear.SelectedValue;
            strperson_group_code = cboPerson_group.SelectedValue;
            strdirector_code = cboDirector.SelectedValue;
            strproduce_code = cboProduce.SelectedValue;
            strunit_code = cboUnit.SelectedValue;
            strPay_Month = cboPay_Month.SelectedValue;
            strPay_Year = cboPay_Year.SelectedValue;

            if (!strYear.Equals(""))
            {
                strCriteria = strCriteria + "  And  view_payment.payment_year = '" + strYear + "'  ";
            }

            if (!strPay_Month.Equals(""))
            {
                strCriteria = strCriteria + "  And  view_payment.pay_month='" + strPay_Month + "' ";
            }

            if (!strPay_Year.Equals(""))
            {
                strCriteria = strCriteria + "  And  view_payment.pay_year='" + strPay_Year + "' ";
            }

            if (!strperson_group_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  view_payment.payment_detail_person_group_code ='" + strperson_group_code + "' ";
            }

            if (!strproduce_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  view_payment.payment_detail_produce_code= '" + strproduce_code + "' ";
            }


            if (!strdirector_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  view_payment.payment_detail_director_code = '" + strdirector_code + "' ";
            }

            if (!strunit_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  view_payment.payment_detail_unit_code= '" + strunit_code + "' ";
            }

            //strCriteria += " and (view_payment.person_group_code IN (" + PersonGroupList + ") ";
            //strCriteria += " or view_payment.person_group_item IN (" + PersonGroupList + ") )";
            strCriteria += " and view_payment.payment_detail_person_group_code IN (" + PersonGroupList + ",'') ";

            if (DirectorLock == "Y")
            {
                strCriteria += " and substring(view_payment.payment_detail_director_code,4,2) = substring('" + DirectorCode + "',4,2) ";
            }

            if (base.myBudgetType != "M")
            {
                strCriteria = strCriteria + "  And  view_payment.payment_detail_budget_type ='" + base.myBudgetType + "' ";
            }


            if (!txtperson_code.Text.Equals(""))
            {
                strCriteria = strCriteria + "  And  view_payment.person_code= '" + txtperson_code.Text.Trim() + "' ";
            }

            Session["payment_date"] = txtpayment_date.Text;

            string strReport_id = string.Empty;
            cPayment oPayment = new cPayment();
            string strMessage = string.Empty;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria2 = " and  report_group_code='" + cboDoctype.SelectedValue + "' and person_group_code='" + cboPerson_group.SelectedValue + "' And report_year='" + strYear + "' and report_group_show = '" + RadioButtonList1.SelectedValue + "' ";
            if (oPayment.SP_PAYMENT_REPORT_SEL(strCriteria2, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    stritem_code = dt.Rows[0]["item_code"].ToString();
                    strReport_id = dt.Rows[0]["report_id"].ToString();
                    if (!stritem_code.Equals(""))
                    {
                        if (stritem_code.Contains("08-082"))
                        {
                            strCriteria = strCriteria + "  And (view_payment.item_code= '" + stritem_code + "'  OR  substring(view_payment.item_code,5,7)= '08-084' )";
                        }
                        //else if (stritem_code.Contains("10-001"))
                        //{
                        //    strCriteria = strCriteria + "  And (view_payment.item_code= '" + stritem_code + "'  OR  substring(view_payment.item_code,5,7)= '09-001' )";
                        //}                            
                        else
                        {
                            strCriteria = strCriteria + "  And  view_payment.item_code= '" + stritem_code + "' ";
                        }
                    }
                    if (stritem_code.Substring(4, 6).Equals("03-002"))
                    {
                        strCriteria = strCriteria.Replace("view_payment.", "");
                    }
                    Session["criteria"] = strCriteria;
                    strScript = "windowOpenMaximize(\"../../App_Control/reportsparameter/payment_report_show.aspx?report_id=" + strReport_id +
                                                                 "&months=" + cboPay_Month.Text + "&year=" + cboPay_Year.Text + "\", \"_blank\");\n";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PaymentPage", strScript, true);

                }
                else
                {

                    strCriteria2 = " and  report_group_code='" + cboDoctype.SelectedValue + "' and person_group_code=''  " + " And report_year='" + strYear + "' and  report_group_show='" + RadioButtonList1.SelectedValue + "' ";
                    if (oPayment.SP_PAYMENT_REPORT_SEL(strCriteria2, ref ds, ref strMessage))
                    {
                        dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            stritem_code = dt.Rows[0]["item_code"].ToString();
                            strReport_id = dt.Rows[0]["report_id"].ToString();
                            if (!stritem_code.Equals(""))
                            {
                                if (stritem_code.Contains("08-082"))
                                {
                                    strCriteria = strCriteria + "  And (view_payment.item_code= '" + stritem_code + "'  OR  substring(view_payment.item_code,5,7)= '08-084' )";
                                }
                                else
                                {
                                    strCriteria = strCriteria + "  And  view_payment.item_code= '" + stritem_code + "' ";
                                }
                            }
                            if (stritem_code.Substring(4, 6).Equals("03-002"))
                            {
                                strCriteria = strCriteria.Replace("view_payment.", "");
                            }
                            Session["criteria"] = strCriteria;
                            strScript = "windowOpenMaximize(\"../../App_Control/reportsparameter/payment_report_show.aspx?report_id=" + strReport_id +
                                                                         "&months=" + cboPay_Month.Text + "&year=" + cboPay_Year.Text + "\", \"_blank\");\n";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PaymentPage", strScript, true);

                        }
                        else
                        {
                            strScript = "alert('ไม่มีการกำหนดรหัสรายงานโปรดตรวจสอบ');";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PaymentPage", strScript, true);
                        }
                    }

                }
            }
        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitRadioList();
            InitcboDocType();
            InitcboDirector();
            InitcboProduce();
        }

        protected void cboDirector_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboUnit();
        }

        protected void cboUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboUnit();

        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboDocType();
            InitcboPerson_group();
        }

        protected void cboDoctype_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboPerson_group();
        }

        protected void cboYear_SelectedIndexChanged1(object sender, EventArgs e)
        {
            InitcboDirector();
            InitcboProduce();
            InitRadioList();
            InitcboDocType();
            InitcboPerson_group();
        }

        protected void cboPay_Month_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitRadioList();
            InitcboPerson_group();
        }

    }
}
