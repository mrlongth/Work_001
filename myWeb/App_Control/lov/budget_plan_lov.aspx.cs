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

namespace myWeb.App_Control.lov
{
    public partial class budget_plan_lov : PageBase
    {

        #region private data
        private string strConn = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"];
        private bool[] blnAccessRight = new bool[5] { false, false, false, false, false };
        private string strPrefixCtr = "ctl00$ASPxRoundPanel2$ContentPlaceHolder1$";
        #endregion

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                imgFind.Attributes.Add("onMouseOver", "src='../../images/button/Search2.png'");
                imgFind.Attributes.Add("onMouseOut", "src='../../images/button/Search.png'");

                Session["menulov_name"] = "ค้นหาข้อมูลผังงบประมาณ";

                #region Set QueryString
                if (Request.QueryString["budget_plan_year"] != null)
                {
                    ViewState["year"] = Request.QueryString["budget_plan_year"].ToString();
                }
                else
                {
                    ViewState["year"] = string.Empty;
                }
                if (ViewState["year"].ToString().Equals(""))
                {
                    ViewState["year"] = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["yearnow"].ToString();
                }
                if (Request.QueryString["budget_type"] != null)
                {
                    ViewState["BudgetType"] = Helper.CStr(Request.QueryString["budget_type"]);
                }



                txtyear.Text = ViewState["year"].ToString();
                txtyear.CssClass = "textboxdis";
                txtyear.ReadOnly = true;
                if (Request.QueryString["budget_plan_code"] != null)
                {
                    ViewState["budget_plan_code"] = Request.QueryString["budget_plan_code"].ToString();
                    txtbudget_plan_code.Text = ViewState["budget_plan_code"].ToString();
                }
                else
                {
                    ViewState["budget_plan_code"] = string.Empty;
                    txtbudget_plan_code.Text = string.Empty;
                }

                //if (Request.QueryString["unit_name"] != null)
                //{
                //    ViewState["unit_name"] = Request.QueryString["unit_name"].ToString();
                //    txtunit_name.Text = ViewState["unit_name"].ToString();
                //}
                //else
                //{
                //    ViewState["unit_name"] = string.Empty;
                //    txtunit_name.Text = string.Empty;
                //}

                //if (Request.QueryString["activity_name"] != null)
                //{
                //    ViewState["activity_name"] = Request.QueryString["activity_name"].ToString();
                //    txtactivity_name.Text = ViewState["activity_name"].ToString();
                //}
                //else
                //{
                //    ViewState["activity_name"] = string.Empty;
                //    txtactivity_name.Text = string.Empty;
                //}

                //if (Request.QueryString["plan_name"] != null)
                //{
                //    ViewState["plan_name"] = Request.QueryString["plan_name"].ToString();
                //    txtplan_name.Text = ViewState["plan_name"].ToString();
                //}
                //else
                //{
                //    ViewState["plan_name"] = string.Empty;
                //    txtplan_name.Text = string.Empty;
                //}

                if (Request.QueryString["work_name"] != null)
                {
                    ViewState["work_name"] = Request.QueryString["work_name"].ToString();
                    txtwork_name.Text = ViewState["work_name"].ToString();
                }
                else
                {
                    ViewState["work_name"] = string.Empty;
                    txtwork_name.Text = string.Empty;
                }

                if (Request.QueryString["ctrl1"] != null)
                {
                    ViewState["ctrl1"] = Request.QueryString["ctrl1"].ToString();
                }
                else
                {
                    ViewState["ctrl1"] = string.Empty;
                }

                if (Request.QueryString["ctrl2"] != null)
                {
                    ViewState["ctrl2"] = Request.QueryString["ctrl2"].ToString();
                }
                else
                {
                    ViewState["ctrl2"] = string.Empty;
                }

                if (Request.QueryString["ctrl3"] != null)
                {
                    ViewState["ctrl3"] = Request.QueryString["ctrl3"].ToString();
                }
                else
                {
                    ViewState["ctrl3"] = string.Empty;
                }

                if (Request.QueryString["ctrl4"] != null)
                {
                    ViewState["ctrl4"] = Request.QueryString["ctrl4"].ToString();
                }
                else
                {
                    ViewState["ctrl4"] = string.Empty;
                }

                if (Request.QueryString["ctrl5"] != null)
                {
                    ViewState["ctrl5"] = Request.QueryString["ctrl5"].ToString();
                }
                else
                {
                    ViewState["ctrl5"] = string.Empty;
                }

