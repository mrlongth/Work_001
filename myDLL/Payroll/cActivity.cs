using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace myDLL
{
    public class cActivity : IDisposable
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

        public cActivity()
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

        #region SP_SEL_ACTIVITY
        public bool SP_SEL_ACTIVITY(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_ACTIVITY_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_ACTIVITY_SEL");
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

        #region SP_ACTIVITY_BUDGET_SEL
        public bool SP_ACTIVITY_BUDGET_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_ACTIVITY_BUDGET_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_ACTIVITY_BUDGET_SEL");
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

        #region SP_INS_ACTIVITY
        public bool SP_INS_ACTIVITY(string pActivity_year, string pActivity_name, string pProduce_code,
                                                                        string pActive, string pC_created_by, string pbudget_type, ref string strMessage)
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
                oCommand.CommandText = "sp_ACTIVITY_INS";
                // - - - - - - - - - - - -             
                SqlParameter oParam_activity_year = new SqlParameter("Activity_year", SqlDbType.NVarChar);
                oParam_activity_year.Direction = ParameterDirection.Input;
                oParam_activity_year.Value = pActivity_year;
                oCommand.Parameters.Add(oParam_activity_year);
                // - - - - - - - - - - - -             
                SqlParameter oParam_activity_name = new SqlParameter("Activity_name", SqlDbType.NVarChar);
                oParam_activity_name.Direction = ParameterDirection.Input;
                oParam_activity_name.Value = pActivity_name;
                oCommand.Parameters.Add(oParam_activity_name);
                // - - - - - - - - - - - -             
                SqlParameter oParam_Produce_code = new SqlParameter("Produce_code", SqlDbType.NVarChar);
                oParam_Produce_code.Direction = ParameterDirection.Input;
                oParam_Produce_code.Value = pProduce_code;
                oCommand.Parameters.Add(oParam_Produce_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_Active = new SqlParameter("c_active", SqlDbType.NVarChar);
                oParam_Active.Direction = ParameterDirection.Input;
                oParam_Active.Value = pActive;
                oCommand.Parameters.Add(oParam_Active);
                // - - - - - - - - - - - -             
                SqlParameter oParam_c_created_by = new SqlParameter("c_created_by", SqlDbType.NVarChar);
                oParam_c_created_by.Direction = ParameterDirection.Input;
                oParam_c_created_by.Value = pC_created_by;
                oCommand.Parameters.Add(oParam_c_created_by);

                // - - - - - - - - - - - -             
                SqlParameter oParam_budget_type = new SqlParameter("budget_type", SqlDbType.NVarChar);
                oParam_budget_type.Direction = ParameterDirection.Input;
                oParam_budget_type.Value = pbudget_type;
                oCommand.Parameters.Add(oParam_budget_type);




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

        #region SP_UPD_ACTIVITY
        public bool SP_UPD_ACTIVITY(string pActivity_code, string pActivity_year, string pActivity_name, string pProduce_code,
                                                                            string pActive, string pC_updated_by, string pbudget_type, ref string strMessage)
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
                oCommand.CommandText = "sp_ACTIVITY_UPD";
                // - - - - - - - - - - - -             
                SqlParameter oParam_activity_code = new SqlParameter("Activity_code", SqlDbType.NVarChar);
                oParam_activity_code.Direction = ParameterDirection.Input;
                oParam_activity_code.Value = pActivity_code;
                oCommand.Parameters.Add(oParam_activity_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_activity_year = new SqlParameter("Activity_year", SqlDbType.NVarChar);
                oParam_activity_year.Direction = ParameterDirection.Input;
                oParam_activity_year.Value = pActivity_year;
                oCommand.Parameters.Add(oParam_activity_year);
                // - - - - - - - - - - - -             
                SqlParameter oParam_activity_name = new SqlParameter("Activity_name", SqlDbType.NVarChar);
                oParam_activity_name.Direction = ParameterDirection.Input;
                oParam_activity_name.Value = pActivity_name;
                oCommand.Parameters.Add(oParam_activity_name);
                // - - - - - - - - - - - -             
                SqlParameter oParam_Produce_code = new SqlParameter("Produce_code", SqlDbType.NVarChar);
                oParam_Produce_code.Direction = ParameterDirection.Input;
                oParam_Produce_code.Value = pProduce_code;
                oCommand.Parameters.Add(oParam_Produce_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_Active = new SqlParameter("c_active", SqlDbType.NVarChar);
                oParam_Active.Direction = ParameterDirection.Input;
                oParam_Active.Value = pActive;
                oCommand.Parameters.Add(oParam_Active);
                // - - - - - - - - - - - -             
                SqlParameter oParam_c_updated_by = new SqlParameter("c_updated_by", SqlDbType.NVarChar);
                oParam_c_updated_by.Direction = ParameterDirection.Input;
                oParam_c_updated_by.Value = pC_updated_by;
                oCommand.Parameters.Add(oParam_c_updated_by);
                // - - - - - - - - - - - -             
                SqlParameter oParam_budget_type = new SqlParameter("budget_type", SqlDbType.NVarChar);
                oParam_budget_type.Direction = ParameterDirection.Input;
                oParam_budget_type.Value = pbudget_type;
                oCommand.Parameters.Add(oParam_budget_type);
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

        #region SP_DEL_ACTIVITY
        public bool SP_DEL_ACTIVITY(string pActivity_code, string pActive, string pC_updated_by, ref string strMessage)
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
                oCommand.CommandText = "sp_ACTIVITY_DEL";
                // - - - - - - - - - - - -             
                SqlParameter oParam_activity_code = new SqlParameter("Activity_code", SqlDbType.NVarChar);
                oParam_activity_code.Direction = ParameterDirection.Input;
                oParam_activity_code.Value = pActivity_code;
                oCommand.Parameters.Add(oParam_activity_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_Active = new SqlParameter("C_active", SqlDbType.NVarChar);
                oParam_Active.Direction = ParameterDirection.Input;
                oParam_Active.Value = pActive;
                oCommand.Parameters.Add(oParam_Active);
                // - - - - - - - - - - - -             
                SqlParameter oParam_c_updated_by = new SqlParameter("c_updated_by", SqlDbType.NVarChar);
                oParam_c_updated_by.Direction = ParameterDirection.Input;
                oParam_c_updated_by.Value = pC_updated_by;
                oCommand.Parameters.Add(oParam_c_updated_by);
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
