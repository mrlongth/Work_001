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

namespace myWeb.App_Control.payment_return
{
    public partial class payment_return_control : PageBase
    {

        public static string getNumber(object pNumber)
        {
            if (!pNumber.ToString().Equals(""))
            {
                string strNumber = String.Format("{0:#,##0.00}", float.Parse(pNumber.ToString()));
                return strNumber;
            }
            return "";
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            lblError.Text = "";
            if (!IsPostBack)
            {
                imgSaveOnly.Attributes.Add("onMouseOver", "src='../../images/controls/save2.jpg'");
                imgSaveOnly.Attributes.Add("onMouseOut", "src='../../images/controls/save.jpg'");
                BtnR1.Style.Add("display", "none");
                BtnR2.Style.Add("display", "none");
                ViewState["sort"] = "item_code";
                ViewState["direction"] = "ASC";

                #region set QueryString

                if (Request.QueryString["payment_doc"] != null)
                {
                    ViewState["payment_doc"] = Request.QueryString["payment_doc"];
                    txtpayment_doc.Text = ViewState["payment_doc"].ToString();
                }

                if (Request.QueryString["page"] != null)
                {
                    ViewState["page"] = Request.QueryString["page"];
                }
                if (Request.QueryString["mode"] != null)
                {
                    ViewState["mode"] = Request.QueryString["mode"];
                }
                if (Request.QueryString["PageStatus"] != null)
                {
                    ViewState["PageStatus"] = Request.QueryString["PageStatus"];
                }

                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    InitcboRound();
                    txtpayment_date.Text = cCommon.CheckDate(DateTime.Now.ToShortDateString());
                    ViewState["page"] = Request.QueryString["page"];
                    txtpayment_doc.CssClass = "textboxdis";
                    imgClear_item.Visible = true;
                    imgList_item.Visible = true;
                }
                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    setData();
                }

                #endregion

