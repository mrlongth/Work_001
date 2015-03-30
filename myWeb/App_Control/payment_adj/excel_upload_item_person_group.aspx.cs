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
    public partial class excel_upload_item_person_group : PageBase
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
                    string strScript1 = "window.parent.document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + UserGUID + "';" +
                                                       "window.parent.__doPostBack('ctl00$ASPxRoundPanel1$ASPxRoundPanel2$ContentPlaceHolder1$LinkButton1','');ClosePopUp('1');";
                    //string strScript1 = "ClosePopUp('1');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
                }
            }
        }

        private bool SaveData(string strFilename)
        {
            string strMessage = string.Empty;
            bool boolResult = false;
            cExcelReader oExcelReader = new cExcelReader();
            DataTable odtExcelAll = new DataTable();
            cItem_person_group oItem_person_group = new cItem_person_group();
            try
            {
                InitExcel(ref oExcelReader, Server.MapPath(strFilename));
                oExcelReader.SheetName = "data";
                odtExcelAll = oExcelReader.GetTable("data", " and A00 <> '' ");
                oItem_person_group.SP_ITEM_PERSON_GROUP_SQL("Truncate table item_person_group_tmp", ref strMessage);
                string strc_created_by;
                string strMassege = string.Empty;
                strc_created_by = Session["username"].ToString();

                for (int i = 0; i < odtExcelAll.Rows.Count; i++)
                {
                    string strCmd = "insert into item_person_group_tmp  (";
                    string strCmd_values = " values (";
                    for (int j = 0; j < 70; j++)
                    {
                        if (j == 0)
                        {
                            strCmd += "A00";
                            strCmd_values += "'" + Helper.CStr(odtExcelAll.Rows[i]["A00"]) + "' ";
                        }
                        else
                        {
                            if (j < 10)
                            {
                                strCmd += "," + "A0" + j.ToString();
                                strCmd_values += ",'" + Helper.CStr(odtExcelAll.Rows[i]["A0" + j.ToString()]) + "' ";
                            }
                            else
                            {
                                strCmd += "," + "A" + j.ToString();
                                strCmd_values += ",'" + Helper.CStr(odtExcelAll.Rows[i]["A" + j.ToString()]) + "' ";
                            }
                        }
                    }
                    strCmd += ")" + strCmd_values + ")";
                    oItem_person_group.SP_ITEM_PERSON_GROUP_SQL(strCmd, ref strMessage);
                }
                //DataSet ds = new DataSet();
                //oItem_person_group.SP_ITEM_PERSON_GROUP_IMPORT(ViewState["pay_month"].ToString(), ViewState["pay_year"].ToString(), ref ds, ref strMessage);
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
                oItem_person_group.Dispose();
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
