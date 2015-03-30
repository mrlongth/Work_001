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

namespace myWeb.App_Control.branch
{
    public partial class branch_view : PageBase
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            //if (Session["username"] == null)
            //{
            //    string strScript = "<script language=\"javascript\">\n self.opener.document.location.href=\"../../index.aspx\";\n self.close();\n</script>\n";
            //    this.RegisterStartupScript("close", strScript);
            //    return;
            //}
            lblError.Text = "";
            if (!IsPostBack)
            {
                Session["menupopup_name"] = "แสดงข้อมูลสาขา";

                #region set QueryString
                if (Request.QueryString["branch_code"] != null)
                {
                    ViewState["branch_code"] = Request.QueryString["branch_code"].ToString();
                    setData();
                }

                #endregion

                //imgClose.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgClose"].Rows[0]["title"].ToString());
                //imgClose.Attributes.Add("onclick", "ClosePopUp('1');return false;");
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
            cBranch oBranch = new cBranch();
            DataSet ds = new DataSet();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strbranch_code = string.Empty,
                strbranch_name = string.Empty,
                strbank_code = string.Empty,
                strbank_name = string.Empty,
                strc_active = string.Empty,
                strcreatedBy = string.Empty,
                strupdatedBy = string.Empty,
                strcreatedDate = string.Empty,
                strupdatedDate = string.Empty;
                
            try
            {
                strCriteria = " and branch_code = '" + ViewState["branch_code"].ToString() + "' ";
                if (!oBranch.SP_SEL_BRANCH(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region get Data
                        strbank_code = ds.Tables[0].Rows[0]["branch_code"].ToString();
                        strbranch_name = ds.Tables[0].Rows[0]["branch_name"].ToString();
                        strbank_code = ds.Tables[0].Rows[0]["bank_code"].ToString();
                        strbank_name = ds.Tables[0].Rows[0]["bank_name"].ToString();
                        strc_active = ds.Tables[0].Rows[0]["c_active"].ToString();
                        strcreatedBy = ds.Tables[0].Rows[0]["c_created_by"].ToString();
                        strupdatedBy = ds.Tables[0].Rows[0]["c_updated_by"].ToString();
                        strcreatedDate = ds.Tables[0].Rows[0]["d_created_date"].ToString();
                        strupdatedDate = ds.Tables[0].Rows[0]["d_updated_date"].ToString();
                        #endregion

                        #region set Control
                        txtbranch_code.Text = strbank_name;
                        txtbranch_name.Text = strbranch_name;
                        txtbank_code.Text = strbank_code;
                        txtbank_name.Text = strbank_name;
                        if (strc_active.Equals("Y"))
                        {
                            chkStatus.Checked = true;
                        }
                        else
                        {
                            chkStatus.Checked = false;
                        }
                        txtUpdatedBy.Text = strupdatedBy;
                        txtUpdatedDate.Text = strupdatedDate;
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

       
    }
}