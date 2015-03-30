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
using System.IO;

namespace myEFrom.App_Control.loan
{
    public partial class loan_control : PageBase
    {
        private string BudgetType
        {
            get
            {
                ViewState["BudgetType"] = cboBudget_type.SelectedValue;
                return ViewState["BudgetType"].ToString();
            }
            set
            {
                ViewState["BudgetType"] = value;
            }
        }

        private bool bIsGridDetailEmpty
        {
            get
            {
                if (ViewState["bIsGridDetailEmpty"] == null)
                {
                    ViewState["bIsGridDetailEmpty"] = false;
                }
                return (bool)ViewState["bIsGridDetailEmpty"];
            }
            set
            {
                ViewState["bIsGridDetailEmpty"] = value;
            }
        }
        private long LoanDetailID
        {
            get
            {
                if (ViewState["LoanDetailID"] == null)
                {
                    ViewState["LoanDetailID"] = 1000000;
                }
                return long.Parse(ViewState["LoanDetailID"].ToString());
            }
            set
            {
                ViewState["LoanDetailID"] = value;
            }
        }
        private DataTable dtLoanDetail
        {
            get
            {
                if (ViewState["dtLoanDetail"] == null)
                {
                    cefLoan objEfloan = new cefLoan();
                    _strMessage = string.Empty;
                    _strCriteria = " and loan_id = " + Helper.CLong(ViewState["loan_id"]) + " order by loan_detail_id";
                    DataTable dtTemp = objEfloan.SP_LOAN_DETAIL_SEL(_strCriteria);
                    dtTemp.Columns.Add("row_status");
                    if (dtTemp.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtTemp.Rows)
                        {
                            dr["row_status"] = "O";
                        }
                    }
                    ViewState["dtLoanDetail"] = dtTemp;
                }
                return (DataTable)ViewState["dtLoanDetail"];
            }
            set
            {
                ViewState["dtLoanDetail"] = value;
            }
        }

