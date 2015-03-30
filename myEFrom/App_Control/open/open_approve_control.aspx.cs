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
using System.Collections.Generic;
using Aware.WebControls;

namespace myEFrom.App_Control.open
{
    public partial class open_approve_control : PageBase
    {

        protected void Page_LoadComplete(object sender, EventArgs e)
        {

        }

        protected void Page_Init(object sender, EventArgs e)
        {
            base.PermissionURL = "~/App_Control/open/open_approve_list.aspx";
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            lblError.Text = "";
            if (!IsPostBack)
            {
                imgSaveOnly.Attributes.Add("onMouseOver", "src='../../images/button/save_add2.png'");
                imgSaveOnly.Attributes.Add("onMouseOut", "src='../../images/button/save_add.png'");

                #region set QueryString

                if (Request.QueryString["open_detail_approve_id"] != null)
                {
                    ViewState["open_detail_approve_id"] = Request.QueryString["open_detail_approve_id"].ToString();
                }

                if (Request.QueryString["type"] != null)
                {
                    ViewState["type"] = Request.QueryString["type"].ToString();
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

                if (Request.QueryString["PageStatus"] != null)
                {
                    ViewState["PageStatus"] = Request.QueryString["PageStatus"].ToString();
                }


                #endregion

                setData();

            }
        }

        private bool saveData()
        {
            bool blnResult = false;
            int intopen_detail_approve_id = 0;
            int intopen_head_id = 0;
            string strApproveNote = string.Empty;
            string strUserName = string.Empty;
            string strApprove_status = "P";
            cefOpen objEfOpen = new cefOpen();
            cefLoan objEfLoan = new cefLoan();
            try
            {
                #region set Data
                strUserName = Session["username"].ToString();
                intopen_detail_approve_id = Helper.CInt(ViewState["open_detail_approve_id"]);
                strApproveNote = txtapprove_note.Text;
                strApprove_status = cboApprove_status.SelectedValue;
                intopen_head_id = Helper.CInt(ViewState["open_head_id"]);
                #endregion

                #region update

                if (ViewState["type"].ToString() == "open")
                {
                    if (objEfOpen.SP_OPEN_DETAIL_APPROVE_EDIT(intopen_detail_approve_id, strApproveNote,
                        strApprove_status, strUserName))
                    {
                        objEfOpen.SP_OPEN_HEAD_APPROVE_UPD(intopen_head_id, strUserName);
                        blnResult = true;
                    }
                }
                else
                {
                    if (objEfLoan.SP_LOAN_DETAIL_APPROVE_EDIT(intopen_detail_approve_id, strApproveNote,
                        strApprove_status, strUserName))
                    {
                        objEfLoan.SP_LOAN_HEAD_APPROVE_UPD(intopen_head_id, strUserName);
                        blnResult = true;
                    }                    
                }

                #endregion
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                objEfOpen.Dispose();
            }
            return blnResult;
        }

        private void setData()
        {
            cefOpen opjEfOpen = new cefOpen();
            cefLoan opjEfLoan = new cefLoan();
            DataTable dt = new DataTable();
            try
            {
                if (ViewState["mode"].ToString() == "view")
                {
                    imgSaveOnly.Visible = false;
                    txtapprove_note.ReadOnly = true;
                    txtapprove_note.CssClass = "textboxdis";
                    cboApprove_status.Enabled = false;
                    cboApprove_status.CssClass = "textboxdis";
                }
                if (ViewState["type"].ToString() == "open")
                {
                    lblopen.Text = "รายการขออนุมัติเบิก :";
                    lblperson_open_tile.Text = "ผู้ขออนุมัติเบิก :";
                    _strCriteria = " and open_detail_approve_id= " + ViewState["open_detail_approve_id"].ToString();
                    dt = opjEfOpen.SP_OPEN_DETAIL_APPROVE_SEL(_strCriteria);
                    if (dt.Rows.Count > 0)
                    {
                        #region get Data

                        lblopen_title.Text = Helper.CStr(dt.Rows[0]["open_title"]);
                        lblapprove_person.Text = Helper.CStr(dt.Rows[0]["title_name"]) +
                                                 Helper.CStr(dt.Rows[0]["person_thai_name"]) + " " +
                                                 Helper.CStr(dt.Rows[0]["person_thai_surname"]);
                        lblperson_open_name.Text = Helper.CStr(dt.Rows[0]["req_title_name"]) +
                                                 Helper.CStr(dt.Rows[0]["req_person_thai_name"]) + " " +
                                                 Helper.CStr(dt.Rows[0]["req_person_thai_surname"]);
                        ViewState["open_head_id"] = dt.Rows[0]["open_head_id"].ToString();
                        if (cboApprove_status.Items.FindByValue(dt.Rows[0]["approve_status"].ToString()) != null)
                        {
                            cboApprove_status.SelectedIndex = -1;
                            cboApprove_status.Items.FindByValue(dt.Rows[0]["approve_status"].ToString()).Selected = true;
                        }
                        txtapprove_note.Text = dt.Rows[0]["approve_note"].ToString();

                        #endregion
                    }
                }
                else
                {
                    lblopen.Text = "รายละเอียดสัญญายืมเงิน :";
                    lblperson_open_tile.Text = "ผู้ขอยืมเงิน :";                   
                    _strCriteria = " and loan_detail_approve_id= " + ViewState["open_detail_approve_id"];
                    dt = opjEfLoan.SP_LOAN_DETAIL_APPROVE_SEL(_strCriteria);
                    if (dt.Rows.Count > 0)
                    {
                        #region get Data

                        lblopen_title.Text = Helper.CStr(dt.Rows[0]["loan_reason"]);
                        lblapprove_person.Text = Helper.CStr(dt.Rows[0]["title_name"]) +
                                                 Helper.CStr(dt.Rows[0]["person_thai_name"]) + " " +
                                                 Helper.CStr(dt.Rows[0]["person_thai_surname"]);
                        lblperson_open_name.Text = Helper.CStr(dt.Rows[0]["req_title_name"]) +
                                              Helper.CStr(dt.Rows[0]["req_person_thai_name"]) + " " +
                                              Helper.CStr(dt.Rows[0]["req_person_thai_surname"]);
                    
                        ViewState["open_head_id"] = dt.Rows[0]["loan_id"].ToString();
                        if (cboApprove_status.Items.FindByValue(dt.Rows[0]["approve_status"].ToString()) != null)
                        {
                            cboApprove_status.SelectedIndex = -1;
                            cboApprove_status.Items.FindByValue(dt.Rows[0]["approve_status"].ToString()).Selected = true;
                        }
                        txtapprove_note.Text = dt.Rows[0]["approve_note"].ToString();

                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        protected void imgSaveOnly_Click(object sender, ImageClickEventArgs e)
        {
            if (saveData())
            {
                 MsgBox("บันทึกข้อมูลสมบูรณ์");
                 string strScript1 = "$('#divdes1').text().replace('เพิ่ม','แก้ไข');ClosePopUpListPost('1','1');";
                 ScriptManager.RegisterStartupScript(Page, Page.GetType(), "SavePageClose", strScript1, true);
                //setData();
            }
        }

    }
}