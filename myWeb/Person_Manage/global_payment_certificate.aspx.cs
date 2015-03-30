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
    public partial class global_payment_certificate : GlobalPageBase
    {
        #region private data
        private string strConn = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"];
        private bool[] blnAccessRight = new bool[5] { false, false, false, false, false };
        private string strPrefixCtr_main = "ctl00$ContentPlaceHolder1$TabContainer1$";
        #endregion

        protected void Page_Load(object sender, System.EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(global_payment_certificate));
            Session["menupopup_name"] = this.Page;
            lblError.Text = "";
            if (!IsPostBack)
            {
                setData();
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
                strtype_position_name = string.Empty,
                strperson_salaly = string.Empty;
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
                        strperson_salaly = ds.Tables[0].Rows[0]["person_salaly"].ToString();
                        #endregion

                        #region set Control
                        lblPerson_code.Text = strperson_code;
                        lblTitleName.Text = strtitle_name;
                        lblPerson_thai_name.Text = strperson_thai_name;
                        lblPerson_thai_surname.Text = strperson_thai_surname;
                        lblPosition_name.Text = strposition_name;

                        lblType_position_name.Text = strtype_position_name;
                        lblPerson_salaly.Text = String.Format("{0:0.00}", float.Parse(strperson_salaly));

                        string strSal1 = "0.00";
                        string strSal2 = "0.00";

                        string strSQL = string.Empty;
                        strSQL = "Select Sum(item_debit) as item_debit_sum from [view_person_item] where person_code ='" + base.PersonCode + "' and [item_name] like '%ปจต%' ";
                        ds = null;
                        oCommon.SEL_SQL(strSQL, ref ds, ref strMessage);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            strSal1 = Helper.CStr(ds.Tables[0].Rows[0]["item_debit_sum"], "0.00");
                        }
                        lblPerson_position.Text = String.Format("{0:0.00}", float.Parse(strSal1));
                        strSQL = "Select Sum(item_debit) as item_debit_sum from [view_person_item] where person_code ='" + base.PersonCode + "' and [item_name] like '%ตอบแทน%' ";
                        ds = null;
                        oCommon.SEL_SQL(strSQL, ref ds, ref strMessage);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            strSal2 = Helper.CStr(ds.Tables[0].Rows[0]["item_debit_sum"], "0.00");
                        }
                        lblPerson_reward.Text = String.Format("{0:0.00}", float.Parse(strSal2));
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

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
                strCriteria = strCriteria + "  And  (person_code='" + strPerson_code + "') ";
            }

            Session["criteria"] = strCriteria;

            cPerson cPerson = new cPerson();
            DataSet ds = new DataSet();
            if (!cPerson.SP_PERSON_ALL_SEL(strCriteria, ref ds, ref strMessage))
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

                            report_title = dsCenter.Tables[0].Rows[0]["FACT_NAME"].ToString() + " " + dsCenter.Tables[0].Rows[0]["DIVISION_NAME"].ToString() + " " + dsCenter.Tables[0].Rows[0]["SUB_DI_NAME"].ToString() +" " + strTelNo;
                        }
                    }

                    strReport_code = "Rep_payment_req_certificate";

                    strScript = "window.open(\"global_payment_report.aspx?report_code=" + strReport_code + "&report_title=" + report_title +
                                                     "&payment_position=" + lblPerson_position.Text + "&payment_reward=" + lblPerson_reward.Text + "&payment_type=" + cboTypeLoan.SelectedItem.Text + "\", \"_blank\");\n";
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

    }
}