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

namespace myWeb.Person_Manage
{

    public partial class global_payment_report : GlobalPageBase
    {
        private ReportDocument rptSource = new ReportDocument();
        private Tables crTables;
        private TableLogOnInfo crTableLogOnInfo;
        private ConnectionInfo crConnectionInfo = new CrystalDecisions.Shared.ConnectionInfo();
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
                crTables = rptSource.Database.Tables;

                //apply logon info
                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in rptSource.Database.Tables)
                {
                    crTableLogOnInfo = crTable.LogOnInfo;
                    crTableLogOnInfo.ConnectionInfo = crConnectionInfo;
                    crTable.ApplyLogOnInfo(crTableLogOnInfo);
                }

                //apply logon info for sub report
                foreach (Section crSection in rptSource.ReportDefinition.Sections)
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
                rptSource.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath("~/temp/") + strFilename + ".pdf");
                lnkPdfFile.NavigateUrl = "~/temp/" + strFilename + ".pdf";
                imgPdf.Src = "~/images/icon_pdf.gif";

                rptSource.ExportToDisk(ExportFormatType.RichText, Server.MapPath("~/temp/") + strFilename + ".rtf");
                lnkWordFile.NavigateUrl = "~/temp/" + strFilename + ".rtf";
                imgWord.Src = "~/images/icon_word.gif";

                CrystalReportViewer1.ReportSource = rptSource;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th-TH");
            //getQueryString();
            //printData();
            //CrystalReportViewer1.ReportSource = rptSource;
            //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th-TH");
            //lblError.Text = string.Empty;
            if (IsPostBack)
            {
                getQueryString();
                printData();
                crConnectionInfo.DatabaseName = strDbname;
                crConnectionInfo.ServerName = strServername;
                crConnectionInfo.UserID = strDbuser;
                crConnectionInfo.Password = strDbpassword;
                crTables = rptSource.Database.Tables;

                //apply logon info
                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in rptSource.Database.Tables)
                {
                    crTableLogOnInfo = crTable.LogOnInfo;
                    crTableLogOnInfo.ConnectionInfo = crConnectionInfo;
                    crTable.ApplyLogOnInfo(crTableLogOnInfo);
                }

                //apply logon info for sub report
                foreach (Section crSection in rptSource.ReportDefinition.Sections)
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
                rptSource.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath("~/temp/") + strFilename + ".pdf");
                lnkPdfFile.NavigateUrl = "~/temp/" + strFilename + ".pdf";
                imgPdf.Src = "~/images/icon_pdf.gif";

                rptSource.ExportToDisk(ExportFormatType.RichText, Server.MapPath("~/temp/") + strFilename + ".doc");
                lnkWordFile.NavigateUrl = "~/temp/" + strFilename + ".doc";
                imgWord.Src = "~/images/icon_word.gif";

                CrystalReportViewer1.ReportSource = rptSource;
            }
        }

        private void printData()
        {
            if (ViewState["report_code"].ToString().Equals("Rep_payment_req_certificate"))
            {
                Retive_Rep_payment_req_certificate();
            }
            if (ViewState["report_code"].ToString().Equals("Rep_payment_req_loan"))
            {
                Retive_Rep_payment_req_loan();
            }
            else if (ViewState["report_code"].ToString().Equals("Rep_payment_openbysum"))
            {
                Retive_Rep_payment_openbysum();
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

            if (Request.QueryString["report_code"] != null)
            {
                ViewState["report_code"] = Request.QueryString["report_code"].ToString();
            }
            else
            {
                ViewState["report_code"] = string.Empty;
            }

            if (Request.QueryString["months"] != null)
            {
                ViewState["months"] = Request.QueryString["months"].ToString();
            }
            else
            {
                ViewState["months"] = string.Empty;
            }

            if (Request.QueryString["year"] != null)
            {
                ViewState["year"] = Request.QueryString["year"].ToString();
            }
            else
            {
                ViewState["year"] = string.Empty;
            }

            if (Request.QueryString["payment_position"] != null)
            {
                ViewState["payment_position"] = Request.QueryString["payment_position"].ToString();
            }
            else
            {
                ViewState["payment_position"] = "0.00";
            }

            if (Request.QueryString["payment_reward"] != null)
            {
                ViewState["payment_reward"] = Request.QueryString["payment_reward"].ToString();
            }
            else
            {
                ViewState["payment_reward"] = "0.00";
            }

            if (Request.QueryString["payment_type"] != null)
            {
                ViewState["payment_type"] = Request.QueryString["payment_type"].ToString();
            }
            else
            {
                ViewState["payment_type"] = "0.00";
            }
            if (Request.QueryString["report_title"] != null)
            {
                ViewState["report_title"] = Request.QueryString["report_title"].ToString();
            }
            else
            {
                ViewState["report_title"] = "";
            }


        }

        private void Retive_Rep_payment_req_certificate()
        {
            try
            {

                string strPath = "~/reports/" + ViewState["report_code"].ToString() + ".rpt";
                rptSource.Load(Server.MapPath(strPath));
                TableLogOnInfo logOnInfo = new TableLogOnInfo();
                TableLogOnInfos tableLogOnInfos = new TableLogOnInfos();
                string strUsername = "";
                string strCompanyname = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["companyname"].ToString();
                string strServername = System.Configuration.ConfigurationSettings.AppSettings["servername"];
                string strDbname = System.Configuration.ConfigurationSettings.AppSettings["dbname"];
                string strDbuser = System.Configuration.ConfigurationSettings.AppSettings["dbuser"];
                string strDbpassword = System.Configuration.ConfigurationSettings.AppSettings["dbpassword"];
                // this.Title = ViewState["report_title"].ToString();
                logOnInfo.ConnectionInfo.ServerName = strServername;
                logOnInfo.ConnectionInfo.DatabaseName = strDbname;
                logOnInfo.ConnectionInfo.UserID = strDbuser;
                logOnInfo.ConnectionInfo.Password = strDbpassword;
                tableLogOnInfos.Add(logOnInfo);
                rptSource.SetParameterValue("@vc_criteria", Session["criteria"].ToString());
                rptSource.SetParameterValue("UserName", strUsername);
                rptSource.SetParameterValue("CompanyName", strCompanyname);
                rptSource.SetParameterValue("payment_position", ViewState["payment_position"].ToString());
                rptSource.SetParameterValue("payment_reward", ViewState["payment_reward"].ToString());
                rptSource.SetParameterValue("payment_type", ViewState["payment_type"].ToString());
                rptSource.SetParameterValue("report_title", ViewState["report_title"].ToString());

                CrystalReportViewer1.LogOnInfo = tableLogOnInfos;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
        }

        private void Retive_Rep_payment_req_loan()
        {
            try
            {

                string strPath = "~/reports/" + ViewState["report_code"].ToString() + ".rpt";
                rptSource.Load(Server.MapPath(strPath));
                TableLogOnInfo logOnInfo = new TableLogOnInfo();
                TableLogOnInfos tableLogOnInfos = new TableLogOnInfos();
                string strUsername = "";
                string strCompanyname = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["companyname"].ToString();
                string strServername = System.Configuration.ConfigurationSettings.AppSettings["servername"];
                string strDbname = System.Configuration.ConfigurationSettings.AppSettings["dbname"];
                string strDbuser = System.Configuration.ConfigurationSettings.AppSettings["dbuser"];
                string strDbpassword = System.Configuration.ConfigurationSettings.AppSettings["dbpassword"];
                // this.Title = ViewState["report_title"].ToString();
                logOnInfo.ConnectionInfo.ServerName = strServername;
                logOnInfo.ConnectionInfo.DatabaseName = strDbname;
                logOnInfo.ConnectionInfo.UserID = strDbuser;
                logOnInfo.ConnectionInfo.Password = strDbpassword;
                tableLogOnInfos.Add(logOnInfo);
                rptSource.SetParameterValue("@vc_criteria", Session["criteria"].ToString());
                rptSource.SetParameterValue("UserName", strUsername);
                rptSource.SetParameterValue("CompanyName", strCompanyname);
                rptSource.SetParameterValue("report_title", ViewState["report_title"].ToString());
                CrystalReportViewer1.LogOnInfo = tableLogOnInfos;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
        }


        private void Retive_Rep_payment_openbysum()
        {
            try
            {

                string strPath = "~/reports/" + ViewState["report_code"].ToString() + ".rpt";
                rptSource.Load(Server.MapPath(strPath));
                TableLogOnInfo logOnInfo = new TableLogOnInfo();
                TableLogOnInfos tableLogOnInfos = new TableLogOnInfos();
                string strUsername = base.PersonFullName;
                string strCompanyname = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["companyname"].ToString();
                string strServername = System.Configuration.ConfigurationSettings.AppSettings["servername"];
                string strDbname = System.Configuration.ConfigurationSettings.AppSettings["dbname"];
                string strDbuser = System.Configuration.ConfigurationSettings.AppSettings["dbuser"];
                string strDbpassword = System.Configuration.ConfigurationSettings.AppSettings["dbpassword"];
                this.Title += " - รายงานสรุปยอดเงินงบประมาณคงเหลือ";
                logOnInfo.ConnectionInfo.ServerName = strServername;
                logOnInfo.ConnectionInfo.DatabaseName = strDbname;
                logOnInfo.ConnectionInfo.UserID = strDbuser;
                logOnInfo.ConnectionInfo.Password = strDbpassword;
                tableLogOnInfos.Add(logOnInfo);
                rptSource.SetParameterValue("@vc_criteria", Session["criteria"].ToString());
                rptSource.SetParameterValue("@vc_criteria2", Session["criteria2"].ToString());
                rptSource.SetParameterValue("@vc_criteria3", Session["criteria3"].ToString());
                rptSource.SetParameterValue("UserName", strUsername);
                rptSource.SetParameterValue("CompanyName", strCompanyname);
                rptSource.SetParameterValue("pMonth", getMonth());
                string strYear = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["yearnow"].ToString();
                rptSource.SetParameterValue("pYear", strYear);
                rptSource.SetParameterValue("pReport_title", "รายงานสรุปยอดเงินงบประมาณคงเหลือ");
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
            crTables = rptSource.Database.Tables;

            //apply logon info
            foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in rptSource.Database.Tables)
            {
                crTableLogOnInfo = crTable.LogOnInfo;
                crTableLogOnInfo.ConnectionInfo = crConnectionInfo;
                crTable.ApplyLogOnInfo(crTableLogOnInfo);
            }

            //apply logon info for sub report
            foreach (Section crSection in rptSource.ReportDefinition.Sections)
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

            rptSource.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath("~/temp/") + strFilename + ".pdf");
            lnkPdfFile.NavigateUrl = "~/temp/" + strFilename + ".pdf";
            imgPdf.Src = "~/images/icon_pdf.gif";

            rptSource.ExportToDisk(ExportFormatType.RichText, Server.MapPath("~/temp/") + strFilename + ".doc");
            lnkWordFile.NavigateUrl = "~/temp/" + strFilename + ".doc";
            imgWord.Src = "~/images/icon_word.gif";

            CrystalReportViewer1.ReportSource = rptSource;

        }

    }
}
