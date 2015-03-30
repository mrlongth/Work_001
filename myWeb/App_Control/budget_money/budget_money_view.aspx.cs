using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Aware.WebControls;
using myDLL;

namespace myWeb.App_Control.budget_money
{
    public partial class budget_money_view : PageBase
    {
        #region private data
        private string strPrefixCtr_main = "ctl00$ContentPlaceHolder1";
        #endregion

        public static string getNumber(object pNumber)
        {
            string strNumber = String.Format("{0:#,##0.00}", float.Parse(pNumber.ToString()));
            return strNumber;
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            lblError.Text = "";
            if (!IsPostBack)
            {
                ViewState["sort"] = "lot_code";
                ViewState["direction"] = "ASC";

                #region set QueryString
                if (Request.QueryString["budget_money_doc"] != null)
                {
                    ViewState["budget_money_doc"] = Request.QueryString["budget_money_doc"].ToString();
                }
                if (Request.QueryString["page"] != null)
                {
                    ViewState["page"] = Request.QueryString["page"].ToString();
                }
                if (Request.QueryString["mode"] != null)
                {
                    ViewState["mode"] = Request.QueryString["mode"].ToString();
                }
                if (Request.QueryString["PageStatus"] != null)
                {
                    ViewState["PageStatus"] = Request.QueryString["PageStatus"].ToString();
                }

                if (ViewState["mode"].ToString().ToLower().Equals("view"))
                {
                    setData();
                    txtbudget_money_doc.ReadOnly = true;
                    txtbudget_money_doc.CssClass = "textboxdis";
                }

                #endregion

                txtlot_name.Style.Add("display", "none");
            }
           

        }

        #region private function

        private void InitcboYear()
        {
            string strYear = string.Empty;
            strYear = cboYear.SelectedValue;
            if (strYear.Equals(""))
            {
                strYear = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["yearnow"].ToString();
            }
            DataTable odt;
            int i;
            cboYear.Items.Clear();
            odt = ((DataSet)Application["xmlconfig"]).Tables["cboYear"];
            for (i = 0; i <= odt.Rows.Count - 1; i++)
            {
                cboYear.Items.Add(new ListItem(odt.Rows[i]["Text"].ToString(), odt.Rows[i]["Value"].ToString()));
            }
            if (cboYear.Items.FindByValue(strYear) != null)
            {
                cboYear.SelectedIndex = -1;
                cboYear.Items.FindByValue(strYear).Selected = true;
            }
        }

        #endregion

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            base.OnInit(e);
        }



        #endregion


        private void setData()
        {
            cBudget_money oBudget_money = new cBudget_money();
            DataSet ds = new DataSet();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strbudget_money_year = string.Empty,
                strC_active = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty,
                strCreatedDate = string.Empty,
                strUpdatedDate = string.Empty;
            try
            {
                strCriteria = " and budget_money_doc = '" + ViewState["budget_money_doc"].ToString() + "' ";
                if (!oBudget_money.SP_BUDGET_MONEY_HEAD_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region get Data
                        txtbudget_money_doc.Text = ds.Tables[0].Rows[0]["budget_money_doc"].ToString();
                        txtbudget_money_date.Text = cCommon.CheckDate(ds.Tables[0].Rows[0]["budget_money_date"].ToString());
                        strbudget_money_year = ds.Tables[0].Rows[0]["budget_money_year"].ToString();
                        InitcboYear();
                        if (cboYear.Items.FindByValue(strbudget_money_year) != null)
                        {
                            cboYear.SelectedIndex = -1;
                            cboYear.Items.FindByValue(strbudget_money_year).Selected = true;
                        }

                        txtbudget_plan_code.Text = ds.Tables[0].Rows[0]["budget_plan_code"].ToString();
                        txtbudget_name.Text = ds.Tables[0].Rows[0]["budget_name"].ToString();
                        txtproduce_name.Text = ds.Tables[0].Rows[0]["produce_name"].ToString();
                        txtactivity_name.Text = ds.Tables[0].Rows[0]["activity_name"].ToString();
                        txtplan_name.Text = ds.Tables[0].Rows[0]["plan_name"].ToString();
                        txtwork_name.Text = ds.Tables[0].Rows[0]["work_name"].ToString();
                        txtfund_name.Text = ds.Tables[0].Rows[0]["fund_name"].ToString();
                        //  txtlot_name.Text = ds.Tables[0].Rows[0]["lot_name"].ToString();
                        txtdirector_name.Text = ds.Tables[0].Rows[0]["director_name"].ToString();
                        txtunit_name.Text = ds.Tables[0].Rows[0]["unit_name"].ToString();
                        txtbudget_money_all.Value = ds.Tables[0].Rows[0]["budget_money_all"].ToString();
                        txtbudget_money_use.Value = ds.Tables[0].Rows[0]["budget_money_use"].ToString();
                        txtbudget_money_remain.Value = ds.Tables[0].Rows[0]["budget_money_remain"].ToString();
                        txtcomments.Text = ds.Tables[0].Rows[0]["comments"].ToString();
                        strCreatedBy = ds.Tables[0].Rows[0]["c_created_by"].ToString();
                        strUpdatedBy = ds.Tables[0].Rows[0]["c_updated_by"].ToString();
                        strCreatedDate = ds.Tables[0].Rows[0]["d_created_date"].ToString();
                        strUpdatedDate = ds.Tables[0].Rows[0]["d_updated_date"].ToString();
                        #endregion

                        #region set Control

                        txtbudget_money_doc.CssClass = "textboxdis";
                        cboYear.Enabled = false;
                        cboYear.CssClass = "textboxdis";
                        txtUpdatedBy.Text = strUpdatedBy;
                        txtUpdatedDate.Text = strUpdatedDate;
                        BindGridView();
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        private void BindGridView()
        {
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string strunit_code = string.Empty;
            cItem_group oItem_group = new cItem_group();
            cBudget_money oBudget_money = new cBudget_money();
            DataSet ds = new DataSet();
            int i;
            try
            {

                strCriteria = " and budget_money_doc = '" + ViewState["budget_money_doc"].ToString() + "' ";
                if (!oBudget_money.SP_BUDGET_MONEY_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    ds.Tables[0].Columns.Add("budget_money_has");
                    for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        ds.Tables[0].Rows[i]["budget_money_has"] = "Y";
                    }
                    ds.Tables[0].DefaultView.Sort = ViewState["sort"] + " " + ViewState["direction"];
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oItem_group.Dispose();
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
                CheckBox CheckBox1 = (CheckBox)e.Row.FindControl("CheckBox1");
                TextBox txtbudget_money_suball = (TextBox)e.Row.FindControl("txtbudget_money_suball");
                TextBox txtbudget_money_subuse = (TextBox)e.Row.FindControl("txtbudget_money_subuse");
                txtbudget_money_suball.Attributes.Add("onblur", "CalAmount();");
                txtbudget_money_subuse.Attributes.Add("onblur", "CalAmount();");
         
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