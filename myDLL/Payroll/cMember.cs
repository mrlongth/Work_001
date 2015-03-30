using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace myDLL
{
    public class cMember : IDisposable
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

    public cMember()
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

    #region SP_MEMBER_SEL
    public bool SP_MEMBER_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
            oCommand.CommandText = "sp_MEMBER_SEL";
            SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
            oParamI_vc_criteria.Direction = ParameterDirection.Input;
            oParamI_vc_criteria.Value = strCriteria;
            oCommand.Parameters.Add(oParamI_vc_criteria);
            oAdapter = new SqlDataAdapter(oCommand) ;
            ds = new DataSet();
            oAdapter.Fill(ds, "sp_MEMBER_SEL");
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

    #region SP_INS_MEMBER
    public bool SP_MEMBER_INS(string pmember_code, string pmember_name, string pitem_code, string pActive, string pC_created_by, ref string strMessage)
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
            oCommand.CommandText = "sp_MEMBER_INS";
            // - - - - - - - - - - - -             
            SqlParameter oParam_Member_code = new SqlParameter("member_code", SqlDbType.NVarChar);
            oParam_Member_code.Direction = ParameterDirection.Input;
            oParam_Member_code.Value = pmember_code;
            oCommand.Parameters.Add(oParam_Member_code);
            // - - - - - - - - - - - -             
            SqlParameter oParam_Member_name = new SqlParameter("member_name", SqlDbType.NVarChar);
            oParam_Member_name.Direction = ParameterDirection.Input;
            oParam_Member_name.Value = pmember_name;
            oCommand.Parameters.Add(oParam_Member_name);
            // - - - - - - - - - - - -             
            SqlParameter oParam_item_code = new SqlParameter("item_code", SqlDbType.NVarChar);
            oParam_item_code.Direction = ParameterDirection.Input;
            oParam_item_code.Value = pitem_code;
            oCommand.Parameters.Add(oParam_item_code);
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

    #region SP_MEMBER_UPD
    public bool SP_MEMBER_UPD(string pmember_code, string pmember_name, string pitem_code, string pActive, string pC_updated_by, ref string strMessage)
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
            oCommand.CommandText = "sp_MEMBER_UPD";
            // - - - - - - - - - - - -             
            SqlParameter oParam_Member_code = new SqlParameter("member_code", SqlDbType.NVarChar);
            oParam_Member_code.Direction = ParameterDirection.Input;
            oParam_Member_code.Value = pmember_code;
            oCommand.Parameters.Add(oParam_Member_code);
            // - - - - - - - - - - - -             
            SqlParameter oParam_Member_name = new SqlParameter("member_name", SqlDbType.NVarChar);
            oParam_Member_name.Direction = ParameterDirection.Input;
            oParam_Member_name.Value = pmember_name;
            oCommand.Parameters.Add(oParam_Member_name);
            // - - - - - - - - - - - -             
            SqlParameter oParam_item_code = new SqlParameter("item_code", SqlDbType.NVarChar);
            oParam_item_code.Direction = ParameterDirection.Input;
            oParam_item_code.Value = pitem_code;
            oCommand.Parameters.Add(oParam_item_code);
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

    #region SP_MEMBER_DEL
    public bool SP_MEMBER_DEL(string pmember_code, string pActive, string pC_updated_by, ref string strMessage)
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
            oCommand.CommandText = "sp_MEMBER_DEL";
            // - - - - - - - - - - - -             
            SqlParameter oParam_Member_code = new SqlParameter("member_code", SqlDbType.NVarChar);
            oParam_Member_code.Direction = ParameterDirection.Input;
            oParam_Member_code.Value = pmember_code;
            oCommand.Parameters.Add(oParam_Member_code);
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

    #region IDisposable Members

    void IDisposable.Dispose()
    {
        throw new NotImplementedException();
    }

    #endregion

    }
}
