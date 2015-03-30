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
using Aware.WebControls;

namespace myWeb.App_Control.payment_member
{
    public partial class payment_member : PageBase
    {
        #region private data
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
                imgSaveOnly.Attributes.Add("onMouseOver", "src='../../images/button/save_add2.png'");
                imgSaveOnly.Attributes.Add("onMouseOut", "src='../../images/button/save_add.png'");

                imgFind.Attributes.Add("onMouseOver", "src='../../images/button/Search2.png'");
                imgFind.Attributes.Add("onMouseOut", "src='../../images/button/Search.png'");

                imgCancel.Attributes.Add("onMouseOver", "src='../../images/button/cancel2.png'");
                imgCancel.Attributes.Add("onMouseOut", "src='../../images/button/cancel.png'");


                ViewState["sort"] = "person_code";
                ViewState["direction"] = "ASC";
                //InitcboYear();
                //InitcboPay_Month();
                //InitcboPay_Year();
                InitcboRound();
                InitcboMember();
                lblitem_code.Text = string.Empty;
                lblitem_name.Text = string.Empty;
                //cboYear.Enabled = true;
                //cboPay_Month.Enabled = true;
                //cboPay_Year.Enabled = true;
                cboMember.Enabled = true;
                txtper_unit.Enabled = true;
                RadioButtonList1.Enabled = true;
                RadioButtonList1.SelectedValue = "A";

                imgSaveOnly.Visible = false;
                imgFind.Visible = true;
                imgCancel.Visible = true;

                imgSaveOnly.Attributes.Add("onclick", "return confirm(\"คุณต้องการบันทึกข้อมูลนี้หรือไม่ ?\");");

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

        private void InitcboMember()
        {
            cMember oMember = new cMember();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strMember = string.Empty;
            strMember = cboMember.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and c_active='Y' ";
            if (oMember.SP_MEMBER_SEL(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboMember.Items.Clear();
                cboMember.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboMember.Items.Add(new ListItem(dt.Rows[i]["member_name"].ToString(), dt.Rows[i]["member_code"].ToString()));
                }
                if (cboMember.Items.FindByValue(strMember) != null)
                {
                    cboMember.SelectedIndex = -1;
                    cboMember.Items.FindByValue(strMember).Selected = true;
                }
            }
        }

        private void InittxtItem_Code()
        {
            cMember oMember = new cMember();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strMember = string.Empty;
            strMember = cboMember.SelectedValue;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            lblitem_code.Text = string.Empty;
            lblitem_name.Text = string.Empty;
            strCriteria = " and member_code='" + strMember + "' and c_active='Y' ";
            if (oMember.SP_MEMBER_SEL(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    lblitem_code.Text = dt.Rows[0]["item_code"].ToString();
                    lblitem_name.Text = dt.Rows[0]["item_name"].ToString();
                }
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
            //this.imgSaveOnly.Click += new System.Web.UI.ImageClickEventHandler(this.imgSaveOnly_Click);
        }
        #endregion

        private bool saveData()
        {
            bool blnResult = false;
            string strMessage = string.Empty;
            string strActive = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty;
            string strScript = string.Empty;
            string ppayment_doc = string.Empty;
            string pitem_code = string.Empty;
            string ppayment_item_recv = "0";
            string ppayment_item_pay = string.Empty;
            string ppayment_item_tax = "Y";
            string ppayment_item_sos = "Y";
            string pcomments_sub = string.Empty;
            int i;
            cPayment oPayment = new cPayment();
            try
            {
                strActive = "Y";
                strCreatedBy = Session["username"].ToString();
                strUpdatedBy = Session["username"].ToString();
                pitem_code = lblitem_code.Text;


                for (i = 0; i <= GridView1.Rows.Count - 1; i++)
                {
                    if ((i + 2) < 10)
                    {
                        ppayment_doc = Request.Form[GridView1.ClientID.Replace("_", "$") + "$ctl0" + (i + 2).ToString() + "$hdfpayment_doc"];
                        ppayment_item_pay = Request.Form[GridView1.ClientID.Replace("_", "$") + "$ctl0" + (i + 2).ToString() + "$txtitem_credit"];
                    }
                    else
                    {
                        ppayment_doc = Request.Form[GridView1.ClientID.Replace("_", "$") + "$ctl" + (i + 2).ToString() + "$hdfpayment_doc"];
                        ppayment_item_pay = Request.Form[GridView1.ClientID.Replace("_", "$") + "$ctl" + (i + 2).ToString() + "$txtitem_credit"];
                    }
                    ppayment_item_pay = ppayment_item_pay.Replace(",", "");

                    DataSet ds = new DataSet();
                    string strCheckDup = string.Empty;
                    strCheckDup = " and payment_doc = '" + ppayment_doc + "' " +
                                                  " and item_code = '" + pitem_code + "' ";
                    if (!oPayment.SP_PAYMENT_SEL(strCheckDup, ref ds, ref strMessage))
                    {
                        lblError.Text = strMessage;
                    }
                    else
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            string strpayment_detail_id = ds.Tables[0].Rows[0]["payment_detail_id"].ToString();
                            string strBudgetType = ds.Tables[0].Rows[0]["payment_detail_budget_type"].ToString();
                            if (!oPayment.SP_PAYMENT_DETAIL_UPD(ppayment_doc, pitem_code, ppayment_item_recv, ppayment_item_pay, ppayment_item_tax,
                                                                                        ppayment_item_sos, pcomments_sub, strActive, strUpdatedBy, strBudgetType,strpayment_detail_id,
                                                                                        ref strMessage))
                            {
                                lblError.Text = strMessage;
                            }
                        }
                        else
                        {
                            if (!oPayment.SP_PAYMENT_DETAIL_INS(ppayment_doc, pitem_code, ppayment_item_recv, ppayment_item_pay, ppayment_item_tax,
                                                                                        ppayment_item_sos, pcomments_sub, strActive, strCreatedBy, ref strMessage))
                            {
                                lblError.Text = strMessage;
                            }
                        }
                    }
                }
                blnResult = true;
            }
            catch (Exception ex)
            {
                blnResult = false;
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oPayment.Dispose();
            }
            return blnResult;
        }

