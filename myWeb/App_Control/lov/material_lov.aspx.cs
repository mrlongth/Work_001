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
    public partial class material_lov : PageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                imgFind.Attributes.Add("onMouseOver", "src='../../images/button/Search2.png'");
                imgFind.Attributes.Add("onMouseOut", "src='../../images/button/Search.png'");

                if (Request.QueryString["material_code"] != null)
                {
                    ViewState["material_code"] = Request.QueryString["material_code"].ToString();
                    txtmaterial_code.Text = ViewState["material_code"].ToString();
                }
                else
                {
                    ViewState["material_code"] = string.Empty;
                    txtmaterial_code.Text = string.Empty;
                }

                if (Request.QueryString["material_name"] != null)
                {
                    ViewState["material_name"] = Request.QueryString["material_name"].ToString();
                    txtmaterial_name.Text = ViewState["material_name"].ToString();
                }
                else
                {
                    ViewState["material_name"] = string.Empty;
                    txtmaterial_name.Text = string.Empty;
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

                ViewState["sort"] = "material_code";
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
            cMaterial oMaterial = new cMaterial();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string strmaterial_id = string.Empty;
            string strmaterial_code = string.Empty;
            string strmaterial_name = string.Empty;
            string strScript = string.Empty;
            strmaterial_code = txtmaterial_code.Text.Replace("'", "''").Trim();
            strmaterial_name = txtmaterial_name.Text.Replace("'", "''").Trim();
            if (!strmaterial_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (material_code like '%" + strmaterial_code + "%') ";
            }
            if (!strmaterial_name.Equals(""))
            {
                strCriteria = strCriteria + "  And  (material_name like '%" + strmaterial_name + "%') ";
            }

            try
            {
                if (oMaterial.SP_3D_MATERIAL_SEL(strCriteria, ref ds, ref strMessage))
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        strmaterial_code = ds.Tables[0].Rows[0]["material_code"].ToString();
                        strmaterial_name = ds.Tables[0].Rows[0]["material_name"].ToString();
                        strmaterial_id = ds.Tables[0].Rows[0]["material_id"].ToString();
                        if (!ViewState["show"].ToString().Equals("1"))
                        {
                            strScript = "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + strmaterial_code + "';\n " +
                                            "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + strmaterial_name + "';\n" +
                                            "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl3"].ToString() + "').value='" + strmaterial_id + "';\n" +
                                            "ClosePopUp('" + ViewState["show"].ToString() + "');";
                        }
                        else
                        {
                            strScript = "window.parent.document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + strmaterial_code + "';\n " +
                                            "window.parent.document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + strmaterial_name + "';\n" +
                                            "window.parent.document.getElementById('" + ViewState["ctrl3"].ToString() + "').value='" + strmaterial_id + "';\n" +
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
                oMaterial.Dispose();
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
                Label lblmaterial_name = (Label)e.Row.FindControl("lblmaterial_name");
                LinkButton lblmaterial_code = (LinkButton)e.Row.FindControl("lblmaterial_code");
                HiddenField hddmaterial_id = (HiddenField)e.Row.FindControl("hddmaterial_id");
        
                if (!ViewState["show"].ToString().Equals("1"))
                {
                    string strScript = "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].$('#" + ViewState["ctrl1"].ToString() + "').val('" + lblmaterial_code.Text + "');\n " +
                                       "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].$('#" + ViewState["ctrl2"].ToString() + "').val('" + lblmaterial_name.Text + "');\n" +
                                       "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].$('#" + ViewState["ctrl3"].ToString() + "').val('" + hddmaterial_id.Value.ToString() + "');\n";

                    strScript += "ClosePopUp('" + ViewState["show"].ToString() + "');";
                    lblmaterial_code.Attributes.Add("onclick", strScript);
                }
                else
                {
                    string strScript = "window.parent.document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + lblmaterial_code.Text + "';\n " +
                             "window.parent.document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + lblmaterial_name.Text + "';\n" +
                             "window.parent.document.getElementById('" + ViewState["ctrl3"].ToString() + "').value='" + hddmaterial_id.Value.ToString() + "';\n";

                    strScript += "ClosePopUp('" + ViewState["show"].ToString() + "');";
                    lblmaterial_code.Attributes.Add("onclick", strScript);
                   
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
