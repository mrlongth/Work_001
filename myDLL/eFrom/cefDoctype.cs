using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace myDLL
{
    public class cefDoctype : IDisposable
    {
        private string _strConn = string.Empty;
        public string ConnectionString
        {
            get
            {
                return _strConn;
            }
            set
            {
                if (value == string.Empty)
                {
                    _strConn = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"];
                }
                else
                {
                    _strConn = value;
                }
            }
        }

        public cefDoctype()
        {
            //
            // TODO: Add constructor logic here
            //
            _strConn = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"];
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #region SP_DOCTYPE_SEL
        public DataTable SP_DOCTYPE_SEL(string strCriteria)
        {
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            strCriteria += strCriteria.ToLower().Contains("order by") ? string.Empty : " Order by ef_doctype_name";
            try
            {
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = "Select * from ef_doctype where 1=1 " + strCriteria;
                oAdapter = new SqlDataAdapter(oCommand);
                oAdapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oConn.Close();
                oCommand.Dispose();
                oConn.Dispose();
            }
            return dt;
        }
        #endregion

        #region SP_DOCTYPE_INS
        public bool SP_DOCTYPE_INS(ref int pef_doctype_code, string pef_doctype_name , string pcreated_by)
        {
            bool blnResult = false;
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            try
            {
                string strSql = "Insert into ef_doctype (ef_doctype_name,c_created_by,d_created_date) values ( " +
                                "'" + pef_doctype_name + "'," +
                                "'" + pcreated_by + "'," +
                                "'" +  cCommon.GetDateTimeNow() + "')";
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = strSql;
                oCommand.ExecuteNonQuery();
                var dt = SP_DOCTYPE_SEL(" and ef_doctype_name='" + pef_doctype_name + "'");
                pef_doctype_code = Helper.CInt(dt.Rows[0]["ef_doctype_code"]);
                blnResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oConn.Close();
                oCommand.Dispose();
                oConn.Dispose();
            }
            return blnResult;
        }
        #endregion

        #region SP_DOCTYPE_UPD
        public bool SP_DOCTYPE_UPD(int pef_doctype_code, string pef_doctype_name, string pupdated_by)
        {
            bool blnResult = false;
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            try
            {
                string strSql = "Update ef_doctype Set " +
                                " ef_doctype_name = '" + pef_doctype_name + "'," +
                                " c_updated_by= '" + pupdated_by + "'," +
                                " d_updated_date= '" +  cCommon.GetDateTimeNow() + "'" +
                                " Where ef_doctype_code = " + pef_doctype_code;                               
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = strSql;
                oCommand.ExecuteNonQuery();
                blnResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oConn.Close();
                oCommand.Dispose();
                oConn.Dispose();
            }
            return blnResult;
        }
        #endregion

        #region SP_DOCTYPE_DEL
        public bool SP_DOCTYPE_DEL(int pef_doctype_code)
        {
            bool blnResult = false;
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            try
            {
                string strSql = "Delete from ef_doctype Where ef_doctype_code = " + pef_doctype_code;
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = strSql;
                oCommand.ExecuteNonQuery();
                blnResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oConn.Close();
                oCommand.Dispose();
                oConn.Dispose();
            }
            return blnResult;
        }
        #endregion


        #region IDisposable Members

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
