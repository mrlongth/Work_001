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

namespace myWeb.App_Control.person
{
    public partial class person_item_import : PageBase
    {
        private string strPrefixCtr = "ctl00$ASPxRoundPanel1$ASPxRoundPanel2$ContentPlaceHolder1$";

        public static string getNumber(object pNumber)
        {
            string strNumber = String.Format("{0:#,##0.00}", float.Parse(pNumber.ToString()));
            return strNumber;
        }

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

                imgImport.Attributes.Add("onMouseOver", "src='../../images/button/import2.png'");
                imgImport.Attributes.Add("onMouseOut", "src='../../images/button/import.png'");

                imgSaveOnly2.Attributes.Add("onMouseOver", "src='../../images/button/save_add2.png'");
                imgSaveOnly2.Attributes.Add("onMouseOut", "src='../../images/button/save_add.png'");

                imgCancel2.Attributes.Add("onMouseOver", "src='../../images/button/cancel2.png'");
                imgCancel2.Attributes.Add("onMouseOut", "src='../../images/button/cancel.png'");


                string strYear = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["yearnow"].ToString();

                imgList_item.Attributes.Add("onclick", "OpenPopUp('750px','400px','93%','ค้นหาข้อมูลรายได้' ,'../lov/item_lov.aspx?item_code='+document.forms[0]." + strPrefixCtr + "txtitem_code.value+" +
                              "'&item_name='+document.forms[0]." + strPrefixCtr + "txtitem_name.value+'&Item_type='+document.forms[0]." + strPrefixCtr +
                                "cboItem_type.options[document.forms[0]." + strPrefixCtr + "cboItem_type.selectedIndex].value+'" +
                              "&ctrl1=" + txtitem_code.ClientID + "&ctrl2=" + txtitem_name.ClientID + "&item_type=D&from=member', '1');return false;");

                imgClear_item.Attributes.Add("onclick", "document.forms[0]." + strPrefixCtr + "txtitem_code.value='';" +
                                        "document.forms[0]." + strPrefixCtr + "txtitem_name.value=''; return false;");

                imgList_item2.Attributes.Add("onclick", "OpenPopUp('750px','400px','93%','ค้นหาข้อมูลรายได้' ,'../lov/item_lov.aspx?item_code='+document.forms[0]." + strPrefixCtr + "txtitem_code2.value+" +
                        "'&item_name='+document.forms[0]." + strPrefixCtr + "txtitem_name2.value+" +
                        "'&ctrl1=" + txtitem_code2.ClientID + "&ctrl2=" + txtitem_name2.ClientID + "&item_type=D&from=member', '1');return false;");

                imgClear_item2.Attributes.Add("onclick", "document.forms[0]." + strPrefixCtr + "txtitem_code2.value='';" +
                                        "document.forms[0]." + strPrefixCtr + "txtitem_name2.value=''; return false;");

                panelSeek2.Visible = false;
                panelSeek.Visible = true;

                ViewState["sort"] = "person_code";
                ViewState["direction"] = "ASC";
                InitcboPerson_group();
                InitcboDirector();
                InitcboUnit();

