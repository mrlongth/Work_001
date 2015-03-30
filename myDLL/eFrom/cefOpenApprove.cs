using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace myDLL
{
    public class cefOpenApprove : IDisposable
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

        public cefOpenApprove()
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

        #region SP_OPEN_APPROVE_SEL
        public DataTable SP_OPEN_APPROVE_SEL(string strCriteria)
        {
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            strCriteria += strCriteria.ToLower().Contains("order by") ? string.Empty : " Order by approve_level";
            try
            {
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = "Select * from view_ef_open_approve where 1=1 " + strCriteria;
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

        #region SP_OPEN_APPROVE_INS
        public bool SP_OPEN_APPROVE_INS(
             int popen_code,
             int papprove_code,
             int approve_level,
             string pperson_manage_code,
             string pperson_manage_name,
             string pperson_approve_code,
             string pbudget_type)
        {
            bool blnResult = false;
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            try
            {
                string strSql = "Insert into ef_open_approve ([open_code],[approve_code],[approve_level],[person_manage_code],[person_manage_name],[person_approve_code],[budget_type]) values ( " +
                                "'" + popen_code + "'," +
                                "'" + papprove_code + "'," +
                                "'" + approve_level + "'," +
                                "'" + pperson_manage_code + "'," +
                                "'" + pperson_manage_name + "'," +
                                "'" + pperson_approve_code + "'," +
                                "'" + pbudget_type + "')";
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

        #region SP_OPEN_APPROVE_DEL_BY_OPEN_CODE
        public bool SP_OPEN_APPROVE_DEL_BY_OPEN_CODE(int popen_code, string pbudget_type)
        {
            bool blnResult = false;
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            try
            {
                string strSql = "delete from ef_open_approve where open_code = " + popen_code + " and budget_type='" + pbudget_type + "' ";
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
