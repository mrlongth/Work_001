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

namespace myWeb.App_Control.unit
{
    public partial class unit_control : PageBase
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            lblError.Text = "";
            if (!IsPostBack)
            {
                imgSaveOnly.Attributes.Add("onMouseOver", "src='../../images/controls/save2.jpg'");
                imgSaveOnly.Attributes.Add("onMouseOut", "src='../../images/controls/save.jpg'");

                imgClear.Attributes.Add("onMouseOver", "src='../../images/controls/clear2.jpg'");
                imgClear.Attributes.Add("onMouseOut", "src='../../images/controls/clear.jpg'");

                Session["menupopup_name"] = "";
                ViewState["sort"] = "unit_code";
                ViewState["direction"] = "ASC";
                #region set QueryString
                if (Request.QueryString["unit_code"] != null)
                {
                    ViewState["unit_code"] = Request.QueryString["unit_code"].ToString();
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

                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    InitcboYear();
                    InitcboDirector();
                    InitcboBudgetType();
                    ViewState["page"] = Request.QueryString["page"];
                    txtunit_code.ReadOnly = true;
                    txtunit_code.CssClass = "textboxdis";
                    chkStatus.Checked = true;
                    txtunit_code.CssClass = "textboxdis";
                }
                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    setData();
                    txtunit_code.ReadOnly = true;
                    txtunit_code.CssClass = "textboxdis";
                    if (ViewState["PageStatus"] != null)
                    {
                        if (ViewState["PageStatus"].ToString().ToLower().Equals("save"))
                        {
                            txtunit_code.Text = "";
                            txtunit_name.Text = "";
                            txtunit_name.ReadOnly = false;
                            txtunit_name.CssClass = "textbox";
                            chkStatus.Checked = true;
                            string strScript1 =
                                "self.opener.document.forms[0].ctl00$ASPxRoundPanel1$ContentPlaceHolder2$txthpage.value=" + ViewState["page"].ToString() + ";\n" +
                                "self.opener.document.forms[0].submit();\n" +
                                "self.focus();\n";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
                        }
                    }
                }
                if (this.BudgetType == "R")
                {
                    if (cboBudget_type.Items.FindByValue("R") != null)
                    {
                        cboBudget_type.SelectedIndex = -1;
                        cboBudget_type.Items.FindByValue("R").Selected = true;
                    }
                    cboBudget_type.Enabled = false;
                    cboBudget_type.CssClass = "textboxdis";

                }

