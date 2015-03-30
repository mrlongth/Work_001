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

namespace myWeb.App_Control.payment_adj
{
    public partial class excel_upload : PageBase
    {

        private string UserGUID
        {
            get
            {
                if (ViewState["UserGUID"] == null)
                {
                    ViewState["UserGUID"] = System.Guid.NewGuid().ToString();
                }
                return ViewState["UserGUID"].ToString();
            }
            set
            {
                ViewState["UserGUID"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                imgSaveOnly.Attributes.Add("onMouseOver", "src='../../images/controls/Upload2.jpg'");
                imgSaveOnly.Attributes.Add("onMouseOut", "src='../../images/controls/Upload.jpg'");
                if (Request.QueryString["ctrl1"] != null)
                {
                    ViewState["ctrl1"] = Request.QueryString["ctrl1"].ToString();
                }
                if (Request.QueryString["item_code"] != null)
                {
                    ViewState["item_code"] = Request.QueryString["item_code"].ToString();
                }
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
                string strFilename = "~/excel_import/excel_" + base.UserLoginName  + "_" + DateTime.Now.ToString("yyyyMMddHH-mm-ss") + ".xls";
                FileUpload1.SaveAs(MapPath(strFilename));
                if (SaveData(strFilename))
                {
                    string strScript1 = "window.parent.document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" +  UserGUID + "';" +
                                                       "window.parent.__doPostBack('ctl00$ASPxRoundPanel1$ASPxRoundPanel2$ContentPlaceHolder1$LinkButton1','');ClosePopUp('1');";
                    //string strScript1 = "ClosePopUp('1');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
                }
            }
        }

        private bool SaveData(string strFilename)
        {
            bool boolResult = false;
            cExcelReader oExcelReader = new cExcelReader();
            DataTable odtExcelAll = new DataTable();
            cPayment objPayment = new cPayment();
            try
            {
                InitExcel(ref oExcelReader, Server.MapPath(strFilename));
                oExcelReader.SheetName = "data";
                odtExcelAll = oExcelReader.GetTable("data", "");
                string strpayment_year;
                string strpay_month;
                string strppay_year;
                string strperson_code;
                string stritem_code;
                string stritem_amt;
                string strc_created_by;
                string strMassege = string.Empty;
                strc_created_by = Session["username"].ToString();
                objPayment.SP_PAYMENT_ITEM_IMPORT_DEL(UserGUID, strc_created_by, ref strMassege);
                for (int i = 0; i < odtExcelAll.Rows.Count; i++)
                {
                    strpayment_year = ViewState["payment_year"].ToString();
                    strpay_month = ViewState["pay_month"].ToString();
                    strppay_year = ViewState["pay_year"].ToString();
                    strperson_code = Helper.CStr(odtExcelAll.Rows[i][0]);
                    stritem_code = ViewState["item_code"].ToString();
                    stritem_amt = Helper.CStr(Helper.CDbl(odtExcelAll.Rows[i][3]));
                    if (!string.IsNullOrEmpty(strperson_code))
                    {
                        objPayment.SP_IMPORT_PAYMENT_ITEM_INS(strpayment_year, strpay_month, strppay_year, strperson_code, stritem_code, stritem_amt ,UserGUID, strc_created_by, ref strMassege);
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

        private void InitExcel(ref cExcelReader exr, string pPath)
        {
            exr.ExcelFilename = pPath;
            exr.Headers = true;
            exr.MixedData = true;
            exr.KeepConnectionOpen = true;
            //exr.Open()
        }

    }
}
