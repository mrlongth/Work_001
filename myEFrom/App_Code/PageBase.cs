using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using myDLL;
using DevExpress.Web.ASPxRoundPanel;

namespace myEFrom
{
    public class PageBase : Page
    {

        #region Public Fields

        public bool _boolResult;
        public string _strMessage = string.Empty;
        public enum Mode { SEARCH, NEW, EDIT, VIEW };
        public string _strCriteria;


        #endregion

        #region Property General


        public string IsLogin
        {
            get
            {
                if (Session["IsLogin"] == null)
                {
                    Session["IsLogin"] = "N";
                }
                return Session["IsLogin"].ToString();
            }
            set
            {
                Session["IsLogin"] = value;
            }
        }

        public string PageDes
        {
            get
            {
                if (ViewState["PageDes"] == null)
                {
                    ViewState["PageDes"] = string.Empty;
                }
                return ViewState["PageDes"].ToString();
            }
            set
            {
                ViewState["PageDes"] = value;
            }
        }

        public string PageTitle
        {
            get
            {
                if (ViewState["PageTitle"] == null)
                {
                    ViewState["PageTitle"] = string.Empty;
                }
                return ViewState["PageTitle"].ToString();
            }
            set
            {
                ViewState["PageTitle"] = value;
            }
        }

        private string ProgramVersion
        {
            get
            {
                try
                {
                    ViewState["ProgramVersion"] = ConfigurationManager.AppSettings["ProgramVersion"].ToString();
                }
                catch
                {
                    ViewState["ProgramVersion"] = "ระบบจัดการแบบฟอร์มออนไลน์ มหาวิทยาลัยแม่โจ้";
                }
                return ViewState["ProgramVersion"].ToString();
            }
            set
            {
                ViewState["ProgramVersion"] = value;
            }
        }

        public int UserID
        {
            get
            {
                if (Session["UserID"] == null)
                {
                    Session["UserID"] = "0";
                }
                return int.Parse(Session["UserID"].ToString());
            }
            set
            {
                Session["UserID"] = value;
            }
        }

        public string UserLoginName
        {
            get
            {
                if (Session["LoginName"] == null)
                {
                    return "";
                }
                else
                {
                    return Session["LoginName"].ToString();
                }
            }
            set
            {
                Session["LoginName"] = value;
            }
        }

        public string PersonGroupList
        {
            get
            {
                //if (Session["LoginName"] == null)
                //{
                //    GetUserProfile();
                //}
                //return Session["LoginName"].ToString();
                if (Session["PersonGroupList"] == null)
                {
                    return "";
                }
                else
                {
                    return Session["PersonGroupList"].ToString();
                }
            }
            set
            {
                Session["PersonGroupList"] = value;
            }
        }

        public string DirectorLock
        {
            get
            {
                if (Session["DirectorLock"] == null)
                {
                    Session["DirectorLock"] = "N";
                }
                return Session["DirectorLock"].ToString();
            }
            set
            {
                Session["DirectorLock"] = value;
            }
        }

        public string PersonCode
        {
            get
            {
                if (Session["PersonCode"] == null)
                {
                    return "";
                }
                else
                {
                    return Session["PersonCode"].ToString();
                }
            }
            set
            {
                Session["PersonCode"] = value;
            }
        }

        public string ApproveFor
        {
            get
            {
                if (Session["ApproveFor"] == null)
                {
                    return "";
                }
                else
                {
                    return Session["ApproveFor"].ToString();
                }
            }
            set
            {
                Session["ApproveFor"] = value;
            }
        }


        


        public string PersonFullName
        {
            get
            {
                if (Session["PersonFullName"] == null)
                {
                    return "";
                }
                else
                {
                    return Session["PersonFullName"].ToString();
                }
            }
            set
            {
                Session["PersonFullName"] = value;
            }
        }

        public string UserGroupCode
        {
            get
            {
                if (Session["UserGroupCode"] == null)
                {
                    return "";
                }
                else
                {
                    return Session["UserGroupCode"].ToString();
                }
            }
            set
            {
                Session["UserGroupCode"] = value;
            }
        }

