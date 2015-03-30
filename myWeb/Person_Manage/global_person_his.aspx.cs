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

namespace myWeb.Person_Manage
{
    public partial class global_person_his : GlobalPageBase
    {
        #region private data
        private string strConn = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"];
        private bool[] blnAccessRight = new bool[5] { false, false, false, false, false };
        private string strPrefixCtr_main = "ctl00$ContentPlaceHolder1$TabContainer1$";
        #endregion
        public static string getNumber(object pNumber)
        {
            if (!pNumber.ToString().Equals(""))
            {
                string strNumber = String.Format("{0:#,##0.00}", float.Parse(pNumber.ToString()));
                return strNumber;
            }
            return "";
        }
     
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
            AjaxPro.Utility.RegisterTypeForAjax(typeof(global_person_his));
            lblError.Text = "";
            if (!IsPostBack)
            {
                lblAge.Text = string.Empty;
                ViewState["sort"] = "item_type";
                ViewState["direction"] = "DESC";
                ViewState["sort1"] = "member_code";
                ViewState["direction1"] = "ASC";
                ViewState["sort2"] = "change_date";
                ViewState["direction2"] = "ASC";
                TabContainer1.ActiveTabIndex = 0;
                InitcboRound();
                setData();

                //InitcboBudgetType();
            }

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
            strmember_type = cboMember_type.SelectedValue;
            strperson_group_code = cboPerson_group.SelectedValue;
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
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            if (oMember_type.SP_MEMBER_TYPE_SEL(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboMember_type.Items.Clear();
                cboMember_type.Items.Add(new ListItem("N", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboMember_type.Items.Add(new ListItem(dt.Rows[i]["member_type_name"].ToString(), dt.Rows[i]["member_type_code"].ToString()));
                }
                if (cboMember_type.Items.FindByValue(strmember_type) != null)
                {
                    cboMember_type.SelectedIndex = -1;
                    cboMember_type.Items.FindByValue(strmember_type).Selected = true;
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

        private void InitcboRound()
        {
            cPayment_round oPayment_round = new cPayment_round();
            DataSet ds = new DataSet();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strYear = string.Empty;
            string strPay_Month = string.Empty;
            string strPay_Year = string.Empty;
            try
            {
                strCriteria = " and round_status= 'O' ";
                if (!oPayment_round.SP_PAYMENT_ROUND_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region get Data
                        strYear = ds.Tables[0].Rows[0]["payment_year"].ToString();
                        strPay_Month = ds.Tables[0].Rows[0]["pay_month"].ToString();
                        strPay_Year = ds.Tables[0].Rows[0]["pay_year"].ToString();
                        #endregion
                    }
                    else
                    {
                        #region get Data
                        strYear = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["yearnow"].ToString();
                        if (DateTime.Now.Year < 2200)
                        {
                            strPay_Year = (DateTime.Now.Year + 543).ToString();
                        }
                        if (DateTime.Now.Month < 10)
                            strPay_Month = "0" + DateTime.Now.Month;
                        else
                            strPay_Month = DateTime.Now.Month.ToString();
                        #endregion
                    }

                    #region set Control
                    InitcboPay_Month();
                    if (cboPay_Month.Items.FindByValue(strPay_Month) != null)
                    {
                        cboPay_Month.SelectedIndex = -1;
                        cboPay_Month.Items.FindByValue(strPay_Month).Selected = true;
                    }

                    InitcboPay_Year();
                    if (cboPay_Year.Items.FindByValue(strPay_Year) != null)
                    {
                        cboPay_Year.SelectedIndex = -1;
                        cboPay_Year.Items.FindByValue(strPay_Year).Selected = true;
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
                oPayment_round.Dispose();
            }
        }

        private void InitcboPay_Month()
        {
            string strMonth = string.Empty;
            strMonth = cboPay_Month.SelectedValue;
            if (strMonth.Equals(""))
            {
                if (DateTime.Now.Month < 10)
                {
                    strMonth = "0" + DateTime.Now.Month.ToString();
                }
                else
                {
                    strMonth = DateTime.Now.Month.ToString();
                }
            }
            DataTable odt;
            int i;
            cboPay_Month.Items.Clear();
            odt = ((DataSet)Application["xmlconfig"]).Tables["cboMonth"];
            for (i = 0; i <= odt.Rows.Count - 1; i++)
            {
                cboPay_Month.Items.Add(new ListItem(odt.Rows[i]["Text"].ToString(), odt.Rows[i]["Value"].ToString()));
            }
            if (cboPay_Month.Items.FindByValue(strMonth) != null)
            {
                cboPay_Month.SelectedIndex = -1;
                cboPay_Month.Items.FindByValue(strMonth).Selected = true;
            }
        }

        private void InitcboPay_Year()
        {
            string strYear = string.Empty;
            strYear = cboPay_Year.SelectedValue;
            if (strYear.Equals(""))
            {
                if (DateTime.Now.Year < 2200)
                {
                    strYear = (DateTime.Now.Year + 543).ToString();
                }
                else
                {
                    strYear = DateTime.Now.Year.ToString();
                }
            }
            DataTable odt;
            int i;
            cboPay_Year.Items.Clear();
            odt = ((DataSet)Application["xmlconfig"]).Tables["cboYear"];
            for (i = 0; i <= odt.Rows.Count - 1; i++)
            {
                cboPay_Year.Items.Add(new ListItem(odt.Rows[i]["Text"].ToString(), odt.Rows[i]["Value"].ToString()));
            }
            if (cboPay_Year.Items.FindByValue(strYear) != null)
            {
                cboPay_Year.SelectedIndex = -1;
                cboPay_Year.Items.FindByValue(strYear).Selected = true;
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
            //this.imgSaveOnly.Click += new System.Web.UI.ImageClickEventHandler(this.imgSaveOnly_Click);
            //this.imgSave.Click += new System.Web.UI.ImageClickEventHandler(this.imgSave_Click);

        }
        #endregion

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
                strperson_salaly = string.Empty,
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
                strCriteria = " and person_code = '" + base.PersonCode + "' ";
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
                        strperson_salaly = ds.Tables[0].Rows[0]["person_salaly"].ToString();
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
                            imgPerson.ImageUrl = "../person_pic/" + strperson_pic;
                        }
                        else
                        {
                            imgPerson.ImageUrl = "../person_pic/image_n_a.jpg";
                        }

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
                        txtperson_salaly.Text = String.Format("{0:0.00}", float.Parse(strperson_salaly));
                        InitcboPerson_group();
                        if (cboPerson_group.Items.FindByValue(strperson_group) != null)
                        {
                            cboPerson_group.SelectedIndex = -1;
                            cboPerson_group.Items.FindByValue(strperson_group).Selected = true;
                        }
                        txtperson_start.Text = cCommon.CheckDate(strperson_start);
                        txtperson_end.Text = cCommon.CheckDate(strperson_end);
                        InitcboMember_type();
                        if (cboMember_type.Items.FindByValue(strmember_type) != null)
                        {
                            cboMember_type.SelectedIndex = -1;
                            cboMember_type.Items.FindByValue(strmember_type).Selected = true;
                        }

                        txtmember_type_add.Text = String.Format("{0:0.00}", float.Parse(strmember_type_add));
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
                            DateTime dperson_birth = DateTime.Parse(txtperson_birth.Text);
                            long intperson_birth = cCommon.DateTimeUtil.DateDiff(cCommon.DateInterval.Year, dperson_birth.Date, DateTime.Now.Date);
                            lblAge.Text = "อายุปัจจุบัน  " + intperson_birth.ToString() + "  ปี";
                        }
                        catch
                        {
                        }
                        BindGridView1();
                        BindGridView2();
                        // BindGridView3();
                        BindGridView4();
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
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
            strperson_code = base.PersonCode;
            strCriteria = " And (person_code='" + strperson_code + "')  ";
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
                    lblitem_debit.Text = String.Format("{0:#,##0.00}", float.Parse(lblitem_debit.Text));
                }
                if (!lblitem_credit.Text.Equals(""))
                {
                    lblitem_credit.Text = String.Format("{0:#,##0.00}", float.Parse(lblitem_credit.Text));
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
                    imgStatus.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgStatus"].Rows[0]["img"].ToString().Substring(3);
                    imgStatus.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgStatus"].Rows[0]["title"].ToString());
                    imgStatus.Attributes.Add("onclick", "return false;");
                }
                else
                {
                    imgStatus.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgStatus"].Rows[0]["imgdisable"].ToString().Substring(3);
                    imgStatus.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgStatus"].Rows[0]["titledisable"].ToString());
                    imgStatus.Attributes.Add("onclick", "return false;");
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
                                ((LinkButton)c).Text += "<img border=0 src='../images/controls/tridown.gif'>";
                            }
                            else
                            {
                                ((LinkButton)c).Text += "<img border=0 src='../images/controls/triup.gif'>";
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

            strperson_code = base.PersonCode;
            strCriteria = " And (person_code='" + strperson_code + "')  ";

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
                Label lblc_active = (Label)e.Row.FindControl("lblc_active1");
                string strStatus = lblc_active.Text;

                #region set ImageStatus
                ImageButton imgStatus = (ImageButton)e.Row.FindControl("imgStatus1");
                if (strStatus.Equals("Y"))
                {
                    imgStatus.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgStatus"].Rows[0]["img"].ToString().Substring(3);
                    imgStatus.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgStatus"].Rows[0]["title"].ToString());
                    imgStatus.Attributes.Add("onclick", "return false;");
                }
                else
                {
                    imgStatus.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgStatus"].Rows[0]["imgdisable"].ToString().Substring(3);
                    imgStatus.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgStatus"].Rows[0]["titledisable"].ToString());
                    imgStatus.Attributes.Add("onclick", "return false;");
                }
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
                                ((LinkButton)c).Text += "<img border=0 src='../images/controls/tridown.gif'>";
                            }
                            else
                            {
                                ((LinkButton)c).Text += "<img border=0 src='../images/controls/triup.gif'>";
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
                TabContainer1.ActiveTabIndex = 5;
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

        //private void BindGridView3()
        //{
        //    cPerson oPerson = new cPerson();
        //    DataSet ds = new DataSet();
        //    string strMessage = string.Empty;
        //    string strCriteria = string.Empty;
        //    string strperson_code = string.Empty;

        //    strperson_code = base.PersonCode;
        //    strCriteria = " And (person_code='" + strperson_code + "')  ";

        //    try
        //    {
        //        if (!oPerson.SP_PERSON_POSITION_SEL(strCriteria, ref ds, ref strMessage))
        //        {
        //            lblError.Text = strMessage;
        //        }
        //        else
        //        {
        //            ds.Tables[0].DefaultView.Sort = ViewState["sort2"] + " " + ViewState["direction2"];
        //            GridView3.DataSource = ds.Tables[0];
        //            GridView3.DataBind();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblError.Text = ex.Message.ToString();
        //    }
        //    finally
        //    {
        //        if (GridView3.Rows.Count == 0)
        //        {
        //            EmptyGridFix(GridView3);
        //        }
        //        oPerson.Dispose();
        //        ds.Dispose();
        //    }
        //}

        //protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType.Equals(DataControlRowType.Header))
        //    {
        //        for (int iCol = 0; iCol < e.Row.Cells.Count; iCol++)
        //        {
        //            e.Row.Cells[iCol].Attributes.Add("class", "table_h");
        //            e.Row.Cells[iCol].Wrap = false;
        //        }
        //    }
        //    else if (e.Row.RowType.Equals(DataControlRowType.DataRow) || e.Row.RowState.Equals(DataControlRowState.Alternate))
        //    {
        //        #region Set datagrid row color
        //        string strEvenColor, strOddColor, strMouseOverColor;
        //        strEvenColor = ((DataSet)Application["xmlconfig"]).Tables["colorDataGridRow"].Rows[0]["Even"].ToString();
        //        strOddColor = ((DataSet)Application["xmlconfig"]).Tables["colorDataGridRow"].Rows[0]["Odd"].ToString();
        //        strMouseOverColor = ((DataSet)Application["xmlconfig"]).Tables["colorDataGridRow"].Rows[0]["MouseOver"].ToString();

        //        e.Row.Style.Add("valign", "top");
        //        e.Row.Style.Add("cursor", "hand");
        //        e.Row.Attributes.Add("onMouseOver", "this.style.backgroundColor='" + strMouseOverColor + "'");

        //        if (e.Row.RowState.Equals(DataControlRowState.Alternate))
        //        {
        //            e.Row.Attributes.Add("bgcolor", strOddColor);
        //            e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor='" + strOddColor + "'");
        //        }
        //        else
        //        {
        //            e.Row.Attributes.Add("bgcolor", strEvenColor);
        //            e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor='" + strEvenColor + "'");
        //        }
        //        #endregion
        //        Label lblNo = (Label)e.Row.FindControl("lblNo2");
        //        int nNo = e.Row.RowIndex + 1;
        //        lblNo.Text = nNo.ToString();
        //        Label lblchange_date = (Label)e.Row.FindControl("lblchange_date");
        //        Label lblsalary_old = (Label)e.Row.FindControl("lblsalary_old");
        //        Label lblsalary_new = (Label)e.Row.FindControl("lblsalary_new");
        //        if (!lblchange_date.Text.Equals(""))
        //        {
        //            lblchange_date.Text = cCommon.CheckDate(lblchange_date.Text);
        //            lblsalary_old.Text = String.Format("{0:#,##0.00}", float.Parse(lblsalary_old.Text));
        //            lblsalary_new.Text = String.Format("{0:#,##0.00}", float.Parse(lblsalary_new.Text));
        //        }
        //        Label lblc_active = (Label)e.Row.FindControl("lblc_active2");
        //        string strStatus = lblc_active.Text;

        //        #region set ImageStatus
        //        ImageButton imgStatus = (ImageButton)e.Row.FindControl("imgStatus2");
        //        if (strStatus.Equals("Y"))
        //        {
        //            imgStatus.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgStatus"].Rows[0]["img"].ToString();
        //            imgStatus.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgStatus"].Rows[0]["title"].ToString());
        //            imgStatus.Attributes.Add("onclick", "return false;");
        //        }
        //        else
        //        {
        //            imgStatus.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgStatus"].Rows[0]["imgdisable"].ToString();
        //            imgStatus.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgStatus"].Rows[0]["titledisable"].ToString());
        //            imgStatus.Attributes.Add("onclick", "return false;");
        //        }
        //        #endregion

        //        #region set Image Edit & Delete

        //        ImageButton imgEdit = (ImageButton)e.Row.FindControl("imgEdit2");
        //        //Label lblCanEdit = (Label)e.Row.FindControl("lblCanEdit2");
        //        imgEdit.Attributes.Add("onclick", "OpenPopUp('800px','340px','91%','แก้ไขข้อมูลประวัติตำแหน่งบุคคลากร','person_position_control.aspx?mode=edit&person_code=" +
        //                                      txtperson_code.Text + "&person_name=" + txtperson_thai_name.Text + "  " + txtperson_thai_surname.Text + "&change_date=" +
        //                                      lblchange_date.Text + "','2');return false;");
        //        imgEdit.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgEdit"].Rows[0]["img"].ToString();
        //        imgEdit.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgEdit"].Rows[0]["title"].ToString());

        //        ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete2");
        //        imgDelete.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["img"].ToString();
        //        imgDelete.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["title"].ToString());
        //        imgDelete.Attributes.Add("onclick", "return confirm(\"คุณต้องการลบข้อมูลประวัติตำแหน่งบุคคลากรนี้ ?\");");
        //        #endregion

        //    }
        //}

        //protected void GridView3_RowCreated(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType.Equals(DataControlRowType.Header))
        //    {
        //        #region Create Item Header
        //        bool bSort = false;
        //        int i = 0;
        //        for (i = 0; i < GridView3.Columns.Count; i++)
        //        {
        //            if (ViewState["sort2"].Equals(GridView3.Columns[i].SortExpression))
        //            {
        //                bSort = true;
        //                break;
        //            }
        //        }
        //        if (bSort)
        //        {
        //            foreach (System.Web.UI.Control c in e.Row.Controls[i].Controls)
        //            {
        //                if (c.GetType().ToString().Equals("System.Web.UI.WebControls.DataControlLinkButton"))
        //                {
        //                    if (ViewState["direction2"].Equals("ASC"))
        //                    {
        //                        ((LinkButton)c).Text += "<img border=0 src='../images/controls/tridown.gif'>";
        //                    }
        //                    else
        //                    {
        //                        ((LinkButton)c).Text += "<img border=0 src='../images/controls/triup.gif'>";
        //                    }
        //                }
        //            }
        //        }
        //        #endregion
        //    }
        //}

        //protected void GridView3_Sorting(object sender, GridViewSortEventArgs e)
        //{
        //    try
        //    {
        //        if (ViewState["sort2"].ToString().Equals(e.SortExpression.ToString()))
        //        {
        //            if (ViewState["direction2"].Equals("DESC"))
        //                ViewState["direction2"] = "ASC";
        //            else
        //                ViewState["direction2"] = "DESC";
        //        }
        //        else
        //        {
        //            ViewState["sort2"] = e.SortExpression;
        //            ViewState["direction2"] = "ASC";
        //        }
        //        BindGridView3();
        //        TabContainer1.ActiveTabIndex = 6;
        //    }
        //    catch (Exception ex)
        //    {
        //        lblError.Text = ex.Message.ToString();
        //    }
        //}

        //protected void GridView3_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    string strMessage = string.Empty;
        //    string strCheck = string.Empty;
        //    string strScript = string.Empty;
        //    string strUpdatedBy = Session["username"].ToString();
        //    Label lblchange_date = (Label)GridView3.Rows[e.RowIndex].FindControl("lblchange_date");
        //    cPerson oPerson = new cPerson();
        //    try
        //    {
        //        if (!oPerson.SP_PERSON_POSITION_DEL(txtperson_code.Text, lblchange_date.Text, "N", strUpdatedBy, ref strMessage))
        //        {
        //            lblError.Text = strMessage;
        //        }
        //        //else
        //        //{
        //        //    string strScript1 =
        //        //    "self.opener.document.forms[0].ctl00$ASPxRoundPanel1$ContentPlaceHolder2$txthpage.value=" + ViewState["page"].ToString() + ";\n" +
        //        //    "self.opener.document.forms[0].submit();\n" +
        //        //    "self.focus();\n";
        //        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        lblError.Text = ex.Message.ToString();
        //    }
        //    finally
        //    {
        //        oPerson.Dispose();
        //    }
        //    BindGridView3();
        //}

        #endregion

        #region GridView4 Event
        protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.Header))
            {
                for (int iCol = 0; iCol < e.Row.Cells.Count; iCol++)
                {
                    e.Row.Cells[iCol].Attributes.Add("class", "table_h");
                    e.Row.Cells[iCol].Wrap = false;
                }
                txttotal_recv.Value = 0;
                txttotal_pay.Value = 0;
                txttotal_all.Value = 0;
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
                Label lblitem_code = (Label)e.Row.FindControl("lblitem_code");
                Label lblc_active = (Label)e.Row.FindControl("lblc_active");
                Label lblpayment_detail_lot_code = (Label)e.Row.FindControl("lblpayment_detail_lot_code");
                Label lblpayment_detail_lot_name = (Label)e.Row.FindControl("lblpayment_detail_lot_name");
                
                Label lblbudget_type = (Label)e.Row.FindControl("lblbudget_type");
                HiddenField hddbudget_type = (HiddenField)e.Row.FindControl("hddbudget_type");
                if (lblbudget_type.Text.Equals("R"))
                {
                    lblbudget_type.Text = "เงินรายได้";
                }
                else if (lblbudget_type.Text.Equals("B"))
                {
                    lblbudget_type.Text = "เงินงบประมาณ";
                }

                int nNo = (GridView4.PageSize * GridView4.PageIndex) + e.Row.RowIndex + 1;
                lblNo.Text = nNo.ToString();


            

                string strStatus = lblc_active.Text;
                DataRowView dv = (DataRowView)e.Row.DataItem;
                txttotal_recv.Value = Helper.CDbl(txttotal_recv.Value) + Helper.CDbl(dv["payment_item_recv"]);
                txttotal_pay.Value = Helper.CDbl(txttotal_pay.Value) + Helper.CDbl(dv["payment_item_pay"]);
                
                DataSet ds = new DataSet();
                cLot objLot = new cLot();
                string strMessage = string.Empty;
                objLot.SP_SEL_LOT(" And  lot_code = '" + lblpayment_detail_lot_code.Text + "'", ref ds, ref strMessage);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblpayment_detail_lot_name.Text = ds.Tables[0].Rows[0]["lot_name"].ToString();
                }
                ds.Dispose();
                objLot.Dispose();

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

            }
            else if (e.Row.RowType.Equals(DataControlRowType.Footer))
            {
                txttotal_all.Value = Helper.CDbl(txttotal_recv.Value) - Helper.CDbl(txttotal_pay.Value);
            }
        }

