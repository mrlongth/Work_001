using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using myDLL;
using System.IO;

namespace myEFrom.App_Control.reportsparameter
{

    public partial class open_report_show : PageBase
    {
        protected CrystalDecisions.CrystalReports.Engine.ReportDocument oRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
        protected CrystalDecisions.CrystalReports.Engine.Tables crTables;
        protected CrystalDecisions.Shared.TableLogOnInfo crTableLogOnInfo;
        protected CrystalDecisions.Shared.ConnectionInfo crConnectionInfo = new CrystalDecisions.Shared.ConnectionInfo();
        protected CrystalDecisions.Shared.ParameterValues crParameterValues;
        protected CrystalDecisions.Shared.ParameterDiscreteValue crParameterDiscreteValue;
        protected CrystalDecisions.CrystalReports.Engine.ParameterFieldDefinitions crParameterFieldDefinitions;
        protected CrystalDecisions.CrystalReports.Engine.ParameterFieldDefinition crParameterFieldDefinition;

        string strServername = System.Configuration.ConfigurationSettings.AppSettings["servername"];
        string strDbname = System.Configuration.ConfigurationSettings.AppSettings["dbname"];
        string strDbuser = System.Configuration.ConfigurationSettings.AppSettings["dbuser"];
        string strDbpassword = System.Configuration.ConfigurationSettings.AppSettings["dbpassword"];


        public string ReportDirectoryTemp
        {
            get
            {
                if (ViewState["ReportDirectoryTemp"] == null)
                {
                    try
                    {
                        ViewState["ReportDirectoryTemp"] = System.Configuration.ConfigurationManager.AppSettings["ReportDirectoryTemp"];

                    }
                    catch
                    {
                        ViewState["ReportDirectoryTemp"] = string.Empty;
                    }
                }
                return ViewState["ReportDirectoryTemp"].ToString();
            }
            set { ViewState["ReportDirectoryTemp"] = value; }
        }

