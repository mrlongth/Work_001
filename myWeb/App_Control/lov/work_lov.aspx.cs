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
    public partial class work_lov : PageBase
    {

        #region private data
        private string strConn = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"];
        private bool[] blnAccessRight = new bool[5] { false, false, false, false, false };
        private string strPrefixCtr = "ctl00$ASPxRoundPanel1$ASPxRoundPanel2$ContentPlaceHolder1$";
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            // Thread.Sleep(2000);
            if (!IsPostBack)
            {
                imgFind.Attributes.Add("onMouseOver", "src='../../images/button/Search2.png'");
                imgFind.Attributes.Add("onMouseOut", "src='../../images/button/Search.png'");

                Session["menulov_name"] = "ค้นหาข้อมูลงาน";
                if (Request.QueryString["year"] != null)
                {
                    ViewState["work_year"] = Request.QueryString["year"].ToString();
                    txtwork_year.Text = ViewState["work_year"].ToString();
                    txtwork_year.CssClass = "textboxdis";
                    txtwork_year.ReadOnly = true;
                }

                if (Request.QueryString["work_code"] != null)
                {
                    ViewState["work_code"] = Request.QueryString["work_code"].ToString();
                    txtwork_code.Text = ViewState["work_code"].ToString();
                }
                else
                {
                    ViewState["work_code"] = string.Empty;
                    txtwork_code.Text = string.Empty;
                }
                if (Request.QueryString["work_name"] != null)
                {
                    ViewState["work_name"] = Request.QueryString["work_name"].ToString();
                    txtwork_name.Text = ViewState["work_name"].ToString();
                }
                else
                {
                    ViewState["work_name"] = string.Empty;
                    txtwork_name.Text = string.Empty;
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
                ViewState["sort"] = "work_code";
                ViewState["direction"] = "ASC";
                BindGridView();
            }
            else
            {
                BindGridView();
            }
        }

        #region private function
        
        private void BindGridView()
        {
            cWork owork = new cWork();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string strwork_code = string.Empty;
            string strwork_name = string.Empty;
            string strScript = string.Empty;
            string strwork_year = string.Empty;
            strwork_code = txtwork_code.Text.Replace("'", "''").Trim();
            strwork_name = txtwork_name.Text.Replace("'", "''").Trim();
            strwork_year = txtwork_year.Text.Replace("'", "''").Trim(); ;
            if (!strwork_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (work_code like '%" + strwork_code + "%') ";
            }
            if (!strwork_name.Equals(""))
            {
                strCriteria = strCriteria + "  And  (work_name like '%" + strwork_name + "%')";
            }
            if (!strwork_year.Equals(""))
            {
                strCriteria = strCriteria + "  And  (work_year = '" + strwork_year + "') ";
            }
            strCriteria = strCriteria + "  And  (c_active ='Y') ";
            try
            {
                if (owork.SP_SEL_WORK(strCriteria, ref ds, ref strMessage))
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        strwork_code = ds.Tables[0].Rows[0]["work_code"].ToString();
                        strwork_name = ds.Tables[0].Rows[0]["work_name"].ToString();
                        if (!ViewState["show"].ToString().Equals("1"))
                        {
                            strScript = "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + strwork_code + "';\n " +
                                                "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + strwork_name + "';\n" +
                                                "ClosePopUp('" + ViewState["show"].ToString() + "');";
                        }
                        else
                        {
                            strScript = "window.parent.document.forms[0]." + ViewState["ctrl1"].ToString() + ".value='" + strwork_code + "';\n " +
                                                "window.parent.document.forms[0]." + ViewState["ctrl2"].ToString() + ".value='" + strwork_name + "';\n" +
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
                owork.Dispose();
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
                Label lblwork_code = (Label)e.Row.FindControl("lblwork_code");
                Label lblwork_name = (Label)e.Row.FindControl("lblwork_name");

                if (!ViewState["show"].ToString().Equals("1"))
                {
                    lblwork_code.Text = "<a href=\"\" onclick=\"" +
                                        "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + lblwork_code.Text + "';\n " +
                                        "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + lblwork_name.Text + "';\n" +
                                        "ClosePopUp('" + ViewState["show"].ToString() + "');" +
                                        "return false;\" >" + lblwork_code.Text + "</a>";
                }
                else
                {
                    lblwork_code.Text = "<a href=\"\" onclick=\"" +
                                    "window.parent.document.forms[0]." + ViewState["ctrl1"].ToString() + ".value='" + lblwork_code.Text + "';\n " +
                                    "window.parent.document.forms[0]." + ViewState["ctrl2"].ToString() + ".value='" + lblwork_name.Text + "';\n" +
                                    "ClosePopUp('" + ViewState["show"].ToString() + "');" +
                                    "return false;\" >" + lblwork_code.Text + "</a>";
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

       

  

    }
}
