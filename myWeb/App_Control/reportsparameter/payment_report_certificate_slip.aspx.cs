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

    public partial class payment_report_certificate_slip : PageBase
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
            if (!IsPostBack)
            {
                getQueryString();
                Retive_Rep_payment_slip();
                crConnectionInfo.DatabaseName = strDbname;
                crConnectionInfo.ServerName = strServername;
                crConnectionInfo.UserID = strDbuser;
                crConnectionInfo.Password = strDbpassword;
                crTables = rptSource.Database.Tables;
                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    crTableLogOnInfo = crTable.LogOnInfo;
                    crTableLogOnInfo.ConnectionInfo = crConnectionInfo;
                    crTable.ApplyLogOnInfo(crTableLogOnInfo);
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
            getQueryString();
            Retive_Rep_payment_slip();
            CrystalReportViewer1.ReportSource = rptSource;
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
                cPayment oPayment = new cPayment();
                string strMessage = string.Empty;
                string strCriteria2 = string.Empty;
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                strCriteria2 = " and report_id='" + ViewState["report_id"].ToString() + "' ";
                if (oPayment.SP_PAYMENT_REPORT_SEL(strCriteria2, ref ds, ref strMessage))
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ViewState["report_code"] = dt.Rows[0]["report_code"].ToString();
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

            if (Request.QueryString["criteria"] != null)
            {
                ViewState["criteria"] = HttpUtility.HtmlDecode(Request.QueryString["criteria"].ToString());
            }
            else
            {
                ViewState["criteria"] = string.Empty;
            }

            if (Request.QueryString["item_des"] != null)
            {
                ViewState["item_des"] = Request.QueryString["item_des"].ToString();
            }
            else
            {
                ViewState["item_des"] = "0";
            }

            if (Request.QueryString["person_retiregroup"] != null)
            {
                ViewState["person_retiregroup"] = Request.QueryString["person_retiregroup"].ToString();
            }
            else
            {
                ViewState["person_retiregroup"] = "";
            }


        }

        private void Retive_Rep_payment_slip()
        {
            try
            {
                string strPath = "~/reports/" + ViewState["report_code"].ToString() + ".rpt";
                rptSource.Load(Server.MapPath(strPath));
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

                cPayment oPayment = new cPayment();
                DataSet ds = new DataSet();
                DataSet dsC = new DataSet();
                DataSet dsD = new DataSet();
                string strMessage = string.Empty;
                string strCriteria = string.Empty;
                if (!oPayment.SP_REP_PAYMENT_CERTIFICATE_SEL(
                    Session["criteria"].ToString().Replace("ph.",""),
                    ViewState["months"].ToString(),
                    ViewState["year"].ToString(),
                    "D",
                    ref dsD, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                if (!oPayment.SP_REP_PAYMENT_CERTIFICATE_SEL(
                     Session["criteria"].ToString().Replace("ph.", ""),
                     ViewState["months"].ToString(),
                     ViewState["year"].ToString(),
                     "C",
                     ref dsC, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                if (!oPayment.SP_REP_PAYMENT_MAIN_CERTIFICATE_SEL(
                      Session["criteria"].ToString(),
                     ViewState["months"].ToString(),
                     ViewState["year"].ToString(),
                     ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                rptSource.SetDataSource(ds.Tables[0]);
                rptSource.OpenSubreport("sub_debit").SetDataSource(dsD.Tables[0]);
                rptSource.OpenSubreport("sub_credit").SetDataSource(dsC.Tables[0]);

                rptSource.SetParameterValue("UserName", strUsername);
                rptSource.SetParameterValue("CompanyName", strCompanyname);
                rptSource.SetParameterValue("pMonth", getMonth());
                rptSource.SetParameterValue("pYear", ViewState["year"].ToString());

                CrystalReportViewer1.LogOnInfo = tableLogOnInfos;

                oPayment.Dispose();
                ds.Dispose();
                dsC.Dispose();
                dsD.Dispose();


            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
        }

    }
}
