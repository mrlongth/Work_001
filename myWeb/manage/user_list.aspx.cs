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

namespace myWeb.manage
{
    public partial class user_list : PageBase
    {
        protected string _strSortList = "UserID";
        protected string _strCriteria = string.Empty;

        #region "Private Method"

        protected bool SaveData()
        {
            bool boolResult = false;
            UserBLL objUserBLL = new UserBLL();
            DataTable dt = new DataTable();
            int sintUserGroupID = 0;
            int intUserId = 0;
            string strLoginName = string.Empty;
            string strPassword = string.Empty;
            string strEmpCode = string.Empty;
            string strNameTH = string.Empty;
            string strNameEN = string.Empty;
            string strEmail = string.Empty;
            int intDefaultPageSize = 0;
            string strStatus = "Y";
            string strRemark = string.Empty;
            try
            {
                intUserId = Helper.CInt(hddUserID.Value);
              //  sintUserGroupID = short.Parse(ddlUserGroupID.SelectedItem.Value);
                strLoginName = txtLoginName.Text;
                strPassword = Cryptorengine.Encrypt(txtLoginName.Text, true);
                strEmpCode = txtEmpCode.Text;
                strNameTH = txtNameTH.Text;
                strNameEN = txtNameEN.Text;
                strEmail = txtEmail.Text;
                strRemark = txtRemark.Text;

                if (!string.IsNullOrEmpty(ddlDefaultPageSize.SelectedItem.Value))
                {
                    intDefaultPageSize = Helper.CInt(ddlDefaultPageSize.SelectedItem.Value);
                }

                strStatus = rdlStatus.SelectedValue;
                switch ((Mode)ViewState["Mode"])
                {
                    case Mode.NEW:
                        objUserBLL.Insert(ref intUserId, sintUserGroupID, strLoginName, strPassword, strEmpCode, strNameTH, strNameEN, strEmail,
                            intDefaultPageSize, strStatus, strRemark, UserLoginName, ref _strMessage);
                        MsgBox(string.Format(Resources.Message.CompleteInsert, Resources.Label.LabelLoginName));
                        boolResult = true;
                        break;
                    case Mode.EDIT:
                        objUserBLL.Update(intUserId, sintUserGroupID, strLoginName, strPassword, strEmpCode, strNameTH, strNameEN, strEmail,
                            intDefaultPageSize, strStatus, strRemark, UserLoginName, ref _strMessage);
                        MsgBox(Icon.Information, string.Format(Resources.Message.CompleteUpdate, Resources.Label.LabelLoginName));
                        boolResult = true;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgBox(Icon.Error, ErrorMessageCheck(ex.Message, Resources.Label.LabelLoginName));
            }
            finally
            {
                objUserBLL.Dispose();
            }
            return boolResult;
        }

        protected void SetData(int intUserId)
        {
            UserBLL objUserBLL = new UserBLL();
            DataTable dt;
            DataRow dr;

            dt = objUserBLL.SelectByID(intUserId);
            if (dt.Rows.Count > 0)
            {
                InitializeControl();
                dr = dt.Rows[0];
                hddUserID.Value = Helper.CStr(dr["UserID"]);
                ddlUserGroupID.SelectedValue = Helper.CStr(dr["UserGroupID"]);
                txtLoginName.Text = Helper.CStr(dr["LoginName"]);
                txtPassword.Text = Cryptorengine.Decrypt(Helper.CStr(dr["Password"]), true);
                txtEmpCode.Text = Helper.CStr(dr["EmpCode"]);
                txtNameTH.Text = Helper.CStr(dr["NameTH"]);
                txtNameEN.Text = Helper.CStr(dr["NameEN"]);
                txtEmail.Text = Helper.CStr(dr["Email"]);
                if (Helper.CInt(dr["DefaultPageSize"]) != 0)
                    ddlDefaultPageSize.SelectedValue = Helper.CStr(dr["DefaultPageSize"]);
                rdlStatus.SelectedValue = Helper.CStr(dr["Status"]);
                txtRemark.Text = Helper.CStr(dr["Remark"]);
            }
            dt.Dispose();
            objUserBLL.Dispose();
        }

        protected void InitializeControl()
        {
            DataTable dt = BinderDropdownList.BindderGetGeneralType("UserGroupType", Enumeration.DropDownListType.PleaseSelect);
            ddlUserGroupID.DataSource = dt;
            ddlUserGroupID.DataBind();

            DataTable dt2 = BinderDropdownList.BindderGetGeneralType("PageSize", Enumeration.DropDownListType.PleaseSelect);
            ddlDefaultPageSize.DataSource = dt2;
            ddlDefaultPageSize.DataBind();

        }

        protected void DataPager(string strDataPagerValue)
        {
            string[] aDataPager = strDataPagerValue.Split("$".ToCharArray());
            if (aDataPager.Length > 0)
            {
                if (aDataPager[0].ToString() == "DataPager2")
                {
                    string strPage = aDataPager[2].ToString().Substring(5);
                    BindGridList(int.Parse(strPage));
                }
            }

        }

        protected void SetPanel(Mode eMode)
        {
            switch (eMode)
            {
                case Mode.SEARCH:
                    ViewState["Mode"] = Mode.SEARCH;
                    panelList.Visible = true;
                    panelControl.Visible = false;
                    break;
                case Mode.NEW:
                    ViewState["Mode"] = Mode.NEW;
                    panelList.Visible = false;
                    panelControl.Visible = true;
                    lnkBtnSaveOnly.Visible = true;
                    lnkBtnSaveClose.Visible = true;
                    SetControls(panelControl, eMode);
                    break;
                case Mode.EDIT:
                    ViewState["Mode"] = Mode.EDIT;
                    panelList.Visible = false;
                    panelControl.Visible = true;
                    lnkBtnSaveOnly.Visible = true;
                    lnkBtnSaveClose.Visible = true;
                    SetControls(panelControl, eMode);
                    break;
                case Mode.VIEW:
                    ViewState["Mode"] = Mode.VIEW;
                    panelList.Visible = false;
                    panelControl.Visible = true;
                    lnkBtnSaveOnly.Visible = false;
                    lnkBtnSaveClose.Visible = false;
                    SetControls(panelControl, eMode);
                    break;
                default:
                    break;
            }
        }

        protected void BindGridList(int intPageIndex)
        {
            UserBLL objUserBLL = new UserBLL();
            DataTable dtList = new DataTable();
            try
            {
                int intTotalRecord = 0;
                _strCriteria = string.Empty;
                if (ddlListUserGroup.SelectedIndex != 0)
                {
                    _strCriteria += " AND UserGroupID='" + ddlListUserGroup.SelectedValue + "' ";
                }
                if (!string.IsNullOrEmpty(txtListLoginName.Text))
                {
                    _strCriteria += " AND LoginName LIKE '%" + Helper.ISql(txtListLoginName.Text) + "%' ";
                }
                if (!string.IsNullOrEmpty(txtListEmpCode.Text))
                {
                    _strCriteria += " AND EmpCode LIKE '%" + Helper.ISql(txtListEmpCode.Text) + "%' ";
                }
                if (!string.IsNullOrEmpty(txtListNameTH.Text))
                {
                    _strCriteria += " AND NameTH LIKE '%" + Helper.ISql(txtListNameTH.Text) + "%' ";
                }
                if (!string.IsNullOrEmpty(txtListNameEN.Text))
                {
                    _strCriteria += " AND NameEN LIKE '%" + Helper.ISql(txtListNameEN.Text) + "%' ";
                }
                if (!string.IsNullOrEmpty(txtListEmail.Text))
                {
                    _strCriteria += " AND Email LIKE '%" + Helper.ISql(txtListEmail.Text) + "%' ";
                }
                if (rdlListStatus.SelectedIndex != 0)
                {
                    _strCriteria += " AND Status='" + rdlListStatus.SelectedValue + "' ";
                }
                _strCriteria += " ORDER BY " + SortExpression + " " + SortDirection;
                objUserBLL.SelectPaging(ref dtList, ref intTotalRecord, _strCriteria, intPageIndex, PageSize, ref _strMessage);
                PageIndex = intPageIndex;
                gViewList.DataSource = dtList;
                gViewList.DataBind();
                litDataPager.Text = Feedback360.Utility.DataPager.SetDataPager(intTotalRecord, intPageIndex, PageSize, PagingSize);
            }
            catch (Exception ex)
            {
                MsgBox(Icon.Error, ex.Message);
            }
            finally
            {
                dtList.Dispose();
                objUserBLL.Dispose();
            }
        }

        #endregion

        #region "GridViewList Event"

        protected void gViewList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowState == DataControlRowState.Alternate)
            {
                AwImageButton imgBtnView = (AwImageButton)e.Row.FindControl("imgBtnView");
                AwImageButton imgBtnEdit = (AwImageButton)e.Row.FindControl("imgBtnEdit");
                AwImageButton imgBtnDelete = (AwImageButton)e.Row.FindControl("imgBtnDelete");
                DataRowView dv = (DataRowView)e.Row.DataItem;
                string strScript = Helper.ReplaceScript(string.Format(Resources.Message.DeleteConfirm, dv["LoginName"].ToString()));
                imgBtnDelete.Attributes.Add("onclick", "return confirm('" + strScript + "');");
                if (!IsUserEdit) imgBtnEdit.Enabled = false;
                if (!IsUserDelete) imgBtnDelete.Enabled = false;
                if (!IsUserView) imgBtnView.Enabled = false;
            }
            GridViewDataBound((System.Web.UI.WebControls.GridView)sender, e);
        }

