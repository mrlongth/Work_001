using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace myDLL
{
    public class cUnit : IDisposable
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

        public cUnit()
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

        #region SP_SEL_UNIT
        public bool SP_SEL_UNIT(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_UNIT_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_SEL_UNIT");
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

        #region SP_UNIT_INS
        public bool SP_UNIT_INS(string pUnit_year, string pUnit_name, string pDirector_code,string pUnit_order, string pActive, string pC_created_by, string pbudget_type, ref string strMessage)
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
                oCommand.CommandText = "sp_UNIT_INS";
                // - - - - - - - - - - - -             
                SqlParameter oParam_Unit_year = new SqlParameter("unit_year", SqlDbType.NVarChar);
                oParam_Unit_year.Direction = ParameterDirection.Input;
                oParam_Unit_year.Value = pUnit_year;
                oCommand.Parameters.Add(oParam_Unit_year);
                // - - - - - - - - - - - -             
                SqlParameter oParam_Unit_name = new SqlParameter("unit_name", SqlDbType.NVarChar);
                oParam_Unit_name.Direction = ParameterDirection.Input;
                oParam_Unit_name.Value = pUnit_name;
                oCommand.Parameters.Add(oParam_Unit_name);
                // - - - - - - - - - - - -             
                SqlParameter oParam_director_code = new SqlParameter("director_code", SqlDbType.NVarChar);
                oParam_director_code.Direction = ParameterDirection.Input;
                oParam_director_code.Value = pDirector_code;
                oCommand.Parameters.Add(oParam_director_code);
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

                SqlParameter oParam_budget_type = new SqlParameter("budget_type", SqlDbType.NVarChar);
                oParam_budget_type.Direction = ParameterDirection.Input;
                oParam_budget_type.Value = pbudget_type;
                oCommand.Parameters.Add(oParam_budget_type);

                SqlParameter oParam_unit_order = new SqlParameter("unit_order", SqlDbType.Int);
                oParam_unit_order.Direction = ParameterDirection.Input;
                oParam_unit_order.Value = int.Parse(pUnit_order);
                oCommand.Parameters.Add(oParam_unit_order);
                // - - - - - - - - - - - -        

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

        #region SP_UPD_UNIT
        public bool SP_UPD_UNIT(string pUnit_code, string pUnit_year, string pUnit_name, string pDirector_code,string pUnit_order, string pActive, string pC_updated_by, string pbudget_type, ref string strMessage)
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
                oCommand.CommandText = "sp_UNIT_UPD";
                // - - - - - - - - - - - -             
                SqlParameter oParam_Unit_code = new SqlParameter("unit_code", SqlDbType.NVarChar);
                oParam_Unit_code.Direction = ParameterDirection.Input;
                oParam_Unit_code.Value = pUnit_code;
                oCommand.Parameters.Add(oParam_Unit_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_Unit_year = new SqlParameter("unit_year", SqlDbType.NVarChar);
                oParam_Unit_year.Direction = ParameterDirection.Input;
                oParam_Unit_year.Value = pUnit_year;
                oCommand.Parameters.Add(oParam_Unit_year);
                // - - - - - - - - - - - -             
                SqlParameter oParam_Unit_name = new SqlParameter("unit_name", SqlDbType.NVarChar);
                oParam_Unit_name.Direction = ParameterDirection.Input;
                oParam_Unit_name.Value = pUnit_name;
                oCommand.Parameters.Add(oParam_Unit_name);
                // - - - - - - - - - - - -             
                SqlParameter oParam_director_code = new SqlParameter("director_code", SqlDbType.NVarChar);
                oParam_director_code.Direction = ParameterDirection.Input;
                oParam_director_code.Value = pDirector_code;
                oCommand.Parameters.Add(oParam_director_code);
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

                SqlParameter oParam_unit_order = new SqlParameter("unit_order", SqlDbType.Int);
                oParam_unit_order.Direction = ParameterDirection.Input;
                oParam_unit_order.Value = int.Parse(pUnit_order);
                oCommand.Parameters.Add(oParam_unit_order);
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

        #region SP_UNIT_DEL_
        public bool SP_DEL_UNIT(string pUnit_code, string pActive, string pC_updated_by, ref string strMessage)
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
                oCommand.CommandText = "sp_UNIT_DEL";
                // - - - - - - - - - - - -             
                SqlParameter oParam_Unit_code = new SqlParameter("unit_code", SqlDbType.NVarChar);
                oParam_Unit_code.Direction = ParameterDirection.Input;
                oParam_Unit_code.Value = pUnit_code;
                oCommand.Parameters.Add(oParam_Unit_code);
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
