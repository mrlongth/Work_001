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

namespace myWeb.App_Control.activity
{
    public partial class activity_control : PageBase
    {
        #region private data
        private string strConn = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"];
        private bool[] blnAccessRight = new bool[5] { false, false, false, false, false };
        #endregion
        protected void Page_Load(object sender, System.EventArgs e)
        {
            //AjaxPro.Utility.RegisterTypeForAjax(typeof(activity_control));

            //if (Session["username"] == null)
            //{
            //    string strScript = "<script language=\"javascript\">\n self.opener.document.location.href=\"../../index.aspx\";\n self.close();\n</script>\n";
            //    this.RegisterStartupScript("close", strScript);
            //    return;
            //}
            lblError.Text = "";
            if (!IsPostBack)
            {
                imgSaveOnly.Attributes.Add("onMouseOver", "src='../../images/controls/save2.jpg'");
                imgSaveOnly.Attributes.Add("onMouseOut", "src='../../images/controls/save.jpg'");

                imgClear.Attributes.Add("onMouseOver", "src='../../images/controls/clear2.jpg'");
                imgClear.Attributes.Add("onMouseOut", "src='../../images/controls/clear.jpg'");


                Session["menupopup_name"] = "";
                ViewState["sort"] = "activity_code";
                ViewState["direction"] = "ASC";
                #region set QueryString
                if (Request.QueryString["activity_code"] != null)
                {
                    ViewState["activity_code"] = Request.QueryString["activity_code"].ToString();
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
                    InitcboBudget();
                    InitcboProduce();
                    ViewState["page"] = Request.QueryString["page"];
                    txtactivity_code.ReadOnly = true;
                    txtactivity_code.CssClass = "textboxdis";
                    chkStatus.Checked = true;
                    txtactivity_code.CssClass = "textboxdis";
                }
                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    setData();
                    txtactivity_code.ReadOnly = true;
                    txtactivity_code.CssClass = "textboxdis";
                    //if (ViewState["PageStatus"] != null)
                    //{
                    //    if (ViewState["PageStatus"].ToString().ToLower().Equals("save"))
                    //    {
                    //        txtactivity_code.Text = "";
                    //        txtactivity_name.Text = "";
                    //        txtactivity_name.ReadOnly = false;
                    //        txtactivity_name.CssClass = "textbox";
                    //        chkStatus.Checked = true; 
                    //        txtactivity_code.Focus();
                    //        string strScript1 =
                    //            "self.opener.document.forms[0].ctl00$ASPxRoundPanel1$ContentPlaceHolder2$txthpage.value=" + ViewState["page"].ToString() + ";\n" +
                    //            "self.opener.document.forms[0].submit();\n" +
                    //            "self.focus();\n";
                    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
                    //    }
                    //}
                }

                #endregion
                //imgClose.Attributes.Add("onclick", "ClosePopUp('" + ViewState["page"].ToString() + "','1');return false;");
                // #region add ajax method to control
                //cboYear.Attributes.Add("onchange", "changeBudget(this);");
                //cboYear.AutoPostBack = false;
                //cboBudget.Attributes.Add("onchange", "changeProduce(this);");
                //cboBudget.AutoPostBack = false;
                //#endregion


                //if (this.BudgetType == "R")
                //{
                //    foreach (Control c in Page.Controls)
                //    {
                //        base.SetLabel(c, "กิจกรรม", "งานรอง");
                //        base.SetLabel(c, "แผนงบประมาณ", "แผนงาน");
                //        base.SetLabel(c, "ผลผลิต", "งานหลัก");
                //    }
                //}


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
            InitcboBudget();
        }


        private void InitcboBudget()
        {
            cBudget oBudget = new cBudget();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strbudget_code = string.Empty;
            string strYear = cboYear.SelectedValue;
            strbudget_code = cboBudget.SelectedValue; ;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and budget_year = '" + strYear + "'  and  c_active='Y' ";
            strCriteria = strCriteria + "  And budget_type ='" + this.BudgetType + "' ";
            if (oBudget.SP_SEL_BUDGET(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboBudget.Items.Clear();
                cboBudget.Items.Add(new ListItem("---- เลือกข้อมูลทั้งหมด ----", ""));
                int i;
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboBudget.Items.Add(new ListItem(dt.Rows[i]["budget_name"].ToString(), dt.Rows[i]["budget_code"].ToString()));
                }
                if (cboBudget.Items.FindByValue(strbudget_code) != null)
                {
                    cboBudget.SelectedIndex = -1;
                    cboBudget.Items.FindByValue(strbudget_code).Selected = true;
                }
            }
            InitcboProduce();
        }

