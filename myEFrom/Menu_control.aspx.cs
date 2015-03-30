using System;
using System.Data;
using System.Web.UI;
using myDLL;

namespace myEFrom
{
    public partial class Menu_control : PageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["error"] != null && Session["error"].ToString() == "NotAccess")
            {
                MsgBox("คุณไม่มีสิทธิ์ในการใช้เมนู : " + Session["error_menu"]);
                Session["error"] = null;
                Session["error_menu"] = null;
            }

            if (!IsPostBack)
            {
                CheckPermissionButton();
                lbntApproveAlert.Attributes.Add("onclick", "OpenPopUp('600px','250px','93%','แจ้งเตือนรายการรออนุมัติ','App_Control/open/open_alert.aspx','1');return false;");
            }           
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (GetApproveCount() > 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "RegisterAlertRunScript", "$(document).ready(function () { setTimeout(function() { $('#" + lbntApproveAlert.ClientID + "').trigger('click'); }, 1000); });", true);
            }
        }


        private void CheckPermissionButton()
        {
            cefUser objEfUser = new cefUser();
            _strMessage = string.Empty;
            _strCriteria = " and UserGroupCode = '" + base.UserGroupCode + "' " +
                           " and [MenuNavigationUrl] in ('~/App_Control/open/open_list.aspx' , '~/App_Control/loan/loan_list.aspx' , '~/App_Control/open/open_approve_list.aspx') and CanView = 'Y' ";
            DataTable dtTemp = objEfUser.SP_USER_GROUP_MENU_SEL(_strCriteria);
            pnlApprove.Visible = false;
            pnlLoan.Visible = false;
            pnlOpen.Visible = false;
            foreach (DataRow dr in dtTemp.Rows)
            {
                if (dr["MenuNavigationUrl"].ToString() == "~/App_Control/open/open_list.aspx")
                    pnlOpen.Visible = true;
                else if (dr["MenuNavigationUrl"].ToString() == "~/App_Control/loan/loan_list.aspx")
                    pnlLoan.Visible = true;
                else if (dr["MenuNavigationUrl"].ToString() == "~/App_Control/open/open_approve_list.aspx")
                    pnlApprove.Visible = true;
            }
        }

        protected void btnApprove_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/App_Control/open/open_approve_list.aspx");
        }

        protected void btnLoan_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/App_Control/loan/loan_list.aspx");
        }

        protected void btnOpen_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/App_Control/open/open_list.aspx");
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
                var result = Helper.CInt(dt.Rows[0]["item_count"]) + Helper.CInt(dtLoan.Rows[0]["item_count"]);
                return result;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            return 0;
        }

        protected void lbkGetOpen_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App_Control/open/open_approve_list.aspx");            
        }





    }
}
