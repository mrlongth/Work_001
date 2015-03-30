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

namespace myWeb.App_Control.item_acc_save
{
    public partial class item_acc_save_control : PageBase
    {
        #region private data
        private string strPrefixCtr_main = "ctl00$ContentPlaceHolder1";
        private decimal mTotalall;
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
                imgSaveOnly.Attributes.Add("onMouseOver", "src='../../images/controls/save2.jpg'");
                imgSaveOnly.Attributes.Add("onMouseOut", "src='../../images/controls/save.jpg'");

                imgClear.Attributes.Add("onMouseOver", "src='../../images/controls/clear2.jpg'");
                imgClear.Attributes.Add("onMouseOut", "src='../../images/controls/clear.jpg'");

                ViewState["sort"] = "item_acc_code";
                ViewState["chk"] = "N";
                ViewState["direction"] = "ASC";
                InitcboRound();
                #region set QueryString
                if (Request.QueryString["page"] != null)
                {
                    ViewState["page"] = Request.QueryString["page"].ToString();
                }
                if (Request.QueryString["mode"] != null)
                {
                    ViewState["mode"] = Request.QueryString["mode"].ToString();
                }
                if (Request.QueryString["item_acc_doc"] != null)
                {
                    ViewState["item_acc_doc"] = Request.QueryString["item_acc_doc"].ToString();
                }
                else
                {
                    ViewState["item_acc_doc"] = string.Empty;
                }

                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    txtitem_acc_doc.CssClass = "textboxdis";
                }
                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    setData();
                    txtitem_acc_doc.ReadOnly = true;
                    txtitem_acc_doc.CssClass = "textboxdis";
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

