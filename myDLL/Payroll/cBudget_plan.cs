using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace myDLL
{
    public class cBudget_plan : IDisposable
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

    public cBudget_plan()
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

    #region SP_BUDGET_PLAN_SEL
    public bool SP_BUDGET_PLAN_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
            oCommand.CommandText = "sp_BUDGET_PLAN_SEL";
            SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
            oParamI_vc_criteria.Direction = ParameterDirection.Input;
            oParamI_vc_criteria.Value = strCriteria;
            oCommand.Parameters.Add(oParamI_vc_criteria);
            oAdapter = new SqlDataAdapter(oCommand);
            ds = new DataSet();
            oAdapter.Fill(ds, "sp_BUDGET_PLAN_SEL");
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

    #region SP_BUDGET_PLAN_INS
    public bool SP_BUDGET_PLAN_INS(string pbudget_plan_year ,string punit_code ,string pactivity_code ,
                                                string pplan_code ,string pwork_code ,string pfund_code ,
                                                string pActive, string pC_created_by, string pbudget_type ,ref string strMessage)
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
            oCommand.CommandText = "sp_BUDGET_PLAN_INS";
            // - - - - - - - - - - - -             
            SqlParameter oParam_Budget_Plan_year= new SqlParameter("budget_plan_year", SqlDbType.NVarChar);
            oParam_Budget_Plan_year.Direction = ParameterDirection.Input;
            oParam_Budget_Plan_year.Value = pbudget_plan_year;
            oCommand.Parameters.Add(oParam_Budget_Plan_year);
            // - - - - - - - - - - - -             punit_code
            SqlParameter oParam_unit_code = new SqlParameter("unit_code", SqlDbType.NVarChar);
            oParam_unit_code.Direction = ParameterDirection.Input;
            oParam_unit_code.Value = punit_code;
            oCommand.Parameters.Add(oParam_unit_code);
            // - - - - - - - - - - - -             pactivity_code
            SqlParameter oParam_activity_code = new SqlParameter("activity_code", SqlDbType.NVarChar);
            oParam_activity_code.Direction = ParameterDirection.Input;
            oParam_activity_code.Value = pactivity_code;
            oCommand.Parameters.Add(oParam_activity_code);
            // - - - - - - - - - - - -             pplan_code
            SqlParameter oParam_plan_code = new SqlParameter("plan_code", SqlDbType.NVarChar);
            oParam_plan_code.Direction = ParameterDirection.Input;
            oParam_plan_code.Value = pplan_code;
            oCommand.Parameters.Add(oParam_plan_code);
            // - - - - - - - - - - - -             pwork_code
            SqlParameter oParam_work_code = new SqlParameter("work_code", SqlDbType.NVarChar);
            oParam_work_code.Direction = ParameterDirection.Input;
            oParam_work_code.Value = pwork_code;
            oCommand.Parameters.Add(oParam_work_code);
            // - - - - - - - - - - - -             pfund_code
            SqlParameter oParam_fund_code = new SqlParameter("fund_code", SqlDbType.NVarChar);
            oParam_fund_code.Direction = ParameterDirection.Input;
            oParam_fund_code.Value = pfund_code;
            oCommand.Parameters.Add(oParam_fund_code);
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

    #region SP_BUDGET_PLAN_UPD
    public bool SP_BUDGET_PLAN_UPD(string pbudget_plan_code, string pbudget_plan_year, string punit_code, string pactivity_code,
                                                                                  string pplan_code, string pwork_code, string pfund_code, 
                                                                                  string pActive, string pC_updated_by,string pbudget_type, ref string strMessage)
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
            oCommand.CommandText = "sp_BUDGET_PLAN_UPD";
            // - - - - - - - - - - - -             
            SqlParameter oParam_Budget_Plan_code = new SqlParameter("budget_plan_code", SqlDbType.NVarChar);
            oParam_Budget_Plan_code.Direction = ParameterDirection.Input;
            oParam_Budget_Plan_code.Value = pbudget_plan_code;
            oCommand.Parameters.Add(oParam_Budget_Plan_code);
            // - - - - - - - - - - - -             
            SqlParameter oParam_Budget_Plan_year = new SqlParameter("budget_plan_year", SqlDbType.NVarChar);
            oParam_Budget_Plan_year.Direction = ParameterDirection.Input;
            oParam_Budget_Plan_year.Value = pbudget_plan_year;
            oCommand.Parameters.Add(oParam_Budget_Plan_year);
            // - - - - - - - - - - - -             punit_code
            SqlParameter oParam_unit_code = new SqlParameter("unit_code", SqlDbType.NVarChar);
            oParam_unit_code.Direction = ParameterDirection.Input;
            oParam_unit_code.Value = punit_code;
            oCommand.Parameters.Add(oParam_unit_code);
            // - - - - - - - - - - - -             pactivity_code
            SqlParameter oParam_activity_code = new SqlParameter("activity_code", SqlDbType.NVarChar);
            oParam_activity_code.Direction = ParameterDirection.Input;
            oParam_activity_code.Value = pactivity_code;
            oCommand.Parameters.Add(oParam_activity_code);
            // - - - - - - - - - - - -             pplan_code
            SqlParameter oParam_plan_code = new SqlParameter("plan_code", SqlDbType.NVarChar);
            oParam_plan_code.Direction = ParameterDirection.Input;
            oParam_plan_code.Value = pplan_code;
            oCommand.Parameters.Add(oParam_plan_code);
            // - - - - - - - - - - - -             pwork_code
            SqlParameter oParam_work_code = new SqlParameter("work_code", SqlDbType.NVarChar);
            oParam_work_code.Direction = ParameterDirection.Input;
            oParam_work_code.Value = pwork_code;
            oCommand.Parameters.Add(oParam_work_code);
            // - - - - - - - - - - - -             pfund_code
            SqlParameter oParam_fund_code = new SqlParameter("fund_code", SqlDbType.NVarChar);
            oParam_fund_code.Direction = ParameterDirection.Input;
            oParam_fund_code.Value = pfund_code;
            oCommand.Parameters.Add(oParam_fund_code);
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

            SqlParameter oParam_budget_type = new SqlParameter("budget_type", SqlDbType.NVarChar);
            oParam_budget_type.Direction = ParameterDirection.Input;
            oParam_budget_type.Value = pbudget_type;
            oCommand.Parameters.Add(oParam_budget_type);

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

    #region SP_BUDGET_PLAN_DEL
    public bool SP_BUDGET_PLAN_DEL(string pbudget_plan_code, string pActive, string pC_updated_by, ref string strMessage)
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
            oCommand.CommandText = "sp_BUDGET_PLAN_DEL";
            // - - - - - - - - - - - -             
            SqlParameter oParam_Budget_Plan_code = new SqlParameter("budget_plan_code", SqlDbType.NVarChar);
            oParam_Budget_Plan_code.Direction = ParameterDirection.Input;
            oParam_Budget_Plan_code.Value = pbudget_plan_code;
            oCommand.Parameters.Add(oParam_Budget_Plan_code);
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
