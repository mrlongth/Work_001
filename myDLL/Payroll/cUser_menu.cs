using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace myDLL
{
    public class cUser_menu : IDisposable
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

        public cUser_menu()
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

        #region SP_USER_MENU_SEL
        public bool SP_USER_MENU_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "SP_USER_MENU_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "SP_USER_MENU_SEL");
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

        #region SP_USER_MENU_MANAGE_SEL
        public bool SP_USER_MENU_MANAGE_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "SP_USER_MENU_MANAGE_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "SP_USER_MENU_MANAGE_SEL");
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
        
        #region SP_USER_MENU_DEL
        public bool SP_USER_MENU_DEL(string pUserID, ref string strMessage)
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
                oCommand.CommandText = "SP_USER_MENU_DEL";
                oCommand.Parameters.Add("pUserID", SqlDbType.Int).Value = pUserID;
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

        #region SP_USER_MENU_INS
        public bool SP_USER_MENU_INS(
            	string pUserID ,
	            string pMenuID ,
	            string pCanView ,
	            string pCanInsert ,
	            string pCanEdit ,
	            string pCanDelete ,
	            string pCanApprove ,
	            string pCanExtra ,
	            string pCreatedBy ,
                ref string strMessage)
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
                oCommand.CommandText = "SP_USER_MENU_INS";
                oCommand.Parameters.Add("pUserID", SqlDbType.Int).Value = pUserID;
                oCommand.Parameters.Add("pMenuID", SqlDbType.Int).Value = pMenuID;
                oCommand.Parameters.Add("pCanView", SqlDbType.VarChar).Value = pCanView;
                oCommand.Parameters.Add("pCanInsert", SqlDbType.VarChar).Value = pCanInsert;
                oCommand.Parameters.Add("pCanEdit", SqlDbType.VarChar).Value = pCanEdit;
                oCommand.Parameters.Add("pCanDelete", SqlDbType.VarChar).Value = pCanDelete;
                oCommand.Parameters.Add("pCanApprove", SqlDbType.VarChar).Value = pCanApprove;
                oCommand.Parameters.Add("pCanExtra", SqlDbType.VarChar).Value = pCanExtra;
                oCommand.Parameters.Add("pCreatedBy", SqlDbType.VarChar).Value = pCreatedBy;
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

        #region SP_USER_MENU_UPD
        public bool SP_USER_MENU_UPD(
                string pUserID,
                string pMenuID,
                string pCanView,
                string pCanInsert,
                string pCanEdit,
                string pCanDelete,
                string pCanApprove,
                string pCanExtra,
                string pUpdatedBy,
                ref string strMessage)
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
                oCommand.CommandText = "SP_USER_MENU_UPD";
                oCommand.Parameters.Add("pUserID", SqlDbType.Int).Value = pUserID;
                oCommand.Parameters.Add("pMenuID", SqlDbType.Int).Value = pMenuID;
                oCommand.Parameters.Add("pCanView", SqlDbType.VarChar).Value = pCanView;
                oCommand.Parameters.Add("pCanInsert", SqlDbType.VarChar).Value = pCanInsert;
                oCommand.Parameters.Add("pCanEdit", SqlDbType.VarChar).Value = pCanEdit;
                oCommand.Parameters.Add("pCanDelete", SqlDbType.VarChar).Value = pCanDelete;
                oCommand.Parameters.Add("pCanApprove", SqlDbType.VarChar).Value = pCanApprove;
                oCommand.Parameters.Add("pCanExtra", SqlDbType.VarChar).Value = pCanExtra;
                oCommand.Parameters.Add("pUpdatedBy", SqlDbType.VarChar).Value = pUpdatedBy;
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