        protected void gViewList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow gvRow;
            HiddenField hddUserID;
            int intUserID = 0;
            UserBLL objUserBLL = new UserBLL();
            try
            {
                switch (e.CommandName.ToLower())
                {
                    case "view":
                        gvRow = gViewList.Rows[int.Parse(e.CommandArgument.ToString()) - 1];
                        hddUserID = (HiddenField)gvRow.FindControl("hddUserID");
                        intUserID = Helper.CInt(hddUserID.Value);
                        SetData(intUserID);
                        SetPanel(Mode.VIEW);
                        break;
                    case "edit":
                        gvRow = gViewList.Rows[int.Parse(e.CommandArgument.ToString()) - 1];
                        hddUserID = (HiddenField)gvRow.FindControl("hddUserID");
                        intUserID = Helper.CInt(hddUserID.Value);
                        SetData(intUserID);
                        SetPanel(Mode.EDIT);
                        break;
                    case "delete":
                        gvRow = gViewList.Rows[int.Parse(e.CommandArgument.ToString()) - 1];
                        hddUserID = (HiddenField)gvRow.FindControl("hddUserID");
                        intUserID = Helper.CInt(hddUserID.Value);
                        objUserBLL.Delete(intUserID, ref _strMessage);
                        BindGridList(1);
                        MsgBox(Icon.Information, Resources.Message.CompleteDelete);
                        break;
                    case "sort":
                        GridViewSoring((GridView)sender, e);
                        BindGridList(1);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgBox(Icon.Error, ErrorMessageCheck(ex.Message, Resources.Label.LabelMenuName));
            }
            finally
            {
                objUserBLL.Dispose();
            }
        }

