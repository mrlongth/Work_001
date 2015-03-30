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

namespace myWeb.App_Control.item
{
    public partial class item_control : PageBase
    {

        #region private data
        private bool[] blnAccessRight = new bool[5] { false, false, false, false, false };
        private string strPrefixCtr_main = "ctl00$ContentPlaceHolder1$";
        // private string strPrefixCtr = "ctl00$ASPxRoundPanel1$ASPxRoundPanel2$ContentPlaceHolder1$";
        #endregion

        protected void Page_Load(object sender, System.EventArgs e)
        {
            //if (Session["username"] == null)
            //{
            //    string strScript = "<script language=\"javascript\">\n self.opener.document.location.href=\"../../index.aspx\";\n self.close();\n</script>\n";
            //    this.RegisterStartupScript("close", strScript);
            //    return;
            //}
            lblError.Text = "";
            if (!IsPostBack)
            {
                imgSaveOnly.Attributes.Add("onMouseOver", "src='../../images/controls/save2.jpg'");
                imgSaveOnly.Attributes.Add("onMouseOut", "src='../../images/controls/save.jpg'");
                Session["menupopup_name"] = "";
                ViewState["sort"] = "item_code";
                ViewState["direction"] = "ASC";
                #region set QueryString
                if (Request.QueryString["item_code"] != null)
                {
                    ViewState["item_code"] = Request.QueryString["item_code"].ToString();
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

                imgList_item.Attributes.Add("onclick", "OpenPopUp('750px','400px','93%','ค้นหาข้อมูลเช็ค' ,'../lov/cheque_lov.aspx?cheque_code='+document.forms[0]." + strPrefixCtr_main + "txtcheque_code.value+'" +
                "&cheque_name='+document.forms[0]." + strPrefixCtr_main + "txtcheque_name.value+'" +
                "&ctrl1=" + txtcheque_code.ClientID + "&ctrl2=" + txtcheque_name.ClientID + "&show=2', '2');return false;");

                imgClear_item.Attributes.Add("onclick", "document.forms[0]." + strPrefixCtr_main + "txtcheque_code.value='';" +
                                        "document.forms[0]." + strPrefixCtr_main + "txtcheque_name.value=''; return false;");


                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    InitcboYear();
                    InitcboPerson_group();
                    InitcboItem_acc();
                    Session["menupopup_name"] = "เพิ่มข้อมูลผังงบประมาณ";
                    ViewState["page"] = Request.QueryString["page"];
                    txtitem_code.ReadOnly = true;
                    txtitem_code.CssClass = "textboxdis";
                    chkStatus.Checked = true;
                    txtitem_code.CssClass = "textboxdis";
                }
                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    setData();
                    txtitem_code.ReadOnly = true;
                    txtitem_code.CssClass = "textboxdis";
                }

                #endregion
                #region Set Image

                #endregion
                InitcboBudgetType();


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

        private void InitcboPerson_group()
        {
            cPerson_group oPerson_group = new cPerson_group();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strperson_group_code = string.Empty;
            strperson_group_code = cboPerson_group.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and c_active='Y' ";
            if (oPerson_group.SP_PERSON_GROUP_SEL(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboPerson_group.Items.Clear();
                cboPerson_group.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboPerson_group.Items.Add(new ListItem(dt.Rows[i]["person_group_name"].ToString(), dt.Rows[i]["person_group_code"].ToString()));
                }
                if (cboPerson_group.Items.FindByValue(strperson_group_code) != null)
                {
                    cboPerson_group.SelectedIndex = -1;
                    cboPerson_group.Items.FindByValue(strperson_group_code).Selected = true;
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
            strCriteria += "and lot_year='" + cboYear.SelectedValue + "' ";
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
                InitcboItem_group();
            }
        }

        private void InitcboItem_group()
        {
            cItem_group oItem_group = new cItem_group();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strItem_group_code = string.Empty;
            string strlot_code = cboLot.SelectedValue;
            string stritem_group_year = cboYear.SelectedValue;
            strItem_group_code = cboItem_group.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = "and item_group_year='" + stritem_group_year + "' ";
            strCriteria += "and lot_code='" + strlot_code + "' and c_active='Y' ";
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

        private void InitcboItem_acc()
        {
            cItem_acc oItem_acc = new cItem_acc();
            string strMessage = string.Empty,
                   strCriteria = string.Empty,
                   strItem_acc_code = string.Empty;
            strItem_acc_code = cboItem_acc.SelectedValue;
            string strYear = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["yearnow"].ToString();
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = "and item_acc_year='" + strYear + "' and c_active='Y' ";
            if (oItem_acc.SP_ITEM_ACC_SEL(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboItem_acc.Items.Clear();
                cboItem_acc.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboItem_acc.Items.Add(new ListItem(dt.Rows[i]["Item_acc_name"].ToString(), dt.Rows[i]["item_acc_id"].ToString()));
                }
                if (cboItem_acc.Items.FindByValue(strItem_acc_code) != null)
                {
                    cboItem_acc.SelectedIndex = -1;
                    cboItem_acc.Items.FindByValue(strItem_acc_code).Selected = true;
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
            strCriteria = " Select * from  general where g_type = 'budget_type'  Order by g_sort ";
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



        private void setData()
        {
            cItem oItem = new cItem();
            DataSet ds = new DataSet();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string stritem_code = string.Empty,
                stritem_name = string.Empty,
                stritem_year = string.Empty,
                stritem_type = string.Empty,
                stritem_group_code = string.Empty,
                stritem_group_name = string.Empty,
                strlot_code = string.Empty,
                strlot_name = string.Empty,
                strperson_group_code = string.Empty,
                strYear = string.Empty,
                strC_active = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty,
                strCreatedDate = string.Empty,
                strUpdatedDate = string.Empty,
                strcheque_code = string.Empty,
                strcheque_name = string.Empty,
                strcheque_type = string.Empty,
                stritem_acc_code = string.Empty,
                stritem_project_code1 = string.Empty,
                stritem_project_code2 = string.Empty;
            try
            {
                strCriteria = " and item_code = '" + ViewState["item_code"].ToString() + "' ";
                if (!oItem.SP_ITEM_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region get Data
                        strYear = ds.Tables[0].Rows[0]["item_year"].ToString();
                        stritem_code = ds.Tables[0].Rows[0]["item_code"].ToString();
                        stritem_name = ds.Tables[0].Rows[0]["item_name"].ToString();
                        stritem_type = ds.Tables[0].Rows[0]["item_type"].ToString();
                        stritem_group_code = ds.Tables[0].Rows[0]["item_group_code"].ToString();
                        stritem_group_name = ds.Tables[0].Rows[0]["item_group_name"].ToString();
                        strlot_code = ds.Tables[0].Rows[0]["lot_code"].ToString();
                        strlot_name = ds.Tables[0].Rows[0]["lot_name"].ToString();
                        strperson_group_code = ds.Tables[0].Rows[0]["person_group_code"].ToString();
                        strC_active = ds.Tables[0].Rows[0]["c_active"].ToString();
                        strCreatedBy = ds.Tables[0].Rows[0]["c_created_by"].ToString();
                        strUpdatedBy = ds.Tables[0].Rows[0]["c_updated_by"].ToString();
                        strCreatedDate = ds.Tables[0].Rows[0]["d_created_date"].ToString();
                        strUpdatedDate = ds.Tables[0].Rows[0]["d_updated_date"].ToString();
                        strcheque_code = ds.Tables[0].Rows[0]["cheque_code"].ToString();
                        strcheque_name = ds.Tables[0].Rows[0]["cheque_name"].ToString();
                        strcheque_type = ds.Tables[0].Rows[0]["cheque_type"].ToString();

                        stritem_acc_code = ds.Tables[0].Rows[0]["item_acc_id"].ToString();
                        stritem_project_code1 = ds.Tables[0].Rows[0]["item_project_code1"].ToString();
                        stritem_project_code2 = ds.Tables[0].Rows[0]["item_project_code2"].ToString();

                        #endregion

                        #region set Control
                        InitcboYear();
                        if (cboYear.Items.FindByValue(strYear) != null)
                        {
                            cboYear.SelectedIndex = -1;
                            cboYear.Items.FindByValue(strYear).Selected = true;
                        }
                        cboYear.Enabled = false;
                        txtitem_code.Text = stritem_code;
                        txtitem_name.Text = stritem_name;
                        txtitem_project_code1.Text = stritem_project_code1;
                        txtitem_project_code2.Text = stritem_project_code2;


                        if (cboItem_type.Items.FindByValue(stritem_type) != null)
                        {
                            cboItem_type.SelectedIndex = -1;
                            cboItem_type.Items.FindByValue(stritem_type).Selected = true;
                        }

                        InitcboPerson_group();
                        if (cboPerson_group.Items.FindByValue(strperson_group_code) != null)
                        {
                            cboPerson_group.SelectedIndex = -1;
                            cboPerson_group.Items.FindByValue(strperson_group_code).Selected = true;
                        }
                        txtcheque_code.Text = strcheque_code;
                        txtcheque_name.Text = strcheque_name;

                        if (cboCheque_type.Items.FindByValue(strcheque_type) != null)
                        {
                            cboCheque_type.SelectedIndex = -1;
                            cboCheque_type.Items.FindByValue(strcheque_type).Selected = true;
                        }
                        InitcboItem_acc();
                        if (cboItem_acc.Items.FindByValue(stritem_acc_code) != null)
                        {
                            cboItem_acc.SelectedIndex = -1;
                            cboItem_acc.Items.FindByValue(stritem_acc_code).Selected = true;
                        }

                        string strBudget_type = ds.Tables[0].Rows[0]["budget_type"].ToString();
                        InitcboBudgetType();
                        if (cboBudget_type.Items.FindByValue(strBudget_type) != null)
                        {
                            cboBudget_type.SelectedIndex = -1;
                            cboBudget_type.Items.FindByValue(strBudget_type).Selected = true;
                        }

                        txtdirect_pay_code.Text = ds.Tables[0].Rows[0]["direct_pay_code"].ToString();

                        if (strC_active.Equals("Y"))
                        {

                            cboItem_type.Enabled = true;
                            cboItem_type.CssClass = "textbox";

                            txtitem_name.ReadOnly = false;
                            txtitem_name.CssClass = "textbox";

                            cboItem_group.Enabled = true;
                            cboItem_group.CssClass = "textbox";

                            cboLot.Enabled = true;
                            cboLot.CssClass = "textbox";


                            txtcheque_code.ReadOnly = false;
                            txtcheque_code.CssClass = "textbox";

                            txtcheque_name.ReadOnly = false;
                            txtcheque_name.CssClass = "textbox";

                            chkStatus.Checked = true;
                        }
                        else
                        {
                            cboItem_type.Enabled = false;
                            cboItem_type.CssClass = "textboxdis";

                            txtitem_code.ReadOnly = true;
                            txtitem_code.CssClass = "textboxdis";

                            txtitem_name.ReadOnly = true;
                            txtitem_name.CssClass = "textboxdis";

                            cboItem_group.Enabled = false;
                            cboItem_group.CssClass = "textboxdis";

                            cboLot.Enabled = false;
                            cboLot.CssClass = "textboxdis";

                            txtcheque_code.ReadOnly = true;
                            txtcheque_code.CssClass = "textboxdis";

                            txtcheque_name.ReadOnly = true;
                            txtcheque_name.CssClass = "textboxdis";


                            chkStatus.Checked = false;
                        }
                        cboYear.CssClass = "textboxdis";

                        txtitem_code.ReadOnly = true;
                        txtitem_code.CssClass = "textboxdis";

                        if (stritem_type.Equals("D"))
                        {
                            InitcboLot();
                            if (cboLot.Items.FindByValue(strlot_code) != null)
                            {
                                cboLot.SelectedIndex = -1;
                                cboLot.Items.FindByValue(strlot_code).Selected = true;
                            }
                            InitcboItem_group();
                            if (cboItem_group.Items.FindByValue(stritem_group_code) != null)
                            {
                                cboItem_group.SelectedIndex = -1;
                                cboItem_group.Items.FindByValue(stritem_group_code).Selected = true;
                            }
                            cboLot.Enabled = true;
                            cboItem_group.Enabled = true;
                        }
                        else
                        {
                            cboLot.Enabled = false;
                            cboItem_group.Enabled = false;
                        }


                        txtUpdatedBy.Text = strUpdatedBy;
                        txtUpdatedDate.Text = strUpdatedDate;
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        private bool saveData()
        {
            bool blnResult = false;
            bool blnDup = false;
            bool blnDebit = false;
            string strMessage = string.Empty;
            string stritem_code = string.Empty,
                stritem_year = string.Empty,
                stritem_type = string.Empty,
                stritem_name = string.Empty,
                stritem_group_code = string.Empty,
                strlot_code = string.Empty,
                strperson_group_code = string.Empty,
                strActive = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty,
                strcheque_code = string.Empty,
                strcheque_type = string.Empty,
                stritem_acc_code = string.Empty,
                stritem_project_code1 = string.Empty,
                stritem_project_code2 = string.Empty,
                strdirect_pay_code = string.Empty;
            string strScript = string.Empty;
            cItem oItem = new cItem();
            DataSet ds = new DataSet();
            try
            {
                #region set Data
                stritem_code = txtitem_code.Text.Trim();
                stritem_year = cboYear.SelectedValue;
                stritem_name = txtitem_name.Text.Trim();
                stritem_type = cboItem_type.SelectedValue;
                stritem_group_code = cboItem_group.SelectedValue;
                strlot_code = cboLot.SelectedValue;
                strperson_group_code = cboPerson_group.SelectedValue;
                strcheque_code = txtcheque_code.Text;
                strcheque_type = cboCheque_type.SelectedValue;
                stritem_acc_code = cboItem_acc.SelectedValue;
                stritem_project_code1 = txtitem_project_code1.Text;
                stritem_project_code2 = txtitem_project_code2.Text;
                strdirect_pay_code = txtdirect_pay_code.Text;
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

                if (stritem_type.Equals("D"))
                {
                    if (strlot_code.Equals(""))
                    {
                        strScript = "alertjava(\"ประเภทรายการเป็น Debit ต้องระบะรหัสงบประมาณโปรดตรวจสอบ\");\n";
                        blnDebit = true;
                    }
                }

                if (!blnDebit)
                {
                    string strCheckAdd = " and item_code = '" + stritem_code.Trim() + "' ";
                    if (!oItem.SP_ITEM_SEL(strCheckAdd, ref ds, ref strMessage))
                    {
                        lblError.Text = strMessage;
                    }
                    else
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            #region check dup
                            string strCheckDup = string.Empty;
                            strCheckDup = " and item_name='" + stritem_name + "' " +
                                                          " and item_year='" + stritem_year + "' " +
                                                          " and item_code<>'" + stritem_code + "' ";
                            if (!oItem.SP_ITEM_SEL(strCheckDup, ref ds, ref strMessage))
                            {
                                lblError.Text = strMessage;
                            }
                            else
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    strScript =
                                        "alertjava(\"ไม่สามารถแก้ไขข้อมูลได้ เนื่องจาก ข้อมูลรายได้/ค่าใช้จ่ายซ้ำ\");\n";
                                    blnDup = true;
                                }
                            }
                            #endregion
                            #region edit
                            if (!blnDup)
                            {
                                if (oItem.SP_ITEM_UPD(stritem_code, stritem_year, stritem_name, stritem_type, stritem_group_code,
                                                      strlot_code, strperson_group_code, strActive, strUpdatedBy, strcheque_code,
                                                      strcheque_type, stritem_acc_code, stritem_project_code1,
                                                      stritem_project_code2, cboBudget_type.SelectedValue, strdirect_pay_code, ref strMessage))
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
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "chkdup", strScript, true);
                            }
                            #endregion
                        }
                        else
                        {
                            #region check dup
                            string strCheckDup = string.Empty;
                            strCheckDup = " and item_name='" + stritem_name + "' " +
                                                          " and item_year='" + stritem_year + "' ";
                            if (!oItem.SP_ITEM_SEL(strCheckDup, ref ds, ref strMessage))
                            {
                                lblError.Text = strMessage;
                            }
                            else
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    strScript =
                                        "alert(\"ไม่สามารถแก้ไขข้อมูลได้ เนื่องจาก ข้อมูลรายได้/ค่าใช้จ่ายซ้ำ\");\n";
                                    blnDup = true;
                                }
                            }
                            #endregion
                            #region insert
                            if (!blnDup)
                            {
                                if (oItem.SP_ITEM_INS(stritem_year, stritem_name, stritem_type, stritem_group_code,
                                                      strlot_code, strperson_group_code, strActive, strCreatedBy,
                                                      strcheque_code, strcheque_type, stritem_acc_code,
                                                      stritem_project_code1, stritem_project_code2,
                                                      cboBudget_type.SelectedValue, strdirect_pay_code, ref strMessage))
                                {
                                    string strGetcode = " and item_name='" + stritem_name + "' " +
                                                                          " and item_year='" + stritem_year + "' " +
                                                                          " and item_type='" + stritem_type + "' " +
                                                                          " and item_group_code='" + stritem_group_code + "' " +
                                                                          " and lot_code ='" + strlot_code + "' ";

                                    if (!oItem.SP_ITEM_SEL(strGetcode, ref ds, ref strMessage))
                                    {
                                        lblError.Text = strMessage;
                                    }
                                    if (ds.Tables[0].Rows.Count > 0)
                                    {
                                        stritem_code = ds.Tables[0].Rows[0]["item_code"].ToString();
                                    }
                                    ViewState["item_code"] = stritem_code;
                                    blnResult = true;
                                }
                                else
                                {
                                    lblError.Text = strMessage.ToString();
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "close", strScript, true);
                            }
                            #endregion
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "chk", strScript, true);
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oItem.Dispose();
            }
            return blnResult;
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

        private void imgSaveOnly_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (saveData())
            {
                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    txtitem_code.Text = string.Empty;
                    txtitem_name.Text = string.Empty;
                    cboItem_type.SelectedIndex = 0;
                    InitcboLot();
                    txtitem_name.Focus();
                    string strScript1 = "RefreshMain('" + ViewState["page"].ToString() + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
                }
                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    string strScript1 = "ClosePopUpListPost('" + ViewState["page"].ToString() + "','1');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
                }
                MsgBox("บันทึกข้อมูลสมบูรณ์");
            }
        }

        protected void cboItem_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboLot();
            if (cboItem_type.SelectedValue.Equals("C"))
            {
                cboLot.SelectedIndex = 0;
                cboLot.Enabled = false;
                //cboItem_group.SelectedIndex = 0;
                //cboItem_group.Enabled = false;

                //txtcheque_code.Enabled = true;
                //txtcheque_name.Enabled = true;
                //txtcheque_code.CssClass = "textbox";
                //txtcheque_name.CssClass = "textbox";
                //imgList_item.Visible = true;
                //imgClear_item.Visible = true;

            }
            else
            {
                cboLot.Enabled = true;
                cboItem_group.Enabled = true;

                //txtcheque_code.Enabled = false;
                //txtcheque_name.Enabled = false;
                //txtcheque_code.CssClass = "textboxdis";
                //txtcheque_name.CssClass = "textboxdis";
                //imgList_item.Visible = false;
                //imgClear_item.Visible = false;
                //txtcheque_name.Text = string.Empty;
                //txtcheque_code.Text = string.Empty;


            }
        }

        protected void cboLot_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboItem_group();
        }

    }
}