                imgList_item.Attributes.Add("onclick", "OpenPopUp('910px','520px','93%','ค้นหาข้อมูลการจ่ายเงินเดือน' ,'../lov/payment_lov.aspx?" +

                                    "payment_doc='+document.getElementById('" + txtpayment_doc.ClientID + "').value+'&" + "ctrl1=" + txtpayment_doc.ClientID + "&show=2', '2');return false;");


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
            bool blnDup = false;
            bool blnResult = false;
            string strScript = string.Empty;
            string strMessage = string.Empty;
            string strpayment_doc = string.Empty;
            string strpayment_return_comment = string.Empty;
            string strc_active = string.Empty;
            string strCreatedBy = string.Empty;
            string strUpdatedBy = string.Empty;
            cPayment_return oPayment_return = new cPayment_return();
            DataSet ds = new DataSet();
            try
            {
                #region set Data
                strpayment_doc = txtpayment_doc.Text;
                strpayment_return_comment = txtcomments.Text;
                strc_active = "Y";
                strCreatedBy = Session["username"].ToString();
                strUpdatedBy = Session["username"].ToString();
                #endregion
                if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    #region update
                    if (!oPayment_return.SP_PAYMENT_RETURN_HEAD_UPD(strpayment_doc, strpayment_return_comment, strc_active, strUpdatedBy, ref strMessage))
                    {
                        lblError.Text = strMessage;
                    }
                    else
                    {

                        #region update detail
                        GridViewRow gviewRow;
                        int i;
                        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
                        {
                            gviewRow = GridView1.Rows[i];
                            HiddenField hddpayment_return_detail_id = (HiddenField)gviewRow.FindControl("hddpayment_return_detail_id");
                            Aware.WebControls.AwNumeric txtpayment_return_money = (Aware.WebControls.AwNumeric)gviewRow.FindControl("txtpayment_return_money");
                            Label lblitem_code = (Label)gviewRow.FindControl("lblitem_code");
                            TextBox txtcomments_sub = (TextBox)gviewRow.FindControl("txtcomments_sub");
                            TextBox txtpayment_return_ap = (TextBox)gviewRow.FindControl("txtpayment_return_ap");

                            if (!oPayment_return.SP_PAYMENT_RETURN_DETAIL_UPD(hddpayment_return_detail_id.Value.ToString(), strpayment_doc, txtpayment_return_money.Value.ToString(),txtpayment_return_ap.Text , txtcomments_sub.Text, strUpdatedBy, ref strMessage)) 
                            {
                                lblError.Text = strMessage;
                            }
                        }

                        #endregion

                        blnResult = true;
                    }
                    #endregion
                }
                else
                {
                    #region check dup
                    string strCheckDup = string.Empty;
                    strCheckDup = " and payment_doc='" + strpayment_doc + "' ";
                    if (!oPayment_return.SP_PAYMENT_RETURN_HEAD_SEL(strCheckDup, ref ds, ref strMessage))
                    {
                        lblError.Text = strMessage;
                    }
                    else
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            strScript =
                                "alert(\"ไม่สามารถแก้ไขข้อมูลได้ เนื่องจาก ข้อมูลซ้ำ\");\n";
                            blnDup = true;
                        }
                        if (!blnDup)
                        {
                            if (!oPayment_return.SP_PAYMENT_RETURN_HEAD_INS(strpayment_doc, strpayment_return_comment, strc_active, strUpdatedBy, ref strMessage))
                            {
                                lblError.Text = strMessage;
                            }
                            else
                            {
                                blnResult = true;
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "close", strScript, true);
                        }
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
                oPayment_return.Dispose();
            }
            return blnResult;
        }

        private void imgSaveOnly_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (saveData())
            {
                setData();
                string strScript1 = "RefreshMain('" + ViewState["page"].ToString() + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
                MsgBox("บันทึกข้อมูลสมบูรณ์");
            }
        }

        private void setData()
        {
            cPayment_return oPayment_return = new cPayment_return();
            DataSet ds = new DataSet();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strpayment_doc = string.Empty;
            string strpayment_year = string.Empty;
            string strpay_year = string.Empty;
            string strpay_month = string.Empty;
            string strperson_code = string.Empty;
            string strperson_name = string.Empty;
            string strunit_name = string.Empty;
            string strposition_name = string.Empty;
            string strcomments = string.Empty;

            try
            {
                strCriteria = " and payment_doc = '" + txtpayment_doc.Text.Trim() + "' ";

                if (!oPayment_return.SP_PAYMENT_RETURN_HEAD_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ViewState["mode"] = "edit";
                        #region get Data
                        strpayment_doc = ds.Tables[0].Rows[0]["payment_doc"].ToString();
                        ViewState["payment_doc"] = strpayment_doc;
                        strpayment_year = ds.Tables[0].Rows[0]["payment_year"].ToString();
                        strpay_year = ds.Tables[0].Rows[0]["pay_year"].ToString();
                        strpay_month = ds.Tables[0].Rows[0]["pay_month"].ToString();
                        strperson_code = ds.Tables[0].Rows[0]["person_code"].ToString();
                        strperson_name = ds.Tables[0].Rows[0]["title_name"].ToString() +
                                                             ds.Tables[0].Rows[0]["person_thai_name"].ToString() + "  " +
                                                             ds.Tables[0].Rows[0]["person_thai_surname"].ToString();
                        strunit_name = ds.Tables[0].Rows[0]["unit_name"].ToString();
                        strposition_name = ds.Tables[0].Rows[0]["position_name"].ToString();
                        strcomments = ds.Tables[0].Rows[0]["payment_return_comment"].ToString();

                        #endregion

                        #region set Control
                        txtpayment_doc.Text = strpayment_doc;

                        InitcboYear();
                        if (cboYear.Items.FindByValue(strpayment_year) != null)
                        {
                            cboYear.SelectedIndex = -1;
                            cboYear.Items.FindByValue(strpayment_year).Selected = true;
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

                        txtperson_code.Text = strperson_code;
                        txtperson_name.Text = strperson_name;
                        txtunit_name.Text = strunit_name;
                        txtposition_name.Text = strposition_name;
                        txtcomments.Text = strcomments;

                        txtUpdatedBy.Text = ds.Tables[0].Rows[0]["c_updated_by"].ToString();
                        txtUpdatedDate.Text = ds.Tables[0].Rows[0]["d_updated_date"].ToString();


                        #endregion

                        BindGridView();
                        GridView1.Visible = true;

                        imgClear_item.Visible = false;
                        imgList_item.Visible = false;


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
            cPayment_return oPayment_return = new cPayment_return();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string strunit_code = string.Empty;
            try
            {
                strCriteria = " and payment_doc = '" + ViewState["payment_doc"].ToString() + "' ";
                if (!oPayment_return.SP_PAYMENT_RETURN_SEL(strCriteria, ref ds, ref strMessage))
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
                if (GridView1.Rows.Count == 0)
                {
                    EmptyGridFix(GridView1);
                }
                oPayment_return.Dispose();
                ds.Dispose();
            }
        }

        private void setPayment()
        {
            cPayment oPayment = new cPayment();
            DataSet ds = new DataSet();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strpayment_doc = string.Empty;
            string strpayment_year = string.Empty;
            string strpay_year = string.Empty;
            string strpay_month = string.Empty;
            string strperson_code = string.Empty;
            string strperson_name = string.Empty;
            string strunit_name = string.Empty;
            string strposition_name = string.Empty;
            string strperson_level = string.Empty;
            string strcomments = string.Empty;

            try
            {
                strCriteria = " and payment_doc = '" + txtpayment_doc.Text.Trim() + "' ";
                if (!oPayment.SP_PAYMENT_HEAD_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region get Data
                        strpayment_doc = ds.Tables[0].Rows[0]["payment_doc"].ToString();
                        strpayment_year = ds.Tables[0].Rows[0]["payment_year"].ToString();
                        strpay_year = ds.Tables[0].Rows[0]["pay_year"].ToString();
                        strpay_month = ds.Tables[0].Rows[0]["pay_month"].ToString();
                        strperson_code = ds.Tables[0].Rows[0]["person_code"].ToString();
                        strperson_name = ds.Tables[0].Rows[0]["title_name"].ToString() +
                                                             ds.Tables[0].Rows[0]["person_thai_name"].ToString() + "  " +
                                                             ds.Tables[0].Rows[0]["person_thai_surname"].ToString();
                        strunit_name = ds.Tables[0].Rows[0]["unit_name"].ToString();
                        strposition_name = ds.Tables[0].Rows[0]["position_name"].ToString();
                        strcomments = ds.Tables[0].Rows[0]["comments"].ToString();

                        #endregion

                        #region set Control
                        txtpayment_doc.Text = strpayment_doc;

                        InitcboYear();
                        if (cboYear.Items.FindByValue(strpayment_year) != null)
                        {
                            cboYear.SelectedIndex = -1;
                            cboYear.Items.FindByValue(strpayment_year).Selected = true;
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

                        txtperson_code.Text = strperson_code;
                        txtperson_name.Text = strperson_name;
                        txtunit_name.Text = strunit_name;
                        txtposition_name.Text = strposition_name;
                        txtcomments.Text = strcomments;

                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
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
                for (int iCol = 0; iCol < e.Row.Cells.Count; iCol++)
                {
                    e.Row.Cells[iCol].Attributes.Add("class", "table_h");
                    e.Row.Cells[iCol].Wrap = false;
                }
                Label lblitem_code = (Label)e.Row.FindControl("lblitem_code");
                ImageButton imgAdd = (ImageButton)e.Row.FindControl("imgAdd");
                imgAdd.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgGridAdd"].Rows[0]["img"].ToString();
                imgAdd.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgGridAdd"].Rows[0]["title"].ToString());
                imgAdd.Attributes.Add("onclick", "OpenPopUp('800px','450px','95%','เพิ่มข้อมูลส่งเงินคืนคลัง','payment_return_lov.aspx?mode=add&person_code=" +
                                 txtperson_code.Text + "&person_name=" + txtperson_name.Text + "&payment_doc=" + txtpayment_doc.Text +
                                  "&year=" + cboYear.SelectedValue + "','2');return false;");
                ViewState["total_return"] = "0";
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
                Label lblitem_code = (Label)e.Row.FindControl("lblitem_code");
                HiddenField hddpayment_return_detail_id = (HiddenField)e.Row.FindControl("hddpayment_return_detail_id");
                Aware.WebControls.AwNumeric txtpayment_return_money = (Aware.WebControls.AwNumeric)e.Row.FindControl("txtpayment_return_money");
                int nNo = (GridView1.PageSize * GridView1.PageIndex) + e.Row.RowIndex + 1;
                lblNo.Text = nNo.ToString();

                //ImageButton imgEdit = (ImageButton)e.Row.FindControl("imgEdit");
                ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");

                #region set Image Edit & Delete

                //imgEdit.Attributes.Add("onclick", "OpenPopUp('800px','325px','92%','แก้ไขข้อมูลส่งเงินคืนคลัง','payment_return_item_control.aspx?mode=edit&payment_return_detail_id=" +
                //hddpayment_return_detail_id.Value + "&page=" + GridView1.PageIndex.ToString() + "','2');return false;");

                //imgEdit.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgEdit"].Rows[0]["img"].ToString();
                //imgEdit.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgEdit"].Rows[0]["title"].ToString());

                imgDelete.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["img"].ToString();
                imgDelete.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["title"].ToString());
                imgDelete.Attributes.Add("onclick", "return confirm(\"คุณต้องการลบข้อมูลนี้หรือไม่ ?\");");

                #endregion


                ViewState["total_return"] = double.Parse(ViewState["total_return"].ToString()) + double.Parse(txtpayment_return_money.Value.ToString());
                txtpayment_net.Value = ViewState["total_return"].ToString();
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

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string strMessage = string.Empty;
            string strCheck = string.Empty;
            string strScript = string.Empty;
            HiddenField hddpayment_return_detail_id = (HiddenField)GridView1.Rows[e.RowIndex].FindControl("hddpayment_return_detail_id");
            cPayment_return oPayment_return = new cPayment_return();
            try
            {
                if (!oPayment_return.SP_PAYMENT_RETURN_DETAIL_DEL(hddpayment_return_detail_id.Value.ToString(), ref strMessage))
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
                oPayment_return.Dispose();
            }
            setData();
        }

        protected void BtnR1_Click(object sender, EventArgs e)
        {
            setPayment();
        }

        protected void imgClear_item_Click(object sender, ImageClickEventArgs e)
        {
            txtpayment_doc.Text = string.Empty;
            InitcboRound();
            txtperson_code.Text = string.Empty;
            txtperson_name.Text = string.Empty;
            txtunit_name.Text = string.Empty;
            txtposition_name.Text = string.Empty;
            txtcomments.Text = string.Empty;
            BindGridView();
            GridView1.Visible = false;
        }

        protected void BtnR2_Click(object sender, EventArgs e)
        {
            setData();
        }


    }
}