        public string PositionMangeCode
        {
            get
            {
                if (Session["PositionMangeCode"] == null)
                {
                    return "";
                }
                else
                {
                    return Session["PositionMangeCode"].ToString();
                }
            }
            set
            {
                Session["PositionMangeCode"] = value;
            }
        }

        public string PositionMangeName
        {
            get
            {
                if (Session["PositionMangeName"] == null)
                {
                    return "";
                }
                else
                {
                    return Session["PositionMangeName"].ToString();
                }
            }
            set
            {
                Session["PositionMangeName"] = value;
            }
        }

        public string PositionCode
        {
            get
            {
                if (Session["PositionCode"] == null)
                {
                    return "";
                }
                else
                {
                    return Session["PositionCode"].ToString();
                }
            }
            set
            {
                Session["PositionCode"] = value;
            }
        }

        public string PositionName
        {
            get
            {
                if (Session["PositionName"] == null)
                {
                    return "";
                }
                else
                {
                    return Session["PositionName"].ToString();
                }
            }
            set
            {
                Session["PositionName"] = value;
            }
        }

        public string UnitLock
        {
            get
            {
                if (Session["UnitLock"] == null)
                {
                    Session["UnitLock"] = "N";
                }
                return Session["UnitLock"].ToString();
            }
            set
            {
                Session["UnitLock"] = value;
            }
        }

        public string UnitCodeList
        {
            get
            {
                if (Session["UnitCodeList"] == null)
                {
                    return "";
                }
                else
                {
                    return Session["UnitCodeList"].ToString();
                }
            }
            set
            {
                Session["UnitCodeList"] = value;
            }
        }

        public string UnitCode
        {
            get
            {
                if (Session["UnitCode"] == null)
                {
                    return "";
                }
                else
                {
                    return Session["UnitCode"].ToString();
                }
            }
            set
            {
                Session["UnitCode"] = value;
            }
        }

        public string DirectorCode
        {
            get
            {
                if (Session["DirectorCode"] == null)
                {
                    return "";
                }
                else
                {
                    return Session["DirectorCode"].ToString();
                }
            }
            set
            {
                Session["DirectorCode"] = value;
            }
        }

        public string DirectorName
        {
            get
            {
                if (Session["DirectorName"] == null)
                {
                    return "";
                }
                else
                {
                    return Session["DirectorName"].ToString();
                }
            }
            set
            {
                Session["DirectorName"] = value;
            }
        }

        public string LotCodeList
        {
            get
            {
                if (Session["LotCodeList"] == null)
                {
                    return "";
                }
                else
                {
                    return Session["LotCodeList"].ToString();
                }
            }
            set
            {
                Session["LotCodeList"] = value;
            }
        }

        public string UserNameEN
        {
            get
            {
                if (Session["NameEN"] == null)
                {
                    Session["NameEN"] = string.Empty;
                }
                return Session["NameEN"].ToString();
            }
            set
            {
                Session["NameEN"] = value;
            }
        }

        public bool IsUserNew
        {
            get { return (bool)ViewState["IsUserNew"]; }
            set { ViewState["IsUserNew"] = value; }
        }

        public bool IsUserView
        {
            get { return (bool)ViewState["IsUserView"]; }
            set { ViewState["IsUserView"] = value; }
        }

        public bool IsUserEdit
        {
            get { return (bool)ViewState["IsUserEdit"]; }
            set { ViewState["IsUserEdit"] = value; }
        }

        public bool IsUserDelete
        {
            get { return (bool)ViewState["IsUserDelete"]; }
            set { ViewState["IsUserDelete"] = value; }
        }

        public bool IsUserApprove
        {
            get { return (bool)ViewState["IsUserApprove"]; }
            set { ViewState["IsUserApprove"] = value; }
        }

        public bool IsUserExtra
        {
            get { return (bool)ViewState["IsUserExtra"]; }
            set { ViewState["IsUserExtra"] = value; }
        }

        public string myBudgetType
        {
            get
            {
                if (Session["myBudgetType"] == null)
                {
                    return "";
                }
                else
                {
                    return Session["myBudgetType"].ToString();
                }
            }
            set
            {
                Session["myBudgetType"] = value;
            }
        }

