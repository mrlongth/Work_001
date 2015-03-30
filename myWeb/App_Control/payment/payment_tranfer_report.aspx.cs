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
using myDLL;

namespace myWeb.App_Control.payment
{
    public partial class payment_tranfer_report : PageBase
    {
        #region private data
        private bool[] blnAccessRight = new bool[5] { false, false, false, false, false };
        private string strPrefixCtr = "ctl00$ASPxRoundPanel1$ASPxRoundPanel2$ContentPlaceHolder1$";
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                imgList_budget_plan_sr.Attributes.Add("onclick", "OpenPopUp('950px','500px','94%','ค้นหาข้อมูลผังงบประมาณประจำปี','../lov/budget_plan_lov.aspx?" +
                                                "budget_plan_code='+getElementById('" + txtbudget_plan_code_sr.ClientID + "').value+'" +
                                                "&ctrl1=" + txtbudget_plan_code_sr.ClientID + "&show=1', '1');return false;");
                imgClear_budget_plan_sr.Attributes.Add("onclick", "getElementById('" + txtbudget_plan_code_sr.ClientID + "').value='';return false;");

                imgList_budget_plan_ds.Attributes.Add("onclick", "OpenPopUp('950px','500px','94%','ค้นหาข้อมูลผังงบประมาณประจำปี','../lov/budget_plan_lov.aspx?" +
                                "budget_plan_code='+getElementById('" + txtbudget_plan_code_ds.ClientID + "').value+'" +
                                "&ctrl1=" + txtbudget_plan_code_ds.ClientID + "', '1');return false;");
                imgClear_budget_plan_ds.Attributes.Add("onclick", "getElementById('" + txtbudget_plan_code_ds.ClientID + "').value='';return false;");

                InitcboRound();


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
            
            InitcboDirector_sr();
            InitcboDirector_ds();
            InitcboLot_sr();
            InitcboLot_ds();
            InitcboItem_group_sr();
            InitcboItem_group_ds();
        
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

        private void InitcboDirector_sr()
        {
            cDirector oDirector = new cDirector();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strDirector_code_sr = string.Empty;
            string strYear = cboYear.SelectedValue;
            strDirector_code_sr = cboDirector_sr.SelectedValue;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and director_year = '" + strYear + "'  and  c_active='Y' ";
            if (DirectorLock == "Y")
            {
                strCriteria += " and substring(director_code,4,2) = substring('" + DirectorCode + "',4,2) ";
            }
            if (oDirector.SP_SEL_DIRECTOR(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboDirector_sr.Items.Clear();
                cboDirector_sr.Items.Add(new ListItem("---- เลือกข้อมูลทั้งหมด ----", ""));

                int i;
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboDirector_sr.Items.Add(new ListItem(dt.Rows[i]["director_name"].ToString(), dt.Rows[i]["director_code"].ToString()));
                }
                if (cboDirector_sr.Items.FindByValue(strDirector_code_sr) != null)
                {
                    cboDirector_sr.SelectedIndex = -1;
                    cboDirector_sr.Items.FindByValue(strDirector_code_sr).Selected = true;
                }
                InitcboUnit_sr();
            }
        }

        private void InitcboDirector_ds()
        {
            cDirector oDirector = new cDirector();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strDirector_code_ds = string.Empty;
            string strYear = cboYear.SelectedValue;
            strDirector_code_ds = cboDirector_ds.SelectedValue;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and director_year = '" + strYear + "'  and  c_active='Y' ";
            if (DirectorLock == "Y")
            {
                strCriteria += " and substring(director_code,4,2) = substring('" + DirectorCode + "',4,2) ";
            }
            if (oDirector.SP_SEL_DIRECTOR(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboDirector_ds.Items.Clear();
                cboDirector_ds.Items.Add(new ListItem("---- เลือกข้อมูลทั้งหมด ----", ""));

                int i;
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboDirector_ds.Items.Add(new ListItem(dt.Rows[i]["director_name"].ToString(), dt.Rows[i]["director_code"].ToString()));
                }
                if (cboDirector_ds.Items.FindByValue(strDirector_code_ds) != null)
                {
                    cboDirector_ds.SelectedIndex = -1;
                    cboDirector_ds.Items.FindByValue(strDirector_code_ds).Selected = true;
                }
                InitcboUnit_ds();
            }
        }

        private void InitcboUnit_sr()
        {
            cUnit oUnit = new cUnit();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strUnit_code_sr = cboUnit_sr.SelectedValue;
            string strDirector_code_sr = cboDirector_sr.SelectedValue;
            string strYear = cboYear.SelectedValue;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and unit.unit_year = '" + strYear + "'  and  unit.c_active='Y' " +
                                   " and unit.director_code = '" + strDirector_code_sr + "' ";
            if (oUnit.SP_SEL_UNIT(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboUnit_sr.Items.Clear();
                cboUnit_sr.Items.Add(new ListItem("---- เลือกข้อมูลทั้งหมด ----", ""));
                int i;
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboUnit_sr.Items.Add(new ListItem(dt.Rows[i]["unit_name"].ToString(), dt.Rows[i]["unit_code"].ToString()));
                }
                if (cboUnit_sr.Items.FindByValue(strUnit_code_sr) != null)
                {
                    cboUnit_sr.SelectedIndex = -1;
                    cboUnit_sr.Items.FindByValue(strUnit_code_sr).Selected = true;
                }
            }
        }

