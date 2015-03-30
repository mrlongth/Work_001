using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using myDLL;

namespace myWeb.App_Control.payment_back
{
    public partial class payment_back_item_control : PageBase
    {
        #region private data
        private bool[] blnAccessRight = new bool[5] { false, false, false, false, false };
        private string strPrefixCtr = "ctl00$ContentPlaceHolder1$";
        #endregion

        public string strpayment_back_title
        {
            get
            {
                if (Request.QueryString["payment_back_type"] != null)
                {
                    ViewState["payment_back_type"] = Request.QueryString["payment_back_type"].ToString();
                }

                if (ViewState["payment_back_type"].ToString().Equals("O"))
                {
                    ViewState["payment_back_title"] = "ข้อมูลตกเบิกรายการเดิม";
                }
                else
                {
                    ViewState["payment_back_title"] = "ข้อมูลตกเบิกรายการใหม่";
                    txtpayment_item_old.ReadOnly = true;
                    txtpayment_item_old.CssClass = "textboxdis";
                }
                return ViewState["payment_back_title"].ToString();
            }
            set { ViewState["payment_back_title"] = value; }
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            lblError.Text = "";
            if (!IsPostBack)
            {
                txtdate_begin.Attributes.Add("onchange",
                     "document.getElementById('" + txtdate_count_des.ClientID + "').value=show_diff_date(document.getElementById('" + txtdate_begin.ClientID + "').value,document.getElementById('" + txtdate_end.ClientID + "').value);" +
                     "document.getElementById('" + txtdate_count_day.ClientID + "').value=show_diff_day(document.getElementById('" + txtdate_begin.ClientID + "').value,document.getElementById('" + txtdate_end.ClientID + "').value);" +
                     "document.getElementById('" + txtdate_count_month.ClientID + "').value=show_diff_month(document.getElementById('" + txtdate_begin.ClientID + "').value,document.getElementById('" + txtdate_end.ClientID + "').value);" +
                     "document.getElementById('" + txtdate_count_year.ClientID + "').value=show_diff_year(document.getElementById('" + txtdate_begin.ClientID + "').value,document.getElementById('" + txtdate_end.ClientID + "').value);" +
                     "return false;");
                txtdate_end.Attributes.Add("onchange",
                    "document.getElementById('" + txtdate_count_des.ClientID + "').value=show_diff_date(document.getElementById('" + txtdate_begin.ClientID + "').value,document.getElementById('" + txtdate_end.ClientID + "').value);" +
                    "document.getElementById('" + txtdate_count_day.ClientID + "').value=show_diff_day(document.getElementById('" + txtdate_begin.ClientID + "').value,document.getElementById('" + txtdate_end.ClientID + "').value);" +
                    "document.getElementById('" + txtdate_count_month.ClientID + "').value=show_diff_month(document.getElementById('" + txtdate_begin.ClientID + "').value,document.getElementById('" + txtdate_end.ClientID + "').value);" +
                    "document.getElementById('" + txtdate_count_year.ClientID + "').value=show_diff_year(document.getElementById('" + txtdate_begin.ClientID + "').value,document.getElementById('" + txtdate_end.ClientID + "').value);" +
                    "return false;");
                #region set QueryString
                if (Request.QueryString["payment_back_id"] != null)
                {
                    ViewState["payment_back_id"] = Request.QueryString["payment_back_id"].ToString();
                }
                else
                {
                    ViewState["payment_back_id"] = "0";
                }

                if (Request.QueryString["payment_back_detail_id"] != null)
                {
                    ViewState["payment_back_detail_id"] = Request.QueryString["payment_back_detail_id"].ToString();
                }
                else
                {
                    ViewState["payment_back_detail_id"] = "0";
                }


                if (Request.QueryString["payment_doc"] != null)
                {
                    ViewState["payment_doc"] = Request.QueryString["payment_doc"].ToString();
                    lblpayment_doc.Text = ViewState["payment_doc"].ToString();
                }

                if (Request.QueryString["person_code"] != null)
                {
                    ViewState["person_code"] = Request.QueryString["person_code"].ToString();
                    lblperson_code.Text = ViewState["person_code"].ToString();
                }

                if (Request.QueryString["person_name"] != null)
                {
                    ViewState["person_name"] = Request.QueryString["person_name"].ToString();
                    lblperson_name.Text = ViewState["person_name"].ToString();
                }
                if (Request.QueryString["item_code"] != null)
                {
                    ViewState["item_code"] = Request.QueryString["item_code"].ToString();
                    txtitem_code.Text = ViewState["item_code"].ToString();
                }
                if (Request.QueryString["year"] != null)
                {
                    ViewState["year"] = Request.QueryString["year"].ToString();
                    txtyear.Text = ViewState["year"].ToString();
                }
                if (Request.QueryString["mode"] != null)
                {
                    ViewState["mode"] = Request.QueryString["mode"].ToString();
                }
                if (Request.QueryString["page"] != null)
                {
                    ViewState["page"] = Request.QueryString["page"].ToString();
                }
                if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    setData();
                }
                else
                {
                    txtdate_begin.Text = cCommon.CheckDate(DateTime.Now.Date.ToString());
                    txtdate_end.Text = cCommon.CheckDate(DateTime.Now.Date.ToString());

                }


                #endregion
                #region Set Image

                imgList_item.Attributes.Add("onclick", "OpenPopUp('800px','400px','93%','ค้นหา" + strpayment_back_title + "' ,'../lov/item_lov.aspx?" +
                                                "item_code='+document.forms[0]." + strPrefixCtr + "txtitem_code.value+" +
                                                "'&item_name='+document.forms[0]." + strPrefixCtr + "txtitem_name.value+" +
                                                "'&year='+document.forms[0]." + strPrefixCtr + "txtyear.value+" +
                                                "'&person_code=" + lblperson_code.Text +
                                                "&payment_back_type=" + ViewState["payment_back_type"].ToString() +
                                                "&item_type=D&from=back&ctrl1=" + txtitem_code.ClientID + "&ctrl2=" + txtitem_name.ClientID +
                                                "&show=3', '3');return false;");
                #endregion
                txtdate_count_des.Style.Add("display", "none");
                BtnR1.Style.Add("display", "none");
                this.Title = strpayment_back_title;
                imgSaveOnly.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgSaveOnly"].Rows[0]["title"].ToString());
            }
        }

