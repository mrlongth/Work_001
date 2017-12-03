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

namespace myWeb.App_Control.person
{
    public partial class person_control : PageBase
    {
        #region private data
        private string strConn = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"];
        private bool[] blnAccessRight = new bool[5] { false, false, false, false, false };
        private string strPrefixCtr_main = "ctl00$ContentPlaceHolder1$TabContainer1$";
        #endregion

        public static string getItemtype(object mData)
        {
            if (mData.Equals("D"))
            {
                return "Debit";
            }
            else
            {
                return "Credit";
            }
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(person_control));
            Session["menupopup_name"] = this.Page;
            lblError.Text = "";
            if (!IsPostBack)
            {
                lblAge.Text = string.Empty;

                imgSaveOnly.Attributes.Add("onMouseOver", "src='../../images/controls/save2.jpg'");
                imgSaveOnly.Attributes.Add("onMouseOut", "src='../../images/controls/save.jpg'");

                BtnR1.Style.Add("display", "none");
                BtnR2.Style.Add("display", "none");
                BtnR3.Style.Add("display", "none");
                BtnR4.Style.Add("display", "none");

                imgSaveOnly.Attributes.Add("onclick", "RunValidationsAndSetActiveTab();");
                txtperson_id.Attributes.Add("onblur", "checkInt(this,9999999999999)");
                txtperson_salaly.Attributes.Add("onblur", "chkDecimal(this,2,',')");
                txtmember_type_add.Attributes.Add("onblur", "chkDecimal(this,2,',')");
                ViewState["sort"] = "item_type";
                ViewState["direction"] = "DESC";
                ViewState["sort1"] = "member_code";
                ViewState["direction1"] = "ASC";
                ViewState["sort2"] = "change_date";
                ViewState["direction2"] = "ASC";

                ViewState["sort3"] = "loan_name";
                ViewState["direction3"] = "ASC";

                TabContainer1.ActiveTabIndex = 0;

                #region set QueryString
                if (Request.QueryString["person_code"] != null)
                {
                    ViewState["person_code"] = Request.QueryString["person_code"].ToString();
                }
                if (Request.QueryString["page"] != null)
                {
                    ViewState["page"] = Request.QueryString["page"].ToString();
                }
                if (Request.QueryString["mode"] != null)
                {
                    ViewState["mode"] = Request.QueryString["mode"].ToString();
                }
                else
                {
                    ViewState["mode"] = string.Empty;
                }
                if (Request.QueryString["PageStatus"] != null)
                {
                    ViewState["PageStatus"] = Request.QueryString["PageStatus"].ToString();
                }
                if (Request.QueryString["FromPage"] != null)
                {
                    ViewState["FromPage"] = Request.QueryString["FromPage"].ToString();
                }

                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    InitcboTitle();
                    InitcboPerson_group();
                    InitcboMember_type();
                    InitcboPerson_work_status();
                    Session["menupopup_name"] = "เพิ่มข้อมูลบุคคลากร";
                    ViewState["page"] = Request.QueryString["page"];
                    txtperson_code.ReadOnly = true;
                    txtperson_code.CssClass = "textboxdis";
                    txtperson_code.CssClass = "textboxdis";
                    TabContainer1.Tabs[0].Visible = true;
                    TabContainer1.Tabs[1].Visible = true;
                    TabContainer1.Tabs[2].Visible = true;
                    TabContainer1.Tabs[3].Visible = true;
                    TabContainer1.Tabs[4].Visible = false;
                    TabContainer1.Tabs[5].Visible = false;
                    TabContainer1.Tabs[6].Visible = false;
                    if (ViewState["FromPage"] != null && ViewState["FromPage"].ToString() == "person_center")
                    {
                        setDataCenter();
                    }
                }
                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    Session["menupopup_name"] = "แก้ไขข้อมูลบุคคลากร";
                    setData();
                    txtperson_code.ReadOnly = true;
                    txtperson_code.CssClass = "textboxdis";
                }

                #endregion

                #region add ajax method to control
                //string strGBK = string.Empty,
                //            strGSJ = string.Empty,
                //            strSOS = string.Empty;
                //strGBK = ((DataSet)Application["xmlconfig"]).Tables["MemberType"].Rows[0]["GBK"].ToString();
                //strGSJ = ((DataSet)Application["xmlconfig"]).Tables["MemberType"].Rows[0]["GSJ"].ToString();
                //strSOS = ((DataSet)Application["xmlconfig"]).Tables["MemberType"].Rows[0]["SOS"].ToString();

                //cboPerson_group.Attributes.Add("onchange", "changeMembertype(this,'" + strGBK + "','" + strGSJ + "','" + strSOS + "');");
                //cboPerson_group.AutoPostBack = false;
                #endregion

                #region Set Image

                imgperson_pic.Attributes.Add("onclick", "OpenPopUp('500px','200px','80%','อัพโหลดรูปบุคคลากร' ,'../person/person_upload.aspx?" +
                                                                    "ctrl1=" + txtperson_pic.ClientID + "&ctrl2=" + imgPerson.ClientID + "&show=2', '2');return false;");
                imgClear_person_pic.Attributes.Add("onclick", "document.getElementById('" + txtperson_pic.ClientID + "').value='';" +
                                                                                                             "document.getElementById('" + imgPerson.ClientID + "').src='../../person_pic/image_n_a.jpg';return false;");

                imgList_position.Attributes.Add("onclick", "OpenPopUp('800px','400px','93%','ค้นหาข้อมูลตำแหน่งปัจจุบัน' ,'../lov/position_lov.aspx?position_code='+document.forms[0]." +
                                                                strPrefixCtr_main + "TabPanel2$txtposition_code.value+" + "'&position_name='+document.forms[0]." + strPrefixCtr_main +
                                                                "TabPanel2$txtposition_name.value+" + "'&ctrl1=" + txtposition_code.ClientID + "&" +
                                                                "ctrl2=" + txtposition_name.ClientID + "&show=2', '2');return false;");
                imgClear_position.Attributes.Add("onclick", "document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtposition_code.value='';document.forms[0]." +
                                                        strPrefixCtr_main + "TabPanel2$txtposition_name.value=''; return false;");

                imgList_level.Attributes.Add("onclick", "OpenPopUp('800px','400px','93%','ค้นหาข้อมูลระดับตำแหน่ง' ,'../lov/level_position_lov.aspx?level_position_code='+document.forms[0]." +
                                                strPrefixCtr_main + "TabPanel2$txtperson_level.value+" + "'&position_name='+document.forms[0]." + strPrefixCtr_main +
                                                "TabPanel2$txtlevel_position_name.value+" + "'&ctrl1=" + txtperson_level.ClientID + "&" +
                                                "ctrl2=" + txtlevel_position_name.ClientID + "&show=2', '2');return false;");
                imgClear_level.Attributes.Add("onclick", "document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtperson_level.value='';document.forms[0]." +
                                                        strPrefixCtr_main + "TabPanel2$txtlevel_position_name.value=''; return false;");


                imgList_type.Attributes.Add("onclick", "OpenPopUp('800px','400px','93%','ค้นหาข้อมูลประเภทตำแหน่ง' ,'../lov/type_position_lov.aspx?type_position_code='+document.forms[0]." +
                                                              strPrefixCtr_main + "TabPanel2$txttype_position_code.value+" + "'&type_position_name='+document.forms[0]." + strPrefixCtr_main +
                                                              "TabPanel2$txttype_position_name.value+" + "'&ctrl1=" + txttype_position_code.ClientID + "&" +
                                                              "ctrl2=" + txttype_position_name.ClientID + "&show=2', '2');return false;");
                imgClear_type.Attributes.Add("onclick", "document.forms[0]." + strPrefixCtr_main + "TabPanel2$txttype_position_code.value='';document.forms[0]." +
                                                        strPrefixCtr_main + "TabPanel2$txttype_position_name.value=''; return false;");


                imgList_branch.Attributes.Add("onclick", "OpenPopUp('800px','400px','93%','ค้นหาข้อมูลสาขาธนาคาร' ,'../lov/branch_lov.aspx?branch_code='+document.forms[0]." +
                                                                strPrefixCtr_main + "TabPanel2$txtbranch_code.value+" + "'&branch_name='+document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtbranch_name.value+" +
                                                                "'&ctrl1=" + txtbranch_code.ClientID + "&ctrl2=" + txtbranch_name.ClientID + "&ctrl3=" + txtbank_name.ClientID + "&show=2', '2');return false;");
                imgClear_branch.Attributes.Add("onclick", "document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtbranch_code.value='';" +
                                                            "document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtbranch_name.value=''; " +
                                                            "document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtbank_name.value=''; " +
                                                            "return false;");



                imgList_branch_2.Attributes.Add("onclick", "OpenPopUp('800px','400px','93%','ค้นหาข้อมูลสาขาธนาคาร (เงินรายได้)' ,'../lov/branch_lov.aspx?branch_code='+document.forms[0]." +
                                                strPrefixCtr_main + "TabPanel2$txtbranch_code_2.value+" + "'&branch_name='+document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtbranch_name_2.value+" +
                                                "'&ctrl1=" + txtbranch_code_2.ClientID + "&ctrl2=" + txtbranch_name_2.ClientID + "&ctrl3=" + txtbank_name_2.ClientID + "&show=2', '2');return false;");

                imgClear_branch_2.Attributes.Add("onclick", "document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtbranch_code_2.value='';" +
                                                            "document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtbranch_name_2.value=''; " +
                                                            "document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtbank_name_2.value=''; " +
                                                            "return false;");



                imgList_person_manage.Attributes.Add("onclick", "OpenPopUp('800px','400px','93%','ค้นหาข้อมูลตำแหน่งทางการบริหาร' ,'../lov/person_manage_lov.aspx?" +
                                                            "person_manage_code='+document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtperson_manage_code.value+" +
                                                            "'&person_manage_name='+document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtperson_manage_name.value+" +
                                                            "'&ctrl1=" + txtperson_manage_code.ClientID + "&ctrl2=" + txtperson_manage_name.ClientID + "&show=2', '2');return false;");
                imgClear_person_manage.Attributes.Add("onclick", "document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtperson_manage_code.value='';" +
                                                                "document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtperson_manage_name.value=''; return false;");

                string strBusget_type = cboBudget_type.SelectedValue;
                imgList_budget_plan.Attributes.Add("onclick", "OpenPopUp('950px','500px','94%','ค้นหาข้อมูลผังงบประมาณประจำปี' ,'../lov/budget_plan_lov.aspx?budget_type=" + strBusget_type +
                                                                "&budget_plan_code='+document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtbudget_plan_code.value+'" +
                                                                "&budget_name='+document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtbudget_name.value+'" +
                                                                "&produce_name='+document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtproduce_name.value+'" +
                                                                "&activity_name='+document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtactivity_name.value+'" +
                                                                "&plan_name='+document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtplan_name.value+'" +
                                                                "&work_name='+document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtwork_name.value+'" +
                                                                "&fund_name='+document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtfund_name.value+'" +
                                                                "&director_name='+document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtdirector_name.value+'" +
                                                                "&unit_name='+document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtunit_name.value+'" +
                                                                "&budget_plan_year='+document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtbudget_plan_year.value+'" +
                                                                "&ctrl1=" + txtbudget_plan_code.ClientID +
                                                                "&ctrl2=" + txtbudget_name.ClientID +
                                                                "&ctrl3=" + txtproduce_name.ClientID +
                                                                "&ctrl4=" + txtactivity_name.ClientID +
                                                                "&ctrl5=" + txtplan_name.ClientID +
                                                                "&ctrl6=" + txtwork_name.ClientID +
                                                                "&ctrl7=" + txtfund_name.ClientID +
                                                                "&ctrl9=" + txtdirector_name.ClientID +
                                                                "&ctrl10=" + txtunit_name.ClientID +
                                                                "&ctrl11=" + txtbudget_plan_year.ClientID + "&show=2', '2');return false;");

