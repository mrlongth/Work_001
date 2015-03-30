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

namespace myWeb.App_Control.branch
{
    public partial class branch_control : PageBase
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {

            #region set Username
            //if (Session["username"] == null)
            //{
            //    string strScript = "<script language=\"javascript\">\n self.opener.document.location.href=\"../../index.aspx\";\n self.close();\n</script>\n";
            //    this.RegisterStartupScript("close", strScript);
            //    return;
            //}
            #endregion

            lblError.Text = "";
            if (!IsPostBack)
            {
                imgSaveOnly.Attributes.Add("onMouseOver", "src='../../images/controls/save2.jpg'");
                imgSaveOnly.Attributes.Add("onMouseOut", "src='../../images/controls/save.jpg'");

                imgClear.Attributes.Add("onMouseOver", "src='../../images/controls/clear2.jpg'");
                imgClear.Attributes.Add("onMouseOut", "src='../../images/controls/clear.jpg'");

                Session["menupopup_name"] = "";
                ViewState["sort"] = "branch_code";
                ViewState["direction"] = "ASC";
                txtbranch_code.ReadOnly = true;
                txtbranch_code.CssClass = "textboxdis";
                #region set QueryString
                if (Request.QueryString["branch_code"] != null)
                {
                    ViewState["branch_code"] = Request.QueryString["branch_code"].ToString();
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
                    InitcboBank();
                    Session["menupopup_name"] = "เพิ่มข้อมูลสาขา";
                    ViewState["page"] = Request.QueryString["page"];
                    chkStatus.Checked = true;
                }
                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    Session["menupopup_name"] = "แก้ไขข้อมูลสาขา";
                    setData();
                    //if (ViewState["PageStatus"] != null)
                    //{
                    //    if (ViewState["PageStatus"].ToString().ToLower().Equals("save"))
                    //    {
                    //        txtbranch_code.Text = "";
                    //        txtbranch_name.Text = "";
                    //        txtbranch_name.ReadOnly = false;
                    //        txtbranch_name.CssClass = "textbox";
                    //        chkStatus.Checked = true;
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
            }
        }

        #region private function