        public string PermissionURL
        {
            get
            {
                if (Session["PermissionURL"] == null)
                {
                    return "";
                }
                else
                {
                    return Session["PermissionURL"].ToString();
                }
            }
            set
            {
                Session["PermissionURL"] = value;
            }
        }

        protected string CurrentPageUrl
        {
            get
            {
                if (HttpContext.Current.Session["CurrentPageUrl"] == null)
                {
                    return "Default.aspx";
                }
                else
                {
                    return (HttpContext.Current.Session["CurrentPageUrl"].ToString());
                }
            }
            set
            {
                HttpContext.Current.Session["CurrentPageUrl"] = value;
            }
        }

        #endregion

        public void InitUserAccessRight(string pUserGroupCode)
        {
            cefUser objEfUserMenu = new cefUser();
            DataTable table = new DataTable();
            try
            {
                string currentUrl = this.PermissionURL == "" ? this.GetCurrentUrl() : this.PermissionURL;
                currentUrl = Server.UrlDecode(currentUrl);
                string strCriteria = string.Empty;
                strCriteria = " And  MenuNavigationUrl='" + currentUrl + "' and UserGroupCode = '" + pUserGroupCode + "' ";
                table = objEfUserMenu.SP_USER_GROUP_MENU_SEL(strCriteria);

                if (table.Rows.Count > 0)
                {
                    IsUserView = false;
                    IsUserNew = false;
                    IsUserEdit = false;
                    IsUserDelete = false;
                    IsUserApprove = false;
                    IsUserExtra = false;
                    DataRow rowArray = table.Rows[0];
                    string str6 = rowArray["CanView"].ToString();
                    string str5 = rowArray["CanInsert"].ToString();
                    string str3 = rowArray["CanEdit"].ToString();
                    string str2 = rowArray["CanDelete"].ToString();
                    string str = rowArray["CanApprove"].ToString();
                    string str4 = rowArray["CanExtra"].ToString();
                    if (str6 == "N")
                    {
                        Session["error_message"] = "คุณไม่มีสิทธิ์ในการใช้งานเมนู : " + rowArray["MenuName"].ToString();
                    }
                    if (str6 == "Y")
                    {
                        IsUserView = true;
                    }
                    if (str5 == "Y")
                    {
                        IsUserNew = true;
                    }
                    if (str3 == "Y")
                    {
                        IsUserEdit = true;
                    }
                    if (str2 == "Y")
                    {
                        IsUserDelete = true;
                    }
                    if (str == "Y")
                    {
                        IsUserApprove = true;
                    }
                    if (str4 == "Y")
                    {
                        IsUserExtra = true;
                    }
                    PageTitle = rowArray["MenuName"] + " (" + ProgramVersion + ")";
                    PageDes = rowArray["MenuName"].ToString();
                }
            }
            catch (Exception ex)
            {
                table.Dispose();
            }

        }

        protected bool SetPersonUserProfile(string strUserName, ref string _strError)
        {
            bool booResult = false;
            cPerson objPerson = new cPerson();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string strCriteria;
            string strMessage = string.Empty;
            strCriteria = " And person_id='" + strUserName + "' ";
            objPerson.SP_PERSON_LIST_SEL(strCriteria, ref ds, ref strMessage);
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                Session["PersonID"] = strUserName;
                Session["PersonUserName"] = strUserName;
                Session["PersonBudgetPlanCode"] = Helper.CStr(dt.Rows[0]["budget_plan_code"]); ;
                Session["DirectorCode"] = Helper.CStr(dt.Rows[0]["director_code"]); ;
                Session["DirectorName"] = Helper.CStr(dt.Rows[0]["director_name"]);
                Session["UnitCode"] = Helper.CStr(dt.Rows[0]["unit_code"]); ;
                Session["PersonCode"] = Helper.CStr(dt.Rows[0]["person_code"]);
                if (Helper.CStr(dt.Rows[0]["ef_approve_for"]).Length == 0)
                {
                    Session["ApproveFor"] = Helper.CStr(dt.Rows[0]["person_code"]);                
                }
                else
                {
                    Session["ApproveFor"] = Helper.CStr(dt.Rows[0]["ef_approve_for"]);                
                }
                    

                Session["PersonFullName"] = Helper.CStr(dt.Rows[0]["title_name"]) + Helper.CStr(dt.Rows[0]["person_thai_name"]) + "  " + Helper.CStr(dt.Rows[0]["person_thai_surname"]);
                Session["username"] = (Helper.CStr(dt.Rows[0]["person_thai_name"])).Substring(0, dt.Rows[0]["person_thai_name"].ToString().Length > 10 ? 10 : dt.Rows[0]["person_thai_name"].ToString().Length);
                Session["PositionManageCode"] = Helper.CStr(dt.Rows[0]["person_manage_code"]);
                Session["PositionManageName"] = Helper.CStr(dt.Rows[0]["person_manage_name"]);
                Session["PositionCode"] = Helper.CStr(dt.Rows[0]["position_code"]);
                Session["PositionName"] = Helper.CStr(dt.Rows[0]["position_name"]);
                Session["UserGroupCode"] = Helper.CStr(dt.Rows[0]["ef_user_group_list"]) != "" ? Helper.CStr(dt.Rows[0]["ef_user_group_list"]) : "User";

                booResult = true;
            }
            else
            {
                _strError = "ไม่พบผู้ใช้งานนี้";
            }
            return booResult;
        }

