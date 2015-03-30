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

namespace myWeb.App_Control.lov
{
    public partial class person_lov : PageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                imgFind.Attributes.Add("onMouseOver", "src='../../images/button/Search2.png'");
                imgFind.Attributes.Add("onMouseOut", "src='../../images/button/Search.png'");

                //imgList_unit.Attributes.Add("onclick", "OpenPopUp('800px','400px','93%','ค้นหาข้อมูลหน่วยงาน' ,'../lov/unit_lov.aspx?" +
                // "unit_year='+document.forms[0]." + strPrefixCtr + "txtyear.value+" +
                // "'&unit_code='+document.forms[0]." + strPrefixCtr + "txtunit_code.value+" +
                // "'&unit_name='+document.forms[0]." + strPrefixCtr + "txtunit_name.value+" +
                // "'&ctrl1=" + strPrefixCtr + "txtunit_code&ctrl2=" + strPrefixCtr + "txtunit_name&show=2', '2');return false;");

                //imgClear_unit.Attributes.Add("onclick", "document.getElementById('" + txtunit_code.ClientID + "').value='';document.getElementById('" + txtunit_name.ClientID + "').value=''; return false;");

                InitcboWork_status();
                InitcboPerson_group();
                InitcboDirector();

                if (Request.QueryString["year"] != null)
                {
                    ViewState["year"] = Request.QueryString["year"].ToString();
                    txtyear.Text = ViewState["year"].ToString();
                    txtyear.CssClass = "textboxdis";
                    txtyear.ReadOnly = true;
                }

                if (Request.QueryString["person_code"] != null)
                {
                    ViewState["person_code"] = Request.QueryString["person_code"].ToString();
                    txtperson_code.Text = ViewState["person_code"].ToString();
                }
                else
                {
                    ViewState["person_code"] = string.Empty;
                    txtperson_code.Text = string.Empty;
                }

                if (Request.QueryString["person_name"] != null)
                {
                    ViewState["person_name"] = Request.QueryString["person_name"].ToString();
                    txtperson_name.Text = ViewState["person_name"].ToString();
                }
                else
                {
                    ViewState["person_name"] = string.Empty;
                    txtperson_name.Text = string.Empty;
                }

                if (Request.QueryString["ctrl1"] != null)
                {
                    ViewState["ctrl1"] = Request.QueryString["ctrl1"].ToString();
                }
                else
                {
                    ViewState["ctrl1"] = string.Empty;
                }

                if (Request.QueryString["ctrl2"] != null)
                {
                    ViewState["ctrl2"] = Request.QueryString["ctrl2"].ToString();
                }
                else
                {
                    ViewState["ctrl2"] = string.Empty;
                }

                if (Request.QueryString["ctrl3"] != null)
                {
                    ViewState["ctrl3"] = Request.QueryString["ctrl3"].ToString();
                }
                else
                {
                    ViewState["ctrl3"] = string.Empty;
                }

                if (Request.QueryString["ctrl4"] != null)
                {
                    ViewState["ctrl4"] = Request.QueryString["ctrl4"].ToString();
                }
                else
                {
                    ViewState["ctrl4"] = string.Empty;
                }

                if (Request.QueryString["ctrl5"] != null)
                {
                    ViewState["ctrl5"] = Request.QueryString["ctrl5"].ToString();
                }
                else
                {
                    ViewState["ctrl5"] = string.Empty;
                }

                if (Request.QueryString["show"] != null)
                {
                    ViewState["show"] = Request.QueryString["show"].ToString();
                }
                else
                {
                    ViewState["show"] = "1";
                }

                if (Request.QueryString["from"] != null)
                {
                    ViewState["from"] = Request.QueryString["from"].ToString();
                }
                else
                {
                    ViewState["from"] = string.Empty;
                }



