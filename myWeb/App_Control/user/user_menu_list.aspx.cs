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
using Aware.WebControls;
using myDLL;

namespace myWeb.App_Control.user
{
    public partial class user_menu_list : PageBase
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            lblError.Text = "";
            if (!IsPostBack)
            {
                imgSaveOnly.Attributes.Add("onMouseOver", "src='../../images/button/save_add2.png'");
                imgSaveOnly.Attributes.Add("onMouseOut", "src='../../images/button/save_add.png'");

                imgFind.Attributes.Add("onMouseOver", "src='../../images/button/Search2.png'");
                imgFind.Attributes.Add("onMouseOut", "src='../../images/button/Search.png'");

                imgCancel.Attributes.Add("onMouseOver", "src='../../images/button/cancel2.png'");
                imgCancel.Attributes.Add("onMouseOut", "src='../../images/button/cancel.png'");

                string strYear = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["yearnow"].ToString();

                imgList.Attributes.Add("onclick", "OpenPopUp('850px','450px','94%','ค้นหาข้อมูลผู้ใช้งาน' ,'../lov/user_lov.aspx?loginname='+document.getElementById('" + txtloginname.ClientID + "').value+'" +
                                                                              "&person_name='+document.getElementById('" + txtperson_name.ClientID + "').value+'" +
                                                                              "&ctrl1=" + txtloginname.ClientID + "&ctrl2=" + txtperson_name.ClientID +
                                                                              "&show=1', '1');return false;");
                imgClear.Attributes.Add("onclick", "document.getElementById('" + txtloginname.ClientID + "').value='';" +
                                                                                                    "document.getElementById('" + txtperson_name.ClientID + "').value=''; " +
                                                                                                    "return false;");

                ViewState["sort"] = "loginname";
                ViewState["direction"] = "ASC";

                TabContainer1.Visible = false;
                imgFind.Visible = true;
                imgSaveOnly.Visible = false;
                chkdirector_lock.Visible = false;
                chkunit_lock.Visible = false;

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
            //  this.imgSaveOnly.Click += new System.Web.UI.ImageClickEventHandler(this.imgSaveOnly_Click);
        }
        #endregion


        public string myDirectorCode
        {
            get
            {
                if (ViewState["myDirectorCode"] == null)
                {
                    ViewState["myDirectorCode"] =  string.Empty;
                }
                return ViewState["myDirectorCode"].ToString();
            }
            set
            {
                ViewState["myDirectorCode"] = value;
            }
        }

        public string myUnitCodeList
        {
            get
            {
                if (ViewState["myUnitCodeList"] == null)
                {
                    ViewState["myUnitCodeList"] = string.Empty;
                }
                return ViewState["myUnitCodeList"].ToString();
            }
            set
            {
                ViewState["myUnitCodeList"] = value;
            }
        }



