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
using System.Xml.Linq;
using myDLL;

namespace myWeb.App_Control.payment_return
{
    public partial class payment_return_lov : PageBase
    {

        public static string getNumber(object pNumber)
        {
            if (!pNumber.ToString().Equals(""))
            {
                string strNumber = String.Format("{0:#,##0.00}", float.Parse(pNumber.ToString()));
                return strNumber;
            }
            return "";
        }
        private string strPrefixCtr_main = "ctl00$ContentPlaceHolder1$TabContainer1$";

        protected void Page_Load(object sender, System.EventArgs e)
        {
            lblError.Text = "";
            if (!IsPostBack)
            {
                BtnR1.Style.Add("display", "none");
                LinkButton1.Style.Add("display", "none");
                ViewState["sort"] = "payment_item_recv";
                ViewState["direction"] = "DESC";
                InitcboRound();
                #region set QueryString


                if (Request.QueryString["payment_doc"] != null)
                {
                    ViewState["payment_doc"] = Request.QueryString["payment_doc"].ToString();
                    txtpayment_doc.Text = ViewState["payment_doc"].ToString();
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

                if (Request.QueryString["PageStatus"] != null)
                {
                    ViewState["PageStatus"] = Request.QueryString["PageStatus"].ToString();
                }

                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {

                    ViewState["page"] = Request.QueryString["page"];
                    txtpayment_doc.CssClass = "textboxdis";
                }
                BindGridView();

                #endregion
            }
        }

        private void InitcboPay_Month()
        {
            string strMonth = string.Empty;
            strMonth = cboPay_Month.SelectedValue;
            if (strMonth.Equals(""))
            {
                if (DateTime.Now.Month < 10)
                {
                    strMonth = "0" + DateTime.Now.Month.ToString();
                }
                else
                {
                    strMonth = DateTime.Now.Month.ToString();
                }
            }
            DataTable odt;
            int i;
            cboPay_Month.Items.Clear();
            odt = ((DataSet)Application["xmlconfig"]).Tables["cboMonth"];
            for (i = 0; i <= odt.Rows.Count - 1; i++)
            {
                cboPay_Month.Items.Add(new ListItem(odt.Rows[i]["Text"].ToString(), odt.Rows[i]["Value"].ToString()));
            }
            if (cboPay_Month.Items.FindByValue(strMonth) != null)
            {
                cboPay_Month.SelectedIndex = -1;
                cboPay_Month.Items.FindByValue(strMonth).Selected = true;
            }
        }

        private void InitcboPay_Year()
        {
            string strYear = string.Empty;
            strYear = cboPay_Year.SelectedValue;
            if (strYear.Equals(""))
            {
                if (DateTime.Now.Year < 2200)
                {
                    strYear = (DateTime.Now.Year + 543).ToString();
                }
                else
                {
                    strYear = DateTime.Now.Year.ToString();
                }
            }
            DataTable odt;
            int i;
            cboPay_Year.Items.Clear();
            odt = ((DataSet)Application["xmlconfig"]).Tables["cboYear"];
            for (i = 0; i <= odt.Rows.Count - 1; i++)
            {
                cboPay_Year.Items.Add(new ListItem(odt.Rows[i]["Text"].ToString(), odt.Rows[i]["Value"].ToString()));
            }
            if (cboPay_Year.Items.FindByValue(strYear) != null)
            {
                cboPay_Year.SelectedIndex = -1;
                cboPay_Year.Items.FindByValue(strYear).Selected = true;
            }
        }

        private void InitcboRound()
        {
            cPayment_round oPayment_round = new cPayment_round();
            DataSet ds = new DataSet();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strYear = string.Empty;
            string strPay_Month = string.Empty;
            string strPay_Year = string.Empty;
            try
            {
                strCriteria = " and round_status= 'O' ";
                if (!oPayment_round.SP_PAYMENT_ROUND_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region get Data
                        strYear = ds.Tables[0].Rows[0]["payment_year"].ToString();
                        strPay_Month = ds.Tables[0].Rows[0]["pay_month"].ToString();
                        strPay_Year = ds.Tables[0].Rows[0]["pay_year"].ToString();
                        #endregion

                        #region set Control

                        InitcboPay_Month();
                        if (cboPay_Month.Items.FindByValue(strPay_Month) != null)
                        {
                            cboPay_Month.SelectedIndex = -1;
                            cboPay_Month.Items.FindByValue(strPay_Month).Selected = true;
                        }

                        InitcboPay_Year();
                        if (cboPay_Year.Items.FindByValue(strPay_Year) != null)
                        {
                            cboPay_Year.SelectedIndex = -1;
                            cboPay_Year.Items.FindByValue(strPay_Year).Selected = true;
                        }


                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oPayment_round.Dispose();
            }
        }


        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            //this.imgSaveOnly.Click += new System.Web.UI.ImageClickEventHandler(this.imgSaveOnly_Click);
        }
        #endregion

        private void BindGridView()
        {
            cPayment oPayment = new cPayment();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string strunit_code = string.Empty;
            try
            {
                strCriteria = " and pay_month = '" + cboPay_Month.SelectedValue + "' " +
                              " and pay_year = '" + cboPay_Year.SelectedValue + "' " +
                              " and person_code = (Select prh.person_code from view_payment_return_head prh where prh.payment_doc='" + txtpayment_doc.Text + "')" +
                              " and item_type = 'D' and  (payment_doc+item_code) not in (select isnull(prd.payment_doc_pay+prd.item_code,'') from  view_payment_return prd where person_code = prd.person_code)";
                if (!oPayment.SP_PAYMENT_ALL_SEL(strCriteria, ref ds, ref strMessage))
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
                if (GridView1.Rows.Count == 0)
                {
                    EmptyGridFix(GridView1);
                }
                oPayment.Dispose();
                ds.Dispose();
            }
        }

        #region EmptyGridFix
        protected void EmptyGridFix(GridView grdView)
        {
            // normally executes after a grid load method
            if (grdView.Rows.Count == 0 &&
                grdView.DataSource != null)
            {
                DataTable dt = null;

                // need to clone sources otherwise it will be indirectly adding to 
                // the original source

                if (grdView.DataSource is DataSet)
                {
                    dt = ((DataSet)grdView.DataSource).Tables[0].Clone();
                }
                else if (grdView.DataSource is DataTable)
                {
                    dt = ((DataTable)grdView.DataSource).Clone();
                }

                if (dt == null)
                {
                    return;
                }

                dt.Rows.Add(dt.NewRow()); // add empty row
                grdView.DataSource = dt;
                grdView.DataBind();

                // hide row
                grdView.Rows[0].Visible = false;
                grdView.Rows[0].Controls.Clear();
            }

            // normally executes at all postbacks
            if (grdView.Rows.Count == 1 &&
                grdView.DataSource == null)
            {
                bool bIsGridEmpty = true;

                // check first row that all cells empty
                for (int i = 0; i < grdView.Rows[0].Cells.Count; i++)
                {
                    if (grdView.Rows[0].Cells[i].Text != string.Empty)
                    {
                        bIsGridEmpty = false;
                    }
                }
                // hide row
                if (bIsGridEmpty)
                {
                    grdView.Rows[0].Visible = false;
                    grdView.Rows[0].Controls.Clear();
                }
            }
        }
        #endregion

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.Header))
            {
                for (int iCol = 0; iCol < e.Row.Cells.Count; iCol++)
                {
                    e.Row.Cells[iCol].Attributes.Add("class", "table_h");
                    e.Row.Cells[iCol].Wrap = false;
                }

                Label lblitem_code = (Label)e.Row.FindControl("lblitem_code");
                Label lblc_active = (Label)e.Row.FindControl("lblc_active");


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
                Label lblitem_code = (Label)e.Row.FindControl("lblitem_code");
                int nNo = (GridView1.PageSize * GridView1.PageIndex) + e.Row.RowIndex + 1;
                lblNo.Text = nNo.ToString();
                DataRowView dv = (DataRowView)e.Row.DataItem;


                #region set Image Edit & Delete
                ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                //imgDelete.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["img"].ToString();
                imgDelete.ImageUrl = @"../../images/button/return.png";
                imgDelete.Attributes.Add("title", "สงเงินคืนคลัง");
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

        protected void BtnR1_Click(object sender, EventArgs e)
        {
            BindGridView();
        }


        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            BindGridView();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string strMessage = string.Empty;
            string strCheck = string.Empty;
            string strScript = string.Empty;
            string strpayment_doc = string.Empty;
            string stritem_code = string.Empty;
            string strpayment_return_comment = string.Empty;
            string strc_active = string.Empty;
            string strCreatedBy = string.Empty;
            string strUpdatedBy = string.Empty;

            strpayment_doc = txtpayment_doc.Text;
            strc_active = "Y";
            strCreatedBy = Session["username"].ToString();
            strUpdatedBy = Session["username"].ToString();

            Label lblitem_code = (Label)GridView1.Rows[e.RowIndex].FindControl("lblitem_code");
            Aware.WebControls.AwNumeric txtpayment_item_pay = (Aware.WebControls.AwNumeric)GridView1.Rows[e.RowIndex].FindControl("txtpayment_item_pay");
            Label lblpayment_doc = (Label)GridView1.Rows[e.RowIndex].FindControl("lblpayment_doc");

            cPayment_return oPayment_return = new cPayment_return();
            try
            {
                if (!oPayment_return.SP_PAYMENT_RETURN_DETAIL_INS(strpayment_doc, lblpayment_doc.Text, lblitem_code.Text, txtpayment_item_pay.Value.ToString(), "", "", strCreatedBy, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    BindGridView();
                    string strScript1 = "window.parent.frames['iframeShow1'].__doPostBack('ctl00$ContentPlaceHolder1$BtnR2','');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);

                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oPayment_return.Dispose();
            }
        }

        protected void cboPay_Year_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridView();
        }

        protected void cboPay_Month_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridView();
        }


    }
}