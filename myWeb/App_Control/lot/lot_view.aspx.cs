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

namespace myWeb.App_Control.lot
{
    public partial class lot_view : PageBase
    {

        private string BudgetType
        {
            get
            {
                if (ViewState["BudgetType"] == null)
                {
                    ViewState["BudgetType"] = Helper.CStr(Request.QueryString["budget_type"]);
                }
                return ViewState["BudgetType"].ToString();
            }
            set
            {
                ViewState["BudgetType"] = value;
            }
        }
        
        
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
                Session["menupopup_name"] = "แสดงข้อมูลงบประมาณ";

                #region set QueryString
                if (Request.QueryString["lot_code"] != null)
                {
                    ViewState["lot_code"] = Request.QueryString["lot_code"].ToString();
                    setData();
                }

                #endregion

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
            cLot olot = new cLot();
            DataSet ds = new DataSet();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strlot_code = string.Empty,
                strlot_name = string.Empty,
                strYear = string.Empty,
                strC_active = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty,
                strCreatedDate = string.Empty,
                strUpdatedDate = string.Empty;
                
            try
            {
                strCriteria = " and lot_code = '" + ViewState["lot_code"].ToString() + "' ";
                if (!olot.SP_SEL_LOT(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region get Data
                        strlot_code = ds.Tables[0].Rows[0]["lot_code"].ToString();
                        strlot_name = ds.Tables[0].Rows[0]["lot_name"].ToString();
                        strYear = ds.Tables[0].Rows[0]["lot_year"].ToString();
                        strC_active = ds.Tables[0].Rows[0]["c_active"].ToString();
                        strCreatedBy = ds.Tables[0].Rows[0]["c_created_by"].ToString();
                        strUpdatedBy = ds.Tables[0].Rows[0]["c_updated_by"].ToString();
                        strCreatedDate = ds.Tables[0].Rows[0]["d_created_date"].ToString();
                        strUpdatedDate = ds.Tables[0].Rows[0]["d_updated_date"].ToString();
                        #endregion

                        #region set Control
                        txtlot_code.Text = strlot_code;
                        txtlot_name.Text = strlot_name;
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