                panelSeek2.Visible = false;
                panelSeek.Visible = true;
                GridView1.Visible = true;
                // GridView2.Visible = false;
                imgExcel.Visible = true;
                imgFind.Visible = false;
                imgSaveOnly.Visible = true;
                RequiredFieldValidator3.Enabled = false;

            }
        }

        #region private function

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
            strCriteria += " and person_group_code IN (" + PersonGroupList + ") ";
            if (oPerson_group.SP_PERSON_GROUP_SEL(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboPerson_group.Items.Clear();
                cboPerson_group.Items.Add(new ListItem("---- เลือกข้อมูลทั้งหมด ----", ""));
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

        private void InitcboDirector()
        {
            cDirector oDirector = new cDirector();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string strDirector_code = string.Empty;
            string strYear = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["yearnow"].ToString();
            strDirector_code = cboDirector.SelectedValue;
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
                cboDirector.Items.Clear();
                cboDirector.Items.Add(new ListItem("---- เลือกข้อมูลทั้งหมด ----", ""));
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
            string strYear = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["yearnow"].ToString();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            strCriteria = " and unit.unit_year = '" + strYear + "'  and  unit.c_active='Y' ";
            strCriteria = strCriteria + " and unit.director_code = '" + strDirector_code + "' ";
            if (oUnit.SP_SEL_UNIT(strCriteria, ref ds, ref strMessage))
            {
                dt = ds.Tables[0];
                cboUnit.Items.Clear();
                cboUnit.Items.Add(new ListItem("---- เลือกข้อมูลทั้งหมด ----", ""));
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
            //  this.imgSaveOnly.Click += new System.Web.UI.ImageClickEventHandler(this.imgSaveOnly_Click);
        }
        #endregion

        private bool saveData()
        {
            bool blnResult = false;
            string strMessage = string.Empty;
            string strActive = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty;
            string strScript = string.Empty;
            string pitem_code = string.Empty;
            string ppayment_item_tax = "Y";
            string ppayment_item_sos = "Y";
            string pcomments_sub = string.Empty;
            string strYear = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["yearnow"].ToString();
            int i;
            cPerson oPerson = new cPerson();
            try
            {
                strActive = "Y";
                strCreatedBy = Session["username"].ToString();
                strUpdatedBy = Session["username"].ToString();
                if (RadioButtonList1.SelectedValue == "A")
                {
                    pitem_code = txtitem_code.Text;
                }
                if (RadioButtonList1.SelectedValue == "E")
                {
                    pitem_code = txtitem_code.Text;
                }
                else
                {
                    pitem_code = txtitem_code2.Text;
                }

                GridViewRow gviewRow;

                for (i = 0; i <= GridView1.Rows.Count - 1; i++)
                {
                    gviewRow = GridView1.Rows[i];
                    Label lblperson_code = (Label)gviewRow.FindControl("lblperson_code");
                    AwNumeric txtmoney_credit = (AwNumeric)gviewRow.FindControl("txtmoney_credit");
                    CheckBox CheckBox1 = (CheckBox)gviewRow.FindControl("CheckBox1");

                    if (CheckBox1.Checked)
                    {
                        DataSet ds = new DataSet();
                        string strCheckDup = string.Empty;
                        string strCredit = "0";
                        string strDebit = "0";
                        if (cboItem_type.SelectedValue == "C")
                        {
                            strCredit = txtmoney_credit.Value.ToString();
                        }
                        else
                        {
                            strDebit = txtmoney_credit.Value.ToString();
                        }
                        if (!oPerson.SP_PERSON_ITEM_UPD(lblperson_code.Text, strYear, pitem_code, strDebit, strCredit, ppayment_item_tax,
                                  ppayment_item_sos, strActive, strUpdatedBy, "", "", "", ref strMessage))
                        {
                            lblError.Text = strMessage;
                        }
                    }
                }
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
                oPerson.Dispose();
            }
            return blnResult;
        }

        private void BindGridView()
        {
            lnkExcelFile.NavigateUrl = string.Empty;
            imgExcel.Src = "~/images/icon_exceldisable.gif";
            cPerson oPerson = new cPerson();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            //int i;
            string strPerson_group = cboPerson_group.SelectedValue;
            string strDirector = cboDirector.SelectedValue;
            string strUnit = cboUnit.SelectedValue;
            string stritem_code = txtitem_code.Text;
            cExcelReader oExcelReader = new cExcelReader();
            DataTable odtExcelAll = new DataTable();

            try
            {
                if (!stritem_code.Equals(""))
                {
                    strCriteria = strCriteria + "  and  item_code= '" + stritem_code + "' ";
                }

                if (!strPerson_group.Equals(""))
                {
                    strCriteria = strCriteria + "  and  person_group_code= '" + strPerson_group + "' ";
                }

                if (!strDirector.Equals(""))
                {
                    strCriteria = strCriteria + "  and  director_code= '" + strDirector + "' ";
                }

                if (!strUnit.Equals(""))
                {
                    strCriteria = strCriteria + "  and  unit_code= '" + strUnit + "' ";
                }

                strCriteria += " and person_group_code IN (" + PersonGroupList + ") ";

                if (DirectorLock == "Y")
                {
                    strCriteria += " and substring(director_code,4,2) = substring('" + DirectorCode + "',4,2) ";
                }

                if (!oPerson.SP_PERSON_ITEM_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];
                    string strFilename = "~/temp/excel" + txtitem_code.Text + "_" + DateTime.Now.ToString("yyyyMMddHH-mm-ss") + ".xls";
                    System.IO.File.Copy(Server.MapPath("~/excel_template/import.xls"), Server.MapPath(strFilename));
                    InitExcel(ref oExcelReader, Server.MapPath(strFilename));
                    oExcelReader.SheetName = "data";
                    //odtExcelAll = oExcelReader.GetTable("data", " And 1=2");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string strCmd = "Insert into [" + oExcelReader.SheetName + "$]  (person_code,person_name,person_surname) " +
                                                                                                 " Values ('" + Helper.CStr(dt.Rows[i]["person_code"]) + "','" +
                                                                                                  Helper.CStr(dt.Rows[i]["person_thai_name"]) + "','" +
                                                                                                  Helper.CStr(dt.Rows[i]["person_thai_surname"]) + "')";
                        oExcelReader.InsertTable(strCmd);
                    }
                    lnkExcelFile.NavigateUrl = strFilename;
                    imgExcel.Src = "~/images/icon_excel.gif";
                    MsgBox("สร้างแบบฟร์อมสมบูรณ์ พบข้อมูลทั้งสิ้น " + dt.Rows.Count + " รายการ คลิกที่รูปเพื่อดาวห์โหลด");
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oPerson.Dispose();
                ds.Dispose();
                oExcelReader.Dispose();
                odtExcelAll.Dispose();
            }
        }

        private void InitExcel(ref cExcelReader exr, string pPath)
        {
            exr.ExcelFilename = pPath;
            exr.Headers = true;
            exr.MixedData = true;
            exr.KeepConnectionOpen = true;
            //exr.Open()
        }

        private void BindGridView4()
        {
            cPerson oPerson = new cPerson();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            int i;
            string stritem_code = txtitem_code2.Text;
            string strGUID = hddGUID.Value.ToString();
            try
            {
                strCriteria = " and  item_code= '" + stritem_code + "' " +
                                        " and user_guid='" + strGUID + "' ";

                if (!oPerson.SP_PERSON_ITEM_IMPORT(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    ds.Tables[0].DefaultView.Sort = ViewState["sort"] + " " + ViewState["direction"];
                    ds.Tables[0].Columns.Add("item_has");
                    for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        ds.Tables[0].Rows[i]["item_has"] = "N";
                    }
                    ds.Tables[0].AcceptChanges();
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();

                    //imgSaveOnly.Visible = false;
                    imgCancel.Visible = true;


                    if (GridView1.Rows.Count > 0)
                    {
                        RadioButtonList1.Enabled = false;
                        txtitem_code2.Enabled = false;
                        txtitem_name2.Enabled = false;
                        imgList_item2.Enabled = false;
                        imgClear_item2.Enabled = false;

                        imgSaveOnly2.Visible = true;
                        imgImport.Visible = false;
                        imgCancel2.Visible = true;

                        GridView1.Visible = true;

                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oPerson.Dispose();
                ds.Dispose();
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.Header))
            {
                //Find the checkbox control in header and add an attribute
                ((CheckBox)e.Row.FindControl("cbSelectAll")).Attributes.Add("onclick", "javascript:SelectAll('" +
                        ((CheckBox)e.Row.FindControl("cbSelectAll")).ClientID + "')");

                for (int iCol = 0; iCol < e.Row.Cells.Count; iCol++)
                {
                    e.Row.Cells[iCol].Attributes.Add("class", "table_h");
                    e.Row.Cells[iCol].Wrap = false;
                    ViewState["sumall"] = "0.00";
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
                AwNumeric txtmoney_credit = (AwNumeric)e.Row.FindControl("txtmoney_credit");
                txtmoney_credit.Attributes.Add("onchange", "funcsum();");

                ViewState["sumall"] = String.Format("{0:#,##0.00}", decimal.Parse(ViewState["sumall"].ToString()) + decimal.Parse(txtmoney_credit.Text));

                #region set Image Edit & Delete

                ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                imgDelete.ImageUrl = ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["img"].ToString();
                imgDelete.Attributes.Add("title", ((DataSet)Application["xmlconfig"]).Tables["imgDelete"].Rows[0]["title"].ToString());
                imgDelete.Attributes.Add("onclick", "return confirm(\"คุณต้องการลบข้อมูลรับ/จ่ายบุคคลากร " + txtitem_code.Text + " : " + txtitem_name.Text + " ?\");");

                #endregion

                if (RadioButtonList1.SelectedValue.Equals("E"))
                {
                    imgDelete.Visible = true;
                }
                else
                {
                    imgDelete.Visible = false;
                }
            }
            else if (e.Row.RowType.Equals(DataControlRowType.Footer))
            {
                AwNumeric txtsummoney_credit = (AwNumeric)e.Row.FindControl("txtsummoney_credit");
                txtsummoney_credit.Value = ViewState["sumall"].ToString();
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

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string strMessage = string.Empty;
            string strCheck = string.Empty;
            string strScript = string.Empty;
            string strUpdatedBy = Session["username"].ToString();
            string strYear = ((DataSet)Application["xmlconfig"]).Tables["default"].Rows[0]["yearnow"].ToString();
            Label lblperson_code = (Label)GridView1.Rows[e.RowIndex].FindControl("lblperson_code");
            CheckBox CheckBox = (CheckBox)GridView1.Rows[e.RowIndex].FindControl("CheckBox");
            cPerson oPerson = new cPerson();
            try
            {
                if (!oPerson.SP_PERSON_ITEM_DEL(lblperson_code.Text, strYear, txtitem_code.Text, "N", strUpdatedBy, ref strMessage))
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
            BindGridViewEdit();
        }

        protected void imgSaveOnly_Click(object sender, ImageClickEventArgs e)
        {
            if (RadioButtonList1.SelectedValue.Equals("A"))
            {
                BindGridView();
            }
            else if (RadioButtonList1.SelectedValue.Equals("E"))
            {

                int intX = 0;
                for (int intCount = 0; intCount <= (GridView1.Rows.Count - 1); intCount++)
                {
                    GridViewRow row = GridView1.Rows[intCount];
                    CheckBox chkImportID = (CheckBox)row.FindControl("CheckBox1");
                    if (chkImportID.Checked)
                    {
                        intX += intX + 1;
                    }
                }
                if (intX == 0)
                {
                    string strScript = "alert('กรุณาเลือกข้อมูล');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowError", strScript, true);
                }
                else
                {
                    saveData();
                    BindGridViewEdit();
                }
            }
        }

        protected void ClearData()
        {
            InitcboPerson_group();
            cboPerson_group.SelectedIndex = 0;
            InitcboDirector();
            cboDirector.SelectedIndex = 0;
            InitcboUnit();
            cboUnit.SelectedIndex = 0;


            //imgSaveOnly.Visible = false;
            imgCancel.Visible = true;
            RadioButtonList1.SelectedValue = "A";
            txtitem_code.Text = string.Empty;
            txtitem_name.Text = string.Empty;

            RadioButtonList1.Enabled = true;
            cboPerson_group.Enabled = true;
            cboDirector.Enabled = true;
            cboUnit.Enabled = true;
            //cboYear.Enabled = true;
            //cboPay_Month.Enabled = true;
            //cboPay_Year.Enabled = true;
            txtitem_code.Enabled = true;
            txtitem_name.Enabled = true;
            imgList_item.Enabled = true;
            imgClear_item.Enabled = true;

            txtitem_code2.Enabled = true;
            txtitem_name2.Enabled = true;
            imgList_item2.Enabled = true;
            imgClear_item2.Enabled = true;


            panelSeek.Visible = true;
            panelSeek2.Visible = false;

            txtitem_code2.Text = "";
            txtitem_name2.Text = "";
            imgImport.Visible = true;
            imgSaveOnly2.Visible = false;

            GridView1.Visible = false;
            // GridView2.Visible = false;
            imgExcel.Visible = true;
            imgFind.Visible = false;
            imgSaveOnly.Visible = true;
            RequiredFieldValidator3.Enabled = false;

            cboItem_type.Enabled = true;
            cboItem_type.SelectedIndex = 0;

            lnkExcelFile.NavigateUrl = string.Empty;
            imgExcel.Src = "~/images/icon_exceldisable.gif";

        }

        protected void imgCancel_Click(object sender, ImageClickEventArgs e)
        {
            ClearData();
        }

        protected void cboDirector_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboUnit();
        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitcboDirector();
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lnkExcelFile.NavigateUrl = string.Empty;
            imgExcel.Src = "~/images/icon_exceldisable.gif";
            GridView1.Visible = false;
            //  GridView2.Visible = false;
            BindGridViewTmp();
            if (RadioButtonList1.SelectedValue.Equals("I"))
            {
                panelSeek.Visible = false;
                panelSeek2.Visible = true;
                imgSaveOnly2.Visible = false;
                GridView1.Visible = true;
                //   GridView2.Visible = false;
                imgExcel.Visible = true;
                imgFind.Visible = false;
                RequiredFieldValidator3.Enabled = false;
            }
            else if (RadioButtonList1.SelectedValue.Equals("E"))
            {
                panelSeek2.Visible = false;
                panelSeek.Visible = true;
                GridView1.Visible = true;
                //  GridView2.Visible = false;
                imgExcel.Visible = false;
                imgFind.Visible = true;
                imgSaveOnly.Visible = false;
                RequiredFieldValidator3.Enabled = true;
            }
            else if (RadioButtonList1.SelectedValue.Equals("A"))
            {
                panelSeek2.Visible = false;
                panelSeek.Visible = true;
                GridView1.Visible = true;
                //    GridView2.Visible = false;
                imgExcel.Visible = true;
                imgFind.Visible = false;
                imgSaveOnly.Visible = true;
                RequiredFieldValidator3.Enabled = false;
            }
        }

        protected void imgImport_Click(object sender, ImageClickEventArgs e)
        {
            UpdatePanel oUpdatePanel;
            string strScript = string.Empty;
            strScript = "OpenPopUp('500px','200px','80%','อัพโหลด Excel File รายรับ' ,'excel_import_upload.aspx?" +
                                                    "&ctrl1=" + hddGUID.ClientID +
                                                    "&item_code='+document.forms[0]." + strPrefixCtr + "txtitem_code2.value+'&show=1', '1');";
            oUpdatePanel = (UpdatePanel)this.Master.FindControl("updatePanel1");
            ScriptManager.RegisterClientScriptBlock(oUpdatePanel, oUpdatePanel.GetType(), "MessageBox", strScript, true);
        }

        protected void LinkButton1_Click1(object sender, EventArgs e)
        {
            BindGridView4();
        }

        protected void imgSaveOnly2_Click(object sender, ImageClickEventArgs e)
        {

            if (saveData())
            {
                ClearData();
            }
        }

        protected void imgFind_Click(object sender, ImageClickEventArgs e)
        {
            BindGridViewEdit();
        }

        private void BindGridViewEdit()
        {
            cPerson oPerson = new cPerson();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            string stritem_code = txtitem_code.Text;
            try
            {
                strCriteria = " and  item_code= '" + stritem_code + "' ";
                strCriteria += " and  item_type= '" + cboItem_type.SelectedValue + "' ";

                strCriteria += " and person_group_code IN (" + PersonGroupList + ") ";
                if (DirectorLock == "Y")
                {
                    strCriteria += " and substring(director_code,4,2) = substring('" + DirectorCode + "',4,2) ";
                }
                if (!oPerson.SP_PERSON_ITEM_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    ds.Tables[0].Columns.Add("money_credit");

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (cboItem_type.SelectedValue == "C")
                        {
                            ds.Tables[0].Rows[i]["money_credit"] = ds.Tables[0].Rows[i]["item_credit"];
                        }
                        else
                        {
                            ds.Tables[0].Rows[i]["money_credit"] = ds.Tables[0].Rows[i]["item_debit"];
                        }
                    }

                    ds.Tables[0].DefaultView.Sort = ViewState["sort"] + " " + ViewState["direction"];
                    ds.Tables[0].AcceptChanges();
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();

                    imgSaveOnly.Visible = true;
                    imgCancel.Visible = true;


                    if (GridView1.Rows.Count > 0)
                    {
                        RadioButtonList1.Enabled = false;
                        txtitem_code.Enabled = false;
                        txtitem_name.Enabled = false;
                        imgList_item.Enabled = false;
                        imgClear_item.Enabled = false;

                        cboDirector.Enabled = false;
                        cboUnit.Enabled = false;
                        cboPerson_group.Enabled = false;
                        cboItem_type.Enabled = false;


                        imgSaveOnly.Visible = true;
                        imgCancel.Visible = true;

                        GridView1.Visible = true;

                    }
                    else
                    {

                        imgSaveOnly.Visible = false;
                        imgCancel.Visible = true;


                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oPerson.Dispose();
                ds.Dispose();
            }
        }

        private void BindGridViewTmp()
        {
            cPayment oPayment = new cPayment();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            int i;
            try
            {
                strCriteria = " and  2=1 ";
                if (!oPayment.SP_PAYMENT_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    ds.Tables[0].DefaultView.Sort = ViewState["sort"] + " " + ViewState["direction"];
                    ds.Tables[0].AcceptChanges();
                    //   GridView2.DataSource = ds.Tables[0];
                    //  GridView2.DataBind();

                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();

                    GridView1.Visible = false;
                    //     GridView2.Visible = false;

                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oPayment.Dispose();
                ds.Dispose();
            }
        }

        protected void cboItem_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtitem_code.Text = "";
            txtitem_name.Text = "";
        }


    }
}