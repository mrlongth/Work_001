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

namespace myWeb.Person_Manage
{
    public partial class global_payment_cumulative : GlobalPageBase
    {
        #region private data
        private string strConn = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"];
        private bool[] blnAccessRight = new bool[5] { false, false, false, false, false };
        private string strPrefixCtr_main = "ctl00$ContentPlaceHolder1$TabContainer1$";
        #endregion

        protected void Page_Load(object sender, System.EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(global_payment_loan));
            ViewState["sort"] = null;
            ViewState["direction"] = "DESC";
            lblError.Text = "";

            if (!IsPostBack)
            {
                InitcboPay_Year();
                setData();
            }

        }

        public static string getMonth(object objMonth)
        {
            string strMonth = objMonth.ToString();
            if (strMonth.Equals("01"))
            {
                strMonth = "มกราคม";
            }
            else if (strMonth.Equals("02"))
            {
                strMonth = "กุมภาพันธ์";
            }
            else if (strMonth.Equals("03"))
            {
                strMonth = "มีนาคม";
            }
            else if (strMonth.Equals("04"))
            {
                strMonth = "เมษายน";
            }
            else if (strMonth.Equals("05"))
            {
                strMonth = "พฤษภาคม";
            }
            else if (strMonth.Equals("06"))
            {
                strMonth = "มิถุนายน";
            }
            else if (strMonth.Equals("07"))
            {
                strMonth = "กรกฎาคม";
            }
            else if (strMonth.Equals("08"))
            {
                strMonth = "สิงหาคม";
            }
            else if (strMonth.Equals("09"))
            {
                strMonth = "กันยายน";
            }
            else if (strMonth.Equals("10"))
            {
                strMonth = "ตุลาคม";
            }
            else if (strMonth.Equals("11"))
            {
                strMonth = "พฤศจิกายน";
            }
            else if (strMonth.Equals("12"))
            {
                strMonth = "ธันวาคม";
            }
            return strMonth;
        }

        public static string getNumber(object pNumber)
        {
            if (!pNumber.ToString().Equals(""))
            {
                string strNumber = String.Format("{0:#,##0.00}", float.Parse(pNumber.ToString()));
                return strNumber;
            }
            return "";
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
            //this.imgSave.Click += new System.Web.UI.ImageClickEventHandler(this.imgSave_Click);

        }
        #endregion

