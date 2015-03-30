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

namespace myWeb.App_Control.cheque_save
{
    public partial class cheque_save_view : PageBase
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
                ViewState["sort"] = "cheque_code";
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
                if (Request.QueryString["cheque_doc"] != null)
                {
                    ViewState["cheque_doc"] = Request.QueryString["cheque_doc"].ToString();
                }
                else
                {
                    ViewState["cheque_doc"] = string.Empty;
                }
                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    txtcheque_date.Text = cCommon.CheckDate(DateTime.Now.ToShortDateString());
                    txtcheque_doc.CssClass = "textboxdis";
                }
                setData();
                txtcheque_doc.ReadOnly = true;
                txtcheque_doc.CssClass = "textboxdis";
                #endregion
                InitcboRound();
                InitcboCheque_bank();
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

        private void InitcboCheque_bank()
        {
            cCheque oCheque = new cCheque();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strCheque_bank_code = string.Empty;
            string strYear = cboYear.SelectedValue;
            strCheque_bank_code = cboCheque_bank_code.SelectedValue;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = "  ";
            if (oCheque.SP_CHEQUE_BANK_SEL(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboCheque_bank_code.Items.Clear();
                cboCheque_bank_code.Items.Add(new ListItem("---- เลือกข้อมูลทั้งหมด ----", ""));
                int i;
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboCheque_bank_code.Items.Add(new ListItem(dt.Rows[i]["cheque_acc_name"].ToString(), dt.Rows[i]["cheque_bank_code"].ToString()));
                }
                if (cboCheque_bank_code.Items.FindByValue(strCheque_bank_code) != null)
                {
                    cboCheque_bank_code.SelectedIndex = -1;
                    cboCheque_bank_code.Items.FindByValue(strCheque_bank_code).Selected = true;
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
            base.OnInit(e);
        }



        #endregion

        private void setData()
        {
            cCheque oCheque = new cCheque();
            DataSet ds = new DataSet();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strcheque_doc = string.Empty;
            string strcheque_date = string.Empty;
            string strcheque_year = string.Empty;
            string strpay_month = string.Empty;
            string strpay_year = string.Empty;
            string strcheque_bank_code = string.Empty;
            string strcheque_comment = string.Empty;
            string strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty,
                strCreatedDate = string.Empty,
                strUpdatedDate = string.Empty;
            try
            {
                strCriteria = " and cheque_doc = '" + ViewState["cheque_doc"].ToString() + "' ";
                if (!oCheque.SP_CHEQUE_HEAD_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region get Data

                        strcheque_doc = ds.Tables[0].Rows[0]["cheque_doc"].ToString();
                        strcheque_date = cCommon.CheckDate(ds.Tables[0].Rows[0]["cheque_date"].ToString());
                        strcheque_year = ds.Tables[0].Rows[0]["cheque_year"].ToString();
                        strpay_month = ds.Tables[0].Rows[0]["pay_month"].ToString();
                        strpay_year = ds.Tables[0].Rows[0]["pay_year"].ToString();
                        strcheque_bank_code = ds.Tables[0].Rows[0]["cheque_bank_code"].ToString();
                        strcheque_comment = ds.Tables[0].Rows[0]["cheque_comment"].ToString();
                        strUpdatedBy = ds.Tables[0].Rows[0]["c_updated_by"].ToString();
                        strUpdatedDate = ds.Tables[0].Rows[0]["d_updated_date"].ToString();
                        #endregion

                        #region set Control

                        txtcheque_doc.Text = ds.Tables[0].Rows[0]["cheque_doc"].ToString();
                        txtcheque_date.Text = cCommon.CheckDate(ds.Tables[0].Rows[0]["cheque_date"].ToString());
                        InitcboYear();
                        if (cboYear.Items.FindByValue(strcheque_year) != null)
                        {
                            cboYear.SelectedIndex = -1;
                            cboYear.Items.FindByValue(strcheque_year).Selected = true;
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

                        InitcboCheque_bank();
                        if (cboCheque_bank_code.Items.FindByValue(strcheque_bank_code) != null)
                        {
                            cboCheque_bank_code.SelectedIndex = -1;
                            cboCheque_bank_code.Items.FindByValue(strcheque_bank_code).Selected = true;
                        }

                        txtcheque_bank_no.Text = ds.Tables[0].Rows[0]["cheque_bank_no"].ToString();
                        txtbank_name.Text = ds.Tables[0].Rows[0]["bank_name"].ToString();
                        txtbranch_name.Text = ds.Tables[0].Rows[0]["branch_name"].ToString();

                        txtcomments.Text = strcheque_comment;
                        txtcheque_doc.CssClass = "textboxdis";
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
            cCheque oCheque = new cCheque();
            DataSet ds = new DataSet();
            try
            {
                strCriteria = strCriteria + "  And  cheque_doc  ='" + ViewState["cheque_doc"].ToString() + "' ";
                if (!oCheque.SP_CHEQUE_DETAIL_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    ds.Tables[0].DefaultView.Sort = ViewState["sort"] + " " + ViewState["direction"];
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                    if (ds.Tables[0].Rows.Count > 8)
                    {
                        GridView1.Width = Unit.Percentage(98);
                    }
                    else
                    {
                        GridView1.Width = Unit.Percentage(100);
                    }
                    txttotal_all.Value = mTotalall;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oCheque.Dispose();
                ds.Dispose();
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.Header))
            {
                mTotalall = 0;
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
                AwNumeric txtcheque_money = (AwNumeric)e.Row.FindControl("txtcheque_money");
                mTotalall = mTotalall + decimal.Parse(txtcheque_money.Value.ToString());
                #region set ImageStatus
                ImageButton imgStatus = (ImageButton)e.Row.FindControl("imgStatus");
                DataRowView rowView = (DataRowView)(e.Row.DataItem);
                if (rowView["cheque_print"].ToString().Equals("Y"))
                {
                    imgStatus.ImageUrl = "../../images/controls/print.gif";
                    imgStatus.Attributes.Add("title", "พิมพ์แล้ว");
                    imgStatus.Attributes.Add("onclick", "return false;");
                }
                else
                {
                    imgStatus.ImageUrl = "../../images/controls/print_disable.gif";
                    imgStatus.Attributes.Add("title", "ยังไม่ได้พิมพ์");
                    imgStatus.Attributes.Add("onclick", "return false;");
                }
                #endregion         

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

    }
}