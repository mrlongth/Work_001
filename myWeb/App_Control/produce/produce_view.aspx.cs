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

namespace myWeb.App_Control.produce
{
    public partial class produce_view : PageBase
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
                Session["menupopup_name"] = "แสดงข้อมูลผลผลิต";

                #region set QueryString
                if (Request.QueryString["produce_code"] != null)
                {
                    ViewState["produce_code"] = Request.QueryString["produce_code"].ToString();
                    setData();
                }

                #endregion

                //imgClose.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgClose"].Rows[0]["title"].ToString());
                //imgClose.Attributes.Add("onclick", "window.parent.$find('show_ModalPopupExtender').hide();" +
                //                                                                     "window.parent.document.getElementById('iframeShow').src='about:blank';return false;");
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
            cProduce oproduce = new cProduce();
            DataSet ds = new DataSet();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strproduce_code = string.Empty,
                strproduce_name = string.Empty,
                strbudget_code = string.Empty,
                strbudget_name = string.Empty,
                strYear = string.Empty,
                strC_active = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty,
                strCreatedDate = string.Empty,
                strUpdatedDate = string.Empty;
                
            try
            {
                strCriteria = " and produce_code = '" + ViewState["produce_code"].ToString() + "' ";
                if (!oproduce.SP_SEL_PRODUCE(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region get Data
                        strproduce_code = ds.Tables[0].Rows[0]["produce_code"].ToString();
                        strproduce_name = ds.Tables[0].Rows[0]["produce_name"].ToString();
                        strbudget_code = ds.Tables[0].Rows[0]["budget_code"].ToString();
                        strbudget_name = ds.Tables[0].Rows[0]["budget_name"].ToString();
                        strYear = ds.Tables[0].Rows[0]["produce_year"].ToString();
                        strC_active = ds.Tables[0].Rows[0]["c_active"].ToString();
                        strCreatedBy = ds.Tables[0].Rows[0]["c_created_by"].ToString();
                        strUpdatedBy = ds.Tables[0].Rows[0]["c_updated_by"].ToString();
                        strCreatedDate = ds.Tables[0].Rows[0]["d_created_date"].ToString();
                        strUpdatedDate = ds.Tables[0].Rows[0]["d_updated_date"].ToString();
                        #endregion

                        #region set Control
                        txtproduce_code.Text = strproduce_code;
                        txtproduce_name.Text = strproduce_name;
                        txtbudget_code.Text = strbudget_code;
                        txtbudget_name.Text = strbudget_name;

                        txtyear.Text = strYear;
                        if (strC_active.Equals("Y"))
                        {
                            chkStatus.Checked = true;
                        }
                        else
                        {
                            chkStatus.Checked = false;
                        }
                        txtUpdatedBy.Text = strUpdatedBy;
                        txtUpdatedDate.Text = strUpdatedDate;
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