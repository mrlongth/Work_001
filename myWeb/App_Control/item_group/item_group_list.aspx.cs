using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Threading;
using myDLL;

namespace myWeb.App_Control.item_group
{
    public partial class item_group_list : PageBase
    {

        #region private data
        private string strConn = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"];
        private string strRecordPerPage;
        private string strPageNo = "1";
        private bool[] blnAccessRight = new bool[5] { false, false, false, false, false };
        private string strPrefixCtr = "ctl00$ASPxRoundPanel1$ASPxRoundPanel2$ContentPlaceHolder1$";
     
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //Thread.Sleep(2000);
            if (!IsPostBack)
            {
                imgNew.Attributes.Add("onMouseOver", "src='../../images/button/save2.png'");
                imgNew.Attributes.Add("onMouseOut", "src='../../images/button/save.png'");

                imgFind.Attributes.Add("onMouseOver", "src='../../images/button/Search2.png'");
                imgFind.Attributes.Add("onMouseOut", "src='../../images/button/Search.png'");

                imgNew.Attributes.Add("onclick", "OpenPopUp('800px','300px','90%','เพิ่มข้อมูลหมวดรายได้/ค่าใช้จ่าย','item_group_control.aspx?mode=add&page=0','1');return false;");
                ViewState["sort"] = "item_group_code";
                ViewState["direction"] = "ASC";
                RadioAll.Checked = true;
                InitcboYear();
                BindGridView(0);
            }
            else
            {
                if (Request.Form["ctl00$ASPxRoundPanel1$ContentPlaceHolder2$GridView1$ctl01$cboPerPage"] != null)
                {
                    strRecordPerPage = Request.Form["ctl00$ASPxRoundPanel1$ContentPlaceHolder2$GridView1$ctl01$cboPerPage"].ToString();
                    strPageNo = Request.Form["ctl00$ASPxRoundPanel1$ContentPlaceHolder2$GridView1$ctl01$txtPage"].ToString();
                }
                if (txthpage.Value != string.Empty)
                {
                    BindGridView(int.Parse(txthpage.Value));
                    txthpage.Value = string.Empty;
                }
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

        private void cboPerPage_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            GridView1.PageSize = int.Parse(strRecordPerPage);
            if (int.Parse(strPageNo) != 0)
            {
                BindGridView(int.Parse(strPageNo) - 1);
            }
            else
            {
                BindGridView(0);
            }
        }

        private void imgGo_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            BindGridView(int.Parse(strPageNo) - 1);
        }
        
