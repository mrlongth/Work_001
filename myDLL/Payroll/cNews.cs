using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace myDLL
{
    public class cNews : IDisposable
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

        public cNews()
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

        #region SP_NEW_SEL
        public bool SP_NEW_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
        {
            bool blnResult = false;
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            try
            {
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.CommandText = "sp_NEW_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_NEW_SEL");
                blnResult = true;
            }
            catch (Exception ex)
            {
                strMessage = ex.Message.ToString();
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

        #region SP_NEW_SHOW_SEL
        public bool SP_NEW_SHOW_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
        {
            bool blnResult = false;
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            try
            {
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.CommandText = "sp_NEW_SHOW_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_NEW_SEL");
                blnResult = true;
            }
            catch (Exception ex)
            {
                strMessage = ex.Message.ToString();
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

        #region SP_NEW_INS

        public bool SP_NEW_INS
            (
                string pnew_title,
                string pnew_des,
                string pnew_type,
                string pnew_status,
                string pnew_file_name,
                string pnew_pic_name,
                string pc_active,
                string pc_created_by,
                ref string strMessage
            )
        {
            bool blnResult = false;
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            try
            {
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.CommandText = "sp_NEW_INS";
                oCommand.Parameters.Add("new_title", SqlDbType.VarChar).Value = pnew_title;
                oCommand.Parameters.Add("new_des", SqlDbType.VarChar).Value = pnew_des;
                oCommand.Parameters.Add("new_type", SqlDbType.VarChar).Value = pnew_type;
                oCommand.Parameters.Add("new_status", SqlDbType.VarChar).Value = pnew_status;
                oCommand.Parameters.Add("new_file_name", SqlDbType.VarChar).Value = pnew_file_name;
                oCommand.Parameters.Add("new_pic_name", SqlDbType.VarChar).Value = pnew_pic_name;
                oCommand.Parameters.Add("c_active", SqlDbType.VarChar).Value = pc_active;
                oCommand.Parameters.Add("c_created_by", SqlDbType.VarChar).Value = pc_created_by;
                // - - - - - - - - - - - -             
                oCommand.ExecuteNonQuery();
                blnResult = true;
            }
            catch (Exception ex)
            {
                strMessage = ex.Message.ToString();
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

        #region SP_NEW_UPD

        public bool SP_NEW_UPD
            (
                string pnew_id,
                string pnew_title,
                string pnew_des,
                string pnew_type,
                string pnew_status,
                string pnew_file_name,
                string pnew_pic_name,
                string pc_active,
                string pc_created_by,
                ref string strMessage
            )
        {
            bool blnResult = false;
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            try
            {
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.CommandText = "sp_NEW_UPD";
                oCommand.Parameters.Add("new_id", SqlDbType.Int).Value = int.Parse(pnew_id);
                oCommand.Parameters.Add("new_title", SqlDbType.VarChar).Value = pnew_title;
                oCommand.Parameters.Add("new_des", SqlDbType.VarChar).Value = pnew_des;
                oCommand.Parameters.Add("new_type", SqlDbType.VarChar).Value = pnew_type;
                oCommand.Parameters.Add("new_status", SqlDbType.VarChar).Value = pnew_status;
                oCommand.Parameters.Add("new_file_name", SqlDbType.VarChar).Value = pnew_file_name;
                oCommand.Parameters.Add("new_pic_name", SqlDbType.VarChar).Value = pnew_pic_name;
                oCommand.Parameters.Add("c_active", SqlDbType.VarChar).Value = pc_active;
                oCommand.Parameters.Add("c_updated_by", SqlDbType.VarChar).Value = pc_created_by;
                // - - - - - - - - - - - -             
                oCommand.ExecuteNonQuery();
                blnResult = true;
            }
            catch (Exception ex)
            {
                strMessage = ex.Message.ToString();
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

        #region SP_NEW_DEL

        public bool SP_NEW_DEL
            (
                string pnew_id,
                ref string strMessage
            )
        {
            bool blnResult = false;
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            try
            {
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.CommandText = "sp_NEW_DEL";
                oCommand.Parameters.Add("new_id", SqlDbType.Int).Value = int.Parse(pnew_id);              
                // - - - - - - - - - - - -             
                oCommand.ExecuteNonQuery();
                blnResult = true;
            }
            catch (Exception ex)
            {
                strMessage = ex.Message.ToString();
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
