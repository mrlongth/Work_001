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
    public partial class paymentdetail_report : PageBase
    {
        #region private data
        private bool[] blnAccessRight = new bool[5] { false, false, false, false, false };
        private string strPrefixCtr = "ctl00$ASPxRoundPanel1$ASPxRoundPanel2$ContentPlaceHolder1$";
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                imgList_item.Attributes.Add("onclick", "OpenPopUp('800px','400px','93%','ค้นหาข้อมูลรายได้/ค่าใช้จ่าย' ,'../lov/item_lov.aspx?year='+document.forms[0]." + strPrefixCtr +
                                                             "cboYear.options[document.forms[0]." + strPrefixCtr + "cboYear.selectedIndex].value+" +
                                                             "'&item_code='+document.forms[0]." + strPrefixCtr + "txtitem_code.value+" +
                                                             "'&item_name='+document.forms[0]." + strPrefixCtr + "txtitem_name.value+" +
                                                             "'&ctrl1=" + txtitem_code.ClientID + "&ctrl2=" + txtitem_name.ClientID + "&from=report', '1');return false;");

                imgClear_item.Attributes.Add("onclick", "document.forms[0]." + strPrefixCtr + "txtitem_code.value='';" +
                                        "document.forms[0]." + strPrefixCtr + "txtitem_name.value='';return false;");
                //InitcboYear();
                InitcboBank();
                InitcboRound();
                InitcboPerson_group();
                cboProduce.Visible = false;
                lblBank.Visible = false;
                cboBank.Visible = false;

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
            InitcboProduce();

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
                    Session["DatePay"] = "";
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region get Data
                        strYear = ds.Tables[0].Rows[0]["payment_year"].ToString();
                        strPay_Month = ds.Tables[0].Rows[0]["pay_month"].ToString();
                        strPay_Year = ds.Tables[0].Rows[0]["pay_year"].ToString();
                        Session["DatePay"] = ds.Tables[0].Rows[0]["comments"].ToString();
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
            strCriteria = " and c_active='Y' ";
            strCriteria += " order by person_group_name";
            //strCriteria += " and person_group_code IN (" + PersonGroupList + ") ";
            if (oPerson_group.SP_PERSON_GROUP_SEL(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboPerson_group.Items.Clear();
                cboPerson_group.Items.Add(new ListItem("---- เลือกข้อมูลทั้งหมด ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboPerson_group.Items.Add(new ListItem(dt.Rows[i]["person_group_name"].ToString(), dt.Rows[i]["person_group_code"].ToString()));
                }
                cboPerson_group.Items.Add(new ListItem("พนักงานกลุ่มอื่นๆ", ""));
                if (cboPerson_group.Items.FindByValue(strperson_group_code) != null)
                {
                    cboPerson_group.SelectedIndex = -1;
                    cboPerson_group.Items.FindByValue(strperson_group_code).Selected = true;
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
            strCriteria = " and c_active='Y'  and lot_year='" + cboYear.SelectedValue + "'";
            strCriteria = " and budget_type='" + this.BudgetType + "'";
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

        private void InitcboBank()
        {
            cBank oBank = new cBank();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strBankCode = cboBank.SelectedValue;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and c_active='Y' ";
            if (oBank.SP_SEL_BANK(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboBank.Items.Clear();
                cboBank.Items.Add(new ListItem("---- เลือกข้อมูลทั้งหมด ----", ""));
                int i;
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboBank.Items.Add(new ListItem(dt.Rows[i]["bank_name"].ToString(), dt.Rows[i]["bank_code"].ToString()));
                }
                if (cboBank.Items.FindByValue(strBankCode) != null)
                {
                    cboBank.SelectedIndex = -1;
                    cboBank.Items.FindByValue(strBankCode).Selected = true;
                }
            }
        }


        private void InitcboLoan()
        {
            cCommon oCommon = new cCommon();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strCode = cboBank.SelectedValue;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " Select * from  loan order by loan_name ";
            if (oCommon.SEL_SQL(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboBank.Items.Clear();
                int i;
                cboBank.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboBank.Items.Add(new ListItem(dt.Rows[i]["loan_name"].ToString(), dt.Rows[i]["loan_code"].ToString()));
                }
                if (cboBank.Items.FindByValue(strCode) != null)
                {
                    cboBank.SelectedIndex = -1;
                    cboBank.Items.FindByValue(strCode).Selected = true;
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
            string strScript = string.Empty;
            string strPay_Month = string.Empty;
            string strPay_Year = string.Empty;
            string strReport_code = string.Empty;
            string strProduce = string.Empty;
            string strLot = string.Empty;
            string strBankCode = string.Empty;
            string strCriteriaDesc = string.Empty;
            stritem_code = txtitem_code.Text;
            strYear = cboYear.SelectedValue;
            strperson_group_code = cboPerson_group.SelectedValue;
            strdirector_code = cboDirector.SelectedValue;
            strunit_code = cboUnit.SelectedValue;
            strPay_Month = cboPay_Month.SelectedValue;
            strPay_Year = cboPay_Year.SelectedValue;
            strProduce = cboProduce.SelectedValue;
            strLot = cboLot.SelectedValue;
            strBankCode = cboBank.SelectedValue;

            if (!strYear.Equals(""))
            {
                strCriteria = strCriteria + "  And  view_payment.payment_year = '" + strYear + "'  ";
            }

            if (!strPay_Month.Equals(""))
            {
                if (!RadioButtonList1.SelectedValue.Equals("17"))
                {
                    strCriteria = strCriteria + "  And  view_payment.pay_month='" + strPay_Month + "' ";
                }
            }

            if (!strPay_Year.Equals(""))
            {
                if (!RadioButtonList1.SelectedValue.Equals("17"))
                {
                    strCriteria = strCriteria + "  And  view_payment.pay_year='" + strPay_Year + "' ";
                }
            }

            if (!strperson_group_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  view_payment.payment_detail_person_group_code ='" + strperson_group_code + "' ";
                strCriteriaDesc += "กลุ่มบุคคลากร : " + cboPerson_group.SelectedItem.Text + "  ";
            }

            if (!strdirector_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  view_payment.payment_detail_director_code = '" + strdirector_code + "' ";
                strCriteriaDesc += "สังกัด : " + cboDirector.SelectedItem.Text + "  ";
            }

            if (!strunit_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  view_payment.payment_detail_unit_code= '" + strunit_code + "' ";
                strCriteriaDesc += "หน่วยงาน/สาขา : " + cboUnit.SelectedItem.Text + "  ";
            }

            if (!stritem_code.Equals(""))
            {

                if (RadioAll.Checked)
                {
                    strCriteria = strCriteria + "  And  view_payment.item_code in ( '" + stritem_code + "A' , '" + stritem_code + "' )";
                }
                else if (RadioPaymentBack.Checked)
                {
                    strCriteria = strCriteria + "  And  view_payment.item_code= '" + stritem_code + "A' ";
                }
                else
                {
                    strCriteria = strCriteria + "  And  view_payment.item_code= '" + stritem_code + "' ";
                }

            }

            if (!strProduce.Equals(""))
            {
                strCriteria = strCriteria + "  And  view_payment.payment_detail_produce_code= '" + strProduce + "' ";
            }

            if (!strLot.Equals(""))
            {
                strCriteria = strCriteria + "  And  view_payment.payment_detail_lot_code= '" + strLot + "' ";
            }

            if (RadioButtonList1.SelectedValue.Equals("6") || RadioButtonList1.SelectedValue.Equals("6_1"))
            {
                if (base.myBudgetType != "R")
                {
                    strCriteria += " and view_payment.payment_detail_person_group_code IN (" + PersonGroupList + ") ";
                }
            }
            else
            {
                if (base.myBudgetType != "R")
                {
                    strCriteria += " and view_payment.payment_detail_person_group_code IN (" + PersonGroupList + ") ";
                   // strCriteria += " or view_payment.person_group_item IN (" + PersonGroupList + ") )";
                }
            }


            if (DirectorLock == "Y")
            {
                strCriteria += " and substring(view_payment.payment_detail_director_code,4,2) = substring('" + DirectorCode + "',4,2) ";
            }

            if (base.myBudgetType != "M")
            {
                strCriteria = strCriteria + "  And  view_payment.payment_detail_budget_type ='" + base.myBudgetType + "' ";
            }


            if (RadioButtonList1.SelectedValue.Equals("0"))
            {
                if (!strperson_group_code.Equals(""))
                {
                    strCriteria = strCriteria + "  And  view_payment.payment_detail_person_group_code ='" + strperson_group_code + "' ";
                }
                strReport_code = "Rep_allcredit";
                strCriteria = strCriteria.Replace("view_payment.", "");
            }
            else if (RadioButtonList1.SelectedValue.Equals("0_1"))
            {
                if (!strperson_group_code.Equals(""))
                {
                    strCriteria = strCriteria + "  And  view_payment.payment_detail_person_group_code ='" + strperson_group_code + "' ";
                }
                strReport_code = "Rep_allcredit_1";
                strCriteria = strCriteria.Replace("view_payment.", "");
            }
            else if (RadioButtonList1.SelectedValue.Equals("1"))
            {
                if (!strperson_group_code.Equals(""))
                {
                    strCriteria = strCriteria + "  And  view_payment.payment_detail_person_group_code ='" + strperson_group_code + "' ";
                }
                strReport_code = "Rep_paymentbyitem&item_des=" + txtitem_name.Text.Trim();
                strCriteria = strCriteria.Replace("view_payment.", "");
            }
            else if (RadioButtonList1.SelectedValue.Equals("1_1"))
            {
                if (!strperson_group_code.Equals(""))
                {
                    strCriteria = strCriteria + "  And  view_payment.payment_detail_person_group_code ='" + strperson_group_code + "' ";
                }
                strReport_code = "Rep_paymentbyitem_1&item_des=" + txtitem_name.Text.Trim();
                strCriteria = strCriteria.Replace("view_payment.", "");
            }
            else if (RadioButtonList1.SelectedValue.Equals("2"))
            {
                strReport_code = "Rep_paymentbydirector&item_des=" + txtitem_name.Text.Trim() + "&persongroup=" + cboPerson_group.SelectedItem.Text;
                strCriteria = strCriteria.Replace("view_payment.", "");
            }

            else if (RadioButtonList1.SelectedValue.Equals("6"))
            {
                if (!strperson_group_code.Equals(""))
                {
                    strCriteria = strCriteria + "  And  view_payment.payment_detail_person_group_code ='" + strperson_group_code + "' ";
                }
                strReport_code = "Rep_paymentsumbypersongroup";
                strCriteria = strCriteria.Replace("view_payment.", "");
            }
            else if (RadioButtonList1.SelectedValue.Equals("6_1"))
            {
                if (!strperson_group_code.Equals(""))
                {
                    strCriteria = strCriteria + "  And  view_payment.payment_detail_person_group_code ='" + strperson_group_code + "' ";
                }
                strReport_code = "Rep_paymentsumbypersongroup_1";
                strCriteria = strCriteria.Replace("view_payment.", "");
            }

            else if (RadioButtonList1.SelectedValue.Equals("8"))
            {
                strReport_code = "Rep_exceldebitall";
                if (base.myBudgetType == "R")
                {
                    strReport_code = "Rep_exceldebitallincome";
                }
                if (!strperson_group_code.Equals(""))
                {
                    strCriteria = strCriteria + "  And  view_payment.payment_detail_person_group_code ='" + strperson_group_code + "' ";
                }
                strCriteria = strCriteria.Replace("view_payment.", "");
            }

            else if (RadioButtonList1.SelectedValue.Equals("14"))
            {
                strReport_code = "Rep_payment&item_des=" + txtitem_name.Text.Trim();
                strCriteria = strCriteria.Replace("view_payment.", "");
            }
            else if (RadioButtonList1.SelectedValue.Equals("15"))
            {
                strReport_code = "Rep_paymentbycredit";
                strCriteria = strCriteria + "  And  item_type= 'C' ";
                strCriteria = strCriteria.Replace("view_payment.", "");
            }

            else if (RadioButtonList1.SelectedValue.Equals("18"))
            {
                if (!strperson_group_code.Equals(""))
                {
                    strCriteria = strCriteria + "  And  view_payment.payment_detail_person_group_code ='" + strperson_group_code + "' ";
                }
                strReport_code = "Rep_paymentbycreditall";
                strCriteria = strCriteria.Replace("view_payment.", "");
            }
        
            else if (RadioButtonList1.SelectedValue.Equals("A1"))
            {
                if (!strperson_group_code.Equals(""))
                {
                    strCriteria = strCriteria + "  And  view_payment.payment_detail_person_group_code ='" + strperson_group_code + "' ";
                }
                strCriteria = strCriteria + "  And  item_type = 'C' ";
                strReport_code = "Rep_excelcreditall";
                strCriteria = strCriteria.Replace("view_payment.", "");
            }

            else if (RadioButtonList1.SelectedValue.Equals("A2") ||
                     RadioButtonList1.SelectedValue.Equals("A4") ||
                     RadioButtonList1.SelectedValue.Equals("A5"))
            {
                if (!strBankCode.Equals(""))
                {
                    if (base.myBudgetType != "R")
                    {
                        strCriteria += "  And  branch.bank_code ='" + strBankCode + "' ";
                    }
                    else
                    {
                        strCriteria += "  And  (case when nullif(branch_2.bank_code,'') is not null then branch_2.bank_code else branch.bank_code end)='" + strBankCode + "'  ";
                    }

                }

                Session["MoneyType"] = "";
                if (this.BudgetType == "R")
                {
                    Session["MoneyType"] = "(งบประมาณเงินรายได้)";
                }
                else
                {
                    if (PersonGroupList.Contains("03"))
                    {
                        Session["MoneyType"] = "(เงินอุดหนุนทั่วไป)";
                    }
                    else
                    {
                        Session["MoneyType"] = "(งบประมาณเงินแผ่นดิน)";
                    }
                }
                Session["BankName"] = cboBank.SelectedItem.Text;
                if (RadioButtonList1.SelectedValue.Equals("A2"))
                {
                    strReport_code = "Rep_payment_bank";
                }
                else
                {
                    strReport_code = "Rep_payment_bank_cover";               
                }

                strCriteria = strCriteria.Replace("view_payment.", "");

            }
            else if (RadioButtonList1.SelectedValue.Equals("A3"))
            {
                if (!strperson_group_code.Equals(""))
                {
                    strCriteria = strCriteria + "  And  view_payment.payment_detail_person_group_code ='" + strperson_group_code + "' ";
                }
                strReport_code = "Rep_paymentbyitem_produce&item_des=" + txtitem_name.Text.Trim();
                strCriteria = strCriteria.Replace("view_payment.", "");
            }
            else if (RadioButtonList1.SelectedValue.Equals("A6"))
            {
                if (!strperson_group_code.Equals(""))
                {
                    strCriteria = strCriteria + "  And  view_payment.payment_detail_person_group_code ='" + strperson_group_code + "' ";
                }
                if (!strBankCode.Equals(""))
                {
                    strCriteria += "  And  loan_code ='" + strBankCode + "' ";                  
                }
                strReport_code = "Rep_payment_loan" ;
                strCriteria = strCriteria.Replace("view_payment.", "").Replace("payment_detail_", "");
            }
            else if (RadioButtonList1.SelectedValue.Equals("A7"))
            {
                strReport_code = "Rep_paymentdirectpay";
                strCriteria = strCriteria.Replace("view_payment.", "").Replace("payment_detail_","");
            }

            else if (RadioButtonList1.SelectedValue.Equals("A8"))
            {
                strReport_code = "Rep_paymentdirectpaybyproduce";
                strCriteria = strCriteria.Replace("view_payment.", "").Replace("payment_detail_", "");
            }

            Session["criteria"] = strCriteria;
            Session["criteriadesc"] = strCriteriaDesc;

            strScript = "windowOpenMaximize(\"../../App_Control/reportsparameter/payment_report_show.aspx?report_code=" + strReport_code +
                                                         "&months=" + cboPay_Month.Text + "&year=" + cboPay_Year.Text + "\", \"_blank\");\n";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript, true);

        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            //InitcboRound();
            InitcboDirector();
            InitcboProduce();
            InitcboLot();
        }

        protected void cboDirector_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboUnit();
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblLot.Visible = false;
            cboLot.Visible = false;
            Label15.Visible = false;

            if ((RadioButtonList1.SelectedValue == "2") || (RadioButtonList1.SelectedValue == "A3"))
            {
                cboProduce.SelectedIndex = 0;
                cboProduce.Visible = true;
                Label15.Visible = true;
            }
            else
            {
                cboProduce.SelectedIndex = 0;
                cboProduce.Visible = false;
            }

            if (RadioButtonList1.SelectedValue == "6")
            {
                txtitem_code.Text = "";
                txtitem_name.Text = "";
                txtitem_code.CssClass = "textboxdis";
                txtitem_name.CssClass = "textboxdis";
                txtitem_code.Enabled = false;
                txtitem_name.Enabled = false;
                imgList_item.Enabled = false;
                imgClear_item.Enabled = false;
            }
            else if (RadioButtonList1.SelectedValue == "9")
            {
                txtitem_code.Text = "";
                txtitem_name.Text = "";
                txtitem_code.CssClass = "textboxdis";
                txtitem_name.CssClass = "textboxdis";
                txtitem_code.Enabled = false;
                txtitem_name.Enabled = false;
                imgList_item.Enabled = false;
                imgClear_item.Enabled = false;
            }
            else if (RadioButtonList1.SelectedValue == "10")
            {
                txtitem_code.Text = "";
                txtitem_name.Text = "";
                txtitem_code.CssClass = "textboxdis";
                txtitem_name.CssClass = "textboxdis";
                txtitem_code.Enabled = false;
                txtitem_name.Enabled = false;
                imgList_item.Enabled = false;
                imgClear_item.Enabled = false;
            }
            else if (RadioButtonList1.SelectedValue == "11")
            {
                txtitem_code.Text = "";
                txtitem_name.Text = "";
                txtitem_code.CssClass = "textboxdis";
                txtitem_name.CssClass = "textboxdis";
                txtitem_code.Enabled = false;
                txtitem_name.Enabled = false;
                imgList_item.Enabled = false;
                imgClear_item.Enabled = false;
            }
            else if (RadioButtonList1.SelectedValue == "12")
            {
                txtitem_code.Text = "";
                txtitem_name.Text = "";
                txtitem_code.CssClass = "textboxdis";
                txtitem_name.CssClass = "textboxdis";
                txtitem_code.Enabled = false;
                txtitem_name.Enabled = false;
                imgList_item.Enabled = false;
                imgClear_item.Enabled = false;
            }
            else if (RadioButtonList1.SelectedValue == "13")
            {
                txtitem_code.Text = "";
                txtitem_name.Text = "";
                txtitem_code.CssClass = "textboxdis";
                txtitem_name.CssClass = "textboxdis";
                txtitem_code.Enabled = false;
                txtitem_name.Enabled = false;
                imgList_item.Enabled = false;
                imgClear_item.Enabled = false;
            }
            else if (RadioButtonList1.SelectedValue == "15")
            {
                txtitem_code.Text = "";
                txtitem_name.Text = "";
                txtitem_code.CssClass = "textboxdis";
                txtitem_name.CssClass = "textboxdis";
                txtitem_code.Enabled = false;
                txtitem_name.Enabled = false;
                imgList_item.Enabled = false;
                imgClear_item.Enabled = false;
            }
            else if (RadioButtonList1.SelectedValue == "16")
            {
                txtitem_code.Text = "";
                txtitem_name.Text = "";
                txtitem_code.CssClass = "textboxdis";
                txtitem_name.CssClass = "textboxdis";
                txtitem_code.Enabled = false;
                txtitem_name.Enabled = false;
                imgList_item.Enabled = false;
                imgClear_item.Enabled = false;
                lblLot.Visible = true;
                cboLot.Visible = true;
            }
            else if (RadioButtonList1.SelectedValue == "17")
            {
                txtitem_code.Text = "";
                txtitem_name.Text = "";
                txtitem_code.CssClass = "textboxdis";
                txtitem_name.CssClass = "textboxdis";
                txtitem_code.Enabled = false;
                txtitem_name.Enabled = false;
                imgList_item.Enabled = false;
                imgClear_item.Enabled = false;
                lblLot.Visible = true;
                cboLot.Visible = true;
            }
            else if (RadioButtonList1.SelectedValue == "A2" || 
                RadioButtonList1.SelectedValue == "A4" || 
                RadioButtonList1.SelectedValue == "A5" )
            {
                txtitem_code.Text = "";
                txtitem_name.Text = "";
                txtitem_code.CssClass = "textboxdis";
                txtitem_name.CssClass = "textboxdis";
                txtitem_code.Enabled = false;
                txtitem_name.Enabled = false;
                imgList_item.Enabled = false;
                imgClear_item.Enabled = false;
                lblLot.Visible = false;
                cboLot.Visible = false;
                lblBank.Visible = true;
                cboBank.Visible = true;
                InitcboBank();
            }

            else if (RadioButtonList1.SelectedValue == "A6")
            {
                txtitem_code.Text = "";
                txtitem_name.Text = "";
                txtitem_code.CssClass = "textboxdis";
                txtitem_name.CssClass = "textboxdis";
                txtitem_code.Enabled = false;
                txtitem_name.Enabled = false;
                imgList_item.Enabled = false;
                imgClear_item.Enabled = false;
                lblLot.Visible = false;
                cboLot.Visible = false;
                lblBank.Visible = true;
                cboBank.Visible = true;
                InitcboLoan();
            }
            else
            {
                txtitem_code.CssClass = "textbox";
                txtitem_name.CssClass = "textbox";
                txtitem_code.Enabled = true;
                txtitem_name.Enabled = true;
                imgList_item.Enabled = true;
                imgClear_item.Enabled = true;
                lblLot.Visible = false;
                cboLot.Visible = false;
                lblBank.Visible = false;
                cboBank.Visible = false;
            }


        }

    }
}
