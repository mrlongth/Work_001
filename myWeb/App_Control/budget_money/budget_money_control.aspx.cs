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
using Aware.WebControls;
using myDLL;

namespace myWeb.App_Control.budget_money
{
    public partial class budget_money_control : PageBase
    {
        #region private data
        private string strPrefixCtr_main = "ctl00$ContentPlaceHolder1";
        #endregion

        private string BudgetType
        {
            get
            {
                if (ViewState["BudgetType"] == null)
                {
                    ViewState["BudgetType"] = Helper.CStr(Request.QueryString["budget_type"]);
                }
                return ViewState["BudgetType"].ToString();
            }
            set
            {
                ViewState["BudgetType"] = value;
            }
        }
        
        public static string getNumber(object pNumber)
        {
            string strNumber = String.Format("{0:#,##0.00}", float.Parse(pNumber.ToString()));
            return strNumber;
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            lblError.Text = "";
            if (!IsPostBack)
            {
                imgSaveOnly.Attributes.Add("onMouseOver", "src='../../images/controls/save2.jpg'");
                imgSaveOnly.Attributes.Add("onMouseOut", "src='../../images/controls/save.jpg'");


                imgClear.Attributes.Add("onMouseOver", "src='../../images/controls/clear2.jpg'");
                imgClear.Attributes.Add("onMouseOut", "src='../../images/controls/clear.jpg'");

                Session["menupopup_name"] = "";
                ViewState["sort"] = "lot_code";
                ViewState["direction"] = "ASC";
                #region set QueryString
                if (Request.QueryString["budget_money_doc"] != null)
                {
                    ViewState["budget_money_doc"] = Request.QueryString["budget_money_doc"].ToString();
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

                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    InitcboYear();
                    txtbudget_money_date.Text = cCommon.CheckDate(DateTime.Now.ToShortDateString());
                    ViewState["page"] = Request.QueryString["page"];
                    txtbudget_money_doc.CssClass = "textboxdis";
                    CheckBox2.Visible = false;
                }
                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    setData();
                    txtbudget_money_doc.ReadOnly = true;
                    txtbudget_money_doc.CssClass = "textboxdis";
                    CheckBox2.Visible = true;
                }

                #endregion
                imgList_budget_plan.Attributes.Add("onclick", "OpenPopUp('950px','500px','94%','ค้นหาข้อมูลผังงบประมาณประจำปี' ,'../lov/budget_plan_lov.aspx?budget_type=" + this.BudgetType + "&from_page=budgetmoney" +
                                                "&budget_plan_code='+document.forms[0]." + strPrefixCtr_main + "$txtbudget_plan_code.value+'" +
                                                "&budget_name='+document.forms[0]." + strPrefixCtr_main + "$txtbudget_name.value+'" +
                                                "&produce_name='+document.forms[0]." + strPrefixCtr_main + "$txtproduce_name.value+'" +
                                                "&activity_name='+document.forms[0]." + strPrefixCtr_main + "$txtactivity_name.value+'" +
                                                "&plan_name='+document.forms[0]." + strPrefixCtr_main + "$txtplan_name.value+'" +
                                                "&work_name='+document.forms[0]." + strPrefixCtr_main + "$txtwork_name.value+'" +
                                                "&fund_name='+document.forms[0]." + strPrefixCtr_main + "$txtfund_name.value+'" +
                    //   "&lot_name='+document.forms[0]." + strPrefixCtr_main + "$txtlot_name.value+'" +
                                                "&director_name='+document.forms[0]." + strPrefixCtr_main + "$txtdirector_name.value+'" +
                                                "&unit_name='+document.forms[0]." + strPrefixCtr_main + "$txtunit_name.value+'" +
                                                "&ctrl1=" + txtbudget_plan_code.ClientID +
                                                "&ctrl2=" + txtbudget_name.ClientID +
                                                "&ctrl3=" + txtproduce_name.ClientID +
                                                "&ctrl4=" + txtactivity_name.ClientID +
                                                "&ctrl5=" + txtplan_name.ClientID +
                                                "&ctrl6=" + txtwork_name.ClientID +
                                                "&ctrl7=" + txtfund_name.ClientID +
                    //    "&ctrl8=" + txtlot_name.ClientID +
                                                "&ctrl9=" + txtdirector_name.ClientID +
                                                "&ctrl10=" + txtunit_name.ClientID +
                                                "&show=2', '2');return false;");

                imgClear_budget_plan.Attributes.Add("onclick",
                                                                "document.forms[0]." + strPrefixCtr_main + "$txtbudget_plan_code.value='';" +
                                                                "document.forms[0]." + strPrefixCtr_main + "$txtbudget_name.value='';" +
                                                                "document.forms[0]." + strPrefixCtr_main + "$txtproduce_name.value='';" +
                                                                "document.forms[0]." + strPrefixCtr_main + "$txtactivity_name.value='';" +
                                                                "document.forms[0]." + strPrefixCtr_main + "$txtplan_name.value='';" +
                                                                "document.forms[0]." + strPrefixCtr_main + "$txtwork_name.value='';" +
                                                                "document.forms[0]." + strPrefixCtr_main + "$txtfund_name.value='';" +
                    //   "document.forms[0]." + strPrefixCtr_main + "$txtlot_name.value='';" +
                                                                "document.forms[0]." + strPrefixCtr_main + "$txtdirector_name.value='';" +
                                                                "document.forms[0]." + strPrefixCtr_main + "$txtunit_name.value='';" +
                                                                "return false;");

                BtnR1.Style.Add("display", "none");
                LinkButton1.Style.Add("display", "none");
                txtlot_name.Style.Add("display", "none");

                //if (this.BudgetType == "R")
                //{
                //    foreach (Control c in Page.Controls)
                //    {
                //        base.SetLabel(c, "แผนงาน", "งานย่อย");
                //        base.SetLabel(c, "กิจกรรม", "งานรอง");
                //        base.SetLabel(c, "แผนงบประมาณ", "แผนงาน");
                //        base.SetLabel(c, "ผลผลิต", "งานหลัก");
                //    }
                //}

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

        #endregion

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            base.OnInit(e);
        }



