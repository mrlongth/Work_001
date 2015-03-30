using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace myDLL
{
    public class cUser : IDisposable
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

        public cUser()
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

        #region SP_USER_SEL
        public bool SP_USER_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_USER_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_USER_SEL");
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

        #region SP_USER_DEL
        public bool SP_USER_DEL(string pUserID, ref string strMessage)
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
                oCommand.CommandText = "sp_USER_DEL";
                oCommand.Parameters.Add("puserID", SqlDbType.Int).Value = pUserID;
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

        #region SP_USER_INS
        public bool SP_USER_INS(
                ref string pUserID,
                string pperson_code,
                string pLoginName,
                string pPassword,
                string pEmail,
                string puser_group_code ,
                string pStatus,
                string pRemark,
                string pc_created_by,
                string pbudget_type,
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
                oCommand.CommandText = "sp_USER_INS";
                oCommand.Parameters.Add("puserID", SqlDbType.Int).Value = pUserID;
                oCommand.Parameters.Add("pperson_code", SqlDbType.VarChar).Value = pperson_code;
                oCommand.Parameters.Add("ploginname", SqlDbType.VarChar).Value = pLoginName;
                oCommand.Parameters.Add("ppassword", SqlDbType.VarChar).Value = Cryptorengine.Encrypt(pPassword,true);
                oCommand.Parameters.Add("pemail", SqlDbType.VarChar).Value = pEmail;
                oCommand.Parameters.Add("puser_group_code", SqlDbType.VarChar).Value = puser_group_code;
                oCommand.Parameters.Add("pStatus", SqlDbType.VarChar).Value = pStatus;
                oCommand.Parameters.Add("pRemark", SqlDbType.VarChar).Value = pRemark;
                oCommand.Parameters.Add("pCreatedBy", SqlDbType.VarChar).Value = pc_created_by;
                oCommand.Parameters.Add("budget_type", SqlDbType.VarChar).Value = pbudget_type;
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

        #region SP_USER_UPD
        public bool SP_USER_UPD(
                string pUserID,
                string pperson_code,
                string pLoginName,
                string pPassword,
                string pEmail,
                string puser_group_code,
                string pStatus,
                string pRemark,
                string pc_updated_by,
                string pbudget_type,
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
                oCommand.CommandText = "sp_USER_UPD";
                oCommand.Parameters.Add("puserID", SqlDbType.Int).Value = pUserID;
                oCommand.Parameters.Add("pperson_code", SqlDbType.VarChar).Value = pperson_code;
                oCommand.Parameters.Add("ploginname", SqlDbType.VarChar).Value = pLoginName;
                oCommand.Parameters.Add("ppassword", SqlDbType.VarChar).Value = Cryptorengine.Encrypt(pPassword,true);
                oCommand.Parameters.Add("pemail", SqlDbType.VarChar).Value = pEmail;
                oCommand.Parameters.Add("puser_group_code", SqlDbType.VarChar).Value = puser_group_code;
                oCommand.Parameters.Add("pStatus", SqlDbType.VarChar).Value = pStatus;
                oCommand.Parameters.Add("pRemark", SqlDbType.VarChar).Value = pRemark;
                oCommand.Parameters.Add("pUpdatedBy", SqlDbType.VarChar).Value = pc_updated_by;
                oCommand.Parameters.Add("budget_type", SqlDbType.VarChar).Value = pbudget_type;
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

        #region SP_USER_PERSON_GROUP_UPD
        public bool SP_USER_PERSON_GROUP_UPD(
                string pUserID,
                string person_group_list,
                string pdirector_lock,
                string punit_lock,
                //string punit_code_list,
                string pc_updated_by,
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
                oCommand.CommandText = "SP_USER_PERSON_GROUP_UPD";
                oCommand.Parameters.Add("puserID", SqlDbType.Int).Value = pUserID;
                oCommand.Parameters.Add("pperson_group_list", SqlDbType.VarChar).Value = person_group_list;
                oCommand.Parameters.Add("pdirector_lock", SqlDbType.VarChar).Value = pdirector_lock;
                oCommand.Parameters.Add("punit_lock", SqlDbType.VarChar).Value = punit_lock;
                //oCommand.Parameters.Add("punit_code_list", SqlDbType.VarChar).Value = punit_code_list;                
                oCommand.Parameters.Add("pUpdatedBy", SqlDbType.VarChar).Value = pc_updated_by;
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
