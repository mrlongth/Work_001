using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace myDLL
{
    public class cBudget : IDisposable
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

    public cBudget()
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

    #region SP_SEL_BUDGET
    public bool SP_SEL_BUDGET(string strCriteria, ref DataSet ds, ref string strMessage)
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
            oCommand.CommandText = "sp_BUDGET_SEL";
            SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
            oParamI_vc_criteria.Direction = ParameterDirection.Input;
            oParamI_vc_criteria.Value = strCriteria;
            oCommand.Parameters.Add(oParamI_vc_criteria);
            oAdapter = new SqlDataAdapter(oCommand);
            ds = new DataSet();
            oAdapter.Fill(ds, "sp_BUDGET_SEL");
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

    #region SP_INS_BUDGET
    public bool SP_INS_BUDGET(string pbudget_year,string pbudget_name, 
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
            oCommand.CommandText = "sp_BUDGET_INS";
            // - - - - - - - - - - - -             
            SqlParameter oParam_budget_year= new SqlParameter("budget_year", SqlDbType.NVarChar);
            oParam_budget_year.Direction = ParameterDirection.Input;
            oParam_budget_year.Value = pbudget_year;
            oCommand.Parameters.Add(oParam_budget_year);
            // - - - - - - - - - - - -             
            SqlParameter oParam_budget_name = new SqlParameter("budget_name", SqlDbType.NVarChar);
            oParam_budget_name.Direction = ParameterDirection.Input;
            oParam_budget_name.Value = pbudget_name;
            oCommand.Parameters.Add(oParam_budget_name);
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

            SqlParameter oParam_pbudget_type = new SqlParameter("budget_type", SqlDbType.NVarChar);
            oParam_pbudget_type.Direction = ParameterDirection.Input;
            oParam_pbudget_type.Value = pbudget_type;
            oCommand.Parameters.Add(oParam_pbudget_type);


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

    #region SP_UPD_BUDGET
    public bool SP_UPD_BUDGET(string pbudget_code, string pbudget_year, string pbudget_name,
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
            oCommand.CommandText = "sp_BUDGET_UPD";
            // - - - - - - - - - - - -             
            SqlParameter oParam_budget_code = new SqlParameter("budget_code", SqlDbType.NVarChar);
            oParam_budget_code.Direction = ParameterDirection.Input;
            oParam_budget_code.Value = pbudget_code;
            oCommand.Parameters.Add(oParam_budget_code);
            // - - - - - - - - - - - -             
            SqlParameter oParam_budget_year = new SqlParameter("budget_year", SqlDbType.NVarChar);
            oParam_budget_year.Direction = ParameterDirection.Input;
            oParam_budget_year.Value = pbudget_year;
            oCommand.Parameters.Add(oParam_budget_year);
            // - - - - - - - - - - - -             
            SqlParameter oParam_budget_name = new SqlParameter("budget_name", SqlDbType.NVarChar);
            oParam_budget_name.Direction = ParameterDirection.Input;
            oParam_budget_name.Value = pbudget_name;
            oCommand.Parameters.Add(oParam_budget_name);
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

            SqlParameter oParam_pbudget_type = new SqlParameter("budget_type", SqlDbType.NVarChar);
            oParam_pbudget_type.Direction = ParameterDirection.Input;
            oParam_pbudget_type.Value = pbudget_type;
            oCommand.Parameters.Add(oParam_pbudget_type);

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

    #region SP_DEL_BUDGET
    public bool SP_DEL_BUDGET(string pbudget_code, string pActive, string pC_updated_by, ref string strMessage)
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
            oCommand.CommandText = "sp_BUDGET_DEL";
            // - - - - - - - - - - - -             
            SqlParameter oParam_budget_code = new SqlParameter("budget_code", SqlDbType.NVarChar);
            oParam_budget_code.Direction = ParameterDirection.Input;
            oParam_budget_code.Value = pbudget_code;
            oCommand.Parameters.Add(oParam_budget_code);
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
