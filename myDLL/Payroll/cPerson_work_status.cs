using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace myDLL
{
    public class cPerson_work_status : IDisposable
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

    public cPerson_work_status()
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

    #region SP_PERSON_WORK_STATUS_SEL
    public bool SP_PERSON_WORK_STATUS_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
            oCommand.CommandText = "sp_PERSON_WORK_STATUS_SEL";
            SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
            oParamI_vc_criteria.Direction = ParameterDirection.Input;
            oParamI_vc_criteria.Value = strCriteria;
            oCommand.Parameters.Add(oParamI_vc_criteria);
            oAdapter = new SqlDataAdapter(oCommand) ;
            ds = new DataSet();
            oAdapter.Fill(ds, "sp_PERSON_WORK_STATUS_SEL");
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

    #region SP_PERSON_WORK_STATUS_INS
    public bool SP_PERSON_WORK_STATUS_INS(string pPerson_work_status_code, string pPerson_work_status_name, string pActive, string pC_created_by, ref string strMessage)
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
            oCommand.CommandText = "sp_PERSON_WORK_STATUS_INS";
            // - - - - - - - - - - - -             
            SqlParameter oParam_PERSON_WORK_STATUS_code = new SqlParameter("Person_work_status_code", SqlDbType.NVarChar);
            oParam_PERSON_WORK_STATUS_code.Direction = ParameterDirection.Input;
            oParam_PERSON_WORK_STATUS_code.Value = pPerson_work_status_code;
            oCommand.Parameters.Add(oParam_PERSON_WORK_STATUS_code);
            // - - - - - - - - - - - -             
            SqlParameter oParam_PERSON_WORK_STATUS_name = new SqlParameter("Person_work_status_name", SqlDbType.NVarChar);
            oParam_PERSON_WORK_STATUS_name.Direction = ParameterDirection.Input;
            oParam_PERSON_WORK_STATUS_name.Value = pPerson_work_status_name;
            oCommand.Parameters.Add(oParam_PERSON_WORK_STATUS_name);
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

    #region SP_PERSON_WORK_STATUS_UPD
    public bool SP_PERSON_WORK_STATUS_UPD(string pPerson_work_status_code, string pPerson_work_status_name, string pActive, string pC_updated_by, ref string strMessage)
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
            oCommand.CommandText = "sp_PERSON_WORK_STATUS_UPD";
            // - - - - - - - - - - - -             
            SqlParameter oParam_PERSON_WORK_STATUS_code = new SqlParameter("Person_work_status_code", SqlDbType.NVarChar);
            oParam_PERSON_WORK_STATUS_code.Direction = ParameterDirection.Input;
            oParam_PERSON_WORK_STATUS_code.Value = pPerson_work_status_code;
            oCommand.Parameters.Add(oParam_PERSON_WORK_STATUS_code);
            // - - - - - - - - - - - -             
            SqlParameter oParam_PERSON_WORK_STATUS_name = new SqlParameter("Person_work_status_name", SqlDbType.NVarChar);
            oParam_PERSON_WORK_STATUS_name.Direction = ParameterDirection.Input;
            oParam_PERSON_WORK_STATUS_name.Value = pPerson_work_status_name;
            oCommand.Parameters.Add(oParam_PERSON_WORK_STATUS_name);
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

    #region SP_PERSON_WORK_STATUS_DEL
    public bool SP_PERSON_WORK_STATUS_DEL(string pPerson_work_status_code, string pActive, string pC_updated_by, ref string strMessage)
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
            oCommand.CommandText = "sp_PERSON_WORK_STATUS_DEL";
            // - - - - - - - - - - - -             
            SqlParameter oParam_PERSON_WORK_STATUS_code = new SqlParameter("Person_work_status_code", SqlDbType.NVarChar);
            oParam_PERSON_WORK_STATUS_code.Direction = ParameterDirection.Input;
            oParam_PERSON_WORK_STATUS_code.Value = pPerson_work_status_code;
            oCommand.Parameters.Add(oParam_PERSON_WORK_STATUS_code);
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