        #endregion

        private bool saveData()
        {
            bool blnResult = false;
            bool blnDup = false;
            string strMessage = string.Empty;
            //Head
            string strbudget_money_doc = string.Empty;
            string strbudget_money_date = string.Empty;
            string strbudget_money_year = string.Empty;
            string strbudget_plan_code = string.Empty;
            string strbudget_money_all = string.Empty;
            string strbudget_money_adjust = string.Empty;
            string strbudget_money_use = string.Empty;
            string strbudget_money_remain = string.Empty;
            string strcomments = string.Empty;
            //Detail
            string stritem_group_code = string.Empty;
            string strbudget_item_group_code = string.Empty;
            string strlot_code = string.Empty;
            string strbudget_money_suball = string.Empty;
            string strbudget_money_subadjust = string.Empty;
            string strbudget_money_subuse = string.Empty;
            string strbudget_money_subremain = string.Empty;
            string strActive = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty;
            string strScript = string.Empty;
            cBudget_money oBudget_money = new cBudget_money();
            DataSet ds = new DataSet();
            try
            {
                #region set Data
                strbudget_money_doc = txtbudget_money_doc.Text;
                strbudget_money_date = txtbudget_money_date.Text.Trim();
                strbudget_money_year = cboYear.SelectedValue;
                strbudget_money_all = txtbudget_money_all.Value.ToString();
                strbudget_money_adjust = txtbudget_money_adjust.Value.ToString();
                strbudget_money_use = txtbudget_money_use.Value.ToString();
                strbudget_money_remain = txtbudget_money_remain.Value.ToString();
                strbudget_plan_code = txtbudget_plan_code.Text;
                strcomments = txtcomments.Text;
                strCreatedBy = Session["username"].ToString();
                strUpdatedBy = Session["username"].ToString();
                #endregion

                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    #region insert
                    string strCheckDup = string.Empty;
                    strCheckDup = " and budget_plan_code = '" + strbudget_plan_code + "' and budget_money_year = '" + strbudget_money_year + "' ";
                    if (!oBudget_money.SP_BUDGET_MONEY_HEAD_SEL(strCheckDup, ref ds, ref strMessage))
                    {
                        lblError.Text = strMessage;
                    }
                    else
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            blnDup = true;
                            strScript =
                                "CalAmount();alert(\"ไม่สามารถเพิ่มข้อมูลได้ เนื่องจาก" +
                                "\\n ปี : " + strbudget_money_year.Trim() +
                                "\\nซ้ำ\");\n";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript, true);
                        }
                        else
                        {
                            if (!oBudget_money.sp_BUDGET_MONEY_INS(ref strbudget_money_doc, strbudget_money_date, strbudget_money_year, strbudget_plan_code, strbudget_money_all,strbudget_money_adjust,
                                                                                                                        strbudget_money_use, strbudget_money_remain, strcomments, "Y", strCreatedBy, ref strMessage))
                            {
                                lblError.Text = strMessage;
                            }
                            else
                            {
                                DataSet dsCHK = new DataSet();
                                strCheckDup = " and budget_plan_code = '" + strbudget_plan_code + "' and budget_money_year = '" + strbudget_money_year + "' ";
                                if (!oBudget_money.SP_BUDGET_MONEY_HEAD_SEL(strCheckDup, ref dsCHK, ref strMessage))
                                {
                                    lblError.Text = strMessage;
                                }
                                else
                                {
                                    strbudget_money_doc = dsCHK.Tables[0].Rows[0]["budget_money_doc"].ToString();
                                    ViewState["budget_money_doc"] = strbudget_money_doc;
                                }
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    #region update
                    if (!oBudget_money.SP_BUDGET_MONEY_UPD(strbudget_money_doc, strbudget_money_date, strbudget_money_year, strbudget_plan_code, strbudget_money_all,strbudget_money_adjust,
                                                                strbudget_money_use, strbudget_money_remain, strcomments, "Y", strUpdatedBy, ref strMessage))
                    {
                        lblError.Text = strMessage;

                    }
                    #endregion
                }
                if (!blnDup)
                {
                    #region insert detail
                    if (!oBudget_money.SP_BUDGET_MONEY_DETAIL_DEL(strbudget_money_doc, "Y", strUpdatedBy, ref strMessage))
                    {
                        lblError.Text = strMessage;
                    }

                    GridViewRow gviewRow;
                    string strChk;
                    int i;
                    for (i = 0; i <= GridView1.Rows.Count - 1; i++)
                    {
                        gviewRow = GridView1.Rows[i];
                        CheckBox CheckBox1 = (CheckBox)gviewRow.FindControl("CheckBox1");
                        Label lbllot_code = (Label)gviewRow.FindControl("lbllot_code");
                        Label lblitem_group_code = (Label)gviewRow.FindControl("lblitem_group_code");
                        Label lblbudget_item_group_code = (Label)gviewRow.FindControl("lblbudget_item_group_code");

                        AwNumeric txtbudget_money_suball = (AwNumeric)gviewRow.FindControl("txtbudget_money_suball");
                        AwNumeric txtbudget_money_subadjust = (AwNumeric)gviewRow.FindControl("txtbudget_money_subadjust");
                        AwNumeric txtbudget_money_subuse = (AwNumeric)gviewRow.FindControl("txtbudget_money_subuse");
                        AwNumeric txtbudget_money_subremain = (AwNumeric)gviewRow.FindControl("txtbudget_money_subremain");
                        stritem_group_code = lblitem_group_code.Text;
                        strlot_code = lbllot_code.Text;
                        strbudget_item_group_code = lblbudget_item_group_code.Text;
                        strbudget_money_suball = txtbudget_money_suball.Value.ToString();
                        strbudget_money_subadjust = txtbudget_money_subadjust.Value.ToString();
                        strbudget_money_subuse = txtbudget_money_subuse.Value.ToString();
                        strbudget_money_subremain = txtbudget_money_subremain.Value.ToString();
                        if (CheckBox1.Checked)
                        {
                            DataSet dsDetail = new DataSet();
                            strChk = " And budget_money_doc='" + strbudget_money_doc + "' " +
                                             " And lot_code = '" + strlot_code + "' and item_group_code = '" + stritem_group_code + "' " +
                                             " And budget_item_group_code = '" + strbudget_item_group_code + "' ";
                            if (!oBudget_money.SP_BUDGET_MONEY_SEL(strChk, ref dsDetail, ref strMessage))
                            {
                                lblError.Text = strMessage;
                            }
                            else
                            {
                                if (dsDetail.Tables[0].Rows.Count > 0)
                                {
                                    if (!oBudget_money.SP_BUDGET_MONEY_DETAIL_UPD(strbudget_money_doc, stritem_group_code, strlot_code,strbudget_item_group_code, strbudget_money_suball,strbudget_money_subadjust, strbudget_money_subuse,
                                                                                                                                strbudget_money_subremain, "Y", strUpdatedBy, ref strMessage))
                                    {
                                        lblError.Text = strMessage;
                                    }
                                }
                                else
                                {
                                    if (!oBudget_money.SP_BUDGET_MONEY_DETAIL_INS(strbudget_money_doc, stritem_group_code, strlot_code,strbudget_item_group_code, strbudget_money_suball,strbudget_money_subadjust, strbudget_money_subuse,
                                                                                                                                strbudget_money_subremain, "Y", strCreatedBy, ref strMessage))
                                    {
                                        lblError.Text = strMessage;
                                    }
                                }
                            }
                            dsDetail.Dispose();
                        }
                    }
                    #endregion
                    blnResult = true;
                }

            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oBudget_money.Dispose();
            }
            return blnResult;
        }

