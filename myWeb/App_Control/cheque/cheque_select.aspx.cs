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

namespace myWeb.App_Control.cheque
{
    public partial class cheque_select : PageBase
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
                imgSaveOnly.Attributes.Add("onMouseOver", "src='../../images/controls/save2.jpg'");
                imgSaveOnly.Attributes.Add("onMouseOut", "src='../../images/controls/save.jpg'");
                ViewState["sort"] = "cheque_code";
                ViewState["direction"] = "ASC";
                #region set QueryString
                if (Request.QueryString["cheque_doc"] != null)
                {
                    ViewState["cheque_doc"] = Request.QueryString["cheque_doc"].ToString();
                }
                if (Request.QueryString["pay_month"] != null)
                {
                    ViewState["pay_month"] = Request.QueryString["pay_month"].ToString();
                }
                if (Request.QueryString["pay_year"] != null)
                {
                    ViewState["pay_year"] = Request.QueryString["pay_year"].ToString();
                }
                if (Request.QueryString["cheque_type"] != null)
                {
                    ViewState["cheque_type"] = Request.QueryString["cheque_type"].ToString();
                }
                else
                {
                    ViewState["cheque_type"] = "M";
                }
                if (ViewState["cheque_type"].ToString() == "M")
                {
                    GridView1.Columns[3].Visible = false;
                }
                #endregion
                setData();
            }
        }

        #region private function

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

        private bool saveData()
        {
            bool blnResult = false;
            string strMessage = string.Empty;
            //Detail
            string strcheque_doc = string.Empty;
            string strcheque_code = string.Empty;
            string strcheque_no = string.Empty;
            string strcheque_pvno = string.Empty;
            string strcheque_money = string.Empty;
            string strcheque_money_thai = string.Empty;
            string strcheque_comment_sub = string.Empty;
            string strcheque_print = string.Empty;
            string strdirector_code = string.Empty;

            string strcheque_date_print = string.Empty;
            string strcheque_date_pay = string.Empty;
            string strcheque_date_bank = string.Empty;
            string strcheque_deka = string.Empty;
            string strcheque_acccode = string.Empty;

            string strScript = string.Empty;
            cCheque oCheque = new cCheque();
            DataSet ds = new DataSet();
            try
            {
                #region set Data
                strcheque_doc = txtcheque_doc.Text;
                #endregion

                #region insert detail
                GridViewRow gviewRow;
                int i;
                for (i = 0; i <= GridView1.Rows.Count - 1; i++)
                {
                    gviewRow = GridView1.Rows[i];
                    DataRowView rowView = (DataRowView)(gviewRow.DataItem);
                    Label lblcheque_code = (Label)gviewRow.FindControl("lblcheque_code");
                    CheckBox CheckBox1 = (CheckBox)gviewRow.FindControl("CheckBox1");
                    Label lblcheque_name = (Label)gviewRow.FindControl("lblcheque_name");
                    AwNumeric txtcheque_money = (AwNumeric)gviewRow.FindControl("txtcheque_money");
                    Label lbldirector_code = (Label)gviewRow.FindControl("lbldirector_code");
                    strcheque_code = lblcheque_code.Text;
                    strcheque_money = txtcheque_money.Value.ToString();
                    strcheque_comment_sub = string.Empty;
                    strcheque_print = "N";
                    strdirector_code = lbldirector_code.Text;

                    strcheque_date_print = txtcheque_date_print.Text;
                    strcheque_date_pay = txtcheque_date_pay.Text;
                    strcheque_date_bank = txtcheque_date_bank.Text;
                    strcheque_deka = txtcheque_deka.Text;
                    strcheque_acccode = txtcheque_acccode.Text;

                    if (CheckBox1.Checked)
                    {
                        
                        if (!oCheque.SP_CHEQUE_DETAIL_INS(strcheque_doc, strcheque_code, strcheque_no, strcheque_pvno, strcheque_money, strcheque_money_thai,
                                                          strcheque_comment_sub, strcheque_print, strdirector_code,
                                                          strcheque_date_print, strcheque_date_pay, strcheque_date_bank, strcheque_deka, strcheque_acccode, ref strMessage))
                        {
                            lblError.Text = strMessage;
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
                oCheque.Dispose();
            }
            return blnResult;
        }

        private void setData()
        {
            cCheque oCheque = new cCheque();
            DataSet ds = new DataSet();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strcheque_doc = ViewState["cheque_doc"].ToString();
            string strpay_month = ViewState["pay_month"].ToString();
            string strpay_year = ViewState["pay_year"].ToString();
            try
            {
                strCriteria = " and cheque_doc = '" + strcheque_doc + "' ";
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
                        strpay_month = ds.Tables[0].Rows[0]["pay_month"].ToString();
                        strpay_year = ds.Tables[0].Rows[0]["pay_year"].ToString();
                        #endregion

                        #region set Control

                        txtcheque_doc.Text = ds.Tables[0].Rows[0]["cheque_doc"].ToString();
                        txtcheque_date_print.Text = cCommon.CheckDate(DateTime.Now.Date.ToString());
                        txtcheque_date_pay.Text = cCommon.CheckDate(DateTime.Now.Date.ToString());
                        txtcheque_date_bank.Text = cCommon.CheckDate(DateTime.Now.Date.ToString());
              

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
            string strCriteria2 = string.Empty;
            cCheque oCheque = new cCheque();
            DataSet ds = new DataSet();
            try
            {
                if (ViewState["cheque_type"].ToString() == "M")
                {
                   // strCriteria = " and c_created_by = '" + UserLoginName + "' ";
                    strCriteria = " and pay_month ='" + cboPay_Month.SelectedValue + "'   and  pay_year  ='" + cboPay_Year.SelectedValue + "' ";
                    strCriteria += " and cheque_type ='" + ViewState["cheque_type"].ToString() + "'  ";
                    
                    strCriteria2 = " and pay_month ='" + cboPay_Month.SelectedValue + "'   and  pay_year  ='" + cboPay_Year.SelectedValue + "' ";
                    if (base.myBudgetType == "B")
                    {
                        strCriteria += " and payment_detail_person_group_code in (" + PersonGroupList + ",'') ";
                        strCriteria2 += " and payment_detail_person_group_code in (" + PersonGroupList + ",'') ";
                    }
                    //else 
                    //{
                    //    strCriteria += " and payment_detail_person_group_code in (" + PersonGroupList + ",'') ";
                    //    strCriteria2 += " and payment_detail_person_group_code in (" + PersonGroupList + ",'') ";
                    //}



                    if (base.myBudgetType != "M")
                    {
                        strCriteria += "  And  payment_detail_budget_type ='" + base.myBudgetType + "' ";
                        strCriteria2 += "  And  payment_detail_budget_type ='" + base.myBudgetType + "' ";
                    }

                    if (!oCheque.SP_CHEQUE_SELECT_SEL(strCriteria,strCriteria2, ref ds, ref strMessage))
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
                else
                {
                    //strCriteria = " and c_created_by = '" + UserLoginName + "' ";
                    strCriteria = " and  pay_month ='" + cboPay_Month.SelectedValue + "' and pay_year  ='" + cboPay_Year.SelectedValue + "' ";
                    strCriteria += " and cheque_type ='" + ViewState["cheque_type"].ToString() + "' ";
                    strCriteria += " and payment_detail_person_group_code in (" + PersonGroupList + ",'') ";

                    strCriteria2 = " and  pay_month ='" + cboPay_Month.SelectedValue + "' and pay_year  ='" + cboPay_Year.SelectedValue + "' ";
                    strCriteria2 += " and cheque_type ='" + ViewState["cheque_type"].ToString() + "' ";


                    if (base.myBudgetType != "M")
                    {
                        strCriteria += "  And  payment_detail_budget_type ='" + base.myBudgetType + "' ";
                        strCriteria2 += "  And  payment_detail_budget_type ='" + base.myBudgetType + "' ";
                    }


                    if (!oCheque.SP_CHEQUE_UNIT_SELECT_SEL(strCriteria,strCriteria2, ref ds, ref strMessage))
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
                oCheque.Dispose();
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
                    setData();
                    string strScript1 = "window.parent.frames['iframeShow1'].__doPostBack('ctl00$ContentPlaceHolder1$BtnR1','');ClosePopUp('2');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
                    MsgBox("บันทึกข้อมูลสมบูรณ์");
                }
            }
        }

    }
}