        private void InitcboUnit_ds()
        {
            cUnit oUnit = new cUnit();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strUnit_code_ds = cboUnit_ds.SelectedValue;
            string strDirector_code_ds = cboDirector_ds.SelectedValue;
            string strYear = cboYear.SelectedValue;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and unit.unit_year = '" + strYear + "'  and  unit.c_active='Y' " +
                                   " and unit.director_code = '" + strDirector_code_ds + "' ";
            if (oUnit.SP_SEL_UNIT(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboUnit_ds.Items.Clear();
                cboUnit_ds.Items.Add(new ListItem("---- เลือกข้อมูลทั้งหมด ----", ""));
                int i;
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboUnit_ds.Items.Add(new ListItem(dt.Rows[i]["unit_name"].ToString(), dt.Rows[i]["unit_code"].ToString()));
                }
                if (cboUnit_ds.Items.FindByValue(strUnit_code_ds) != null)
                {
                    cboUnit_ds.SelectedIndex = -1;
                    cboUnit_ds.Items.FindByValue(strUnit_code_ds).Selected = true;
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
                    InitcboYear();
                    if (cboYear.Items.FindByValue(strYear) != null)
                    {
                        cboYear.SelectedIndex = -1;
                        cboYear.Items.FindByValue(strYear).Selected = true;
                    }

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

        private void InitcboLot_sr()
        {
            cLot oLot = new cLot();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strLot_code_sr = string.Empty;
            string strLot_sr = cboLot_sr.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and c_active='Y'  and lot_year='" + cboYear.SelectedValue + "'";

            if (oLot.SP_SEL_LOT(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboLot_sr.Items.Clear();
                cboLot_sr.Items.Add(new ListItem("---- เลือกข้อมูลทั้งหมด ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboLot_sr.Items.Add(new ListItem(dt.Rows[i]["lot_name"].ToString(), dt.Rows[i]["lot_code"].ToString()));
                }
                if (cboLot_sr.Items.FindByValue(strLot_code_sr) != null)
                {
                    cboLot_sr.SelectedIndex = -1;
                    cboLot_sr.Items.FindByValue(strLot_code_sr).Selected = true;
                }
            }
        }

        private void InitcboLot_ds()
        {
            cLot oLot = new cLot();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strLot_code_ds = string.Empty;
            string strLot_ds = cboLot_ds.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and c_active='Y'  and lot_year='" + cboYear.SelectedValue + "'";
            if (oLot.SP_SEL_LOT(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboLot_ds.Items.Clear();
                cboLot_ds.Items.Add(new ListItem("---- เลือกข้อมูลทั้งหมด ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboLot_ds.Items.Add(new ListItem(dt.Rows[i]["lot_name"].ToString(), dt.Rows[i]["lot_code"].ToString()));
                }
                if (cboLot_ds.Items.FindByValue(strLot_code_ds) != null)
                {
                    cboLot_ds.SelectedIndex = -1;
                    cboLot_ds.Items.FindByValue(strLot_code_ds).Selected = true;
                }
            }
        }

        private void InitcboItem_group_sr()
        {
            cItem_group oItem_group = new cItem_group();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strItem_group_code_sr = string.Empty;
            string strlot_code_sr = cboLot_sr.SelectedValue;
            strItem_group_code_sr = cboItemgroup_sr.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = "and lot_code='" + strlot_code_sr + "' and c_active='Y' ";
            if (oItem_group.SP_SEL_item_group(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboItemgroup_sr.Items.Clear();
                cboItemgroup_sr.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboItemgroup_sr.Items.Add(new ListItem(dt.Rows[i]["Item_group_name"].ToString(), dt.Rows[i]["Item_group_code"].ToString()));
                }
                if (cboItemgroup_sr.Items.FindByValue(strItem_group_code_sr) != null)
                {
                    cboItemgroup_sr.SelectedIndex = -1;
                    cboItemgroup_sr.Items.FindByValue(strItem_group_code_sr).Selected = true;
                }
            }
        }

        private void InitcboItem_group_ds()
        {
            cItem_group oItem_group = new cItem_group();
            string strMessage = string.Empty,
                        strCriteria = string.Empty,
                        strItem_group_code_ds = string.Empty;
            string strlot_code_ds = cboLot_ds.SelectedValue;
            strItem_group_code_ds = cboItemgroup_ds.SelectedValue;
            int i;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = "and lot_code='" + strlot_code_ds + "' and c_active='Y' ";
            if (oItem_group.SP_SEL_item_group(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboItemgroup_ds.Items.Clear();
                cboItemgroup_ds.Items.Add(new ListItem("---- กรุณาเลือกข้อมูล ----", ""));
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cboItemgroup_ds.Items.Add(new ListItem(dt.Rows[i]["Item_group_name"].ToString(), dt.Rows[i]["Item_group_code"].ToString()));
                }
                if (cboItemgroup_ds.Items.FindByValue(strItem_group_code_ds) != null)
                {
                    cboItemgroup_ds.SelectedIndex = -1;
                    cboItemgroup_ds.Items.FindByValue(strItem_group_code_ds).Selected = true;
                }
            }
        }

        #endregion

        protected void imgPrint_Click(object sender, ImageClickEventArgs e)
        {
            string strCriteria = string.Empty;
            string strCriteria2 = string.Empty;
            string strYear = string.Empty;
            string strActive = string.Empty;

            string strbudget_plan_code_sr = string.Empty;
            string strdirector_code_sr = string.Empty;
            string strunit_code_sr = string.Empty;
            string strlot_code_sr = string.Empty;
            string stritem_code_sr = string.Empty;

            string strbudget_plan_code_ds = string.Empty;
            string strdirector_code_ds = string.Empty;
            string strunit_code_ds = string.Empty;
            string strlot_code_ds = string.Empty;
            string stritem_code_ds = string.Empty;

            string strScript = string.Empty;
            string strPay_Month = string.Empty;
            string strPay_Year = string.Empty;
            string strReport_code = string.Empty;

            strPay_Month = cboPay_Month.SelectedValue;
            strPay_Year = cboPay_Year.SelectedValue;

            strYear = cboYear.SelectedValue;
            strbudget_plan_code_sr = txtbudget_plan_code_sr.Text;
            strdirector_code_sr = cboDirector_sr.SelectedValue;
            strunit_code_sr = cboUnit_sr.SelectedValue;
            strlot_code_sr = cboLot_sr.SelectedValue;
            stritem_code_sr = cboItemgroup_sr.SelectedValue;

            strbudget_plan_code_ds = txtbudget_plan_code_ds.Text;
            strdirector_code_ds = cboDirector_ds.SelectedValue;
            strunit_code_ds = cboUnit_ds.SelectedValue;
            strlot_code_ds = cboLot_ds.SelectedValue;
            stritem_code_ds = cboItemgroup_ds.SelectedValue;


            if (!strYear.Equals(""))
            {
                strCriteria = strCriteria + "  And  budget_year = '" + strYear + "'  ";
            }

            if (!strPay_Month.Equals(""))
            {
                strCriteria = strCriteria + "  And  budget_tranfer_month='" + strPay_Month + "' ";
            }

            if (!strPay_Year.Equals(""))
            {
                strCriteria = strCriteria + "  And  budget_tranfer_year='" + strPay_Year + "' ";
            }

            if (!strdirector_code_sr.Equals(""))
            {
                strCriteria = strCriteria + "  And  director_code_sr = '" + strdirector_code_sr + "' ";
            }

            if (!strdirector_code_ds.Equals(""))
            {
                strCriteria = strCriteria + "  And  director_code_ds = '" + strdirector_code_ds + "' ";
            }

            if (!strunit_code_sr.Equals(""))
            {
                strCriteria = strCriteria + "  And  unit_code_sr = '" + strunit_code_sr + "' ";
            }

            if (!strunit_code_ds.Equals(""))
            {
                strCriteria = strCriteria + "  And  unit_code_ds = '" + strunit_code_ds + "' ";
            }

            if (!strlot_code_sr.Equals(""))
            {
                strCriteria = strCriteria + "  And  lot_code_sr = '" + strlot_code_sr + "' ";
            }

            if (!strlot_code_ds.Equals(""))
            {
                strCriteria = strCriteria + "  And  lot_code_sr = '" + strlot_code_ds + "' ";
            }

            if (!strbudget_plan_code_sr.Equals(""))
            {
                strCriteria = strCriteria + "  And  budget_plan_code_sr = '" + strbudget_plan_code_sr + "' ";
            }

            if (!strbudget_plan_code_ds.Equals(""))
            {
                strCriteria = strCriteria + "  And  budget_plan_code_ds = '" + strbudget_plan_code_ds + "' ";
            }

            if (RadioButtonList1.SelectedValue.Equals("1"))
            {
                strReport_code = "Rep_paymentbytranfer02";
            }
            else if (RadioButtonList1.SelectedValue.Equals("2"))
            {
                strReport_code = "Rep_paymentbytranfer";
            }

            Session["criteria"] = strCriteria;
            strScript = "windowOpenMaximize(\"../../App_Control/reportsparameter/payment_report_show.aspx?report_code=" + strReport_code +
                                                         "&months=" + cboPay_Month.Text + "&year=" + cboPay_Year.Text + "\", \"_blank\");\n";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript, true);

        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
           // InitcboRound();
            InitcboDirector_sr();
            InitcboDirector_ds();
            InitcboLot_sr();
            InitcboLot_ds();
            InitcboItem_group_sr();
            InitcboItem_group_ds();
        }

        protected void cboDirector_sr_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboUnit_sr();
        }

        protected void cboDirector_ds_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboUnit_ds();
        }


        protected void cboLot_sr_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboItem_group_sr();
        }

        protected void cboLot_ds_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboItem_group_ds();
        }

     
     

      

    }
}
