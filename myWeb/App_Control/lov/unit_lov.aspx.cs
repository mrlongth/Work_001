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
    public partial class unit_lov : PageBase
    {

        #region private data
        private string strConn = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"];
        private bool[] blnAccessRight = new bool[5] { false, false, false, false, false };
        private string strPrefixCtr = "ctl00$ASPxRoundPanel1$ContentPlaceHolder1$";
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            // Thread.Sleep(2000);
            if (!IsPostBack)
            {
                imgFind.Attributes.Add("onMouseOver", "src='../../images/button/Search2.png'");
                imgFind.Attributes.Add("onMouseOut", "src='../../images/button/Search.png'");

                Session["menulov_name"] = "ค้นหาข้อมูลหน่วยงาน";
               
                if (Request.QueryString["unit_year"] != null)
                {
                    ViewState["unit_year"] = Request.QueryString["unit_year"].ToString();
                    txtunit_year.Text = ViewState["unit_year"].ToString();
                    txtunit_year.CssClass = "textboxdis";
                    txtunit_year.ReadOnly = true;
                }

                if (Request.QueryString["unit_code"] != null)
                {
                    ViewState["unit_code"] = Request.QueryString["unit_code"].ToString();
                    txtunit_code.Text = ViewState["unit_code"].ToString();
                }
                else
                {
                    ViewState["unit_code"] = string.Empty;
                    txtunit_code.Text = string.Empty;
                }
                if (Request.QueryString["unit_name"] != null)
                {
                    ViewState["unit_name"] = Request.QueryString["unit_name"].ToString();
                    txtunit_name.Text = ViewState["unit_name"].ToString();
                }
                else
                {
                    ViewState["unit_name"] = string.Empty;
                    txtunit_name.Text = string.Empty;
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

                if (Request.QueryString["show"] != null)
                {
                    ViewState["show"] = Request.QueryString["show"].ToString();
                }
                else
                {
                    ViewState["show"] = "1";
                }

                ViewState["sort"] = "unit_code";
                ViewState["direction"] = "ASC";
                BindGridView();
            }
            else
            {
                BindGridView();
            }
        }

        #region private function
      
        private void InitcboDirector()
        {
            cDirector oDirector = new cDirector();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strDirector_code = string.Empty;
            string strYear = txtunit_year.Text.Trim(); 
             strDirector_code = cboDirector.SelectedValue;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and director_year = '" + strYear + "'  and  c_active='Y' ";
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
            }
        }


        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public DataTable GetDataDirector(string strYear)
        {
            cDirector oDirector = new cDirector();
            string strMessage = string.Empty,
                        strCriteria = string.Empty;
            string strDirector_code = string.Empty,
                        strDirector_name = string.Empty;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and director_year = '" + strYear + "'  and  c_active='Y' ";
            if (oDirector.SP_SEL_DIRECTOR(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        private void BindGridView()
        {
            InitcboDirector();
            cUnit oUnit = new cUnit();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string strunit_code = string.Empty;
            string strunit_name = string.Empty;
            string strdirector_code = string.Empty;
            string strScript = string.Empty;
            string strunit_year = string.Empty;
            strunit_code = txtunit_code.Text.Replace("'", "''").Trim();
            strunit_name = txtunit_name.Text.Replace("'", "''").Trim();
            strunit_year = txtunit_year.Text.Replace("'", "''").Trim(); ;
            strdirector_code = cboDirector.SelectedValue;
            if (!strunit_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (unit.unit_code like '%" + strunit_code + "%') ";
            }
            if (!strunit_name.Equals(""))
            {
                strCriteria = strCriteria + "  And  (unit.unit_name like '%" + strunit_name + "%')";
            }
            if (!strunit_year.Equals(""))
            {
                strCriteria = strCriteria + "  And  (unit.unit_year = '" + strunit_year + "') ";
            }
            if (!strdirector_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (unit.director_code = '" + strdirector_code + "') ";
            }
            strCriteria = strCriteria + "  And  (unit.c_active ='Y') ";
            try
            {
                if (oUnit.SP_SEL_UNIT(strCriteria, ref ds, ref strMessage))
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        strunit_code = ds.Tables[0].Rows[0]["unit_code"].ToString();
                        strunit_name = ds.Tables[0].Rows[0]["unit_name"].ToString();
                        if (!ViewState["show"].ToString().Equals("1"))
                        {
                            strScript = "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) -1) + "'].document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + strunit_code + "';\n " +
                                                "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) -1) + "'].document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + strunit_name + "';\n" +
                                                "ClosePopUp('" + ViewState["show"].ToString() + "');";
                        }
                        else
                        {
                            strScript = "window.parent.document.forms[0]." + ViewState["ctrl1"].ToString() + ".value='" + strunit_code + "';\n " +
                                                "window.parent.document.forms[0]." + ViewState["ctrl2"].ToString() + ".value='" + strunit_name + "';\n" +
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
                else
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
                oUnit.Dispose();
                ds.Dispose();
            }
        }
        #endregion

        protected void imgFind_Click(object sender, ImageClickEventArgs e)
        {
            BindGridView();
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

                if (!ViewState["show"].ToString().Equals("1"))
                {
                    lblunit_code.Text = "<a href=\"\" onclick=\"" +
                                        "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + lblunit_code.Text + "';\n " +
                                        "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + lblunit_name.Text + "';\n" +
                                        "ClosePopUp('" + ViewState["show"].ToString() + "');" +
                                        "return false;\" >" + lblunit_code.Text + "</a>";
                }
                else
                {
                    lblunit_code.Text = "<a href=\"\" onclick=\"" +
                                    "window.parent.document.forms[0]." + ViewState["ctrl1"].ToString() + ".value='" + lblunit_code.Text + "';\n " +
                                    "window.parent.document.forms[0]." + ViewState["ctrl2"].ToString() + ".value='" + lblunit_name.Text + "';\n" +
                                    "ClosePopUp('" + ViewState["show"].ToString() + "');" +
                                    "return false;\" >" + lblunit_code.Text + "</a>";
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

        protected void cboDirector_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridView();
        }

    }
}
