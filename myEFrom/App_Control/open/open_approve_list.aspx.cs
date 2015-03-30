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
using myDLL;
using System.Collections.Generic;
using Aware.WebControls;

namespace myEFrom.App_Control.open
{
    public partial class open_approve_list : PageBase
    {

        #region private data
        private string strRecordPerPage;
        private string strPageNo = "1";
        private bool[] blnAccessRight = new bool[5] { false, false, false, false, false };
        private cefOpen objEfOpen = new cefOpen();

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
            //Thread.Sleep(2000);
            //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th-TH");
            //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("th-TH");

            if (!IsPostBack)
            {

                imgFind.Attributes.Add("onMouseOver", "src='../../images/button/Search2.png'");
                imgFind.Attributes.Add("onMouseOut", "src='../../images/button/Search.png'");

                imgList_open.Attributes.Add("onclick", "OpenPopUp('800px','400px','93%','ค้นหาข้อมูลรายการขออนุมัติเบิกจ่าย' ,'../lov/open_lov.aspx?" +
                                                    "budget_type='+document.forms[0]." + cboBudget_type.UniqueID + ".options[document.forms[0]." + cboBudget_type.UniqueID + ".selectedIndex].value+" +
                                                    "'&year='+document.forms[0]." + cboYear.UniqueID + ".options[document.forms[0]." + cboYear.UniqueID + ".selectedIndex].value+" +
                                                    "'&item_code='+document.forms[0]." + txtopen_code.UniqueID + ".value+" +
                                                    "'&item_name='+document.forms[0]." + txtopen_title.UniqueID + ".value+" +
                                                    "'&ctrl1=" + txtopen_code.ClientID + "&ctrl2=" + txtopen_title.ClientID + "', '1');return false;");

                imgClear_open.Attributes.Add("onclick", "document.forms[0]." + txtopen_code.UniqueID + ".value='';" +
                                        "document.forms[0]." + txtopen_title.UniqueID + ".value='';return false;");


                lblperson_name.Text = base.PersonFullName;
                txtfrom_date.Text = "";
                txtto_date.Text = "";

                ViewState["sort"] = "doc_type_no";
                ViewState["direction"] = "ASC";
                InitcboYear();
                InitcboBudgetType();
                //cboBudget_type.SelectedValue = this.BudgetType;
                InitcboDirector();
                InitcboBudget();
                BindGridView(0);

            }
            else
            {
                if (Request.Form["ctl00$ASPxRoundPanel1$ContentPlaceHolder2$GridView1$ctl01$cboPerPage"] != null)
                {
                    strRecordPerPage = Request.Form["ctl00$ASPxRoundPanel1$ContentPlaceHolder2$GridView1$ctl01$cboPerPage"].ToString();
                    strPageNo = Request.Form["ctl00$ASPxRoundPanel1$ContentPlaceHolder2$GridView1$ctl01$txtPage"].ToString();
                }
                if (txthpage.Value != string.Empty)
                {
                    BindGridView(int.Parse(txthpage.Value));
                    txthpage.Value = string.Empty;
                }
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            PermissionURL = "";
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "RegisterScript", "createDate('" + txtfrom_date.ClientID + "','" + DateTime.Now.Date.ToString("dd/MM/yyyy") + "');createDate('" + txtto_date.ClientID + "','" + DateTime.Now.Date.ToString("dd/MM/yyyy") + "','" + DateTime.Now.Date.ToString("dd/MM/yyyy") + "');", true);

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

        private void InitcboBudget()
        {
            cBudget oBudget = new cBudget();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strYear = cboYear.SelectedValue;
            string strbudget_code = cboBudget.SelectedValue;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and budget_year = '" + strYear + "'  and  c_active='Y' ";
            strCriteria = strCriteria + "  And budget_type ='" + this.BudgetType + "' ";
            if (oBudget.SP_SEL_BUDGET(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboBudget.Items.Clear();
                cboBudget.Items.Add(new ListItem("--- เลือกทั้งหมด ---", ""));
                int i;
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboBudget.Items.Add(new ListItem(dt.Rows[i]["budget_name"].ToString(), dt.Rows[i]["budget_code"].ToString()));
                }
                if (cboBudget.Items.FindByValue(strbudget_code) != null)
                {
                    cboBudget.SelectedIndex = -1;
                    cboBudget.Items.FindByValue(strbudget_code).Selected = true;
                }
            }
            InitcboProduce();
        }

