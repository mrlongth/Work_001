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

namespace myWeb.App_Control.payment_member_type
{
    public partial class payment_member_type : PageBase
    {
        #region private data
        #endregion

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


                ViewState["sort"] = "person_code";
                ViewState["direction"] = "ASC";
                //InitcboYear();
                //InitcboPay_Month();
                //InitcboPay_Year();
                InitcboRound();
                InitcboMember_type();
                lblcredititem_code.Text = string.Empty;
                lblcredititem_name.Text = string.Empty;
                //cboYear.Enabled = true;
                //cboPay_Month.Enabled = true;
                //cboPay_Year.Enabled = true;
                cboMember_type.Enabled = true;
                txtrate1.Enabled = true;
                RadioButtonList1.Enabled = true;
                RadioButtonList1.SelectedValue = "A";

                imgSaveOnly.Visible = false;
                imgFind.Visible = true;
                imgCancel.Visible = true;

                lblRate1.Visible = false;
                lblItemCode.Visible = false;
                txtrate1.Visible = false;
                lblcredititem_code.Visible = false;
                lblcredititem_name.Visible = false;
                lblcredit_mid.Visible = false;

                lblRate2.Visible = false;
                lblItemCode2.Visible = false;
                txtrate2.Visible = false;
                lbldebitcompany_code.Visible = false;
                lbldebitcompany_name.Visible = false;
                lblcompany_mid.Visible = false;

                lblRate3.Visible = false;
                lblItemCode3.Visible = false;
                txtrate3.Visible = false;
                lbldebitextra_code.Visible = false;
                lbldebitextra_name.Visible = false;
                lblextra_mid.Visible = false;

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

        private void InitcboMember_type()
        {
            cMember_type oMember_type = new cMember_type();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strMember = string.Empty;
            strMember = cboMember_type.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and c_active='Y' ";
            strCriteria += " and person_group_code IN (" + PersonGroupList + ",'') ";

            if (oMember_type.SP_MEMBER_TYPE_SEL(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboMember_type.Items.Clear();
                cboMember_type.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboMember_type.Items.Add(new ListItem(dt.Rows[i]["member_type_name"].ToString(), dt.Rows[i]["member_type_code"].ToString()));
                }
                if (cboMember_type.Items.FindByValue(strMember) != null)
                {
                    cboMember_type.SelectedIndex = -1;
                    cboMember_type.Items.FindByValue(strMember).Selected = true;
                }
            }
        }

