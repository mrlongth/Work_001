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
    public partial class paymentdetail_report3 : PageBase
    {
        #region private data
        private bool[] blnAccessRight = new bool[5] { false, false, false, false, false };
        private string strPrefixCtr = "ctl00$ASPxRoundPanel1$ASPxRoundPanel2$ContentPlaceHolder1$";
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                imgList_item.Attributes.Add("onclick", "OpenPopUp('800px','400px','93%','ค้นหาข้อมูลรายได้/ค่าใช้จ่าย' ,'../lov/direct_pay_item_lov.aspx?year='+document.forms[0]." + strPrefixCtr +
                                                       "cboYear.options[document.forms[0]." + strPrefixCtr + "cboYear.selectedIndex].value+" +
                                                       "'&item_code='+document.forms[0]." + strPrefixCtr + "txtitem_code.value+" +
                                                       "'&item_name='+document.forms[0]." + strPrefixCtr + "txtitem_name.value+" +
                                                       "'&ctrl1=" + txtitem_code.ClientID + "&ctrl2=" + txtitem_name.ClientID + "&from=report', '1');return false;");

                imgClear_item.Attributes.Add("onclick", "document.forms[0]." + strPrefixCtr + "txtitem_code.value='';" +
                                        "document.forms[0]." + strPrefixCtr + "txtitem_name.value='';return false;");




                imgList_position.Attributes.Add("onclick", "OpenPopUp('800px','400px','93%','ค้นหาข้อมูลตำแหน่งปัจจุบัน' ,'../lov/position_lov.aspx?position_code='+$('#" + txtposition_code.ClientID + "').val()+'&position_name='+$('#" + txtposition_name.ClientID + "').val()+'&ctrl1=" + txtposition_code.ClientID + "&" +
                                                               "ctrl2=" + txtposition_name.ClientID + "&show=1', '1');return false;");
                imgClear_position.Attributes.Add("onclick", "$('#" + txtposition_code.ClientID + "').val('');$('#" + txtposition_name.ClientID + "').val(''); return false;");

                InitcboRound();
                InitcboPerson_group();
                InitcboBudgetType();

                InitcboType_position();
                InitcboLevel_position();
                InitcboPerson_manage();

                cboProduce.Enabled = false;

                LabelPosition.Visible = false;
                txtposition_code.Visible = false;

                imgList_position.Visible = false;
                imgClear_position.Visible = false;
                txtposition_name.Visible = false;

                LabelLevel_position.Visible = false;
                cboLevel_position.Visible = false;

                LabelPerson_manage.Visible = false;
                cboPerson_manage.Visible = false;

                LabelType_position.Visible = false;
                cboType_position.Visible = false;
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
            InitcboLot();
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
            strCriteria += " and person_group_code IN (" + PersonGroupList + ") ";
            if (oPerson_group.SP_PERSON_GROUP_SEL(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboPerson_group.Items.Clear();
                cboPerson_group.Items.Add(new ListItem("---- เลือกข้อมูลทั้งหมด ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboPerson_group.Items.Add(new ListItem(dt.Rows[i]["person_group_name"].ToString(), dt.Rows[i]["person_group_code"].ToString()));
                }
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
            strCriteria = " and unit.unit_year = '" + strYear + "'  and  unit.c_active='Y' " +
                                   " and unit.director_code = '" + strDirector_code + "' ";
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

        private void InitcboType_position()
        {
            cPosition oPosition = new cPosition();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strtype_position_code = string.Empty;
            strtype_position_code = cboType_position.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and c_active='Y' ";
            if (oPosition.SP_TYPE_POSITION_SEL(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboType_position.Items.Clear();
                cboType_position.Items.Add(new ListItem("---- เลือกข้อมูลทั้งหมด ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboType_position.Items.Add(new ListItem(dt.Rows[i]["type_position_name"].ToString(), dt.Rows[i]["type_position_code"].ToString()));
                }
                if (cboType_position.Items.FindByValue(strtype_position_code) != null)
                {
                    cboType_position.SelectedIndex = -1;
                    cboType_position.Items.FindByValue(strtype_position_code).Selected = true;
                }
            }
        }

        private void InitcboLevel_position()
        {
            cPosition oPosition = new cPosition();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strlevel_position_code = string.Empty;
            strlevel_position_code = cboLevel_position.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and c_active='Y' ";
            if (oPosition.SP_LEVEL_POSITION_SEL(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboLevel_position.Items.Clear();
                cboLevel_position.Items.Add(new ListItem("---- เลือกข้อมูลทั้งหมด ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboLevel_position.Items.Add(new ListItem(dt.Rows[i]["level_position_name"].ToString(), dt.Rows[i]["level_position_code"].ToString()));
                }
                if (cboLevel_position.Items.FindByValue(strlevel_position_code) != null)
                {
                    cboLevel_position.SelectedIndex = -1;
                    cboLevel_position.Items.FindByValue(strlevel_position_code).Selected = true;
                }
            }
        }

        private void InitcboPerson_manage()
        {
            cPerson_manage oPosition = new cPerson_manage();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strPerson_manage_code = string.Empty;
            strPerson_manage_code = cboPerson_manage.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and c_active='Y' ";
            if (oPosition.SP_PERSON_MANAGE_SEL(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboPerson_manage.Items.Clear();
                cboPerson_manage.Items.Add(new ListItem("---- เลือกข้อมูลทั้งหมด ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboPerson_manage.Items.Add(new ListItem(dt.Rows[i]["Person_manage_name"].ToString(), dt.Rows[i]["Person_manage_code"].ToString()));
                }
                if (cboPerson_manage.Items.FindByValue(strPerson_manage_code) != null)
                {
                    cboPerson_manage.SelectedIndex = -1;
                    cboPerson_manage.Items.FindByValue(strPerson_manage_code).Selected = true;
                }
            }
        }

        private void InitcboBudgetType()
        {

            cCommon oCommon = new cCommon();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strCode = this.myBudgetType;
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
            if (strCode != "M") cboBudget_type.Enabled = false;
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

            string strPosition_code = string.Empty;
            string strPosition_name = string.Empty;
            string strType_position = string.Empty;
            string strLevel_position = string.Empty;
            string strPerson_manage = string.Empty;
            string strCondition = string.Empty;

            strYear = cboYear.SelectedValue;
            strPay_Year = cboPay_Year.SelectedValue;

            strPay_Month = cboPay_Month.SelectedValue;
            if (strPay_Month.Length > 0)
                strCondition += "เดือนที่จ่าย : " + cboPay_Month.SelectedItem.Text + "   ";

            stritem_code = txtitem_code.Text;
            if (stritem_code.Length > 0)
                strCondition += "รายได้/จ่าย : " + txtitem_code.Text + "-" + txtitem_name.Text + "   ";

            strperson_group_code = cboPerson_group.SelectedValue;
            if (strperson_group_code.Length > 0)
                strCondition += "กลุ่มบุคคลากร : " + cboPerson_group.SelectedItem.Text + "   ";

            strdirector_code = cboDirector.SelectedValue;
            if (strdirector_code.Length > 0)
                strCondition += "สังกัด : " + cboDirector.SelectedItem.Text + "   ";

            strunit_code = cboUnit.SelectedValue;
            if (strunit_code.Length > 0)
                strCondition += "หน่วยงาน : " + cboUnit.SelectedItem.Text + "   ";

            strProduce = cboProduce.SelectedValue;
            if (strProduce.Length > 0)
                strCondition += "ผลผลิต : " + cboProduce.SelectedItem.Text + "   ";

            if (txtposition_code.Enabled) strPosition_code = txtposition_code.Text;
            if (txtposition_name.Enabled) strPosition_name = txtposition_name.Text;
            if (cboType_position.Enabled && cboType_position.SelectedIndex != 0) strType_position = cboType_position.SelectedItem.Text;
            if (cboLevel_position.Enabled) strLevel_position = cboLevel_position.SelectedValue;
            if (cboPerson_manage.Enabled) strPerson_manage = cboPerson_manage.SelectedValue;

            strLot = cboLot.SelectedValue;
            if (!RadioButtonList1.SelectedValue.Equals("20") &&
                !RadioButtonList1.SelectedValue.Equals("A2") &&
                !RadioButtonList1.SelectedValue.Equals("A3") &&
                !RadioButtonList1.SelectedValue.Equals("A4") &&
                !RadioButtonList1.SelectedValue.Equals("A6") &&
                !RadioButtonList1.SelectedValue.Equals("A7") &&
                !RadioButtonList1.SelectedValue.Equals("A8"))
            {
                strCriteria = "  And  view_payment.payment_detail_budget_type = '" + cboBudget_type.SelectedValue + "'  ";

            }
            if (!strYear.Equals(""))
            {
                if (!RadioButtonList1.SelectedValue.Equals("A4") &&
                    !RadioButtonList1.SelectedValue.Equals("20") &&
                    !RadioButtonList1.SelectedValue.Equals("A6") &&
                    !RadioButtonList1.SelectedValue.Equals("A7") &&
                    !RadioButtonList1.SelectedValue.Equals("A2") &&
                    !RadioButtonList1.SelectedValue.Equals("A8"))
                {
                    strCriteria = strCriteria + "  And  view_payment.payment_year = '" + strYear + "'  ";
                }
                else
                {
                    if (RadioButtonList1.SelectedValue.Equals("A2"))
                    {
                        strCriteria = strCriteria + "  And  item_acc_year = '" + strYear + "'  ";
                    }
                    else if (RadioButtonList1.SelectedValue.Equals("A8"))
                    {
                        strCriteria = strCriteria + "  And  payment_head.payment_year = '" + strYear + "'  ";
                    }
                    else
                    {
                        strCriteria = strCriteria + "  And  cheque_year = '" + strYear + "'  ";
                    }
                }
            }

            if (!strPay_Month.Equals(""))
            {
                if (!RadioButtonList1.SelectedValue.Equals("A4") &&
                !RadioButtonList1.SelectedValue.Equals("17") &&
                !RadioButtonList1.SelectedValue.Equals("A1") &&
                !RadioButtonList1.SelectedValue.Equals("A1-2") &&
                !RadioButtonList1.SelectedValue.Equals("A8"))
                {
                    strCriteria = strCriteria + "  And  view_payment.pay_month='" + strPay_Month + "' ";
                }
                else
                {
                    if (RadioButtonList1.SelectedValue.Equals("A8"))
                    {
                        strCriteria = strCriteria + "  And  payment_head.pay_month = '" + strPay_Month + "'  ";
                    }
                }
            }

            if (!strPay_Year.Equals(""))
            {
                if (!RadioButtonList1.SelectedValue.Equals("A4") &&
                    !RadioButtonList1.SelectedValue.Equals("17") &&
                    !RadioButtonList1.SelectedValue.Equals("A1") &&
                    !RadioButtonList1.SelectedValue.Equals("A1-2") &&
                    !RadioButtonList1.SelectedValue.Equals("A8"))
                {
                    strCriteria = strCriteria + "  And  view_payment.pay_year='" + strPay_Year + "' ";
                }
                else
                {
                    if (RadioButtonList1.SelectedValue.Equals("A8"))
                    {
                        strCriteria = strCriteria + "  And  payment_head.pay_year = '" + strPay_Year + "'  ";
                    }
                }
            }

            if (!strperson_group_code.Equals(""))
            {
                if (!RadioButtonList1.SelectedValue.Equals("A4") &&
                    !RadioButtonList1.SelectedValue.Equals("A2"))
                {
                    if (cboBudget_type.SelectedValue != "R")
                    {
                        strCriteria = strCriteria + "  And  view_payment.payment_detail_person_group_code ='" + strperson_group_code + "' ";
                    }
                }
                else
                {
                    if (cboBudget_type.SelectedValue != "R")
                    {
                        strCriteria = strCriteria + "  And  view_payment.payment_detail_person_group_code ='" + strperson_group_code + "' ";
                    }
                }
            }

            if (!strdirector_code.Equals(""))
            {
                if (!RadioButtonList1.SelectedValue.Equals("20") &&
                    !RadioButtonList1.SelectedValue.Equals("A2") &&
                    !RadioButtonList1.SelectedValue.Equals("A3") &&
                    !RadioButtonList1.SelectedValue.Equals("A4") &&
                    !RadioButtonList1.SelectedValue.Equals("A6") &&
                    !RadioButtonList1.SelectedValue.Equals("A7") &&
                    !RadioButtonList1.SelectedValue.Equals("A2"))
                {
                    strCriteria = strCriteria + "  And  view_payment.payment_detail_director_code = '" + strdirector_code + "' ";
                }
                else
                {
                    strCriteria = strCriteria + "  And  view_payment.director_code = '" + strdirector_code + "' ";
                }

            }

            if (!strunit_code.Equals(""))
            {
                if (!RadioButtonList1.SelectedValue.Equals("20") &&
                    !RadioButtonList1.SelectedValue.Equals("A2") &&
                    !RadioButtonList1.SelectedValue.Equals("A3") &&
                    !RadioButtonList1.SelectedValue.Equals("A4") &&
                    !RadioButtonList1.SelectedValue.Equals("A6") &&
                    !RadioButtonList1.SelectedValue.Equals("A7") &&
                    !RadioButtonList1.SelectedValue.Equals("A2"))
                {
                    strCriteria = strCriteria + "  And  view_payment.payment_detail_unit_code= '" + strunit_code + "' ";
                }
                else
                {
                    strCriteria = strCriteria + "  And  view_payment.unit_code= '" + strunit_code + "' ";
                }
            }

            if (!stritem_code.Equals(""))
            {
                if (!RadioButtonList1.SelectedValue.Equals("A8"))
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
                if (!RadioButtonList1.SelectedValue.Equals("A2"))
                {
                    strCriteria = strCriteria + "  And  view_payment.payment_detail_lot_code= '" + strLot + "' ";
                }
            }

            if (!strPosition_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  view_payment.position_code= '" + strPosition_code + "' ";
            }
            if (!strPosition_name.Equals(""))
            {
                strCriteria = strCriteria + "  And  view_payment.position_name Like  '%" + strPosition_name + "%' ";
            }
            if (!strType_position.Equals(""))
            {
                strCriteria = strCriteria + "  And  view_payment.type_position_name= '" + strType_position + "' ";
            }
            if (!strLevel_position.Equals(""))
            {
                strCriteria = strCriteria + "  And  view_payment.person_level= '" + strLevel_position + "' ";
            }
            if (!strPerson_manage.Equals(""))
            {
                strCriteria = strCriteria + "  And  view_payment.person_manage_code= '" + strPerson_manage + "' ";
            }

            if (!RadioButtonList1.SelectedValue.Equals("A4") &&
                !RadioButtonList1.SelectedValue.Equals("20") &&
                !RadioButtonList1.SelectedValue.Equals("A2") &&
                !RadioButtonList1.SelectedValue.Equals("A3") &&
                !RadioButtonList1.SelectedValue.Equals("A7") &&
                !RadioButtonList1.SelectedValue.Equals("A6"))
            {
                if (cboBudget_type.SelectedValue != "R")
                {
                    strCriteria += " and view_payment.payment_detail_person_group_code IN (" + PersonGroupList + ") ";
                }
            }

            if (DirectorLock == "Y")
            {
                if (!RadioButtonList1.SelectedValue.Equals("A2") &&
                    !RadioButtonList1.SelectedValue.Equals("20") &&
                    !RadioButtonList1.SelectedValue.Equals("A3") &&
                    !RadioButtonList1.SelectedValue.Equals("A6") &&
                    !RadioButtonList1.SelectedValue.Equals("A7") &&
                    !RadioButtonList1.SelectedValue.Equals("A4"))
                {
                    strCriteria += " and substring(view_payment.payment_detail_director_code,4,2) = substring('" + DirectorCode + "',4,2) ";
                }
                else
                {
                    strCriteria += " and substring(director_code,4,2) = substring('" + DirectorCode + "',4,2) ";
                }
            }


            if (RadioButtonList1.SelectedValue.Equals("17"))
            {
                strCriteria = strCriteria + "  And  (pay_year+'/'+pay_month) <= '" + strPay_Year + "/" + strPay_Month + "' ";
                strCriteria = strCriteria + "  And  (pay_year+'/'+pay_month) >= '" + (int.Parse(strPay_Year) - 1) + "/10' ";
            }


            if (RadioButtonList1.SelectedValue.Equals("3"))
            {
                strReport_code = "Rep_paymentbyall";
                strCriteria = strCriteria.Replace("view_payment.", "");
            }
            else if (RadioButtonList1.SelectedValue.Equals("4"))
            {
                strReport_code = "Rep_paymentsumbyproduce";
                strCriteria = strCriteria.Replace("view_payment.", "");
            }
            else if (RadioButtonList1.SelectedValue.Equals("5"))
            {
                strReport_code = "Rep_paymentsumbyunit";
                if (cboBudget_type.SelectedValue == "R") strReport_code = "Rep_paymentincomesumbyunit";
                strCriteria = strCriteria.Replace("view_payment.", "");
            }

            else if (RadioButtonList1.SelectedValue.Equals("7"))
            {
                strReport_code = "Rep_paymentsumbyunitandpersongroup";
                strCriteria = strCriteria.Replace("view_payment.", "");
            }

            else if (RadioButtonList1.SelectedValue.Equals("16"))
            {
                strReport_code = "Rep_paymentbylot";
                strCriteria = strCriteria + "  And  item_type= 'D' ";
                strCriteria = strCriteria.Replace("view_payment.", "");
            }

            else if (RadioButtonList1.SelectedValue.Equals("A1"))
            {
                strReport_code = "Rep_paymentyearbylot";
                strCriteria = strCriteria + "  And  view_payment.item_type= 'D' ";
                Session["criteria2"] = strCriteria.Replace("view_payment.", "a.");
                strCriteria = strCriteria.Replace("view_payment.", "");
            }

            else if (RadioButtonList1.SelectedValue.Equals("A1-2"))
            {
                strReport_code = "Rep_paymentyearbylotproduce";
                strCriteria = strCriteria + "  And  view_payment.item_type= 'D' ";
                Session["criteria2"] = strCriteria.Replace("view_payment.", "a.");
                strCriteria = strCriteria.Replace("view_payment.", "");
            }

            else if (RadioButtonList1.SelectedValue.Equals("17"))
            {
                strReport_code = "Rep_paymentbylotyear";
                strCriteria = strCriteria + "  And  item_type= 'D' ";
                strCriteria = strCriteria.Replace("view_payment.", "");
            }

            else if (RadioButtonList1.SelectedValue.Equals("19"))
            {
                strCriteria = strCriteria + "  And  item_type= 'D' ";
                strReport_code = "Rep_paymentdebitbyperson";
                strCriteria = strCriteria.Replace("view_payment.", "");
            }
            else if (RadioButtonList1.SelectedValue.Equals("20"))
            {
                strCriteria += "  And  c_created_by = '" + base.UserLoginName + "' ";
                strCriteria += "  And  cheque_print= 'Y' ";
                strCriteria += "  And  cheque_money > 0 ";

                strReport_code = "Rep_cheque_record";
                strCriteria = strCriteria.Replace("view_payment.", "");
            }

            else if (RadioButtonList1.SelectedValue.Equals("A6"))
            {
                strCriteria += "  And  c_created_by = '" + base.UserLoginName + "' ";
                strCriteria += "  And  cheque_print= 'Y' ";
                strCriteria += "  And  cheque_money > 0 ";
                strReport_code = "Rep_cheque_recv";
                strCriteria = strCriteria.Replace("view_payment.", "");
            }

            else if (RadioButtonList1.SelectedValue.Equals("A7"))
            {
                strCriteria += "  And  c_created_by = '" + base.UserLoginName + "' ";
                strCriteria += "  And  cheque_print= 'Y' ";
                strCriteria += "  And  cheque_money > 0 ";
                strReport_code = "Rep_cheque_recv2";
                strCriteria = strCriteria.Replace("view_payment.", "");
            }


            else if (RadioButtonList1.SelectedValue.Equals("A2"))
            {
                strCriteria += "  And  c_created_by = '" + base.UserLoginName + "' ";
                strReport_code = "Rep_item_acc";
                if (cboBudget_type.SelectedValue == "R")
                {
                    strReport_code = "Rep_item_acc_income";
                }

                strCriteria = strCriteria.Replace("view_payment.", "");
            }
            else if (RadioButtonList1.SelectedValue.Equals("A3"))
            {
                strReport_code = "Rep_payment_return";
                strCriteria = strCriteria.Replace("view_payment.", "");
            }

            else if (RadioButtonList1.SelectedValue.Equals("A4"))
            {
                strReport_code = "Rep_person";
                strCriteria = strCriteria.Replace("view_payment.", "");
            }

            else if (RadioButtonList1.SelectedValue.Equals("A5"))
            {
                strReport_code = "Rep_paymentdebitbyposition";
                strCriteria = strCriteria + "  And  view_payment.item_type= 'D' ";
            }

            else if (RadioButtonList1.SelectedValue.Equals("A8"))
            {
                strReport_code = "Rep_payment_tax";
                strCriteria = strCriteria.Replace("view_payment.", "view_person_list.").Replace(".payment_detail_", ".");
            }

            Session["condition"] = strCondition;
            Session["criteria"] = strCriteria;
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
            cboLot.Enabled = false;

            LabelPosition.Visible = false;
            txtposition_code.Visible = false;

            imgList_position.Visible = false;
            imgClear_position.Visible = false;
            txtposition_name.Visible = false;

            LabelLevel_position.Visible = false;
            cboLevel_position.Visible = false;

            LabelPerson_manage.Visible = false;
            cboPerson_manage.Visible = false;

            LabelType_position.Visible = false;
            cboType_position.Visible = false;

            Label15.Visible = false;
            cboProduce.Visible = false;

            if (RadioButtonList1.SelectedValue == "2")
            {
                cboProduce.SelectedIndex = 0;
                cboProduce.Enabled = true;
            }
            else
            {
                cboProduce.SelectedIndex = 0;
                cboProduce.Enabled = false;
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
                cboLot.Enabled = true;
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
                cboLot.Enabled = true;
            }
            else if (RadioButtonList1.SelectedValue == "20"
                || RadioButtonList1.SelectedValue == "A6"
                || RadioButtonList1.SelectedValue == "A7")
            {
                txtitem_code.Text = "";
                txtitem_name.Text = "";
                txtitem_code.CssClass = "textboxdis";
                txtitem_name.CssClass = "textboxdis";
                txtitem_code.Enabled = false;
                txtitem_name.Enabled = false;
                imgList_item.Enabled = false;
                imgClear_item.Enabled = false;
                cboDirector.Enabled = false;
                cboUnit.Enabled = false;
                cboPerson_group.Enabled = false;
            }

            else if (RadioButtonList1.SelectedValue == "A1-2")
            {
                cboLot.Enabled = true;
                cboProduce.Visible = true;
                Label15.Visible = true;
                cboProduce.Enabled = true;
            }

            else if (RadioButtonList1.SelectedValue == "A2")
            {
                txtitem_code.Text = "";
                txtitem_name.Text = "";
                txtitem_code.CssClass = "textbox";
                txtitem_name.CssClass = "textbox";
                txtitem_code.Enabled = true;
                txtitem_name.Enabled = true;
                imgList_item.Enabled = true;
                imgClear_item.Enabled = true;
                cboDirector.Enabled = false;
                cboUnit.Enabled = false;
                cboPerson_group.Enabled = false;
            }
            else if (RadioButtonList1.SelectedValue == "A3")
            {
                txtitem_code.Text = "";
                txtitem_name.Text = "";
                txtitem_code.CssClass = "textbox";
                txtitem_name.CssClass = "textbox";
                txtitem_code.Enabled = true;
                txtitem_name.Enabled = true;
                imgList_item.Enabled = true;
                imgClear_item.Enabled = true;
                lblLot.Enabled = true;
            }
            else if (RadioButtonList1.SelectedValue == "A4")
            {
                txtitem_code.Text = "";
                txtitem_name.Text = "";
                txtitem_code.CssClass = "textboxdis";
                txtitem_name.CssClass = "textboxdis";
                txtitem_code.Enabled = false;
                txtitem_name.Enabled = false;
                imgList_item.Enabled = false;
                imgClear_item.Enabled = false;
                cboDirector.Enabled = true;
                cboUnit.Enabled = true;
                cboPerson_group.Enabled = true;
            }

            else if (RadioButtonList1.SelectedValue == "A5")
            {
                txtitem_code.Text = "";
                txtitem_name.Text = "";
                txtitem_code.CssClass = "textboxdis";
                txtitem_name.CssClass = "textboxdis";
                txtitem_code.Enabled = false;
                txtitem_name.Enabled = false;
                imgList_item.Enabled = false;
                imgClear_item.Enabled = false;
                cboDirector.Enabled = true;
                cboUnit.Enabled = true;
                cboPerson_group.Enabled = true;

                LabelPosition.Visible = true;
                txtposition_code.Visible = true;

                imgList_position.Visible = true;
                imgClear_position.Visible = true;
                txtposition_name.Visible = true;

                LabelLevel_position.Visible = true;
                cboLevel_position.Visible = true;

                LabelPerson_manage.Visible = true;
                cboPerson_manage.Visible = true;

                LabelType_position.Visible = true;
                cboType_position.Visible = true;

                txtitem_code.CssClass = "textbox";
                txtitem_name.CssClass = "textbox";
                txtitem_code.Enabled = true;
                txtitem_name.Enabled = true;
                imgList_item.Enabled = true;
                imgClear_item.Enabled = true;
                cboBudget_type.Enabled = true;
            }
            else
            {
                cboDirector.Enabled = true;
                cboUnit.Enabled = true;
                cboPerson_group.Enabled = true;
                txtitem_code.CssClass = "textbox";
                txtitem_name.CssClass = "textbox";
                txtitem_code.Enabled = true;
                txtitem_name.Enabled = true;
                imgList_item.Enabled = true;
                imgClear_item.Enabled = true;
                cboLot.Enabled = false;
            }
        }

    }
}
