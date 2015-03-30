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

namespace myWeb.App_Control.payment_adj
{
    public partial class payment_export : PageBase
    {
        private string strPrefixCtr = "ctl00$ASPxRoundPanel1$ASPxRoundPanel2$ContentPlaceHolder1$";

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
                imgExport.Attributes.Add("onMouseOver", "src='../../images/button/export.jpg'");
                imgExport.Attributes.Add("onMouseOut", "src='../../images/button/export.jpg'");

                imgCancel.Attributes.Add("onMouseOver", "src='../../images/button/cancel2.png'");
                imgCancel.Attributes.Add("onMouseOut", "src='../../images/button/cancel.png'");

                string strYear = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["yearnow"].ToString();

                imgList_item.Attributes.Add("onclick", "OpenPopUp('750px','400px','93%','ค้นหาข้อมูลค่าใช้จ่าย-เบิกตรง' ,'../lov/direct_pay_item_lov.aspx?year='+document.forms[0]." + strPrefixCtr +
                              "cboYear.options[document.forms[0]." + strPrefixCtr + "cboYear.selectedIndex].value+" +
                              "'&item_description='+document.forms[0]." + strPrefixCtr + "txtdirect_pay_code_list.value+" +
                              "'&ctrl1=" + txtdirect_pay_code_list.ClientID + "&item_type=C&from=member', '1');return false;");

                imgClear_item.Attributes.Add("onclick", "document.forms[0]." + strPrefixCtr + "txtdirect_pay_code_list.value=''; return false;");

                panelSeek2.Visible = false;
                panelSeek.Visible = true;

                ViewState["sort"] = "person_code";
                ViewState["direction"] = "ASC";
                InitcboRound();
                InitcboPerson_group();
                InitcboDirector();
                InitcboUnit();

                panelSeek2.Visible = false;
                panelSeek.Visible = true;
                imgTxt.Visible = true;
                RequiredFieldValidator3.Enabled = false;

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
            txtdirect_pay_code_list.Text = string.Empty;

            RadioButtonList1.Enabled = true;
            cboPerson_group.Enabled = true;
            cboDirector.Enabled = true;
            cboUnit.Enabled = true;
            //cboYear.Enabled = true;
            //cboPay_Month.Enabled = true;
            //cboPay_Year.Enabled = true;
            txtdirect_pay_code_list.Enabled = true;
            imgList_item.Enabled = true;
            imgClear_item.Enabled = true;



            panelSeek.Visible = true;
            panelSeek2.Visible = false;

            imgTxt.Visible = true;
            RequiredFieldValidator3.Enabled = false;

            lnkTxtFile.NavigateUrl = string.Empty;
            imgTxt.Src = "~/images/icon_txtdisable.gif";

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
            lnkTxtFile.NavigateUrl = string.Empty;
            imgTxt.Src = "~/images/icon_txtdisable.gif";
            if (RadioButtonList1.SelectedValue.Equals("I"))
            {
                panelSeek.Visible = false;
                panelSeek2.Visible = true;
                imgTxt.Visible = true;
                RequiredFieldValidator3.Enabled = false;
            }

        }


        protected void imgExport_Click(object sender, ImageClickEventArgs e)
        {
            BindGridView();
        }

