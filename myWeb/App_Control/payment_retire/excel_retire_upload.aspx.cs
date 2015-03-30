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

namespace myWeb.App_Control.excel_retire
{
    public partial class excel_retire_upload : PageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                imgSaveOnly.Attributes.Add("onMouseOver", "src='../../images/controls/Upload2.jpg'");
                imgSaveOnly.Attributes.Add("onMouseOut", "src='../../images/controls/Upload.jpg'");
                if (Request.QueryString["payment_year"] != null)
                {
                    ViewState["payment_year"] = Request.QueryString["payment_year"].ToString();
                }
                if (Request.QueryString["pay_month"] != null)
                {
                    ViewState["pay_month"] = Request.QueryString["pay_month"].ToString();
                }
                if (Request.QueryString["pay_year"] != null)
                {
                    ViewState["pay_year"] = Request.QueryString["pay_year"].ToString();
                }
            }
        }

        protected void imgSaveOnly_Click(object sender, ImageClickEventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string strFilename = "~/excel_import/excel_" + base.UserLoginName + "_" + DateTime.Now.ToString("yyyyMMddHH-mm-ss") + ".xls";
                FileUpload1.SaveAs(MapPath(strFilename));
                if (SaveData(strFilename))
                {
                    string strScript1 = "alert('นำเข้าข้อมูลสมบูรณ์');ClosePopUp('1');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
                }
            }
        }

        private bool SaveData(string strFilename)
        {
            bool boolResult = false;
            cExcelReader oExcelReader = new cExcelReader();
            DataTable odtExcelAll = new DataTable();
            cCommon objCommon = new cCommon();
            try
            {
                InitExcel(ref oExcelReader, Server.MapPath(strFilename));
                oExcelReader.SheetName = "data";
                odtExcelAll = oExcelReader.GetTable("data", " and F1 > 0");
                string strpayment_year;
                string strpay_month;
                string strppay_year;
                string strperson_name;
                string strperson_code;

                string strpr_doc;
                string stritem_amt;
                string strc_created_by;
                string strMassege = string.Empty;
                string strSQL = string.Empty;
                string strSQLColumn = string.Empty;
                string strSQLValue = string.Empty;

                strc_created_by = Session["username"].ToString();
                strpayment_year = ViewState["payment_year"].ToString();
                strpay_month = ViewState["pay_month"].ToString();
                strppay_year = ViewState["pay_year"].ToString();

                strSQL = "delete payment_retire_detail where pr_doc in " +
                         "(select pr_doc from payment_retire_head where [pay_month]='" + strpay_month + "' and [pay_year]='" + strppay_year + "' ";
                objCommon.EXE_SQL(strSQL, ref strMassege);

                strSQL = "delete payment_retire_head where [pay_month]='" + strpay_month + "' and [pay_year]='" + strppay_year + "'";
                objCommon.EXE_SQL(strSQL, ref strMassege);

                for (int j = 4; j < odtExcelAll.Columns.Count; j++)
                {
                    if (odtExcelAll.Columns[j].ColumnName.Length > 3)
                    {
                        strSQLColumn += odtExcelAll.Columns[j].ColumnName + ",";
                    }
                }
                if (strSQLColumn.Length > 0) strSQLColumn = strSQLColumn.Substring(0, strSQLColumn.Length - 1);
                for (int i = 0; i < odtExcelAll.Rows.Count; i++)
                {

                    strperson_code = Helper.CStr(getPersonID(odtExcelAll.Rows[i][1].ToString()));
                    strperson_name = Helper.CStr(odtExcelAll.Rows[i][2]);
                    if (!string.IsNullOrEmpty(strperson_name))
                    {

                        strSQL = "insert into payment_retire_head ([pr_date],[pr_year],[pay_month],[pay_year],[pr_person_code] , [pr_person_name] " +
                                " ,[c_created_by],[d_created_date],[c_updated_by],[d_updated_date]) ";
                        strSQL += "values (getdate(),'" + strpayment_year + "','" + strpay_month + "','" + strppay_year + "','" + strperson_code + "','" + strperson_name + "' " +
                                " ,'" + base.UserLoginName + "',getdate(),'" + base.UserLoginName + "',getdate()) ";

                        objCommon.EXE_SQL(strSQL, ref strMassege);
                        DataSet ds = new DataSet();
                        objCommon.SEL_SQL("Select max(pr_doc) max_pr_doc from payment_retire_head", ref ds, ref strMassege);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            strpr_doc = Helper.CStr(ds.Tables[0].Rows[0]["max_pr_doc"]);
                            for (int j = 4; j < odtExcelAll.Columns.Count; j++)
                            {
                                if (odtExcelAll.Columns[j].ColumnName.Length > 3)
                                {
                                    stritem_amt = Helper.CStr(Helper.CDbl(odtExcelAll.Rows[i][j]));
                                    if (Helper.CDbl(stritem_amt) > 0)
                                    {
                                        strSQL = "insert into payment_retire_detail ([pr_doc],[item_code],[pr_item_money]" +
                                        ",[c_created_by],[d_created_date],[c_updated_by],[d_updated_date]) " +
                                        "values ('" + strpr_doc + "','" + odtExcelAll.Columns[j].ColumnName + "','" +
                                        stritem_amt + "','" + base.UserLoginName + "',getdate(),'" +
                                        base.UserLoginName + "',getdate())";
                                        objCommon.EXE_SQL(strSQL, ref strMassege);
                                    }
                                }

                            }
                        }
                        ds.Dispose();
                    }
                }
                boolResult = true;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
            finally
            {
                oExcelReader.Dispose();
                odtExcelAll.Dispose();
            }
            return boolResult;
        }

        public object getPersonID(string str)
        {
            DataSet ods = new DataSet();
            string strError = string.Empty;
            string strReturn = string.Empty;
            cPerson_retire oPerson_retire = new cPerson_retire();
            try
            {
                oPerson_retire.SP_PERSON_RETIRE_SEL(" And  person_id='" + str.ToString().Trim() + "'", ref ods, ref strError);
                if (ods.Tables[0].Rows.Count > 0)
                {
                    strReturn = ods.Tables[0].Rows[0]["pr_person_code"].ToString();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "ไม่สามารถจัดการข้อมูล เนื่องจาก " + ex.Message;
            }
            return strReturn;
        }


        private void InitExcel(ref cExcelReader exr, string pPath)
        {
            exr.ExcelFilename = pPath;
            exr.Headers = true;
            exr.MixedData = false;
            exr.KeepConnectionOpen = true;
            //exr.Open()
        }

    }
}