        private void setData()
        {
            imgClear.Visible = false;
            cBudget_money oBudget_money = new cBudget_money();
            DataSet ds = new DataSet();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strbudget_money_year = string.Empty,
                strC_active = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty,
                strCreatedDate = string.Empty,
                strUpdatedDate = string.Empty;
            try
            {
                strCriteria = " and budget_money_doc = '" + ViewState["budget_money_doc"].ToString() + "' ";
                if (!oBudget_money.SP_BUDGET_MONEY_HEAD_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region get Data
                        txtbudget_money_doc.Text = ds.Tables[0].Rows[0]["budget_money_doc"].ToString();
                        txtbudget_money_date.Text = cCommon.CheckDate(ds.Tables[0].Rows[0]["budget_money_date"].ToString());
                        strbudget_money_year = ds.Tables[0].Rows[0]["budget_money_year"].ToString();
                        InitcboYear();
                        if (cboYear.Items.FindByValue(strbudget_money_year) != null)
                        {
                            cboYear.SelectedIndex = -1;
                            cboYear.Items.FindByValue(strbudget_money_year).Selected = true;
                        }

                        txtbudget_plan_code.Text = ds.Tables[0].Rows[0]["budget_plan_code"].ToString();
                        txtbudget_name.Text = ds.Tables[0].Rows[0]["budget_name"].ToString();
                        txtproduce_name.Text = ds.Tables[0].Rows[0]["produce_name"].ToString();
                        txtactivity_name.Text = ds.Tables[0].Rows[0]["activity_name"].ToString();
                        txtplan_name.Text = ds.Tables[0].Rows[0]["plan_name"].ToString();
                        txtwork_name.Text = ds.Tables[0].Rows[0]["work_name"].ToString();
                        txtfund_name.Text = ds.Tables[0].Rows[0]["fund_name"].ToString();
                        //  txtlot_name.Text = ds.Tables[0].Rows[0]["lot_name"].ToString();
                        txtdirector_name.Text = ds.Tables[0].Rows[0]["director_name"].ToString();
                        txtunit_name.Text = ds.Tables[0].Rows[0]["unit_name"].ToString();
                        txtbudget_money_all.Value = ds.Tables[0].Rows[0]["budget_money_all"].ToString();
                        txtbudget_money_use.Value = ds.Tables[0].Rows[0]["budget_money_use"].ToString();
                        txtbudget_money_remain.Value = ds.Tables[0].Rows[0]["budget_money_remain"].ToString();
                        txtcomments.Text = ds.Tables[0].Rows[0]["comments"].ToString();
                        strCreatedBy = ds.Tables[0].Rows[0]["c_created_by"].ToString();
                        strUpdatedBy = ds.Tables[0].Rows[0]["c_updated_by"].ToString();
                        strCreatedDate = ds.Tables[0].Rows[0]["d_created_date"].ToString();
                        strUpdatedDate = ds.Tables[0].Rows[0]["d_updated_date"].ToString();
                        #endregion

                        #region set Control

                        txtbudget_money_doc.CssClass = "textboxdis";
                        cboYear.Enabled = false;
                        cboYear.CssClass = "textboxdis";
                        txtUpdatedBy.Text = strUpdatedBy;
                        txtUpdatedDate.Text = strUpdatedDate;
                        BindGridView();
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        private void BindGridView()
        {
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string strunit_code = string.Empty;
            cItem_group oItem_group = new cItem_group();
            cBudget_money oBudget_money = new cBudget_money();
            DataSet ds = new DataSet();
            int i;
            try
            {
                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    strCriteria = "   And  lot_code<> '' and item_group_code <> '' ";
                    strCriteria += "  And  item_year = '" + cboYear.SelectedValue + "' ";
                    strCriteria += "  And  budget_type ='" + this.BudgetType + "' ";

                    if (!oItem_group.SP_BUDGET_ITEM_GROUP_SEL(strCriteria, ref ds, ref strMessage))
                    {
                        lblError.Text = strMessage;
                    }
                    else
                    {
                        ds.Tables[0].Columns.Add("budget_money_suball");
                        ds.Tables[0].Columns.Add("budget_money_subadjust");
                        ds.Tables[0].Columns.Add("budget_money_subuse");
                        ds.Tables[0].Columns.Add("budget_money_subremain");
                        ds.Tables[0].Columns.Add("budget_money_has");
                        for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                        {
                            ds.Tables[0].Rows[i]["budget_money_has"] = "Y";
                            ds.Tables[0].Rows[i]["budget_money_suball"] = "0";
                            ds.Tables[0].Rows[i]["budget_money_subadjust"] = "0";
                            ds.Tables[0].Rows[i]["budget_money_subuse"] = "0";
                            ds.Tables[0].Rows[i]["budget_money_subremain"] = "0";
                        }
                        ds.Tables[0].DefaultView.Sort = ViewState["sort"] + " " + ViewState["direction"];
                        GridView1.DataSource = ds.Tables[0];
                        GridView1.DataBind();
                    }
                }
                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    if (CheckBox2.Checked)
                    {
                        strCriteria = "   And  lot_code<> '' and item_group_code <> '' ";
                        strCriteria += "  And  item_year = '" + cboYear.SelectedValue + "' ";
                        strCriteria += "  And  budget_type IN ('M','" + this.BudgetType + "') ";
                        if (!oItem_group.SP_BUDGET_ITEM_GROUP_SEL(strCriteria, ref ds, ref strMessage))
                        {
                            lblError.Text = strMessage;
                        }
                        else
                        {
                            ds.Tables[0].Columns.Add("budget_money_has");
                            ds.Tables[0].Columns.Add("budget_money_suball");
                            ds.Tables[0].Columns.Add("budget_money_subadjust");
                            ds.Tables[0].Columns.Add("budget_money_subuse");
                            ds.Tables[0].Columns.Add("budget_money_subremain");
                            for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                            {
                                DataSet dsChk = new DataSet();
                                strCriteria = " and budget_money_doc = '" + ViewState["budget_money_doc"].ToString() + "' " +
                                                       " and item_group_code = '" + ds.Tables[0].Rows[i]["item_group_code"].ToString() + "' " +
                                                       " and lot_code = '" + ds.Tables[0].Rows[i]["lot_code"].ToString() + "' ";
                                if (!oBudget_money.SP_BUDGET_MONEY_SEL(strCriteria, ref dsChk, ref strMessage))
                                {
                                    lblError.Text = strMessage;
                                }
                                else
                                {
                                    if (dsChk.Tables[0].Rows.Count > 0)
                                    {
                                        ds.Tables[0].Rows[i]["budget_money_has"] = "Y";
                                        ds.Tables[0].Rows[i]["budget_money_suball"] = dsChk.Tables[0].Rows[0]["budget_money_suball"];
                                        ds.Tables[0].Rows[i]["budget_money_subuse"] = dsChk.Tables[0].Rows[0]["budget_money_subuse"];
                                        ds.Tables[0].Rows[i]["budget_money_subremain"] = dsChk.Tables[0].Rows[0]["budget_money_subremain"];
                                        ds.Tables[0].Rows[i]["budget_money_subadjust"] = dsChk.Tables[0].Rows[0]["budget_money_subadjust"];
                                    }
                                    else
                                    {
                                        ds.Tables[0].Rows[i]["budget_money_has"] = "N";
                                        ds.Tables[0].Rows[i]["budget_money_suball"] = "0";
                                        ds.Tables[0].Rows[i]["budget_money_subuse"] = "0";
                                        ds.Tables[0].Rows[i]["budget_money_subremain"] = "0";
                                        ds.Tables[0].Rows[i]["budget_money_subadjust"] = "0";
                                    }

                                }
                            }
                            ds.Tables[0].DefaultView.Sort = ViewState["sort"] + " " + ViewState["direction"];
                            GridView1.DataSource = ds.Tables[0];
                            GridView1.DataBind();
                        }
                    }
                    else
                    {
                        strCriteria = " and budget_money_doc = '" + ViewState["budget_money_doc"].ToString() + "' ";
                        if (!oBudget_money.SP_BUDGET_MONEY_SEL(strCriteria, ref ds, ref strMessage))
                        {
                            lblError.Text = strMessage;
                        }
                        else
                        {
                            ds.Tables[0].Columns.Add("budget_money_has");
                            for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                            {
                                ds.Tables[0].Rows[i]["budget_money_has"] = "Y";
                            }
                            ds.Tables[0].DefaultView.Sort = ViewState["sort"] + " " + ViewState["direction"];
                            GridView1.DataSource = ds.Tables[0];
                            GridView1.DataBind();
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
                oItem_group.Dispose();
                ds.Dispose();
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.Header))
            {
                ((CheckBox)e.Row.FindControl("cbSelectAll")).Attributes.Add("onclick", "javascript:SelectAll('" +
                 ((CheckBox)e.Row.FindControl("cbSelectAll")).ClientID + "')");

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
                int nNo = (GridView1.PageSize * GridView1.PageIndex) + e.Row.RowIndex + 1;
                lblNo.Text = nNo.ToString();
                CheckBox CheckBox1 = (CheckBox)e.Row.FindControl("CheckBox1");
                TextBox txtbudget_money_suball = (TextBox)e.Row.FindControl("txtbudget_money_suball");
                TextBox txtbudget_money_subadjust = (TextBox)e.Row.FindControl("txtbudget_money_subadjust");
                TextBox txtbudget_money_subuse = (TextBox)e.Row.FindControl("txtbudget_money_subuse");
                txtbudget_money_suball.Attributes.Add("onblur", "CalAmount();");
                txtbudget_money_subadjust.Attributes.Add("onblur", "CalAmount();");
                txtbudget_money_subuse.Attributes.Add("onblur", "CalAmount();");

                txtbudget_money_suball.Attributes.Add("onkeyup", "CalAmount();");
                txtbudget_money_subadjust.Attributes.Add("onkeyup", "CalAmount();");
                txtbudget_money_subuse.Attributes.Add("onkeyup", "CalAmount();");


                if (CheckBox1.Checked)
                {
                    txtbudget_money_suball.Enabled = true;
                    txtbudget_money_subadjust.Enabled = true;
                    txtbudget_money_subuse.Enabled = true;
                }
                else
                {
                    txtbudget_money_suball.Enabled = false;
                    txtbudget_money_subadjust.Enabled = false;
                    txtbudget_money_subuse.Enabled = false;
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
                BindGridView();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        protected void imgClear_Click(object sender, ImageClickEventArgs e)
        {
            txtbudget_money_doc.Enabled = true;
            txtbudget_money_doc.CssClass = "textboxdis";
            txtbudget_money_doc.Text = string.Empty;

            cboYear.Enabled = true;
            cboYear.CssClass = "textbox";

            txtbudget_plan_code.Text = string.Empty;
            txtbudget_name.Text = string.Empty;
            txtproduce_name.Text = string.Empty;
            txtactivity_name.Text = string.Empty;
            txtplan_name.Text = string.Empty;
            txtwork_name.Text = string.Empty;
            txtfund_name.Text = string.Empty;
            txtlot_name.Text = string.Empty;
            txtdirector_name.Text = string.Empty;
            txtunit_name.Text = string.Empty;
            txtbudget_money_all.Value = 0;
            txtbudget_money_use.Value = 0;
            txtbudget_money_remain.Value = 0;

            txtcomments.Text = string.Empty;
            txtbudget_money_date.Text = cCommon.CheckDate(DateTime.Now.ToShortDateString());

            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            cBudget_money oBudget_money = new cBudget_money();
            DataSet ds = new DataSet();
            try
            {
                strCriteria = " and 1=2";
                if (!oBudget_money.SP_BUDGET_MONEY_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    ds.Tables[0].DefaultView.Sort = ViewState["sort"] + " " + ViewState["direction"];
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oBudget_money.Dispose();
                ds.Dispose();
            }
        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboYear.Enabled = false;
            BindGridView();
        }

        protected void cboDirector_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboYear.Enabled = false;
            BindGridView();
        }

        protected void cboUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridView();
            cboYear.Enabled = false;
        }

        protected void BtnR1_Click(object sender, EventArgs e)
        {
            BindGridView();
        }

        protected void imgSaveOnly_Click(object sender, ImageClickEventArgs e)
        {
            int intX = 0;
            for (int intCount = 0; intCount <= (GridView1.Rows.Count - 1); intCount++)
            {
                GridViewRow row = GridView1.Rows[intCount];
                CheckBox chkImportID = (CheckBox)row.FindControl("CheckBox1");
                if (chkImportID.Checked)
                {
                    intX += intX + 1;
                }
            }
            if (intX == 0)
            {
                string strScript = "alert('กรุณาเลือกข้อมูล');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowError", strScript, true);
            }
            else
            {
                if (saveData())
                {
                    ViewState["mode"] = "edit";
                    CheckBox2.Checked = false;
                    setData();
                    string strScript1 = "RefreshMain('" + ViewState["page"].ToString() + "');";
                    //   string strScript1 = "ClosePopUpListPost('" + ViewState["page"].ToString() + "','1');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
                }
                MsgBox("บันทึกข้อมูลสมบูรณ์");
            }
        }

        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            BindGridView();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            BindGridView();
        }

    }
}