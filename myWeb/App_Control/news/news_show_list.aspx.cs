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

namespace myWeb.App_Control.news
{
    public partial class news_show_list : PageBase
    {

        #region private data
        private string strRecordPerPage;
        private string strPageNo = "1";
        private bool[] blnAccessRight = new bool[5] { false, false, false, false, false };
        private string strPrefixCtr = "ctl00$ASPxRoundPanel1$ASPxRoundPanel2$ContentPlaceHolder1$";
        private string strPrefixCtr_2 = "ctl00$ASPxRoundPanel1$ASPxRoundPanel2$ContentPlaceHolder1$";
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                imgFind.Attributes.Add("onMouseOver", "src='../../images/button/Search2.png'");
                imgFind.Attributes.Add("onMouseOut", "src='../../images/button/Search.png'");


                ViewState["sort"] = "d_created_date";
                ViewState["direction"] = "DESC";
                BindGridView(0);

            }
            else
            {
                if (Request.Form[strPrefixCtr_2 + "GridView1$ctl01$cboPerPage"] != null)
                {
                    strRecordPerPage = Request.Form[strPrefixCtr_2 + "GridView1$ctl01$cboPerPage"].ToString();
                    strPageNo = Request.Form[strPrefixCtr_2 + "GridView1$ctl01$txtPage"].ToString();
                }
                if (txthpage.Value != string.Empty)
                {
                    BindGridView(int.Parse(txthpage.Value));
                    txthpage.Value = string.Empty;
                }
            }

        }

        #region private function

      
        private void BindGridView(int nPageNo)
        {
            cNews oNews = new cNews();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;

            if (!txtCetreria.Equals(""))
            {
                strCriteria = strCriteria + "  And  (new_title like '%" + txtCetreria.Text.Trim() + "%') ";
            }


            try
            {
                if (!oNews.SP_NEW_SEL(strCriteria, ref ds, ref strMessage))
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
                oNews.Dispose();
                ds.Dispose();
                if (GridView1.Rows.Count > 0)
                {
                    GridView1.TopPagerRow.Visible = true;
                }
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
                
                HiddenField hddnew_id = (HiddenField)e.Row.FindControl("hddnew_id");
                LinkButton lblnew_title = (LinkButton)e.Row.FindControl("lblnew_title");
                Label lblnew_type = (Label)e.Row.FindControl("lblnew_type");
                lblnew_title.PostBackUrl = "~/App_Control/news/news_show_view.aspx?new_id=" + hddnew_id.Value.ToString() + "&new_type=" + lblnew_type.Text ;
                
                #region set ImageStatus
                Label lblnew_status = (Label)e.Row.FindControl("lblnew_status");
                string strNew_status = lblnew_status.Text;
                //N = Normal , P = Pin , Q = Quick
                ImageButton imgNew_status = (ImageButton)e.Row.FindControl("imgNew_status");
                if (strNew_status.Equals("Q"))
                {
                    imgNew_status.ImageUrl = "~/images/new/quick.gif";
                    imgNew_status.Attributes.Add("title", "ข่าวด่วน");
                    imgNew_status.Attributes.Add("onclick", "return false;");
                }
                else if (strNew_status.Equals("P"))
                {
                    imgNew_status.ImageUrl = "~/images/new/pin.gif";
                    imgNew_status.Attributes.Add("title", "ข่าวปักมุด");
                    imgNew_status.Attributes.Add("onclick", "return false;");
                }
                else
                {
                    imgNew_status.ImageUrl = "~/images/new/normal.gif";
                    imgNew_status.Attributes.Add("title", "ข่าวทั่วไป");
                    imgNew_status.Attributes.Add("onclick", "return false;");
                }
                
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
            Label lblpayment_doc = (Label)GridView1.Rows[e.RowIndex].FindControl("lblpayment_doc");
            cPayment oPayment = new cPayment();
            try
            {
                if (!oPayment.SP_PAYMENT_DEL(lblpayment_doc.Text, "N", strUpdatedBy, ref strMessage))
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
                oPayment.Dispose();
            }
            BindGridView(0);
        }

    }
}
