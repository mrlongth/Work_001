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
using System.Threading;
using Aware.DAL;
using myDLL;


namespace myWeb
{
    public partial class Site_list : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AddJavaScriptInclude();
            //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th-TH");
            //Thread.Sleep(1000);
            ASPxMenu1.Visible = false;
            if (!IsPostBack)
            {
                GenMenu();
                if (Session["username"] == null)
                {
                    string strScript = "window.parent.document.location.href='../../Default.aspx';";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ClosePage", strScript, true);
                    return;
                }

                imgClose1.Attributes.Add("onMouseOver", "src = '../../images/Delete2.png'");
                imgClose1.Attributes.Add("onMouseOut", "src = '../../images/Delete.png'");

                imgClose2.Attributes.Add("onMouseOver", "src = '../../images/Delete2.png'");
                imgClose2.Attributes.Add("onMouseOut", "src = '../../images/Delete.png'");

                imgClose3.Attributes.Add("onMouseOver", "src = '../../images/Delete2.png'");
                imgClose3.Attributes.Add("onMouseOut", "src = '../../images/Delete.png'");

                imgClose1.Attributes.Add("onclick", "ClosePopUp('1');return false;");
                imgClose2.Attributes.Add("onclick", "ClosePopUp('2');return false;");
                imgClose3.Attributes.Add("onclick", "ClosePopUp('3');return false;");
            }
            try
            {
                panelShow1.Style.Add("display", "none");
                btnShow.Style.Add("display", "none");
                panelShow2.Style.Add("display", "none");
                btnShow2.Style.Add("display", "none");
                panelShow3.Style.Add("display", "none");
                btnShow3.Style.Add("display", "none");
                UserLabel.Text = Session["username"].ToString();
                //ASPxRoundPanel1.HeaderText = Session["menu_name"].ToString();
                ASPxMenu1.Visible = true;
            }
            catch
            {
                Response.Redirect("~/Default.aspx");
            }
        }

        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.ASPxMenu.MenuItemEventArgs e)
        {
             Session["menu_name"] = e.Item.Text;
           

        }

        protected void GenMenu()
        {
            cUser_menu objUserMenu = new cUser_menu();
            DataSet ds = new DataSet();
            DataTable dtList = new DataTable();
            DataTable dt = new DataTable();
            DataRow[] drMenu;
            DataRow[] drMenu_sub;
            try
            {
                string strCriteria = string.Empty;
                string strMassege = string.Empty;
                strCriteria = " And MenuParent = 0 ";
                strCriteria += " And Menu_Status='Y' ";
                strCriteria += " And UserID=" + Session["UserID"].ToString();
                strCriteria += " And CanView= 'Y' ";
                strCriteria += " Order by MenuOrder ";
                objUserMenu.SP_USER_MENU_SEL(strCriteria, ref ds, ref strMassege);
                dtList = ds.Tables[0];

                strCriteria = " And UserID=" + Session["UserID"].ToString();
                strCriteria += " And Menu_Status='Y' ";
                strCriteria += " And CanView= 'Y' ";
                strCriteria += " Order by MenuOrder ";
                objUserMenu.SP_USER_MENU_SEL(strCriteria, ref ds, ref strMassege);
                dt = ds.Tables[0];

                ASPxMenu1.Items.Clear();
                for (int i = 0; i < dtList.Rows.Count; i++)
                {
                    DevExpress.Web.ASPxMenu.MenuItem menuItem = new DevExpress.Web.ASPxMenu.MenuItem();
                    menuItem.Text = dtList.Rows[i]["MenuName"].ToString();
                    menuItem.NavigateUrl = dtList.Rows[i]["MenuNavigationUrl"].ToString();
                    menuItem.Image.Url =Helper.CStr(dtList.Rows[i]["MenuImageUrl"].ToString());
                    drMenu = dt.Select("MenuParent=" + dtList.Rows[i]["MenuID"].ToString());
                    for (int j = 0; j < drMenu.Length; j++)
                    {
                        DevExpress.Web.ASPxMenu.MenuItem menuItem_sub = new DevExpress.Web.ASPxMenu.MenuItem();
                        menuItem_sub.Text = drMenu[j]["MenuName"].ToString();
                        menuItem_sub.NavigateUrl = drMenu[j]["MenuNavigationUrl"].ToString();
                        menuItem_sub.Image.Url  = drMenu[j]["MenuImageUrl"].ToString();
                        drMenu_sub = dt.Select("MenuParent=" + drMenu[j]["MenuID"].ToString());
                        for (int k = 0; k < drMenu_sub.Length; k++)
                        {
                            DevExpress.Web.ASPxMenu.MenuItem menuItem_sub2 = new DevExpress.Web.ASPxMenu.MenuItem();
                            menuItem_sub2.Text = drMenu_sub[k]["MenuName"].ToString();
                            menuItem_sub2.NavigateUrl = drMenu_sub[k]["MenuNavigationUrl"].ToString();
                            menuItem_sub2.Image.Url = drMenu_sub[k]["MenuImageUrl"].ToString();
                            menuItem_sub.Items.Add(menuItem_sub2);
                        }
                        menuItem.Items.Add(menuItem_sub);
                    }
                    ASPxMenu1.Items.Add(menuItem);
                }
            }
            catch (Exception ex)
            {
                MsgBox(ex.Message);
            }
            finally
            {
                dtList.Dispose();
                objUserMenu.Dispose();
            }
        }

        protected void AddJavaScriptInclude()
        {
            HtmlGenericControl script;


            script = new HtmlGenericControl("script");
            script.Attributes["type"] = "text/javascript";
            script.Attributes["src"] = (cCommon.AbsoluteWebRoot + "js/jquery-1.7.2.min.js");
            Page.Header.Controls.Add(script);

            script = new HtmlGenericControl("script");
            script.Attributes["type"] = "text/javascript";
            script.Attributes["src"] = (cCommon.AbsoluteWebRoot + "scripts/form.js");
            Page.Header.Controls.Add(script);

            script = new HtmlGenericControl("script");
            script.Attributes["type"] = "text/javascript";
            script.Attributes["src"] = (cCommon.AbsoluteWebRoot + "javascript/AwNumeric.js");
            Page.Header.Controls.Add(script);

            script = new HtmlGenericControl("script");
            script.Attributes["type"] = "text/javascript";
            script.Attributes["src"] = (cCommon.AbsoluteWebRoot + "javascript/AwTextBox.js");
            Page.Header.Controls.Add(script);

        }
    


        public void MsgBox(string strMessage)
        {
            string strScript = string.Empty;
            strScript = "alert('" + strMessage + "');";
            ScriptManager.RegisterClientScriptBlock(updatePanel1, updatePanel1.GetType(), "MessageBox", Helper.ReplaceScript(strScript), true);
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session["UserID"] = "0";
            Response.Redirect("~/Default.aspx");
        }

    }
}
