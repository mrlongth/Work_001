using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace myDLL
{
    public class cEdu : IDisposable
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

    public cEdu()
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

    #region SP_EDU_SEL
    public bool SP_EDU_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
            oCommand.CommandText = "sp_EDU_SEL";
            SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
            oParamI_vc_criteria.Direction = ParameterDirection.Input;
            oParamI_vc_criteria.Value = strCriteria;
            oCommand.Parameters.Add(oParamI_vc_criteria);
            oAdapter = new SqlDataAdapter(oCommand);
            ds = new DataSet();
            oAdapter.Fill(ds, "sp_EDU_SEL");
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

    #region SP_INS_EDU
    public bool SP_INS_EDU(string pEdu_year, string pEdu_name, string pUnit_code, 
                                                                    string pActive, string pC_created_by, ref string strMessage)
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
            oCommand.CommandText = "sp_EDU_INS";
            // - - - - - - - - - - - -             
            SqlParameter oParam_activity_year= new SqlParameter("Edu_year", SqlDbType.NVarChar);
            oParam_activity_year.Direction = ParameterDirection.Input;
            oParam_activity_year.Value = pEdu_year;
            oCommand.Parameters.Add(oParam_activity_year);
            // - - - - - - - - - - - -             
            SqlParameter oParam_activity_name = new SqlParameter("Edu_name", SqlDbType.NVarChar);
            oParam_activity_name.Direction = ParameterDirection.Input;
            oParam_activity_name.Value = pEdu_name;
            oCommand.Parameters.Add(oParam_activity_name);
            // - - - - - - - - - - - -             
            SqlParameter oParam_Unit_code = new SqlParameter("Unit_code", SqlDbType.NVarChar);
            oParam_Unit_code.Direction = ParameterDirection.Input;
            oParam_Unit_code.Value = pUnit_code;
            oCommand.Parameters.Add(oParam_Unit_code);
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

    #region SP_UPD_EDU
    public bool SP_UPD_EDU(string pEdu_code, string pEdu_year, string pEdu_name, string pUnit_code, 
                                                                        string pActive, string pC_updated_by, ref string strMessage)
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
            oCommand.CommandText = "sp_EDU_UPD";
            // - - - - - - - - - - - -             
            SqlParameter oParam_activity_code = new SqlParameter("Edu_code", SqlDbType.NVarChar);
            oParam_activity_code.Direction = ParameterDirection.Input;
            oParam_activity_code.Value = pEdu_code;
            oCommand.Parameters.Add(oParam_activity_code);
            // - - - - - - - - - - - -             
            SqlParameter oParam_activity_year = new SqlParameter("Edu_year", SqlDbType.NVarChar);
            oParam_activity_year.Direction = ParameterDirection.Input;
            oParam_activity_year.Value = pEdu_year;
            oCommand.Parameters.Add(oParam_activity_year);
            // - - - - - - - - - - - -             
            SqlParameter oParam_activity_name = new SqlParameter("Edu_name", SqlDbType.NVarChar);
            oParam_activity_name.Direction = ParameterDirection.Input;
            oParam_activity_name.Value = pEdu_name;
            oCommand.Parameters.Add(oParam_activity_name);
            // - - - - - - - - - - - -             
            SqlParameter oParam_Unit_code = new SqlParameter("Unit_code", SqlDbType.NVarChar);
            oParam_Unit_code.Direction = ParameterDirection.Input;
            oParam_Unit_code.Value = pUnit_code;
            oCommand.Parameters.Add(oParam_Unit_code);
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

    #region SP_DEL_EDU
    public bool SP_DEL_EDU(string pEdu_code, string pActive, string pC_updated_by, ref string strMessage)
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
            oCommand.CommandText = "sp_EDU_DEL";
            // - - - - - - - - - - - -             
            SqlParameter oParam_activity_code = new SqlParameter("Edu_code", SqlDbType.NVarChar);
            oParam_activity_code.Direction = ParameterDirection.Input;
            oParam_activity_code.Value = pEdu_code;
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
