using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Threading;
using System.Text;
using myDLL;

namespace myWeb.App_Control.budget_plan
{
    public partial class budget_plan_list : PageBase
    {

        #region private data
        private string strRecordPerPage;
        private string strPageNo = "1";
        private bool[] blnAccessRight = new bool[5] { false, false, false, false, false };
     
        private string BudgetType
        {
            get
            {
                if (ViewState["BudgetType"] == null)
                {
                    ViewState["BudgetType"] = Helper.CStr(Request.QueryString["budget_type"]);
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


                imgPrint.Attributes.Add("onMouseOver", "src='../../images/button/print2.png'");
                imgPrint.Attributes.Add("onMouseOut", "src='../../images/button/print.png'");

                #region Set Image

                //imgList_unit.Attributes.Add("onclick", "OpenPopUp('800px','400px','93%','ค้นหาข้อมูลหน่วยงาน' ,'../lov/unit_lov.aspx?unit_year='+document.forms[0]." + strPrefixCtr +
                //                                                         "cboYear.options[document.forms[0]." + strPrefixCtr + "cboYear.selectedIndex].value+" +
                //                                                         "'&unit_code='+document.forms[0]." + strPrefixCtr + "txtunit_code.value+" +
                //                                                         "'&unit_name='+document.forms[0]." + strPrefixCtr + "txtunit_name.value+" +
                //                                                         "'&ctrl1=" + txtunit_code.ClientID + "&ctrl2=" + txtunit_name.ClientID + "', '1');return false;");
                //imgClear_unit.Attributes.Add("onclick", "document.forms[0]." + strPrefixCtr + "txtunit_code.value='';document.forms[0]." + strPrefixCtr + "txtunit_name.value=''; return false;");
                ////imgList_activity.Attributes.Add("onclick", "OpenPopUp('900px','350px','93%'
                //imgList_activity.Attributes.Add("onclick", "OpenPopUp('900px','350px','93%','ค้นหาข้อมูลกิจกรรม' ,'../lov/activity_lov.aspx?year='+document.forms[0]." + strPrefixCtr +
                //                                                        "cboYear.options[document.forms[0]." + strPrefixCtr + "cboYear.selectedIndex].value+" +
                //                                                        "'&activity_code='+document.forms[0]." + strPrefixCtr + "txtactivity_code.value+" +
                //                                                        "'&activity_name='+document.forms[0]." + strPrefixCtr + "txtactivity_name.value+" +
                //                                                        "'&ctrl1=" + txtactivity_code.ClientID + "&ctrl2=" + txtactivity_name.ClientID + "', '1');return false;");
                //imgClear_activity.Attributes.Add("onclick", "document.forms[0]." + strPrefixCtr + "txtactivity_code.value='';document.forms[0]." + strPrefixCtr + "txtactivity_name.value=''; return false;");

                //imgList_plan.Attributes.Add("onclick", "OpenPopUp('800px','400px','93%','ค้นหาข้อมูลแผนงาน' ,'../lov/plan_lov.aspx?year='+document.forms[0]." + strPrefixCtr +
                //                                                        "cboYear.options[document.forms[0]." + strPrefixCtr + "cboYear.selectedIndex].value+" +
                //                                                        "'&plan_code='+document.forms[0]." + strPrefixCtr + "txtplan_code.value+" +
                //                                                        "'&plan_name='+document.forms[0]." + strPrefixCtr + "txtplan_name.value+" +
                //                                                        "'&ctrl1=" + txtplan_code.ClientID + "&ctrl2=" + txtplan_name.ClientID + "', '1');return false;");
                //imgClear_plan.Attributes.Add("onclick", "document.forms[0]." + strPrefixCtr + "txtplan_code.value='';document.forms[0]." + strPrefixCtr + "txtplan_name.value=''; return false;");

                //imgList_work.Attributes.Add("onclick", "OpenPopUp('800px','400px','93%','ค้นหาข้อมูลงาน' ,'../lov/work_lov.aspx?year='+document.forms[0]." + strPrefixCtr +
                //                                                    "cboYear.options[document.forms[0]." + strPrefixCtr + "cboYear.selectedIndex].value+" +
                //                                                    "'&work_code='+document.forms[0]." + strPrefixCtr + "txtwork_code.value+" +
                //                                                    "'&work_name='+document.forms[0]." + strPrefixCtr + "txtwork_name.value+" +
                //                                                    "'&ctrl1=" + txtwork_code.ClientID + "&ctrl2=" + txtwork_name.ClientID + "', '1');return false;");
                //imgClear_work.Attributes.Add("onclick", "document.forms[0]." + strPrefixCtr + "txtwork_code.value='';document.forms[0]." + strPrefixCtr + "txtwork_name.value=''; return false;");

                #endregion

                imgNew.Attributes.Add("onclick", "OpenPopUp('950px','350px','90%','เพิ่ม" + base.PageDes + "','budget_plan_control.aspx?budget_type=" + this.BudgetType + "&mode=add&page=0','1');return false;");
                ViewState["sort"] = "budget_plan_code";
                ViewState["direction"] = "ASC";
                RadioAll.Checked = true;
                InitcboYear();
                InitcboDirector();
                InitcboBudget();
                InitcboPlan();
                BindGridView(0);

                //if (this.BudgetType == "R")
                //{
                //    foreach (Control c in Page.Controls)
                //    {
                //        base.SetLabel(c, "แผนงาน", "งานย่อย");
                //        base.SetLabel(c, "กิจกรรม", "งานรอง");
                //        base.SetLabel(c, "แผนงบประมาณ", "แผนงาน");
                //        base.SetLabel(c, "ผลผลิต", "งานหลัก");                       
                //    }
                //}

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
                    BindGridView(int.Parse(txthpage.Value));
                    txthpage.Value = string.Empty;
                }
            }
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
                cboDirector.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
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
            strCriteria = strCriteria + " and unit.director_code = '" + strDirector_code + "' ";
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
                cboUnit.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
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

        private void InitcboBudget()
        {
            cBudget oBudget = new cBudget();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strYear = cboYear.SelectedValue; 
            string strbudget_code = cboBudget.SelectedValue;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and budget_year = '" + strYear + "'  and  c_active='Y' ";
            strCriteria = strCriteria + "  And budget_type ='" + this.BudgetType + "' ";
            if (oBudget.SP_SEL_BUDGET(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboBudget.Items.Clear();
                cboBudget.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                int i;
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboBudget.Items.Add(new ListItem(dt.Rows[i]["budget_name"].ToString(), dt.Rows[i]["budget_code"].ToString()));
                }
                if (cboBudget.Items.FindByValue(strbudget_code) != null)
                {
                    cboBudget.SelectedIndex = -1;
                    cboBudget.Items.FindByValue(strbudget_code).Selected = true;
                }
            }
            InitcboProduce();
        }

        private void InitcboProduce()
        {
            cProduce oProduce = new cProduce();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strbudget_code = string.Empty,
                        strproduce_code = string.Empty,
                        strproduce_name = string.Empty;
            string strYear = cboYear.SelectedValue; 
            strbudget_code = cboBudget.SelectedValue;
            strproduce_code = cboProduce.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and  produce.c_active='Y' ";
            strCriteria = strCriteria + "  And produce.budget_type ='" + this.BudgetType + "' ";

            //if (!strbudget_code.Equals(""))
            //{
                strCriteria = strCriteria + " and produce.budget_code= '" + strbudget_code + "' ";
            //}

            if (oProduce.SP_SEL_PRODUCE(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboProduce.Items.Clear();
                cboProduce.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboProduce.Items.Add(new ListItem(dt.Rows[i]["produce_name"].ToString(), dt.Rows[i]["produce_code"].ToString()));
                }
                if (cboProduce.Items.FindByValue(strproduce_code) != null)
                {
                    cboProduce.SelectedIndex = -1;
                    cboProduce.Items.FindByValue(strproduce_code).Selected = true;
                }
            }
            InitcboActivity();
        }

        private void InitcboActivity()
        {
            cActivity oActivity = new cActivity();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        stractivity_code = string.Empty,
                        strbudget_code = string.Empty,
                        strproduce_code = string.Empty;
            stractivity_code = cboActivity.SelectedValue;
            strbudget_code = cboBudget.SelectedValue;
            strproduce_code = cboProduce.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = "  and  activity.c_active='Y' ";
            strCriteria = strCriteria + "  And activity.budget_type ='" + this.BudgetType + "' ";

            //if (!strbudget_code.Equals(""))
            //{
                strCriteria = strCriteria + " and  produce.budget_code= '" + strbudget_code + "' ";
            //}

            //if (!strproduce_code.Equals(""))
            //{
                strCriteria = strCriteria + " and activity.produce_code= '" + strproduce_code + "' ";
            //}


            if (oActivity.SP_SEL_ACTIVITY(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboActivity.Items.Clear();
                cboActivity.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboActivity.Items.Add(new ListItem(dt.Rows[i]["activity_name"].ToString(), dt.Rows[i]["activity_code"].ToString()));
                }
                if (cboActivity.Items.FindByValue(stractivity_code) != null)
                {
                    cboActivity.SelectedIndex = -1;
                    cboActivity.Items.FindByValue(stractivity_code).Selected = true;
                }
            }
        }

        private void InitcboPlan()
        {
            cPlan oPlan = new cPlan();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strplan_code = string.Empty;
            string strYear =  cboYear.SelectedValue; 
            strplan_code = cboPlan_code.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and plan_year = '" + strYear + "'  and  c_active='Y' ";
            strCriteria = strCriteria + "  And budget_type ='" + this.BudgetType + "' ";
            if (oPlan.SP_SEL_PLAN(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboPlan_code.Items.Clear();
                cboPlan_code.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboPlan_code.Items.Add(new ListItem(dt.Rows[i]["plan_name"].ToString(), dt.Rows[i]["plan_code"].ToString()));
                }
                if (cboPlan_code.Items.FindByValue(strplan_code) != null)
                {
                    cboPlan_code.SelectedIndex = -1;
                    cboPlan_code.Items.FindByValue(strplan_code).Selected = true;
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
            InitcboYear();
            cBudget_plan oBudget_plan = new cBudget_plan();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string strbudget_plan_year = string.Empty;
            string strbudget_plan_code = string.Empty;
            string strbudget_code = string.Empty;
            string strproduce_code = string.Empty;
            string strunit_code = string.Empty;
            string strdirector_code = string.Empty;
            string stractivity_code = string.Empty;
            string strplan_code = string.Empty;
            string strwork_code = string.Empty;
            string strwork_name = string.Empty;
            string stractive = string.Empty;
            string strScript = string.Empty;
            #region Criteria
            strbudget_plan_year = cboYear.SelectedValue;
            strbudget_plan_code = txtbudget_plan_code.Text.Replace("'", "''").Trim();
            strbudget_code = cboBudget.SelectedValue;
            strproduce_code = cboProduce.SelectedValue;
            strdirector_code = cboDirector.SelectedValue;
            strunit_code = cboUnit.SelectedValue;
            stractivity_code = cboActivity.SelectedValue;
            strplan_code = cboPlan_code.SelectedValue;
     
            if (!strbudget_plan_year.Equals(""))
            {
                strCriteria = strCriteria + "  And  (budget_plan_year = '" + strbudget_plan_year + "') ";
            }
            if (!strbudget_plan_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (budget_plan_code ='" + strbudget_plan_code + "') ";
            }

            if (!strproduce_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (produce_code ='" + strproduce_code + "') ";
            }

            if (!strbudget_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (budget_code ='" + strbudget_code + "') ";
            }

            if (!strdirector_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (director_code ='" + strdirector_code + "') ";
            }

            if (!strunit_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (unit_code ='" + strunit_code + "') ";
            }
            if (!stractivity_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (activity_code = '" + stractivity_code + "') ";
            }
            if (!strplan_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (plan_code = '" + strplan_code + "') ";
            }
            if (!strwork_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (work_code like '%" + strwork_code + "%') ";
            }
            if (!strwork_name.Equals(""))
            {
                strCriteria = strCriteria + "  And  (work_name like '%" + strwork_name + "%') ";
            }

            if (DirectorLock == "Y")
            {
                strCriteria += " and substring(director_code,4,2) = substring('" + DirectorCode + "',4,2) ";
            }

            #endregion

            if (RadioActive.Checked)
            {
                strCriteria = strCriteria + "  And  (c_active ='Y') ";
            }
            else if (RadioCancel.Checked)
            {
                strCriteria = strCriteria + "  And  (c_active ='N') ";
            }

            strCriteria = strCriteria + " and budget_type ='" + this.BudgetType + "' ";           

            try
            {
                if (!oBudget_plan.SP_BUDGET_PLAN_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    try
                    {
                        GridView1.PageIndex = nPageNo;
                        txthTotalRecord.Value = ds.Tables[0].Rows.Count.ToString();
                        ds.Tables[0].DefaultView.Sort = ViewState["sort"] + " " + ViewState["direction"];
                        GridView1.DataSource = ds.Tables[0];
                        GridView1.DataBind();
                    }
                    catch
                    {
                        GridView1.PageIndex = 0;
                        txthTotalRecord.Value = ds.Tables[0].Rows.Count.ToString();
                        ds.Tables[0].DefaultView.Sort = ViewState["sort"] + " " + ViewState["direction"];
                        GridView1.DataSource = ds.Tables[0];
                        GridView1.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oBudget_plan.Dispose();
                ds.Dispose();
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
                int nNo = (GridView1.PageSize * GridView1.PageIndex) + e.Row.RowIndex + 1;
                lblNo.Text = nNo.ToString();
                Label lblbudget_plan_code = (Label)e.Row.FindControl("lblbudget_plan_code");
                Label lblbudget_plan_name = (Label)e.Row.FindControl("lblbudget_plan_name");
                Label lblc_active = (Label)e.Row.FindControl("lblc_active");
                string strStatus = lblc_active.Text;

                #region set ImageStatus
                ImageButton imgStatus = (ImageButton)e.Row.FindControl("imgStatus");
                if (strStatus.Equals("Y"))
                {
                    imgStatus.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgStatus"].Rows[0]["img"].ToString();
                    imgStatus.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgStatus"].Rows[0]["title"].ToString());
                    imgStatus.Attributes.Add("onclick", "return false;");
                }
                else
                {
                    imgStatus.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgStatus"].Rows[0]["imgdisable"].ToString();
                    imgStatus.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgStatus"].Rows[0]["titledisable"].ToString());
                    imgStatus.Attributes.Add("onclick", "return false;");
                }
                #endregion

                #region set ImageView
                ImageButton imgView = (ImageButton)e.Row.FindControl("imgView");
                imgView.Attributes.Add("onclick", "OpenPopUp('950px','350px','94%','แสดงข้อมูลผังงบประมาณ','budget_plan_view.aspx?mode=view&budget_plan_code=" + 
                                                                lblbudget_plan_code.Text + "','1');return false;");
                imgView.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgView"].Rows[0]["img"].ToString();
                imgView.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgView"].Rows[0]["title"].ToString());
                #endregion

                #region set Image Edit & Delete
                ImageButton imgEdit = (ImageButton)e.Row.FindControl("imgEdit");
                Label lblCanEdit = (Label)e.Row.FindControl("lblCanEdit");
                imgEdit.Attributes.Add("onclick", "OpenPopUp('900px','350px','90%','แก้ไข" + base.PageDes + "','budget_plan_control.aspx?budget_type=" + this.BudgetType + "&mode=edit&budget_plan_code="
                                                            + lblbudget_plan_code.Text + "&page=" + GridView1.PageIndex.ToString() + "&canEdit=Y','1');return false;");
                imgEdit.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgEdit"].Rows[0]["img"].ToString();
                imgEdit.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgEdit"].Rows[0]["title"].ToString());

                ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                imgDelete.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["img"].ToString();
                imgDelete.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["title"].ToString());
                imgDelete.Attributes.Add("onclick", "return confirm(\"คุณต้องการลบผังงบประมาณ  : " + lblbudget_plan_code.Text + " ?\");");
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
            Label lblbudget_plan_code = (Label)GridView1.Rows[e.RowIndex].FindControl("lblbudget_plan_code");
            cBudget_plan oBudget_plan = new cBudget_plan();
            try
            {
                if (!oBudget_plan.SP_BUDGET_PLAN_DEL(lblbudget_plan_code.Text, "N", strUpdatedBy, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oBudget_plan.Dispose();
            }
            BindGridView(0);
        }

        protected void imgPrint_Click(object sender, ImageClickEventArgs e)
        {
            string strScript = string.Empty;
            string strMessage = string.Empty;
            string strCriteria = string.Empty;

            string strbudget_plan_year = string.Empty;
            string strbudget_plan_code = string.Empty;
            string strbudget_code = string.Empty;
            string strproduce_code = string.Empty;
            string strunit_code = string.Empty;
            string strdirector_code = string.Empty;
            string stractivity_code = string.Empty;
            string strplan_code = string.Empty;
            string strwork_code = string.Empty;
            string strwork_name = string.Empty;
            string stractive = string.Empty;
            #region Criteria
            strbudget_plan_year = cboYear.SelectedValue;
            strbudget_plan_code = txtbudget_plan_code.Text.Replace("'", "''").Trim();
            strbudget_code = cboBudget.SelectedValue;
            strproduce_code = cboProduce.SelectedValue;
            strdirector_code = cboDirector.SelectedValue;
            strunit_code = cboUnit.SelectedValue;
            stractivity_code = cboActivity.SelectedValue;
            strplan_code = cboPlan_code.SelectedValue;
            //   strwork_code = txtwork_code.Text.Replace("'", "''").Trim();
            //  strwork_name = txtwork_name.Text.Replace("'", "''").Trim();

            if (!strbudget_plan_year.Equals(""))
            {
                strCriteria = strCriteria + "  And  (budget_plan_year = '" + strbudget_plan_year + "') ";
            }
            if (!strbudget_plan_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (budget_plan_code ='" + strbudget_plan_code + "') ";
            }

            if (!strproduce_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (produce_code ='" + strproduce_code + "') ";
            }

            if (!strbudget_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (budget_code ='" + strbudget_code + "') ";
            }

            if (!strdirector_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (director_code ='" + strdirector_code + "') ";
            }

            if (!strunit_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (unit_code ='" + strunit_code + "') ";
            }
            if (!stractivity_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (activity_code = '" + stractivity_code + "') ";
            }
            if (!strplan_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (plan_code = '" + strplan_code + "') ";
            }
            if (!strwork_code.Equals(""))
            {
                strCriteria = strCriteria + "  And  (work_code like '%" + strwork_code + "%') ";
            }
            if (!strwork_name.Equals(""))
            {
                strCriteria = strCriteria + "  And  (work_name like '%" + strwork_name + "%') ";
            }
            #endregion

            if (RadioActive.Checked)
            {
                strCriteria = strCriteria + "  And  (c_active ='Y') ";
            }
            else if (RadioCancel.Checked)
            {
                strCriteria = strCriteria + "  And  (c_active ='N') ";
            }
            strCriteria = strCriteria + " and budget_type ='" + this.BudgetType + "' ";           
            strScript = "windowOpenMaximize(\"../../App_Control/reportsparameter/basic_reports.aspx?sp_name=sp_BUDGET_PLAN_SEL&report_name=Rep_budget_plan.rpt&report_des=รายงานข้อมูลฝังงบประมาณ&criteria=" + strCriteria + "\", \"_blank\");\n";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript, true);
        }


        protected void cboDirector_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboUnit();
            BindGridView(1);
        }

        protected void cboBudget_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboProduce();
            BindGridView(1);
        }

        protected void cboProduce_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboActivity();
            BindGridView(1);

        }

        protected void cboActivity_SelectedIndexChanged(object sender, EventArgs e)
        {
              BindGridView(1);  
        }

        protected void cboPlan_code_SelectedIndexChanged(object sender, EventArgs e)
        {
           // InitcboPlan();
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

    }
}