        public string GetCurrentUrl()
        {
            //restore tree view node selected
            string strCurrentPageUrl = this.Page.AppRelativeVirtualPath.ToString();
            if (this.Page.ClientQueryString.ToString() != string.Empty)
            {
                strCurrentPageUrl = strCurrentPageUrl + "?" + this.Page.ClientQueryString;
            }
            if (strCurrentPageUrl.Length > 0)
            {
                strCurrentPageUrl = Server.UrlEncode(strCurrentPageUrl);
            }

            return strCurrentPageUrl;
        }

        public void MsgBox(string strMessage)
        {
            UpdatePanel oUpdatePanel;
            string strScript = string.Empty;
            strScript = "alert('" + strMessage + "');";
            oUpdatePanel = (UpdatePanel)this.Master.FindControl("updatePanel1");
            ScriptManager.RegisterClientScriptBlock(oUpdatePanel, oUpdatePanel.GetType(), "MessageBox", strScript, true);
        }

        public string GetPopupScript(string strString, int intWidth, int intHeight, int intLevel, bool boolIsReturn)
        {
            Panel pnlShow = (Panel)Page.Master.FindControl("panelShow" + intLevel);
            string strJsScript;

            strJsScript = "document.getElementById('" + pnlShow.ClientID + "').style.width='" + intWidth + "px';" +
                          "document.getElementById('" + pnlShow.ClientID + "').style.height='" + intHeight + "px';" +
                          "document.getElementById('iframeShow" + intLevel + "').src='" + strString + "&lov_height=" + intHeight + "" +
                          "&timestamp=" + DateTime.Now.ToString("ddMMyyyyHHmmssfff") + "';$find('show" + intLevel + "_ModalPopupExtender').show(); ";
            if (boolIsReturn)
            {
                strJsScript += " return false;";
            }
            return strJsScript;
        }

        public void RunScript(string strScript)
        {
            UpdatePanel oUpdatePanel;
            oUpdatePanel = (UpdatePanel)this.Master.FindControl("updatePanel1");
            ScriptManager.RegisterClientScriptBlock(oUpdatePanel, oUpdatePanel.GetType(), "RunScript", strScript, true);
        }

