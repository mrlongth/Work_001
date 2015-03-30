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

namespace myWeb.App_Control.payment_adj
{
    public partial class payment_adj_control : PageBase
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
                imgSaveOnly.Attributes.Add("onMouseOver", "src='../../images/button/save_add2.png'");
                imgSaveOnly.Attributes.Add("onMouseOut", "src='../../images/button/save_add.png'");

                imgFind.Attributes.Add("onMouseOver", "src='../../images/button/Search2.png'");
                imgFind.Attributes.Add("onMouseOut", "src='../../images/button/Search.png'");
                ViewState["sort"] = "payment_doc";
                ViewState["direction"] = "ASC";
   
                InitcboRound();
                InitcboDirector();
                InitcboUnit();
                InitcboPerson_group();
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

        private void InitcboDirector()
        {
            cDirector oDirector = new cDirector();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strDirector_code = string.Empty;
            string strYear = cboYear.SelectedValue;
            strDirector_code = cboDirector.SelectedValue;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and director_year = '" + strYear + "'  and  c_active='Y' ";
            if (oDirector.SP_SEL_DIRECTOR(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboDirector.Items.Clear();
                cboDirector.Items.Add(new ListItem("---- เลือกข้อมูลทั้งหมด ----", ""));
                int i;
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboDirector.Items.Add(new ListItem(dt.Rows[i]["director_name"].ToString(), dt.Rows[i]["director_code"].ToString()));
                }
                if (cboDirector.Items.FindByValue(strDirector_code) != null)
                {
                    cboDirector.SelectedIndex = -1;
                    cboDirector.Items.FindByValue(strDirector_code).Selected = true;
                }
                InitcboUnit();
            }
        }

        private void InitcboUnit()
        {
            cUnit oUnit = new cUnit();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strUnit_code = cboUnit.SelectedValue;
            string strDirector_code = cboDirector.SelectedValue;
            string strYear = cboYear.SelectedValue;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and unit.unit_year = '" + strYear + "'  and  unit.c_active='Y' ";
            strCriteria = strCriteria + " and unit.director_code = '" + strDirector_code + "' ";
            if (oUnit.SP_SEL_UNIT(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboUnit.Items.Clear();
                cboUnit.Items.Add(new ListItem("---- เลือกข้อมูลทั้งหมด ----", ""));
                int i;
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboUnit.Items.Add(new ListItem(dt.Rows[i]["unit_name"].ToString(), dt.Rows[i]["unit_code"].ToString()));
                }
                if (cboUnit.Items.FindByValue(strUnit_code) != null)
                {
                    cboUnit.SelectedIndex = -1;
                    cboUnit.Items.FindByValue(strUnit_code).Selected = true;
                }
            }
        }

