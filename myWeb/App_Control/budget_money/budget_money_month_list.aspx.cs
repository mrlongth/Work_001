using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Threading;
using System.Text;
using myDLL;

namespace myWeb.App_Control.budget_money
{
    public partial class budget_money_month_list : PageBase
    {

        #region private data
        private string strRecordPerPage;
        private string strPageNo = "1";
        #endregion

        public static string getNumber(object pNumber)
        {
            string strNumber = String.Format("{0:#,##0.00}", float.Parse(pNumber.ToString()));
            return strNumber;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                imgFind.Attributes.Add("onMouseOver", "src='../../images/button/Search2.png'");
                imgFind.Attributes.Add("onMouseOut", "src='../../images/button/Search.png'");


                ViewState["sort"] = "budget_plan_code";
                ViewState["direction"] = "ASC";
                //InitcboYear();
                //InitcboPay_Month();
                //InitcboPay_Year();

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

                InitcboRound();
                InitcboActivity();
                //BindGridView(0);
            }
            else
            {
                if (Request.Form["ctl00$ASPxRoundPanel1$ContentPlaceHolder2$GridView1$ctl01$cboPerPage"] != null)
                {
                    strRecordPerPage = Request.Form["ctl00$ASPxRoundPanel1$ContentPlaceHolder2$GridView1$ctl01$cboPerPage"].ToString();
                    strPageNo = Request.Form["ctl00$ASPxRoundPanel1$ContentPlaceHolder2$GridView1$ctl01$txtPage"].ToString();
                }
                if (txthpage.Value != string.Empty)
                {
                    BindGridView(int.Parse(txthpage.Value));
                    txthpage.Value = string.Empty;
                }
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
            InitcboDirector();
        }

        private void InitcboDirector()
        {
            cDirector oDirector = new cDirector();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strDirector_code = string.Empty;
            string strYear = cboYear.SelectedValue;
            strDirector_code = cboDirector.SelectedValue;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and director_year = '" + strYear + "'  and  c_active='Y' ";
            if (DirectorLock == "Y")
            {
                strCriteria += " and substring(director_code,4,2) = substring('" + DirectorCode + "',4,2) ";
            }
            if (this.BudgetType == "R")
            {
                strCriteria = strCriteria + " and budget_type <> 'B' ";
            }
            else
            {
                strCriteria = strCriteria + " and budget_type <> 'R' ";
            }
            if (oDirector.SP_SEL_DIRECTOR(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboDirector.Items.Clear();
                cboDirector.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                int i;
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboDirector.Items.Add(new ListItem(dt.Rows[i]["director_name"].ToString(), dt.Rows[i]["director_code"].ToString()));
                }
                if (cboDirector.Items.FindByValue(strDirector_code) != null)
                {
                    cboDirector.SelectedIndex = -1;
                    cboDirector.Items.FindByValue(strDirector_code).Selected = true;
                }
                InitcboUnit();
            }
        }

        private void InitcboUnit()
        {
            cUnit oUnit = new cUnit();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strUnit_code = cboUnit.SelectedValue;
            string strDirector_code = cboDirector.SelectedValue;
            string strYear = cboYear.SelectedValue;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and unit.unit_year = '" + strYear + "'  and  unit.c_active='Y' ";
            strCriteria = strCriteria + " and unit.director_code = '" + strDirector_code + "' ";
            if (this.BudgetType == "R")
            {
                strCriteria = strCriteria + " and unit.budget_type <> 'B' ";
            }
            else
            {
                strCriteria = strCriteria + " and unit.budget_type <> 'R' ";
            }
            if (oUnit.SP_SEL_UNIT(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboUnit.Items.Clear();
                cboUnit.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                int i;
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboUnit.Items.Add(new ListItem(dt.Rows[i]["unit_name"].ToString(), dt.Rows[i]["unit_code"].ToString()));
                }
                if (cboUnit.Items.FindByValue(strUnit_code) != null)
                {
                    cboUnit.SelectedIndex = -1;
                    cboUnit.Items.FindByValue(strUnit_code).Selected = true;
                }
            }
        }

