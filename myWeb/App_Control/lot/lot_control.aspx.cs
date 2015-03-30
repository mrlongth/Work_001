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
    public partial class lot_control : PageBase
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
                imgSaveOnly.Attributes.Add("onMouseOver", "src='../../images/controls/save2.jpg'");
                imgSaveOnly.Attributes.Add("onMouseOut", "src='../../images/controls/save.jpg'");

                InitcboYear();
                #region set QueryString
                if (Request.QueryString["lot_code"] != null)
                {
                    ViewState["lot_code"] = Request.QueryString["lot_code"].ToString();
                }
                if (Request.QueryString["page"] != null)
                {
                    ViewState["page"] = Request.QueryString["page"].ToString();
                }
                if (Request.QueryString["mode"] != null)
                {
                    ViewState["mode"] = Request.QueryString["mode"].ToString();
                }
                if (Request.QueryString["PageStatus"] != null)
                {
                    ViewState["PageStatus"] = Request.QueryString["PageStatus"].ToString();
                }

                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    ViewState["page"] = Request.QueryString["page"];
                    Session["menupopup_name"] =  "เพิ่มข้อมูลงบประมาณ ";
                    txtlot_code.ReadOnly = true;
                    txtlot_code.CssClass = "textboxdis";
                    chkStatus.Checked = true;
                }

                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    setData();
                    txtlot_code.ReadOnly = true;
                    txtlot_code.CssClass = "textboxdis";
                    Session["menupopup_name"] = "แก้ไขข้อมูลงบประมาณ ";
                    if (ViewState["PageStatus"] != null)
                    {
                        if (ViewState["PageStatus"].ToString().ToLower().Equals("save"))
                        {
                            string strScript1 =
                                "self.opener.document.forms[0].ctl00$ASPxRoundPanel1$ContentPlaceHolder2$txthpage.value=" + ViewState["page"].ToString() + ";\n" +
                                "self.opener.document.forms[0].submit();\n" +
                                "self.focus();\n";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "frMainPage", strScript1, true);
                        }
                    }
                }

                #endregion
                imgSaveOnly.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgSaveOnly"].Rows[0]["title"].ToString());
            }
        }

        #region private function

        private void InitcboYear()
        {
            string strYear = string.Empty;
            strYear = cboYear.SelectedValue;
            if (strYear.Equals(""))
            {
                strYear = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["yearnow"].ToString();
            }
            DataTable odt;
            int i;
            cboYear.Items.Clear();
            odt = ((DataSet)Application["xmlconfig"]).Tables["cboYear"];
            for (i = 0; i <= odt.Rows.Count - 1; i++)
            {
                cboYear.Items.Add(new ListItem(odt.Rows[i]["Text"].ToString(), odt.Rows[i]["Value"].ToString()));
            }
            if (cboYear.Items.FindByValue(strYear) != null)
            {
                cboYear.SelectedIndex = -1;
                cboYear.Items.FindByValue(strYear).Selected = true;
            }
        }

        #endregion

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
            this.imgSaveOnly.Click += new System.Web.UI.ImageClickEventHandler(this.imgSaveOnly_Click);
            //this.imgSave.Click += new System.Web.UI.ImageClickEventHandler(this.imgSave_Click);

        }
        #endregion

        private bool saveData()
        {
            bool blnResult = false;
            bool blnDup = false;
            string strMessage = string.Empty;
            string strlot_code = string.Empty,
                strlot_year = string.Empty,
                strlot_name = string.Empty,
                strActive = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty;
            string strScript = string.Empty;
            cLot olot = new cLot();
            DataSet ds = new DataSet();
            try
            {
                #region set Data
                strlot_code = txtlot_code.Text.Trim();
                strlot_name = txtlot_name.Text;
                strlot_year = cboYear.SelectedItem.Value;
                if (chkStatus.Checked == true)
                {
                    strActive = "Y";
                }
                else 
                {
                    strActive = "N";
                }
                strCreatedBy = Session["username"].ToString();
                strUpdatedBy = Session["username"].ToString();
                #endregion
                if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    #region edit
                    if (!blnDup)
                    {
                        if (olot.SP_UPD_LOT(strlot_code, strlot_year, strlot_name, strActive, strUpdatedBy, this.BudgetType, ref strMessage))
                        {
                            blnResult = true;
                        }
                        else
                        {
                            lblError.Text = strMessage.ToString();
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "frMainPage", strScript, true);
                    }
                    #endregion
                }
                else
                {
                    #region check dup
                    string strCheckDup = string.Empty;
                    strCheckDup = " and lot_name = '" + strlot_name.Trim() + "' and lot_year = '" + strlot_year  + "' ";
                    if (!olot.SP_SEL_LOT(strCheckDup, ref ds, ref strMessage))
                    {
                        lblError.Text = strMessage;
                    }
                    else
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            strScript = 
                                "alert(\"ไม่สามารถเพิ่มข้อมูล เนื่องจากข้อมูล " + strlot_name.Trim() + " ปี " + strlot_year.Trim() + "  ซ้ำ\");\n" ;
                            blnDup = true;
                        }
                    }
                    #endregion
                    #region insert
                    if (!blnDup)
                    {
                        if (olot.SP_INS_LOT(strlot_year, strlot_name, strActive, strCreatedBy, this.BudgetType, ref strMessage))
                        {
                            string  strGetcode = " and lot_name = '" + strlot_name.Trim() + "' and lot_year = '" + strlot_year + "' ";
                            if (!olot.SP_SEL_LOT(strGetcode, ref ds, ref strMessage))
                            {
                                lblError.Text = strMessage;
                            }
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                strlot_code = ds.Tables[0].Rows[0]["lot_code"].ToString();
                            }
                            ViewState["lot_code"] = strlot_code;
                            blnResult = true;
                        }
                        else
                        {
                            lblError.Text = strMessage.ToString();
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "frMainPage", strScript, true);
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                olot.Dispose();
            }
            return blnResult;
        }

        private void imgSaveOnly_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (saveData())
            {
                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    txtlot_code.Text = string.Empty;
                    txtlot_name.Text = string.Empty;
                    txtlot_name.Focus();
                    string strScript1 = "RefreshMain('" + ViewState["page"].ToString() + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
                }
                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    string strScript1 = "ClosePopUpListPost('" + ViewState["page"].ToString() + "','1');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
                }
                MsgBox("บันทึกข้อมูลสมบูรณ์");
            }
        }

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
                        InitcboYear();
                        if (cboYear.Items.FindByValue(strYear) != null)
                        {
                            cboYear.SelectedIndex = -1;
                            cboYear.Items.FindByValue(strYear).Selected = true;
                        }
                        if (strC_active.Equals("Y"))
                        {
                            txtlot_name.ReadOnly = false;
                            txtlot_name.CssClass = "textbox";
                            chkStatus.Checked = true;
                        }
                        else
                        {
                            txtlot_name.ReadOnly = true;
                            txtlot_name.CssClass = "textboxdis";
                            chkStatus.Checked = false;
                        }
                        cboYear.Enabled = false ;
                        cboYear.CssClass = "textboxdis";
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