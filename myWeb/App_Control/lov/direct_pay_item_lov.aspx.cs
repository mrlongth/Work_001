using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using myDLL;

namespace myWeb.App_Control.lov
{
    public partial class direct_pay_item_lov : PageBase
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

                if (Request.QueryString["year"] != null)
                {
                    ViewState["item_year"] = Request.QueryString["year"].ToString();
                    txtyear.Text = ViewState["item_year"].ToString();
                    txtyear.CssClass = "textboxdis";
                    txtyear.ReadOnly = true;
                }

                if (Request.QueryString["item_description"] != null)
                {
                    ViewState["item_description"] = Request.QueryString["item_description"].ToString();
                    txtdescription.Text = ViewState["item_description"].ToString();
                }
                else
                {
                    ViewState["item_description"] = string.Empty;
                    txtdescription.Text = string.Empty;
                }


                if (Request.QueryString["ctrl1"] != null)
                {
                    ViewState["ctrl1"] = Request.QueryString["ctrl1"].ToString();
                }
                else
                {
                    ViewState["ctrl1"] = string.Empty;
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
                    ViewState["from"] = "";
                }

                if (txtyear.Text.Length == 0)
                {
                    txtyear.Text = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["yearnow"].ToString();
                }

                ViewState["sort"] = "direct_pay_code";
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
            cItem oItem = new cItem();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string stritem_year;
            string strdescription;
            stritem_year = txtyear.Text.Replace("'", "''").Trim();
            strdescription = txtdescription.Text.Replace("'", "''").Trim();
            if (!stritem_year.Equals(""))
            {
                strCriteria = strCriteria + "  And  (item_year = '" + stritem_year + "') ";
            }
            if (!strdescription.Equals(""))
            {
                strCriteria = strCriteria + "  And  (item_code like '%" + strdescription + "%' ";
                strCriteria = strCriteria + "  OR item_name like '%" + strdescription + "%' ";
                strCriteria = strCriteria + "  OR direct_pay_code like '%" + strdescription + "%') ";
            }

            try
            {
                if (oItem.SP_DIRECT_PAY_ITEM_SEL(strCriteria, ref ds, ref strMessage))
                {
                    ds.Tables[0].DefaultView.Sort = ViewState["sort"] + " " + ViewState["direction"];
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
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
                oItem.Dispose();
                ds.Dispose();
            }
        }

        #endregion

        protected void imgFind_Click(object sender, ImageClickEventArgs e)
        {
            BindGridView();
        }

        protected void imgSelect_Click(object sender, ImageClickEventArgs e)
        {
            var strScript = string.Empty;
            var strCodeList = string.Empty;
            CheckBox check;
            Label lbldirect_pay_code;
            var ichk = 0;
            for (var i = 0; i < GridView1.Rows.Count; i++)
            {
                check = (CheckBox)GridView1.Rows[i].FindControl("chkSelect");
                lbldirect_pay_code = (Label)GridView1.Rows[i].FindControl("lbldirect_pay_code");
                if (Request.Form[check.UniqueID]=="on")
                {
                    ichk++;
                    strCodeList += lbldirect_pay_code.Text + ",";
                }                        
            }
            if (ichk > 10)
            {
                strScript = "alert('สามารถเลือกรายการได้สูงสุด 10 รายการ โปรดตรวจสอบ')";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "close", strScript, true);
                return;
            }
            if (strCodeList.Length > 0)
                strCodeList = strCodeList.Substring(0, strCodeList.Length - 1);
            if (!ViewState["show"].ToString().Equals("1"))
            {
                strScript = "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + strCodeList + "';\n " +
                                    "ClosePopUp('" + ViewState["show"].ToString() + "');";
            }
            else
            {
                strScript = "window.parent.document.forms[0]." + ViewState["ctrl1"].ToString() + ".value='" + strCodeList + "';\n " +
                                    "ClosePopUp('" + ViewState["show"].ToString() + "');";
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "close", strScript, true);

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

        public static string getItemtype(object mData)
        {
            if (mData.Equals("D"))
            {
                return "Debit";
            }
            else
            {
                return "Credit";
            }
        }

    }
}