        #region private function
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
        #endregion

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
            this.imgSaveOnly.Click += new System.Web.UI.ImageClickEventHandler(this.imgSaveOnly_Click);
        }
        #endregion

        private bool saveData()
        {
            bool blnResult = false;
            bool blnDup = false;
            string strMessage = string.Empty;
            string strpayment_back_detail_id = string.Empty;
            string strpayment_back_id = string.Empty;
            string strdate_begin = string.Empty;
            string strdate_end = string.Empty;
            string strdate_count_day = string.Empty;
            string strdate_count_month = string.Empty;
            string strdate_count_year = string.Empty;
            string strdate_count_des = string.Empty;
            string stritem_code = string.Empty;
            string stritem_name = string.Empty;
            string strpayment_item_old = string.Empty;
            string strpayment_item_new = string.Empty;
            string strpayment_item_diff = string.Empty;
            string strpayment_item_back = string.Empty;
            string strcomments_sub = string.Empty,
             strActive = string.Empty,
             strCreatedBy = string.Empty,
             strUpdatedBy = string.Empty;
            string strScript = string.Empty;
            cPayment_back oPayment_back = new cPayment_back();
            DataSet ds = new DataSet();
            try
            {
                #region set Data
                strpayment_back_detail_id = ViewState["payment_back_detail_id"].ToString();
                strpayment_back_id = ViewState["payment_back_id"].ToString();
                strdate_begin = Request.Form["ctl00$ContentPlaceHolder1$txtdate_begin"].ToString();
                strdate_end = Request.Form["ctl00$ContentPlaceHolder1$txtdate_end"].ToString();
                strdate_count_day = txtdate_count_day.Value.ToString();
                strdate_count_month = txtdate_count_month.Value.ToString();
                strdate_count_year = txtdate_count_year.Value.ToString();
                strdate_count_des = txtdate_count_des.Text;
                stritem_code = txtitem_code.Text;
                stritem_name = txtitem_name.Text;
                strpayment_item_old = txtpayment_item_old.Value.ToString();
                strpayment_item_new = txtpayment_item_new.Value.ToString();
                strpayment_item_diff = txtpayment_item_diff.Value.ToString();
                strpayment_item_back = txtpayment_item_back.Value.ToString();
                strcomments_sub = txtcomments_sub.Text;
                strCreatedBy = Session["username"].ToString();
                strUpdatedBy = Session["username"].ToString();
                #endregion
                if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    #region edit

                    if (stritem_code.Substring(4) == "09-039")
                    {
                        if (oPayment_back.SP_PAYMENT_SOS_DETAIL_UPD(strpayment_back_detail_id, strpayment_back_id,
                            strdate_begin, strdate_end, strdate_count_day, strdate_count_month, strdate_count_year,
                            strdate_count_des, stritem_code, strpayment_item_old, strpayment_item_new, strpayment_item_diff,
                            strpayment_item_back, strcomments_sub, strUpdatedBy, ref strMessage))
                        {
                            blnResult = true;
                        }
                        else
                        {
                            lblError.Text = strMessage.ToString();
                        }
                    }
                    else
                    {
                        if (oPayment_back.SP_PAYMENT_BACK_DETAIL_UPD(strpayment_back_detail_id, strpayment_back_id,
                            strdate_begin, strdate_end, strdate_count_day, strdate_count_month, strdate_count_year,
                            strdate_count_des, stritem_code, strpayment_item_old, strpayment_item_new, strpayment_item_diff,
                            strpayment_item_back, strcomments_sub, strUpdatedBy, ref strMessage))
                        {
                            blnResult = true;
                        }
                        else
                        {
                            lblError.Text = strMessage.ToString();
                        }
                    }
                    #endregion
                }
                else
                {
                    #region insert
                    if (oPayment_back.SP_PAYMENT_BACK_DETAIL_INS(strpayment_back_id,
                        strdate_begin, strdate_end, strdate_count_day, strdate_count_month, strdate_count_year,
                        strdate_count_des, stritem_code, strpayment_item_old, strpayment_item_new, strpayment_item_diff,
                        strpayment_item_back, strcomments_sub, strUpdatedBy, ref strMessage))
                    {
                        blnResult = true;
                    }
                    else
                    {
                        lblError.Text = strMessage.ToString();
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oPayment_back.Dispose();
            }
            return blnResult;
        }

        private void imgSaveOnly_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (saveData())
            {
                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    txtitem_code.Text = string.Empty;
                    txtitem_name.Text = string.Empty;
                    txtpayment_item_back.Text = "0.00";
                    txtitem_code.Focus();
                    string strScript1 = "window.parent.frames['iframeShow1'].__doPostBack('ctl00$ContentPlaceHolder1$BtnR2','');ClosePopUp('2');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
                }
                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    string strScript1 = "window.parent.frames['iframeShow1'].__doPostBack('ctl00$ContentPlaceHolder1$BtnR2','');ClosePopUp('2');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
                }
                MsgBox("บันทึกข้อมูลสมบูรณ์");
            }
        }

        private void setData()
        {
            cPayment_back oPayment_back = new cPayment_back();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string strpayment_back_id = string.Empty;
            string strdate_begin = string.Empty;
            string strdate_end = string.Empty;
            string strdate_count_day = string.Empty;
            string strdate_count_month = string.Empty;
            string strdate_count_year = string.Empty;
            string strdate_count_des = string.Empty;
            string stritem_code = string.Empty;
            string stritem_name = string.Empty;
            string strpayment_item_old = string.Empty;
            string strpayment_item_new = string.Empty;
            string strpayment_item_diff = string.Empty;
            string strpayment_item_back = string.Empty;
            string strcomments_sub = string.Empty,
                strC_active = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty,
                strCreatedDate = string.Empty,
                strUpdatedDate = string.Empty;
            try
            {
                strCriteria = " and payment_back_detail_id = '" + ViewState["payment_back_detail_id"].ToString() + "' ";

                if (!oPayment_back.SP_PAYMENT_BACK_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region get Data
                        lblpayment_doc.Text = ds.Tables[0].Rows[0]["payment_doc"].ToString();
                        lblperson_code.Text = ds.Tables[0].Rows[0]["person_code"].ToString();
                        lblperson_name.Text = ds.Tables[0].Rows[0]["person_thai_name"].ToString() + "  " + ds.Tables[0].Rows[0]["person_thai_surname"].ToString();
                        txtyear.Text = ds.Tables[0].Rows[0]["payment_year"].ToString();
                        txtdate_begin.Text = cCommon.CheckDate(ds.Tables[0].Rows[0]["date_begin"].ToString());
                        txtdate_end.Text = cCommon.CheckDate(ds.Tables[0].Rows[0]["date_end"].ToString());
                        txtdate_count_day.Value = ds.Tables[0].Rows[0]["date_count_day"].ToString();
                        txtdate_count_month.Value = ds.Tables[0].Rows[0]["date_count_month"].ToString();
                        txtdate_count_year.Value = ds.Tables[0].Rows[0]["date_count_year"].ToString();
                        txtdate_count_des.Text = ds.Tables[0].Rows[0]["date_count_des"].ToString();
                        txtitem_code.Text = ds.Tables[0].Rows[0]["item_code"].ToString();
                        txtitem_name.Text = ds.Tables[0].Rows[0]["item_name"].ToString();

                        txtpayment_item_old.Value = ds.Tables[0].Rows[0]["payment_item_old"].ToString();
                        txtpayment_item_new.Value = ds.Tables[0].Rows[0]["payment_item_new"].ToString();
                        txtpayment_item_diff.Value = ds.Tables[0].Rows[0]["payment_item_diff"].ToString();
                        txtpayment_item_back.Value = ds.Tables[0].Rows[0]["payment_item_back"].ToString();

                        txtcomments_sub.Text = ds.Tables[0].Rows[0]["comments_sub"].ToString();
                        txtUpdatedBy.Text = ds.Tables[0].Rows[0]["c_updated_by"].ToString();
                        txtUpdatedDate.Text = ds.Tables[0].Rows[0]["d_updated_date"].ToString();
                        imgList_item.Visible = false;
                        imgClear_item.Visible = false;
                        txtitem_code.Enabled = false;
                        txtdate_begin.Enabled = false;
                        txtdate_end.Enabled = false;
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        protected void BtnR1_Click(object sender, EventArgs e)
        {
            setPayment_Item();
        }

        private void setPayment_Item()
        {
            cPerson oPerson = new cPerson();
            DataSet ds = new DataSet();
            string strMessage = string.Empty, strCriteria = string.Empty;
            txtdate_begin.Text = Request.Form["ctl00$ContentPlaceHolder1$txtdate_begin"].ToString();
            txtdate_end.Text = Request.Form["ctl00$ContentPlaceHolder1$txtdate_end"].ToString();
            try
            {
                strCriteria = " and person_code = '" + lblperson_code.Text.Trim() + "' " +
                              " and person_item_year = '" + txtyear.Text + "' " +
                              " and item_code = '" + txtitem_code.Text + "' ";
                if (!oPerson.SP_PERSON_ITEM_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region get Data
                        txtpayment_item_old.Value = ds.Tables[0].Rows[0]["item_debit"].ToString();
                        txtpayment_item_new.Value = ds.Tables[0].Rows[0]["item_debit"].ToString();
                        txtpayment_item_diff.Value = 0;
                        txtpayment_item_back.Value = 0;
                        txtpayment_item_new.Focus();
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        private void cal_Back()
        {
            int nMonth, nYear, nDayofMonth;
            DateTime dDate;
            dDate = DateTime.Parse(txtdate_end.Text);
            nMonth = dDate.Month;
            nYear = dDate.Year;
            if (nYear > 2200)
            {
                nYear = nYear - 543;
            }
            nDayofMonth = DateTime.DaysInMonth(nYear, nMonth);
            txtdate_begin.Text = Request.Form["ctl00$ContentPlaceHolder1$txtdate_begin"].ToString();
            txtdate_end.Text = Request.Form["ctl00$ContentPlaceHolder1$txtdate_end"].ToString();
            txtpayment_item_diff.Value = double.Parse(txtpayment_item_new.Value.ToString()) - double.Parse(txtpayment_item_old.Value.ToString());
            txtpayment_item_back.Value = double.Parse(txtdate_count_year.Value.ToString()) * 12 * double.Parse(txtpayment_item_diff.Value.ToString()) +
                                         double.Parse(txtdate_count_month.Value.ToString()) * double.Parse(txtpayment_item_diff.Value.ToString()) +
                                         double.Parse(txtdate_count_day.Value.ToString()) * double.Parse(txtpayment_item_diff.Value.ToString()) / nDayofMonth;
            txtpayment_item_back.Value = Math.Floor(double.Parse(txtpayment_item_back.Value.ToString()));
        }

        protected void txtpayment_item_new_TextChanged(object sender, EventArgs e)
        {
            cal_Back();
        }

        protected void txtpayment_item_old_TextChanged(object sender, EventArgs e)
        {
            cal_Back();
        }

        protected void imgClear_item_Click(object sender, ImageClickEventArgs e)
        {
            txtitem_code.Text = string.Empty;
            txtitem_name.Text = string.Empty;
            txtpayment_item_new.Value = 0;
            txtpayment_item_old.Value = 0;
            txtpayment_item_diff.Value = 0;
            txtpayment_item_back.Value = 0;
        }

        protected void imgList_item_Click(object sender, ImageClickEventArgs e)
        {

        }

    }
}