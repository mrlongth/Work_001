using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using myDLL;

namespace myEFrom.App_Control.user
{
    public partial class user_group_menu_list : PageBase
    {


        private DataTable dtUserGroup
        {
            get
            {
                if (ViewState["dtUserGroup"] == null)
                {
                    cCommon oCommon = new cCommon();
                    string strMessage = string.Empty, strCriteria = string.Empty;
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    strCriteria = " Select * from  general where g_type = 'ef_user_group' Order by g_sort ";
                    if (oCommon.SEL_SQL(strCriteria, ref ds, ref strMessage))
                    {
                        dt = ds.Tables[0];
                    }
                    ViewState["dtUserGroup"] = dt;
                }
                return (DataTable)ViewState["dtUserGroup"];
            }
            set
            {
                ViewState["dtUserGroup"] = value;
            }
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            lblError.Text = "";
            if (!IsPostBack)
            {
                imgSaveOnly.Attributes.Add("onMouseOver", "src='../../images/button/save_add2.png'");
                imgSaveOnly.Attributes.Add("onMouseOut", "src='../../images/button/save_add.png'");


                imgCancel.Attributes.Add("onMouseOver", "src='../../images/button/cancel2.png'");
                imgCancel.Attributes.Add("onMouseOut", "src='../../images/button/cancel.png'");

                ViewState["sort"] = "MenuOrder";
                ViewState["direction"] = "ASC";

                imgSaveOnly.Visible = false;
                imgSaveOnly.Visible = base.IsUserEdit;

                GridView1.Visible = false;
                InitcboUserGroup();
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            PermissionURL = "";
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

        private void InitcboUserGroup()
        {
            var strCode = cboUserGroup.SelectedValue;
            cboUserGroup.Items.Clear();
            cboUserGroup.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
            int i;
            for (i = 0; i <= dtUserGroup.Rows.Count - 1; i++)
            {
                cboUserGroup.Items.Add(new ListItem(dtUserGroup.Rows[i]["g_name"].ToString(), dtUserGroup.Rows[i]["g_code"].ToString()));
            }
            if (cboUserGroup.Items.FindByValue(strCode) != null)
            {
                cboUserGroup.SelectedIndex = -1;
                cboUserGroup.Items.FindByValue(strCode).Selected = true;
            }
        }


        private bool saveData()
        {
            bool blnResult = false;
            cefUser objEfUser = new cefUser();
            try
            {
                string strCanView;
                string strCanInsert;
                string strCanEdit;
                string strCanDelete;
                string strCanApprove;
                string strCanExtra;
                if (objEfUser.SP_USER_GROUP_MENU_DEL(cboUserGroup.SelectedValue))
                {
                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        HiddenField hddMenuID = (HiddenField)row.FindControl("hddMenuID");
                        CheckBox chkCanView = (CheckBox)row.FindControl("chkCanView");
                        CheckBox chkCanInsert = (CheckBox)row.FindControl("chkCanInsert");
                        CheckBox chkCanEdit = (CheckBox)row.FindControl("chkCanEdit");
                        CheckBox chkCanDelete = (CheckBox)row.FindControl("chkCanDelete");
                        CheckBox chkCanApprove = (CheckBox)row.FindControl("chkCanApprove");
                        CheckBox chkCanExtra = (CheckBox)row.FindControl("chkCanExtra");
                        int intMenuId = Helper.CInt(hddMenuID.Value);
                        strCanView = chkCanView.Checked ? "Y" : "N";
                        strCanInsert = chkCanInsert.Checked ? "Y" : "N";
                        strCanEdit = chkCanEdit.Checked ? "Y" : "N";
                        strCanDelete = chkCanDelete.Checked ? "Y" : "N";
                        strCanApprove = chkCanApprove.Checked ? "Y" : "N";
                        strCanExtra = chkCanExtra.Checked ? "Y" : "N";
                        objEfUser.SP_USER_GROUP_MENU_INS(cboUserGroup.SelectedValue, intMenuId, strCanView, strCanInsert, strCanEdit, strCanDelete, strCanApprove, strCanExtra);
                    }
                }
                blnResult = true;
                MsgBox("บันทึกข้อมูลสมบูรณ์");
            }
            catch (Exception ex)
            {
                blnResult = false;
                lblError.Text = ex.Message;
            }
            finally
            {
                objEfUser.Dispose();
            }
            return blnResult;
        }

        private void BindGridView()
        {
            cefUser objEfUser = new cefUser();
            DataTable dt = new DataTable();
            try
            {
                dt = objEfUser.SP_USER_GROUP_MENU_GROUP(cboUserGroup.SelectedValue);
                GridView1.DataSource = dt;
                GridView1.DataBind();
                GridView1.Visible = true;
                imgSaveOnly.Visible = true;

            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
            finally
            {
                objEfUser.Dispose();
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.Header))
            {

                for (int iCol = 0; iCol < e.Row.Cells.Count; iCol++)
                {
                    e.Row.Cells[iCol].Attributes.Add("class", "table_h");
                    e.Row.Cells[iCol].Wrap = false;
                }

                ((CheckBox)e.Row.FindControl("chkViewAll")).Attributes.Add("onclick", "javascript:SelectAll('" +
                (e.Row.FindControl("chkViewAll")).ClientID + "',3)");


                ((CheckBox)e.Row.FindControl("chkInsertAll")).Attributes.Add("onclick", "javascript:SelectAll('" +
                (e.Row.FindControl("chkInsertAll")).ClientID + "',4)");


                ((CheckBox)e.Row.FindControl("chkEditAll")).Attributes.Add("onclick", "javascript:SelectAll('" +
                (e.Row.FindControl("chkEditAll")).ClientID + "',5)");

                ((CheckBox)e.Row.FindControl("chkDeleteAll")).Attributes.Add("onclick", "javascript:SelectAll('" +
                (e.Row.FindControl("chkDeleteAll")).ClientID + "',6)");


                ((CheckBox)e.Row.FindControl("chkApproveAll")).Attributes.Add("onclick", "javascript:SelectAll('" +
                (e.Row.FindControl("chkApproveAll")).ClientID + "',7)");

                ((CheckBox)e.Row.FindControl("chkExtraAll")).Attributes.Add("onclick", "javascript:SelectAll('" +
                (e.Row.FindControl("chkExtraAll")).ClientID + "',8)");


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
                DataRowView dv = (DataRowView)e.Row.DataItem;

                CheckBox chkCanView = (CheckBox)e.Row.FindControl("chkCanView");

                CheckBox chkCanInsert = (CheckBox)e.Row.FindControl("chkCanInsert");

                CheckBox chkCanEdit = (CheckBox)e.Row.FindControl("chkCanEdit");

                CheckBox chkCanDelete = (CheckBox)e.Row.FindControl("chkCanDelete");

                CheckBox chkCanApprove = (CheckBox)e.Row.FindControl("chkCanApprove");

                CheckBox chkCanExtra = (CheckBox)e.Row.FindControl("chkCanExtra");

                if (dv["IsCanView"].ToString().Equals("Y"))
                {
                    chkCanView.Visible = true;
                }
                if (dv["IsCanInsert"].ToString().Equals("Y"))
                {
                    chkCanInsert.Visible = true;
                }
                if (dv["IsCanEdit"].ToString().Equals("Y"))
                {
                    chkCanEdit.Visible = true;
                }
                if (dv["IsCanDelete"].ToString().Equals("Y"))
                {
                    chkCanDelete.Visible = true;
                }
                if (dv["IsCanApprove"].ToString().Equals("Y"))
                {
                    chkCanApprove.Visible = true;
                }
                if (dv["IsCanExtra"].ToString().Equals("Y"))
                {
                    chkCanExtra.Visible = true;
                }
                // Check Visible checkbox
                if (dv["CanView"].ToString().Equals("Y"))
                {
                    chkCanView.Checked = true;
                }
                if (dv["CanInsert"].ToString().Equals("Y"))
                {
                    chkCanInsert.Checked = true;
                }
                if (dv["CanEdit"].ToString().Equals("Y"))
                {
                    chkCanEdit.Checked = true;
                }
                if (dv["CanDelete"].ToString().Equals("Y"))
                {
                    chkCanDelete.Checked = true;
                }
                if (dv["CanApprove"].ToString().Equals("Y"))
                {
                    chkCanApprove.Checked = true;
                }
                if (dv["CanExtra"].ToString().Equals("Y"))
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
            saveData();
        }

        protected void imgCancel_Click(object sender, ImageClickEventArgs e)
        {
            imgSaveOnly.Visible = false;
            GridView1.Visible = false;
            cboUserGroup.SelectedIndex = 0;
        }

        protected void cboUserGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboUserGroup.SelectedIndex == 0)
            {
                imgSaveOnly.Visible = false;
                GridView1.Visible = false;
                cboUserGroup.SelectedIndex = 0;
            }
            else
                BindGridView();
        }


    }


}