        private void BindGridView(int nPageNo)
        {
            cItem_group oItem_group = new cItem_group();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string stritem_group_code = string.Empty;
            string stritem_group_name = string.Empty;
            string strActive = string.Empty;
            string stritem_group_year = string.Empty;
            stritem_group_code = txtitem_group_code.Text.Replace("'", "''").Trim();
            stritem_group_name = txtitem_group_name.Text.Replace("'", "''").Trim();
            stritem_group_year = cboYear.SelectedValue;
            if (!stritem_group_code.Equals("0"))
            {
                strCriteria = strCriteria + "  And  (item_group_code like '%" + stritem_group_code + "%') ";
            }
            if (!stritem_group_name.Equals("0"))
            {
                strCriteria = strCriteria + "  And  (item_group_name like '%" + stritem_group_name + "%')";
            }
            if (!stritem_group_year.Equals("0"))
            {
                strCriteria = strCriteria + "  And  (item_group_year = '" + stritem_group_year + "') ";
            }
            if (RadioActive.Checked)
            {
                strCriteria = strCriteria + "  And  (c_active ='Y') ";
            }
            else if (RadioCancel.Checked)
            {
                strCriteria = strCriteria + "  And  (c_active ='N') ";
            }
            try
            {
                if (!oItem_group.SP_SEL_item_group(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    try
                    {
                        GridView1.PageIndex = nPageNo;
                        txthTotalRecord.Value = ds.Tables[0].Rows.Count.ToString();
                        ds.Tables[0].DefaultView.Sort = ViewState["sort"] + " " + ViewState["direction"];
                        GridView1.DataSource = ds.Tables[0];
                        GridView1.DataBind();
                    }
                    catch
                    {
                        GridView1.PageIndex = 0;
                        txthTotalRecord.Value = ds.Tables[0].Rows.Count.ToString();
                        ds.Tables[0].DefaultView.Sort = ViewState["sort"] + " " + ViewState["direction"];
                        GridView1.DataSource = ds.Tables[0];
                        GridView1.DataBind();
                    }
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
                if (GridView1.Rows.Count > 0)
                {
                    GridView1.TopPagerRow.Visible = true;
                }
            }
        }

        protected void imgFind_Click(object sender, ImageClickEventArgs e)
        {
            BindGridView(0);
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
                Label lblitem_group_code = (Label)e.Row.FindControl("lblitem_group_code");
                Label lblitem_group_name = (Label)e.Row.FindControl("lblitem_group_name");
                Label lblc_active = (Label)e.Row.FindControl("lblc_active");
                string strStatus = lblc_active.Text;

                #region set ImageStatus
                ImageButton imgStatus = (ImageButton)e.Row.FindControl("imgStatus");
                if (strStatus.Equals("Y"))
                {
                    imgStatus.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgStatus"].Rows[0]["img"].ToString();
                    imgStatus.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgStatus"].Rows[0]["title"].ToString());
                    imgStatus.Attributes.Add("onclick", "return false;");
                }
                else
                {
                    imgStatus.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgStatus"].Rows[0]["imgdisable"].ToString();
                    imgStatus.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgStatus"].Rows[0]["titledisable"].ToString());
                    imgStatus.Attributes.Add("onclick", "return false;");
                }
                #endregion

                #region set ImageView
                ImageButton imgView = (ImageButton)e.Row.FindControl("imgView");
                imgView.Attributes.Add("onclick", "OpenPopUp('800px','300px','90%','แสดงข้อมูลหมวดรายได้/ค่าใช้จ่าย','item_group_view.aspx?mode=view&item_group_code=" + 
                                                                                lblitem_group_code.Text + "','1');return false;");
                imgView.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgView"].Rows[0]["img"].ToString();
                imgView.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgView"].Rows[0]["title"].ToString());
                #endregion

                #region set Image Edit & Delete

                ImageButton imgEdit = (ImageButton)e.Row.FindControl("imgEdit");
                Label lblCanEdit = (Label)e.Row.FindControl("lblCanEdit");
                imgEdit.Attributes.Add("onclick", "OpenPopUp('800px','300px','90%','แก้ไขข้อมูลหมวดรายได้/ค่าใช้จ่าย','item_group_control.aspx?mode=edit&item_group_code=" + 
                                                                            lblitem_group_code.Text + "&page=" + GridView1.PageIndex.ToString() + "&canEdit=Y','1');return false;");
                imgEdit.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgEdit"].Rows[0]["img"].ToString();
                imgEdit.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgEdit"].Rows[0]["title"].ToString());

                ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                imgDelete.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["img"].ToString();
                imgDelete.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["title"].ToString());
                imgDelete.Attributes.Add("onclick", "return confirm(\"คุณต้องการลบหมวดรายได้/ค่าใช้จ่าย   " + lblitem_group_code.Text + " : " + lblitem_group_name.Text + " ?\");");
                #endregion

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
            else if (e.Row.RowType.Equals(DataControlRowType.Pager))
            {
                TableCell tbc = e.Row.Cells[0];
                Label lblPrev = null;
                Label lblNext = null;
                ImageButton lbtnPrev = null;
                ImageButton lbtnNext = null;

                #region find and store Previous and Next Page
                TableRow tbr = (TableRow)tbc.Controls[0].Controls[0];
                foreach (System.Web.UI.Control c in tbr.Controls)
                {
                    if (c.GetType().ToString().Equals("System.Web.UI.WebControls.Label"))
                    {
                        Label lbl = (Label)c;
                        if (lbl.Text.IndexOf("P") != -1)
                        {
                            lblPrev = lbl;
                            lblPrev.Text = string.Empty;
                        }
                        if (lbl.Text.IndexOf("N") != -1)
                        {
                            lblNext = lbl;
                            lblNext.Text = string.Empty;
                        }
                    }
                    if (c.Controls[0].GetType().ToString().Equals("System.Web.UI.WebControls.DataControlImageButton"))
                    {
                        ImageButton lbtn = (ImageButton)c.Controls[0];
                        if (lbtn.AlternateText.IndexOf("P") != -1)
                        {
                            lbtnPrev = lbtn;
                            lbtnPrev.ImageUrl = "~/images/prev.gif";
                        }
                        if (lbtn.AlternateText.IndexOf("N") != -1)
                        {
                            lbtnNext = lbtn;
                            lbtnNext.ImageUrl = "~/images/next.gif";
                        }
                    }
                }
                #endregion

                #region render new pager
                 tbc.Text = string.Empty;
                Literal lblPager = new Literal();
                lblPager.Text = "<TABLE border='0' width='100%' cellpadding='0' cellspacing='0'><TR><TD width='30%' valign='middle'>";
                tbc.Controls.Add(lblPager);

                Label lblTotalRecord = new Label();
                lblTotalRecord.Attributes.Add("class", "label_h");
                lblTotalRecord.Text = "พบข้อมูล " + txthTotalRecord.Value.ToString() + " รายการ.";
                tbc.Controls.Add(lblTotalRecord);

                lblPager = new Literal();
                lblPager.Text = "</TD><TD width='30%' align='center' valign='middle'>";
                tbc.Controls.Add(lblPager);

                DropDownList cboPerPage = new DropDownList();
                cboPerPage.ID = "cboPerPage";

                DataTable entries;
                if ((DataSet)Application["xmlconfig"] == null)
                    return;
                else
                    entries = ((DataSet)Application["xmlconfig"]).Tables["RecordPerPage"];

                for (int i = 0; i < entries.Rows.Count; i++)
                {
                    cboPerPage.Items.Add(new ListItem(entries.Rows[i][0].ToString(), entries.Rows[i][1].ToString()));
                }

                if (cboPerPage.Items.FindByValue(strRecordPerPage) != null)
                {
                    cboPerPage.Items.FindByValue(strRecordPerPage).Selected = true;
                }

                cboPerPage.AutoPostBack = true;
                cboPerPage.SelectedIndexChanged += new System.EventHandler(cboPerPage_SelectedIndexChanged);
                tbc.Controls.Add(cboPerPage);

                lblPager = new Literal();
                lblPager.Text = "&nbsp;&nbsp;&nbsp;<span class=\"label_h\">รายการ/หน้า</span></TD><TD width='40%' align='right' valign='middle'>";
                tbc.Controls.Add(lblPager);

                if (lblPrev != null)
                {
                    tbc.Controls.Add(lblPrev);
                }
                else if (lbtnPrev != null)
                {
                    tbc.Controls.Add(lbtnPrev);
                }

                lblPager = new Literal();
                lblPager.Text = "&nbsp;&nbsp;&nbsp;<span class=\"label_h\">หน้าที่: </span>";
                tbc.Controls.Add(lblPager);

                TextBox txtPage = new TextBox();
                txtPage.AutoPostBack = false;
                txtPage.ID = "txtPage";
                txtPage.Width = System.Web.UI.WebControls.Unit.Parse("30px");
                txtPage.Attributes.Add("class", "text1");
                txtPage.Style.Add("text-align", "right");
                int nCurrentPage = (GridView1.PageIndex + 1);
                txtPage.Text = nCurrentPage.ToString();//strPageNo;
                txtPage.Attributes.Add("onkeypress", "javascript: return checkKeyCode(event);");
                txtPage.Attributes.Add("onkeyup", "javasctipt: checkInt(this, " + GridView1.PageCount.ToString() + ");");
                tbc.Controls.Add(txtPage);

                lblPager = new Literal();
                lblPager.Text = "<span class=\"label_h\"> จากทั้งหมด " + GridView1.PageCount.ToString() + "&nbsp;&nbsp;</span>";
                tbc.Controls.Add(lblPager);

                lblPager = new Literal();
                lblPager.Text = "&nbsp;&nbsp;";
                tbc.Controls.Add(lblPager);

                ImageButton imgGo = new ImageButton();
                imgGo.ID = "imgGo";
                imgGo.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgGo"].Rows[0]["img"].ToString();
                imgGo.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgGo"].Rows[0]["title"].ToString());
                imgGo.Attributes.Add("onclick", "javascript: return checkPage(" + GridView1.PageCount.ToString() + ",'กรุณาระบุข้อมูลให้ถูกต้อง.|||ctl00$ASPxRoundPanel1$ContentPlaceHolder2$GridView1$ctl01$txtPage');");
                imgGo.Click += new System.Web.UI.ImageClickEventHandler(this.imgGo_Click);
                tbc.Controls.Add(imgGo);

                lblPager = new Literal();
                lblPager.Text = "&nbsp;&nbsp;&nbsp;";
                tbc.Controls.Add(lblPager);

                if (lblNext != null)
                {
                    tbc.Controls.Add(lblNext);
                }
                else if (lbtnNext != null)
                {
                    tbc.Controls.Add(lbtnNext);
                }

                lblPager = new Literal();
                lblPager.Text = "</TD></TR></TABLE>";
                tbc.Controls.Add(lblPager);

                #endregion
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindGridView(e.NewPageIndex);
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
                GridViewRow item = (GridViewRow)GridView1.Controls[0].Controls[0];
                TextBox txtPage = (TextBox)item.FindControl("txtPage");
                BindGridView(int.Parse(txtPage.Text) - 1);
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }
        
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string strMessage = string.Empty;
            string strCheck = string.Empty;
            string strScript = string.Empty;
            string strUpdatedBy = Session["username"].ToString();
            Label lblitem_group_code = (Label)GridView1.Rows[e.RowIndex].FindControl("lblitem_group_code");
            cItem_group oItem_group = new cItem_group();
            try
            {
                if (!oItem_group.SP_ITEM_GROUP_DEL(lblitem_group_code.Text, "N", strUpdatedBy, ref strMessage))
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
                oItem_group.Dispose();
            }
            BindGridView(0);
        }

    }
}
