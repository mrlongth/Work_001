using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Threading;
using System.Text;
using myDLL;

namespace myEFrom.App_Control.open
{
    public partial class open_list : PageBase
    {

        #region private data
        private string strRecordPerPage;
        private string strPageNo = "1";
        private bool[] blnAccessRight = new bool[5] { false, false, false, false, false };
        private cefOpen objEfOpen = new cefOpen();

        private string BudgetType
        {
            get
            {
                if (ViewState["BudgetType"] == null)
                {
                    if (Request.QueryString["budget_type"] != null)
                    {
                        ViewState["BudgetType"] = Helper.CStr(Request.QueryString["budget_type"]);
                    }
                    else
                    {
                        ViewState["BudgetType"] = "B";
                    }
                }
                return ViewState["BudgetType"].ToString();
            }
            set
            {
                ViewState["BudgetType"] = value;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //Thread.Sleep(2000);
            if (!IsPostBack)
            {
                imgNew.Attributes.Add("onMouseOver", "src='../../images/button/save2.png'");
                imgNew.Attributes.Add("onMouseOut", "src='../../images/button/save.png'");

                imgFind.Attributes.Add("onMouseOver", "src='../../images/button/Search2.png'");
                imgFind.Attributes.Add("onMouseOut", "src='../../images/button/Search.png'");

                imgList_open.Attributes.Add("onclick", "OpenPopUp('800px','400px','93%','ค้นหาข้อมูลรายการขออนุมัติเบิกจ่าย' ,'../lov/open_lov.aspx?" +
                                                    "budget_type='+document.forms[0]." + cboBudget_type.UniqueID + ".options[document.forms[0]." + cboBudget_type.UniqueID + ".selectedIndex].value+" +
                                                    "'&year='+document.forms[0]." + cboYear.UniqueID + ".options[document.forms[0]." + cboYear.UniqueID + ".selectedIndex].value+" +
                                                    "'&item_code='+document.forms[0]." + txtopen_code.UniqueID + ".value+" +
                                                    "'&item_name='+document.forms[0]." + txtopen_title.UniqueID + ".value+" +
                                                    "'&ctrl1=" + txtopen_code.ClientID + "&ctrl2=" + txtopen_title.ClientID + "', '1');return false;");

                imgClear_open.Attributes.Add("onclick", "document.forms[0]." + txtopen_code.UniqueID + ".value='';" +
                                        "document.forms[0]." + txtopen_title.UniqueID + ".value='';return false;");



                imgList_person.Attributes.Add("onclick", "OpenPopUp('900px','500px','94%','ค้นหาข้อมูลบุคคลากร' ,'../lov/person_lov.aspx?" +
                     "from=open_control&person_code='+getElementById('" + txtperson_code.ClientID + "').value+'" +
                     "&person_name='+getElementById('" + txtperson_name.ClientID + "').value+'" +
                    "&ctrl1=" + txtperson_code.ClientID + "&ctrl2=" + txtperson_name.ClientID + "&show=1', '1');return false;");

                imgClear_person.Attributes.Add("onclick", "document.getElementById('" + txtperson_code.ClientID + "').value='';document.getElementById('" + txtperson_name.ClientID + "').value=''; return false;");

                txtfrom_date.Text = cCommon.CheckDate(DateTime.Now.AddMonths(-6).ToShortDateString());
                txtto_date.Text = cCommon.CheckDate(DateTime.Now.ToShortDateString());

                imgNew.Attributes.Add("onclick", "OpenPopUp('950px','550px','95%','เพิ่ม" + base.PageDes + "','open_control.aspx?budget_type='+document.forms[0]." + cboBudget_type.UniqueID + ".options[document.forms[0]." + cboBudget_type.UniqueID + ".selectedIndex].value+" + "'&mode=add&page=0','1');return false;");
                imgNew.Visible = base.IsUserNew;

                
                ViewState["sort"] = "open_doc";
                ViewState["direction"] = "ASC";
                InitcboYear();
                InitcboBudgetType();
                InitcboDirector();
                BindGridView(0);

            }
            else
            {
                if (Request.Form["ctl00$ASPxRoundPanel1$ContentPlaceHolder2$GridView1$ctl01$cboPerPage"] != null)
                {
                    strRecordPerPage = Request.Form["ctl00$ASPxRoundPanel1$ContentPlaceHolder2$GridView1$ctl01$cboPerPage"].ToString();
                    strPageNo = Request.Form["ctl00$ASPxRoundPanel1$ContentPlaceHolder2$GridView1$ctl01$txtPage"].ToString();
                }
                if (txthpage.Value != string.Empty)
                {
                    BindGridView(int.Parse(strPageNo) - 1);
                    //BindGridView(int.Parse(txthpage.Value));
                    txthpage.Value = string.Empty;
                }
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            PermissionURL = "";
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "RegisterScript", "createDate('" + txtfrom_date.ClientID + "','" + DateTime.Now.Date.ToString("dd/MM/yyyy") + "');createDate('" + txtto_date.ClientID + "','" + DateTime.Now.Date.ToString("dd/MM/yyyy") + "','" + DateTime.Now.Date.ToString("dd/MM/yyyy") + "');", true);

        }

        #region private function

        private void InitcboYear()
        {
            string strYear = string.Empty;
            strYear = cboYear.SelectedValue;
            if (strYear.Equals(""))
            {
                strYear = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["yearnow"].ToString();
            }
            DataTable odt;
            int i;
            cboYear.Items.Clear();
            odt = ((DataSet)Application["xmlconfig"]).Tables["cboYear"];
            for (i = 0; i <= odt.Rows.Count - 1; i++)
            {
                cboYear.Items.Add(new ListItem(odt.Rows[i]["Text"].ToString(), odt.Rows[i]["Value"].ToString()));
            }
            if (cboYear.Items.FindByValue(strYear) != null)
            {
                cboYear.SelectedIndex = -1;
                cboYear.Items.FindByValue(strYear).Selected = true;
            }
        }

        private void InitcboDirector()
        {
            cDirector oDirector = new cDirector();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strDirector_code = string.Empty;
            string strYear = cboYear.SelectedValue;
            strDirector_code = cboDirector.SelectedValue;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and director_year = '" + strYear + "'  and  c_active='Y' ";
            if (DirectorLock == "Y")
            {
                strCriteria += " and director_code = '" + DirectorCode + "' ";
            }
            if (this.BudgetType == "R")
            {
                strCriteria = strCriteria + " and budget_type <> 'B' ";
            }
            else
            {
                strCriteria = strCriteria + " and budget_type <> 'R' ";
            }
            if (oDirector.SP_SEL_DIRECTOR(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboDirector.Items.Clear();
                cboDirector.Items.Add(new ListItem("--- เลือกทั้งหมด ---", ""));
                int i;
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboDirector.Items.Add(new ListItem(dt.Rows[i]["director_name"].ToString(), dt.Rows[i]["director_code"].ToString()));
                }
                if (cboDirector.Items.FindByValue(strDirector_code) != null)
                {
                    cboDirector.SelectedIndex = -1;
                    cboDirector.Items.FindByValue(strDirector_code).Selected = true;
                }
                InitcboUnit();
            }
        }

        private void InitcboUnit()
        {
            cUnit oUnit = new cUnit();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strUnit_code = cboUnit.SelectedValue;
            string strDirector_code = cboDirector.SelectedValue;
            string strYear = cboYear.SelectedValue;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and unit.unit_year = '" + strYear + "'  and  unit.c_active='Y' ";
            strCriteria += " and unit.director_code = '" + strDirector_code + "' ";
            if (UnitLock == "Y")
            {
                strCriteria += " and substring(unit.unit_code,4,5) in (" + this.UnitCodeList + ") ";
            }

            if (this.BudgetType == "R")
            {
                strCriteria = strCriteria + " and unit.budget_type <> 'B' ";
            }
            else
            {
                strCriteria = strCriteria + " and unit.budget_type <> 'R' ";
            }
            if (oUnit.SP_SEL_UNIT(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboUnit.Items.Clear();
                cboUnit.Items.Add(new ListItem("--- เลือกทั้งหมด ---", ""));
                int i;
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboUnit.Items.Add(new ListItem(dt.Rows[i]["unit_name"].ToString(), dt.Rows[i]["unit_code"].ToString()));
                }
                if (cboUnit.Items.FindByValue(strUnit_code) != null)
                {
                    cboUnit.SelectedIndex = -1;
                    cboUnit.Items.FindByValue(strUnit_code).Selected = true;
                }
            }
        }

        private void InitcboBudgetType()
        {
            cCommon oCommon = new cCommon();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strCode = cboBudget_type.SelectedValue;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " Select * from  general where g_type = 'budget_type' and g_code <> 'M' Order by g_sort ";
            if (oCommon.SEL_SQL(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboBudget_type.Items.Clear();
                int i;
                cboBudget_type.Items.Add(new ListItem("--- เลือกทั้งหมด ---", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboBudget_type.Items.Add(new ListItem(dt.Rows[i]["g_name"].ToString(), dt.Rows[i]["g_code"].ToString()));
                }
                if (cboBudget_type.Items.FindByValue(strCode) != null)
                {
                    cboBudget_type.SelectedIndex = -1;
                    cboBudget_type.Items.FindByValue(strCode).Selected = true;
                }
            }
        }

        #endregion

        private void cboPerPage_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            GridView1.PageSize = int.Parse(strRecordPerPage);
            BindGridView(0);
        }

        private void imgGo_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            BindGridView(int.Parse(strPageNo) - 1);
        }

        protected void imgFind_Click(object sender, ImageClickEventArgs e)
        {
            BindGridView(0);
        }

        private void BindGridView(int nPageNo)
        {
            //InitcboYear();
            cefOpen objefOpen = new cefOpen();
            DataTable dt = new DataTable();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string stropen_doc = string.Empty;
            string strbudget_plan_year = string.Empty;
            string stropen_code = string.Empty;
            string stropen_title = string.Empty;
            string strunit_code = string.Empty;
            string strdirector_code = string.Empty;
            string strbudget_type = string.Empty;
            string strapprove_status = string.Empty;
            string strperson_code = string.Empty;
            string strperson_name = string.Empty;

            #region Criteria
            stropen_doc = txtopen_doc.Text;
            strbudget_plan_year = cboYear.SelectedValue;
            strdirector_code = cboDirector.SelectedValue;
            strunit_code = cboUnit.SelectedValue;
            stropen_code = txtopen_code.Text.Trim();
            stropen_title = txtopen_title.Text.Trim();
            strbudget_type = cboBudget_type.SelectedValue;
            strapprove_status = cboApproveStatus.SelectedValue;
            strperson_name = txtperson_name.Text.Trim();
            strperson_code = txtperson_code.Text.Trim();
            var strbegin_date = txtfrom_date.Text.Length > 0 ? cCommon.SeekDate(txtfrom_date.Text) : "";
            var strend_date = txtto_date.Text.Length > 0 ? cCommon.SeekDate(txtto_date.Text) : "";

            if (!strbudget_plan_year.Equals(""))
            {
                strCriteria = strCriteria + "  And  (open_year = '" + strbudget_plan_year + "') ";
            }
            if (!strbudget_type.Equals(""))
            {
                strCriteria = strCriteria + "  And  (budget_type = '" + strbudget_type + "') ";
            }

            if (!stropen_doc.Equals(""))
            {
                strCriteria = strCriteria + "  And  (open_doc ='" + stropen_doc + "') ";
            }

            if (!strdirector_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (director_code ='" + strdirector_code + "') ";
            }

            if (!strunit_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (unit_code ='" + strunit_code + "') ";
            }

            if (!stropen_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (open_code like '%" + stropen_code + "%') ";
            }
            if (!stropen_title.Equals(""))
            {
                strCriteria = strCriteria + "  And  (open_title like '%" + stropen_title + "%') ";
            }

            if (!strapprove_status.Equals(""))
            {
                strCriteria = strCriteria + "  And  (approve_head_status = '" + strapprove_status + "') ";
            }
            if (!strperson_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (person_code like '%" + strperson_code + "%') ";
            }
            if (!strperson_name.Equals(""))
            {
                strCriteria = strCriteria + "  And  (person_thai_name like '%" + strperson_name + "%'  " +
                                            "  OR person_thai_surname like '%" + strperson_name + "%'  " +
                                            "  OR '" + strperson_name + "' like ('%'+person_thai_name+'%'+person_thai_surname+'%')) ";
            }

            if (!strbegin_date.Equals(""))
            {
                strCriteria += "  And  (open_date >= '" + strbegin_date + "') ";
            }

            if (!strend_date.Equals(""))
            {
                strCriteria += "  And  (open_date <= '" + strend_date + "') ";
            }


            if (base.UserGroupCode != "Admin")
            {
                strCriteria += " and person_code ='" + base.PersonCode + "' ";
            }

            #endregion

            try
            {
                dt = objefOpen.SP_OPEN_COUNT_HEAD_SEL(strCriteria);
                try
                {
                    GridView1.PageIndex = nPageNo;
                    txthTotalRecord.Value = dt.Rows.Count.ToString();
                    dt.DefaultView.Sort = ViewState["sort"] + " " + ViewState["direction"];
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                catch
                {
                    GridView1.PageIndex = 0;
                    txthTotalRecord.Value = dt.Rows.Count.ToString();
                    dt.DefaultView.Sort = ViewState["sort"] + " " + ViewState["direction"];
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                objefOpen.Dispose();
                if (GridView1.Rows.Count > 0)
                {
                    GridView1.TopPagerRow.Visible = true;
                }
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

                Label lblNo = (Label)e.Row.FindControl("lblNo");
                Label lblopen_doc = (Label)e.Row.FindControl("lblopen_doc");
                int nNo = (GridView1.PageSize * GridView1.PageIndex) + e.Row.RowIndex + 1;
                lblNo.Text = nNo.ToString();
                DataRowView dv = (DataRowView)e.Row.DataItem;
                HiddenField hhdopen_head_id = (HiddenField)e.Row.FindControl("hhdopen_head_id");
                Label lblApprove_status = (Label)e.Row.FindControl("lblApprove_status");
                Repeater Repeater1 = (Repeater)e.Row.FindControl("Repeater1");
                if (Helper.CStr(dv["approve_head_status"]) == "P")
                {
                    lblApprove_status.Text = "รออนุมัติ";
                }
                else if (Helper.CStr(dv["approve_head_status"]) == "A")
                {
                    lblApprove_status.Text = "อนุมัติ";
                }
                else if (Helper.CStr(dv["approve_head_status"]) == "N")
                {
                    lblApprove_status.Text = "ไม่อนุมัติ";
                }
                else if (Helper.CStr(dv["approve_head_status"]) == "X")
                {
                    lblApprove_status.Text = "อนุมัติบางส่วน";
                }
                else if (Helper.CStr(dv["approve_head_status"]) == "W")
                {
                    lblApprove_status.Text = "รายการยังไม่สมบูรณ์";
                }
                else if (Helper.CStr(dv["approve_head_status"]) == "C")
                {
                    lblApprove_status.Text = "ยกเลิกรายการ";
                }

                Image imgLoan = (Image)e.Row.FindControl("imgLoan");               
                imgLoan.Visible = Helper.CInt(dv["loan_count"]) > 0;

                var dt = objEfOpen.SP_OPEN_DETAIL_APPROVE_SEL(" and open_head_id=" + Helper.CInt(dv["open_head_id"]) + " order by approve_level");
                Repeater1.DataSource = dt;
                Repeater1.DataBind();

                Repeater RepeaterLoan = (Repeater)e.Row.FindControl("RepeaterLoan");
                dt = objEfOpen.SP_OPEN_LOAN_SEL(" and open_head_id=" + Helper.CInt(dv["open_head_id"]));
                RepeaterLoan.DataSource = dt;
                RepeaterLoan.DataBind();

                objEfOpen.Dispose();

                #region set Image Edit & Delete

                ImageButton imgView = (ImageButton)e.Row.FindControl("imgView");
                imgView.Attributes.Add("onclick", "OpenPopUp('950px','550px','95%','แสดงรายละเอียด" + base.PageDes + "','open_control.aspx?budget_type=" + dv["budget_type"].ToString() + "&mode=view&open_head_id="
                                                            + hhdopen_head_id.Value + "&page=" + GridView1.PageIndex.ToString() + "&canEdit=Y','1');return false;");


                ImageButton imgCopy = (ImageButton)e.Row.FindControl("imgCopy");
                imgCopy.Attributes.Add("onclick", "OpenPopUp('950px','550px','95%','คัดลอก" + base.PageDes + "','open_control.aspx?budget_type=" + dv["budget_type"].ToString() + "&mode=copy&open_head_id="
                                                            + hhdopen_head_id.Value + "&page=" + GridView1.PageIndex.ToString() + "&canEdit=Y','1');return false;");
                
                
                imgView.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgView"].Rows[0]["img"].ToString();
                imgView.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgView"].Rows[0]["title"].ToString());
                imgView.Visible = base.IsUserView;

                ImageButton imgEdit = (ImageButton)e.Row.FindControl("imgEdit");
                imgEdit.Attributes.Add("onclick", "OpenPopUp('950px','550px','95%','แก้ไข" + base.PageDes + "','open_control.aspx?budget_type=" + dv["budget_type"].ToString() + "&mode=edit&open_head_id="
                                                            + hhdopen_head_id.Value + "&page=" + GridView1.PageIndex.ToString() + "&canEdit=Y','1');return false;");
                imgEdit.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgEdit"].Rows[0]["img"].ToString();
                imgEdit.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgEdit"].Rows[0]["title"].ToString());
                imgEdit.Visible = base.IsUserEdit;
                if (base.UserGroupCode != "Admin")
                {
                    imgEdit.Visible = imgEdit.Visible && Helper.CStr(dv["approve_head_status"]) == "W";
                }
             

                ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                imgDelete.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["img"].ToString();
                imgDelete.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["title"].ToString());
                imgDelete.Attributes.Add("onclick", "return confirm(\"คุณต้องการยกเลิกรายการขออนุมัติเบิกจ่าย  : " + lblopen_doc.Text + " หรือไม่?\");");
                imgDelete.Visible = base.IsUserDelete && Helper.CStr(dv["approve_head_status"]) != "C";
                if (base.UserGroupCode != "Admin")
                {
                    imgDelete.Visible = imgDelete.Visible && Helper.CStr(dv["approve_head_status"]) == "W";
                }

                ImageButton imgPrint = (ImageButton)e.Row.FindControl("imgPrint");
                imgPrint.ImageUrl = "../../images/controls/print.png";
                imgPrint.Attributes.Add("title", "พิมพ์");
                string strScript = "window.open(\"../../App_Control/reportsparameter/open_report_show.aspx?report_code=Rep_open01&open_head_id=" + hhdopen_head_id.Value + "\", \"_blank\");return false;\n";
                imgPrint.Attributes.Add("onclick", strScript);
                //imgPrint.Visible = Helper.CStr(dv["approve_head_status"]) != "W" && Helper.CStr(dv["approve_head_status"]) != "C";
                imgPrint.Visible = true;
                if (base.UserGroupCode != "Admin")
                {
                    imgPrint.Visible = imgPrint.Visible && Helper.CStr(dv["person_code"]) == base.PersonCode;
                }

                if (!imgDelete.Visible && Helper.CStr(dv["approve_head_status"]) == "C")
                {
                    ImageButton imgRestore = (ImageButton)e.Row.FindControl("imgRestore");
                    imgRestore.ImageUrl = "../../images/back_2.png";
                    imgRestore.Attributes.Add("title", "คืนรายการ");
                    imgRestore.Attributes.Add("onclick", "return confirm(\"คุณต้องการคืนรายการขออนุมัติเบิกจ่าย  : " + lblopen_doc.Text + " หรือไม่? เมื่อทำการคืนรายการท่านจะสามารถแก้ไขข้อมูลใดๆ ได้\");");
                    imgRestore.Visible = base.IsUserDelete;
                }

                if (Helper.CStr(dv["approve_head_status"]) == "W")
                {
                    ImageButton imgPass = (ImageButton)e.Row.FindControl("imgPass");
                    imgPass.ImageUrl = "../../images/move.png";
                    imgPass.Attributes.Add("title", "ผ่านรายการ");
                    imgPass.Attributes.Add("onclick", "return confirm(\"คุณต้องการผ่านรายการขออนุมัติเบิกจ่าย  : " + lblopen_doc.Text + " หรือไม่? เมื่อทำการผ่านรายการท่านจะไม่สามารถแก้ไขข้อมูลใดๆ ได้\");");
                    imgPass.Visible =  Helper.CStr(dv["approve_head_status"]) == "W" && Helper.CStr(dv["open_title"]).Length > 0;
                }

                if (Helper.CStr(dv["approve_head_status"]) == "A")
                {
                    imgEdit.Visible = false;
                }

                #endregion

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
            else if (e.Row.RowType.Equals(DataControlRowType.Pager))
            {
                TableCell tbc = e.Row.Cells[0];
                Label lblPrev = null;
                Label lblNext = null;
                ImageButton lbtnPrev = null;
                ImageButton lbtnNext = null;

                #region find and store Previous and Next Page
                TableRow tbr = (TableRow)tbc.Controls[0].Controls[0];
                foreach (System.Web.UI.Control c in tbr.Controls)
                {
                    if (c.GetType().ToString().Equals("System.Web.UI.WebControls.Label"))
                    {
                        Label lbl = (Label)c;
                        if (lbl.Text.IndexOf("P") != -1)
                        {
                            lblPrev = lbl;
                            lblPrev.Text = string.Empty;
                        }
                        if (lbl.Text.IndexOf("N") != -1)
                        {
                            lblNext = lbl;
                            lblNext.Text = string.Empty;
                        }
                    }
                    if (c.Controls[0].GetType().ToString().Equals("System.Web.UI.WebControls.DataControlImageButton"))
                    {
                        ImageButton lbtn = (ImageButton)c.Controls[0];
                        if (lbtn.AlternateText.IndexOf("P") != -1)
                        {
                            lbtnPrev = lbtn;
                            lbtnPrev.ImageUrl = "~/images/prev.gif";
                        }
                        if (lbtn.AlternateText.IndexOf("N") != -1)
                        {
                            lbtnNext = lbtn;
                            lbtnNext.ImageUrl = "~/images/next.gif";
                        }
                    }
                }
                #endregion

                #region render new pager
                tbc.Text = string.Empty;
                Literal lblPager = new Literal();
                lblPager.Text = "<TABLE border='0' width='100%' cellpadding='0' cellspacing='0'><TR><TD width='30%' valign='middle'>";
                tbc.Controls.Add(lblPager);

                Label lblTotalRecord = new Label();
                lblTotalRecord.Attributes.Add("class", "label_h");
                lblTotalRecord.Text = "พบข้อมูล " + txthTotalRecord.Value.ToString() + " รายการ.";
                tbc.Controls.Add(lblTotalRecord);

                lblPager = new Literal();
                lblPager.Text = "</TD><TD width='30%' align='center' valign='middle'>";
                tbc.Controls.Add(lblPager);

                DropDownList cboPerPage = new DropDownList();
                cboPerPage.ID = "cboPerPage";

                DataTable entries;
                if ((DataSet)Application["xmlconfig"] == null)
                    return;
                else
                    entries = ((DataSet)Application["xmlconfig"]).Tables["RecordPerPage"];

                for (int i = 0; i < entries.Rows.Count; i++)
                {
                    cboPerPage.Items.Add(new ListItem(entries.Rows[i][0].ToString(), entries.Rows[i][1].ToString()));
                }

                if (cboPerPage.Items.FindByValue(strRecordPerPage) != null)
                {
                    cboPerPage.Items.FindByValue(strRecordPerPage).Selected = true;
                }

                cboPerPage.AutoPostBack = true;
                cboPerPage.SelectedIndexChanged += new System.EventHandler(cboPerPage_SelectedIndexChanged);
                tbc.Controls.Add(cboPerPage);

                lblPager = new Literal();
                lblPager.Text = "&nbsp;&nbsp;&nbsp;<span class=\"label_h\">รายการ/หน้า</span></TD><TD width='40%' align='right' valign='middle'>";
                tbc.Controls.Add(lblPager);

                if (lblPrev != null)
                {
                    tbc.Controls.Add(lblPrev);
                }
                else if (lbtnPrev != null)
                {
                    tbc.Controls.Add(lbtnPrev);
                }

                lblPager = new Literal();
                lblPager.Text = "&nbsp;&nbsp;&nbsp;<span class=\"label_h\">หน้าที่: </span>";
                tbc.Controls.Add(lblPager);

                TextBox txtPage = new TextBox();
                txtPage.AutoPostBack = false;
                txtPage.ID = "txtPage";
                txtPage.Width = System.Web.UI.WebControls.Unit.Parse("30px");
                txtPage.Attributes.Add("class", "text1");
                txtPage.Style.Add("text-align", "right");
                int nCurrentPage = (GridView1.PageIndex + 1);
                txtPage.Text = nCurrentPage.ToString();//strPageNo;
                txtPage.Attributes.Add("onkeypress", "javascript: return checkKeyCode(event);");
                txtPage.Attributes.Add("onkeyup", "javasctipt: checkInt(this, " + GridView1.PageCount.ToString() + ");");
                tbc.Controls.Add(txtPage);

                lblPager = new Literal();
                lblPager.Text = "<span class=\"label_h\"> จากทั้งหมด " + GridView1.PageCount.ToString() + "&nbsp;&nbsp;</span>";
                tbc.Controls.Add(lblPager);

                lblPager = new Literal();
                lblPager.Text = "&nbsp;&nbsp;";
                tbc.Controls.Add(lblPager);

                ImageButton imgGo = new ImageButton();
                imgGo.ID = "imgGo";
                imgGo.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgGo"].Rows[0]["img"].ToString();
                imgGo.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgGo"].Rows[0]["title"].ToString());
                imgGo.Attributes.Add("onclick", "javascript: return checkPage(" + GridView1.PageCount.ToString() + ",'กรุณาระบุข้อมูลให้ถูกต้อง.|||ctl00$ASPxRoundPanel1$ContentPlaceHolder2$GridView1$ctl01$txtPage');");
                imgGo.Click += new System.Web.UI.ImageClickEventHandler(this.imgGo_Click);
                tbc.Controls.Add(imgGo);

                lblPager = new Literal();
                lblPager.Text = "&nbsp;&nbsp;&nbsp;";
                tbc.Controls.Add(lblPager);

                if (lblNext != null)
                {
                    tbc.Controls.Add(lblNext);
                }
                else if (lbtnNext != null)
                {
                    tbc.Controls.Add(lbtnNext);
                }

                lblPager = new Literal();
                lblPager.Text = "</TD></TR></TABLE>";
                tbc.Controls.Add(lblPager);

                #endregion
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindGridView(e.NewPageIndex);
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
                GridViewRow item = (GridViewRow)GridView1.Controls[0].Controls[0];
                TextBox txtPage = (TextBox)item.FindControl("txtPage");
                BindGridView(int.Parse(txtPage.Text) - 1);
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string strMessage = string.Empty;
            string strCheck = string.Empty;
            string strScript = string.Empty;
            string strUpdatedBy = Session["username"].ToString();
            HiddenField hhdopen_head_id = (HiddenField)GridView1.Rows[e.RowIndex].FindControl("hhdopen_head_id");
            cefOpen objefOpen = new cefOpen();
            try
            {
                //if (objefOpen.SP_OPEN_DETAIL_DEL(Helper.CInt(hhdopen_head_id.Value)) &&
                //    objefOpen.SP_OPEN_DETAIL_APPROVE_ALL_DEL(Helper.CInt(hhdopen_head_id.Value)) &&
                if (objefOpen.SP_OPEN_HEAD_DEL(Helper.CInt(hhdopen_head_id.Value)))
                {
                    BindGridView(0);
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                objefOpen.Dispose();
            }
        }

        protected void cboDirector_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboUnit();
            BindGridView(1);
        }

        protected void cboBudget_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridView(1);
        }

        protected void cboUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridView(1);
            //  InitcboUnit();
        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboDirector();
        }

        protected void cboBudget_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BudgetType = cboBudget_type.SelectedValue;
            InitcboDirector();
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            DataRowView dv = (DataRowView)e.Item.DataItem;
            Label lblApprove_status = (Label)e.Item.FindControl("lblApprove_status");
            ImageButton btnViewComment = (ImageButton)e.Item.FindControl("btnViewComment");

            btnViewComment.Attributes.Add("onclick", "OpenPopUp('800px','300px','90%','ข้อคิดเห็นเพิ่มเติมของผู้อนุมัติรายการขออนุมัติเบิก','open_approve_control.aspx?mode=view&type=open&open_head_id="
                                            + dv["open_head_id"].ToString() + "&open_detail_approve_id=" + dv["open_detail_approve_id"].ToString() + "&page=" + GridView1.PageIndex.ToString() + "','1');return false;");

            btnViewComment.Visible = false;
            if (Helper.CStr(dv["approve_status"]) == "P")
            {
                lblApprove_status.Text = "รออนุมัติ";
                lblApprove_status.BackColor = System.Drawing.Color.DarkBlue;
                lblApprove_status.ForeColor = System.Drawing.Color.White;
            }
            else if (Helper.CStr(dv["approve_status"]) == "A")
            {
                lblApprove_status.Text = "อนุมัติ";
                lblApprove_status.BackColor = System.Drawing.Color.Green;
                lblApprove_status.ForeColor = System.Drawing.Color.White;
                btnViewComment.Visible = true;
            }
            else if (Helper.CStr(dv["approve_status"]) == "N")
            {
                lblApprove_status.Text = "ไม่อนุมัติ";
                lblApprove_status.BackColor = System.Drawing.Color.Red;
                lblApprove_status.ForeColor = System.Drawing.Color.White;
                btnViewComment.Visible = true;
            }



        }

        protected void RepeaterLoan_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {


            //ImageButton btnViewComment = (ImageButton)e.Item.FindControl("btnViewComment");

            //btnViewComment.Attributes.Add("onclick", "OpenPopUp('800px','300px','90%','ข้อคิดเห็นเพิ่มเติมของผู้อนุมัติรายการขออนุมัติเบิก','open_approve_control.aspx?mode=view&type=open&open_head_id="
            //                                + dv["open_head_id"].ToString() + "&open_detail_approve_id=" + dv["open_detail_approve_id"].ToString() + "&page=" + GridView1.PageIndex.ToString() + "','1');return false;");

            //btnViewComment.Visible = false;
            //if (Helper.CStr(dv["approve_status"]) == "P")
            //{
            //    lblApprove_status.Text = "รออนุมัติ";
            //    lblApprove_status.BackColor = System.Drawing.Color.DarkBlue;
            //    lblApprove_status.ForeColor = System.Drawing.Color.White;
            //}
            //else if (Helper.CStr(dv["approve_status"]) == "A")
            //{
            //    lblApprove_status.Text = "อนุมัติ";
            //    lblApprove_status.BackColor = System.Drawing.Color.Green;
            //    lblApprove_status.ForeColor = System.Drawing.Color.White;
            //    btnViewComment.Visible = true;
            //}
            //else if (Helper.CStr(dv["approve_status"]) == "N")
            //{
            //    lblApprove_status.Text = "ไม่อนุมัติ";
            //    lblApprove_status.BackColor = System.Drawing.Color.Red;
            //    lblApprove_status.ForeColor = System.Drawing.Color.White;
            //    btnViewComment.Visible = true;
            //}



        }


        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow gvRow = null;
            HiddenField hhdopen_head_id = null;
            switch (e.CommandName)
            {
                case "Pass":
                case "Restore":
                    gvRow = GridView1.Rows[Helper.CInt(e.CommandArgument) - 1];
                    hhdopen_head_id = (HiddenField)gvRow.FindControl("hhdopen_head_id");
                    break;
                default:
                    break;
            }
            cefOpen objefOpen = new cefOpen();
            switch (e.CommandName)
            {
                case "Pass":
                    try
                    {
                        if (objefOpen.SP_OPEN_HEAD_PASS(Helper.CInt(hhdopen_head_id.Value)))
                        {
                            BindGridView(0);
                        }
                    }
                    catch (Exception ex)
                    {
                        lblError.Text = ex.Message.ToString();
                    }
                    finally
                    {
                        objefOpen.Dispose();
                    }
                    break;
                case "Restore":
                    try
                    {
                        if (objefOpen.SP_OPEN_HEAD_RESTORE(Helper.CInt(hhdopen_head_id.Value)))
                        {
                            BindGridView(0);
                        }
                    }
                    catch (Exception ex)
                    {
                        lblError.Text = ex.Message.ToString();
                    }
                    finally
                    {
                        objefOpen.Dispose();
                    }
                    break;
                default:
                    break;
            }
        }

    }
}