        private void InitcboProduce()
        {
            cProduce oProduce = new cProduce();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strbudget_code = string.Empty,
                        strproduce_code = string.Empty,
                        strproduce_name = string.Empty;
            strbudget_code = cboBudget.SelectedValue;
            strproduce_code = cboProduce.SelectedValue; ;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and produce.budget_code= '" + strbudget_code + "'  and  produce.c_active='Y' ";
            strCriteria = strCriteria + "  And produce.budget_type ='" + this.BudgetType + "' ";
            if (oProduce.SP_SEL_PRODUCE(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboProduce.Items.Clear();
                cboProduce.Items.Add(new ListItem("---- เลือกข้อมูลทั้งหมด ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboProduce.Items.Add(new ListItem(dt.Rows[i]["produce_name"].ToString(), dt.Rows[i]["produce_code"].ToString()));
                }
                if (cboProduce.Items.FindByValue(strproduce_code) != null)
                {
                    cboProduce.SelectedIndex = -1;
                    cboProduce.Items.FindByValue(strproduce_code).Selected = true;
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
          //  InitcboYear();
            InitcboBudget();
            InitcboProduce();
            bool blnResult = false;
            bool blnDup = false;
            string strMessage = string.Empty;
            string stractivity_code = string.Empty,
                stractivity_year = string.Empty,
                stractivity_name = string.Empty,
                strproduce_code = string.Empty,
                strActive = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty;
            string strScript = string.Empty;
            cActivity oactivity = new cActivity();
            DataSet ds = new DataSet();
            try
            {
                #region set Data
                //activity_code
                stractivity_code = txtactivity_code.Text ;
                if (Request.Form["ctl00$ASPxRoundPanel1$ContentPlaceHolder1$ASPxRoundPanel2$txtactivity_code"] != null)
                {
                    stractivity_code = Request.Form["ctl00$ASPxRoundPanel1$ContentPlaceHolder1$ASPxRoundPanel2$txtactivity_code"].ToString();
                }
                //activity_name
                stractivity_name = txtactivity_name.Text;
                if (Request.Form["ctl00$ASPxRoundPanel1$ContentPlaceHolder1$ASPxRoundPanel2$txtactivity_name"] != null)
                {
                    stractivity_name = Request.Form["ctl00$ASPxRoundPanel1$ContentPlaceHolder1$ASPxRoundPanel2$txtactivity_name"].ToString();
                }
                //activity_year
                stractivity_year = cboYear.SelectedValue;
                //budget_code
                strproduce_code = cboProduce.SelectedValue;
                if (Request.Form["ctl00$ASPxRoundPanel1$ContentPlaceHolder1$cboProduce"] != null)
                {
                    strproduce_code = Request.Form["ctl00$ASPxRoundPanel1$ContentPlaceHolder1$cboProduce"].ToString();
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

                string strCheckAdd = " and activity.activity_code = '" + stractivity_code.Trim() + "' ";
                if (!oactivity.SP_SEL_ACTIVITY(strCheckAdd, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region check dup
                        string strCheckDup = string.Empty;
                        strCheckDup = " and activity.activity_name = '" + stractivity_name.Trim() + "' and activity.activity_year = '" + stractivity_year.Trim() + "' " +
                                                      " and activity.produce_code='" + strproduce_code.Trim() + "'  and  activity.activity_code <> '" + stractivity_code.Trim() + "' ";
                        if (!oactivity.SP_SEL_ACTIVITY(strCheckDup, ref ds, ref strMessage))
                        {
                            lblError.Text = strMessage;
                        }
                        else
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                strScript =
                                    "alert(\"ไม่สามารถแก้ไขข้อมูลได้ เนื่องจาก" +
                                    "\\nข้อมูล : " + stractivity_name.Trim() +
                                    "\\nข้อมูล : " + cboProduce.SelectedItem.Text +
                                    "\\nข้อมูล : " + cboBudget.SelectedItem.Text +
                                    "\\n ปี : " + stractivity_year.Trim() +
                                    "\\nซ้ำ\");\n";
                                blnDup = true;
                            }
                        }
                        #endregion
                        #region edit
                        if (!blnDup)
                        {
                            if (oactivity.SP_UPD_ACTIVITY(stractivity_code, stractivity_year, stractivity_name, strproduce_code, strActive, strUpdatedBy, this.BudgetType, ref strMessage))
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
                        strCheckDup = " and activity.activity_name = '" + stractivity_name.Trim() + "' and activity.activity_year = '" + stractivity_year + "' " +
                                                      " and activity.produce_code='" + strproduce_code.Trim() + "' ";
                        if (!oactivity.SP_SEL_ACTIVITY(strCheckDup, ref ds, ref strMessage))
                        {
                            lblError.Text = strMessage;
                        }
                        else
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                strScript =
                                    "alert(\"ไม่สามารถเพิ่มข้อมูลได้ เนื่องจาก" +
                                    "\\nข้อมูล : " + stractivity_name .Trim() +
                                    "\\nข้อมูล : " + cboProduce.SelectedItem.Text  +
                                    "\\nข้อมูล : " + cboBudget.SelectedItem.Text +
                                    "\\n ปี : " + stractivity_year.Trim() +
                                    "\\nซ้ำ\");\n";
                                blnDup = true;
                            }
                        }
                        #endregion
                        #region insert
                        if (!blnDup)
                        {
                            if (oactivity.SP_INS_ACTIVITY(stractivity_year, stractivity_name, strproduce_code, strActive, strCreatedBy, this.BudgetType, ref strMessage))
                            {
                                string strGetcode = " and activity_name = '" + stractivity_name.Trim() + "' and activity_year = '" + stractivity_year + "' " +
                                                                      " and activity.produce_code='" + strproduce_code.Trim() + " '";
                                if (!oactivity.SP_SEL_ACTIVITY(strGetcode, ref ds, ref strMessage))
                                {
                                    lblError.Text = strMessage;
                                }
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    stractivity_code = ds.Tables[0].Rows[0]["activity_code"].ToString();
                                }
                                ViewState["activity_code"] = stractivity_code;
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
                oactivity.Dispose();
            }
            return blnResult;
        }

        private void imgSaveOnly_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (saveData())
            {
                txtactivity_code.Text = "";
                txtactivity_name.Text = "";
                txtactivity_name.ReadOnly = false;
                txtactivity_name.CssClass = "textbox";
                chkStatus.Checked = true;
                txtactivity_name.Focus();
                BindGridView();
                string strScript1 = "RefreshMain('" + ViewState["page"].ToString() + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
                MsgBox("บันทึกข้อมูลสมบูรณ์");
            }
        }

        private void setData()
        {
            cActivity oactivity = new cActivity();
            DataSet ds = new DataSet();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string stractivity_code = string.Empty,
                stractivity_name = string.Empty,
                strbudget_code = string.Empty,
                strproduce_code = string.Empty,
                strYear = string.Empty,
                strC_active = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty,
                strCreatedDate = string.Empty,
                strUpdatedDate = string.Empty;
            try
            {
                strCriteria = " and activity_code = '" + ViewState["activity_code"].ToString() + "' ";
                if (!oactivity.SP_SEL_ACTIVITY(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region get Data
                        stractivity_code = ds.Tables[0].Rows[0]["activity_code"].ToString();
                        stractivity_name = ds.Tables[0].Rows[0]["activity_name"].ToString();
                        strbudget_code = ds.Tables[0].Rows[0]["budget_code"].ToString();
                        strproduce_code = ds.Tables[0].Rows[0]["produce_code"].ToString();
                        strYear = ds.Tables[0].Rows[0]["activity_year"].ToString();
                        strC_active = ds.Tables[0].Rows[0]["c_active"].ToString();
                        strCreatedBy = ds.Tables[0].Rows[0]["c_created_by"].ToString();
                        strUpdatedBy = ds.Tables[0].Rows[0]["c_updated_by"].ToString();
                        strCreatedDate = ds.Tables[0].Rows[0]["d_created_date"].ToString();
                        strUpdatedDate = ds.Tables[0].Rows[0]["d_updated_date"].ToString();
                        #endregion

                        #region set Control
                        txtactivity_code.Text = stractivity_code;
                        txtactivity_name.Text = stractivity_name;

                        InitcboYear();
                        if (cboYear.Items.FindByValue(strYear) != null)
                        {
                            cboYear.SelectedIndex = -1;
                            cboYear.Items.FindByValue(strYear).Selected = true;
                        }

                        InitcboBudget();
                        if (cboBudget.Items.FindByValue(strbudget_code) != null)
                        {
                            cboBudget.SelectedIndex = -1;
                            cboBudget.Items.FindByValue(strbudget_code).Selected = true;
                        }

                        InitcboProduce();
                        if (cboProduce.Items.FindByValue(strproduce_code) != null)
                        {
                            cboProduce.SelectedIndex = -1;
                            cboProduce.Items.FindByValue(strproduce_code).Selected = true;
                        }

                        if (strC_active.Equals("Y"))
                        {
                            txtactivity_name.ReadOnly = false;
                            txtactivity_name.CssClass = "textbox";
                            chkStatus.Checked = true;
                        }
                        else
                        {
                            txtactivity_name.ReadOnly = true;
                            txtactivity_name.CssClass = "textboxdis";
                            chkStatus.Checked = false;
                        }

                        cboYear.Enabled = false;
                        cboYear.CssClass = "textboxdis";
                        cboBudget.Enabled = false;
                        cboBudget.CssClass = "textboxdis";
                        cboProduce.Enabled = false;
                        cboProduce.CssClass = "textboxdis";
                        
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
            cActivity oactivity = new cActivity();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string strproduce_code = string.Empty;
            strproduce_code = cboProduce.SelectedValue;
            strCriteria = strCriteria + "  And  (activity.produce_code = '" + strproduce_code + "')  ";
            strCriteria = strCriteria + "  And activity.budget_type ='" + this.BudgetType + "' ";
            try
            {
                if (!oactivity.SP_SEL_ACTIVITY(strCriteria, ref ds, ref strMessage))
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
                oactivity.Dispose();
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
                Label lblactivity_code = (Label)e.Row.FindControl("lblactivity_code");
                Label lblactivity_name = (Label)e.Row.FindControl("lblactivity_name");
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
                imgDelete.Attributes.Add("onclick", "return confirm(\"คุณต้องการลบผลผลิตประจำปี   " + lblactivity_code.Text + " : " + lblactivity_name.Text + " ?\");");
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
            Label lblactivity_code = (Label)GridView1.Rows[e.RowIndex].FindControl("lblactivity_code");
            cActivity oactivity = new cActivity();
            try
            {
                if (!oactivity.SP_DEL_ACTIVITY(lblactivity_code.Text, "N", strUpdatedBy, ref strMessage))
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
                oactivity.Dispose();
            }
            BindGridView();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Label lblactivity_code = (Label)GridView1.Rows[e.NewEditIndex].FindControl("lblactivity_code");
            Label lblactivity_name = (Label)GridView1.Rows[e.NewEditIndex].FindControl("lblactivity_name");
            Label lblc_active = (Label)GridView1.Rows[e.NewEditIndex].FindControl("lblc_active");
            txtactivity_code.Text = lblactivity_code.Text;
            txtactivity_name.Text = lblactivity_name.Text;
            string strC_active = lblc_active.Text;
            if (strC_active.Equals("Y"))
            {
                txtactivity_name.ReadOnly = false;
                txtactivity_name.CssClass = "textbox";
                chkStatus.Checked = true;
            }
            else
            {
                txtactivity_name.ReadOnly = true;
                txtactivity_name.CssClass = "textboxdis";
                chkStatus.Checked = false;
            }
            txtactivity_name.Focus();
        }

        protected void imgClear_Click(object sender, ImageClickEventArgs e)
        {
            txtactivity_code.Text = "";
            txtactivity_name.Text = "";
            txtactivity_name.ReadOnly = false;
            txtactivity_name.CssClass = "textbox";
            chkStatus.Checked = true;
            txtactivity_name.Focus();
        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboYear();
        }

        protected void cboBudget_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboBudget(); 
        }
    }
}