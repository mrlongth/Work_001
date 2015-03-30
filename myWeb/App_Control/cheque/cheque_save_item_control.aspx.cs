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

namespace myWeb.App_Control.cheque
{
    public partial class cheque_save_item_control : PageBase
    {

        #region private data
        private bool[] blnAccessRight = new bool[5] { false, false, false, false, false };
        private string strPrefixCtr_main = "ctl00$ContentPlaceHolder1$";
        // private string strPrefixCtr = "ctl00$ASPxRoundPanel1$ASPxRoundPanel2$ContentPlaceHolder1$";
        #endregion

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
                Session["menupopup_name"] = "";
                ViewState["sort"] = "cheque_doc";
                ViewState["direction"] = "ASC";
                #region set QueryString
                if (Request.QueryString["cheque_doc"] != null)
                {
                    ViewState["cheque_doc"] = Request.QueryString["cheque_doc"].ToString();
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

                imgList_item.Attributes.Add("onclick", "OpenPopUp('750px','400px','93%','ค้นหาข้อมูลผู้จ่ายเช็ค' ,'../lov/cheque_lov.aspx?cheque_code='+document.forms[0]." + strPrefixCtr_main + "txtcheque_code.value+'" +
                "&cheque_name='+document.forms[0]." + strPrefixCtr_main + "txtcheque_name.value+'" +
                "&ctrl1=" + txtcheque_code.ClientID + "&ctrl2=" + txtcheque_name.ClientID + "&show=3', '3');return false;");

                imgClear_item.Attributes.Add("onclick", "document.forms[0]." + strPrefixCtr_main + "txtcheque_code.value='';" +
                                        "document.forms[0]." + strPrefixCtr_main + "txtcheque_name.value=''; return false;");

                #endregion
                #region Set Image

                #endregion

                //if (!cboItem_type.SelectedValue.Equals("C"))
                //{
                //    txtcheque_code.Enabled = false;
                //    txtcheque_name.Enabled = false;
                //    txtcheque_code.CssClass = "textboxdis";
                //    txtcheque_name.CssClass = "textboxdis";                    
                //    imgList_item.Visible = false;
                //    imgClear_item.Visible = false;
                //}

                txtcheque_date_print.Text = cCommon.CheckDate(DateTime.Now.Date.ToString());
                txtcheque_date_pay.Text = cCommon.CheckDate(DateTime.Now.Date.ToString());
                txtcheque_date_bank.Text = cCommon.CheckDate(DateTime.Now.Date.ToString());


            }
        }

