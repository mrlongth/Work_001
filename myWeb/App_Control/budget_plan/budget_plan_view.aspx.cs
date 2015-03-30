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

namespace myWeb.App_Control.budget_plan
{
    public partial class budget_plan_view : PageBase
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
                Session["menupopup_name"] = "แสดงข้อมูลผังงบประมาณ";
                #region set QueryString
                if (Request.QueryString["budget_plan_code"] != null)
                {
                    ViewState["budget_plan_code"] = Request.QueryString["budget_plan_code"].ToString();
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
            cBudget_plan obudget_plan = new cBudget_plan();
            DataSet ds = new DataSet();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strbudget_plan_code = string.Empty,
                strunit_code= string.Empty,
                stractivity_code= string.Empty,
                strplan_code= string.Empty,
                strwork_code= string.Empty,
                strfund_code= string.Empty,
                strlot_code= string.Empty,
                strunit_name= string.Empty,
                strdirector_code= string.Empty,
                strdirector_name= string.Empty,
                stractivity_name= string.Empty,
                strproduce_code= string.Empty,
                strproduce_name= string.Empty,
                strbudget_code= string.Empty,
                strbudget_name= string.Empty,
                strplan_name= string.Empty,
                strwork_name= string.Empty,
                strfund_name= string.Empty,
                strlot_name = string.Empty,
                strYear = string.Empty,
                strC_active = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty,
                strCreatedDate = string.Empty,
                strUpdatedDate = string.Empty;
                
            try
            {
                strCriteria = " and budget_plan_code = '" + ViewState["budget_plan_code"].ToString() + "' ";
                if (!obudget_plan.SP_BUDGET_PLAN_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region get Data
                        strbudget_plan_code = ds.Tables[0].Rows[0]["budget_plan_code"].ToString();
                        strunit_code = ds.Tables[0].Rows[0]["unit_code"].ToString();
                        strunit_name = ds.Tables[0].Rows[0]["unit_name"].ToString();
                        stractivity_code = ds.Tables[0].Rows[0]["activity_code"].ToString();
                        stractivity_name = ds.Tables[0].Rows[0]["activity_name"].ToString();
                        strplan_code = ds.Tables[0].Rows[0]["plan_code"].ToString();
                        strplan_name = ds.Tables[0].Rows[0]["plan_name"].ToString();
                        strwork_code = ds.Tables[0].Rows[0]["work_code"].ToString();
                        strwork_name = ds.Tables[0].Rows[0]["work_name"].ToString();
                        strfund_code = ds.Tables[0].Rows[0]["fund_code"].ToString();
                        strfund_name = ds.Tables[0].Rows[0]["fund_name"].ToString();
                        //strlot_code = ds.Tables[0].Rows[0]["lot_code"].ToString();
                        //strlot_name = ds.Tables[0].Rows[0]["lot_name"].ToString();
                        strdirector_code = ds.Tables[0].Rows[0]["director_code"].ToString();
                        strdirector_name = ds.Tables[0].Rows[0]["director_name"].ToString();
                        strproduce_code = ds.Tables[0].Rows[0]["produce_code"].ToString();
                        strproduce_name = ds.Tables[0].Rows[0]["produce_name"].ToString();
                        strbudget_code = ds.Tables[0].Rows[0]["budget_code"].ToString();
                        strbudget_name = ds.Tables[0].Rows[0]["budget_name"].ToString();
                        strYear = ds.Tables[0].Rows[0]["budget_plan_year"].ToString();
                        strC_active = ds.Tables[0].Rows[0]["c_active"].ToString();
                        strCreatedBy = ds.Tables[0].Rows[0]["c_created_by"].ToString();
                        strUpdatedBy = ds.Tables[0].Rows[0]["c_updated_by"].ToString();
                        strCreatedDate = ds.Tables[0].Rows[0]["d_created_date"].ToString();
                        strUpdatedDate = ds.Tables[0].Rows[0]["d_updated_date"].ToString();
                        #endregion

                        #region set Control
                        txtbudget_plan_code.Text = strbudget_plan_code;
                        txtunit_code.Text =strunit_code ;
                        txtunit_name.Text = strunit_name;
                        txtactivity_code.Text = stractivity_code;
                        txtactivity_name.Text = stractivity_name;
                        txtplan_code.Text = strplan_code;
                        txtplan_name.Text = strplan_name;
                        txtwork_code.Text = strwork_code;
                        txtwork_name.Text = strwork_name;
                        txtfund_code.Text = strfund_code;
                        txtfund_name.Text = strfund_name;
                        //txtlot_code.Text = strlot_code;
                        //txtlot_name.Text = strlot_name;
                        txtdirector_code.Text = strdirector_code;
                        txtdirector_name.Text = strdirector_name;
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