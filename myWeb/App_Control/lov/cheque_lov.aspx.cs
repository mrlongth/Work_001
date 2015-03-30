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
    public partial class cheque_lov : PageBase
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

                if (Request.QueryString["cheque_code"] != null)
                {
                    ViewState["cheque_code"] = Request.QueryString["cheque_code"].ToString();
                    txtcheque_code.Text = ViewState["cheque_code"].ToString();
                }
                else
                {
                    ViewState["cheque_code"] = string.Empty;
                    txtcheque_code.Text = string.Empty;
                }
                if (Request.QueryString["cheque_name"] != null)
                {
                    ViewState["cheque_name"] = Request.QueryString["cheque_name"].ToString();
                    txtcheque_name.Text = ViewState["cheque_name"].ToString();
                }
                else
                {
                    ViewState["cheque_name"] = string.Empty;
                    txtcheque_name.Text = string.Empty;
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
                ViewState["sort"] = "cheque_code";
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
            cCheque oCheque = new cCheque();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string strcheque_code = string.Empty;
            string strcheque_name = string.Empty;
            string strScript = string.Empty;
            strcheque_code = txtcheque_code.Text.Replace("'", "''").Trim();
            strcheque_name = txtcheque_name.Text.Replace("'", "''").Trim();
            if (!strcheque_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (cheque_code like '%" + strcheque_code + "%') ";
            }
            if (!strcheque_name.Equals(""))
            {
                strCriteria = strCriteria + "  And  (cheque_name like '%" + strcheque_name + "%')";
            }
            strCriteria = strCriteria + "  And  (c_active ='Y') ";
            try
            {
                if (oCheque.SP_SEL_CHEQUE(strCriteria, ref ds, ref strMessage))
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        strcheque_code = ds.Tables[0].Rows[0]["cheque_code"].ToString();
                        strcheque_name = ds.Tables[0].Rows[0]["cheque_name"].ToString();
                        if (!ViewState["show"].ToString().Equals("1"))
                        {
                            strScript = "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + strcheque_code + "';\n " +
                                                "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + strcheque_name + "';\n" +
                                                "ClosePopUp('" + ViewState["show"].ToString() + "');";
                        }
                        else
                        {
                            strScript = "window.parent.document.forms[0]." + ViewState["ctrl1"].ToString() + ".value='" + strcheque_code + "';\n " +
                                                "window.parent.document.forms[0]." + ViewState["ctrl2"].ToString() + ".value='" + strcheque_name + "';\n" +
                                                "ClosePopUp('" + ViewState["show"].ToString() + "');";
                        }
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "close", strScript, true);
                    }
                    else
                    {
                        ds.Tables[0].DefaultView.Sort = ViewState["sort"] + " " + ViewState["direction"];
                        GridView1.DataSource = ds.Tables[0];
                        GridView1.DataBind();
                        if (GridView1.Rows.Count > 12)
                        {
                            GridView1.Width = Unit.Percentage(97);
                        }
                        else
                        {
                            GridView1.Width = Unit.Percentage(100);
                        }
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
                oCheque.Dispose();
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
                Label lblcheque_code = (Label)e.Row.FindControl("lblcheque_code");
                Label lblcheque_name = (Label)e.Row.FindControl("lblcheque_name");
                string strScript;
                if (!ViewState["show"].ToString().Equals("1"))
                {
                    strScript= "<a href=\"\" onclick=\"" +
                                        "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + lblcheque_code.Text + "';\n " +
                                        "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + lblcheque_name.Text + "';\n" +
                                        "ClosePopUp('" + ViewState["show"].ToString() + "');" +
                                        "return false;\" >" + lblcheque_code.Text + "</a>";
                    lblcheque_code.Text = strScript;
                }
                else
                {
                    strScript = "<a href=\"\" onclick=\"" +
                                        "window.parent.document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + lblcheque_code.Text + "';\n " +
                                        "window.parent.document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + lblcheque_name.Text + "';\n" +
                                        "ClosePopUp('" + ViewState["show"].ToString() + "');" +
                                        "return false;\" >" + lblcheque_code.Text + "</a>";
                    lblcheque_code.Text = strScript;
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