        private void setData()
        {
            cPerson oPerson = new cPerson();
            cCommon oCommon = new cCommon();
            DataSet ds = new DataSet();
            string strMessage = string.Empty, strCriteria = string.Empty;
            #region clear Data
            //Tab 1 
            string
                strperson_code = string.Empty,
                strtitle_name = string.Empty,
                strperson_thai_name = string.Empty,
                strperson_thai_surname = string.Empty,
                strposition_name = string.Empty,
                strtype_position_name = string.Empty;
            #endregion
            try
            {
                strCriteria = " and person_code = '" + base.PersonCode + "' ";
                if (!oPerson.SP_PERSON_ALL_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region get Data
                        //Tab 1 
                        strperson_code = ds.Tables[0].Rows[0]["person_code"].ToString();
                        strtitle_name = ds.Tables[0].Rows[0]["title_name"].ToString();
                        strperson_thai_name = ds.Tables[0].Rows[0]["person_thai_name"].ToString();
                        strperson_thai_surname = ds.Tables[0].Rows[0]["person_thai_surname"].ToString();
                        strposition_name = ds.Tables[0].Rows[0]["position_name"].ToString();
                        strtype_position_name = ds.Tables[0].Rows[0]["type_position_name"].ToString();
                        #endregion

                        #region set Control

                        base.PersonGroupCode = ds.Tables[0].Rows[0]["person_group_code"].ToString();


                        lblPerson_code.Text = strperson_code;
                        lblTitleName.Text = strtitle_name;
                        lblPerson_thai_name.Text = strperson_thai_name;
                        lblPerson_thai_surname.Text = strperson_thai_surname;
                        lblPosition_name.Text = strposition_name;
                        lblCumulative_acc.Text = ds.Tables[0].Rows[0]["Cumulative_acc"].ToString();
                        string strCumulative_money = "0.00";
                        try
                        {
                            strCumulative_money = getNumber(ds.Tables[0].Rows[0]["Cumulative_money"].ToString());
                        }
                        catch
                        {
                            strCumulative_money = "0.00";
                        }
                        lblCumulative_money.Text = strCumulative_money;

                        DataSet dsSum = new DataSet();
                        string strCumulativeStart = System.Configuration.ConfigurationSettings.AppSettings["CumulativeStart"];
                        strCriteria = " And person_code='" + strperson_code + "' and substring(item_code,5,7) in ('09-042','09-043','09-068')  ";
                        strCriteria += " And payment_detail_person_group_code ='" + base.PersonGroupCode + "' ";
                        strCriteria += " And (pay_year+'/'+pay_month) >= '" + strCumulativeStart + "'  ";          
                        if (!oPerson.SP_PERSON_CUMULATIVE_SEL(strCriteria, ref dsSum, ref strMessage))
                        {
                            lblError.Text = strMessage;
                        }
                        else
                        {
                            if (dsSum.Tables[0].Rows.Count > 0)
                            {
                               
                                string strCumulative_money_sum = dsSum.Tables[0].Rows[0]["payment_item_pay_sum"].ToString();
                                try
                                {
                                    strCumulative_money_sum = Helper.CStr(Helper.CDbl(strCumulative_money_sum) + Helper.CDbl(lblCumulative_money.Text));
                                    strCumulative_money_sum = getNumber(strCumulative_money_sum);
                                }
                                catch
                                {
                                    strCumulative_money_sum = "0.00";
                                }
                                lblCumulative_money_all.Text = strCumulative_money_sum;
                            }
                        }

                        #endregion

                      
                        BindGridView1();
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }


        #region GridView Event

        private void BindGridView1()
        {
            cPayment oPayment = new cPayment();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string strperson_code = string.Empty;
            strperson_code = base.PersonCode;
            string strCumulativeStart = System.Configuration.ConfigurationSettings.AppSettings["CumulativeStart"];
            if (cboPay_Year.SelectedValue != "") {
                strCriteria = " And pay_year='" + cboPay_Year.SelectedValue + "' ";
            }
            strCriteria += " And person_code='" + strperson_code + "' and substring(item_code,5,7) in ('09-042','09-043','09-068')  ";
            strCriteria += " And payment_detail_person_group_code ='" + base.PersonGroupCode + "' ";
            strCriteria += " And (pay_year+'/'+pay_month) >= '" + strCumulativeStart + "'  order by (pay_year+'/'+pay_month) desc ";
            
            try
            {
                if (!oPayment.SP_PAYMENT_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ViewState["sort"] != null)
                    {
                        ds.Tables[0].DefaultView.Sort = ViewState["sort"] + " " + ViewState["direction"];
                    }
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
                oPayment.Dispose();
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
                    if (ViewState["sort"] != null)
                    {
                        if (ViewState["sort"].Equals(GridView1.Columns[i].SortExpression))
                        {
                            bSort = true;
                            break;
                        }
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
                                ((LinkButton)c).Text += "<img border=0 src='../images/controls/tridown.gif'>";
                            }
                            else
                            {
                                ((LinkButton)c).Text += "<img border=0 src='../images/controls/triup.gif'>";
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
                if (ViewState["sort"] != null && ViewState["sort"].ToString().Equals(e.SortExpression.ToString()))
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
                BindGridView1();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            e.Cancel = true;
        }


        #endregion

        protected void imgPrint_Click(object sender, ImageClickEventArgs e)
        {
            PrintData();
        }


        protected void PrintData()
        {

            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string strPerson_code = string.Empty;
            string strReport_code = string.Empty;
            string strScript = string.Empty;
            strPerson_code = base.PersonCode;

            if (!strPerson_code.Equals(""))
            {
                strCriteria = " And (person_code='" + strPerson_code + "') and item_type = 'C' and substring(item_code,5,7) not in ('09-001','09-100')  ";

            }
            string strItem_code = string.Empty;
            Label lblitem_code = null;
            GridViewRow grow = null;
            CheckBox chkCheck = null;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                grow = GridView1.Rows[i];
                chkCheck = (CheckBox)grow.FindControl("chkCheck");
                lblitem_code = (Label)grow.FindControl("lblitem_code");

                if (chkCheck.Checked)
                {
                    strItem_code += "'" + lblitem_code.Text + "',";
                }
            }
            if (strItem_code.Length > 0)
            {
                strCriteria += " and item_code in (" + strItem_code.Substring(0, strItem_code.Length - 1) + ") ";
            }
            Session["criteria"] = strCriteria;

            cPerson cPerson = new cPerson();
            DataSet ds = new DataSet();
            if (!cPerson.SP_PERSON_ITEM_SEL(strCriteria, ref ds, ref strMessage))
            {
                lblError.Text = strMessage;
            }
            else
            {

                if (ds.Tables[0].Rows.Count > 0)
                {
                    string report_title = string.Empty;
                    string strperson_director_address = System.Configuration.ConfigurationSettings.AppSettings["person_director_address"];
                    cPerson_center oPerson_center = new cPerson_center();
                    DataSet dsCenter = new DataSet();

                    if (!oPerson_center.SP_PERSON_CENTER_SEL("And  (CITIZEN_ID= '" + base.PersonID + "') ", ref dsCenter, ref strMessage))
                    {
                        lblError.Text = strMessage;
                    }
                    else
                    {
                        if (dsCenter.Tables[0].Rows.Count > 0)
                        {
                            string strTelNo = string.Empty;
                            try
                            {
                                strTelNo = dsCenter.Tables[0].Rows[0]["TELEPHONE_WORK"].ToString();
                                strTelNo = "โทร. " + strTelNo.Substring(7, 4);
                            }
                            catch { }
                            report_title = dsCenter.Tables[0].Rows[0]["FACT_NAME"].ToString() + " " + dsCenter.Tables[0].Rows[0]["DIVISION_NAME"].ToString() + " " + dsCenter.Tables[0].Rows[0]["SUB_DI_NAME"].ToString() + " " + strTelNo;
                            // report_title = dsCenter.Tables[0].Rows[0]["SUB_DI_NAME"].ToString() + " " + dsCenter.Tables[0].Rows[0]["DIVISION_NAME"].ToString() + " " + dsCenter.Tables[0].Rows[0]["FACT_NAME"].ToString() + " " + strTelNo;
                        }
                    }

                    strReport_code = "Rep_payment_req_loan";

                    strScript = "window.open(\"global_payment_report.aspx?report_code=" + strReport_code + "&report_title=" + report_title + "\", \"_blank\");\n";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript, true);
                }
                else
                {
                    strScript = "alert('ไม่พบข้อมูล โปรดตรวจสอบเงื่อนไขในการแสดงผล');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript, true);
                    return;
                }

            }

        }

        protected void cboPay_Year_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridView1();
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
            cboPay_Year.Items.Add(new ListItem("---เลือกทั้งหมด---", ""));
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
    }
}