                if (Request.QueryString["ctrl6"] != null)
                {
                    ViewState["ctrl6"] = Request.QueryString["ctrl6"].ToString();
                }
                else
                {
                    ViewState["ctrl6"] = string.Empty;
                }

                if (Request.QueryString["ctrl7"] != null)
                {
                    ViewState["ctrl7"] = Request.QueryString["ctrl7"].ToString();
                }
                else
                {
                    ViewState["ctrl7"] = string.Empty;
                }

                if (Request.QueryString["ctrl8"] != null)
                {
                    ViewState["ctrl8"] = Request.QueryString["ctrl8"].ToString();
                }
                else
                {
                    ViewState["ctrl8"] = string.Empty;
                }

                if (Request.QueryString["ctrl9"] != null)
                {
                    ViewState["ctrl9"] = Request.QueryString["ctrl9"].ToString();
                }
                else
                {
                    ViewState["ctrl9"] = string.Empty;
                }

                if (Request.QueryString["ctrl10"] != null)
                {
                    ViewState["ctrl10"] = Request.QueryString["ctrl10"].ToString();
                }
                else
                {
                    ViewState["ctrl10"] = string.Empty;
                }

                if (Request.QueryString["ctrl11"] != null)
                {
                    ViewState["ctrl11"] = Request.QueryString["ctrl11"].ToString();
                }
                else
                {
                    ViewState["ctrl11"] = string.Empty;
                }
                #endregion

                if (Request.QueryString["show"] != null)
                {
                    ViewState["show"] = Request.QueryString["show"].ToString();
                }
                else
                {
                    ViewState["show"] = "1";
                }

                if (Request.QueryString["from_page"] != null)
                {
                    ViewState["from_page"] = Request.QueryString["from_page"].ToString();
                }
                else
                {
                    ViewState["from_page"] = "";
                }



                #region Set Image

                //imgList_unit.Attributes.Add("onclick", "OpenPopUp('800px','400px','93%','ค้นหาข้อมูลหน่วยงาน' ,'../lov/unit_lov.aspx?unit_year='+document.forms[0]." + strPrefixCtr + "txtyear.value+" +
                //                                                         "'&unit_code='+document.forms[0]." + strPrefixCtr + "txtunit_code.value+" +
                //                                                         "'&unit_name='+document.forms[0]." + strPrefixCtr + "txtunit_name.value+" +
                //                                                         "'&ctrl1=" + txtunit_code.ClientID + "&ctrl2=" + txtunit_name.ClientID + "&show=3', '3');return false;");
                //imgClear_unit.Attributes.Add("onclick", "document.forms[0]." + strPrefixCtr + "txtunit_code.value='';document.forms[0]." + strPrefixCtr + "txtunit_name.value=''; return false;");

                //imgList_activity.Attributes.Add("onclick", "OpenPopUp('900px','450px','93%','ค้นหาข้อมูลกิจกรรม' ,'../lov/activity_lov.aspx?year='+document.forms[0]." + strPrefixCtr + "txtyear.value+" +
                //                                                        "'&activity_code='+document.forms[0]." + strPrefixCtr + "txtactivity_code.value+" +
                //                                                        "'&activity_name='+document.forms[0]." + strPrefixCtr + "txtactivity_name.value+" +
                //                                                        "'&ctrl1=" + txtactivity_code.ClientID + "&ctrl2=" + txtactivity_name.ClientID + "&show=3', '3');return false;");
                //imgClear_activity.Attributes.Add("onclick", "document.forms[0]." + strPrefixCtr + "txtactivity_code.value='';document.forms[0]." + strPrefixCtr + "txtactivity_name.value=''; return false;");

                //imgList_plan.Attributes.Add("onclick", "OpenPopUp('800px','400px','93%','ค้นหาข้อมูลแผนงาน' ,'../lov/plan_lov.aspx?year='+document.forms[0]." + strPrefixCtr + "txtyear.value+" +
                //                                                        "'&plan_code='+document.forms[0]." + strPrefixCtr + "txtplan_code.value+" +
                //                                                        "'&plan_name='+document.forms[0]." + strPrefixCtr + "txtplan_name.value+" +
                //                                                        "'&ctrl1=" + txtplan_code.ClientID + "&ctrl2=" + txtplan_name.ClientID + "&show=3', '3');return false;");
                //imgClear_plan.Attributes.Add("onclick", "document.forms[0]." + strPrefixCtr + "txtplan_code.value='';document.forms[0]." + strPrefixCtr + "txtplan_name.value=''; return false;");