        private bool saveData(string strUserID)
        {
            bool blnResult = false;
            string strMessage = string.Empty;
            string strActive = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty;
            string strScript = string.Empty;
            int i;
            cUser_menu oUser_menu = new cUser_menu();
            cUser oUser = new cUser();
            DataSet ds = new DataSet();
            try
            {
                string strCanView;
                string strCanInsert;
                string strCanEdit;
                string strCanDelete;
                string strCanApprove;
                string strCanExtra;
                oUser_menu.SP_USER_MENU_DEL(strUserID, ref strMessage);
                for (i = 0; i <= (GridView1.Rows.Count - 1); i++)
                {
                    GridViewRow row = GridView1.Rows[i];
                    HiddenField hddMenuID = (HiddenField)row.FindControl("hddMenuID");
                    CheckBox chkCanView = (CheckBox)row.FindControl("chkCanView");
                    CheckBox chkCanInsert = (CheckBox)row.FindControl("chkCanInsert");
                    CheckBox chkCanEdit = (CheckBox)row.FindControl("chkCanEdit");
                    CheckBox chkCanDelete = (CheckBox)row.FindControl("chkCanDelete");
                    CheckBox chkCanApprove = (CheckBox)row.FindControl("chkCanApprove");
                    CheckBox chkCanExtra = (CheckBox)row.FindControl("chkCanExtra");
                    string intMenuId = hddMenuID.Value;
                    strCanView = chkCanView.Checked == true ? "Y" : "N";
                    strCanInsert = chkCanInsert.Checked == true ? "Y" : "N";
                    strCanEdit = chkCanEdit.Checked == true ? "Y" : "N";
                    strCanDelete = chkCanDelete.Checked == true ? "Y" : "N";
                    strCanApprove = chkCanApprove.Checked == true ? "Y" : "N";
                    strCanExtra = chkCanExtra.Checked == true ? "Y" : "N";
                    //if (chkCanView.Checked | chkCanInsert.Checked | chkCanEdit.Checked | chkCanDelete.Checked | chkCanApprove.Checked | chkCanExtra.Checked)
                    //{
                    oUser_menu.SP_USER_MENU_INS(strUserID, intMenuId, strCanView, strCanInsert, strCanEdit, strCanDelete, strCanApprove, strCanExtra, UserLoginName, ref strMessage);
                    // }
                }
                string strperson_group_list = string.Empty;
                for (i = 0; i <= (GridView2.Rows.Count - 1); i++)
                {
                    GridViewRow row = GridView2.Rows[i];
                    CheckBox chkPersonGroup = (CheckBox)row.FindControl("chkPersonGroup");
                    if (chkPersonGroup.Checked)
                    {
                        Label lblperson_group_code = (Label)row.FindControl("lblperson_group_code");
                        strperson_group_list += lblperson_group_code.Text + ",";
                    }
                }
                if (strperson_group_list.Length > 0)
                {
                    strperson_group_list = strperson_group_list.Substring(0, strperson_group_list.Length - 1);
                }
                string strdirector_lock = "N";
                if (chkdirector_lock.Checked)
                {
                    strdirector_lock = "Y";
                }
                string strunit_lock = "N";
                if (chkunit_lock.Checked)
                {
                    strunit_lock = "Y";
                }
                oUser.SP_USER_PERSON_GROUP_UPD(strUserID, strperson_group_list, strdirector_lock, strunit_lock, UserLoginName, ref strMessage);
                blnResult = true;
                MsgBox("บันทึกข้อมูลสมบูรณ์");
            }
            catch (Exception ex)
            {
                blnResult = false;
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oUser_menu.Dispose();
            }
            return blnResult;
        }