        private void setData()
        {
            /*
             cItem oItem = new cItem();
             DataSet ds = new DataSet();
             string strMessage = string.Empty, strCriteria = string.Empty;
             string stritem_code = string.Empty,
                 stritem_name = string.Empty,
                 stritem_year = string.Empty,
                 stritem_type = string.Empty,
                 stritem_group_code = string.Empty,
                 stritem_group_name = string.Empty,
                 strlot_code = string.Empty,
                 strlot_name = string.Empty,
                 strperson_group_code = string.Empty,
                 strYear = string.Empty,
                 strC_active = string.Empty,
                 strCreatedBy = string.Empty,
                 strUpdatedBy = string.Empty,
                 strCreatedDate = string.Empty,
                 strUpdatedDate = string.Empty,
                 strcheque_code = string.Empty,
                 strcheque_name = string.Empty,
                 strcheque_type = string.Empty;
             try
             {
                 strCriteria = " and item_code = '" + ViewState["item_code"].ToString() + "' ";
                 if (!oItem.SP_ITEM_SEL(strCriteria, ref ds, ref strMessage))
                 {
                     lblError.Text = strMessage;
                 }
                 else
                 {
                     if (ds.Tables[0].Rows.Count > 0)
                     {
                         #region get Data
                         strYear = ds.Tables[0].Rows[0]["item_year"].ToString();
                         stritem_code = ds.Tables[0].Rows[0]["item_code"].ToString();
                         stritem_name = ds.Tables[0].Rows[0]["item_name"].ToString();
                         stritem_type = ds.Tables[0].Rows[0]["item_type"].ToString();
                         stritem_group_code = ds.Tables[0].Rows[0]["item_group_code"].ToString();
                         stritem_group_name = ds.Tables[0].Rows[0]["item_group_name"].ToString();
                         strlot_code = ds.Tables[0].Rows[0]["lot_code"].ToString();
                         strlot_name = ds.Tables[0].Rows[0]["lot_name"].ToString();
                         strperson_group_code = ds.Tables[0].Rows[0]["person_group_code"].ToString();
                         strC_active = ds.Tables[0].Rows[0]["c_active"].ToString();
                         strCreatedBy = ds.Tables[0].Rows[0]["c_created_by"].ToString();
                         strUpdatedBy = ds.Tables[0].Rows[0]["c_updated_by"].ToString();
                         strCreatedDate = ds.Tables[0].Rows[0]["d_created_date"].ToString();
                         strUpdatedDate = ds.Tables[0].Rows[0]["d_updated_date"].ToString();
                         strcheque_code = ds.Tables[0].Rows[0]["cheque_code"].ToString();
                         strcheque_name = ds.Tables[0].Rows[0]["cheque_name"].ToString();
                         strcheque_type = ds.Tables[0].Rows[0]["cheque_type"].ToString();
                         #endregion

                         #region set Control
                         InitcboYear();
                         if (cboYear.Items.FindByValue(strYear) != null)
                         {
                             cboYear.SelectedIndex = -1;
                             cboYear.Items.FindByValue(strYear).Selected = true;
                         }
                         cboYear.Enabled = false;
                         txtitem_code.Text = stritem_code;
                         txtitem_name.Text = stritem_name;
                         if (cboItem_type.Items.FindByValue(stritem_type) != null)
                         {
                             cboItem_type.SelectedIndex = -1;
                             cboItem_type.Items.FindByValue(stritem_type).Selected = true;
                         }
                        
                         InitcboPerson_group();
                         if (cboPerson_group.Items.FindByValue(strperson_group_code) != null)
                         {
                             cboPerson_group.SelectedIndex = -1;
                             cboPerson_group.Items.FindByValue(strperson_group_code).Selected = true;
                         }
                         txtcheque_code.Text = strcheque_code;
                         txtcheque_name.Text = strcheque_name;
                         if (cboCheque_type.Items.FindByValue(strcheque_type) != null)
                         {
                             cboCheque_type.SelectedIndex = -1;
                             cboCheque_type.Items.FindByValue(strcheque_type).Selected = true;
                         }

                         if (strC_active.Equals("Y"))
                         {

                             cboItem_type.Enabled = true;
                             cboItem_type.CssClass = "textbox";

                             txtitem_name.ReadOnly = false;
                             txtitem_name.CssClass = "textbox";

                             cboItem_group.Enabled = true;
                             cboItem_group.CssClass = "textbox";

                             cboLot.Enabled = true;
                             cboLot.CssClass = "textbox";


                             txtcheque_code.ReadOnly = false;
                             txtcheque_code.CssClass = "textbox";

                             txtcheque_name.ReadOnly = false;
                             txtcheque_name.CssClass = "textbox";

                             chkStatus.Checked = true;
                         }
                         else
                         {
                             cboItem_type.Enabled = false;
                             cboItem_type.CssClass = "textboxdis";

                             txtitem_code.ReadOnly = true;
                             txtitem_code.CssClass = "textboxdis";

                             txtitem_name.ReadOnly = true;
                             txtitem_name.CssClass = "textboxdis";

                             cboItem_group.Enabled = false;
                             cboItem_group.CssClass = "textboxdis";

                             cboLot.Enabled = false;
                             cboLot.CssClass = "textboxdis";

                             txtcheque_code.ReadOnly = true;
                             txtcheque_code.CssClass = "textboxdis";

                             txtcheque_name.ReadOnly = true;
                             txtcheque_name.CssClass = "textboxdis";


                             chkStatus.Checked = false;
                         }
                         cboYear.CssClass = "textboxdis";

                         txtitem_code.ReadOnly = true;
                         txtitem_code.CssClass = "textboxdis";

                         if (stritem_type.Equals("D"))
                         {
                             InitcboLot();
                             if (cboLot.Items.FindByValue(strlot_code) != null)
                             {
                                 cboLot.SelectedIndex = -1;
                                 cboLot.Items.FindByValue(strlot_code).Selected = true;
                             }
                             InitcboItem_group();
                             if (cboItem_group.Items.FindByValue(stritem_group_code) != null)
                             {
                                 cboItem_group.SelectedIndex = -1;
                                 cboItem_group.Items.FindByValue(stritem_group_code).Selected = true;
                             }
                             cboLot.Enabled = true;
                             cboItem_group.Enabled = true;
                         }
                         else
                         {
                             cboLot.Enabled = false;
                             cboItem_group.Enabled = false;
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
             } */
        }