        private void BindGridView()
        {
            lnkTxtFile.NavigateUrl = string.Empty;
            imgTxt.Src = "~/images/icon_txtdisable.gif";
            cPayment oPayment = new cPayment();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string strCodeList = string.Empty;
            //int i;
            string strYear = cboYear.SelectedValue;
            string strpay_month = cboPay_Month.SelectedValue;
            string strpay_year = cboPay_Year.SelectedValue;
            string strPerson_group = cboPerson_group.SelectedValue;
            string strDirector = cboDirector.SelectedValue;
            string strUnit = cboUnit.SelectedValue;

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

                strCodeList = txtdirect_pay_code_list.Text.Trim();

                if (!oPayment.SP_EXPORT_DIRECT_PAY_ITEM_SEL(strCriteria, strCodeList, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    DataTable dt = ds.Tables[0];
                    const string StrFilename = "~/temp/LOAN.TXT";
                    var file = new System.IO.StreamWriter(Server.MapPath(StrFilename));
                    string lines;
                    foreach (DataRow dr in dt.Rows)
                    {
                        lines = Helper.CStr(dr["person_id"]) + ",";
                        lines += Helper.CStr(dr["title_name"]) + "" + Helper.CStr(dr["person_thai_name"]) + " "
                                 + Helper.CStr(dr["person_thai_surname"]) + ",";
                        //if (Helper.CStr(dr["loan_code_01"]).Length > 0 && Helper.CDbl(dr["loan_money_01"]) > 0)
                        {
                            lines += (Helper.CDbl(dr["loan_money_01"], 0) > 0 ? Helper.CStr(dr["loan_code_01"]) : "0") + ",";
                            lines += Helper.CDbl(dr["loan_money_01"], 0) + ",";
                        }
                        //if (Helper.CStr(dr["loan_code_02"]).Length > 0 && Helper.CDbl(dr["loan_money_02"]) > 0)
                        {
                            lines += (Helper.CDbl(dr["loan_money_02"], 0) > 0 ? Helper.CStr(dr["loan_code_02"]) : "0") + ",";
                            lines += Helper.CDbl(dr["loan_money_02"], 0) + ",";
                        }
                        //if (Helper.CStr(dr["loan_code_03"]).Length > 0 && Helper.CDbl(dr["loan_money_03"]) > 0)
                        {

                            lines += (Helper.CDbl(dr["loan_money_03"], 0) > 0 ? Helper.CStr(dr["loan_code_03"]) : "0") + ",";
                            lines += Helper.CDbl(dr["loan_money_03"], 0) + ",";

                        }
                        //if (Helper.CStr(dr["loan_code_04"]).Length > 0 && Helper.CDbl(dr["loan_money_04"]) > 0)
                        {

                            lines += (Helper.CDbl(dr["loan_money_04"], 0) > 0 ? Helper.CStr(dr["loan_code_04"]) : "0") + ",";
                            lines += Helper.CDbl(dr["loan_money_04"], 0) + ",";
                        }
                        //if (Helper.CStr(dr["loan_code_05"]).Length > 0 && Helper.CDbl(dr["loan_money_05"]) > 0)
                        {

                            lines += (Helper.CDbl(dr["loan_money_05"], 0) > 0 ? Helper.CStr(dr["loan_code_05"]) : "0") + ",";
                            lines += Helper.CDbl(dr["loan_money_05"], 0) + ",";
                        }
                        //if (Helper.CStr(dr["loan_code_06"]).Length > 0 && Helper.CDbl(dr["loan_money_06"]) > 0)
                        {

                            lines += (Helper.CDbl(dr["loan_money_06"], 0) > 0 ? Helper.CStr(dr["loan_code_06"]) : "0") + ",";
                            lines += Helper.CDbl(dr["loan_money_06"], 0) + ",";


                        }
                        //if (Helper.CStr(dr["loan_code_07"]).Length > 0 && Helper.CDbl(dr["loan_money_07"]) > 0)
                        {
                            lines += (Helper.CDbl(dr["loan_money_07"], 0) > 0 ? Helper.CStr(dr["loan_code_07"]) : "0") + ",";
                            lines += Helper.CDbl(dr["loan_money_07"], 0) + ",";
                        }
                        //if (Helper.CStr(dr["loan_code_08"]).Length > 0 && Helper.CDbl(dr["loan_money_08"]) > 0)
                        {

                            lines += (Helper.CDbl(dr["loan_money_08"], 0) > 0 ? Helper.CStr(dr["loan_code_08"]) : "0") + ",";
                            lines += Helper.CDbl(dr["loan_money_08"], 0) + ",";
                        }
                        //if (Helper.CStr(dr["loan_code_09"]).Length > 0 && Helper.CDbl(dr["loan_money_09"]) > 0)
                        {

                            lines += (Helper.CDbl(dr["loan_money_09"], 0) > 0 ? Helper.CStr(dr["loan_code_09"]) : "0") + ",";
                            lines += Helper.CDbl(dr["loan_money_09"], 0) + ",";
                        }
                        //if (Helper.CStr(dr["loan_code_10"]).Length > 0 && Helper.CDbl(dr["loan_money_10"]) > 0)
                        {

                            lines += (Helper.CDbl(dr["loan_money_10"], 0) > 0 ? Helper.CStr(dr["loan_code_10"]) : "0") + ",";
                            lines += Helper.CDbl(dr["loan_money_10"], 0);

                        }
                        file.WriteLine(lines);
                    }
                    file.Close();
                    lnkTxtFile.NavigateUrl = StrFilename;
                    imgTxt.Src = "~/images/icon_txt.gif";
                    MsgBox("ส่งออกข้อมูลสมบูรณ์ พบข้อมูลทั้งสิ้น " + dt.Rows.Count + " รายการ คลิกที่รูปเพื่อดาวห์โหลด");
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



    }
}