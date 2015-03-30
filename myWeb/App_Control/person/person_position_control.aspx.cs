using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using myDLL;

namespace myWeb.App_Control.person
{
    public partial class person_position_control : PageBase
    {
        #region private data
        private string strConn = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"];
        private bool[] blnAccessRight = new bool[5] { false, false, false, false, false };
        private string strPrefixCtr = "ctl00$ContentPlaceHolder1$";
        #endregion

        private void StoreDataFromJS()
        {
            if (Request.Form[strPrefixCtr + "txtchange_date"] != null)
            {
                txtchange_date.Text = Request.Form[strPrefixCtr + "txtchange_date"].ToString();
            }
            else
            {
                txtchange_date.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
           // AjaxPro.Utility.RegisterTypeForAjax(typeof(person_position_control));
            lblError.Text = "";
            if (!IsPostBack)
            {
                #region set QueryString
                if (Request.QueryString["person_code"] != null)
                {
                    ViewState["person_code"] = Request.QueryString["person_code"].ToString();
                    lblperson_code.Text = ViewState["person_code"].ToString();
                }
                if (Request.QueryString["person_name"] != null)
                {
                    ViewState["person_name"] = Request.QueryString["person_name"].ToString();
                    lblperson_name.Text = ViewState["person_name"].ToString();
                }
                if (Request.QueryString["change_date"] != null)
                {
                    ViewState["change_date"] = Request.QueryString["change_date"].ToString();
                    txtchange_date.Text = cCommon.CheckDate(ViewState["change_date"].ToString());
                }

                if (Request.QueryString["position_code"] != null)
                {
                    ViewState["position_code"] = Request.QueryString["position_code"].ToString();
                }
        
                if (Request.QueryString["position_name"] != null)
                {
                    ViewState["position_name"] = Request.QueryString["position_name"].ToString();
                }
                if (Request.QueryString["person_salary"] != null)
                {
                    ViewState["person_salary"] = Request.QueryString["person_salary"].ToString();
                }

                if (Request.QueryString["person_level"] != null)
                {
                    ViewState["person_level"] = Request.QueryString["person_level"].ToString();
                }
                else
                {
                    ViewState["person_level"] = string.Empty;  
                }
                if (Request.QueryString["person_salary"] != null)
                {
                    ViewState["person_salary"] = Request.QueryString["person_salary"].ToString();
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
                    txtchange_date.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtposition_old.Text = ViewState["position_code"].ToString();
                    txtposition_old_name.Text = ViewState["position_name"].ToString();
                    txtsalary_old.Text = ViewState["person_salary"].ToString();
                    txtlevel_old.Text = ViewState["person_level"].ToString();
                }
                else
                {
                    setData();
                }

                #endregion

                #region Set Image

                imgList_position_old.Attributes.Add("onclick", "OpenPopUp('800px','400px','93%','ค้นหาข้อมูลตำแหน่งเดิม' ,'../lov/position_lov.aspx?" +
                                                                "position_code='+document.getElementById('" + txtposition_old.ClientID + "').value+'&" +
                                                                "position_name='+document.getElementById('" + txtposition_old_name.ClientID + "').value+'&" +
                                                                "ctrl1=" + txtposition_old.ClientID + "&ctrl2=" + txtposition_old_name.ClientID + "&show=3', '3');return false;");

                imgClear_position_old.Attributes.Add("onclick", "document.getElementById('" + txtposition_old.ClientID + "').value='';" +
                                                                                                     "document.getElementById('" + txtposition_old_name.ClientID + "').value='';return false;");

                imgList_position_new.Attributes.Add("onclick", "OpenPopUp('800px','400px','93%','ค้นหาข้อมูลตำแหน่งใหม่' ,'../lov/position_lov.aspx?" +
                                                "position_code='+document.getElementById('" + txtposition_new.ClientID + "').value+'&" +
                                                "position_name='+document.getElementById('" + txtposition_new_name.ClientID + "').value+'&" +
                                                "ctrl1=" + txtposition_new.ClientID + "&ctrl2=" + txtposition_new_name.ClientID + "&show=3', '3');return false;");

                imgClear_position_new.Attributes.Add("onclick", "document.getElementById('" + txtposition_new.ClientID + "').value='';" +
                                                                                                     "document.getElementById('" + txtposition_new_name.ClientID + "').value='';return false;");

                #endregion

                txtsalary_new.Attributes.Add("onblur", "chkDecimal(this,2,',')");
                txtsalary_old.Attributes.Add("onblur", "chkDecimal(this,2,',')");
                imgSaveOnly.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgSaveOnly"].Rows[0]["title"].ToString());
                chkStatus.Checked = true;
                txtchange_date.Focus();
            }
            else
            {
                StoreDataFromJS();
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
            this.imgSaveOnly.Click += new System.Web.UI.ImageClickEventHandler(this.imgSaveOnly_Click);
        }
        #endregion

        private bool saveData()
        {
            bool blnResult = false;
            bool blnDup = false;
            string strMessage = string.Empty;
            string strperson_code = string.Empty,
                strchange_date= string.Empty,
                strsalary_old= string.Empty,
                strsalary_new= string.Empty,
                strposition_old= string.Empty,
                strposition_new = string.Empty,
                strlevel_old = string.Empty,
                strlevel_new = string.Empty,
                strActive = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty;
            string strScript = string.Empty;
            cPerson oPerson = new cPerson();
            DataSet ds = new DataSet();
            try
            {
                #region set Data
                strperson_code = lblperson_code.Text.Trim();
                strchange_date = txtchange_date.Text.Trim();
                strsalary_old = txtsalary_old.Text.Trim();
                strsalary_new = txtsalary_new.Text.Trim();
                strposition_old = txtposition_old.Text.Trim();
                strposition_new = txtposition_new.Text.Trim();
                strlevel_old = txtlevel_old.Text.Trim();
                strlevel_new = txtlevel_new.Text.Trim();
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
                        if (oPerson.SP_PERSON_POSITION_UPD(strperson_code, strchange_date, strsalary_old, strsalary_new,
                                                                                                    strposition_old, strposition_new, strlevel_old, strlevel_new,strActive, strUpdatedBy, ref strMessage))
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
                    strCheckDup = " and person_code = '" + strperson_code.Trim() + "' " +
                                                  " and change_date = '" + cCommon.SeekDate(strchange_date.Trim()) + "' ";
                    if (!oPerson.SP_PERSON_POSITION_SEL(strCheckDup, ref ds, ref strMessage))
                    {
                        lblError.Text = strMessage;
                    }
                    else
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            strScript =
                                "alert(\"ไม่สามารถเพิ่มข้อมูล เนื่องจากข้อมูล " + strchange_date.Trim()  + "  ซ้ำ\");\n";
                            blnDup = true;
                        }
                    }
                    #endregion
                    #region insert
                    if (!blnDup)
                    {
                        if (oPerson.SP_PERSON_POSITION_INS(strperson_code, strchange_date, strsalary_old, strsalary_new,
                                                                                                    strposition_old, strposition_new, strlevel_old, strlevel_new,strActive, strCreatedBy, ref strMessage))
                        {
                            ViewState["change_date"] = strchange_date;
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
                oPerson.Dispose();
            }
            return blnResult;
        }

        private void imgSaveOnly_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (saveData())
            {
                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    txtchange_date.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtsalary_old.Text = "0.00";
                    txtsalary_new.Text = "0.00";
                    txtposition_old.Text = string.Empty;
                    txtposition_old_name.Text = string.Empty;
                    txtposition_new.Text = string.Empty;
                    txtposition_new_name.Text = string.Empty;
                    txtchange_date.Focus();
                    string strScript1 = "window.parent.frames['iframeShow1'].__doPostBack('ctl00$ContentPlaceHolder1$BtnR3','')";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
                }
                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    string strScript1 = "window.parent.frames['iframeShow1'].__doPostBack('ctl00$ContentPlaceHolder1$BtnR3','');ClosePopUp('2');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
                }
                MsgBox("บันทึกข้อมูลสมบูรณ์");
            }
        }

