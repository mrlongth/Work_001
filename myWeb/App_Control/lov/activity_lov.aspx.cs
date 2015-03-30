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
    public partial class activity_lov : PageBase
    {

        #region private data
        private string strConn = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"];
        private bool[] blnAccessRight = new bool[5] { false, false, false, false, false };
        private string strPrefixCtr = "ctl00$ASPxRoundPanel2$ContentPlaceHolder1$";
        #endregion
        
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(activity_lov));
            if (!IsPostBack)
            {
                imgFind.Attributes.Add("onMouseOver", "src='../../images/button/Search2.png'");
                imgFind.Attributes.Add("onMouseOut", "src='../../images/button/Search.png'");

                Session["menulov_name"] = "ค้นหาข้อมูลกิจกรรม";
                if (Request.QueryString["year"] != null)
                {
                    ViewState["year"] = Request.QueryString["year"].ToString();
                    txtyear.Text = ViewState["year"].ToString();
                    txtyear.CssClass = "textboxdis";
                    txtyear.ReadOnly = true;
                }
                if (Request.QueryString["activity_code"] != null)
                {
                    ViewState["activity_code"] = Request.QueryString["activity_code"].ToString();
                    txtactivity_code.Text = ViewState["activity_code"].ToString();
                }
                else
                {
                    ViewState["activity_code"] = string.Empty;
                    txtactivity_code.Text = string.Empty;
                }
                if (Request.QueryString["activity_name"] != null)
                {
                    ViewState["activity_name"] = Request.QueryString["activity_name"].ToString();
                    txtactivity_name.Text = ViewState["activity_name"].ToString();
                }
                else
                {
                    ViewState["activity_name"] = string.Empty;
                    txtactivity_name.Text = string.Empty;
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
                ViewState["sort"] = "activity_code";
                ViewState["direction"] = "ASC";
                InitcboBudget();
                BindGridView();
            }
        }

        #region private function

        private void InitcboBudget()
        {
            cBudget oBudget = new cBudget();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strbudget_code = string.Empty;
            string strYear = txtyear.Text.Trim();
            strbudget_code = cboBudget.SelectedValue;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and budget_year = '" + strYear + "'  and  c_active='Y' ";
            if (oBudget.SP_SEL_BUDGET(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboBudget.Items.Clear();
                cboBudget.Items.Add(new ListItem("---- เลือกข้อมูลทั้งหมด ----", ""));
                int i;
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboBudget.Items.Add(new ListItem(dt.Rows[i]["budget_name"].ToString(), dt.Rows[i]["budget_code"].ToString()));
                }
                if (cboBudget.Items.FindByValue(strbudget_code) != null)
                {
                    cboBudget.SelectedIndex = -1;
                    cboBudget.Items.FindByValue(strbudget_code).Selected = true;
                }
            }
           // cboProduce.AutoPostBack = false;  
            InitcboProduce();
        }

        private void InitcboProduce()
        {
            cProduce oProduce = new cProduce();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strbudget_code = string.Empty,
                        strproduce_code = string.Empty,
                        strproduce_name = string.Empty;
            strbudget_code = cboBudget.SelectedValue;
            strproduce_code = cboProduce.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and produce.budget_code= '" + strbudget_code + "'  and  produce.c_active='Y' ";
            if (oProduce.SP_SEL_PRODUCE(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboProduce.Items.Clear();
                cboProduce.Items.Add(new ListItem("---- เลือกข้อมูลทั้งหมด ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboProduce.Items.Add(new ListItem(dt.Rows[i]["produce_name"].ToString(), dt.Rows[i]["produce_code"].ToString()));
                }
                if (cboProduce.Items.FindByValue(strproduce_code) != null)
                {
                    cboProduce.SelectedIndex = -1;
                    cboProduce.Items.FindByValue(strproduce_code).Selected = true;
                }
            }
        }

        //[AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        //public DataTable GetDataProduce(string strbudget_code)
        //{
        //    cProduce oProduce = new cProduce();
        //    string strMessage = string.Empty,
        //                strCriteria = string.Empty;
        //    string strproduce_code = string.Empty,
        //                strproduce_name = string.Empty;
        //    DataSet ds = new DataSet();
        //    DataTable dt = new DataTable();
        //    strCriteria = " and produce.budget_code = '" + strbudget_code + "'  and  produce.c_active='Y' ";
        //    if (oProduce.SP_SEL_PRODUCE(strCriteria, ref ds, ref strMessage))
        //    {
        //        dt = ds.Tables[0];
        //    }
        //    return dt;
        //}
        #endregion

        protected void imgFind_Click(object sender, ImageClickEventArgs e)
        {
            BindGridView();
        }

        private void BindGridView()
        {
           // InitcboBudget();
            cActivity oactivity = new cActivity();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string stractivity_code = string.Empty;
            string stractivity_name = string.Empty;
            string strbudget_code = string.Empty;
            string strproduce_code = string.Empty;            
            string stractivity_year = string.Empty;
            string strScript = string.Empty;
            stractivity_code = txtactivity_code.Text.Replace("'", "''").Trim();
            stractivity_name = txtactivity_name.Text.Replace("'", "''").Trim();
            stractivity_year = txtyear.Text;
            strbudget_code = "";
            if (Request.Form[strPrefixCtr + "cboBudget"] != null)
            {
                strbudget_code = Request.Form[strPrefixCtr + "cboBudget"].ToString();
            }
            strproduce_code = "";
            if (Request.Form[strPrefixCtr + "cboProduce"] != null)
            {
                strproduce_code = Request.Form[strPrefixCtr + "cboProduce"].ToString();
            }
            if (!stractivity_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (activity.activity_code like '%" + stractivity_code + "%') ";
            }
            if (!stractivity_name.Equals(""))
            {
                strCriteria = strCriteria + "  And  (activity.activity_name like '%" + stractivity_name + "%')";
            }
            if (!stractivity_year.Equals(""))
            {
                strCriteria = strCriteria + "  And  (activity.activity_year = '" + stractivity_year + "') ";
            }
            if (!strbudget_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (produce.budget_code = '" + strbudget_code + "') ";
            }
            if (!strproduce_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (activity.produce_code = '" + strproduce_code + "') ";
            }
            strCriteria = strCriteria + "  And  (activity.c_active ='Y') ";
            try
            {
                if (oactivity.SP_SEL_ACTIVITY(strCriteria, ref ds, ref strMessage))
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        stractivity_code = ds.Tables[0].Rows[0]["activity_code"].ToString();
                        stractivity_name = ds.Tables[0].Rows[0]["activity_name"].ToString();
                        if (!ViewState["show"].ToString().Equals("1"))
                        {
                            strScript = "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + stractivity_code + "';\n " +
                                                "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + stractivity_name + "';\n" +
                                                "ClosePopUp('" + ViewState["show"].ToString() + "');";
                        }
                        else
                        {
                            strScript = "window.parent.document.forms[0]." + ViewState["ctrl1"].ToString() + ".value='" + stractivity_code + "';\n " +
                                                "window.parent.document.forms[0]." + ViewState["ctrl2"].ToString() + ".value='" + stractivity_name + "';\n" +
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
                oactivity.Dispose();
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
                Label lblactivity_code = (Label)e.Row.FindControl("lblactivity_code");
                Label lblactivity_name = (Label)e.Row.FindControl("lblactivity_name");
                if (!ViewState["show"].ToString().Equals("1"))
                {
                    lblactivity_code.Text = "<a href=\"\" onclick=\"" +
                                        "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + lblactivity_code.Text + "';\n " +
                                        "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + lblactivity_name.Text + "';\n" +
                                        "ClosePopUp('" + ViewState["show"].ToString() + "');" +
                                        "return false;\" >" + lblactivity_code.Text + "</a>";
                }
                else
                {
                    lblactivity_code.Text = "<a href=\"\" onclick=\"" +
                                    "window.parent.document.forms[0]." + ViewState["ctrl1"].ToString() + ".value='" + lblactivity_code.Text + "';\n " +
                                    "window.parent.document.forms[0]." + ViewState["ctrl2"].ToString() + ".value='" + lblactivity_name.Text + "';\n" +
                                    "ClosePopUp('" + ViewState["show"].ToString() + "');" +
                                    "return false;\" >" + lblactivity_code.Text + "</a>";
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

        protected void cboBudget_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboProduce();
           BindGridView();
        }

        protected void cboProduce_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridView();
        }

    
    }
}
