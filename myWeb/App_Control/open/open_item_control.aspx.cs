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

namespace myWeb.App_Control.open
{
    public partial class open_item_control : PageBase
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
        private string BudgetType
        {
            get
            {
                if (ViewState["BudgetType"] == null)
                {
                    if (Request.QueryString["budget_type"] != null)
                    {
                        ViewState["BudgetType"] = Helper.CStr(Request.QueryString["budget_type"]);
                    }
                    else
                    {
                        ViewState["BudgetType"] = "B";
                    }
                }
                return ViewState["BudgetType"].ToString();
            }
            set
            {
                ViewState["BudgetType"] = value;
            }
        }

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
                    cOpen oOpen = new cOpen();
                    DataSet ds = new DataSet();
                    _strMessage = string.Empty;
                    _strCriteria = " and open_id = " +  Helper.CInt(hddopen_id.Value);
                    if (!oOpen.SP_OPEN_ITEM_SEL(_strCriteria, ref ds, ref _strMessage))
                    {
                        lblError.Text = _strMessage;
                    }
                    ViewState["dtOpenItem"] = ds.Tables[0];
                }
                return (DataTable)ViewState["dtOpenItem"];
            }
            set
            {
                ViewState["dtOpenItem"] = value;
            }
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "RegisterScript", "RegisterScript();", true);
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            lblError.Text = "";
            if (!IsPostBack)
            {
                imgSaveOnly.Attributes.Add("onMouseOver", "src='../../images/button/save_add2.png'");
                imgSaveOnly.Attributes.Add("onMouseOut", "src='../../images/button/save_add.png'");

                ViewState["sort"] = "open_item_id";
                ViewState["direction"] = "ASC";

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

                InitcboLot();
                InitcboItem_group();
                InitcboOpenLevel();

                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    ViewState["page"] = Request.QueryString["page"];
                    TabContainer1.Tabs[1].Visible = false;
                }
                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    setData();
                }
            }
        }

        #region private function

        private void InitcboLot()
        {
            cLot oLot = new cLot();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strLot_code = string.Empty;
            string strLot = cboLot.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string strYear = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["yearnow"].ToString();

            strCriteria = " and c_active='Y' ";
            strCriteria += " and lot_year='" + strYear + "' ";
            if (oLot.SP_SEL_LOT(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboLot.Items.Clear();
                cboLot.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboLot.Items.Add(new ListItem(dt.Rows[i]["lot_name"].ToString(), dt.Rows[i]["lot_code"].ToString()));
                }
                if (cboLot.Items.FindByValue(strLot) != null)
                {
                    cboLot.SelectedIndex = -1;
                    cboLot.Items.FindByValue(strLot).Selected = true;
                }
            }
        }

        private void InitcboItem_group()
        {
            cItem_group oItem_group = new cItem_group();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strItem_group_code = string.Empty;
            string strYear = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["yearnow"].ToString();
            string strlot_code = cboLot.SelectedValue;
            strItem_group_code = cboItem_group.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = "and item_group_year='" + strYear + "' ";
            if (oItem_group.SP_SEL_item_group(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboItem_group.Items.Clear();
                cboItem_group.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboItem_group.Items.Add(new ListItem(dt.Rows[i]["Item_group_name"].ToString(), dt.Rows[i]["Item_group_code"].ToString()));
                }
                if (cboItem_group.Items.FindByValue(strItem_group_code) != null)
                {
                    cboItem_group.SelectedIndex = -1;
                    cboItem_group.Items.FindByValue(strItem_group_code).Selected = true;
                }
            }
        }

        private void InitcboOpenLevel()
        {
            cCommon oCommon = new cCommon();
            string strMessage = string.Empty,
                        strCriteria = string.Empty;
            string strOpen_level = cboOpen_level.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            strCriteria = " and g_type='open_level' ";
            if (oCommon.SP_SEL_OBJECT("sp_GENERAL_SEL", strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboOpen_level.Items.Clear();
                cboOpen_level.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboOpen_level.Items.Add(new ListItem(dt.Rows[i]["g_name"].ToString(), dt.Rows[i]["g_code"].ToString()));
                }
                if (cboOpen_level.Items.FindByValue(strOpen_level) != null)
                {
                    cboOpen_level.SelectedIndex = -1;
                    cboOpen_level.Items.FindByValue(strOpen_level).Selected = true;
                }
            }
        }


        #endregion

        private bool saveData()
        {
            bool blnResult = false;
            string strMessage = string.Empty;
            string stropen_code = string.Empty;
            string strcomments = string.Empty;
            string strActive = string.Empty;
            string strCreatedBy = string.Empty;
            string strUpdatedBy = string.Empty;
            string strUnitCode = string.Empty;
            string strDirectorCode = string.Empty;
            string strOpen_level = string.Empty;

            cOpen oOpen = new cOpen();
            try
            {
                #region set Data
                stropen_code = txtopen_code.Text;
                strCreatedBy = Session["username"].ToString();
                strUpdatedBy = Session["username"].ToString();
                #endregion
                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    #region insert
                    if (!oOpen.SP_OPEN_INS(ref stropen_code, txtopen_to.Text, txtopen_title.Text, txtopen_command_desc.Text, txtopen_desc.Text, cboOpen_level.SelectedValue,
                        txtopen_report_code.Text.Trim(), txtopen_remark.Text.Trim(), cboItem_group.SelectedValue, cboLot.SelectedValue, strUpdatedBy, ref strMessage))
                    {
                        lblError.Text = strMessage;
                    }
                    else
                    {
                        ViewState["open_code"] = stropen_code;
                        blnResult = true;
                    }
                    #endregion
                }
                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    #region update
                    if (!oOpen.SP_OPEN_UPD(hddopen_id.Value, stropen_code, txtopen_to.Text, txtopen_title.Text, txtopen_command_desc.Text, txtopen_desc.Text, cboOpen_level.SelectedValue,
                        txtopen_report_code.Text.Trim(), txtopen_remark.Text.Trim(), cboItem_group.SelectedValue, cboLot.SelectedValue, strUpdatedBy, ref strMessage))
                    {
                        lblError.Text = strMessage;
                    }
                    else
                    {
                        SaveItem();
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
                oOpen.Dispose();
            }
            return blnResult;
        }

        private void setData()
        {
            cOpen oopen = new cOpen();
            DataSet ds = new DataSet();
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
                if (!oopen.SP_OPEN_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region get Data
                        //cboYear.SelectedValue = ds.Tables[0].Rows[0]["open_year"].ToString();
                        txtopen_code.Text = ds.Tables[0].Rows[0]["open_code"].ToString();
                        hddopen_id.Value = ds.Tables[0].Rows[0]["open_id"].ToString();
                        txtopen_to.Text = ds.Tables[0].Rows[0]["open_to"].ToString();
                        txtopen_title.Text = ds.Tables[0].Rows[0]["open_title"].ToString();
                        txtopen_command_desc.Text = ds.Tables[0].Rows[0]["open_command_desc"].ToString();
                     
                        txtopen_desc.Text = ds.Tables[0].Rows[0]["open_desc"].ToString();                     
                        txtopen_remark.Text = ds.Tables[0].Rows[0]["open_remark"].ToString();

                        InitcboItem_group();
                        cboItem_group.SelectedValue = ds.Tables[0].Rows[0]["item_group_code"].ToString();

                        InitcboLot();
                        cboLot.SelectedValue = ds.Tables[0].Rows[0]["lot_code"].ToString();

                        InitcboOpenLevel();
                        cboOpen_level.SelectedValue = ds.Tables[0].Rows[0]["open_level"].ToString();

                        txtopen_report_code.Text = ds.Tables[0].Rows[0]["open_report_code"].ToString();

                        strCreatedBy = ds.Tables[0].Rows[0]["c_created_by"].ToString();
                        strUpdatedBy = ds.Tables[0].Rows[0]["c_updated_by"].ToString();
                        strCreatedDate = ds.Tables[0].Rows[0]["d_created_date"].ToString();
                        strUpdatedDate = ds.Tables[0].Rows[0]["d_updated_date"].ToString();
                        #endregion

                        #region set Control
                        txtUpdatedBy.Text = strUpdatedBy;
                        txtUpdatedDate.Text = strUpdatedDate;
                        BindGridItem();
                        #endregion
                    }
                }
                TabContainer1.Tabs[0].Visible = true;
                TabContainer1.Tabs[1].Visible = true;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }
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
            cOpen oOpen = new cOpen();
            try
            {
                StoreItem();
                if (!oOpen.SP_OPEN_ITEM_DEL(hddopen_id.Value, ref _strMessage))
                {
                    lblError.Text = _strMessage;
                }
                else
                {
                    foreach (DataRow dr in this.dtOpenItem.Rows)
                    {
                        if (Helper.CInt(dr["material_id"]) > 0)
                        {
                            if (!oOpen.SP_OPEN_ITEM_INS("0",
                                    hddopen_id.Value,
                                    Helper.CStr(dr["material_id"]),
                                    Helper.CStr(dr["open_rate"]),
                                    ref _strMessage))
                            {
                                lblError.Text = _strMessage;
                            }
                            else
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
                oOpen.Dispose();
            }
            return blnResult;
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

    }
}