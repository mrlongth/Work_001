using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using myDLL;

namespace myWeb
{
    public partial class Default : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblError.Text = string.Empty;
            Title = String.Format("{0} ({1})", "Login", ConfigurationManager.AppSettings["ProgramVersion"].ToString());
            if (!IsPostBack)
            {
                if (Session["username"] != null && Session["username"].ToString() == "0")
                {
                    Session.Abandon();
                }
                Session["ISRetire"] = false;
                SetProfile();
                lblError.Text = string.Empty;
                ViewState["Count"] = "0";
                BindPin();
                BindNew();
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            string strUser = txtUser.Text.Trim();
            string strPass = txtPass.Text.Trim();
            string strMessage = string.Empty;
            string strPersonID = string.Empty;

            if (SetUserProfile(strUser, strPass, ref strMessage))
            {
                Session["username"] = txtUser.Text;
                Session["menu_name"] = "ระบบบริหารงานบุคคล";
                Response.Redirect("Menu_control.aspx");
            }
            else
            {
                strUser += cboDomain.SelectedItem.Text;
                strMessage = string.Empty;
                strPersonID = MjuVerifyUser(strUser, strPass, ref strMessage);
                if (strPersonID.Length > 0)
                {
                    if (SetPersonUserProfile(strPersonID, ref strMessage))
                    {
                        Response.Redirect("MainPerson.aspx");
                    }
                    MsgBox("ไม่สามารถ Login เข้าสู่ระบบได้ เนื่องจาก " + strMessage);
                }
                else
                {
                    strMessage = string.Empty;
                    strUser = txtUser.Text.Trim();
                    strPass = txtPass.Text.Trim();
                    if (SetPersonRetireProfile(strUser, strPass, ref strMessage))
                    {
                        Response.Redirect("~/Person_Manage/global_payment_retire_slip.aspx");
                        MsgBox("ไม่สามารถ Login เข้าสู่ระบบได้ เนื่องจาก " + strMessage);
                    }
                    else
                    {
                        if (strMessage.Length > 0)
                        {
                            MsgBox("ไม่สามารถ Login เข้าสู่ระบบได้ เนื่องจาก" + strMessage);
                        }
                        else
                        {
                            MsgBox("ไม่สามารถ Login เข้าสู่ระบบได้ เนื่องจาก Username หรือ Password ผิดพลาด");
                        }
                    }
                }
                BindPin();
                BindNew();
            }
        }


