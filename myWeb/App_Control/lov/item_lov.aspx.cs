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
    public partial class item_lov : PageBase
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

                if (Request.QueryString["item_code"] != null)
                {
                    ViewState["item_code"] = Request.QueryString["item_code"].ToString();
                    txtitem_code.Text = ViewState["item_code"].ToString();
                }
                else
                {
                    ViewState["item_code"] = string.Empty;
                    txtitem_code.Text = string.Empty;
                }

                if (Request.QueryString["item_type"] != null)
                {
                    ViewState["item_type"] = Request.QueryString["item_type"].ToString();
                    if (ViewState["item_type"].ToString().Length > 0)
                    {
                        ViewState["item_type"] = ViewState["item_type"].ToString().Substring(0, 1);
                    }
                    if (cboItem_type.Items.FindByValue(ViewState["item_type"].ToString()) != null)
                    {
                        cboItem_type.SelectedIndex = -1;
                        cboItem_type.Items.FindByValue(ViewState["item_type"].ToString()).Selected = true;
                    }
                }
                else
                {
                    ViewState["item_type"] = string.Empty;
                    cboItem_type.SelectedIndex = 0;
                }

                if (Request.QueryString["item_name"] != null)
                {
                    ViewState["item_name"] = Request.QueryString["item_name"].ToString();
                    txtitem_name.Text = ViewState["item_name"].ToString();
                }
                else
                {
                    ViewState["item_name"] = string.Empty;
                    txtitem_name.Text = string.Empty;
                }
                if (Request.QueryString["person_group_code"] != null)
                {
                    ViewState["person_group_code"] = Request.QueryString["person_group_code"].ToString();
                }
                else
                {
                    ViewState["person_group_code"] = string.Empty;
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


                if (Request.QueryString["ctrl4"] != null)
                {
                    ViewState["ctrl4"] = Request.QueryString["ctrl4"].ToString();
                }
                else
                {
                    ViewState["ctrl4"] = string.Empty;
                }

                if (Request.QueryString["ctrl5"] != null)
                {
                    ViewState["ctrl5"] = Request.QueryString["ctrl5"].ToString();
                }
                else
                {
                    ViewState["ctrl5"] = string.Empty;
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

                if (Request.QueryString["person_code"] != null)
                {
                    ViewState["person_code"] = Request.QueryString["person_code"].ToString();
                }
                else
                {
                    ViewState["person_code"] = "";
                }

                if (ViewState["from"].ToString().Equals("member") | ViewState["from"].ToString().Equals("back"))
                {
                    cboItem_type.Enabled = false;
                }

                if (ViewState["from"].ToString().Equals("payment"))
                {
                    if (DirectorLock == "Y")
                    {
                        cboItem_type.SelectedValue = "C";
                        cboItem_type.Enabled = false;
                    }
                }

                if (Request.QueryString["payment_back_type"] != null)
                {
                    ViewState["payment_back_type"] = Request.QueryString["payment_back_type"].ToString();
                }
                else 
                {
                    ViewState["payment_back_type"] = "";
                }

                if (txtyear.Text.Length == 0) 
                {
                    txtyear.Text = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["yearnow"].ToString();
                }
            
                ViewState["sort"] = "item_code";
                ViewState["direction"] = "ASC";
                InitcboPerson_group();
                BindGridView();
            }
            else
            {
                BindGridView();
            }
        }

        #region private function

        private void InitcboPerson_group()
        {
            cPerson_group oPerson_group = new cPerson_group();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strperson_group_code = string.Empty;
            strperson_group_code = ViewState["person_group_code"].ToString();
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and c_active='Y' ";
            strCriteria += " and person_group_code IN (" + PersonGroupList + ") ";
            if (oPerson_group.SP_PERSON_GROUP_SEL(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboPerson_group.Items.Clear();
                cboPerson_group.Items.Add(new ListItem("---- เลือกข้อมูลทั้งหมด ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboPerson_group.Items.Add(new ListItem(dt.Rows[i]["person_group_name"].ToString(), dt.Rows[i]["person_group_code"].ToString()));
                }
                if (cboPerson_group.Items.FindByValue(strperson_group_code) != null)
                {
                    cboPerson_group.SelectedIndex = -1;
                    cboPerson_group.Items.FindByValue(strperson_group_code).Selected = true;
                }
            }
        }


        private void BindGridView()
        {
            cItem oItem = new cItem();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string stritem_year = string.Empty;
            string stritem_code = string.Empty;
            string stritem_name = string.Empty;
            string stritem_type = string.Empty;
            string stritem_group_code = string.Empty;
            string stritem_group_name = string.Empty;
            string strlot_name = string.Empty;
            string strperson_group_code = string.Empty;
            string strperson_code = string.Empty;
            string strScript = string.Empty;
            stritem_year = txtyear.Text.Replace("'", "''").Trim();
            stritem_code = txtitem_code.Text.Replace("'", "''").Trim();
            stritem_name = txtitem_name.Text.Replace("'", "''").Trim();
            stritem_type = cboItem_type.SelectedValue;
            strperson_group_code = cboPerson_group.SelectedItem.Value;
            strperson_code = ViewState["person_code"].ToString();
            if (!stritem_year.Equals(""))
            {
                strCriteria = strCriteria + "  And  (item_year = '" + stritem_year + "') ";
            }
            if (!stritem_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (item_code like '%" + stritem_code + "%') ";
            }
            if (!stritem_name.Equals(""))
            {
                strCriteria = strCriteria + "  And  (item_name like '%" + stritem_name + "%') ";
            }
         
            if (!stritem_group_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (item_group_code like '%" + stritem_group_code + "%') ";
            }
            if (!stritem_group_name.Equals(""))
            {
                strCriteria = strCriteria + "  And  (item_group_name like '%" + stritem_group_name + "%') ";
            }
            //if (!strperson_group_code.Equals(""))
            //{
            //    strCriteria = strCriteria + "  And  (person_group_code ='" + strperson_group_code + "' Or  person_group_code='') ";
            //}

            if (!strperson_code.Equals(""))
            {

                if (!stritem_type.Equals(""))
                {
                    strCriteria = strCriteria + "  And  (item_type = '" + stritem_type + "' OR substring(item_code,5,7) in ('09-001','09-100') ) ";
                }
                strCriteria = strCriteria + "  And (substring(item_code,5,7)<>'03-002' OR (substring(item_code,5,7) in ('09-001','09-100')))";
                if (ViewState["payment_back_type"].ToString().Equals("O"))
                {
                    strCriteria = strCriteria + "  And substring(item_code,5,7) in(Select substring(item_code,5,7) from view_payment_debit where person_code ='" + strperson_code + "' ) ";
                }
                else if (ViewState["payment_back_type"].ToString().Equals("N"))
                {
                    strCriteria = strCriteria + "  And (person_group_code  in(Select person_group_code from person_work where person_code ='" + strperson_code + "') or person_group_code ='') ";
                }
                strCriteria = strCriteria + "  And  (person_group_code in (Select person_group_code from person_work Where person_code='" + strperson_code + "') Or  person_group_code='') ";
            }
            else
            {

                if (!stritem_type.Equals(""))
                {
                    strCriteria = strCriteria + "  And  (item_type = '" + stritem_type + "') ";
                }
            
            }

            strCriteria = strCriteria + "  And  (c_active ='Y') ";

            //if (DirectorLock == "N")
            //{
            //    strCriteria += " and  lot_code in (''," + LotCodeList + ") ";
            //}

            try
            {
                if (oItem.SP_ITEM_SEL(strCriteria, ref ds, ref strMessage))
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        stritem_code = ds.Tables[0].Rows[0]["item_code"].ToString();
                        stritem_name = ds.Tables[0].Rows[0]["item_name"].ToString();
                        stritem_type = ds.Tables[0].Rows[0]["item_type"].ToString();
                        stritem_group_name = ds.Tables[0].Rows[0]["item_group_name"].ToString();
                        strlot_name = ds.Tables[0].Rows[0]["lot_name"].ToString();
                        if (stritem_type.Equals("D"))
                        {
                            stritem_type = "Debit";
                        }
                        else
                        {
                            stritem_type = "Credit";
                        }
                        stritem_name = ds.Tables[0].Rows[0]["item_name"].ToString();
                        if (!ViewState["show"].ToString().Equals("1"))
                        {

                            if (ViewState["from"].ToString().Equals("member"))
                            {
                                strScript = "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + stritem_code + "';\n " +
                                                    "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + stritem_name + "';\n" +
                                                    "ClosePopUp('" + ViewState["show"].ToString() + "');";
                            }
                            else if (ViewState["from"].ToString().Equals("back"))
                            {
                                strScript = "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + stritem_code + "';\n " +
                                                    "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + stritem_name + "';\n" +
                                                    "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].__doPostBack('ctl00$ContentPlaceHolder1$BtnR1','');" +
                                                    "ClosePopUp('" + ViewState["show"].ToString() + "');";

                            }
                            else
                            {
                                strScript = "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + stritem_code + "';\n " +
                                                "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + stritem_name + "';\n" +
                                                "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl3"].ToString() + "').value='" + stritem_type + "';\n" +
                                                "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl4"].ToString() + "').value='" + stritem_group_name + "';\n" +
                                                "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl5"].ToString() + "').value='" + strlot_name + "';\n" +
                                                "ClosePopUp('" + ViewState["show"].ToString() + "');";

                            }    

                        }
                        else
                        {
                            strScript = "window.parent.document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + stritem_code + "';\n " +
                                                "window.parent.document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + stritem_name + "';\n" +
                                                "window.parent.document.getElementById('" + ViewState["ctrl3"].ToString() + "').value='" + stritem_type + "';\n" +
                                                "window.parent.document.getElementById('" + ViewState["ctrl4"].ToString() + "').value='" + stritem_group_name + "';\n" +
                                                "window.parent.document.getElementById('" + ViewState["ctrl5"].ToString() + "').value='" + strlot_name + "';\n" +
                                                "ClosePopUp('" + ViewState["show"].ToString() + "');";


                            if (ViewState["from"].ToString().Equals("member") | ViewState["from"].ToString().Equals("report"))
                            {
                                strScript = "window.parent.document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + stritem_code + "';\n " +
                                                    "window.parent.document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + stritem_name + "';\n" +
                                                    "ClosePopUp('" + ViewState["show"].ToString() + "');";
                            }


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
                oItem.Dispose();
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
                Label lblitem_code = (Label)e.Row.FindControl("lblitem_code");
                Label lblitem_name = (Label)e.Row.FindControl("lblitem_name");
                Label lblitem_type = (Label)e.Row.FindControl("lblitem_type");
                Label lblitem_group_name = (Label)e.Row.FindControl("lblitem_group_name");
                Label lbllot_name = (Label)e.Row.FindControl("lbllot_name");
                if (!ViewState["show"].ToString().Equals("1"))
                {
                    if (ViewState["from"].ToString().Equals("member"))
                    {
                        lblitem_code.Text = "<a href=\"\" onclick=\"" +
                                            "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + lblitem_code.Text + "';\n " +
                                            "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + lblitem_name.Text + "';\n" +
                                            "ClosePopUp('" + ViewState["show"].ToString() + "');" +
                                            "return false;\" >" + lblitem_code.Text + "</a>";

                    }
                    else if (ViewState["from"].ToString().Equals("back"))
                    {
                        lblitem_code.Text = "<a href=\"\" onclick=\"" +
                                            "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + lblitem_code.Text + "';\n " +
                                            "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + lblitem_name.Text + "';\n" +
                                            "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].__doPostBack('ctl00$ContentPlaceHolder1$BtnR1','');" +
                                            "ClosePopUp('" + ViewState["show"].ToString() + "');" +
                                            "return false;\" >" + lblitem_code.Text + "</a>";

                    }

                    else
                    {
                        lblitem_code.Text = "<a href=\"\" onclick=\"" +
                        "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + lblitem_code.Text + "';\n " +
                        "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + lblitem_name.Text + "';\n" +
                        "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl3"].ToString() + "').value='" + lblitem_type.Text + "';\n" +
                        "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl4"].ToString() + "').value='" + lblitem_group_name.Text + "';\n" +
                        "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl5"].ToString() + "').value='" + lbllot_name.Text + "';\n" +
                        "ClosePopUp('" + ViewState["show"].ToString() + "');" +
                        "return false;\" >" + lblitem_code.Text + "</a>";
                    }

                }
                else
                {

                    if (ViewState["from"].ToString().Equals("member") | ViewState["from"].ToString().Equals("report"))
                    {
                        lblitem_code.Text = "<a href=\"\" onclick=\"" +
                                            "window.parent.document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + lblitem_code.Text + "';\n " +
                                            "window.parent.document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + lblitem_name.Text + "';\n" +
                                            "ClosePopUp('" + ViewState["show"].ToString() + "');" +
                                            "return false;\" >" + lblitem_code.Text + "</a>";

                    }
                    if (ViewState["from"].ToString().Equals("back"))
                    {
                        lblitem_code.Text = "<a href=\"\" onclick=\"" +
                                            "window.parent.document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + lblitem_code.Text + "';\n " +
                                            "window.parent.document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + lblitem_name.Text + "';\n" +
                                            "ClosePopUp('" + ViewState["show"].ToString() + "');" +
                                            "return false;\" >" + lblitem_code.Text + "</a>";

                    }


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

        protected void cboItem_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridView();
        }

        protected void cboPerson_group_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridView();
        }

    }
}
