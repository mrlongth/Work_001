using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using myDLL;

namespace myEFrom.App_Control.material
{
    public partial class material_control : PageBase
    {

        #region private data
        //private bool[] blnAccessRight = new bool[5] { false, false, false, false, false };
        //private string strPrefixCtr_main = "ctl00$ContentPlaceHolder1$";
        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            base.PermissionURL = "~/App_Control/material/material_list.aspx";
        }

        
        protected void Page_Load(object sender, System.EventArgs e)
        {
            lblError.Text = "";
            if (!IsPostBack)
            {
                imgSaveOnly.Attributes.Add("onMouseOver", "src='../../images/controls/save2.jpg'");
                imgSaveOnly.Attributes.Add("onMouseOut", "src='../../images/controls/save.jpg'");
                Session["menupopup_name"] = "";
                ViewState["sort"] = "material_code";
                ViewState["direction"] = "ASC";
                #region set QueryString
                if (Request.QueryString["material_id"] != null)
                {
                    ViewState["material_id"] = Request.QueryString["material_id"].ToString();
                }
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

                //imgList_count.Attributes.Add("onclick", "OpenPopUp('750px','400px','93%','ค้นหาข้อมูลเช็ค' ,'../lov/count_lov.aspx?count_id='+document.forms[0]." + strPrefixCtr_main + "txtcount_id.value+'" +
                //"&count_name='+document.forms[0]." + strPrefixCtr_main + "txtcount_name.value+'" +
                //"&ctrl1=" + txtcount_id.ClientID + "&ctrl2=" + txtcount_name.ClientID + "&show=2', '2');return false;");

                //imgClear_count.Attributes.Add("onclick", "document.forms[0]." + strPrefixCtr_main + "txtcount_id.value='';" +
                //                        "document.forms[0]." + strPrefixCtr_main + "txtcount_name.value=''; return false;");


                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    ViewState["page"] = Request.QueryString["page"];
                    txtmaterial_code.ReadOnly = true;
                    txtmaterial_code.CssClass = "textboxdis";
                }
                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    setData();
                    txtmaterial_code.ReadOnly = true;
                    txtmaterial_code.CssClass = "textboxdis";
                }

                #endregion

            }
        }

        #region private function

        private void setData()
        {
            c3dMaterial obj3dMaterial = new c3dMaterial();
            string strCriteria = string.Empty;
            string strmaterial_code = string.Empty,
                strmaterial_name = string.Empty,
                strUpdatedBy = string.Empty,
                strUpdatedDate = string.Empty,
                strcount_id = string.Empty,
                strcount_name = string.Empty;
            double pstandard_price, plast_price;
            try
            {
                strCriteria = " and material_id = '" + ViewState["material_id"].ToString() + "' ";
                var dt = obj3dMaterial.SP_MATERIAL_SEL(strCriteria);
                if (dt.Rows.Count > 0)
                {
                    #region get Data
                    strmaterial_code = dt.Rows[0]["material_code"].ToString();
                    strmaterial_name = dt.Rows[0]["material_name"].ToString();
                    strcount_id = dt.Rows[0]["count_id"].ToString();
                    strcount_name = dt.Rows[0]["count_name"].ToString();
                    pstandard_price = Helper.CDbl(dt.Rows[0]["standard_price"]);
                    plast_price = Helper.CDbl(dt.Rows[0]["last_price"]);
                    strUpdatedBy = dt.Rows[0]["c_updated_by"].ToString();
                    strUpdatedDate = dt.Rows[0]["d_updated_date"].ToString();
                    #endregion

                    #region set Control
                    txtmaterial_code.Text = strmaterial_code;
                    txtmaterial_name.Text = strmaterial_name;
                    txtcount_id.Text = strcount_id;
                    txtcount_name.Text = strcount_name;
                    txtstandard_price.Value = pstandard_price;
                    txtlast_price.Value = plast_price;
                    txtUpdatedBy.Text = strUpdatedBy;
                    txtUpdatedDate.Text = strUpdatedDate;
                    #endregion
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        private bool saveData()
        {
            bool blnResult = false;
            string strmaterial_code = string.Empty,
                strmaterial_name = string.Empty,
                strUserName = string.Empty;

            double pstandard_price, plast_price;
            string strScript = string.Empty;
            int intmaterial_id = Helper.CInt(ViewState["material_id"]);
            int intcount_id;
            c3dMaterial obj3dMaterial = new c3dMaterial();
            DataSet ds = new DataSet();
            try
            {
                #region set Data
                strmaterial_code = txtmaterial_code.Text;
                strmaterial_name = txtmaterial_name.Text;
                intcount_id = Helper.CInt(txtcount_id.Text);
                pstandard_price = Helper.CDbl(txtstandard_price.Value);
                plast_price = Helper.CDbl(txtlast_price.Value);
                strUserName = Session["username"].ToString();

                #endregion

                if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    blnResult = obj3dMaterial.SP_MATERIAL_UPD(intmaterial_id, strmaterial_code, strmaterial_name, intcount_id, pstandard_price, plast_price, "E", strUserName);
                }
                else
                {
                    #region insert
                    if (obj3dMaterial.SP_MATERIAL_INS(ref intmaterial_id,ref strmaterial_code, strmaterial_name, intcount_id, pstandard_price, plast_price, "E", strUserName))
                    {
                        ViewState["material_id"] = intmaterial_id;
                        blnResult = true;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate") && ex.Message.Contains("IX_material_name"))
                {
                    strScript = "alert(\"ไม่สามารถแก้ไขข้อมูล เนื่องจากข้อมูล " + strmaterial_name.Trim() + "  ซ้ำ\");\n";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "frMainPage", strScript, true);
                }
                else
                {
                    lblError.Text = ex.Message.ToString();
                }
            }
            finally
            {
                obj3dMaterial.Dispose();
            }
            return blnResult;
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
        }
        #endregion

        private void imgSaveOnly_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (saveData())
            {
                if (ViewState["mode"].ToString().ToLower().Equals("add"))
                {
                    txtmaterial_code.Text = string.Empty;
                    txtmaterial_name.Text = string.Empty;
                    txtcount_id.Text = string.Empty;
                    txtcount_name.Text = string.Empty;
                    txtlast_price.Value = 0;
                    txtstandard_price.Value = 0;                    
                    txtmaterial_name.Focus();
                    string strScript1 = "RefreshMain('" + ViewState["page"].ToString() + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
                }
                else if (ViewState["mode"].ToString().ToLower().Equals("edit"))
                {
                    string strScript1 = "ClosePopUpListPost('" + ViewState["page"].ToString() + "','1');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenPage", strScript1, true);
                }
                MsgBox("บันทึกข้อมูลสมบูรณ์");
            }
        }    
    }
}