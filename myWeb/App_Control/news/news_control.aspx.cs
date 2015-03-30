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

namespace myWeb.App_Control.news
{
    public partial class news_control : PageBase
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

                imgperson_pic.Attributes.Add("onclick", "OpenPopUp('500px','200px','85%','อัพโหลดไฟล์แนบ' ,'../news/news_upload.aspx?" +
                                                    "ctrl1=" + txtnews_file_name.ClientID + "&ctrl2=" + imgPerson.ClientID + "&show=2', '2');return false;");
                imgClear_person_pic.Attributes.Add("onclick", "document.getElementById('" + txtnews_file_name.ClientID + "').value='';" +
                                                                                                             "document.getElementById('" + imgPerson.ClientID + "').src='../../person_pic/image_n_a2.jpg';return false;");

                imgSaveOnly.Attributes.Add("onMouseOver", "src='../../images/controls/save2.jpg'");
                imgSaveOnly.Attributes.Add("onMouseOut", "src='../../images/controls/save.jpg'");
                #region set QueryString
                if (Request.QueryString["new_id"] != null)
                {
                    ViewState["new_id"] = Request.QueryString["new_id"].ToString();
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
                    //Session["menupopup_name"] =  "เพิ่มข้อมูลข่าว";
                    //txtnews_code.ReadOnly = true;
                    //txtnews_code.CssClass = "textboxdis";
                    //chkStatus.Checked = true;
                }

                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    setData();
                    Session["menupopup_name"] = "แก้ไขข้อมูลข่าว";
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
            string strActive = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty;
            string strScript = string.Empty;
            string strnews_id = "0";
            string strnews_file_name = string.Empty;
            cNews oNews = new cNews();
            DataSet ds = new DataSet();
            try
            {
                #region set Data
                if (txtnews_file_name.Text.Length > 0)
                {
                    strnews_file_name = txtnews_file_name.Text;
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
                        if (oNews.SP_NEW_UPD(ViewState["new_id"].ToString(), txtnew_title.Text, txtnew_des.Text, cboNew_type.SelectedValue, cboNew_status.SelectedValue, strnews_file_name, "", strActive, strUpdatedBy, ref strMessage))
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
                    #region insert
                    if (!blnDup)
                    {
                        if (oNews.SP_NEW_INS(txtnew_title.Text, txtnew_des.Text, cboNew_type.SelectedValue, cboNew_status.SelectedValue, strnews_file_name, "", strActive, strCreatedBy, ref strMessage))
                        {
                            string strGetcode = " and new_title = '" + txtnew_title.Text + "' ";
                            if (!oNews.SP_NEW_SEL(strGetcode, ref ds, ref strMessage))
                            {
                                lblError.Text = strMessage;
                            }
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                strnews_id = ds.Tables[0].Rows[0]["new_id"].ToString();
                            }
                            ViewState["news_id"] = strnews_id;
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
                oNews.Dispose();
            }
            return blnResult;
        }

        private void imgSaveOnly_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (saveData())
            {
                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
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
            cNews oNews = new cNews();
            DataSet ds = new DataSet();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strnew_id = string.Empty,
                strnews_file_name = string.Empty,
                strnews_title = string.Empty,
                strnews_des = string.Empty,
                strnews_type = string.Empty,
                strnews_status = string.Empty,
                strC_active = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty,
                strCreatedDate = string.Empty,
                strUpdatedDate = string.Empty;
            try
            {
                strCriteria = " and new_id = " + ViewState["new_id"].ToString() + " ";
                if (!oNews.SP_NEW_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region get Data
                        strnew_id = ds.Tables[0].Rows[0]["new_id"].ToString();
                        strnews_title = ds.Tables[0].Rows[0]["new_title"].ToString();
                        strnews_des = ds.Tables[0].Rows[0]["new_des"].ToString();
                        strnews_type = ds.Tables[0].Rows[0]["new_type"].ToString();
                        strnews_status = ds.Tables[0].Rows[0]["new_status"].ToString();
                        strnews_file_name = ds.Tables[0].Rows[0]["new_file_name"].ToString();                        
                        strC_active = ds.Tables[0].Rows[0]["c_active"].ToString();
                        strCreatedBy = ds.Tables[0].Rows[0]["c_created_by"].ToString();
                        strUpdatedBy = ds.Tables[0].Rows[0]["c_updated_by"].ToString();
                        strCreatedDate = ds.Tables[0].Rows[0]["d_created_date"].ToString();
                        strUpdatedDate = ds.Tables[0].Rows[0]["d_updated_date"].ToString();
                        #endregion

                        #region set Control
                        ViewState["new_id"] = strnew_id;
                        txtnew_title.Text = strnews_title;
                        txtnew_des.Text = strnews_title;
                        cboNew_type.SelectedValue = strnews_type;
                        cboNew_status.SelectedValue = strnews_status;
                        txtnews_file_name.Text = strnews_file_name;

                        if (strC_active.Equals("Y"))
                        {
                            chkStatus.Checked = true;
                        }
                        else
                        {
                            chkStatus.Checked = false;
                        }
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