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

namespace myWeb.App_Control.open
{
    public partial class open_person_select : PageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                imgFind.Attributes.Add("onMouseOver", "src='../../images/button/Search2.png'");
                imgFind.Attributes.Add("onMouseOut", "src='../../images/button/Search.png'");

                imgSave.Attributes.Add("onMouseOver", "src='../../images/button/save_add2.png'");
                imgSave.Attributes.Add("onMouseOut", "src='../../images/button/save_add.png'");


                ViewState["year"] = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["yearnow"].ToString();

                if (Request.QueryString["open_id"] != null)
                {
                    ViewState["open_id"] = Request.QueryString["open_id"].ToString();
                }
                else
                {
                    ViewState["open_id"] = "0";
                }

                if (Request.QueryString["open_head_id"] != null)
                {
                    ViewState["open_head_id"] = Request.QueryString["open_head_id"].ToString();
                }
                else
                {
                    ViewState["open_head_id"] = "0";
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

                ViewState["sort"] = "person_code";
                ViewState["direction"] = "ASC";
                BindGridView();
            }

        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ChkAll", "$(document).ready(function () { ChkAll(); }); //end document ready", true);
        }

        protected void imgFind_Click(object sender, ImageClickEventArgs e)
        {
            BindGridView();
        }

        private void BindGridView()
        {

            cOpen oOpen = new cOpen();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string strYear = string.Empty;
            string strperson_code = string.Empty;
            string strperson_name = string.Empty;
            strperson_code = txtperson_code.Text.Replace("'", "''").Trim();
            strperson_name = txtperson_name.Text.Replace("'", "''").Trim();

            if (!strperson_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (person_code= '" + strperson_code + "') ";
            }

            if (!strperson_name.Equals(""))
            {
                strCriteria = strCriteria + "  And  (person_thai_name like '%" + strperson_name + "%'  " +
                                                              "  OR person_thai_surname like '%" + strperson_name + "%' )";
            }
            strCriteria += "  And  (person_code NOT IN (SELECT person_code from open_person where open_head_id=" + ViewState["open_head_id"].ToString() + ")) ";
            strCriteria += "  And  (budget_plan_code IN (SELECT budget_plan_code from open_head where open_head_id=" + ViewState["open_head_id"].ToString() + ")) ";
            //strCriteria += " and person_group_code IN (" + PersonGroupList + ") ";

            if (DirectorLock == "Y")
            {
                strCriteria += " and substring(budget_plan_director_code,4,2) = substring('" + DirectorCode + "',4,2) ";
            }

            if (UnitLock == "Y")
            {
                strCriteria += " and substring(budget_plan_unit_code,4,5) in (" + this.UnitCodeList + ") ";
            }

            try
            {
                int intOpenID = Helper.CInt(ViewState["open_id"]);
                if (!oOpen.SP_OPEN_PERSON_TMP_SEL(intOpenID, strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
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
                oOpen.Dispose();
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
                DataRowView dv = (DataRowView)e.Row.DataItem;

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

        protected void imgSave_Click(object sender, ImageClickEventArgs e)
        {
            if (SavePerson())
            {
                MsgBox("บันทึกข้อมูลสมบูรณ์");
                string strScript = "ClosePopUp('" + ViewState["show"].ToString() + "');";
                strScript += "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].__doPostBack('ctl00$ContentPlaceHolder1$lbkGetPerson','');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "GetPerson", strScript, true);          
            }
        }

        private bool SavePerson()
        {
            bool blnResult = false;
            cOpen oOpen = new cOpen();
            try
            {
                foreach (GridViewRow gr in GridView1.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");
                    Label lblperson_code = (Label)gr.FindControl("lblperson_code");
                    HiddenField hddopen_rate = (HiddenField)gr.FindControl("hddopen_rate");
                    if (chkSelect.Checked)
                    {
                        if (!oOpen.SP_OPEN_PERSON_INS(ViewState["open_head_id"].ToString(),
                                Helper.CStr(lblperson_code.Text),
                                DateTime.Now.ToString("dd/MM/yyyy"),
                                DateTime.Now.ToString("dd/MM/yyyy"),
                                "0",
                                "0",
                                "0",
                                hddopen_rate.Value,
                                "0",
                                ref _strMessage))
                        {
                            lblError.Text = _strMessage;
                            return false;
                        }
                    }
                }
                blnResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oOpen.Dispose();
            }
            return blnResult;
        }


    }
}
