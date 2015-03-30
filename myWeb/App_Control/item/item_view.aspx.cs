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

namespace myWeb.App_Control.item
{
    public partial class item_view : PageBase
    {
        #region private data
        private string strConn = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"];
        private bool[] blnAccessRight = new bool[5] { false, false, false, false, false };
        #endregion
        protected void Page_Load(object sender, System.EventArgs e)
        {
            //if (Session["username"] == null)
            //{
            //    string strScript = "<script language=\"javascript\">\n self.opener.document.location.href=\"../../index.aspx\";\n self.close();\n</script>\n";
            //    this.RegisterStartupScript("close", strScript);
            //    return;
            //}
            lblError.Text = "";
            if (!IsPostBack)
            {
                Session["menupopup_name"] = "แสดงข้อมูลรายได้/ค่าใช้จ่าย";
                #region set QueryString
                if (Request.QueryString["item_code"] != null)
                {
                    ViewState["item_code"] = Request.QueryString["item_code"].ToString();
                }
                #endregion
                setData();
            }
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

        }
        #endregion

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
                cboPerson_group.Items.Add(new ListItem("---- ทั้งหมด ----", ""));
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

        private void setData()
        {
            cItem oItem = new cItem();
            DataSet ds = new DataSet();
            string strMessage = string.Empty, strCriteria = string.Empty;
            string stritem_code = string.Empty,
                stritem_name = string.Empty,
                stritem_year = string.Empty,
                stritem_type = string.Empty,
                stritem_group_code = string.Empty,
                stritem_group_name = string.Empty,
                strlot_code = string.Empty,
                strlot_name = string.Empty,
                strYear = string.Empty,
                strperson_group_code = string.Empty,
                strC_active = string.Empty,
                strCreatedBy = string.Empty,
                strUpdatedBy = string.Empty,
                strCreatedDate = string.Empty,
                strUpdatedDate = string.Empty,
                strcheque_code = string.Empty,
                strcheque_name = string.Empty;
            try
            {
                strCriteria = " and item_code = '" + ViewState["item_code"].ToString() + "' ";
                if (!oItem.SP_ITEM_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region get Data
                        strYear = ds.Tables[0].Rows[0]["item_year"].ToString();
                        stritem_code = ds.Tables[0].Rows[0]["item_code"].ToString();
                        stritem_name = ds.Tables[0].Rows[0]["item_name"].ToString();
                        stritem_type = ds.Tables[0].Rows[0]["item_type"].ToString();
                        stritem_group_code = ds.Tables[0].Rows[0]["item_group_code"].ToString();
                        stritem_group_name = ds.Tables[0].Rows[0]["item_group_name"].ToString();
                        strlot_code = ds.Tables[0].Rows[0]["lot_code"].ToString();
                        strlot_name = ds.Tables[0].Rows[0]["lot_name"].ToString();
                        strperson_group_code = ds.Tables[0].Rows[0]["person_group_code"].ToString();
                        strC_active = ds.Tables[0].Rows[0]["c_active"].ToString();
                        strCreatedBy = ds.Tables[0].Rows[0]["c_created_by"].ToString();
                        strUpdatedBy = ds.Tables[0].Rows[0]["c_updated_by"].ToString();
                        strCreatedDate = ds.Tables[0].Rows[0]["d_created_date"].ToString();
                        strUpdatedDate = ds.Tables[0].Rows[0]["d_updated_date"].ToString();
                        strcheque_code = ds.Tables[0].Rows[0]["cheque_code"].ToString();
                        strcheque_name = ds.Tables[0].Rows[0]["cheque_name"].ToString();

                        #endregion                    
                    }
                    txtitem_year.Text = strYear;
                    txtitem_code.Text = stritem_code;
                    txtitem_name.Text = stritem_name;
                    txtitem_group_code.Text = stritem_group_code;
                    txtitem_group_name.Text = stritem_group_name;
                    txtlot_code.Text = strlot_code;
                    txtlot_name.Text = strlot_name;
                    txtcheque_code.Text = strcheque_code;
                    txtcheque_name.Text = strcheque_name;

                    if (cboItem_type.Items.FindByValue(stritem_type) != null)
                    {
                        cboItem_type.SelectedIndex = -1;
                        cboItem_type.Items.FindByValue(stritem_type).Selected = true;
                    }

                    InitcboPerson_group();
                    if (cboPerson_group.Items.FindByValue(strperson_group_code) != null)
                    {
                        cboPerson_group.SelectedIndex = -1;
                        cboPerson_group.Items.FindByValue(strperson_group_code).Selected = true;
                    }

                    if (strC_active.Equals("Y"))
                    {
                        chkStatus.Checked = true;
                    }
                    else
                    {
                        chkStatus.Checked = false;
                    }

                    txtitem_code.ReadOnly = true;
                    txtitem_code.CssClass = "textboxdis";

                    cboItem_type.Enabled = false;
                    cboItem_type.CssClass = "textboxdis";

                    txtitem_code.ReadOnly = true;
                    txtitem_code.CssClass = "textboxdis";

                    txtitem_name.ReadOnly = true;
                    txtitem_name.CssClass = "textboxdis";

                    txtitem_group_code.ReadOnly = true;
                    txtitem_group_code.CssClass = "textboxdis";

                    txtitem_group_name.ReadOnly = true;
                    txtitem_group_name.CssClass = "textboxdis";

                    txtlot_code.ReadOnly = true;
                    txtlot_code.CssClass = "textboxdis";

                    txtlot_name.ReadOnly = true;
                    txtlot_name.CssClass = "textboxdis";


                    txtUpdatedBy.Text = strUpdatedBy;
                    txtUpdatedDate.Text = strUpdatedDate;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

    
    }
}