using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.MobileControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using myDLL;
using System.Collections.Generic;
using Aware.WebControls;
using Label = System.Web.UI.WebControls.Label;
using TextBox = System.Web.UI.WebControls.TextBox;
using System.IO;

namespace myEFrom.App_Control.open
{
    public partial class open_control : PageBase
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
        private long OpenDetailID
        {
            get
            {
                if (ViewState["OpenDetailID"] == null)
                {
                    ViewState["OpenDetailID"] = 1000000;
                }
                return long.Parse(ViewState["OpenDetailID"].ToString());
            }
            set
            {
                ViewState["OpenDetailID"] = value;
            }
        }
        private DataTable dtOpenDetail
        {
            get
            {
                if (ViewState["dtOpenDetail"] == null)
                {
                    cefOpen objEfloan = new cefOpen();
                    _strMessage = string.Empty;
                    _strCriteria = " and open_head_id = " + Helper.CInt(ViewState["open_head_id"]) + " order by open_detail_id";
                    DataTable dtTemp = objEfloan.SP_OPEN_DETAIL_SEL(_strCriteria);
                    dtTemp.Columns.Add("row_status");
                    if (dtTemp.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtTemp.Rows)
                        {
                            dr["row_status"] = "O";
                        }
                    }
                    ViewState["dtOpenDetail"] = dtTemp;
                }
                return (DataTable)ViewState["dtOpenDetail"];
            }
            set
            {
                ViewState["dtOpenDetail"] = value;
            }
        }

        //private DataTable dtOpenDetail
        //{
        //    get
        //    {
        //        if (ViewState["dtOpenDetail"] == null)
        //        {
        //            cefOpen objEfloan = new cefOpen();
        //            _strMessage = string.Empty;
        //            _strCriteria = " and open_head_id = " + Helper.CInt(ViewState["open_head_id"]) + " order by open_detail_id";
        //            DataTable dtTemp = objEfloan.SP_OPEN_DETAIL_SEL(_strCriteria);
        //            dtTemp.Columns.Add("row_status");
        //            if (dtTemp.Rows.Count > 0)
        //            {
        //                foreach (DataRow dr in dtTemp.Rows)
        //                {
        //                    dr["row_status"] = "O";
        //                }
        //                ViewState["dtOpenDetail"] = dtTemp;
        //            }
        //            else
        //            {
        //                DataRow rw;
        //                cefOpenItem objEfloanItem = new cefOpenItem();
        //                _strCriteria = " and open_code = " + Helper.CInt(txtopen_code.Text);
        //                DataTable dt = objEfloanItem.SP_OPEN_ITEM_SEL(_strCriteria);
        //                dt.Columns.Add("row_status");
        //                foreach (DataRow dr in dt.Rows)
        //                {
        //                    rw = dtTemp.NewRow();
        //                    rw["open_detail_id"] = ++this.OpenDetailID;
        //                    rw["material_id"] = Helper.CInt(dr["material_id"]);
        //                    rw["material_name"] = Helper.CStr(dr["material_name"]);
        //                    rw["material_detail"] = string.Empty;
        //                    rw["open_detail_amount"] = 0;
        //                    rw["open_detail_remark"] = string.Empty;
        //                    rw["row_status"] = "N";
        //                    dtTemp.Rows.Add(rw);
        //                }
        //                ViewState["dtOpenDetail"] = dtTemp;
        //            }
        //        }

        //        return (DataTable)ViewState["dtOpenDetail"];
        //    }
        //    set
        //    {
        //        ViewState["dtOpenDetail"] = value;
        //    }
        //}

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
        private long OpenApproveID
        {
            get
            {
                if (ViewState["OpenApproveID"] == null)
                {
                    ViewState["OpenApproveID"] = 1000000;
                }
                return long.Parse(ViewState["OpenApproveID"].ToString());
            }
            set
            {
                ViewState["OpenApproveID"] = value;
            }
        }
        //private DataTable dtOpenApprove
        //{
        //    get
        //    {
        //        if (ViewState["dtOpenApprove"] == null)
        //        {
        //            cefOpen objEfloan = new cefOpen();
        //            _strMessage = string.Empty;
        //            _strCriteria = " and open_head_id = " + Helper.CInt(ViewState["open_head_id"]) +
        //                           " order by approve_level";
        //            DataTable dtTemp = objEfloan.SP_OPEN_DETAIL_APPROVE_SEL(_strCriteria);
        //            dtTemp.Columns.Add("row_status");
        //            if (dtTemp.Rows.Count > 0)
        //            {
        //                foreach (DataRow dr in dtTemp.Rows)
        //                {
        //                    dr["row_status"] = "O";
        //                }
        //                ViewState["dtOpenApprove"] = dtTemp;
        //            }
        //            else
        //            {
        //                DataRow rw;
        //                cefApproveBudget objApproveBudget = new cefApproveBudget();

        //                if (Helper.CInt(txtopen_code.Text) == 0)
        //                {
        //                    DataTable dtBudget = objApproveBudget.SP_APPROVE_BUDGET_SEL(" and ef_budget_type_approve in ('" + cboBudget_type.SelectedValue + "','H')");
        //                    foreach (DataRow dr in dtBudget.Rows)
        //                    {
        //                        rw = dtTemp.NewRow();
        //                        rw["open_detail_approve_id"] = ++this.OpenApproveID;
        //                        rw["approve_code"] = Helper.CInt(dr["approve_code"]);
        //                        rw["approve_name"] = Helper.CStr(dr["approve_name"]);
        //                        rw["approve_level"] = Helper.CInt(dr["approve_level"]);
        //                        rw["approve_remark"] = string.Empty;
        //                        rw["person_code"] = Helper.CStr(dr["ef_person_code_approve"]);
        //                        rw["person_thai_name"] = Helper.CStr(dr["title_name"]) + Helper.CStr(dr["person_thai_name"]) + " " + Helper.CStr(dr["person_thai_surname"]);
        //                        rw["person_manage_code"] = Helper.CStr(dr["ef_approve_position"]);
        //                        rw["person_manage_name"] = Helper.CStr(dr["ef_approve_position_name"]);
        //                        rw["row_status"] = "N";
        //                        dtTemp.Rows.Add(rw);
        //                    }
        //                }
        //                else
        //                {

        //                    cefOpenApprove objEfloanApprove = new cefOpenApprove();
        //                    string strbudget_type = cboBudget_type.SelectedValue;
        //                    strbudget_type = strbudget_type == "X" ? "R" : strbudget_type;

        //                    _strCriteria = " and open_code = " + Helper.CInt(txtopen_code.Text) + " and budget_type = '" + strbudget_type + "' ";
        //                    DataTable dt = objEfloanApprove.SP_OPEN_APPROVE_SEL(_strCriteria);
        //                    foreach (DataRow dr in dt.Rows)
        //                    {
        //                        rw = dtTemp.NewRow();
        //                        rw["open_detail_approve_id"] = ++this.OpenApproveID;
        //                        rw["approve_code"] = Helper.CInt(dr["approve_code"]);
        //                        rw["approve_name"] = Helper.CStr(dr["approve_name"]);
        //                        rw["approve_level"] = Helper.CInt(dr["approve_level"]);
        //                        rw["approve_remark"] = string.Empty;
        //                        rw["person_code"] = Helper.CStr(dr["person_approve_code"]);
        //                        rw["person_thai_name"] = Helper.CStr(dr["title_name"]) + Helper.CStr(dr["person_thai_name"]) + " " + Helper.CStr(dr["person_thai_surname"]);
        //                        rw["person_manage_code"] = Helper.CStr(dr["person_manage_code"]);
        //                        rw["person_manage_name"] = Helper.CStr(dr["person_manage_name"]);
        //                        rw["row_status"] = "N";
        //                        dtTemp.Rows.Add(rw);
        //                    }
        //                }
        //                ViewState["dtOpenApprove"] = dtTemp;
        //            }
        //        }
        //        return (DataTable)ViewState["dtOpenApprove"];
        //    }
        //    set
        //    {
        //        ViewState["dtOpenApprove"] = value;
        //        ViewState["dtOpenApproveChk"] = null;
        //    }
        //}
        private DataTable dtOpenApprove
        {
            get
            {
                if (ViewState["dtOpenApprove"] == null)
                {
                    cefOpen objEfloan = new cefOpen();
                    _strMessage = string.Empty;
                    _strCriteria = " and open_head_id = " + Helper.CInt(ViewState["open_head_id"]) + " order by approve_level";
                    DataTable dtTemp = objEfloan.SP_OPEN_DETAIL_APPROVE_SEL(_strCriteria);
                    dtTemp.Columns.Add("row_status");
                    if (dtTemp.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtTemp.Rows)
                        {
                            dr["row_status"] = "O";
                        }
                    }
                    ViewState["dtOpenApprove"] = dtTemp;
                }
                return (DataTable)ViewState["dtOpenApprove"];
            }
            set
            {
                ViewState["dtOpenApprove"] = value;
                ViewState["dtOpenApproveChk"] = null;
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

        private bool bIsGridLoanEmpty
        {
            get
            {
                if (ViewState["bIsGridLoanEmpty"] == null)
                {
                    ViewState["bIsGridLoanEmpty"] = false;
                }
                return (bool)ViewState["bIsGridLoanEmpty"];
            }
            set
            {
                ViewState["bIsGridLoanEmpty"] = value;
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
                    var objEfloan = new cefOpen();
                    _strMessage = string.Empty;
                    _strCriteria = " and open_head_id = " + Helper.CInt(ViewState["open_head_id"]);
                    var dtTemp = objEfloan.SP_OPEN_LOAN_SEL(_strCriteria);
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
        private long OpenAttachID
        {
            get
            {
                if (ViewState["OpenAttachID"] == null)
                {
                    ViewState["OpenAttachID"] = 1000000;
                }
                return long.Parse(ViewState["OpenAttachID"].ToString());
            }
            set
            {
                ViewState["OpenAttachID"] = value;
            }
        }
        private DataTable dtOpenAttach
        {
            get
            {
                if (ViewState["dtOpenAttach"] == null)
                {
                    var objEfloan = new cefOpen();
                    _strMessage = string.Empty;
                    _strCriteria = " and open_head_id = " + Helper.CInt(ViewState["open_head_id"]);
                    var dtTemp = objEfloan.SP_OPEN_ATTACH_SEL(_strCriteria);
                    dtTemp.Columns.Add("row_status");
                    if (dtTemp.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtTemp.Rows)
                        {
                            dr["row_status"] = "O";
                        }
                    }
                    ViewState["dtOpenAttach"] = dtTemp;
                }
                return (DataTable)ViewState["dtOpenAttach"];
            }
            set
            {
                ViewState["dtOpenAttach"] = value;
            }
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "RegisterScript", "RegisterScript();createDate('" +
                        txtopen_date.ClientID + "','" + DateTime.Now.Date.ToString("dd/MM/yyyy") + "');", true);

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "RegisterScriptTinymce", "tinymce.init({selector: '#" + txtopen_desc.ClientID + "' , toolbar: false ,  menubar: false , plugins: ['preview','code']  });", true);
            //ScriptManager.RegisterOnSubmitStatement(this, this.GetType(), "", "tinyMCE.triggerSave();");
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["mode"] != null && Request.QueryString["mode"].ToLower().Equals("view"))
            {
                base.PermissionURL = "~/App_Control/open/open_approve_list.aspx";
            }
            else
            {
                base.PermissionURL = "~/App_Control/open/open_list.aspx";
            }

        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            lblError.Text = "";
            if (!ScriptManager.GetCurrent(this).IsInAsyncPostBack)
            {
                ScriptManager.RegisterOnSubmitStatement(this, this.GetType(), "BeforePostback", "BeforePostback()");
            }
      
            if (!IsPostBack)
            {
                imgSaveOnly.Attributes.Add("onMouseOver", "src='../../images/button/save_add2.png'");
                imgSaveOnly.Attributes.Add("onMouseOut", "src='../../images/button/save_add.png'");

                imgPrint.Attributes.Add("onMouseOver", "src='../../images/button/print2.png'");
                imgPrint.Attributes.Add("onMouseOut", "src='../../images/button/print.png'");

                ViewState["sort"] = "open_detail_id";
                ViewState["direction"] = "ASC";

                ViewState["sort2"] = "approve_level";
                ViewState["direction2"] = "ASC";

                ViewState["sort3"] = "open_loan_id";
                ViewState["direction3"] = "ASC";

                ViewState["sort4"] = "open_attach_id";
                ViewState["direction4"] = "ASC";


                //if (Request.Browser.Type.ToUpper().Contains("IE")) // replace with your check
                //{
                //    txtopen_title.Rows = 2;
                //    txtopen_desc.Rows = 4;
                //    txtopen_remark.Rows = 1;
                //}
                //else if (Request.Browser.Type.ToUpper().Contains("CHROME")) // replace with your check
                //{
                //    txtopen_desc.Rows = 2;
                //}

                #region set QueryString

                if (Request.QueryString["open_head_id"] != null)
                {
                    ViewState["open_head_id"] = Request.QueryString["open_head_id"].ToString();
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


                #endregion

                #region Set Image


                imgList_open.Attributes.Add("onclick", "OpenPopUp('800px','400px','93%','ค้นหาข้อมูลรายการขออนุมัติเบิกจ่าย' ,'../lov/open_lov.aspx?" +
                                                    "year='+document.forms[0]." + cboYear.UniqueID + ".options[document.forms[0]." + cboYear.UniqueID + ".selectedIndex].value+" +
                                                    "'&item_code='+document.forms[0]." + txtopen_code.UniqueID + ".value+" +
                                                    "'&item_name='+document.forms[0]." + txtopen_title.UniqueID + ".value+" +
                                                    "'&ctrl1=" + txtopen_code.ClientID + "&ctrl2=" + txtopen_title.ClientID + "&lbkGetloan=" + lbkGetOpen.UniqueID + "&show=2&from=open_control', '2');return false;");

                //imgClear_loan.Attributes.Add("onclick", "document.forms[0]." + txtopen_code.UniqueID + ".value='';" +
                //                        "document.forms[0]." + txtloan_title.UniqueID + ".value='';return false;");

                imgList_person.Attributes.Add("onclick", "OpenPopUp('900px','500px','94%','ค้นหาข้อมูลบุคคลากร' ,'../lov/person_lov.aspx?" +
                     "from=open_control&person_code='+getElementById('" + txtopen_person.ClientID + "').value+'" +
                     "&person_name='+getElementById('" + txtopen_person_name.ClientID + "').value+'" +
                    "&ctrl1=" + txtopen_person.ClientID + "&ctrl2=" + txtopen_person_name.ClientID + "&show=2', '2');return false;");

                imgClear_person.Attributes.Add("onclick", "document.getElementById('" + txtopen_person.ClientID + "').value='';document.getElementById('" + txtopen_person_name.ClientID + "').value=''; return false;");

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
                                                                "&lbkGetOpen=" + lbkGetOpen.UniqueID +
                                                                "&show=2', '2');return false;");

                //imgClear_budget_plan.Attributes.Add("onclick",
                //                                                "document.forms[0]." + strPrefixCtr_main + "TabPanel1$txtbudget_plan_code.value='';" +
                //                                                "document.forms[0]." + strPrefixCtr_main + "TabPanel1$txtbudget_name.value='';" +
                //                                                "document.forms[0]." + strPrefixCtr_main + "TabPanel1$txtproduce_name.value='';" +
                //                                                "document.forms[0]." + strPrefixCtr_main + "TabPanel1$txtactivity_name.value='';" +
                //                                                "document.forms[0]." + strPrefixCtr_main + "TabPanel1$txtplan_name.value='';" +
                //                                                "document.forms[0]." + strPrefixCtr_main + "TabPanel1$txtwork_name.value='';" +
                //                                                "document.forms[0]." + strPrefixCtr_main + "TabPanel1$txtfund_name.value='';" +
                //                                                "document.forms[0]." + strPrefixCtr_main + "TabPanel1$txtdirector_name.value='';" +
                //                                                "document.forms[0]." + strPrefixCtr_main + "TabPanel1$txtunit_name.value='';" +
                //                                                "return false;");

                #endregion

                InitcboBudgetType();
                InitcboDoctype();
                InitcboYear();
                InitcboOpen_to("");

                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    ViewState["page"] = Request.QueryString["page"];
                    TabContainer1.Tabs[1].Visible = false;
                    TabContainer1.Tabs[2].Visible = false;
                    TabContainer1.Tabs[3].Visible = false;
                    TabContainer1.Tabs[4].Visible = false;
                    TabContainer1.Tabs[5].Visible = false;
                    txtopen_date.Text = cCommon.CheckDate(DateTime.Now.ToShortDateString());
                    txtopen_path.Text = this.DirectorName;
                    txtopen_person.Text = this.PersonCode;
                    txtopen_person_name.Text = this.PersonFullName;
                }
                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    setData();
                }
                else if (ViewState["mode"].ToString().ToLower().Equals("copy"))
                {
                    setData();
                    ViewState["open_head_id"] = null;
                    txtopen_doc.Text = "";

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

        private void InitcboOpen_to(string strText)
        {
            List<string> listopen_to = new List<string>();
            listopen_to.AddRange(strText.Split(','));
            cboOpen_to.Items.Clear();
            cboOpen_to.Items.Add(new ListItem("--เลือกข้อมูล--", ""));
            foreach (string str in listopen_to)
            {
                if (str.Length > 0)
                {
                    cboOpen_to.Items.Add(new ListItem(str, str));
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

        private bool saveData()
        {
            bool blnResult = false;
            string strMessage = string.Empty;
            int intopen_head_id = 0;
            string stropen_doc = string.Empty;
            string strUserName = string.Empty;
            string strloanTo = string.Empty;

            cefOpen objEfloan = new cefOpen();
            try
            {
                #region set Data
                stropen_doc = txtopen_doc.Text;
                strUserName = Session["username"].ToString();
                strloanTo = txtopen_to.Text.Trim().Length > 0 ? txtopen_to.Text.Trim() : cboOpen_to.SelectedValue;
                #endregion
                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    #region insert
                    if (objEfloan.SP_OPEN_HEAD_INS(ref intopen_head_id, ref stropen_doc, cboYear.SelectedValue, txtopen_date.Text,
                        txtopen_path.Text, txtopen_no.Text, Helper.CInt(txtopen_code.Text), strloanTo, txtopen_title.Text,
                        txtopen_command_desc.Text, txtopen_desc.Text, cboBudget_type.SelectedValue, txtbudget_type_text.Text, txtbudget_plan_code.Text, cboDirector.SelectedValue,
                       cboUnit.SelectedValue, cboBudget.SelectedValue, cboProduce.SelectedValue, cboActivity.SelectedValue, cboPlan.SelectedValue,
                        cboWork.SelectedValue, cboFund.SelectedValue, cboLot.SelectedValue, txtopen_person.Text, txtopen_tel.Text, txtopen_remark.Text, 0, txtopen_doc.Text, cboDoctype.SelectedValue,txtopen_old_year.Text,  strUserName))
                    {
                        ViewState["open_head_id"] = intopen_head_id;
                        blnResult = true;
                    }
                    #endregion
                }
                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    #region update
                    intopen_head_id = Helper.CInt(ViewState["open_head_id"]);

                    //if (!CheckDupLoan())
                    //{
                    if (objEfloan.SP_OPEN_HEAD_UPD(intopen_head_id, cboYear.SelectedValue, txtopen_date.Text,
                        txtopen_path.Text, txtopen_no.Text, Helper.CInt(txtopen_code.Text), strloanTo,
                        txtopen_title.Text, txtopen_command_desc.Text, txtopen_desc.Text, cboBudget_type.SelectedValue,
                        txtbudget_type_text.Text, txtbudget_plan_code.Text, cboDirector.SelectedValue,
                        cboUnit.SelectedValue, cboBudget.SelectedValue, cboProduce.SelectedValue,
                        cboActivity.SelectedValue, cboPlan.SelectedValue,
                        cboWork.SelectedValue, cboFund.SelectedValue, cboLot.SelectedValue, txtopen_person.Text,
                        txtopen_tel.Text, txtopen_remark.Text, 0, txtopen_doc.Text, cboDoctype.SelectedValue,txtopen_old_year.Text, strUserName))
                    {
                        this.StoreAllData();

                        SaveDetail();
                        SaveApprove();

                        SaveLoan();
                        SaveAttach();

                        objEfloan.SP_OPEN_HEAD_APPROVE_UPD(intopen_head_id, strUserName);
                        objEfloan.SP_OPEN_HEAD_SUM_UPD(intopen_head_id, strUserName);

                        blnResult = true;
                    }
                    //}
                    //else
                    //{
                    //    string strScript = "alert(\"ไม่สามารถแก้ไขข้อมูล เนื่องจากข้อมูล ข้อมูลเลขที่สัญญายืมเงิน  ซ้ำ\");\n";
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "frMainPage", strScript, true);               
                    //}                   
                    #endregion
                }
                else if (ViewState["mode"].ToString().ToLower().Equals("copy"))
                {
                    #region insert
                    if (objEfloan.SP_OPEN_HEAD_INS(ref intopen_head_id, ref stropen_doc, cboYear.SelectedValue, txtopen_date.Text,
                        txtopen_path.Text, txtopen_no.Text, Helper.CInt(txtopen_code.Text), strloanTo, txtopen_title.Text,
                        txtopen_command_desc.Text, txtopen_desc.Text, cboBudget_type.SelectedValue, txtbudget_type_text.Text, txtbudget_plan_code.Text, cboDirector.SelectedValue,
                       cboUnit.SelectedValue, cboBudget.SelectedValue, cboProduce.SelectedValue, cboActivity.SelectedValue, cboPlan.SelectedValue,
                        cboWork.SelectedValue, cboFund.SelectedValue, cboLot.SelectedValue, txtopen_person.Text, txtopen_tel.Text, txtopen_remark.Text, 0, txtopen_doc.Text, cboDoctype.SelectedValue,txtopen_old_year.Text, strUserName))
                    {
                        ViewState["open_head_id"] = intopen_head_id;

                        SaveDetail();
                        SaveApprove();
                        SaveLoan();
                        SaveAttach();

                        objEfloan.SP_OPEN_HEAD_APPROVE_UPD(intopen_head_id, strUserName);
                        objEfloan.SP_OPEN_HEAD_SUM_UPD(intopen_head_id, strUserName);

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
            string strScript = "windowloanMaximize(\"../../App_Control/reportsparameter/open_report_show.aspx?open_head_id=" + hddopen_head_id.Value + "\", \"_blank\");\n";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "loanPage", strScript, true);
        }

        private void setData()
        {
            cefOpen opjEfloan = new cefOpen();
            DataTable dt = new DataTable();
            string strMessage = string.Empty,
                strCriteria = string.Empty,
                strUpdatedBy = string.Empty,
                strUpdatedDate = string.Empty,
                stropen_to_source = string.Empty;
            try
            {
                txtopen_doc.ReadOnly = true;
                txtopen_doc.CssClass = "textboxdis";
                if (ViewState["mode"].ToString() != "copy")
                {
                    ViewState["mode"] = "edit";
                }
                strCriteria = " and open_head_id = '" + ViewState["open_head_id"].ToString() + "' ";
                dt = opjEfloan.SP_OPEN_HEAD_SEL(strCriteria);
                if (dt.Rows.Count > 0)
                {
                    #region get Data
                    hddopen_head_id.Value = dt.Rows[0]["open_head_id"].ToString();
                    txtopen_doc.Text = dt.Rows[0]["open_doc"].ToString();
                    cboYear.SelectedValue = dt.Rows[0]["open_year"].ToString();
                    txtopen_path.Text = dt.Rows[0]["open_path"].ToString();
                    txtopen_no.Text = dt.Rows[0]["open_no"].ToString();
                    txtopen_old_year.Text = dt.Rows[0]["open_old_year"].ToString();

                    InitcboBudgetType();
                    if (cboBudget_type.Items.FindByValue(dt.Rows[0]["budget_type"].ToString()) != null)
                    {
                        cboBudget_type.SelectedIndex = -1;
                        cboBudget_type.Items.FindByValue(dt.Rows[0]["budget_type"].ToString()).Selected = true;
                    }
                    if (cboBudget_type.SelectedValue == "X")
                    {
                        RequiredFieldValidator16.Enabled = true;
                        RequiredFieldValidator9.Enabled = false;
                        RequiredFieldValidator10.Enabled = false;
                        RequiredFieldValidator11.Enabled = false;
                        RequiredFieldValidator12.Enabled = false;
                        RequiredFieldValidator13.Enabled = false;
                        RequiredFieldValidator14.Enabled = false;
                        RequiredFieldValidator15.Enabled = false;
                        RequiredFieldValidator5.Enabled = false;
                        RequiredFieldValidator6.Enabled = false;
                        lblReqActivity.Visible = false;
                        lblReqBudget.Visible = false;
                        lblReqFund.Visible = false;
                        lblReqPlan.Visible = false;
                        lblReqProduce.Visible = false;
                        lblReqWork.Visible = false;
                        lblReqUnit.Visible = false;
                        lblReqDirector.Visible = false;
                        lblReqBudget_type_text.Visible = true;

                    }
                    else
                    {
                        RequiredFieldValidator16.Enabled = false;
                        RequiredFieldValidator9.Enabled = true;
                        RequiredFieldValidator10.Enabled = true;
                        RequiredFieldValidator11.Enabled = true;
                        RequiredFieldValidator12.Enabled = true;
                        RequiredFieldValidator13.Enabled = true;
                        RequiredFieldValidator14.Enabled = true;
                        RequiredFieldValidator15.Enabled = true;
                        RequiredFieldValidator5.Enabled = true;
                        RequiredFieldValidator6.Enabled = true;
                        lblReqActivity.Visible = true;
                        lblReqBudget.Visible = true;
                        lblReqFund.Visible = true;
                        lblReqPlan.Visible = true;
                        lblReqProduce.Visible = true;
                        lblReqWork.Visible = true;
                        lblReqUnit.Visible = true;
                        lblReqDirector.Visible = true;
                        lblReqBudget_type_text.Visible = false;
                    }


                    if (cboDoctype.Items.FindByValue(dt.Rows[0]["ef_doctype_code"].ToString()) != null)
                    {
                        cboDoctype.SelectedIndex = -1;
                        cboDoctype.Items.FindByValue(dt.Rows[0]["ef_doctype_code"].ToString()).Selected = true;
                    }

                    txtopen_date.Text = cCommon.CheckDate(dt.Rows[0]["open_date"].ToString());
                    txtopen_code.Text = Helper.CInt(dt.Rows[0]["open_code"]) > 0 ? dt.Rows[0]["open_code"].ToString() : "";
                    stropen_to_source = dt.Rows[0]["open_to_source"].ToString();
                    InitcboOpen_to(stropen_to_source);
                    if (cboOpen_to.Items.FindByValue(dt.Rows[0]["open_to"].ToString()) != null)
                    {
                        cboOpen_to.SelectedIndex = -1;
                        cboOpen_to.Items.FindByValue(dt.Rows[0]["open_to"].ToString()).Selected = true;
                    }
                    txtopen_to.Text = dt.Rows[0]["open_to"].ToString();

                    txtopen_title.Text = dt.Rows[0]["open_title"].ToString();
                    txtopen_desc.Text = dt.Rows[0]["open_desc"].ToString();
                    txtopen_command_desc.Text = dt.Rows[0]["open_command_desc"].ToString();
                    txtopen_tel.Text = dt.Rows[0]["open_tel"].ToString();
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

                    txtopen_remark.Text = dt.Rows[0]["open_remark"].ToString();
                    txtopen_person.Text = dt.Rows[0]["person_open"].ToString();
                    txtopen_person_name.Text = Helper.CStr(dt.Rows[0]["person_thai_name"]) + " " + Helper.CStr(dt.Rows[0]["person_thai_surname"]);
                    strUpdatedBy = dt.Rows[0]["c_updated_by"].ToString();
                    strUpdatedDate = dt.Rows[0]["d_updated_date"].ToString();
                    #endregion

                    #region set Control
                    if (base.UserGroupCode != "Admin")
                    {
                        GridView2.Columns[6].Visible = false;
                    }
                    else
                    {
                        GridView2.Columns[6].Visible = true;
                    }

                    txtUpdatedBy.Text = strUpdatedBy;
                    txtUpdatedDate.Text = strUpdatedDate;
                    //this.dtOpenDetail = null;
                    BindGridDetail();

                    //this.dtOpenApprove = null;
                    if (string.IsNullOrEmpty(txtopen_code.Text) && string.IsNullOrEmpty(txtopen_title.Text))
                    {
                        foreach (DataRow dr in this.dtOpenApprove.Rows)
                        {
                            dr["row_status"] = "D";
                        }
                        this.GetOpenApprove();
                    }
                    BindGridApprove();

                    //this.dtOpenLoan = null;
                    BindGridLoan();

                    //this.dtOpenAttach = null;
                    BindGridAttach();
                    #endregion

                }
                TabContainer1.Tabs[1].Visible = true;
                TabContainer1.Tabs[2].Visible = true;
                TabContainer1.Tabs[3].Visible = true;
                TabContainer1.Tabs[4].Visible = true;
                TabContainer1.Tabs[5].Visible = true;
                
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "RegisterScript", "tinymce.init({selector: '#" + txtopen_desc.ClientID + "' , toolbar: false ,  menubar: false , plugins: ['preview','code']  });", true);
        
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
            StoreLoan();
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
                HiddenField hddopen_detail_id;
                HiddenField hddmaterial_id;
                TextBox txtmaterial_name;
                TextBox txtmaterial_detail;
                AwNumeric txtopen_detail_amount;
                TextBox txtopen_detail_remark;
                foreach (GridViewRow gvRow in GridView1.Rows)
                {
                    hddopen_detail_id = (HiddenField)gvRow.FindControl("hddopen_detail_id");
                    hddmaterial_id = (HiddenField)gvRow.FindControl("hddmaterial_id");
                    txtmaterial_name = (TextBox)gvRow.FindControl("txtmaterial_name");
                    txtmaterial_detail = (TextBox)gvRow.FindControl("txtmaterial_detail");
                    txtopen_detail_amount = (AwNumeric)gvRow.FindControl("txtopen_detail_amount");
                    txtopen_detail_remark = (TextBox)gvRow.FindControl("txtopen_detail_remark");
                    foreach (DataRow dr in this.dtOpenDetail.Rows)
                    {
                        if (Helper.CLong(dr["open_detail_id"]) == Helper.CLong(hddopen_detail_id.Value))
                        {
                            dr["open_detail_id"] = hddopen_detail_id.Value;
                            dr["material_id"] = hddmaterial_id.Value;
                            dr["material_name"] = txtmaterial_name.Text;
                            dr["material_detail"] = txtmaterial_detail.Text;
                            dr["open_detail_amount"] = txtopen_detail_amount.Value;
                            dr["open_detail_remark"] = txtopen_detail_remark.Text;
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
                if (ViewState["mode"].ToString() == "copy")
                {
                    foreach (DataRow dr in this.dtOpenDetail.Rows)
                    {
                        dr["open_detail_id"] = ++this.OpenDetailID;
                        dr["row_status"] = "N";
                    }
                }
                dv = new DataView(this.dtOpenDetail, "row_status<>'D'", (ViewState["sort"] + " " + ViewState["direction"]), DataViewRowState.CurrentRows);
                GridView1.DataSource = dv.ToTable();
                GridView1.DataBind();
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

            bool blnResult = false;
            cefOpen cefOpen = new cefOpen();
            try
            {
                string strUserName = Session["username"].ToString();
                //StoreAllData();
                foreach (DataRow dr in this.dtOpenDetail.Rows)
                {
                    if (Helper.CStr(dr["row_status"]) == "N")
                    {
                        if (Helper.CStr(dr["material_name"]).Trim().Length > 0)
                        {
                            cefOpen.SP_OPEN_DETAIL_INS(Helper.CInt(ViewState["open_head_id"]),
                                Helper.CInt(dr["material_id"]),
                                Helper.CStr(dr["material_name"]),
                                Helper.CStr(dr["material_detail"]),
                                Helper.CStr(dr["open_detail_remark"]),
                                Helper.CDbl(dr["open_detail_amount"]));
                        }
                    }
                    else if (Helper.CStr(dr["row_status"]) == "O")
                    {
                        if (Helper.CStr(dr["material_name"]).Trim().Length > 0)
                        {
                            cefOpen.SP_OPEN_DETAIL_UPD(
                                Helper.CLong(dr["open_detail_id"]),
                                Helper.CInt(ViewState["open_head_id"]),
                                Helper.CInt(dr["material_id"]),
                                Helper.CStr(dr["material_name"]),
                                Helper.CStr(dr["material_detail"]),
                                Helper.CStr(dr["open_detail_remark"]),
                                Helper.CDbl(dr["open_detail_amount"]));
                        }
                    }
                    else if (Helper.CStr(dr["row_status"]) == "D")
                    {
                        cefOpen.SP_OPEN_DETAIL_DEL(Helper.CInt(dr["open_detail_id"]));
                    }
                }
                this.dtOpenDetail = null;
                blnResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cefOpen.Dispose();
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
                    imgDelete.Visible = IsUserDelete;
                    #endregion

                    ViewState["TotalAmount"] = Helper.CDbl(ViewState["TotalAmount"]) +
                                               Helper.CDbl(dv["open_detail_amount"]);
                }

            }
            else if (e.Row.RowType.Equals(DataControlRowType.Footer))
            {
                AwNumeric txtopen_amount = (AwNumeric)e.Row.FindControl("txtopen_amount");
                txtopen_amount.Value = Helper.CDbl(ViewState["TotalAmount"]);
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
            string strMessage = string.Empty;
            string strScript = string.Empty;
            HiddenField hddopen_detail_id = (HiddenField)GridView1.Rows[e.RowIndex].FindControl("hddopen_detail_id");
            try
            {
                StoreAllData();
                int i = 0;
                foreach (DataRow dr in this.dtOpenDetail.Rows)
                {
                    if (Helper.CLong(dr["open_detail_id"]) == Helper.CLong(hddopen_detail_id.Value))
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
                    DataRow dr = this.dtOpenDetail.NewRow();
                    dr["open_detail_id"] = ++this.OpenDetailID;
                    dr["material_id"] = "0";
                    dr["material_name"] = string.Empty;
                    dr["material_detail"] = string.Empty;
                    dr["open_detail_amount"] = 0;
                    dr["open_detail_remark"] = string.Empty;
                    dr["row_status"] = "N";
                    this.dtOpenDetail.Rows.Add(dr);
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
                HiddenField hddopen_detail_approve_id;
                DropDownList cboApprove, cboApproveStatus;
                AwNumeric txtapprove_level;
                TextBox txtperson_manage_code;
                TextBox txtperson_manage_name;
                TextBox txtapprove_person_code;
                TextBox txtapprove_person_name;
                TextBox txtapprove_remark;
                foreach (GridViewRow gvRow in GridView2.Rows)
                {
                    hddopen_detail_approve_id = (HiddenField)gvRow.FindControl("hddopen_detail_approve_id");
                    cboApprove = (DropDownList)gvRow.FindControl("cboApprove");
                    txtapprove_level = (AwNumeric)gvRow.FindControl("txtapprove_level");
                    txtapprove_person_code = (TextBox)gvRow.FindControl("txtapprove_person_code");
                    txtapprove_person_name = (TextBox)gvRow.FindControl("txtapprove_person_name");
                    txtperson_manage_code = (TextBox)gvRow.FindControl("txtperson_manage_code");
                    txtperson_manage_name = (TextBox)gvRow.FindControl("txtperson_manage_name");
                    txtapprove_remark = (TextBox)gvRow.FindControl("txtapprove_remark");
                    cboApproveStatus = (DropDownList)gvRow.FindControl("cboApproveStatus");
                    foreach (DataRow dr in this.dtOpenApprove.Rows)
                    {
                        if (Helper.CLong(dr["open_detail_approve_id"]) == Helper.CLong(hddopen_detail_approve_id.Value))
                        {
                            dr["approve_code"] = Helper.CInt(cboApprove.SelectedValue);
                            dr["approve_name"] = Helper.CStr(cboApprove.SelectedItem.Text);
                            dr["approve_level"] = Helper.CInt(txtapprove_level.Value);
                            dr["approve_remark"] = Helper.CStr(txtapprove_remark.Text);
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
                if (ViewState["mode"].ToString() == "copy")
                {
                    foreach (DataRow dr in this.dtOpenApprove.Rows)
                    {
                        dr["open_detail_approve_id"] = ++this.OpenApproveID;
                        dr["row_status"] = "N";
                        dr["approve_status"] = "p";
                    }
                }
                dv = new DataView(this.dtOpenApprove, "row_status<>'D'", (ViewState["sort2"] + " " + ViewState["direction2"]), DataViewRowState.CurrentRows);
                GridView2.DataSource = dv.ToTable();
                GridView2.DataBind();
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
            cefOpen cefOpen = new cefOpen();
            try
            {
                string strUserName = Session["username"].ToString();
                //StoreAllData();
                foreach (DataRow dr in this.dtOpenApprove.Rows)
                {
                    if (Helper.CStr(dr["person_code"]).Trim().Length > 0)
                    {
                        if (Helper.CStr(dr["row_status"]) == "N")
                        {
                            cefOpen.SP_OPEN_DETAIL_APPROVE_INS(
                                Helper.CInt(ViewState["open_head_id"]),
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
                            cefOpen.SP_OPEN_DETAIL_APPROVE_UPD(
                                Helper.CInt(dr["open_detail_approve_id"]),
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
                            cefOpen.SP_OPEN_DETAIL_APPROVE_DEL(Helper.CInt(dr["open_detail_approve_id"]));
                        }
                    }
                }
                this.dtOpenApprove = null;
                blnResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cefOpen.Dispose();
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
                    this.InitcboApprove(cboApprove);
                    if (cboApprove.Items.FindByValue(strApprove_code) != null)
                    {
                        cboApprove.SelectedIndex = -1;
                        cboApprove.Items.FindByValue(strApprove_code).Selected = true;
                    }
                    var strApprove_status = Helper.CStr(dv["approve_status"]);
                    if (cboApproveStatus.Items.FindByValue(strApprove_status) != null)
                    {
                        cboApproveStatus.SelectedIndex = -1;
                        cboApproveStatus.Items.FindByValue(strApprove_status).Selected = true;
                    }


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
            string strMessage = string.Empty;
            string strScript = string.Empty;
            HiddenField hddopen_detail_approve_id = (HiddenField)GridView2.Rows[e.RowIndex].FindControl("hddopen_detail_approve_id");
            try
            {
                StoreAllData();
                int i = 0;
                foreach (DataRow dr in this.dtOpenApprove.Rows)
                {
                    if (Helper.CInt(dr["open_detail_approve_id"]) == Helper.CLong(hddopen_detail_approve_id.Value))
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
                    DataRow dr = this.dtOpenApprove.NewRow();
                    dr["open_detail_approve_id"] = ++this.OpenApproveID;
                    dr["approve_name"] = string.Empty;
                    dr["approve_level"] = 0;
                    dr["approve_remark"] = string.Empty;
                    dr["person_code"] = string.Empty;
                    dr["person_thai_name"] = string.Empty;
                    dr["person_manage_code"] = string.Empty;
                    dr["person_manage_name"] = string.Empty;
                    dr["approve_status"] = "P";
                    dr["row_status"] = "N";
                    this.dtOpenApprove.Rows.Add(dr);
                    BindGridApprove();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region GridView3 Event

        private void StoreLoan()
        {
            try
            {
                HiddenField hddopen_loan_id;
                HiddenField hddloan_id;
                TextBox txtloan_doc;
                HiddenField hddloan_reason;
                HiddenField hddloan_date;
                HiddenField hddloan_req;

                foreach (GridViewRow gvRow in GridView3.Rows)
                {
                    hddopen_loan_id = (HiddenField)gvRow.FindControl("hddopen_loan_id");
                    hddloan_id = (HiddenField)gvRow.FindControl("hddloan_id");
                    txtloan_doc = (TextBox)gvRow.FindControl("txtloan_doc");
                    hddloan_reason = (HiddenField)gvRow.FindControl("hddloan_reason");
                    hddloan_date = (HiddenField)gvRow.FindControl("hddloan_date");
                    hddloan_req = (HiddenField)gvRow.FindControl("hddloan_req");
                    foreach (DataRow dr in this.dtOpenLoan.Rows)
                    {
                        if (Helper.CLong(dr["open_loan_id"]) == Helper.CLong(hddopen_loan_id.Value))
                        {
                            dr["open_loan_id"] = hddopen_loan_id.Value;
                            dr["loan_id"] = hddloan_id.Value;
                            dr["loan_doc"] = txtloan_doc.Text;
                            dr["loan_reason"] = hddloan_reason.Value;
                            if (hddloan_date.Value.Length > 0)
                                dr["loan_date"] = hddloan_date.Value;
                            dr["loan_req"] = Helper.CDbl(hddloan_req.Value);
                            break;
                        }
                    }
                }
                BindGridLoan();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
            }
        }
        private void BindGridLoan()
        {
            DataView dv = null;
            try
            {
                if (ViewState["mode"].ToString() == "copy")
                {
                    this.dtOpenLoan = null;
                }
                dv = new DataView(this.dtOpenLoan, "row_status<>'D'", (ViewState["sort3"] + " " + ViewState["direction3"]), DataViewRowState.CurrentRows);
                GridView3.DataSource = dv.ToTable();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
            finally
            {
                bIsGridLoanEmpty = false;
                if (dv.ToTable().Rows.Count == 0)
                {
                    bIsGridLoanEmpty = true;
                    EmptyGridFix(GridView3);
                }
                else
                {
                    GridView3.DataBind();
                }
            }
        }
        private bool SaveLoan()
        {
            bool blnResult = false;
            cefOpen objEefOpen = new cefOpen();
            try
            {
                string strUserName = Session["username"].ToString();
                //StoreAllData();
                foreach (DataRow dr in this.dtOpenLoan.Rows)
                {
                    if (Helper.CStr(dr["loan_doc"]).Length > 0)
                    {
                        if (Helper.CStr(dr["row_status"]) == "N")
                        {
                            objEefOpen.SP_OPEN_LOAN_INS(
                                Helper.CInt(ViewState["open_head_id"]),
                                Helper.CLong(dr["loan_id"]),
                                strUserName);
                        }
                        else if (Helper.CStr(dr["row_status"]) == "O")
                        {
                            objEefOpen.SP_OPEN_LOAN_UPD(
                               Helper.CLong(dr["open_loan_id"]),
                                Helper.CInt(ViewState["open_head_id"]),
                                Helper.CLong(dr["loan_id"]),
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
                ViewState["TotalLoanAmount"] = 0;

            }
            else if (e.Row.RowType.Equals(DataControlRowType.DataRow) || e.Row.RowState.Equals(DataControlRowState.Alternate))
            {
                if (!this.bIsGridLoanEmpty)
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
                    imgDelete.Visible = base.IsUserDelete;
                    #endregion

                    var imgPrint = (ImageButton)e.Row.FindControl("imgPrint");
                    imgPrint.Attributes.Add("onclick", "OpenPopUp('550px','280px','92%','เลือกรายงานที่ต้องการพิมพ์','../loan/loan_print.aspx?loan_id=" + Helper.CInt(dv["loan_id"]) + "','2');return false;");
                    imgPrint.Visible = Helper.CStr(dv["row_status"]) == "O";


                    var imgView = (ImageButton)e.Row.FindControl("imgView");
                    imgView.Attributes.Add("onclick", "OpenPopUp('950px','550px','95%','แสดงรายละเอียดสัญายืมเงิน','../loan/loan_control.aspx?mode=view&loan_id="
                                                                + Helper.CInt(dv["loan_id"]) + "','2');return false;");
                    imgView.Visible = Helper.CStr(dv["row_status"]) == "O";


                    ViewState["TotalLoanAmount"] = Helper.CDbl(ViewState["TotalLoanAmount"]) +
                                               Helper.CDbl(dv["loan_req"]);
                }

            }
            else if (e.Row.RowType.Equals(DataControlRowType.Footer))
            {
                AwNumeric txttotal_loan_req = (AwNumeric)e.Row.FindControl("txttotal_loan_req");
                txttotal_loan_req.Value = Helper.CDbl(ViewState["TotalLoanAmount"]);
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
            BindGridLoan();
        }

        protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {
                case "ADD":
                    StoreAllData();
                    DataRow dr = dtOpenLoan.NewRow();
                    dr["row_status"] = "N";
                    dr["open_loan_id"] = ++OpenLoanID;
                    dr["loan_id"] = 0;
                    dr["loan_doc"] = string.Empty;
                    dr["loan_reason"] = string.Empty;
                    //dr["loan_date"] = DBNull.Value;
                    dr["loan_req"] = 0;
                    this.dtOpenLoan.Rows.Add(dr);
                    BindGridLoan();
                    break;
            }
        }
        #endregion

        #region GridView4 Event

        private void StoreAttach()
        {
            try
            {
                HiddenField hddopen_attach_id;
                TextBox txtopen_attach_file_name;
                TextBox txtopen_attach_des;
                foreach (GridViewRow gvRow in GridView4.Rows)
                {
                    hddopen_attach_id = (HiddenField)gvRow.FindControl("hddopen_attach_id");
                    txtopen_attach_des = (TextBox)gvRow.FindControl("txtopen_attach_des");
                    txtopen_attach_file_name = (TextBox)gvRow.FindControl("txtopen_attach_file_name");
                    foreach (DataRow dr in this.dtOpenAttach.Rows)
                    {
                        if (Helper.CLong(dr["open_attach_id"]) == Helper.CLong(hddopen_attach_id.Value))
                        {
                            dr["open_attach_id"] = hddopen_attach_id.Value;
                            dr["open_attach_des"] = txtopen_attach_des.Text;
                            dr["open_attach_file_name"] = txtopen_attach_file_name.Text;
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
                if (ViewState["mode"].ToString() == "copy")
                {
                    foreach (DataRow dr in this.dtOpenAttach.Rows)
                    {
                        dr["open_attach_id"] = ++this.OpenAttachID;
                        dr["row_status"] = "N";
                    }
                }
                dv = new DataView(this.dtOpenAttach, "row_status<>'D'", (ViewState["sort4"] + " " + ViewState["direction4"]), DataViewRowState.CurrentRows);
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
                foreach (DataRow dr in this.dtOpenAttach.Rows)
                {
                    if (Helper.CStr(dr["open_attach_file_name"]).Length > 0)
                    {
                        var strOpenAttachFileName = Helper.CStr(dr["open_attach_file_name"]);
                        strOpenAttachFileName = Server.MapPath(strOpenAttachFileName);
                        if (strOpenAttachFileName.Contains("\\temp\\attach_") && System.IO.File.Exists(strOpenAttachFileName))
                        {
                            var extention = System.IO.Path.GetExtension(strOpenAttachFileName);
                            if (Helper.CStr(dr["row_status"]) == "N")
                            {
                                string path = Server.MapPath("~/attach_file/" + txtopen_doc.Text);
                                if (!Directory.Exists(path))
                                {
                                    Directory.CreateDirectory(path);
                                }
                                string strRawSaveFile = "~/attach_file/" + txtopen_doc.Text + "/attach_file_" +
                                                  DateTime.Now.ToString("yyyyMMddHHmmssfff") + extention;
                                string strSaveFile = Server.MapPath(strRawSaveFile);
                                System.IO.File.Copy(strOpenAttachFileName, strSaveFile);
                                dr["open_attach_file_name"] = strRawSaveFile;
                            }
                        }
                        else if (Helper.CStr(dr["row_status"]) == "D")
                        {
                            if (System.IO.File.Exists(strOpenAttachFileName))
                            {
                                System.IO.File.Delete(strOpenAttachFileName);
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
            cefOpen objEefOpen = new cefOpen();
            try
            {
                string strUserName = Session["username"].ToString();
                //StoreAllData();
                if (UploadFile())
                {
                    foreach (DataRow dr in this.dtOpenAttach.Rows)
                    {
                        if (Helper.CStr(dr["open_attach_file_name"]).Length > 0)
                        {
                            if (Helper.CStr(dr["row_status"]) == "N")
                            {
                                objEefOpen.SP_OPEN_ATTACH_INS(
                                    Helper.CInt(ViewState["open_head_id"]),
                                    Helper.CStr(dr["open_attach_des"]),
                                    Helper.CStr(dr["open_attach_file_name"]),
                                    strUserName);
                            }
                            else if (Helper.CStr(dr["row_status"]) == "O")
                            {
                                objEefOpen.SP_OPEN_ATTACH_UPD(
                                    Helper.CLong(dr["open_attach_id"]),
                                    Helper.CInt(ViewState["open_head_id"]),
                                    Helper.CStr(dr["open_attach_des"]),
                                    strUserName);
                            }
                            else if (Helper.CStr(dr["row_status"]) == "D")
                            {
                                objEefOpen.SP_OPEN_ATTACH_DEL(Helper.CLong(dr["open_attach_id"]));
                            }
                        }
                    }
                }
                this.dtOpenAttach = null;
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

                    TextBox txtopen_attach_file_name = (TextBox)e.Row.FindControl("txtopen_attach_file_name");
                    ImageButton imgList_attach = (ImageButton)e.Row.FindControl("imgList_attach");
                    ImageButton imgClear_attach = (ImageButton)e.Row.FindControl("imgClear_attach");
                    HyperLink lnkBtnAttach = (HyperLink)e.Row.FindControl("lnkBtnAttach");

                    if (Helper.CStr(dv["row_status"]) == "O")
                    {
                        txtopen_attach_file_name.Visible = false;
                        imgList_attach.Visible = false;
                        imgClear_attach.Visible = false;
                        lnkBtnAttach.Visible = true;
                    }
                    else
                    {
                        txtopen_attach_file_name.Visible = true;
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
            var hddopen_attach_id = (HiddenField)GridView4.Rows[e.RowIndex].FindControl("hddopen_attach_id");
            try
            {
                StoreAllData();
                int i = 0;
                foreach (DataRow dr in dtOpenAttach.Rows)
                {
                    if (Helper.CLong(dr["open_attach_id"]) == Helper.CLong(hddopen_attach_id.Value))
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
                    DataRow dr = dtOpenAttach.NewRow();
                    dr["row_status"] = "N";
                    dr["open_attach_id"] = OpenAttachID++;
                    dr["open_attach_des"] = string.Empty;
                    dr["open_attach_file_name"] = string.Empty;
                    this.dtOpenAttach.Rows.Add(dr);
                    BindGridAttach();
                    break;
            }
        }
        #endregion

        protected void BtnR1_Click(object sender, EventArgs e)
        {
            setData();
        }

        protected void lbkGetOpen_Click(object sender, EventArgs e)
        {
            cefOpen objEfloan = new cefOpen();
            DataTable dt;
            string strMessage = string.Empty, strCriteria = string.Empty,
                    stropen_report_code, stropen_to;
            try
            {
                strCriteria = " and open_code = '" + txtopen_code.Text + "' ";
                dt = objEfloan.SP_OPEN_SEL(strCriteria);
                if (dt.Rows.Count > 0)
                {
                    txtopen_code.Text = Helper.CStr(dt.Rows[0]["open_code"]);
                    stropen_to = Helper.CStr(dt.Rows[0]["open_to"]);
                    txtopen_title.Text = Helper.CStr(dt.Rows[0]["open_title"]);
                    txtopen_command_desc.Text = dt.Rows[0]["open_command_desc"].ToString();

                    txtopen_desc.Text = objEfloan.GetOpenDesc(Helper.CStr(dt.Rows[0]["open_desc"]), txtopen_person.Text);


                    stropen_report_code = Helper.CStr(dt.Rows[0]["open_report_code"]);
                    txtopen_remark.Text = Helper.CStr(dt.Rows[0]["open_remark"]);
                    InitcboOpen_to(stropen_to);

                    //this.dtOpenDetail = null;
                    foreach (DataRow dr in this.dtOpenDetail.Rows)
                    {
                        dr["row_status"] = "D";
                    }
                    this.GetOpenDetail();
                    BindGridDetail();

                    foreach (DataRow dr in this.dtOpenApprove.Rows)
                    {
                        dr["row_status"] = "D";
                    }
                    this.GetOpenApprove();
                    BindGridApprove();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
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

        protected void imgClear_item_Click(object sender, ImageClickEventArgs e)
        {
            txtopen_code.Text = string.Empty;
            cboOpen_to.Items.Clear();
            txtopen_title.Text = string.Empty;
            txtopen_command_desc.Text = string.Empty;
            txtopen_desc.Text = string.Empty;
            //cboLot.SelectedIndex = 0;
            InitcboOpen_to("");
            txtopen_to.Text = string.Empty;
            txtopen_remark.Text = string.Empty;
            foreach (DataRow dr in this.dtOpenDetail.Rows)
            {
                dr["row_status"] = "D";
            }
            BindGridDetail();

            foreach (DataRow dr in this.dtOpenApprove.Rows)
            {
                dr["row_status"] = "D";
            }
            BindGridApprove();

        }

        protected void imgSaveOnly_Click(object sender, ImageClickEventArgs e)
        {
            if (saveData())
            {
                string strScript1 = "$('#divdes1').text().replace('เพิ่ม','แก้ไข');PopUpListPost('1','1');";
                if (ViewState["mode"].ToString().ToLower().Equals("copy"))
                {
                    strScript1 = "$('#divdes1').text().replace('คัดลอก','แก้ไข');PopUpListPost('1','1');";
                    ViewState["mode"] = "add";
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "loanPage", strScript1, true);
                MsgBox("บันทึกข้อมูลสมบูรณ์");
                setData();
            }
        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            //InitcboBudget();
            InitcboYear();
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

        protected void cboOpen_to_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtopen_to.Text = cboOpen_to.SelectedValue;
        }

        protected void cboBudget_type_SelectedIndexChanged(object sender, EventArgs e)
        {
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

            if (cboBudget_type.SelectedValue == "X")
            {
                RequiredFieldValidator16.Enabled = true;
                RequiredFieldValidator9.Enabled = false;
                RequiredFieldValidator10.Enabled = false;
                RequiredFieldValidator11.Enabled = false;
                RequiredFieldValidator12.Enabled = false;
                RequiredFieldValidator13.Enabled = false;
                RequiredFieldValidator14.Enabled = false;
                RequiredFieldValidator15.Enabled = false;
                RequiredFieldValidator5.Enabled = false;
                RequiredFieldValidator6.Enabled = false;
                lblReqActivity.Visible = false;
                lblReqBudget.Visible = false;
                lblReqFund.Visible = false;
                lblReqPlan.Visible = false;
                lblReqProduce.Visible = false;
                lblReqWork.Visible = false;
                lblReqUnit.Visible = false;
                lblReqDirector.Visible = false;
                lblReqBudget_type_text.Visible = true;
              
            }
            else
            {
                RequiredFieldValidator16.Enabled = false;
                RequiredFieldValidator9.Enabled = true;
                RequiredFieldValidator10.Enabled = true;
                RequiredFieldValidator11.Enabled = true;
                RequiredFieldValidator12.Enabled = true;
                RequiredFieldValidator13.Enabled = true;
                RequiredFieldValidator14.Enabled = true;
                RequiredFieldValidator15.Enabled = true;
                RequiredFieldValidator5.Enabled = true;
                RequiredFieldValidator6.Enabled = true;
                lblReqActivity.Visible = true;
                lblReqBudget.Visible = true;
                lblReqFund.Visible = true;
                lblReqPlan.Visible = true;
                lblReqProduce.Visible = true;
                lblReqWork.Visible = true;
                lblReqUnit.Visible = true;
                lblReqDirector.Visible = true;
                lblReqBudget_type_text.Visible = false;
            }
            if (string.IsNullOrEmpty(txtopen_code.Text) && string.IsNullOrEmpty(txtopen_title.Text)) 
            {
                foreach (DataRow dr in this.dtOpenApprove.Rows)
                {
                    dr["row_status"] = "D";
                }
                this.GetOpenApprove();
                BindGridApprove();
            }
        }

        private void GetOpenApprove()
        {

            cefOpen objEfloan = new cefOpen();
            _strMessage = string.Empty;

            DataRow rw;
            cefApproveBudget objApproveBudget = new cefApproveBudget();

            cefOpenApprove objEfloanApprove = new cefOpenApprove();
            string strbudget_type = cboBudget_type.SelectedValue;
            strbudget_type = strbudget_type == "X" ? "R" : strbudget_type;

            _strCriteria = " and open_code = " + Helper.CInt(txtopen_code.Text) + " and budget_type = '" + strbudget_type + "' ";
            DataTable dt = objEfloanApprove.SP_OPEN_APPROVE_SEL(_strCriteria);
            foreach (DataRow dr in dt.Rows)
            {
                rw = dtOpenApprove.NewRow();
                rw["open_detail_approve_id"] = ++this.OpenApproveID;
                rw["approve_code"] = Helper.CInt(dr["approve_code"]);
                rw["approve_name"] = Helper.CStr(dr["approve_name"]);
                rw["approve_level"] = Helper.CInt(dr["approve_level"]);
                rw["approve_remark"] = string.Empty;
                rw["person_code"] = Helper.CStr(dr["person_approve_code"]);
                rw["person_thai_name"] = Helper.CStr(dr["title_name"]) + Helper.CStr(dr["person_thai_name"]) + " " + Helper.CStr(dr["person_thai_surname"]);
                rw["person_manage_code"] = Helper.CStr(dr["person_manage_code"]);
                rw["person_manage_name"] = Helper.CStr(dr["person_manage_name"]);
                rw["row_status"] = "N";
                this.dtOpenApprove.Rows.Add(rw);
            }
            if (dt.Rows.Count == 0)
            {
                DataTable dtBudget = objApproveBudget.SP_APPROVE_BUDGET_SEL(" and ef_budget_type_approve in ('" + strbudget_type + "','H')");
                foreach (DataRow dr in dtBudget.Rows)
                {
                    rw = dtOpenApprove.NewRow();
                    rw["open_detail_approve_id"] = ++this.OpenApproveID;
                    rw["approve_code"] = Helper.CInt(dr["approve_code"]);
                    rw["approve_name"] = Helper.CStr(dr["approve_name"]);
                    rw["approve_level"] = Helper.CInt(dr["approve_level"]);
                    rw["approve_remark"] = string.Empty;
                    rw["person_code"] = Helper.CStr(dr["ef_person_code_approve"]);
                    rw["person_thai_name"] = Helper.CStr(dr["title_name"]) + Helper.CStr(dr["person_thai_name"]) + " " + Helper.CStr(dr["person_thai_surname"]);
                    rw["person_manage_code"] = Helper.CStr(dr["ef_approve_position"]);
                    rw["person_manage_name"] = Helper.CStr(dr["ef_approve_position_name"]);
                    rw["row_status"] = "N";
                    this.dtOpenApprove.Rows.Add(rw);
                }
            }
        }


        private void GetOpenDetail()
        {
            DataRow rw;
            cefOpenItem objEfloanItem = new cefOpenItem();
            _strCriteria = " and open_code = " + Helper.CInt(txtopen_code.Text);
            DataTable dt = objEfloanItem.SP_OPEN_ITEM_SEL(_strCriteria);
            foreach (DataRow dr in dt.Rows)
            {
                rw = this.dtOpenDetail.NewRow();
                rw["open_detail_id"] = ++this.OpenDetailID;
                rw["material_id"] = Helper.CInt(dr["material_id"]);
                rw["material_name"] = Helper.CStr(dr["material_name"]);
                rw["material_detail"] = string.Empty;
                rw["open_detail_amount"] = 0;
                rw["open_detail_remark"] = string.Empty;
                rw["row_status"] = "N";
                this.dtOpenDetail.Rows.Add(rw);
            }
        }

    }
}