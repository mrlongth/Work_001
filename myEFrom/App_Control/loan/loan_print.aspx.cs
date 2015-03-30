using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.UI;
using myDLL;

namespace myEFrom.App_Control.loan
{
    public partial class loan_print : PageBase
    {

        protected void Page_LoadComplete(object sender, EventArgs e)
        {

        }

        protected void Page_Init(object sender, EventArgs e)
        {
            base.PermissionURL = "~/App_Control/loan/loan_list.aspx";
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            lblError.Text = "";
            if (!IsPostBack)
            {

                #region set QueryString

                if (Request.QueryString["loan_id"] != null)
                {
                    ViewState["loan_id"] = Request.QueryString["loan_id"].ToString();
                }

                #endregion


            }
        }

        protected void PrintData01()
        {
            string strCriteria = string.Empty;
            string strScript = string.Empty;
            strCriteria = "  And loan_id =" + ViewState["loan_id"];
            cefLoan objcEfLoan = new cefLoan();
            DataTable dt;
            dt = objcEfLoan.SP_LOAN_HEAD_SEL(strCriteria);
            if (dt.Rows.Count > 0)
            {
                Session["criteria"] = strCriteria;
                strScript = "window.open(\"../reportsparameter/open_report_show.aspx?report_code=Rep_loan01&loan_id=" + ViewState["loan_id"] + "&person_code=" + 
                    dt.Rows[0]["person_code"] + "\", \"_blank\");\n";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript, true);
            }
            else
            {
                strScript = "alert('ไม่พบข้อมูล โปรดตรวจสอบ');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript, true);
            }
        }

        protected void PrintData02()
        {
            string strCriteria = string.Empty;
            string strScript = string.Empty;
            strCriteria = "  And loan_id =" + ViewState["loan_id"];
            cefLoan objcEfLoan = new cefLoan();
            DataTable dt;
            dt = objcEfLoan.SP_LOAN_HEAD_SEL(strCriteria);
            if (dt.Rows.Count > 0)
            {
                Session["criteria"] = strCriteria;
                strScript = "window.open(\"../reportsparameter/open_report_show.aspx?report_code=Rep_loan02&loan_id=" + ViewState["loan_id"] + "&person_code=" +
                    dt.Rows[0]["person_code"] + "\", \"_blank\");\n";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript, true);
            }
            else
            {
                strScript = "alert('ไม่พบข้อมูล โปรดตรวจสอบ');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript, true);
            }
        }

        protected void btnLoan01_Click(object sender, ImageClickEventArgs e)
        {
            PrintData01();
        }

        protected void btnLoan2_Click(object sender, ImageClickEventArgs e)
        {
            PrintData02();
        }
    }
}