        private void InitcboActivity()
        {
            cActivity oActivity = new cActivity();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        stractivity_code = string.Empty,
                        strdirector_code = string.Empty,
                        strunit_code = string.Empty;
            string strYear = cboYear.SelectedValue;
            stractivity_code = cboActivity.SelectedValue;
            strdirector_code = cboDirector.SelectedValue;
            strunit_code = cboUnit.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            if (strdirector_code.Length > 0)
            {
                strCriteria = "  and  director_code='" + strdirector_code + "' ";
            }
            if (strunit_code.Length > 0)
            {
                strCriteria += "  and  unit_code='" + strunit_code + "' ";
            }

            strCriteria += "  and budget_plan_year = '" + strYear + "' ";

            if (DirectorLock == "Y")
            {
                strCriteria += " and substring(director_code,4,2) = substring('" + DirectorCode + "',4,2) ";
            }
            if (this.BudgetType == "R")
            {
                strCriteria = strCriteria + " and budget_type <> 'B' ";
            }
            else
            {
                strCriteria = strCriteria + " and budget_type <> 'R' ";
            }


            if (oActivity.SP_ACTIVITY_BUDGET_SEL(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboActivity.Items.Clear();
                cboActivity.Items.Add(new ListItem("---- เลือกข้อมูลทั้งหมด ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboActivity.Items.Add(new ListItem(dt.Rows[i]["activity_name"].ToString(), dt.Rows[i]["activity_code"].ToString()));
                }
                if (cboActivity.Items.FindByValue(stractivity_code) != null)
                {
                    cboActivity.SelectedIndex = -1;
                    cboActivity.Items.FindByValue(stractivity_code).Selected = true;
                }
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
                    }
                    else
                    {
                        #region get Data
                        strYear = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["yearnow"].ToString();
                        if (DateTime.Now.Year < 2200)
                        {
                            strPay_Year = (DateTime.Now.Year + 543).ToString();
                        }
                        if (DateTime.Now.Month < 10)
                            strPay_Month = "0" + DateTime.Now.Month;
                        else
                            strPay_Month = DateTime.Now.Month.ToString();
                        #endregion
                    }

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


        private void cboPerPage_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            GridView1.PageSize = int.Parse(strRecordPerPage);
            if (int.Parse(strPageNo) != 0)
            {
                BindGridView(int.Parse(strPageNo) - 1);
            }
            else
            {
                BindGridView(0);
            }
        }

        private void imgGo_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            BindGridView(int.Parse(strPageNo) - 1);
        }

        private void BindGridView(int nPageNo)
        {

            cPayment oPayment = new cPayment();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string strCriteria2 = string.Empty;
            string strpayment_year = cboYear.SelectedValue;
            string strpay_month = cboPay_Month.SelectedValue;
            string strpay_year = cboPay_Year.SelectedValue;
            string strDirector_code = cboDirector.SelectedValue;
            string strUnit_code = cboUnit.SelectedValue;
            string stractivity_code = cboActivity.SelectedValue;
            string strstatus = string.Empty;

            strCriteria = strCriteria + "  And  (payment_year  = '" + strpayment_year + "') ";

            strCriteria = strCriteria + "  And  (pay_year  = '" + strpay_year + "') ";

            strCriteria = strCriteria + "  And  (pay_month  = '" + strpay_month + "') ";

            strCriteria2 = " And [budget_money_year] = '" + cboYear.SelectedValue + "' ";

            if (!strDirector_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (Director_code = '" + strDirector_code + "') ";
                strCriteria2 = strCriteria2 + "  And  (Director_code = '" + strDirector_code + "') ";
            }

            if (!strUnit_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (Unit_code = '" + strUnit_code + "') ";
                strCriteria2 = strCriteria2 + "  And  (Unit_code = '" + strUnit_code + "') ";
            }

            if (!stractivity_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (activity_code = '" + stractivity_code + "') ";
                strCriteria2 = strCriteria2 + "  And  (activity_code = '" + stractivity_code + "') ";
            }

            if (DirectorLock == "Y")
            {
                strCriteria += " and director_code = '" + DirectorCode + "' ";
                strCriteria2 += " and director_code = '" + DirectorCode + "' ";
            }

            strCriteria = strCriteria + " and payment_detail_budget_type ='" + this.BudgetType + "' ";
            strCriteria2 = strCriteria2 + " and budget_type ='" + this.BudgetType + "' ";

            //if (RadioLess0.Checked)
            //{
            //    strCriteria2 = "  having isnull( " +
            //    "(select budget_money_head_s.budget_money_all  from budget_money_head as budget_money_head_s " +
            //    " where view_payment_debit.budget_plan_code=budget_money_head_s.budget_plan_code ),0) - isnull(sum([payment_item_recv]),0) < 0 ";
            //}
            //else if (RadioOver0.Checked)
            //{
            //    strCriteria2 = "  having isnull( " +
            //     "(select budget_money_head_s.budget_money_all  from budget_money_head as budget_money_head_s " +
            //     " where view_payment_debit.budget_plan_code=budget_money_head_s.budget_plan_code ),0) - isnull(sum([payment_item_recv]),0) > 0 ";
            //}



            try
            {
                if (!oPayment.SP_PAYMENT_DEBIT_SEL(strCriteria, strCriteria2, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    try
                    {
                        GridView1.PageIndex = nPageNo;
                        txthTotalRecord.Value = ds.Tables[0].Rows.Count.ToString();
                        ds.Tables[0].DefaultView.Sort = ViewState["sort"] + " " + ViewState["direction"];
                        GridView1.DataSource = ds.Tables[0];
                        GridView1.DataBind();
                    }
                    catch
                    {
                        GridView1.PageIndex = 0;
                        txthTotalRecord.Value = ds.Tables[0].Rows.Count.ToString();
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
                oPayment.Dispose();
                ds.Dispose();
                if (GridView1.Rows.Count > 0)
                {
                    GridView1.TopPagerRow.Visible = true;
                }
            }
        }

        protected void imgFind_Click(object sender, ImageClickEventArgs e)
        {
            BindGridView(0);
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


                ViewState["sum_debit_recv"] = 0;
                ViewState["sum_debit_all"] = 0;

                ViewState["sum_tranfer_in"] = 0;
                ViewState["sum_tranfer_out"] = 0;

                ViewState["sum_debit_remain"] = 0;
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
                //Label txtpayment_debit_recv = (Label)e.Row.FindControl("lblpayment_debit_remain");
                //if (decimal.Parse(lblbudget_money_all.Text.Replace(",", "")) < 0)
                //{
                //    lblbudget_money_all.ForeColor = System.Drawing.Color.Red;
                //}
                Label lblbudget_plan_code = (Label)e.Row.FindControl("lblbudget_plan_code");
                //Label lblbudget_money_remain = (Label)e.Row.FindControl("lblbudget_money_remain");

                //txtbudget_money_all.Text = String.Format("{0:#,##0.00}", decimal.Parse(txtbudget_money_all.Text) + decimal.Parse(lblbudget_money_all.Text));
                //txtbudget_money_use.Text = String.Format("{0:#,##0.00}", decimal.Parse(txtbudget_money_use.Text) + decimal.Parse(lblbudget_money_use.Text));
                //txtbudget_money_remain.Text = String.Format("{0:#,##0.00}", decimal.Parse(txtbudget_money_remain.Text) + decimal.Parse(lblbudget_money_remain.Text));

                Aware.WebControls.AwNumeric txtpayment_debit_recv = (Aware.WebControls.AwNumeric)e.Row.FindControl("txtpayment_debit_recv");
                Aware.WebControls.AwNumeric txtpayment_debit_all = (Aware.WebControls.AwNumeric)e.Row.FindControl("txtpayment_debit_all");
                Aware.WebControls.AwNumeric txtpayment_debit_remain = (Aware.WebControls.AwNumeric)e.Row.FindControl("txtpayment_debit_remain");
                Aware.WebControls.AwNumeric txttranfer_in = (Aware.WebControls.AwNumeric)e.Row.FindControl("txttranfer_in");
                Aware.WebControls.AwNumeric txttranfer_out = (Aware.WebControls.AwNumeric)e.Row.FindControl("txttranfer_out");

                txtpayment_debit_remain.Value = double.Parse(txtpayment_debit_all.Value.ToString()) -
                                                double.Parse(txtpayment_debit_recv.Value.ToString()) +
                                                double.Parse(txttranfer_in.Value.ToString()) -
                                                double.Parse(txttranfer_out.Value.ToString());

                ViewState["sum_debit_recv"] = double.Parse(ViewState["sum_debit_recv"].ToString()) + double.Parse(txtpayment_debit_recv.Value.ToString());
                ViewState["sum_debit_all"] = double.Parse(ViewState["sum_debit_all"].ToString()) + double.Parse(txtpayment_debit_all.Value.ToString());
                ViewState["sum_tranfer_in"] = double.Parse(ViewState["sum_tranfer_in"].ToString()) + double.Parse(txttranfer_in.Value.ToString());
                ViewState["sum_tranfer_out"] = double.Parse(ViewState["sum_tranfer_out"].ToString()) + double.Parse(txttranfer_out.Value.ToString());
                ViewState["sum_debit_remain"] = double.Parse(ViewState["sum_debit_remain"].ToString()) + double.Parse(txtpayment_debit_remain.Value.ToString());




                #region set ImageView
                ImageButton imgView = (ImageButton)e.Row.FindControl("imgView");
                imgView.Attributes.Add("onclick", "OpenPopUp('990px','550px','95%' , 'ข้อมูลเงินงบประมาณประจำเดือน' , 'budget_money_month_view.aspx?mode=edit&budget_plan_code=" +
                                                            lblbudget_plan_code.Text + "&page=" + GridView1.PageIndex.ToString() + "&pay_month=" + cboPay_Month.SelectedValue + "&pay_year=" + cboPay_Year.SelectedValue + "' , '1');return false;");
                imgView.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgView"].Rows[0]["img"].ToString();
                imgView.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgView"].Rows[0]["title"].ToString());
                #endregion

                #region set Image Edit & Delete

                ImageButton imgEdit = (ImageButton)e.Row.FindControl("imgEdit");

                imgEdit.Attributes.Add("onclick", "OpenPopUp('990px','550px','95%' , 'ข้อมูลเงินงบประมาณประจำเดือน' , 'budget_money_month_control.aspx?mode=edit&budget_plan_code=" +
                                          lblbudget_plan_code.Text +
                                          "&budget_type=" + this.BudgetType +
                                          "&page=" + GridView1.PageIndex.ToString() +
                                          "&pay_month=" + cboPay_Month.SelectedValue +
                                          "&pay_year=" + cboPay_Year.SelectedValue + "' , '1');return false;");
                imgEdit.ImageUrl = "../../images/controls/edit.gif";
                imgEdit.Attributes.Add("title", "ข้อมูลเงินงบประมาณ");

                //ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                //imgDelete.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["img"].ToString();
                //imgDelete.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["title"].ToString());
                //imgDelete.Attributes.Add("onclick", "return confirm(\"คุณต้องการลบข้อมูล   " + lblbudget_money_doc.Text + " : " + lblunit_name.Text + " ?\");");

                imgEdit.Visible = IsUserEdit;

                #endregion
            }

            if (e.Row.RowType.Equals(DataControlRowType.Footer))
            {

                Aware.WebControls.AwNumeric txtpayment_debit_recv = (Aware.WebControls.AwNumeric)e.Row.FindControl("txtpayment_debit_recv");
                Aware.WebControls.AwNumeric txtpayment_debit_all = (Aware.WebControls.AwNumeric)e.Row.FindControl("txtpayment_debit_all");
                Aware.WebControls.AwNumeric txtpayment_debit_remain = (Aware.WebControls.AwNumeric)e.Row.FindControl("txtpayment_debit_remain");
                Aware.WebControls.AwNumeric txttranfer_in = (Aware.WebControls.AwNumeric)e.Row.FindControl("txttranfer_in");
                Aware.WebControls.AwNumeric txttranfer_out = (Aware.WebControls.AwNumeric)e.Row.FindControl("txttranfer_out");

                txtpayment_debit_recv.Value = ViewState["sum_debit_recv"];
                txtpayment_debit_all.Value = ViewState["sum_debit_all"];

                txttranfer_in.Value = ViewState["sum_tranfer_in"];

                txttranfer_out.Value = ViewState["sum_tranfer_out"];

                txtpayment_debit_remain.Value = ViewState["sum_debit_remain"];


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
            else if (e.Row.RowType.Equals(DataControlRowType.Pager))
            {
                TableCell tbc = e.Row.Cells[0];
                Label lblPrev = null;
                Label lblNext = null;
                ImageButton lbtnPrev = null;
                ImageButton lbtnNext = null;

                #region find and store Previous and Next Page
                TableRow tbr = (TableRow)tbc.Controls[0].Controls[0];
                foreach (System.Web.UI.Control c in tbr.Controls)
                {
                    if (c.GetType().ToString().Equals("System.Web.UI.WebControls.Label"))
                    {
                        Label lbl = (Label)c;
                        if (lbl.Text.IndexOf("P") != -1)
                        {
                            lblPrev = lbl;
                            lblPrev.Text = string.Empty;
                        }
                        if (lbl.Text.IndexOf("N") != -1)
                        {
                            lblNext = lbl;
                            lblNext.Text = string.Empty;
                        }
                    }
                    if (c.Controls[0].GetType().ToString().Equals("System.Web.UI.WebControls.DataControlImageButton"))
                    {
                        ImageButton lbtn = (ImageButton)c.Controls[0];
                        if (lbtn.AlternateText.IndexOf("P") != -1)
                        {
                            lbtnPrev = lbtn;
                            lbtnPrev.ImageUrl = "~/images/prev.gif";
                        }
                        if (lbtn.AlternateText.IndexOf("N") != -1)
                        {
                            lbtnNext = lbtn;
                            lbtnNext.ImageUrl = "~/images/next.gif";
                        }
                    }
                }
                #endregion

                #region render new pager
                tbc.Text = string.Empty;
                Literal lblPager = new Literal();
                lblPager.Text = "<TABLE border='0' width='100%' cellpadding='0' cellspacing='0'><TR><TD width='30%' valign='middle'>";
                tbc.Controls.Add(lblPager);

                Label lblTotalRecord = new Label();
                lblTotalRecord.Attributes.Add("class", "label_h");
                lblTotalRecord.Text = "พบข้อมูล " + txthTotalRecord.Value.ToString() + " รายการ.";
                tbc.Controls.Add(lblTotalRecord);

                lblPager = new Literal();
                lblPager.Text = "</TD><TD width='30%' align='center' valign='middle'>";
                tbc.Controls.Add(lblPager);

                DropDownList cboPerPage = new DropDownList();
                cboPerPage.ID = "cboPerPage";

                DataTable entries;
                if ((DataSet)Application["xmlconfig"] == null)
                    return;
                else
                    entries = ((DataSet)Application["xmlconfig"]).Tables["RecordPerPage"];

                for (int i = 0; i < entries.Rows.Count; i++)
                {
                    cboPerPage.Items.Add(new ListItem(entries.Rows[i][0].ToString(), entries.Rows[i][1].ToString()));
                }

                if (cboPerPage.Items.FindByValue(strRecordPerPage) != null)
                {
                    cboPerPage.Items.FindByValue(strRecordPerPage).Selected = true;
                }

                cboPerPage.AutoPostBack = true;
                cboPerPage.SelectedIndexChanged += new System.EventHandler(cboPerPage_SelectedIndexChanged);
                tbc.Controls.Add(cboPerPage);

                lblPager = new Literal();
                lblPager.Text = "&nbsp;&nbsp;&nbsp;<span class=\"label_h\">รายการ/หน้า</span></TD><TD width='40%' align='right' valign='middle'>";
                tbc.Controls.Add(lblPager);

                if (lblPrev != null)
                {
                    tbc.Controls.Add(lblPrev);
                }
                else if (lbtnPrev != null)
                {
                    tbc.Controls.Add(lbtnPrev);
                }

                lblPager = new Literal();
                lblPager.Text = "&nbsp;&nbsp;&nbsp;<span class=\"label_h\">หน้าที่: </span>";
                tbc.Controls.Add(lblPager);

                TextBox txtPage = new TextBox();
                txtPage.AutoPostBack = false;
                txtPage.ID = "txtPage";
                txtPage.Width = System.Web.UI.WebControls.Unit.Parse("30px");
                txtPage.Attributes.Add("class", "text1");
                txtPage.Style.Add("text-align", "right");
                int nCurrentPage = (GridView1.PageIndex + 1);
                txtPage.Text = nCurrentPage.ToString();//strPageNo;
                txtPage.Attributes.Add("onkeypress", "javascript: return checkKeyCode(event);");
                txtPage.Attributes.Add("onkeyup", "javasctipt: checkInt(this, " + GridView1.PageCount.ToString() + ");");
                tbc.Controls.Add(txtPage);

                lblPager = new Literal();
                lblPager.Text = "<span class=\"label_h\"> จากทั้งหมด " + GridView1.PageCount.ToString() + "&nbsp;&nbsp;</span>";
                tbc.Controls.Add(lblPager);

                lblPager = new Literal();
                lblPager.Text = "&nbsp;&nbsp;";
                tbc.Controls.Add(lblPager);

                ImageButton imgGo = new ImageButton();
                imgGo.ID = "imgGo";
                imgGo.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgGo"].Rows[0]["img"].ToString();
                imgGo.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgGo"].Rows[0]["title"].ToString());
                imgGo.Attributes.Add("onclick", "javascript: return checkPage(" + GridView1.PageCount.ToString() + ",'กรุณาระบุข้อมูลให้ถูกต้อง.|||ctl00$ASPxRoundPanel1$ContentPlaceHolder2$GridView1$ctl01$txtPage');");
                imgGo.Click += new System.Web.UI.ImageClickEventHandler(this.imgGo_Click);
                tbc.Controls.Add(imgGo);

                lblPager = new Literal();
                lblPager.Text = "&nbsp;&nbsp;&nbsp;";
                tbc.Controls.Add(lblPager);

                if (lblNext != null)
                {
                    tbc.Controls.Add(lblNext);
                }
                else if (lbtnNext != null)
                {
                    tbc.Controls.Add(lbtnNext);
                }

                lblPager = new Literal();
                lblPager.Text = "</TD></TR></TABLE>";
                tbc.Controls.Add(lblPager);

                #endregion
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindGridView(e.NewPageIndex);
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
                GridViewRow item = (GridViewRow)GridView1.Controls[0].Controls[0];
                TextBox txtPage = (TextBox)item.FindControl("txtPage");
                string strPage = Request.Form["ctl00$ASPxRoundPanel1$ContentPlaceHolder2$GridView1$ctl01$txtPage"].ToString();
                //   BindGridView(int.Parse(txtPage.Text) - 1);
                BindGridView(int.Parse(strPage) - 1);
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //string strMessage = string.Empty;
            //string strCheck = string.Empty;
            //string strScript = string.Empty;
            //string strUpdatedBy = Session["username"].ToString();
            //Label lblbudget_money_doc = (Label)GridView1.Rows[e.RowIndex].FindControl("lblbudget_money_doc");
            //cBudget_money oBudget_money = new cBudget_money();
            //try
            //{
            //    if (!oBudget_money.SP_BUDGET_MONEY_DEL(lblbudget_money_doc.Text, "N", strUpdatedBy, ref strMessage))
            //    {
            //        lblError.Text = strMessage;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    lblError.Text = ex.Message.ToString();
            //}
            //finally
            //{
            //    oBudget_money.Dispose();
            //}
            //BindGridView(0);
        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboDirector();
            //BindGridView(0);
        }

        protected void cboDirector_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboUnit();
            //BindGridView(0);
        }

        protected void cboUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboActivity();
            //BindGridView(0);
        }

        protected void cboActivity_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BindGridView(0);
        }

        protected void cboPay_Year_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BindGridView(0);
        }

        protected void cboPay_Month_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BindGridView(0);
        }

    }
}