        private void BindGridView()
        {
            cUser_menu objUserMenu = new cUser_menu();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string strloginname = txtloginname.Text;

            try
            {
                strCriteria = " and loginname='" + strloginname + "' ";
                strCriteria += " And Menu_Status='Y' ";

                objUserMenu.SP_USER_MENU_MANAGE_SEL(strCriteria, ref ds, ref strMessage);
                GridView1.DataSource = ds;
                GridView1.DataBind();

                cUser oUser = new cUser();
                string strCheck = string.Empty;
                strCheck = " and [loginname] = '" + txtloginname.Text.Trim() + "' ";
                if (!oUser.SP_USER_SEL(strCheck, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        string strScript =
                              "alert('ไม่สามารถบันข้อมูลได้ เนื่องจาก" +
                              "\\nไม่พบข้อมูล Username : " + txtloginname.Text.Trim() + "');";
                        MsgBox(strScript);
                        return;
                    }
                    else
                    {
                        this.myDirectorCode = ds.Tables[0].Rows[0]["director_code"].ToString();
                        this.myUnitCodeList = ds.Tables[0].Rows[0]["unit_code_list"].ToString(); 
                        hddperson_group_list.Value = ds.Tables[0].Rows[0]["person_group_list"].ToString();
                        if (ds.Tables[0].Rows[0]["director_lock"].ToString().Equals("Y"))
                        {
                            chkdirector_lock.Checked = true;
                        }
                        else
                        {
                            chkdirector_lock.Checked = false;
                        }
                        if (ds.Tables[0].Rows[0]["unit_lock"].ToString().Equals("Y"))
                        {
                            chkunit_lock.Checked = true;
                        }
                        else
                        {
                            chkunit_lock.Checked = false;
                        }
                    }
                }

                if (GridView1.Rows.Count > 0)
                {
                    TabContainer1.Visible = true;
                    TabContainer1.ActiveTabIndex = 0;
                    imgSaveOnly.Visible = true;
                    chkdirector_lock.Visible = true;
                    chkunit_lock.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                objUserMenu.Dispose();
                ds.Dispose();
            }
        }

        private void BindGridView2()
        {
            cPerson_group oPerson_group = new cPerson_group();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string strActive = string.Empty;
            strCriteria = strCriteria + "  And  (c_active ='Y') ";
            try
            {
                if (!oPerson_group.SP_PERSON_GROUP_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    GridView2.DataSource = ds.Tables[0];
                    GridView2.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oPerson_group.Dispose();
                ds.Dispose();
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.Header))
            {
                //Find the checkbox control in header and add an attribute
                //((CheckBox)e.Row.FindControl("cbSelectAll")).Attributes.Add("onclick", "javascript:SelectAll('" +
                //        ((CheckBox)e.Row.FindControl("cbSelectAll")).ClientID + "')");

                for (int iCol = 0; iCol < e.Row.Cells.Count; iCol++)
                {
                    e.Row.Cells[iCol].Attributes.Add("class", "table_h");
                    e.Row.Cells[iCol].Wrap = false;
                }

                ((CheckBox)e.Row.FindControl("chkViewAll")).Attributes.Add("onclick", "javascript:SelectAll('" +
                 ((CheckBox)e.Row.FindControl("chkViewAll")).ClientID + "',3)");

                //CheckBox chkCanViewAll = (CheckBox)e.Row.FindControl("chkViewAll");
                //if (chkCanViewAll != null)
                //{
                //    chkCanViewAll.Attributes.Add("onclick", "SelectAllCheckboxes(this, 'chkCanView'); ");
                //}


                ((CheckBox)e.Row.FindControl("chkInsertAll")).Attributes.Add("onclick", "javascript:SelectAll('" +
              ((CheckBox)e.Row.FindControl("chkInsertAll")).ClientID + "',4)");


                //CheckBox chkCanInsertAll = (CheckBox)e.Row.FindControl("chkInsertAll");
                //if (chkCanInsertAll != null)
                //{
                //    chkCanInsertAll.Attributes.Add("onclick", "SelectAllCheckboxes(this, 'chkCanInsert'); ");
                //}


                ((CheckBox)e.Row.FindControl("chkEditAll")).Attributes.Add("onclick", "javascript:SelectAll('" +
              ((CheckBox)e.Row.FindControl("chkEditAll")).ClientID + "',5)");



                //CheckBox chkCanUpdateAll = (CheckBox)e.Row.FindControl("chkEditAll");
                //if (chkCanUpdateAll != null)
                //{
                //    chkCanUpdateAll.Attributes.Add("onclick", "SelectAllCheckboxes(this, 'chkCanEdit');");
                //}

                ((CheckBox)e.Row.FindControl("chkDeleteAll")).Attributes.Add("onclick", "javascript:SelectAll('" +
           ((CheckBox)e.Row.FindControl("chkDeleteAll")).ClientID + "',6)");


                //CheckBox chkCanDeleteAll = (CheckBox)e.Row.FindControl("chkDeleteAll");
                //if (chkCanDeleteAll != null)
                //{
                //    chkCanDeleteAll.Attributes.Add("onclick", "SelectAllCheckboxes(this, 'chkCanDelete');");
                //}

                ((CheckBox)e.Row.FindControl("chkApproveAll")).Attributes.Add("onclick", "javascript:SelectAll('" +
      ((CheckBox)e.Row.FindControl("chkApproveAll")).ClientID + "',7)");

                //CheckBox chkCanApproveAll = (CheckBox)e.Row.FindControl("chkApproveAll");
                //if (chkCanApproveAll != null)
                //{
                //    chkCanApproveAll.Attributes.Add("onclick", "SelectAllCheckboxes(this, 'chkCanApprove');");
                //}

                ((CheckBox)e.Row.FindControl("chkExtraAll")).Attributes.Add("onclick", "javascript:SelectAll('" +
  ((CheckBox)e.Row.FindControl("chkExtraAll")).ClientID + "',8)");

                //CheckBox chkCanExtraAll = (CheckBox)e.Row.FindControl("chkExtraAll");
                //if (chkCanExtraAll != null)
                //{
                //    chkCanExtraAll.Attributes.Add("onclick", "SelectAllCheckboxes(this, 'chkCanExtra');");
                //}


            }
            else if (e.Row.RowType.Equals(DataControlRowType.DataRow) || e.Row.RowState.Equals(DataControlRowState.Alternate))
            {
                #region Set datagrid row color
                string strEvenColor, strOddColor, strMouseOverColor;
                strEvenColor = ((DataSet)Application["xmlconfig"]).Tables["colorDataGridRow"].Rows[0]["Even"].ToString();
                strOddColor = ((DataSet)Application["xmlconfig"]).Tables["colorDataGridRow"].Rows[0]["Odd"].ToString();
                strMouseOverColor = ((DataSet)Application["xmlconfig"]).Tables["colorDataGridRow"].Rows[0]["MouseOver"].ToString();

                e.Row.Style.Add("valign", "top");
                e.Row.Style.Add("cursor", "hand");
                e.Row.Attributes.Add("onMouseOver", "this.style.backgroundColor='" + strMouseOverColor + "'");

                if (e.Row.RowState.Equals(DataControlRowState.Alternate))
                {
                    e.Row.Attributes.Add("bgcolor", strOddColor);
                    e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor='" + strOddColor + "'");
                }
                else
                {
                    e.Row.Attributes.Add("bgcolor", strEvenColor);
                    e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor='" + strEvenColor + "'");
                }
                #endregion
                Label lblNo = (Label)e.Row.FindControl("lblNo");
                int nNo = (GridView1.PageSize * GridView1.PageIndex) + e.Row.RowIndex + 1;
                lblNo.Text = nNo.ToString();

                CheckBox chkCanView = (CheckBox)e.Row.FindControl("chkCanView");
                Label lblCanViewStatus = (Label)e.Row.FindControl("lblCanViewStatus");
                Label lblMenuCanViewStatus = (Label)e.Row.FindControl("lblMenuCanViewStatus");

                CheckBox chkCanInsert = (CheckBox)e.Row.FindControl("chkCanInsert");
                Label lblCanInsertStatus = (Label)e.Row.FindControl("lblCanInsertStatus");
                Label lblMenuCanInsertStatus = (Label)e.Row.FindControl("lblMenuCanInsertStatus");

                CheckBox chkCanEdit = (CheckBox)e.Row.FindControl("chkCanEdit");
                Label lblCanEditStatus = (Label)e.Row.FindControl("lblCanEditStatus");
                Label lblMenuCanEditStatus = (Label)e.Row.FindControl("lblMenuCanEditStatus");

                CheckBox chkCanDelete = (CheckBox)e.Row.FindControl("chkCanDelete");
                Label lblCanDeleteStatus = (Label)e.Row.FindControl("lblCanDeleteStatus");
                Label lblMenuCanDeleteStatus = (Label)e.Row.FindControl("lblMenuCanDeleteStatus");

                CheckBox chkCanApprove = (CheckBox)e.Row.FindControl("chkCanApprove");
                Label lblCanApproveStatus = (Label)e.Row.FindControl("lblCanApproveStatus");
                Label lblMenuCanApproveStatus = (Label)e.Row.FindControl("lblMenuCanApproveStatus");

                CheckBox chkCanExtra = (CheckBox)e.Row.FindControl("chkCanExtra");
                Label lblCanExtraStatus = (Label)e.Row.FindControl("lblCanExtraStatus");
                Label lblMenuCanExtraStatus = (Label)e.Row.FindControl("lblMenuCanExtraStatus");

                chkCanView.Visible = false;
                chkCanView.Checked = false;

                chkCanInsert.Visible = false;
                chkCanInsert.Checked = false;

                chkCanEdit.Visible = false;
                chkCanEdit.Checked = false;

                chkCanDelete.Visible = false;
                chkCanDelete.Checked = false;

                chkCanApprove.Visible = false;
                chkCanApprove.Checked = false;

                chkCanExtra.Visible = false;
                chkCanExtra.Checked = false;

                if (lblMenuCanViewStatus.Text.Equals("Y"))
                {
                    chkCanView.Visible = true;
                }
                if (lblCanViewStatus.Text.Equals("Y"))
                {
                    chkCanView.Checked = true;
                }

                if (lblMenuCanInsertStatus.Text.Equals("Y"))
                {
                    chkCanInsert.Visible = true;
                }
                if (lblCanInsertStatus.Text.Equals("Y"))
                {
                    chkCanInsert.Checked = true;
                }


                if (lblMenuCanEditStatus.Text.Equals("Y"))
                {
                    chkCanEdit.Visible = true;
                }
                if (lblCanEditStatus.Text.Equals("Y"))
                {
                    chkCanEdit.Checked = true;
                }


                if (lblMenuCanDeleteStatus.Text.Equals("Y"))
                {
                    chkCanDelete.Visible = true;
                }
                if (lblCanDeleteStatus.Text.Equals("Y"))
                {
                    chkCanDelete.Checked = true;
                }

                if (lblMenuCanApproveStatus.Text.Equals("Y"))
                {
                    chkCanApprove.Visible = true;
                }
                if (lblCanApproveStatus.Text.Equals("Y"))
                {
                    chkCanApprove.Checked = true;
                }

                if (lblMenuCanExtraStatus.Text.Equals("Y"))
                {
                    chkCanExtra.Visible = true;
                }
                if (lblCanExtraStatus.Text.Equals("Y"))
                {
                    chkCanExtra.Checked = true;
                }

            }
        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.Header))
            {
                #region Create Item Header
                bool bSort = false;
                int i = 0;
                for (i = 0; i < GridView1.Columns.Count; i++)
                {
                    if (ViewState["sort"].Equals(GridView1.Columns[i].SortExpression))
                    {
                        bSort = true;
                        break;
                    }
                }
                if (bSort)
                {
                    foreach (System.Web.UI.Control c in e.Row.Controls[i].Controls)
                    {
                        if (c.GetType().ToString().Equals("System.Web.UI.WebControls.DataControlLinkButton"))
                        {
                            if (ViewState["direction"].Equals("ASC"))
                            {
                                ((LinkButton)c).Text += "<img border=0 src='" + ((DataSet)Application["xmlconfig"]).Tables["imgAsc"].Rows[0]["img"].ToString() + "'>";
                            }
                            else
                            {
                                ((LinkButton)c).Text += "<img border=0 src='" + ((DataSet)Application["xmlconfig"]).Tables["imgDesc"].Rows[0]["img"].ToString() + "'>";
                            }
                        }
                    }
                }
                #endregion
            }
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                if (ViewState["sort"].ToString().Equals(e.SortExpression.ToString()))
                {
                    if (ViewState["direction"].Equals("DESC"))
                        ViewState["direction"] = "ASC";
                    else
                        ViewState["direction"] = "DESC";
                }
                else
                {
                    ViewState["sort"] = e.SortExpression;
                    ViewState["direction"] = "ASC";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        protected void imgSaveOnly_Click(object sender, ImageClickEventArgs e)
        {

            string strMessage = string.Empty;
            string strScript = string.Empty;
            cUser oUser = new cUser();
            DataSet ds = new DataSet();
            try
            {
                string strCheck = string.Empty;
                strCheck = " and [loginname] = '" + txtloginname.Text.Trim() + "' ";
                if (!oUser.SP_USER_SEL(strCheck, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        strScript =
                            "alert('ไม่สามารถบันข้อมูลได้ เนื่องจาก" +
                            "\\nไม่พบข้อมูล Username : " + txtloginname.Text.Trim() + "');";
                        MsgBox(strScript);
                        return;
                    }
                }
                string strUserID = ds.Tables[0].Rows[0]["userID"].ToString();
                saveData(strUserID);
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oUser.Dispose();
                ds.Dispose();
            }
        }

        protected void imgFind_Click(object sender, ImageClickEventArgs e)
        {
            BindGridView();
            BindGridView2();
        }

        protected void imgCancel_Click(object sender, ImageClickEventArgs e)
        {
            TabContainer1.Visible = false;
            imgSaveOnly.Visible = false;
            chkdirector_lock.Visible = false;
            chkunit_lock.Visible = false;
            txtloginname.Text = "";
            txtperson_name.Text = "";
        }

        protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.Header))
            {
                #region Create Item Header
                bool bSort = false;
                int i = 0;
                for (i = 0; i < GridView2.Columns.Count; i++)
                {
                    if (ViewState["sort"].Equals(GridView2.Columns[i].SortExpression))
                    {
                        bSort = true;
                        break;
                    }
                }
                if (bSort)
                {
                    foreach (System.Web.UI.Control c in e.Row.Controls[i].Controls)
                    {
                        if (c.GetType().ToString().Equals("System.Web.UI.WebControls.DataControlLinkButton"))
                        {
                            if (ViewState["direction"].Equals("ASC"))
                            {
                                ((LinkButton)c).Text += "<img border=0 src='" + ((DataSet)Application["xmlconfig"]).Tables["imgAsc"].Rows[0]["img"].ToString() + "'>";
                            }
                            else
                            {
                                ((LinkButton)c).Text += "<img border=0 src='" + ((DataSet)Application["xmlconfig"]).Tables["imgDesc"].Rows[0]["img"].ToString() + "'>";
                            }
                        }
                    }
                }
                #endregion
            }
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.Header))
            {
                for (int iCol = 0; iCol < e.Row.Cells.Count; iCol++)
                {
                    e.Row.Cells[iCol].Attributes.Add("class", "table_h");
                    e.Row.Cells[iCol].Wrap = false;
                }
                ((CheckBox)e.Row.FindControl("chkAll")).Attributes.Add("onclick", "javascript:SelectAll2('" +
                     ((CheckBox)e.Row.FindControl("chkAll")).ClientID + "',3)");
            }
            else if (e.Row.RowType.Equals(DataControlRowType.DataRow) || e.Row.RowState.Equals(DataControlRowState.Alternate))
            {
                #region Set datagrid row color
                string strEvenColor, strOddColor, strMouseOverColor;
                strEvenColor = ((DataSet)Application["xmlconfig"]).Tables["colorDataGridRow"].Rows[0]["Even"].ToString();
                strOddColor = ((DataSet)Application["xmlconfig"]).Tables["colorDataGridRow"].Rows[0]["Odd"].ToString();
                strMouseOverColor = ((DataSet)Application["xmlconfig"]).Tables["colorDataGridRow"].Rows[0]["MouseOver"].ToString();

                e.Row.Style.Add("valign", "top");
                e.Row.Style.Add("cursor", "hand");
                e.Row.Attributes.Add("onMouseOver", "this.style.backgroundColor='" + strMouseOverColor + "'");

                if (e.Row.RowState.Equals(DataControlRowState.Alternate))
                {
                    e.Row.Attributes.Add("bgcolor", strOddColor);
                    e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor='" + strOddColor + "'");
                }
                else
                {
                    e.Row.Attributes.Add("bgcolor", strEvenColor);
                    e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor='" + strEvenColor + "'");
                }
                #endregion

                Label lblNo = (Label)e.Row.FindControl("lblNo");
                Label lblperson_group_code = (Label)e.Row.FindControl("lblperson_group_code");
                int nNo = (GridView2.PageSize * GridView2.PageIndex) + e.Row.RowIndex + 1;
                lblNo.Text = nNo.ToString();
                string[] strperson_group_list = hddperson_group_list.Value.Split(',');
                CheckBox chkPersonGroup = (CheckBox)e.Row.FindControl("chkPersonGroup");
                chkPersonGroup.Checked = false;
                for (int i = 0; i <= (strperson_group_list.GetUpperBound(0)); i++)
                {
                    if (strperson_group_list[i] == lblperson_group_code.Text)
                    {
                        chkPersonGroup.Checked = true;
                    }
                }
            }
        }



        private void BindGridView3()
        {
            cUnit oUnit = new cUnit();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string strActive = string.Empty;
            strCriteria = strCriteria + "  And  (c_active ='Y') and director_code= '" + this.myDirectorCode + "' ";
            try
            {
                if (!oUnit.SP_SEL_UNIT(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    GridView3.DataSource = ds.Tables[0];
                    GridView3.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oUnit.Dispose();
                ds.Dispose();
            }
        }
        protected void GridView3_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.Header))
            {
                #region Create Item Header
                bool bSort = false;
                int i = 0;
                for (i = 0; i < GridView3.Columns.Count; i++)
                {
                    if (ViewState["sort"].Equals(GridView3.Columns[i].SortExpression))
                    {
                        bSort = true;
                        break;
                    }
                }
                if (bSort)
                {
                    foreach (System.Web.UI.Control c in e.Row.Controls[i].Controls)
                    {
                        if (c.GetType().ToString().Equals("System.Web.UI.WebControls.DataControlLinkButton"))
                        {
                            if (ViewState["direction"].Equals("ASC"))
                            {
                                ((LinkButton)c).Text += "<img border=0 src='" + ((DataSet)Application["xmlconfig"]).Tables["imgAsc"].Rows[0]["img"].ToString() + "'>";
                            }
                            else
                            {
                                ((LinkButton)c).Text += "<img border=0 src='" + ((DataSet)Application["xmlconfig"]).Tables["imgDesc"].Rows[0]["img"].ToString() + "'>";
                            }
                        }
                    }
                }
                #endregion
            }
        }

        protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.Header))
            {
                for (int iCol = 0; iCol < e.Row.Cells.Count; iCol++)
                {
                    e.Row.Cells[iCol].Attributes.Add("class", "table_h");
                    e.Row.Cells[iCol].Wrap = false;
                }
                ((CheckBox)e.Row.FindControl("chkUnitAll")).Attributes.Add("onclick", "javascript:SelectAll2('" +
                     ((CheckBox)e.Row.FindControl("chkUnitAll")).ClientID + "',3)");
            }
            else if (e.Row.RowType.Equals(DataControlRowType.DataRow) || e.Row.RowState.Equals(DataControlRowState.Alternate))
            {
                #region Set datagrid row color
                string strEvenColor, strOddColor, strMouseOverColor;
                strEvenColor = ((DataSet)Application["xmlconfig"]).Tables["colorDataGridRow"].Rows[0]["Even"].ToString();
                strOddColor = ((DataSet)Application["xmlconfig"]).Tables["colorDataGridRow"].Rows[0]["Odd"].ToString();
                strMouseOverColor = ((DataSet)Application["xmlconfig"]).Tables["colorDataGridRow"].Rows[0]["MouseOver"].ToString();

                e.Row.Style.Add("valign", "top");
                e.Row.Style.Add("cursor", "hand");
                e.Row.Attributes.Add("onMouseOver", "this.style.backgroundColor='" + strMouseOverColor + "'");

                if (e.Row.RowState.Equals(DataControlRowState.Alternate))
                {
                    e.Row.Attributes.Add("bgcolor", strOddColor);
                    e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor='" + strOddColor + "'");
                }
                else
                {
                    e.Row.Attributes.Add("bgcolor", strEvenColor);
                    e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor='" + strEvenColor + "'");
                }
                #endregion

                Label lblNo = (Label)e.Row.FindControl("lblNo");
                Label lblunit_code = (Label)e.Row.FindControl("lblunit_code");
                int nNo = (GridView2.PageSize * GridView2.PageIndex) + e.Row.RowIndex + 1;
                lblNo.Text = nNo.ToString();
                string[] strunit_list = this.UnitCodeList.Split(',');
                CheckBox chkUnit = (CheckBox)e.Row.FindControl("chkUnit");
                chkUnit.Checked = false;
                for (int i = 0; i <= (strunit_list.GetUpperBound(0)); i++)
                {
                    if (strunit_list[i] == lblunit_code.Text)
                    {
                        chkUnit.Checked = true;
                    }
                }
            }
        }




    }


}