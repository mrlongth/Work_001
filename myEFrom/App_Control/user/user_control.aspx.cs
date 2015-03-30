using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using myDLL;
using Aware.WebControls;

namespace myEFrom.App_Control.user
{
    public partial class user_control : PageBase
    {

        protected void Page_Init(object sender, EventArgs e)
        {
            base.PermissionURL = "~/App_Control/user/person_list.aspx";
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            lblError.Text = "";
            if (!IsPostBack)
            {
                imgSaveOnly.Attributes.Add("onMouseOver", "src='../../images/button/save_add2.png'");
                imgSaveOnly.Attributes.Add("onMouseOut", "src='../../images/button/save_add.png'");

                ViewState["sort"] = "g_sort";
                ViewState["direction"] = "ASC";

                #region set QueryString

                IsUserEdit = false;
                IsUserDelete = false;


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

                if (Request.QueryString["IsUserEdit"] != null)
                {
                    if (Request.QueryString["IsUserEdit"].ToString() == "Y")
                    {
                        IsUserEdit = true;
                    }
                }

                if (Request.QueryString["IsUserDelete"] != null)
                {
                    if (Request.QueryString["IsUserDelete"].ToString() == "Y")
                    {
                        IsUserDelete = true;
                    }
                }

                if (Request.QueryString["person_code"] != null)
                {
                    ViewState["person_code"] = Request.QueryString["person_code"].ToString();
                }

                #endregion

                imgList_person.Attributes.Add("onclick", "OpenPopUp('900px','500px','94%','ค้นหาข้อมูลบุคคลากร' ,'../lov/person_lov.aspx?" +
                        "from=open_control&person_code='+getElementById('" + txtperson_code.ClientID + "').value+'" +
                        "&person_name='+getElementById('" + txtperson_name.ClientID + "').value+'" +
                       "&ctrl1=" + txtperson_code.ClientID + "&ctrl2=" + txtperson_name.ClientID + "&show=2', '2');return false;");

                imgClear_person.Attributes.Add("onclick", "document.getElementById('" + txtperson_code.ClientID + "').value='';document.getElementById('" + txtperson_name.ClientID + "').value=''; return false;");


                setData();

            }
        }

        private bool saveData()
        {
            bool blnResult = false;
            string strUpdatedBy = string.Empty;
            string struser_group_list = string.Empty;
            strUpdatedBy = Session["username"].ToString();
            CheckBox chkSelect;
            HiddenField hddg_code;
            foreach (GridViewRow gvRow in GridView1.Rows)
            {
                chkSelect = (CheckBox)gvRow.FindControl("chkSelect");
                hddg_code = (HiddenField)gvRow.FindControl("hddg_code");
                if (chkSelect.Checked)
                    struser_group_list += hddg_code.Value + ",";
            }
            if (struser_group_list.Length > 0)
                struser_group_list = struser_group_list.Substring(0, struser_group_list.Length - 1);
            var objEfUser = new cefUser();
            try
            {
                #region update
                if (objEfUser.SP_PERSON_USER_GROUP_UPD(hddperson_code.Value, struser_group_list, txtperson_code.Text ,  strUpdatedBy))
                {
                    blnResult = true;
                }
                #endregion
            }
            catch (Exception ex)
            {

                lblError.Text = ex.Message;
            }
            finally
            {
                objEfUser.Dispose();
            }
            return blnResult;
        }

        private void setData()
        {
            cPerson objPerson = new cPerson();
            DataTable dt;
            DataSet ds = new DataSet();
            var strCriteria = string.Empty;
            var strMessage = string.Empty;
            try
            {
                strCriteria = " and person_code = '" + ViewState["person_code"].ToString() + "' ";
                objPerson.SP_PERSON_LIST_SEL(strCriteria, ref ds, ref strMessage);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    #region get Data
                    hddef_user_group_list.Value = Helper.CStr(dt.Rows[0]["ef_user_group_list"]);
                    hddperson_code.Value = dt.Rows[0]["person_code"].ToString();
                    lblperson_name.Text = dt.Rows[0]["title_name"].ToString() + dt.Rows[0]["person_thai_name"] + " " + dt.Rows[0]["person_thai_surname"];
                    #endregion

                    BindGridItem();

                    strCriteria = " and person_code = '" + Helper.CStr(dt.Rows[0]["ef_approve_for"].ToString()) + "' ";
                    objPerson.SP_PERSON_LIST_SEL(strCriteria, ref ds, ref strMessage);
                    dt = ds.Tables[0];              
                    if (dt.Rows.Count > 0)
                    {
                        #region get Data
                        txtperson_code.Text = dt.Rows[0]["person_code"].ToString();
                        txtperson_name.Text = dt.Rows[0]["person_thai_name"] + " " + dt.Rows[0]["person_thai_surname"];
                        #endregion

                        BindGridItem();

                    }
              
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }


        #region GridView1 Event

        private void BindGridItem()
        {
            cCommon oCommon = new cCommon();
            string strMessage = string.Empty, strCriteria = string.Empty;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                strCriteria = " Select * from  general where g_type = 'ef_user_group' Order by g_sort ";
                if (oCommon.SEL_SQL(strCriteria, ref ds, ref strMessage))
                {
                    dt = ds.Tables[0];
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
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
                DataRowView dv = (DataRowView)e.Row.DataItem;
                CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelect");
                string[] ef_user_group_list = hddef_user_group_list.Value.Split(',');
                if (ef_user_group_list.Contains(Helper.CStr(dv["g_code"])))
                {
                    chkSelect.Checked = true;
                }
                else
                {
                    chkSelect.Checked = false;
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


        #endregion


        protected void imgSaveOnly_Click(object sender, ImageClickEventArgs e)
        {
            if (saveData())
            {
                MsgBox("บันทึกข้อมูลสมบูรณ์");
                string strScript1 = "$('#divdes1').text().replace('เพิ่ม','แก้ไข');ClosePopUpListPost('1','1');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
            }
        }

    }
}