        protected bool SetUserProfile(string strUserName, string strPassword, ref string _strError)
        {
            bool booResult = false;
            cUser objUser = new cUser();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string strCriteria;
            string strMessage = string.Empty;
            string strDecryptPassword = string.Empty;
            strCriteria = " And LoginName='" + strUserName + "' ";
            objUser.SP_USER_SEL(strCriteria, ref ds, ref strMessage);
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                strDecryptPassword = Cryptorengine.Decrypt(Helper.CStr(dt.Rows[0]["Password"]), true);
                if (strDecryptPassword.Equals(strPassword))
                {
                    this.IsLogin = "Y";
                    this.UserID = Helper.CInt(dt.Rows[0]["UserID"]);
                    this.UserLoginName = Helper.CStr(dt.Rows[0]["LoginName"]);
                    this.DirectorLock = Helper.CStr(dt.Rows[0]["director_lock"]);
                    this.DirectorCode = Helper.CStr(dt.Rows[0]["director_code"]);
                    this.DirectorName = Helper.CStr(dt.Rows[0]["director_name"]);

                    try
                    {
                        this.UnitLock = Helper.CStr(dt.Rows[0]["unit_lock"]);
                    }
                    catch
                    {
                        this.UnitLock = "N";

                    }

                    if (this.UnitLock == "Y")
                    {
                        this.UnitCodeList = string.Empty;
                        string[] strunit_code_list = Helper.CStr(dt.Rows[0]["unit_code_list"]).Split(',');
                        for (int i = 0; i <= (strunit_code_list.GetUpperBound(0)); i++)
                        {
                            this.UnitCodeList += "'" + strunit_code_list[i].Substring(3, 5) + "',";
                        }
                        this.UnitCodeList = this.UnitCodeList.Substring(0, this.UnitCodeList.Length - 1);
                    }

                    this.myBudgetType = Helper.CStr(dt.Rows[0]["budget_type"]);
                    string[] strperson_group_list = Helper.CStr(dt.Rows[0]["person_group_list"]).Split(',');
                    for (int i = 0; i <= (strperson_group_list.GetUpperBound(0)); i++)
                    {
                        PersonGroupList = PersonGroupList + "'" + strperson_group_list[i] + "',";
                    }
                    PersonGroupList = PersonGroupList.Substring(0, PersonGroupList.Length - 1);

                    cItem objItem = new cItem();
                    DataTable dt2 = new DataTable();
                    DataSet ds2 = new DataSet();
                    string strYear = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["yearnow"].ToString();
                    strCriteria = " And person_group_code in (" + PersonGroupList + ") And lot_code<>'' ";
                    strCriteria += " And item_year = " + strYear;
                    objItem.SP_ITEM_LOT_GROUP_SEL(strCriteria, ref ds2, ref strMessage);
                    LotCodeList = string.Empty;
                    for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                    {
                        LotCodeList = LotCodeList + "'" + ds2.Tables[0].Rows[i]["lot_code"].ToString() + "',";
                    }
                    if (LotCodeList.Length > 0)
                        LotCodeList = LotCodeList.Substring(0, LotCodeList.Length - 1);
                    booResult = true;
                }
                else
                {
                    _strError = "รหัสผ่านไม่ถูกต้อง";
                }
            }
            else
            {
                _strError = "ไม่พบผู้ใช้งานนี้";
            }
            return booResult;
        }

        protected bool SetPersonUserProfile(string strUserName, ref string _strError)
        {
            bool booResult = false;
            cPerson objPerson = new cPerson();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string strCriteria;
            string strMessage = string.Empty;
            string strDecryptPassword = string.Empty;
            strCriteria = " And person_id='" + strUserName + "' ";
            objPerson.SP_PERSON_LIST_SEL(strCriteria, ref ds, ref strMessage);
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                Session["PersonID"] = strUserName;
                Session["PersonUserName"] = strUserName;
                Session["PersonBudgetPlanCode"] = Helper.CStr(dt.Rows[0]["budget_plan_code"]); ;
                Session["PersonBudgetType"] = Helper.CStr(dt.Rows[0]["person_budget_type"]); ;
                Session["PersonCode"] = Helper.CStr(dt.Rows[0]["person_code"]);
                Session["PersonFullName"] = Helper.CStr(dt.Rows[0]["title_name"]) + Helper.CStr(dt.Rows[0]["person_thai_name"]) + "  " + Helper.CStr(dt.Rows[0]["person_thai_surname"]);
                Session["username"] = Helper.CStr(dt.Rows[0]["title_name"]) + Helper.CStr(dt.Rows[0]["person_thai_name"]) + "  " + Helper.CStr(dt.Rows[0]["person_thai_surname"]);
                booResult = true;
            }
            else
            {
                _strError = "ไม่พบผู้ใช้งานนี้";
            }
            return booResult;
        }

        protected bool SetPersonRetireProfile(string strUserName, string strPassword, ref string _strError)
        {
            bool booResult = false;
            cPerson_retire objPerson_retire = new cPerson_retire();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string strCriteria;
            string strMessage = string.Empty;
            string strDecryptPassword = string.Empty;
            strCriteria = " And person_id='" + strUserName + "' ";
            objPerson_retire.SP_PERSON_RETIRE_SEL(strCriteria, ref ds, ref strMessage);
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                strDecryptPassword = Cryptorengine.Decrypt(Helper.CStr(dt.Rows[0]["person_password"]), true);
                if (strDecryptPassword.Equals(strPassword))
                {
                    Session["username"] = strUserName;
                    Session["PersonID"] = strUserName;
                    Session["PersonUserName"] = strUserName;
                    Session["PersonBudgetPlanCode"] = "";
                    Session["PersonCode"] = Helper.CStr(dt.Rows[0]["pr_person_code"]);
                    Session["PersonFullName"] = Helper.CStr(dt.Rows[0]["title_name"]) + Helper.CStr(dt.Rows[0]["person_thai_name"]) + "  " + Helper.CStr(dt.Rows[0]["person_thai_surname"]);
                    Session["ISRetire"] = true;
                    booResult = true;
                }

            }
            else
            {
                _strError = "ไม่พบผู้ใช้งานนี้";
            }
            return booResult;
        }

        protected string MjuVerifyUser(string strUserName, string strPassword, ref string _strError)
        {
            string PersonID = string.Empty;
            try
            {
                myWeb.th.ac.mju.ouop.verifyuser oServiceClient = new myWeb.th.ac.mju.ouop.verifyuser();
                PersonID = oServiceClient.verifyuserND(strUserName, strPassword);
            }
            catch (Exception ex)
            {
                _strError = ex.Message;
            }
            return PersonID;
        }

        private void BindPin()
        {
            cNews oNew = new cNews();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string strActive = string.Empty;
            strCriteria = strCriteria + "  And  c_active ='Y' And new_status = 'P'  Order by d_created_date desc";
            try
            {
                if (!oNew.SP_NEW_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    RpPin.DataSource = ds.Tables[0];
                    RpPin.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oNew.Dispose();
                ds.Dispose();
            }
        }

        private void BindNew()
        {
            cNews oNew = new cNews();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string strActive = string.Empty;
            strCriteria = strCriteria + "  And  c_active ='Y' And new_status <> 'P'  Order by d_created_date desc";
            try
            {
                if (!oNew.SP_NEW_SHOW_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    RpNew.DataSource = ds.Tables[0];
                    RpNew.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oNew.Dispose();
                ds.Dispose();
            }
        }

        protected void RpNew_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ViewState["Count"] = int.Parse(ViewState["Count"].ToString()) + 1;
            DataRowView dv = (DataRowView)e.Item.DataItem;
            HyperLink lblNewTitle = (HyperLink)e.Item.FindControl("lblNewTitle");
            Label lblDate = (Label)e.Item.FindControl("lblDate");
            Image imgNewType = (Image)e.Item.FindControl("imgNewType");
            ImageButton imgRead_more = (ImageButton)e.Item.FindControl("imgRead_more");

            lblDate.Text = "(" + Helper.CStr(dv["d_created_date"]) + ")";
            lblNewTitle.Text = Helper.CStr(dv["new_title"]);
            string strNew_status = Helper.CStr(dv["new_status"]);
            string strNew_type = Helper.CStr(dv["new_type"]);
            Image imgNewStatus = (Image)e.Item.FindControl("imgNewStatus");
            lblNewTitle.Target = "_blank";
            if (strNew_type.Equals("F"))
            {
                lblNewTitle.NavigateUrl = "~/new_attach/" + Helper.CStr(dv["new_file_name"]);
            }
            else
            {
                lblNewTitle.NavigateUrl = "~/App_Control/news/news_show_view.aspx?new_id=" + Helper.CStr(dv["new_id"]) + "&new_type=" + Helper.CStr(dv["new_type"]);
                imgRead_more.PostBackUrl = lblNewTitle.NavigateUrl;
            }

            if (int.Parse(ViewState["Count"].ToString()) == 1)
            {
                imgNewType.ImageUrl = "~/images/new/update2day.gif";
            }
            else
            {
                if (strNew_status.Equals("Q"))
                {
                    imgNewType.ImageUrl = "~/images/new/quick.gif";
                    imgNewType.Attributes.Add("title", "ข่าวด่วน");
                    imgNewType.Attributes.Add("onclick", "return false;");
                }
                else if (strNew_status.Equals("P"))
                {
                    imgNewType.ImageUrl = "~/images/new/pin.gif";
                    imgNewType.Attributes.Add("title", "ข่าวปักมุด");
                    imgNewType.Attributes.Add("onclick", "return false;");
                }
                else
                {
                    imgNewType.ImageUrl = "~/images/new/normal.gif";
                    imgNewType.Attributes.Add("title", "ข่าวทั่วไป");
                    imgNewType.Attributes.Add("onclick", "return false;");
                    imgNewType.Visible = false;
                }
            }
            if (strNew_type.Equals("F"))
            {
                imgNewStatus.ImageUrl = "~/images/new/attach.gif";
                imgNewStatus.Attributes.Add("title", "ไฟล์แนบอย่างเดียว");
                imgNewStatus.Attributes.Add("onclick", "return false;");
            }
            else
            {
                imgNewStatus.ImageUrl = "~/images/new/normal.gif";
                imgNewStatus.Attributes.Add("title", "ข่าวทั่วไป");
                imgNewStatus.Attributes.Add("onclick", "return false;");
            }
        }

        protected void RpPin_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView dv = (DataRowView)e.Item.DataItem;
            HyperLink lblNewTitle = (HyperLink)e.Item.FindControl("lblNewTitle");
            Label lblDate = (Label)e.Item.FindControl("lblDate");
            Image imgNewStatus = (Image)e.Item.FindControl("imgNewStatus");
            lblDate.Text = "(" + Helper.CStr(dv["d_created_date"]) + ")";
            lblNewTitle.Text = Helper.CStr(dv["new_title"]);
            string strNew_status = Helper.CStr(dv["new_status"]);
            lblNewTitle.NavigateUrl = "~/App_Control/news/news_show_view.aspx?new_id=" + Helper.CStr(dv["new_id"]) + "&new_type=" + Helper.CStr(dv["new_type"]);
            lblNewTitle.Target = "_blank";
            if (int.Parse(ViewState["Count"].ToString()) == 1)
            {
                imgNewStatus.ImageUrl = "~/images/new/update2day.gif";
            }
            else
            {
                if (strNew_status.Equals("Q"))
                {
                    imgNewStatus.ImageUrl = "~/images/new/quick.gif";
                    imgNewStatus.Attributes.Add("title", "ข่าวด่วน");
                    imgNewStatus.Attributes.Add("onclick", "return false;");
                }
                else if (strNew_status.Equals("P"))
                {
                    imgNewStatus.ImageUrl = "~/images/new/pin.gif";
                    imgNewStatus.Attributes.Add("title", "ข่าวปักมุด");
                    imgNewStatus.Attributes.Add("onclick", "return false;");
                }
                else
                {
                    imgNewStatus.ImageUrl = "~/images/new/normal.gif";
                    imgNewStatus.Attributes.Add("title", "ข่าวทั่วไป");
                    imgNewStatus.Attributes.Add("onclick", "return false;");
                    imgNewStatus.Visible = false;
                }
            }
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            ViewState["Count"] = "0";
            BindPin();
            BindNew();
        }
    }
}
