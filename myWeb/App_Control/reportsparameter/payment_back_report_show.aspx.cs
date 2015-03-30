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

namespace myWeb.App_Control.reportsparameter
{

    public partial class payment_back_report_show : PageBase
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
               
                CrystalReportViewer1.ReportSource = rptSource;
            }
        }

        private void printData()
        {
            if (ViewState["report_code"].ToString().Equals("Rep_payment01"))
            {
                Retive_Rep_payment01();
            }
            else if (ViewState["report_code"].ToString().Equals("Rep_payment02"))
            {
                Retive_Rep_payment02();
            }
            else if (ViewState["report_code"].ToString().Equals("Rep_payment03"))
            {
                Retive_Rep_payment03();
            }
            else if (ViewState["report_code"].ToString().Equals("Rep_payment04"))
            {
                Retive_Rep_payment04();
            }
            else if (ViewState["report_code"].ToString().Equals("Rep_payment05"))
            {
                Retive_Rep_payment05();
            }
            else if (ViewState["report_code"].ToString().Equals("Rep_payment06"))
            {
                Retive_Rep_payment06();
            }
            else if (ViewState["report_code"].ToString().Equals("Rep_payment07"))
            {
                Retive_Rep_payment03();
            }
            else if (ViewState["report_code"].ToString().Equals("Rep_payment08"))
            {
                Retive_Rep_payment03();
            }
            else if (ViewState["report_code"].ToString().Equals("Rep_payment09"))
            {
                Retive_Rep_payment09();
            }
            else if (ViewState["report_code"].ToString().Equals("Rep_payment10"))
            {
                Retive_Rep_payment10();
            }
            else if (ViewState["report_code"].ToString().Equals("Rep_paymentSOS"))
            {
                Retive_Rep_paymentSOS();
            }
            else if (ViewState["report_code"].ToString().Equals("Rep_paymentGBK"))
            {
                Retive_Rep_paymentGBK();
            }
            else if (ViewState["report_code"].ToString().Equals("Rep_paymentGSJ"))
            {
                Retive_Rep_paymentGBK();
            }
            else if (ViewState["report_code"].ToString().Equals("Rep_paymentGBK_sum"))
            {
                Retive_Rep_paymentGBK_sum();
            }
            else if (ViewState["report_code"].ToString().Equals("Rep_paymentGSJ_sum"))
            {
                Retive_Rep_paymentGBK_sum();
            }
            else if (ViewState["report_code"].ToString().Equals("Rep_payment01_sum"))
            {
                Retive_Rep_payment01_sum();
            }
            else if (ViewState["report_code"].ToString().Equals("Rep_payment01_major"))
            {
                Retive_Rep_payment01_major();
            }
            else if (ViewState["report_code"].ToString().Equals("Rep_payment02_sum") | ViewState["report_code"].ToString().Equals("Rep_paymentSOS_sum"))
            {
                Retive_Rep_payment02_sum();
            }
            else if (ViewState["report_code"].ToString().Equals("Rep_payment02_major") | ViewState["report_code"].ToString().Equals("Rep_paymentSOS_major"))
            {
                Retive_Rep_payment02_major();
            }

            if (ViewState["report_code"].ToString().Equals("Rep_paymentincome01"))
            {
                Retive_Rep_paymentincome01();
            }
            else if (ViewState["report_code"].ToString().Equals("Rep_paymentincome01_sum"))
            {
                Retive_Rep_paymentincome01_sum();
            }
            else if (ViewState["report_code"].ToString().Equals("Rep_paymentincome01_major"))
            {
                Retive_Rep_paymentincome01_major();
            }
            else if (ViewState["report_code"].ToString().Equals("Rep_paymentincome02"))
            {
                Retive_Rep_paymentincome02();
            }
            else if (ViewState["report_code"].ToString().Equals("Rep_paymentSOS"))
            {
                Retive_Rep_paymentSOS();
            }
            else if (ViewState["report_code"].ToString().Equals("Rep_paymentSOS_sum"))
            {
                ViewState["report_code"] = "Rep_paymentincomeSOS_sum";
                Retive_Rep_payment02_sum();
            }
            else if (ViewState["report_code"].ToString().Equals("Rep_paymentSOS_major"))
            {
                ViewState["report_code"] = "Rep_paymentincomeSOS_major";
                Retive_Rep_payment02_sum();
            }
            else if (ViewState["report_code"].ToString().Equals("Rep_payment_open") |
                     ViewState["report_code"].ToString().Equals("Rep_payment_open_director"))
            {
                Retive_Rep_payment_open();
            }
            else if (ViewState["report_code"].ToString().Equals("Rep_payment_open_all") |
                    ViewState["report_code"].ToString().Equals("Rep_payment_open_all_director"))
            {
                Retive_Rep_payment_open_all();
            }
                else if (ViewState["report_code"].ToString().Equals("Rep_payment_open_gbk") |
                    ViewState["report_code"].ToString().Equals("Rep_payment_open_gbk_director"))
            {
                Retive_Rep_payment_open_gbk();
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
            if (Request.QueryString["report_id"] != null)
            {
                ViewState["report_id"] = Request.QueryString["report_id"].ToString();
            }
            else
            {
                ViewState["report_id"] = "0";
            }

            if (!ViewState["report_id"].Equals("0"))
            {
                cPayment_back oPayment_back = new cPayment_back();
                string strMessage = string.Empty;
                string strCriteria2 = string.Empty;
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                strCriteria2 = " and report_id='" + ViewState["report_id"].ToString() + "' ";
                if (oPayment_back.SP_PAYMENT_BACK_REPORT_SEL(strCriteria2, ref ds, ref strMessage))
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        //ViewState["report_code"] = dt.Rows[0]["report_code"].ToString();

                        if (base.DirectorLock == "Y" && dt.Rows[0]["report_code"].ToString().Contains("Rep_payment_open"))
                        {
                            ViewState["report_code"] = dt.Rows[0]["report_code"].ToString() + "_director";
                        }
                        else
                        {
                            ViewState["report_code"] = dt.Rows[0]["report_code"].ToString();
                        }
                        
                        ViewState["report_title"] = dt.Rows[0]["report_title"].ToString();
                        ViewState["remark1"] = dt.Rows[0]["remark1"].ToString();
                        ViewState["remark2"] = dt.Rows[0]["remark2"].ToString();
                        ViewState["remark3"] = dt.Rows[0]["remark3"].ToString();
                    }
                }
            }
            else
            {
                if (Request.QueryString["report_code"] != null)
                {
                    ViewState["report_code"] = Request.QueryString["report_code"].ToString();
                }
                else
                {
                    ViewState["report_code"] = string.Empty;
                }
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

            //if (Request.QueryString["criteria"] != null)
            //{
            //    Session["criteria"] = Request.QueryString["criteria"].ToString();
            //}
            //else
            //{
            //    Session["criteria"] = string.Empty;
            //}

            if (Request.QueryString["item_des"] != null)
            {
                ViewState["item_des"] = Request.QueryString["item_des"].ToString();
            }
            else
            {
                ViewState["item_des"] = "0";
            }

            if (Request.QueryString["persongroup"] != null)
            {
                ViewState["persongroup"] = Request.QueryString["persongroup"].ToString();
            }
            else
            {
                ViewState["persongroup"] = "";
            }

            if (Request.QueryString["payment_back_type"] != null)
            {
                ViewState["payment_back_type"] = Request.QueryString["payment_back_type"].ToString();
            }
            else
            {
                ViewState["payment_back_type"] = "";
            }


        }

        private void Retive_Rep_payment01()
        {
            try
            {

                string strPath = "~/reports_back/" + ViewState["report_code"].ToString() + "_" + ViewState["payment_back_type"].ToString() + ".rpt";
                rptSource.Load(Server.MapPath(strPath));
                TableLogOnInfo logOnInfo = new TableLogOnInfo();
                TableLogOnInfos tableLogOnInfos = new TableLogOnInfos();
                string strUsername = Session["username"].ToString();
                string strCompanyname = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["companyname"].ToString();
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
                rptSource.SetParameterValue("@vc_criteria", Session["criteria"].ToString());
                rptSource.SetParameterValue("UserName", strUsername);
                rptSource.SetParameterValue("CompanyName", strCompanyname);
                rptSource.SetParameterValue("pMonth", getMonth());
                //rptSource.SetParameterValue("pMonth", ViewState["months"].ToString());
                rptSource.SetParameterValue("pYear", ViewState["year"].ToString());
                rptSource.SetParameterValue("pReport_title", ViewState["report_title"].ToString());
                CrystalReportViewer1.LogOnInfo = tableLogOnInfos;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
        }

        private void Retive_Rep_payment01_sum()
        {
            try
            {
                string strPath = "~/reports_back/" + ViewState["report_code"].ToString() + "_" + ViewState["payment_back_type"].ToString() + ".rpt";
                rptSource.Load(Server.MapPath(strPath));
                TableLogOnInfo logOnInfo = new TableLogOnInfo();
                TableLogOnInfos tableLogOnInfos = new TableLogOnInfos();
                string strUsername = Session["username"].ToString();
                string strCompanyname = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["companyname"].ToString();
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
                rptSource.SetParameterValue("@vc_criteria", Session["criteria"].ToString());
                rptSource.SetParameterValue("UserName", strUsername);
                rptSource.SetParameterValue("CompanyName", strCompanyname);
                rptSource.SetParameterValue("pMonth", getMonth());
                rptSource.SetParameterValue("pYear", ViewState["year"].ToString());
                rptSource.SetParameterValue("pReport_title", ViewState["report_title"].ToString());
                //CrystalReportViewer1.ReportSource = rptSource;
                CrystalReportViewer1.LogOnInfo = tableLogOnInfos;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
        }

        private void Retive_Rep_payment01_major()
        {
            try
            {
                string strPath = "~/reports_back/" + ViewState["report_code"].ToString() + "_" + ViewState["payment_back_type"].ToString() + ".rpt";
                rptSource.Load(Server.MapPath(strPath));
                TableLogOnInfo logOnInfo = new TableLogOnInfo();
                TableLogOnInfos tableLogOnInfos = new TableLogOnInfos();
                string strUsername = Session["username"].ToString();
                string strCompanyname = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["companyname"].ToString();
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
                rptSource.SetParameterValue("@vc_criteria", Session["criteria"].ToString());
                rptSource.SetParameterValue("UserName", strUsername);
                rptSource.SetParameterValue("CompanyName", strCompanyname);
                rptSource.SetParameterValue("pMonth", getMonth());
                rptSource.SetParameterValue("pYear", ViewState["year"].ToString());
                rptSource.SetParameterValue("pReport_title", ViewState["report_title"].ToString());
                rptSource.SetParameterValue("pGroupNo", ViewState["remark1"].ToString());
                rptSource.SetParameterValue("pItem_des", ViewState["remark2"].ToString());
                //CrystalReportViewer1.ReportSource = rptSource;
                CrystalReportViewer1.LogOnInfo = tableLogOnInfos;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
        }


        private void Retive_Rep_payment02()
        {
            try
            {
                string strPath = "~/reports_back/" + ViewState["report_code"].ToString() + "_" + ViewState["payment_back_type"].ToString() + ".rpt";
                rptSource.Load(Server.MapPath(strPath));
                TableLogOnInfo logOnInfo = new TableLogOnInfo();
                TableLogOnInfos tableLogOnInfos = new TableLogOnInfos();
                string strUsername = Session["username"].ToString();
                string strCompanyname = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["companyname"].ToString();
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
                rptSource.SetParameterValue("@vc_criteria", Session["criteria"].ToString());
                rptSource.SetParameterValue("UserName", strUsername);
                rptSource.SetParameterValue("CompanyName", strCompanyname);
                rptSource.SetParameterValue("pMonth", getMonth());
                rptSource.SetParameterValue("pYear", ViewState["year"].ToString());
                rptSource.SetParameterValue("pPerson_group", ViewState["remark1"].ToString());
                //CrystalReportViewer1.ReportSource = rptSource;
                CrystalReportViewer1.LogOnInfo = tableLogOnInfos;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
        }

        private void Retive_Rep_payment02_sum()
        {
            try
            {
                string strPath = "~/reports_back/" + ViewState["report_code"].ToString() + "_" + ViewState["payment_back_type"].ToString() + ".rpt";
                rptSource.Load(Server.MapPath(strPath));
                TableLogOnInfo logOnInfo = new TableLogOnInfo();
                TableLogOnInfos tableLogOnInfos = new TableLogOnInfos();
                string strUsername = Session["username"].ToString();
                string strCompanyname = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["companyname"].ToString();
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
                rptSource.SetParameterValue("@vc_criteria", Session["criteria"].ToString());
                rptSource.SetParameterValue("UserName", strUsername);
                rptSource.SetParameterValue("CompanyName", strCompanyname);
                rptSource.SetParameterValue("pMonth", getMonth());
                rptSource.SetParameterValue("pYear", ViewState["year"].ToString());
                rptSource.SetParameterValue("pReport_title", ViewState["report_title"].ToString());
                //CrystalReportViewer1.ReportSource = rptSource;
                CrystalReportViewer1.LogOnInfo = tableLogOnInfos;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
        }

        private void Retive_Rep_payment02_major()
        {
            try
            {
                string strPath = "~/reports_back/" + ViewState["report_code"].ToString() + "_" + ViewState["payment_back_type"].ToString() + ".rpt";
                rptSource.Load(Server.MapPath(strPath));
                TableLogOnInfo logOnInfo = new TableLogOnInfo();
                TableLogOnInfos tableLogOnInfos = new TableLogOnInfos();
                string strUsername = Session["username"].ToString();
                string strCompanyname = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["companyname"].ToString();
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
                rptSource.SetParameterValue("@vc_criteria", Session["criteria"].ToString());
                rptSource.SetParameterValue("UserName", strUsername);
                rptSource.SetParameterValue("CompanyName", strCompanyname);
                rptSource.SetParameterValue("pMonth", getMonth());
                rptSource.SetParameterValue("pYear", ViewState["year"].ToString());
                rptSource.SetParameterValue("pReport_title", ViewState["report_title"].ToString());
                rptSource.SetParameterValue("pGroupNo", ViewState["remark1"].ToString());
                rptSource.SetParameterValue("pItem_des", ViewState["remark2"].ToString());
                //CrystalReportViewer1.ReportSource = rptSource;
                CrystalReportViewer1.LogOnInfo = tableLogOnInfos;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
        }

        
        private void Retive_Rep_payment03()
        {
            try
            {
                string strPath = "~/reports_back/" + ViewState["report_code"].ToString() + "_" + ViewState["payment_back_type"].ToString() + ".rpt";
                rptSource.Load(Server.MapPath(strPath));
                TableLogOnInfo logOnInfo = new TableLogOnInfo();
                TableLogOnInfos tableLogOnInfos = new TableLogOnInfos();
                string strUsername = Session["username"].ToString();
                string strCompanyname = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["companyname"].ToString();
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
                rptSource.SetParameterValue("@vc_criteria", Session["criteria"].ToString());
                rptSource.SetParameterValue("UserName", strUsername);
                rptSource.SetParameterValue("CompanyName", strCompanyname);
                //rptSource.SetParameterValue("pMonth", getMonth());
                rptSource.SetParameterValue("pMonth", ViewState["months"].ToString());
                rptSource.SetParameterValue("pYear", ViewState["year"].ToString());
                rptSource.SetParameterValue("pReport_title", ViewState["report_title"].ToString());
                rptSource.SetParameterValue("pItem_des", ViewState["remark1"].ToString());
                //CrystalReportViewer1.ReportSource = rptSource;
                CrystalReportViewer1.LogOnInfo = tableLogOnInfos;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
        }

        private void Retive_Rep_payment04()
        {
            try
            {
                string strPath = "~/reports_back/" + ViewState["report_code"].ToString() + "_" + ViewState["payment_back_type"].ToString() + ".rpt";
                rptSource.Load(Server.MapPath(strPath));
                TableLogOnInfo logOnInfo = new TableLogOnInfo();
                TableLogOnInfos tableLogOnInfos = new TableLogOnInfos();
                string strUsername = Session["username"].ToString();
                string strCompanyname = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["companyname"].ToString();
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
                rptSource.SetParameterValue("@vc_criteria", Session["criteria"].ToString());
                rptSource.SetParameterValue("UserName", strUsername);
                rptSource.SetParameterValue("CompanyName", strCompanyname);
                rptSource.SetParameterValue("pMonth", getMonth());
                rptSource.SetParameterValue("pYear", ViewState["year"].ToString());
                rptSource.SetParameterValue("pReport_title", ViewState["report_title"].ToString());
                rptSource.SetParameterValue("pItem_des", ViewState["remark1"].ToString());
                //CrystalReportViewer1.ReportSource = rptSource;
                CrystalReportViewer1.LogOnInfo = tableLogOnInfos;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
        }

        private void Retive_Rep_payment05()
        {
            try
            {
                string strPath = "~/reports_back/" + ViewState["report_code"].ToString() + "_" + ViewState["payment_back_type"].ToString() + ".rpt";
                rptSource.Load(Server.MapPath(strPath));
                TableLogOnInfo logOnInfo = new TableLogOnInfo();
                TableLogOnInfos tableLogOnInfos = new TableLogOnInfos();
                string strUsername = Session["username"].ToString();
                string strCompanyname = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["companyname"].ToString();
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
                rptSource.SetParameterValue("@vc_criteria", Session["criteria"].ToString());
                rptSource.SetParameterValue("UserName", strUsername);
                rptSource.SetParameterValue("CompanyName", strCompanyname);
                rptSource.SetParameterValue("pMonth", getMonth());
                rptSource.SetParameterValue("pYear", ViewState["year"].ToString());
                rptSource.SetParameterValue("pType_des", ViewState["remark1"].ToString());
                //CrystalReportViewer1.ReportSource = rptSource;
                CrystalReportViewer1.LogOnInfo = tableLogOnInfos;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
        }

        private void Retive_Rep_payment06()
        {
            try
            {
                string strPath = "~/reports_back/" + ViewState["report_code"].ToString() + "_" + ViewState["payment_back_type"].ToString() + ".rpt";
                rptSource.Load(Server.MapPath(strPath));
                TableLogOnInfo logOnInfo = new TableLogOnInfo();
                TableLogOnInfos tableLogOnInfos = new TableLogOnInfos();
                string strUsername = Session["username"].ToString();
                string strCompanyname = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["companyname"].ToString();
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
                rptSource.SetParameterValue("@vc_criteria", Session["criteria"].ToString());
                rptSource.SetParameterValue("UserName", strUsername);
                rptSource.SetParameterValue("CompanyName", strCompanyname);
                rptSource.SetParameterValue("pMonth", getMonth());
                rptSource.SetParameterValue("pYear", ViewState["year"].ToString());
                rptSource.SetParameterValue("pItem_des", ViewState["remark1"].ToString());
                //CrystalReportViewer1.ReportSource = rptSource;
                CrystalReportViewer1.LogOnInfo = tableLogOnInfos;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
        }

        private void Retive_Rep_payment09()
        {
            try
            {
                string strPath = "~/reports_back/" + ViewState["report_code"].ToString() + "_" + ViewState["payment_back_type"].ToString() + ".rpt";
                rptSource.Load(Server.MapPath(strPath));
                TableLogOnInfo logOnInfo = new TableLogOnInfo();
                TableLogOnInfos tableLogOnInfos = new TableLogOnInfos();
                string strUsername = Session["username"].ToString();
                string strCompanyname = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["companyname"].ToString();
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
                rptSource.SetParameterValue("@vc_criteria", Session["criteria"].ToString());
                rptSource.SetParameterValue("UserName", strUsername);
                rptSource.SetParameterValue("CompanyName", strCompanyname);
                rptSource.SetParameterValue("pMonth", getMonth());
                rptSource.SetParameterValue("pYear", ViewState["year"].ToString());
                rptSource.SetParameterValue("pReport_title", ViewState["report_title"].ToString());
                rptSource.SetParameterValue("salary_fix", ViewState["remark1"].ToString());
                //CrystalReportViewer1.ReportSource = rptSource;
                CrystalReportViewer1.LogOnInfo = tableLogOnInfos;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
        }

        private void Retive_Rep_payment10()
        {
            try
            {
                string strPath = "~/reports_back/" + ViewState["report_code"].ToString() + "_" + ViewState["payment_back_type"].ToString() + ".rpt";
                rptSource.Load(Server.MapPath(strPath));
                TableLogOnInfo logOnInfo = new TableLogOnInfo();
                TableLogOnInfos tableLogOnInfos = new TableLogOnInfos();
                string strUsername = Session["username"].ToString();
                string strCompanyname = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["companyname"].ToString();
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
                rptSource.SetParameterValue("@vc_criteria", Session["criteria"].ToString());
                rptSource.SetParameterValue("UserName", strUsername);
                rptSource.SetParameterValue("CompanyName", strCompanyname);
                rptSource.SetParameterValue("pMonth", getMonth());
                rptSource.SetParameterValue("pYear", ViewState["year"].ToString());
                rptSource.SetParameterValue("pReport_title", ViewState["report_title"].ToString());
                //CrystalReportViewer1.ReportSource = rptSource;
                CrystalReportViewer1.LogOnInfo = tableLogOnInfos;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
        }

        private void Retive_Rep_paymentSOS()
        {
            try
            {
                string strPath = "~/reports_back/" + ViewState["report_code"].ToString() + "_" + ViewState["payment_back_type"].ToString() + ".rpt";
                rptSource.Load(Server.MapPath(strPath));
                TableLogOnInfo logOnInfo = new TableLogOnInfo();
                TableLogOnInfos tableLogOnInfos = new TableLogOnInfos();
                string strUsername = Session["username"].ToString();
                string strCompanyname = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["companyname"].ToString();
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
                rptSource.SetParameterValue("@vc_criteria", Session["criteria"].ToString());
                rptSource.SetParameterValue("UserName", strUsername);
                rptSource.SetParameterValue("CompanyName", strCompanyname);
                //rptSource.SetParameterValue("pMonth", getMonth());
                rptSource.SetParameterValue("pMonth", ViewState["months"].ToString());
                rptSource.SetParameterValue("pYear", ViewState["year"].ToString());
                rptSource.SetParameterValue("pReport_title", ViewState["report_title"].ToString());
                rptSource.SetParameterValue("pItem_des", ViewState["remark1"].ToString());
                //CrystalReportViewer1.ReportSource = rptSource;
                CrystalReportViewer1.LogOnInfo = tableLogOnInfos;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
        }

        private void Retive_Rep_paymentGBK()
        {
            try
            {
                string strPath = "~/reports_back/" + ViewState["report_code"].ToString() + "_" + ViewState["payment_back_type"].ToString() + ".rpt";
                rptSource.Load(Server.MapPath(strPath));
                TableLogOnInfo logOnInfo = new TableLogOnInfo();
                TableLogOnInfos tableLogOnInfos = new TableLogOnInfos();
                string strUsername = Session["username"].ToString();
                string strCompanyname = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["companyname"].ToString();
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
                rptSource.SetParameterValue("@vc_criteria", Session["criteria"].ToString());
                rptSource.SetParameterValue("@vc_yearmonth", ViewState["year"].ToString() + '/' + ViewState["months"].ToString());
                rptSource.SetParameterValue("UserName", strUsername);
                rptSource.SetParameterValue("CompanyName", strCompanyname);
                rptSource.SetParameterValue("pMonth", getMonth());
                rptSource.SetParameterValue("pYear", ViewState["year"].ToString());
                rptSource.SetParameterValue("pReport_title", ViewState["report_title"].ToString());
                //CrystalReportViewer1.ReportSource = rptSource;
                CrystalReportViewer1.LogOnInfo = tableLogOnInfos;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
        }

        private void Retive_Rep_paymentGBK_sum()
        {
            try
            {
                string strPath = "~/reports_back/" + ViewState["report_code"].ToString() + "_" + ViewState["payment_back_type"].ToString() + ".rpt";
                //string strPath = "~/reports/" + ViewState["report_code"].ToString() + ".rpt";
                rptSource.Load(Server.MapPath(strPath));
                TableLogOnInfo logOnInfo = new TableLogOnInfo();
                TableLogOnInfos tableLogOnInfos = new TableLogOnInfos();
                string strUsername = Session["username"].ToString();
                string strCompanyname = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["companyname"].ToString();
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
                rptSource.SetParameterValue("@vc_criteria", Session["criteria"].ToString());
                rptSource.SetParameterValue("UserName", strUsername);
                rptSource.SetParameterValue("CompanyName", strCompanyname);
                rptSource.SetParameterValue("pMonth", getMonth());
                rptSource.SetParameterValue("pYear", ViewState["year"].ToString());
                rptSource.SetParameterValue("pReport_title", ViewState["report_title"].ToString());
                //CrystalReportViewer1.ReportSource = rptSource;
                CrystalReportViewer1.LogOnInfo = tableLogOnInfos;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
        }

        private void Retive_Rep_paymentincome01()
        {
            try
            {

                string strPath = "~/reports_back/" + ViewState["report_code"].ToString() + "_" + ViewState["payment_back_type"].ToString() + ".rpt";
                rptSource.Load(Server.MapPath(strPath));
                TableLogOnInfo logOnInfo = new TableLogOnInfo();
                TableLogOnInfos tableLogOnInfos = new TableLogOnInfos();
                string strUsername = Session["username"].ToString();
                string strCompanyname = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["companyname"].ToString();
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
                rptSource.SetParameterValue("@vc_criteria", Session["criteria"].ToString());
                rptSource.SetParameterValue("UserName", strUsername);
                rptSource.SetParameterValue("CompanyName", strCompanyname);
                rptSource.SetParameterValue("pMonth", getMonth());
                rptSource.SetParameterValue("pYear", ViewState["year"].ToString());
                rptSource.SetParameterValue("pReport_title", ViewState["report_title"].ToString());
                CrystalReportViewer1.LogOnInfo = tableLogOnInfos;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
        }

        private void Retive_Rep_paymentincome01_sum()
        {
            try
            {
                string strPath = "~/reports_back/" + ViewState["report_code"].ToString() + "_" + ViewState["payment_back_type"].ToString() + ".rpt";
                rptSource.Load(Server.MapPath(strPath));
                TableLogOnInfo logOnInfo = new TableLogOnInfo();
                TableLogOnInfos tableLogOnInfos = new TableLogOnInfos();
                string strUsername = Session["username"].ToString();
                string strCompanyname = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["companyname"].ToString();
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
                rptSource.SetParameterValue("@vc_criteria", Session["criteria"].ToString());
                rptSource.SetParameterValue("UserName", strUsername);
                rptSource.SetParameterValue("CompanyName", strCompanyname);
                rptSource.SetParameterValue("pMonth", getMonth());
                rptSource.SetParameterValue("pYear", ViewState["year"].ToString());
                rptSource.SetParameterValue("pReport_title", ViewState["report_title"].ToString());
                //CrystalReportViewer1.ReportSource = rptSource;
                CrystalReportViewer1.LogOnInfo = tableLogOnInfos;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
        }

        private void Retive_Rep_paymentincome01_major()
        {
            try
            {
                string strPath = "~/reports_back/" + ViewState["report_code"].ToString() + "_" + ViewState["payment_back_type"].ToString() + ".rpt";
                rptSource.Load(Server.MapPath(strPath));
                TableLogOnInfo logOnInfo = new TableLogOnInfo();
                TableLogOnInfos tableLogOnInfos = new TableLogOnInfos();
                string strUsername = Session["username"].ToString();
                string strCompanyname = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["companyname"].ToString();
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
                rptSource.SetParameterValue("@vc_criteria", Session["criteria"].ToString());
                rptSource.SetParameterValue("UserName", strUsername);
                rptSource.SetParameterValue("CompanyName", strCompanyname);
                rptSource.SetParameterValue("pMonth", getMonth());
                rptSource.SetParameterValue("pYear", ViewState["year"].ToString());
                rptSource.SetParameterValue("pReport_title", ViewState["report_title"].ToString());
                rptSource.SetParameterValue("pGroupNo", ViewState["remark1"].ToString());
                rptSource.SetParameterValue("pItem_des", ViewState["remark2"].ToString());
                //CrystalReportViewer1.ReportSource = rptSource;
                CrystalReportViewer1.LogOnInfo = tableLogOnInfos;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
        }

        private void Retive_Rep_paymentincome02()
        {
            try
            {
                string strPath = "~/reports_back/" + ViewState["report_code"].ToString() + "_" + ViewState["payment_back_type"].ToString() + ".rpt";
                rptSource.Load(Server.MapPath(strPath));
                TableLogOnInfo logOnInfo = new TableLogOnInfo();
                TableLogOnInfos tableLogOnInfos = new TableLogOnInfos();
                string strUsername = Session["username"].ToString();
                string strCompanyname = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["companyname"].ToString();
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
                rptSource.SetParameterValue("@vc_criteria", Session["criteria"].ToString());
                rptSource.SetParameterValue("UserName", strUsername);
                rptSource.SetParameterValue("CompanyName", strCompanyname);
                rptSource.SetParameterValue("pMonth", ViewState["months"].ToString());
                rptSource.SetParameterValue("pYear", ViewState["year"].ToString());
                rptSource.SetParameterValue("pReport_title", ViewState["report_title"].ToString());
                rptSource.SetParameterValue("pItem_des", ViewState["remark1"].ToString());
                //CrystalReportViewer1.ReportSource = rptSource;
                CrystalReportViewer1.LogOnInfo = tableLogOnInfos;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
        }

        private void Retive_Rep_payment_open()
        {
            try
            {

                string strPath = "~/reports_back/" + ViewState["report_code"].ToString() + "_" + ViewState["payment_back_type"].ToString() + ".rpt";
                rptSource.Load(Server.MapPath(strPath));
                TableLogOnInfo logOnInfo = new TableLogOnInfo();
                TableLogOnInfos tableLogOnInfos = new TableLogOnInfos();
                string strUsername = Session["username"].ToString();
                string strCompanyname = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["companyname"].ToString();
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
                rptSource.SetParameterValue("@vc_criteria", Session["criteria"].ToString());
                rptSource.SetParameterValue("UserName", strUsername);
                rptSource.SetParameterValue("CompanyName", strCompanyname);
                rptSource.SetParameterValue("pMonth", getMonth());
                string strYear = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["yearnow"].ToString();
                rptSource.SetParameterValue("pYear", strYear);
                rptSource.SetParameterValue("pReport_title", ViewState["report_title"].ToString());
                rptSource.SetParameterValue("pReportDate", cCommon.CheckDate(Session["payment_date"].ToString()));
          
                CrystalReportViewer1.LogOnInfo = tableLogOnInfos;

            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
        }

        private void Retive_Rep_payment_open_all()
        {
            try
            {

                string strPath = "~/reports_back/" + ViewState["report_code"].ToString() + "_" + ViewState["payment_back_type"].ToString() + ".rpt";
                rptSource.Load(Server.MapPath(strPath));
                TableLogOnInfo logOnInfo = new TableLogOnInfo();
                TableLogOnInfos tableLogOnInfos = new TableLogOnInfos();
                string strUsername = Session["username"].ToString();
                string strCompanyname = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["companyname"].ToString();
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
                rptSource.SetParameterValue("@vc_criteria", Session["criteria"].ToString().Replace("view_payment_back.",""));
                rptSource.SetParameterValue("UserName", strUsername);
                rptSource.SetParameterValue("CompanyName", strCompanyname);
                rptSource.SetParameterValue("pMonth", getMonth());
                string strYear = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["yearnow"].ToString();
                rptSource.SetParameterValue("pYear", strYear);
                rptSource.SetParameterValue("pReport_title", ViewState["report_title"].ToString());
                rptSource.SetParameterValue("pReportDate", cCommon.CheckDate(Session["payment_date"].ToString()));
          
                CrystalReportViewer1.LogOnInfo = tableLogOnInfos;

            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
        }

        private void Retive_Rep_payment_open_gbk()
        {
            try
            {

                string strPath = "~/reports_back/" + ViewState["report_code"].ToString() + "_" + ViewState["payment_back_type"].ToString() + ".rpt";
                rptSource.Load(Server.MapPath(strPath));
                TableLogOnInfo logOnInfo = new TableLogOnInfo();
                TableLogOnInfos tableLogOnInfos = new TableLogOnInfos();
                string strUsername = Session["username"].ToString();
                string strCompanyname = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["companyname"].ToString();
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
                rptSource.SetParameterValue("@vc_criteria", Session["criteria"].ToString().Replace("view_payment_back.", "view_payment_all.")
                                         .Replace("view_payment_all.payment_back_type", "view_payment_back_head.payment_back_type")
                                         .Replace("08-001", "08-001A"));
                rptSource.SetParameterValue("UserName", strUsername);
                rptSource.SetParameterValue("CompanyName", strCompanyname);
                rptSource.SetParameterValue("pMonth", getMonth());
                string strYear = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["yearnow"].ToString();
                rptSource.SetParameterValue("pYear", strYear);
                rptSource.SetParameterValue("pReport_title", ViewState["report_title"].ToString());
                rptSource.SetParameterValue("pReportDate", cCommon.CheckDate(Session["payment_date"].ToString()));
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
            //lnkCsvFile.Visible = false;
            //if (ViewState["report_code"].ToString() == "Rep_exceldebitall")
            //{
            //    rptSource.ExportToDisk(ExportFormatType.ExcelRecord, Server.MapPath("~/temp/") + strFilename + ".xls");
            //    lnkCsvFile.NavigateUrl = "~/temp/" + strFilename + ".xls";
            //    imgCsv.Src = "~/images/icon_excel.gif";
            //    lnkCsvFile.Visible = true;
            //}
            CrystalReportViewer1.ReportSource = rptSource;
        }

    
    }
}
