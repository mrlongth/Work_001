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
    public partial class person_group_lov : PageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            // Thread.Sleep(2000);
            if (!IsPostBack)
            {
                imgFind.Attributes.Add("onMouseOver", "src='../../images/button/Search2.png'");
                imgFind.Attributes.Add("onMouseOut", "src='../../images/button/Search.png'");

                Session["menulov_name"] = "ค้นหาข้อมูลกลุ่มบุคคลากร";

                if (Request.QueryString["person_group_code"] != null)
                {
                    ViewState["person_group_code"] = Request.QueryString["person_group_code"].ToString();
                    txtperson_group_code.Text = ViewState["person_group_code"].ToString();
                }
                else
                {
                    ViewState["person_group_code"] = string.Empty;
                    txtperson_group_code.Text = string.Empty;
                }
                if (Request.QueryString["person_group_name"] != null)
                {
                    ViewState["person_group_name"] = Request.QueryString["person_group_name"].ToString();
                    txtperson_group_name.Text = ViewState["person_group_name"].ToString();
                }
                else
                {
                    ViewState["person_group_name"] = string.Empty;
                    txtperson_group_name.Text = string.Empty;
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
                ViewState["sort"] = "person_group_code";
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
            cPerson_group oPerson_group = new cPerson_group();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string strperson_group_code = string.Empty;
            string strperson_group_name = string.Empty;
            string strScript = string.Empty;
            strperson_group_code = txtperson_group_code.Text.Replace("'", "''").Trim();
            strperson_group_name = txtperson_group_name.Text.Replace("'", "''").Trim();
            if (!strperson_group_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (person_group_code like '%" + strperson_group_code + "%') ";
            }
            if (!strperson_group_name.Equals(""))
            {
                strCriteria = strCriteria + "  And  (person_group_name like '%" + strperson_group_name + "%')";
            }
            strCriteria = strCriteria + "  And  (c_active ='Y') ";
            try
            {
                if (oPerson_group.SP_PERSON_GROUP_SEL(strCriteria, ref ds, ref strMessage))
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        strperson_group_code = ds.Tables[0].Rows[0]["person_group_code"].ToString();
                        strperson_group_name = ds.Tables[0].Rows[0]["person_group_name"].ToString();
                        strScript = "self.opener.document.forms[0].elements('" + ViewState["ctrl1"].ToString() + "').value='" + strperson_group_code + "';\n " +
                                            "self.opener.document.forms[0].elements('" + ViewState["ctrl2"].ToString() + "').value='" + strperson_group_name + "';\n" +
                                            "self.close(); \n";
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
                oPerson_group.Dispose();
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
                Label lblperson_group_code = (Label)e.Row.FindControl("lblperson_group_code");
                Label lblperson_group_name = (Label)e.Row.FindControl("lblperson_group_name");
                lblperson_group_code.Text = "<a href=\"\" onclick=\"" +
                    "self.opener.document.forms[0].elements('" + ViewState["ctrl1"].ToString() + "').value='" + lblperson_group_code.Text + "';\n " +
                    "self.opener.document.forms[0].elements('" + ViewState["ctrl2"].ToString() + "').value='" + lblperson_group_name.Text + "';\n" +
                    "self.close(); return false;\" >" + lblperson_group_code.Text + "</a>";
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