        public short ReportAliveTime
        {
            get
            {
                if (ViewState["ReportAliveTime"] == null)
                {
                    try
                    {
                        ViewState["ReportAliveTime"] = System.Configuration.ConfigurationManager.AppSettings["ReportAliveTime"];
                    }
                    catch
                    {
                        ViewState["ReportAliveTime"] = string.Empty;
                    }
                }
                return short.Parse(ViewState["ReportAliveTime"].ToString(), System.Globalization.NumberStyles.Integer, null);
            }
            set { ViewState["ReportAliveTime"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th-TH");
            //lblError.Text = string.Empty;
            if (!IsPostBack)
            {
                getQueryString();
                printData();
                crConnectionInfo.DatabaseName = strDbname;
                crConnectionInfo.ServerName = strServername;
                crConnectionInfo.UserID = strDbuser;
                crConnectionInfo.Password = strDbpassword;
                crTables = oRpt.Database.Tables;

                //apply logon info
                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in oRpt.Database.Tables)
                {
                    crTableLogOnInfo = crTable.LogOnInfo;
                    crTableLogOnInfo.ConnectionInfo = crConnectionInfo;
                    crTable.ApplyLogOnInfo(crTableLogOnInfo);
                }

                //apply logon info for sub report
                foreach (Section crSection in oRpt.ReportDefinition.Sections)
                {
                    foreach (ReportObject crReportObject in crSection.ReportObjects)
                    {
                        if (crReportObject.Kind == ReportObjectKind.SubreportObject)
                        {
                            SubreportObject crSubReportObj = (SubreportObject)(crReportObject);

                            foreach (CrystalDecisions.CrystalReports.Engine.Table oTable in crSubReportObj.OpenSubreport(crSubReportObj.SubreportName).Database.Tables)
                            {
                                crTableLogOnInfo = oTable.LogOnInfo;
                                crTableLogOnInfo.ConnectionInfo = crConnectionInfo;
                                oTable.ApplyLogOnInfo(crTableLogOnInfo);
                            }

                        }

                    }
                }

                string strReportDirectoryTempPhysicalPath = Server.MapPath(this.ReportDirectoryTemp);
                Helper.DeleteUnusedFile(strReportDirectoryTempPhysicalPath, ReportAliveTime);

                string strFilename;
                strFilename = "report_" + DateTime.Now.ToString("yyyyMMddHH-mm-ss");
                var path =  "~/temp/" + strFilename + ".pdf" ;
                oRpt.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath(path));
                //Server.Transfer(path);
                Response.Redirect(path);

                //lnkPdfFile.NavigateUrl = "~/temp/" + strFilename + ".pdf";

                //imgPdf.Src = "~/images/icon_pdf.gif";
                //lnkExcelFile.Visible = false;
                //CrystalReportViewer1.ReportSource = oRpt;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th-TH");
            if (IsPostBack)
            {
                getQueryString();
                printData();
                crConnectionInfo.DatabaseName = strDbname;
                crConnectionInfo.ServerName = strServername;
                crConnectionInfo.UserID = strDbuser;
                crConnectionInfo.Password = strDbpassword;
                crTables = oRpt.Database.Tables;

                //apply logon info
                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in oRpt.Database.Tables)
                {
                    crTableLogOnInfo = crTable.LogOnInfo;
                    crTableLogOnInfo.ConnectionInfo = crConnectionInfo;
                    crTable.ApplyLogOnInfo(crTableLogOnInfo);
                }

                //apply logon info for sub report
                foreach (Section crSection in oRpt.ReportDefinition.Sections)
                {
                    foreach (ReportObject crReportObject in crSection.ReportObjects)
                    {
                        if (crReportObject.Kind == ReportObjectKind.SubreportObject)
                        {
                            SubreportObject crSubReportObj = (SubreportObject)(crReportObject);

                            foreach (CrystalDecisions.CrystalReports.Engine.Table oTable in crSubReportObj.OpenSubreport(crSubReportObj.SubreportName).Database.Tables)
                            {
                                crTableLogOnInfo = oTable.LogOnInfo;
                                crTableLogOnInfo.ConnectionInfo = crConnectionInfo;
                                oTable.ApplyLogOnInfo(crTableLogOnInfo);
                            }

                        }

                    }
                }

                string strReportDirectoryTempPhysicalPath = Server.MapPath(this.ReportDirectoryTemp);
                Helper.DeleteUnusedFile(strReportDirectoryTempPhysicalPath, ReportAliveTime);

                string strFilename;
                strFilename = "report_" + DateTime.Now.ToString("yyyyMMddHH-mm-ss");
                oRpt.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath("~/temp/") + strFilename + ".pdf");
                lnkPdfFile.NavigateUrl = "~/temp/" + strFilename + ".pdf";
                imgPdf.Src = "~/images/icon_pdf.gif";
                lnkExcelFile.Visible = false;
                if (ViewState["report_code"].ToString() == "Rep_exceldebitall")
                {
                    oRpt.ExportToDisk(ExportFormatType.ExcelRecord, Server.MapPath("~/temp/") + strFilename + ".xls");
                    lnkExcelFile.NavigateUrl = "~/temp/" + strFilename + ".xls";
                    imgExcel.Src = "~/images/icon_excel.gif";
                    lnkExcelFile.Visible = true;
                }
                CrystalReportViewer1.ReportSource = oRpt;
            }
        }

        private void printData()
        {
            if (ViewState["report_code"].ToString().Equals("Rep_open01"))
            {
                Retive_Rep_open01();
            }
            else if (ViewState["report_code"].ToString().Equals("Rep_loan01") ||
                ViewState["report_code"].ToString().Equals("Rep_loan02"))
            {
                Retive_Rep_loan01();
            }
            else if (ViewState["report_code"].ToString() == "Rep_loan_record")
            {
                Retive_Rep_loan_record();
            }
            else
            {
                ViewState["report_code"] = "Rep_open01";
                Retive_Rep_open01();
            }
        }

