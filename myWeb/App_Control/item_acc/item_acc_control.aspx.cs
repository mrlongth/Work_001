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

namespace myWeb.App_Control.item_acc
{
    public partial class item_acc_control : PageBase
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
                imgSaveOnly.Attributes.Add("onMouseOver", "src='../../images/controls/save2.jpg'");
                imgSaveOnly.Attributes.Add("onMouseOut", "src='../../images/controls/save.jpg'");

                InitcboYear();
                #region set QueryString
                if (Request.QueryString["item_acc_id"] != null)
                {
                    ViewState["item_acc_id"] = Request.QueryString["item_acc_id"].ToString();
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
                    Session["menupopup_name"] =  "เพิ่มข้อมูลบัญชี ";
                    //txtitem_acc_code.ReadOnly = true;
                    //txtitem_acc_code.CssClass = "textboxdis";
                    chkStatus.Checked = true;
                }

                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    setData();
                    txtitem_acc_code.ReadOnly = true;
                    txtitem_acc_code.CssClass = "textboxdis";
                    Session["menupopup_name"] = "แก้ไขข้อมูลบัญชี ";
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
        }
        #endregion

        private bool saveData()
        {
            bool blnResult = false;
            bool blnDup = false;
            string strMessage = string.Empty;
            string stritem_acc_code = string.Empty,
                stritem_acc_year = string.Empty,
                stritem_acc_name = string.Empty,
                strActive = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty;
            string strScript = string.Empty;
            cItem_acc oItem_acc = new cItem_acc();
            DataSet ds = new DataSet();
            try
            {
                #region set Data
                stritem_acc_code = txtitem_acc_code.Text.Trim();
                stritem_acc_name = txtitem_acc_name.Text;
                stritem_acc_year = cboYear.SelectedItem.Value;
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
                        if (oItem_acc.SP_ITEM_ACC_UPD(hdditem_acc_id.Value.ToString(),stritem_acc_code, 
                                stritem_acc_year,stritem_acc_name, strActive, strUpdatedBy, ref strMessage))
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
                    strCheckDup = " and item_acc_name = '" + stritem_acc_name.Trim() + "' and item_acc_year = '" + stritem_acc_year  + "' ";
                    if (!oItem_acc.SP_ITEM_ACC_SEL(strCheckDup, ref ds, ref strMessage))
                    {
                        lblError.Text = strMessage;
                    }
                    else
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            strScript = 
                                "alert(\"ไม่สามารถเพิ่มข้อมูล เนื่องจากข้อมูล " + stritem_acc_name.Trim() + " ปี " + stritem_acc_year.Trim() + "  ซ้ำ\");\n" ;
                            blnDup = true;
                        }
                    }
                    #endregion
                    #region insert
                    if (!blnDup)
                    {
                        if (oItem_acc.SP_ITEM_ACC_INS(stritem_acc_year, stritem_acc_code,stritem_acc_name, strActive, strCreatedBy, ref strMessage))
                        {
                            string  strGetcode = " and item_acc_name = '" + stritem_acc_name.Trim() + "' and item_acc_year = '" + stritem_acc_year + "' ";
                            if (!oItem_acc.SP_ITEM_ACC_SEL(strGetcode, ref ds, ref strMessage))
                            {
                                lblError.Text = strMessage;
                            }
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                stritem_acc_code = ds.Tables[0].Rows[0]["item_acc_id"].ToString();
                            }
                            ViewState["item_acc_id"] = stritem_acc_code;
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
                oItem_acc.Dispose();
            }
            return blnResult;
        }

        private void imgSaveOnly_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (saveData())
            {
                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    txtitem_acc_code.Text = string.Empty;
                    txtitem_acc_name.Text = string.Empty;
                    txtitem_acc_name.Focus();
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
            cItem_acc oItem_acc = new cItem_acc();
            DataSet ds = new DataSet();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string stritem_acc_code = string.Empty,
                stritem_acc_name = string.Empty,
                strYear = string.Empty,
                strC_active = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty,
                strCreatedDate = string.Empty,
                strUpdatedDate = string.Empty;
            try
            {
                strCriteria = " and item_acc_id = '" + ViewState["item_acc_id"].ToString() + "' ";
                if (!oItem_acc.SP_ITEM_ACC_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region get Data
                        stritem_acc_code = ds.Tables[0].Rows[0]["item_acc_code"].ToString();
                        stritem_acc_name = ds.Tables[0].Rows[0]["item_acc_name"].ToString();
                        strYear = ds.Tables[0].Rows[0]["item_acc_year"].ToString();
                        strC_active = ds.Tables[0].Rows[0]["c_active"].ToString();
                        strCreatedBy = ds.Tables[0].Rows[0]["c_created_by"].ToString();
                        strUpdatedBy = ds.Tables[0].Rows[0]["c_updated_by"].ToString();
                        strCreatedDate = ds.Tables[0].Rows[0]["d_created_date"].ToString();
                        strUpdatedDate = ds.Tables[0].Rows[0]["d_updated_date"].ToString();
                        #endregion

                        #region set Control
                        hdditem_acc_id.Value = ds.Tables[0].Rows[0]["item_acc_id"].ToString();
                        txtitem_acc_code.Text = stritem_acc_code;
                        txtitem_acc_name.Text = stritem_acc_name;
                        InitcboYear();
                        if (cboYear.Items.FindByValue(strYear) != null)
                        {
                            cboYear.SelectedIndex = -1;
                            cboYear.Items.FindByValue(strYear).Selected = true;
                        }
                        if (strC_active.Equals("Y"))
                        {
                            txtitem_acc_name.ReadOnly = false;
                            txtitem_acc_name.CssClass = "textbox";
                            chkStatus.Checked = true;
                        }
                        else
                        {
                            txtitem_acc_name.ReadOnly = true;
                            txtitem_acc_name.CssClass = "textboxdis";
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