        private void InitcboBank()
        {
            cBank oBank = new cBank();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strbank_code = string.Empty;
            string strYear = cboBank.SelectedValue;
            strbank_code = cboBank.SelectedValue;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and c_active='Y' ";
            if (oBank.SP_SEL_BANK(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboBank.Items.Clear();
                cboBank.Items.Add(new ListItem("---- เลือกข้อมูลทั้งหมด ----", ""));
                int i;
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboBank.Items.Add(new ListItem(dt.Rows[i]["bank_name"].ToString(), dt.Rows[i]["bank_code"].ToString()));
                }
                if (cboBank.Items.FindByValue(strbank_code) != null)
                {
                    cboBank.SelectedIndex = -1;
                    cboBank.Items.FindByValue(strbank_code).Selected = true;
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
            //this.imgSave.Click += new System.Web.UI.ImageClickEventHandler(this.imgSave_Click);

        }
        #endregion

        private bool saveData()
        {
            bool blnResult = false;
            bool blnDup = false;
            string strMessage = string.Empty;
            string strbranch_code = string.Empty,
                strbranch_name = string.Empty,
                strbank_code = string.Empty,
                stractive = string.Empty,
                strcreatedby = string.Empty,
                strupdatedby = string.Empty;
            string strScript = string.Empty;
            cBranch oBranch = new cBranch();
            DataSet ds = new DataSet();
            try
            {
                #region set Data
                strbranch_code = txtbranch_code.Text;
                strbranch_name = txtbranch_name.Text;
                //Bank_code
                strbank_code = cboBank.SelectedValue;
                if (Request.Form["ctl00$ASPxRoundPanel1$ContentPlaceHolder1$cboBank"] != null)
                {
                    strbank_code = Request.Form["ctl00$ASPxRoundPanel1$ContentPlaceHolder1$cboBank"].ToString();
                }
                if (chkStatus.Checked == true)
                {
                    stractive = "Y";
                }
                else
                {
                    stractive = "N";
                }
                strcreatedby = Session["username"].ToString();
                strupdatedby = Session["username"].ToString();
                #endregion

                string strCheckAdd = " and branch.branch_code = '" + strbranch_code.Trim() + "' ";
                if (!oBranch.SP_SEL_BRANCH(strCheckAdd, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region check dup
                        string strCheckDup = string.Empty;
                        strCheckDup = " and branch.branch_name = '" + strbranch_name.Trim() + "' " +
                                                      " and branch.bank_code='" + strbank_code.Trim() + "'  and  branch.branch_code <> '" + strbranch_code.Trim() + "' " ;
                        if (!oBranch.SP_SEL_BRANCH(strCheckDup, ref ds, ref strMessage))
                        {
                            lblError.Text = strMessage;
                        }
                        else
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                strScript =
                                    "alert(\"ไม่สามารถแก้ไขข้อมูลได้ เนื่องจาก" +
                                    "\\nข้อมูลธนาคาร : " + cboBank.SelectedItem.Text +
                                    "\\nข้อมูลสาขา : " + strbranch_name.Trim() +
                                    "\\nซ้ำ\");\n";
                                blnDup = true;
                            }
                        }
                        #endregion
                        #region edit
                        if (!blnDup)
                        {
                            if (oBranch.SP_UPD_BRANCH(strbranch_code, strbranch_name, strbank_code, stractive, strupdatedby, ref strMessage))
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
                        strCheckDup = " and branch.branch_name = '" + strbranch_name + "' " +
                                                      " and branch.bank_code='" + strbank_code.Trim() + "' ";
                        if (!oBranch.SP_SEL_BRANCH(strCheckDup, ref ds, ref strMessage))
                        {
                            lblError.Text = strMessage;
                        }
                        else
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                strScript =
                                    "alert(\"ไม่สามารถเพิ่มข้อมูลได้ เนื่องจาก" +
                                    "\\nข้อมูลธนาคาร: " + cboBank.SelectedItem.Text +
                                    "\\nข้อมูลสาขา : " + strbranch_name.Trim() +
                                    "\\nซ้ำ\");\n";
                                blnDup = true;
                            }
                        }
                        #endregion
                        #region insert
                        if (!blnDup)
                        {
                            if (oBranch.SP_INS_BRANCH(strbranch_name, strbank_code, stractive, strcreatedby, ref strMessage))
                            {
                                string strGetcode = " and branch.branch_name = '" + strbranch_name.Trim() + "' " +
                                                                      " and branch.bank_code = '" + strbank_code.Trim() + "' " ;
                                if (!oBranch.SP_SEL_BRANCH(strGetcode, ref ds, ref strMessage))
                                {
                                    lblError.Text = strMessage;
                                }
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    strbranch_code = ds.Tables[0].Rows[0]["branch_code"].ToString();
                                }
                                ViewState["branch_code"] = strbranch_code;
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
                oBranch.Dispose();
            }
            return blnResult;
        }

        private void imgSaveOnly_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (saveData())
            {
                txtbranch_code.Text = "";
                txtbranch_name.Text = "";
                txtbranch_name.ReadOnly = false;
                txtbranch_name.CssClass = "textbox";
                chkStatus.Checked = true;
                txtbranch_name.Focus();
                BindGridView();
                string strScript1 = "RefreshMain('" + ViewState["page"].ToString() + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
                MsgBox("บันทึกข้อมูลสมบูรณ์");
            }
        }               
        
        //private void imgSaveOnly_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        //{
        //    if (saveData())
        //    {
        //        if (ViewState["mode"].ToString().ToLower().Equals("add"))
        //        {
        //            Response.Redirect("branch_control.aspx?mode=edit&branch_code=" + ViewState["branch_code"].ToString() + "&page=" + ViewState["page"].ToString() + "&PageStatus=save", true);
        //        }
        //        else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
        //        {
        //            txtbranch_code.Text = "";
        //            txtbranch_name.Text = "";
        //            txtbranch_name.ReadOnly = false;
        //            txtbranch_name.CssClass = "textbox";
        //            chkStatus.Checked = true;
        //            string strScript1 =
        //                "self.opener.document.forms[0].ctl00$ASPxRoundPanel1$ContentPlaceHolder2$txthpage.value=" + ViewState["page"].ToString() + ";\n" +
        //                "self.opener.document.forms[0].submit();\n" +
        //                "self.focus();\n";
        //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
        //        }
        //        BindGridView();
        //    }
        //}

        private void setData()
        {
            cBranch oBranch = new cBranch();
            DataSet ds = new DataSet();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strbranch_code = string.Empty,
                strbranch_name = string.Empty,
                strbank_code = string.Empty,
                strC_active = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty,
                strCreatedDate = string.Empty,
                strUpdatedDate = string.Empty;
            try
            {
                strCriteria = " and branch.branch_code = '" + ViewState["branch_code"].ToString() + "' ";
                if (!oBranch.SP_SEL_BRANCH(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region get Data
                        strbranch_code = ds.Tables[0].Rows[0]["branch_code"].ToString();
                        strbranch_name = ds.Tables[0].Rows[0]["branch_name"].ToString();
                        strbank_code = ds.Tables[0].Rows[0]["bank_code"].ToString();
                        strC_active = ds.Tables[0].Rows[0]["c_active"].ToString();
                        strCreatedBy = ds.Tables[0].Rows[0]["c_created_by"].ToString();
                        strUpdatedBy = ds.Tables[0].Rows[0]["c_updated_by"].ToString();
                        strCreatedDate = ds.Tables[0].Rows[0]["d_created_date"].ToString();
                        strUpdatedDate = ds.Tables[0].Rows[0]["d_updated_date"].ToString();
                        #endregion

                        #region set Control
                        txtbranch_code.Text = strbranch_code;
                        txtbranch_name.Text = strbranch_name;
                        InitcboBank();
                        if (cboBank.Items.FindByValue(strbank_code) != null)
                        {
                            cboBank.SelectedIndex = -1;
                            cboBank.Items.FindByValue(strbank_code).Selected = true;
                        }
                        if (strC_active.Equals("Y"))
                        {
                            txtbranch_name.ReadOnly = false;
                            txtbranch_name.CssClass = "textbox";
                            chkStatus.Checked = true;
                        }
                        else
                        {
                            txtbranch_name.ReadOnly = true;
                            txtbranch_name.CssClass = "textboxdis";
                            chkStatus.Checked = false;
                        }
                        cboBank.Enabled = false;
                        cboBank.CssClass = "textboxdis";
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

        //private void imgSave_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        //{
        //    bool blnResult = false;
        //    string strScript = string.Empty;
        //    blnResult = saveData();
        //    if (blnResult)
        //    {
        //        strScript = "self.opener.document.forms[0].ctl00$ASPxRoundPanel1$ContentPlaceHolder2$txthpage.value=" + ViewState["page"].ToString() + ";\n" +
        //            "self.opener.document.forms[0].submit();\n" +
        //            "self.close();\n";
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript, true);
        //    }
        //}

        private void BindGridView()
        {
            cBranch oBranch = new cBranch();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string strbank_code = string.Empty;
            strbank_code = cboBank.SelectedValue;
            strCriteria = strCriteria + "  And  (branch.bank_code = '" + strbank_code + "') ";
            try
            {
                if (!oBranch.SP_SEL_BRANCH(strCriteria, ref ds, ref strMessage))
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
                oBranch.Dispose();
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
                Label lblbranch_code = (Label)e.Row.FindControl("lblbranch_code");
                Label lblbranch_name = (Label)e.Row.FindControl("lblbranch_name");
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
                imgDelete.Attributes.Add("onclick", "return confirm(\"คุณต้องการลบสาขา   " + lblbranch_code.Text + " : " + lblbranch_name.Text + " ?\");");
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
            Label lblbranch_code = (Label)GridView1.Rows[e.RowIndex].FindControl("lblbranch_code");
            cBranch oBranch = new cBranch();
            try
            {
                if (!oBranch.SP_DEL_BRANCH(lblbranch_code.Text, "N", strUpdatedBy, ref strMessage))
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
                oBranch.Dispose();
            }
            BindGridView();
        }


        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Label lblbranch_code = (Label)GridView1.Rows[e.NewEditIndex].FindControl("lblbranch_code");
            Label lblbranch_name = (Label)GridView1.Rows[e.NewEditIndex].FindControl("lblbranch_name");
            txtbranch_code.Text = lblbranch_code.Text;
            txtbranch_name.Text = lblbranch_name.Text;
            Label lblc_active = (Label)GridView1.Rows[e.NewEditIndex].FindControl("lblc_active");
            string strC_active = lblc_active.Text;
            if (strC_active.Equals("Y"))
            {
                txtbranch_name.ReadOnly = false;
                txtbranch_name.CssClass = "textbox";
                chkStatus.Checked = true;
            }
            else
            {
                txtbranch_name.ReadOnly = true;
                txtbranch_name.CssClass = "textboxdis";
                chkStatus.Checked = false;
            }
            txtbranch_name.Focus();
        }

        protected void imgClear_Click(object sender, ImageClickEventArgs e)
        {
            txtbranch_code.Text = "";
            txtbranch_name.Text = "";
            txtbranch_name.ReadOnly = false;
            txtbranch_name.CssClass = "textbox";
            chkStatus.Checked = true;
            txtbranch_name.Focus();
        }        
    
    }
}