        private void InittxtItem_Code()
        {
            cMember_type oMember_type = new cMember_type();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strMember = string.Empty;
            strMember = cboMember_type.SelectedValue;
            lblcredititem_code.Text = string.Empty;
            lblcredititem_name.Text = string.Empty;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and member_type_code='" + strMember + "' and c_active='Y' ";

            if (oMember_type.SP_MEMBER_TYPE_SEL(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtrate1.Value = dt.Rows[0]["member_type_rate"];

                    lblcredititem_code.Text = dt.Rows[0]["item_code"].ToString();
                    lblcredititem_name.Text = dt.Rows[0]["item_name"].ToString();

                    txtrate2.Value = dt.Rows[0]["company_rate"];
                    lbldebitcompany_code.Text = dt.Rows[0]["company_code"].ToString();
                    lbldebitcompany_name.Text = dt.Rows[0]["item_company_name"].ToString();

                    txtrate3.Value = dt.Rows[0]["extra_rate"];
                    lbldebitextra_code.Text = dt.Rows[0]["extra_code"].ToString();
                    lbldebitextra_name.Text = dt.Rows[0]["item_extra_name"].ToString();

                }
                else
                {

                    txtrate1.Value = 0;
                    lblcredititem_code.Text = string.Empty;
                    lblcredititem_name.Text = string.Empty;

                    txtrate2.Value = 0;
                    lbldebitcompany_code.Text = string.Empty;
                    lbldebitcompany_name.Text = string.Empty;

                    txtrate3.Value = 0;
                    lbldebitextra_code.Text = string.Empty;
                    lbldebitextra_name.Text = string.Empty;


                }


                if (txtrate1.Text.Equals("0.00"))
                {
                    lblRate1.Visible = false;
                    lblItemCode.Visible = false;
                    txtrate1.Visible = false;
                    lblcredititem_code.Visible = false;
                    lblcredititem_name.Visible = false;
                    lblcredit_mid.Visible = false;
                }
                else
                {
                    lblRate1.Visible = true;
                    lblItemCode.Visible = true;
                    txtrate1.Visible = true;
                    lblcredititem_code.Visible = true;
                    lblcredititem_name.Visible = true;
                    lblcredit_mid.Visible = true;
                }


                if (txtrate2.Text.Equals("0.00"))
                {
                    lblRate2.Visible = false;
                    lblItemCode2.Visible = false;
                    txtrate2.Visible = false;
                    lbldebitcompany_code.Visible = false;
                    lbldebitcompany_name.Visible = false;
                    lblcompany_mid.Visible = false;
                }
                else
                {
                    lblRate2.Visible = true;
                    lblItemCode2.Visible = true;
                    txtrate2.Visible = true;
                    lbldebitcompany_code.Visible = true;
                    lbldebitcompany_name.Visible = true;
                    lblcompany_mid.Visible = true;
                }

                if (txtrate3.Text.Equals("0.00"))
                {
                    lblRate3.Visible = false;
                    lblItemCode3.Visible = false;
                    txtrate3.Visible = false;
                    lbldebitextra_code.Visible = false;
                    lbldebitextra_name.Visible = false;
                    lblextra_mid.Visible = false;
                }
                else
                {
                    lblRate3.Visible = true;
                    lblItemCode3.Visible = true;
                    txtrate3.Visible = true;
                    lbldebitextra_code.Visible = true;
                    lbldebitextra_name.Visible = true;
                    lblextra_mid.Visible = true;
                }

                string strGBK2 = string.Empty;
                strGBK2 = ((DataSet)Application["xmlconfig"]).Tables["MemberType"].Rows[0]["GBK2"].ToString();
                if (cboMember_type.SelectedValue.Equals(strGBK2))
                {
                    lblRate1.Visible = true;
                    txtrate1.Visible = true;
                    txtrate1.ReadOnly = true;

                    lblItemCode.Visible = true;
                    lblcredititem_code.Visible = true;
                    lblcredititem_name.Visible = true;
                    lblcredit_mid.Visible = true;

                }
                else
                {
                    txtrate1.ReadOnly = false;
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
            this.imgSaveOnly.Click += new System.Web.UI.ImageClickEventHandler(this.imgSaveOnly_Click);
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
                pitem_code = lblcredititem_code.Text;

                string strGBK2 = string.Empty;
                strGBK2 = ((DataSet)Application["xmlconfig"]).Tables["MemberType"].Rows[0]["GBK2"].ToString();


                GridViewRow gviewRow;

                for (i = 0; i <= GridView1.Rows.Count - 1; i++)
                {
                    gviewRow = GridView1.Rows[i];
                    HiddenField hdfpayment_doc = (HiddenField)gviewRow.FindControl("hdfpayment_doc");
                    AwNumeric txtperson_salaly = (AwNumeric)gviewRow.FindControl("txtperson_salaly");
                    AwNumeric txtmebertype_credit = (AwNumeric)gviewRow.FindControl("txtmebertype_credit");
                    AwNumeric txtcompany_credit = (AwNumeric)gviewRow.FindControl("txtcompany_credit");
                    AwNumeric txtextra_credit = (AwNumeric)gviewRow.FindControl("txtextra_credit");


                    if (!txtrate1.Text.Equals("0.00") | cboMember_type.SelectedValue.Equals(strGBK2))
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
                                string strBudgetType = ds.Tables[0].Rows[0]["payment_detail_budget_type"].ToString();
                                string strpayment_detail_id = ds.Tables[0].Rows[0]["payment_detail_id"].ToString();
                                if (!oPayment.SP_PAYMENT_DETAIL_UPD(hdfpayment_doc.Value, pitem_code, "0", txtmebertype_credit.Value.ToString(), ppayment_item_tax,
                                                                                            ppayment_item_sos, pcomments_sub, strActive, strUpdatedBy, strBudgetType,strpayment_detail_id, ref strMessage))
                                {
                                    lblError.Text = strMessage;
                                }
                            }
                            else
                            {
                                if (!oPayment.SP_PAYMENT_DETAIL_INS(hdfpayment_doc.Value, pitem_code, "0", txtmebertype_credit.Value.ToString(), ppayment_item_tax,
                                                                                            ppayment_item_sos, pcomments_sub, strActive, strCreatedBy, ref strMessage))
                                {
                                    lblError.Text = strMessage;
                                }
                            }
                        }
                    }
                    if (!txtrate2.Text.Equals("0.00"))
                    {
                        DataSet ds2 = new DataSet();
                        string strCheckDup2 = string.Empty;
                        strCheckDup2 = " and payment_doc = '" + hdfpayment_doc.Value + "' " +
                                                      " and item_code = '" + lbldebitcompany_code.Text + "' ";
                        if (!oPayment.SP_PAYMENT_ACC_SEL(strCheckDup2, ref ds2, ref strMessage))
                        {
                            lblError.Text = strMessage;
                        }
                        else
                        {
                            if (ds2.Tables[0].Rows.Count > 0)
                            {
                                if (!oPayment.SP_PAYMENT_ACC_UPD(hdfpayment_doc.Value, lbldebitcompany_code.Text, txtcompany_credit.Value.ToString(), strActive, strUpdatedBy, ref strMessage))
                                {
                                    lblError.Text = strMessage;
                                }
                            }
                            else
                            {
                                if (!oPayment.SP_PAYMENT_ACC_INS(hdfpayment_doc.Value, lbldebitcompany_code.Text, txtcompany_credit.Value.ToString(), strActive, strUpdatedBy, ref strMessage))
                                {
                                    lblError.Text = strMessage;
                                }
                            }
                        }
                    }

                    if (!txtrate3.Text.Equals("0.00"))
                    {

                        DataSet ds3 = new DataSet();
                        string strCheckDup3 = string.Empty;
                        strCheckDup3 = " and payment_doc = '" + hdfpayment_doc.Value + "' " +
                                                      " and item_code = '" + lbldebitextra_code.Text + "' ";
                        if (!oPayment.SP_PAYMENT_ACC_SEL(strCheckDup3, ref ds3, ref strMessage))
                        {
                            lblError.Text = strMessage;
                        }
                        else
                        {
                            if (ds3.Tables[0].Rows.Count > 0)
                            {
                                if (!oPayment.SP_PAYMENT_ACC_UPD(hdfpayment_doc.Value, lbldebitextra_code.Text, txtextra_credit.Value.ToString(), strActive, strUpdatedBy, ref strMessage))
                                {
                                    lblError.Text = strMessage;
                                }
                            }
                            else
                            {
                                if (!oPayment.SP_PAYMENT_ACC_INS(hdfpayment_doc.Value, lbldebitextra_code.Text, txtextra_credit.Value.ToString(), strActive, strUpdatedBy, ref strMessage))
                                {
                                    lblError.Text = strMessage;
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
            string strmember_type_code = cboMember_type.SelectedValue;
            string strpayment_year = cboYear.SelectedValue;
            string strpay_month = cboPay_Month.SelectedValue;
            string strpay_year = cboPay_Year.SelectedValue;
            string strGBK = string.Empty;
            string strGBK2 = string.Empty;
            strGBK = ((DataSet)Application["xmlconfig"]).Tables["MemberType"].Rows[0]["GBK"].ToString();
            strGBK2 = ((DataSet)Application["xmlconfig"]).Tables["MemberType"].Rows[0]["GBK2"].ToString();
            try
            {
                if (!strmember_type_code.Equals(strGBK2))
                {
                    strCriteria = "  and  member_type_code= '" + strmember_type_code + "' " +
                                           " and person_work_status_code='01' " +
                                           " and payment_year='" + strpayment_year + "' " +
                                           " and pay_month='" + strpay_month + "' " +
                                           " and pay_year='" + strpay_year + "' " +
                                           " and person_group_code IN (" + PersonGroupList + ") ";
                }
                else
                {
                    strmember_type_code = strGBK;
                    strCriteria = "  and  member_type_code= '" + strmember_type_code + "' " +
                                           " and person_work_status_code='01' " +
                                           " and payment_year='" + strpayment_year + "' " +
                                           " and member_type_add > 0 " +
                                           " and pay_month='" + strpay_month + "' " +
                                           " and pay_year='" + strpay_year + "' " +
                                           " and person_group_code IN (" + PersonGroupList + ") ";
                }
                if (!oPayment.SP_PAYMENT_MEMBER_TYPE_TEMP_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    ds.Tables[0].DefaultView.Sort = ViewState["sort"] + " " + ViewState["direction"];
                    ds.Tables[0].Columns.Add("membertype_credit");
                    ds.Tables[0].Columns.Add("company_credit");
                    ds.Tables[0].Columns.Add("extra_credit");
                    ds.Tables[0].Columns.Add("item_has");
                    for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        // GridViewRow row = GridView1.Rows[i] ;
                        // AwNumeric txtperson_salaly = (AwNumeric)row.FindControl("txtperson_salaly");
                        ds.Tables[0].Rows[i]["item_has"] = "N";
                        string strSOS = ((DataSet)Application["xmlconfig"]).Tables["MemberType"].Rows[0]["SOS"].ToString();
                        double dblperson_salaly = 0;
                        try
                        {
                            dblperson_salaly = double.Parse((ds.Tables[0].Rows[i]["person_salaly"].ToString()));
                        }
                        catch { }
                        double dblpayment_item_amt = 0;
                        try
                        {
                            dblpayment_item_amt = double.Parse((ds.Tables[0].Rows[i]["payment_item_amt"].ToString()));
                        }
                        catch { }
                        double dblsos_max = double.Parse(((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["SOS_MAX"].ToString());
                        double dblsos_min = double.Parse(((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["SOS_MIN"].ToString());
                        double dblmember_type_add = double.Parse(ds.Tables[0].Rows[i]["member_type_add"].ToString());
                        ds.Tables[0].Rows[i]["person_salaly"] = dblpayment_item_amt;
                        dblperson_salaly = dblpayment_item_amt;
                        //txtperson_salaly.Value = dblpayment_item_amt;
                        if (strmember_type_code.Equals(strSOS))
                        {
                            if (dblperson_salaly < dblsos_min)
                            {
                                dblperson_salaly = dblsos_min;
                            }

                            if (dblperson_salaly > dblsos_max)
                            {
                                dblperson_salaly = dblsos_max;
                            }
                        }

                        if (double.Parse(txtrate1.Value.ToString()) > 0.0)
                        {
                            if (strmember_type_code.Equals(strSOS))
                            {
                                ds.Tables[0].Rows[i]["membertype_credit"] = Math.Round(((double.Parse(txtrate1.Value.ToString())) * dblperson_salaly) / 100, 0, MidpointRounding.AwayFromZero);
                            }
                            else
                            {
                                ds.Tables[0].Rows[i]["membertype_credit"] = ((double.Parse(txtrate1.Value.ToString())) * dblperson_salaly) / 100;
                            }
                        }
                        else
                        {
                            ds.Tables[0].Rows[i]["membertype_credit"] = 0.00;
                        }
                        if (double.Parse(txtrate2.Value.ToString()) > 0.0)
                        {
                            if (strmember_type_code.Equals(strSOS))
                            {
                                ds.Tables[0].Rows[i]["company_credit"] = Math.Round((double.Parse(txtrate2.Value.ToString()) * dblperson_salaly) / 100, 0, MidpointRounding.AwayFromZero);
                            }
                            else
                            {
                                ds.Tables[0].Rows[i]["company_credit"] = (double.Parse(txtrate2.Value.ToString()) * dblperson_salaly) / 100;
                            }
                        }
                        else
                        {
                            ds.Tables[0].Rows[i]["company_credit"] = 0.00;
                        }
                        if (double.Parse(txtrate3.Value.ToString()) > 0.0)
                        {
                            ds.Tables[0].Rows[i]["extra_credit"] = (double.Parse(txtrate3.Value.ToString()) * dblperson_salaly) / 100;
                        }
                        else
                        {
                            ds.Tables[0].Rows[i]["extra_credit"] = 0.00;
                        }


                        if (cboMember_type.SelectedValue.Equals(strGBK2))
                        {
                            ds.Tables[0].Rows[i]["membertype_credit"] = ((dblmember_type_add) * dblperson_salaly) / 100;
                        }

                    }
                    ds.Tables[0].AcceptChanges();
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();

                    if (double.Parse(txtrate1.Value.ToString()) > 0.0)
                    {
                        GridView1.Columns[GridView1.Columns.Count - 3].Visible = true;
                    }
                    else
                    {
                        GridView1.Columns[GridView1.Columns.Count - 3].Visible = false;
                    }

                    if (double.Parse(txtrate2.Value.ToString()) > 0.0)
                    {
                        GridView1.Columns[GridView1.Columns.Count - 2].Visible = true;
                    }
                    else
                    {
                        GridView1.Columns[GridView1.Columns.Count - 2].Visible = false;
                    }

                    if (double.Parse(txtrate3.Value.ToString()) > 0.0)
                    {
                        GridView1.Columns[GridView1.Columns.Count - 1].Visible = true;
                    }
                    else
                    {
                        GridView1.Columns[GridView1.Columns.Count - 1].Visible = false;
                    }

                    if (cboMember_type.SelectedValue.Equals(strGBK2))
                    {
                        GridView1.Columns[GridView1.Columns.Count - 3].Visible = true;
                    }

                    cboYear.Enabled = false;
                    cboPay_Month.Enabled = false;
                    cboPay_Year.Enabled = false;
                    cboMember_type.Enabled = false;
                    txtrate1.ReadOnly = true;
                    txtrate2.ReadOnly = true;
                    txtrate3.ReadOnly = true;
                    RadioButtonList1.Enabled = false;

                    if (GridView1.Rows.Count > 0)
                    {
                        imgSaveOnly.Visible = true;
                        imgFind.Visible = false;
                        imgCancel.Visible = true;
                    }
                    else
                    {
                        imgSaveOnly.Visible = false;
                        imgFind.Visible = true;
                        imgCancel.Visible = true;
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

        private void BindGridViewTmp()
        {
            cPayment oPayment = new cPayment();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            try
            {
                strCriteria = "  and  1=2 ";

                if (!oPayment.SP_PAYMENT_MEMBER_TYPE_TEMP_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }

                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();

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
                for (int iCol = 0; iCol < e.Row.Cells.Count; iCol++)
                {
                    e.Row.Cells[iCol].Attributes.Add("class", "table_h");
                    e.Row.Cells[iCol].Wrap = false;
                    ViewState["sumperson_salaly"] = "0.00";
                    ViewState["summebertype_credit"] = "0.00";
                    ViewState["sumcompany_credit"] = "0.00";
                    ViewState["sumextra_credit"] = "0.00";
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
                AwNumeric txtperson_salaly = (AwNumeric)e.Row.FindControl("txtperson_salaly");
                AwNumeric txtmebertype_credit = (AwNumeric)e.Row.FindControl("txtmebertype_credit");
                AwNumeric txtcompany_credit = (AwNumeric)e.Row.FindControl("txtcompany_credit");
                AwNumeric txtextra_credit = (AwNumeric)e.Row.FindControl("txtextra_credit");
                ViewState["sumperson_salaly"] = String.Format("{0:#,##0.00}", decimal.Parse(ViewState["sumperson_salaly"].ToString()) + decimal.Parse(txtperson_salaly.Text));
                ViewState["summebertype_credit"] = String.Format("{0:#,##0.00}", decimal.Parse(ViewState["summebertype_credit"].ToString()) + decimal.Parse(txtmebertype_credit.Text));
                ViewState["sumcompany_credit"] = String.Format("{0:#,##0.00}", decimal.Parse(ViewState["sumcompany_credit"].ToString()) + decimal.Parse(txtcompany_credit.Text));
                ViewState["sumextra_credit"] = String.Format("{0:#,##0.00}", decimal.Parse(ViewState["sumextra_credit"].ToString()) + decimal.Parse(txtextra_credit.Text));
            }
            else if (e.Row.RowType.Equals(DataControlRowType.Footer))
            {
                AwNumeric txtsumperson_salaly = (AwNumeric)e.Row.FindControl("txtsumperson_salaly");
                AwNumeric txtsummebertype_credit = (AwNumeric)e.Row.FindControl("txtsummebertype_credit");
                AwNumeric txtsumcompany_credit = (AwNumeric)e.Row.FindControl("txtsumcompany_credit");
                AwNumeric txtsumextra_credit = (AwNumeric)e.Row.FindControl("txtsumextra_credit");
                txtsumperson_salaly.Value = ViewState["sumperson_salaly"].ToString();
                txtsummebertype_credit.Value = ViewState["summebertype_credit"].ToString();
                txtsumcompany_credit.Value = ViewState["sumcompany_credit"].ToString();
                txtsumextra_credit.Value = ViewState["sumextra_credit"].ToString();
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
            BindGridView();
        }

        protected void cboMember_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            InittxtItem_Code();
        }

        protected void imgSaveOnly_Click(object sender, ImageClickEventArgs e)
        {
            if (saveData())
            {
                InitcboYear();
                InitcboPay_Month();
                InitcboPay_Year();
                InitcboMember_type();
                lblcredititem_code.Text = string.Empty;
                lblcredititem_name.Text = string.Empty;
                //cboYear.Enabled = true;
                //cboPay_Month.Enabled = true;
                //cboPay_Year.Enabled = true;
                cboMember_type.Enabled = true;
                cboMember_type.SelectedIndex = 0;
                txtrate1.Enabled = true;
                RadioButtonList1.Enabled = true;
                RadioButtonList1.SelectedValue = "A";

                imgSaveOnly.Visible = false;
                imgFind.Visible = true;
                imgCancel.Visible = true;

                lblRate1.Visible = false;
                lblItemCode.Visible = false;
                txtrate1.Visible = false;
                lblcredititem_code.Visible = false;
                lblcredititem_name.Visible = false;
                lblcredit_mid.Visible = false;

                lblRate2.Visible = false;
                lblItemCode2.Visible = false;
                txtrate2.Visible = false;
                lbldebitcompany_code.Visible = false;
                lbldebitcompany_name.Visible = false;
                lblcompany_mid.Visible = false;

                lblRate3.Visible = false;
                lblItemCode3.Visible = false;
                txtrate3.Visible = false;
                lbldebitextra_code.Visible = false;
                lbldebitextra_name.Visible = false;
                lblextra_mid.Visible = false;
                BindGridView();

                //cboYear.Enabled = true;
                //cboPay_Month.Enabled = true;
                //cboPay_Year.Enabled = true;
                cboMember_type.Enabled = true;
                txtrate1.ReadOnly = false;
                txtrate1.Value = 0.00;
                txtrate2.ReadOnly = false;
                txtrate2.Value = 0.00;
                txtrate3.ReadOnly = false;
                txtrate3.Value = 0.00;
                RadioButtonList1.Enabled = true;
                RadioButtonList1.SelectedValue = "A";

                imgSaveOnly.Visible = false;
                imgFind.Visible = true;
                imgCancel.Visible = true;

                MsgBox("บันทึกข้อมูลสมบูรณ์");

            }
        }

        protected void imgCancel_Click(object sender, ImageClickEventArgs e)
        {
            InitcboYear();
            InitcboPay_Month();
            InitcboPay_Year();
            InitcboMember_type();
            lblcredititem_code.Text = string.Empty;
            lblcredititem_name.Text = string.Empty;
            //cboYear.Enabled = true;
            //cboPay_Month.Enabled = true;
            //cboPay_Year.Enabled = true;
            cboMember_type.Enabled = true;
            cboMember_type.SelectedIndex = 0;
            txtrate1.Enabled = true;
            RadioButtonList1.Enabled = true;
            RadioButtonList1.SelectedValue = "A";

            imgSaveOnly.Visible = false;
            imgFind.Visible = true;
            imgCancel.Visible = true;

            lblRate1.Visible = false;
            lblItemCode.Visible = false;
            txtrate1.Visible = false;
            lblcredititem_code.Visible = false;
            lblcredititem_name.Visible = false;
            lblcredit_mid.Visible = false;

            lblRate2.Visible = false;
            lblItemCode2.Visible = false;
            txtrate2.Visible = false;
            lbldebitcompany_code.Visible = false;
            lbldebitcompany_name.Visible = false;
            lblcompany_mid.Visible = false;

            lblRate3.Visible = false;
            lblItemCode3.Visible = false;
            txtrate3.Visible = false;
            lbldebitextra_code.Visible = false;
            lbldebitextra_name.Visible = false;
            lblextra_mid.Visible = false;
            BindGridViewTmp();

            //cboYear.Enabled = true;
            //cboPay_Month.Enabled = true;
            //cboPay_Year.Enabled = true;
            cboMember_type.Enabled = true;
            txtrate1.ReadOnly = false;
            txtrate1.Value = 0.00;
            txtrate2.ReadOnly = false;
            txtrate2.Value = 0.00;
            txtrate3.ReadOnly = false;
            txtrate3.Value = 0.00;
            RadioButtonList1.Enabled = true;
            RadioButtonList1.SelectedValue = "A";

            imgSaveOnly.Visible = false;
            imgFind.Visible = true;
            imgCancel.Visible = true;
        }


    }
}