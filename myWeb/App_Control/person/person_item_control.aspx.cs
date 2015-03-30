using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using myDLL;

namespace myWeb.App_Control.person
{
    public partial class person_item_control : PageBase
    {
        #region private data
        private string strConn = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"];
        private bool[] blnAccessRight = new bool[5] { false, false, false, false, false };
        private string strPrefixCtr = "ctl00$ContentPlaceHolder1$";
        private string strPrefixCtr_main = "ctl00$ContentPlaceHolder1$TabContainer1$";

        #endregion
        protected void Page_Load(object sender, System.EventArgs e)
        {
            lblError.Text = "";
            if (!IsPostBack)
            {
                Session["menupopup_name"] = "";
                #region set QueryString
                if (Request.QueryString["person_code"] != null)
                {
                    ViewState["person_code"] = Request.QueryString["person_code"].ToString();
                    lblperson_code.Text = ViewState["person_code"].ToString();
                }
                if (Request.QueryString["person_name"] != null)
                {
                    ViewState["person_name"] = Request.QueryString["person_name"].ToString();
                    lblperson_name.Text = ViewState["person_name"].ToString();
                }
                if (Request.QueryString["item_code"] != null)
                {
                    ViewState["item_code"] = Request.QueryString["item_code"].ToString();
                    txtitem_code.Text = ViewState["item_code"].ToString();
                }
                if (Request.QueryString["year"] != null)
                {
                    ViewState["year"] = Request.QueryString["year"].ToString();
                    txtyear.Text = ViewState["year"].ToString();
                }
                if (Request.QueryString["person_group_code"] != null)
                {
                    ViewState["person_group_code"] = Request.QueryString["person_group_code"].ToString();
                }
                else
                {
                    ViewState["person_group_code"] = string.Empty;
                }
                if (Request.QueryString["mode"] != null)
                {
                    ViewState["mode"] = Request.QueryString["mode"].ToString();
                }
                if (Request.QueryString["PageStatus"] != null)
                {
                    ViewState["PageStatus"] = Request.QueryString["PageStatus"].ToString();
                }
                InitcboBudgetType();
                ChangeLabelBudget();
                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    Session["menupopup_name"] = "เพิ่มข้อมูลรายรับ/จ่ายบุคคลากร";
                    ViewState["page"] = Request.QueryString["page"];
                }
                else
                {
                    setData();
                }

                #endregion
                #region Set Image

                imgList_item.Attributes.Add("onclick", "OpenPopUp('800px','400px','93%','ค้นหาข้อมูลรายได้/ค่าใช้จ่าย' ,'../lov/item_lov.aspx?" +
                                                "year='+document.forms[0]." + strPrefixCtr_main + "TabPanel1$txtyear.value+" +
                                                "'&item_code='+document.forms[0]." + strPrefixCtr_main + "TabPanel1$txtitem_code.value+" +
                                                "'&item_name='+document.forms[0]." + strPrefixCtr_main + "TabPanel1$txtitem_name.value+" +
                                                "'&item_group_name='+document.forms[0]." + strPrefixCtr_main + "TabPanel1$txtitem_group_name.value+" +
                                                "'&person_group_code=" + ViewState["person_group_code"].ToString() +
                                                "&ctrl1=" + txtitem_code.ClientID + "&ctrl2=" + txtitem_name.ClientID +
                                                "&ctrl3=" + txtitem_type.ClientID + "&ctrl4=" + txtitem_group_name.ClientID +
                                                "&ctrl5=" + txtlot_name.ClientID + "&show=3', '3');return false;");

                imgClear_item.Attributes.Add("onclick", "$('#" + txtitem_code.ClientID + "').val('');" +
                                        "$('#" + txtitem_name.ClientID + "').val('');" +
                                        "$('#" + txtitem_type.ClientID + "').val('');" +
                                        "$('#" + txtitem_group_name.ClientID + "').val('');" +
                                        "$('#" + txtlot_name.ClientID + "').val('');return false;");

                #endregion
                txtamount.Attributes.Add("onblur", "chkDecimal(this,2,',')");
                imgSaveOnly.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgSaveOnly"].Rows[0]["title"].ToString());
                chkStatus.Checked = true;
                txtitem_code.Focus();

            }
        }

