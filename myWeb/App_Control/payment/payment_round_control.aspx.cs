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

namespace myWeb.App_Control.payment_round
{
    public partial class payment_round_control : PageBase
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            lblError.Text = "";
            if (!IsPostBack)
            {
                imgSaveOnly.Attributes.Add("onMouseOver", "src='../../images/controls/save2.jpg'");
                imgSaveOnly.Attributes.Add("onMouseOut", "src='../../images/controls/save.jpg'");

                Session["menupopup_name"] = "";
                ViewState["sort"] = "person_code";
                ViewState["direction"] = "ASC";

                #region set QueryString
                if (Request.QueryString["round_id"] != null)
                {
                    ViewState["round_id"] = Request.QueryString["round_id"].ToString();
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
                    InitcboPay_Month();
                    InitcboPay_Year();
                    //InitcboPerson_group();
                    ViewState["page"] = Request.QueryString["page"];
                    chkStatus.Checked = true;
                }
                #endregion
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
        //    strCriteria = " and c_active='Y' ";
        //    if (oPerson_group.SP_PERSON_GROUP_SEL(strCriteria, ref ds, ref strMessage))
        //    {
        //        dt = ds.Tables[0];
        //        cboPerson_group.Items.Clear();
        //        cboPerson_group.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
        //        for (i = 0; i <= dt.Rows.Count - 1; i++)
        //        {
        //            cboPerson_group.Items.Add(new ListItem(dt.Rows[i]["person_group_name"].ToString(), dt.Rows[i]["person_group_code"].ToString()));
        //        }
        //        if (cboPerson_group.Items.FindByValue(strperson_group_code) != null)
        //        {
        //            cboPerson_group.SelectedIndex = -1;
        //            cboPerson_group.Items.FindByValue(strperson_group_code).Selected = true;
        //        }
        //    }
        //}

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
            string strYear = string.Empty;
            string strPay_Month = string.Empty;
            string strPay_Year = string.Empty;
            string strComments = string.Empty;
            string strActive = string.Empty;
            string strCreatedBy = string.Empty;
            string strUpdatedBy = string.Empty;
            string strScript = string.Empty;
            // string strRound_id = string.Empty;

            string ppayment_date = string.Empty;
            string ppayment_year = string.Empty;
            string ppay_month = string.Empty;
            string ppay_year = string.Empty;
            string pperson_code = string.Empty;
            string pposition_code = string.Empty;
            string pperson_level = string.Empty;
            string pperson_group_code = string.Empty;
            string pperson_manage_code = string.Empty;
            string pbudget_plan_code = string.Empty;
            string pperson_work_status_code = string.Empty;
            string ppayment_recv = string.Empty;
            string ppayment_pay = string.Empty;
            string ppayment_net = string.Empty;
            string pcomments = string.Empty;
            string pc_status = string.Empty;
            string ptype_position_code = string.Empty;
            string strLastPayment_doc = string.Empty;


            cPayment_round oPayment_round = new cPayment_round();
            cPayment oPayment = new cPayment();
            cPerson oPerson = new cPerson();
            cPerson oPersonLoan = new cPerson();
            DataSet dsPersonLoan = new DataSet();
            DataSet ds = new DataSet();
            DataSet dsLoanMoney = new DataSet();
            try
            {

                #region set Data
                strYear = cboYear.SelectedValue;
                strPay_Month = cboPay_Month.SelectedValue;
                strPay_Year = cboPay_Year.SelectedValue;
                strComments = txtComments.Text;
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

                //string  str= "  and pay_year = '" + strPay_Year + "' " +
                //                                 " and  pay_month = '" + strPay_Month + "' ";
                //if (!oPayment_round.SP_PAYMENT_ROUND_SEL(strCheckDup, ref ds, ref strMessage))
                //{
                //    lblError.Text = strMessage;
                //}


                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    #region insert
                    string strCheckDup = string.Empty;
                    strCheckDup = "  and pay_year = '" + strPay_Year + "' " +
                                                  " and  pay_month = '" + strPay_Month + "' ";
                    if (!oPayment_round.SP_PAYMENT_ROUND_SEL(strCheckDup, ref ds, ref strMessage))
                    {
                        lblError.Text = strMessage;
                        return false;
                    }

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        strScript =
                            "alert(\"ไม่สามารถประมวลผลเงินเดือนได้ เนื่องจาก" +
                            "\\nข้อมูลปีงบประมาณ : " + cboYear.SelectedValue +
                            "\\nรอบเดือนที่จ่าย : " + cboPay_Month.SelectedItem.Text +
                            "\\nรอบปีที่จ่าย : " + cboPay_Year.SelectedValue +
                            "\\nประมวลผลไปแล้ว\");\n";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript, true);
                        return false;
                    }

                    if (!oPayment_round.SP_PAYMENT_ROUND_INS(strYear, strPay_Month, strPay_Year, "O", strComments, strActive, strCreatedBy, ref strMessage))
                    {
                        lblError.Text = strMessage;
                        return false;
                    }

