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

namespace myWeb.App_Control.news
{
    public partial class news_show_view : PageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["new_id"] != null)
                {
                    ViewState["new_id"] = Request.QueryString["new_id"].ToString();
                }
                BindData(int.Parse(ViewState["new_id"].ToString()));
            }

        }

        #region private function


        private void BindData(int nNew_id)
        {
            cNews oNews = new cNews();
            DataSet ds = new DataSet();
            string strMessage = string.Empty;
            string strCriteria = string.Empty;
            strCriteria = strCriteria + "  And  (new_id =" + nNew_id + ") ";
            try
            {
                if (!oNews.SP_NEW_SEL(strCriteria, ref ds, ref strMessage))
                {
                    lblError.Text = strMessage;
                }
                else
                {
                    ltlNew_title.Text = ds.Tables[0].Rows[0]["new_title"].ToString();
                    ltlNew_date.Text = ds.Tables[0].Rows[0]["d_created_date"].ToString();
                    //N = Normal , P = Pin , Q = Quick
                    if (ds.Tables[0].Rows[0]["new_status"].ToString() == "N")
                    {
                        ltlNew_status.Text = "ข่าวทั่วไป";
                    }
                    else if (ds.Tables[0].Rows[0]["new_status"].ToString() == "Q")
                    {
                        ltlNew_status.Text = "ข่าวด่วน";
                        ltlNew_status.ForeColor = System.Drawing.Color.Red;
                    }
                    else if (ds.Tables[0].Rows[0]["new_status"].ToString() == "P")
                    {
                        ltlNew_status.Text = "ปักหมุด";
                    }

                    ltlNew_des.Text = ds.Tables[0].Rows[0]["new_des"].ToString();
                    if (ds.Tables[0].Rows[0]["new_des"].ToString() != "")
                    {
                        lblNew_file_name.Text = ds.Tables[0].Rows[0]["new_file_name"].ToString();
                        lblNew_file_name.NavigateUrl = "";
                    }
                    lblNew_file_name.NavigateUrl = "~/new_attach/" + ds.Tables[0].Rows[0]["new_file_name"].ToString(); ;
                    ltlNew_by.Text = ds.Tables[0].Rows[0]["c_created_by"].ToString();

                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
            finally
            {
                oNews.Dispose();
                ds.Dispose();
            }
        }

        #endregion

    }
}
