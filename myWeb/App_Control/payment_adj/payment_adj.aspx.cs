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
using Aware.WebControls;
using myDLL;

namespace myWeb.App_Control.payment_adj
{
    public partial class payment_adj : PageBase
    {
        private string strPrefixCtr = "ctl00$ASPxRoundPanel1$ASPxRoundPanel2$ContentPlaceHolder1$";

        public static string getNumber(object pNumber)
        {
            string strNumber = String.Format("{0:#,##0.00}", float.Parse(pNumber.ToString()));
            return strNumber;
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            lblError.Text = "";
            if (!IsPostBack)
            {
                imgSaveOnly.Attributes.Add("onMouseOver", "src='../../images/button/save_add2.png'");
                imgSaveOnly.Attributes.Add("onMouseOut", "src='../../images/button/save_add.png'");

                imgFind.Attributes.Add("onMouseOver", "src='../../images/button/Search2.png'");
                imgFind.Attributes.Add("onMouseOut", "src='../../images/button/Search.png'");

                imgCancel.Attributes.Add("onMouseOver", "src='../../images/button/cancel2.png'");
                imgCancel.Attributes.Add("onMouseOut", "src='../../images/button/cancel.png'");

                string strYear = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["yearnow"].ToString();

                imgList_item.Attributes.Add("onclick", "OpenPopUp('750px','400px','93%','ค้นหาข้อมูลรายได้/ค่าใช้จ่าย' ,'../lov/item_lov.aspx?year='+document.forms[0]." + strPrefixCtr +
                              "cboYear.options[document.forms[0]." + strPrefixCtr + "cboYear.selectedIndex].value+" +
                              "'&item_code='+document.forms[0]." + strPrefixCtr + "txtitem_code.value+" +
                              "'&item_name='+document.forms[0]." + strPrefixCtr + "txtitem_name.value+" +
                              "'&ctrl1=" + txtitem_code.ClientID + "&ctrl2=" + txtitem_name.ClientID + "&item_type=C&from=member', '1');return false;");

                imgClear_item.Attributes.Add("onclick", "document.forms[0]." + strPrefixCtr + "txtitem_code.value='';" +
                                        "document.forms[0]." + strPrefixCtr + "txtitem_name.value=''; return false;");

                panelSeek.Visible = true;

                ViewState["sort"] = "person_code";
                ViewState["direction"] = "ASC";
                //InitcboYear();
                //InitcboPay_Month();
                //InitcboPay_Year();
                InitcboRound();
                InitcboPay_Month_OLD();
                InitcboPay_Year_OLD();
                InitcboPerson_group();
                InitcboDirector();
                InitcboUnit();

                lblMonthOld.Visible = false;
                cboPay_Month_OLD.Visible = false;
                lblYearOld.Visible = false;
                cboPay_Year_OLD.Visible = false;

                imgSaveOnly.Visible = false;
                imgFind.Visible = true;
                imgCancel.Visible = true;
                imgSaveOnly.Attributes.Add("onclick", "return confirm(\"คุณต้องการบันทึกข้อมูลนี้หรือไม่ ?\");");
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

        private void InitcboPay_Month_OLD()
        {
            string strMonth = string.Empty;
            strMonth = cboPay_Month_OLD.SelectedValue;
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
            cboPay_Month_OLD.Items.Clear();
            odt = ((DataSet)Application["xmlconfig"]).Tables["cboMonth"];
            for (i = 0; i <= odt.Rows.Count - 1; i++)
            {
                cboPay_Month_OLD.Items.Add(new ListItem(odt.Rows[i]["Text"].ToString(), odt.Rows[i]["Value"].ToString()));
            }
            if (cboPay_Month_OLD.Items.FindByValue(strMonth) != null)
            {
                cboPay_Month_OLD.SelectedIndex = -1;
                cboPay_Month_OLD.Items.FindByValue(strMonth).Selected = true;
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

        private void InitcboPay_Year_OLD()
        {
            string strYear = string.Empty;
            strYear = cboPay_Year_OLD.SelectedValue;
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
            cboPay_Year_OLD.Items.Clear();
            odt = ((DataSet)Application["xmlconfig"]).Tables["cboYear"];
            for (i = 0; i <= odt.Rows.Count - 1; i++)
            {
                cboPay_Year_OLD.Items.Add(new ListItem(odt.Rows[i]["Text"].ToString(), odt.Rows[i]["Value"].ToString()));
            }
            if (cboPay_Year_OLD.Items.FindByValue(strYear) != null)
            {
                cboPay_Year_OLD.SelectedIndex = -1;
                cboPay_Year_OLD.Items.FindByValue(strYear).Selected = true;
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
            strCriteria = " and unit.unit_year = '" + strYear + "'  and  unit.c_active='Y' ";
            strCriteria = strCriteria + " and unit.director_code = '" + strDirector_code + "' ";
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
            //this.imgSaveOnly.Click += new System.Web.UI.ImageClickEventHandler(this.imgSaveOnly_Click);
        }
        #endregion

        private bool saveData()
        {
            bool blnResult = false;
            string strMessage = string.Empty;
            string strActive = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty;
            string strScript = string.Empty;
            string pitem_code = string.Empty;
            string ppayment_item_tax = "Y";
            string ppayment_item_sos = "Y";
            string pcomments_sub = string.Empty;
            int i;
            cPayment oPayment = new cPayment();
            try
            {
                strActive = "Y";
                strCreatedBy = Session["username"].ToString();
                strUpdatedBy = Session["username"].ToString();
                pitem_code = txtitem_code.Text;

                if (pitem_code.Length > 0)
                {
                    GridViewRow gviewRow;

                    for (i = 0; i <= GridView1.Rows.Count - 1; i++)
                    {
                        gviewRow = GridView1.Rows[i];
                        HiddenField hdfpayment_doc = (HiddenField)gviewRow.FindControl("hdfpayment_doc");
                        AwNumeric txtmoney_credit = (AwNumeric)gviewRow.FindControl("txtmoney_credit");
                        CheckBox CheckBox1 = (CheckBox)gviewRow.FindControl("CheckBox1");

                        if (CheckBox1.Checked)
                        {
                            DataSet ds = new DataSet();
                            string strCheckDup = string.Empty;
                            strCheckDup = " and payment_doc = '" + hdfpayment_doc.Value + "' " +
                                                          " and item_code = '" + pitem_code + "' ";
                            if (!oPayment.SP_PAYMENT_SEL(strCheckDup, ref ds, ref strMessage))
                            {
                                lblError.Text = strMessage;
                            }
                            else
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    string strpayment_detail_id = ds.Tables[0].Rows[0]["payment_detail_id"].ToString();
                                    string strBudgetType = ds.Tables[0].Rows[0]["payment_detail_budget_type"].ToString();
                                    if (!oPayment.SP_PAYMENT_DETAIL_UPD(hdfpayment_doc.Value, pitem_code, "0", txtmoney_credit.Value.ToString(), ppayment_item_tax,
                                                                                                ppayment_item_sos, pcomments_sub, strActive, strUpdatedBy, strBudgetType, strpayment_detail_id,
                                                                                                ref strMessage))
                                    {
                                        lblError.Text = strMessage;
                                    }
                                }
                                else
                                {
                                    if (!oPayment.SP_PAYMENT_DETAIL_INS(hdfpayment_doc.Value, pitem_code, "0", txtmoney_credit.Value.ToString(), ppayment_item_tax,
                                                                                                ppayment_item_sos, pcomments_sub, strActive, strCreatedBy, ref strMessage))
                                    {
                                        lblError.Text = strMessage;
                                    }
                                }
                            }
                        }
                    }
                }
                blnResult = true;
            }
            catch (Exception ex)
            {
                blnResult = false;
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oPayment.Dispose();
            }
            return blnResult;
        }

        private void BindGridView()
        {
            cPayment oPayment = new cPayment();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            int i;
            string strYear = cboYear.SelectedValue;
            string strpay_month = cboPay_Month.SelectedValue;
            string strpay_year = cboPay_Year.SelectedValue;
            string strPerson_group = cboPerson_group.SelectedValue;
            string strDirector = cboDirector.SelectedValue;
            string strUnit = cboUnit.SelectedValue;
            string strPay_Month = cboPay_Month.SelectedValue;
            string strPay_Year = cboPay_Year.SelectedValue;
            string strError = "";
            string stritem_code = txtitem_code.Text.Length >= 10 ? txtitem_code.Text.Substring(4, 6) : txtitem_code.Text;
            try
            {
                strCriteria = "  and  substring(item_code,5,6)= '" + stritem_code + "' " +
                                       " and payment_year='" + strYear + "' " +
                                       " and pay_month='" + strpay_month + "' " +
                                       " and pay_year='" + strpay_year + "' ";

                if (!strPerson_group.Equals(""))
                {
                    strCriteria = strCriteria + "  and  person_group_code= '" + strPerson_group + "' ";
                }

                if (!strDirector.Equals(""))
                {
                    strCriteria = strCriteria + "  and  director_code= '" + strDirector + "' ";
                }

                if (!strUnit.Equals(""))
                {
                    strCriteria = strCriteria + "  and  unit_code= '" + strUnit + "' ";
                }
                strCriteria += " and person_group_code IN (" + PersonGroupList + ") ";

                if (DirectorLock == "Y")
                {
                    strCriteria += " and substring(director_code,4,2) = substring('" + DirectorCode + "',4,2) ";

                }
                strCriteria += " and isnull(person_salaly_all,0) > 0 ";
 
                if (!oPayment.SP_PAYMENT_ITEM_TEMP_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    ds.Tables[0].DefaultView.Sort = ViewState["sort"] + " " + ViewState["direction"];
                    ds.Tables[0].Columns.Add("money_credit");
                    ds.Tables[0].Columns.Add("item_has");
                    for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        ds.Tables[0].Rows[i]["item_has"] = "N";
                        strError = "ข้อมูลพนักงานรหัส : " + Helper.CStr(ds.Tables[0].Rows[i]["person_code"]) + " มีปัญหา";

                        if (RadioButtonList2.SelectedValue.Equals("M"))
                        {
                            ds.Tables[0].Rows[i]["money_credit"] = txtrate1.Value;
                        }
                        else
                        {
                            if (ds.Tables[0].Rows[i]["person_salaly_all"] != null)
                            {
                                ds.Tables[0].Rows[i]["money_credit"] = Math.Round(double.Parse(ds.Tables[0].Rows[i]["person_salaly_all"].ToString()) * double.Parse(txtrate1.Value.ToString()) / 100, 0, MidpointRounding.AwayFromZero);
                            }
                            // ds.Tables[0].Rows[i]["money_credit"] = double.Parse(ds.Tables[0].Rows[i]["person_salaly"].ToString()) * double.Parse(txtrate1.Value.ToString()) / 100;
                        }
                    }
                    ds.Tables[0].AcceptChanges();
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();

                    imgSaveOnly.Visible = false;
                    imgFind.Visible = true;
                    imgCancel.Visible = true;


                    if (GridView1.Rows.Count > 0)
                    {
                        RadioButtonList1.Enabled = false;
                        cboPay_Month_OLD.Enabled = false;
                        cboPay_Year_OLD.Enabled = false;
                        cboYear.Enabled = false;
                        cboPerson_group.Enabled = false;
                        cboDirector.Enabled = false;
                        cboUnit.Enabled = false;
                        cboPay_Month.Enabled = false;
                        cboPay_Year.Enabled = false;
                        txtitem_code.Enabled = false;
                        txtitem_name.Enabled = false;
                        imgList_item.Enabled = false;
                        imgClear_item.Enabled = false;
                        RadioButtonList2.Enabled = false;
                        txtrate1.ReadOnly = true;

                        imgSaveOnly.Visible = true;
                        imgFind.Visible = false;
                        imgCancel.Visible = true;

                    }

                    if (RadioButtonList2.SelectedValue.Equals("P"))
                    {
                        GridView1.Columns[GridView1.Columns.Count - 2].Visible = true;
                    }
                    else
                    {
                        GridView1.Columns[GridView1.Columns.Count - 2].Visible = false;
                    }

                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString() + "\n" + strError;
            }
            finally
            {
                oPayment.Dispose();
                ds.Dispose();
            }
        }

        private void BindGridView2()
        {
            cPayment oPayment = new cPayment();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            int i;
            string strYear = cboYear.SelectedValue;
            string strpay_month = cboPay_Month.SelectedValue;
            string strpay_year = cboPay_Year.SelectedValue;
            string strpay_month_old = cboPay_Month_OLD.SelectedValue;
            string strpay_year_old = cboPay_Year_OLD.SelectedValue;
            string strPerson_group = cboPerson_group.SelectedValue;
            string strDirector = cboDirector.SelectedValue;
            string strUnit = cboUnit.SelectedValue;
            string strPay_Month = cboPay_Month.SelectedValue;
            string strPay_Year = cboPay_Year.SelectedValue;
            string stritem_code = txtitem_code.Text.Length >= 10 ? txtitem_code.Text.Substring(4, 6) : txtitem_code.Text;

            try
            {
                strCriteria = "  and  substring(item_code,5,6)= '" + stritem_code + "' " +
                                       " and payment_year='" + strYear + "' " +
                                       " and pay_month='" + strpay_month + "' " +
                                       " and pay_year='" + strpay_year + "' ";

                if (!strPerson_group.Equals(""))
                {
                    strCriteria = strCriteria + "  and  person_group_code= '" + strPerson_group + "' ";
                }

                if (!strDirector.Equals(""))
                {
                    strCriteria = strCriteria + "  and  director_code= '" + strDirector + "' ";
                }

                if (!strUnit.Equals(""))
                {
                    strCriteria = strCriteria + "  and  unit_code= '" + strUnit + "' ";
                }

                strCriteria += " and person_group_code IN (" + PersonGroupList + ") ";
                if (DirectorLock == "Y")
                {
                    strCriteria += " and substring(director_code,4,2) = substring('" + DirectorCode + "',4,2) ";
                }


                if (!oPayment.SP_PAYMENT_ITEM_TEMP_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    ds.Tables[0].DefaultView.Sort = ViewState["sort"] + " " + ViewState["direction"];
                    ds.Tables[0].Columns.Add("money_credit");
                    ds.Tables[0].Columns.Add("item_has");

                    string decPayment_item_pay = "0";
                    cPayment oPayment2 = new cPayment();
                    DataSet ds2 = new DataSet();
                    strCriteria = "  and  substring(item_code,5,6)= '" + stritem_code + "' " +
                                   " and pay_month='" + strpay_month_old + "' " +
                                   " and pay_year='" + strpay_year_old + "' ";
                    if (!oPayment.SP_PAYMENT_SEL(strCriteria, ref ds2, ref strMessage))
                    {
                        lblError.Text = strMessage;
                    }
                    else
                    {
                        if (ds2.Tables[0].Rows.Count > 0)
                        {

                            for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                            {
                                DataRow[] result = ds2.Tables[0].Select("person_code = '" + ds.Tables[0].Rows[i]["person_code"].ToString() + "'");
                                if (result.Count() > 0)
                                {
                                    decPayment_item_pay = result[0]["payment_item_pay"].ToString();
                                    ds.Tables[0].Rows[i]["item_has"] = "N";
                                    ds.Tables[0].Rows[i]["money_credit"] = decPayment_item_pay;
                                }
                            }




                            //cPayment oPayment2 = new cPayment();
                            //DataSet ds2 = new DataSet();
                            //strCriteria = "  and  item_code= '" + stritem_code + "' " +
                            //               " and pay_month='" + strpay_month_old + "' " +
                            //               " and pay_year='" + strpay_year_old + "' " +
                            //               " and person_code='" + ds.Tables[0].Rows[i]["person_code"] + "' ";
                            //if (!oPayment.SP_PAYMENT_SEL(strCriteria, ref ds2, ref strMessage))
                            //{
                            //    lblError.Text = strMessage;
                            //}
                            //else
                            //{
                            //    if (ds2.Tables[0].Rows.Count > 0)
                            //    {
                            //        ds.Tables[0].Rows[i]["money_credit"] = ds2.Tables[0].Rows[0]["payment_item_pay"];
                            //    }
                            //}
                            //oPayment2.Dispose();
                            //ds2.Dispose();







                        }
                    }
                    oPayment2.Dispose();
                    ds2.Dispose();


                    ds.Tables[0].AcceptChanges();
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();

                    imgSaveOnly.Visible = false;
                    imgFind.Visible = true;
                    imgCancel.Visible = true;


                    if (GridView1.Rows.Count > 0)
                    {
                        RadioButtonList1.Enabled = false;
                        cboPay_Month_OLD.Enabled = false;
                        cboPay_Year_OLD.Enabled = false;
                        cboYear.Enabled = false;
                        cboPerson_group.Enabled = false;
                        cboDirector.Enabled = false;
                        cboUnit.Enabled = false;
                        cboPay_Month.Enabled = false;
                        cboPay_Year.Enabled = false;
                        txtitem_code.Enabled = false;
                        txtitem_name.Enabled = false;
                        imgList_item.Enabled = false;
                        imgClear_item.Enabled = false;
                        RadioButtonList2.Enabled = false;
                        txtrate1.ReadOnly = true;

                        imgSaveOnly.Visible = true;
                        imgFind.Visible = false;
                        imgCancel.Visible = true;

                    }

                    if (RadioButtonList2.SelectedValue.Equals("P"))
                    {
                        GridView1.Columns[GridView1.Columns.Count - 2].Visible = true;
                    }
                    else
                    {
                        GridView1.Columns[GridView1.Columns.Count - 2].Visible = false;
                    }

                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oPayment.Dispose();
                ds.Dispose();
            }
        }

        private void BindGridView3()
        {
            cPayment oPayment = new cPayment();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            int i;
            string strYear = cboYear.SelectedValue;
            string strpay_month = cboPay_Month.SelectedValue;
            string strpay_year = cboPay_Year.SelectedValue;
            string strPerson_group = cboPerson_group.SelectedValue;
            string strDirector = cboDirector.SelectedValue;
            string strUnit = cboUnit.SelectedValue;
            string strPay_Month = cboPay_Month.SelectedValue;
            string strPay_Year = cboPay_Year.SelectedValue;
            string stritem_code = txtitem_code.Text;
            try
            {
                strCriteria = " and payment_year='" + strYear + "' " +
                                       " and pay_month='" + strpay_month + "' " +
                                       " and pay_year='" + strpay_year + "' ";

                if (!strPerson_group.Equals(""))
                {
                    strCriteria = strCriteria + "  and  person_group_code= '" + strPerson_group + "' ";
                }

                if (!strDirector.Equals(""))
                {
                    strCriteria = strCriteria + "  and  director_code= '" + strDirector + "' ";
                }

                if (!strUnit.Equals(""))
                {
                    strCriteria = strCriteria + "  and  unit_code= '" + strUnit + "' ";
                }

                strCriteria += " and person_group_code IN (" + PersonGroupList + ") ";
                if (DirectorLock == "Y")
                {
                    strCriteria += " and substring(director_code,4,2) = substring('" + DirectorCode + "',4,2) ";
                }


                if (!oPayment.SP_PAYMENT_HEAD_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    ds.Tables[0].DefaultView.Sort = ViewState["sort"] + " " + ViewState["direction"];
                    ds.Tables[0].Columns.Add("money_credit");
                    ds.Tables[0].Columns.Add("item_has");
                    for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        ds.Tables[0].Rows[i]["item_has"] = "N";
                        if (RadioButtonList2.SelectedValue.Equals("M"))
                        {
                            ds.Tables[0].Rows[i]["money_credit"] = txtrate1.Value;
                        }
                        else
                        {
                            // ds.Tables[0].Rows[i]["money_credit"] = Math.Round(((double.Parse(txtrate1.Value.ToString())) * dblperson_salaly) / 100, 0, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["money_credit"] = Math.Round(double.Parse(ds.Tables[0].Rows[i]["person_salaly"].ToString()) * double.Parse(txtrate1.Value.ToString()) / 100, 0, MidpointRounding.AwayFromZero);
                        }
                    }
                    ds.Tables[0].AcceptChanges();
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();

                    imgSaveOnly.Visible = false;
                    imgFind.Visible = true;
                    imgCancel.Visible = true;


                    if (GridView1.Rows.Count > 0)
                    {
                        RadioButtonList1.Enabled = false;
                        cboPay_Month_OLD.Enabled = false;
                        cboPay_Year_OLD.Enabled = false;
                        cboYear.Enabled = false;
                        cboPerson_group.Enabled = false;
                        cboDirector.Enabled = false;
                        cboUnit.Enabled = false;
                        cboPay_Month.Enabled = false;
                        cboPay_Year.Enabled = false;
                        txtitem_code.Enabled = false;
                        txtitem_name.Enabled = false;
                        imgList_item.Enabled = false;
                        imgClear_item.Enabled = false;
                        RadioButtonList2.Enabled = false;
                        txtrate1.ReadOnly = true;

                        imgSaveOnly.Visible = true;
                        imgFind.Visible = false;
                        imgCancel.Visible = true;

                    }

                    if (RadioButtonList2.SelectedValue.Equals("P"))
                    {
                        GridView1.Columns[GridView1.Columns.Count - 2].Visible = true;
                    }
                    else
                    {
                        GridView1.Columns[GridView1.Columns.Count - 2].Visible = false;
                    }

                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oPayment.Dispose();
                ds.Dispose();
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.Header))
            {
                //Find the checkbox control in header and add an attribute
                ((CheckBox)e.Row.FindControl("cbSelectAll")).Attributes.Add("onclick", "javascript:SelectAll('" +
                        ((CheckBox)e.Row.FindControl("cbSelectAll")).ClientID + "')");

                for (int iCol = 0; iCol < e.Row.Cells.Count; iCol++)
                {
                    e.Row.Cells[iCol].Attributes.Add("class", "table_h");
                    e.Row.Cells[iCol].Wrap = false;
                    ViewState["sumall"] = "0.00";
                }
            }
            else if (e.Row.RowType.Equals(DataControlRowType.DataRow) || e.Row.RowState.Equals(DataControlRowState.Alternate))
            {
                #region Set datagrid row color
                string strEvenColor, strOddColor, strMouseOverColor;
                strEvenColor = ((DataSet)Application["xmlconfig"]).Tables["colorDataGridRow"].Rows[0]["Even"].ToString();
                strOddColor = ((DataSet)Application["xmlconfig"]).Tables["colorDataGridRow"].Rows[0]["Odd"].ToString();
                strMouseOverColor = ((DataSet)Application["xmlconfig"]).Tables["colorDataGridRow"].Rows[0]["MouseOver"].ToString();

                e.Row.Style.Add("valign", "top");
                e.Row.Style.Add("cursor", "hand");
                e.Row.Attributes.Add("onMouseOver", "this.style.backgroundColor='" + strMouseOverColor + "'");

                if (e.Row.RowState.Equals(DataControlRowState.Alternate))
                {
                    e.Row.Attributes.Add("bgcolor", strOddColor);
                    e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor='" + strOddColor + "'");
                }
                else
                {
                    e.Row.Attributes.Add("bgcolor", strEvenColor);
                    e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor='" + strEvenColor + "'");
                }
                #endregion
                Label lblNo = (Label)e.Row.FindControl("lblNo");
                int nNo = (GridView1.PageSize * GridView1.PageIndex) + e.Row.RowIndex + 1;
                lblNo.Text = nNo.ToString();
                AwNumeric txtmoney_credit = (AwNumeric)e.Row.FindControl("txtmoney_credit");
                txtmoney_credit.Attributes.Add("onchange", "funcsum();");

                ViewState["sumall"] = String.Format("{0:#,##0.00}", decimal.Parse(ViewState["sumall"].ToString()) + decimal.Parse(txtmoney_credit.Text));
            }
            else if (e.Row.RowType.Equals(DataControlRowType.Footer))
            {
                AwNumeric txtsummoney_credit = (AwNumeric)e.Row.FindControl("txtsummoney_credit");
                txtsummoney_credit.Value = ViewState["sumall"].ToString();
            }
        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.Header))
            {
                #region Create Item Header
                bool bSort = false;
                int i = 0;
                for (i = 0; i < GridView1.Columns.Count; i++)
                {
                    if (ViewState["sort"].Equals(GridView1.Columns[i].SortExpression))
                    {
                        bSort = true;
                        break;
                    }
                }
                if (bSort)
                {
                    foreach (System.Web.UI.Control c in e.Row.Controls[i].Controls)
                    {
                        if (c.GetType().ToString().Equals("System.Web.UI.WebControls.DataControlLinkButton"))
                        {
                            if (ViewState["direction"].Equals("ASC"))
                            {
                                ((LinkButton)c).Text += "<img border=0 src='" + ((DataSet)Application["xmlconfig"]).Tables["imgAsc"].Rows[0]["img"].ToString() + "'>";
                            }
                            else
                            {
                                ((LinkButton)c).Text += "<img border=0 src='" + ((DataSet)Application["xmlconfig"]).Tables["imgDesc"].Rows[0]["img"].ToString() + "'>";
                            }
                        }
                    }
                }
                #endregion
            }
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                if (ViewState["sort"].ToString().Equals(e.SortExpression.ToString()))
                {
                    if (ViewState["direction"].Equals("DESC"))
                        ViewState["direction"] = "ASC";
                    else
                        ViewState["direction"] = "DESC";
                }
                else
                {
                    ViewState["sort"] = e.SortExpression;
                    ViewState["direction"] = "ASC";
                }
                BindGridView();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        protected void imgFind_Click(object sender, ImageClickEventArgs e)
        {
            if (RadioButtonList1.SelectedValue.Equals("A"))
            {
                BindGridView();
            }
            else if (RadioButtonList1.SelectedValue.Equals("B"))
            {
                BindGridView2();
            }
            else if (RadioButtonList1.SelectedValue.Equals("C"))
            {
                BindGridView3();
            }

        }

        protected void imgSaveOnly_Click(object sender, ImageClickEventArgs e)
        {
            if (saveData())
            {
                InitcboYear();
                InitcboPay_Month();
                InitcboPay_Year();
                InitcboPay_Month_OLD();
                InitcboPay_Year_OLD();
                InitcboPerson_group();
                cboPerson_group.SelectedIndex = 0;
                InitcboDirector();
                cboDirector.SelectedIndex = 0;
                InitcboUnit();
                cboUnit.SelectedIndex = 0;

                lblMonthOld.Visible = false;
                cboPay_Month_OLD.Visible = false;
                lblYearOld.Visible = false;
                cboPay_Year_OLD.Visible = false;

                imgSaveOnly.Visible = false;
                imgFind.Visible = true;
                imgCancel.Visible = true;

                RadioButtonList1.SelectedValue = "A";
                RadioButtonList2.SelectedValue = "M";
                txtitem_code.Text = string.Empty;
                txtitem_name.Text = string.Empty;

                RadioButtonList1.Enabled = true;
                cboPay_Month_OLD.Enabled = true;
                cboPay_Year_OLD.Enabled = true;
                cboPerson_group.Enabled = true;
                cboDirector.Enabled = true;
                cboUnit.Enabled = true;
                //cboYear.Enabled = true;
                //cboPay_Month.Enabled = true;
                //cboPay_Year.Enabled = true;
                txtitem_code.Enabled = true;
                txtitem_name.Enabled = true;
                imgList_item.Enabled = true;
                imgClear_item.Enabled = true;
                RadioButtonList2.Enabled = true;
                txtrate1.ReadOnly = false;


                BindGridView();

                MsgBox("บันทึกข้อมูลสมบูรณ์");

            }
        }

        protected void imgCancel_Click(object sender, ImageClickEventArgs e)
        {
            //InitcboYear();
            //InitcboPay_Month();
            //InitcboPay_Year();
            InitcboPay_Month_OLD();
            InitcboPay_Year_OLD();
            InitcboPerson_group();
            cboPerson_group.SelectedIndex = 0;
            InitcboDirector();
            cboDirector.SelectedIndex = 0;
            InitcboUnit();
            cboUnit.SelectedIndex = 0;

            lblMonthOld.Visible = false;
            cboPay_Month_OLD.Visible = false;
            lblYearOld.Visible = false;
            cboPay_Year_OLD.Visible = false;

            imgSaveOnly.Visible = false;
            imgFind.Visible = true;
            imgCancel.Visible = true;
            RadioButtonList1.SelectedValue = "A";
            RadioButtonList2.SelectedValue = "M";
            txtitem_code.Text = string.Empty;
            txtitem_name.Text = string.Empty;

            RadioButtonList1.Enabled = true;
            cboPay_Month_OLD.Enabled = true;
            cboPay_Year_OLD.Enabled = true;
            cboPerson_group.Enabled = true;
            cboDirector.Enabled = true;
            cboUnit.Enabled = true;
            //cboYear.Enabled = true;
            //cboPay_Month.Enabled = true;
            //cboPay_Year.Enabled = true;
            txtitem_code.Enabled = true;
            txtitem_name.Enabled = true;
            imgList_item.Enabled = true;
            imgClear_item.Enabled = true;
            RadioButtonList2.Enabled = true;
            txtrate1.ReadOnly = false;

            panelSeek.Visible = true;

            BindGridView();
        }

        protected void cboDirector_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboUnit();
        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboDirector();
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonList1.SelectedValue.Equals("B"))
            {
                lblMonthOld.Visible = true;
                cboPay_Month_OLD.Visible = true;
                lblYearOld.Visible = true;
                cboPay_Year_OLD.Visible = true;
                lblRate1.Visible = false;
                RadioButtonList2.Visible = false;
                lblRate2.Visible = false;
                txtrate1.Visible = false;
                panelSeek.Visible = true;

            }
            else
            {
                lblMonthOld.Visible = false;
                cboPay_Month_OLD.Visible = false;
                lblYearOld.Visible = false;
                cboPay_Year_OLD.Visible = false;

                lblRate1.Visible = true;
                RadioButtonList2.Visible = true;
                lblRate2.Visible = true;
                txtrate1.Visible = true;
                panelSeek.Visible = true;

            }
        }

    }
}