        protected void gViewList_Sorting(object sender, GridViewSortEventArgs e)
        {
            e.Cancel = true;
        }

        protected void gViewList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            e.Cancel = true;
        }

        protected void gViewList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            e.Cancel = true;
        }

        #endregion

        #region "PageSize Event"

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageSize = int.Parse(ddlPageSize.SelectedValue);
            BindGridList(1);
        }

        protected void ddlPageSize_DataBound(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (ddlPageSize.Items.FindByValue(PageSize.ToString()) != null)
                {
                    ddlPageSize.SelectedIndex = -1;
                    ddlPageSize.Items.FindByValue(PageSize.ToString()).Selected = true;
                }
            }
        }

        #endregion

        #region "Page Event"

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CompareValidator2.ErrorMessage = String.Format(Resources.Message.RequiredSelectField, Resources.Label.LabelUserGroup);
                reqLoginName.ErrorMessage = String.Format(Resources.Message.RequiredField, Resources.Label.LabelLoginName);

                DataTable dt = BinderDropdownList.BindderGetGeneralType("UserGroupType", Enumeration.DropDownListType.SelectAll);
                ddlListUserGroup.DataSource = dt;
                ddlListUserGroup.DataBind();

                SortExpression = _strSortList;
                SetPanel(Mode.SEARCH);
                BindGridList(1);
            }
            else
            {
                if (Request.Form["__EVENTTARGET"].ToString().IndexOf("DataPager2$") != -1)
                {
                    DataPager(Request.Form["__EVENTTARGET"].ToString());
                }
            }
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            SettingUserAccessRight(gViewList);
        }

        protected void lnkBtnSearch_Click(object sender, EventArgs e)
        {
            BindGridList(1);
        }

        protected void lnkBtnAdd_Click(object sender, EventArgs e)
        {
            InitializeControl();
            SetPanel(Mode.NEW);
        }

        protected void lnkBtnCancel_Click(object sender, EventArgs e)
        {
            SetPanel(Mode.SEARCH);
            BindGridList(PageIndex);
        }

        protected void lnkBtnSaveOnly_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                SetPanel(Mode.EDIT);
            }
        }

        protected void lnkBtnSaveClose_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                SetPanel(Mode.SEARCH);
                BindGridList(PageIndex);
            }
        }

        #endregion
    
    }
}