        #region private function
        public static string getItemtype(object mData)
        {
            if (mData.Equals("D"))
            {
                return "Debit";
            }
            else
            {
                return "Credit";
            }
        }
        private void InitcboBudgetType()
        {
            cCommon oCommon = new cCommon();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strCode = cboBudget_type.SelectedValue;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " Select * from  general where g_type = 'budget_type'  and g_code <> 'M' Order by g_sort ";
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
        }
        private void InitcboLot()
        {
            cLot oLot = new cLot();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strLot_code = string.Empty;
            string strLot = cboLot.SelectedValue;
            string stryear = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["yearnow"].ToString();
            string strBudget_type = cboBudget_type.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and c_active='Y' ";
            strCriteria += "and lot_year='" + stryear + "' ";
            strCriteria += "and budget_type='" + strBudget_type + "' ";

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

        #endregion

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.imgSaveOnly.Click += new System.Web.UI.ImageClickEventHandler(this.imgSaveOnly_Click);
        }
        #endregion

        private bool saveData()
        {
            bool blnResult = false;
            bool blnDup = false;
            string strMessage = string.Empty;
            string strperson_code = string.Empty,
                strperson_item_year = string.Empty,
                stritem_code = string.Empty,
                stritem_name = string.Empty,
                stritem_debit = string.Empty,
                stritem_credit = string.Empty,
                strperson_item_tax = string.Empty,
                strperson_item_sos = string.Empty,
                strActive = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty;
            string strScript = string.Empty;
            cPerson oPerson = new cPerson();
            DataSet ds = new DataSet();
            try
            {
                #region set Data
                strperson_code = lblperson_code.Text.Trim();
                strperson_item_year = txtyear.Text.Trim();
                stritem_code = txtitem_code.Text.Trim();
                stritem_name = txtitem_name.Text.Trim();
                if (txtitem_type.Text.Equals("Debit"))
                {
                    stritem_debit = txtamount.Text;
                    stritem_credit = "0";
                }
                else
                {
                    stritem_debit = "0";
                    stritem_credit = txtamount.Text;
                }
                strperson_item_tax = "Y";
                strperson_item_sos = "Y";
                if (chkStatus.Checked == true)
                {
                    strActive = "Y";
                }
                else
                {
                    strActive = "N";
                }
                strCreatedBy = Session["username"].ToString();
                strUpdatedBy = Session["username"].ToString();
                #endregion
                if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    #region edit
                    if (!blnDup)
                    {
                        if (oPerson.SP_PERSON_ITEM_UPD(strperson_code, strperson_item_year, stritem_code, stritem_debit, stritem_credit, "Y", "Y",
                               strActive, strUpdatedBy, txtbudget_plan_code.Text, cboLot.SelectedValue, cboBudget_type.SelectedValue, ref strMessage))
                        {
                            blnResult = true;
                        }
                        else
                        {
                            lblError.Text = strMessage.ToString();
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "frMainPage", strScript, true);
                    }
                    #endregion
                }
                else
                {
                    #region check dup
                    string strCheckDup = string.Empty;
                    strCheckDup = " and person_code = '" + strperson_code.Trim() + "' " +
                                                  " and item_code = '" + stritem_code.Trim() + "' " +
                                                  " and person_item_year= '" + strperson_item_year.Trim() + "' ";
                    if (!oPerson.SP_PERSON_ITEM_SEL(strCheckDup, ref ds, ref strMessage))
                    {
                        lblError.Text = strMessage;
                    }
                    else
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            strScript =
                                "alert(\"ไม่สามารถเพิ่มข้อมูล เนื่องจากข้อมูล " + stritem_code.Trim() + " : " + stritem_name.Trim() + "  ซ้ำ\");\n";
                            blnDup = true;
                        }
                    }
                    #endregion
                    #region insert
                    if (!blnDup)
                    {
                        if (oPerson.SP_PERSON_ITEM_INS(strperson_code, strperson_item_year, stritem_code, stritem_debit, stritem_credit, "Y", "Y",
                            strActive, strCreatedBy, txtbudget_plan_code.Text, cboLot.SelectedValue, cboBudget_type.SelectedValue, ref strMessage))
                        {
                            ViewState["item_code"] = stritem_code;
                            ViewState["year"] = strperson_item_year;
                            blnResult = true;
                        }
                        else
                        {
                            lblError.Text = strMessage.ToString();
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "frMainPage", strScript, true);
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
                oPerson.Dispose();
            }
            return blnResult;
        }

        private void imgSaveOnly_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (saveData())
            {
                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    txtitem_code.Text = string.Empty;
                    txtitem_name.Text = string.Empty;
                    txtitem_type.Text = string.Empty;
                    txtitem_group_name.Text = string.Empty;
                    txtlot_name.Text = string.Empty;
                    txtamount.Text = "0.00";
                    txtitem_code.Focus();
                    string strScript1 = "window.parent.frames['iframeShow1'].__doPostBack('ctl00$ContentPlaceHolder1$BtnR1','');ClosePopUp('2');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
                }
                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    string strScript1 = "window.parent.frames['iframeShow1'].__doPostBack('ctl00$ContentPlaceHolder1$BtnR1','');ClosePopUp('2');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
                }
                MsgBox("บันทึกข้อมูลสมบูรณ์");
            }
        }

        private void setData()
        {
            cPerson oPerson = new cPerson();
            DataSet ds = new DataSet();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string stritem_code = string.Empty,
                stritem_name = string.Empty,
                strperson_item_year = string.Empty,
                stritem_type = string.Empty,
                stritem_group_code = string.Empty,
                stritem_group_name = string.Empty,
                stritem_credit = string.Empty,
                stritem_debit = string.Empty,
                strlot_code = string.Empty,
                strlot_name = string.Empty,
                strAmount = string.Empty,
                strYear = string.Empty,
                strC_active = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty,
                strCreatedDate = string.Empty,
                strbudget_plan_code = string.Empty,
                strbudget_name = string.Empty,
                strproduce_name = string.Empty,
                stractivity_name = string.Empty,
                strplan_name = string.Empty,
                strwork_name = string.Empty,
                strfund_name = string.Empty,
                strdirector_name = string.Empty,
                strunit_name = string.Empty,
                strbudget_plan_year = string.Empty,
                strperson_item_lot_code = string.Empty,
                strbudget_type = string.Empty,
                strUpdatedDate = string.Empty;

            try
            {
                strCriteria = " and person_code = '" + ViewState["person_code"].ToString() + "' " +
                                " and item_code = '" + ViewState["item_code"].ToString() + "' " +
                                " and person_item_year = '" + ViewState["year"].ToString() + "' ";
                if (!oPerson.SP_PERSON_ITEM_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region get Data
                        strYear = ds.Tables[0].Rows[0]["person_item_year"].ToString();
                        stritem_code = ds.Tables[0].Rows[0]["item_code"].ToString();
                        stritem_name = ds.Tables[0].Rows[0]["item_name"].ToString();
                        stritem_type = ds.Tables[0].Rows[0]["item_type"].ToString();
                        stritem_credit = ds.Tables[0].Rows[0]["item_credit"].ToString();
                        stritem_debit = ds.Tables[0].Rows[0]["item_debit"].ToString();
                        if (stritem_type.Equals("D"))
                        {
                            stritem_type = "Debit";
                            strAmount = String.Format("{0:0.00}", float.Parse(stritem_debit));
                        }
                        else
                        {
                            stritem_type = "Credit";
                            strAmount = String.Format("{0:0.00}", float.Parse(stritem_credit));
                        }
                        stritem_group_code = ds.Tables[0].Rows[0]["item_group_code"].ToString();
                        stritem_group_name = ds.Tables[0].Rows[0]["item_group_name"].ToString();
                        strlot_code = ds.Tables[0].Rows[0]["lot_code"].ToString();
                        strlot_name = ds.Tables[0].Rows[0]["lot_name"].ToString();
                        strC_active = ds.Tables[0].Rows[0]["c_active"].ToString();
                        strCreatedBy = ds.Tables[0].Rows[0]["c_created_by"].ToString();
                        strUpdatedBy = ds.Tables[0].Rows[0]["c_updated_by"].ToString();
                        strCreatedDate = ds.Tables[0].Rows[0]["d_created_date"].ToString();
                        strUpdatedDate = ds.Tables[0].Rows[0]["d_updated_date"].ToString();

                        strbudget_plan_code = ds.Tables[0].Rows[0]["budget_plan_code"].ToString();
                        strbudget_name = ds.Tables[0].Rows[0]["budget_plan_budget_name"].ToString();
                        strproduce_name = ds.Tables[0].Rows[0]["budget_plan_produce_name"].ToString();
                        stractivity_name = ds.Tables[0].Rows[0]["budget_plan_activity_name"].ToString();
                        strplan_name = ds.Tables[0].Rows[0]["budget_plan_plan_name"].ToString();
                        strwork_name = ds.Tables[0].Rows[0]["budget_plan_work_name"].ToString();
                        strfund_name = ds.Tables[0].Rows[0]["budget_plan_fund_name"].ToString();
                        strdirector_name = ds.Tables[0].Rows[0]["budget_plan_director_name"].ToString();
                        strunit_name = ds.Tables[0].Rows[0]["budget_plan_unit_name"].ToString();
                        strbudget_plan_year = ds.Tables[0].Rows[0]["budget_plan_year"].ToString();
                        strbudget_type = ds.Tables[0].Rows[0]["budget_type"].ToString();
                        strperson_item_lot_code = ds.Tables[0].Rows[0]["person_item_lot_code"].ToString();

                        #endregion

                        #region set Control

                        imgList_item.Visible = false;
                        imgClear_item.Visible = false;
                        txtitem_code.Text = stritem_code;
                        txtitem_name.Text = stritem_name;
                        txtitem_group_name.Text = stritem_group_name;
                        txtlot_name.Text = strlot_name;
                        txtitem_type.Text = stritem_type;
                        txtamount.Text = strAmount;

                        txtitem_code.ReadOnly = true;
                        txtitem_code.CssClass = "textboxdis";
                        txtitem_name.ReadOnly = true;
                        txtitem_name.CssClass = "textboxdis";
                        txtitem_group_name.ReadOnly = true;
                        txtitem_group_name.CssClass = "textboxdis";
                        txtlot_name.ReadOnly = true;
                        txtlot_name.CssClass = "textboxdis";
                        txtitem_type.ReadOnly = true;
                        txtitem_type.CssClass = "textboxdis";
                        if (strC_active.Equals("Y"))
                        {
                            txtamount.ReadOnly = false;
                            txtamount.CssClass = "numberbox";
                            chkStatus.Checked = false;
                        }
                        else
                        {
                            txtamount.ReadOnly = true;
                            txtamount.CssClass = "numberdis";
                            chkStatus.Checked = false;
                        }

                        if (cboBudget_type.Items.FindByValue(strbudget_type) != null)
                        {
                            cboBudget_type.SelectedIndex = -1;
                            cboBudget_type.Items.FindByValue(strbudget_type).Selected = true;
                        }

                        ChangeLabelBudget();
                        if (cboLot.Items.FindByValue(strperson_item_lot_code) != null)
                        {
                            cboLot.SelectedIndex = -1;
                            cboLot.Items.FindByValue(strperson_item_lot_code).Selected = true;
                        }

                        txtbudget_plan_code.Text = strbudget_plan_code;
                        txtbudget_name.Text = strbudget_name;
                        txtproduce_name.Text = strproduce_name;
                        txtactivity_name.Text = stractivity_name;
                        txtplan_name.Text = strplan_name;
                        txtwork_name.Text = strwork_name;
                        txtfund_name.Text = strfund_name;
                        txtdirector_name.Text = strdirector_name;
                        txtunit_name.Text = strunit_name;
                        txtbudget_plan_year.Text = strbudget_plan_year;

                        txtUpdatedBy.Text = strUpdatedBy;
                        txtUpdatedDate.Text = strUpdatedDate;
                        txtitem_code.Focus();
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        protected void cboBudget_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeLabelBudget();
        }

        private void ChangeLabelBudget()
        {

            string strBusget_type = cboBudget_type.SelectedValue;
            string strLovTitle = "ค้นหาข้อมูลผังงบประมาณประจำปี (เงินงบประมาณ)";
            if (strBusget_type == "R") strLovTitle = "ค้นหาข้อมูลผังงบประมาณประจำปี (เงินรายได้)";
            imgList_budget_plan.Attributes.Add("onclick", "OpenPopUp('950px','500px','94%','" + strLovTitle + "' ,'../lov/budget_plan_lov.aspx?budget_type=" + strBusget_type +
                                                                "&budget_plan_code='+document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtbudget_plan_code.value+'" +
                                                                "&budget_name='+document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtbudget_name.value+'" +
                                                                "&produce_name='+document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtproduce_name.value+'" +
                                                                "&activity_name='+document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtactivity_name.value+'" +
                                                                "&plan_name='+document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtplan_name.value+'" +
                                                                "&work_name='+document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtwork_name.value+'" +
                                                                "&fund_name='+document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtfund_name.value+'" +
                                                                "&director_name='+document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtdirector_name.value+'" +
                                                                "&unit_name='+document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtunit_name.value+'" +
                                                                "&budget_plan_year='+document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtbudget_plan_year.value+'" +
                                                                "&ctrl1=" + txtbudget_plan_code.ClientID +
                                                                "&ctrl2=" + txtbudget_name.ClientID +
                                                                "&ctrl3=" + txtproduce_name.ClientID +
                                                                "&ctrl4=" + txtactivity_name.ClientID +
                                                                "&ctrl5=" + txtplan_name.ClientID +
                                                                "&ctrl6=" + txtwork_name.ClientID +
                                                                "&ctrl7=" + txtfund_name.ClientID +
                                                                "&ctrl9=" + txtdirector_name.ClientID +
                                                                "&ctrl10=" + txtunit_name.ClientID +
                                                                "&ctrl11=" + txtbudget_plan_year.ClientID + "&show=3', '3');return false;");



            //if (strBusget_type == "B")
            //{
            //    Label54.Text = "แผนงบ :";
            //    Label55.Text = "ผลผลิต :";
            //    Label53.Text = "กิจกรรม :";
            //    Label56.Text = "แผนงาน :";
            //}
            //else
            //{
            //    Label54.Text = "แผนงาน :";
            //    Label55.Text = "งานหลัก :";
            //    Label53.Text = "งานรอง :";
            //    Label56.Text = "งานย่อย :";
            //}

            txtbudget_plan_code.Text = "";
            txtbudget_name.Text = "";
            txtproduce_name.Text = "";
            txtactivity_name.Text = "";
            txtplan_name.Text = "";
            txtwork_name.Text = "";
            txtfund_name.Text = "";
            txtdirector_name.Text = "";
            txtunit_name.Text = "";
            txtbudget_plan_year.Text = "";

            InitcboLot();
        }


    }
}