                ViewState["sort"] = "person_code";
                ViewState["direction"] = "ASC";
            }
            else
            {
                BindGridView();
            }
        }

        #region private function

        private void InitcboWork_status()
        {
            cPerson_work_status oPerson_work_status = new cPerson_work_status();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strperson_work_status_code = cboPerson_work_status.SelectedValue;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = "  and  c_active='Y' ";
            if (oPerson_work_status.SP_PERSON_WORK_STATUS_SEL(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboPerson_work_status.Items.Clear();
                cboPerson_work_status.Items.Add(new ListItem("---- เลือกข้อมูลทั้งหมด ----", ""));
                int i;
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboPerson_work_status.Items.Add(new ListItem(dt.Rows[i]["person_work_status_name"].ToString(), dt.Rows[i]["person_work_status_code"].ToString()));
                }
                if (cboPerson_work_status.Items.FindByValue(strperson_work_status_code) != null)
                {
                    cboPerson_work_status.SelectedIndex = -1;
                    cboPerson_work_status.Items.FindByValue(strperson_work_status_code).Selected = true;
                }
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
            strCriteria += " and person_group_code IN (" + PersonGroupList + ") ";
            if (oPerson_group.SP_PERSON_GROUP_SEL(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboPerson_group.Items.Clear();
                cboPerson_group.Items.Add(new ListItem("---- เลือกข้อมูลทั้งหมด ----", ""));
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

        private void InitcboDirector()
        {
            cDirector oDirector = new cDirector();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strDirector_code = string.Empty;
            string strYear = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["yearnow"].ToString();
            strDirector_code = cboDirector.SelectedValue;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and director_year = '" + strYear + "'  and  c_active='Y' ";
            if (DirectorLock == "Y")
            {
                strCriteria += " and substring(director_code,4,2) = substring('" + DirectorCode + "',4,2) ";
            }

            if (oDirector.SP_SEL_DIRECTOR(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboDirector.Items.Clear();
                cboDirector.Items.Add(new ListItem("---- เลือกข้อมูลทั้งหมด ----", ""));
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
            string strYear = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["yearnow"].ToString();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and unit.unit_year = '" + strYear + "'  and  unit.c_active='Y' " +
                                   " and unit.director_code = '" + strDirector_code + "' ";
            if (oUnit.SP_SEL_UNIT(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboUnit.Items.Clear();
                cboUnit.Items.Add(new ListItem("---- เลือกข้อมูลทั้งหมด ----", ""));
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

        #endregion

        protected void imgFind_Click(object sender, ImageClickEventArgs e)
        {
            BindGridView();
        }

        private void BindGridView()
        {

            InitcboWork_status();
            InitcboPerson_group();

            cPerson oPerson = new cPerson();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string strYear = string.Empty;
            string strActive = string.Empty;
            string strperson_group_code = string.Empty;
            string strperson_group_name = string.Empty;
            string strunit_code = string.Empty;
            string strdirector_code = string.Empty;
            string strperson_code = string.Empty;
            string strperson_name = string.Empty;
            string strperson_work_status_code = string.Empty;
            strperson_group_code = cboPerson_group.Text;
            strdirector_code = cboDirector.SelectedValue;
            strunit_code = cboUnit.SelectedValue;
            strperson_code = txtperson_code.Text.Replace("'", "''").Trim();
            strperson_name = txtperson_name.Text.Replace("'", "''").Trim();
            strperson_work_status_code = cboPerson_work_status.SelectedValue;

            if (!strYear.Equals(""))
            {
                strCriteria = strCriteria + "  And  (budget_plan_year = '" + strYear + "') ";
            }

            if (!strperson_group_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (person_group_code like '%" + strperson_group_code + "%') ";
            }

            if (!strdirector_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (director_code = '" + strdirector_code + "') ";
            }

            if (!strunit_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (unit_code= '" + strunit_code + "') ";
            }

            if (!strperson_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (person_code= '" + strperson_code + "') ";
            }

            if (!strperson_name.Equals(""))
            {
                strCriteria = strCriteria + "  And  (person_thai_name like '%" + strperson_name + "%'  " +
                                                              "  OR person_thai_surname like '%" + strperson_name + "%'  " +
                                                              "  OR person_eng_name like '%" + strperson_name + "%'  " +
                                                              "  OR person_eng_surname like '%" + strperson_name + "%')";
            }

            if (!strperson_work_status_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (person_work_status_code= '" + strperson_work_status_code + "') ";
            }

            if (RadioActive.Checked)
            {
                strCriteria = strCriteria + "  And  (c_active ='Y') ";
            }
            else if (RadioCancel.Checked)
            {
                strCriteria = strCriteria + "  And  (c_active ='N') ";
            }

            strCriteria += " and person_group_code IN (" + PersonGroupList + ") ";
            if (DirectorLock == "Y")
            {
                strCriteria += " and substring(director_code,4,2) = substring('" + DirectorCode + "',4,2) ";
            }


            try
            {
                if (!oPerson.SP_PERSON_LIST_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        string strScript = string.Empty;
                        strperson_code = ds.Tables[0].Rows[0]["person_code"].ToString();
                        strperson_name = ds.Tables[0].Rows[0]["person_thai_name"].ToString() + " " + ds.Tables[0].Rows[0]["person_thai_surname"].ToString();

                        if (!ViewState["show"].ToString().Equals("1"))
                        {
                            strScript = "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + strperson_code + "';\n " +
                                                 "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + strperson_name + "';\n" +
                                                 "ClosePopUp('" + ViewState["show"].ToString() + "');";
                        }
                        else
                        {
                            strScript = "window.parent.document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + strperson_code + "';\n " +
                                                "window.parent.document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + strperson_name + "';\n" +
                                                 "ClosePopUp('" + ViewState["show"].ToString() + "');";
                        }
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "close", strScript, true);
                    }
                    else
                    {
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
                oPerson.Dispose();
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
                Label lblperson_code = (Label)e.Row.FindControl("lblperson_code");
                Label lblperson_name = (Label)e.Row.FindControl("lblperson_name");
                DataRowView dv = (DataRowView)e.Row.DataItem;
                if (ViewState["from"].ToString().Equals("user"))
                {
                    lblperson_code.Text = "<a href=\"\" onclick=\"" +
                                                             "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + lblperson_code.Text + "';\n " +
                                                             "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + lblperson_name.Text + " " + dv["person_thai_surname"].ToString() + "';\n" +
                                                             "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl3"].ToString() + "').value='" + dv["person_group_name"].ToString() + "';\n" +
                                                             "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl4"].ToString() + "').value='" + dv["director_name"].ToString() + "';\n" +
                                                             "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl5"].ToString() + "').value='" + dv["unit_name"].ToString() + "';\n" +
                                                             "ClosePopUp('" + ViewState["show"].ToString() + "');" +
                                                             "return false;\" >" + lblperson_code.Text + "</a>";
                }
                else if (!ViewState["show"].ToString().Equals("1"))
                {
                    lblperson_code.Text = "<a href=\"\" onclick=\"" +
                                                             "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + lblperson_code.Text + "';\n " +
                                                             "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + lblperson_name.Text + " " + dv["person_thai_surname"].ToString() + "';\n" +
                                                             "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].__doPostBack('ctl00$ContentPlaceHolder1$LinkButton1','');"+
                                                             "ClosePopUp('" + ViewState["show"].ToString() + "');" +
                                                             "return false;\" >" + lblperson_code.Text + "</a>";
               
                
                }
                else
                {
                    string strPersonCode = lblperson_code.Text;
                    string strPersonName = lblperson_name.Text + " " + dv["person_thai_surname"].ToString();
                   
                    lblperson_code.Text = "<a href=\"\" onclick=\"";
                    lblperson_code.Text += "window.parent.document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + strPersonCode + "';\n ";
                    lblperson_code.Text += "window.parent.document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + strPersonName + "';\n";
                    if (ViewState["from"].ToString() == "payment_control")
                        lblperson_code.Text += "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].__doPostBack('ctl00$ContentPlaceHolder1$LinkButton1','');";
                    lblperson_code.Text += "ClosePopUp('" + ViewState["show"].ToString() + "');";
                    lblperson_code.Text += "return false;\" >" + strPersonCode + "</a>";
                }
                //if (ViewState["from"].ToString().Equals("payment_control"))
                //{
                //    lblperson_code.Text = "<a href=\"\" onclick=\"" +
                //                                             "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + lblperson_code.Text + "';\n " +
                //                                             "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + lblperson_name.Text + "';\n" +
                //                                             "ClosePopUp('" + ViewState["show"].ToString() + "');" +
                //                                             "return false;\" >" + lblperson_code.Text + "</a>";
                //}

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

        protected void cboDirector_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboUnit();
            BindGridView();
        }

        protected void cboUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridView();
        }

    }
}
