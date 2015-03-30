using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Threading;
using System.Text;
using myDLL;

namespace myWeb.App_Control.payment_retire
{
    public partial class payment_retire_slip : PageBase
    {

        #region private data
        private bool[] blnAccessRight = new bool[5] { false, false, false, false, false };
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                InitcboRound();
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

        #endregion

        protected void imgPrint_Click(object sender, ImageClickEventArgs e)
        {
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string strActive = string.Empty;
            string strYear = string.Empty;
            string strPay_Month = string.Empty;
            string strPay_Year = string.Empty;
            string strPerson_name = string.Empty;
            string strReport_code = string.Empty;
            string strScript = string.Empty;
            strYear = cboYear.SelectedValue;
            strPay_Month = cboPay_Month.SelectedValue;
            strPay_Year = cboPay_Year.SelectedValue;
            strPerson_name = txtperson_name.Text.Trim();

            if (!strYear.Equals(""))
            {
                strCriteria = strCriteria + "  And  (pr_year = '" + strYear + "') ";
            }

            if (!strPay_Month.Equals(""))
            {
                strCriteria = strCriteria + "  And  (pay_month='" + strPay_Month + "') ";
            }

            if (!strPay_Year.Equals(""))
            {
                strCriteria = strCriteria + "  And  (pay_year='" + strPay_Year + "') ";
            }


            if (!strPerson_name.Equals(""))
            {
                strCriteria = strCriteria + "  And  (pr_person_name like '%" + strPerson_name + "%')";
            }

            Session["criteria"] = strCriteria;

            cPayment oPayment = new cPayment();
            DataSet ds = new DataSet();
            if (!oPayment.SP_PAYMENT_RETIRE_HEAD_SEL(strCriteria, ref ds, ref strMessage))
            {
                lblError.Text = strMessage;
            }
            else
            {

                if (ds.Tables[0].Rows.Count > 0)
                {
                    strReport_code = "Rep_payment_retire_slip";
                    strScript = "windowOpenMaximize(\"../../App_Control/reportsparameter/payment_retire_report_slip.aspx?report_code=" + strReport_code +
                                                     "&months=" + cboPay_Month.Text + "&year=" + cboPay_Year.Text + "\", \"_blank\");\n";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript, true);
                }
                else
                {
                    strScript = "alert('ไม่พบข้อมูล โปรดตรวจสอบเงื่อนไขในการแสดงผล');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript, true);
                    return;
                }

            }

        }

    }
}