        protected void GridView4_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.Header))
            {
                #region Create Item Header
                bool bSort = false;
                int i = 0;
                for (i = 0; i < GridView4.Columns.Count; i++)
                {
                    if (ViewState["sort"].Equals(GridView4.Columns[i].SortExpression))
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

        private void BindGridView4()
        {
            cPayment oPayment = new cPayment();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string strunit_code = string.Empty;
            try
            {

                strCriteria = " and pay_year = '" + cboPay_Year.SelectedValue + "' and pay_month = '" + cboPay_Month.SelectedValue + "' " +
                    " and person_code = '" + base.PersonCode + "' and (payment_item_recv > 0 or payment_item_pay > 0) ";
                    //" and person_code = '" + base.PersonCode + "' ";

                if (!oPayment.SP_PAYMENT_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    //ds.Tables[0].DefaultView.Sort = ViewState["sort"] + " " + ViewState["direction"];
                    GridView4.DataSource = ds.Tables[0];
                    GridView4.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                if (GridView4.Rows.Count == 0)
                {
                    EmptyGridFix(GridView4);
                }
                oPayment.Dispose();
                ds.Dispose();
            }
        }

        #endregion


        protected void cboPay_Year_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboPay_Month();
            BindGridView4();
        }

        protected void cboPay_Month_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridView4();
        }


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

        public void DisibleTextBox(Control control)
        {
            foreach (Control ctrl in control.Controls)
            {
                if (ctrl is TextBox)
                {
                    ((TextBox)ctrl).ReadOnly = true;
                }
                else if (ctrl is DropDownList)
                {
                    ((DropDownList)ctrl).Enabled = false;
                }
            }
        }





    }
}