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

namespace myEFrom.App_Control.report_viewer
{
    public partial class loan_report : PageBase
    {
        #region private data
        private bool[] blnAccessRight = new bool[5] { false, false, false, false, false };
        private string strPrefixCtr = "ctl00$ASPxRoundPanel1$ASPxRoundPanel2$ContentPlaceHolder1$";

        private string BudgetType
        {
            get
            {
                if (ViewState["BudgetType"] == null)
                {
                    if (Request.QueryString["budget_type"] != null)
                    {
                        ViewState["BudgetType"] = Helper.CStr(Request.QueryString["budget_type"]);
                    }
                    else
                    {
                        ViewState["BudgetType"] = "B";
                    }
                }
                return ViewState["BudgetType"].ToString();
            }
            set
            {
                ViewState["BudgetType"] = value;
            }
        }


        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                txtfrom_date.Text = cCommon.CheckDate(DateTime.Now.AddMonths(-6).ToShortDateString());
                txtto_date.Text = cCommon.CheckDate(DateTime.Now.ToShortDateString());
                InitcboYear();
                InitcboBudgetType();
                InitcboDirector();
                
                imgList_person.Attributes.Add("onclick", "OpenPopUp('900px','500px','94%','ค้นหาข้อมูลบุคคลากร' ,'../lov/person_lov.aspx?" +
                           "from=loan_control&person_code='+getElementById('" + txtperson_code.ClientID + "').value+'" +
                           "&person_name='+getElementById('" + txtperson_name.ClientID + "').value+'" +
                          "&ctrl1=" + txtperson_code.ClientID + "&ctrl2=" + txtperson_name.ClientID + "&show=1', '1');return false;");

                imgClear_person.Attributes.Add("onclick", "document.getElementById('" + txtperson_code.ClientID + "').value='';document.getElementById('" + txtperson_name.ClientID + "').value=''; return false;");

            }

        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "RegisterScript", "createDate('" + txtfrom_date.ClientID + "','');createDate('" + txtto_date.ClientID + "','');", true);

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
                strCriteria += " and director_code = '" + DirectorCode + "' ";
            }
            if (this.BudgetType == "R")
            {
                strCriteria = strCriteria + " and budget_type <> 'B' ";
            }
            else
            {
                strCriteria = strCriteria + " and budget_type <> 'R' ";
            }
            if (oDirector.SP_SEL_DIRECTOR(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboDirector.Items.Clear();
                cboDirector.Items.Add(new ListItem("--- เลือกทั้งหมด ---", ""));
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
            strCriteria += " and unit.director_code = '" + strDirector_code + "' ";
            if (UnitLock == "Y")
            {
                strCriteria += " and substring(unit.unit_code,4,5) in (" + this.UnitCodeList + ") ";
            }

            if (this.BudgetType == "R")
            {
                strCriteria = strCriteria + " and unit.budget_type <> 'B' ";
            }
            else
            {
                strCriteria = strCriteria + " and unit.budget_type <> 'R' ";
            }
            if (oUnit.SP_SEL_UNIT(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboUnit.Items.Clear();
                cboUnit.Items.Add(new ListItem("--- เลือกทั้งหมด ---", ""));
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

        private void InitcboBudgetType()
        {
            cCommon oCommon = new cCommon();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strCode = cboBudget_type.SelectedValue;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " Select * from  general where g_type = 'budget_type' and g_code <> 'M' Order by g_sort ";
            if (oCommon.SEL_SQL(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboBudget_type.Items.Clear();
                int i;
                cboBudget_type.Items.Add(new ListItem("--- เลือกทั้งหมด ---", ""));
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
        }

        #endregion

        protected void imgPrint_Click(object sender, ImageClickEventArgs e)
        {
            string strCriteria = string.Empty;
            string strloan_doc = string.Empty;
            string strbudget_plan_year = string.Empty;
            string strloan_reason = string.Empty;
            string strbudget_code = string.Empty;
            string strunit_code = string.Empty;
            string strdirector_code = string.Empty;
            string strbudget_type = string.Empty;
            string strLoan_status = string.Empty;
            string strperson_code = string.Empty;
            string strperson_name = string.Empty;
            string strReport_code = string.Empty;
            string strScript = string.Empty;

            #region Criteria
            strloan_doc = txtloan_doc.Text;
            strbudget_plan_year = cboYear.SelectedValue;
            strdirector_code = cboDirector.SelectedValue;
            strunit_code = cboUnit.SelectedValue;
            strloan_reason = txtloan_reason.Text.Trim();
            strbudget_type = cboBudget_type.SelectedValue;
            strLoan_status = "A";
            strperson_name = txtperson_name.Text.Trim();
            strperson_code = txtperson_code.Text.Trim();
            var strbegin_date = txtfrom_date.Text.Length > 0 ? cCommon.SeekDate(txtfrom_date.Text) : "";
            var strend_date = txtto_date.Text.Length > 0 ? cCommon.SeekDate(txtto_date.Text) : "";

            if (!strbudget_plan_year.Equals(""))
            {
                strCriteria = strCriteria + "  And  (loan_year = '" + strbudget_plan_year + "') ";
            }
            if (!strbudget_type.Equals(""))
            {
                strCriteria = strCriteria + "  And  (budget_type = '" + strbudget_type + "') ";
            }

            if (!strloan_doc.Equals(""))
            {
                strCriteria = strCriteria + "  And  (loan_doc like '%" + strloan_doc + "%') ";
            }

            if (!strbudget_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (budget_code ='" + strbudget_code + "') ";
            }

            if (!strdirector_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (director_code ='" + strdirector_code + "') ";
            }

            if (!strunit_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (unit_code ='" + strunit_code + "') ";
            }

            if (!strloan_reason.Equals(""))
            {
                strCriteria = strCriteria + "  And  (loan_reason like '%" + strloan_reason + "%') ";
            }

            if (!strLoan_status.Equals(""))
            {
                strCriteria = strCriteria + "  And  (loan_status = '" + strLoan_status + "') ";
            }
            if (!strperson_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (person_code like '%" + strperson_code + "%') ";
            }
            if (!strperson_name.Equals(""))
            {
                strCriteria = strCriteria + "  And  (person_thai_name like '%" + strperson_name + "%'  " +
                                            "  OR person_thai_surname like '%" + strperson_name + "%'  " +
                                            "  OR '" + strperson_name + "' like ('%'+person_thai_name+'%'+person_thai_surname+'%')) ";
            }

            if (!strbegin_date.Equals(""))
            {
                strCriteria += "  And  (loan_date >= '" + strbegin_date + "') ";
            }

            if (!strend_date.Equals(""))
            {
                strCriteria += "  And  (loan_date <= '" + strend_date + "') ";
            }

            if (base.UserGroupCode == "User" || base.UserGroupCode == "Supervisor")
            {
                strCriteria += " and person_code ='" + base.PersonCode + "' ";
            }

            #endregion

            if (RadioButtonList1.SelectedValue.Equals("A01"))
            {
                strReport_code = "Rep_loan_record";
            }

            Session["criteria"] = strCriteria;
            strScript = "window.open(\"../reportsparameter/open_report_show.aspx?report_code=" + strReport_code + "\", \"_blank\");\n";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript, true);


            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript, true);

        }

        protected void cboDirector_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboUnit();
        }


        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboDirector();
        }

        protected void cboBudget_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BudgetType = cboBudget_type.SelectedValue;
            InitcboDirector();
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {



        }

    }
}
