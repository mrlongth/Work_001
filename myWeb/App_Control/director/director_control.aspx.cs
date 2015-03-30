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

namespace myWeb.App_Control.director
{
    public partial class director_control : PageBase
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            lblError.Text = "";
            if (!IsPostBack)
            {

                imgperson_pic.Attributes.Add("onclick", "OpenPopUp('500px','200px','85%','อัพโหลดรูปลายเซ็นต์' ,'../director/sign_upload.aspx?" +
                                                    "ctrl1=" + txtdirector_sign_image.ClientID + "&ctrl2=" + imgPerson.ClientID + "&show=2', '2');return false;");
                imgClear_person_pic.Attributes.Add("onclick", "document.getElementById('" + txtdirector_sign_image.ClientID + "').value='';" +
                                                                                                             "document.getElementById('" + imgPerson.ClientID + "').src='../../person_pic/image_n_a2.jpg';return false;");


                imgSaveOnly.Attributes.Add("onMouseOver", "src='../../images/controls/save2.jpg'");
                imgSaveOnly.Attributes.Add("onMouseOut", "src='../../images/controls/save.jpg'");
                InitcboBudgetType();

                InitcboYear();
                #region set QueryString
                if (Request.QueryString["director_code"] != null)
                {
                    ViewState["director_code"] = Request.QueryString["director_code"].ToString();
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
                    Session["menupopup_name"] = "เพิ่มข้อมูลสังกัด ";
                    txtdirector_code.ReadOnly = true;
                    txtdirector_code.CssClass = "textboxdis";
                    chkStatus.Checked = true;
                }

                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    setData();
                    txtdirector_code.ReadOnly = true;
                    txtdirector_code.CssClass = "textboxdis";
                    Session["menupopup_name"] = "แก้ไขข้อมูลสังกัด ";
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

        private void InitcboBudgetType()
        {
            cCommon oCommon = new cCommon();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strCode = cboBudget_type.SelectedValue;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " Select * from  general where g_type = 'budget_type'  Order by g_sort ";
            if (oCommon.SEL_SQL(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboBudget_type.Items.Clear();
                int i;
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboBudget_type.Items.Add(new ListItem(dt.Rows[i]["g_name"].ToString(), dt.Rows[i]["g_code"].ToString()));
                }
                if (cboBudget_type.Items.FindByValue(strCode) != null)
                {
                    cboBudget_type.SelectedIndex = -1;
                    cboBudget_type.Items.FindByValue(strCode).Selected = true;
                }
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
            string strdirector_code = string.Empty,
                strdirector_year = string.Empty,
                strdirector_name = string.Empty,
                strdirector_sign_name = string.Empty,
                strdirector_sign_image = string.Empty,
                strsign_position = string.Empty,
                strActive = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty,
                strBudget_type = string.Empty;
            string strScript = string.Empty;
            cDirector oDirector = new cDirector();
            DataSet ds = new DataSet();
            try
            {
                #region set Data
                strdirector_code = txtdirector_code.Text.Trim();
                strdirector_name = txtdirector_name.Text;
                strdirector_year = cboYear.SelectedItem.Value;
                strdirector_sign_name = txtdirector_sign_name.Text;
                strsign_position = txtsign_position.Text;
                strBudget_type = cboBudget_type.SelectedItem.Value;
                if (txtdirector_sign_image.Text.Length > 0)
                {
                    strdirector_sign_image = MapPath("~/person_pic/" + txtdirector_sign_image.Text);
                }

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
                        if (strdirector_sign_image != "")
                        {
                            if (oDirector.SP_UPD_DIRECTOR(strdirector_code, strdirector_year, strdirector_name, strdirector_sign_name, strdirector_sign_image, strsign_position, txtdirector_order.Value.ToString(), strActive, strUpdatedBy, strBudget_type, ref strMessage))
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
                            if (oDirector.SP_UPD_NOIMAGE_DIRECTOR(strdirector_code, strdirector_year, strdirector_name, strdirector_sign_name, strsign_position, txtdirector_order.Value.ToString(), strActive, strUpdatedBy, strBudget_type, ref strMessage))
                            {
                                blnResult = true;
                            }
                            else
                            {
                                lblError.Text = strMessage.ToString();
                            }
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
                    strCheckDup = " and director_name = '" + strdirector_name.Trim() + "' and director_year = '" + strdirector_year + "' ";
                    if (!oDirector.SP_SEL_DIRECTOR(strCheckDup, ref ds, ref strMessage))
                    {
                        lblError.Text = strMessage;
                    }
                    else
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            strScript =
                                "alert(\"ไม่สามารถเพิ่มข้อมูล เนื่องจากข้อมูล " + strdirector_name.Trim() + " ปี " + strdirector_year.Trim() + "  ซ้ำ\");\n";
                            blnDup = true;
                        }
                    }
                    #endregion
                    #region insert
                    if (!blnDup)
                    {
                        if (oDirector.SP_INS_DIRECTOR(strdirector_year, strdirector_name, strdirector_sign_name, strdirector_sign_image, strsign_position, txtdirector_order.Value.ToString(), strActive, strCreatedBy, strBudget_type, ref strMessage))
                        {
                            string strGetcode = " and director_name = '" + strdirector_name.Trim() + "' and director_year = '" + strdirector_year + "' ";
                            if (!oDirector.SP_SEL_DIRECTOR(strGetcode, ref ds, ref strMessage))
                            {
                                lblError.Text = strMessage;
                            }
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                strdirector_code = ds.Tables[0].Rows[0]["director_code"].ToString();
                            }
                            ViewState["director_code"] = strdirector_code;
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
                oDirector.Dispose();
            }
            return blnResult;
        }

        private void imgSaveOnly_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (saveData())
            {
                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    txtdirector_code.Text = string.Empty;
                    txtdirector_name.Text = string.Empty;
                    txtdirector_name.Focus();
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
            cDirector oDirector = new cDirector();
            DataSet ds = new DataSet();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strdirector_code = string.Empty,
                strdirector_name = string.Empty,
                strdirector_sign_name = string.Empty,
                strsign_position = string.Empty,
                strdirector_order = string.Empty,
                strYear = string.Empty,
                strC_active = string.Empty,
                strBudget_type = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty,
                strCreatedDate = string.Empty,
                strUpdatedDate = string.Empty;

            try
            {
                strCriteria = " and director_code = '" + ViewState["director_code"].ToString() + "' ";
                if (!oDirector.SP_SEL_DIRECTOR(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region get Data
                        strdirector_code = ds.Tables[0].Rows[0]["director_code"].ToString();
                        strdirector_name = ds.Tables[0].Rows[0]["director_name"].ToString();
                        strdirector_sign_name = Helper.CStr(ds.Tables[0].Rows[0]["director_sign_name"]);
                        strsign_position = Helper.CStr(ds.Tables[0].Rows[0]["sign_position"]);
                        strYear = ds.Tables[0].Rows[0]["director_year"].ToString();
                        strC_active = ds.Tables[0].Rows[0]["c_active"].ToString();
                        strCreatedBy = ds.Tables[0].Rows[0]["c_created_by"].ToString();
                        strUpdatedBy = ds.Tables[0].Rows[0]["c_updated_by"].ToString();
                        strCreatedDate = ds.Tables[0].Rows[0]["d_created_date"].ToString();
                        strUpdatedDate = ds.Tables[0].Rows[0]["d_updated_date"].ToString();
                        strBudget_type = ds.Tables[0].Rows[0]["budget_type"].ToString();
                        strdirector_order = ds.Tables[0].Rows[0]["director_order"].ToString();
                        #endregion

                        #region set Control
                        txtdirector_code.Text = strdirector_code;
                        txtdirector_name.Text = strdirector_name;
                        txtdirector_sign_name.Text = strdirector_sign_name;
                        txtsign_position.Text = strsign_position;
                        txtdirector_order.Value = strdirector_order;
                        InitcboYear();
                        if (cboYear.Items.FindByValue(strYear) != null)
                        {
                            cboYear.SelectedIndex = -1;
                            cboYear.Items.FindByValue(strYear).Selected = true;
                        }
                        if (strC_active.Equals("Y"))
                        {
                            txtdirector_name.ReadOnly = false;
                            txtdirector_name.CssClass = "textbox";
                            chkStatus.Checked = true;
                        }
                        else
                        {
                            txtdirector_name.ReadOnly = true;
                            txtdirector_name.CssClass = "textboxdis";
                            chkStatus.Checked = false;
                        }

                        InitcboBudgetType();
                        if (cboBudget_type.Items.FindByValue(strBudget_type) != null)
                        {
                            cboBudget_type.SelectedIndex = -1;
                            cboBudget_type.Items.FindByValue(strBudget_type).Selected = true;
                        }


                        cboYear.Enabled = false;
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

        private void imgSave_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            bool blnResult = false;
            string strScript = string.Empty;
            blnResult = saveData();
            if (blnResult)
            {
                strScript =
                    "self.opener.document.forms[0].ctl00$ASPxRoundPanel1$ContentPlaceHolder2$txthpage.value=" + ViewState["page"].ToString() + ";\n" +
                    "self.opener.document.forms[0].submit();\n" +
                    "self.close();\n";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "frMainPage", strScript, true);
                MsgBox("บันทึกข้อมูลสมบูรณ์");
            }
        }
 
    }
}