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
    public partial class menu_list : PageBase
    {
        protected string _strSortList = "MenuOrder";
        protected string _strCriteria = string.Empty;

        #region "Private Method"

        protected bool SaveData()
        {
            MenuBLL objMenuBLL = new MenuBLL();
            DataTable dt = new DataTable();
            int intMenuID;
            string strMenuName;
            string strMenuNavigationUrl;
            string strMenuTarget;
            int intMenuParent;
            int intMenuOrder;
            char strCanView;
            char strCanInsert;
            char strCanEdit;
            char strCanDelete;
            char strCanApprove;
            char strCanExtra;
            string strStatus;
            string strRemark;
            string strLangCode;
            string strMenuName_Lang;
            bool boolResult = false;
            try
            {
                intMenuID = Helper.CInt(hddMenuID.Value);
                strMenuName = Helper.CStr(txtMenuName.Text);
                strMenuNavigationUrl = Helper.CStr(txtMenuNavigationUrl.Text);
                strMenuTarget = Helper.CStr(ddlMenuTarget.SelectedValue);
                intMenuParent = Helper.CInt(ddlMenuParent.SelectedValue);
                intMenuOrder = Helper.CInt(txtMenuOrder.Value);
                strCanView = chkCanView.Checked == true ? 'Y' : 'N';
                strCanInsert = chkCanInsert.Checked == true ? 'Y' : 'N';
                strCanEdit = chkCanEdit.Checked == true ? 'Y' : 'N';
                strCanDelete = chkCanDelete.Checked == true ? 'Y' : 'N';
                strCanApprove = chkCanApprove.Checked == true ? 'Y' : 'N';
                strCanExtra = chkCanExtra.Checked == true ? 'Y' : 'N';
                strStatus = rdoLstStatus.SelectedValue;
                strRemark = Helper.CStr(txtRemark.Text);
                switch ((Mode)ViewState["Mode"])
                {
                    case Mode.NEW:
                        objMenuBLL.Insert(ref intMenuID, strMenuName, strMenuNavigationUrl, strMenuTarget, intMenuParent, intMenuOrder,
                                              strCanView, strCanInsert, strCanEdit, strCanDelete, strCanApprove, strCanExtra,
                                              strStatus, strRemark, UserLoginName, ref _strMessage);
                        MsgBox(Icon.Information, string.Format(Resources.Message.CompleteInsert, Resources.Label.LabelMenuName));
                        boolResult = true;
                        break;
                    case Mode.EDIT:
                        objMenuBLL.Update(intMenuID, strMenuName, strMenuNavigationUrl, strMenuTarget, intMenuParent, intMenuOrder,
                                              strCanView, strCanInsert, strCanEdit, strCanDelete, strCanApprove, strCanExtra,
                                              strStatus, strRemark, UserLoginName, ref _strMessage);
                        for (int i = 0; i < gViewLang.Rows.Count; i++)
                        {
                            GridViewRow row = gViewLang.Rows[i];
                            Label lblLangCode = (Label)row.FindControl("lblLangCode");
                            AwTextBox txtMenuName_Lang = (AwTextBox)row.FindControl("txtMenuName_Lang");
                            strLangCode = lblLangCode.Text;
                            strMenuName_Lang = txtMenuName_Lang.Text;
                            dt = objMenuBLL.MenuLangSelectByMenuIDAndLangCode(intMenuID, strLangCode);
                            if (dt.Rows.Count > 0)
                            {
                                objMenuBLL.MenuLangUpdate(intMenuID, strLangCode, strMenuName_Lang, ref _strMessage);
                            }
                            else
                            {
                                objMenuBLL.MenuLangInsert(intMenuID, strLangCode, strMenuName_Lang, ref _strMessage);
                            }
                        }
                        MsgBox(Icon.Information, string.Format(Resources.Message.CompleteUpdate, Resources.Label.LabelMenuName));
                        boolResult = true;
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
                objMenuBLL.Dispose();
            }
            return boolResult;
        }

        protected void SetData(int intMenuID)
        {
            MenuBLL objMenuBLL = new MenuBLL();
            DataTable dt;
            DataRow dr;

            dt = objMenuBLL.SelectByID(intMenuID);
            if (dt.Rows.Count > 0)
            {
                InitializeControl();
                dr = dt.Rows[0];
                hddMenuID.Value = Helper.CStr(dr["MenuID"]);
                txtMenuName.Text = Helper.CStr(dr["MenuName"]);
                txtMenuNavigationUrl.Text = Helper.CStr(dr["MenuNavigationUrl"]);
                ddlMenuTarget.SelectedValue = Helper.CStr(dr["MenuTarget"]);
                ddlMenuParent.SelectedValue = Helper.CStr(dr["MenuParent"]);
                txtMenuOrder.Value = Helper.CStr(dr["MenuOrder"]);
                chkCanView.Checked = Helper.CStr(dr["CanView"]) == "Y" ? true : false;
                chkCanInsert.Checked = Helper.CStr(dr["CanInsert"]) == "Y" ? true : false;
                chkCanEdit.Checked = Helper.CStr(dr["CanEdit"]) == "Y" ? true : false;
                chkCanDelete.Checked = Helper.CStr(dr["CanDelete"]) == "Y" ? true : false;
                chkCanApprove.Checked = Helper.CStr(dr["CanApprove"]) == "Y" ? true : false;
                chkCanExtra.Checked = Helper.CStr(dr["CanExtra"]) == "Y" ? true : false;
                rdoLstStatus.SelectedValue = Helper.CStr(dr["Status"]);
                txtRemark.Text = Helper.CStr(dr["Remark"]);
                BindGridLang(Helper.CInt(dr["MenuID"]));
                gViewLang.Visible = true;
            }
            dt.Dispose();
            objMenuBLL.Dispose();
        }

        protected void InitializeControl()
        {
            DataTable dt = BindData.DropdownListByType("aw_MenuGetAllParent", "MenuName", "MenuId", "pCriteria", " And LangCode='" + LangCode + "'", Enumeration.DropDownListType.SelectAll);
            ddlMenuParent.DataSource = dt;
            ddlMenuParent.DataBind();
        }

        protected void LockKeyItem(bool boolLock)
        {

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
            MenuBLL objMenuBLL = new MenuBLL();
            DataTable dtList = new DataTable();
            try
            {
                int intTotalRecord = 0;
                _strCriteria = " AND MenuName LIKE '%" + Helper.ISql(txtMenuNameSearch.Text) + "%' ";
                _strCriteria += " AND MenuNavigationUrl LIKE '%" + Helper.ISql(txtMenuNavigationUrlSearch.Text) + "%' ";
                if (rdlStatusSearch.SelectedIndex != 0)
                {
                    _strCriteria += " AND Status ='" + rdlStatusSearch.SelectedValue + "' ";
                }
                _strCriteria += " ORDER BY " + SortExpression + " " + SortDirection;
                objMenuBLL.SelectPaging(ref dtList, ref intTotalRecord, _strCriteria, intPageIndex, PageSize, ref _strMessage);
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
                objMenuBLL.Dispose();
            }
        }

        protected void BindGridLang(int intMenuID)
        {
            MenuBLL objMenuBLL = new MenuBLL();
            DataTable dtLang = new DataTable();
            try
            {
                _strCriteria = " AND MenuID =" + intMenuID + "";
                dtLang = objMenuBLL.MenuLangSelectByCriteria(_strCriteria);
                gViewLang.DataSource = dtLang;
                gViewLang.DataBind();
            }
            catch (Exception ex)
            {
                MsgBox(Icon.Error, ex.Message);
            }
            finally
            {
                dtLang.Dispose();
                objMenuBLL.Dispose();
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
                string strScript = Helper.ReplaceScript(string.Format(Resources.Message.DeleteConfirm, dv["MenuName"].ToString()));
                //imgBtnDelete.Attributes.Add("onclick", "var result = confirm('" + strScript + "');alert(result);");
                //imgBtnDelete.Attributes.Add("onclick", "return btnClick();");
                imgBtnDelete.Attributes.Add("onclick", "return confirm('" + strScript + "');");
                if (!IsUserEdit)
                {
                    imgBtnEdit.Enabled = false;
                }
            }
            GridViewDataBound((System.Web.UI.WebControls.GridView)sender, e);
        }

        protected void gViewList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow gvRow;
            HiddenField hddMenuID;
            int intMenuID = 0;
            MenuBLL objMenuBLL = new MenuBLL();
            try
            {
                switch (e.CommandName.ToLower())
                {
                    case "view":
                        gvRow = gViewList.Rows[int.Parse(e.CommandArgument.ToString()) - 1];
                        hddMenuID = (HiddenField)gvRow.FindControl("hddMenuID");
                        intMenuID = Helper.CInt(hddMenuID.Value);
                        SetData(intMenuID);
                        SetPanel(Mode.VIEW);
                        break;
                    case "edit":
                        gvRow = gViewList.Rows[int.Parse(e.CommandArgument.ToString()) - 1];
                        hddMenuID = (HiddenField)gvRow.FindControl("hddMenuID");
                        intMenuID = Helper.CInt(hddMenuID.Value);
                        SetData(intMenuID);
                        SetPanel(Mode.EDIT);
                        break;
                    case "delete":
                        gvRow = gViewList.Rows[int.Parse(e.CommandArgument.ToString()) - 1];
                        hddMenuID = (HiddenField)gvRow.FindControl("hddMenuID");
                        intMenuID = Helper.CInt(hddMenuID.Value);
                        objMenuBLL.Delete(intMenuID, ref _strMessage);
                        MsgBox(Icon.Information, Resources.Message.CompleteDelete);
                        BindGridList(1);
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
                objMenuBLL.Dispose();
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

        #region "GridViewLang Event"

        protected void gViewLang_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowState == DataControlRowState.Alternate)
            {
                AwImageButton imgBtnDelete = (AwImageButton)e.Row.FindControl("imgBtnDelete");
                AwTextBox txtMenuName_Lang = (AwTextBox)e.Row.FindControl("txtMenuName_Lang");
                DataRowView dv = (DataRowView)e.Row.DataItem;
                imgBtnDelete.Attributes.Add("onclick", "document.getElementById('" + txtMenuName_Lang.ClientID + "').value='';return false;");
            }
            GridViewDataBound((System.Web.UI.WebControls.GridView)sender, e);
        }

        protected void gViewLang_Sorting(object sender, GridViewSortEventArgs e)
        {
            e.Cancel = true;
        }

        protected void gViewLang_RowEditing(object sender, GridViewEditEventArgs e)
        {
            e.Cancel = true;
        }

        protected void gViewLang_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                DataTable dt = BindData.DropdownListByType("aw_MenuGetAllParent", "MenuName", "MenuId", "pCriteria", " And LangCode='" + LangCode + "'", Enumeration.DropDownListType.SelectAll);
                ddlMenuParentSearch.DataSource = dt;
                ddlMenuParentSearch.DataBind();
                gViewLang.Visible = false;

                reqMenuName.ErrorMessage = String.Format(Resources.Message.RequiredField, Resources.Label.LabelMenuName);
                reqMenuParent.ErrorMessage = String.Format(Resources.Message.RequiredField, Resources.Label.LabelMenuParent);
                reqMenuTarget.ErrorMessage = String.Format(Resources.Message.RequiredField, Resources.Label.LabelMenuTarget);
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
            //System.Threading.Thread.Sleep(5000);
            BindGridList(1);
        }

        protected void lnkBtnAdd_Click(object sender, EventArgs e)
        {
            InitializeControl();
            SetPanel(Mode.NEW);
            LockKeyItem(false);
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
                // LockKeyItem(true);
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
