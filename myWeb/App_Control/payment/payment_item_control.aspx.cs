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

namespace myWeb.App_Control.payment
{
    public partial class payment_item_control : PageBase
    {
        #region private data
        private bool[] blnAccessRight = new bool[5] { false, false, false, false, false };
        private string strPrefixCtr = "ctl00$ContentPlaceHolder1$";
        private string strPrefixCtr_main = "ctl00$ContentPlaceHolder1$TabContainer1$";
        #endregion
        protected void Page_Load(object sender, System.EventArgs e)
        {
            lblError.Text = "";
            if (!IsPostBack)
            {
                #region set QueryString
                if (Request.QueryString["payment_doc"] != null)
                {
                    ViewState["payment_doc"] = Request.QueryString["payment_doc"].ToString();
                    lblpayment_doc.Text = ViewState["payment_doc"].ToString();
                }
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

                if (Request.QueryString["budget_type"] != null)
                {
                    ViewState["budget_type"] = Request.QueryString["budget_type"].ToString();
                }




                if (Request.QueryString["year"] != null)
                {
                    ViewState["year"] = Request.QueryString["year"].ToString();
                    txtyear.Text = ViewState["year"].ToString();
                }
                if (Request.QueryString["mode"] != null)
                {
                    ViewState["mode"] = Request.QueryString["mode"].ToString();
                }
                if (Request.QueryString["page"] != null)
                {
                    ViewState["page"] = Request.QueryString["page"].ToString();
                }
                if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    setData();
                }
                else
                {
                    //TabContainer1.Tabs[1].Visible = false;
                    chkStatus.Checked = true;
                    txtitem_code.Focus();
                    InitcboBudgetType();
                    ChangeLabelBudget();
                    InitcboLot();
                    InitcboPerson_group();
                }


                #endregion
                #region Set Image

                imgList_item.Attributes.Add("onclick", "OpenPopUp('800px','400px','93%','ค้นหาข้อมูลรายได้/ค่าใช้จ่าย' ,'../lov/item_lov.aspx?" +
                                                "item_code='+ $('#" + txtitem_code.ClientID + "').val()+" +
                                                "'&item_name='+ $('#" + txtitem_name.ClientID + "').val()+" +
                                                "'&item_group_name='+$('#" + txtitem_group_name.ClientID + "').val()+" +
                                                "'&item_type='+document.forms[0]." + strPrefixCtr_main + "TabPanel1$txtitem_type.value+" +
                                                "'&ctrl1=" + txtitem_code.ClientID + "&ctrl2=" + txtitem_name.ClientID +
                                                "&ctrl3=" + txtitem_type.ClientID + "&ctrl4=" + txtitem_group_name.ClientID +
                                                "&ctrl5=" + txtlot_name.ClientID + "&show=3&from=payment', '3');return false;");

                imgClear_item.Attributes.Add("onclick", "$('#" + txtitem_code.ClientID + "').val('')+" +
                                        "$('#" + txtitem_name.ClientID + "').val('')+" +
                                        "$('#" + txtitem_type.ClientID + "').val('')+" +
                                        "$('#" + txtitem_group_name.ClientID + "').val('')+" +
                                        "$('#" + txtlot_name.ClientID + "').val(''); return false;");

                #region Set Image

                //imgList_item.Attributes.Add("onclick", "OpenPopUp('800px','400px','93%','ค้นหาข้อมูลรายได้/ค่าใช้จ่าย' ,'../lov/item_lov.aspx?" +
                //                                "year='+document.forms[0]." + strPrefixCtr_main + "TabPanel1$txtyear.value+" +
                //                                "'&item_code='+document.forms[0]." + strPrefixCtr_main + "TabPanel1$txtitem_code.value+" +
                //                                "'&item_name='+document.forms[0]." + strPrefixCtr_main + "TabPanel1$txtitem_name.value+" +
                //                                "'&item_group_name='+document.forms[0]." + strPrefixCtr_main + "TabPanel1$txtitem_group_name.value+" +
                //                                "'&person_group_code=" + ViewState["person_group_code"].ToString() +
                //                                "&ctrl1=" + txtitem_code.ClientID + "&ctrl2=" + txtitem_name.ClientID +
                //                                "&ctrl3=" + txtitem_type.ClientID + "&ctrl4=" + txtitem_group_name.ClientID +
                //                                "&ctrl5=" + txtlot_name.ClientID + "&show=3', '3');return false;");

                //imgClear_item.Attributes.Add("onclick", "$('#" + txtitem_code.ClientID + "').val('');" +
                //                        "$('#" + txtitem_name.ClientID + "').val('');" +
                //                        "$('#" + txtitem_type.ClientID + "').val('');" +
                //                        "$('#" + txtitem_group_name.ClientID + "').val('');" +
                //                        "$('#" + txtlot_name.ClientID + "').val('');return false;");

                #endregion


                #endregion
                imgSaveOnly.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgSaveOnly"].Rows[0]["title"].ToString());
                txtitem_code.Focus();
                valValidationSummary.ShowMessageBox = true;
                valValidationSummary.ShowSummary = false;
                valValidationSummary.ValidationGroup = "A";
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
            string strpayment_doc = string.Empty,
                stritem_code = string.Empty,
                stritem_name = string.Empty,
                stritem_debit = string.Empty,
                stritem_credit = string.Empty,
                strperson_item_tax = string.Empty,
                strperson_item_sos = string.Empty,
                strperson_group = string.Empty,
                strcomments_sub = string.Empty,
                strActive = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty;
            string strScript = string.Empty;
            string strpayment_detail_id = "0";
            cPayment oPayment = new cPayment();
            DataSet ds = new DataSet();
            try
            {
                #region set Data
                strpayment_doc = lblpayment_doc.Text.Trim();
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
                strcomments_sub = txtcomments_sub.Text.Trim();
                strCreatedBy = Session["username"].ToString();
                strUpdatedBy = Session["username"].ToString();
                strperson_group = cboPerson_group.SelectedValue;
                strpayment_detail_id = hddpayment_detail_id.Value.ToString();
                #endregion
                if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    #region edit
                    if (!blnDup)
                    {
                        if (oPayment.SP_PAYMENT_DETAIL_UPD(strpayment_doc, stritem_code, stritem_debit, stritem_credit, "Y", "Y",
                                                            strcomments_sub, strActive, strUpdatedBy, cboBudget_type.SelectedValue, strpayment_detail_id, ref strMessage))
                        {
                            oPayment.SP_PAYMENT_DETAIL_BUDGET_UPD(strpayment_doc, stritem_code, txtbudget_plan_code.Text,
                                "N", cboLot.SelectedValue, txtbudget_plan_code.Text, cboLot.SelectedValue, cboPerson_group.Text,
                                cboBudget_type.SelectedValue, hddBudget_type_old.Value.ToString(), strpayment_detail_id, ref strMessage);
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
                    strCheckDup = " and payment_doc = '" + strpayment_doc + "' " +
                                  " and item_code = '" + stritem_code + "' " +
                                  " and person_group_code = '" + strperson_group + "' " +
                                  " and payment_detail_budget_type = '" + cboBudget_type.SelectedValue + "' ";

                    if (!oPayment.SP_PAYMENT_SEL(strCheckDup, ref ds, ref strMessage))
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
                        if (oPayment.SP_PAYMENT_DETAIL_BUDGET_INS(strpayment_doc, stritem_code, stritem_debit, stritem_credit, "Y", "Y",
                                 strcomments_sub, strActive, strCreatedBy, "N", txtbudget_plan_code.Text,
                                 "N", cboLot.SelectedValue, cboBudget_type.SelectedValue, strperson_group ,ref strMessage))
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
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oPayment.Dispose();
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
                    //txtitem_type.Text = string.Empty;
                    txtitem_group_name.Text = string.Empty;
                    txtlot_name.Text = string.Empty;
                    txtamount.Text = "0.00";
                    txtitem_code.Focus();
                    string strScript1 = "window.parent.frames['iframeShow1'].__doPostBack('ctl00$ContentPlaceHolder1$BtnR1','')";
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
            cPayment oPayment = new cPayment();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string strpayment_doc = string.Empty;
            string strperson_code = string.Empty;
            string strperson_name = string.Empty;
            string stritem_code = string.Empty,
                stritem_name = string.Empty,
                stritem_type = string.Empty,
                stritem_group_code = string.Empty,
                stritem_group_name = string.Empty,
                stritem_credit = string.Empty,
                stritem_debit = string.Empty,
                strlot_code = string.Empty,
                strlot_name = string.Empty,
                strAmount = string.Empty,
                strYear = string.Empty,
                strcommentsub = string.Empty,
                strC_active = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty,
                strCreatedDate = string.Empty,
                strUpdatedDate = string.Empty;
            string strpayment_detail_id = "0";

            try
            {
                strCriteria = " and payment_doc = '" + ViewState["payment_doc"].ToString() + "' " +
                                        " and item_code = '" + ViewState["item_code"].ToString() + "' " +
                                        " and payment_detail_budget_type = '" + ViewState["budget_type"].ToString() + "' ";
                if (!oPayment.SP_PAYMENT_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        TabContainer1.Tabs[1].Visible = true;
                        #region get Data
                        strpayment_doc = ds.Tables[0].Rows[0]["payment_doc"].ToString();
                        strperson_code = ds.Tables[0].Rows[0]["person_code"].ToString();
                        strperson_name = ds.Tables[0].Rows[0]["person_thai_name"].ToString() + "  " + ds.Tables[0].Rows[0]["person_thai_surname"].ToString();
                        strYear = ds.Tables[0].Rows[0]["payment_year"].ToString();
                        stritem_code = ds.Tables[0].Rows[0]["item_code"].ToString();
                        stritem_name = ds.Tables[0].Rows[0]["item_name"].ToString();
                        stritem_type = ds.Tables[0].Rows[0]["item_type"].ToString();
                        stritem_debit = ds.Tables[0].Rows[0]["payment_item_recv"].ToString();
                        stritem_credit = ds.Tables[0].Rows[0]["payment_item_pay"].ToString();
                        if (stritem_type.Equals("D"))
                        {
                            stritem_type = "Debit";
                            strAmount = String.Format("{0:#,##0.00}", float.Parse(stritem_debit));
                        }
                        else
                        {
                            stritem_type = "Credit";
                            strAmount = String.Format("{0:#,##0.00}", float.Parse(stritem_credit));
                        }

                        stritem_group_code = ds.Tables[0].Rows[0]["item_group_code"].ToString();
                        stritem_group_name = ds.Tables[0].Rows[0]["item_group_name"].ToString();
                        strlot_code = ds.Tables[0].Rows[0]["item_lot_code"].ToString();
                        strlot_name = ds.Tables[0].Rows[0]["item_lot_name"].ToString();
                        strcommentsub = ds.Tables[0].Rows[0]["comments_sub"].ToString();
                        strC_active = ds.Tables[0].Rows[0]["c_active_detail"].ToString();
                        strCreatedBy = ds.Tables[0].Rows[0]["c_created_by"].ToString();
                        strUpdatedBy = ds.Tables[0].Rows[0]["c_updated_by"].ToString();
                        strCreatedDate = ds.Tables[0].Rows[0]["d_created_date"].ToString();
                        strUpdatedDate = ds.Tables[0].Rows[0]["d_updated_date"].ToString();
                        strpayment_detail_id = ds.Tables[0].Rows[0]["payment_detail_id"].ToString();
                        #endregion

                        #region set Control

                        imgList_item.Enabled = false;
                        imgClear_item.Enabled = false;
                        lblpayment_doc.Text = strpayment_doc;
                        lblperson_code.Text = strperson_code;
                        lblperson_name.Text = strperson_name;

                        txtyear.Text = strYear;
                        txtitem_code.Text = stritem_code;
                        txtitem_name.Text = stritem_name;
                        txtitem_group_name.Text = stritem_group_name;
                        txtlot_name.Text = strlot_name;
                        txtitem_type.Text = stritem_type;
                        txtamount.Value = strAmount;

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
                            chkStatus.Checked = true;
                        }
                        else
                        {
                            txtamount.ReadOnly = true;
                            txtamount.CssClass = "numberdis";
                            chkStatus.Checked = false;
                        }

                        string strbudget_plan_code = ds.Tables[0].Rows[0]["payment_detail_budget_plan_code"].ToString();
                        string strperson_item_lot_code = ds.Tables[0].Rows[0]["payment_detail_lot_code"].ToString();
                        string strperson_group = ds.Tables[0].Rows[0]["payment_detail_person_group_code"].ToString();

                        string strbudget_name = ds.Tables[0].Rows[0]["payment_detail_budget_name"].ToString();
                        string strproduce_name = ds.Tables[0].Rows[0]["payment_detail_produce_name"].ToString();
                        string stractivity_name = ds.Tables[0].Rows[0]["payment_detail_activity_name"].ToString();
                        string strplan_name = ds.Tables[0].Rows[0]["payment_detail_plan_name"].ToString();
                        string strwork_name = ds.Tables[0].Rows[0]["payment_detail_work_name"].ToString();
                        string strfund_name = ds.Tables[0].Rows[0]["payment_detail_fund_name"].ToString();
                        string strdirector_name = ds.Tables[0].Rows[0]["payment_detail_director_name"].ToString();
                        string strunit_name = ds.Tables[0].Rows[0]["payment_detail_unit_name"].ToString();
                        string strbudget_plan_year = ds.Tables[0].Rows[0]["payment_detail_budget_plan_year"].ToString();
                        string strbudget_type = ds.Tables[0].Rows[0]["payment_detail_budget_type"].ToString();
                        InitcboBudgetType();
                        if (cboBudget_type.Items.FindByValue(strbudget_type) != null)
                        {
                            cboBudget_type.SelectedIndex = -1;
                            cboBudget_type.Items.FindByValue(strbudget_type).Selected = true;
                        }
                        hddBudget_type_old.Value = strbudget_type;
                        ChangeLabelBudget();
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

                        InitcboLot();
                        if (cboLot.Items.FindByValue(strperson_item_lot_code) != null)
                        {
                            cboLot.SelectedIndex = -1;
                            cboLot.Items.FindByValue(strperson_item_lot_code).Selected = true;
                        }

                        InitcboPerson_group();
                        if (cboPerson_group.Items.FindByValue(strperson_group) != null)
                        {
                            cboPerson_group.SelectedIndex = -1;
                            cboPerson_group.Items.FindByValue(strperson_group).Selected = true;
                        }

                        hddpayment_detail_id.Value = strpayment_detail_id;
                        txtcomments_sub.Text = strcommentsub;
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
                                                                "&budget_plan_code='+ $('#" + txtbudget_plan_code.ClientID + "').val()+'" +
                                                                "&budget_name='+$('#" + txtbudget_name.ClientID + "').val()+'" +
                                                                "&produce_name='+$('#" + txtproduce_name.ClientID + "').val()+'" +
                                                                "&activity_name='+$('#" + txtactivity_name.ClientID + "').val()+'" +
                                                                "&plan_name='+$('#" + txtplan_name.ClientID + "').val()+'" +
                                                                "&work_name='+$('#" + txtwork_name.ClientID + "').val()+'" +
                                                                "&fund_name='+$('#" + txtfund_name.ClientID + "').val()+'" +
                                                                "&director_name='+$('#" + txtdirector_name.ClientID + "').val()+'" +
                                                                "&unit_name='+$('#" + txtunit_name.ClientID + "').val()+'" +
                                                                "&budget_plan_year='+$('#" + txtbudget_plan_year.ClientID + "').val()+'" +
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

            imgClear_budget_plan.Attributes.Add("onclick", "$('#" + txtbudget_plan_code.ClientID + "').val('');" +
                                                            "$('#" + txtbudget_name.ClientID + "').val('');" +
                                                            "$('#" + txtproduce_name.ClientID + "').val('');" +
                                                            "$('#" + txtactivity_name.ClientID + "').val('');" +
                                                            "$('#" + txtplan_name.ClientID + "').val('');" +
                                                            "$('#" + txtwork_name.ClientID + "').val('');" +
                                                            "$('#" + txtfund_name.ClientID + "').val('');" +
                                                            "$('#" + txtdirector_name.ClientID + "').val('');" +
                                                            "$('#" + txtunit_name.ClientID + "').val('');" +
                                                            "$('#" + txtbudget_plan_year.ClientID + "').val('');return false;");

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

        private void InitcboBudgetType()
        {
            cCommon oCommon = new cCommon();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strCode = cboBudget_type.SelectedValue;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            if (strCode == "")
            {
                strCode = base.myBudgetType;
            }
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

    }
}