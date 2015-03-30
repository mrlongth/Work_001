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

namespace myEFrom.App_Control.lov
{
    public partial class count_lov : PageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                imgFind.Attributes.Add("onMouseOver", "src='../../images/button/Search2.png'");
                imgFind.Attributes.Add("onMouseOut", "src='../../images/button/Search.png'");

                if (Request.QueryString["count_name"] != null)
                {
                    ViewState["count_name"] = Request.QueryString["count_name"].ToString();
                    txtcount_name.Text = ViewState["count_name"].ToString();
                }
                else
                {
                    ViewState["count_name"] = string.Empty;
                    txtcount_name.Text = string.Empty;
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

                if (Request.QueryString["from"] != null)
                {
                    ViewState["from"] = Request.QueryString["from"].ToString();
                }
                else
                {
                    ViewState["from"] = string.Empty;
                }

                ViewState["sort"] = "count_id";
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
            c3dCount obj3dCount = new c3dCount();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string strcount_name = string.Empty;
            string strcount_id = string.Empty;

            string strScript = string.Empty;
            strcount_name = txtcount_name.Text.Replace("'", "''").Trim();
            if (!strcount_name.Equals(""))
            {
                strCriteria = strCriteria + "  And  (count_name like '%" + strcount_name + "%') ";
            }
            try
            {
                var dt = obj3dCount.SP_COUNT_SEL(strCriteria);

                if (dt.Rows.Count == 1)
                {
                    strcount_id = dt.Rows[0]["count_id"].ToString();
                    strcount_name = dt.Rows[0]["count_name"].ToString();
                    if (!ViewState["show"].ToString().Equals("1"))
                    {
                        strScript = "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + strcount_id + "';\n " +
                                    "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + strcount_name + "';\n " +
                                        "ClosePopUp('" + ViewState["show"].ToString() + "');";
                    }
                    else
                    {
                        strScript = "window.parent.document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + strcount_id + "';\n" +
                                "window.parent.document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + strcount_name + "';\n" +
                                        "ClosePopUp('" + ViewState["show"].ToString() + "');";
                    }
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "close", strScript, true);
                }
                else
                {
                    dt.DefaultView.Sort = ViewState["sort"] + " " + ViewState["direction"];
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                obj3dCount.Dispose();
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
                LinkButton lblcount_id = (LinkButton)e.Row.FindControl("lblcount_id");
                LinkButton lblcount_name = (LinkButton)e.Row.FindControl("lblcount_name");
              
                if (!ViewState["show"].ToString().Equals("1"))
                {
                    string strScript = "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].$('#" + ViewState["ctrl1"].ToString() + "').val('" + lblcount_id.Text + "');\n " +
                                "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].$('#" + ViewState["ctrl2"].ToString() + "').val('" + lblcount_name.Text + "');\n ";
                    strScript += "ClosePopUp('" + ViewState["show"].ToString() + "');";
                    lblcount_id.Attributes.Add("onclick", strScript);
                    lblcount_name.Attributes.Add("onclick", strScript);
                }
                else
                {
                    string strScript = "window.parent.document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + lblcount_id.Text + "';\n " +
                                        "window.parent.document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + lblcount_name.Text + "';\n ";

                    strScript += "ClosePopUp('" + ViewState["show"].ToString() + "');";
                    lblcount_id.Attributes.Add("onclick", strScript);
                    lblcount_name.Attributes.Add("onclick", strScript);

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
