using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace myDLL
{
    public class cBudget_rev : IDisposable
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

    public cBudget_rev()
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

    #region SP_BUDGET_REV_SEL
    public bool SP_BUDGET_REV_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
            oCommand.CommandText = "sp_BUDGET_REV_SEL";
            SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
            oParamI_vc_criteria.Direction = ParameterDirection.Input;
            oParamI_vc_criteria.Value = strCriteria;
            oCommand.Parameters.Add(oParamI_vc_criteria);
            oAdapter = new SqlDataAdapter(oCommand);
            ds = new DataSet();
            oAdapter.Fill(ds, "sp_BUDGET_REV_SEL");
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

    #region SP_BUDGET_REV_INS
    public bool SP_BUDGET_REV_INS(string pbudget_rev_year ,string pedu_code ,
                                                string pplan_code ,string pwork_code ,string pfund_code ,
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
            oCommand.CommandText = "sp_BUDGET_REV_INS";
            // - - - - - - - - - - - -             
            SqlParameter oParam_Budget_Plan_year= new SqlParameter("budget_rev_year", SqlDbType.NVarChar);
            oParam_Budget_Plan_year.Direction = ParameterDirection.Input;
            oParam_Budget_Plan_year.Value = pbudget_rev_year;
            oCommand.Parameters.Add(oParam_Budget_Plan_year);
            // - - - - - - - - - - - -             pedu_code
            SqlParameter oParam_edu_code = new SqlParameter("edu_code", SqlDbType.NVarChar);
            oParam_edu_code.Direction = ParameterDirection.Input;
            oParam_edu_code.Value = pedu_code;
            oCommand.Parameters.Add(oParam_edu_code);
            // - - - - - - - - - - - -             pplan_code
            SqlParameter oParam_rev_code = new SqlParameter("plan_code", SqlDbType.NVarChar);
            oParam_rev_code.Direction = ParameterDirection.Input;
            oParam_rev_code.Value = pplan_code;
            oCommand.Parameters.Add(oParam_rev_code);
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

    #region SP_BUDGET_REV_UPD
    public bool SP_BUDGET_REV_UPD(string pbudget_rev_code, string pbudget_rev_year, string pedu_code,
                                                                                  string pplan_code, string pwork_code, string pfund_code, 
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
            oCommand.CommandText = "sp_BUDGET_REV_UPD";
            // - - - - - - - - - - - -             
            SqlParameter oParam_Budget_Plan_code = new SqlParameter("budget_rev_code", SqlDbType.NVarChar);
            oParam_Budget_Plan_code.Direction = ParameterDirection.Input;
            oParam_Budget_Plan_code.Value = pbudget_rev_code;
            oCommand.Parameters.Add(oParam_Budget_Plan_code);
            // - - - - - - - - - - - -             
            SqlParameter oParam_Budget_Plan_year = new SqlParameter("budget_rev_year", SqlDbType.NVarChar);
            oParam_Budget_Plan_year.Direction = ParameterDirection.Input;
            oParam_Budget_Plan_year.Value = pbudget_rev_year;
            oCommand.Parameters.Add(oParam_Budget_Plan_year);
            // - - - - - - - - - - - -             pedu_code
            SqlParameter oParam_edu_code = new SqlParameter("edu_code", SqlDbType.NVarChar);
            oParam_edu_code.Direction = ParameterDirection.Input;
            oParam_edu_code.Value = pedu_code;
            oCommand.Parameters.Add(oParam_edu_code);
            // - - - - - - - - - - - -             pplan_code
            SqlParameter oParam_rev_code = new SqlParameter("plan_code", SqlDbType.NVarChar);
            oParam_rev_code.Direction = ParameterDirection.Input;
            oParam_rev_code.Value = pplan_code;
            oCommand.Parameters.Add(oParam_rev_code);
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

    #region SP_BUDGET_REV_DEL
    public bool SP_BUDGET_REV_DEL(string pbudget_rev_code, string pActive, string pC_updated_by, ref string strMessage)
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
            oCommand.CommandText = "sp_BUDGET_REV_DEL";
            // - - - - - - - - - - - -             
            SqlParameter oParam_Budget_Plan_code = new SqlParameter("budget_rev_code", SqlDbType.NVarChar);
            oParam_Budget_Plan_code.Direction = ParameterDirection.Input;
            oParam_Budget_Plan_code.Value = pbudget_rev_code;
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
