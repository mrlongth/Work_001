using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using myDLL;
using DevExpress.Web.ASPxRoundPanel;

namespace myWeb
{
    public class GlobalPageBase : Page
    {

        #region Public Fields

        public bool _boolResult;
        public string _strMessage = string.Empty;
        public enum Mode { SEARCH, NEW, EDIT, VIEW };
        public string _strCriteria;


        #endregion

        #region Property General


        public string IsLogin
        {
            get
            {
                if (Session["IsLogin"] == null)
                {
                    Session["IsLogin"] = "N";
                }
                return Session["IsLogin"].ToString();
            }
            set
            {
                Session["IsLogin"] = value;
            }
        }

        public string PageTitle
        {
            get
            {
                if (ViewState["PageTitle"] == null)
                {
                    ViewState["PageTitle"] = string.Format("{0} ({1})", string.Empty, ProgramVersion);
                }
                return ViewState["PageTitle"].ToString();
            }
            set
            {
                ViewState["PageTitle"] = value;
            }
        }

        public string PageDes
        {
            get
            {
                if (ViewState["PageDes"] == null)
                {
                    ViewState["PageDes"] = string.Format("{0} ({1})", string.Empty, ProgramVersion);
                }
                return ViewState["PageDes"].ToString();
            }
            set
            {
                ViewState["PageDes"] = value;
            }
        }



        private string ProgramVersion
        {
            get
            {
                try
                {
                    ViewState["ProgramVersion"] = ConfigurationManager.AppSettings["ProgramVersion"].ToString();
                }
                catch
                {
                    ViewState["ProgramVersion"] = "ระบบบริหารงานบุคคลมหาวิทยาลัยแม่โจ้";
                }
                return ViewState["ProgramVersion"].ToString();
            }
            set
            {
                ViewState["ProgramVersion"] = value;
            }
        }


        public string PersonBudgetType
        {
            get
            {
                if (Session["PersonBudgetType"] == null)
                {
                    return "";
                }
                else
                {
                    return Session["PersonBudgetType"].ToString();
                }
            }
            set
            {
                Session["PersonBudgetType"] = value;
            }
        }



        public string PersonID
        {
            get
            {
                if (Session["PersonID"] == null)
                {
                    return "";
                }
                else
                {
                    return Session["PersonID"].ToString();
                }
            }
            set
            {
                Session["PersonID"] = value;
            }
        }

        public string PersonCode
        {
            get
            {
                if (Session["PersonCode"] == null)
                {
                    return "";
                }
                else
                {
                    return Session["PersonCode"].ToString();
                }
            }
            set
            {
                Session["PersonCode"] = value;
            }
        }

        public string PersonGroupCode
        {
            get
            {
                if (Session["PersonGroupCode"] == null)
                {
                    return "";
                }
                else
                {
                    return Session["PersonGroupCode"].ToString();
                }
            }
            set
            {
                Session["PersonGroupCode"] = value;
            }
        }


        public string PersonUserName
        {
            get
            {
                if (Session["PersonUserName"] == null)
                {
                    return "";
                }
                else
                {
                    return Session["PersonUserName"].ToString();
                }
            }
            set
            {
                Session["PersonUserName"] = value;
            }
        }

        public string PersonFullName
        {
            get
            {
                if (Session["PersonFullName"] == null)
                {
                    return "";
                }
                else
                {
                    return Session["PersonFullName"].ToString();
                }
            }
            set
            {
                Session["PersonFullName"] = value;
            }
        }

        public string PersonBudgetPlanCode
        {
            get
            {
                if (Session["PersonBudgetPlanCode"] == null)
                {
                    return "";
                }
                else
                {
                    return Session["PersonBudgetPlanCode"].ToString();
                }
            }
            set
            {
                Session["PersonBudgetPlanCode"] = value;
            }
        }

        #endregion

        public void MsgBox(string strMessage)
        {
            UpdatePanel oUpdatePanel;
            string strScript = string.Empty;
            strScript = "alert('" + strMessage + "');";
            oUpdatePanel = (UpdatePanel)this.Master.FindControl("updatePanel1");
            ScriptManager.RegisterClientScriptBlock(oUpdatePanel, oUpdatePanel.GetType(), "MessageBox", strScript, true);
        }

        protected override void OnPreLoad(System.EventArgs e)
        {
            base.OnPreLoad(e);
            if (!IsPostBack)
            {
                if (Master != null)
                {
                    if (Master.FindControl("ASPxRoundPanel1") != null)
                    {
                        ((ASPxRoundPanel)Master.FindControl("ASPxRoundPanel1")).HeaderText = PageDes;
                    }
                }
            }
            if (Session["PersonUserName"] == null || Session["PersonUserName"].ToString() == "0")
            {
                Response.Redirect("~/Default.aspx?op=NotAccess");
            }
        }

        public void SetLabel(Control control, String old_str, String new_str)
        {
            foreach (Control ctrl in control.Controls)
            {
                if (ctrl is Label)
                {
                    if (((Label)ctrl).Text.Contains(old_str))
                    {
                        ((Label)ctrl).Text = ((Label)ctrl).Text.Replace(old_str, new_str);
                    }
                }
                else if (ctrl is Literal)
                {
                    if (((Literal)ctrl).Text.Contains(old_str))
                    {
                        ((Literal)ctrl).Text = ((Literal)ctrl).Text.Replace(old_str, new_str);
                    }
                }
                else if (ctrl is LinkButton)
                {
                    if (((LinkButton)ctrl).Text.Contains(old_str))
                    {
                        ((LinkButton)ctrl).Text = ((LinkButton)ctrl).Text.Replace(old_str, new_str);
                    }
                }
                else if (ctrl is HyperLink)
                {
                    if (((HyperLink)ctrl).Text.Contains(old_str))
                    {
                        ((HyperLink)ctrl).Text = ((HyperLink)ctrl).Text.Replace(old_str, new_str);
                    }
                }

                else
                {
                    if (ctrl.Controls.Count > 0)
                    {
                        SetLabel(ctrl, old_str, new_str);
                    }
                }
            }
        }


    }
}
