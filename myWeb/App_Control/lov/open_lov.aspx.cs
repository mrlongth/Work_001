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

namespace myWeb.App_Control.open_lov
{
    public partial class open_lov : PageBase
    {

        #region private data
        private string strConn = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"];
        private bool[] blnAccessRight = new bool[5] { false, false, false, false, false };
        //private string strPrefixCtr = "ctl00$ASPxRoundPanel1$ASPxRoundPanel2$ContentPlaceHolder1$";
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                imgFind.Attributes.Add("onMouseOver", "src='../../images/button/Search2.png'");
                imgFind.Attributes.Add("onMouseOut", "src='../../images/button/Search.png'");

                if (Request.QueryString["open_code"] != null)
                {
                    ViewState["open_code"] = Request.QueryString["open_code"].ToString();
                    txtopen_code.Text = ViewState["open_code"].ToString();
                }
                else
                {
                    ViewState["open_code"] = string.Empty;
                    txtopen_code.Text = string.Empty;
                }

                if (Request.QueryString["open_name"] != null)
                {
                    ViewState["open_name"] = Request.QueryString["open_name"].ToString();
                    txtopen_name.Text = ViewState["open_name"].ToString();
                }
                else
                {
                    ViewState["open_name"] = string.Empty;
                    txtopen_name.Text = string.Empty;
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

                if (Request.QueryString["lbkGetOpen"] != null)
                {
                    ViewState["lbkGetOpen"] = Request.QueryString["lbkGetOpen"].ToString();
                }
                else
                {
                    ViewState["lbkGetOpen"] = string.Empty;
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

                ViewState["sort"] = "open_code";
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
            cOpen oOpen = new cOpen();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string stropen_id = string.Empty;
            string stropen_code = string.Empty;
            string stropen_name = string.Empty;
            string strScript = string.Empty;
            stropen_code = txtopen_code.Text.Replace("'", "''").Trim();
            stropen_name = txtopen_name.Text.Replace("'", "''").Trim();
            if (!stropen_code.Equals(""))
            {
                strCriteria += "  And  (open_code like '%" + stropen_code + "%') ";
            }
            if (!stropen_name.Equals(""))
            {
                strCriteria += "  And  (open_title like '%" + stropen_name + "%') ";
            }
            //if (UnitLock == "Y" || DirectorLock == "Y")
            //{
            //    string strCriteria_sub = " and open_id in (select open_id from view_open_item LEFT JOIN [view_person_item] " +
            //                             " ON [view_person_item].item_code = view_open_item.item_code " +
            //                             " AND [view_person_item].[person_work_status_code] IN ('01','02','03') where 1 = 1 ";                        
            //    if (UnitLock == "Y")
            //    {
            //        strCriteria_sub += " and substring([view_person_item].budget_plan_unit_code,4,5) in (" + this.UnitCodeList + ") ";
            //    }
            //    if (DirectorLock == "Y")
            //    {
            //        strCriteria_sub += " and [view_person_item].budget_plan_director_code = '" + DirectorCode + "' ";
            //    }
            //    strCriteria_sub += ")";
            //    strCriteria += strCriteria_sub;
            //}
            try
            {
                if (oOpen.SP_OPEN_SEL(strCriteria, ref ds, ref strMessage))
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        stropen_code = ds.Tables[0].Rows[0]["open_code"].ToString();
                        stropen_name = ds.Tables[0].Rows[0]["open_title"].ToString();
                        stropen_id = ds.Tables[0].Rows[0]["open_id"].ToString();
                        if (!ViewState["show"].ToString().Equals("1"))
                        {
                            strScript = "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + stropen_code + "';\n " +
                                            "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + stropen_name + "';\n" +
                                            "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl3"].ToString() + "').value='" + stropen_id + "';\n" +
                                            "ClosePopUp('" + ViewState["show"].ToString() + "');";
                            if (ViewState["from"].ToString() == "open_control")
                            {
                                strScript += "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].__doPostBack('ctl00$ContentPlaceHolder1$lbkGetOpen','');";
                            }
                        }
                        else
                        {
                            strScript = "window.parent.document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + stropen_code + "';\n " +
                                            "window.parent.document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + stropen_name + "';\n" +
                                            "window.parent.document.getElementById('" + ViewState["ctrl3"].ToString() + "').value='" + stropen_id + "';\n" +
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
                oOpen.Dispose();
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
                LinkButton lblopen_code = (LinkButton)e.Row.FindControl("lblopen_code");
                Label lblopen_name = (Label)e.Row.FindControl("lblopen_name");
                HiddenField hddopen_id = (HiddenField)e.Row.FindControl("hddopen_id");
                DataRowView dv = (DataRowView)e.Row.DataItem;
                if (Helper.CStr(dv["open_remark"].ToString()).Trim().Length > 0)
                {
                    lblopen_name.Text += " (" + Helper.CStr(dv["open_remark"].ToString()) + ")";
                }

                if (!ViewState["show"].ToString().Equals("1"))
                {
                    string strScript = "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].$('#" + ViewState["ctrl1"].ToString() + "').val('" + lblopen_code.Text + "');\n " +
                                       "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].$('#" + ViewState["ctrl2"].ToString() + "').val('" + lblopen_name.Text + "');\n" +
                                       "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].$('#" + ViewState["ctrl3"].ToString() + "').val('" + hddopen_id.Value.ToString() + "');\n";

                    strScript += "ClosePopUp('" + ViewState["show"].ToString() + "');";
                    if (ViewState["from"].ToString() == "open_control")
                    {
                        strScript += "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].__doPostBack('ctl00$ContentPlaceHolder1$lbkGetOpen','');return false;";
                    }
                    lblopen_code.Attributes.Add("onclick", strScript);
                }
                else
                {
                    string strScript = "window.parent.document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + lblopen_code.Text + "';\n " +
                             "window.parent.document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + lblopen_name.Text + "';\n" +
                             "window.parent.document.getElementById('" + ViewState["ctrl3"].ToString() + "').value='" + hddopen_id.Value.ToString() + "';\n";

                    strScript += "ClosePopUp('" + ViewState["show"].ToString() + "');";
                    lblopen_code.Attributes.Add("onclick", strScript);

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
