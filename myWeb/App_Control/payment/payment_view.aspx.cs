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

namespace myWeb.App_Control.payment
{
    public partial class payment_view : PageBase
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
        private string strPrefixCtr_main = "ctl00$ContentPlaceHolder1$TabContainer1$";

        protected void Page_Load(object sender, System.EventArgs e)
        {
            lblError.Text = "";
            if (!IsPostBack)
            {
                BtnR1.Style.Add("display", "none");
                LinkButton1.Style.Add("display", "none");
                ViewState["sort"] = "payment_item_recv";
                ViewState["direction"] = "DESC";
                #region set QueryString

                IsUserEdit = false;
                IsUserDelete = false;

                if (Request.QueryString["IsUserEdit"] != null)
                {
                    if (Request.QueryString["IsUserEdit"].ToString() == "Y")
                    {
                        IsUserEdit = true;
                    }
                }

                if (Request.QueryString["IsUserDelete"] != null)
                {
                    if (Request.QueryString["IsUserDelete"].ToString() == "Y")
                    {
                        IsUserDelete = true;
                    }
                }

                if (Request.QueryString["payment_doc"] != null)
                {
                    ViewState["payment_doc"] = Request.QueryString["payment_doc"].ToString();
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

                if (Request.QueryString["PageStatus"] != null)
                {
                    ViewState["PageStatus"] = Request.QueryString["PageStatus"].ToString();
                }



                #endregion

                setData();
                txtpayment_doc.ReadOnly = true;
                txtpayment_doc.CssClass = "textboxdis";

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
            InitcboBudgetType();
            ChangeLabelBudget();
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

        private void InitcboPerson_group()
        {
            cPerson_group oPerson_group = new cPerson_group();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strperson_group_code = string.Empty;
            strperson_group_code = cboPerson_group.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and c_active='Y' ";
            if (oPerson_group.SP_PERSON_GROUP_SEL(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboPerson_group.Items.Clear();
                cboPerson_group.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboPerson_group.Items.Add(new ListItem(dt.Rows[i]["person_group_name"].ToString(), dt.Rows[i]["person_group_code"].ToString()));
                }
                if (cboPerson_group.Items.FindByValue(strperson_group_code) != null)
                {
                    cboPerson_group.SelectedIndex = -1;
                    cboPerson_group.Items.FindByValue(strperson_group_code).Selected = true;
                }
            }
        }

        private void InitcboPerson_work_status()
        {
            cPerson_work_status oPerson_work_status = new cPerson_work_status();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strperson_work_status = string.Empty;
            strperson_work_status = cboPerson_work_status.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and c_active='Y' ";
            if (oPerson_work_status.SP_PERSON_WORK_STATUS_SEL(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboPerson_work_status.Items.Clear();
                //cboPerson_work_status.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboPerson_work_status.Items.Add(new ListItem(dt.Rows[i]["person_work_status_name"].ToString(), dt.Rows[i]["person_work_status_code"].ToString()));
                }
                if (cboPerson_work_status.Items.FindByValue(strperson_work_status) != null)
                {
                    cboPerson_work_status.SelectedIndex = -1;
                    cboPerson_work_status.Items.FindByValue(strperson_work_status).Selected = true;
                }
            }
        }



        private void InitcboBudgetType()
        {
            cCommon oCommon = new cCommon();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strCode = cboBudget_type.SelectedValue;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " Select * from  general where g_type = 'budget_type' and g_code <> 'M' Order by g_sort ";
            if (oCommon.SEL_SQL(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboBudget_type.Items.Clear();
                int i;
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboBudget_type.Items.Add(new ListItem(dt.Rows[i]["g_name"].ToString(), dt.Rows[i]["g_code"].ToString()));
                }
                if (cboBudget_type.Items.FindByValue(strCode) != null)
                {
                    cboBudget_type.SelectedIndex = -1;
                    cboBudget_type.Items.FindByValue(strCode).Selected = true;
                }
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        #endregion


        private void setData()
        {
            cPayment oPayment = new cPayment();
            DataSet ds = new DataSet();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strpayment_doc = string.Empty;
            string strpayment_date = string.Empty;
            string strpayment_year = string.Empty;
            string strpay_year = string.Empty;
            string strpay_month = string.Empty;
            string strperson_code = string.Empty;
            string strperson_name = string.Empty;
            string strunit_name = string.Empty;
            string strposition_name = string.Empty;
            string strcomments = string.Empty;
            string strBudget_type = string.Empty;
            string strpayment_recv = string.Empty;
            string strpayment_pay = string.Empty;
            string strpayment_net = string.Empty;
            string strC_active = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty,
                strCreatedDate = string.Empty,
                strUpdatedDate = string.Empty;


            string strposition_code = string.Empty,

                strperson_level = string.Empty,
                strperson_level_name = string.Empty,
                strtype_position_code = string.Empty,
                strtype_position_name = string.Empty,

                strperson_group = string.Empty,
                strperson_manage_code = string.Empty,
                strperson_manage_name = string.Empty,
                strbudget_plan_code = string.Empty,
                strbudget_name = string.Empty,
                strproduce_name = string.Empty,
                stractivity_name = string.Empty,
                strplan_name = string.Empty,
                strwork_name = string.Empty,
                strfund_name = string.Empty,
                strdirector_name = string.Empty,
                strperson_work_status = string.Empty;
            strBudget_type = string.Empty;

            try
            {
                strCriteria = " and payment_doc = '" + ViewState["payment_doc"].ToString() + "' ";
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
                        strpayment_date = ds.Tables[0].Rows[0]["payment_date"].ToString();
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
                        strpayment_recv = ds.Tables[0].Rows[0]["payment_recv"].ToString();
                        strpayment_pay = ds.Tables[0].Rows[0]["payment_pay"].ToString();
                        strpayment_net = ds.Tables[0].Rows[0]["payment_net"].ToString();
                        strC_active = ds.Tables[0].Rows[0]["c_active"].ToString();
                        strCreatedBy = ds.Tables[0].Rows[0]["c_created_by"].ToString();
                        strUpdatedBy = ds.Tables[0].Rows[0]["c_updated_by"].ToString();
                        strCreatedDate = ds.Tables[0].Rows[0]["d_created_date"].ToString();
                        strUpdatedDate = ds.Tables[0].Rows[0]["d_updated_date"].ToString();


                        strposition_code = ds.Tables[0].Rows[0]["position_code"].ToString();
                        strposition_name = ds.Tables[0].Rows[0]["position_name"].ToString();

                        strperson_level = ds.Tables[0].Rows[0]["person_level"].ToString();
                        strperson_level_name = ds.Tables[0].Rows[0]["level_position_name"].ToString();
                        strtype_position_code = ds.Tables[0].Rows[0]["type_position_code"].ToString();
                        strtype_position_name = ds.Tables[0].Rows[0]["type_position_name"].ToString();

                        strperson_group = ds.Tables[0].Rows[0]["person_group_code"].ToString();
                        strperson_manage_code = ds.Tables[0].Rows[0]["person_manage_code"].ToString();
                        strperson_manage_name = ds.Tables[0].Rows[0]["person_manage_name"].ToString();
                        strbudget_plan_code = ds.Tables[0].Rows[0]["budget_plan_code"].ToString();
                        strbudget_name = ds.Tables[0].Rows[0]["budget_name"].ToString();
                        strproduce_name = ds.Tables[0].Rows[0]["produce_name"].ToString();
                        stractivity_name = ds.Tables[0].Rows[0]["activity_name"].ToString();
                        strplan_name = ds.Tables[0].Rows[0]["plan_name"].ToString();
                        strwork_name = ds.Tables[0].Rows[0]["work_name"].ToString();
                        strfund_name = ds.Tables[0].Rows[0]["fund_name"].ToString();
                        strdirector_name = ds.Tables[0].Rows[0]["director_name"].ToString();
                        strunit_name = ds.Tables[0].Rows[0]["unit_name"].ToString();
                        strperson_work_status = ds.Tables[0].Rows[0]["person_work_status_code"].ToString();

                        #endregion

                        #region set Control
                        txtpayment_doc.Text = strpayment_doc;
                        txtpayment_date.Text = cCommon.CheckDate(strpayment_date);

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

                        InitcboPerson_group();
                        if (cboPerson_group.Items.FindByValue(strperson_group) != null)
                        {
                            cboPerson_group.SelectedIndex = -1;
                            cboPerson_group.Items.FindByValue(strperson_group).Selected = true;
                        }

                        InitcboPerson_work_status();
                        if (cboPerson_work_status.Items.FindByValue(strperson_work_status) != null)
                        {
                            cboPerson_work_status.SelectedIndex = -1;
                            cboPerson_work_status.Items.FindByValue(strperson_work_status).Selected = true;
                        }

                        strBudget_type = ds.Tables[0].Rows[0]["person_budget_type"].ToString();
                        InitcboBudgetType();
                        if (cboBudget_type.Items.FindByValue(strBudget_type) != null)
                        {
                            cboBudget_type.SelectedIndex = -1;
                            cboBudget_type.Items.FindByValue(strBudget_type).Selected = true;
                        }
                        ChangeLabelBudget();


                        txtperson_code.Text = strperson_code;
                        txtperson_name.Text = strperson_name;
                        txtunit_name.Text = strunit_name;
                        txtposition_name.Text = strposition_name;
                        txtcomments.Text = strcomments;

                        txtpayment_recv.Value = strpayment_recv;
                        txtpayment_pay.Value = strpayment_pay;
                        txtpayment_net.Value = strpayment_net;


                        txtposition_code.Text = strposition_code;
                        txtposition_name.Text = strposition_name;

                        txtperson_level.Text = strperson_level;
                        txtlevel_position_name.Text = strperson_level_name;
                        txttype_position_code.Text = strtype_position_code;
                        txttype_position_name.Text = strtype_position_name;

                        //txtperson_postionno.Text = strperson_postionno;
                        txtperson_manage_code.Text = strperson_manage_code;
                        txtperson_manage_name.Text = strperson_manage_name;
                        txtbudget_plan_code.Text = strbudget_plan_code;
                        txtbudget_name.Text = strbudget_name;
                        txtproduce_name.Text = strproduce_name;
                        txtactivity_name.Text = stractivity_name;
                        txtplan_name.Text = strplan_name;
                        txtwork_name.Text = strwork_name;
                        txtfund_name.Text = strfund_name;
                        txtdirector_name.Text = strdirector_name;
                        txtunit_name.Text = strunit_name;

                        if (strC_active.Equals("Y"))
                        {
                            chkStatus.Checked = true;
                        }
                        else
                        {
                            chkStatus.Checked = false;
                        }
                        txtUpdatedBy.Text = strUpdatedBy;
                        txtUpdatedDate.Text = strUpdatedDate;
                        BindGridView();
                        BindGridView2();
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
            cPayment oPayment = new cPayment();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string strunit_code = string.Empty;
            try
            {

                strCriteria = " and payment_doc = '" + ViewState["payment_doc"].ToString() + "' ";
                if (!oPayment.SP_PAYMENT_SEL(strCriteria, ref ds, ref strMessage))
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
                oPayment.Dispose();
                ds.Dispose();
            }
        }

        private void BindGridView2()
        {
            cPayment oPayment = new cPayment();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string strunit_code = string.Empty;
            try
            {

                strCriteria = " and payment_doc = '" + ViewState["payment_doc"].ToString() + "' ";
                if (!oPayment.SP_PAYMENT_ACC_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    ds.Tables[0].DefaultView.Sort = "payment_acc" + " " + ViewState["direction"];
                    GridView2.DataSource = ds.Tables[0];
                    GridView2.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                if (GridView2.Rows.Count == 0)
                {
                    EmptyGridFix(GridView2);
                }
                oPayment.Dispose();
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
                for (int iCol = 0; iCol < e.Row.Cells.Count; iCol++)
                {
                    e.Row.Cells[iCol].Attributes.Add("class", "table_h");
                    e.Row.Cells[iCol].Wrap = false;
                }

                Label lblitem_code = (Label)e.Row.FindControl("lblitem_code");
                Label lblc_active = (Label)e.Row.FindControl("lblc_active");


                ImageButton imgAdd = (ImageButton)e.Row.FindControl("imgAdd");
                imgAdd.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgGridAdd"].Rows[0]["img"].ToString();
                imgAdd.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgGridAdd"].Rows[0]["title"].ToString());
                imgAdd.Attributes.Add("onclick", "OpenPopUp('800px','425px','94%','เพิ่มข้อมูลการจ่ายเงินเดือนรายรับ/จ่ายบุคคลากร','payment_item_control.aspx?mode=add&person_code=" +
                                                                                txtperson_code.Text + "&person_name=" + txtperson_name.Text + "&payment_doc=" +
                                                                                txtpayment_doc.Text + "&year=" + cboYear.SelectedValue + "','2');return false;");

                imgAdd.Visible = false;
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
                Label lblc_active = (Label)e.Row.FindControl("lblc_active");
                Label lblpayment_detail_lot_code = (Label)e.Row.FindControl("lblpayment_detail_lot_code");
                Label lblpayment_detail_lot_name = (Label)e.Row.FindControl("lblpayment_detail_lot_name");

                Label lblbudget_type = (Label)e.Row.FindControl("lblbudget_type");
                HiddenField hddbudget_type = (HiddenField)e.Row.FindControl("hddbudget_type");
                if (lblbudget_type.Text.Equals("R"))
                {
                    lblbudget_type.Text = "เงินรายได้";
                }
                else if (lblbudget_type.Text.Equals("B"))
                {
                    lblbudget_type.Text = "เงินงบประมาณ";
                }

                int nNo = (GridView1.PageSize * GridView1.PageIndex) + e.Row.RowIndex + 1;
                lblNo.Text = nNo.ToString();

                string strStatus = lblc_active.Text;
                DataRowView dv = (DataRowView)e.Row.DataItem;
                DataSet ds = new DataSet();
                cLot objLot = new cLot();
                string strMessage = string.Empty;
                objLot.SP_SEL_LOT(" And  lot_code = '" + lblpayment_detail_lot_code.Text + "'", ref ds, ref strMessage);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblpayment_detail_lot_name.Text = ds.Tables[0].Rows[0]["lot_name"].ToString();
                }
                ds.Dispose();
                objLot.Dispose();

                #region set ImageStatus
                ImageButton imgStatus = (ImageButton)e.Row.FindControl("imgStatus");
                if (strStatus.Equals("Y"))
                {
                    imgStatus.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgStatus"].Rows[0]["img"].ToString();
                    imgStatus.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgStatus"].Rows[0]["title"].ToString());
                    imgStatus.Attributes.Add("onclick", "return false;");
                }
                else
                {
                    imgStatus.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgStatus"].Rows[0]["imgdisable"].ToString();
                    imgStatus.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgStatus"].Rows[0]["titledisable"].ToString());
                    imgStatus.Attributes.Add("onclick", "return false;");
                }
                #endregion

                #region set Image Edit & Delete

                ImageButton imgEdit = (ImageButton)e.Row.FindControl("imgEdit");
                imgEdit.Attributes.Add("onclick", "OpenPopUp('800px','425px','94%','แก้ไขข้อมูลการจ่ายเงินเดือนรายรับ/จ่ายบุคคลากร','payment_item_control.aspx?mode=edit&payment_doc=" +
                            txtpayment_doc.Text + "&item_code=" + lblitem_code.Text +
                            "&budget_type=" + hddbudget_type.Value.ToString() +
                            "&page=" + GridView1.PageIndex.ToString() + "','2');return false;");

                imgEdit.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgEdit"].Rows[0]["img"].ToString();
                imgEdit.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgEdit"].Rows[0]["title"].ToString());

                ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                imgDelete.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["img"].ToString();
                imgDelete.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["title"].ToString());
                imgDelete.Attributes.Add("onclick", "return confirm(\"คุณต้องการลบข้อมูลนี้หรือไม่ ?\");");
                #endregion

                imgEdit.Visible = IsUserEdit;
                imgDelete.Visible = IsUserDelete;

                if (DirectorLock == "Y")
                {
                    if (Helper.CStr(dv["item_type"]) != "C")
                    {
                        imgEdit.Enabled = false;
                        imgEdit.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgEdit"].Rows[0]["imgdisable"].ToString();
                        imgEdit.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgEdit"].Rows[0]["titledisable"].ToString());

                        imgDelete.Enabled = false;
                        imgDelete.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["imgdisable"].ToString();
                        imgDelete.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["titledisable"].ToString());


                    }
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

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            
        }


        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.Header))
            {
                for (int iCol = 0; iCol < e.Row.Cells.Count; iCol++)
                {
                    e.Row.Cells[iCol].Attributes.Add("class", "table_h");
                    e.Row.Cells[iCol].Wrap = false;
                }

                Label lblitem_code = (Label)e.Row.FindControl("lblitem_code");
                Label lblc_active = (Label)e.Row.FindControl("lblc_active");

                ImageButton imgAdd = (ImageButton)e.Row.FindControl("imgAdd");
                imgAdd.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgGridAdd"].Rows[0]["img"].ToString();
                imgAdd.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgGridAdd"].Rows[0]["title"].ToString());
                imgAdd.Attributes.Add("onclick", "OpenPopUp('800px','425px','94%','เพิ่มข้อมูลการจ่ายเงินเดือนรายรับ (ส่วนกลาง)','payment_item_acc_control.aspx?mode=add&person_code=" +
                                                                                txtperson_code.Text + "&person_name=" + txtperson_name.Text + "&payment_doc=" +
                                                                                txtpayment_doc.Text + "&year=" + cboYear.SelectedValue + "','2');return false;");

                imgAdd.Visible = false;
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
                Label lblpayment_detail_lot_code = (Label)e.Row.FindControl("lblpayment_detail_lot_code");
                Label lblpayment_detail_lot_name = (Label)e.Row.FindControl("lblpayment_detail_lot_name");

                int nNo = (GridView2.PageSize * GridView2.PageIndex) + e.Row.RowIndex + 1;
                lblNo.Text = nNo.ToString();

                DataRowView dv = (DataRowView)e.Row.DataItem;
                DataSet ds = new DataSet();
                cLot objLot = new cLot();
                string strMessage = string.Empty;
                objLot.SP_SEL_LOT(" And  lot_code = '" + lblpayment_detail_lot_code.Text + "'", ref ds, ref strMessage);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblpayment_detail_lot_name.Text = ds.Tables[0].Rows[0]["lot_name"].ToString();
                }
                ds.Dispose();
                objLot.Dispose();


                #region set Image Edit & Delete

                ImageButton imgEdit = (ImageButton)e.Row.FindControl("imgEdit");
                imgEdit.Attributes.Add("onclick", "OpenPopUp('800px','425px','94%','แก้ไขข้อมูลการจ่ายเงินเดือนรายรับ (ส่วนกลาง)','payment_item_acc_control.aspx?mode=edit&payment_doc=" +
                            txtpayment_doc.Text + "&item_code=" + lblitem_code.Text + "&page=" + GridView1.PageIndex.ToString() + "','2');return false;");

                imgEdit.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgEdit"].Rows[0]["img"].ToString();
                imgEdit.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgEdit"].Rows[0]["title"].ToString());

                ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                imgDelete.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["img"].ToString();
                imgDelete.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["title"].ToString());
                imgDelete.Attributes.Add("onclick", "return confirm(\"คุณต้องการลบข้อมูลนี้หรือไม่ ?\");");
                #endregion

                imgEdit.Visible = IsUserEdit;
                imgDelete.Visible = IsUserDelete;




                if (DirectorLock == "Y")
                {
                    if (Helper.CStr(dv["item_type"]) != "C")
                    {
                        imgEdit.Enabled = false;
                        imgEdit.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgEdit"].Rows[0]["imgdisable"].ToString();
                        imgEdit.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgEdit"].Rows[0]["titledisable"].ToString());

                        imgDelete.Enabled = false;
                        imgDelete.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["imgdisable"].ToString();
                        imgDelete.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["titledisable"].ToString());


                    }
                }

            }
        }

        protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.Header))
            {
                #region Create Item Header
                bool bSort = false;
                int i = 0;
                for (i = 0; i < GridView2.Columns.Count; i++)
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

        protected void GridView2_Sorting(object sender, GridViewSortEventArgs e)
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
                BindGridView2();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string strMessage = string.Empty;
            string strCheck = string.Empty;
            string strScript = string.Empty;
            string strUpdatedBy = Session["username"].ToString();
            Label lblitem_code = (Label)GridView2.Rows[e.RowIndex].FindControl("lblitem_code");
            cPayment oPayment = new cPayment();
            try
            {
                if (!oPayment.SP_PAYMENT_ACC_DEL(txtpayment_doc.Text, lblitem_code.Text, ref strMessage))
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
                oPayment.Dispose();
            }
            setData();
        }


        protected void BtnR1_Click(object sender, EventArgs e)
        {
            setData();
        }

        protected void BtnR2_Click(object sender, EventArgs e)
        {
            setData2();
        }

        private void setData2()
        {
            cPerson oPerson = new cPerson();
            DataSet ds = new DataSet();
            string strMessage = string.Empty, strCriteria = string.Empty;
            #region clear Data
            //Tab 1 
            string strperson_code = string.Empty,
                strperson_thai_name = string.Empty,
                strperson_thai_surname = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty,
                strCreatedDate = string.Empty,
                strUpdatedDate = string.Empty;
            //Tab 2 
            string strposition_code = string.Empty,
                strposition_name = string.Empty,
                strperson_level = string.Empty,
                strperson_postionno = string.Empty,
                strperson_group = string.Empty,
                strperson_manage_code = string.Empty,
                strperson_manage_name = string.Empty,
                strbudget_plan_code = string.Empty,
                strbudget_name = string.Empty,
                strproduce_name = string.Empty,
                stractivity_name = string.Empty,
                strplan_name = string.Empty,
                strwork_name = string.Empty,
                strfund_name = string.Empty,
                strdirector_name = string.Empty,
                strunit_name = string.Empty,
                strperson_work_status = string.Empty;
            #endregion
            try
            {
                strCriteria = " and person_code = '" + txtperson_code.Text + "' ";
                if (!oPerson.SP_PERSON_ALL_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region get Data
                        strperson_code = ds.Tables[0].Rows[0]["person_code"].ToString();
                        strperson_thai_name = ds.Tables[0].Rows[0]["person_thai_name"].ToString();
                        strperson_thai_surname = ds.Tables[0].Rows[0]["person_thai_surname"].ToString();
                        strCreatedBy = ds.Tables[0].Rows[0]["c_created_by"].ToString();
                        strUpdatedBy = ds.Tables[0].Rows[0]["c_updated_by"].ToString();
                        strCreatedDate = ds.Tables[0].Rows[0]["d_created_date"].ToString();
                        strUpdatedDate = ds.Tables[0].Rows[0]["d_updated_date"].ToString();
                        //Tab 2 
                        strposition_code = ds.Tables[0].Rows[0]["position_code"].ToString();
                        strposition_name = ds.Tables[0].Rows[0]["position_name"].ToString();
                        strperson_level = ds.Tables[0].Rows[0]["person_level"].ToString();
                        strperson_postionno = ds.Tables[0].Rows[0]["person_postionno"].ToString();
                        strperson_group = ds.Tables[0].Rows[0]["person_group_code"].ToString();
                        strperson_manage_code = ds.Tables[0].Rows[0]["person_manage_code"].ToString();
                        strperson_manage_name = ds.Tables[0].Rows[0]["person_manage_name"].ToString();
                        strbudget_plan_code = ds.Tables[0].Rows[0]["budget_plan_code"].ToString();
                        strbudget_name = ds.Tables[0].Rows[0]["budget_name"].ToString();
                        strproduce_name = ds.Tables[0].Rows[0]["produce_name"].ToString();
                        stractivity_name = ds.Tables[0].Rows[0]["activity_name"].ToString();
                        strplan_name = ds.Tables[0].Rows[0]["plan_name"].ToString();
                        strwork_name = ds.Tables[0].Rows[0]["work_name"].ToString();
                        strfund_name = ds.Tables[0].Rows[0]["fund_name"].ToString();
                        strdirector_name = ds.Tables[0].Rows[0]["director_name"].ToString();
                        strunit_name = ds.Tables[0].Rows[0]["unit_name"].ToString();
                        strperson_work_status = ds.Tables[0].Rows[0]["person_work_status_code"].ToString();
                        #endregion

                        #region set Control
                        txtperson_code.Text = strperson_code;
                        txtperson_name.Text = strperson_thai_name + " " + strperson_thai_surname;
                        //txtUpdatedBy.Text = strUpdatedBy;
                        //txtUpdatedDate.Text = strUpdatedDate;

                        //Tab 2 
                        txtposition_code.Text = strposition_code;
                        txtposition_name.Text = strposition_name;
                        txtperson_level.Text = strperson_level;
                        InitcboPerson_group();
                        if (cboPerson_group.Items.FindByValue(strperson_group) != null)
                        {
                            cboPerson_group.SelectedIndex = -1;
                            cboPerson_group.Items.FindByValue(strperson_group).Selected = true;
                        }
                        txtperson_manage_code.Text = strperson_manage_code;
                        txtperson_manage_name.Text = strperson_manage_name;
                        txtbudget_plan_code.Text = strbudget_plan_code;
                        txtbudget_name.Text = strbudget_name;
                        txtproduce_name.Text = strproduce_name;
                        txtactivity_name.Text = stractivity_name;
                        txtplan_name.Text = strplan_name;
                        txtwork_name.Text = strwork_name;
                        txtfund_name.Text = strfund_name;
                        txtdirector_name.Text = strdirector_name;
                        txtunit_name.Text = strunit_name;
                        InitcboPerson_work_status();
                        if (cboPerson_work_status.Items.FindByValue(strperson_work_status) != null)
                        {
                            cboPerson_work_status.SelectedIndex = -1;
                            cboPerson_work_status.Items.FindByValue(strperson_work_status).Selected = true;
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

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            setData2();
        }

        protected void imgClear_item_Click(object sender, ImageClickEventArgs e)
        {
            txtperson_code.Text = string.Empty;
            txtperson_name.Text = string.Empty;


            //Tab 2 
            txtposition_code.Text = string.Empty;
            txtposition_name.Text = string.Empty;
            txtperson_level.Text = string.Empty;
            InitcboPerson_group();

            txtperson_manage_code.Text = string.Empty;
            txtperson_manage_name.Text = string.Empty;
            txtbudget_plan_code.Text = string.Empty;
            txtbudget_name.Text = string.Empty;
            txtproduce_name.Text = string.Empty;
            txtactivity_name.Text = string.Empty;
            txtplan_name.Text = string.Empty;
            txtwork_name.Text = string.Empty;
            txtfund_name.Text = string.Empty;
            txtdirector_name.Text = string.Empty;
            txtunit_name.Text = string.Empty;
            InitcboPerson_work_status();

        }


        private void ChangeLabelBudget()
        {

            //string strBusget_type = cboBudget_type.SelectedValue;

            //if (strBusget_type == "B")
            //{
            //    Label54.Text = "แผนงบ :";
            //    Label55.Text = "ผลผลิต :";
            //    Label53.Text = "กิจกรรม :";
            //    Label56.Text = "แผนงาน :";
            //}
            //else
            //{
            //    Label54.Text = "แผนงาน :";
            //    Label55.Text = "งานหลัก :";
            //    Label53.Text = "งานรอง :";
            //    Label56.Text = "งานย่อย :";
            //}
        }



    }
}