        private string getMonth()
        {
            string strMonth = string.Empty;
            if (ViewState["months"].ToString().Equals("01"))
            {
                strMonth = "มกราคม";
            }
            else if (ViewState["months"].ToString().Equals("02"))
            {
                strMonth = "กุมภาพันธ์";
            }
            else if (ViewState["months"].ToString().Equals("03"))
            {
                strMonth = "มีนาคม";
            }
            else if (ViewState["months"].ToString().Equals("04"))
            {
                strMonth = "เมษายน";
            }
            else if (ViewState["months"].ToString().Equals("05"))
            {
                strMonth = "พฤษภาคม";
            }
            else if (ViewState["months"].ToString().Equals("06"))
            {
                strMonth = "มิถุนายน";
            }
            else if (ViewState["months"].ToString().Equals("07"))
            {
                strMonth = "กรกฎาคม";
            }
            else if (ViewState["months"].ToString().Equals("08"))
            {
                strMonth = "สิงหาคม";
            }
            else if (ViewState["months"].ToString().Equals("09"))
            {
                strMonth = "กันยายน";
            }
            else if (ViewState["months"].ToString().Equals("10"))
            {
                strMonth = "ตุลาคม";
            }
            else if (ViewState["months"].ToString().Equals("11"))
            {
                strMonth = "พฤศจิกายน";
            }
            else if (ViewState["months"].ToString().Equals("12"))
            {
                strMonth = "ธันวาคม";
            }
            return strMonth;
        }

        private void getQueryString()
        {
            if (Request.QueryString["open_head_id"] != null)
            {
                ViewState["open_head_id"] = Request.QueryString["open_head_id"].ToString();
            }
            else
            {
                ViewState["open_head_id"] = "0";
            }

            if (Request.QueryString["loan_id"] != null)
            {
                ViewState["loan_id"] = Request.QueryString["loan_id"].ToString();
            }
            else
            {
                ViewState["loan_id"] = "0";
            }


            if (Request.QueryString["report_code"] != null)
            {
                ViewState["report_code"] = Request.QueryString["report_code"].ToString();
            }
            else
            {
                ViewState["report_code"] = "";
            }

            if (Request.QueryString["person_code"] != null)
            {
                ViewState["person_code"] = Request.QueryString["person_code"].ToString();
            }
            else
            {
                ViewState["person_code"] = "";
            }

            if (ViewState["report_code"].ToString() == "Rep_open01")
            {
                cefOpen objEfOpen = new cefOpen();
                string strCriteria2 = string.Empty;
                DataTable dt = new DataTable();
                strCriteria2 = " and open_head_id=" + ViewState["open_head_id"] + " ";
                ViewState["criteria"] = strCriteria2;
                dt = objEfOpen.SP_OPEN_HEAD_SEL(strCriteria2);
                if (dt.Rows.Count > 0)
                {
                    ViewState["report_code"] = dt.Rows[0]["open_report_code"].ToString();
                    ViewState["report_title"] = dt.Rows[0]["open_title"].ToString();

                    ViewState["open_to_desc"] = string.Empty;
                    ViewState["companyname"] = dt.Rows[0]["director_name"].ToString() + "  " + dt.Rows[0]["unit_name"].ToString();
                }
            }
            else if (ViewState["report_code"].ToString() == "Rep_loan01")
            {
                ViewState["report_title"] = "สัญญายืมเงิน";

                ViewState["open_to_desc"] = string.Empty;
                ViewState["companyname"] = "";
                ViewState["criteria"] = " and loan_id = " + ViewState["loan_id"];
                ViewState["criteria2"] = " and loan_id <> " + ViewState["loan_id"] + " and person_code = '" + ViewState["person_code"] + "' and loan_approve > loan_return and loan_status = 'A' ";
            }
            else if (ViewState["report_code"].ToString() == "Rep_loan02")
            {
                ViewState["report_title"] = "หนังสือชี้แจงสัญญายืมเงินคงค้าง";

                ViewState["open_to_desc"] = string.Empty;
                ViewState["companyname"] = "";
                ViewState["criteria"] = " and loan_id = " + ViewState["loan_id"];
                ViewState["criteria2"] = " and loan_id <> " + ViewState["loan_id"] + " and person_code = '" + ViewState["person_code"] + "' and loan_approve > loan_return and loan_status = 'A' ";
            }
           

        }

