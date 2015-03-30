using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace myDLL
{
    public class cefApprove : IDisposable
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

        public cefApprove()
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

        #region SP_APPROVE_SEL
        public DataTable SP_APPROVE_SEL(string strCriteria)
        {
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            strCriteria += strCriteria.ToLower().Contains("order by") ? string.Empty : " Order by approve_name";
            try
            {
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = "Select * from ef_approve where 1=1 " + strCriteria;
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

        #region SP_APPROVE_INS
        public bool SP_APPROVE_INS(ref int papprove_code, string papprove_name ,int papprove_level, string pcreated_by)
        {
            bool blnResult = false;
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            try
            {
                string strSql = "Insert into ef_approve (approve_name,approve_level,c_created_by,d_created_date) values ( " +
                                "'" + papprove_name + "'," +
                                "" + papprove_level + "," +
                                "'" + pcreated_by + "'," +
                                "'" + cCommon.GetDateTimeNow() + "')";
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = strSql;
                oCommand.ExecuteNonQuery();
                var dt = SP_APPROVE_SEL(" and approve_name='" + papprove_name + "'");
                papprove_code = Helper.CInt(dt.Rows[0]["approve_code"]);
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

        #region SP_APPROVE_UPD
        public bool SP_APPROVE_UPD(int papprove_code, string papprove_name,int papprove_level, string pupdated_by)
        {
            bool blnResult = false;
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            try
            {
                string strSql = "Update ef_approve Set " +
                                " approve_name = '" + papprove_name + "'," +
                                " approve_level = " + papprove_level + "," +
                                " c_updated_by= '" + pupdated_by + "'," +
                                " d_updated_date= '" +  cCommon.GetDateTimeNow() + "'" +
                                " Where approve_code = " + papprove_code;                               
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

        #region SP_APPROVE_DEL
        public bool SP_APPROVE_DEL(int papprove_code)
        {
            bool blnResult = false;
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            try
            {
                string strSql = "Delete from ef_approve Where approve_code = " + papprove_code;
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