        private void InitcboRound()
        {
            cPayment_round oPayment_round = new cPayment_round();
            DataSet ds = new DataSet();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strYear = string.Empty;
            string strPay_Month = string.Empty;
            string strPay_Year = string.Empty;
            try
            {
                strCriteria = " and round_status= 'O' ";
                if (!oPayment_round.SP_PAYMENT_ROUND_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region get Data
                        strYear = ds.Tables[0].Rows[0]["payment_year"].ToString();
                        strPay_Month = ds.Tables[0].Rows[0]["pay_month"].ToString();
                        strPay_Year = ds.Tables[0].Rows[0]["pay_year"].ToString();
                        #endregion

                        #region set Control
                        InitcboYear();
                        if (cboYear.Items.FindByValue(strYear) != null)
                        {
                            cboYear.SelectedIndex = -1;
                            cboYear.Items.FindByValue(strYear).Selected = true;
                        }

                        InitcboPay_Month();
                        if (cboPay_Month.Items.FindByValue(strPay_Month) != null)
                        {
                            cboPay_Month.SelectedIndex = -1;
                            cboPay_Month.Items.FindByValue(strPay_Month).Selected = true;
                        }

                        InitcboPay_Year();
                        if (cboPay_Year.Items.FindByValue(strPay_Year) != null)
                        {
                            cboPay_Year.SelectedIndex = -1;
                            cboPay_Year.Items.FindByValue(strPay_Year).Selected = true;
                        }


                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oPayment_round.Dispose();
            }
        }

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
                    else {
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
            string strMessage = string.Empty;
            //Head
            string stritem_acc_doc = string.Empty;
            string stritem_acc_year = string.Empty;
            string strpay_month = string.Empty;
            string strpay_year = string.Empty;
            string stritem_acc_comment = string.Empty;
            //Detail
            string stritem_acc_detail_id = "0";
            string stritem_acc_deka;
            string stritem_acc_group;
            string stritem_acc_amount;
            string stritem_acc_tax;
            string stritem_acc_total;
            string stritem_acc_code;
            string stritem_project_code;
            string stritem_code;
            string strproduce_code;

            string strc_user = Session["username"].ToString();

            string strScript = string.Empty;
            bool blnDup = false;
            cItem_acc oItem_acc = new cItem_acc();
            DataSet ds = new DataSet();
            try
            {

                #region set Data
                stritem_acc_doc = txtitem_acc_doc.Text;
                stritem_acc_year = cboYear.SelectedValue;
                strpay_month = cboPay_Month.SelectedValue;
                strpay_year = cboPay_Year.SelectedValue;
                stritem_acc_comment = txtcomments.Text;
                #endregion

                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    #region insert
                    string strCheckDup = string.Empty;
                    strCheckDup = " and pay_year = '" + strpay_year + "' " +
                                  " and pay_month = '" + strpay_month + "'" +
                                  " and budget_type = '" + this.BudgetType + "'" +
                                  " and c_created_by = '" + UserLoginName + "' ";
                    if (!oItem_acc.SP_ITEM_ACC_HEAD_SEL(strCheckDup, ref ds, ref strMessage))
                    {
                        lblError.Text = strMessage;
                    }
                    else
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            blnDup = true;
                            strScript =
                                "alert(\"ไม่สามารถเพิ่มข้อมูลได้ เนื่องจาก" +
                                "\\n รอบเดือน : " + cboPay_Month.SelectedItem.Text + "   ปี : " + strpay_year +
                                "\\nซ้ำ\");\n";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript, true);
                        }
                        else
                        {
                            if (!oItem_acc.SP_ITEM_ACC_HEAD_INS(stritem_acc_year, strpay_month,
                                            strpay_year, stritem_acc_comment, strc_user, "B", ref strMessage))
                            {
                                lblError.Text = strMessage;
                            }
                            else
                            {
                                DataSet dsCHK = new DataSet();
                                strCheckDup = " and pay_year = '" + strpay_year + "' " +
                                              " and pay_month = '" + strpay_month + "' " +
                                              " and budget_type = '" + this.BudgetType + "'" +                   
                                              " and c_created_by = '" + UserLoginName + "' ";
                                if (!oItem_acc.SP_ITEM_ACC_HEAD_SEL(strCheckDup, ref dsCHK, ref strMessage))
                                {
                                    lblError.Text = strMessage;
                                }
                                else
                                {
                                    stritem_acc_doc = dsCHK.Tables[0].Rows[0]["item_acc_doc"].ToString();
                                    ViewState["item_acc_doc"] = stritem_acc_doc;
                                    blnResult = true;
                                }
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    #region update
                    if (!oItem_acc.SP_ITEM_ACC_HEAD_UPD(stritem_acc_doc, stritem_acc_year, strpay_month, strpay_year,
                         stritem_acc_comment, strc_user, ref strMessage))
                    {
                        lblError.Text = strMessage;
                    }
                    else
                    {
                        #region insert detail
                        GridViewRow gviewRow;
                        int i;
                        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
                        {
                            gviewRow = GridView1.Rows[i];
                            Label lblitem_acc_code = (Label)gviewRow.FindControl("lblitem_acc_code");
                            //if (lblitem_acc_code.Text != "")
                            //{
                            HiddenField hdditem_acc_detail_id = (HiddenField)gviewRow.FindControl("hdditem_acc_detail_id");
                            TextBox txtitem_acc_deka = (TextBox)gviewRow.FindControl("txtitem_acc_deka");
                            TextBox txtitem_acc_group = (TextBox)gviewRow.FindControl("txtitem_acc_group");
                            TextBox txtitem_project_code = (TextBox)gviewRow.FindControl("txtitem_project_code");
                            AwNumeric txtitem_acc_amount = (AwNumeric)gviewRow.FindControl("txtitem_acc_amount");
                            AwNumeric txtitem_acc_tax = (AwNumeric)gviewRow.FindControl("txtitem_acc_tax");
                            AwNumeric txtitem_acc_total = (AwNumeric)gviewRow.FindControl("txtitem_acc_total");
                            //Label lblitem_acc_code = (Label)gviewRow.FindControl("txtitem_code");
                            Label lblitem_code = (Label)gviewRow.FindControl("lblitem_code");
                            Label lblproduce_code = (Label)gviewRow.FindControl("lblproduce_code");
                            Label lblperson_group_code_acc = (Label)gviewRow.FindControl("lblperson_group_code_acc");
                            Label lblpayment_back_type = (Label)gviewRow.FindControl("lblpayment_back_type");

                            stritem_acc_detail_id = hdditem_acc_detail_id.Value.ToString();
                            stritem_acc_deka = txtitem_acc_deka.Text.Trim();
                            stritem_acc_group = txtitem_acc_group.Text.Trim();
                            stritem_acc_amount = txtitem_acc_amount.Value.ToString();
                            stritem_acc_tax = txtitem_acc_tax.Value.ToString();
                            stritem_acc_total = txtitem_acc_total.Value.ToString();
                            stritem_acc_code = lblitem_acc_code.Text;
                            stritem_project_code = txtitem_project_code.Text.Trim();
                            strproduce_code = lblproduce_code.Text;
                            stritem_code = lblitem_code.Text;

                            if (stritem_acc_detail_id != "0")
                            {
                                if (!oItem_acc.SP_ITEM_ACC_DETAIL_UPD(stritem_acc_detail_id, txtitem_acc_doc.Text.Trim(),
                                            stritem_acc_deka, stritem_acc_group, stritem_acc_amount,
                                            stritem_acc_tax, stritem_acc_total, stritem_acc_code,
                                            stritem_project_code, stritem_code, strproduce_code,
                                           ref strMessage))
                                {
                                    lblError.Text = strMessage;
                                }
                            }
                            else
                            {
                                if (!oItem_acc.SP_ITEM_ACC_DETAIL_INS(txtitem_acc_doc.Text.Trim(), stritem_acc_deka, stritem_acc_group,
                                            stritem_acc_amount, stritem_acc_tax, stritem_acc_total, stritem_acc_code,
                                            stritem_project_code, stritem_code, strproduce_code, lblperson_group_code_acc.Text.Trim(),
                                            lblpayment_back_type.Text, ref strMessage))
                                {
                                    lblError.Text = strMessage;
                                }

                            }
                            //}
                        }
                        #endregion
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
                oItem_acc.Dispose();
            }
            return blnResult;
        }

        private void setData()
        {
            imgClear.Visible = false;
            cItem_acc oItem_acc = new cItem_acc();
            DataSet ds = new DataSet();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string stritem_acc_doc = string.Empty;
            string stritem_acc_year = string.Empty;
            string strpay_month = string.Empty;
            string strpay_year = string.Empty;
            string stritem_acc_comment = string.Empty;
            string strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty,
                strCreatedDate = string.Empty,
                strUpdatedDate = string.Empty;
            try
            {
                strCriteria = " and item_acc_doc = '" + ViewState["item_acc_doc"].ToString() + "' ";
                if (!oItem_acc.SP_ITEM_ACC_HEAD_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region get Data

                        stritem_acc_doc = ds.Tables[0].Rows[0]["item_acc_doc"].ToString();
                        stritem_acc_year = ds.Tables[0].Rows[0]["item_acc_year"].ToString();
                        strpay_month = ds.Tables[0].Rows[0]["pay_month"].ToString();
                        strpay_year = ds.Tables[0].Rows[0]["pay_year"].ToString();
                        stritem_acc_comment = ds.Tables[0].Rows[0]["item_acc_comment"].ToString();
                        strUpdatedBy = ds.Tables[0].Rows[0]["c_updated_by"].ToString();
                        strUpdatedDate = ds.Tables[0].Rows[0]["d_updated_date"].ToString();
                        #endregion

                        #region set Control

                        txtitem_acc_doc.Text = ds.Tables[0].Rows[0]["item_acc_doc"].ToString();
                        InitcboYear();
                        if (cboYear.Items.FindByValue(stritem_acc_year) != null)
                        {
                            cboYear.SelectedIndex = -1;
                            cboYear.Items.FindByValue(stritem_acc_year).Selected = true;
                        }

                        InitcboPay_Year();
                        if (cboPay_Year.Items.FindByValue(strpay_year) != null)
                        {
                            cboPay_Year.SelectedIndex = -1;
                            cboPay_Year.Items.FindByValue(strpay_year).Selected = true;
                        }

                        InitcboPay_Month();
                        if (cboPay_Month.Items.FindByValue(strpay_month) != null)
                        {
                            cboPay_Month.SelectedIndex = -1;
                            cboPay_Month.Items.FindByValue(strpay_month).Selected = true;
                        }

                        txtcomments.Text = stritem_acc_comment;
                        txtitem_acc_doc.CssClass = "textboxdis";
                        cboYear.Enabled = false;
                        cboYear.CssClass = "textboxdis";
                        txtUpdatedBy.Text = strUpdatedBy;
                        txtUpdatedDate.Text = strUpdatedDate;

                        if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                        {
                            BindGridView();
                        }

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
            cItem_acc oItem_acc = new cItem_acc();
            DataSet ds = new DataSet();
            try
            {
                strCriteria = "  And  item_acc_doc  ='" + ViewState["item_acc_doc"].ToString() + "' ";

                if (!oItem_acc.SP_ITEM_ACC_DETAIL_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {

                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        strCriteria = "   And  pay_month  ='" + cboPay_Month.SelectedValue + "' ";
                        strCriteria += "  And  pay_year  ='" + cboPay_Year.SelectedValue + "' ";

                        if (base.myBudgetType == "B")
                        {
                            strCriteria += "  And  payment_detail_person_group_code in (" + PersonGroupList + ",'') ";
                        }

                        if (base.myBudgetType != "M")
                        {
                            strCriteria += "  And  payment_detail_budget_type ='" + base.myBudgetType + "' ";
                        }


                        if (!oItem_acc.SP_ITEM_ACC_TMP_SEL(strCriteria, ref ds, ref strMessage))
                        {
                            lblError.Text = strMessage;
                        }
                    }
                    ds.Tables[0].DefaultView.Sort = ViewState["sort"].ToString() + " " + ViewState["direction"].ToString();
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
                if (GridView1.Rows.Count == 0)
                {
                    EmptyGridFix(GridView1);
                }
                oItem_acc.Dispose();
                ds.Dispose();
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
                grdView.DataSource == null)
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

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.Header))
            {
                mTotalall = 0;
                //ImageButton imgAdd = (ImageButton)e.Row.FindControl("imgAdd");
                //imgAdd.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgGridAdd"].Rows[0]["img"].ToString();
                //imgAdd.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgGridAdd"].Rows[0]["title"].ToString());
                //imgAdd.Attributes.Add("onclick", "OpenPopUp('800px','350px','93%','เพิ่มข้อมูลสมุดเงินรับ','item_acc_save_item_control.aspx?item_acc_doc=" + txtitem_acc_doc.Text) ;

                for (int iCol = 0; iCol < e.Row.Cells.Count; iCol++)
                {
                    e.Row.Cells[iCol].Attributes.Add("class", "table_h");
                    e.Row.Cells[iCol].Wrap = false;
                }
                ViewState["item_acc_amount"] = "0";
                ViewState["item_acc_tax"] = "0";
                ViewState["item_acc_total"] = "0";
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
                DataRowView rowView = (DataRowView)(e.Row.DataItem);
                AwNumeric txtitem_acc_amount = (AwNumeric)e.Row.FindControl("txtitem_acc_amount");
                AwNumeric txtitem_acc_tax = (AwNumeric)e.Row.FindControl("txtitem_acc_tax");
                AwNumeric txtitem_acc_total = (AwNumeric)e.Row.FindControl("txtitem_acc_total");

                ViewState["item_acc_amount"] = decimal.Parse(ViewState["item_acc_amount"].ToString()) + decimal.Parse(txtitem_acc_amount.Value.ToString());
                ViewState["item_acc_tax"] = decimal.Parse(ViewState["item_acc_tax"].ToString()) + decimal.Parse(txtitem_acc_tax.Value.ToString());
                ViewState["item_acc_total"] = decimal.Parse(ViewState["item_acc_total"].ToString()) + decimal.Parse(txtitem_acc_total.Value.ToString());

                #region set Image Delete
                ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                imgDelete.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["img"].ToString();
                imgDelete.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["title"].ToString());
                imgDelete.Attributes.Add("onclick", "return confirm('คุณต้องการลบข้อมูลนี้หรือไม่')");
                #endregion

            }
            else if (e.Row.RowType.Equals(DataControlRowType.Footer))
            {
                AwNumeric txtitem_acc_amount_total = (AwNumeric)e.Row.FindControl("txtitem_acc_amount_total");
                AwNumeric txtitem_acc_tax_total = (AwNumeric)e.Row.FindControl("txtitem_acc_tax_total");
                AwNumeric txtitem_acc_total_total = (AwNumeric)e.Row.FindControl("txtitem_acc_total_total");
                txtitem_acc_amount_total.Value = decimal.Parse(ViewState["item_acc_amount"].ToString());
                txtitem_acc_tax_total.Value = decimal.Parse(ViewState["item_acc_tax"].ToString());
                txtitem_acc_total_total.Value = decimal.Parse(ViewState["item_acc_total"].ToString());
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

        protected void BtnR1_Click(object sender, EventArgs e)
        {
            BindGridView();
        }

        protected void imgSaveOnly_Click(object sender, ImageClickEventArgs e)
        {
            if (saveData())
            {
                ViewState["mode"] = "edit";
                setData();
                string strScript1 = "RefreshMain('" + ViewState["page"].ToString() + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
                MsgBox("บันทึกข้อมูลสมบูรณ์");
            }
        }


        protected void imgClear_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string strMessage = string.Empty;
            string strScript = string.Empty;
            HiddenField hdditem_acc_detail_id = (HiddenField)GridView1.Rows[e.RowIndex].FindControl("hdditem_acc_detail_id");

            cItem_acc oItem_acc = new cItem_acc();
            try
            {
                if (!oItem_acc.SP_ITEM_ACC_DETAIL_DEL(hdditem_acc_detail_id.Value.ToString(), ref strMessage))
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
                oItem_acc.Dispose();
            }
            BindGridView();
        }

    }
}