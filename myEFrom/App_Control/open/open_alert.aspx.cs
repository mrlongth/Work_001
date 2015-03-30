
using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using myDLL;


namespace myEFrom.App_Control.open
{
    public partial class open_alert : PageBase
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                lblperson_name.Text = base.PersonFullName;
                LinkButton1.Text = GetApproveCount().ToString(CultureInfo.InvariantCulture);
            }
        }

        private int GetApproveCount()
        {
            string strCriteria = string.Empty;
            string strCriteriaLoan = string.Empty;
            cCommon oCommon = new cCommon();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataTable dtLoan = new DataTable();
            #region Criteria

            strCriteria = "Select count(1) as item_count from view_ef_open_detail_approve ";
            strCriteria += "  Where (person_code = '" + base.PersonCode + "') ";
            strCriteria += "  And   (approve_status = 'P') ";
            strCriteria += "  And  (approve_head_status not in ('C','W')) ";
            strCriteria += "  And  (select count(1) from ef_open_detail_approve " +
                           "  Where ef_open_detail_approve.open_head_id= view_ef_open_detail_approve.open_head_id " +
                           "  And view_ef_open_detail_approve.approve_level > ef_open_detail_approve.approve_level " +
                           "  And ef_open_detail_approve.approve_status <> 'A') = 0 ";

            strCriteriaLoan = "Select count(1) as item_count from view_ef_loan_detail_approve ";
            strCriteriaLoan += "  Where (person_code = '" + base.PersonCode + "') ";
            strCriteriaLoan += "  And   (approve_status = 'P') ";
            strCriteriaLoan += "  And  (loan_status not in ('C','W')) ";
            strCriteriaLoan += "  And  (select count(1) from ef_loan_detail_approve " +
                                    "  Where ef_loan_detail_approve.loan_id= view_ef_loan_detail_approve.loan_id " +
                                    "  And view_ef_loan_detail_approve.approve_level > ef_loan_detail_approve.approve_level " +
                                    "  And ef_loan_detail_approve.approve_status <> 'A') = 0 ";

            #endregion
            try
            {
                if (oCommon.SEL_SQL(strCriteria, ref ds, ref _strMessage))
                {
                    dt = ds.Tables[0];
                }
                if (oCommon.SEL_SQL(strCriteriaLoan, ref ds, ref _strMessage))
                {
                    dtLoan = ds.Tables[0];
                }
                return Helper.CInt(dt.Rows[0]["item_count"]) + Helper.CInt(dtLoan.Rows[0]["item_count"]);
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            return 0;
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            var strScript = "window.parent.__doPostBack('ctl00$ASPxRoundPanel1$ContentPlaceHolder2$lbkGetOpen','');ClosePopUp('1')";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "close", strScript, true);
        }

    }
}