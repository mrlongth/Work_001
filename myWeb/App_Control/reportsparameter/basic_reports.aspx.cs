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
    
    public partial class basic_reports : PageBase
    {
        private ReportDocument rptSource = new ReportDocument();

        protected void Page_Load(object sender, EventArgs e)
        {
            lblError.Text = string.Empty;
            if (!IsPostBack)
            {
                getQueryString();
                RetiveReportStore();
            }
        }

        protected void Page_Init(object sender, EventArgs e) 
        {
            getQueryString();
            RetiveReportStore();
        }
        
        private void getQueryString()
        {
            if (Request.QueryString["sp_name"] != null)
            {
                ViewState["sp_name"] = Request.QueryString["sp_name"].ToString();
            }
            else
            {
                ViewState["sp_name"] = string.Empty;
            }

            if (Request.QueryString["report_name"] != null)
            {
                ViewState["report_name"] = Request.QueryString["report_name"].ToString();
            }
            else
            {
                ViewState["report_name"] = string.Empty;
            }

            if (Request.QueryString["report_des"] != null)
            {
                ViewState["report_des"] = Request.QueryString["report_des"].ToString();
            }
            else
            {
                ViewState["report_des"] = string.Empty;
            }

            if (Request.QueryString["criteria"] != null)
            {
                ViewState["criteria"] = Request.QueryString["criteria"].ToString();
            }
            else
            {
                ViewState["criteria"] = string.Empty;
            }
        }

        private void RetiveReportStore()
        {
            try
            {
                string strPath = "~/reports/" + ViewState["report_name"].ToString();
                rptSource.Load(Server.MapPath(strPath));
                TableLogOnInfo logOnInfo = new TableLogOnInfo();
                TableLogOnInfos tableLogOnInfos = new TableLogOnInfos();
                string strUsername = Session["username"].ToString();
                string strCompanyname = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["companyname"].ToString();
                string strServername = System.Configuration.ConfigurationSettings.AppSettings["servername"];
                string strDbname = System.Configuration.ConfigurationSettings.AppSettings["dbname"];
                string strDbuser = System.Configuration.ConfigurationSettings.AppSettings["dbuser"];
                string strDbpassword = System.Configuration.ConfigurationSettings.AppSettings["dbpassword"];
                this.Title = ViewState["report_des"].ToString();
                logOnInfo.ConnectionInfo.ServerName = strServername;
                logOnInfo.ConnectionInfo.DatabaseName = strDbname;
                logOnInfo.ConnectionInfo.UserID = strDbuser;
                logOnInfo.ConnectionInfo.Password = strDbpassword;
                tableLogOnInfos.Add(logOnInfo);
                rptSource.SetParameterValue("@vc_criteria", ViewState["criteria"].ToString());
                rptSource.SetParameterValue("UserName", strUsername);
                rptSource.SetParameterValue("CompanyName", strCompanyname);
                CrystalReportViewer1.ReportSource = rptSource;
                CrystalReportViewer1.LogOnInfo = tableLogOnInfos;
              //  CrystalReportViewer1.DataBind();
              //  Response.Write(ViewState["criteria"].ToString());
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
        }
    
    }
}
