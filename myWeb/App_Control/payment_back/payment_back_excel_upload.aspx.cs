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

namespace myWeb.App_Control.payment_back
{
    public partial class payment_back_excel_upload : PageBase
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
                if (Request.QueryString["year"] != null)
                {
                    ViewState["year"] = Request.QueryString["year"].ToString();
                }
                if (Request.QueryString["pay_year"] != null)
                {
                    ViewState["pay_year"] = Request.QueryString["pay_year"].ToString();
                }
                if (Request.QueryString["pay_month"] != null)
                {
                    ViewState["pay_month"] = Request.QueryString["pay_month"].ToString();
                }
            }
        }

        protected void imgSaveOnly_Click(object sender, ImageClickEventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string strFilename = "~/excel_import/excel_payment_back_" + DateTime.Now.ToString("yyyyMMddHH-mm-ss") + ".xls";
                FileUpload1.SaveAs(MapPath(strFilename));
                if (SaveData(strFilename))
                {
                    string strScript1 = "window.parent.document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + UserGUID + "';" +
                                                       "window.parent.__doPostBack('ctl00$ASPxRoundPanel1$ASPxRoundPanel2$ContentPlaceHolder1$LinkButton1','');ClosePopUp('1');";
                    //string strScript1 = "ClosePopUp('1');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
                }
            }
        }

        private bool SaveData(string strFilename)
        {
            bool boolResult = false;
            var oExcelReader = new cExcelReader();
            var odtExcelAll = new DataTable();
            var oPayment_back = new cPayment_back();
            try
            {
                InitExcel(ref oExcelReader, Server.MapPath(strFilename));
                oExcelReader.SheetName = "data";
                odtExcelAll = oExcelReader.GetTable("data", "");
                
                string ppayment_year;
                string ppay_year;
                string ppay_month;
                string pbk_person_code ;
                string pbk_person_name;
                string pbk_person_surname;
                string pitem_code ;
                string ppayment_item_old;
                string ppayment_item_new ;
                string ppayment_item_diff ;
                string ppayment_item_back;

                string str_person_code = System.Configuration.ConfigurationSettings.AppSettings["back:person_code"];
                string str_person_name = System.Configuration.ConfigurationSettings.AppSettings["back:person_name"];
                string str_person_surname = System.Configuration.ConfigurationSettings.AppSettings["back:person_surname"];
                string str_payment_item_old = System.Configuration.ConfigurationSettings.AppSettings["back:item_old"];
                string str_payment_item_new = System.Configuration.ConfigurationSettings.AppSettings["back:item_new"];
                string str_payment_item_diff = System.Configuration.ConfigurationSettings.AppSettings["back:item_diff"];
                string str_payment_item_back = System.Configuration.ConfigurationSettings.AppSettings["back:item_back"];
                
                string pc_created_by;
                string strMassege = string.Empty;
                pc_created_by = Session["username"].ToString();
                oPayment_back.SP_IMPORT_PAYMENT_BACK_DEL(pc_created_by, ref strMassege);
                for (int i = 0; i < odtExcelAll.Rows.Count; i++)
                {
                    ppayment_year = ViewState["year"].ToString();
                    ppay_year = ViewState["pay_year"].ToString();
                    ppay_month = ViewState["pay_month"].ToString();
                    pbk_person_code = Helper.CStr(odtExcelAll.Rows[i][int.Parse(str_person_code)]);
                    pbk_person_name = Helper.CStr(odtExcelAll.Rows[i][int.Parse(str_person_name)]);
                    pbk_person_surname = Helper.CStr(odtExcelAll.Rows[i][int.Parse(str_person_surname)]);
                    ppayment_item_old =  Helper.CStr(odtExcelAll.Rows[i][int.Parse(str_payment_item_old)]) ;
                    ppayment_item_new =  Helper.CStr(odtExcelAll.Rows[i][int.Parse(str_payment_item_new)]) ;
                    ppayment_item_diff = Helper.CStr(odtExcelAll.Rows[i][int.Parse(str_payment_item_diff)]);
                    ppayment_item_back = Helper.CStr(odtExcelAll.Rows[i][int.Parse(str_payment_item_back)]);
                    pitem_code = ViewState["item_code"].ToString();
                    if (!string.IsNullOrEmpty(pbk_person_code))
                    {
                        oPayment_back.SP_IMPORT_PAYMENT_BACK_INS(ppayment_year,ppay_year ,ppay_month,  pbk_person_code, pbk_person_name, pbk_person_surname, 
                            pitem_code, ppayment_item_old, ppayment_item_new , ppayment_item_diff,ppayment_item_back, "" , pc_created_by, ref strMassege);
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
                oPayment_back.Dispose();
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
