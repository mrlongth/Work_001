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
using Aware.WebControls;
using myDLL;

namespace myWeb.App_Control.payment_back
{
    public partial class payment_back_import : PageBase
    {
        private string strPrefixCtr = "ctl00$ASPxRoundPanel1$ASPxRoundPanel2$ContentPlaceHolder1$";

        public static string getNumber(object pNumber)
        {
            string strNumber = pNumber.ToString();
            if (pNumber.ToString().Length > 0)
            {
                strNumber = String.Format("{0:#,##0.00}", decimal.Parse(pNumber.ToString()));
            }
            return strNumber;
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            lblError.Text = "";
            if (!IsPostBack)
            {
                imgSaveOnly.Attributes.Add("onMouseOver", "src='../../images/button/save_add2.png'");
                imgSaveOnly.Attributes.Add("onMouseOut", "src='../../images/button/save_add.png'");

                imgCancel.Attributes.Add("onMouseOver", "src='../../images/button/cancel2.png'");
                imgCancel.Attributes.Add("onMouseOut", "src='../../images/button/cancel.png'");

                imgImport.Attributes.Add("onMouseOver", "src='../../images/button/import2.png'");
                imgImport.Attributes.Add("onMouseOut", "src='../../images/button/import.png'");

                string strYear = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["yearnow"].ToString();

                imgList_item.Attributes.Add("onclick", "OpenPopUp('750px','400px','93%','ค้นหาข้อมูลรายได้' ,'../lov/item_lov.aspx?year='+document.forms[0]." + strPrefixCtr +
                              "cboYear.options[document.forms[0]." + strPrefixCtr + "cboYear.selectedIndex].value+" +
                              "'&item_code='+document.forms[0]." + strPrefixCtr + "txtitem_code.value+" +
                              "'&item_name='+document.forms[0]." + strPrefixCtr + "txtitem_name.value+" +
                              "'&ctrl1=" + txtitem_code.ClientID + "&ctrl2=" + txtitem_name.ClientID + "&item_type=D&from=back', '1');return false;");

                imgClear_item.Attributes.Add("onclick", "document.forms[0]." + strPrefixCtr + "txtitem_code.value='';" +
                                        "document.forms[0]." + strPrefixCtr + "txtitem_name.value=''; return false;");


                ViewState["sort"] = "payment_doc";
                ViewState["direction"] = "ASC";
                InitcboRound();
                GridView1.Visible = true;
                imgSaveOnly.Visible = false;
                imgCancel.Visible = false;
                imgImport.Visible = true;


                txtdate_begin.Text = cCommon.CheckDate(DateTime.Now.ToShortDateString());
                txtdate_end.Text = cCommon.CheckDate(DateTime.Now.ToShortDateString());


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
            //InitcboDirector();
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
            //  this.imgSaveOnly.Click += new System.Web.UI.ImageClickEventHandler(this.imgSaveOnly_Click);
        }
        #endregion

        private bool saveData()
        {
            bool blnResult = false;
            string strMessage = string.Empty;
            string strActive = string.Empty, strCreatedBy = string.Empty;
            string pitem_code = string.Empty;
            string pcomments_sub = string.Empty;
            int i;
            var oPayment_back = new cPayment_back();
            try
            {
                strActive = "Y";
                strCreatedBy = Session["username"].ToString();
                if (RadioButtonList1.SelectedValue == "A")
                {
                    pitem_code = txtitem_code.Text;
                }
                else
                {
                    pitem_code = txtitem_code.Text;
                }

                GridViewRow gviewRow;

                for (i = 0; i <= GridView1.Rows.Count - 1; i++)
                {
                    gviewRow = GridView1.Rows[i];
                    var lblpayment_doc = (Label)gviewRow.FindControl("lblpayment_doc");
                    var txtpayment_item_old = (AwNumeric)gviewRow.FindControl("txtpayment_item_old");
                    var txtpayment_item_new = (AwNumeric)gviewRow.FindControl("txtpayment_item_new");
                    var txtpayment_item_diff = (AwNumeric)gviewRow.FindControl("txtpayment_item_diff");
                    var txtpayment_item_back = (AwNumeric)gviewRow.FindControl("txtpayment_item_back");
                    var CheckBox1 = (CheckBox)gviewRow.FindControl("CheckBox1");
                    if (CheckBox1.Checked)
                    {
                        if (!oPayment_back.SP_IMPORT_PAYMENT_BACK_SAVE(lblpayment_doc.Text, rdoPayment_back_type.SelectedValue, txtdate_begin.Text,
                            txtdate_end.Text, txtdate_count_day.Text, txtdate_count_month.Text, txtdate_count_year.Text, txtdate_count_des.Text,
                            pitem_code, txtpayment_item_old.Value.ToString(), txtpayment_item_new.Value.ToString(), txtpayment_item_diff.Value.ToString(),
                            txtpayment_item_back.Value.ToString(), "", strCreatedBy, ref strMessage))
                        {
                            lblError.Text = strMessage + " : " + lblpayment_doc.Text;
                            return false;
                        }
                    }
                }


                blnResult = true;
                MsgBox("บันทึกข้อมูลสมบูรณ์");
            }
            catch (Exception ex)
            {
                blnResult = false;
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oPayment_back.Dispose();
            }
            return blnResult;
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.Header))
            {
                //Find the checkbox control in header and add an attribute
                ((CheckBox)e.Row.FindControl("cbSelectAll")).Attributes.Add("onclick", "javascript:SelectAll('" +
                        ((CheckBox)e.Row.FindControl("cbSelectAll")).ClientID + "')");

                for (int iCol = 0; iCol < e.Row.Cells.Count; iCol++)
                {
                    e.Row.Cells[iCol].Attributes.Add("class", "table_h");
                    e.Row.Cells[iCol].Wrap = false;
                    ViewState["sumall"] = "0.00";
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
                AwNumeric txtpayment_item_back = (AwNumeric)e.Row.FindControl("txtpayment_item_back");
                txtpayment_item_back.Attributes.Add("onchange", "funcsum();");

                ViewState["sumall"] = String.Format("{0:#,##0.00}", decimal.Parse(ViewState["sumall"].ToString()) + decimal.Parse(txtpayment_item_back.Text));
            }
            else if (e.Row.RowType.Equals(DataControlRowType.Footer))
            {
                AwNumeric txtsumpayment_item_back = (AwNumeric)e.Row.FindControl("txtsumpayment_item_back");
                txtsumpayment_item_back.Value = ViewState["sumall"].ToString();
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
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        protected void ClearData()
        {


            imgCancel.Visible = true;
            txtitem_code.Text = string.Empty;
            txtitem_name.Text = string.Empty;

            txtitem_code.Enabled = true;
            txtitem_name.Enabled = true;
            imgList_item.Enabled = true;
            imgClear_item.Enabled = true;

            imgImport.Visible = true;
            GridView1.Visible = false;
            imgSaveOnly.Visible = false;

        }

        protected void imgCancel_Click(object sender, ImageClickEventArgs e)
        {
            ClearData();
        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void imgImport_Click(object sender, ImageClickEventArgs e)
        {
            UpdatePanel oUpdatePanel;
            string strScript = string.Empty;
            strScript = "OpenPopUp('500px','200px','80%','อัพโหลด Excel File ตกเบิก' ,'payment_back_excel_upload.aspx?" +
                                                    "&ctrl1=" + hddGUID.ClientID +
                                                    "&item_code='+document.forms[0]." + strPrefixCtr + "txtitem_code.value+'" +
                                                    "&year='+document.forms[0]." + strPrefixCtr + "cboYear.options[document.forms[0]." + strPrefixCtr + "cboYear.selectedIndex].value+'" +
                                                    "&pay_year='+document.forms[0]." + strPrefixCtr + "cboPay_Year.options[document.forms[0]." + strPrefixCtr + "cboPay_Year.selectedIndex].value+'" +
                                                    "&pay_month='+document.forms[0]." + strPrefixCtr + "cboPay_Month.options[document.forms[0]." + strPrefixCtr + "cboPay_Month.selectedIndex].value+'" +
                                                    "&from=payment_back" +
                                                    "&is_bonus=1&show=1', '1');";
            oUpdatePanel = (UpdatePanel)this.Master.FindControl("updatePanel1");
            ScriptManager.RegisterClientScriptBlock(oUpdatePanel, oUpdatePanel.GetType(), "MessageBox", strScript, true);
        }

        protected void LinkButton1_Click1(object sender, EventArgs e)
        {
            BindGridView();
        }

        private void BindGridView()
        {
            var oPayment_back = new cPayment_back();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            int i;
            string strYear = cboYear.SelectedValue;
            string strPay_Year = cboPay_Year.SelectedValue;
            string strPay_Month = cboPay_Month.SelectedValue;
            string stritem_code = txtitem_code.Text;
            string pc_created_by = Session["username"].ToString();
            try
            {
                strCriteria = " and  item_code= '" + stritem_code + "' " +
                                        " and payment_year='" + strYear + "' " +
                                        " and pay_year='" + strPay_Year + "' " +
                                        " and pay_month='" + strPay_Month + "' " +
                                        " and c_created_by='" + pc_created_by + "' ";

                if (!oPayment_back.SP_IMPORT_PAYMENT_BACK_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    ds.Tables[0].DefaultView.Sort = ViewState["sort"] + " " + ViewState["direction"];
                    ds.Tables[0].Columns.Add("item_has");
                    for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        ds.Tables[0].Rows[i]["item_has"] = "N";
                    }
                    ds.Tables[0].AcceptChanges();
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                    imgSaveOnly.Visible = true;
                    imgCancel.Visible = true;
                    imgImport.Visible = false;

                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oPayment_back.Dispose();
                ds.Dispose();
            }
        }

        protected void imgSaveOnly_Click(object sender, ImageClickEventArgs e)
        {
            if (saveData())
            {
                ClearData();
            }
        }


        protected void Page_LoadComplete(object sender, EventArgs e)
        {

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "RegisterScript", "createDate('" + txtdate_begin.ClientID + "','" + DateTime.Now.Date.ToString("dd/MM/yyyy") + "');createDate('" + txtdate_end.ClientID + "','" + DateTime.Now.Date.ToString("dd/MM/yyyy") + "','" + DateTime.Now.Date.ToString("dd/MM/yyyy") + "');", true);


            txtdate_begin.Attributes.Add("onblur",
                 "document.getElementById('" + txtdate_count_des.ClientID + "').value=show_diff_date(document.getElementById('" + txtdate_begin.ClientID + "').value,document.getElementById('" + txtdate_end.ClientID + "').value);" +
                 "document.getElementById('" + txtdate_count_day.ClientID + "').value=show_diff_day(document.getElementById('" + txtdate_begin.ClientID + "').value,document.getElementById('" + txtdate_end.ClientID + "').value);" +
                 "document.getElementById('" + txtdate_count_month.ClientID + "').value=show_diff_month(document.getElementById('" + txtdate_begin.ClientID + "').value,document.getElementById('" + txtdate_end.ClientID + "').value);" +
                 "document.getElementById('" + txtdate_count_year.ClientID + "').value=show_diff_year(document.getElementById('" + txtdate_begin.ClientID + "').value,document.getElementById('" + txtdate_end.ClientID + "').value);" +
                 "return false;");
            txtdate_end.Attributes.Add("onblur",
                "document.getElementById('" + txtdate_count_des.ClientID + "').value=show_diff_date(document.getElementById('" + txtdate_begin.ClientID + "').value,document.getElementById('" + txtdate_end.ClientID + "').value);" +
                "document.getElementById('" + txtdate_count_day.ClientID + "').value=show_diff_day(document.getElementById('" + txtdate_begin.ClientID + "').value,document.getElementById('" + txtdate_end.ClientID + "').value);" +
                "document.getElementById('" + txtdate_count_month.ClientID + "').value=show_diff_month(document.getElementById('" + txtdate_begin.ClientID + "').value,document.getElementById('" + txtdate_end.ClientID + "').value);" +
                "document.getElementById('" + txtdate_count_year.ClientID + "').value=show_diff_year(document.getElementById('" + txtdate_begin.ClientID + "').value,document.getElementById('" + txtdate_end.ClientID + "').value);" +
                "return false;");



        }

    }
}