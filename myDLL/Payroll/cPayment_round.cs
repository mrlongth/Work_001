using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace myDLL
{
    public class cPayment_round : IDisposable
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

        public cPayment_round()
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

        #region SP_PAYMENT_ROUND_SEL
        public bool SP_PAYMENT_ROUND_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_PAYMENT_ROUND_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_PAYMENT_ROUND_SEL");
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

        #region SP_PAYMENT_ROUND_DEL
        public bool SP_PAYMENT_ROUND_DEL(string pRound_id, string pActive, string pC_updated_by, ref string strMessage)
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
                oCommand.CommandText = "sp_PAYMENT_ROUND_DEL";
                // - - - - - - - - - - - -             
                SqlParameter oParam_round_id = new SqlParameter("pround_id", SqlDbType.Int);
                oParam_round_id.Direction = ParameterDirection.Input;
                oParam_round_id.Value = int.Parse(pRound_id);
                oCommand.Parameters.Add(oParam_round_id);
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

        #region SP_PAYMENT_ROUND_INS
        public bool SP_PAYMENT_ROUND_INS(
                string ppayment_year,
                string ppay_month,
                string ppay_year,
                string pround_status,
                string pcomments,
                string pc_active,
                string pc_created_by,
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
                oCommand.CommandText = "sp_PAYMENT_ROUND_INS";
                oCommand.Parameters.Add("ppayment_year", SqlDbType.VarChar).Value =  ppayment_year;
                oCommand.Parameters.Add("ppay_month", SqlDbType.VarChar).Value =  ppay_month;
                oCommand.Parameters.Add("ppay_year", SqlDbType.VarChar).Value =  ppay_year ;
                oCommand.Parameters.Add("pround_status", SqlDbType.VarChar).Value =  pround_status;
                oCommand.Parameters.Add("pcomments", SqlDbType.VarChar).Value =  pcomments;
                oCommand.Parameters.Add("pc_active", SqlDbType.VarChar).Value =  pc_active;
                oCommand.Parameters.Add("pc_created_by", SqlDbType.VarChar).Value = pc_created_by;
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

        #region SP_PAYMENT_ROUND_UPD
        public bool SP_PAYMENT_ROUND_UPD(string pRound_id, string pround_status, string pC_updated_by, ref string strMessage)
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
                oCommand.CommandText = "sp_PAYMENT_ROUND_UPD";
                // - - - - - - - - - - - -             
                SqlParameter oParam_round_id = new SqlParameter("pround_id", SqlDbType.Int);
                oParam_round_id.Direction = ParameterDirection.Input;
                oParam_round_id.Value = int.Parse(pRound_id);
                oCommand.Parameters.Add(oParam_round_id);
                // - - - - - - - - - - - -             
                SqlParameter oParam_Round_status = new SqlParameter("pround_status", SqlDbType.NVarChar);
                oParam_Round_status.Direction = ParameterDirection.Input;
                oParam_Round_status.Value = pround_status;
                oCommand.Parameters.Add(oParam_Round_status);
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