                    string strwork_status = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["work_status"].ToString();
                    string strPerson = " And  person_work_status_code in  (" + strwork_status + ")  And   c_active = 'Y' " +
                                                        "And  person_group_code in (select person_group_code from person_group where c_active='Y')";
                    DataSet ds3 = new DataSet();
                    if (!oPerson.SP_PERSON_LIST_SEL(strPerson, ref ds3, ref strMessage))
                    {
                        lblError.Text = strMessage;
                        return false;
                    }

                    int i;
                    for (i = 0; i <= ds3.Tables[0].Rows.Count - 1; i++)
                    {
                        // Add Payment

                        #region set  value
                        ppayment_date = DateTime.Now.Date.ToString();
                        ppayment_year = strYear;
                        ppay_month = strPay_Month;
                        ppay_year = strPay_Year;
                        pperson_code = ds3.Tables[0].Rows[i]["person_code"].ToString();
                        pposition_code = ds3.Tables[0].Rows[i]["position_code"].ToString();
                        pperson_level = ds3.Tables[0].Rows[i]["person_level"].ToString();
                        pperson_group_code = ds3.Tables[0].Rows[i]["person_group_code"].ToString();
                        pperson_manage_code = ds3.Tables[0].Rows[i]["person_manage_code"].ToString();
                        pbudget_plan_code = ds3.Tables[0].Rows[i]["budget_plan_code"].ToString();
                        pperson_work_status_code = ds3.Tables[0].Rows[i]["person_work_status_code"].ToString();
                        ptype_position_code = ds3.Tables[0].Rows[i]["type_position_code"].ToString();
                        ppayment_recv = "0";
                        ppayment_pay = "0";
                        ppayment_net = "0";
                        pcomments = strComments;
                        pc_status = "O";
                        #endregion

                        if (!oPayment.SP_PAYMENT_HEAD_SEL(" and person_code='" + pperson_code + "' order by pay_year +'-'+ pay_month desc  ", ref dsPersonLoan, ref strMessage))
                        {
                            lblError.Text = strMessage + " Code : " + pperson_code;
                            return false;
                        }
                        else
                        { 
                            if (dsPersonLoan.Tables[0].Rows.Count > 0)
                            {
                                strLastPayment_doc = dsPersonLoan.Tables[0].Rows[0]["payment_doc"].ToString();
                            }                        
                        }

                        if (!oPayment.SP_PAYMENT_HEAD_INS(ppayment_date, ppayment_year, ppay_month, ppay_year, pperson_code, pposition_code, pperson_level,
                                    pperson_group_code, pperson_manage_code, pbudget_plan_code, pperson_work_status_code, ppayment_recv,
                                    ppayment_pay, ppayment_net, pcomments, pc_status, strActive, ptype_position_code, strCreatedBy, ref strMessage))
                        {
                            lblError.Text = strMessage + " Code : " + pperson_code;
                            return false;
                        }

                        DataSet dsPaymentDoc = new DataSet();
                        string strPaymentDoc = " and pay_year = '" + strPay_Year + "' " +
                                              " and  pay_month = '" + strPay_Month + "' " +
                                              " and  person_code='" + pperson_code + "'";
                        if (!oPayment.SP_PAYMENT_HEAD_SEL(strPaymentDoc, ref dsPaymentDoc, ref strMessage))
                        {
                            lblError.Text = strMessage + " Code : " + pperson_code;
                            return false;
                        }

                        if (dsPaymentDoc.Tables[0].Rows.Count > 0)
                        {
                            string ppayment_doc = dsPaymentDoc.Tables[0].Rows[0]["payment_doc"].ToString();
                            DataSet ds4 = new DataSet();
                            string strPerson_item = " And (person_code='" + pperson_code + "')  And  (person_item_year='" + strYear + "')  And  (c_active='Y') ";
                            if (!oPerson.SP_PERSON_ITEM_SEL(strPerson_item, ref ds4, ref strMessage))
                            {
                                lblError.Text = strMessage + " Code : " + pperson_code;
                                return false;
                            }

                            int intCount;
                            //decimal decDebit = 0;
                            //decimal decCredit = 0;
                            //decimal decRemain = 0;
                            for (intCount = 0; intCount <= ds4.Tables[0].Rows.Count - 1; intCount++)
                            {
                                string pitem_code = string.Empty;
                                string ppayment_item_recv = string.Empty;
                                string ppayment_item_pay = string.Empty;
                                string ppayment_item_tax = string.Empty;
                                string ppayment_item_sos = string.Empty;
                                string pcomments_sub = string.Empty;
                                #region set  value
                                pitem_code = ds4.Tables[0].Rows[intCount]["item_code"].ToString();
                                ppayment_item_recv = ds4.Tables[0].Rows[intCount]["item_debit"].ToString();
                                ppayment_item_pay = ds4.Tables[0].Rows[intCount]["item_credit"].ToString();
                                ppayment_item_tax = ds4.Tables[0].Rows[intCount]["person_item_tax"].ToString();
                                ppayment_item_sos = ds4.Tables[0].Rows[intCount]["person_item_sos"].ToString();
                                //decDebit = decDebit + decimal.Parse (ppayment_item_recv);
                                //decCredit = decCredit + decimal.Parse (ppayment_item_pay);
                                pcomments_sub = strComments;
                                #endregion
                                if (!oPayment.SP_PAYMENT_DETAIL_INS(ppayment_doc, pitem_code, ppayment_item_recv, ppayment_item_pay, ppayment_item_tax,
                                     ppayment_item_sos, pcomments_sub, strActive, strCreatedBy, ref strMessage))
                                {
                                    lblError.Text = strMessage + " Code : " + pperson_code;
                                    return false;
                                }
                            }

                            //SP_PAYMENT_LOAN
                            string strPerson_code = dsPaymentDoc.Tables[0].Rows[0]["person_code"].ToString();
                            string strPaymentLoan = " and  person_code='" + strPerson_code + "'";
                         
                            if (!oPersonLoan.SP_PERSON_LOAN_SEL(strPaymentLoan, ref dsPersonLoan, ref strMessage))
                            {
                                lblError.Text = strMessage + " Code : " + pperson_code;
                                return false;
                            }

                            int intCountL;
                            string strloan_code;
                            string strloan_acc;
                            string strloan_money = "0";

                            for (intCountL = 0; intCountL <= dsPersonLoan.Tables[0].Rows.Count - 1; intCountL++)
                            {

                                strloan_code = dsPersonLoan.Tables[0].Rows[intCountL]["loan_code"].ToString();
                                strloan_acc = dsPersonLoan.Tables[0].Rows[intCountL]["loan_acc"].ToString();

                                string strPaymentDetail = " and payment_doc = '" + strLastPayment_doc + "' " +
                                                            " and person_code = '" + strPerson_code + "' " +
                                                          " and loan_code = '" + strloan_code + "'" +
                                                          " and loan_acc = '" + strloan_acc + "'";

                                if (!oPayment.SP_PAYMENT_LOAN_SEL(strPaymentDetail, ref dsLoanMoney, ref strMessage))
                                {
                                    lblError.Text = strMessage + " Code : " + pperson_code;
                                    return false;
                                }
                                else
                                {
                                    if (dsLoanMoney.Tables[0].Rows.Count > 0)
                                    {
                                        strloan_money = dsLoanMoney.Tables[0].Rows[0]["loan_money"].ToString();
                                    }
                                }

                                if (!oPayment.SP_PAYMENT_LOAN_INS(ppayment_doc, strloan_code, strloan_acc, strloan_money, strCreatedBy, ref strMessage))
                                {
                                    lblError.Text = strMessage + " Code : " + pperson_code;
                                }

                            }

                            //----- End SP_PAYMENT_LOAN -------

                        }


                    }
                    blnResult = true;
                    #endregion
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString() + " Code : " + pperson_code;
            }
            finally
            {
                oPayment_round.Dispose();
                oPayment.Dispose();
                oPerson.Dispose();
            }
            return blnResult;
        }

        private void imgSaveOnly_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (saveData())
            {
                string strScript =
                       "alert(\"ประมวลผลเงินเดือน " +
                       "\\nปีงบประมาณ : " + cboYear.SelectedValue +
                       "\\nรอบเดือนที่จ่าย : " + cboPay_Month.SelectedItem.Text +
                       "\\nรอบปีที่จ่าย : " + cboPay_Year.SelectedValue +
                    //"\\nกลุ่มบุคคลากร : " + cboPerson_group.SelectedItem.Text +
                       "\\nเสร็จเรียบร้อย\");\n" +
                       "ClosePopUpListPost('" + ViewState["page"].ToString() + "','1');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript, true);
            }
        }

        private void setData()
        {
            cPayment_round oPayment_round = new cPayment_round();
            DataSet ds = new DataSet();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strYear = string.Empty;
            string strPay_Month = string.Empty;
            string strPay_Year = string.Empty;
            string strPerson_group_code = string.Empty;
            string strComments = string.Empty;
            string strActive = string.Empty;
            string strRound_status = string.Empty;
            string strCreatedBy = string.Empty;
            string strUpdatedBy = string.Empty;
            string strCreatedDate = string.Empty;
            string strUpdatedDate = string.Empty;
            try
            {
                strCriteria = " and round_id = '" + ViewState["round_id"].ToString() + "' ";
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
                        strPerson_group_code = ds.Tables[0].Rows[0]["person_group_code"].ToString();
                        strComments = ds.Tables[0].Rows[0]["comments"].ToString();
                        strActive = ds.Tables[0].Rows[0]["c_active"].ToString();
                        strRound_status = ds.Tables[0].Rows[0]["round_status"].ToString();
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

                        //InitcboPerson_group();
                        //if (cboPerson_group.Items.FindByValue(strPerson_group_code) != null)
                        //{
                        //    cboPerson_group.SelectedIndex = -1;
                        //    cboPerson_group.Items.FindByValue(strPerson_group_code).Selected = true;
                        //}

                        if (strActive.Equals("Y"))
                        {
                            chkStatus.Checked = true;
                        }
                        else
                        {
                            chkStatus.Checked = false;
                        }
                        txtComments.Text = strComments;
                        cboYear.Enabled = false;
                        cboYear.CssClass = "textboxdis";
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