        private void InitcboPerson_group()
        {
            cPerson_group oPerson_group = new cPerson_group();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strperson_group_code = string.Empty;
            strperson_group_code = cboPerson_group.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and c_active='Y' ";
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
                        InitcboYear();
                        if (cboYear.Items.FindByValue(strYear) != null)
                        {
                            cboYear.SelectedIndex = -1;
                            cboYear.Items.FindByValue(strYear).Selected = true;
                        }

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

        //private bool saveData()
        //{
        //    bool blnResult = false;
        //    string strMessage = string.Empty;
        //    //Detail
        //    string strcheque_doc = string.Empty;
        //    string strcheque_code = string.Empty;
        //    string strcheque_no = string.Empty;
        //    string strcheque_pvno = string.Empty;
        //    string strcheque_money = string.Empty;
        //    string strcheque_money_thai = string.Empty;
        //    string strcheque_comment_sub = string.Empty;
        //    string strcheque_print = string.Empty;
        //    string strdirector_code = string.Empty;
        //    string strScript = string.Empty;
        //    cCheque oCheque = new cCheque();
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        #region set Data
        //       // strcheque_doc = txtcheque_doc.Text;
        //        #endregion

        //        #region insert detail
        //        GridViewRow gviewRow;
        //        int i;
        //        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        //        {
        //            gviewRow = GridView1.Rows[i];
        //            DataRowView rowView = (DataRowView)(gviewRow.DataItem);
        //            Label lblcheque_code = (Label)gviewRow.FindControl("lblcheque_code");
        //            CheckBox CheckBox1 = (CheckBox)gviewRow.FindControl("CheckBox1");
        //            Label lblcheque_name = (Label)gviewRow.FindControl("lblcheque_name");
        //            AwNumeric txtcheque_money = (AwNumeric)gviewRow.FindControl("txtcheque_money");
        //            Label lbldirector_code = (Label)gviewRow.FindControl("lbldirector_code");
        //            strcheque_code = lblcheque_code.Text;
        //            strcheque_money = txtcheque_money.Value.ToString();
        //            strcheque_comment_sub = string.Empty;
        //            strcheque_print = "N";
        //            strdirector_code = lbldirector_code.Text;
        //            if (CheckBox1.Checked)
        //            {
        //                if (!oCheque.SP_CHEQUE_DETAIL_INS(strcheque_doc, strcheque_code, strcheque_no, strcheque_pvno, strcheque_money, strcheque_money_thai,
        //                                                                                                    strcheque_comment_sub, strcheque_print, strdirector_code, ref strMessage))
        //                {
        //                    lblError.Text = strMessage;
        //                }
        //            }
        //        #endregion
        //            blnResult = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblError.Text = ex.Message.ToString();
        //    }
        //    finally
        //    {
        //        oCheque.Dispose();
        //    }
        //    return blnResult;
        //}

        private void BindGridView()
        {
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            cPayment oPayment = new cPayment();
            DataSet ds = new DataSet();
            try
            {
                string strYear = string.Empty;
                string strPay_Month = string.Empty;
                string strPay_Year = string.Empty;
                string strPerson_group_code = string.Empty;
                string strPerson_code = string.Empty;
                string strPerson_name = string.Empty;
                string strPayment_doc = string.Empty;
                string strdirector_code = string.Empty;
                string strunit_code = string.Empty;

                strYear = cboYear.SelectedValue;
                strPay_Month = cboPay_Month.SelectedValue;
                strPay_Year = cboPay_Year.SelectedValue;
                strPerson_group_code = cboPerson_group.SelectedValue;
                strdirector_code = cboDirector.SelectedValue;
                strunit_code = cboUnit.SelectedValue;
                strPerson_code = txtperson_code.Text.Trim();
                strPerson_name = txtperson_name.Text.Trim();
                strPayment_doc = txtpayment_doc.Text.Trim();

                if (!strYear.Equals(""))
                {
                    strCriteria = strCriteria + "  And  (payment_year = '" + strYear + "') ";
                }

                if (!strPay_Month.Equals(""))
                {
                    strCriteria = strCriteria + "  And  (pay_month='" + strPay_Month + "') ";
                }

                if (!strPay_Year.Equals(""))
                {
                    strCriteria = strCriteria + "  And  (pay_year='" + strPay_Year + "') ";
                }

                if (!strPerson_group_code.Equals(""))
                {
                    strCriteria = strCriteria + "  And  (person_group_code='" + strPerson_group_code + "') ";
                }

                if (!strdirector_code.Equals(""))
                {
                    strCriteria = strCriteria + "  And  (director_code = '" + strdirector_code + "') ";
                }

                if (!strunit_code.Equals(""))
                {
                    strCriteria = strCriteria + "  And  (unit_code= '" + strunit_code + "') ";
                }

                if (!strPerson_code.Equals(""))
                {
                    strCriteria = strCriteria + "  And  (person_code='" + strPerson_code + "') ";
                }

                if (!strPerson_name.Equals(""))
                {
                    strCriteria = strCriteria + "  And  (person_thai_name like '%" + strPerson_name + "%'  " +
                                                                  "  OR person_thai_surname like '%" + strPerson_name + "%'  " +
                                                                  "  OR person_eng_name like '%" + strPerson_name + "%'  " +
                                                                  "  OR person_eng_surname like '%" + strPerson_name + "%')";
                }

                if (!strPayment_doc.Equals(""))
                {
                    strCriteria = strCriteria + "  And  (payment_doc like '" + strPayment_doc + "%') ";
                }

                strCriteria = strCriteria + "  And  (c_active ='Y') ";

                if (!oPayment.SP_PAYMENT_HEAD_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    ds.Tables[0].DefaultView.Sort = ViewState["sort"] + " " + ViewState["direction"];
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                    if (ds.Tables[0].Rows.Count > 10)
                    {
                        GridView1.Width = Unit.Percentage(97);
                    }
                    else
                    {
                        GridView1.Width = Unit.Percentage(100);                    
                    }
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
                ((CheckBox)e.Row.FindControl("cbSelectAll")).Attributes.Add("onclick", "javascript:SelectAll('" +
                 ((CheckBox)e.Row.FindControl("cbSelectAll")).ClientID + "')");

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

        //protected void imgSaveOnly_Click(object sender, ImageClickEventArgs e)
        //{
        //    int intX = 0;
        //    for (int intCount = 0; intCount <= (GridView1.Rows.Count - 1); intCount++)
        //    {
        //        GridViewRow row = GridView1.Rows[intCount];
        //        CheckBox chkImportID = (CheckBox)row.FindControl("CheckBox1");
        //        if (chkImportID.Checked)
        //        {
        //            intX += intX + 1;
        //        }
        //    }
        //    if (intX == 0)
        //    {
        //        string strScript = "alert('กรุณาเลือกข้อมูล');";
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowError", strScript, true);
        //    }
        //    else
        //    {
        //        if (saveData())
        //        {
        //            string strScript1 = "window.parent.__doPostBack('ctl00$ContentPlaceHolder1$BtnR1','');ClosePopUp('1');";
        //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
        //            MsgBox("บันทึกข้อมูลสมบูรณ์");
        //        }
        //    }
        //}

        protected void cboDirector_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboUnit();
        }

        protected void imgFind_Click(object sender, ImageClickEventArgs e)
        {
            BindGridView();
        }


    }
}
