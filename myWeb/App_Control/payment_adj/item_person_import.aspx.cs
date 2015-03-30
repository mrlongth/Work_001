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
using Aware.WebControls;
using myDLL;

namespace myWeb.App_Control.item_person_import
{
    public partial class payment_import : PageBase
    {
        private string strPrefixCtr = "ctl00$ASPxRoundPanel1$ASPxRoundPanel2$ContentPlaceHolder1$";
        private string strPrefixCtr_2 = "ctl00$ASPxRoundPanel1$ContentPlaceHolder2$";
        
        private string strRecordPerPage ="100";
        private string strPageNo = "1";

        public static string getNumber(object pNumber)
        {
            string strNumber = pNumber.ToString();
            if (pNumber.ToString().Length > 0)
            {
                strNumber = String.Format("{0:#,##0.00}", decimal.Parse(pNumber.ToString()));
            }
            return strNumber;
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            lblError.Text = "";
            if (!IsPostBack)
            {
                imgSaveOnly.Attributes.Add("onMouseOver", "src='../../images/button/save_add2.png'");
                imgSaveOnly.Attributes.Add("onMouseOut", "src='../../images/button/save_add.png'");

                //imgFind.Attributes.Add("onMouseOver", "src='../../images/button/Search2.png'");
                //imgFind.Attributes.Add("onMouseOut", "src='../../images/button/Search.png'");

                imgCancel.Attributes.Add("onMouseOver", "src='../../images/button/cancel2.png'");
                imgCancel.Attributes.Add("onMouseOut", "src='../../images/button/cancel.png'");

                imgImport.Attributes.Add("onMouseOver", "src='../../images/button/import2.png'");
                imgImport.Attributes.Add("onMouseOut", "src='../../images/button/import.png'");

                //imgSaveOnly2.Attributes.Add("onMouseOver", "src='../../images/button/save_add2.png'");
                //imgSaveOnly2.Attributes.Add("onMouseOut", "src='../../images/button/save_add.png'");

                imgCancel2.Attributes.Add("onMouseOver", "src='../../images/button/cancel2.png'");
                imgCancel2.Attributes.Add("onMouseOut", "src='../../images/button/cancel.png'");

                string strYear = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["yearnow"].ToString();

                panelSeek2.Visible = false;
                panelSeek.Visible = true;

                ViewState["sort"] = "A00";
                ViewState["direction"] = "ASC";
                InitcboRound();
                InitcboPerson_group();
                InitcboDirector();
                InitcboUnit();

                panelSeek2.Visible = false;
                panelSeek.Visible = true;
                GridView1.Visible = false;
             
                imgExcel.Visible = true;
                //imgFind.Visible = false;
                imgSaveOnly.Visible = true;
                GridView1.PageSize = int.Parse(strRecordPerPage);

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
                    BindGridView4(int.Parse(txthpage.Value));
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
            cboYear2.Items.Clear();
            odt = ((DataSet)Application["xmlconfig"]).Tables["cboYear"];
            for (i = 0; i <= odt.Rows.Count - 1; i++)
            {
                cboYear.Items.Add(new ListItem(odt.Rows[i]["Text"].ToString(), odt.Rows[i]["Value"].ToString()));
                cboYear2.Items.Add(new ListItem(odt.Rows[i]["Text"].ToString(), odt.Rows[i]["Value"].ToString()));
            }
            if (cboYear.Items.FindByValue(strYear) != null)
            {
                cboYear.SelectedIndex = -1;
                cboYear.Items.FindByValue(strYear).Selected = true;
                cboYear2.SelectedIndex = -1;
                cboYear2.Items.FindByValue(strYear).Selected = true;
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
            cboPay_Month2.Items.Clear();
            odt = ((DataSet)Application["xmlconfig"]).Tables["cboMonth"];
            for (i = 0; i <= odt.Rows.Count - 1; i++)
            {
                cboPay_Month.Items.Add(new ListItem(odt.Rows[i]["Text"].ToString(), odt.Rows[i]["Value"].ToString()));
                cboPay_Month2.Items.Add(new ListItem(odt.Rows[i]["Text"].ToString(), odt.Rows[i]["Value"].ToString()));
            }
            if (cboPay_Month.Items.FindByValue(strMonth) != null)
            {
                cboPay_Month.SelectedIndex = -1;
                cboPay_Month.Items.FindByValue(strMonth).Selected = true;
                cboPay_Month2.SelectedIndex = -1;
                cboPay_Month2.Items.FindByValue(strMonth).Selected = true;
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
            cboPay_Year2.Items.Clear();
            odt = ((DataSet)Application["xmlconfig"]).Tables["cboYear"];
            for (i = 0; i <= odt.Rows.Count - 1; i++)
            {
                cboPay_Year.Items.Add(new ListItem(odt.Rows[i]["Text"].ToString(), odt.Rows[i]["Value"].ToString()));
                cboPay_Year2.Items.Add(new ListItem(odt.Rows[i]["Text"].ToString(), odt.Rows[i]["Value"].ToString()));
            }
            if (cboPay_Year.Items.FindByValue(strYear) != null)
            {
                cboPay_Year.SelectedIndex = -1;
                cboPay_Year.Items.FindByValue(strYear).Selected = true;
                cboPay_Year2.SelectedIndex = -1;
                cboPay_Year2.Items.FindByValue(strYear).Selected = true;
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
            if (DirectorLock == "Y")
            {
                strCriteria += " and substring(director_code,4,2) = substring('" + DirectorCode + "',4,2) ";
            }
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
                            cboYear2.SelectedIndex = -1;
                            cboYear2.Items.FindByValue(strYear).Selected = true;
                        }

                        InitcboPay_Month();
                        if (cboPay_Month.Items.FindByValue(strPay_Month) != null)
                        {
                            cboPay_Month.SelectedIndex = -1;
                            cboPay_Month.Items.FindByValue(strPay_Month).Selected = true;
                            cboPay_Month2.SelectedIndex = -1;
                            cboPay_Month2.Items.FindByValue(strPay_Month).Selected = true;
                        }

                        InitcboPay_Year();
                        if (cboPay_Year.Items.FindByValue(strPay_Year) != null)
                        {
                            cboPay_Year.SelectedIndex = -1;
                            cboPay_Year.Items.FindByValue(strPay_Year).Selected = true;
                            cboPay_Year2.SelectedIndex = -1;
                            cboPay_Year2.Items.FindByValue(strPay_Year).Selected = true;
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
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            //  this.imgSaveOnly.Click += new System.Web.UI.ImageClickEventHandler(this.imgSaveOnly_Click);
        }
        #endregion

        private bool saveData()
        {
            bool blnResult = false;
            string strMessage = string.Empty;
            string strActive = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty;
            string strScript = string.Empty;
            string pitem_code = string.Empty;
            string ppayment_item_tax = "Y";
            string ppayment_item_sos = "Y";
            string pcomments_sub = string.Empty;
            int i;
            cPayment oPayment = new cPayment();
            try
            {
                strActive = "Y";
                strCreatedBy = Session["username"].ToString();
                strUpdatedBy = Session["username"].ToString();


                GridViewRow gviewRow;

                for (i = 0; i <= GridView1.Rows.Count - 1; i++)
                {
                    gviewRow = GridView1.Rows[i];
                    HiddenField hdfpayment_doc = (HiddenField)gviewRow.FindControl("hdfpayment_doc");
                    AwNumeric txtmoney_credit = (AwNumeric)gviewRow.FindControl("txtmoney_credit");
                    CheckBox CheckBox1 = (CheckBox)gviewRow.FindControl("CheckBox1");

                    if (CheckBox1.Checked)
                    {
                        DataSet ds = new DataSet();
                        string strCheckDup = string.Empty;
                        strCheckDup = " and payment_doc = '" + hdfpayment_doc.Value + "' " +
                                                      " and item_code = '" + pitem_code + "' ";
                        if (!oPayment.SP_PAYMENT_SEL(strCheckDup, ref ds, ref strMessage))
                        {
                            lblError.Text = strMessage;
                        }
                        else
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                string strpayment_detail_id = ds.Tables[0].Rows[0]["payment_detail_id"].ToString();         
                                string strBudgetType = ds.Tables[0].Rows[0]["payment_detail_budget_type"].ToString();
                                if (!oPayment.SP_PAYMENT_DETAIL_UPD(hdfpayment_doc.Value, pitem_code, "0", txtmoney_credit.Value.ToString(), ppayment_item_tax,
                                                                                            ppayment_item_sos, pcomments_sub, strActive, strUpdatedBy,strBudgetType,strpayment_detail_id, ref strMessage))
                                {
                                    lblError.Text = strMessage;
                                }
                            }
                            else
                            {
                                if (!oPayment.SP_PAYMENT_DETAIL_INS(hdfpayment_doc.Value, pitem_code, "0", txtmoney_credit.Value.ToString(), ppayment_item_tax,
                                                                                            ppayment_item_sos, pcomments_sub, strActive, strCreatedBy, ref strMessage))
                                {
                                    lblError.Text = strMessage;
                                }
                            }
                        }
                    }
                }
                blnResult = true;
                MsgBox("บันทึกข้อมูลสมบูรณ์");
            }
            catch (Exception ex)
            {
                blnResult = false;
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oPayment.Dispose();
            }
            return blnResult;
        }
  
        private void BindGridView()
        {
            lnkExcelFile.NavigateUrl = string.Empty;
            imgExcel.Src = "~/images/icon_exceldisable.gif";
            cItem_person_group oItem_person_group = new cItem_person_group();
            DataSet ds = new DataSet();
            DataSet ds2 = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            //int i;
            string strYear = cboYear.SelectedValue;
            string strpay_month = cboPay_Month.SelectedValue;
            string strpay_year = cboPay_Year.SelectedValue;
            string strPerson_group = cboPerson_group.SelectedValue;
            string strDirector = cboDirector.SelectedValue;
            string strUnit = cboUnit.SelectedValue;
            string strPay_Month = cboPay_Month.SelectedValue;
            string strPay_Year = cboPay_Year.SelectedValue;
            cExcelReader oExcelReader = new cExcelReader();
            DataTable odtExcelAll = new DataTable();

            try
            {
                strCriteria = " and payment_year='" + strYear + "' " +
                              " and pay_month='" + strpay_month + "' " +
                              " and pay_year='" + strpay_year + "' ";
                if (!strPerson_group.Equals(""))
                {
                    strCriteria = strCriteria + "  and  person_group_code= '" + strPerson_group + "' ";
                }

                if (!strDirector.Equals(""))
                {
                    strCriteria = strCriteria + "  and  director_code= '" + strDirector + "' ";
                }

                if (!strUnit.Equals(""))
                {
                    strCriteria = strCriteria + "  and  unit_code= '" + strUnit + "' ";
                }

                strCriteria += " and person_group_code IN (" + PersonGroupList + ") ";

                if (DirectorLock == "Y")
                {
                    strCriteria += " and substring(director_code,4,2) = substring('" + DirectorCode + "',4,2) ";
                }

                if (!oItem_person_group.SP_ITEM_PERSON_GROUP_CAL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    DataTable dt = new DataTable();
                    //DataTable dt2 = new DataTable();

                    dt = ds.Tables[0];

                    //strCriteria = " And item_action = 'Y' ";
                    //if (!oItem_person_group.SP_ITEM_PERSON_GROUP_SEL(strCriteria, ref ds2, ref strMessage))
                    //{
                    //    lblError.Text = strMessage;
                    //}
                    //dt2 = ds2.Tables[0];

                    string strFilename = "~/temp/excel_" + base.UserLoginName + "_" + DateTime.Now.ToString("yyyyMMddHH-mm-ss") + ".xls";
                    System.IO.File.Copy(Server.MapPath("~/excel_template/import_item_person_group.xls"), Server.MapPath(strFilename));
                    InitExcel(ref oExcelReader, Server.MapPath(strFilename));
                    oExcelReader.SheetName = "data";
                    //odtExcelAll = oExcelReader.GetTable("data", " And 1=2");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string strCmd = "insert into [" + oExcelReader.SheetName + "$]  (";
                        string strCmd_values = " values (";
                        for (int j = 0; j < 70; j++)
                        {
                            if (j == 0)
                            {
                                strCmd += "A00";
                                strCmd_values += "'" + Helper.CStr(dt.Rows[i]["A00"]) + "' ";
                            }
                            else
                            {
                                if (j < 10)
                                {
                                    strCmd += "," + "A0" + j.ToString() ;
                                    strCmd_values += ",'" + Helper.CStr(dt.Rows[i]["A0" + j.ToString()]) + "' ";
                                }
                                else
                                {
                                    strCmd += "," + "A" + j.ToString() ;
                                    strCmd_values += ",'" + Helper.CStr(dt.Rows[i]["A" + j.ToString()]) + "' ";
                                }
                            }
                        }
                        strCmd += ")" + strCmd_values + ")";
                        oExcelReader.InsertTable(strCmd);
                    }
                    lnkExcelFile.NavigateUrl = strFilename;
                    imgExcel.Src = "~/images/icon_excel.gif";
                    MsgBox("สร้างแบบฟร์อมสมบูรณ์ พบข้อมูลทั้งสิ้น " + dt.Rows.Count + " รายการ คลิกที่รูปเพื่อดาวห์โหลด");
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oItem_person_group.Dispose();
                ds.Dispose();
                oExcelReader.Dispose();
                odtExcelAll.Dispose();
            }
        }

        private void UpdatePersonCode()
        {
            lnkExcelFile.NavigateUrl = string.Empty;
            imgExcel.Src = "~/images/icon_exceldisable.gif";
            cItem_person_group oItem_person_group = new cItem_person_group();
            cPerson oPerson = new cPerson();
            DataSet ds = new DataSet();
            DataSet ds2 = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            //int i;
            string strYear = cboYear.SelectedValue;
            string strpay_month = cboPay_Month.SelectedValue;
            string strpay_year = cboPay_Year.SelectedValue;
            string strPerson_group = cboPerson_group.SelectedValue;
            string strDirector = cboDirector.SelectedValue;
            string strUnit = cboUnit.SelectedValue;
            string strPay_Month = cboPay_Month.SelectedValue;
            string strPay_Year = cboPay_Year.SelectedValue;
            cExcelReader oExcelReader = new cExcelReader();
            DataTable odtExcelAll = new DataTable();
            try
            {
                if (!strPerson_group.Equals(""))
                {
                    strCriteria = strCriteria + "  and  person_group_code= '" + strPerson_group + "' ";
                }

                if (!strDirector.Equals(""))
                {
                    strCriteria = strCriteria + "  and  director_code= '" + strDirector + "' ";
                }

                if (!strUnit.Equals(""))
                {
                    strCriteria = strCriteria + "  and  unit_code= '" + strUnit + "' ";
                }

                strCriteria += " and person_group_code IN (" + PersonGroupList + ") ";

                if (DirectorLock == "Y")
                {
                    strCriteria += " and substring(director_code,4,2) = substring('" + DirectorCode + "',4,2) ";
                }

                if (!oPerson.SP_PERSON_LIST_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];
                    //string strFilename = "~/temp/excel_" + base.UserLoginName + "_" + DateTime.Now.ToString("yyyyMMddHH-mm-ss") + ".xls";
                    string strFilename = "~/excel_template/import_item_person_group.xls";
                    //System.IO.File.Copy(Server.MapPath("~/excel_template/import_item_person_group.xls"), Server.MapPath(strFilename));
                    InitExcel(ref oExcelReader, Server.MapPath(strFilename));
                    oExcelReader.SheetName = "data";
                    //odtExcelAll = oExcelReader.GetTable("data", " And 1=2");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string strCmd = "update [" + oExcelReader.SheetName + "$]  set ";
                        strCmd += " A00='" + dt.Rows[i]["person_code"].ToString() + "' ";
                        strCmd += " Where A02 Like '%" + dt.Rows[i]["person_thai_name"].ToString() + " " + 
                                                         dt.Rows[i]["person_thai_surname"].ToString() + "%' ";
                        oExcelReader.InsertTable(strCmd);
                    }
                    lnkExcelFile.NavigateUrl = strFilename;
                    imgExcel.Src = "~/images/icon_excel.gif";
                    MsgBox("แก้ไขสมบูรณ์ คลิกที่รูปเพื่อดาวห์โหลด");
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oItem_person_group.Dispose();
                ds.Dispose();
                oExcelReader.Dispose();
                odtExcelAll.Dispose();
            }
        }

        private void InitExcel(ref cExcelReader exr, string pPath)
        {
            exr.ExcelFilename = pPath;
            exr.Headers = true;
            exr.MixedData = true;
            exr.KeepConnectionOpen = true;
            //exr.Open()
        }

        private void BindGridView4(int nPageNo)
        {
            cItem_person_group oItem_person_group = new cItem_person_group();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            int i;
            string strYear = cboYear2.SelectedValue;
            string strpay_month = cboPay_Month2.SelectedValue;
            string strpay_year = cboPay_Year2.SelectedValue;
            try
            {
                strCriteria = " order by A00 ";
                if (!oItem_person_group.SP_ITEM_PERSON_GROUP_TMP_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    ds.Tables[0].DefaultView.Sort = ViewState["sort"] + " " + ViewState["direction"];
                    ds.Tables[0].Columns.Add("item_has");
                    for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        ds.Tables[0].Rows[i]["item_has"] = "N";
                    }
                    ds.Tables[0].AcceptChanges();
                    
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




                    //imgSaveOnly.Visible = false;
                    imgCancel.Visible = true;
                    GridView1.Visible = false;

                    if (GridView1.Rows.Count > 0)
                    {
                        RadioButtonList1.Enabled = false;
                        cboPay_Month2.Enabled = false;
                        cboPay_Year2.Enabled = false;


                        //imgSaveOnly2.Visible = true;
                        imgImport.Visible = false;
                        imgCancel2.Visible = true;

                        GridView1.Visible = true;

                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oItem_person_group.Dispose();
                ds.Dispose();
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.Header))
            {
                //Find the checkbox control in header and add an attribute
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
            BindGridView4(e.NewPageIndex);
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
                BindGridView4(int.Parse(txtPage.Text) - 1);
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

        protected void imgSaveOnly_Click(object sender, ImageClickEventArgs e)
        {
            if (RadioButtonList1.SelectedValue.Equals("A"))
            {
                UpdatePersonCode();
            }
            //else if (RadioButtonList1.SelectedValue.Equals("E"))
            //{

            //    int intX = 0;
            //    for (int intCount = 0; intCount <= (GridView2.Rows.Count - 1); intCount++)
            //    {
            //        GridViewRow row = GridView2.Rows[intCount];
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
            //        saveData2();
            //       // BindGridViewEdit();
            //    }
            //}
        }

        protected void ClearData()
        {
            InitcboPerson_group();
            cboPerson_group.SelectedIndex = 0;
            InitcboDirector();
            cboDirector.SelectedIndex = 0;
            InitcboUnit();
            cboUnit.SelectedIndex = 0;


            //imgSaveOnly.Visible = false;
            imgCancel.Visible = true;
            RadioButtonList1.SelectedValue = "A";

            RadioButtonList1.Enabled = true;
            cboPerson_group.Enabled = true;
            cboDirector.Enabled = true;
            cboUnit.Enabled = true;
            //cboYear.Enabled = true;
            //cboPay_Month.Enabled = true;
            //cboPay_Year.Enabled = true;


            panelSeek.Visible = true;
            panelSeek2.Visible = false;

            imgImport.Visible = true;
           // imgSaveOnly2.Visible = false;

            GridView1.Visible = false;
            imgExcel.Visible = true;
            //imgFind.Visible = false;
            imgSaveOnly.Visible = true;

            lnkExcelFile.NavigateUrl = string.Empty;
            imgExcel.Src = "~/images/icon_exceldisable.gif";

        }

        protected void imgCancel_Click(object sender, ImageClickEventArgs e)
        {
            ClearData();
        }

        protected void cboDirector_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboUnit();
        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboDirector();
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lnkExcelFile.NavigateUrl = string.Empty;
            imgExcel.Src = "~/images/icon_exceldisable.gif";
            GridView1.Visible = false;
           // BindGridViewTmp();
            if (RadioButtonList1.SelectedValue.Equals("I"))
            {
                panelSeek.Visible = false;
                panelSeek2.Visible = true;
                //imgSaveOnly2.Visible = false;
                GridView1.Visible = false;
                imgExcel.Visible = true;
                //imgFind.Visible = false;
            }          
            else if (RadioButtonList1.SelectedValue.Equals("A"))
            {
                panelSeek2.Visible = false;
                panelSeek.Visible = true;
                GridView1.Visible = false;
                imgExcel.Visible = true;
                //imgFind.Visible = false;
                imgSaveOnly.Visible = true;
            }
        }

        protected void imgImport_Click(object sender, ImageClickEventArgs e)
        {
            UpdatePanel oUpdatePanel;
            string strScript = string.Empty;
            strScript = "OpenPopUp('500px','200px','80%','อัพโหลด Excel File' ,'../payment_adj/excel_upload_item_person_group.aspx?" +
                                                    "&ctrl1=" + hddGUID.ClientID +
                                                    "&payment_year=" + cboYear2.SelectedValue +
                                                    "&pay_month=" + cboPay_Month2.SelectedValue +
                                                    "&pay_year=" + cboPay_Year2.SelectedValue + "&show=1', '1');";
            oUpdatePanel = (UpdatePanel)this.Master.FindControl("updatePanel1");
            ScriptManager.RegisterClientScriptBlock(oUpdatePanel, oUpdatePanel.GetType(), "MessageBox", strScript, true);
        }

        protected void LinkButton1_Click1(object sender, EventArgs e)
        {
            BindGridView4(0);
        }

        private void cboPerPage_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            GridView1.PageSize = int.Parse(strRecordPerPage);
            if (int.Parse(strPageNo) != 0)
            {
                BindGridView4(int.Parse(strPageNo) - 1);
            }
            else
            {
                BindGridView4(0);
            }
        }

        private void imgGo_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            BindGridView4(int.Parse(strPageNo) - 1);
        }



    }
}