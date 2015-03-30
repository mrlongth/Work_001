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
    public partial class global_payment_back_report : GlobalPageBase
    {
        #region private data
        private bool[] blnAccessRight = new bool[5] { false, false, false, false, false };
        private string strPrefixCtr = "ctl00$ASPxRoundPanel1$ASPxRoundPanel2$ContentPlaceHolder1$";
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                InitcboRound();
                InitRadioList();
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

        private void InitRadioList()
        {
            cPayment_back oPayment_back = new cPayment_back();
            string strMessage = string.Empty;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            //string strCriteria = " and c_active='A' ";
            //strCriteria += " and  report_year = '" + cboYear.SelectedValue + "' ";
            //strCriteria += " and person_group_code IN (" + PersonGroupList + ",'') ";

            string strCriteria = " and ((report_name not like '%ใบประกอบ%') " +
                                 " and (report_name not like '%ใบปะหน้า%') " +
                                 " and (report_name not like '%ใบขออนุมัติ%') " +
                                 " and (report_name not like '%ใบขอนุมัติ%')) ";
            
            strCriteria += " and  report_year = '" + cboYear.SelectedValue + "' ";
            strCriteria += " and  substring(item_code,1,10) in (select substring(item_code,1,10) from view_payment where payment_year = '" + cboYear.SelectedValue + "' ";
            strCriteria += " and  pay_year = '" + cboPay_Year.SelectedValue + "' " + " and  pay_month = '" + cboPay_Month.SelectedValue + "' ";
            strCriteria += " and  person_code = '" + base.PersonCode + "' " + " and  item_code like '%A%') ";
            //strCriteria += " and  budget_type IN ('B','M') ";

            imgPrint.Visible = false;
            RadioButtonList1.Items.Clear();
            if (oPayment_back.SP_PAYMENT_BACK_REPORT_SEL(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        RadioButtonList1.Items.Add(new ListItem(dt.Rows[i]["report_name"].ToString(), dt.Rows[i]["report_id"].ToString()));
                    }
                    if (RadioButtonList1.Items.Count > 0)
                    {
                        RadioButtonList1.SelectedIndex = 0;
                    }
                    imgPrint.Visible = true;
                }
            }
        }

        #endregion

        protected void imgPrint_Click(object sender, ImageClickEventArgs e)
        {
            if (base.PersonBudgetType == "B")
            {
                Print_B();
            }
            else
            {
                Print_R();
            }
        }

        protected void Print_B()
        {
            string strCriteria = string.Empty;
            string strCriteria2 = string.Empty;
            string strYear = string.Empty;
            string stritem_code = string.Empty;
            string strScript = string.Empty;
            string strPay_Month = string.Empty;
            string strPay_Year = string.Empty;

            strYear = cboYear.SelectedValue;
            strPay_Month = cboPay_Month.SelectedValue;
            strPay_Year = cboPay_Year.SelectedValue;

            if (!strYear.Equals(""))
            {
                strCriteria = strCriteria + "  And  view_payment_back.payment_year = '" + strYear + "'  ";
            }

            if (!strPay_Month.Equals(""))
            {
                strCriteria = strCriteria + "  And  view_payment_back.pay_month='" + strPay_Month + "' ";
            }

            if (!strPay_Year.Equals(""))
            {
                strCriteria = strCriteria + "  And  view_payment_back.pay_year='" + strPay_Year + "' ";
            }

            if (RadioButtonList2.SelectedValue.Equals("0"))
            {
                strCriteria = strCriteria + "  And  view_payment_back.payment_back_type= 'N' ";
            }
            else
            {
                strCriteria = strCriteria + "  And  view_payment_back.payment_back_type= 'O' ";
            }
            strCriteria = strCriteria + "  And  view_payment_back.person_code= '" + base.PersonCode + "' ";


            string strReport_id = string.Empty;
            cPayment_back oPayment_back = new cPayment_back();
            string strMessage = string.Empty;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria2 = " and  report_id = " + RadioButtonList1.SelectedValue + " ";
            if (oPayment_back.SP_PAYMENT_BACK_REPORT_SEL(strCriteria2, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    stritem_code = dt.Rows[0]["item_code"].ToString();
                    strReport_id = dt.Rows[0]["report_id"].ToString();

                    if (!stritem_code.Equals(""))
                    {
                        strCriteria = strCriteria + "  And  view_payment_back.item_code= '" + stritem_code + "' ";
                    }
                    if (stritem_code.Substring(4, 6).Equals("03-002"))
                    {
                        strCriteria = strCriteria.Replace("view_payment_back.", "");
                    }
                    Session["criteria"] = strCriteria;
                    if (RadioButtonList2.SelectedValue.Equals("0"))
                    {
                        strScript = "windowOpenMaximize(\"../../App_Control/reportsparameter/payment_back_report_show.aspx?report_id=" + strReport_id + "&payment_back_type=N" +
                                                                     "&months=" + cboPay_Month.Text + "&year=" + cboPay_Year.Text + "\", \"_blank\");\n";
                    }
                    else
                    {
                        strScript = "windowOpenMaximize(\"../App_Control/reportsparameter/payment_back_report_show.aspx?report_id=" + strReport_id + "&payment_back_type=O" +
                                                                     "&months=" + cboPay_Month.Text + "&year=" + cboPay_Year.Text + "\", \"_blank\");\n";
                    }
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript, true);
                }
            }
        }

        protected void Print_R()
        {
            string strCriteria = string.Empty;
            string strCriteria2 = string.Empty;
            string strYear = string.Empty;
            string stritem_code = string.Empty;
            string strScript = string.Empty;
            string strPay_Month = string.Empty;
            string strPay_Year = string.Empty;

            strYear = cboYear.SelectedValue;
            strPay_Month = cboPay_Month.SelectedValue;
            strPay_Year = cboPay_Year.SelectedValue;


            if (!strYear.Equals(""))
            {
                strCriteria = strCriteria + "  And  view_payment_back.payment_year = '" + strYear + "'  ";
            }

            if (!strPay_Month.Equals(""))
            {
                strCriteria = strCriteria + "  And  view_payment_back.pay_month='" + strPay_Month + "' ";
            }

            if (!strPay_Year.Equals(""))
            {
                strCriteria = strCriteria + "  And  view_payment_back.pay_year='" + strPay_Year + "' ";
            }

            //strCriteria = strCriteria + "  And  view_payment_back.payment_detail_budget_type ='" + base.PersonBudgetType + "' ";

            if (RadioButtonList2.SelectedValue.Equals("0"))
            {
                strCriteria = strCriteria + "  And  view_payment_back.payment_back_type= 'N' ";
            }
            else
            {
                strCriteria = strCriteria + "  And  view_payment_back.payment_back_type= 'O' ";
            }


            strCriteria = strCriteria + "  And  view_payment_back.person_code= '" + base.PersonCode + "' ";


            string strReport_id = string.Empty;
            cPayment_back oPayment_back = new cPayment_back();
            string strMessage = string.Empty;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria2 = " and  report_id = " + RadioButtonList1.SelectedValue + " ";

            if (oPayment_back.SP_PAYMENT_BACK_REPORT_SEL(strCriteria2, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    stritem_code = dt.Rows[0]["item_code"].ToString();
                    strReport_id = dt.Rows[0]["report_id"].ToString();

                    if (!stritem_code.Equals(""))
                    {
                        strCriteria = strCriteria + "  And  view_payment_back.item_code= '" + stritem_code + "' ";
                    }
                    if (stritem_code.Substring(4, 6).Equals("03-002"))
                    {
                        strCriteria = strCriteria.Replace("view_payment_back.", "");
                    }
                    Session["criteria"] = strCriteria;
                    if (RadioButtonList2.SelectedValue.Equals("0"))
                    {
                        strScript = "windowOpenMaximize(\"../App_Control/reportsparameter/payment_back_report_income_show.aspx?report_id=" + strReport_id + "&payment_back_type=N" +
                                                                     "&months=" + cboPay_Month.Text + "&year=" + cboPay_Year.Text + "\", \"_blank\");\n";
                    }
                    else
                    {
                        strScript = "windowOpenMaximize(\"../App_Control/reportsparameter/payment_back_report_income_show.aspx?report_id=" + strReport_id + "&payment_back_type=O" +
                                                                     "&months=" + cboPay_Month.Text + "&year=" + cboPay_Year.Text + "\", \"_blank\");\n";
                    }
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript, true);

                }

            }
        }


        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitRadioList();
        }

        protected void cboPay_Month_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitRadioList();
        }

        protected void cboPay_Year_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }
}