                #endregion
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
            string strDirector_code = cboDirector.SelectedValue;
            string strYear = cboYear.SelectedValue;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and Director_year = '" + strYear + "'  and  c_active='Y' ";
            if (oDirector.SP_SEL_DIRECTOR(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboDirector.Items.Clear();
                cboDirector.Items.Add(new ListItem("---- เลือกข้อมูลทั้งหมด ----", ""));
                int i;
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboDirector.Items.Add(new ListItem(dt.Rows[i]["Director_name"].ToString(), dt.Rows[i]["Director_code"].ToString()));
                }
                if (cboDirector.Items.FindByValue(strDirector_code) != null)
                {
                    cboDirector.SelectedIndex = -1;
                    cboDirector.Items.FindByValue(strDirector_code).Selected = true;
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
            strCriteria = " Select * from  general where g_type = 'budget_type'  Order by g_sort ";
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

        #endregion

        #region Web Form Designer generated code

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.imgSaveOnly.Click += new System.Web.UI.ImageClickEventHandler(this.imgSaveOnly_Click);
        }

        #endregion

        private bool saveData()
        {
            bool blnResult = false;
            bool blnDup = false;
            string strMessage = string.Empty;
            string strunit_code = string.Empty,
                strunit_year = string.Empty,
                strunit_name = string.Empty,
                strDirector_code = string.Empty,
                strActive = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty;
            string strScript = string.Empty;
            string strBudget_type = string.Empty;

            cUnit oUnit = new cUnit();
            DataSet ds = new DataSet();
            try
            {
                #region set Data
                strunit_code = txtunit_code.Text;
                strunit_name = txtunit_name.Text;
                //unit_year
                strunit_year = cboYear.SelectedValue;
                strBudget_type = cboBudget_type.SelectedValue;
                //Director_code
                strDirector_code = cboDirector.SelectedValue;
                if (Request.Form["ctl00$ASPxRoundPanel1$ContentPlaceHolder1$cboDirector"] != null)
                {
                    strDirector_code = Request.Form["ctl00$ASPxRoundPanel1$ContentPlaceHolder1$cboDirector"].ToString();
                }
                if (chkStatus.Checked == true)
                {
                    strActive = "Y";
                }
                else
                {
                    strActive = "N";
                }
                strCreatedBy = Session["username"].ToString();
                strUpdatedBy = Session["username"].ToString();
                #endregion

                string strCheckAdd = " and unit.unit_code = '" + strunit_code.Trim() + "' ";
                if (!oUnit.SP_SEL_UNIT(strCheckAdd, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region check dup
                        string strCheckDup = string.Empty;
                        strCheckDup = " and unit.unit_name = '" + strunit_name.Trim() + "' and unit.unit_year = '" + strunit_year.Trim() + "' " +
                                                      " and unit.director_code='" + strDirector_code.Trim() + "'  and  unit.unit_code <> '" + strunit_code.Trim() + "' ";
                        if (!oUnit.SP_SEL_UNIT(strCheckDup, ref ds, ref strMessage))
                        {
                            lblError.Text = strMessage;
                        }
                        else
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                strScript =
                                    "alert(\"ไม่สามารถแก้ไขข้อมูลได้ เนื่องจาก" +
                                    "\\nข้อมูลหน่วยงาน : " + strunit_name.Trim() +
                                    "\\nข้อมูลสังกัด : " + cboDirector.SelectedItem.Text +
                                    "\\n ปี : " + strunit_year.Trim() +
                                    "\\nซ้ำ\");\n";
                                blnDup = true;
                            }
                        }
                        #endregion
                        #region edit
                        if (!blnDup)
                        {
                            if (oUnit.SP_UPD_UNIT(strunit_code, strunit_year, strunit_name, strDirector_code, txtunit_order.Value.ToString(), strActive, strUpdatedBy, strBudget_type, ref strMessage))
                            {
                                blnResult = true;
                            }
                            else
                            {
                                lblError.Text = strMessage.ToString();
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "chkdup", strScript, true);
                        }
                        #endregion
                    }
                    else
                    {
                        #region check dup
                        string strCheckDup = string.Empty;
                        strCheckDup = " and unit.unit_name = '" + strunit_name.Trim() + "' and unit.unit_year = '" + strunit_year + "' " +
                                                      " and unit.Director_code='" + strDirector_code.Trim() + "' ";
                        if (!oUnit.SP_SEL_UNIT(strCheckDup, ref ds, ref strMessage))
                        {
                            lblError.Text = strMessage;
                        }
                        else
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                strScript =
                                    "alert(\"ไม่สามารถเพิ่มข้อมูลได้ เนื่องจาก" +
                                    "\\nข้อมูลหน่วยงาน : " + strunit_name.Trim() +
                                    "\\nข้อมูลสังกัด : " + cboDirector.SelectedItem.Text +
                                    "\\n ปี : " + strunit_year.Trim() +
                                    "\\nซ้ำ\");\n";
                                blnDup = true;
                            }
                        }
                        #endregion
                        #region insert
                        if (!blnDup)
                        {
                            if (oUnit.SP_UNIT_INS(strunit_year, strunit_name, strDirector_code, txtunit_order.Value.ToString(), strActive, strCreatedBy, strBudget_type, ref strMessage))
                            {
                                string strGetcode = " and unit_name = '" + strunit_name.Trim() + "' and unit_year = '" + strunit_year + "' " +
                                                                      " and unit.Director_code='" + strDirector_code.Trim() + "' ";
                                if (!oUnit.SP_SEL_UNIT(strGetcode, ref ds, ref strMessage))
                                {
                                    lblError.Text = strMessage;
                                }
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    strunit_code = ds.Tables[0].Rows[0]["unit_code"].ToString();
                                }
                                ViewState["unit_code"] = strunit_code;
                                blnResult = true;
                            }
                            else
                            {
                                lblError.Text = strMessage.ToString();
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "close", strScript, true);
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
                oUnit.Dispose();
            }
            return blnResult;
        }

        private void imgSaveOnly_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (saveData())
            {
                txtunit_code.Text = "";
                txtunit_name.Text = "";
                txtunit_name.ReadOnly = false;
                txtunit_name.CssClass = "textbox";
                txtunit_order.Value = "0";
                chkStatus.Checked = true;
                txtunit_name.Focus();
                BindGridView();
                string strScript1 = "RefreshMain('" + ViewState["page"].ToString() + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
                MsgBox("บันทึกข้อมูลสมบูรณ์");
            }
        }

        private void setData()
        {
            cUnit oUnit = new cUnit();
            DataSet ds = new DataSet();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strunit_code = string.Empty,
                strunit_name = string.Empty,
                strDirector_code = string.Empty,
                strDirector_name = string.Empty,
                strYear = string.Empty,
                strC_active = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty,
                strCreatedDate = string.Empty,
                strUpdatedDate = string.Empty,
                strBudget_type = string.Empty,
                strunit_order = string.Empty;
            try
            {
                strCriteria = " and unit_code = '" + ViewState["unit_code"].ToString() + "' ";
                if (!oUnit.SP_SEL_UNIT(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region get Data
                        strunit_code = ds.Tables[0].Rows[0]["unit_code"].ToString();
                        strunit_name = ds.Tables[0].Rows[0]["unit_name"].ToString();
                        strDirector_code = ds.Tables[0].Rows[0]["Director_code"].ToString();
                        strDirector_name = ds.Tables[0].Rows[0]["Director_name"].ToString();
                        strYear = ds.Tables[0].Rows[0]["unit_year"].ToString();
                        strC_active = ds.Tables[0].Rows[0]["c_active"].ToString();
                        strCreatedBy = ds.Tables[0].Rows[0]["c_created_by"].ToString();
                        strUpdatedBy = ds.Tables[0].Rows[0]["c_updated_by"].ToString();
                        strCreatedDate = ds.Tables[0].Rows[0]["d_created_date"].ToString();
                        strUpdatedDate = ds.Tables[0].Rows[0]["d_updated_date"].ToString();
                        strBudget_type = ds.Tables[0].Rows[0]["budget_type"].ToString();
                        strunit_order = ds.Tables[0].Rows[0]["unit_order"].ToString();

                        #endregion

                        #region set Control
                        txtunit_code.Text = strunit_code;
                        txtunit_name.Text = strunit_name;
                        txtunit_order.Value = strunit_order;
                        InitcboYear();
                        if (cboYear.Items.FindByValue(strYear) != null)
                        {
                            cboYear.SelectedIndex = -1;
                            cboYear.Items.FindByValue(strYear).Selected = true;
                        }
                        InitcboDirector();
                        if (cboDirector.Items.FindByValue(strDirector_code) != null)
                        {
                            cboDirector.SelectedIndex = -1;
                            cboDirector.Items.FindByValue(strDirector_code).Selected = true;
                        }
                        InitcboBudgetType();
                        if (cboBudget_type.Items.FindByValue(strBudget_type) != null)
                        {
                            cboBudget_type.SelectedIndex = -1;
                            cboBudget_type.Items.FindByValue(strBudget_type).Selected = true;
                        }
                        if (strC_active.Equals("Y"))
                        {
                            txtunit_name.ReadOnly = false;
                            txtunit_name.CssClass = "textbox";
                            chkStatus.Checked = true;
                        }
                        else
                        {
                            txtunit_name.ReadOnly = true;
                            txtunit_name.CssClass = "textboxdis";
                            chkStatus.Checked = false;
                        }
                        //cboDirector.Enabled = false;
                        //cboDirector.CssClass = "textboxdis";
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
            cUnit oUnit = new cUnit();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string strDirector_code = string.Empty;
            strDirector_code = cboDirector.SelectedValue;
            strCriteria = strCriteria + "  And  (unit.Director_code = '" + strDirector_code + "') ";
            if (this.BudgetType == "B")
            {
                strCriteria = strCriteria + "  And  (unit.budget_type <> 'R') ";
            }
            else
            {
                strCriteria = strCriteria + "  And  (unit.budget_type = 'R') ";
            }
            try
            {
                if (!oUnit.SP_SEL_UNIT(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    try
                    {
                        ds.Tables[0].DefaultView.Sort = ViewState["sort"] + " " + ViewState["direction"];
                        GridView1.DataSource = ds.Tables[0];
                        GridView1.DataBind();
                    }
                    catch
                    {
                        GridView1.PageIndex = 0;
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
                oUnit.Dispose();
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
                Label lblunit_code = (Label)e.Row.FindControl("lblunit_code");
                Label lblunit_name = (Label)e.Row.FindControl("lblunit_name");
                Label lblc_active = (Label)e.Row.FindControl("lblc_active");
                string strStatus = lblc_active.Text;

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
                Label lblCanEdit = (Label)e.Row.FindControl("lblCanEdit");
                imgEdit.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgEdit"].Rows[0]["img"].ToString();
                imgEdit.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgEdit"].Rows[0]["title"].ToString());

                ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                imgDelete.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["img"].ToString();
                imgDelete.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["title"].ToString());
                imgDelete.Attributes.Add("onclick", "return confirm(\"คุณต้องการลบหน่วยงาน   " + lblunit_code.Text + " : " + lblunit_name.Text + " ?\");");
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

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindGridView();
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
            string strUpdatedBy = Session["username"].ToString();
            Label lblunit_code = (Label)GridView1.Rows[e.RowIndex].FindControl("lblunit_code");
            cUnit oUnit = new cUnit();
            try
            {
                if (!oUnit.SP_DEL_UNIT(lblunit_code.Text, "N", strUpdatedBy, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    string strScript1 = "RefreshMain('" + ViewState["page"].ToString() + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oUnit.Dispose();
            }
            BindGridView();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Label lblunit_code = (Label)GridView1.Rows[e.NewEditIndex].FindControl("lblunit_code");
            Label lblunit_name = (Label)GridView1.Rows[e.NewEditIndex].FindControl("lblunit_name");
            txtunit_code.Text = lblunit_code.Text;
            txtunit_name.Text = lblunit_name.Text;
            Label lblc_active = (Label)GridView1.Rows[e.NewEditIndex].FindControl("lblc_active");
            string strC_active = lblc_active.Text;

            Label lblbudget_type = (Label)GridView1.Rows[e.NewEditIndex].FindControl("lblbudget_type");
            string strbudget_type = lblbudget_type.Text;



            if (strC_active.Equals("Y"))
            {
                txtunit_name.ReadOnly = false;
                txtunit_name.CssClass = "textbox";
                chkStatus.Checked = true;

                InitcboBudgetType();
                if (cboBudget_type.Items.FindByValue(strbudget_type) != null)
                {
                    cboBudget_type.SelectedIndex = -1;
                    cboBudget_type.Items.FindByValue(strbudget_type).Selected = true;
                }


            }
            else
            {
                txtunit_name.ReadOnly = true;
                txtunit_name.CssClass = "textboxdis";
                chkStatus.Checked = false;
            }
            txtunit_code.ReadOnly = true;
            txtunit_code.CssClass = "textboxdis";


            InitcboBudgetType();
            if (cboBudget_type.Items.FindByValue(strbudget_type) != null)
            {
                cboBudget_type.SelectedIndex = -1;
                cboBudget_type.Items.FindByValue(strbudget_type).Selected = true;
            }


            txtunit_name.Focus();
        }

        protected void imgClear_Click(object sender, ImageClickEventArgs e)
        {
            txtunit_code.Text = "";
            txtunit_name.Text = "";
            txtunit_name.ReadOnly = false;
            txtunit_name.CssClass = "textbox";
            chkStatus.Checked = true;
            txtunit_name.Focus();
        }
    }
}