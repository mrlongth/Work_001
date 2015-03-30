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

namespace myWeb.App_Control.member_type
{
    public partial class member_type_view : PageBase
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
                Session["menupopup_name"] = "แสดงข้อมูลประเภทสมาชิก";

                #region set QueryString
                if (Request.QueryString["member_type_code"] != null)
                {
                    ViewState["member_type_code"] = Request.QueryString["member_type_code"].ToString();
                    setData();
                }

                #endregion

                //imgClose.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgClose"].Rows[0]["img"].ToString();
                //imgClose.Attributes.Add("member_type", ((DataSet)Application["xmlconfig"]).Tables["imgClose"].Rows[0]["title"].ToString());
                //imgClose.Attributes.Add("onclick", "window.close();");
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
            cMember_type oMember_type = new cMember_type();
            DataSet ds = new DataSet();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strmember_type_code = string.Empty,
                strmember_type_name = string.Empty,
                strmember_type_rate = string.Empty,
                strC_active = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty,
                strCreatedDate = string.Empty,
                strUpdatedDate = string.Empty;
            try
            {
                strCriteria = " and member_type_code = '" + ViewState["member_type_code"].ToString() + "' ";
                if (!oMember_type.SP_MEMBER_TYPE_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region get Data
                        strmember_type_code = ds.Tables[0].Rows[0]["member_type_code"].ToString();
                        strmember_type_name = ds.Tables[0].Rows[0]["member_type_name"].ToString();
                        strmember_type_rate = String.Format("{0:0.00}", float.Parse(ds.Tables[0].Rows[0]["member_type_rate"].ToString()));
                        strC_active = ds.Tables[0].Rows[0]["c_active"].ToString();
                        strCreatedBy = ds.Tables[0].Rows[0]["c_created_by"].ToString();
                        strUpdatedBy = ds.Tables[0].Rows[0]["c_updated_by"].ToString();
                        strCreatedDate = ds.Tables[0].Rows[0]["d_created_date"].ToString();
                        strUpdatedDate = ds.Tables[0].Rows[0]["d_updated_date"].ToString();
                        #endregion

                        #region set Control
                        txtmember_type_code.Text = strmember_type_code;
                        txtmember_type_name.Text = strmember_type_name;
                        txtmember_type_rate.Text = strmember_type_rate;
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