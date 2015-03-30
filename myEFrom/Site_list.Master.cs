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


namespace myEFrom
{
    public partial class Site_list : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AddJavaScriptInclude();
            //Thread.Sleep(1000);
            ASPxMenu1.Visible = false;
            //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th-TH");            

            if (!IsPostBack)
            {
                GenMenu();
                if (Session["PersonFullName"] == null)
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
                UserLabel.Text = Session["PersonFullName"].ToString();
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
            cefUser objEfUser = new cefUser();
            DataTable dtList = new DataTable();
            DataTable dt = new DataTable();
            DataRow[] drMenu;
            DataRow[] drMenu_sub;
            try
            {
                string strCriteria = string.Empty;
                strCriteria = " And MenuParent = 0 ";
                strCriteria += " And UserGroupCode= '" + Session["UserGroupCode"] + "'";
                strCriteria += " And CanView= 'Y' ";
                strCriteria += " Order by MenuOrder ";
                dtList = objEfUser.SP_USER_GROUP_MENU_SEL(strCriteria);

                strCriteria = " And UserGroupCode='" + Session["UserGroupCode"] + "' ";
                strCriteria += " And CanView= 'Y' ";
                strCriteria += " Order by MenuOrder ";
                dt = objEfUser.SP_USER_GROUP_MENU_SEL(strCriteria);
                
                ASPxMenu1.Items.Clear();
                for (int i = 0; i < dtList.Rows.Count; i++)
                {
                    var menuItem = new DevExpress.Web.ASPxMenu.MenuItem();
                    menuItem.Text = dtList.Rows[i]["MenuName"].ToString();
                    menuItem.NavigateUrl = dtList.Rows[i]["MenuNavigationUrl"].ToString();
                    menuItem.Image.Url = Helper.CStr(dtList.Rows[i]["MenuImageUrl"].ToString());
                    drMenu = dt.Select("MenuParent=" + dtList.Rows[i]["MenuID"].ToString());
                    for (int j = 0; j < drMenu.Length; j++)
                    {
                       var menuItem_sub = new DevExpress.Web.ASPxMenu.MenuItem();
                        menuItem_sub.Text = drMenu[j]["MenuName"].ToString();
                        menuItem_sub.NavigateUrl = drMenu[j]["MenuNavigationUrl"].ToString();
                        menuItem_sub.Image.Url = drMenu[j]["MenuImageUrl"].ToString();
                        drMenu_sub = dt.Select("MenuParent=" + drMenu[j]["MenuID"].ToString());
                        for (int k = 0; k < drMenu_sub.Length; k++)
                        {
                            var menuItem_sub2 = new DevExpress.Web.ASPxMenu.MenuItem();
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
                objEfUser.Dispose();
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
            script.Attributes["src"] = (cCommon.AbsoluteWebRoot + "js/jquery-ui-1.8.22.custom.js");
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
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
            ViewState.Clear();
            FormsAuthentication.SignOut();
            Session["UserID"] = "0";
            Response.Redirect("~/Default.aspx");
        }

    }
}