                imgClear_budget_plan.Attributes.Add("onclick",
                                                                "document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtbudget_plan_code.value='';" +
                                                                "document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtbudget_name.value='';" +
                                                                "document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtproduce_name.value='';" +
                                                                "document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtactivity_name.value='';" +
                                                                "document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtplan_name.value='';" +
                                                                "document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtwork_name.value='';" +
                                                                "document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtfund_name.value='';" +
                                                                "document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtdirector_name.value='';" +
                                                                "document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtunit_name.value='';" +
                                                                "document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtbudget_plan_year.value=''; return false;");

                #endregion


                InitcboBudgetType();
            }
            else
            {
                StoreDataFromJS();
            }
            //if (FileUploaderAJAX1.IsPosting)
            //{
            //    this.managePost();
            //}
        }

        #region private function

        private void InitcboTitle()
        {
            cTitle oTitle = new cTitle();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strtitle_code = string.Empty;
            strtitle_code = cboTitle.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and c_active='Y' ";
            if (oTitle.SP_SEL_TITLE(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboTitle.Items.Clear();
                cboTitle.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboTitle.Items.Add(new ListItem(dt.Rows[i]["title_name"].ToString(), dt.Rows[i]["title_code"].ToString()));
                }
                if (cboTitle.Items.FindByValue(strtitle_code) != null)
                {
                    cboTitle.SelectedIndex = -1;
                    cboTitle.Items.FindByValue(strtitle_code).Selected = true;
                }
            }
        }

        private void InitcboPerson_group()
        {
            cPerson_group oPerson_group = new cPerson_group();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strperson_group_code = string.Empty;
            strperson_group_code = cboPerson_group.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and c_active='Y' ";
            if (oPerson_group.SP_PERSON_GROUP_SEL(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboPerson_group.Items.Clear();
                cboPerson_group.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboPerson_group.Items.Add(new ListItem(dt.Rows[i]["person_group_name"].ToString(), dt.Rows[i]["person_group_code"].ToString()));
                }
                if (cboPerson_group.Items.FindByValue(strperson_group_code) != null)
                {
                    cboPerson_group.SelectedIndex = -1;
                    cboPerson_group.Items.FindByValue(strperson_group_code).Selected = true;
                }
            }
        }

        private void InitcboPerson_work_status()
        {
            cPerson_work_status oPerson_work_status = new cPerson_work_status();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strperson_work_status = string.Empty;
            strperson_work_status = cboPerson_work_status.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and c_active='Y' ";
            if (oPerson_work_status.SP_PERSON_WORK_STATUS_SEL(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboPerson_work_status.Items.Clear();
                //cboPerson_work_status.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboPerson_work_status.Items.Add(new ListItem(dt.Rows[i]["person_work_status_name"].ToString(), dt.Rows[i]["person_work_status_code"].ToString()));
                }
                if (cboPerson_work_status.Items.FindByValue(strperson_work_status) != null)
                {
                    cboPerson_work_status.SelectedIndex = -1;
                    cboPerson_work_status.Items.FindByValue(strperson_work_status).Selected = true;
                }
            }
        }

        private void InitcboMember_type()
        {
            cMember_type oMember_type = new cMember_type();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strmember_type = string.Empty,
                        strperson_group_code = string.Empty,
                        strGBK = string.Empty,
                        strGSJ = string.Empty,
                        strSOS = string.Empty;
            int i;
            strGBK = ((DataSet)Application["xmlconfig"]).Tables["MemberType"].Rows[0]["GBK"].ToString();
            strGSJ = ((DataSet)Application["xmlconfig"]).Tables["MemberType"].Rows[0]["GSJ"].ToString();
            strSOS = ((DataSet)Application["xmlconfig"]).Tables["MemberType"].Rows[0]["SOS"].ToString();
            string strPVD = ((DataSet)Application["xmlconfig"]).Tables["MemberType"].Rows[0]["PVD"].ToString();
            string strPVD2 = ((DataSet)Application["xmlconfig"]).Tables["MemberType"].Rows[0]["PVD2"].ToString();
            string strPVD3 = "08";
          

            foreach (ListItem item in cboMember_type_checkboxes.Items)
            {
                if (item.Selected)
                {
                    strmember_type += item.Value + ",";
                }
            }
            if (strmember_type.Length > 0)
                strmember_type = strmember_type.Substring(0, strmember_type.Length - 1);

            strperson_group_code = cboPerson_group.SelectedValue;

            // ข้าราชการ
            if (strperson_group_code.Equals("01"))
            {
                strCriteria = " and member_type_code='" + strGBK + "' and c_active='Y' ";
            }
            // ข้าราชการ ขร. พม.
            else if (strperson_group_code.Equals("15"))
            {
                strCriteria = " and member_type_code IN ('" + strGBK + "','" + strSOS + "','" + strPVD3 + "') and c_active='Y' ";
            }

            // ลูกจ้าง
            else if (strperson_group_code.Equals("02"))
            {
                strCriteria = " and member_type_code='" + strGSJ + "' and c_active='Y' ";
            }

            // พนักงานมหาวิทยาลัย และ ลูกจ้าง พม.
            else if (strperson_group_code.Equals("03") || strperson_group_code.Equals("16"))
            {
                strCriteria = " and member_type_code IN ('" + strSOS + "','" + strPVD + "') and c_active='Y' ";
            }

            else if (strperson_group_code.Equals("11"))
            {
                strCriteria = " and member_type_code IN ('" + strSOS + "','" + strPVD2 + "') and c_active='Y' ";
            }
            else
            {
                strCriteria = " and member_type_code='" + strSOS + "' and c_active='Y' ";
            }
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            if (oMember_type.SP_MEMBER_TYPE_SEL(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboMember_type_checkboxes.Items.Clear();

                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboMember_type_checkboxes.Items.Add(new ListItem(dt.Rows[i]["member_type_name"].ToString(), dt.Rows[i]["member_type_code"].ToString()));
                }

                cboMember_type_checkboxes.Style.SelectBoxCssClass = "dd_chk_select_cust";
                cboMember_type_checkboxes.Style.SelectBoxWidth = 200;
                cboMember_type_checkboxes.Style.DropDownBoxBoxWidth = 300;


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

        protected void cboBudget_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeLabelBudget();
        }

        private void ChangeLabelBudget()
        {

            string strBusget_type = cboBudget_type.SelectedValue;
            string strLovTitle = "ค้นหาข้อมูลผังงบประมาณประจำปี (เงินงบประมาณ)";
            if (strBusget_type == "R") strLovTitle = "ค้นหาข้อมูลผังงบประมาณประจำปี (เงินรายได้)";
            imgList_budget_plan.Attributes.Add("onclick", "OpenPopUp('950px','500px','94%','" + strLovTitle + "' ,'../lov/budget_plan_lov.aspx?budget_type=" + strBusget_type +
                                                                "&budget_plan_code='+document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtbudget_plan_code.value+'" +
                                                                "&budget_name='+document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtbudget_name.value+'" +
                                                                "&produce_name='+document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtproduce_name.value+'" +
                                                                "&activity_name='+document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtactivity_name.value+'" +
                                                                "&plan_name='+document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtplan_name.value+'" +
                                                                "&work_name='+document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtwork_name.value+'" +
                                                                "&fund_name='+document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtfund_name.value+'" +
                                                                "&director_name='+document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtdirector_name.value+'" +
                                                                "&unit_name='+document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtunit_name.value+'" +
                                                                "&budget_plan_year='+document.forms[0]." + strPrefixCtr_main + "TabPanel2$txtbudget_plan_year.value+'" +
                                                                "&ctrl1=" + txtbudget_plan_code.ClientID +
                                                                "&ctrl2=" + txtbudget_name.ClientID +
                                                                "&ctrl3=" + txtproduce_name.ClientID +
                                                                "&ctrl4=" + txtactivity_name.ClientID +
                                                                "&ctrl5=" + txtplan_name.ClientID +
                                                                "&ctrl6=" + txtwork_name.ClientID +
                                                                "&ctrl7=" + txtfund_name.ClientID +
                                                                "&ctrl9=" + txtdirector_name.ClientID +
                                                                "&ctrl10=" + txtunit_name.ClientID +
                                                                "&ctrl11=" + txtbudget_plan_year.ClientID + "&show=2', '2');return false;");



            //if (strBusget_type == "B")
            //{
            //    Label54.Text = "แผนงบ :";
            //    Label55.Text = "รายการ :";
            //    Label53.Text = "กิจกรรม :";
            //    Label56.Text = "แผนงาน :";
            //}
            //else
            //{
            //    Label54.Text = "แผนงาน :";
            //    Label55.Text = "งานหลัก :";
            //    Label53.Text = "งานรอง :";
            //    Label56.Text = "งานย่อย :";
            //}
        }

        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public DataTable GetDataMemberType(string stperson_group, string strGBK, string strGSJ, string strSOS)
        {
            cMember_type oMember_type = new cMember_type();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
            strperson_group_code = string.Empty;
            strperson_group_code = stperson_group.Trim();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            if (strperson_group_code.Equals(strGBK))
            {
                strCriteria = " and member_type_code='" + strGBK + "' and c_active='Y' ";
            }
            else if (strperson_group_code.Equals(strGSJ))
            {
                strCriteria = " and member_type_code='" + strGSJ + "' and c_active='Y' ";
            }
            else
            {
                strCriteria = " and member_type_code='" + strSOS + "' and c_active='Y' ";
            }
            if (oMember_type.SP_MEMBER_TYPE_SEL(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        private void StoreDataFromJS()
        {
            if (Request.Form[strPrefixCtr_main + "TabPanel2$txtperson_start"] != null)
            {
                txtperson_start.Text = Request.Form[strPrefixCtr_main + "TabPanel2$txtperson_start"].ToString();
            }
            else
            {
                txtperson_start.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }

            if (Request.Form[strPrefixCtr_main + "TabPanel2$txtperson_end"] != null)
            {
                txtperson_end.Text = Request.Form[strPrefixCtr_main + "TabPanel2$txtperson_end"].ToString();
            }
            else
            {
                txtperson_end.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
            if (Request.Form[strPrefixCtr_main + "TabPanel3$txtperson_birth"] != null)
            {
                txtperson_birth.Text = Request.Form[strPrefixCtr_main + "TabPanel3$txtperson_birth"].ToString();
            }
            else
            {
                txtperson_birth.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

        #endregion

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
            this.imgSaveOnly.Click += new System.Web.UI.ImageClickEventHandler(this.imgSaveOnly_Click);
            //this.imgSave.Click += new System.Web.UI.ImageClickEventHandler(this.imgSave_Click);

        }
        #endregion

        private bool saveData1()
        {
            bool blnResult = false;
            bool blnDup = false;
            string strMessage = string.Empty;
            //Tab 1 
            string strperson_code = string.Empty,
                strtitle_code = string.Empty,
                strperson_thai_name = string.Empty,
                strperson_thai_surname = string.Empty,
                strperson_eng_name = string.Empty,
                strperson_eng_surname = string.Empty,
                strperson_nickname = string.Empty,
                strperson_id = string.Empty,
                strperson_pic = string.Empty,
                strC_active = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty,
                strCreatedDate = string.Empty,
                strUpdatedDate = string.Empty;
            string strScript = string.Empty;
            cPerson oPerson = new cPerson();
            DataSet ds = new DataSet();
            try
            {
                #region set Data
                strperson_code = txtperson_code.Text;
                strtitle_code = cboTitle.SelectedValue;
                if (Request.Form[strPrefixCtr_main + "TabPanel1$cboTitle"] != null)
                {
                    strtitle_code = Request.Form[strPrefixCtr_main + "TabPanel1$cboTitle"].ToString();
                }
                strperson_thai_name = txtperson_thai_name.Text;
                strperson_thai_surname = txtperson_thai_surname.Text;
                strperson_eng_name = txtperson_eng_name.Text;
                strperson_eng_surname = txtperson_eng_surname.Text;
                strperson_nickname = txtperson_nickname.Text;
                strperson_id = txtperson_id.Text;
                strperson_pic = Request.Form[strPrefixCtr_main + "TabPanel1$txtperson_pic"];

                if (chkStatus.Checked == true)
                {
                    strC_active = "Y";
                }
                else
                {
                    strC_active = "N";
                }
                strCreatedBy = Session["username"].ToString();
                strUpdatedBy = Session["username"].ToString();
                #endregion
                if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    #region check dup
                    string strCheckDup = string.Empty;
                    strCheckDup = " and person_thai_name = '" + strperson_thai_name.Trim() + "' and person_thai_surname= '" +
                                                  strperson_thai_surname.Trim() + "' and person_code<>'" + strperson_code.Trim() + "' ";
                    if (!oPerson.SP_PERSON_LIST_SEL(strCheckDup, ref ds, ref strMessage))
                    {
                        lblError.Text = strMessage;
                    }
                    else
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            strScript =
                                "alert(\"ไม่สามารถแก้ไขข้อมูลได้ เนื่องจาก" +
                                "\\nข้อมูลบุคคลกร : " + strperson_thai_name.Trim() + "  " + strperson_thai_surname.Trim() +
                                "\\nซ้ำ\");\n";
                            blnDup = true;
                        }
                    }
                    #endregion
                    #region edit
                    if (!blnDup)
                    {
                        if (oPerson.SP_PERSON_HIS_UPD(strperson_code, strtitle_code, strperson_thai_name, strperson_thai_surname, strperson_eng_name,
                                                                                                 strperson_eng_surname, strperson_nickname, strperson_id, strperson_pic, strC_active, strUpdatedBy, ref strMessage))
                        {
                            saveData2();
                            saveData3();
                            saveData4();
                            UploadFile();
                            blnResult = true;
                        }
                        else
                        {
                            lblError.Text = strMessage.ToString();
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "chkdup", strScript, true);
                    }
                    #endregion
                }
                else
                {
                    #region check dup
                    string strCheckDup = string.Empty;
                    strCheckDup = " and person_thai_name = '" + strperson_thai_name.Trim() + "' and person_thai_surname= '" + strperson_thai_surname.Trim() + "' ";
                    if (!oPerson.SP_PERSON_LIST_SEL(strCheckDup, ref ds, ref strMessage))
                    {
                        lblError.Text = strMessage;
                    }
                    else
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            strScript =
                                "alert(\"ไม่สามารถแก้ไขข้อมูลได้ เนื่องจาก" +
                                "\\nข้อมูลบุคคลกร : " + strperson_thai_name.Trim() + "  " + strperson_thai_surname.Trim() +
                                "\\nซ้ำ\");\n";
                            blnDup = true;
                        }
                    }
                    #endregion
                    #region insert
                    if (!blnDup)
                    {
                        if (oPerson.SP_PERSON_HIS_INS(strtitle_code, strperson_thai_name, strperson_thai_surname, strperson_eng_name,
                                                                                                 strperson_eng_surname, strperson_nickname, strperson_id, strperson_pic, strC_active, strCreatedBy, ref strMessage))
                        {
                            string strGetcode = " and person_thai_name = '" + strperson_thai_name.Trim() + "' and person_thai_surname = '" + strperson_thai_surname + "' ";
                            if (!oPerson.SP_PERSON_LIST_SEL(strGetcode, ref ds, ref strMessage))
                            {
                                lblError.Text = strMessage;
                            }
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                strperson_code = ds.Tables[0].Rows[0]["person_code"].ToString();
                            }
                            ViewState["person_code"] = strperson_code;
                            txtperson_code.Text = ViewState["person_code"].ToString();
                            saveData2();
                            saveData3();
                            saveData4();
                            UploadFile();
                            blnResult = true;
                        }
                        else
                        {
                            lblError.Text = strMessage.ToString();
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "close", strScript, true);
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oPerson.Dispose();
            }
            return blnResult;
        }

        private bool saveData2()
        {
            bool blnResult = false;
            string strMessage = string.Empty;
            //Tab 2
            string strperson_code = string.Empty,
                strposition_code = string.Empty,
                strperson_level = string.Empty,
                strperson_level_name = string.Empty,
                strtype_position_code = string.Empty,
                strtype_position_name = string.Empty,

                strperson_postionno = string.Empty,
                strbranch_code = string.Empty,
                strbank_no = string.Empty,
                strbranch_code_2 = string.Empty,
                strbank_no_2 = string.Empty,

                strperson_salaly = string.Empty,
                strperson_group = string.Empty,
                strperson_start = string.Empty,
                strperson_end = string.Empty,
                strmember_type = string.Empty,
                strmember_type_add = string.Empty,
                strperson_manage_code = string.Empty,
                strbudget_plan_code = string.Empty,
                strperson_work_status = string.Empty,
                strUpdatedBy = string.Empty;
            string strScript = string.Empty;
            cPerson oPerson = new cPerson();
            DataSet ds = new DataSet();
            try
            {
                #region set Data
                strperson_code = txtperson_code.Text;
                strposition_code = txtposition_code.Text;
                strperson_level = txtperson_level.Text;
                strperson_level_name = txtlevel_position_name.Text;
                strtype_position_code = txttype_position_code.Text;
                strtype_position_name = txttype_position_name.Text;

                strperson_postionno = txtperson_postionno.Text;
                strbranch_code = txtbranch_code.Text;
                strbank_no = txtbank_no.Text;


                strperson_salaly = txtperson_salaly.Text;
                strperson_group = cboPerson_group.SelectedValue;
                if (Request.Form[strPrefixCtr_main + "TabPanel2$cboPerson_group"] != null)
                {
                    strperson_group = Request.Form[strPrefixCtr_main + "TabPanel2$cboPerson_group"].ToString();
                }
                strperson_start = txtperson_start.Text;
                strperson_end = txtperson_end.Text;

                foreach (ListItem item in cboMember_type_checkboxes.Items)
                {
                    if (item.Selected)
                    {
                        strmember_type += item.Value + ",";
                    }
                }
                if (strmember_type.Length > 0)
                    strmember_type = strmember_type.Substring(0, strmember_type.Length - 1);


                //if (Request.Form[strPrefixCtr_main + "TabPanel2$cboMember_type"] != null)
                //{
                //    strmember_type = Request.Form[strPrefixCtr_main + "TabPanel2$cboMember_type"].ToString();
                //}

                strmember_type_add = txtmember_type_add.Text;
                strperson_manage_code = txtperson_manage_code.Text;
                strbudget_plan_code = txtbudget_plan_code.Text;
                strperson_work_status = cboPerson_work_status.SelectedValue;
                if (Request.Form[strPrefixCtr_main + "TabPanel2$cboPerson_work_status"] != null)
                {
                    strperson_work_status = Request.Form[strPrefixCtr_main + "TabPanel2$cboPerson_work_status"].ToString();
                }
                strUpdatedBy = Session["username"].ToString();
                #endregion
                #region edit
                strbranch_code_2 = txtbranch_code_2.Text;
                strbank_no_2 = txtbank_no_2.Text;
                if (oPerson.SP_PERSON_WORK_UPD(strperson_code, strposition_code, strperson_level, strperson_postionno, strbranch_code,
                                               strbank_no, strperson_salaly, strperson_start, strperson_end, strperson_group,
                                               strmember_type, strmember_type_add, strperson_manage_code, strbudget_plan_code,
                                               strperson_work_status, strUpdatedBy, txttype_position_code.Text, strbranch_code_2,
                                               strbank_no_2, txtperson_salaly_2.Text, txtperson_salaly_3.Text, ref strMessage))
                {
                    blnResult = true;
                }
                else
                {
                    lblError.Text = strMessage.ToString();
                }


                if (oPerson.SP_PERSON_CUMULATIVE_UPD(strperson_code, txtCumulative_acc.Text, txtCumulative_money.Text, ref strMessage))
                {
                    blnResult = true;
                }
                else
                {
                    lblError.Text = strMessage.ToString();
                }





                #endregion
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oPerson.Dispose();
            }
            return blnResult;
        }

        private bool saveData3()
        {
            bool blnResult = false;
            string strMessage = string.Empty;
            //Tab 2
            string strperson_code = string.Empty,
                strperson_sex = string.Empty,
                strperson_width = string.Empty,
                strperson_high = string.Empty,
                strperson_origin = string.Empty,
                strperson_nation = string.Empty,
                strperson_religion = string.Empty,
                strperson_birth = string.Empty,
                strperson_marry = string.Empty,
                strUpdatedBy = string.Empty;
            string strScript = string.Empty;

            cPerson oPerson = new cPerson();
            DataSet ds = new DataSet();
            try
            {
                #region set Data
                strperson_code = txtperson_code.Text;
                strperson_sex = cboPerson_sex.SelectedValue;
                strperson_width = txtperson_width.Text;
                strperson_high = txtperson_high.Text;
                strperson_origin = txtperson_origin.Text;
                strperson_nation = txtperson_nation.Text;
                strperson_religion = txtperson_religion.Text;
                strperson_birth = txtperson_birth.Text;
                strperson_marry = cboPerson_marry.SelectedValue;
                strUpdatedBy = Session["username"].ToString();
                #endregion
                #region edit
                if (oPerson.SP_PERSON_STATUS_UPD(strperson_code, strperson_sex, strperson_width, strperson_high, strperson_origin,
                                                                                                strperson_nation, strperson_religion, strperson_birth, strperson_marry, strUpdatedBy, ref strMessage))
                {
                    blnResult = true;
                }
                else
                {
                    lblError.Text = strMessage.ToString();
                }
                #endregion
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oPerson.Dispose();
            }
            return blnResult;
        }

        private bool saveData4()
        {
            bool blnResult = false;
            string strMessage = string.Empty;
            //Tab 2
            string strperson_code = string.Empty,
                strperson_room = string.Empty,
                strperson_floor = string.Empty,
                strperson_village = string.Empty,
                strperson_homeno = string.Empty,
                strperson_soi = string.Empty,
                strperson_moo = string.Empty,
                strperson_road = string.Empty,
                strperson_tambol = string.Empty,
                strperson_aumphur = string.Empty,
                strperson_province = string.Empty,
                strperson_postno = string.Empty,
                strperson_tel = string.Empty,
                strperson_contact = string.Empty,
                strperson_ralation = string.Empty,
                strperson_contact_tel = string.Empty,
                strUpdatedBy = string.Empty;
            string strScript = string.Empty;
            cPerson oPerson = new cPerson();
            DataSet ds = new DataSet();
            try
            {
                #region set Data
                strperson_code = txtperson_code.Text;
                strperson_room = txtperson_room.Text;
                strperson_floor = txtperson_floor.Text;
                strperson_village = txtperson_village.Text;
                strperson_homeno = txtperson_homeno.Text;
                strperson_soi = txtperson_soi.Text;
                strperson_moo = txtperson_moo.Text;
                strperson_road = txtperson_road.Text;
                strperson_tambol = txtperson_tambol.Text;
                strperson_aumphur = txtperson_aumphur.Text;
                strperson_province = txtperson_province.Text;
                strperson_postno = txtperson_postno.Text;
                strperson_tel = txtperson_tel.Text;
                strperson_contact = txtperson_contact.Text;
                strperson_ralation = txtperson_ralation.Text;
                strperson_contact_tel = txtperson_contact_tel.Text;
                strUpdatedBy = Session["username"].ToString();
                #endregion
                #region edit
                if (oPerson.SP_PERSON_ADDRESS_UPD(strperson_code, strperson_room, strperson_floor, strperson_village, strperson_homeno, strperson_soi,
                                                                                                        strperson_moo, strperson_road, strperson_tambol, strperson_aumphur, strperson_province, strperson_postno,
                                                                                                        strperson_tel, strperson_contact, strperson_ralation, strperson_contact_tel, strUpdatedBy, ref strMessage))
                {
                    blnResult = true;
                }
                else
                {
                    lblError.Text = strMessage.ToString();
                }
                #endregion
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oPerson.Dispose();
            }
            return blnResult;
        }

        private void UploadFile()
        {
        }

        private void imgSaveOnly_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (saveData1())
            {
                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    Response.Redirect("person_control.aspx?mode=edit&person_code=" + ViewState["person_code"].ToString() + "&page=" + ViewState["page"].ToString() + "&PageStatus=save", true);
                }
                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    setData();
                    txtperson_code.ReadOnly = true;
                    txtperson_code.CssClass = "textboxdis";
                    // string strScript1 = "RefreshMain('" + ViewState["page"].ToString() + "');";
                    string strScript1 = "ClosePopUpListPost('" + ViewState["page"].ToString() + "','1');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
                }
                setData();
                txtperson_code.ReadOnly = true;
                txtperson_code.CssClass = "textboxdis";
                BindGridView1();
                BindGridView2();
                BindGridView3();
                MsgBox("บันทึกข้อมูลสมบูรณ์");
            }
        }

        private void setData()
        {
            cPerson oPerson = new cPerson();
            DataSet ds = new DataSet();
            string strMessage = string.Empty, strCriteria = string.Empty;
            #region clear Data
            //Tab 1 
            string strperson_code = string.Empty,
                strtitle_code = string.Empty,
                strperson_thai_name = string.Empty,
                strperson_thai_surname = string.Empty,
                strperson_eng_name = string.Empty,
                strperson_eng_surname = string.Empty,
                strperson_nickname = string.Empty,
                strperson_id = string.Empty,
                strperson_pic = string.Empty,
                strC_active = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty,
                strCreatedDate = string.Empty,
                strUpdatedDate = string.Empty,
                strBudget_type = string.Empty;
            //Tab 2 
            string strposition_code = string.Empty,
                strposition_name = string.Empty,

                strperson_level = string.Empty,
                strperson_level_name = string.Empty,
                strtype_position_code = string.Empty,
                strtype_position_name = string.Empty,

                strperson_postionno = string.Empty,

                strbranch_code = string.Empty,
                strbranch_name = string.Empty,
                strbank_name = string.Empty,
                strbank_no = string.Empty,

                strbranch_code2 = string.Empty,
                strbranch_name2 = string.Empty,
                strbank_name2 = string.Empty,
                strbank_no2 = string.Empty,

                strperson_salaly = string.Empty,
                strperson_salaly_2 = string.Empty,
                strperson_salaly_3 = string.Empty,

                strperson_group = string.Empty,
                strperson_start = string.Empty,
                strperson_end = string.Empty,
                strmember_type = string.Empty,
                strmember_type_add = string.Empty,
                strperson_manage_code = string.Empty,
                strperson_manage_name = string.Empty,
                strbudget_plan_code = string.Empty,
                strbudget_name = string.Empty,
                strproduce_name = string.Empty,
                stractivity_name = string.Empty,
                strplan_name = string.Empty,
                strwork_name = string.Empty,
                strfund_name = string.Empty,
                strdirector_name = string.Empty,
                strunit_name = string.Empty,
                strbudget_plan_year = string.Empty,
                strperson_work_status = string.Empty;
            //Tab 3
            string strperson_sex = string.Empty,
                strperson_width = string.Empty,
                strperson_high = string.Empty,
                strperson_origin = string.Empty,
                strperson_nation = string.Empty,
                strperson_religion = string.Empty,
                strperson_birth = string.Empty,
                strperson_marry = string.Empty;
            //Tab 4
            string strperson_room = string.Empty,
                strperson_floor = string.Empty,
                strperson_village = string.Empty,
                strperson_homeno = string.Empty,
                strperson_soi = string.Empty,
                strperson_moo = string.Empty,
                strperson_road = string.Empty,
                strperson_tambol = string.Empty,
                strperson_aumphur = string.Empty,
                strperson_province = string.Empty,
                strperson_postno = string.Empty,
                strperson_tel = string.Empty,
                strperson_contact = string.Empty,
                strperson_ralation = string.Empty,
                strperson_contact_tel = string.Empty;
            #endregion
            try
            {
                strCriteria = " and person_code = '" + ViewState["person_code"].ToString() + "' ";
                if (!oPerson.SP_PERSON_ALL_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region get Data
                        //Tab 1 
                        strperson_code = ds.Tables[0].Rows[0]["person_code"].ToString();
                        strtitle_code = ds.Tables[0].Rows[0]["title_code"].ToString();
                        strperson_thai_name = ds.Tables[0].Rows[0]["person_thai_name"].ToString();
                        strperson_thai_surname = ds.Tables[0].Rows[0]["person_thai_surname"].ToString();
                        strperson_eng_name = ds.Tables[0].Rows[0]["person_eng_name"].ToString();
                        strperson_eng_surname = ds.Tables[0].Rows[0]["person_eng_surname"].ToString();
                        strperson_nickname = ds.Tables[0].Rows[0]["person_nickname"].ToString();
                        strperson_id = ds.Tables[0].Rows[0]["person_id"].ToString();
                        strperson_pic = ds.Tables[0].Rows[0]["person_pic"].ToString();
                        strC_active = ds.Tables[0].Rows[0]["c_active"].ToString();
                        strCreatedBy = ds.Tables[0].Rows[0]["c_created_by"].ToString();
                        strUpdatedBy = ds.Tables[0].Rows[0]["c_updated_by"].ToString();
                        strCreatedDate = ds.Tables[0].Rows[0]["d_created_date"].ToString();
                        strUpdatedDate = ds.Tables[0].Rows[0]["d_updated_date"].ToString();
                        //Tab 2 
                        strposition_code = ds.Tables[0].Rows[0]["position_code"].ToString();
                        strposition_name = ds.Tables[0].Rows[0]["position_name"].ToString();

                        strperson_level = ds.Tables[0].Rows[0]["person_level"].ToString();
                        strperson_level_name = ds.Tables[0].Rows[0]["level_position_name"].ToString();
                        strtype_position_code = ds.Tables[0].Rows[0]["type_position_code"].ToString();
                        strtype_position_name = ds.Tables[0].Rows[0]["type_position_name"].ToString();

                        strperson_postionno = ds.Tables[0].Rows[0]["person_postionno"].ToString();

                        strbranch_code = ds.Tables[0].Rows[0]["branch_code"].ToString();
                        strbranch_name = ds.Tables[0].Rows[0]["branch_name"].ToString();
                        strbank_name = ds.Tables[0].Rows[0]["bank_name"].ToString();
                        strbank_no = ds.Tables[0].Rows[0]["bank_no"].ToString();

                        strbranch_code2 = ds.Tables[0].Rows[0]["branch_code_2"].ToString();
                        strbranch_name2 = ds.Tables[0].Rows[0]["branch_name_2"].ToString();
                        strbank_name2 = ds.Tables[0].Rows[0]["bank_name_2"].ToString();
                        strbank_no2 = ds.Tables[0].Rows[0]["bank_no_2"].ToString();

                        strperson_salaly = ds.Tables[0].Rows[0]["person_salaly"].ToString();
                        if (string.IsNullOrEmpty(strperson_salaly)) strperson_salaly = "0";

                        strperson_salaly_2 = ds.Tables[0].Rows[0]["person_salaly_2"].ToString();
                        if (string.IsNullOrEmpty(strperson_salaly_2)) strperson_salaly_2 = "0";
                        strperson_salaly_3 = ds.Tables[0].Rows[0]["person_salaly_3"].ToString();
                        if (string.IsNullOrEmpty(strperson_salaly_3)) strperson_salaly_3 = "0";

                        strperson_group = ds.Tables[0].Rows[0]["person_group_code"].ToString();
                        strperson_start = ds.Tables[0].Rows[0]["person_start"].ToString();
                        strperson_end = ds.Tables[0].Rows[0]["person_end"].ToString();
                        strmember_type = ds.Tables[0].Rows[0]["member_type_code"].ToString();
                        strmember_type_add = ds.Tables[0].Rows[0]["member_type_add"].ToString();
                        strperson_manage_code = ds.Tables[0].Rows[0]["person_manage_code"].ToString();
                        strperson_manage_name = ds.Tables[0].Rows[0]["person_manage_name"].ToString();
                        strBudget_type = ds.Tables[0].Rows[0]["person_budget_type"].ToString();

                        if (cboBudget_type.Items.FindByValue(strBudget_type) != null)
                        {
                            cboBudget_type.SelectedIndex = -1;
                            cboBudget_type.Items.FindByValue(strBudget_type).Selected = true;
                        }



                        strbudget_plan_code = ds.Tables[0].Rows[0]["budget_plan_code"].ToString();
                        strbudget_name = ds.Tables[0].Rows[0]["budget_name"].ToString();
                        strproduce_name = ds.Tables[0].Rows[0]["produce_name"].ToString();
                        stractivity_name = ds.Tables[0].Rows[0]["activity_name"].ToString();
                        strplan_name = ds.Tables[0].Rows[0]["plan_name"].ToString();
                        strwork_name = ds.Tables[0].Rows[0]["work_name"].ToString();
                        strfund_name = ds.Tables[0].Rows[0]["fund_name"].ToString();
                        strdirector_name = ds.Tables[0].Rows[0]["director_name"].ToString();
                        strunit_name = ds.Tables[0].Rows[0]["unit_name"].ToString();
                        strbudget_plan_year = ds.Tables[0].Rows[0]["budget_plan_year"].ToString();
                        strperson_work_status = ds.Tables[0].Rows[0]["person_work_status_code"].ToString();



                        //Tab 3
                        strperson_sex = ds.Tables[0].Rows[0]["person_sex"].ToString();
                        strperson_width = ds.Tables[0].Rows[0]["person_width"].ToString();
                        strperson_high = ds.Tables[0].Rows[0]["person_high"].ToString();
                        strperson_origin = ds.Tables[0].Rows[0]["person_origin"].ToString();
                        strperson_nation = ds.Tables[0].Rows[0]["person_nation"].ToString();
                        strperson_religion = ds.Tables[0].Rows[0]["person_religion"].ToString();
                        strperson_birth = ds.Tables[0].Rows[0]["person_birth"].ToString();
                        strperson_marry = ds.Tables[0].Rows[0]["person_marry"].ToString();
                        //Tab 4
                        strperson_room = ds.Tables[0].Rows[0]["person_room"].ToString();
                        strperson_floor = ds.Tables[0].Rows[0]["person_floor"].ToString();
                        strperson_village = ds.Tables[0].Rows[0]["person_village"].ToString();
                        strperson_homeno = ds.Tables[0].Rows[0]["person_homeno"].ToString();
                        strperson_soi = ds.Tables[0].Rows[0]["person_soi"].ToString();
                        strperson_moo = ds.Tables[0].Rows[0]["person_moo"].ToString();
                        strperson_road = ds.Tables[0].Rows[0]["person_road"].ToString();
                        strperson_tambol = ds.Tables[0].Rows[0]["person_tambol"].ToString();
                        strperson_aumphur = ds.Tables[0].Rows[0]["person_aumphur"].ToString();
                        strperson_province = ds.Tables[0].Rows[0]["person_province"].ToString();
                        strperson_postno = ds.Tables[0].Rows[0]["person_postno"].ToString();
                        strperson_tel = ds.Tables[0].Rows[0]["person_tel"].ToString();
                        strperson_contact = ds.Tables[0].Rows[0]["person_contact"].ToString();
                        strperson_ralation = ds.Tables[0].Rows[0]["person_ralation"].ToString();
                        strperson_contact_tel = ds.Tables[0].Rows[0]["person_contact_tel"].ToString();

                        strBudget_type = ds.Tables[0].Rows[0]["person_budget_type"].ToString();


                        #endregion

                        #region set Control
                        TabContainer1.Tabs[1].Visible = true;
                        TabContainer1.Tabs[2].Visible = true;
                        TabContainer1.Tabs[3].Visible = true;
                        TabContainer1.Tabs[4].Visible = true;
                        TabContainer1.Tabs[5].Visible = true;
                        TabContainer1.Tabs[6].Visible = true;
                        //Tab 1 
                        txtperson_code.Text = strperson_code;
                        Session["person_code"] = strperson_code;
                        InitcboTitle();
                        if (cboTitle.Items.FindByValue(strtitle_code) != null)
                        {
                            cboTitle.SelectedIndex = -1;
                            cboTitle.Items.FindByValue(strtitle_code).Selected = true;
                        }
                        txtperson_thai_name.Text = strperson_thai_name;
                        txtperson_thai_surname.Text = strperson_thai_surname;
                        txtperson_eng_name.Text = strperson_eng_name;
                        txtperson_eng_surname.Text = strperson_eng_surname;
                        txtperson_nickname.Text = strperson_nickname;
                        txtperson_id.Text = strperson_id;
                        txtperson_pic.Text = strperson_pic;
                        if (strperson_pic.Length != 0)
                        {
                            imgPerson.ImageUrl = "../../person_pic/" + strperson_pic;
                        }
                        else
                        {
                            imgPerson.ImageUrl = "../../person_pic/image_n_a.jpg";
                        }
                        txtUpdatedBy.Text = strUpdatedBy;
                        txtUpdatedDate.Text = strUpdatedDate;

                        if (strC_active.Equals("Y"))
                        {
                            chkStatus.Checked = true;
                        }
                        else
                        {
                            chkStatus.Checked = false;
                        }


                        //Tab 2 

                        InitcboBudgetType();
                        if (cboBudget_type.Items.FindByValue(strBudget_type) != null)
                        {
                            cboBudget_type.SelectedIndex = -1;
                            cboBudget_type.Items.FindByValue(strBudget_type).Selected = true;
                        }
                        ChangeLabelBudget();

                        txtposition_code.Text = strposition_code;
                        txtposition_name.Text = strposition_name;

                        txtperson_level.Text = strperson_level;
                        txtlevel_position_name.Text = strperson_level_name;
                        txttype_position_code.Text = strtype_position_code;
                        txttype_position_name.Text = strtype_position_name;

                        txtperson_postionno.Text = strperson_postionno;

                        txtbranch_code.Text = strbranch_code;
                        txtbranch_name.Text = strbranch_name;
                        txtbank_name.Text = strbank_name;
                        txtbank_no.Text = strbank_no;

                        txtbranch_code_2.Text = strbranch_code2;
                        txtbranch_name_2.Text = strbranch_name2;
                        txtbank_name_2.Text = strbank_name2;
                        txtbank_no_2.Text = strbank_no2;

                        txtperson_salaly.Text = String.Format("{0:0.00}", decimal.Parse(strperson_salaly));
                        txtperson_salaly_2.Text = String.Format("{0:0.00}", decimal.Parse(strperson_salaly_2));
                        txtperson_salaly_3.Text = String.Format("{0:0.00}", decimal.Parse(strperson_salaly_3));

                        InitcboPerson_group();
                        if (cboPerson_group.Items.FindByValue(strperson_group) != null)
                        {
                            cboPerson_group.SelectedIndex = -1;
                            cboPerson_group.Items.FindByValue(strperson_group).Selected = true;
                        }
                        txtperson_start.Text = cCommon.CheckDate(strperson_start);
                        txtperson_end.Text = cCommon.CheckDate(strperson_end);
                        InitcboMember_type();

                        foreach (ListItem item in cboMember_type_checkboxes.Items)
                        {
                            item.Selected = strmember_type.Contains(item.Value);
                        }


                        txtmember_type_add.Text = String.Format("{0:0.00}", decimal.Parse(strmember_type_add));
                        txtperson_manage_code.Text = strperson_manage_code;
                        txtperson_manage_name.Text = strperson_manage_name;
                        txtbudget_plan_code.Text = strbudget_plan_code;
                        txtbudget_name.Text = strbudget_name;
                        txtproduce_name.Text = strproduce_name;
                        txtactivity_name.Text = stractivity_name;
                        txtplan_name.Text = strplan_name;
                        txtwork_name.Text = strwork_name;
                        txtfund_name.Text = strfund_name;
                        txtdirector_name.Text = strdirector_name;
                        txtunit_name.Text = strunit_name;
                        txtbudget_plan_year.Text = strbudget_plan_year;
                        InitcboPerson_work_status();
                        if (cboPerson_work_status.Items.FindByValue(strperson_work_status) != null)
                        {
                            cboPerson_work_status.SelectedIndex = -1;
                            cboPerson_work_status.Items.FindByValue(strperson_work_status).Selected = true;
                        }



                        //Tab 3
                        if (cboPerson_sex.Items.FindByValue(strperson_sex) != null)
                        {
                            cboPerson_sex.SelectedIndex = -1;
                            cboPerson_sex.Items.FindByValue(strperson_sex).Selected = true;
                        }
                        txtperson_width.Text = strperson_width;
                        txtperson_high.Text = strperson_high;
                        txtperson_origin.Text = strperson_origin;
                        txtperson_nation.Text = strperson_nation;
                        txtperson_religion.Text = strperson_religion;
                        txtperson_birth.Text = cCommon.CheckDate(strperson_birth);
                        if (cboPerson_marry.Items.FindByValue(strperson_marry) != null)
                        {
                            cboPerson_marry.SelectedIndex = -1;
                            cboPerson_marry.Items.FindByValue(strperson_marry).Selected = true;
                        }
                        //Tab 4
                        txtperson_room.Text = strperson_room;
                        txtperson_floor.Text = strperson_floor;
                        txtperson_village.Text = strperson_village;
                        txtperson_homeno.Text = strperson_homeno;
                        txtperson_soi.Text = strperson_soi;
                        txtperson_moo.Text = strperson_moo;
                        txtperson_road.Text = strperson_road;
                        txtperson_tambol.Text = strperson_tambol;
                        txtperson_aumphur.Text = strperson_aumphur;
                        txtperson_province.Text = strperson_province;
                        txtperson_postno.Text = strperson_postno;
                        txtperson_tel.Text = strperson_tel;
                        txtperson_contact.Text = strperson_contact;
                        txtperson_ralation.Text = strperson_ralation;
                        txtperson_contact_tel.Text = strperson_contact_tel;
                        try
                        {
                            var dateperson_birth = cCommon.SeekDate(txtperson_birth.Text);
                            DateTime dperson_birth = DateTime.Parse(dateperson_birth);
                            long intperson_birth = cCommon.DateTimeUtil.DateDiff(cCommon.DateInterval.Year, dperson_birth.Date, DateTime.Now.Date);
                            lblAge.Text = "อายุปัจจุบัน  " + intperson_birth.ToString() + "  ปี";

                        }
                        catch
                        {

                        }

                        txtCumulative_acc.Text = ds.Tables[0].Rows[0]["cumulative_acc"].ToString();
                        string strCumulative_money = "0.00";
                        try
                        {
                            strCumulative_money = String.Format("{0:0.00}", decimal.Parse(ds.Tables[0].Rows[0]["Cumulative_money"].ToString()));
                        }
                        catch
                        {
                            strCumulative_money = "0.00";
                        }
                        txtCumulative_money.Text = strCumulative_money;

                        BindGridView1();
                        BindGridView2();
                        BindGridView3();
                        BindGridViewLoan();
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
                throw ex;
            }
        }

        #region GridView Event

        private void BindGridView1()
        {
            cPerson oPerson = new cPerson();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string strperson_code = string.Empty;
            string strYear = string.Empty;
            if (ViewState["mode"].ToString().ToLower().Equals("add"))
            {
                strCriteria = " And 1=2  ";
            }
            else
            {
                strperson_code = ViewState["person_code"].ToString();
                strYear = txtbudget_plan_year.Text;
                strCriteria = " And (person_code='" + strperson_code + "')  ";
                // strCriteria = " And (person_code='" + strperson_code + "')  And  (person_item_year='" + strYear + "')  ";
            }
            try
            {
                if (!oPerson.SP_PERSON_ITEM_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    ds.Tables[0].DefaultView.Sort = ViewState["sort"] + " " + ViewState["direction"];
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                if (GridView1.Rows.Count == 0)
                {
                    EmptyGridFix(GridView1);
                }
                oPerson.Dispose();
                ds.Dispose();
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
                ImageButton imgAdd = (ImageButton)e.Row.FindControl("imgAdd");
                imgAdd.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgGridAdd"].Rows[0]["img"].ToString();
                imgAdd.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgGridAdd"].Rows[0]["title"].ToString());

                imgAdd.Attributes.Add("onclick", "OpenPopUp('800px','400px','95%','เพิ่มข้อมูลรายรับ/จ่ายบุคคลากร','person_item_control.aspx?mode=add&person_code=" + txtperson_code.Text +
                                                              "&person_name=" + txtperson_thai_name.Text + "  " + txtperson_thai_surname.Text +
                                                              "&person_group_code=" + cboPerson_group.SelectedValue +
                                                              "&year=" + txtbudget_plan_year.Text + "','2');return false;");
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
                Label lblitem_code = (Label)e.Row.FindControl("lblitem_code");
                Label lblitem_name = (Label)e.Row.FindControl("lblitem_name");
                Label lblc_active = (Label)e.Row.FindControl("lblc_active");
                Label lblitem_debit = (Label)e.Row.FindControl("lblitem_debit");
                Label lblitem_credit = (Label)e.Row.FindControl("lblitem_credit");
                Label lblbudget_type = (Label)e.Row.FindControl("lblbudget_type");


                string strStatus = lblc_active.Text;
                if (!lblitem_debit.Text.Equals(""))
                {
                    lblitem_debit.Text = String.Format("{0:#,##0.00}", decimal.Parse(lblitem_debit.Text));
                }
                if (!lblitem_credit.Text.Equals(""))
                {
                    lblitem_credit.Text = String.Format("{0:#,##0.00}", decimal.Parse(lblitem_credit.Text));
                }
                if (lblbudget_type.Text.Equals("R"))
                {
                    lblbudget_type.Text = "เงินรายได้";
                }
                else if (lblbudget_type.Text.Equals("B"))
                {
                    lblbudget_type.Text = "เงินงบประมาณ";
                }




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

                #region set Image Edit & Delete

                ImageButton imgEdit = (ImageButton)e.Row.FindControl("imgEdit");
                imgEdit.Attributes.Add("onclick", "OpenPopUp('800px','400px','95%','แก้ไขข้อมูลรายรับ/จ่ายบุคคลากร','person_item_control.aspx?mode=edit&person_code=" +
                               txtperson_code.Text + "&person_name=" + txtperson_thai_name.Text + "  " + txtperson_thai_surname.Text + "&item_code=" +
                               lblitem_code.Text + "&year=" + txtbudget_plan_year.Text + "','2');return false;");

                Label lblCanEdit = (Label)e.Row.FindControl("lblCanEdit");
                imgEdit.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgEdit"].Rows[0]["img"].ToString();
                imgEdit.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgEdit"].Rows[0]["title"].ToString());

                ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                imgDelete.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["img"].ToString();
                imgDelete.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["title"].ToString());
                imgDelete.Attributes.Add("onclick", "return confirm(\"คุณต้องการลบข้อมูลรับ/จ่ายบุคคลากร " + lblitem_code.Text + " : " + lblitem_name.Text + " ?\");");
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
                BindGridView1();
                TabContainer1.ActiveTabIndex = 4;
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
            Label lblitem_code = (Label)GridView1.Rows[e.RowIndex].FindControl("lblitem_code");
            cPerson oPerson = new cPerson();
            try
            {
                if (!oPerson.SP_PERSON_ITEM_DEL(txtperson_code.Text, txtbudget_plan_year.Text, lblitem_code.Text, "N", strUpdatedBy, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                //else
                //{
                //    string strScript1 =
                //    "self.opener.document.forms[0].ctl00$ASPxRoundPanel1$ContentPlaceHolder2$txthpage.value=" + ViewState["page"].ToString() + ";\n" +
                //    "self.opener.document.forms[0].submit();\n" +
                //    "self.focus();\n";
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
                //}
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oPerson.Dispose();
            }
            BindGridView1();
        }

        #endregion

        #region GridView2 Event

        private void BindGridView2()
        {
            cPerson oPerson = new cPerson();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string strperson_code = string.Empty;
            if (ViewState["mode"].ToString().ToLower().Equals("add"))
            {
                strCriteria = " And 1=2  ";
            }
            else
            {
                strperson_code = ViewState["person_code"].ToString();
                strCriteria = " And (person_code='" + strperson_code + "')  ";
            }
            try
            {
                if (!oPerson.SP_PERSON_MEMBER_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    ds.Tables[0].DefaultView.Sort = ViewState["sort1"] + " " + ViewState["direction1"];
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
                if (GridView2.Rows.Count == 0)
                {
                    EmptyGridFix(GridView2);
                }
                oPerson.Dispose();
                ds.Dispose();
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
                ImageButton imgAdd = (ImageButton)e.Row.FindControl("imgAdd1");
                imgAdd.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgGridAdd"].Rows[0]["img"].ToString();
                imgAdd.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgGridAdd"].Rows[0]["title"].ToString());
                imgAdd.Attributes.Add("onclick", "OpenPopUp('800px','300px','91%','เพิ่มข้อมูลสมาชิก(ฌาปนกิจ) บุคคลากร','person_member_control.aspx?mode=add&person_code=" + txtperson_code.Text +
                                                              "&person_name=" + txtperson_thai_name.Text + "  " + txtperson_thai_surname.Text +
                                                              "&year=" + txtbudget_plan_year.Text + "','2');return false;");

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
                Label lblNo = (Label)e.Row.FindControl("lblNo1");
                int nNo = e.Row.RowIndex + 1;
                lblNo.Text = nNo.ToString();
                Label lblmember_code = (Label)e.Row.FindControl("lblmember_code");
                Label lblmember_name = (Label)e.Row.FindControl("lblmember_name");
                Label lblc_active = (Label)e.Row.FindControl("lblc_active1");
                string strStatus = lblc_active.Text;

                #region set ImageStatus
                ImageButton imgStatus = (ImageButton)e.Row.FindControl("imgStatus1");
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

                #region set Image Edit & Delete

                ImageButton imgEdit = (ImageButton)e.Row.FindControl("imgEdit1");
                //Label lblCanEdit = (Label)e.Row.FindControl("lblCanEdit2");
                imgEdit.Attributes.Add("onclick", "OpenPopUp('800px','300px','91%','แก้ไขข้อมูลสมาชิก(ฌาปนกิจ) บุคคลากร','person_member_control.aspx?mode=edit&person_code=" +
                                                              txtperson_code.Text + "&person_name=" + txtperson_thai_name.Text + "  " + txtperson_thai_surname.Text + "&member_code=" +
                                                              lblmember_code.Text + "&year=" + txtbudget_plan_year.Text + "','2');return false;");

                imgEdit.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgEdit"].Rows[0]["img"].ToString();
                imgEdit.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgEdit"].Rows[0]["title"].ToString());

                ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete1");
                imgDelete.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["img"].ToString();
                imgDelete.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["title"].ToString());
                imgDelete.Attributes.Add("onclick", "return confirm(\"คุณต้องการลบข้อมูลสมาชิก(ฌาปนกิจ) บุคคลากร " + lblmember_code.Text + " : " + lblmember_name.Text + " ?\");");
                #endregion

            }
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
                    if (ViewState["sort1"].Equals(GridView2.Columns[i].SortExpression))
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

        protected void GridView2_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                if (ViewState["sort1"].ToString().Equals(e.SortExpression.ToString()))
                {
                    if (ViewState["direction1"].Equals("DESC"))
                        ViewState["direction1"] = "ASC";
                    else
                        ViewState["direction1"] = "DESC";
                }
                else
                {
                    ViewState["sort1"] = e.SortExpression;
                    ViewState["direction1"] = "ASC";
                }
                BindGridView2();
                TabContainer1.ActiveTabIndex = 6;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string strMessage = string.Empty;
            string strCheck = string.Empty;
            string strScript = string.Empty;
            string strUpdatedBy = Session["username"].ToString();
            Label lblmember_code = (Label)GridView2.Rows[e.RowIndex].FindControl("lblmember_code");
            cPerson oPerson = new cPerson();
            try
            {
                if (!oPerson.SP_PERSON_MEMBER_DEL(txtperson_code.Text, lblmember_code.Text, "N", strUpdatedBy, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                //else
                //{
                //    string strScript1 =
                //    "self.opener.document.forms[0].ctl00$ASPxRoundPanel1$ContentPlaceHolder2$txthpage.value=" + ViewState["page"].ToString() + ";\n" +
                //    "self.opener.document.forms[0].submit();\n" +
                //    "self.focus();\n";
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
                //}
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oPerson.Dispose();
            }
            BindGridView2();
        }

        #endregion

        #region GridView3 Event

        private void BindGridView3()
        {
            cPerson oPerson = new cPerson();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string strperson_code = string.Empty;
            if (ViewState["mode"].ToString().ToLower().Equals("add"))
            {
                strCriteria = " And 1=2  ";
            }
            else
            {
                strperson_code = ViewState["person_code"].ToString();
                strCriteria = " And (person_code='" + strperson_code + "')  ";
            }
            try
            {
                if (!oPerson.SP_PERSON_POSITION_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    ds.Tables[0].DefaultView.Sort = ViewState["sort2"] + " " + ViewState["direction2"];
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
                if (GridView3.Rows.Count == 0)
                {
                    EmptyGridFix(GridView3);
                }
                oPerson.Dispose();
                ds.Dispose();
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
                ImageButton imgAdd = (ImageButton)e.Row.FindControl("imgAdd2");
                imgAdd.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgGridAdd"].Rows[0]["img"].ToString();
                imgAdd.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgGridAdd"].Rows[0]["title"].ToString());
                imgAdd.Attributes.Add("onclick", "OpenPopUp('800px','340px','91%','เพิ่มข้อมูลประวัติตำแหน่งบุคคลากร','person_position_control.aspx?mode=add&person_code=" + txtperson_code.Text +
                                                              "&person_name=" + txtperson_thai_name.Text + "  " + txtperson_thai_surname.Text +
                                                              "&position_code=" + txtposition_code.Text + "&position_name=" + txtposition_name.Text +
                                                              "&person_level=" + txtperson_level.Text +
                                                              "&person_salary=" + txtperson_salaly.Text + "','2');return false;");
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
                Label lblNo = (Label)e.Row.FindControl("lblNo2");
                int nNo = e.Row.RowIndex + 1;
                lblNo.Text = nNo.ToString();
                Label lblchange_date = (Label)e.Row.FindControl("lblchange_date");
                Label lblsalary_old = (Label)e.Row.FindControl("lblsalary_old");
                Label lblsalary_new = (Label)e.Row.FindControl("lblsalary_new");
                if (!lblchange_date.Text.Equals(""))
                {
                    lblchange_date.Text = cCommon.CheckDate(lblchange_date.Text);
                    lblsalary_old.Text = String.Format("{0:#,##0.00}", decimal.Parse(lblsalary_old.Text));
                    lblsalary_new.Text = String.Format("{0:#,##0.00}", decimal.Parse(lblsalary_new.Text));
                }
                Label lblc_active = (Label)e.Row.FindControl("lblc_active2");
                string strStatus = lblc_active.Text;

                #region set ImageStatus
                ImageButton imgStatus = (ImageButton)e.Row.FindControl("imgStatus2");
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

                #region set Image Edit & Delete

                ImageButton imgEdit = (ImageButton)e.Row.FindControl("imgEdit2");
                //Label lblCanEdit = (Label)e.Row.FindControl("lblCanEdit2");
                imgEdit.Attributes.Add("onclick", "OpenPopUp('800px','340px','91%','แก้ไขข้อมูลประวัติตำแหน่งบุคคลากร','person_position_control.aspx?mode=edit&person_code=" +
                                              txtperson_code.Text + "&person_name=" + txtperson_thai_name.Text + "  " + txtperson_thai_surname.Text + "&change_date=" +
                                              lblchange_date.Text + "','2');return false;");
                imgEdit.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgEdit"].Rows[0]["img"].ToString();
                imgEdit.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgEdit"].Rows[0]["title"].ToString());

                ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete2");
                imgDelete.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["img"].ToString();
                imgDelete.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["title"].ToString());
                imgDelete.Attributes.Add("onclick", "return confirm(\"คุณต้องการลบข้อมูลประวัติตำแหน่งบุคคลากรนี้ ?\");");
                #endregion

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
                    if (ViewState["sort2"].Equals(GridView3.Columns[i].SortExpression))
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
                            if (ViewState["direction2"].Equals("ASC"))
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

        protected void GridView3_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                if (ViewState["sort2"].ToString().Equals(e.SortExpression.ToString()))
                {
                    if (ViewState["direction2"].Equals("DESC"))
                        ViewState["direction2"] = "ASC";
                    else
                        ViewState["direction2"] = "DESC";
                }
                else
                {
                    ViewState["sort2"] = e.SortExpression;
                    ViewState["direction2"] = "ASC";
                }
                BindGridView3();
                TabContainer1.ActiveTabIndex = 7;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        protected void GridView3_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string strMessage = string.Empty;
            string strCheck = string.Empty;
            string strScript = string.Empty;
            string strUpdatedBy = Session["username"].ToString();
            Label lblchange_date = (Label)GridView3.Rows[e.RowIndex].FindControl("lblchange_date");
            cPerson oPerson = new cPerson();
            try
            {
                if (!oPerson.SP_PERSON_POSITION_DEL(txtperson_code.Text, lblchange_date.Text, "N", strUpdatedBy, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                //else
                //{
                //    string strScript1 =
                //    "self.opener.document.forms[0].ctl00$ASPxRoundPanel1$ContentPlaceHolder2$txthpage.value=" + ViewState["page"].ToString() + ";\n" +
                //    "self.opener.document.forms[0].submit();\n" +
                //    "self.focus();\n";
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
                //}
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oPerson.Dispose();
            }
            BindGridView3();
        }

        #endregion


        #region gViewLoan Event

        private void BindGridViewLoan()
        {
            cPerson oPerson = new cPerson();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string strperson_code = string.Empty;
            string strYear = string.Empty;
            if (ViewState["mode"].ToString().ToLower().Equals("add"))
            {
                strCriteria = " And 1=2  ";
            }
            else
            {
                strperson_code = ViewState["person_code"].ToString();
                strYear = txtbudget_plan_year.Text;
                strCriteria = " And (person_code='" + strperson_code + "')  ";
                // strCriteria = " And (person_code='" + strperson_code + "')  And  (person_item_year='" + strYear + "')  ";
            }
            try
            {
                if (!oPerson.SP_PERSON_LOAN_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    ds.Tables[0].DefaultView.Sort = ViewState["sort3"] + " " + ViewState["direction3"];
                    gViewLoan.DataSource = ds.Tables[0];
                    gViewLoan.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                if (gViewLoan.Rows.Count == 0)
                {
                    EmptyGridFix(gViewLoan);
                }
                oPerson.Dispose();
                ds.Dispose();
            }
        }

        protected void gViewLoan_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.Header))
            {
                for (int iCol = 0; iCol < e.Row.Cells.Count; iCol++)
                {
                    e.Row.Cells[iCol].Attributes.Add("class", "table_h");
                    e.Row.Cells[iCol].Wrap = false;
                }
                ImageButton imgAdd = (ImageButton)e.Row.FindControl("imgAdd");
                imgAdd.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgGridAdd"].Rows[0]["img"].ToString();
                imgAdd.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgGridAdd"].Rows[0]["title"].ToString());

                imgAdd.Attributes.Add("onclick", "OpenPopUp('750px','330px','95%','เพิ่มข้อมูลเงินกู้บุคคลากร','person_loan_control.aspx?mode=add&person_code=" + txtperson_code.Text +
                                                             "&person_name=" + txtperson_thai_name.Text + "  " + txtperson_thai_surname.Text + "','2');return false;");
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

                Label lblloan_code = (Label)e.Row.FindControl("lblloan_code");
                Label lblloan_name = (Label)e.Row.FindControl("lblloan_name");
                Label lblloan_acc = (Label)e.Row.FindControl("lblloan_acc");

                #region set Image Edit & Delete

                ImageButton imgEdit = (ImageButton)e.Row.FindControl("imgEdit");
                imgEdit.Attributes.Add("onclick", "OpenPopUp('750px','330px','95%','แก้ไขข้อมูลเงินกู้บุคคลากร','person_loan_control.aspx?mode=edit&person_code=" +
                               txtperson_code.Text + "&person_name=" + txtperson_thai_name.Text + "  " + txtperson_thai_surname.Text + "&loan_code=" +
                               lblloan_code.Text + "&loan_acc=" + lblloan_acc.Text + "','2');return false;");

                Label lblCanEdit = (Label)e.Row.FindControl("lblCanEdit");
                imgEdit.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgEdit"].Rows[0]["img"].ToString();
                imgEdit.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgEdit"].Rows[0]["title"].ToString());

                ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                imgDelete.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["img"].ToString();
                imgDelete.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["title"].ToString());
                imgDelete.Attributes.Add("onclick", "return confirm(\"คุณต้องการลบข้อมูลเงินกู้บุคคลากร " + lblloan_code.Text + " : " + lblloan_name.Text + " ?\");");
                #endregion

            }
        }

        protected void gViewLoan_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.Header))
            {
                #region Create Item Header
                bool bSort = false;
                int i = 0;
                for (i = 0; i < gViewLoan.Columns.Count; i++)
                {
                    if (ViewState["sort3"].Equals(gViewLoan.Columns[i].SortExpression))
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
                            if (ViewState["direction3"].Equals("ASC"))
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

        protected void gViewLoan_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                if (ViewState["sort3"].ToString().Equals(e.SortExpression.ToString()))
                {
                    if (ViewState["direction3"].Equals("DESC"))
                        ViewState["direction3"] = "ASC";
                    else
                        ViewState["direction3"] = "DESC";
                }
                else
                {
                    ViewState["sort3"] = e.SortExpression;
                    ViewState["direction3"] = "ASC";
                }
                BindGridViewLoan();
                TabContainer1.ActiveTabIndex = 5;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        protected void gViewLoan_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string strMessage = string.Empty;
            string strCheck = string.Empty;
            string strScript = string.Empty;
            string strUpdatedBy = Session["username"].ToString();
            Label lblloan_code = (Label)gViewLoan.Rows[e.RowIndex].FindControl("lblloan_code");
            Label lblloan_acc = (Label)gViewLoan.Rows[e.RowIndex].FindControl("lblloan_acc");
            cPerson oPerson = new cPerson();
            try
            {
                if (!oPerson.SP_PERSON_LOAN_DEL(txtperson_code.Text, lblloan_code.Text, lblloan_acc.Text, ref strMessage))
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
                oPerson.Dispose();
            }
            BindGridViewLoan();
        }

        #endregion


        #region EmptyGridFix
        protected void EmptyGridFix(GridView grdView)
        {
            // normally executes after a grid load method
            if (grdView.Rows.Count == 0 &&
                grdView.DataSource != null)
            {
                DataTable dt = null;

                // need to clone sources otherwise it will be indirectly adding to 
                // the original source

                if (grdView.DataSource is DataSet)
                {
                    dt = ((DataSet)grdView.DataSource).Tables[0].Clone();
                }
                else if (grdView.DataSource is DataTable)
                {
                    dt = ((DataTable)grdView.DataSource).Clone();
                }

                if (dt == null)
                {
                    return;
                }

                dt.Rows.Add(dt.NewRow()); // add empty row
                grdView.DataSource = dt;
                grdView.DataBind();

                // hide row
                grdView.Rows[0].Visible = false;
                grdView.Rows[0].Controls.Clear();
            }

            // normally executes at all postbacks
            if (grdView.Rows.Count == 1 &&
                grdView.DataSource == null)
            {
                bool bIsGridEmpty = true;

                // check first row that all cells empty
                for (int i = 0; i < grdView.Rows[0].Cells.Count; i++)
                {
                    if (grdView.Rows[0].Cells[i].Text != string.Empty)
                    {
                        bIsGridEmpty = false;
                    }
                }
                // hide row
                if (bIsGridEmpty)
                {
                    grdView.Rows[0].Visible = false;
                    grdView.Rows[0].Controls.Clear();
                }
            }
        }
        #endregion

        protected void BtnR1_Click(object sender, EventArgs e)
        {
            BindGridView1();
        }

        protected void BtnR2_Click(object sender, EventArgs e)
        {
            BindGridView2();
        }

        protected void BtnR3_Click(object sender, EventArgs e)
        {
            BindGridView3();
        }

        public string getTitle(object str)
        {
            DataSet ods = new DataSet();
            string strError = string.Empty;
            string strReturn = string.Empty;
            cTitle oTitle = new cTitle();
            str = ChkStrNull(str);
            try
            {
                oTitle.SP_SEL_TITLE(" And  title_name='" + str.ToString().Trim() + "'", ref ods, ref strError);
                if (ods.Tables[0].Rows.Count > 0)
                {
                    strReturn = ods.Tables[0].Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "ไม่สามารถจัดการข้อมูล เนื่องจาก " + ex.Message;
            }
            return strReturn;
        }

        public string getPosition(object str)
        {
            DataSet ods = new DataSet();
            string strError = string.Empty;
            string strReturn = string.Empty;
            cPosition oPosition = new cPosition();
            str = ChkStrNull(str);
            try
            {
                oPosition.SP_POSITION_SEL(" And  position_name='" + str.ToString().Trim() + "'", ref ods, ref strError);
                if (ods.Tables[0].Rows.Count > 0)
                {
                    strReturn = ods.Tables[0].Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "ไม่สามารถจัดการข้อมูล เนื่องจาก " + ex.Message;
            }
            return strReturn;
        }

        public string getPerson_group(object str)
        {
            DataSet ods = new DataSet();
            string strError = string.Empty;
            string strReturn = string.Empty;
            cPerson_group oPerson_group = new cPerson_group();
            str = ChkStrNull(str);
            try
            {
                oPerson_group.SP_PERSON_GROUP_SEL(" And  person_group_name='" + str.ToString().Trim() + "'", ref ods, ref strError);
                if (ods.Tables[0].Rows.Count > 0)
                {
                    strReturn = ods.Tables[0].Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "ไม่สามารถจัดการข้อมูล เนื่องจาก " + ex.Message;
            }
            return strReturn;
        }

        public string getPerson_manage(object str)
        {
            DataSet ods = new DataSet();
            string strError = string.Empty;
            string strReturn = string.Empty;
            cPerson_manage oPerson_manage = new cPerson_manage();
            str = ChkStrNull(str);
            try
            {
                oPerson_manage.SP_PERSON_MANAGE_SEL(" And  person_manage_name='" + str.ToString().Trim() + "'", ref ods, ref strError);
                if (ods.Tables[0].Rows.Count > 0)
                {
                    strReturn = ods.Tables[0].Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "ไม่สามารถจัดการข้อมูล เนื่องจาก " + ex.Message;
            }
            return strReturn;
        }

        public string getSex(object str)
        {
            str = ChkStrNull(str);
            string strReturn = string.Empty;
            if (str == "ชาย")
            {
                strReturn = "M";
            }
            else
            {
                strReturn = "F";
            }
            return strReturn;
        }

        public string getMarry(object str)
        {
            str = ChkStrNull(str);
            string strReturn = string.Empty;
            if (str == "โสด")
            {
                strReturn = "1";
            }
            else if (str == "สมรส")
            {
                strReturn = "2";
            }
            else if (str == "หย่า")
            {
                strReturn = "3";
            }
            else if (str == "หม้าย")
            {
                strReturn = "4";
                return strReturn;
            }
            else
            {
                strReturn = "1";
            }
            return strReturn;
        }

        public string ChkStrNull(object str)
        {
            return str.ToString().Trim();
        }


        private void setDataCenter()
        {
            cPerson_center oPerson = new cPerson_center();
            cCommon oCommon = new cCommon();

            DataSet ds = new DataSet();
            DataSet dsPersonCode = new DataSet();
            string strMessage = string.Empty, strCriteria = string.Empty;
            #region clear Data
            //Tab 1 
            string strperson_code = string.Empty,
                strtitle_code = string.Empty,
                strperson_thai_name = string.Empty,
                strperson_thai_surname = string.Empty,
                strperson_eng_name = string.Empty,
                strperson_eng_surname = string.Empty,
                strperson_nickname = string.Empty,
                strperson_id = string.Empty,
                strperson_pic = string.Empty,
                strC_active = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty,
                strCreatedDate = string.Empty,
                strUpdatedDate = string.Empty,
                strBudget_type = string.Empty;
            //Tab 2 
            string strposition_code = string.Empty,
                strposition_name = string.Empty,

                strperson_level = string.Empty,
                strperson_level_name = string.Empty,
                strtype_position_code = string.Empty,
                strtype_position_name = string.Empty,

                strperson_postionno = string.Empty,
                strbranch_code = string.Empty,
                strbranch_name = string.Empty,
                strbank_name = string.Empty,
                strbank_no = string.Empty,
                strperson_salaly = string.Empty,
                strperson_group = string.Empty,
                strperson_start = string.Empty,
                strperson_end = string.Empty,
                strmember_type = string.Empty,
                strmember_type_add = "0",
                strperson_manage_code = string.Empty,
                strperson_manage_name = string.Empty,
                strbudget_plan_code = string.Empty,
                strbudget_name = string.Empty,
                strproduce_name = string.Empty,
                stractivity_name = string.Empty,
                strplan_name = string.Empty,
                strwork_name = string.Empty,
                strfund_name = string.Empty,
                strdirector_name = string.Empty,
                strunit_name = string.Empty,
                strbudget_plan_year = string.Empty,
                strperson_work_status = string.Empty;
            //Tab 3
            string strperson_sex = string.Empty,
                strperson_width = string.Empty,
                strperson_high = string.Empty,
                strperson_origin = string.Empty,
                strperson_nation = string.Empty,
                strperson_religion = string.Empty,
                strperson_birth = string.Empty,
                strperson_marry = string.Empty;
            //Tab 4
            string strperson_room = string.Empty,
                strperson_floor = string.Empty,
                strperson_village = string.Empty,
                strperson_homeno = string.Empty,
                strperson_soi = string.Empty,
                strperson_moo = string.Empty,
                strperson_road = string.Empty,
                strperson_tambol = string.Empty,
                strperson_aumphur = string.Empty,
                strperson_province = string.Empty,
                strperson_postno = string.Empty,
                strperson_tel = string.Empty,
                strperson_contact = string.Empty,
                strperson_ralation = string.Empty,
                strperson_contact_tel = string.Empty;
            #endregion
            try
            {
                strCriteria = " and CITIZEN_ID = '" + ViewState["person_code"].ToString() + "' ";
                if (!oPerson.SP_PERSON_CENTER_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region get Data
                        //Tab 1 
                        string strSQL = " SELECT dbo.FormatNumber(cast(MAX(person_code) as int)+1,5) as person_code FROM [person_his]";
                        oCommon.SEL_SQL(strSQL, ref dsPersonCode, ref strMessage);
                        if (dsPersonCode.Tables[0].Rows.Count > 0) strperson_code = dsPersonCode.Tables[0].Rows[0]["person_code"].ToString();
                        strtitle_code = getTitle(ds.Tables[0].Rows[0]["TITLE_NAME"]);
                        strperson_thai_name = ds.Tables[0].Rows[0]["STF_FNAME"].ToString();
                        strperson_thai_surname = ds.Tables[0].Rows[0]["STF_LNAME"].ToString();
                        strperson_eng_name = ds.Tables[0].Rows[0]["NAME_ENG"].ToString();
                        strperson_eng_surname = ds.Tables[0].Rows[0]["SURNAME_ENG"].ToString();
                        strperson_nickname = "";
                        strperson_id = ds.Tables[0].Rows[0]["CITIZEN_ID"].ToString();
                        strperson_pic = "";
                        strC_active = "Y";
                        strCreatedBy = "";
                        strUpdatedBy = "";
                        strCreatedDate = "";
                        strUpdatedDate = "";
                        //Tab 2 
                        strposition_code = getPosition(ds.Tables[0].Rows[0]["POSITION_WORK"]);
                        strposition_name = ds.Tables[0].Rows[0]["POSITION_WORK"].ToString();

                        //strperson_level = ds.Tables[0].Rows[0]["person_level"].ToString();
                        //strperson_level_name = ds.Tables[0].Rows[0]["level_position_name"].ToString();
                        //strtype_position_code = ds.Tables[0].Rows[0]["type_position_code"].ToString();
                        strtype_position_name = ds.Tables[0].Rows[0]["positionBlockLevelName"].ToString();

                        strperson_postionno = ds.Tables[0].Rows[0]["PCNO"].ToString();
                        //strbranch_code = ds.Tables[0].Rows[0]["branch_code"].ToString();
                        //strbranch_name = ds.Tables[0].Rows[0]["branch_name"].ToString();
                        // strbank_name = ds.Tables[0].Rows[0]["bank_name"].ToString();
                        // strbank_no = ds.Tables[0].Rows[0]["bank_no"].ToString();
                        strperson_salaly = ds.Tables[0].Rows[0]["SALARY"].ToString();
                        strperson_group = getPerson_group(ds.Tables[0].Rows[0]["GROUP_TYPE_NAME"]);
                        strperson_start = ds.Tables[0].Rows[0]["DATE_INWORK"].ToString();
                        strperson_end = ds.Tables[0].Rows[0]["DATE_RETIRE"].ToString();
                        // strmember_type = ds.Tables[0].Rows[0]["member_type_code"].ToString();
                        // strmember_type_add = ds.Tables[0].Rows[0]["member_type_add"].ToString();
                        strperson_manage_code = getPerson_manage(ds.Tables[0].Rows[0]["ADMIN_NAME"]);
                        strperson_manage_name = ds.Tables[0].Rows[0]["ADMIN_NAME"].ToString();
                        // strBudget_type = ds.Tables[0].Rows[0]["person_budget_type"].ToString();

                        //if (cboBudget_type.Items.FindByValue(strBudget_type) != null)
                        //{
                        //    cboBudget_type.SelectedIndex = -1;
                        //    cboBudget_type.Items.FindByValue(strBudget_type).Selected = true;
                        //}

                        // strbudget_plan_code = ds.Tables[0].Rows[0]["budget_plan_code"].ToString();
                        // strbudget_name = ds.Tables[0].Rows[0]["budget_name"].ToString();
                        // strproduce_name = ds.Tables[0].Rows[0]["produce_name"].ToString();
                        //  stractivity_name = ds.Tables[0].Rows[0]["activity_name"].ToString();
                        //  strplan_name = ds.Tables[0].Rows[0]["plan_name"].ToString();
                        //  strwork_name = ds.Tables[0].Rows[0]["work_name"].ToString();
                        //  strfund_name = ds.Tables[0].Rows[0]["fund_name"].ToString();
                        //  strdirector_name = ds.Tables[0].Rows[0]["director_name"].ToString();
                        //   strunit_name = ds.Tables[0].Rows[0]["unit_name"].ToString();
                        //   strbudget_plan_year = ds.Tables[0].Rows[0]["budget_plan_year"].ToString();
                        strperson_work_status = "01";



                        //Tab 3
                        strperson_sex = getSex(ds.Tables[0].Rows[0]["GENDER_NAME"]);
                        // strperson_width = ds.Tables[0].Rows[0]["person_width"].ToString();
                        // strperson_high = ds.Tables[0].Rows[0]["person_high"].ToString();
                        // strperson_origin = ds.Tables[0].Rows[0]["person_origin"].ToString();
                        // strperson_nation = ds.Tables[0].Rows[0]["person_nation"].ToString();
                        // strperson_religion = ds.Tables[0].Rows[0]["person_religion"].ToString();
                        strperson_birth = ds.Tables[0].Rows[0]["BIRTHDAY"].ToString();
                        strperson_marry = getMarry(ds.Tables[0].Rows[0]["MARRIED_NAME"]);
                        //Tab 4
                        // strperson_room = ds.Tables[0].Rows[0]["person_room"].ToString();
                        //  strperson_floor = ds.Tables[0].Rows[0]["person_floor"].ToString();
                        // strperson_village = ds.Tables[0].Rows[0]["person_village"].ToString();
                        strperson_homeno = ds.Tables[0].Rows[0]["HOMEADD"].ToString();
                        strperson_soi = ds.Tables[0].Rows[0]["SOI"].ToString();
                        strperson_moo = ds.Tables[0].Rows[0]["MOO"].ToString();
                        strperson_road = ds.Tables[0].Rows[0]["STREET"].ToString();
                        strperson_tambol = ds.Tables[0].Rows[0]["DISTRICT"].ToString();
                        strperson_aumphur = ds.Tables[0].Rows[0]["AMPHUR"].ToString();
                        strperson_province = ds.Tables[0].Rows[0]["PROVINCE_NAME_TH"].ToString();
                        strperson_postno = ds.Tables[0].Rows[0]["ZIPCODE"].ToString();
                        //strperson_tel = ds.Tables[0].Rows[0]["person_tel"].ToString();
                        // strperson_contact = ds.Tables[0].Rows[0]["person_contact"].ToString();
                        //strperson_ralation = ds.Tables[0].Rows[0]["person_ralation"].ToString();
                        // strperson_contact_tel = ds.Tables[0].Rows[0]["person_contact_tel"].ToString();

                        strBudget_type = "B";


                        #endregion

                        #region set Control
                        TabContainer1.Tabs[1].Visible = true;
                        TabContainer1.Tabs[2].Visible = true;
                        TabContainer1.Tabs[3].Visible = true;
                        TabContainer1.Tabs[4].Visible = false;
                        TabContainer1.Tabs[5].Visible = false;
                        TabContainer1.Tabs[6].Visible = false;
                        //Tab 1 
                        txtperson_code.Text = strperson_code;
                        Session["person_code"] = strperson_code;
                        InitcboTitle();
                        if (cboTitle.Items.FindByValue(strtitle_code) != null)
                        {
                            cboTitle.SelectedIndex = -1;
                            cboTitle.Items.FindByValue(strtitle_code).Selected = true;
                        }
                        txtperson_thai_name.Text = strperson_thai_name;
                        txtperson_thai_surname.Text = strperson_thai_surname;
                        txtperson_eng_name.Text = strperson_eng_name;
                        txtperson_eng_surname.Text = strperson_eng_surname;
                        txtperson_nickname.Text = strperson_nickname;
                        txtperson_id.Text = strperson_id;
                        txtperson_pic.Text = strperson_pic;
                        if (strperson_pic.Length != 0)
                        {
                            imgPerson.ImageUrl = "../../person_pic/" + strperson_pic;
                        }
                        else
                        {
                            imgPerson.ImageUrl = "../../person_pic/image_n_a.jpg";
                        }
                        txtUpdatedBy.Text = strUpdatedBy;
                        txtUpdatedDate.Text = strUpdatedDate;

                        if (strC_active.Equals("Y"))
                        {
                            chkStatus.Checked = true;
                        }
                        else
                        {
                            chkStatus.Checked = false;
                        }


                        //Tab 2 

                        InitcboBudgetType();
                        if (cboBudget_type.Items.FindByValue(strBudget_type) != null)
                        {
                            cboBudget_type.SelectedIndex = -1;
                            cboBudget_type.Items.FindByValue(strBudget_type).Selected = true;
                        }
                        ChangeLabelBudget();

                        txtposition_code.Text = strposition_code;
                        txtposition_name.Text = strposition_name;

                        txtperson_level.Text = strperson_level;
                        txtlevel_position_name.Text = strperson_level_name;
                        txttype_position_code.Text = strtype_position_code;
                        txttype_position_name.Text = strtype_position_name;

                        txtperson_postionno.Text = strperson_postionno;
                        txtbranch_code.Text = strbranch_code;
                        txtbranch_name.Text = strbranch_name;
                        txtbank_name.Text = strbank_name;
                        txtbank_no.Text = strbank_no;
                        try
                        {
                            txtperson_salaly.Text = String.Format("{0:0.00}", decimal.Parse(strperson_salaly));
                        }
                        catch { }
                        InitcboPerson_group();
                        if (cboPerson_group.Items.FindByValue(strperson_group) != null)
                        {
                            cboPerson_group.SelectedIndex = -1;
                            cboPerson_group.Items.FindByValue(strperson_group).Selected = true;
                        }
                        try
                        {
                            txtperson_start.Text = cCommon.CheckDate(strperson_start);
                        }
                        catch { }
                        try
                        {
                            txtperson_end.Text = cCommon.CheckDate(strperson_end);
                        }
                        catch { }
                        InitcboMember_type();
                        foreach (ListItem item in cboMember_type_checkboxes.Items)
                        {
                            item.Selected = strmember_type.Contains(item.Value);
                        }


                        try
                        {
                            txtmember_type_add.Text = String.Format("{0:0.00}", decimal.Parse(strmember_type_add));
                        }
                        catch { }
                        txtperson_manage_code.Text = strperson_manage_code;
                        txtperson_manage_name.Text = strperson_manage_name;
                        txtbudget_plan_code.Text = strbudget_plan_code;
                        txtbudget_name.Text = strbudget_name;
                        txtproduce_name.Text = strproduce_name;
                        txtactivity_name.Text = stractivity_name;
                        txtplan_name.Text = strplan_name;
                        txtwork_name.Text = strwork_name;
                        txtfund_name.Text = strfund_name;
                        txtdirector_name.Text = strdirector_name;
                        txtunit_name.Text = strunit_name;
                        txtbudget_plan_year.Text = strbudget_plan_year;
                        InitcboPerson_work_status();
                        if (cboPerson_work_status.Items.FindByValue(strperson_work_status) != null)
                        {
                            cboPerson_work_status.SelectedIndex = -1;
                            cboPerson_work_status.Items.FindByValue(strperson_work_status).Selected = true;
                        }



                        //Tab 3
                        if (cboPerson_sex.Items.FindByValue(strperson_sex) != null)
                        {
                            cboPerson_sex.SelectedIndex = -1;
                            cboPerson_sex.Items.FindByValue(strperson_sex).Selected = true;
                        }
                        txtperson_width.Text = strperson_width;
                        txtperson_high.Text = strperson_high;
                        txtperson_origin.Text = strperson_origin;
                        txtperson_nation.Text = strperson_nation;
                        txtperson_religion.Text = strperson_religion;
                        try
                        {
                            txtperson_birth.Text = cCommon.CheckDate(strperson_birth);
                        }
                        catch { }
                        if (cboPerson_marry.Items.FindByValue(strperson_marry) != null)
                        {
                            cboPerson_marry.SelectedIndex = -1;
                            cboPerson_marry.Items.FindByValue(strperson_marry).Selected = true;
                        }
                        //Tab 4
                        txtperson_room.Text = strperson_room;
                        txtperson_floor.Text = strperson_floor;
                        txtperson_village.Text = strperson_village;
                        txtperson_homeno.Text = strperson_homeno;
                        txtperson_soi.Text = strperson_soi;
                        txtperson_moo.Text = strperson_moo;
                        txtperson_road.Text = strperson_road;
                        txtperson_tambol.Text = strperson_tambol;
                        txtperson_aumphur.Text = strperson_aumphur;
                        txtperson_province.Text = strperson_province;
                        txtperson_postno.Text = strperson_postno;
                        txtperson_tel.Text = strperson_tel;
                        txtperson_contact.Text = strperson_contact;
                        txtperson_ralation.Text = strperson_ralation;
                        txtperson_contact_tel.Text = strperson_contact_tel;
                        try
                        {
                            DateTime dperson_birth = DateTime.Parse(txtperson_birth.Text);
                            long intperson_birth = cCommon.DateTimeUtil.DateDiff(cCommon.DateInterval.Year, dperson_birth.Date, DateTime.Now.Date);
                            lblAge.Text = "อายุปัจจุบัน  " + intperson_birth.ToString() + "  ปี";
                        }
                        catch { }
                        // BindGridView1();
                        // BindGridView2();
                        // BindGridView3();
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        protected void BtnR4_Click(object sender, EventArgs e)
        {
            BindGridViewLoan();
        }

        protected void cboPerson_group_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboMember_type();
        }

    }
}