        private void setData()
        {
            cPerson oPerson = new cPerson();
            DataSet ds = new DataSet();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string stritem_code = string.Empty,
                 strchange_date= string.Empty,
                strsalary_old= string.Empty,
                strsalary_new= string.Empty,
                strposition_old= string.Empty,
                strposition_new = string.Empty,
                strposition_old_name = string.Empty,
                strposition_new_name = string.Empty,
                strlevel_old = string.Empty,
                strlevel_new = string.Empty,
                strC_active = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty,
                strCreatedDate = string.Empty,
                strUpdatedDate = string.Empty;
            try
            {
                strCriteria =  " and person_code = '" + ViewState["person_code"].ToString() + "' " +
                                        " and change_date = '" + cCommon.SeekDate(txtchange_date.Text) + "' ";
                if (!oPerson.SP_PERSON_POSITION_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region get Data
                        strsalary_old = ds.Tables[0].Rows[0]["salary_old"].ToString();
                        strsalary_new = ds.Tables[0].Rows[0]["salary_new"].ToString();
                        strposition_old = ds.Tables[0].Rows[0]["position_old"].ToString();
                        strposition_old_name = ds.Tables[0].Rows[0]["position_old_name"].ToString();
                        strposition_new = ds.Tables[0].Rows[0]["position_new"].ToString();
                        strposition_new_name = ds.Tables[0].Rows[0]["position_new_name"].ToString();
                        strlevel_old = ds.Tables[0].Rows[0]["level_old"].ToString();
                        strlevel_new = ds.Tables[0].Rows[0]["level_new"].ToString();
                        strC_active = ds.Tables[0].Rows[0]["c_active"].ToString();
                        strCreatedBy = ds.Tables[0].Rows[0]["c_created_by"].ToString();
                        strUpdatedBy = ds.Tables[0].Rows[0]["c_updated_by"].ToString();
                        strCreatedDate = ds.Tables[0].Rows[0]["d_created_date"].ToString();
                        strUpdatedDate = ds.Tables[0].Rows[0]["d_updated_date"].ToString();
                        #endregion

                        #region set Control                 
                        txtsalary_old.Text  =  String.Format("{0:0.00}", float.Parse(strsalary_old));
                        txtsalary_new.Text  =  String.Format("{0:0.00}", float.Parse(strsalary_new));
                        txtposition_old.Text  =strposition_old ;
                        txtposition_old_name.Text = strposition_old_name;
                        txtposition_new.Text = strposition_new;
                        txtposition_new_name.Text = strposition_new_name;
                        txtchange_date.ReadOnly = true;
                        txtchange_date.CssClass = "textboxdis";
                        txtlevel_old.Text = strlevel_old;
                        txtlevel_new.Text = strlevel_new;
                        if (strC_active.Equals("Y"))
                        {
                            txtsalary_old.ReadOnly = false;
                            txtsalary_old.CssClass = "numberbox";
                            txtsalary_new.ReadOnly = false;
                            txtsalary_new.CssClass = "numberbox";
                            txtposition_old.ReadOnly = false;
                            txtposition_old.CssClass = "textbox";
                            txtposition_old_name.ReadOnly = false;
                            txtposition_old_name.CssClass = "textbox";
                            txtposition_new.ReadOnly = false;
                            txtposition_new.CssClass = "textbox";
                            txtposition_new_name.ReadOnly = false;
                            txtposition_new_name.CssClass = "textbox";
                            txtlevel_old.ReadOnly = false;
                            txtlevel_old.CssClass = "textbox";
                            txtlevel_new.ReadOnly = false;
                            txtlevel_new.CssClass = "textbox";
                            chkStatus.Checked = true;
                        }
                        else
                        {
                            txtsalary_old.ReadOnly = true;
                            txtsalary_old.CssClass = "numberdis";
                            txtsalary_new.ReadOnly = true;
                            txtsalary_new.CssClass = "numberdis";
                            txtposition_old.ReadOnly = true;
                            txtposition_old.CssClass = "textboxdis";
                            txtposition_old_name.ReadOnly = true;
                            txtposition_old_name.CssClass = "textboxdis";
                            txtposition_new.ReadOnly = true ;
                            txtposition_new.CssClass = "textboxdis";
                            txtposition_new_name.ReadOnly = true;
                            txtposition_new_name.CssClass = "textboxdis";
                            txtlevel_old.ReadOnly = true;
                            txtlevel_old.CssClass = "textboxdis";
                            txtlevel_new.ReadOnly = true;
                            txtlevel_new.CssClass = "textboxdis";
                            chkStatus.Checked = true;
                        }
                        txtUpdatedBy.Text = strUpdatedBy;
                        txtUpdatedDate.Text = strUpdatedDate;
                        txtsalary_old.Focus();
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