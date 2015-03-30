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
    public partial class cheque_save_control : PageBase
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

                txtcheque_date.Text = cCommon.CheckDate(DateTime.Now.Date.ToString());
              

                ViewState["sort"] = "cheque_code";
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
                if (Request.QueryString["cheque_doc"] != null)
                {
                    ViewState["cheque_doc"] = Request.QueryString["cheque_doc"].ToString();
                }
                else
                {
                    ViewState["cheque_doc"] = string.Empty;
                }

                if (Request.QueryString["cheque_type"] != null)
                {
                    ViewState["cheque_type"] = Request.QueryString["cheque_type"].ToString();
                }
                else
                {
                    ViewState["cheque_type"] = "M";
                }

                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    txtcheque_date.Text = cCommon.CheckDate(DateTime.Now.ToShortDateString());
                    txtcheque_doc.CssClass = "textboxdis";
                }
                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    setData();
                    txtcheque_doc.ReadOnly = true;
                    txtcheque_doc.CssClass = "textboxdis";
                }
                if (ViewState["cheque_type"].ToString() == "M") 
                {
                    GridView1.Columns[1].Visible = false;
                    this.Title = this.Title + " (ส่วนกลาง)";
                }else
                {
                    this.Title = this.Title + " (หน่วยงาน)";                    
                }

                #endregion
              
                InitcboCheque_bank();
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

        private bool saveData()
        {
            bool blnResult = false;
            string strMessage = string.Empty;
            //Head
            string strcheque_doc = string.Empty;
            string strcheque_date = string.Empty;
            string strcheque_year = string.Empty;
            string strpay_month = string.Empty;
            string strpay_year = string.Empty;
            string strcheque_bank_code = string.Empty;
            string strcheque_comment = string.Empty;
            //Detail
            string strcheque_detail_id = "0" ;
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

            string strc_user = Session["username"].ToString();
            string strcheque_type = ViewState["cheque_type"].ToString() ;
            string strScript = string.Empty;
            bool blnDup = false;
            cCheque oCheque = new cCheque();
            DataSet ds = new DataSet();
            try
            {

                #region set Data
                strcheque_doc = txtcheque_doc.Text;
                strcheque_date = txtcheque_date.Text;
                strcheque_year = cboYear.SelectedValue;
                strpay_month = cboPay_Month.SelectedValue;
                strpay_year = cboPay_Year.SelectedValue;
                strcheque_bank_code = cboCheque_bank_code.SelectedValue;
                strcheque_comment = txtcomments.Text;
                #endregion

                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    #region insert
                    string strCheckDup = string.Empty;
                    strCheckDup = " and cheque_bank_code = '" + strcheque_bank_code + "' and pay_year = '" + strpay_year + "' " +
                                  " and pay_month = '" + strpay_month + "' and cheque_type='" + strcheque_type + "' " +
                                  " and c_created_by = '" + UserLoginName + "' ";
                    if (!oCheque.SP_CHEQUE_HEAD_SEL(strCheckDup, ref ds, ref strMessage))
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
                                "\\n ปัญชีธนาคาร : " + cboCheque_bank_code.SelectedItem.Text +
                                "\\n รอบเดือน : " + cboPay_Month.SelectedItem.Text + "   ปี : " + strpay_year +
                                "\\n ผู้บันทึก : " + UserLoginName +
                                "\\nซ้ำ\");\n";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript, true);
                        }
                        else
                        {
                            if (!oCheque.SP_CHEQUE_HEAD_INS(strcheque_doc, strcheque_date, strcheque_year, strpay_month, strpay_year,
                                                                                                                        strcheque_bank_code, strcheque_comment, strc_user, strcheque_type, ref strMessage))
                            {
                                lblError.Text = strMessage;
                            }
                            else
                            {
                                DataSet dsCHK = new DataSet();
                                strCheckDup = " and cheque_bank_code = '" + strcheque_bank_code + "' and pay_year = '" + strpay_year + "' " +
                                              " and pay_month = '" + strpay_month + "' and cheque_type='" + ViewState["cheque_type"].ToString() + "' " +
                                              " and c_created_by = '" + UserLoginName + "' ";

                                if (!oCheque.SP_CHEQUE_HEAD_SEL(strCheckDup, ref dsCHK, ref strMessage))
                                {
                                    lblError.Text = strMessage;
                                }
                                else
                                {
                                    strcheque_doc = dsCHK.Tables[0].Rows[0]["cheque_doc"].ToString();
                                    ViewState["cheque_doc"] = strcheque_doc;
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
                    if (!oCheque.SP_CHEQUE_HEAD_UPD(strcheque_doc, strcheque_date, strcheque_year, strpay_month, strpay_year,
                         strcheque_bank_code, strcheque_comment, strc_user, strcheque_type, ref strMessage))
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
                            Label lblcheque_code = (Label)gviewRow.FindControl("lblcheque_code");
                            if (lblcheque_code.Text != "") 
                            {
                                Label lblcheque_name = (Label)gviewRow.FindControl("lblcheque_name");
                                TextBox txtcheque_no = (TextBox)gviewRow.FindControl("txtcheque_no");
                                TextBox txtcheque_pvno = (TextBox)gviewRow.FindControl("txtcheque_pvno");
                                HiddenField hddcheque_money_thai = (HiddenField)gviewRow.FindControl("hddcheque_money_thai");
                                HiddenField hddcheque_print = (HiddenField)gviewRow.FindControl("hddcheque_print");
                                AwNumeric txtcheque_money = (AwNumeric)gviewRow.FindControl("txtcheque_money");
                                Label lbldirector_code = (Label)gviewRow.FindControl("lbldirector_code");
                                HiddenField hddcheque_detail_id = (HiddenField)gviewRow.FindControl("hddcheque_detail_id");

                                TextBox txtcheque_date_print = (TextBox)gviewRow.FindControl("txtcheque_date_print");
                                TextBox txtcheque_date_pay = (TextBox)gviewRow.FindControl("txtcheque_date_pay");
                                TextBox txtcheque_date_bank = (TextBox)gviewRow.FindControl("txtcheque_date_bank");
                                TextBox txtcheque_deka = (TextBox)gviewRow.FindControl("txtcheque_deka");
                                TextBox txtcheque_acccode = (TextBox)gviewRow.FindControl("txtcheque_acccode");

                                strcheque_code = lblcheque_code.Text;
                                strcheque_no = txtcheque_no.Text;
                                strcheque_pvno = txtcheque_pvno.Text;
                                strcheque_money = txtcheque_money.Value.ToString();
                                strcheque_money_thai = hddcheque_money_thai.Value;
                                strcheque_comment_sub = string.Empty;
                                strcheque_print = hddcheque_print.Value;
                                strdirector_code = lbldirector_code.Text;
                                strcheque_detail_id = hddcheque_detail_id.Value.ToString();

                                strcheque_date_print = txtcheque_date_print.Text;
                                strcheque_date_pay = txtcheque_date_pay.Text;
                                strcheque_date_bank = txtcheque_date_bank.Text;
                                strcheque_deka = txtcheque_deka.Text;
                                strcheque_acccode = txtcheque_acccode.Text;

                                if (!oCheque.SP_CHEQUE_DETAIL_UPD(strcheque_detail_id, strcheque_doc, strcheque_code, strcheque_no, strcheque_pvno, strcheque_money, strcheque_money_thai,
                                     strcheque_comment_sub, strcheque_print, strdirector_code, 
                                     strcheque_date_print ,strcheque_date_pay ,strcheque_date_bank, strcheque_deka,strcheque_acccode,  ref strMessage))
                                {
                                    lblError.Text = strMessage;
                                }
                            }
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
                oCheque.Dispose();
            }
            return blnResult;
        }

        private void setData()
        {
            imgClear.Visible = false;
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
            cCheque oCheque = new cCheque();
            DataSet ds = new DataSet();
            try
            {
                strCriteria += "  And  cheque_doc  ='" + ViewState["cheque_doc"].ToString() + "' ";

                if (!oCheque.SP_CHEQUE_DETAIL_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    ds.Tables[0].DefaultView.Sort = ViewState["sort"] + " " + ViewState["direction"];
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ViewState["chk"] = "Y";
                    }
                    else 
                    {
                        ViewState["chk"] = "N";
                    };
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();                  
                    txttotal_all.Value = mTotalall;
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
                oCheque.Dispose();
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
                ImageButton imgAdd = (ImageButton)e.Row.FindControl("imgAdd");
                imgAdd.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgGridAdd"].Rows[0]["img"].ToString();
                imgAdd.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgGridAdd"].Rows[0]["title"].ToString());

                if (ViewState["cheque_type"].ToString() == "M")
                {
                    GridView1.Columns[1].Visible = false;
                    //imgAdd.Attributes.Add("onclick", "OpenPopUp('800px','500px','94%','เลือกผู้จ่ายเช็ค','cheque_select.aspx?cheque_type=" + ViewState["cheque_type"].ToString() + "&cheque_doc=" +
                    //                                                               ViewState["cheque_doc"].ToString() + "&pay_year=" + cboYear.SelectedValue + "&pay_month=" + cboPay_Month.SelectedValue + "','2');return false;");
                    if (ViewState["chk"].ToString() == "Y")
                    {
                        imgAdd.Attributes.Add("onclick", "OpenPopUp('800px','350px','93%','เพิ่มข้อมูลการจ่ายเช็ค','cheque_save_item_control.aspx?cheque_doc=" +
                                                                                       ViewState["cheque_doc"].ToString() + "&pay_year=" + cboYear.SelectedValue + "&pay_month=" + cboPay_Month.SelectedValue + "','2');return false;");
                    }
                    else
                    {
                        imgAdd.Attributes.Add("onclick", "OpenPopUp('800px','500px','94%','เลือกผู้จ่ายเช็ค','cheque_select.aspx?cheque_type=" + ViewState["cheque_type"].ToString() + "&cheque_doc=" +
                                                                                       ViewState["cheque_doc"].ToString() + "&pay_year=" + cboYear.SelectedValue + "&pay_month=" + cboPay_Month.SelectedValue + "','2');return false;");
                    }
                }
                else 
                {
                    if (ViewState["chk"].ToString() == "Y")
                    {
                        imgAdd.Attributes.Add("onclick", "OpenPopUp('800px','350px','93%','เพิ่มข้อมูลการจ่ายเช็ค','cheque_save_item_control.aspx?cheque_doc=" +
                                                                                       ViewState["cheque_doc"].ToString() + "&pay_year=" + cboYear.SelectedValue + "&pay_month=" + cboPay_Month.SelectedValue + "','2');return false;");
                    }
                    else
                    {
                        imgAdd.Attributes.Add("onclick", "OpenPopUp('800px','500px','94%','เลือกผู้จ่ายเช็ค','cheque_select.aspx?cheque_type=" + ViewState["cheque_type"].ToString() + "&cheque_doc=" +
                                                                                       ViewState["cheque_doc"].ToString() + "&pay_year=" + cboYear.SelectedValue + "&pay_month=" + cboPay_Month.SelectedValue + "','2');return false;");                    
                    }
                }                
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
                    imgStatus.ImageUrl = "../../images/print.png";
                    imgStatus.Attributes.Add("title", "พิมพ์แล้ว");
                    imgStatus.Attributes.Add("onclick", "return false;");
                }
                else
                {
                    imgStatus.ImageUrl = "../../images/print_dis.png";
                    imgStatus.Attributes.Add("title", "ยังไม่ได้พิมพ์");
                    imgStatus.Attributes.Add("onclick", "return false;");
                }
                #endregion

                #region set Image  Delete
                ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                imgDelete.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["img"].ToString();
                imgDelete.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["title"].ToString());
                imgDelete.Attributes.Add("onclick", "return confirm(\"คุณต้องการลบข้อมูลนี้ ?\");");
                #endregion
                TextBox txtcheque_date_print = (TextBox)e.Row.FindControl("txtcheque_date_print");
                TextBox txtcheque_date_pay = (TextBox)e.Row.FindControl("txtcheque_date_pay");
                TextBox txtcheque_date_bank = (TextBox)e.Row.FindControl("txtcheque_date_bank");
                if (txtcheque_date_print.Text.Length > 0) 
                {
                    txtcheque_date_print.Text = cCommon.CheckDate(txtcheque_date_print.Text);
                    txtcheque_date_pay.Text = cCommon.CheckDate(txtcheque_date_pay.Text);
                    txtcheque_date_bank.Text = cCommon.CheckDate(txtcheque_date_bank.Text);                
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

        protected void cboCheque_bank_code_SelectedIndexChanged(object sender, EventArgs e)
        {
            cCheque oCheque = new cCheque();
            DataSet ds = new DataSet();
            string strCriteria, strMessage = string.Empty;
            try
            {
                strCriteria = " and cheque_bank_code = '" + cboCheque_bank_code.SelectedValue + "' ";
                if (!oCheque.SP_CHEQUE_BANK_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region set Control
                        txtcheque_bank_no.Text = ds.Tables[0].Rows[0]["cheque_bank_no"].ToString();
                        txtbank_name.Text = ds.Tables[0].Rows[0]["bank_name"].ToString();
                        txtbranch_name.Text = ds.Tables[0].Rows[0]["branch_name"].ToString();
                        #endregion
                    }
                    else
                    {
                        txtcheque_bank_no.Text = string.Empty;
                        txtbank_name.Text = string.Empty;
                        txtbranch_name.Text = string.Empty;
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

        protected void imgClear_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string strMessage = string.Empty;
            string strScript = string.Empty;
            Label lblcheque_code = (Label)GridView1.Rows[e.RowIndex].FindControl("lblcheque_code");
            HiddenField hddcheque_detail_id = (HiddenField)GridView1.Rows[e.RowIndex].FindControl("hddcheque_detail_id");

            cCheque oCheque = new cCheque();
            try
            {
                if (!oCheque.SP_CHEQUE_DETAIL_DEL(hddcheque_detail_id.Value.ToString(), ref strMessage))
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
                oCheque.Dispose();
            }
            BindGridView();
        }

    }
}