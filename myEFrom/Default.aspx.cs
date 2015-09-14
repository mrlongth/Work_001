using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using myDLL;

namespace myEFrom
{
    public partial class Default : PageBase
    {

        protected string ReturnUrl
        {
            get
            {
                if (ViewState["ReturnUrl"] == null)
                    ViewState["ReturnUrl"] = string.Empty;
                if (Request.QueryString["ReturnUrl"] != null)
                    ViewState["ReturnUrl"] = Request.QueryString["ReturnUrl"].ToString();
                return ViewState["ReturnUrl"].ToString();
            }
            set
            {
                ViewState["ReturnUrl"] = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            lblError.Text = string.Empty;
            if (this.PersonCode == null || this.PersonCode != "")
            {
                Response.Redirect("~/Menu_control.aspx");
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            string strUser = txtUser.Text.Trim();
            string strPass = txtPass.Text.Trim();
            string strMessage = string.Empty;
            string strPersonID = string.Empty;

            strUser += cboDomain.SelectedItem.Text;
            strMessage = string.Empty;
            strPersonID = MjuVerifyUser(strUser, strPass, ref strMessage);
            //strPersonID = "3501500288110";
            if (strPersonID.Length > 0)
            {
                if (SetPersonUserProfile(strPersonID, ref strMessage))
                {
                    SetAuthenticate(strPersonID, base.UserGroupCode, chkRemeber.Checked);
                    //Session["username"] = (Helper.CStr(dt.Rows[0]["person_thai_name"])).Substring(0, 10);                               
                    if (string.IsNullOrEmpty(this.ReturnUrl))
                    {
                        this.ReturnUrl = "~/Menu_control.aspx";
                    }
                    Response.Redirect(this.ReturnUrl);
                }
            }
            else
            {
                if (strMessage.Length > 0)
                {
                    MsgBox("ไม่สามารถ Login เข้าสู่ระบบได้ เนื่องจาก" + strMessage);
                }
                else
                {
                    MsgBox("ไม่สามารถ Login เข้าสู่ระบบได้ เนื่องจาก Username หรือ Password ผิดพลาด");
                }
            }
        }

        protected string MjuVerifyUser(string strUserName, string strPassword, ref string _strError)
        {
            string PersonID = string.Empty;
            try
            {
                var oServiceClient = new myEFrom.th.ac.mju.ouop.verifyuser();
                PersonID = oServiceClient.verifyuserND(strUserName, strPassword);
                //PersonID = "3501400557381";
                //PersonID = "3509900876521";
            }
            catch (Exception ex)
            {
                _strError = ex.Message;
            }
            return PersonID;
        }

        private void SetAuthenticate(string pUserName, string pUserGroup, bool pPersistantCookie)
        {

            DateTime dateExpire = DateTime.Now.AddMinutes(30);
            if (pPersistantCookie)
                dateExpire = DateTime.Now.AddYears(1);
            // Create a new ticket used for authentication
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
               1, // Ticket version
               pUserName, // Username associated with ticket
               DateTime.Now, // Date/time issued
               dateExpire, // Date/time to expire
               true, // "true" for a persistent user cookie
               pUserGroup, // User-data, in this case the roles
               FormsAuthentication.FormsCookiePath);// Path cookie valid for
            // Encrypt the cookie using the machine key for secure transport
            string hash = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(
               FormsAuthentication.FormsCookieName, // Name of auth cookie
               hash); // Hashed ticket

            // Set the cookie's expiration time to the tickets expiration time
            if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;

            // Add the cookie to the list for outgoing response
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);


        }


    }
}