        private bool saveData()
        {
            bool blnResult = false;
            bool blnDup = false;
            string strMessage = string.Empty;
            string strcheque_doc = string.Empty;
            string strcheque_code = string.Empty;
            string strcheque_no = string.Empty;
            string strcheque_pvno = string.Empty;
            string strcheque_money = string.Empty;
            string strcheque_money_thai = string.Empty;
            string strcheque_comment_sub = string.Empty;
            string strcheque_print = string.Empty;
            string strdirector_code = string.Empty;

            string strcheque_date_print = string.Empty;
            string strcheque_date_pay = string.Empty;
            string strcheque_date_bank = string.Empty;
            string strcheque_deka = string.Empty;
            string strcheque_acccode = string.Empty;

            string strScript = string.Empty;
            cCheque oCheque = new cCheque();
            DataSet ds = new DataSet();
            try
            {
                #region set Data
                strcheque_code = txtcheque_code.Text;
                strcheque_doc = ViewState["cheque_doc"].ToString();
                strcheque_code = txtcheque_code.Text;
                strcheque_no = txtcheque_no.Text;
                strcheque_pvno = txtcheque_pvno.Text;
                strcheque_money = txtcheque_money.Value.ToString();
                strcheque_money_thai = string.Empty;
                strcheque_comment_sub = string.Empty;
                strcheque_print = "N";
                strdirector_code = string.Empty;

                strcheque_date_print = txtcheque_date_print.Text;
                strcheque_date_pay = txtcheque_date_pay.Text;
                strcheque_date_bank = txtcheque_date_bank.Text;
                strcheque_deka = txtcheque_deka.Text;
                strcheque_acccode = txtcheque_acccode.Text;

                #endregion
                string strCheckAdd = " and cheque_code = '" + strcheque_code.Trim() + "' And cheque_doc='" + strcheque_doc + "' ";
                if (!oCheque.SP_CHEQUE_DETAIL_SEL(strCheckAdd, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        strScript =
                            "alert(\"ไม่สามารถทำรายการได้ เนื่องจาก ข้อมูลซ้ำ\");\n";
                        blnDup = true;
                    }
                    else
                    {
                        #region insert
                        if (!oCheque.SP_CHEQUE_DETAIL_INS(strcheque_doc, strcheque_code, strcheque_no, strcheque_pvno, strcheque_money, strcheque_money_thai,
                                                          strcheque_comment_sub, strcheque_print, strdirector_code,
                                                          strcheque_date_print, strcheque_date_pay, strcheque_date_bank, strcheque_deka, strcheque_acccode, ref strMessage))
                        {
                            lblError.Text = strMessage;
                        }
                        else
                        {
                            blnResult = true;
                        }
                        #endregion
                    }
                }
                if (blnDup)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "chk", strScript, true);
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oCheque.Dispose();
            }
            return blnResult;
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

        private void imgSaveOnly_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (saveData())
            {
                string strScript1 = "window.parent.frames['iframeShow1'].__doPostBack('ctl00$ContentPlaceHolder1$BtnR1','');ClosePopUp('2');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
                MsgBox("บันทึกข้อมูลสมบูรณ์");
            }
        }

    }
}
