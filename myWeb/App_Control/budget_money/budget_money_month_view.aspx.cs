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
    public partial class budget_money_month_view : PageBase
    {
        #region private data
        private string strPrefixCtr_main = "ctl00$ContentPlaceHolder1";
        #endregion

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

                imgClear.Attributes.Add("onMouseOver", "src='../../images/controls/clear2.jpg'");
                imgClear.Attributes.Add("onMouseOut", "src='../../images/controls/clear.jpg'");

                ViewState["sort"] = "item_lot_code";
                ViewState["direction"] = "ASC";

                #region set QueryString

                if (Request.QueryString["page"] != null)
                {
                    ViewState["page"] = Request.QueryString["page"].ToString();
                }
                if (Request.QueryString["mode"] != null)
                {
                    ViewState["mode"] = Request.QueryString["mode"].ToString();
                }

                if (Request.QueryString["budget_plan_code"] != null)
                {
                    ViewState["budget_plan_code"] = Request.QueryString["budget_plan_code"].ToString();
                }

                if (Request.QueryString["pay_year"] != null)
                {
                    ViewState["pay_year"] = Request.QueryString["pay_year"].ToString();
                }

                if (Request.QueryString["pay_month"] != null)
                {
                    ViewState["pay_month"] = Request.QueryString["pay_month"].ToString();
                }

                if (Request.QueryString["PageStatus"] != null)
                {
                    ViewState["PageStatus"] = Request.QueryString["PageStatus"].ToString();
                }

                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {

                    ViewState["page"] = Request.QueryString["page"];
                }
                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    setData();
                }

                #endregion

                BtnR1.Style.Add("display", "none");

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

        private void InitcboPay_Month()
        {
            string strMonth = string.Empty;
            strMonth = cboPay_Month.SelectedValue;
            if (strMonth.Equals(""))
            {
                if (DateTime.Now.Month < 10)
                {
                    strMonth = "0" + DateTime.Now.Month.ToString();
                }
                else
                {
                    strMonth = DateTime.Now.Month.ToString();
                }
            }
            DataTable odt;
            int i;
            cboPay_Month.Items.Clear();
            odt = ((DataSet)Application["xmlconfig"]).Tables["cboMonth"];
            for (i = 0; i <= odt.Rows.Count - 1; i++)
            {
                cboPay_Month.Items.Add(new ListItem(odt.Rows[i]["Text"].ToString(), odt.Rows[i]["Value"].ToString()));
            }
            if (cboPay_Month.Items.FindByValue(strMonth) != null)
            {
                cboPay_Month.SelectedIndex = -1;
                cboPay_Month.Items.FindByValue(strMonth).Selected = true;
            }
        }

        private void InitcboPay_Year()
        {
            string strYear = string.Empty;
            strYear = cboPay_Year.SelectedValue;
            if (strYear.Equals(""))
            {
                if (DateTime.Now.Year < 2200)
                {
                    strYear = (DateTime.Now.Year + 543).ToString();
                }
                else
                {
                    strYear = DateTime.Now.Year.ToString();
                }
            }
            DataTable odt;
            int i;
            cboPay_Year.Items.Clear();
            odt = ((DataSet)Application["xmlconfig"]).Tables["cboYear"];
            for (i = 0; i <= odt.Rows.Count - 1; i++)
            {
                cboPay_Year.Items.Add(new ListItem(odt.Rows[i]["Text"].ToString(), odt.Rows[i]["Value"].ToString()));
            }
            if (cboPay_Year.Items.FindByValue(strYear) != null)
            {
                cboPay_Year.SelectedIndex = -1;
                cboPay_Year.Items.FindByValue(strYear).Selected = true;
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

        //private bool saveData()
        //{
        //    bool blnResult = false;
        //    string strMessage = string.Empty;
        //    //Head
        //    string strbudget_money_doc = string.Empty;
        //    string strbudget_money_date = string.Empty;
        //    string strbudget_money_year = string.Empty;
        //    string strbudget_plan_code = string.Empty;
        //    string strbudget_money_all = string.Empty;
        //    string strbudget_money_adjust = string.Empty;
        //    string strbudget_money_use = string.Empty;
        //    string strbudget_money_remain = string.Empty;
        //    string strcomments = string.Empty;
        //    //Detail
        //    string stritem_group_code = string.Empty;
        //    string strbudget_item_group_code = string.Empty;
        //    string strlot_code = string.Empty;
        //    string strbudget_money_suball = string.Empty;
        //    string strbudget_money_subadjust = string.Empty;
        //    string strbudget_money_subuse = string.Empty;
        //    string strbudget_money_subremain = string.Empty;
        //    string strActive = string.Empty,
        //        strCreatedBy = string.Empty,
        //        strUpdatedBy = string.Empty;
        //    string strScript = string.Empty;
        //    cBudget_money oBudget_money = new cBudget_money();
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        #region set Data
        //        strbudget_money_year = cboYear.SelectedValue;
        //        strbudget_money_all = txtbudget_money_all.Value.ToString();
        //        strbudget_money_adjust = txtbudget_money_adjust.Value.ToString();
        //        strbudget_money_use = txtbudget_money_use.Value.ToString();
        //        strbudget_money_remain = txtbudget_money_remain.Value.ToString();
        //        strbudget_plan_code = txtbudget_plan_code.Text;
        //        strCreatedBy = Session["username"].ToString();
        //        strUpdatedBy = Session["username"].ToString();
        //        #endregion

        //        if (ViewState["mode"].ToString().ToLower().Equals("add"))
        //        {
        //            #region insert
        //            //string strCheckDup = string.Empty;
        //            //strCheckDup = " and budget_plan_code = '" + strbudget_plan_code + "' and budget_money_year = '" + strbudget_money_year + "' ";
        //            //if (!oBudget_money.SP_BUDGET_MONEY_HEAD_SEL(strCheckDup, ref ds, ref strMessage))
        //            //{
        //            //    lblError.Text = strMessage;
        //            //}
        //            //else
        //            //{
        //            //    if (ds.Tables[0].Rows.Count > 0)
        //            //    {
        //            //        strScript =
        //            //            "CalAmount();alert(\"ไม่สามารถเพิ่มข้อมูลได้ เนื่องจาก" +
        //            //            "\\n ปี : " + strbudget_money_year.Trim() +
        //            //            "\\nซ้ำ\");\n";
        //            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript, true);
        //            //    }
        //            //    else
        //            //    {
        //            //        if (!oBudget_money.sp_BUDGET_MONEY_INS(ref strbudget_money_doc, strbudget_money_date, strbudget_money_year, strbudget_plan_code, strbudget_money_all,
        //            //                                                                                                    strbudget_money_use, strbudget_money_remain, strcomments, "Y", strCreatedBy, ref strMessage))
        //            //        {
        //            //            lblError.Text = strMessage;
        //            //        }
        //            //        else
        //            //        {
        //            //            DataSet dsCHK = new DataSet();
        //            //            strCheckDup = " and budget_plan_code = '" + strbudget_plan_code + "' and budget_money_year = '" + strbudget_money_year + "' ";
        //            //            if (!oBudget_money.SP_BUDGET_MONEY_HEAD_SEL(strCheckDup, ref dsCHK, ref strMessage))
        //            //            {
        //            //                lblError.Text = strMessage;
        //            //            }
        //            //            else
        //            //            {
        //            //                strbudget_money_doc = dsCHK.Tables[0].Rows[0]["budget_money_doc"].ToString();
        //            //                ViewState["budget_money_doc"] = strbudget_money_doc;
        //            //            }
        //            //        }
        //            //    }
        //            //}
        //            #endregion
        //        }
        //        else
        //        {
        //            #region update
        //            if (!oBudget_money.SP_BUDGET_MONEY_UPD(strbudget_money_doc, strbudget_money_date, strbudget_money_year, strbudget_plan_code, strbudget_money_all,strbudget_money_adjust,
        //                                                        strbudget_money_use, strbudget_money_remain, strcomments, "Y", strUpdatedBy, ref strMessage))
        //            {
        //                lblError.Text = strMessage;

        //            }
        //            #endregion
        //        }

        //        #region insert detail
        //        if (!oBudget_money.SP_BUDGET_MONEY_DETAIL_DEL(strbudget_money_doc, "Y", strUpdatedBy, ref strMessage))
        //        {
        //            lblError.Text = strMessage;
        //        }

        //        GridViewRow gviewRow;
        //        string strChk;
        //        int i;
        //        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        //        {
        //            gviewRow = GridView1.Rows[i];
        //            CheckBox CheckBox1 = (CheckBox)gviewRow.FindControl("CheckBox1");
        //            Label lbllot_code = (Label)gviewRow.FindControl("lbllot_code");
        //            Label lblitem_group_code = (Label)gviewRow.FindControl("lblitem_group_code");
        //            Label lblbudget_item_group_code = (Label)gviewRow.FindControl("lblbudget_item_group_code");
        //            AwNumeric txtbudget_money_suball = (AwNumeric)gviewRow.FindControl("txtbudget_money_suball");
        //            AwNumeric txtbudget_money_subuse = (AwNumeric)gviewRow.FindControl("txtbudget_money_subuse");
        //            AwNumeric txtbudget_money_subremain = (AwNumeric)gviewRow.FindControl("txtbudget_money_subremain");
        //            stritem_group_code = lblitem_group_code.Text;
        //            strbudget_item_group_code = lblbudget_item_group_code.Text;
        //            strlot_code = lbllot_code.Text;
        //            strbudget_money_suball = txtbudget_money_suball.Value.ToString();
        //            strbudget_money_subuse = txtbudget_money_subuse.Value.ToString();
        //            strbudget_money_subremain = txtbudget_money_subremain.Value.ToString();
        //            if (CheckBox1.Checked)
        //            {
        //                DataSet dsDetail = new DataSet();
        //                strChk = " And budget_money_doc='" + strbudget_money_doc + "' " +
        //                                 " And lot_code = '" + strlot_code + "' and item_group_code = '" + stritem_group_code + "' ";
        //                if (!oBudget_money.SP_BUDGET_MONEY_SEL(strChk, ref dsDetail, ref strMessage))
        //                {
        //                    lblError.Text = strMessage;
        //                }
        //                else
        //                {
        //                    if (dsDetail.Tables[0].Rows.Count > 0)
        //                    {
        //                        if (!oBudget_money.SP_BUDGET_MONEY_DETAIL_UPD(strbudget_money_doc, stritem_group_code, strlot_code, strbudget_item_group_code, strbudget_money_suball,strbudget_money_subadjust , strbudget_money_subuse,
        //                                                                                                                    strbudget_money_subremain, "Y", strUpdatedBy, ref strMessage))
        //                        {
        //                            lblError.Text = strMessage;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (!oBudget_money.SP_BUDGET_MONEY_DETAIL_INS(strbudget_money_doc, stritem_group_code, strlot_code, strbudget_item_group_code ,strbudget_money_suball, strbudget_money_subuse,
        //                                                                                                                    strbudget_money_subremain, "Y", strCreatedBy, ref strMessage))
        //                        {
        //                            lblError.Text = strMessage;
        //                        }
        //                    }
        //                }
        //                dsDetail.Dispose();
        //            }
        //        }
        //        #endregion

        //        blnResult = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        lblError.Text = ex.Message.ToString();
        //    }
        //    finally
        //    {
        //        oBudget_money.Dispose();
        //    }
        //    return blnResult;
        //}

        private void setData()
        {
            imgClear.Visible = false;
            cPayment oPayment = new cPayment();
            DataSet ds = new DataSet();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strYear = string.Empty;
            string strPay_year = string.Empty;
            string strPay_month = string.Empty,
                strC_active = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty,
                strCreatedDate = string.Empty,
                strUpdatedDate = string.Empty;
            try
            {
                strCriteria = " and budget_plan_code = '" + ViewState["budget_plan_code"].ToString() + "' " +
                                        " and pay_year = '" + ViewState["pay_year"].ToString() + "' " +
                                        " and pay_month = '" + ViewState["pay_month"].ToString() + "' ";

                if (!oPayment.SP_PAYMENT_DEBIT_ALL_SEL(strCriteria, "", ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region get Data

                        strYear = ds.Tables[0].Rows[0]["payment_year"].ToString();
                        InitcboYear();
                        if (cboYear.Items.FindByValue(strYear) != null)
                        {
                            cboYear.SelectedIndex = -1;
                            cboYear.Items.FindByValue(strYear).Selected = true;
                        }

                        strPay_year = ds.Tables[0].Rows[0]["pay_year"].ToString();
                        InitcboPay_Year();
                        if (cboPay_Year.Items.FindByValue(strPay_year) != null)
                        {
                            cboPay_Year.SelectedIndex = -1;
                            cboPay_Year.Items.FindByValue(strPay_year).Selected = true;
                        }

                        strPay_month = ds.Tables[0].Rows[0]["pay_month"].ToString();
                        InitcboPay_Month();
                        if (cboPay_Month.Items.FindByValue(strPay_month) != null)
                        {
                            cboPay_Month.SelectedIndex = -1;
                            cboPay_Month.Items.FindByValue(strPay_month).Selected = true;
                        }

                        txtbudget_plan_code.Text = ds.Tables[0].Rows[0]["budget_plan_code"].ToString();
                        txtbudget_name.Text = ds.Tables[0].Rows[0]["budget_name"].ToString();
                        txtproduce_name.Text = ds.Tables[0].Rows[0]["produce_name"].ToString();
                        txtactivity_name.Text = ds.Tables[0].Rows[0]["activity_name"].ToString();
                        txtplan_name.Text = ds.Tables[0].Rows[0]["plan_name"].ToString();
                        txtwork_name.Text = ds.Tables[0].Rows[0]["work_name"].ToString();
                        txtfund_name.Text = ds.Tables[0].Rows[0]["fund_name"].ToString();
                        txtdirector_name.Text = ds.Tables[0].Rows[0]["director_name"].ToString();
                        txtunit_name.Text = ds.Tables[0].Rows[0]["unit_name"].ToString();

                        //strCreatedBy = ds.Tables[0].Rows[0]["c_created_by"].ToString();
                        //strUpdatedBy = ds.Tables[0].Rows[0]["c_updated_by"].ToString();
                        //strCreatedDate = ds.Tables[0].Rows[0]["d_created_date"].ToString();
                        //strUpdatedDate = ds.Tables[0].Rows[0]["d_updated_date"].ToString();
                        #endregion

                        #region set Control

                        cboYear.Enabled = false;
                        cboPay_Month.Enabled = false;
                        cboPay_Year.Enabled = false;

                        cboYear.CssClass = "textboxdis";
                        txtUpdatedBy.Text = strUpdatedBy;
                        txtUpdatedDate.Text = strUpdatedDate;
                        BindGridView();

                        txtbudget_money_use.Value = ViewState["sum_debit_recv"].ToString();
                        txtbudget_money_all.Value = ViewState["sum_debit_all"].ToString();
                        txtbudget_money_remain.Value = ViewState["sum_debit_remain"].ToString();                     


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
            cPayment oPayment = new cPayment();
            DataSet ds = new DataSet();
            int i;
            try
            {
                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {

                }
                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    strCriteria = " and budget_plan_code = '" + ViewState["budget_plan_code"].ToString() + "' " +
                             " and pay_year = '" + ViewState["pay_year"].ToString() + "' " +
                             " and pay_month = '" + ViewState["pay_month"].ToString() + "' ";

                    if (!oPayment.SP_PAYMENT_DEBIT_ALL_SEL(strCriteria, "", ref ds, ref strMessage))
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
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                ds.Dispose();
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.Header))
            {
                
                ViewState["sum_debit_recv"] = 0;
                ViewState["sum_debit_all"] = 0;
                ViewState["sum_debit_remain"] = 0;

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

                Aware.WebControls.AwNumeric txtpayment_debit_recv = (Aware.WebControls.AwNumeric)e.Row.FindControl("txtpayment_debit_recv");
                Aware.WebControls.AwNumeric txtpayment_debit_all = (Aware.WebControls.AwNumeric)e.Row.FindControl("txtpayment_debit_all");
                Aware.WebControls.AwNumeric txtpayment_debit_remain = (Aware.WebControls.AwNumeric)e.Row.FindControl("txtpayment_debit_remain");
                
                Label lblitem_lot_code = (Label)e.Row.FindControl("lblitem_lot_code");
                Label lblitem_group_code = (Label)e.Row.FindControl("lblitem_group_code");
                HiddenField hdfbudget_tranfer_doc = (HiddenField)e.Row.FindControl("hdfbudget_tranfer_doc");

                ViewState["sum_debit_recv"] = double.Parse(ViewState["sum_debit_recv"].ToString()) + double.Parse(txtpayment_debit_recv.Value.ToString());
                ViewState["sum_debit_all"] = double.Parse(ViewState["sum_debit_all"].ToString()) + double.Parse(txtpayment_debit_all.Value.ToString()) ;
                ViewState["sum_debit_remain"] = double.Parse(ViewState["sum_debit_remain"].ToString()) + double.Parse(txtpayment_debit_remain.Value.ToString()) ;
                       
                #region set Image Edit & Delete
                //ImageButton imgEdit = (ImageButton)e.Row.FindControl("imgEdit");
                //imgEdit.Attributes.Add("onclick", "OpenPopUp('900px','470px','93%' , 'โอนข้อมูลข้อมูลค่าใช้จ่าย' , 'budget_tranfer_control.aspx?mode=edit&budget_plan_code=" +
                //                                            txtbudget_plan_code.Text +
                //                                            "&lot_code=" + lblitem_lot_code.Text + "&item_group_code=" +  lblitem_group_code.Text  + "&year=" + cboYear.SelectedValue + 
                //                                            "&pay_month=" + cboPay_Month.SelectedValue + "&pay_year=" + cboPay_Year.SelectedValue + "&show=2' , '2');return false;");
                //imgEdit.ImageUrl = "../../images/controls/Transfer.png";
                //imgEdit.Attributes.Add("title", "ข้อมูลเงินงบประมาณ");

                //#region check dup
                //string strCheckDup = string.Empty;
                //string strMessage = string.Empty;
                //cBudget_money oBudget_money = new cBudget_money();
                //DataSet ds = new DataSet();
                //strCheckDup = " and budget_year = '" +  cboYear.SelectedValue   + "' " +
                //                              " and budget_tranfer_year = '" + cboPay_Year.SelectedValue + "' " +
                //                              " and budget_tranfer_month = '" + cboPay_Month.SelectedValue + "' " +
                //                              " and budget_plan_code_ds = '" + txtbudget_plan_code.Text + "' " +
                //                              " and lot_code_ds = '" + lblitem_lot_code.Text + "' " +
                //                              " and item_group_code_ds = '" + lblitem_group_code.Text + "' ";
                //if (!oBudget_money.SP_BUDGET_TRANFER_SEL(strCheckDup, ref ds, ref strMessage))
                //{
                //    lblError.Text = strMessage;
                //}
                //else
                //{
                //    if (ds.Tables[0].Rows.Count > 0)
                //    {
                //        hdfbudget_tranfer_doc.Value = ds.Tables[0].Rows[0]["budget_tranfer_doc"].ToString();
                //        ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                //        imgDelete.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["img"].ToString();
                //        imgDelete.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["title"].ToString());
                //        imgDelete.Attributes.Add("onclick", "return confirm(\"คุณต้องการลบข้อมูลนี้หรือไม่ ?\");");
                //    }
                //    else
                //    {
                //        hdfbudget_tranfer_doc.Value = string.Empty; 
                //        ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                //        imgDelete.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["imgdisable"].ToString();
                //        imgDelete.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["titledisable"].ToString());
                //        imgDelete.Attributes.Add("onclick", "return false;");
                //        imgDelete.Enabled = false;
                //    }
                //}
                //#endregion

                #endregion

            }

        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string strMessage = string.Empty;
            string strCheck = string.Empty;
            string strScript = string.Empty;
            string strUpdatedBy = Session["username"].ToString();
            Label lblitem_code = (Label)GridView1.Rows[e.RowIndex].FindControl("lblitem_code");
            HiddenField hdfbudget_tranfer_doc = (HiddenField)GridView1.Rows[e.RowIndex].FindControl("hdfbudget_tranfer_doc");
            cBudget_money oBudget_money = new cBudget_money();
            try
            {
                if (!oBudget_money.SP_BUDGET_TRANFER_DEL(hdfbudget_tranfer_doc.Value, strUpdatedBy, ref strMessage ))
                {
                    lblError.Text = strMessage;
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
            string strScript1 = "RefreshMain('" + ViewState["page"].ToString() + "');";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
            BindGridView();
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

            cboYear.Enabled = true;
            cboYear.CssClass = "textbox";

            txtbudget_plan_code.Text = string.Empty;
            txtbudget_name.Text = string.Empty;
            txtproduce_name.Text = string.Empty;
            txtactivity_name.Text = string.Empty;
            txtplan_name.Text = string.Empty;
            txtwork_name.Text = string.Empty;
            txtfund_name.Text = string.Empty;

            txtdirector_name.Text = string.Empty;
            txtunit_name.Text = string.Empty;
            txtbudget_money_all.Value = 0;
            txtbudget_money_use.Value = 0;
            txtbudget_money_remain.Value = 0;


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

        protected void BtnR1_Click(object sender, EventArgs e)
        {
            BindGridView();
            string strScript1 = "RefreshMain('" + ViewState["page"].ToString() + "');";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
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
            //else
            //{
            //    if (saveData())
            //    {
            //        ViewState["mode"] = "edit";
            //        MsgBox("บันทึกข้อมูลสมบูรณ์");
            //    }
            //}
        }
        
    }
}