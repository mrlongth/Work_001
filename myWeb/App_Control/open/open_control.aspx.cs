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

namespace myWeb.App_Control.open
{
    public partial class open_control : PageBase
    {
        public static string getNumber(object pNumber)
        {
            if (!pNumber.ToString().Equals(""))
            {
                string strNumber = String.Format("{0:#,##0.00}", float.Parse(pNumber.ToString()));
                return strNumber;
            }
            return "";
        }
        private string strPrefixCtr_main = "ctl00$ContentPlaceHolder1$TabContainer1$";
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
        private bool bIsGridCommadeEmpty
        {
            get
            {
                if (ViewState["bIsGridCommadeEmpty"] == null)
                {
                    ViewState["bIsGridCommadeEmpty"] = false;
                }
                return (bool)ViewState["bIsGridCommadeEmpty"];
            }
            set
            {
                ViewState["bIsGridCommadeEmpty"] = value;
            }
        }
        private int OpenCommandID
        {
            get
            {
                if (ViewState["OpenCommandID"] == null)
                {
                    ViewState["OpenCommandID"] = 1000000;
                }
                return int.Parse(ViewState["OpenCommandID"].ToString());
            }
            set
            {
                ViewState["OpenCommandID"] = value;
            }
        }
        private DataTable dtOpenCommand
        {
            get
            {
                if (ViewState["dtOpenCommand"] == null)
                {
                    cOpen oopen = new cOpen();
                    DataSet ds = new DataSet();
                    _strMessage = string.Empty;
                    _strCriteria = " and open_head_id = " + hddopen_head_id.Value;
                    if (!oopen.SP_OPEN_COMMAND_SEL(_strCriteria, ref ds, ref _strMessage))
                    {
                        lblError.Text = _strMessage;
                    }
                    else
                    {
                        ViewState["dtOpenCommand"] = ds.Tables[0];

                    }
                }
                return (DataTable)ViewState["dtOpenCommand"];
            }
            set
            {
                ViewState["dtOpenCommand"] = value;
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
                    cOpen oOpen = new cOpen();
                    DataSet ds = new DataSet();
                    _strMessage = string.Empty;
                    _strCriteria = " and open_head_id = " + Helper.CInt(hddopen_head_id.Value) + " order by open_code";
                    if (!oOpen.SP_OPEN_DETAIL_SEL(_strCriteria, ref ds, ref _strMessage))
                    {
                        lblError.Text = _strMessage;
                    }
                    else
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ViewState["dtOpenDetail"] = ds.Tables[0];
                        }
                        else
                        {
                            _strCriteria = " and open_id = " + Helper.CInt(hddopen_id.Value); // +" and [view_person_item].[budget_plan_code] = '" + txtbudget_plan_code.Text + "' ";
                            if (!oOpen.sp_OPEN_DETAIL_TMP_SEL(_strCriteria, ref ds, ref _strMessage))
                            {
                                lblError.Text = _strMessage;
                            }
                            else
                            {
                                foreach (DataRow dr in ds.Tables[0].Rows)
                                {
                                    dr["open_detail_id"] = this.OpenDetailID++;
                                }
                                ViewState["dtOpenDetail"] = ds.Tables[0];
                            }
                        }
                    }
                }
                return (DataTable)ViewState["dtOpenDetail"];
            }
            set
            {
                ViewState["dtOpenDetail"] = value;
            }
        }

        private bool bIsGridPersonEmpty
        {
            get
            {
                if (ViewState["bIsGridPersonEmpty"] == null)
                {
                    ViewState["bIsGridPersonEmpty"] = false;
                }
                return (bool)ViewState["bIsGridPersonEmpty"];
            }
            set
            {
                ViewState["bIsGridPersonEmpty"] = value;
            }
        }
        private long OpenPersonID
        {
            get
            {
                if (ViewState["OpenPersonID"] == null)
                {
                    ViewState["OpenPersonID"] = 1000000;
                }
                return long.Parse(ViewState["OpenPersonID"].ToString());
            }
            set
            {
                ViewState["OpenPersonID"] = value;
            }
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "RegisterScript", "RegisterScript();", true);
            }
            // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "load_total_all", "  load_total_all();", true);
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


                ViewState["sort"] = "open_command_id";
                ViewState["direction"] = "ASC";

                ViewState["sort2"] = "material_code";
                ViewState["direction2"] = "ASC";

                ViewState["sort3"] = "person_code";
                ViewState["direction2"] = "ASC";


                #region set QueryString

                IsUserEdit = false;
                IsUserDelete = false;

                if (Request.QueryString["IsUserEdit"] != null)
                {
                    if (Request.QueryString["IsUserEdit"].ToString() == "Y")
                    {
                        IsUserEdit = true;
                    }
                }

                if (Request.QueryString["IsUserDelete"] != null)
                {
                    if (Request.QueryString["IsUserDelete"].ToString() == "Y")
                    {
                        IsUserDelete = true;
                    }
                }

                if (Request.QueryString["open_doc"] != null)
                {
                    ViewState["open_doc"] = Request.QueryString["open_doc"].ToString();
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


                #endregion

                #region Set Image


                imgList_open.Attributes.Add("onclick", "OpenPopUp('800px','400px','93%','ค้นหาข้อมูลรายการขออนุมัติเบิกจ่าย' ,'../lov/open_lov.aspx?" +
                                                    "year='+document.forms[0]." + cboYear.UniqueID + ".options[document.forms[0]." + cboYear.UniqueID + ".selectedIndex].value+" +
                                                    "'&item_code='+document.forms[0]." + txtopen_code.UniqueID + ".value+" +
                                                    "'&item_name='+document.forms[0]." + txtopen_title.UniqueID + ".value+" +
                                                    "'&ctrl1=" + txtopen_code.ClientID + "&ctrl2=" + txtopen_title.ClientID + "&ctrl3=" + hddopen_id.ClientID + "&lbkGetOpen=" + lbkGetOpen.UniqueID + "&show=2&from=open_control', '2');return false;");

                //imgClear_open.Attributes.Add("onclick", "document.forms[0]." + txtopen_code.UniqueID + ".value='';" +
                //                        "document.forms[0]." + txtopen_title.UniqueID + ".value='';return false;");

                imgList_person.Attributes.Add("onclick", "OpenPopUp('900px','500px','94%','ค้นหาข้อมูลบุคคลากร' ,'../lov/person_lov.aspx?" +
                     "from=open_control&person_code='+getElementById('" + txtopen_person.ClientID + "').value+'" +
                     "&person_name='+getElementById('" + txtopen_person_name.ClientID + "').value+'" +
                    "&ctrl1=" + txtopen_person.ClientID + "&ctrl2=" + txtopen_person_name.ClientID + "&show=2', '2');return false;");

                imgClear_person.Attributes.Add("onclick", "document.getElementById('" + txtopen_person.ClientID + "').value='';document.getElementById('" + txtopen_person_name.ClientID + "').value=''; return false;");

                string strYear = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["yearnow"].ToString();
                imgList_budget_plan.Attributes.Add("onclick", "OpenPopUp('950px','500px','94%','ค้นหาข้อมูลผังงบประมาณประจำปี' ,'../lov/budget_plan_lov.aspx?" +
                                                                "budget_plan_code='+getElementById('" + txtbudget_plan_code.ClientID + "').value+'" +
                                                                "&budget_name='+getElementById('" + txtbudget_name.ClientID + "').value+'" +
                                                                "&produce_name='+getElementById('" + txtproduce_name.ClientID + "').value+'" +
                                                                "&activity_name='+getElementById('" + txtactivity_name.ClientID + "').value+'" +
                                                                "&plan_name='+getElementById('" + txtplan_name.ClientID + "').value+'" +
                                                                "&work_name='+getElementById('" + txtwork_name.ClientID + "').value+'" +
                                                                "&fund_name='+getElementById('" + txtfund_name.ClientID + "').value+'" +
                                                                "&director_name='+getElementById('" + txtdirector_name.ClientID + "').value+'" +
                                                                "&unit_name='+getElementById('" + txtunit_name.ClientID + "').value+'" +
                                                                "&budget_plan_year=" + strYear + "" +
                                                                "&ctrl1=" + txtbudget_plan_code.ClientID +
                                                                "&ctrl2=" + txtbudget_name.ClientID +
                                                                "&ctrl3=" + txtproduce_name.ClientID +
                                                                "&ctrl4=" + txtactivity_name.ClientID +
                                                                "&ctrl5=" + txtplan_name.ClientID +
                                                                "&ctrl6=" + txtwork_name.ClientID +
                                                                "&ctrl7=" + txtfund_name.ClientID +
                                                                "&ctrl9=" + txtdirector_name.ClientID +
                                                                "&ctrl10=" + txtunit_name.ClientID +
                                                                "&show=2', '2');return false;");

                imgClear_budget_plan.Attributes.Add("onclick",
                                                                "document.forms[0]." + strPrefixCtr_main + "TabPanel1$txtbudget_plan_code.value='';" +
                                                                "document.forms[0]." + strPrefixCtr_main + "TabPanel1$txtbudget_name.value='';" +
                                                                "document.forms[0]." + strPrefixCtr_main + "TabPanel1$txtproduce_name.value='';" +
                                                                "document.forms[0]." + strPrefixCtr_main + "TabPanel1$txtactivity_name.value='';" +
                                                                "document.forms[0]." + strPrefixCtr_main + "TabPanel1$txtplan_name.value='';" +
                                                                "document.forms[0]." + strPrefixCtr_main + "TabPanel1$txtwork_name.value='';" +
                                                                "document.forms[0]." + strPrefixCtr_main + "TabPanel1$txtfund_name.value='';" +
                                                                "document.forms[0]." + strPrefixCtr_main + "TabPanel1$txtdirector_name.value='';" +
                                                                "document.forms[0]." + strPrefixCtr_main + "TabPanel1$txtunit_name.value='';" +
                                                                "return false;");

                #endregion

                InitcboYear();
                InitcboLot();
                InitcboItem_group();
                InitcboBudgetType();
                ChangeLabelBudget();
                InitcboOpenLevel();
                InitcboOpen_to("");
                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    ViewState["page"] = Request.QueryString["page"];
                    TabContainer1.Tabs[1].Visible = false;
                    TabContainer1.Tabs[2].Visible = false;
                    TabContainer1.Tabs[3].Visible = false;
                    txtopen_date.Text = cCommon.CheckDate(DateTime.Now.ToShortDateString());

                }
                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    setData();
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
                if (cboBudget_type.Items.FindByValue(strCode) != null)
                {
                    cboBudget_type.SelectedIndex = -1;
                    cboBudget_type.Items.FindByValue(strCode).Selected = true;

                }
            }
            cboBudget_type.Enabled = false;
            cboBudget_type.CssClass = "textboxdis";
        }

        private void InitcboLot()
        {
            cLot oLot = new cLot();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strLot_code = string.Empty;
            string strLot = cbolot.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and c_active='Y' ";
            strCriteria += " and lot_year='" + cboYear.SelectedValue + "' ";
            if (oLot.SP_SEL_LOT(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cbolot.Items.Clear();
                cbolot.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cbolot.Items.Add(new ListItem(dt.Rows[i]["lot_name"].ToString(), dt.Rows[i]["lot_code"].ToString()));
                }
                if (cbolot.Items.FindByValue(strLot) != null)
                {
                    cbolot.SelectedIndex = -1;
                    cbolot.Items.FindByValue(strLot).Selected = true;
                }
            }
        }

        private void InitcboItem_group()
        {
            cItem_group oItem_group = new cItem_group();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strItem_group_code = string.Empty;
            string strlot_code = cbolot.SelectedValue;
            string stritem_group_year = cboYear.SelectedValue;
            strItem_group_code = cboItem_group.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = "and item_group_year='" + stritem_group_year + "' ";
            if (oItem_group.SP_SEL_item_group(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboItem_group.Items.Clear();
                cboItem_group.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboItem_group.Items.Add(new ListItem(dt.Rows[i]["Item_group_name"].ToString(), dt.Rows[i]["Item_group_code"].ToString()));
                }
                if (cboItem_group.Items.FindByValue(strItem_group_code) != null)
                {
                    cboItem_group.SelectedIndex = -1;
                    cboItem_group.Items.FindByValue(strItem_group_code).Selected = true;
                }
            }
        }

        private void InitcboOpen_to(string strText)
        {
            List<string> listopen_to = new List<string>();
            listopen_to.AddRange(strText.Split(','));
            cboopen_to.Items.Clear();
            foreach (string str in listopen_to)
            {
                if (str.Length > 0)
                {
                    cboopen_to.Items.Add(new ListItem(str, str));
                }
            }
            if (cboopen_to.Items.Count != 1)
            {
                cboopen_to.Items.Insert(0, new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
            }

        }

        private void InitcboOpenLevel()
        {
            cCommon oCommon = new cCommon();
            string strMessage = string.Empty,
                        strCriteria = string.Empty;

            string strOpen_level = "A";
            if (base.UnitLock == "Y")
            {
                strOpen_level = "U";
            }
            else if (base.DirectorLock == "Y")
            {
                strOpen_level = "D";
            } int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            strCriteria = " and g_type='open_level' ";
            if (oCommon.SP_SEL_OBJECT("sp_GENERAL_SEL", strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboOpen_level.Items.Clear();
                cboOpen_level.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboOpen_level.Items.Add(new ListItem(dt.Rows[i]["g_name"].ToString(), dt.Rows[i]["g_code"].ToString()));
                }
                if (cboOpen_level.Items.FindByValue(strOpen_level) != null)
                {
                    cboOpen_level.SelectedIndex = -1;
                    cboOpen_level.Items.FindByValue(strOpen_level).Selected = true;
                }
            }
        }


        #endregion

        private bool saveData()
        {
            bool blnResult = false;
            string strMessage = string.Empty;
            string stropen_doc = string.Empty;
            string strcomments = string.Empty;
            string strActive = string.Empty;
            string strCreatedBy = string.Empty;
            string strUpdatedBy = string.Empty;
            string strUnitCode = string.Empty;
            string strDirectorCode = string.Empty;
            string strOpen_level = string.Empty;

            cOpen oOpen = new cOpen();
            try
            {
                #region set Data
                stropen_doc = txtopen_doc.Text;
                strCreatedBy = Session["username"].ToString();
                strUpdatedBy = Session["username"].ToString();
                strOpen_level = cboOpen_level.SelectedValue;
                #endregion
                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    #region insert
                    if (!oOpen.SP_OPEN_HEAD_INS(ref stropen_doc, cboYear.SelectedValue, hddopen_id.Value, cboopen_to.SelectedValue, txtopen_title.Text.Trim(),
                        txtopen_command_desc.Text.Trim(), txtopen_desc.Text.Trim(), txtopen_tel.Text.Trim(), strUnitCode, strDirectorCode, txtbudget_plan_code.Text.Trim(), txtopen_person.Text.Trim(),
                        strOpen_level, txtopen_date.Text , strUpdatedBy, ref strMessage))
                    {
                        lblError.Text = strMessage;
                    }
                    else
                    {
                        ViewState["open_doc"] = stropen_doc;
                        blnResult = true;
                    }
                    #endregion
                }
                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    #region update
                    if (!oOpen.SP_OPEN_HEAD_UPD(hddopen_head_id.Value, txtopen_doc.Text.Trim(), cboYear.SelectedValue, hddopen_id.Value, cboopen_to.SelectedValue, txtopen_title.Text.Trim(),
                        txtopen_command_desc.Text.Trim(), txtopen_tel.Text.Trim(), txtopen_desc.Text.Trim(), strUnitCode, strDirectorCode, txtbudget_plan_code.Text.Trim(), txtopen_person.Text.Trim(),
                        strOpen_level, txtopen_date.Text, strUpdatedBy, ref strMessage))
                    {
                        lblError.Text = strMessage;
                    }
                    else
                    {
                        SaveCommand();
                        SaveDetail();
                        SavePerson();
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
                oOpen.Dispose();
            }
            return blnResult;
        }

        protected void imgPrint_Click(object sender, ImageClickEventArgs e)
        {
            string strScript = "windowOpenMaximize(\"../../App_Control/reportsparameter/open_report_show.aspx?open_head_id=" + hddopen_head_id.Value + "\", \"_blank\");\n";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript, true);
        }

        private void setData()
        {
            cOpen oopen = new cOpen();
            DataSet ds = new DataSet();
            string strMessage = string.Empty,
                strCriteria = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty,
                strCreatedDate = string.Empty,
                strUpdatedDate = string.Empty,
                stropen_to_source = string.Empty;
            try
            {
                txtopen_doc.ReadOnly = true;
                txtopen_doc.CssClass = "textboxdis";
                ViewState["mode"] = "edit";
                strCriteria = " and open_doc = '" + ViewState["open_doc"].ToString() + "' ";
                if (!oopen.SP_OPEN_HEAD_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region get Data
                        hddopen_head_id.Value = ds.Tables[0].Rows[0]["open_head_id"].ToString();
                        txtopen_doc.Text = ds.Tables[0].Rows[0]["open_doc"].ToString();
                        cboYear.SelectedValue = ds.Tables[0].Rows[0]["open_year"].ToString();
                        cboBudget_type.SelectedValue = ds.Tables[0].Rows[0]["budget_type"].ToString();
                        txtopen_code.Text = ds.Tables[0].Rows[0]["open_code"].ToString();
                        hddopen_id.Value = ds.Tables[0].Rows[0]["open_id"].ToString();

                        stropen_to_source = ds.Tables[0].Rows[0]["open_to_source"].ToString();
                        InitcboOpen_to(stropen_to_source);
                        cboopen_to.SelectedValue = ds.Tables[0].Rows[0]["open_to"].ToString();

                        txtopen_title.Text = ds.Tables[0].Rows[0]["open_title"].ToString();
                        txtopen_command_desc.Text = ds.Tables[0].Rows[0]["open_command_desc"].ToString();
                        txtopen_desc.Text = ds.Tables[0].Rows[0]["open_desc"].ToString();
                        txtopen_tel.Text = ds.Tables[0].Rows[0]["open_tel"].ToString();

                        InitcboItem_group();
                        cboItem_group.SelectedValue = ds.Tables[0].Rows[0]["item_group_code"].ToString();

                        InitcboLot();
                        cbolot.SelectedValue = ds.Tables[0].Rows[0]["lot_code"].ToString();

                        InitcboOpenLevel();
                        cboOpen_level.SelectedValue = ds.Tables[0].Rows[0]["open_level"].ToString();

                        txtbudget_plan_code.Text = ds.Tables[0].Rows[0]["budget_plan_code"].ToString();
                        txtbudget_name.Text = ds.Tables[0].Rows[0]["budget_name"].ToString();
                        txtproduce_name.Text = ds.Tables[0].Rows[0]["produce_name"].ToString();
                        txtactivity_name.Text = ds.Tables[0].Rows[0]["activity_name"].ToString();
                        txtplan_name.Text = ds.Tables[0].Rows[0]["plan_name"].ToString();
                        txtwork_name.Text = ds.Tables[0].Rows[0]["work_name"].ToString();
                        txtfund_name.Text = ds.Tables[0].Rows[0]["fund_name"].ToString();
                        txtdirector_name.Text = ds.Tables[0].Rows[0]["budget_director_name"].ToString();
                        txtunit_name.Text = ds.Tables[0].Rows[0]["budget_unit_name"].ToString();
                        txtopen_person.Text = ds.Tables[0].Rows[0]["person_open"].ToString();
                        txtopen_person_name.Text = ds.Tables[0].Rows[0]["person_thai_name"].ToString() + " " + ds.Tables[0].Rows[0]["person_thai_surname"].ToString();

                        txtopen_date.Text = cCommon.CheckDate(ds.Tables[0].Rows[0]["open_date"].ToString());



                        strCreatedBy = ds.Tables[0].Rows[0]["c_created_by"].ToString();
                        strUpdatedBy = ds.Tables[0].Rows[0]["c_updated_by"].ToString();
                        strCreatedDate = ds.Tables[0].Rows[0]["d_created_date"].ToString();
                        strUpdatedDate = ds.Tables[0].Rows[0]["d_updated_date"].ToString();
                        #endregion

                        #region set Control
                        ChangeLabelBudget();
                        txtUpdatedBy.Text = strUpdatedBy;
                        txtUpdatedDate.Text = strUpdatedDate;
                        BindGridCommand();
                        BindGridDetail();
                        BindGridPerson();
                        #endregion
                    }
                }
                TabContainer1.Tabs[1].Visible = true;
                TabContainer1.Tabs[2].Visible = true;
                TabContainer1.Tabs[3].Visible = true;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        private void StoreCommand()
        {
            try
            {
                HiddenField hddopen_command_id;
                TextBox txtopen_date, txtopen_desc, txtopen_no;
                foreach (GridViewRow gvRow in GridView1.Rows)
                {
                    hddopen_command_id = (HiddenField)gvRow.FindControl("hddopen_command_id");
                    txtopen_date = (TextBox)gvRow.FindControl("txtopen_date");
                    txtopen_desc = (TextBox)gvRow.FindControl("txtopen_desc");
                    txtopen_no = (TextBox)gvRow.FindControl("txtopen_no");
                    foreach (DataRow dr in this.dtOpenCommand.Rows)
                    {
                        if (Helper.CInt(dr["open_command_id"]) == Helper.CInt(hddopen_command_id.Value))
                        {
                            dr["open_no"] = txtopen_no.Text.Trim();
                            dr["open_date"] = txtopen_date.Text.Trim();
                            dr["open_desc"] = txtopen_desc.Text.Trim();
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
        private void BindGridCommand()
        {
            DataView dv = null;
            try
            {
                dv = new DataView(this.dtOpenCommand, "", (ViewState["sort"] + " " + ViewState["direction"]), DataViewRowState.CurrentRows);
                GridView1.DataSource = dv.ToTable();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                this.bIsGridCommadeEmpty = false;
                if (dv.ToTable().Rows.Count == 0)
                {
                    this.bIsGridCommadeEmpty = true;
                    EmptyGridFix(GridView1);
                }
                else
                {
                    GridView1.DataBind();
                }
            }
        }
        private bool SaveCommand()
        {
            bool blnResult = false;
            cOpen oOpen = new cOpen();
            try
            {
                StoreCommand();
                if (!oOpen.SP_OPEN_COMMAND_DEL(hddopen_head_id.Value, ref _strMessage))
                {
                    lblError.Text = _strMessage;
                }
                else
                {
                    foreach (DataRow dr in this.dtOpenCommand.Rows)
                    {
                        if (Helper.CStr(dr["open_no"]).Length > 0)
                        {
                            if (!oOpen.SP_OPEN_COMMAND_INS(hddopen_head_id.Value,
                                    Helper.CStr(dr["open_no"]),
                                    Helper.CStr(dr["open_date"]),
                                    Helper.CStr(dr["open_desc"]),
                                    ref _strMessage))
                            {
                                lblError.Text = _strMessage;
                            }
                            else
                            {
                                blnResult = true;
                            }
                        }
                    }
                    this.dtOpenCommand = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oOpen.Dispose();
            }
            return blnResult;
        }

        private void StoreDetail()
        {
            try
            {
                HiddenField hddopen_detail_id;
                HiddenField hddmaterial_id;
                TextBox txtmaterial_code;
                TextBox txtmaterial_name;
                AwNumeric txtopen_rate;
                TextBox txtopen_begin_date;
                TextBox txtopen_end_date;
                AwNumeric txtopen_qty_month;
                AwNumeric txtopen_qty_day;
                AwNumeric txtopen_qty_person;
                AwNumeric txtopen_all_rate;
                foreach (GridViewRow gvRow in GridView2.Rows)
                {
                    hddopen_detail_id = (HiddenField)gvRow.FindControl("hddopen_detail_id");
                    hddmaterial_id = (HiddenField)gvRow.FindControl("hddmaterial_id");
                    txtmaterial_code = (TextBox)gvRow.FindControl("txtmaterial_code");
                    txtmaterial_name = (TextBox)gvRow.FindControl("txtmaterial_name");
                    txtopen_rate = (AwNumeric)gvRow.FindControl("txtopen_rate");
                    txtopen_begin_date = (TextBox)gvRow.FindControl("txtopen_begin_date");
                    txtopen_end_date = (TextBox)gvRow.FindControl("txtopen_end_date");
                    txtopen_qty_month = (AwNumeric)gvRow.FindControl("txtopen_qty_month");
                    txtopen_qty_day = (AwNumeric)gvRow.FindControl("txtopen_qty_day");
                    txtopen_qty_person = (AwNumeric)gvRow.FindControl("txtopen_qty_person");
                    txtopen_all_rate = (AwNumeric)gvRow.FindControl("txtopen_all_rate");
                    foreach (DataRow dr in this.dtOpenDetail.Rows)
                    {
                        if (Helper.CLong(dr["open_detail_id"]) == Helper.CLong(hddopen_detail_id.Value))
                        {
                            dr["open_detail_id"] = hddopen_detail_id.Value;
                            dr["material_id"] = hddmaterial_id.Value;
                            dr["material_code"] = txtmaterial_code.Text;
                            dr["material_name"] = txtmaterial_name.Text;
                            dr["open_rate"] = txtopen_rate.Value;
                            dr["open_begin_date"] = txtopen_begin_date.Text;
                            dr["open_end_date"] = txtopen_end_date.Text;
                            dr["open_qty_month"] = txtopen_qty_month.Text;
                            dr["open_qty_day"] = txtopen_qty_day.Value;
                            dr["open_qty_person"] = txtopen_qty_person.Value;
                            dr["open_rate_all"] = txtopen_all_rate.Value;
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
                dv = new DataView(this.dtOpenDetail, "", (ViewState["sort2"] + " " + ViewState["direction2"]), DataViewRowState.CurrentRows);
                GridView2.DataSource = dv.ToTable();
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
                    EmptyGridFix(GridView2);
                }
                else
                {
                    GridView2.DataBind();
                }
            }
        }
        private bool SaveDetail()
        {
            bool blnResult = false;
            cOpen oOpen = new cOpen();
            try
            {
                StoreDetail();
                if (!oOpen.SP_OPEN_DETAIL_DEL(hddopen_head_id.Value, ref _strMessage))
                {
                    lblError.Text = _strMessage;
                }
                else
                {
                    foreach (DataRow dr in this.dtOpenDetail.Rows)
                    {
                        if (Helper.CInt(dr["material_id"]) > 0)
                        {
                            if (!oOpen.SP_OPEN_DETAIL_INS(hddopen_head_id.Value,
                                    Helper.CStr(dr["material_id"]),
                                    Helper.CStr(dr["open_begin_date"]),
                                    Helper.CStr(dr["open_end_date"]),
                                    Helper.CStr(dr["open_qty_month"]),
                                    Helper.CStr(dr["open_qty_day"]),
                                    Helper.CStr(dr["open_qty_person"]),
                                    Helper.CStr(dr["open_rate"]),
                                    Helper.CStr(dr["open_rate_all"]),
                                    ref _strMessage))
                            {
                                lblError.Text = _strMessage;
                            }
                            else
                            {
                                blnResult = true;
                            }
                        }
                    }
                    this.dtOpenDetail = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oOpen.Dispose();
            }
            return blnResult;
        }

        private void SavePerson()
        {
            cOpen oOpen = new cOpen();
            try
            {
                HiddenField hddopen_person_id;
                TextBox txtperson_code;
                TextBox txtperson_name;
                AwNumeric txtopen_rate;
                TextBox txtopen_begin_date;
                TextBox txtopen_end_date;
                AwNumeric txtopen_qty_month;
                AwNumeric txtopen_qty_day;
                AwNumeric txtopen_qty_person;
                AwNumeric txtopen_all_rate;
                foreach (GridViewRow gvRow in GridView3.Rows)
                {
                    hddopen_person_id = (HiddenField)gvRow.FindControl("hddopen_person_id");
                    txtperson_code = (TextBox)gvRow.FindControl("txtperson_code");
                    txtperson_name = (TextBox)gvRow.FindControl("txtperson_name");
                    txtopen_rate = (AwNumeric)gvRow.FindControl("txtopen_rate");
                    txtopen_begin_date = (TextBox)gvRow.FindControl("txtopen_begin_date");
                    txtopen_end_date = (TextBox)gvRow.FindControl("txtopen_end_date");
                    txtopen_qty_month = (AwNumeric)gvRow.FindControl("txtopen_qty_month");
                    txtopen_qty_day = (AwNumeric)gvRow.FindControl("txtopen_qty_day");
                    txtopen_qty_person = (AwNumeric)gvRow.FindControl("txtopen_qty_person");
                    txtopen_all_rate = (AwNumeric)gvRow.FindControl("txtopen_all_rate");
                    if (Helper.CInt(hddopen_person_id.Value) > 0)
                    {
                        if (!oOpen.SP_OPEN_PERSON_UPD(hddopen_person_id.Value,
                                hddopen_head_id.Value,
                                txtperson_code.Text,
                                txtopen_begin_date.Text,
                                txtopen_end_date.Text,
                                txtopen_qty_month.Value.ToString(),
                                txtopen_qty_day.Value.ToString(),
                                txtopen_qty_person.Value.ToString(),
                                txtopen_rate.Value.ToString(),
                                txtopen_all_rate.Value.ToString(),
                                ref _strMessage))
                        {
                            lblError.Text = _strMessage;
                            return;
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
                oOpen.Dispose();
            }
        }
        private void BindGridPerson()
        {
            cOpen oOpen = new cOpen();
            DataSet ds = new DataSet();
            DataTable dtOpenPerson = null;
            try
            {
                _strMessage = string.Empty;
                _strCriteria = " and open_head_id = " + hddopen_head_id.Value + " order by " + ViewState["sort3"] + " " + ViewState["direction3"];
                if (!oOpen.SP_OPEN_PERSON_SEL(_strCriteria, ref ds, ref _strMessage))
                {
                    lblError.Text = _strMessage;
                    return;
                }
                else
                {
                    dtOpenPerson = ds.Tables[0];
                    GridView3.DataSource = dtOpenPerson;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                this.bIsGridPersonEmpty = false;
                if (dtOpenPerson.Rows.Count == 0)
                {
                    this.bIsGridPersonEmpty = true;
                    EmptyGridFix(GridView3);
                }
                else
                {
                    GridView3.DataBind();
                }
            }
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

            }
            else if (e.Row.RowType.Equals(DataControlRowType.DataRow) || e.Row.RowState.Equals(DataControlRowState.Alternate))
            {
                if (!this.bIsGridCommadeEmpty)
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
                    TextBox txtopen_date = (TextBox)e.Row.FindControl("txtopen_date");
                    txtopen_date.Text = ((DateTime)dv["open_date"]).ToString("dd/MM/yyyy");

                    #region set Image Delete

                    ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                    imgDelete.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["img"].ToString();
                    imgDelete.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["title"].ToString());
                    imgDelete.Attributes.Add("onclick", "return confirm(\"คุณต้องการลบข้อมูลนี้หรือไม่ ?\");");
                    #endregion
                }

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
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string strMessage = string.Empty;
            string strScript = string.Empty;
            HiddenField hddopen_command_id = (HiddenField)GridView1.Rows[e.RowIndex].FindControl("hddopen_command_id");

            cOpen oOpen = new cOpen();
            try
            {
                StoreCommand();
                int i = 0;
                foreach (DataRow dr in this.dtOpenCommand.Rows)
                {
                    if (Helper.CInt(dr["open_command_id"]) == Helper.CInt(hddopen_command_id.Value))
                    {
                        this.dtOpenCommand.Rows.Remove(dr);
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
                oOpen.Dispose();
            }
            BindGridCommand();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {
                case "ADD":
                    StoreCommand();
                    DataRow dr = this.dtOpenCommand.NewRow();
                    dr["open_command_id"] = ++this.OpenCommandID;
                    dr["open_no"] = "";
                    dr["open_date"] = DateTime.Now;
                    dr["open_desc"] = "";
                    this.dtOpenCommand.Rows.Add(dr);
                    BindGridCommand();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region GridView2 Event
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
                    int nNo = (GridView2.PageSize * GridView2.PageIndex) + e.Row.RowIndex + 1;
                    lblNo.Text = nNo.ToString();
                    TextBox txtopen_begin_date = (TextBox)e.Row.FindControl("txtopen_begin_date");
                    TextBox txtopen_end_date = (TextBox)e.Row.FindControl("txtopen_end_date");
                    txtopen_begin_date.Text = ((DateTime)dv["open_begin_date"]).ToString("dd/MM/yyyy");
                    txtopen_end_date.Text = ((DateTime)dv["open_end_date"]).ToString("dd/MM/yyyy");

                    #region set Image Delete

                    ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                    imgDelete.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["img"].ToString();
                    imgDelete.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["title"].ToString());
                    imgDelete.Attributes.Add("onclick", "return confirm(\"คุณต้องการลบข้อมูลนี้หรือไม่ ?\");");
                    #endregion
                }

            }
            if (e.Row.RowType.Equals(DataControlRowType.Footer))
            {
                e.Row.Cells[0].ColumnSpan = 2;
                e.Row.Cells.RemoveAt(1);
                e.Row.Cells[2].ColumnSpan = 4;
                e.Row.Cells.RemoveAt(3);
                e.Row.Cells.RemoveAt(4);
                e.Row.Cells.RemoveAt(5);
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
                    if (ViewState["sort2"].Equals(GridView2.Columns[i].SortExpression))
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

        protected void GridView2_Sorting(object sender, GridViewSortEventArgs e)
        {
        }

        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string strMessage = string.Empty;
            string strScript = string.Empty;
            HiddenField hddopen_detail_id = (HiddenField)GridView2.Rows[e.RowIndex].FindControl("hddopen_detail_id");
            try
            {
                StoreDetail();
                int i = 0;
                foreach (DataRow dr in this.dtOpenDetail.Rows)
                {
                    if (Helper.CLong(dr["open_detail_id"]) == Helper.CLong(hddopen_detail_id.Value))
                    {
                        this.dtOpenDetail.Rows.Remove(dr);
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

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {
                case "ADD":
                    StoreDetail();
                    DataRow dr = this.dtOpenDetail.NewRow();
                    dr["open_detail_id"] = ++this.OpenDetailID;
                    dr["material_id"] = 0;
                    dr["material_code"] = "";
                    dr["material_name"] = "";
                    dr["open_rate"] = 0;
                    dr["open_begin_date"] = DateTime.Now;
                    dr["open_end_date"] = DateTime.Now;
                    dr["open_qty_month"] = 0;
                    dr["open_qty_day"] = 0;
                    dr["open_qty_person"] = 0;
                    dr["open_rate_all"] = 0;
                    this.dtOpenDetail.Rows.Add(dr);
                    BindGridDetail();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region GridView3 Event
        protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
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
                imgAdd.Attributes.Add("onclick", "OpenPopUp('850px','450px','95%','ค้นหาบุคคลากร','open_person_select.aspx?open_id='+$('#" + hddopen_id.ClientID + "').val()+'&open_head_id='+$('#" + hddopen_head_id.ClientID + "').val()+'&page=0&show=2','2');return false;");
            }
            else if (e.Row.RowType.Equals(DataControlRowType.DataRow) || e.Row.RowState.Equals(DataControlRowState.Alternate))
            {
                if (!this.bIsGridPersonEmpty)
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
                    TextBox txtopen_begin_date = (TextBox)e.Row.FindControl("txtopen_begin_date");
                    TextBox txtopen_end_date = (TextBox)e.Row.FindControl("txtopen_end_date");
                    txtopen_begin_date.Text = ((DateTime)dv["open_begin_date"]).ToString("dd/MM/yyyy");
                    txtopen_end_date.Text = ((DateTime)dv["open_end_date"]).ToString("dd/MM/yyyy");

                    #region set Image Delete

                    ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                    imgDelete.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["img"].ToString();
                    imgDelete.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["title"].ToString());
                    imgDelete.Attributes.Add("onclick", "return confirm(\"คุณต้องการลบข้อมูลนี้หรือไม่ ?\");");
                    #endregion
                }

            }
            if (e.Row.RowType.Equals(DataControlRowType.Footer))
            {
                e.Row.Cells[0].ColumnSpan = 2;
                e.Row.Cells.RemoveAt(1);
                e.Row.Cells[2].ColumnSpan = 4;
                e.Row.Cells.RemoveAt(3);
                e.Row.Cells.RemoveAt(4);
                e.Row.Cells.RemoveAt(5);
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
            string strMessage = string.Empty;
            string strScript = string.Empty;
            cOpen oOpen = new cOpen();
            HiddenField hddopen_person_id = (HiddenField)GridView3.Rows[e.RowIndex].FindControl("hddopen_person_id");
            try
            {
                if (!oOpen.SP_OPEN_PERSON_DEL(hddopen_person_id.Value.ToString(), ref _strMessage))
                {
                    lblError.Text = _strMessage;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
            }
            BindGridPerson();
        }

        #endregion

        protected void BtnR1_Click(object sender, EventArgs e)
        {
            setData();
        }

        protected void lbkGetOpen_Click(object sender, EventArgs e)
        {
            cOpen oOpen = new cOpen();
            DataSet ds = new DataSet();
            string strMessage = string.Empty, strCriteria = string.Empty,
                    stropen_report_code, stropen_to, stritem_group, strlot;
            try
            {
                strCriteria = " and open_code = '" + txtopen_code.Text + "' ";
                if (!oOpen.SP_OPEN_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    hddopen_id.Value = Helper.CStr(ds.Tables[0].Rows[0]["open_id"]);
                    txtopen_code.Text = Helper.CStr(ds.Tables[0].Rows[0]["open_code"]);
                    stropen_to = Helper.CStr(ds.Tables[0].Rows[0]["open_to"]);
                    txtopen_title.Text = Helper.CStr(ds.Tables[0].Rows[0]["open_title"]);
                    txtopen_command_desc.Text = ds.Tables[0].Rows[0]["open_command_desc"].ToString();
                    txtopen_desc.Text = Helper.CStr(ds.Tables[0].Rows[0]["open_desc"]);
                    stropen_report_code = Helper.CStr(ds.Tables[0].Rows[0]["open_report_code"]);
                    strlot = Helper.CStr(ds.Tables[0].Rows[0]["lot_code"]);
                    stritem_group = Helper.CStr(ds.Tables[0].Rows[0]["item_group_code"]);
                    stropen_to = Helper.CStr(ds.Tables[0].Rows[0]["open_to"]);
                    if (cbolot.Items.FindByValue(strlot) != null)
                    {
                        cbolot.SelectedIndex = -1;
                        cbolot.Items.FindByValue(strlot).Selected = true;
                    }
                    InitcboItem_group();
                    if (cboItem_group.Items.FindByValue(stritem_group) != null)
                    {
                        cboItem_group.SelectedIndex = -1;
                        cboItem_group.Items.FindByValue(stritem_group).Selected = true;
                    }
                    InitcboOpen_to(stropen_to);



                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        protected void lbkGetPerson_Click(object sender, EventArgs e)
        {
            ViewState["dtOpenPerson"] = null;
            BindGridPerson();
        }

        protected void imgClear_item_Click(object sender, ImageClickEventArgs e)
        {
            txtopen_code.Text = string.Empty;
            cboopen_to.Items.Clear();
            txtopen_title.Text = string.Empty;
            txtopen_command_desc.Text = string.Empty;
            txtopen_desc.Text = string.Empty;
            cboItem_group.SelectedIndex = 0;
            cbolot.SelectedIndex = 0;
            InitcboOpen_to("");

        }

        private void ChangeLabelBudget()
        {

            string strBusget_type = cboBudget_type.SelectedValue;
            string strLovTitle = "ค้นหาข้อมูลผังงบประมาณประจำปี (เงินงบประมาณ)";
            if (strBusget_type == "R") strLovTitle = "ค้นหาข้อมูลผังงบประมาณประจำปี (เงินรายได้)";

            string strYear = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["yearnow"].ToString();
            imgList_budget_plan.Attributes.Add("onclick", "OpenPopUp('950px','500px','94%','" + strLovTitle + "' ,'../lov/budget_plan_lov.aspx?budget_type=" + strBusget_type +
                                                            "&budget_plan_code='+getElementById('" + txtbudget_plan_code.ClientID + "').value+'" +
                                                            "&budget_name='+getElementById('" + txtbudget_name.ClientID + "').value+'" +
                                                            "&produce_name='+getElementById('" + txtproduce_name.ClientID + "').value+'" +
                                                            "&activity_name='+getElementById('" + txtactivity_name.ClientID + "').value+'" +
                                                            "&plan_name='+getElementById('" + txtplan_name.ClientID + "').value+'" +
                                                            "&work_name='+getElementById('" + txtwork_name.ClientID + "').value+'" +
                                                            "&fund_name='+getElementById('" + txtfund_name.ClientID + "').value+'" +
                                                            "&director_name='+getElementById('" + txtdirector_name.ClientID + "').value+'" +
                                                            "&unit_name='+getElementById('" + txtunit_name.ClientID + "').value+'" +
                                                            "&budget_plan_year=" + strYear + "" +
                                                            "&ctrl1=" + txtbudget_plan_code.ClientID +
                                                            "&ctrl2=" + txtbudget_name.ClientID +
                                                            "&ctrl3=" + txtproduce_name.ClientID +
                                                            "&ctrl4=" + txtactivity_name.ClientID +
                                                            "&ctrl5=" + txtplan_name.ClientID +
                                                            "&ctrl6=" + txtwork_name.ClientID +
                                                            "&ctrl7=" + txtfund_name.ClientID +
                                                            "&ctrl9=" + txtdirector_name.ClientID +
                                                            "&ctrl10=" + txtunit_name.ClientID +
                                                            "&show=2', '2');return false;");
        }

        protected void imgSaveOnly_Click(object sender, ImageClickEventArgs e)
        {
            if (saveData())
            {
                string strScript1 = "$('#divdes1').text().replace('เพิ่ม','แก้ไข');PopUpListPost('1','1');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
                MsgBox("บันทึกข้อมูลสมบูรณ์");
                setData();
            }
        }



    }
}