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
    public partial class branch_lov : PageBase
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

                Session["menulov_name"] = "ค้นหาข้อมูลสาขาธนาคาร";

                if (Request.QueryString["branch_code"] != null)
                {
                    ViewState["branch_code"] = Request.QueryString["branch_code"].ToString();
                    txtbranch_code.Text = ViewState["branch_code"].ToString();
                }
                else
                {
                    ViewState["branch_code"] = string.Empty;
                    txtbranch_code.Text = string.Empty;
                }
                if (Request.QueryString["branch_name"] != null)
                {
                    ViewState["branch_name"] = Request.QueryString["branch_name"].ToString();
                    txtbranch_name.Text = ViewState["branch_name"].ToString();
                }
                else
                {
                    ViewState["branch_name"] = string.Empty;
                    txtbranch_name.Text = string.Empty;
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
                    ViewState["ctrl3"] = string.Empty;
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

                ViewState["sort"] = "branch_code";
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
            InitcboBank();
            cBranch oBranch = new cBranch();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string strbranch_code = string.Empty;
            string strbranch_name = string.Empty;
            string strbank_code = string.Empty;
            string strbank_name = string.Empty;
            string strScript = string.Empty;
            strbranch_code = txtbranch_code.Text.Replace("'", "''").Trim();
            strbranch_name = txtbranch_name.Text.Replace("'", "''").Trim();
            strbank_code = cboBank.SelectedValue;
            if (Request.Form[strPrefixCtr + "cboBank"] != null)
            {
                strbank_code = Request.Form[strPrefixCtr + "cboBank"].ToString();
            }
            if (!strbank_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (branch.bank_code ='" + strbank_code + "') ";
            }
            if (!strbranch_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (branch.branch_code like '%" + strbranch_code + "%') ";
            }
            if (!strbranch_name.Equals(""))
            {
                strCriteria = strCriteria + "  And  (branch.branch_name like '%" + strbranch_name + "%')";
            }
            strCriteria = strCriteria + "  And  (branch.c_active ='Y') ";
            try
            {
                if (oBranch.SP_SEL_BRANCH(strCriteria, ref ds, ref strMessage))
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        strbranch_code = ds.Tables[0].Rows[0]["branch_code"].ToString();
                        strbranch_name = ds.Tables[0].Rows[0]["branch_name"].ToString();
                        strbank_name = ds.Tables[0].Rows[0]["bank_name"].ToString();

                        if (!ViewState["show"].ToString().Equals("1"))
                        {
                            strScript = "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + strbranch_code + "';\n " +
                                                 "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + strbranch_name + "';\n" +
                                                 "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl3"].ToString() + "').value='" + strbank_name + "';\n" +
                                                 "ClosePopUp('" + ViewState["show"].ToString() + "');";
                        }
                        else
                        {
                            strScript = "window.parent.document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + strbranch_code + "';\n " +
                                                "window.parent.document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + strbranch_name + "';\n" +
                                                "window.parent.document.getElementById('" + ViewState["ctrl3"].ToString() + "').value='" + strbank_name + "';\n" +
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
                oBranch.Dispose();
                ds.Dispose();
            }
        }

        private void InitcboBank()
        {
            cBank oBank = new cBank();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strbank_code = cboBank.SelectedValue;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and  c_active='Y' ";
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
                Label lblbranch_code = (Label)e.Row.FindControl("lblbranch_code");
                Label lblbranch_name = (Label)e.Row.FindControl("lblbranch_name");
                Label lblbank_name = (Label)e.Row.FindControl("lblbank_name");
                if (!ViewState["show"].ToString().Equals("1"))
                {
                    lblbranch_code.Text = "<a href=\"\" onclick=\"" +
                                                             "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + lblbranch_code.Text + "';\n " +
                                                             "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + lblbranch_name.Text + "';\n" +
                                                             "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl3"].ToString() + "').value='" + lblbank_name.Text + "';\n" +
                                                             "ClosePopUp('" + ViewState["show"].ToString() + "');" +
                                                             "return false;\" >" + lblbranch_code.Text + "</a>";
                }
                else
                {
                    lblbranch_code.Text = "<a href=\"\" onclick=\"" +
                                                            "window.parent.document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + lblbranch_code.Text + "';\n " +
                                                            "window.parent.document.getElementById('" + ViewState["ctrl2"].ToString() + ").value='" + lblbranch_name.Text + "';\n" +
                                                            "window.parent.document.getElementById('" + ViewState["ctrl3"].ToString() + ").value='" + lblbank_name.Text + "';\n" +
                                                            "ClosePopUp('" + ViewState["show"].ToString() + "');" +
                                                            "return false;\" >" + lblbranch_code.Text + "</a>";
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

        protected void cboBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridView();
        }


    }
}
