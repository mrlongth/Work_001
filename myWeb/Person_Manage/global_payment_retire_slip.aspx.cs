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

namespace myWeb.Person_Manage
{
    public partial class global_payment_retire_slip : GlobalPageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitcboPay_Year();
            }
        }

        #region private function

        private void InitcboPay_Month()
        {
            cCommon oCommon = new cCommon();
            DataSet ds = new DataSet();
            string strYear = string.Empty;
            strYear = cboPay_Year.SelectedValue;

            string strSQL = " SELECT pay_month FROM [payment_round] Where [round_status]='C' and pay_year = '" + strYear + "' Group by pay_month order by pay_month";
            oCommon.SEL_SQL(strSQL, ref ds, ref _strMessage);

            string strMonth = string.Empty;

            strMonth = ds.Tables[0].Rows.Count.ToString();
            if (int.Parse(strMonth) < 10)
            {
                strMonth = "0" + strMonth;
            }
            else
            {
                strMonth = ds.Tables[0].Rows.Count.ToString();
            }

            DataTable odt;
            int i;
            cboPay_Month.Items.Clear();
            odt = ((DataSet)Application["xmlconfig"]).Tables["cboMonth"];
            for (int j = 0; j <= ds.Tables[0].Rows.Count - 1; j++)
            {
                for (i = 0; i <= 11; i++)
                {
                    if (i + 1 == int.Parse(ds.Tables[0].Rows[j]["pay_month"].ToString()))
                    {
                        cboPay_Month.Items.Add(new ListItem(odt.Rows[i]["Text"].ToString(), odt.Rows[i]["Value"].ToString()));
                        break;
                    }
                }
            }
            if (cboPay_Month.Items.FindByValue(strMonth) != null)
            {
                cboPay_Month.SelectedIndex = -1;
                cboPay_Month.Items.FindByValue(strMonth).Selected = true;
            }
        }

        private void InitcboPay_Year()
        {
            cCommon oCommon = new cCommon();
            DataSet ds = new DataSet();
            string strSQL = " SELECT pay_year FROM payment_round where [round_status]='C' Group by pay_year order by pay_year";
            oCommon.SEL_SQL(strSQL, ref ds, ref _strMessage);
            string strYear = string.Empty;
            strYear = cboPay_Year.SelectedValue;
            if (strYear.Equals(""))
            {
                strYear = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["pay_year"].ToString();
            }
            DataTable odt;
            int i;
            cboPay_Year.Items.Clear();
            odt = ds.Tables[0];
            for (i = 0; i <= odt.Rows.Count - 1; i++)
            {
                cboPay_Year.Items.Add(new ListItem(odt.Rows[i]["pay_year"].ToString(), odt.Rows[i]["pay_year"].ToString()));
            }
            if (cboPay_Year.Items.FindByValue(strYear) != null)
            {
                cboPay_Year.SelectedIndex = -1;
                cboPay_Year.Items.FindByValue(strYear).Selected = true;
            }
            InitcboPay_Month();
        }

        #endregion

        protected void imgPrint_Click(object sender, ImageClickEventArgs e)
        {
            PrintData();
        }

        protected void PrintData()
        {
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string strActive = string.Empty;
            string strPay_Month = string.Empty;
            string strPay_Year = string.Empty;
            string strPerson_code = string.Empty;
            string strReport_code = string.Empty;
            string strScript = string.Empty;
            strPay_Month = cboPay_Month.SelectedValue;
            strPay_Year = cboPay_Year.SelectedValue;
            strPerson_code = base.PersonCode;

            if (!strPay_Month.Equals(""))
            {
                strCriteria = strCriteria + "  And  (pay_month='" + strPay_Month + "') ";
            }

            if (!strPay_Year.Equals(""))
            {
                strCriteria = strCriteria + "  And  (pay_year='" + strPay_Year + "') ";
            }

            if (!strPerson_code.Equals(""))
            {
                strCriteria = strCriteria + "  And pr_person_code='" + strPerson_code + "' ";
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
                    strScript = "windowOpenMaximize(\"global_payment_retire_report_slip.aspx?report_code=" + strReport_code +
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

        protected void cboPay_Year_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboPay_Month();
        }

    }
}
