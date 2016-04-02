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
using System.Collections.Generic;
using Aware.WebControls;

namespace myEFrom.App_Control.open
{
    public partial class open_item_control : PageBase
    {

        private bool bIsGridItemEmpty
        {
            get
            {
                if (ViewState["bIsGridItemEmpty"] == null)
                {
                    ViewState["bIsGridItemEmpty"] = false;
                }
                return (bool)ViewState["bIsGridItemEmpty"];
            }
            set
            {
                ViewState["bIsGridItemEmpty"] = value;
            }
        }
        private long OpenItemID
        {
            get
            {
                if (ViewState["OpenItemID"] == null)
                {
                    ViewState["OpenItemID"] = 1000000;
                }
                return long.Parse(ViewState["OpenItemID"].ToString());
            }
            set
            {
                ViewState["OpenItemID"] = value;
            }
        }
        private DataTable dtOpenItem
        {
            get
            {
                if (ViewState["dtOpenItem"] == null)
                {
                    cefOpenItem objEfOpenItem = new cefOpenItem();
                    DataTable dt;
                    _strCriteria = " and open_code = " + Helper.CInt(txtopen_code.Text);
                    dt = objEfOpenItem.SP_OPEN_ITEM_SEL(_strCriteria);
                    ViewState["dtOpenItem"] = dt;
                }
                return (DataTable)ViewState["dtOpenItem"];
            }
            set
            {
                ViewState["dtOpenItem"] = value;
            }
        }

        private bool bIsGridApproveEmpty
        {
            get
            {
                if (ViewState["bIsGridApproveEmpty"] == null)
                {
                    ViewState["bIsGridApproveEmpty"] = false;
                }
                return (bool)ViewState["bIsGridApproveEmpty"];
            }
            set
            {
                ViewState["bIsGridApproveEmpty"] = value;
            }
        }
        private long OpenApproveID
        {
            get
            {
                if (ViewState["OpenApproveID"] == null)
                {
                    ViewState["OpenApproveID"] = 1000000;
                }
                return long.Parse(ViewState["OpenApproveID"].ToString());
            }
            set
            {
                ViewState["OpenApproveID"] = value;
            }
        }
        private DataTable dtOpenApprove
        {
            get
            {
                if (ViewState["dtOpenApprove"] == null)
                {
                    cefOpenApprove objEfOpenApprove = new cefOpenApprove();
                    DataTable dt;
                    _strCriteria = " and open_code = " + Helper.CInt(txtopen_code.Text) + " and budget_type='B' ";
                    dt = objEfOpenApprove.SP_OPEN_APPROVE_SEL(_strCriteria);
                    ViewState["dtOpenApprove"] = dt;
                }
                return (DataTable)ViewState["dtOpenApprove"];
            }
            set
            {
                ViewState["dtOpenApprove"] = value;
            }
        }


        private bool bIsGridApproveIncomeEmpty
        {
            get
            {
                if (ViewState["bIsGridApproveIncomeEmpty"] == null)
                {
                    ViewState["bIsGridApproveIncomeEmpty"] = false;
                }
                return (bool)ViewState["bIsGridApproveIncomeEmpty"];
            }
            set
            {
                ViewState["bIsGridApproveIncomeEmpty"] = value;
            }
        }
        private long OpenApproveIncomeID
        {
            get
            {
                if (ViewState["OpenApproveIncomeID"] == null)
                {
                    ViewState["OpenApproveIncomeID"] = 1000000;
                }
                return long.Parse(ViewState["OpenApproveIncomeID"].ToString());
            }
            set
            {
                ViewState["OpenApproveIncomeID"] = value;
            }
        }
        private DataTable dtOpenApproveIncome
        {
            get
            {
                if (ViewState["dtOpenApproveIncome"] == null)
                {
                    cefOpenApprove objEfOpenApproveIncome = new cefOpenApprove();
                    DataTable dt;
                    _strCriteria = " and open_code = " + Helper.CInt(txtopen_code.Text) + " and budget_type='R' " ;
                    dt = objEfOpenApproveIncome.SP_OPEN_APPROVE_SEL(_strCriteria);
                    ViewState["dtOpenApproveIncome"] = dt;
                }
                return (DataTable)ViewState["dtOpenApproveIncome"];
            }
            set
            {
                ViewState["dtOpenApproveIncome"] = value;
            }
        }