        private void Retive_Rep_open01()
        {
            try
            {
                string strPath = "~/reports/" + ViewState["report_code"].ToString() + ".rpt";
                oRpt.Load(Server.MapPath(strPath));
                TableLogOnInfo logOnInfo = new TableLogOnInfo();
                TableLogOnInfos tableLogOnInfos = new TableLogOnInfos();
                string strUsername = Session["username"].ToString();
                string strCompanyname = ViewState["companyname"].ToString();
                string strServername = System.Configuration.ConfigurationSettings.AppSettings["servername"];
                string strDbname = System.Configuration.ConfigurationSettings.AppSettings["dbname"];
                string strDbuser = System.Configuration.ConfigurationSettings.AppSettings["dbuser"];
                string strDbpassword = System.Configuration.ConfigurationSettings.AppSettings["dbpassword"];
                this.Title = ViewState["report_title"].ToString();
                logOnInfo.ConnectionInfo.ServerName = strServername;
                logOnInfo.ConnectionInfo.DatabaseName = strDbname;
                logOnInfo.ConnectionInfo.UserID = strDbuser;
                logOnInfo.ConnectionInfo.Password = strDbpassword;
                tableLogOnInfos.Add(logOnInfo);
                oRpt.SetParameterValue("@vc_criteria", ViewState["criteria"].ToString());
                oRpt.SetParameterValue("UserName", strUsername);
                oRpt.SetParameterValue("CompanyName", ViewState["companyname"].ToString());
                oRpt.SetParameterValue("OpenToDesc", ViewState["open_to_desc"].ToString());
                CrystalReportViewer1.LogOnInfo = tableLogOnInfos;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
        }

        private void Retive_Rep_loan01()
        {
            try
            {
                string strPath = "~/reports/" + ViewState["report_code"] + ".rpt";
                oRpt.Load(Server.MapPath(strPath));
                TableLogOnInfo logOnInfo = new TableLogOnInfo();
                TableLogOnInfos tableLogOnInfos = new TableLogOnInfos();
                string strUsername = Session["username"].ToString();
                string strCompanyname = ViewState["companyname"].ToString();
                string strServername = System.Configuration.ConfigurationSettings.AppSettings["servername"];
                string strDbname = System.Configuration.ConfigurationSettings.AppSettings["dbname"];
                string strDbuser = System.Configuration.ConfigurationSettings.AppSettings["dbuser"];
                string strDbpassword = System.Configuration.ConfigurationSettings.AppSettings["dbpassword"];
                this.Title = ViewState["report_title"].ToString();
                logOnInfo.ConnectionInfo.ServerName = strServername;
                logOnInfo.ConnectionInfo.DatabaseName = strDbname;
                logOnInfo.ConnectionInfo.UserID = strDbuser;
                logOnInfo.ConnectionInfo.Password = strDbpassword;
                tableLogOnInfos.Add(logOnInfo);
                oRpt.SetParameterValue("@vc_criteria", ViewState["criteria"].ToString());
                oRpt.SetParameterValue("@vc_criteria2", ViewState["criteria2"].ToString());
                CrystalReportViewer1.LogOnInfo = tableLogOnInfos;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
        }

        private void Retive_Rep_loan_record()
        {
            try
            {
                string strPath = "~/reports/" + ViewState["report_code"].ToString() + ".rpt";
                oRpt.Load(Server.MapPath(strPath));
                TableLogOnInfo logOnInfo = new TableLogOnInfo();
                TableLogOnInfos tableLogOnInfos = new TableLogOnInfos();
                string strUsername = Session["username"].ToString();
                string strCompanyname = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["companyname"].ToString();
                string strServername = System.Configuration.ConfigurationSettings.AppSettings["servername"];
                string strDbname = System.Configuration.ConfigurationSettings.AppSettings["dbname"];
                string strDbuser = System.Configuration.ConfigurationSettings.AppSettings["dbuser"];
                string strDbpassword = System.Configuration.ConfigurationSettings.AppSettings["dbpassword"];
                logOnInfo.ConnectionInfo.ServerName = strServername;
                logOnInfo.ConnectionInfo.DatabaseName = strDbname;
                logOnInfo.ConnectionInfo.UserID = strDbuser;
                logOnInfo.ConnectionInfo.Password = strDbpassword;
                tableLogOnInfos.Add(logOnInfo);
                oRpt.SetParameterValue("@vc_criteria", Session["criteria"].ToString());
                oRpt.SetParameterValue("UserName", strUsername);
                oRpt.SetParameterValue("CompanyName", strCompanyname);
                CrystalReportViewer1.LogOnInfo = tableLogOnInfos;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
        }


        protected void CrystalReportViewer1_Navigate(object source, CrystalDecisions.Web.NavigateEventArgs e)
        {
            getQueryString();
            printData();
            crConnectionInfo.DatabaseName = strDbname;
            crConnectionInfo.ServerName = strServername;
            crConnectionInfo.UserID = strDbuser;
            crConnectionInfo.Password = strDbpassword;
            crTables = oRpt.Database.Tables;

            //apply logon info
            foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in oRpt.Database.Tables)
            {
                crTableLogOnInfo = crTable.LogOnInfo;
                crTableLogOnInfo.ConnectionInfo = crConnectionInfo;
                crTable.ApplyLogOnInfo(crTableLogOnInfo);
            }

            //apply logon info for sub report
            //foreach (Section crSection in oRpt.ReportDefinition.Sections)
            //{
            //    foreach (ReportObject crReportObject in crSection.ReportObjects)
            //    {
            //        if (crReportObject.Kind == ReportObjectKind.SubreportObject)
            //        {
            //            SubreportObject crSubReportObj = (SubreportObject)(crReportObject);

            //            foreach (CrystalDecisions.CrystalReports.Engine.Table oTable in crSubReportObj.OpenSubreport(crSubReportObj.SubreportName).Database.Tables)
            //            {
            //                crTableLogOnInfo = oTable.LogOnInfo;
            //                crTableLogOnInfo.ConnectionInfo = crConnectionInfo;
            //                oTable.ApplyLogOnInfo(crTableLogOnInfo);
            //            }

            //        }

            //    }
            //}

            string strReportDirectoryTempPhysicalPath = Server.MapPath(this.ReportDirectoryTemp);
            Helper.DeleteUnusedFile(strReportDirectoryTempPhysicalPath, ReportAliveTime);

            string strFilename;
            strFilename = "report_" + DateTime.Now.ToString("yyyyMMddHH-mm-ss");
            oRpt.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath("~/temp/") + strFilename + ".pdf");
            lnkPdfFile.NavigateUrl = "~/temp/" + strFilename + ".pdf";
            imgPdf.Src = "~/images/icon_pdf.gif";
            lnkExcelFile.Visible = false;
            if (ViewState["report_code"].ToString() == "Rep_exceldebitall")
            {
                oRpt.ExportToDisk(ExportFormatType.ExcelRecord, Server.MapPath("~/temp/") + strFilename + ".xls");
                lnkExcelFile.NavigateUrl = "~/temp/" + strFilename + ".xls";
                imgExcel.Src = "~/images/icon_excel.gif";
                lnkExcelFile.Visible = true;
            }
            CrystalReportViewer1.ReportSource = oRpt;
        }

    }
}