        private void InitcboProduce()
        {
            cProduce oProduce = new cProduce();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strbudget_code = string.Empty,
                        strproduce_code = string.Empty,
                        strproduce_name = string.Empty;
            string strYear = cboYear.SelectedValue;
            strbudget_code = cboBudget.SelectedValue;
            strproduce_code = cboProduce.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and  produce.c_active='Y' ";
            strCriteria = strCriteria + "  And produce.budget_type ='" + this.BudgetType + "' ";

            //if (!strbudget_code.Equals(""))
            //{
            strCriteria = strCriteria + " and produce.budget_code= '" + strbudget_code + "' ";
            //}

            if (oProduce.SP_SEL_PRODUCE(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboProduce.Items.Clear();
                cboProduce.Items.Add(new ListItem("--- เลือกทั้งหมด ---", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboProduce.Items.Add(new ListItem(dt.Rows[i]["produce_name"].ToString(), dt.Rows[i]["produce_code"].ToString()));
                }
                if (cboProduce.Items.FindByValue(strproduce_code) != null)
                {
                    cboProduce.SelectedIndex = -1;
                    cboProduce.Items.FindByValue(strproduce_code).Selected = true;
                }
            }
            InitcboActivity();
        }

        private void InitcboActivity()
        {
            cActivity oActivity = new cActivity();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        stractivity_code = string.Empty,
                        strbudget_code = string.Empty,
                        strproduce_code = string.Empty;
            stractivity_code = cboActivity.SelectedValue;
            strbudget_code = cboBudget.SelectedValue;
            strproduce_code = cboProduce.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = "  and  activity.c_active='Y' ";
            strCriteria = strCriteria + "  And activity.budget_type ='" + this.BudgetType + "' ";

            //if (!strbudget_code.Equals(""))
            //{
            strCriteria = strCriteria + " and  produce.budget_code= '" + strbudget_code + "' ";
            //}

            //if (!strproduce_code.Equals(""))
            //{
            strCriteria = strCriteria + " and activity.produce_code= '" + strproduce_code + "' ";
            //}


            if (oActivity.SP_SEL_ACTIVITY(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboActivity.Items.Clear();
                cboActivity.Items.Add(new ListItem("--- เลือกทั้งหมด ---", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboActivity.Items.Add(new ListItem(dt.Rows[i]["activity_name"].ToString(), dt.Rows[i]["activity_code"].ToString()));
                }
                if (cboActivity.Items.FindByValue(stractivity_code) != null)
                {
                    cboActivity.SelectedIndex = -1;
                    cboActivity.Items.FindByValue(stractivity_code).Selected = true;
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

        private void cboPerPage_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            GridView1.PageSize = int.Parse(strRecordPerPage);
            BindGridView(0);
        }

        private void imgGo_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            BindGridView(int.Parse(strPageNo) - 1);
        }

        protected void imgFind_Click(object sender, ImageClickEventArgs e)
        {
            BindGridView(0);
        }

        private void BindGridView(int nPageNo)
        {
            //InitcboYear();
            cefOpen objefOpen = new cefOpen();
            cefLoan objefLoan = new cefLoan();
            DataTable dtMain = new DataTable();
            string strCriteria = string.Empty;
            string strCriteriaLoan = string.Empty;
            string strCriteriaAll = string.Empty;
            string stropen_doc = string.Empty;
            string strbudget_plan_year = string.Empty;
            string stropen_code = string.Empty;
            string stropen_title = string.Empty;
            string strbudget_code = string.Empty;
            string strproduce_code = string.Empty;
            string strunit_code = string.Empty;
            string strdirector_code = string.Empty;
            string stractivity_code = string.Empty;
            string strwork_code = string.Empty;
            string strbudget_type = string.Empty;
            string strapprove_status = string.Empty;
            string strbegin_date = string.Empty;
            string strend_date = string.Empty;
            string strScript = string.Empty;

            #region Criteria
            stropen_doc = txtopen_doc.Text;
            strbudget_plan_year = cboYear.SelectedValue;
            strbudget_code = cboBudget.SelectedValue;
            strproduce_code = cboProduce.SelectedValue;
            strdirector_code = cboDirector.SelectedValue;
            strunit_code = cboUnit.SelectedValue;
            stractivity_code = cboActivity.SelectedValue;
            stropen_code = txtopen_code.Text.Trim();
            stropen_title = txtopen_title.Text.Trim();
            strbudget_type = cboBudget_type.SelectedValue;
            strapprove_status = cboApproveStatus.SelectedValue;
            strbegin_date = txtfrom_date.Text.Length > 0 ? cCommon.SeekDate(txtfrom_date.Text) : "";
            strend_date = txtto_date.Text.Length > 0 ? cCommon.SeekDate(txtto_date.Text) : "";
            if (!strbudget_type.Equals(""))
            {
                strCriteria += "  And  (budget_type = '" + strbudget_type + "') ";
            }

            if (!stropen_doc.Equals(""))
            {
                strCriteria += "  And  (open_doc ='" + stropen_doc + "') ";
            }

            if (!strproduce_code.Equals(""))
            {
                strCriteria += "  And  (produce_code ='" + strproduce_code + "') ";
            }

            if (!strbudget_code.Equals(""))
            {
                strCriteria += "  And  (budget_code ='" + strbudget_code + "') ";
            }

            if (!strdirector_code.Equals(""))
            {
                strCriteria += "  And  (director_code ='" + strdirector_code + "') ";
            }

            if (!strunit_code.Equals(""))
            {
                strCriteria += "  And  (unit_code ='" + strunit_code + "') ";
            }
            if (!stractivity_code.Equals(""))
            {
                strCriteria += "  And  (activity_code = '" + stractivity_code + "') ";
            }
            if (!strwork_code.Equals(""))
            {
                strCriteria += "  And  (work_code like '%" + strwork_code + "%') ";
            }
            strCriteriaLoan = strCriteria;
            if (!strbudget_plan_year.Equals(""))
            {
                strCriteria += "  And  (open_year = '" + strbudget_plan_year + "') ";
                strCriteriaLoan += "  And  (loan_year = '" + strbudget_plan_year + "') ";
            }
            if (!stropen_code.Equals(""))
            {
                strCriteria += "  And  (open_code like '%" + stropen_code + "%') ";
            }
            if (!stropen_title.Equals(""))
            {
                strCriteria += "  And  (open_title like '%" + stropen_title + "%') ";
                strCriteriaLoan += "  And  (loan_reason like '%" + stropen_title + "%') ";
            }

            if (!strapprove_status.Equals(""))
            {
                strCriteria += "  And  (approve_status = '" + strapprove_status + "') ";
                strCriteriaLoan += "  And  (approve_status =  '" + strapprove_status + "') ";
            }

            if (!strbegin_date.Equals(""))
            {
                strCriteria += "  And  (open_date >= '" + strbegin_date + "') ";
                strCriteriaLoan += "  And  (loan_date >=  '" + strbegin_date + "') ";
            }

            if (!strend_date.Equals(""))
            {
                strCriteria += "  And  (open_date <= '" + strend_date + "') ";
                strCriteriaLoan += "  And  (loan_date <=  '" + strend_date + "') ";
            }
            
            strCriteria += "  And  (approve_head_status not in ('C','W')) ";
            strCriteriaLoan += "  And  (loan_status not in ('C','W')) ";

            strCriteria += "  And  (person_code in ('" + base.PersonCode + "','" + base.ApproveFor + "')) ";
            strCriteriaLoan += "  And  (person_code in ('" + base.PersonCode + "','" + base.ApproveFor + "')) ";

            strCriteria += "  And  (select count(1) from ef_open_detail_approve " +
                           "  Where ef_open_detail_approve.open_head_id= view_ef_open_detail_approve_count.open_head_id " +
                           "  And view_ef_open_detail_approve_count.approve_level > ef_open_detail_approve.approve_level " +
                           "  And ef_open_detail_approve.approve_status <> 'A') = 0 ";
            strCriteria += " Order by open_doc";

            strCriteriaLoan += "  And  (select count(1) from ef_loan_detail_approve " +
                                    "  Where ef_loan_detail_approve.loan_id= view_ef_loan_detail_approve.loan_id " +
                                    "  And view_ef_loan_detail_approve.approve_level > ef_loan_detail_approve.approve_level " +
                                    "  And ef_loan_detail_approve.approve_status <> 'A') = 0 ";
            strCriteriaLoan += " Order by loan_doc";


            #endregion

            //strCriteria = strCriteria + " and budget_type ='" + this.BudgetType + "' ";           

            try
            {
                DataTable dtTemp;
                DataTable dtOpenLoan;
                DataTable dtLoop;
                DataRow drNew;
                if (cboSearchType.SelectedValue == "O")
                {
                    dtMain = objefOpen.SP_OPEN_DETAIL_APPROVE_COUNT_SEL(strCriteria);
                    dtMain.Columns.Add("doc_type");
                    dtMain.Columns.Add("doc_type_no");
                    foreach (DataRow dr in dtMain.Rows)
                    {
                        dr["doc_type"] = "O";
                        dr["doc_type_no"] = dr["open_doc"].ToString();
                    }

                    if (chkShowOnly.Checked == false)
                    {
                        dtLoop = dtMain.Copy();
                        foreach (DataRow dr in dtLoop.Rows)
                        {
                            dr["doc_type"] = "O";
                            dr["doc_type_no"] = dr["open_doc"].ToString();
                            _strCriteria = " and open_head_id = " + Helper.CInt(dr["open_head_id"]);
                            dtOpenLoan = objefOpen.SP_OPEN_LOAN_SEL(_strCriteria);
                            _strCriteria = "";
                            foreach (DataRow drOpenLoan in dtOpenLoan.Rows)
                            {
                                if (!strCriteriaAll.Contains(drOpenLoan["loan_doc"].ToString()))
                                    _strCriteria += "'" + drOpenLoan["loan_doc"] + "',";
                            }
                            if (_strCriteria.Length > 0)
                            {
                                _strCriteria = _strCriteria.Substring(0, _strCriteria.Length - 1);
                                // _strCriteria = " and loan_doc in (" + _strCriteria + ") and person_code ='" + base.PersonCode + "'";
                                _strCriteria = " and person_code in ('" + base.PersonCode + "','" + base.ApproveFor + "') ";
                                _strCriteria += "  And  (select count(1) from ef_loan_detail_approve " +
                                   "  Where ef_loan_detail_approve.loan_id= view_ef_loan_detail_approve.loan_id " +
                                   "  And view_ef_loan_detail_approve.approve_level > ef_loan_detail_approve.approve_level " +
                                   "  And ef_loan_detail_approve.approve_status <> 'A') = 0 ";
                                if (!strapprove_status.Equals(""))
                                {
                                    _strCriteria += "  And  (approve_status = '" + strapprove_status + "') ";
                                }

                                dtTemp = objefLoan.SP_LOAN_DETAIL_APPROVE_SEL(_strCriteria);
                                foreach (DataRow drTemp in dtTemp.Rows)
                                {
                                    drNew = dtMain.NewRow();
                                    drNew["doc_type"] = "L";
                                    //drNew["doc_type_no"] = dr["open_doc"].ToString() + "_" + Helper.CStr(drTemp["loan_doc"]);
                                    drNew["doc_type_no"] = dr["open_doc"].ToString() + "_" + Helper.CStr(drTemp["loan_doc"]);
                                    drNew["open_detail_approve_id"] = Helper.CLong(drTemp["loan_detail_approve_id"]);
                                    drNew["open_head_id"] = Helper.CLong(drTemp["loan_id"]);
                                    drNew["open_doc"] = Helper.CStr(drTemp["loan_doc"]);
                                    drNew["open_date"] = cCommon.CheckDate(drTemp["loan_date"].ToString());

                                    drNew["d_process_date"] = cCommon.CheckDateTime(drTemp["d_process_date"].ToString());

                                    drNew["director_code"] = Helper.CStr(drTemp["director_code"]);
                                    drNew["director_name"] = Helper.CStr(drTemp["director_name"]);
                                    drNew["unit_name"] = Helper.CStr(drTemp["unit_name"]);
                                    drNew["open_title"] = Helper.CStr(drTemp["loan_reason"]);
                                    drNew["req_title_name"] = Helper.CStr(drTemp["req_title_name"]);
                                    drNew["req_person_thai_name"] = Helper.CStr(drTemp["req_person_thai_name"]);
                                    drNew["req_person_thai_name"] = Helper.CStr(drTemp["req_person_thai_name"]);
                                    drNew["approve_status"] = Helper.CStr(drTemp["approve_status"]);
                                    drNew["ef_doctype_code"] = Helper.CInt(drTemp["ef_doctype_code"]);
                                    drNew["ef_doctype_name"] = Helper.CStr(drTemp["ef_doctype_name"]);
                                    drNew["ef_doctype_pic"] = Helper.CStr(drTemp["ef_doctype_pic"]);
                                    drNew["loan_count"] = 0;
                                    dtMain.Rows.Add(drNew);
                                    strCriteriaAll += Helper.CStr(drTemp["loan_doc"]) + ",";
                                }
                            }
                        }
                    }
                }
                else
                {
                    dtMain = objefOpen.SP_OPEN_DETAIL_APPROVE_SEL(" and 1=2");
                    var dtLoan = objefLoan.SP_LOAN_DETAIL_APPROVE_SEL(strCriteriaLoan);
                    dtMain.Columns.Add("doc_type");
                    dtMain.Columns.Add("doc_type_no");
                    dtMain.Columns.Add("loan_count");
                    foreach (DataRow dr in dtLoan.Rows)
                    {
                        drNew = dtMain.NewRow();
                        drNew["doc_type"] = "L";
                        drNew["doc_type_no"] = dr["loan_doc"].ToString();
                        drNew["open_detail_approve_id"] = Helper.CLong(dr["loan_detail_approve_id"]);
                        drNew["open_head_id"] = Helper.CLong(dr["loan_id"]);
                        drNew["open_doc"] = Helper.CStr(dr["loan_doc"]);
                        drNew["open_date"] = cCommon.CheckDate(dr["loan_date"].ToString());

                        drNew["d_process_date"] = cCommon.CheckDateTime(dr["d_process_date"].ToString());
                        
                        drNew["director_code"] = Helper.CStr(dr["director_code"]);
                        drNew["director_name"] = Helper.CStr(dr["director_name"]);
                        drNew["unit_name"] = Helper.CStr(dr["unit_name"]);
                        drNew["open_title"] = Helper.CStr(dr["loan_reason"]);
                        drNew["req_title_name"] = Helper.CStr(dr["req_title_name"]);
                        drNew["req_person_thai_name"] = Helper.CStr(dr["req_person_thai_name"]);
                        drNew["req_person_thai_name"] = Helper.CStr(dr["req_person_thai_name"]);
                        drNew["approve_status"] = Helper.CStr(dr["approve_status"]);
                        drNew["ef_doctype_code"] = Helper.CInt(dr["ef_doctype_code"]);
                        drNew["ef_doctype_name"] = Helper.CStr(dr["ef_doctype_name"]);
                        drNew["ef_doctype_pic"] = Helper.CStr(dr["ef_doctype_pic"]);
                        drNew["loan_count"] = 0;
                        dtMain.Rows.Add(drNew);
                    }

                    if (chkShowOnly.Checked == false)
                    {
                        dtLoop = dtMain.Copy();
                        foreach (DataRow dr in dtLoop.Rows)
                        {
                            _strCriteria = " and loan_id = " + Helper.CLong(dr["open_head_id"]);
                            dtOpenLoan = objefOpen.SP_OPEN_LOAN_SEL(_strCriteria);
                            _strCriteria = "";
                            foreach (DataRow drOpenLoan in dtOpenLoan.Rows)
                            {
                                if (!strCriteriaAll.Contains(drOpenLoan["open_doc"].ToString()))
                                    _strCriteria += "'" + drOpenLoan["open_doc"] + "',";
                            }
                            if (_strCriteria.Length > 0)
                            {
                                _strCriteria = _strCriteria.Substring(0, _strCriteria.Length - 1);
                                _strCriteria = " and open_doc in (" + _strCriteria + ") and person_code in ('" + base.PersonCode + "','" + base.ApproveFor + "') ";
                                _strCriteria += "  And  (select count(1) from ef_open_detail_approve " +
                                                "  Where ef_open_detail_approve.open_head_id= view_ef_open_detail_approve.open_head_id " +
                                                "  And view_ef_open_detail_approve.approve_level > ef_open_detail_approve.approve_level " +
                                                "  And ef_open_detail_approve.approve_status <> 'A') = 0 ";
                                if (!strapprove_status.Equals(""))
                                {
                                    _strCriteria += "  And  (approve_status = '" + strapprove_status + "') ";
                                }
                                dtTemp = objefOpen.SP_OPEN_DETAIL_APPROVE_SEL(_strCriteria);
                                foreach (DataRow drTemp in dtTemp.Rows)
                                {
                                    drNew = dtMain.NewRow();
                                    drNew["doc_type"] = "O";
                                    drNew["doc_type_no"] = dr["open_doc"].ToString() + "_" +
                                                           Helper.CStr(drTemp["open_doc"]);
                                    drNew["open_detail_approve_id"] = Helper.CLong(drTemp["open_detail_approve_id"]);
                                    drNew["open_head_id"] = Helper.CInt(drTemp["open_head_id"]);
                                    drNew["open_doc"] = Helper.CStr(drTemp["open_doc"]);
                                    drNew["open_date"] = cCommon.CheckDate(drTemp["open_date"].ToString());

                                    drNew["d_process_date"] = cCommon.CheckDateTime(drTemp["d_process_date"].ToString());
                        
                                    drNew["director_code"] = Helper.CStr(drTemp["director_code"]);
                                    drNew["director_name"] = Helper.CStr(drTemp["director_name"]);
                                    drNew["unit_name"] = Helper.CStr(drTemp["unit_name"]);
                                    drNew["open_title"] = Helper.CStr(drTemp["open_title"]);
                                    drNew["req_title_name"] = Helper.CStr(drTemp["req_title_name"]);
                                    drNew["req_person_thai_name"] = Helper.CStr(drTemp["req_person_thai_name"]);
                                    drNew["req_person_thai_name"] = Helper.CStr(drTemp["req_person_thai_name"]);
                                    drNew["approve_status"] = Helper.CStr(drTemp["approve_status"]);
                                    drNew["ef_doctype_code"] = Helper.CInt(drTemp["ef_doctype_code"]);
                                    drNew["ef_doctype_name"] = Helper.CStr(drTemp["ef_doctype_name"]);
                                    drNew["ef_doctype_pic"] = Helper.CStr(drTemp["ef_doctype_pic"]);
                                    dtMain.Rows.Add(drNew);
                                    strCriteriaAll += Helper.CStr(drTemp["open_doc"]) + ",";
                                }
                            }
                        }
                    }
                }


                GridView1.PageIndex = nPageNo;
                txthTotalRecord.Value = dtMain.Rows.Count.ToString();
                dtMain.DefaultView.Sort = ViewState["sort"] + " " + ViewState["direction"];
                GridView1.DataSource = dtMain;
                GridView1.DataBind();


            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                objefOpen.Dispose();
                if (GridView1.Rows.Count > 0)
                {
                    GridView1.TopPagerRow.Visible = true;
                }
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
                Label lblopen_doc = (Label)e.Row.FindControl("lblopen_doc");
                int nNo = (GridView1.PageSize * GridView1.PageIndex) + e.Row.RowIndex + 1;
                lblNo.Text = nNo.ToString();
                DataRowView dv = (DataRowView)e.Row.DataItem;
                HiddenField hhdopen_head_id = (HiddenField)e.Row.FindControl("hhdopen_head_id");
                Label lblApprove_status = (Label)e.Row.FindControl("lblApprove_status");
                Label lbldoc_type_no = (Label)e.Row.FindControl("lbldoc_type_no");

                #region set Image Edit & Delete

                var imgEdit = (ImageButton)e.Row.FindControl("imgEdit");

                if (Helper.CStr(dv["doc_type"]) == "O")
                {
                    imgEdit.Attributes.Add("onclick", "OpenPopUp('800px','300px','90%','ทำการอนุมัติรายการขออนุมัติเบิก','open_approve_control.aspx?mode=edit&type=open&open_head_id="
                                                                + hhdopen_head_id.Value + "&open_detail_approve_id=" + dv["open_detail_approve_id"].ToString() + "&page=" + GridView1.PageIndex.ToString() + "&canEdit=Y','1');return false;");
                }
                else
                {
                    imgEdit.Attributes.Add("onclick", "OpenPopUp('800px','300px','90%','ทำการอนุมัติสัญญายืมเงิน','open_approve_control.aspx?mode=edit&type=loan&open_head_id="
                                                                + hhdopen_head_id.Value + "&open_detail_approve_id=" + dv["open_detail_approve_id"].ToString() + "&page=" + GridView1.PageIndex.ToString() + "&canEdit=Y','1');return false;");
                }

                imgEdit.ImageUrl = "../../images/controls/approve.png";
                imgEdit.Attributes.Add("title", "คลิกเพื่อทำรายการอนุมัติ");

                var imgPrint = (ImageButton)e.Row.FindControl("imgPrint");
                var imgView = (ImageButton)e.Row.FindControl("imgView");
                if (Helper.CStr(dv["doc_type"]) == "O")
                {
                    imgView.Attributes.Add("onclick", "OpenPopUp('950px','550px','95%','แสดงรายละเอียดการขออนุมัติเบิก','open_control.aspx?mode=view&open_head_id="
                        + hhdopen_head_id.Value + "&page=" + GridView1.PageIndex + "&canEdit=N','1');return false;");
                   
                    string strScript = "window.open(\"../../App_Control/reportsparameter/open_report_show.aspx?report_code=Rep_open01&open_head_id=" + hhdopen_head_id.Value + "\", \"_blank\");return false;\n";
                    imgPrint.Attributes.Add("onclick", strScript);

                }
                else
                {
                    imgView.Attributes.Add("onclick", "OpenPopUp('950px','550px','95%','แสดงรายละเอียดสัญญายืมเงิน','../loan/loan_control.aspx?mode=view&loan_id="
                                            + hhdopen_head_id.Value + "&page=" + GridView1.PageIndex + "&canEdit=N','1');return false;");
                    imgPrint.Attributes.Add("onclick", "OpenPopUp('550px','280px','92%','เลือกรายงานที่ต้องการพิมพ์','../loan/loan_print.aspx?loan_id=" + hhdopen_head_id.Value + "','1');return false;");

                }
                imgView.Visible = base.IsUserView;

                #endregion

                if (Helper.CStr(dv["approve_status"]) == "P")
                {
                    lblApprove_status.Text = "รออนุมัติ";
                    lblApprove_status.BackColor = System.Drawing.Color.DarkBlue;
                    lblApprove_status.ForeColor = System.Drawing.Color.White;
                    imgEdit.Visible = true;
                }
                else if (Helper.CStr(dv["approve_status"]) == "A")
                {
                    lblApprove_status.Text = "อนุมัติ";
                    lblApprove_status.BackColor = System.Drawing.Color.Green;
                    lblApprove_status.ForeColor = System.Drawing.Color.White;
                    imgEdit.Visible = false;
                }
                else if (Helper.CStr(dv["approve_status"]) == "N")
                {
                    lblApprove_status.Text = "ไม่อนุมัติ";
                    lblApprove_status.BackColor = System.Drawing.Color.Red;
                    lblApprove_status.ForeColor = System.Drawing.Color.White;
                    imgEdit.Visible = false;
                }
                if (Helper.CStr(dv["doc_type"]) == "O")
                {
                    lbldoc_type_no.Text = cboSearchType.SelectedValue == "O" ? "ขออนุมัติเบิก" : "=>ขออนุมัติเบิก";
                }
                else if (Helper.CStr(dv["doc_type"]) == "L")
                {
                    lbldoc_type_no.Text = cboSearchType.SelectedValue == "O" ? "=>สัญญายืมเงิน" : "สัญญายืมเงิน";
                }

                imgEdit.Visible = imgEdit.Visible ? base.IsUserApprove : imgEdit.Visible;

                Image imgLoan = (Image)e.Row.FindControl("imgLoan");
                imgLoan.Visible = Helper.CInt(dv["loan_count"]) > 0;


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
            else if (e.Row.RowType.Equals(DataControlRowType.Pager))
            {
                TableCell tbc = e.Row.Cells[0];
                Label lblPrev = null;
                Label lblNext = null;
                ImageButton lbtnPrev = null;
                ImageButton lbtnNext = null;

                #region find and store Previous and Next Page
                TableRow tbr = (TableRow)tbc.Controls[0].Controls[0];
                foreach (System.Web.UI.Control c in tbr.Controls)
                {
                    if (c.GetType().ToString().Equals("System.Web.UI.WebControls.Label"))
                    {
                        Label lbl = (Label)c;
                        if (lbl.Text.IndexOf("P") != -1)
                        {
                            lblPrev = lbl;
                            lblPrev.Text = string.Empty;
                        }
                        if (lbl.Text.IndexOf("N") != -1)
                        {
                            lblNext = lbl;
                            lblNext.Text = string.Empty;
                        }
                    }
                    if (c.Controls[0].GetType().ToString().Equals("System.Web.UI.WebControls.DataControlImageButton"))
                    {
                        ImageButton lbtn = (ImageButton)c.Controls[0];
                        if (lbtn.AlternateText.IndexOf("P") != -1)
                        {
                            lbtnPrev = lbtn;
                            lbtnPrev.ImageUrl = "~/images/prev.gif";
                        }
                        if (lbtn.AlternateText.IndexOf("N") != -1)
                        {
                            lbtnNext = lbtn;
                            lbtnNext.ImageUrl = "~/images/next.gif";
                        }
                    }
                }
                #endregion

                #region render new pager
                tbc.Text = string.Empty;
                Literal lblPager = new Literal();
                lblPager.Text = "<TABLE border='0' width='100%' cellpadding='0' cellspacing='0'><TR><TD width='30%' valign='middle'>";
                tbc.Controls.Add(lblPager);

                Label lblTotalRecord = new Label();
                lblTotalRecord.Attributes.Add("class", "label_h");
                lblTotalRecord.Text = "พบข้อมูล " + txthTotalRecord.Value.ToString() + " รายการ.";
                tbc.Controls.Add(lblTotalRecord);

                lblPager = new Literal();
                lblPager.Text = "</TD><TD width='30%' align='center' valign='middle'>";
                tbc.Controls.Add(lblPager);

                DropDownList cboPerPage = new DropDownList();
                cboPerPage.ID = "cboPerPage";

                DataTable entries;
                if ((DataSet)Application["xmlconfig"] == null)
                    return;
                else
                    entries = ((DataSet)Application["xmlconfig"]).Tables["RecordPerPage"];

                for (int i = 0; i < entries.Rows.Count; i++)
                {
                    cboPerPage.Items.Add(new ListItem(entries.Rows[i][0].ToString(), entries.Rows[i][1].ToString()));
                }

                if (cboPerPage.Items.FindByValue(strRecordPerPage) != null)
                {
                    cboPerPage.Items.FindByValue(strRecordPerPage).Selected = true;
                }

                cboPerPage.AutoPostBack = true;
                cboPerPage.SelectedIndexChanged += new System.EventHandler(cboPerPage_SelectedIndexChanged);
                tbc.Controls.Add(cboPerPage);

                lblPager = new Literal();
                lblPager.Text = "&nbsp;&nbsp;&nbsp;<span class=\"label_h\">รายการ/หน้า</span></TD><TD width='40%' align='right' valign='middle'>";
                tbc.Controls.Add(lblPager);

                if (lblPrev != null)
                {
                    tbc.Controls.Add(lblPrev);
                }
                else if (lbtnPrev != null)
                {
                    tbc.Controls.Add(lbtnPrev);
                }

                lblPager = new Literal();
                lblPager.Text = "&nbsp;&nbsp;&nbsp;<span class=\"label_h\">หน้าที่: </span>";
                tbc.Controls.Add(lblPager);

                TextBox txtPage = new TextBox();
                txtPage.AutoPostBack = false;
                txtPage.ID = "txtPage";
                txtPage.Width = System.Web.UI.WebControls.Unit.Parse("30px");
                txtPage.Attributes.Add("class", "text1");
                txtPage.Style.Add("text-align", "right");
                int nCurrentPage = (GridView1.PageIndex + 1);
                txtPage.Text = nCurrentPage.ToString();//strPageNo;
                txtPage.Attributes.Add("onkeypress", "javascript: return checkKeyCode(event);");
                txtPage.Attributes.Add("onkeyup", "javasctipt: checkInt(this, " + GridView1.PageCount.ToString() + ");");
                tbc.Controls.Add(txtPage);

                lblPager = new Literal();
                lblPager.Text = "<span class=\"label_h\"> จากทั้งหมด " + GridView1.PageCount.ToString() + "&nbsp;&nbsp;</span>";
                tbc.Controls.Add(lblPager);

                lblPager = new Literal();
                lblPager.Text = "&nbsp;&nbsp;";
                tbc.Controls.Add(lblPager);

                ImageButton imgGo = new ImageButton();
                imgGo.ID = "imgGo";
                imgGo.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgGo"].Rows[0]["img"].ToString();
                imgGo.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgGo"].Rows[0]["title"].ToString());
                imgGo.Attributes.Add("onclick", "javascript: return checkPage(" + GridView1.PageCount.ToString() + ",'กรุณาระบุข้อมูลให้ถูกต้อง.|||ctl00$ASPxRoundPanel1$ContentPlaceHolder2$GridView1$ctl01$txtPage');");
                imgGo.Click += new System.Web.UI.ImageClickEventHandler(this.imgGo_Click);
                tbc.Controls.Add(imgGo);

                lblPager = new Literal();
                lblPager.Text = "&nbsp;&nbsp;&nbsp;";
                tbc.Controls.Add(lblPager);

                if (lblNext != null)
                {
                    tbc.Controls.Add(lblNext);
                }
                else if (lbtnNext != null)
                {
                    tbc.Controls.Add(lbtnNext);
                }

                lblPager = new Literal();
                lblPager.Text = "</TD></TR></TABLE>";
                tbc.Controls.Add(lblPager);

                #endregion
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindGridView(e.NewPageIndex);
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
                GridViewRow item = (GridViewRow)GridView1.Controls[0].Controls[0];
                TextBox txtPage = (TextBox)item.FindControl("txtPage");
                BindGridView(int.Parse(txtPage.Text) - 1);
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string strMessage = string.Empty;
            string strCheck = string.Empty;
            string strScript = string.Empty;
            string strUpdatedBy = Session["username"].ToString();
            HiddenField hhdopen_head_id = (HiddenField)GridView1.Rows[e.RowIndex].FindControl("hhdopen_head_id");
            cefOpen objefOpen = new cefOpen();
            try
            {
                if (objefOpen.SP_OPEN_DETAIL_DEL(Helper.CInt(hhdopen_head_id.Value)) &&
                    objefOpen.SP_OPEN_DETAIL_APPROVE_ALL_DEL(Helper.CInt(hhdopen_head_id.Value)) &&
                     objefOpen.SP_OPEN_HEAD_DEL(Helper.CInt(hhdopen_head_id.Value)))
                {
                    BindGridView(0);
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                objefOpen.Dispose();
            }
        }

        protected void cboDirector_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboUnit();
            BindGridView(1);
        }

        protected void cboBudget_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboProduce();
            BindGridView(1);
        }

        protected void cboProduce_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboActivity();
            BindGridView(1);

        }

        protected void cboActivity_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridView(1);
        }

        protected void cboPlan_code_SelectedIndexChanged(object sender, EventArgs e)
        {
            // InitcboPlan();
            BindGridView(1);
        }

        protected void cboUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridView(1);
            //  InitcboUnit();
        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboDirector();
        }

        protected void cboBudget_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BudgetType = cboBudget_type.SelectedValue;
            InitcboDirector();
            InitcboBudget();
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            DataRowView dv = (DataRowView)e.Item.DataItem;
            Label lblApprove_status = (Label)e.Item.FindControl("lblApprove_status");
            if (Helper.CStr(dv["approve_status"]) == "P")
            {
                lblApprove_status.Text = "รออนุมัติ";
                lblApprove_status.BackColor = System.Drawing.Color.DarkBlue;
                lblApprove_status.ForeColor = System.Drawing.Color.White;
            }
            else if (Helper.CStr(dv["approve_status"]) == "A")
            {
                lblApprove_status.Text = "อนุมัติ";
                lblApprove_status.BackColor = System.Drawing.Color.Green;
                lblApprove_status.ForeColor = System.Drawing.Color.White;
            }
            else if (Helper.CStr(dv["approve_status"]) == "N")
            {
                lblApprove_status.Text = "ไม่อนุมัติ";
                lblApprove_status.BackColor = System.Drawing.Color.Red;
                lblApprove_status.ForeColor = System.Drawing.Color.White;
            }



        }

        protected void cboSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSearchType.SelectedValue == "O")
            {
                lblopen_doc.Text = "เลขที่การขออนุมัติ :";
                lblperson.Text = "ผู้ขออนุมัติ :";
                lblopen.Text = "รายการขออนุมัติ :";
                lblShowOnly.Text = "แสดงเฉพาะข้อมูลขออนุมัติ :";
                txtopen_code.Visible = true;
                txtopen_title.Visible = true;
                imgClear_open.Visible = true;
                imgList_open.Visible = true;
                chkShowOnly.Checked = true;
            }
            else
            {
                lblopen_doc.Text = "เลขที่การยืมเงิน :";
                lblperson.Text = "ผู้ขอยืม :";
                lblopen.Text = "รายละเอียดสัญญา :";
                lblShowOnly.Text = "แสดงเฉพาะข้อมูลยืมเงิน :";
                txtopen_code.Visible = false;
                txtopen_title.Visible = true;
                imgClear_open.Visible = false;
                imgList_open.Visible = false;
                chkShowOnly.Checked = true;
            }
            BindGridView(0);
        }
    }
}