        private void BindGridView()
        {
            cPayment oPayment = new cPayment();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            int i;
            string strmember_code = cboMember.SelectedValue;
            string strpayment_year = cboYear.SelectedValue;
            string strpay_month = cboPay_Month.SelectedValue;
            string strpay_year = cboPay_Year.SelectedValue;

            try
            {
                strCriteria = "  and  member_code= '" + strmember_code + "' " +
                                       " and payment_year='" + strpayment_year + "' " +
                                       " and pay_month='" + strpay_month + "' " +
                                       " and pay_year='" + strpay_year + "' ";
                strCriteria += " and person_group_code IN (" + PersonGroupList + ") ";
                if (DirectorLock == "Y")
                {
                    strCriteria += " and substring(director_code,4,2) = substring('" + DirectorCode + "',4,2) ";
                }

                if (!oPayment.SP_PAYMENT_MEMBER_TEMP_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    ds.Tables[0].DefaultView.Sort = ViewState["sort"] + " " + ViewState["direction"];
                    ds.Tables[0].Columns.Add("item_credit");
                    ds.Tables[0].Columns.Add("item_has");
                    for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        ds.Tables[0].Rows[i]["item_has"] = "N";
                        ds.Tables[0].Rows[i]["item_credit"] = double.Parse(txtper_unit.Value.ToString()) * double.Parse((ds.Tables[0].Rows[i]["member_quan"].ToString()));
                        if (RadioButtonList1.SelectedValue.Equals("B"))
                        {
                            DataSet dsChk = new DataSet();
                            string strCheckDup = string.Empty;
                            strCheckDup = " and payment_doc = '" + ds.Tables[0].Rows[i]["payment_doc"] + "' " +
                                                          " and item_code = '" + lblitem_code.Text + "' ";

                            strCheckDup += " and person_group_code IN (" + PersonGroupList + ") ";
                            if (DirectorLock == "Y")
                            {
                                strCriteria += " and substring(director_code,4,2) = substring('" + DirectorCode + "',4,2) ";
                            }

                            if (!oPayment.SP_PAYMENT_SEL(strCheckDup, ref dsChk, ref strMessage))
                            {
                                lblError.Text = strMessage;
                            }
                            else
                            {
                                if (dsChk.Tables[0].Rows.Count > 0)
                                {
                                    ds.Tables[0].Rows[i]["item_credit"] = dsChk.Tables[0].Rows[0]["payment_item_pay"];
                                    ds.Tables[0].Rows[i]["item_has"] = "Y";
                                }
                            }
                        }
                    }
                    ds.Tables[0].AcceptChanges();
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();

                    cboYear.Enabled = false;
                    cboPay_Month.Enabled = false;
                    cboPay_Year.Enabled = false;
                    cboMember.Enabled = false;
                    txtper_unit.ReadOnly = true;
                    RadioButtonList1.Enabled = false;

                    if (GridView1.Rows.Count > 0)
                    {
                        imgSaveOnly.Visible = true;
                        imgFind.Visible = false;
                        imgCancel.Visible = true;
                    }
                    else
                    {
                        imgSaveOnly.Visible = false;
                        imgFind.Visible = true;
                        imgCancel.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oPayment.Dispose();
                ds.Dispose();
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
                ViewState["summember_quan"] = "0.00";
                ViewState["sumitem_credit"] = "0.00";
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
                AwNumeric txtmember_quan = (AwNumeric)e.Row.FindControl("txtmember_quan");
                AwNumeric txtitem_credit = (AwNumeric)e.Row.FindControl("txtitem_credit");
                ViewState["summember_quan"] = String.Format("{0:#,##0.00}", decimal.Parse(ViewState["summember_quan"].ToString()) + decimal.Parse(txtmember_quan.Value.ToString()));
                ViewState["sumitem_credit"] = String.Format("{0:#,##0.00}", decimal.Parse(ViewState["sumitem_credit"].ToString()) + decimal.Parse(txtitem_credit.Value.ToString()));


                txtmember_quan.Attributes.Add("onchange", "CalAmount('" + txtitem_credit.ClientID + "','" + txtmember_quan.ClientID + "','" + txtper_unit.Value + "');return false;");
                HiddenField hdfitem_has = (HiddenField)e.Row.FindControl("hdfitem_has");
                if (hdfitem_has.Value.Equals("Y"))
                {
                    txtitem_credit.CssClass = "numberdis";
                }

            }
            else if (e.Row.RowType.Equals(DataControlRowType.Footer))
            {
                AwNumeric txtsummember_quan = (AwNumeric)e.Row.FindControl("txtsummember_quan");
                AwNumeric txtsumitem_credit = (AwNumeric)e.Row.FindControl("txtsumitem_credit");
                txtsummember_quan.Value = ViewState["summember_quan"].ToString();
                txtsumitem_credit.Value = ViewState["sumitem_credit"].ToString();
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

        protected void imgFind_Click(object sender, ImageClickEventArgs e)
        {
            BindGridView();
        }

        protected void cboMember_SelectedIndexChanged(object sender, EventArgs e)
        {
            InittxtItem_Code();
            txtper_unit.Focus();
        }

        protected void imgSaveOnly_Click(object sender, ImageClickEventArgs e)
        {
            if (saveData())
            {
                InitcboYear();
                InitcboPay_Month();
                InitcboPay_Year();
                InitcboMember();
                cboMember.SelectedIndex = 0;
                lblitem_code.Text = string.Empty;
                lblitem_name.Text = string.Empty;
                BindGridView();
                //cboYear.Enabled = true;
                //cboPay_Month.Enabled = true;
                //cboPay_Year.Enabled = true;
                cboMember.Enabled = true;
                txtper_unit.ReadOnly = false;
                txtper_unit.Value = 0.00;
                RadioButtonList1.Enabled = true;
                RadioButtonList1.SelectedValue = "A";

                imgSaveOnly.Visible = false;
                imgFind.Visible = true;
                imgCancel.Visible = true;

                MsgBox("บันทึกข้อมูลสมบูรณ์");
            }
        }

        protected void imgCancel_Click(object sender, ImageClickEventArgs e)
        {
            InitcboYear();
            InitcboPay_Month();
            InitcboPay_Year();
            InitcboMember();
            cboMember.SelectedIndex = 0;
            lblitem_code.Text = string.Empty;
            lblitem_name.Text = string.Empty;
            BindGridView();
            //cboYear.Enabled = true;
            //cboPay_Month.Enabled = true;
            //cboPay_Year.Enabled = true;
            cboMember.Enabled = true;
            txtper_unit.ReadOnly = false;
            txtper_unit.Value = 0.00;
            RadioButtonList1.Enabled = true;
            RadioButtonList1.SelectedValue = "A";

            imgSaveOnly.Visible = false;
            imgFind.Visible = true;
            imgCancel.Visible = true;
        }

        public void MsgBox(string strMessage)
        {
            UpdatePanel oUpdatePanel;
            string strScript = string.Empty;
            strScript = "alert('" + strMessage + "');";
            oUpdatePanel = (UpdatePanel)this.Master.FindControl("updatePanel1");
            ScriptManager.RegisterClientScriptBlock(oUpdatePanel, oUpdatePanel.GetType(), "MessageBox", strScript, true);
        }

    }
}