        protected override void OnPreLoad(System.EventArgs e)
        {
            base.OnPreLoad(e);
            SetProfile();
            string strPhysicalPath = Request.PhysicalPath;
            System.IO.FileInfo fi = new System.IO.FileInfo(strPhysicalPath);
            var strCurrentPageFileName = fi.Name;
            if (!IsPostBack || this.CurrentPageUrl != strCurrentPageFileName)
            {
                try
                {
                    FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
                    FormsAuthenticationTicket ticket = id.Ticket;
                    var strUserName = ticket.Name;
                    var strUserGroupCode = ticket.UserData;
                    if (SetPersonUserProfile(strUserName, ref _strMessage))
                    {
                        InitUserAccessRight(strUserGroupCode);
                    }
                    else
                    {
                        if (strCurrentPageFileName.ToLower() != "default.aspx")
                        {
                            Response.Redirect(string.Format("~/Default.aspx?&ReturnUrl={0}", GetCurrentUrl()));
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (strCurrentPageFileName.ToLower() != "default.aspx")
                    {
                        Response.Redirect(string.Format("~/Default.aspx?&ReturnUrl={0}", GetCurrentUrl()));
                    }
                }

            }
            if (!IsPostBack)
            {
                if (this.Master != null)
                {
                    if (Master.FindControl("ASPxRoundPanel1") != null)
                    {
                        ((ASPxRoundPanel)Master.FindControl("ASPxRoundPanel1")).HeaderText = PageDes;
                    }
                }
            }
            if (PageTitle != "")
                Title = PageTitle;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnLoadComplete(System.EventArgs e)
        {
            base.OnLoadComplete(e);
            if (!IsPostBack)
            {
                if (ViewState["IsUserView"] != null && !this.IsUserView)
                    Response.Redirect("~/App_Control/error/error_page.aspx");
               
            }
        }


        protected void SetProfile()
        {
            if (Application["xmlconfig"] == null)
            {
                try
                {
                    #region read xml config file store to variable
                    cAware.Profile.Xml oXml = new cAware.Profile.Xml();
                    oXml.Name = Server.MapPath("xml\\") + System.Configuration.ConfigurationSettings.AppSettings["xmlconfig"];
                    DataSet ds = new DataSet();
                    DataTable dt;
                    DataRow rw;
                    int i = 0;

                    #region "Record Per Page"
                    dt = new DataTable("RecordPerPage");
                    dt.Columns.Add("Text");
                    dt.Columns.Add("Value");

                    string[] entries = oXml.GetEntryNames("RecordPerPage");
                    for (i = 0; i < entries.Length; i++)
                    {
                        rw = dt.NewRow();
                        rw[0] = entries[i].ToString();
                        rw[1] = oXml.GetValue("RecordPerPage", entries[i].ToString(), "");
                        dt.Rows.Add(rw);
                    }
                    ds.Tables.Add(dt);
                    #endregion

                    #region "imgFind"
                    dt = new DataTable("imgFind");
                    dt.Columns.Add("img");
                    dt.Columns.Add("title");
                    dt.Columns.Add("imgdisable");
                    dt.Columns.Add("titledisable");
                    rw = dt.NewRow();
                    rw[0] = oXml.GetValue("imgFind", "img");
                    rw[1] = oXml.GetValue("imgFind", "title");
                    rw[2] = oXml.GetValue("imgFind", "imgdisable");
                    rw[3] = oXml.GetValue("imgFind", "titledisable");
                    dt.Rows.Add(rw);
                    ds.Tables.Add(dt);
                    #endregion

                    #region "imgNew"
                    dt = new DataTable("imgNew");
                    dt.Columns.Add("img");
                    dt.Columns.Add("title");
                    dt.Columns.Add("imgdisable");
                    dt.Columns.Add("titledisable");
                    rw = dt.NewRow();
                    rw[0] = oXml.GetValue("imgNew", "img");
                    rw[1] = oXml.GetValue("imgNew", "title");
                    rw[2] = oXml.GetValue("imgNew", "imgdisable");
                    rw[3] = oXml.GetValue("imgNew", "titledisable");
                    dt.Rows.Add(rw);
                    ds.Tables.Add(dt);
                    #endregion

                    #region "imgView"
                    dt = new DataTable("imgView");
                    dt.Columns.Add("img");
                    dt.Columns.Add("title");
                    dt.Columns.Add("imgdisable");
                    dt.Columns.Add("titledisable");
                    rw = dt.NewRow();
                    rw[0] = oXml.GetValue("imgView", "img");
                    rw[1] = oXml.GetValue("imgView", "title");
                    rw[2] = oXml.GetValue("imgView", "imgdisable");
                    rw[3] = oXml.GetValue("imgView", "titledisable");
                    dt.Rows.Add(rw);
                    ds.Tables.Add(dt);
                    #endregion

                    #region "imgEdit"
                    dt = new DataTable("imgEdit");
                    dt.Columns.Add("img");
                    dt.Columns.Add("title");
                    dt.Columns.Add("imgdisable");
                    dt.Columns.Add("titledisable");
                    rw = dt.NewRow();
                    rw[0] = oXml.GetValue("imgEdit", "img");
                    rw[1] = oXml.GetValue("imgEdit", "title");
                    rw[2] = oXml.GetValue("imgEdit", "imgdisable");
                    rw[3] = oXml.GetValue("imgEdit", "titledisable");
                    dt.Rows.Add(rw);
                    ds.Tables.Add(dt);
                    #endregion

                    #region "imgEditMain"
                    //imgEditMain
                    dt = new DataTable("imgEditMain");
                    dt.Columns.Add("img");
                    dt.Columns.Add("title");
                    dt.Columns.Add("imgdisable");
                    dt.Columns.Add("titledisable");
                    rw = dt.NewRow();
                    rw[0] = oXml.GetValue("imgEditMain", "img");
                    rw[1] = oXml.GetValue("imgEditMain", "title");
                    rw[2] = oXml.GetValue("imgEditMain", "imgdisable");
                    rw[3] = oXml.GetValue("imgEditMain", "titledisable");
                    dt.Rows.Add(rw);
                    ds.Tables.Add(dt);
                    #endregion

                    #region "imgLock"
                    dt = new DataTable("imgLock");
                    dt.Columns.Add("img");
                    dt.Columns.Add("title");
                    dt.Columns.Add("imgdisable");
                    dt.Columns.Add("titledisable");
                    rw = dt.NewRow();
                    rw[0] = oXml.GetValue("imgLock", "img");
                    rw[1] = oXml.GetValue("imgLock", "title");
                    rw[2] = oXml.GetValue("imgLock", "imgdisable");
                    rw[3] = oXml.GetValue("imgLock", "titledisable");
                    dt.Rows.Add(rw);
                    ds.Tables.Add(dt);
                    #endregion

                    #region "imgDelete"
                    dt = new DataTable("imgDelete");
                    dt.Columns.Add("img");
                    dt.Columns.Add("title");
                    dt.Columns.Add("imgdisable");
                    dt.Columns.Add("titledisable");
                    rw = dt.NewRow();
                    rw[0] = oXml.GetValue("imgDelete", "img");
                    rw[1] = oXml.GetValue("imgDelete", "title");
                    rw[2] = oXml.GetValue("imgDelete", "imgdisable");
                    rw[3] = oXml.GetValue("imgDelete", "titledisable");
                    dt.Rows.Add(rw);
                    ds.Tables.Add(dt);
                    #endregion

                    #region "colorDataGridRow"
                    dt = new DataTable("colorDataGridRow");
                    dt.Columns.Add("Even");
                    dt.Columns.Add("Odd");
                    dt.Columns.Add("MouseOver");
                    rw = dt.NewRow();
                    rw[0] = oXml.GetValue("colorDataGridRow", "Even");
                    rw[1] = oXml.GetValue("colorDataGridRow", "Odd");
                    rw[2] = oXml.GetValue("colorDataGridRow", "MouseOver");
                    dt.Rows.Add(rw);
                    ds.Tables.Add(dt);
                    #endregion

                    #region "imgSave"
                    dt = new DataTable("imgSave");
                    dt.Columns.Add("img");
                    dt.Columns.Add("title");
                    dt.Columns.Add("imgdisable");
                    dt.Columns.Add("titledisable");
                    rw = dt.NewRow();
                    rw[0] = oXml.GetValue("imgSave", "img");
                    rw[1] = oXml.GetValue("imgSave", "title");
                    rw[2] = oXml.GetValue("imgSave", "imgdisable");
                    rw[3] = oXml.GetValue("imgSave", "titledisable");
                    dt.Rows.Add(rw);
                    ds.Tables.Add(dt);
                    #endregion

                    #region "imgSaveOnly"
                    dt = new DataTable("imgSaveOnly");
                    dt.Columns.Add("img");
                    dt.Columns.Add("title");
                    dt.Columns.Add("imgdisable");
                    dt.Columns.Add("titledisable");
                    rw = dt.NewRow();
                    rw[0] = oXml.GetValue("imgSaveOnly", "img");
                    rw[1] = oXml.GetValue("imgSaveOnly", "title");
                    rw[2] = oXml.GetValue("imgSaveOnly", "imgdisable");
                    rw[3] = oXml.GetValue("imgSaveOnly", "titledisable");
                    dt.Rows.Add(rw);
                    ds.Tables.Add(dt);
                    #endregion

                    #region "imgSaveAdd"
                    dt = new DataTable("imgSaveAdd");
                    dt.Columns.Add("img");
                    dt.Columns.Add("title");
                    dt.Columns.Add("imgdisable");
                    dt.Columns.Add("titledisable");
                    rw = dt.NewRow();
                    rw[0] = oXml.GetValue("imgSaveAdd", "img");
                    rw[1] = oXml.GetValue("imgSaveAdd", "title");
                    rw[2] = oXml.GetValue("imgSaveAdd", "imgdisable");
                    rw[3] = oXml.GetValue("imgSaveAdd", "titledisable");
                    dt.Rows.Add(rw);
                    ds.Tables.Add(dt);
                    #endregion

                    #region "imgClose"
                    dt = new DataTable("imgClose");
                    dt.Columns.Add("img");
                    dt.Columns.Add("title");
                    dt.Columns.Add("imgdisable");
                    dt.Columns.Add("titledisable");
                    rw = dt.NewRow();
                    rw[0] = oXml.GetValue("imgClose", "img");
                    rw[1] = oXml.GetValue("imgClose", "title");
                    rw[2] = oXml.GetValue("imgClose", "imgdisable");
                    rw[3] = oXml.GetValue("imgClose", "titledisable");
                    dt.Rows.Add(rw);
                    ds.Tables.Add(dt);
                    #endregion

                    #region "imgAsc"
                    dt = new DataTable("imgAsc");
                    dt.Columns.Add("img");
                    dt.Columns.Add("title");
                    rw = dt.NewRow();
                    rw[0] = oXml.GetValue("imgAsc", "img");
                    rw[1] = oXml.GetValue("imgAsc", "title");
                    dt.Rows.Add(rw);
                    ds.Tables.Add(dt);
                    #endregion

                    #region "imgDesc"
                    dt = new DataTable("imgDesc");
                    dt.Columns.Add("img");
                    dt.Columns.Add("title");
                    rw = dt.NewRow();
                    rw[0] = oXml.GetValue("imgDesc", "img");
                    rw[1] = oXml.GetValue("imgDesc", "title");
                    dt.Rows.Add(rw);
                    ds.Tables.Add(dt);
                    #endregion

                    #region "imgClear"
                    dt = new DataTable("imgClear");
                    dt.Columns.Add("img");
                    dt.Columns.Add("title");
                    rw = dt.NewRow();
                    rw[0] = oXml.GetValue("imgClear", "img");
                    rw[1] = oXml.GetValue("imgClear", "title");
                    dt.Rows.Add(rw);
                    ds.Tables.Add(dt);
                    #endregion

                    #region "imgList"
                    dt = new DataTable("imgList");
                    dt.Columns.Add("img");
                    dt.Columns.Add("title");
                    rw = dt.NewRow();
                    rw[0] = oXml.GetValue("imgList", "img");
                    rw[1] = oXml.GetValue("imgList", "title");
                    dt.Rows.Add(rw);
                    ds.Tables.Add(dt);
                    #endregion

                    #region "imgViewDetail"
                    dt = new DataTable("imgViewDetail");
                    dt.Columns.Add("img");
                    dt.Columns.Add("title");
                    rw = dt.NewRow();
                    rw[0] = oXml.GetValue("imgViewDetail", "img");
                    rw[1] = oXml.GetValue("imgViewDetail", "title");
                    dt.Rows.Add(rw);
                    ds.Tables.Add(dt);
                    #endregion

                    #region "imgGo"
                    dt = new DataTable("imgGo");
                    dt.Columns.Add("img");
                    dt.Columns.Add("title");
                    rw = dt.NewRow();
                    rw[0] = oXml.GetValue("imgGo", "img");
                    rw[1] = oXml.GetValue("imgGo", "title");
                    dt.Rows.Add(rw);
                    ds.Tables.Add(dt);
                    #endregion

                    #region "imgStatus"
                    dt = new DataTable("imgStatus");
                    dt.Columns.Add("img");
                    dt.Columns.Add("title");
                    dt.Columns.Add("imgdisable");
                    dt.Columns.Add("titledisable");
                    rw = dt.NewRow();
                    rw[0] = oXml.GetValue("imgStatus", "img");
                    rw[1] = oXml.GetValue("imgStatus", "title");
                    rw[2] = oXml.GetValue("imgStatus", "imgdisable");
                    rw[3] = oXml.GetValue("imgStatus", "titledisable");
                    dt.Rows.Add(rw);
                    ds.Tables.Add(dt);
                    #endregion

                    #region "HardCode"
                    dt = new DataTable("MemberType");
                    dt.Columns.Add("GBK");
                    dt.Columns.Add("GSJ");
                    dt.Columns.Add("SOS");
                    dt.Columns.Add("GBK2");

                    rw = dt.NewRow();
                    rw[0] = oXml.GetValue("MemberType", "GBK");
                    rw[1] = oXml.GetValue("MemberType", "GSJ");
                    rw[2] = oXml.GetValue("MemberType", "SOS");
                    rw[3] = oXml.GetValue("MemberType", "GBK2");
                    dt.Rows.Add(rw);
                    ds.Tables.Add(dt);
                    #endregion

                    #region "cboYear"
                    dt = new DataTable("cboYear");
                    dt.Columns.Add("Text");
                    dt.Columns.Add("Value");
                    entries = oXml.GetEntryNames("cboYear");
                    for (i = 0; i < entries.Length; i++)
                    {
                        rw = dt.NewRow();
                        rw[0] = entries[i].ToString();
                        rw[1] = oXml.GetValue("cboYear", entries[i].ToString(), "");
                        dt.Rows.Add(rw);
                    }
                    ds.Tables.Add(dt);
                    #endregion

                    #region "cboMonth"
                    dt = new DataTable("cboMonth");
                    dt.Columns.Add("Value");
                    dt.Columns.Add("Text");
                    entries = oXml.GetEntryNames("cboMonth");
                    for (i = 0; i < entries.Length; i++)
                    {
                        rw = dt.NewRow();
                        rw[0] = entries[i].ToString();
                        rw[1] = oXml.GetValue("cboMonth", entries[i].ToString(), "");
                        dt.Rows.Add(rw);
                    }
                    ds.Tables.Add(dt);
                    #endregion

                    #region "imgGridAdd"
                    dt = new DataTable("imgGridAdd");
                    dt.Columns.Add("img");
                    dt.Columns.Add("title");
                    dt.Columns.Add("imgdisable");
                    dt.Columns.Add("titledisable");
                    rw = dt.NewRow();
                    rw[0] = oXml.GetValue("imgGridAdd", "img");
                    rw[1] = oXml.GetValue("imgGridAdd", "title");
                    rw[2] = oXml.GetValue("imgGridAdd", "imgdisable");
                    rw[3] = oXml.GetValue("imgGridAdd", "titledisable");
                    dt.Rows.Add(rw);
                    ds.Tables.Add(dt);
                    #endregion

                    #region "Document Default"
                    dt = new DataTable("default");
                    dt.Columns.Add("pagetitle");
                    dt.Columns.Add("yearnow");
                    dt.Columns.Add("companyname");
                    dt.Columns.Add("work_status");
                    dt.Columns.Add("SOS_MAX");
                    dt.Columns.Add("SOS_MIN");
                    rw = dt.NewRow();
                    rw[0] = oXml.GetValue("default", "pagetitle");
                    rw[1] = oXml.GetValue("default", "yearnow");
                    rw[2] = oXml.GetValue("default", "companyname");
                    rw[3] = oXml.GetValue("default", "work_status");
                    rw[4] = oXml.GetValue("default", "SOS_MAX");
                    rw[5] = oXml.GetValue("default", "SOS_MIN");
                    dt.Rows.Add(rw);
                    ds.Tables.Add(dt);
                    #endregion

                    Application["xmlconfig"] = ds;

                    #endregion
                }
                catch (Exception ex)
                {
                    return;
                }
            }

        }

    }
}