                imgList_work.Attributes.Add("onclick", "OpenPopUp('800px','400px','93%','ค้นหาข้อมูลงาน' ,'../lov/work_lov.aspx?year='+document.forms[0]." + strPrefixCtr + "txtyear.value+" +
                                                                    "'&work_code='+document.forms[0]." + strPrefixCtr + "txtwork_code.value+" +
                                                                    "'&work_name='+document.forms[0]." + strPrefixCtr + "txtwork_name.value+" +
                                                                    "'&ctrl1=" + txtwork_code.ClientID + "&ctrl2=" + txtwork_name.ClientID + "&show=3', '3');return false;");
                imgClear_work.Attributes.Add("onclick", "document.forms[0]." + strPrefixCtr + "txtwork_code.value='';document.forms[0]." + strPrefixCtr + "txtwork_name.value=''; return false;");

                #endregion

                InitcboDirector();
                InitcboBudget();

                InitcboPlan();

                ViewState["sort"] = "budget_plan_code";
                ViewState["direction"] = "ASC";
                txtyear.Text = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["yearnow"].ToString();
                BindGridView();

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
                BindGridView();
            }
        }

        #region private function


        private void BindGridView()
        {
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
            strbudget_plan_year = txtyear.Text.Replace("'", "''").Trim();
            strbudget_plan_code = txtbudget_plan_code.Text.Replace("'", "''").Trim();
            strbudget_code = cboBudget.SelectedValue;
            strproduce_code = cboProduce.SelectedValue;
            strdirector_code = cboDirector.SelectedValue;
            strunit_code = cboUnit.SelectedValue;
            stractivity_code = cboActivity.SelectedValue;
            strplan_code = cboPlan_code.SelectedValue;
            strwork_code = txtwork_code.Text.Replace("'", "''").Trim();
            strwork_name = txtwork_name.Text.Replace("'", "''").Trim();

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
            strCriteria = strCriteria + "  And  (c_active ='Y') ";

            if (ViewState["from_page"].ToString().Equals("budgetmoney"))
            {
                strCriteria = strCriteria + "  And  budget_type='" + this.BudgetType + "' ";
            }
            else
            {
                if (this.BudgetType != "M")
                {
                    strCriteria = strCriteria + " and budget_type ='" + this.BudgetType + "' ";
                }
            }

            if (DirectorLock == "Y")
            {
                strCriteria += " and substring(director_code,4,2) = substring('" + DirectorCode + "',4,2) ";
            }

            if (UnitLock == "Y")
            {
                strCriteria += " and substring(unit_code,4,5) in (" + this.UnitCodeList + ") ";
            }

            //budget_type

            #endregion
            try
            {
                if (oBudget_plan.SP_BUDGET_PLAN_SEL(strCriteria, ref ds, ref strMessage))
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        string strpbudget_plan_code = string.Empty,
                                    strpbudget_name = string.Empty,
                                     strpproduce_name = string.Empty,
                                     strpactivity_name = string.Empty,
                                     strpplan_name = string.Empty,
                                     strpwork_name = string.Empty,
                                     strpfund_name = string.Empty,
                                     strpdirector_name = string.Empty,
                                     strpunit_name = string.Empty,
                                     strpbudget_plan_year = string.Empty;
                        strpbudget_plan_code = ds.Tables[0].Rows[0]["budget_plan_code"].ToString();
                        strpbudget_name = ds.Tables[0].Rows[0]["budget_name"].ToString();
                        strpproduce_name = ds.Tables[0].Rows[0]["produce_name"].ToString();
                        strpactivity_name = ds.Tables[0].Rows[0]["activity_name"].ToString();
                        strpplan_name = ds.Tables[0].Rows[0]["plan_name"].ToString();
                        strpwork_name = ds.Tables[0].Rows[0]["work_name"].ToString();
                        strpfund_name = ds.Tables[0].Rows[0]["fund_name"].ToString();
                        strpdirector_name = ds.Tables[0].Rows[0]["director_name"].ToString();
                        strpunit_name = ds.Tables[0].Rows[0]["unit_name"].ToString();
                        strpactivity_name = ds.Tables[0].Rows[0]["activity_name"].ToString();
                        if (!ViewState["show"].ToString().Equals("1"))
                        {


                            if (!ViewState["ctrl1"].ToString().Equals(""))
                            {
                                strScript = "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + strpbudget_plan_code + "';\n ";
                            }

                            if (!ViewState["ctrl2"].ToString().Equals(""))
                            {
                                strScript = strScript + "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + strpbudget_name + "';\n";
                            }
                            if (!ViewState["ctrl3"].ToString().Equals(""))
                            {
                                strScript = strScript + "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl3"].ToString() + "').value='" + strpproduce_name + "';\n";
                            }
                            if (!ViewState["ctrl4"].ToString().Equals(""))
                            {
                                strScript = strScript + "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl4"].ToString() + "').value='" + strpactivity_name + "';\n";
                            }
                            if (!ViewState["ctrl5"].ToString().Equals(""))
                            {
                                strScript = strScript + "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl5"].ToString() + "').value='" + strpplan_name + "';\n";
                            }
                            if (!ViewState["ctrl6"].ToString().Equals(""))
                            {
                                strScript = strScript + "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl6"].ToString() + "').value='" + strpwork_name + "';\n";
                            }
                            if (!ViewState["ctrl7"].ToString().Equals(""))
                            {
                                strScript = strScript + "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl7"].ToString() + "').value='" + strpfund_name + "';\n";
                            }
                            if (!ViewState["ctrl9"].ToString().Equals(""))
                            {
                                strScript = strScript + "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl9"].ToString() + "').value='" + strpdirector_name + "';\n";
                            }
                            if (!ViewState["ctrl10"].ToString().Equals(""))
                            {
                                strScript = strScript + "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl10"].ToString() + "').value='" + strpunit_name + "';\n";
                            }

                            if (!ViewState["ctrl11"].ToString().Equals(""))
                            {
                                strScript = strScript + "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl11"].ToString() + "').value='" + txtyear.Text + "';\n";
                            }
                            if (ViewState["from_page"].ToString().Equals("budget_tranfer_control"))
                            {
                                strScript = strScript + "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].__doPostBack('ctl00$ContentPlaceHolder1$BtnR1','');";
                            }

                            if (ViewState["from_page"].ToString().Equals("budgetmoney"))
                            {
                                strScript = strScript + "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].__doPostBack('ctl00$ContentPlaceHolder1$LinkButton1','');";
                            }



                            if (ViewState["ctrl11"].ToString().Equals(""))
                            {
                                //strScript = strScript + "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].__doPostBack('ctl00$ContentPlaceHolder1$BtnR1','');";
                            }
                            strScript = strScript + "ClosePopUp('" + ViewState["show"].ToString() + "');";

                            //strScript = "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + strpbudget_plan_code + "';\n " +
                            //                     "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + strpbudget_name + "';\n" +
                            //                     "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl3"].ToString() + "').value='" + strpproduce_name + "';\n" +
                            //                     "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl4"].ToString() + "').value='" + strpactivity_name + "';\n" +
                            //                     "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl5"].ToString() + "').value='" + strpplan_name + "';\n" +
                            //                     "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl6"].ToString() + "').value='" + strpwork_name + "';\n" +
                            //                     "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl7"].ToString() + "').value='" + strpfund_name + "';\n" +
                            //                     "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl9"].ToString() + "').value='" + strpdirector_name + "';\n" +
                            //                     "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl10"].ToString() + "').value='" + strpunit_name + "';\n" +
                            //                     "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl11"].ToString() + "').value='" + txtyear.Text + "';\n" +
                            //                    "ClosePopUp('" + ViewState["show"].ToString() + "');";
                        }
                        else
                        {
                            strScript = "window.parent.document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + strpbudget_plan_code + "';\n " +
                                                "window.parent.document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + strpbudget_name + "';\n" +
                                                "window.parent.document.getElementById('" + ViewState["ctrl3"].ToString() + "').value='" + strpproduce_name + "';\n" +
                                                "window.parent.document.getElementById('" + ViewState["ctrl4"].ToString() + "').value='" + strpactivity_name + "';\n" +
                                                "window.parent.document.getElementById('" + ViewState["ctrl5"].ToString() + "').value='" + strpplan_name + "';\n" +
                                                "window.parent.document.getElementById('" + ViewState["ctrl6"].ToString() + "').value='" + strpwork_name + "';\n" +
                                                "window.parent.document.getElementById('" + ViewState["ctrl7"].ToString() + "').value='" + strpfund_name + "';\n" +
                                                "window.parent.document.getElementById('" + ViewState["ctrl9"].ToString() + "').value='" + strpdirector_name + "';\n" +
                                                "window.parent.document.getElementById('" + ViewState["ctrl10"].ToString() + "').value='" + strpunit_name + "';\n" +
                                                "window.parent.document.getElementById('" + ViewState["ctrl11"].ToString() + "').value='" + txtyear.Text + "';\n" +
                                                "window.parent.$find('show_ModalPopupExtender').hide();" +
                                                "ClosePopUp('" + ViewState["show"].ToString() + "');";
                        }
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "close", strScript, true);
                    }
                    else
                    {
                        ds.Tables[0].DefaultView.Sort = ViewState["sort"] + " " + ViewState["direction"];
                        GridView1.DataSource = ds.Tables[0];
                        GridView1.DataBind();
                    }
                }
                else
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

                oBudget_plan.Dispose();
                ds.Dispose();
            }
        }



        private void InitcboDirector()
        {
            cDirector oDirector = new cDirector();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strDirector_code = string.Empty;
            string strYear = ViewState["year"].ToString();
            strDirector_code = cboDirector.SelectedValue;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and director_year = '" + strYear + "'  and  c_active='Y' ";
            if (DirectorLock == "Y")
            {
                strCriteria += " and substring(director_code,4,2) = substring('" + DirectorCode + "',4,2) ";
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
            string strYear = ViewState["year"].ToString();
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
            if (UnitLock == "Y")
            {
                strCriteria += " and substring(unit.unit_code,4,5) in (" + this.UnitCodeList + ") ";
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
            string strYear = ViewState["year"].ToString();
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
            string strYear = ViewState["year"].ToString();
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
            string strYear = ViewState["year"].ToString();
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

        protected void imgFind_Click(object sender, ImageClickEventArgs e)
        {
            BindGridView();
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
                int nNo = e.Row.RowIndex + 1;
                lblNo.Text = nNo.ToString();
                Label lblbudget_plan_code = (Label)e.Row.FindControl("lblbudget_plan_code");
                Label lblbudget_name = (Label)e.Row.FindControl("lblbudget_name");
                Label lblproduce_name = (Label)e.Row.FindControl("lblproduce_name");
                Label lblactivity_name = (Label)e.Row.FindControl("lblactivity_name");
                Label lblplan_name = (Label)e.Row.FindControl("lblplan_name");
                Label lblwork_name = (Label)e.Row.FindControl("lblwork_name");
                Label lblfund_name = (Label)e.Row.FindControl("lblfund_name");
                Label lbldirector_name = (Label)e.Row.FindControl("lbldirector_name");
                Label lblunit_name = (Label)e.Row.FindControl("lblunit_name");
                if (!ViewState["show"].ToString().Equals("1"))
                {
                    string strShow;
                    strShow = "<a href=\"\" onclick=\"";
                    if (!ViewState["ctrl1"].ToString().Equals(""))
                    {
                        strShow = strShow + "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + lblbudget_plan_code.Text + "';\n ";
                    }

                    if (!ViewState["ctrl2"].ToString().Equals(""))
                    {
                        strShow = strShow + "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + lblbudget_name.Text + "';\n";
                    }
                    if (!ViewState["ctrl3"].ToString().Equals(""))
                    {
                        strShow = strShow + "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl3"].ToString() + "').value='" + lblproduce_name.Text + "';\n";
                    }
                    if (!ViewState["ctrl4"].ToString().Equals(""))
                    {
                        strShow = strShow + "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl4"].ToString() + "').value='" + lblactivity_name.Text + "';\n";
                    }
                    if (!ViewState["ctrl5"].ToString().Equals(""))
                    {
                        strShow = strShow + "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl5"].ToString() + "').value='" + lblplan_name.Text + "';\n";
                    }
                    if (!ViewState["ctrl6"].ToString().Equals(""))
                    {
                        strShow = strShow + "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl6"].ToString() + "').value='" + lblwork_name.Text + "';\n";
                    }
                    if (!ViewState["ctrl7"].ToString().Equals(""))
                    {
                        strShow = strShow + "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl7"].ToString() + "').value='" + lblfund_name.Text + "';\n";
                    }
                    if (!ViewState["ctrl9"].ToString().Equals(""))
                    {
                        strShow = strShow + "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl9"].ToString() + "').value='" + lbldirector_name.Text + "';\n";
                    }
                    if (!ViewState["ctrl10"].ToString().Equals(""))
                    {
                        strShow = strShow + "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl10"].ToString() + "').value='" + lblunit_name.Text + "';\n";
                    }

                    if (!ViewState["ctrl11"].ToString().Equals(""))
                    {
                        strShow = strShow + "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].document.getElementById('" + ViewState["ctrl11"].ToString() + "').value='" + txtyear.Text + "';\n";
                    }
                    if (ViewState["from_page"].ToString().Equals("budgetmoney"))
                    {
                        strShow = strShow + "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].__doPostBack('ctl00$ContentPlaceHolder1$LinkButton1','');";
                    }

                    if (ViewState["from_page"].ToString().Equals("budget_tranfer_control"))
                    {
                        strShow = strShow + "window.parent.frames['iframeShow" + (int.Parse(ViewState["show"].ToString()) - 1) + "'].__doPostBack('ctl00$ContentPlaceHolder1$BtnR1','');";
                    }
                    strShow = strShow + "ClosePopUp('" + ViewState["show"].ToString() + "');";
                    strShow = strShow + "return false;\" >" + lblbudget_plan_code.Text + "</a>";
                    lblbudget_plan_code.Text = strShow;
                }
                else
                {
                    string strShow;
                    strShow = "<a href=\"\" onclick=\"";
                    if (!ViewState["ctrl1"].ToString().Equals(""))
                    {

                        strShow += "window.parent.document.getElementById('" + ViewState["ctrl1"].ToString() + "').value='" + lblbudget_plan_code.Text + "';\n ";
                    }
                    if (!ViewState["ctrl2"].ToString().Equals(""))
                    {
                        strShow += "window.parent.document.getElementById('" + ViewState["ctrl2"].ToString() + "').value='" + lblbudget_name.Text + "';\n";
                    }
                    if (!ViewState["ctrl3"].ToString().Equals(""))
                    {
                        strShow += "window.parent.document.getElementById('" + ViewState["ctrl3"].ToString() + "').value='" + lblproduce_name.Text + "';\n";
                    }
                    if (!ViewState["ctrl4"].ToString().Equals(""))
                    {
                        strShow += "window.parent.document.getElementById('" + ViewState["ctrl4"].ToString() + "').value='" + lblactivity_name.Text + "';\n";
                    }
                    if (!ViewState["ctrl5"].ToString().Equals(""))
                    {
                        strShow += "window.parent.document.getElementById('" + ViewState["ctrl5"].ToString() + "').value='" + lblplan_name.Text + "';\n";
                    }
                    if (!ViewState["ctrl6"].ToString().Equals(""))
                    {
                        strShow += "window.parent.document.getElementById('" + ViewState["ctrl6"].ToString() + "').value='" + lblwork_name.Text + "';\n";
                    }
                    if (!ViewState["ctrl7"].ToString().Equals(""))
                    {
                        strShow += "window.parent.document.getElementById('" + ViewState["ctrl7"].ToString() + "').value='" + lblfund_name.Text + "';\n";
                    }
                    if (!ViewState["ctrl9"].ToString().Equals(""))
                    {
                        strShow += "window.parent.document.getElementById('" + ViewState["ctrl9"].ToString() + "').value='" + lbldirector_name.Text + "';\n";
                    }
                    if (!ViewState["ctrl10"].ToString().Equals(""))
                    {
                        strShow += "window.parent.document.getElementById('" + ViewState["ctrl10"].ToString() + "').value='" + lblunit_name.Text + "';\n";
                    }
                    if (!ViewState["ctrl11"].ToString().Equals(""))
                    {
                        strShow = strShow + "window.parent.document.getElementById('" + ViewState["ctrl11"].ToString() + "').value='" + txtyear.Text + "';\n";
                    }
                    strShow = strShow + "ClosePopUp('" + ViewState["show"].ToString() + "');";
                    strShow = strShow + "return false;\" >" + lblbudget_plan_code.Text + "</a>";
                    lblbudget_plan_code.Text = strShow;
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
                BindGridView();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        protected void cboDirector_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboUnit();
        }

        protected void cboBudget_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboProduce();
        }

        protected void cboProduce_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboActivity();
        }

        protected void cboActivity_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboActivity();
            //  BindGridView();  
        }

        protected void cboPlan_code_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboPlan();
            //BindGridView();  
        }

        protected void cboUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboUnit();
        }

    }
}
