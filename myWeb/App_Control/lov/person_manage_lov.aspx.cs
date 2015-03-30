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
    public partial class person_manage_lov : PageBase
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

                Session["menulov_name"] = "ค้นหาข้อมูลตำแหน่งบริหาร";

                if (Request.QueryString["person_manage_code"] != null)
                {
                    ViewState["person_manage_code"] = Request.QueryString["person_manage_code"].ToString();
                    txtperson_manage_code.Text = ViewState["person_manage_code"].ToString();
                }
                else
                {
                    ViewState["person_manage_code"] = string.Empty;
                    txtperson_manage_code.Text = string.Empty;
                }
                if (Request.QueryString["person_manage_name"] != null)
                {
                    ViewState["person_manage_name"] = Request.QueryString["person_manage_name"].ToString();
                    txtperson_manage_name.Text = ViewState["person_manage_name"].ToString();
                }
                else
                {
                    ViewState["person_manage_name"] = string.Empty;
                    txtperson_manage_name.Text = string.Empty;
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

                ViewState["sort"] = "person_manage_code";
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
            cPerson_manage oPerson_manage = new cPerson_manage();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string strperson_manage_code = string.Empty;
            string strperson_manage_name = string.Empty;
            string strScript = string.Empty;
            strperson_manage_code = txtperson_manage_code.Text.Replace("'", "''").Trim();
            strperson_manage_name = txtperson_manage_name.Text.Replace("'", "''").Trim();
            if (!strperson_manage_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (person_manage_code like '%" + strperson_manage_code + "%') ";
            }
            if (!strperson_manage_name.Equals(""))
            {
                strCriteria = strCriteria + "  And  (person_manage_name like '%" + strperson_manage_name + "%')";
            }
            strCriteria = strCriteria + "  And  (c_active ='Y') ";
            try
            {
                if (oPerson_manage.SP_PERSON_MANAGE_SEL(strCriteria, ref ds, ref strMessage))
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        strperson_manage_code = ds.Tables[0].Rows[0]["person_manage_code"].ToString();
                        strperson_manage_name = ds.Tables[0].Rows[0]["person_manage_name"].ToString();
                        if (!ViewState["show"].ToString().Equals("1"))
                        {
                            strScript = "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + strperson_manage_code + "';\n " +
                                                 "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + strperson_manage_name + "';\n" +
                                                 "ClosePopUp('" + ViewState["show"].ToString() + "');";
                        }
                        else
                        {
                            strScript = "window.parent.document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + strperson_manage_code + "';\n " +
                                                "window.parent.document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + strperson_manage_name + "';\n" +
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
                oPerson_manage.Dispose();
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
                Label lblperson_manage_code = (Label)e.Row.FindControl("lblperson_manage_code");
                Label lblperson_manage_name = (Label)e.Row.FindControl("lblperson_manage_name");
                if (!ViewState["show"].ToString().Equals("1"))
                {
                    lblperson_manage_code.Text = "<a href=\"\" onclick=\"" +
                                         "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + lblperson_manage_code.Text + "';\n " +
                                         "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + lblperson_manage_name.Text + "';\n" +
                                         "ClosePopUp('" + ViewState["show"].ToString() + "');" +
                                         "return false;\" >" + lblperson_manage_code.Text + "</a>";
                }
                else
                {
                    lblperson_manage_code.Text = "<a href=\"\" onclick=\"" +
                                                            "window.parent.document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + lblperson_manage_code.Text + "';\n " +
                                                            "window.parent.document.getElementById('" + ViewState["ctrl2"].ToString() + ").value='" + lblperson_manage_name.Text + "';\n" +
                                                            "ClosePopUp('" + ViewState["show"].ToString() + "');" +
                                                            "return false;\" >" + lblperson_manage_code.Text + "</a>";
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