        private DataTable dtApprove
        {
            get
            {
                if (ViewState["dtApprove"] == null)
                {
                    cefApprove objEfApprove = new cefApprove();
                    DataTable dt;
                    _strCriteria = " order by approve_level";
                    dt = objEfApprove.SP_APPROVE_SEL(_strCriteria);
                    ViewState["dtApprove"] = dt;
                }
                return (DataTable)ViewState["dtApprove"];
            }
            set
            {
                ViewState["dtApprove"] = value;
            }
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "RegisterScript", "RegisterScript();", true);
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            base.PermissionURL = "~/App_Control/open/open_item_list.aspx";
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            lblError.Text = "";
            if (!ScriptManager.GetCurrent(this).IsInAsyncPostBack)
            {
                ScriptManager.RegisterOnSubmitStatement(this, this.GetType(), "BeforePostback", "BeforePostback()");
            }
      
            if (!IsPostBack)
            {
                imgSaveOnly.Attributes.Add("onMouseOver", "src='../../images/button/save_add2.png'");
                imgSaveOnly.Attributes.Add("onMouseOut", "src='../../images/button/save_add.png'");

                ViewState["sort"] = "open_item_id";
                ViewState["direction"] = "ASC";

                ViewState["sort2"] = "open_approve_id";
                ViewState["direction2"] = "ASC";


                ViewState["sort3"] = "open_approve_id";
                ViewState["direction3"] = "ASC";

                
                #region set QueryString

                if (Request.QueryString["open_code"] != null)
                {
                    ViewState["open_code"] = Request.QueryString["open_code"].ToString();
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

                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    ViewState["page"] = Request.QueryString["page"];
                    TabContainer1.Tabs[0].Visible = true;
                    TabContainer1.Tabs[1].Visible = false;
                    TabContainer1.Tabs[2].Visible = false;
                    TabContainer1.Tabs[3].Visible = false;
                }
                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    setData();
                }
            }
        }

        private bool saveData()
        {
            bool blnResult = false;
            string strMessage = string.Empty;
            int intopen_code = 0;
            string strcomments = string.Empty;
            string strCreatedBy = string.Empty;
            string strUpdatedBy = string.Empty;
            string strUnitCode = string.Empty;
            string strDirectorCode = string.Empty;
            cefOpen objEfOpen = new cefOpen();
            try
            {
                #region set Data
                intopen_code = Helper.CInt(txtopen_code.Text);
                strCreatedBy = Session["username"].ToString();
                strUpdatedBy = Session["username"].ToString();
                #endregion
                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    #region insert
                    if (objEfOpen.SP_OPEN_INS(ref intopen_code, txtopen_to.Text, txtopen_title.Text, txtopen_command_desc.Text,
                        txtopen_desc.Text, txtopen_report_code.Text.Trim(), txtopen_remark.Text.Trim(), strUpdatedBy))
                    {
                        ViewState["open_code"] = intopen_code;
                        blnResult = true;
                    }
                    #endregion
                }
                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    #region update
                    if (objEfOpen.SP_OPEN_UPD(intopen_code, txtopen_to.Text, txtopen_title.Text, txtopen_command_desc.Text, txtopen_desc.Text,
                        txtopen_report_code.Text.Trim(), txtopen_remark.Text.Trim(), strUpdatedBy))
                    {
                        SaveItem();
                        SaveApprove();
                        SaveApproveIncome();
                        blnResult = true;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate") && ex.Message.Contains("IX_open_to"))
                {
                    string strScript = "alert(\"ไม่สามารถแก้ไขข้อมูล เนื่องจากข้อมูล " + txtopen_title.Text + "  ซ้ำ\");\n";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "frMainPage", strScript, true);
                }
                else
                {
                    lblError.Text = ex.Message.ToString();
                }
            }
            finally
            {
                objEfOpen.Dispose();
            }
            return blnResult;
        }

        private void setData()
        {
            cefOpen objEfOpen = new cefOpen();
            DataTable dt;
            string strMessage = string.Empty,
                strCriteria = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty,
                strCreatedDate = string.Empty,
                strUpdatedDate = string.Empty,
                stropen_to_source = string.Empty;
            try
            {
                txtopen_code.ReadOnly = true;
                txtopen_code.CssClass = "textboxdis";
                ViewState["mode"] = "edit";
                strCriteria = " and open_code = '" + ViewState["open_code"].ToString() + "' ";
                dt = objEfOpen.SP_OPEN_SEL(strCriteria);
                if (dt.Rows.Count > 0)
                {
                    #region get Data
                    //cboYear.SelectedValue = ds.Tables[0].Rows[0]["open_year"].ToString();
                    txtopen_code.Text = dt.Rows[0]["open_code"].ToString();
                    txtopen_to.Text = dt.Rows[0]["open_to"].ToString();
                    txtopen_title.Text = dt.Rows[0]["open_title"].ToString();
                    txtopen_command_desc.Text = dt.Rows[0]["open_command_desc"].ToString();

                    txtopen_desc.Text = dt.Rows[0]["open_desc"].ToString();
                    txtopen_remark.Text = dt.Rows[0]["open_remark"].ToString();

                    txtopen_report_code.Text = dt.Rows[0]["open_report_code"].ToString();

                    strCreatedBy = dt.Rows[0]["c_created_by"].ToString();
                    strUpdatedBy = dt.Rows[0]["c_updated_by"].ToString();
                    strCreatedDate = dt.Rows[0]["d_created_date"].ToString();
                    strUpdatedDate = dt.Rows[0]["d_updated_date"].ToString();
                    #endregion

                    #region set Control
                    txtUpdatedBy.Text = strUpdatedBy;
                    txtUpdatedDate.Text = strUpdatedDate;
                    BindGridItem();
                    BindGridApprove();
                    BindGridApproveIncome();
                    #endregion
                }

                TabContainer1.Tabs[0].Visible = true;
                TabContainer1.Tabs[1].Visible = true;
                TabContainer1.Tabs[2].Visible = true;
                TabContainer1.Tabs[3].Visible = true;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        private void InitcboApprove(DropDownList cboApprove)
        {
            string strapprove_code = cboApprove.SelectedValue;
            cboApprove.Items.Clear();
            cboApprove.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
            for (var i = 0; i <= this.dtApprove.Rows.Count - 1; i++)
            {
                cboApprove.Items.Add(new ListItem(this.dtApprove.Rows[i]["approve_name"].ToString(),
                    this.dtApprove.Rows[i]["approve_code"].ToString()));
            }
            if (cboApprove.Items.FindByValue(strapprove_code) != null)
            {
                cboApprove.SelectedIndex = -1;
                cboApprove.Items.FindByValue(strapprove_code).Selected = true;
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
                grdView.DataSource != null)
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

        #region GridView2 Event

        private void StoreItem()
        {
            try
            {
                HiddenField hddopen_item_id;
                HiddenField hddmaterial_id;
                TextBox txtmaterial_code;
                TextBox txtmaterial_name;
                AwNumeric txtopen_rate;
                foreach (GridViewRow gvRow in GridView2.Rows)
                {
                    hddopen_item_id = (HiddenField)gvRow.FindControl("hddopen_item_id");
                    hddmaterial_id = (HiddenField)gvRow.FindControl("hddmaterial_id");
                    txtmaterial_code = (TextBox)gvRow.FindControl("txtmaterial_code");
                    txtmaterial_name = (TextBox)gvRow.FindControl("txtmaterial_name");
                    txtopen_rate = (AwNumeric)gvRow.FindControl("txtopen_rate");
                    foreach (DataRow dr in this.dtOpenItem.Rows)
                    {
                        if (Helper.CLong(dr["open_item_id"]) == Helper.CLong(hddopen_item_id.Value))
                        {
                            dr["material_id"] = hddmaterial_id.Value;
                            dr["material_code"] = txtmaterial_code.Text;
                            dr["material_name"] = txtmaterial_name.Text;
                            dr["open_rate"] = txtopen_rate.Value;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
            }
        }
        private void BindGridItem()
        {
            DataView dv = null;
            try
            {
                dv = new DataView(this.dtOpenItem, "", (ViewState["sort"] + " " + ViewState["direction"]), DataViewRowState.CurrentRows);
                GridView2.DataSource = dv.ToTable();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                this.bIsGridItemEmpty = false;
                if (dv.ToTable().Rows.Count == 0)
                {
                    this.bIsGridItemEmpty = true;
                    EmptyGridFix(GridView2);
                }
                else
                {
                    GridView2.DataBind();
                }
            }
        }
        private bool SaveItem()
        {
            bool blnResult = false;
            cefOpenItem objEfOpenItem = new cefOpenItem();
            try
            {
                StoreItem();
                if (objEfOpenItem.SP_OPEN_ITEM_DEL_BY_OPEN_CODE(Helper.CInt(txtopen_code.Text)))
                {
                    foreach (DataRow dr in this.dtOpenItem.Rows)
                    {
                        if (Helper.CInt(dr["material_id"]) > 0)
                        {
                            if (objEfOpenItem.SP_OPEN_ITEM_INS(
                                    Helper.CInt(txtopen_code.Text),
                                    Helper.CInt(dr["material_id"]),
                                    Helper.CDbl(dr["open_rate"])))
                            {
                                blnResult = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objEfOpenItem.Dispose();
            }
            return blnResult;
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

                ImageButton imgAdd = (ImageButton)e.Row.FindControl("imgAdd");
                imgAdd.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgGridAdd"].Rows[0]["img"].ToString();
                imgAdd.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgGridAdd"].Rows[0]["title"].ToString());

            }
            else if (e.Row.RowType.Equals(DataControlRowType.DataRow) || e.Row.RowState.Equals(DataControlRowState.Alternate))
            {
                if (!this.bIsGridItemEmpty)
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
                    DataRowView dv = (DataRowView)e.Row.DataItem;
                    Label lblNo = (Label)e.Row.FindControl("lblNo");
                    int nNo = (GridView2.PageSize * GridView2.PageIndex) + e.Row.RowIndex + 1;
                    lblNo.Text = nNo.ToString();

                    #region set Image Delete

                    ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                    imgDelete.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["img"].ToString();
                    imgDelete.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["title"].ToString());
                    imgDelete.Attributes.Add("onclick", "return confirm(\"คุณต้องการลบข้อมูลนี้หรือไม่ ?\");");
                    imgDelete.Visible = base.IsUserDelete;
                    #endregion
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
                    if (ViewState["sort"].Equals(GridView2.Columns[i].SortExpression))
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
        }

        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string strMessage = string.Empty;
            string strScript = string.Empty;
            HiddenField hddopen_item_id = (HiddenField)GridView2.Rows[e.RowIndex].FindControl("hddopen_item_id");
            try
            {
                StoreItem();
                int i = 0;
                foreach (DataRow dr in this.dtOpenItem.Rows)
                {
                    if (Helper.CLong(dr["open_item_id"]) == Helper.CLong(hddopen_item_id.Value))
                    {
                        this.dtOpenItem.Rows.Remove(dr);
                        break;
                    }
                    ++i;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
            }
            BindGridItem();
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {
                case "ADD":
                    StoreItem();
                    DataRow dr = this.dtOpenItem.NewRow();
                    dr["open_item_id"] = ++this.OpenItemID;
                    dr["material_id"] = 0;
                    dr["material_code"] = "";
                    dr["material_name"] = "";
                    dr["open_rate"] = 0;
                    this.dtOpenItem.Rows.Add(dr);
                    BindGridItem();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region GridView3 Event

        private void StoreApprove()
        {
            try
            {
                HiddenField hddopen_approve_id;
                DropDownList cboApprove;
                AwNumeric txtapprove_level;
                TextBox txtperson_manage_code;
                TextBox txtperson_manage_name;
                TextBox txtapprove_person_code;
                TextBox txtapprove_person_name;
                foreach (GridViewRow gvRow in GridView3.Rows)
                {
                    hddopen_approve_id = (HiddenField)gvRow.FindControl("hddopen_approve_id");
                    cboApprove = (DropDownList)gvRow.FindControl("cboApprove");
                    txtapprove_level = (AwNumeric)gvRow.FindControl("txtapprove_level");
                    txtperson_manage_code = (TextBox)gvRow.FindControl("txtperson_manage_code");
                    txtperson_manage_name = (TextBox)gvRow.FindControl("txtperson_manage_name");
                    txtapprove_person_code = (TextBox)gvRow.FindControl("txtapprove_person_code");
                    txtapprove_person_name = (TextBox)gvRow.FindControl("txtapprove_person_name");


                    foreach (DataRow dr in this.dtOpenApprove.Rows)
                    {
                        if (Helper.CLong(dr["open_approve_id"]) == Helper.CLong(hddopen_approve_id.Value))
                        {
                            dr["approve_code"] = Helper.CInt(cboApprove.SelectedValue);
                            dr["approve_level"] = Helper.CInt(txtapprove_level.Value);
                            dr["person_manage_code"] = txtperson_manage_code.Text;
                            dr["person_manage_name"] = txtperson_manage_name.Text;
                            dr["person_approve_code"] = txtapprove_person_code.Text;
                            dr["person_name"] = txtapprove_person_name.Text;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
            }
        }
        private void BindGridApprove()
        {
            DataView dv = null;
            try
            {
                dv = new DataView(this.dtOpenApprove, "", (ViewState["sort2"] + " " + ViewState["direction2"]), DataViewRowState.CurrentRows);
                GridView3.DataSource = dv.ToTable();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                this.bIsGridApproveEmpty = false;
                if (dv.ToTable().Rows.Count == 0)
                {
                    this.bIsGridApproveEmpty = true;
                    EmptyGridFix(GridView3);
                }
                else
                {
                    GridView3.DataBind();
                }
            }
        }
        private bool SaveApprove()
        {
            bool blnResult = false;
            cefOpenApprove objEfOpenApprove = new cefOpenApprove();
            try
            {
                StoreApprove();
                if (objEfOpenApprove.SP_OPEN_APPROVE_DEL_BY_OPEN_CODE(Helper.CInt(txtopen_code.Text),"B"))
                {
                    foreach (DataRow dr in this.dtOpenApprove.Rows)
                    {
                        if (Helper.CStr(dr["person_approve_code"]) != "")
                        {
                            if (objEfOpenApprove.SP_OPEN_APPROVE_INS(
                                    Helper.CInt(txtopen_code.Text),
                                    Helper.CInt(dr["approve_code"]),
                                    Helper.CInt(dr["approve_level"]),
                                    Helper.CStr(dr["person_manage_code"]),
                                    Helper.CStr(dr["person_manage_name"]),
                                    Helper.CStr(dr["person_approve_code"]),
                                    "B"))
                            {
                                blnResult = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objEfOpenApprove.Dispose();
            }
            return blnResult;
        }

        protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.Header))
            {
                for (int iCol = 0; iCol < e.Row.Cells.Count; iCol++)
                {
                    e.Row.Cells[iCol].Attributes.Add("class", "table_h");
                    e.Row.Cells[iCol].Wrap = false;
                }

                ImageButton imgAdd = (ImageButton)e.Row.FindControl("imgAdd");
                imgAdd.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgGridAdd"].Rows[0]["img"].ToString();
                imgAdd.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgGridAdd"].Rows[0]["title"].ToString());

            }
            else if (e.Row.RowType.Equals(DataControlRowType.DataRow) || e.Row.RowState.Equals(DataControlRowState.Alternate))
            {
                if (!this.bIsGridApproveEmpty)
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
                    DataRowView dv = (DataRowView)e.Row.DataItem;
                    Label lblNo = (Label)e.Row.FindControl("lblNo");
                    int nNo = (GridView3.PageSize * GridView3.PageIndex) + e.Row.RowIndex + 1;
                    lblNo.Text = nNo.ToString();
                    var strApprove_code = Helper.CStr(dv["Approve_code"]);
                    DropDownList cboApprove = (DropDownList)e.Row.FindControl("cboApprove");
                    this.InitcboApprove(cboApprove);
                    if (cboApprove.Items.FindByValue(strApprove_code) != null)
                    {
                        cboApprove.SelectedIndex = -1;
                        cboApprove.Items.FindByValue(strApprove_code).Selected = true;
                    }

                    #region set Image Delete

                    ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                    imgDelete.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["img"].ToString();
                    imgDelete.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["title"].ToString());
                    imgDelete.Attributes.Add("onclick", "return confirm(\"คุณต้องการลบข้อมูลนี้หรือไม่ ?\");");
                    imgDelete.Visible = base.IsUserDelete;
                    #endregion
                }

            }
        }

        protected void GridView3_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.Header))
            {
                #region Create Item Header
                bool bSort = false;
                int i = 0;
                for (i = 0; i < GridView3.Columns.Count; i++)
                {
                    if (ViewState["sort"].Equals(GridView3.Columns[i].SortExpression))
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

        protected void GridView3_Sorting(object sender, GridViewSortEventArgs e)
        {
        }

        protected void GridView3_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string strMessage = string.Empty;
            string strScript = string.Empty;
            HiddenField hddopen_approve_id = (HiddenField)GridView3.Rows[e.RowIndex].FindControl("hddopen_approve_id");
            try
            {
                StoreApprove();
                int i = 0;
                foreach (DataRow dr in this.dtOpenApprove.Rows)
                {
                    if (Helper.CInt(dr["open_approve_id"]) == Helper.CInt(hddopen_approve_id.Value))
                    {
                        this.dtOpenApprove.Rows.Remove(dr);
                        break;
                    }
                    ++i;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
            }
            BindGridApprove();
        }

        protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {
                case "ADD":
                    StoreApprove();
                    DataRow dr = this.dtOpenApprove.NewRow();
                    dr["open_approve_id"] = ++this.OpenApproveID;
                    dr["approve_code"] = 0;
                    dr["approve_level"] = 0;
                    dr["person_manage_code"] = "";
                    dr["person_manage_name"] = "";
                    dr["person_approve_code"] = "";
                    dr["person_name"] = "";
                    this.dtOpenApprove.Rows.Add(dr);
                    BindGridApprove();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region GridView4 Event

        private void StoreApproveIncome()
        {
            try
            {
                HiddenField hddopen_approve_id;
                DropDownList cboApprove;
                AwNumeric txtapprove_level;
                TextBox txtperson_manage_code;
                TextBox txtperson_manage_name;
                TextBox txtapprove_person_code;
                TextBox txtapprove_person_name;
                foreach (GridViewRow gvRow in GridView4.Rows)
                {
                    
                    hddopen_approve_id = (HiddenField)gvRow.FindControl("hddopen_approve_id");
                    cboApprove = (DropDownList)gvRow.FindControl("cboApprove");
                    txtapprove_level = (AwNumeric)gvRow.FindControl("txtapprove_level");
                    txtperson_manage_code = (TextBox)gvRow.FindControl("txtperson_manage_code");
                    txtperson_manage_name = (TextBox)gvRow.FindControl("txtperson_manage_name");
                    txtapprove_person_code = (TextBox)gvRow.FindControl("txtapprove_person_code");
                    txtapprove_person_name = (TextBox)gvRow.FindControl("txtapprove_person_name");

                    foreach (DataRow dr in this.dtOpenApproveIncome.Rows)
                    {
                        if (Helper.CLong(dr["open_approve_id"]) == Helper.CLong(hddopen_approve_id.Value))
                        {
                            dr["approve_code"] = Helper.CInt(cboApprove.SelectedValue);
                            dr["approve_level"] = Helper.CInt(txtapprove_level.Value);
                            dr["person_manage_code"] = txtperson_manage_code.Text;
                            dr["person_manage_name"] = txtperson_manage_name.Text;
                            dr["person_approve_code"] = txtapprove_person_code.Text;
                            dr["person_name"] = txtapprove_person_name.Text;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
            }
        }
        private void BindGridApproveIncome()
        {
            DataView dv = null;
            try
            {
                dv = new DataView(this.dtOpenApproveIncome, "", (ViewState["sort3"] + " " + ViewState["direction3"]), DataViewRowState.CurrentRows);
                GridView4.DataSource = dv.ToTable();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                this.bIsGridApproveIncomeEmpty = false;
                if (dv.ToTable().Rows.Count == 0)
                {
                    this.bIsGridApproveIncomeEmpty = true;
                    EmptyGridFix(GridView4);
                }
                else
                {
                    GridView4.DataBind();
                }
            }
        }
        private bool SaveApproveIncome()
        {
            bool blnResult = false;
            cefOpenApprove objEfOpenApprove = new cefOpenApprove();
            try
            {
                StoreApproveIncome();
                if (objEfOpenApprove.SP_OPEN_APPROVE_DEL_BY_OPEN_CODE(Helper.CInt(txtopen_code.Text), "R"))
                {
                    foreach (DataRow dr in this.dtOpenApproveIncome.Rows)
                    {
                        if (Helper.CStr(dr["person_approve_code"]) != "")
                        {
                            if (objEfOpenApprove.SP_OPEN_APPROVE_INS(
                                    Helper.CInt(txtopen_code.Text),
                                    Helper.CInt(dr["approve_code"]),
                                    Helper.CInt(dr["approve_level"]),
                                    Helper.CStr(dr["person_manage_code"]),
                                    Helper.CStr(dr["person_manage_name"]),
                                    Helper.CStr(dr["person_approve_code"]),
                                    "R"))
                            {
                                blnResult = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objEfOpenApprove.Dispose();
            }
            return blnResult;
        }


        protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.Header))
            {
                for (int iCol = 0; iCol < e.Row.Cells.Count; iCol++)
                {
                    e.Row.Cells[iCol].Attributes.Add("class", "table_h");
                    e.Row.Cells[iCol].Wrap = false;
                }

                ImageButton imgAdd = (ImageButton)e.Row.FindControl("imgAdd");
                imgAdd.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgGridAdd"].Rows[0]["img"].ToString();
                imgAdd.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgGridAdd"].Rows[0]["title"].ToString());

            }
            else if (e.Row.RowType.Equals(DataControlRowType.DataRow) || e.Row.RowState.Equals(DataControlRowState.Alternate))
            {
                if (!this.bIsGridApproveIncomeEmpty)
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
                    DataRowView dv = (DataRowView)e.Row.DataItem;
                    Label lblNo = (Label)e.Row.FindControl("lblNo");
                    int nNo = (GridView4.PageSize * GridView4.PageIndex) + e.Row.RowIndex + 1;
                    lblNo.Text = nNo.ToString();
                    var strApprove_code = Helper.CStr(dv["Approve_code"]);
                    DropDownList cboApprove = (DropDownList)e.Row.FindControl("cboApprove");
                    this.InitcboApprove(cboApprove);
                    if (cboApprove.Items.FindByValue(strApprove_code) != null)
                    {
                        cboApprove.SelectedIndex = -1;
                        cboApprove.Items.FindByValue(strApprove_code).Selected = true;
                    }

                    #region set Image Delete

                    ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                    imgDelete.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["img"].ToString();
                    imgDelete.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["title"].ToString());
                    imgDelete.Attributes.Add("onclick", "return confirm(\"คุณต้องการลบข้อมูลนี้หรือไม่ ?\");");
                    imgDelete.Visible = base.IsUserDelete;
                    #endregion
                }

            }
        }

        protected void GridView4_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.Header))
            {
                #region Create Item Header
                bool bSort = false;
                int i = 0;
                for (i = 0; i < GridView4.Columns.Count; i++)
                {
                    if (ViewState["sort3"].Equals(GridView4.Columns[i].SortExpression))
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
                            if (ViewState["direction3"].Equals("ASC"))
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

        protected void GridView4_Sorting(object sender, GridViewSortEventArgs e)
        {
        }

        protected void GridView4_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string strMessage = string.Empty;
            string strScript = string.Empty;
            HiddenField hddopen_approve_id = (HiddenField)GridView4.Rows[e.RowIndex].FindControl("hddopen_approve_id");
            try
            {
                StoreApproveIncome();
                int i = 0;
                foreach (DataRow dr in this.dtOpenApproveIncome.Rows)
                {
                    if (Helper.CInt(dr["open_approve_id"]) == Helper.CInt(hddopen_approve_id.Value))
                    {
                        this.dtOpenApproveIncome.Rows.Remove(dr);
                        break;
                    }
                    ++i;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
            }
            BindGridApproveIncome();
        }

        protected void GridView4_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {
                case "ADD":
                    StoreApproveIncome();
                    DataRow dr = this.dtOpenApproveIncome.NewRow();
                    dr["open_approve_id"] = ++this.OpenApproveIncomeID ;
                    dr["approve_code"] = 0;
                    dr["approve_level"] = 0;
                    dr["person_manage_code"] = "";
                    dr["person_manage_name"] = "";
                    dr["person_approve_code"] = "";
                    dr["person_name"] = "";
                    this.dtOpenApproveIncome.Rows.Add(dr);
                    BindGridApproveIncome();
                    break;
                default:
                    break;
            }
        }
        #endregion


        protected void BtnR1_Click(object sender, EventArgs e)
        {
            setData();
        }

        protected void imgSaveOnly_Click(object sender, ImageClickEventArgs e)
        {
            if (saveData())
            {
                string strScript1 = "$('#divdes1').text().replace('เพิ่ม','แก้ไข');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
                MsgBox("บันทึกข้อมูลสมบูรณ์");
                setData();
            }
        }

        protected void cboApprove_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList cboApprove = (DropDownList)sender;
            AwNumeric txtapprove_level = (AwNumeric)cboApprove.NamingContainer.FindControl("txtapprove_level");
            var dv = new DataView(this.dtApprove, "approve_code='" + cboApprove.SelectedValue + "'", "", DataViewRowState.CurrentRows);
            DataTable dt = dv.ToTable();
            if (dt.Rows.Count > 0)
            {
                txtapprove_level.Value = dt.Rows[0]["approve_level"].ToString();
            }
        }

    }
}