        private bool bIsGridApproveEmpty
        {
            get
            {
                if (ViewState["bIsGridApproveEmpty"] == null)
                {
                    ViewState["bIsGridApproveEmpty"] = false;
                }
                return (bool)ViewState["bIsGridApproveEmpty"];
            }
            set
            {
                ViewState["bIsGridApproveEmpty"] = value;
            }
        }
        private long LoanApproveID
        {
            get
            {
                if (ViewState["LoanApproveID"] == null)
                {
                    ViewState["LoanApproveID"] = 1000000;
                }
                return long.Parse(ViewState["LoanApproveID"].ToString());
            }
            set
            {
                ViewState["LoanApproveID"] = value;
            }
        }
        private DataTable dtLoanApprove
        {
            get
            {
                if (ViewState["dtLoanApprove"] == null)
                {
                    cefLoan objEfloan = new cefLoan();
                    _strMessage = string.Empty;
                    _strCriteria = " and loan_id = " + Helper.CLong(ViewState["loan_id"]) +
                                   " order by approve_level";
                    DataTable dtTemp = objEfloan.SP_LOAN_DETAIL_APPROVE_SEL(_strCriteria);
                    dtTemp.Columns.Add("row_status");
                    if (dtTemp.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtTemp.Rows)
                        {
                            dr["row_status"] = "O";
                        }
                    }
                    else
                    {
                        DataRow rw;
                        cefApproveBudget objApproveBudget = new cefApproveBudget();
                        DataTable dtBudget = objApproveBudget.SP_APPROVE_BUDGET_SEL(" and ef_budget_type_approve in ('L','H') ");
                        foreach (DataRow drow in dtBudget.Rows)
                        {
                            rw = dtTemp.NewRow();
                            rw["loan_detail_approve_id"] = ++this.LoanApproveID;
                            rw["approve_code"] = Helper.CInt(drow["approve_code"]);
                            rw["approve_name"] = Helper.CStr(drow["approve_name"]);
                            rw["approve_level"] = Helper.CInt(drow["approve_level"]);
                            rw["approve_remark"] = "เจ้าหน้าที่ผู้ตรวจสอบ";
                            rw["person_code"] = Helper.CStr(drow["ef_person_code_approve"]);
                            rw["person_thai_name"] = Helper.CStr(drow["title_name"]) + Helper.CStr(drow["person_thai_name"]) + " " + Helper.CStr(drow["person_thai_surname"]);
                            rw["person_manage_code"] = Helper.CStr(drow["ef_approve_position"]);
                            rw["person_manage_name"] = Helper.CStr(drow["ef_approve_position_name"]);
                            rw["approve_status"] = "P";
                            rw["row_status"] = "N";
                            dtTemp.Rows.Add(rw);
                        }
                    }
                    ViewState["dtLoanApprove"] = dtTemp;
                }
                return (DataTable)ViewState["dtLoanApprove"];
            }
            set
            {
                ViewState["dtLoanApprove"] = value;
            }
        }
        private DataTable dtApprove
        {
            get
            {
                if (ViewState["dtApprove"] == null)
                {
                    cefApprove objEfApprove = new cefApprove();
                    DataTable dt;
                    _strCriteria = " order by approve_level";
                    dt = objEfApprove.SP_APPROVE_SEL(_strCriteria);
                    ViewState["dtApprove"] = dt;
                }
                return (DataTable)ViewState["dtApprove"];
            }
            set
            {
                ViewState["dtApprove"] = value;
            }
        }

        private bool bIsGridOpenEmpty
        {
            get
            {
                if (ViewState["bIsGridOpenEmpty"] == null)
                {
                    ViewState["bIsGridOpenEmpty"] = false;
                }
                return (bool)ViewState["bIsGridOpenEmpty"];
            }
            set
            {
                ViewState["bIsGridOpenEmpty"] = value;
            }
        }
        private long OpenLoanID
        {
            get
            {
                if (ViewState["OpenLoanID"] == null)
                {
                    ViewState["OpenLoanID"] = 1000000;
                }
                return long.Parse(ViewState["OpenLoanID"].ToString());
            }
            set
            {
                ViewState["OpenLoanID"] = value;
            }
        }
        private DataTable dtOpenLoan
        {
            get
            {
                if (ViewState["dtOpenLoan"] == null)
                {
                    var objEfOpen = new cefOpen();
                    _strMessage = string.Empty;
                    _strCriteria = " and loan_id = " + Helper.CLong(ViewState["loan_id"]);
                    var dtTemp = objEfOpen.SP_OPEN_LOAN_SEL(_strCriteria);
                    dtTemp.Columns.Add("row_status");
                    if (dtTemp.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtTemp.Rows)
                        {
                            dr["row_status"] = "O";
                        }
                    }
                    ViewState["dtOpenLoan"] = dtTemp;
                }
                return (DataTable)ViewState["dtOpenLoan"];
            }
            set
            {
                ViewState["dtOpenLoan"] = value;
            }
        }

        private bool bIsGridAttachEmpty
        {
            get
            {
                if (ViewState["bIsGridAttachEmpty"] == null)
                {
                    ViewState["bIsGridAttachEmpty"] = false;
                }
                return (bool)ViewState["bIsGridAttachEmpty"];
            }
            set
            {
                ViewState["bIsGridAttachEmpty"] = value;
            }
        }
        private long LoanAttachID
        {
            get
            {
                if (ViewState["LoanAttachID"] == null)
                {
                    ViewState["LoanAttachID"] = 1000000;
                }
                return long.Parse(ViewState["LoanAttachID"].ToString());
            }
            set
            {
                ViewState["LoanAttachID"] = value;
            }
        }
        private DataTable dtLoanAttach
        {
            get
            {
                if (ViewState["dtLoanAttach"] == null)
                {
                    var objEfloan = new cefLoan();
                    _strMessage = string.Empty;
                    _strCriteria = " and loan_id = " + Helper.CInt(ViewState["loan_id"]);
                    var dtTemp = objEfloan.SP_LOAN_ATTACH_SEL(_strCriteria);
                    dtTemp.Columns.Add("row_status");
                    if (dtTemp.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtTemp.Rows)
                        {
                            dr["row_status"] = "O";
                        }
                    }
                    ViewState["dtLoanAttach"] = dtTemp;
                }
                return (DataTable)ViewState["dtLoanAttach"];
            }
            set
            {
                ViewState["dtLoanAttach"] = value;
            }
        }


        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "RegisterScript", "RegisterScript();createDate('" +
                    txtloan_date.ClientID + "','" + DateTime.Now.Date.ToString("dd/MM/yyyy") + "');createDate('" +
                    txtloan_date_due.ClientID + "','" + DateTime.Now.Date.ToString("dd/MM/yyyy") + "');", true);
            }
            // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "load_total_all", "  load_total_all();", true);
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["mode"] != null && Request.QueryString["mode"].ToLower().Equals("view"))
            {
                base.PermissionURL = "~/App_Control/open/open_approve_list.aspx";
            }
            else
            {
                base.PermissionURL = "~/App_Control/loan/loan_list.aspx";
            }
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            lblError.Text = "";
            if (!IsPostBack)
            {
                imgSaveOnly.Attributes.Add("onMouseOver", "src='../../images/button/save_add2.png'");
                imgSaveOnly.Attributes.Add("onMouseOut", "src='../../images/button/save_add.png'");

                imgPrint.Attributes.Add("onMouseOver", "src='../../images/button/print2.png'");
                imgPrint.Attributes.Add("onMouseOut", "src='../../images/button/print.png'");


                ViewState["sort"] = "loan_detail_id";
                ViewState["direction"] = "ASC";

                ViewState["sort2"] = "approve_level";
                ViewState["direction2"] = "ASC";

                ViewState["sort3"] = "open_loan_id";
                ViewState["direction3"] = "ASC";

                ViewState["sort4"] = "loan_attach_id";
                ViewState["direction4"] = "ASC";

                //if (Request.Browser.Type.ToUpper().Contains("IE")) // replace with your check
                //{
                //    txtloan_reason.Rows = 2;
                //    txtloan_remark.Rows = 2;
                //}
                //else if (Request.Browser.Type.ToUpper().Contains("CHROME")) // replace with your check
                //{
                //    //txtloan_desc.Rows = 2;
                //}

                #region set QueryString

                if (Request.QueryString["loan_id"] != null)
                {
                    ViewState["loan_id"] = Request.QueryString["loan_id"].ToString();
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

                if (Request.QueryString["PageStatus"] != null)
                {
                    ViewState["PageStatus"] = Request.QueryString["PageStatus"].ToString();
                }

                if (Request.QueryString["open_head_id"] != null)
                {
                    ViewState["open_head_id"] = Request.QueryString["open_head_id"].ToString();
                }

                #endregion

                #region Set Image


                imgList_person.Attributes.Add("onclick", "OpenPopUp('900px','500px','94%','ค้นหาข้อมูลบุคคลากร' ,'../lov/person_lov.aspx?" +
                     "from=loan_control&person_code='+getElementById('" + txtloan_person.ClientID + "').value+'" +
                     "&person_name='+getElementById('" + txtloan_person_name.ClientID + "').value+'" +
                    "&ctrl1=" + txtloan_person.ClientID + "&ctrl2=" + txtloan_person_name.ClientID + "&show=2', '2');return false;");

                imgClear_person.Attributes.Add("onclick", "document.getElementById('" + txtloan_person.ClientID + "').value='';document.getElementById('" + txtloan_person_name.ClientID + "').value=''; return false;");

                string strYear = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["yearnow"].ToString();
                imgList_budget_plan.Attributes.Add("onclick", "OpenPopUp('950px','500px','94%','ค้นหาข้อมูลผังงบประมาณประจำปี' ,'../lov/budget_plan_lov.aspx?" +
                                                                "budget_plan_code='+getElementById('" + txtbudget_plan_code.ClientID + "').value+'" +
                                                                "&budget_name='+getElementById('" + cboBudget.ClientID + "').value+'" +
                                                                "&produce_name='+getElementById('" + cboProduce.ClientID + "').value+'" +
                                                                "&activity_name='+getElementById('" + cboActivity.ClientID + "').value+'" +
                                                                "&plan_name='+getElementById('" + cboPlan.ClientID + "').value+'" +
                                                                "&work_name='+getElementById('" + cboWork.ClientID + "').value+'" +
                                                                "&fund_name='+getElementById('" + cboFund.ClientID + "').value+'" +
                                                                "&director_name='+getElementById('" + cboDirector.ClientID + "').value+'" +
                                                                "&unit_name='+getElementById('" + cboUnit.ClientID + "').value+'" +
                                                                "&budget_type='+getElementById('" + cboBudget_type.ClientID + "').value+'" +
                                                                "&budget_plan_year='+getElementById('" + cboYear.ClientID + "').value+'" +
                                                                "&ctrl1=" + txtbudget_plan_code.ClientID +
                                                                "&ctrl2=" + hddBudget.ClientID +
                                                                "&ctrl3=" + hddProduce.ClientID +
                                                                "&ctrl4=" + hddActivity.ClientID +
                                                                "&ctrl5=" + hddPlan.ClientID +
                                                                "&ctrl6=" + hddWork.ClientID +
                                                                "&ctrl7=" + hddFund.ClientID +
                                                                "&ctrl9=" + hddDirector.ClientID +
                                                                "&ctrl10=" + hddUnit.ClientID +
                                                                "&ctrl11=" + hddBudget_type.ClientID +
                                                                "&show=2', '2');return false;");



                #endregion

                InitcboBudgetType();

                InitcboYear();

                InitcboLoan_offer();

                InitcboDoctype();

                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    ViewState["page"] = Request.QueryString["page"];
                    TabContainer1.Tabs[1].Visible = false;
                    TabContainer1.Tabs[2].Visible = false;
                    TabContainer1.Tabs[3].Visible = false;
                    TabContainer1.Tabs[4].Visible = false;
                    txtloan_date.Text = cCommon.CheckDate(DateTime.Now.ToShortDateString());
                    //txtloan_date_due.Text = cCommon.CheckDate(DateTime.Now.ToShortDateString());
                    txtloan_path.Text = this.DirectorName;
                    txtloan_person.Text = this.PersonCode;
                    txtloan_person_name.Text = this.PersonFullName;
                    txtposition_code.Text = this.PositionCode;
                    txtposition_name.Text = this.PositionName;

                }
                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    setData();
                    if (txtloan_person.Text != base.PersonCode && IsUserApprove && base.UserGroupCode != "Admin")
                    {
                        SetControlView(TabContainer1);
                        txtloan_date_due.ReadOnly = false;
                        txtloan_date_due.CssClass = "textbox";

                        txtloan_doc_no.ReadOnly = false;
                        txtloan_doc_no.CssClass = "textbox";

                        txtloan_doc_no.ReadOnly = false;
                        txtloan_doc_no.CssClass = "textbox";

                        txtloan_return.ReadOnly = false;
                        txtloan_return.CssClassDefault = "AwNumericDefault";

                        txtloan_return_remark.ReadOnly = false;
                        txtloan_return_remark.CssClass = "textbox";

                        foreach (Control ctrl in TabContainer1.Tabs[0].Controls[1].Controls)
                        {
                            if (ctrl is RequiredFieldValidator)
                            {
                                ((RequiredFieldValidator)ctrl).Enabled = false;
                            }
                        }

                    }
                }
                else if (ViewState["mode"].ToString().ToLower().Equals("add_edit"))
                {
                    autoSaveData();
                    setData();
                    setDataFromOpen();
                    if (saveData())
                    {
                        string strScript1 = "$('#divdes1').text().replace('เพิ่ม','แก้ไข');PopUpListPost('1','1');";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "loanPage", strScript1, true);
                        MsgBox("บันทึกข้อมูลสมบูรณ์");
                        setData();
                    }
                }

                else if (ViewState["mode"].ToString().ToLower().Equals("view"))
                {
                    base.PermissionURL = "~/App_Control/open/open_approve_list.aspx";

                    setData();
                    SetControlView(TabContainer1);
                    imgSaveOnly.Visible = false;
                }
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
            InitcboBudget();
            cboBudget.SelectedIndex = 0;

            InitcboDirector();
            //cboDirector.SelectedIndex = 0;

            InitcboPlan();
            cboPlan.SelectedIndex = 0;

            InitcboWork();
            cboWork.SelectedIndex = 0;

            InitcboFund();
            cboFund.SelectedIndex = 0;

            InitcboLot();
            cboLot.SelectedIndex = 0;

        }

        private void InitcboBudgetType()
        {
            cCommon oCommon = new cCommon();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strCode = this.BudgetType;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " Select * from  general where g_type = 'budget_type' and g_code <> 'M' Order by g_sort ";
            if (oCommon.SEL_SQL(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboBudget_type.Items.Clear();
                int i;
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboBudget_type.Items.Add(new ListItem(dt.Rows[i]["g_name"].ToString(), dt.Rows[i]["g_code"].ToString()));
                }
                cboBudget_type.Items.Add(new ListItem("อื่นๆ", "X"));
                if (cboBudget_type.Items.FindByValue(strCode) != null)
                {
                    cboBudget_type.SelectedIndex = -1;
                    cboBudget_type.Items.FindByValue(strCode).Selected = true;

                }
            }
        }

        private void InitcboLot()
        {
            cLot oLot = new cLot();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strLot_code = string.Empty;
            string strLot = cboLot.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and c_active='Y' ";
            strCriteria += " and lot_year='" + cboYear.SelectedValue + "' ";
            if (oLot.SP_SEL_LOT(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboLot.Items.Clear();
                cboLot.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboLot.Items.Add(new ListItem(dt.Rows[i]["lot_name"].ToString(), dt.Rows[i]["lot_code"].ToString()));
                }
                if (cboLot.Items.FindByValue(strLot) != null)
                {
                    cboLot.SelectedIndex = -1;
                    cboLot.Items.FindByValue(strLot).Selected = true;
                }
            }
        }

        private void InitcboLoan_offer()
        {
            cPerson_manage oPerson_manage = new cPerson_manage();
            string strMessage = string.Empty,
                strCriteria = string.Empty;
            string strPerson_manage = cboLoan_offer.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and c_active='Y' ";
            if (oPerson_manage.SP_PERSON_MANAGE_SEL(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboLoan_offer.Items.Clear();
                cboLoan_offer.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboLoan_offer.Items.Add(new ListItem(dt.Rows[i]["person_manage_name"].ToString(), dt.Rows[i]["person_manage_name"].ToString()));
                }
                if (cboLoan_offer.Items.FindByText(strPerson_manage) != null)
                {
                    cboLoan_offer.SelectedIndex = -1;
                    cboLoan_offer.Items.FindByValue(strPerson_manage).Selected = true;
                }
            }
        }

        private void InitcboBudget()
        {
            cBudget oBudget = new cBudget();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strbudget_code = string.Empty;
            string strYear = cboYear.SelectedValue;
            strbudget_code = cboBudget.SelectedValue; ;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and budget_year = '" + strYear + "'  and  c_active='Y' ";
            strCriteria = strCriteria + "  And budget_type ='" + this.BudgetType + "' ";
            if (oBudget.SP_SEL_BUDGET(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboBudget.Items.Clear();
                cboBudget.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
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
            cboProduce.SelectedIndex = 0;
        }

        private void InitcboProduce()
        {
            cProduce oProduce = new cProduce();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strbudget_code = string.Empty,
                        strproduce_code = string.Empty,
                        strproduce_name = string.Empty;
            strbudget_code = cboBudget.SelectedValue;
            strproduce_code = cboProduce.SelectedValue; ;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and produce.budget_code= '" + strbudget_code + "'  and  produce.c_active='Y' ";
            strCriteria = strCriteria + "  And produce.budget_type ='" + this.BudgetType + "' ";
            if (oProduce.SP_SEL_PRODUCE(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboProduce.Items.Clear();
                cboProduce.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
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
            cboActivity.SelectedIndex = 0;
        }

        private void InitcboDirector()
        {
            cDirector oDirector = new cDirector();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strDirector_code = string.Empty;
            string strYear = cboYear.SelectedValue;
            strDirector_code = cboDirector.SelectedValue == "" ? base.DirectorCode : cboDirector.SelectedValue;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and director_year = '" + strYear + "'  and  c_active='Y' ";
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
                cboDirector.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
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
                //cboUnit.SelectedIndex = 0;

            }
        }

        private void InitcboUnit()
        {
            cUnit oUnit = new cUnit();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strUnit_code = cboUnit.SelectedValue == "" ? base.UnitCode : cboUnit.SelectedValue;
            string strDirector_code = cboDirector.SelectedValue;
            string strYear = cboYear.SelectedValue;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and unit.unit_year = '" + strYear + "'  and  unit.c_active='Y' ";
            strCriteria = strCriteria + " and unit.director_code = '" + strDirector_code + "' ";
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
                cboUnit.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
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

        private void InitcboDoctype()
        {
            cefDoctype objEfDoctype = new cefDoctype();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strDoctype = string.Empty;
            strDoctype = cboDoctype.SelectedValue;
            int i;
            DataTable dt = new DataTable();
            strCriteria = " order by ef_doctype_name ";
            dt = objEfDoctype.SP_DOCTYPE_SEL(strCriteria);
            cboDoctype.Items.Clear();
            cboDoctype.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
            for (i = 0; i <= dt.Rows.Count - 1; i++)
            {
                cboDoctype.Items.Add(new ListItem(dt.Rows[i]["ef_doctype_name"].ToString(), dt.Rows[i]["ef_doctype_code"].ToString()));
            }
            if (cboDoctype.Items.FindByValue(strDoctype) != null)
            {
                cboDoctype.SelectedIndex = -1;
                cboDoctype.Items.FindByValue(strDoctype).Selected = true;
            }
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

            strCriteria = strCriteria + " and  produce.budget_code= '" + strbudget_code + "' ";
            strCriteria = strCriteria + " and activity.produce_code= '" + strproduce_code + "' ";
            strCriteria = strCriteria + " and activity.budget_type ='" + this.BudgetType + "' ";


            if (oActivity.SP_SEL_ACTIVITY(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboActivity.Items.Clear();
                cboActivity.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
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

        private void InitcboPlan()
        {
            cPlan oPlan = new cPlan();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strplan_code = string.Empty;
            string strYear = cboYear.SelectedValue;
            strplan_code = cboPlan.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and plan_year = '" + strYear + "'  and  c_active='Y' ";
            strCriteria = strCriteria + " and budget_type ='" + this.BudgetType + "' ";

            if (oPlan.SP_SEL_PLAN(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboPlan.Items.Clear();
                cboPlan.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboPlan.Items.Add(new ListItem(dt.Rows[i]["plan_name"].ToString(), dt.Rows[i]["plan_code"].ToString()));
                }
                if (cboPlan.Items.FindByValue(strplan_code) != null)
                {
                    cboPlan.SelectedIndex = -1;
                    cboPlan.Items.FindByValue(strplan_code).Selected = true;
                }
            }
        }

        private void InitcboWork()
        {
            cWork oWork = new cWork();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
            strwork_code = string.Empty;
            string strYear = cboYear.SelectedValue;
            strwork_code = cboWork.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and work_year = '" + strYear + "'  and  c_active='Y' ";
            strCriteria = strCriteria + " and budget_type ='" + this.BudgetType + "' ";

            if (oWork.SP_SEL_WORK(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboWork.Items.Clear();
                cboWork.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboWork.Items.Add(new ListItem(dt.Rows[i]["work_name"].ToString(), dt.Rows[i]["work_code"].ToString()));
                }
                if (cboWork.Items.FindByValue(strwork_code) != null)
                {
                    cboWork.SelectedIndex = -1;
                    cboWork.Items.FindByValue(strwork_code).Selected = true;
                }
            }
        }

        private void InitcboFund()
        {
            cFund oFund = new cFund();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
            strfund_code = string.Empty;
            string strYear = cboYear.SelectedValue;
            strfund_code = cboFund.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and fund_year = '" + strYear + "'  and  c_active='Y' ";
            strCriteria = strCriteria + " and budget_type ='" + this.BudgetType + "' ";

            if (oFund.SP_SEL_FUND(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboFund.Items.Clear();
                cboFund.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboFund.Items.Add(new ListItem(dt.Rows[i]["fund_name"].ToString(), dt.Rows[i]["fund_code"].ToString()));
                }
                if (cboFund.Items.FindByValue(strfund_code) != null)
                {
                    cboFund.SelectedIndex = -1;
                    cboFund.Items.FindByValue(strfund_code).Selected = true;
                }
            }
        }

        private void InitcboApprove(DropDownList cboApprove)
        {
            string strapprove_code = cboApprove.SelectedValue;
            cboApprove.Items.Clear();
            cboApprove.Items.Add(new ListItem("--เลือกข้อมูล--", ""));
            for (var i = 0; i <= this.dtApprove.Rows.Count - 1; i++)
            {
                cboApprove.Items.Add(new ListItem(this.dtApprove.Rows[i]["approve_name"].ToString(),
                    this.dtApprove.Rows[i]["approve_code"].ToString()));
            }
            if (cboApprove.Items.FindByValue(strapprove_code) != null)
            {
                cboApprove.SelectedIndex = -1;
                cboApprove.Items.FindByValue(strapprove_code).Selected = true;
            }
        }

        private void InitcboBank()
        {
            cBank oBank = new cBank();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string strBank_name = cboPay_bank.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            if (oBank.SP_SEL_BANK(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboPay_bank.Items.Clear();
                cboPay_bank.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboPay_bank.Items.Add(new ListItem(dt.Rows[i]["bank_name"].ToString(), dt.Rows[i]["bank_name"].ToString()));
                }
                if (cboPay_bank.Items.FindByValue(strBank_name) != null)
                {
                    cboPay_bank.SelectedIndex = -1;
                    cboPay_bank.Items.FindByValue(strBank_name).Selected = true;
                }
            }
        }

        private void InitcboBank_Branch()
        {
            cBranch oBranch = new cBranch();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strBank_name = string.Empty;
            strBank_name = cboPay_bank.SelectedValue;
            string strBranch_name = cboPay_bank_branch.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and bank.bank_name = '" + strBank_name + "' ";
            if (oBranch.SP_SEL_BRANCH(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboPay_bank_branch.Items.Clear();
                cboPay_bank_branch.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboPay_bank_branch.Items.Add(new ListItem(dt.Rows[i]["branch_name"].ToString(), dt.Rows[i]["branch_name"].ToString()));
                }
                if (cboPay_bank_branch.Items.FindByValue(strBranch_name) != null)
                {
                    cboPay_bank_branch.SelectedIndex = -1;
                    cboPay_bank_branch.Items.FindByValue(strBranch_name).Selected = true;
                }
            }
        }


        public void SetControlView(Control control)
        {
            foreach (Control ctrl in control.Controls)
            {
                if (ctrl is ImageButton)
                {
                    if (!ctrl.ID.Contains("imgPrint") && !ctrl.ID.Contains("imgView"))
                        ctrl.Visible = false;
                }
                else if (ctrl is TextBox)
                {
                    ((TextBox)ctrl).ReadOnly = true;
                    ((TextBox)ctrl).CssClass = "textboxdis";
                }
                else if (ctrl is DropDownList)
                {
                    ((DropDownList)ctrl).Enabled = false;
                    ((DropDownList)ctrl).CssClass = "textboxdis";
                }
                else
                {
                    if (ctrl.Controls.Count > 0)
                    {
                        SetControlView(ctrl);
                    }
                }
            }
        }

        #endregion

        private bool autoSaveData()
        {
            bool blnResult = false;
            string strMessage = string.Empty;
            long lintloan_id = 0;
            string strloan_doc = string.Empty;
            string strUserName = string.Empty;
            string strloan_reason = string.Empty;
            string strCriteria = string.Empty;

            cefOpen objEfOpen = new cefOpen();
            cefLoan objEfLoan = new cefLoan();
            DataTable dt = new DataTable();
            DataRow dr;
            try
            {
                #region set Data
                strUserName = Session["username"].ToString();
                #endregion
                if (ViewState["mode"].ToString().ToLower().Equals("add_edit"))
                {
                    #region insert


                    strCriteria = " and open_head_id = '" + ViewState["open_head_id"].ToString() + "' ";
                    dt = objEfOpen.SP_OPEN_HEAD_SEL(strCriteria);
                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.Rows[0];

                        strloan_reason = Helper.CStr(dr["open_title"]).Replace("ขออนุมัติเบิก", "เพื่อเป็น");

                        ViewState["open_code"] = Helper.CStr(dr["open_code"]);

                        if (objEfLoan.SP_LOAN_HEAD_INS(ref lintloan_id, ref strloan_doc, txtloan_doc_no.Text, Helper.CStr(dr["open_year"]), DateTime.Now.Date.ToString("dd/MM/yyyy"), "",
                             Helper.CStr(dr["open_path"]), "", Helper.CStr(dr["person_open"]), Helper.CStr(dr["position_code"]), Helper.CStr(dr["position_name"]),
                            strloan_reason, Helper.CStr(dr["open_to"]), Helper.CStr(dr["budget_type"]), Helper.CStr(dr["budget_type_text"]), Helper.CStr(dr["budget_plan_code"]), Helper.CStr(dr["director_code"]),
                            Helper.CStr(dr["unit_code"]), Helper.CStr(dr["budget_code"]), Helper.CStr(dr["produce_code"]), Helper.CStr(dr["activity_code"]), Helper.CStr(dr["plan_code"]),
                            Helper.CStr(dr["work_code"]), Helper.CStr(dr["fund_code"]), Helper.CStr(dr["lot_code"]), "0", "0", "W", Helper.CStr(dr["open_tel"]), Helper.CStr(dr["open_remark"]), 0, "", Helper.CStr(dr["ef_doctype_code"]), strUserName))
                        {
                            ViewState["loan_id"] = lintloan_id;
                            ViewState["mode"] = "edit";
                            blnResult = true;
                        }

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
                objEfOpen.Dispose();
                objEfLoan.Dispose();
            }
            return blnResult;
        }

        private bool saveData()
        {
            bool blnResult = false;
            string strMessage = string.Empty;
            long lintloan_id = 0;
            string strloan_doc = string.Empty;
            string strUserName = string.Empty;
            string strloanTo = string.Empty;

            cefLoan objEfloan = new cefLoan();
            try
            {
                #region set Data
                strloan_doc = txtloan_doc.Text;
                strUserName = Session["username"].ToString();
                strloanTo = txtloan_offer.Text.Trim().Length > 0 ? txtloan_offer.Text.Trim() : cboLoan_offer.SelectedValue;
                #endregion
                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    #region insert
                    if (objEfloan.SP_LOAN_HEAD_INS(ref lintloan_id, ref strloan_doc, txtloan_doc_no.Text, cboYear.SelectedValue, txtloan_date.Text, txtloan_date_due.Text,
                        txtloan_path.Text, txtloan_no.Text, txtloan_person.Text, txtposition_code.Text, txtposition_name.Text,
                        txtloan_reason.Text, strloanTo, cboBudget_type.SelectedValue, txtbudget_type_text.Text, txtbudget_plan_code.Text, cboDirector.SelectedValue,
                       cboUnit.SelectedValue, cboBudget.SelectedValue, cboProduce.SelectedValue, cboActivity.SelectedValue, cboPlan.SelectedValue,
                        cboWork.SelectedValue, cboFund.SelectedValue, cboLot.SelectedValue, "0", "0", "W", txtloan_tel.Text, txtloan_remark.Text, 0, "", cboDoctype.SelectedValue, strUserName))
                    {
                        ViewState["loan_id"] = lintloan_id;
                        ViewState["mode"] = "edit";
                        blnResult = true;
                    }
                    #endregion
                }
                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    #region update
                    lintloan_id = Helper.CLong(ViewState["loan_id"]);

                    if (objEfloan.SP_LOAN_HEAD_UPD(lintloan_id, strloan_doc, txtloan_doc_no.Text, cboYear.SelectedValue, txtloan_date.Text, txtloan_date_due.Text,
                       txtloan_path.Text, txtloan_no.Text, txtloan_person.Text, txtposition_code.Text, txtposition_name.Text,
                       txtloan_reason.Text, strloanTo, cboBudget_type.SelectedValue, txtbudget_type_text.Text, txtbudget_plan_code.Text, cboDirector.SelectedValue,
                       cboUnit.SelectedValue, cboBudget.SelectedValue, cboProduce.SelectedValue, cboActivity.SelectedValue, cboPlan.SelectedValue,
                       cboWork.SelectedValue, cboFund.SelectedValue, cboLot.SelectedValue, txtloan_tel.Text, txtloan_remark.Text, Helper.CDbl(txtloan_return.Value), txtloan_return_remark.Text, cboDoctype.SelectedValue,
                       RadioButtonList1.SelectedValue, txtpay_acc_no.Text, txtpay_name.Text, cboPay_bank.SelectedValue, cboPay_bank_branch.SelectedValue, txtpay_remark.Text, strUserName))
                    {
                        SaveDetail();
                        SaveApprove();
                        SaveOpen();
                        SaveAttach();
                        objEfloan.SP_LOAN_HEAD_APPROVE_UPD(lintloan_id, strUserName);
                        objEfloan.SP_LOAN_HEAD_SUM_UPD(lintloan_id, strUserName);
                        blnResult = true;
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
                objEfloan.Dispose();
            }
            return blnResult;
        }

        protected void imgPrint_Click(object sender, ImageClickEventArgs e)
        {
            string strScript = "windowloanMaximize(\"../../App_Control/reportsparameter/loan_report_show.aspx?loan_id=" + hddloan_id.Value + "\", \"_blank\");\n";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "loanPage", strScript, true);
        }

        private void setData()
        {
            cefLoan opjEfloan = new cefLoan();
            DataTable dt = new DataTable();
            string strMessage = string.Empty,
                strCriteria = string.Empty,
                strUpdatedBy = string.Empty,
                strUpdatedDate = string.Empty;
            try
            {
                txtloan_doc.ReadOnly = true;
                txtloan_doc.CssClass = "textboxdis";
                strCriteria = " and loan_id = '" + Helper.CInt(ViewState["loan_id"]) + "' ";
                dt = opjEfloan.SP_LOAN_HEAD_SEL(strCriteria);
                if (dt.Rows.Count > 0)
                {
                    #region get Data
                    hddloan_id.Value = dt.Rows[0]["loan_id"].ToString();
                    txtloan_doc.Text = dt.Rows[0]["loan_doc"].ToString();
                    cboYear.SelectedValue = dt.Rows[0]["loan_year"].ToString();
                    txtloan_path.Text = dt.Rows[0]["loan_path"].ToString();
                    txtloan_no.Text = dt.Rows[0]["loan_no"].ToString();

                    InitcboBudgetType();
                    if (cboBudget_type.Items.FindByValue(dt.Rows[0]["budget_type"].ToString()) != null)
                    {
                        cboBudget_type.SelectedIndex = -1;
                        cboBudget_type.Items.FindByValue(dt.Rows[0]["budget_type"].ToString()).Selected = true;
                    }


                    txtloan_date.Text = cCommon.CheckDate(dt.Rows[0]["loan_date"].ToString());

                    txtloan_date_due.Text = dt.Rows[0]["loan_date_due"].ToString().Length > 0 ? cCommon.CheckDate(dt.Rows[0]["loan_date_due"].ToString()) : "";

                    InitcboLoan_offer();
                    if (cboLoan_offer.Items.FindByValue(dt.Rows[0]["loan_offer"].ToString()) != null)
                    {
                        cboLoan_offer.SelectedIndex = -1;
                        cboLoan_offer.Items.FindByValue(dt.Rows[0]["loan_offer"].ToString()).Selected = true;
                    }

                    if (cboDoctype.Items.FindByValue(dt.Rows[0]["ef_doctype_code"].ToString()) != null)
                    {
                        cboDoctype.SelectedIndex = -1;
                        cboDoctype.Items.FindByValue(dt.Rows[0]["ef_doctype_code"].ToString()).Selected = true;
                    }

                    txtloan_offer.Text = dt.Rows[0]["loan_offer"].ToString();

                    txtloan_reason.Text = dt.Rows[0]["loan_reason"].ToString();

                    //txtloan_command_desc.Text = dt.Rows[0]["loan_command_desc"].ToString();
                    txtloan_tel.Text = dt.Rows[0]["loan_tel"].ToString();
                    txtbudget_plan_code.Text = dt.Rows[0]["budget_plan_code"].ToString();
                    InitcboBudget();
                    if (cboBudget.Items.FindByValue(dt.Rows[0]["budget_code"].ToString()) != null)
                    {
                        cboBudget.SelectedIndex = -1;
                        cboBudget.Items.FindByValue(dt.Rows[0]["budget_code"].ToString()).Selected = true;
                    }
                    txtbudget_type_text.Text = dt.Rows[0]["budget_type_text"].ToString();

                    InitcboProduce();
                    if (cboProduce.Items.FindByValue(dt.Rows[0]["produce_code"].ToString()) != null)
                    {
                        cboProduce.SelectedIndex = -1;
                        cboProduce.Items.FindByValue(dt.Rows[0]["produce_code"].ToString()).Selected = true;
                    }

                    InitcboActivity();
                    if (cboActivity.Items.FindByValue(dt.Rows[0]["activity_code"].ToString()) != null)
                    {
                        cboActivity.SelectedIndex = -1;
                        cboActivity.Items.FindByValue(dt.Rows[0]["activity_code"].ToString()).Selected = true;
                    }

                    InitcboDirector();
                    if (cboDirector.Items.FindByValue(dt.Rows[0]["director_code"].ToString()) != null)
                    {
                        cboDirector.SelectedIndex = -1;
                        cboDirector.Items.FindByValue(dt.Rows[0]["director_code"].ToString()).Selected = true;
                    }

                    InitcboUnit();
                    if (cboUnit.Items.FindByValue(dt.Rows[0]["unit_code"].ToString()) != null)
                    {
                        cboUnit.SelectedIndex = -1;
                        cboUnit.Items.FindByValue(dt.Rows[0]["unit_code"].ToString()).Selected = true;
                    }

                    InitcboPlan();
                    if (cboPlan.Items.FindByValue(dt.Rows[0]["plan_code"].ToString()) != null)
                    {
                        cboPlan.SelectedIndex = -1;
                        cboPlan.Items.FindByValue(dt.Rows[0]["plan_code"].ToString()).Selected = true;
                    }

                    InitcboWork();
                    if (cboWork.Items.FindByValue(dt.Rows[0]["work_code"].ToString()) != null)
                    {
                        cboWork.SelectedIndex = -1;
                        cboWork.Items.FindByValue(dt.Rows[0]["work_code"].ToString()).Selected = true;
                    }

                    InitcboFund();
                    if (cboFund.Items.FindByValue(dt.Rows[0]["fund_code"].ToString()) != null)
                    {
                        cboFund.SelectedIndex = -1;
                        cboFund.Items.FindByValue(dt.Rows[0]["fund_code"].ToString()).Selected = true;
                    }

                    InitcboLot();
                    if (cboLot.Items.FindByValue(dt.Rows[0]["lot_code"].ToString()) != null)
                    {
                        cboLot.SelectedIndex = -1;
                        cboLot.Items.FindByValue(dt.Rows[0]["lot_code"].ToString()).Selected = true;
                    }


                    if (RadioButtonList1.Items.FindByValue(dt.Rows[0]["pay_type"].ToString()) != null)
                    {
                        RadioButtonList1.SelectedIndex = -1;
                        RadioButtonList1.Items.FindByValue(dt.Rows[0]["pay_type"].ToString()).Selected = true;
                    }
                    SetPayLabel(RadioButtonList1.SelectedValue);

                    txtpay_acc_no.Text = dt.Rows[0]["pay_acc_no"].ToString();
                    txtpay_name.Text = dt.Rows[0]["pay_name"].ToString();
                    InitcboBank();
                    if (cboPay_bank.Items.FindByValue(dt.Rows[0]["pay_bank"].ToString()) != null)
                    {
                        cboPay_bank.SelectedIndex = -1;
                        cboPay_bank.Items.FindByValue(dt.Rows[0]["pay_bank"].ToString()).Selected = true;
                    }

                    InitcboBank_Branch();
                    if (cboPay_bank_branch.Items.FindByValue(dt.Rows[0]["pay_bank_branch"].ToString()) != null)
                    {
                        cboPay_bank_branch.SelectedIndex = -1;
                        cboPay_bank_branch.Items.FindByValue(dt.Rows[0]["pay_bank_branch"].ToString()).Selected = true;
                    }
                    txtpay_remark.Text = dt.Rows[0]["pay_remark"].ToString();

                    txtloan_remark.Text = dt.Rows[0]["loan_remark"].ToString();
                    txtloan_person.Text = dt.Rows[0]["person_code"].ToString();
                    txtloan_person_name.Text = Helper.CStr(dt.Rows[0]["person_thai_name"]) + " " + Helper.CStr(dt.Rows[0]["person_thai_surname"]);
                    txtposition_code.Text = dt.Rows[0]["position_code"].ToString();
                    txtposition_name.Text = Helper.CStr(dt.Rows[0]["position_name"]);

                    txtloan_return.Value = Helper.CDbl(dt.Rows[0]["loan_return"]);
                    txtloan_return_remark.Text = Helper.CStr(dt.Rows[0]["loan_return_remark"]);
                    txtloan_doc_no.Text = Helper.CStr(dt.Rows[0]["loan_doc_no"]);

                    strUpdatedBy = dt.Rows[0]["c_updated_by"].ToString();
                    strUpdatedDate = dt.Rows[0]["d_updated_date"].ToString();

                    lblLoan_status.Text = getLoanStatusText(Helper.CStr(dt.Rows[0]["loan_status"]));


                    #endregion

                    #region set Control
                    txtUpdatedBy.Text = strUpdatedBy;
                    txtUpdatedDate.Text = strUpdatedDate;
                    BindGridDetail();
                    BindGridApprove();
                    BindGridOpen();
                    BindGridAttach();
                    #endregion
                }
                TabContainer1.Tabs[1].Visible = true;
                TabContainer1.Tabs[2].Visible = true;
                TabContainer1.Tabs[3].Visible = true;
                TabContainer1.Tabs[4].Visible = true;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        private string getLoanStatusText(string loan_status)
        {
            string result = "";
            if (loan_status == "P")
            {
                result = "รออนุมัติ";
            }
            else if (loan_status == "A")
            {
                result = "อนุมัติ";
            }
            else if (loan_status == "N")
            {
                result = "ไม่อนุมัติ";
            }
            else if (loan_status == "X")
            {
                result = "อนุมัติบางส่วน";
            }
            else if (loan_status == "W")
            {
                result = "รายการยังไม่สมบูรณ์";
            }
            else if (loan_status == "C")
            {
                result = "ยกเลิกรายการ";
            }
            else if (loan_status == "S")
            {
                result = "ชำระคืนบางส่วน";
            }
            else if (loan_status == "F")
            {
                result = "ชำระคืนหมดแล้ว";
            }
            return result;
        }

        private void setDataFromOpen()
        {
            cefOpenApprove objOpenApprove = new cefOpenApprove();
            cefOpen objOpen = new cefOpen();
            DataTable dt = new DataTable();
            DataRow dr;
            string strMessage = string.Empty,
                strCriteria = string.Empty,
                strUpdatedBy = string.Empty,
                strUpdatedDate = string.Empty;
            try
            {
                //Set Loan Detail
                _strCriteria = " and open_head_id = " + Helper.CInt(ViewState["open_head_id"]) + " order by open_detail_id";
                dt = objOpen.SP_OPEN_DETAIL_SEL(_strCriteria);
                foreach (DataRow drow in dt.Rows)
                {
                    dr = this.dtLoanDetail.NewRow();
                    dr["loan_detail_id"] = ++this.LoanDetailID;
                    dr["material_id"] = Helper.CInt(drow["material_id"]);
                    dr["material_name"] = Helper.CStr(drow["material_name"]);
                    dr["material_detail"] = Helper.CStr(drow["material_detail"]);
                    dr["loan_detail_amount"] = Helper.CDbl(drow["open_detail_amount"]);
                    dr["loan_detail_remark"] = Helper.CStr(drow["open_detail_remark"]);
                    dr["row_status"] = "N";
                    this.dtLoanDetail.Rows.Add(dr);
                }
                BindGridDetail();


                //Set Loan Approve              
                string strbudget_type = cboBudget_type.SelectedValue;
                strbudget_type = strbudget_type == "X" ? "R" : strbudget_type;
                _strCriteria = " and open_code = " + Helper.CInt(ViewState["open_code"]) + " and budget_type = '" + strbudget_type + "' ";
                dt = objOpenApprove.SP_OPEN_APPROVE_SEL(_strCriteria);
                int countRow = 0;
                foreach (DataRow drow in dt.Rows)
                {
                    countRow++;
                    dr = this.dtLoanApprove.NewRow();
                    dr["loan_detail_approve_id"] = ++this.LoanApproveID;
                    dr["approve_code"] = Helper.CInt(drow["approve_code"]);
                    dr["approve_name"] = Helper.CStr(drow["approve_name"]);
                    dr["approve_level"] = Helper.CInt(drow["approve_level"]);
                    if (countRow != dt.Rows.Count)
                    {
                        dr["approve_remark"] = "หัวหน้าผู้ควบคุม";
                    }
                    else
                    {
                        dr["approve_remark"] = "ผู้อนุมัติ";
                    }
                    dr["person_code"] = Helper.CStr(drow["person_approve_code"]);
                    dr["person_thai_name"] = Helper.CStr(drow["title_name"]) + Helper.CStr(drow["person_thai_name"]) + " " + Helper.CStr(drow["person_thai_surname"]);
                    dr["person_manage_code"] = Helper.CStr(drow["person_manage_code"]);
                    dr["person_manage_name"] = Helper.CStr(drow["person_manage_name"]);
                    dr["approve_status"] = "P";
                    dr["row_status"] = "N";
                    this.dtLoanApprove.Rows.Add(dr);
                }
                BindGridApprove();

                //Open Loan
                strCriteria = " and open_head_id = '" + ViewState["open_head_id"].ToString() + "' ";
                dt = objOpen.SP_OPEN_HEAD_SEL(strCriteria);
                if (dt.Rows.Count > 0)
                {
                    DataRow drow = dt.Rows[0];
                    dr = this.dtOpenLoan.NewRow();
                    dr["open_loan_id"] = ++OpenLoanID;
                    dr["open_head_id"] = Helper.CInt(drow["open_head_id"]);
                    dr["open_doc"] = Helper.CStr(drow["open_doc"]);
                    dr["open_title"] = Helper.CStr(drow["open_title"]);
                    dr["open_amount"] = Helper.CStr(drow["open_amount"]);
                    dr["open_date"] = cCommon.CheckDate(drow["open_date"].ToString());
                    dr["row_status"] = "N";
                    this.dtOpenLoan.Rows.Add(dr);
                    BindGridOpen();
                }

            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        private void StoreAllData()
        {
            StoreDetail();
            StoreApprove();
            StoreOpen();
            StoreAttach();
        }


        #region EmptyGridFix
        protected void EmptyGridFix(GridView grdView)
        {
            // normally executes after a grid load method
            if (grdView.Rows.Count == 0 &&
                grdView.DataSource != null)
            {
                DataTable dt = null;

                // need to clone sources otherwise it will be indirectly adding to 
                // the original source

                if (grdView.DataSource is DataSet)
                {
                    dt = ((DataSet)grdView.DataSource).Tables[0].Clone();
                }
                else if (grdView.DataSource is DataTable)
                {
                    dt = ((DataTable)grdView.DataSource).Clone();
                }

                if (dt == null)
                {
                    return;
                }

                dt.Rows.Add(dt.NewRow()); // add empty row
                grdView.DataSource = dt;
                grdView.DataBind();

                // hide row
                grdView.Rows[0].Visible = false;
                grdView.Rows[0].Controls.Clear();
            }

            // normally executes at all postbacks
            if (grdView.Rows.Count == 1 &&
                grdView.DataSource != null)
            {
                bool bIsGridEmpty = true;

                // check first row that all cells empty
                for (int i = 0; i < grdView.Rows[0].Cells.Count; i++)
                {
                    if (grdView.Rows[0].Cells[i].Text != string.Empty)
                    {
                        bIsGridEmpty = false;
                    }
                }
                // hide row
                if (bIsGridEmpty)
                {
                    grdView.Rows[0].Visible = false;
                    grdView.Rows[0].Controls.Clear();
                }
            }
        }
        #endregion

        #region GridView1 Event

        private void StoreDetail()
        {
            try
            {
                HiddenField hddloan_detail_id;
                HiddenField hddmaterial_id;
                TextBox txtmaterial_name;
                TextBox txtmaterial_detail;
                AwNumeric txtloan_detail_amount;
                TextBox txtloan_detail_remark;
                foreach (GridViewRow gvRow in GridView1.Rows)
                {
                    hddloan_detail_id = (HiddenField)gvRow.FindControl("hddloan_detail_id");
                    hddmaterial_id = (HiddenField)gvRow.FindControl("hddmaterial_id");
                    txtmaterial_name = (TextBox)gvRow.FindControl("txtmaterial_name");
                    txtmaterial_detail = (TextBox)gvRow.FindControl("txtmaterial_detail");
                    txtloan_detail_amount = (AwNumeric)gvRow.FindControl("txtloan_detail_amount");
                    txtloan_detail_remark = (TextBox)gvRow.FindControl("txtloan_detail_remark");
                    foreach (DataRow dr in this.dtLoanDetail.Rows)
                    {
                        if (Helper.CLong(dr["loan_detail_id"]) == Helper.CLong(hddloan_detail_id.Value))
                        {
                            dr["loan_detail_id"] = hddloan_detail_id.Value;
                            dr["material_id"] = hddmaterial_id.Value;
                            dr["material_name"] = txtmaterial_name.Text;
                            dr["material_detail"] = txtmaterial_detail.Text;
                            dr["loan_detail_amount"] = txtloan_detail_amount.Value;
                            dr["loan_detail_remark"] = txtloan_detail_remark.Text;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
            }
        }
        private void BindGridDetail()
        {
            DataView dv = null;
            try
            {
                dv = new DataView(this.dtLoanDetail, "row_status<>'D'", (ViewState["sort"] + " " + ViewState["direction"]), DataViewRowState.CurrentRows);
                GridView1.DataSource = dv.ToTable();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                this.bIsGridDetailEmpty = false;
                if (dv.ToTable().Rows.Count == 0)
                {
                    this.bIsGridDetailEmpty = true;
                    EmptyGridFix(GridView1);
                }
                else
                {
                    GridView1.DataBind();
                }
            }
        }
        private bool SaveDetail()
        {
            bool blnResult = true;
            cefLoan objEfloan = new cefLoan();
            try
            {
                string strUserName = Session["username"].ToString();
                StoreAllData();
                foreach (DataRow dr in this.dtLoanDetail.Rows)
                {
                    if (Helper.CStr(dr["material_name"]).Trim().Length > 0)
                    {
                        if (Helper.CStr(dr["row_status"]) == "N")
                        {
                            objEfloan.SP_LOAN_DETAIL_INS(Helper.CLong(ViewState["loan_id"]),
                                Helper.CInt(dr["material_id"]),
                                Helper.CStr(dr["material_name"]),
                                Helper.CStr(dr["material_detail"]),
                                Helper.CStr(dr["loan_detail_remark"]),
                                Helper.CDbl(dr["loan_detail_amount"]));
                        }
                        else if (Helper.CStr(dr["row_status"]) == "O")
                        {
                            objEfloan.SP_LOAN_DETAIL_UPD(Helper.CLong(dr["loan_detail_id"]),
                                 Helper.CInt(dr["material_id"]),
                                Helper.CStr(dr["material_name"]),
                                Helper.CStr(dr["material_detail"]),
                                Helper.CStr(dr["loan_detail_remark"]),
                                Helper.CDbl(dr["loan_detail_amount"]));
                        }
                        else if (Helper.CStr(dr["row_status"]) == "D")
                        {
                            objEfloan.SP_LOAN_DETAIL_DEL(Helper.CLong(dr["loan_detail_id"]));
                        }
                    }
                }
                this.dtLoanDetail = null;
                blnResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objEfloan.Dispose();
            }
            return blnResult;
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

                ImageButton imgAdd = (ImageButton)e.Row.FindControl("imgAdd");
                imgAdd.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgGridAdd"].Rows[0]["img"].ToString();
                imgAdd.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgGridAdd"].Rows[0]["title"].ToString());
                ViewState["TotalAmount"] = 0;

            }
            else if (e.Row.RowType.Equals(DataControlRowType.DataRow) || e.Row.RowState.Equals(DataControlRowState.Alternate))
            {
                if (!this.bIsGridDetailEmpty)
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
                    DataRowView dv = (DataRowView)e.Row.DataItem;
                    Label lblNo = (Label)e.Row.FindControl("lblNo");
                    int nNo = (GridView1.PageSize * GridView1.PageIndex) + e.Row.RowIndex + 1;
                    lblNo.Text = nNo.ToString();
                    #region set Image Delete

                    ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                    imgDelete.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["img"].ToString();
                    imgDelete.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["title"].ToString());
                    imgDelete.Attributes.Add("onclick", "return confirm(\"คุณต้องการลบข้อมูลนี้หรือไม่ ?\");");
                    #endregion

                    ViewState["TotalAmount"] = Helper.CDbl(ViewState["TotalAmount"]) +
                                               Helper.CDbl(dv["loan_detail_amount"]);
                }

            }
            else if (e.Row.RowType.Equals(DataControlRowType.Footer))
            {
                AwNumeric txtloan_amount = (AwNumeric)e.Row.FindControl("txtloan_amount");
                txtloan_amount.Value = Helper.CDbl(ViewState["TotalAmount"]);
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
                    if (ViewState["sort2"].Equals(GridView1.Columns[i].SortExpression))
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
                            if (ViewState["direction2"].Equals("ASC"))
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
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var hddloan_detail_id = (HiddenField)GridView1.Rows[e.RowIndex].FindControl("hddloan_detail_id");
            try
            {
                StoreAllData();
                int i = 0;
                foreach (DataRow dr in this.dtLoanDetail.Rows)
                {
                    if (Helper.CLong(dr["loan_detail_id"]) == Helper.CLong(hddloan_detail_id.Value))
                    {
                        dr["row_status"] = "D";
                        break;
                    }
                    ++i;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
            }
            BindGridDetail();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {
                case "ADD":
                    StoreAllData();
                    DataRow dr = this.dtLoanDetail.NewRow();
                    dr["loan_detail_id"] = ++this.LoanDetailID;
                    dr["material_id"] = "0";
                    dr["material_name"] = string.Empty;
                    dr["material_detail"] = string.Empty;
                    dr["loan_detail_amount"] = 0;
                    dr["loan_detail_remark"] = string.Empty;
                    dr["row_status"] = "N";
                    this.dtLoanDetail.Rows.Add(dr);
                    BindGridDetail();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region GridView2 Event

        private void StoreApprove()
        {
            try
            {
                HiddenField hddloan_detail_approve_id;
                DropDownList cboApprove, cboApproveStatus;
                AwNumeric txtapprove_level;
                TextBox txtperson_manage_code;
                TextBox txtperson_manage_name;
                TextBox txtapprove_person_code;
                TextBox txtapprove_person_name;
                TextBox txtapprove_remark;
                DropDownList cboApproveRemark;
                foreach (GridViewRow gvRow in GridView2.Rows)
                {
                    hddloan_detail_approve_id = (HiddenField)gvRow.FindControl("hddloan_detail_approve_id");
                    cboApprove = (DropDownList)gvRow.FindControl("cboApprove");
                    txtapprove_level = (AwNumeric)gvRow.FindControl("txtapprove_level");
                    txtapprove_person_code = (TextBox)gvRow.FindControl("txtapprove_person_code");
                    txtapprove_person_name = (TextBox)gvRow.FindControl("txtapprove_person_name");
                    txtperson_manage_code = (TextBox)gvRow.FindControl("txtperson_manage_code");
                    txtperson_manage_name = (TextBox)gvRow.FindControl("txtperson_manage_name");
                    txtapprove_remark = (TextBox)gvRow.FindControl("txtapprove_remark");
                    cboApproveStatus = (DropDownList)gvRow.FindControl("cboApproveStatus");
                    cboApproveRemark = (DropDownList)gvRow.FindControl("cboApproveRemark");
                    foreach (DataRow dr in this.dtLoanApprove.Rows)
                    {
                        if (Helper.CLong(dr["loan_detail_approve_id"]) == Helper.CLong(hddloan_detail_approve_id.Value))
                        {
                            dr["approve_code"] = Helper.CInt(cboApprove.SelectedValue);
                            //dr["approve_name"] = Helper.CStr(cboApprove.SelectedItem.Text);
                            dr["approve_level"] = Helper.CInt(txtapprove_level.Value);
                            dr["approve_remark"] = Helper.CStr(txtapprove_remark.Text).Length == 0 ? cboApproveRemark.SelectedValue : Helper.CStr(txtapprove_remark.Text);
                            dr["person_code"] = Helper.CStr(txtapprove_person_code.Text);
                            dr["person_thai_name"] = Helper.CStr(txtapprove_person_name.Text);
                            dr["person_thai_surname"] = string.Empty;
                            dr["person_manage_code"] = Helper.CStr(txtperson_manage_code.Text);
                            dr["person_manage_name"] = Helper.CStr(txtperson_manage_name.Text);
                            dr["approve_status"] = Helper.CStr(cboApproveStatus.SelectedValue);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
            }
        }
        private void BindGridApprove()
        {
            DataView dv = null;
            try
            {
                dv = new DataView(this.dtLoanApprove, "row_status<>'D'", (ViewState["sort2"] + " " + ViewState["direction2"]), DataViewRowState.CurrentRows);
                GridView2.DataSource = dv.ToTable();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                this.bIsGridApproveEmpty = false;
                if (dv.ToTable().Rows.Count == 0)
                {
                    this.bIsGridApproveEmpty = true;
                    EmptyGridFix(GridView2);
                }
                else
                {
                    GridView2.DataBind();
                }
            }
        }
        private bool SaveApprove()
        {
            bool blnResult = false;
            cefLoan objEefLoan = new cefLoan();
            try
            {
                string strUserName = Session["username"].ToString();
                StoreAllData();
                foreach (DataRow dr in this.dtLoanApprove.Rows)
                {
                    if (Helper.CStr(dr["person_code"]).Trim().Length > 0)
                    {
                        if (Helper.CStr(dr["row_status"]) == "N")
                        {
                            objEefLoan.SP_LOAN_DETAIL_APPROVE_INS(
                                Helper.CInt(ViewState["loan_id"]),
                                Helper.CInt(dr["approve_code"]),
                                Helper.CStr(dr["approve_name"]),
                                Helper.CInt(dr["approve_level"]),
                                Helper.CStr(dr["person_code"]),
                                Helper.CStr(dr["person_manage_code"]),
                                Helper.CStr(dr["person_manage_name"]),
                                Helper.CStr(dr["approve_remark"]),
                                Helper.CStr(dr["approve_status"]),
                                strUserName);
                        }
                        else if (Helper.CStr(dr["row_status"]) == "O")
                        {
                            objEefLoan.SP_LOAN_DETAIL_APPROVE_UPD(
                                Helper.CInt(dr["loan_detail_approve_id"]),
                                Helper.CInt(dr["approve_code"]),
                                Helper.CStr(dr["approve_name"]),
                                Helper.CInt(dr["approve_level"]),
                                Helper.CStr(dr["person_code"]),
                                Helper.CStr(dr["person_manage_code"]),
                                Helper.CStr(dr["person_manage_name"]),
                                Helper.CStr(dr["approve_remark"]),
                                Helper.CStr(dr["approve_status"]),
                                strUserName);
                        }
                        else if (Helper.CStr(dr["row_status"]) == "D")
                        {
                            objEefLoan.SP_LOAN_DETAIL_APPROVE_DEL(Helper.CInt(dr["loan_detail_approve_id"]));
                        }
                    }
                }
                this.dtLoanApprove = null;
                blnResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objEefLoan.Dispose();
            }
            return blnResult;
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.Header))
            {
                for (int iCol = 0; iCol < e.Row.Cells.Count; iCol++)
                {
                    e.Row.Cells[iCol].Attributes.Add("class", "table_h");
                    e.Row.Cells[iCol].Wrap = false;
                }

                ImageButton imgAdd = (ImageButton)e.Row.FindControl("imgAdd");
                imgAdd.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgGridAdd"].Rows[0]["img"].ToString();
                imgAdd.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgGridAdd"].Rows[0]["title"].ToString());

            }
            else if (e.Row.RowType.Equals(DataControlRowType.DataRow) || e.Row.RowState.Equals(DataControlRowState.Alternate))
            {
                if (!this.bIsGridApproveEmpty)
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
                    DataRowView dv = (DataRowView)e.Row.DataItem;
                    Label lblNo = (Label)e.Row.FindControl("lblNo");
                    int nNo = (GridView2.PageSize * GridView2.PageIndex) + e.Row.RowIndex + 1;
                    lblNo.Text = nNo.ToString();
                    var strApprove_code = Helper.CStr(dv["approve_code"]);
                    DropDownList cboApprove = (DropDownList)e.Row.FindControl("cboApprove");
                    DropDownList cboApproveStatus = (DropDownList)e.Row.FindControl("cboApproveStatus");
                    DropDownList cboApproveRemark = (DropDownList)e.Row.FindControl("cboApproveRemark");
                    //this.InitcboApprove(cboApprove);
                    //if (cboApprove.Items.FindByValue(strApprove_code) != null)
                    //{
                    //    cboApprove.SelectedIndex = -1;
                    //    cboApprove.Items.FindByValue(strApprove_code).Selected = true;
                    //}
                    var strApprove_status = Helper.CStr(dv["approve_status"]);
                    if (cboApproveStatus.Items.FindByValue(strApprove_status) != null)
                    {
                        cboApproveStatus.SelectedIndex = -1;
                        cboApproveStatus.Items.FindByValue(strApprove_status).Selected = true;
                    }

                    var strApprove_Remark = Helper.CStr(dv["approve_remark"]);
                    if (cboApproveRemark.Items.FindByValue(strApprove_Remark) != null)
                    {
                        cboApproveRemark.SelectedIndex = -1;
                        cboApproveRemark.Items.FindByValue(strApprove_Remark).Selected = true;
                    }

                    if (ViewState["mode"].ToString().ToLower().Equals("view"))
                    {
                        cboApproveRemark.Visible = false;
                    }


                    #region set Image Delete

                    ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                    imgDelete.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["img"].ToString();
                    imgDelete.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["title"].ToString());
                    imgDelete.Attributes.Add("onclick", "return confirm(\"คุณต้องการลบข้อมูลนี้หรือไม่ ?\");");
                    #endregion
                }

            }
        }

        protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.Header))
            {
                #region Create Item Header
                bool bSort = false;
                int i = 0;
                for (i = 0; i < GridView2.Columns.Count; i++)
                {
                    if (ViewState["sort"].Equals(GridView2.Columns[i].SortExpression))
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

        protected void GridView2_Sorting(object sender, GridViewSortEventArgs e)
        {
        }

        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            HiddenField hddloan_detail_approve_id = (HiddenField)GridView2.Rows[e.RowIndex].FindControl("hddloan_detail_approve_id");
            try
            {
                StoreAllData();
                int i = 0;
                foreach (DataRow dr in this.dtLoanApprove.Rows)
                {
                    if (Helper.CInt(dr["loan_detail_approve_id"]) == Helper.CInt(hddloan_detail_approve_id.Value))
                    {
                        dr["row_status"] = "D";
                        break;
                    }
                    ++i;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
            }
            BindGridApprove();
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {
                case "ADD":
                    StoreAllData();
                    DataRow dr = this.dtLoanApprove.NewRow();
                    dr["loan_detail_approve_id"] = ++this.LoanApproveID;
                    dr["approve_name"] = string.Empty;
                    dr["approve_level"] = 0;
                    dr["approve_remark"] = string.Empty;
                    dr["person_code"] = string.Empty;
                    dr["person_thai_name"] = string.Empty;
                    dr["person_manage_code"] = string.Empty;
                    dr["person_manage_name"] = string.Empty;
                    dr["approve_status"] = "P";
                    dr["row_status"] = "N";
                    this.dtLoanApprove.Rows.Add(dr);
                    BindGridApprove();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region GridView3 Event

        private void StoreOpen()
        {
            try
            {
                HiddenField hddopen_loan_id;
                HiddenField hddopen_head_id;
                TextBox txtopen_doc;
                HiddenField hddopen_title;
                HiddenField hddopen_date;
                HiddenField hddopen_amount;

                foreach (GridViewRow gvRow in GridView3.Rows)
                {
                    hddopen_loan_id = (HiddenField)gvRow.FindControl("hddopen_loan_id");
                    hddopen_head_id = (HiddenField)gvRow.FindControl("hddopen_head_id");
                    txtopen_doc = (TextBox)gvRow.FindControl("txtopen_doc");
                    hddopen_title = (HiddenField)gvRow.FindControl("hddopen_title");
                    hddopen_date = (HiddenField)gvRow.FindControl("hddopen_date");
                    hddopen_amount = (HiddenField)gvRow.FindControl("hddopen_amount");
                    foreach (DataRow dr in this.dtOpenLoan.Rows)
                    {
                        if (Helper.CLong(dr["open_loan_id"]) == Helper.CLong(hddopen_loan_id.Value))
                        {
                            dr["open_loan_id"] = hddopen_loan_id.Value;
                            dr["open_head_id"] = hddopen_head_id.Value;
                            dr["open_doc"] = txtopen_doc.Text;
                            dr["open_title"] = hddopen_title.Value;
                            if (hddopen_date.Value.Length > 0)
                                dr["open_date"] = hddopen_date.Value;
                            dr["open_amount"] = hddopen_amount.Value;
                            break;
                        }
                    }
                }
                BindGridOpen();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
            }
        }
        private void BindGridOpen()
        {
            DataView dv = null;
            try
            {
                dv = new DataView(this.dtOpenLoan, "row_status<>'D'", (ViewState["sort3"] + " " + ViewState["direction3"]), DataViewRowState.CurrentRows);
                GridView3.DataSource = dv.ToTable();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
            finally
            {
                bIsGridOpenEmpty = false;
                if (dv.ToTable().Rows.Count == 0)
                {
                    bIsGridOpenEmpty = true;
                    EmptyGridFix(GridView3);
                }
                else
                {
                    GridView3.DataBind();
                }
            }
        }
        private bool SaveOpen()
        {
            bool blnResult = false;
            cefOpen objEefOpen = new cefOpen();
            try
            {
                string strUserName = Session["username"].ToString();
                StoreAllData();
                foreach (DataRow dr in this.dtOpenLoan.Rows)
                {
                    if (Helper.CStr(dr["open_doc"]).Length > 0)
                    {
                        if (Helper.CStr(dr["row_status"]) == "N")
                        {
                            objEefOpen.SP_OPEN_LOAN_INS(
                                Helper.CInt(dr["open_head_id"]),
                                Helper.CLong(ViewState["loan_id"]),
                                strUserName);
                        }
                        else if (Helper.CStr(dr["row_status"]) == "O")
                        {
                            objEefOpen.SP_OPEN_LOAN_UPD(
                               Helper.CLong(dr["open_loan_id"]),
                                Helper.CInt(dr["open_head_id"]),
                                Helper.CLong(ViewState["loan_id"]),
                               strUserName);
                        }
                        else if (Helper.CStr(dr["row_status"]) == "D")
                        {
                            objEefOpen.SP_OPEN_LOAN_DEL(Helper.CLong(dr["open_loan_id"]));
                        }
                    }
                }
                this.dtOpenLoan = null;
                blnResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objEefOpen.Dispose();
            }
            return blnResult;
        }

        protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.Header))
            {
                for (int iCol = 0; iCol < e.Row.Cells.Count; iCol++)
                {
                    e.Row.Cells[iCol].Attributes.Add("class", "table_h");
                    e.Row.Cells[iCol].Wrap = false;
                }

                var imgAdd = (ImageButton)e.Row.FindControl("imgAdd");
                imgAdd.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgGridAdd"].Rows[0]["img"].ToString();
                imgAdd.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgGridAdd"].Rows[0]["title"].ToString());
                ViewState["TotalOpenAmount"] = 0;

            }
            else if (e.Row.RowType.Equals(DataControlRowType.DataRow) || e.Row.RowState.Equals(DataControlRowState.Alternate))
            {
                if (!this.bIsGridOpenEmpty)
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
                    DataRowView dv = (DataRowView)e.Row.DataItem;
                    Label lblNo = (Label)e.Row.FindControl("lblNo");
                    int nNo = (GridView3.PageSize * GridView3.PageIndex) + e.Row.RowIndex + 1;
                    lblNo.Text = nNo.ToString();
                    #region set Image Delete

                    ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                    imgDelete.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["img"].ToString();
                    imgDelete.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["title"].ToString());
                    imgDelete.Attributes.Add("onclick", "return confirm(\"คุณต้องการลบข้อมูลนี้หรือไม่ ?\");");
                    #endregion


                    ImageButton imgPrint = (ImageButton)e.Row.FindControl("imgPrint");

                    imgPrint.ImageUrl = "../../images/controls/print.png";
                    imgPrint.Attributes.Add("title", "พิมพ์");
                    string strScript = "window.open(\"../../App_Control/reportsparameter/open_report_show.aspx?report_code=Rep_open01&open_head_id=" + Helper.CStr(dv["open_head_id"]) + "\", \"_blank\");return false;\n";
                    imgPrint.Attributes.Add("onclick", strScript);
                    imgPrint.Visible = Helper.CStr(dv["row_status"]) == "O";

                    var imgView = (ImageButton)e.Row.FindControl("imgView");
                    imgView.Attributes.Add("onclick", "OpenPopUp('950px','550px','95%','แสดงรายละเอียดการขออนุมัติ','../open/open_control.aspx?mode=view&open_head_id="
                                                                + Helper.CInt(dv["open_head_id"]) + "','2');return false;");
                    imgView.Visible = Helper.CStr(dv["row_status"]) == "O";


                    ViewState["TotalOpenAmount"] = Helper.CDbl(ViewState["TotalOpenAmount"]) +
                                               Helper.CDbl(dv["open_amount"]);
                }

            }
            else if (e.Row.RowType.Equals(DataControlRowType.Footer))
            {
                AwNumeric txttotal_open_amount = (AwNumeric)e.Row.FindControl("txttotal_open_amount");
                txttotal_open_amount.Value = Helper.CDbl(ViewState["TotalOpenAmount"]);
            }
        }

        protected void GridView3_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.Header))
            {
                #region Create Item Header
                bool bSort = false;
                int i = 0;
                for (i = 0; i < GridView3.Columns.Count; i++)
                {
                    if (ViewState["sort3"].Equals(GridView3.Columns[i].SortExpression))
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
                            if (ViewState["direction3"].Equals("ASC"))
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

        protected void GridView3_Sorting(object sender, GridViewSortEventArgs e)
        {
        }

        protected void GridView3_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var hddopen_loan_id = (HiddenField)GridView3.Rows[e.RowIndex].FindControl("hddopen_loan_id");
            try
            {
                StoreAllData();
                int i = 0;
                foreach (DataRow dr in dtOpenLoan.Rows)
                {
                    if (Helper.CLong(dr["open_loan_id"]) == Helper.CLong(hddopen_loan_id.Value))
                    {
                        dr["row_status"] = "D";
                        break;
                    }
                    ++i;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
            BindGridOpen();
        }

        protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {
                case "ADD":
                    StoreAllData();
                    DataRow dr = dtOpenLoan.NewRow();
                    dr["open_loan_id"] = ++OpenLoanID;
                    dr["open_head_id"] = 0;
                    dr["open_doc"] = string.Empty;
                    dr["open_title"] = string.Empty;
                    dr["open_amount"] = 0;
                    dr["row_status"] = "N";
                    dtOpenLoan.Rows.Add(dr);
                    BindGridOpen();
                    break;
            }
        }
        #endregion

        #region GridView4 Event

        private void StoreAttach()
        {
            try
            {
                HiddenField hddloan_attach_id;
                TextBox txtloan_attach_file_name;
                TextBox txtloan_attach_des;
                foreach (GridViewRow gvRow in GridView4.Rows)
                {
                    hddloan_attach_id = (HiddenField)gvRow.FindControl("hddloan_attach_id");
                    txtloan_attach_des = (TextBox)gvRow.FindControl("txtloan_attach_des");
                    txtloan_attach_file_name = (TextBox)gvRow.FindControl("txtloan_attach_file_name");
                    foreach (DataRow dr in this.dtLoanAttach.Rows)
                    {
                        if (Helper.CLong(dr["loan_attach_id"]) == Helper.CLong(hddloan_attach_id.Value))
                        {
                            dr["loan_attach_id"] = hddloan_attach_id.Value;
                            dr["loan_attach_des"] = txtloan_attach_des.Text;
                            dr["loan_attach_file_name"] = txtloan_attach_file_name.Text;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
            }
        }
        private void BindGridAttach()
        {
            DataView dv = null;
            try
            {
                dv = new DataView(this.dtLoanAttach, "row_status<>'D'", (ViewState["sort4"] + " " + ViewState["direction4"]), DataViewRowState.CurrentRows);
                GridView4.DataSource = dv.ToTable();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
            finally
            {
                bIsGridApproveEmpty = false;
                if (dv.ToTable().Rows.Count == 0)
                {
                    bIsGridApproveEmpty = true;
                    EmptyGridFix(GridView4);
                }
                else
                {
                    GridView4.DataBind();
                }
            }
        }

        private bool UploadFile()
        {
            try
            {
                foreach (DataRow dr in this.dtLoanAttach.Rows)
                {
                    if (Helper.CStr(dr["loan_attach_file_name"]).Length > 0)
                    {
                        var strLoanAttachFileName = Helper.CStr(dr["loan_attach_file_name"]);
                        strLoanAttachFileName = Server.MapPath(strLoanAttachFileName);
                        if (strLoanAttachFileName.Contains("\\temp\\attach_") && System.IO.File.Exists(strLoanAttachFileName))
                        {
                            var extention = System.IO.Path.GetExtension(strLoanAttachFileName);
                            if (Helper.CStr(dr["row_status"]) == "N")
                            {
                                string path = Server.MapPath("~/attach_file/" + txtloan_doc.Text);
                                if (!Directory.Exists(path))
                                {
                                    Directory.CreateDirectory(path);
                                }
                                string strRawSaveFile = "~/attach_file/" + txtloan_doc.Text + "/attach_file_" +
                                                  DateTime.Now.ToString("yyyyMMddHHmmssfff") + extention;
                                string strSaveFile = Server.MapPath(strRawSaveFile);
                                System.IO.File.Copy(strLoanAttachFileName, strSaveFile);
                                dr["loan_attach_file_name"] = strRawSaveFile;
                            }
                        }
                        else if (Helper.CStr(dr["row_status"]) == "D")
                        {
                            if (System.IO.File.Exists(strLoanAttachFileName))
                            {
                                System.IO.File.Delete(strLoanAttachFileName);
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }
        private bool SaveAttach()
        {
            bool blnResult = false;
            cefLoan objEefLoan = new cefLoan();
            try
            {
                string strUserName = Session["username"].ToString();
                StoreAllData();
                if (UploadFile())
                {
                    foreach (DataRow dr in this.dtLoanAttach.Rows)
                    {
                        if (Helper.CStr(dr["loan_attach_file_name"]).Length > 0)
                        {
                            if (Helper.CStr(dr["row_status"]) == "N")
                            {
                                objEefLoan.SP_LOAN_ATTACH_INS(
                                    Helper.CInt(ViewState["loan_id"]),
                                    Helper.CStr(dr["loan_attach_des"]),
                                    Helper.CStr(dr["loan_attach_file_name"]),
                                    strUserName);
                            }
                            else if (Helper.CStr(dr["row_status"]) == "O")
                            {
                                objEefLoan.SP_LOAN_ATTACH_UPD(
                                    Helper.CLong(dr["loan_attach_id"]),
                                    Helper.CInt(ViewState["loan_id"]),
                                    Helper.CStr(dr["loan_attach_des"]),
                                    strUserName);
                            }
                            else if (Helper.CStr(dr["row_status"]) == "D")
                            {
                                objEefLoan.SP_LOAN_ATTACH_DEL(Helper.CLong(dr["loan_attach_id"]));
                            }
                        }
                    }
                }
                this.dtLoanAttach = null;
                blnResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objEefLoan.Dispose();
            }
            return blnResult;
        }

        protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.Header))
            {
                for (int iCol = 0; iCol < e.Row.Cells.Count; iCol++)
                {
                    e.Row.Cells[iCol].Attributes.Add("class", "table_h");
                    e.Row.Cells[iCol].Wrap = false;
                }

                var imgAdd = (ImageButton)e.Row.FindControl("imgAdd");
                imgAdd.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgGridAdd"].Rows[0]["img"].ToString();
                imgAdd.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgGridAdd"].Rows[0]["title"].ToString());
            }
            else if (e.Row.RowType.Equals(DataControlRowType.DataRow) || e.Row.RowState.Equals(DataControlRowState.Alternate))
            {
                if (!this.bIsGridAttachEmpty)
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
                    DataRowView dv = (DataRowView)e.Row.DataItem;
                    Label lblNo = (Label)e.Row.FindControl("lblNo");

                    TextBox txtloan_attach_file_name = (TextBox)e.Row.FindControl("txtloan_attach_file_name");
                    ImageButton imgList_attach = (ImageButton)e.Row.FindControl("imgList_attach");
                    ImageButton imgClear_attach = (ImageButton)e.Row.FindControl("imgClear_attach");
                    HyperLink lnkBtnAttach = (HyperLink)e.Row.FindControl("lnkBtnAttach");

                    if (Helper.CStr(dv["row_status"]) == "O")
                    {
                        txtloan_attach_file_name.Visible = false;
                        imgList_attach.Visible = false;
                        imgClear_attach.Visible = false;
                        lnkBtnAttach.Visible = true;
                    }
                    else
                    {
                        txtloan_attach_file_name.Visible = true;
                        imgList_attach.Visible = true;
                        imgClear_attach.Visible = true;
                        lnkBtnAttach.Visible = false;
                    }

                    int nNo = (GridView4.PageSize * GridView4.PageIndex) + e.Row.RowIndex + 1;
                    lblNo.Text = nNo.ToString();
                    #region set Image Delete

                    ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                    imgDelete.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["img"].ToString();
                    imgDelete.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["title"].ToString());
                    imgDelete.Attributes.Add("onclick", "return confirm(\"คุณต้องการลบข้อมูลนี้หรือไม่ ?\");");
                    imgDelete.Visible = base.IsUserDelete;
                    #endregion
                }

            }
        }

        protected void GridView4_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.Header))
            {
                #region Create Item Header
                bool bSort = false;
                int i = 0;
                for (i = 0; i < GridView4.Columns.Count; i++)
                {
                    if (ViewState["sort4"].Equals(GridView4.Columns[i].SortExpression))
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
                            if (ViewState["direction4"].Equals("ASC"))
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

        protected void GridView4_Sorting(object sender, GridViewSortEventArgs e)
        {
        }

        protected void GridView4_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var hddloan_attach_id = (HiddenField)GridView4.Rows[e.RowIndex].FindControl("hddloan_attach_id");
            try
            {
                StoreAllData();
                int i = 0;
                foreach (DataRow dr in dtLoanAttach.Rows)
                {
                    if (Helper.CLong(dr["loan_attach_id"]) == Helper.CLong(hddloan_attach_id.Value))
                    {
                        dr["row_status"] = "D";
                        break;
                    }
                    ++i;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
            BindGridAttach();
        }

        protected void GridView4_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {
                case "ADD":
                    StoreAllData();
                    DataRow dr = dtLoanAttach.NewRow();
                    dr["row_status"] = "N";
                    dr["loan_attach_id"] = ++LoanAttachID;
                    dr["loan_attach_des"] = string.Empty;
                    dr["loan_attach_file_name"] = string.Empty;
                    this.dtLoanAttach.Rows.Add(dr);
                    BindGridAttach();
                    break;
            }
        }
        #endregion

        protected void BtnR1_Click(object sender, EventArgs e)
        {
            setData();
        }

        protected void lbkGetBudgetPlan_Click(object sender, EventArgs e)
        {
            if (cboBudget_type.Items.FindByValue(hddBudget_type.Value) != null)
            {
                cboBudget_type.SelectedIndex = -1;
                cboBudget_type.Items.FindByValue(hddBudget_type.Value).Selected = true;
            }

            InitcboBudget();
            if (cboBudget.Items.FindByValue(hddBudget.Value) != null)
            {
                cboBudget.SelectedIndex = -1;
                cboBudget.Items.FindByValue(hddBudget.Value).Selected = true;
            }

            InitcboProduce();
            if (cboProduce.Items.FindByValue(hddProduce.Value) != null)
            {
                cboProduce.SelectedIndex = -1;
                cboProduce.Items.FindByValue(hddProduce.Value).Selected = true;
            }

            InitcboActivity();
            if (cboActivity.Items.FindByValue(hddActivity.Value) != null)
            {
                cboActivity.SelectedIndex = -1;
                cboActivity.Items.FindByValue(hddActivity.Value).Selected = true;
            }

            InitcboDirector();
            if (cboDirector.Items.FindByValue(hddDirector.Value) != null)
            {
                cboDirector.SelectedIndex = -1;
                cboDirector.Items.FindByValue(hddDirector.Value).Selected = true;
            }

            InitcboUnit();
            if (cboUnit.Items.FindByValue(hddUnit.Value) != null)
            {
                cboUnit.SelectedIndex = -1;
                cboUnit.Items.FindByValue(hddUnit.Value).Selected = true;
            }

            InitcboPlan();
            if (cboPlan.Items.FindByValue(hddPlan.Value) != null)
            {
                cboPlan.SelectedIndex = -1;
                cboPlan.Items.FindByValue(hddPlan.Value).Selected = true;
            }

            InitcboWork();
            if (cboWork.Items.FindByValue(hddWork.Value) != null)
            {
                cboWork.SelectedIndex = -1;
                cboWork.Items.FindByValue(hddWork.Value).Selected = true;
            }

            InitcboFund();
            if (cboFund.Items.FindByValue(hddFund.Value) != null)
            {
                cboFund.SelectedIndex = -1;
                cboFund.Items.FindByValue(hddFund.Value).Selected = true;
            }

            InitcboLot();
            if (cboLot.Items.FindByValue(hddLot.Value) != null)
            {
                cboLot.SelectedIndex = -1;
                cboLot.Items.FindByValue(hddLot.Value).Selected = true;
            }

        }

        protected void imgSaveOnly_Click(object sender, ImageClickEventArgs e)
        {
            if (saveData())
            {
                string strScript1 = "$('#divdes1').text().replace('เพิ่ม','แก้ไข');PopUpListPost('1','1');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "loanPage", strScript1, true);
                MsgBox("บันทึกข้อมูลสมบูรณ์");
                setData();
            }
        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboBudget();
        }

        protected void cboBudget_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboProduce();
        }

        protected void cboProduce_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboActivity();
        }

        protected void cboDirector_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboUnit();
        }

        protected void imgClear_budget_plan_Click(object sender, ImageClickEventArgs e)
        {
            txtbudget_plan_code.Text = string.Empty;
            InitcboYear();

        }

        protected void cboApprove_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList cboApprove = (DropDownList)sender;
            AwNumeric txtapprove_level = (AwNumeric)cboApprove.NamingContainer.FindControl("txtapprove_level");
            var dv = new DataView(this.dtApprove, "approve_code='" + cboApprove.SelectedValue + "'", "", DataViewRowState.CurrentRows);
            DataTable dt = dv.ToTable();
            if (dt.Rows.Count > 0)
            {
                txtapprove_level.Value = dt.Rows[0]["approve_level"].ToString();
            }
        }

        protected void cboLoan_offer_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtloan_offer.Text = cboLoan_offer.SelectedValue;
        }

        protected void cboApproveRemark_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList cboApproveRemark = (DropDownList)sender;
            TextBox txtapprove_remark = (TextBox)cboApproveRemark.NamingContainer.FindControl("txtapprove_remark");
            txtapprove_remark.Text = cboApproveRemark.SelectedValue;
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetPayLabel(RadioButtonList1.SelectedValue);
            txtpay_name.Text = this.PersonFullName;
            if (RadioButtonList1.SelectedValue == "B")
            {
                DataSet ds = new DataSet();
                DataRow dr ;
                cPerson oPerson = new cPerson();
                string strMessage = "";
                string strCriteria = "and [person_code] = '" + txtloan_person.Text + "'  ";
               
                if (oPerson.SP_PERSON_ALL_SEL(strCriteria, ref ds, ref strMessage))
                {
                    dr = ds.Tables[0].Rows[0];
                    txtpay_acc_no.Text = Helper.CStr(dr["bank_no"]);
                    InitcboBank();
                    if (cboPay_bank.Items.FindByValue(dr["bank_name"].ToString()) != null)
                    {
                        cboPay_bank.SelectedIndex = -1;
                        cboPay_bank.Items.FindByValue(dr["bank_name"].ToString()).Selected = true;
                    }

                    InitcboBank_Branch();
                    if (cboPay_bank_branch.Items.FindByValue(dr["branch_name"].ToString()) != null)
                    {
                        cboPay_bank_branch.SelectedIndex = -1;
                        cboPay_bank_branch.Items.FindByValue(dr["branch_name"].ToString()).Selected = true;
                    }
                }               
            }
            else
            {
                txtpay_acc_no.Text = "";
                cboPay_bank.SelectedIndex = 0;
                InitcboBank_Branch();
            }
        }

        private void SetPayLabel(string pay_type)
        {
            if (pay_type == "B")
            {
                lblpay_acc_no.Text = "เลขที่บัญชี";
                lblpay_name.Text = "ชื่อบัญชี";
                lblpay_bank.Text = "ธนาคาร";
                lblpay_bank_branch.Text = "สาขาธนาคาร";
            }
            else
            {
                lblpay_acc_no.Text = "เลขที่เช็ค";
                lblpay_name.Text = "ชื่อผู้จ่ายเช็ค";
                lblpay_bank.Text = "เช็คธนาคาร";
                lblpay_bank_branch.Text = "สาขาธนาคาร";
            }
        }

        protected void cboPay_bank_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboBank_Branch();
        }

    }
}