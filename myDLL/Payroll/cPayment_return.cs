using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace myDLL
{
    public class cPayment_return : IDisposable
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

        public cPayment_return()
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

        #region SP_PAYMENT_RETURN_HEAD_INS
        public bool SP_PAYMENT_RETURN_HEAD_INS(
                string ppayment_doc,
                string ppayment_return_comment,
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
                oCommand.CommandText = "sp_PAYMENT_RETURN_HEAD_INS";
                oCommand.Parameters.Add("@ppayment_doc", SqlDbType.VarChar).Value = ppayment_doc;
                oCommand.Parameters.Add("@ppayment_return_comment", SqlDbType.VarChar).Value = ppayment_return_comment;
                oCommand.Parameters.Add("@pc_active", SqlDbType.VarChar).Value = pc_active;
                oCommand.Parameters.Add("@pc_created_by", SqlDbType.VarChar).Value = pc_created_by;
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

        #region SP_PAYMENT_RETURN_HEAD_UPD
        public bool SP_PAYMENT_RETURN_HEAD_UPD(
                string ppayment_doc,
                string ppayment_return_comment,
                string pc_active,
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
                oCommand.CommandText = "sp_PAYMENT_RETURN_HEAD_UPD";
                oCommand.Parameters.Add("@ppayment_doc", SqlDbType.VarChar).Value = ppayment_doc;
                oCommand.Parameters.Add("@ppayment_return_comment", SqlDbType.VarChar).Value = ppayment_return_comment;
                oCommand.Parameters.Add("@pc_active", SqlDbType.VarChar).Value = pc_active;
                oCommand.Parameters.Add("@pc_updated_by", SqlDbType.VarChar).Value = pc_updated_by;
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

        #region SP_PAYMENT_RETURN_HEAD_SEL
        public bool SP_PAYMENT_RETURN_HEAD_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_PAYMENT_RETURN_HEAD_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_PAYMENT_RETURN_HEAD_SEL");
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

        #region SP_PAYMENT_RETURN_HEAD_DEL
        public bool SP_PAYMENT_RETURN_HEAD_DEL(string ppayment_doc, ref string strMessage)
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
                oCommand.CommandText = "sp_PAYMENT_RETURN_HEAD_DEL";
                oCommand.Parameters.Add("@ppayment_doc", SqlDbType.VarChar).Value = ppayment_doc;
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
        
        #region SP_PAYMENT_RETURN_DETAIL_INS
        public bool SP_PAYMENT_RETURN_DETAIL_INS(
                string ppayment_doc,
                string ppayment_doc_pay,
                string pitem_code,
                string ppayment_return_money,
                string ppayment_return_ap,
                string pcomments_sub, 
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
                oCommand.CommandText = "sp_PAYMENT_RETURN_DETAIL_INS";
                oCommand.Parameters.Add("@ppayment_doc", SqlDbType.VarChar).Value = ppayment_doc;
                oCommand.Parameters.Add("@ppayment_doc_pay", SqlDbType.VarChar).Value = ppayment_doc_pay;
                oCommand.Parameters.Add("@pitem_code", SqlDbType.VarChar).Value = pitem_code;
                oCommand.Parameters.Add("@ppayment_return_money", SqlDbType.Money).Value = float.Parse(ppayment_return_money);
                oCommand.Parameters.Add("@ppayment_return_ap", SqlDbType.VarChar).Value = ppayment_return_ap;
                oCommand.Parameters.Add("@pcomments_sub", SqlDbType.VarChar).Value = pcomments_sub;
                oCommand.Parameters.Add("@pc_created_by", SqlDbType.VarChar).Value = pc_created_by;
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

        #region SP_PAYMENT_RETURN_DETAIL_UPD
        public bool SP_PAYMENT_RETURN_DETAIL_UPD(
                string ppayment_return_detail_id,
                string ppayment_doc,
                string ppayment_return_money,
                string ppayment_return_ap,
                string pcomments_sub,
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
                oCommand.CommandText = "SP_PAYMENT_RETURN_DETAIL_UPD";
                oCommand.Parameters.Add("@ppayment_return_detail_id", SqlDbType.Int).Value = int.Parse(ppayment_return_detail_id);
                oCommand.Parameters.Add("@ppayment_doc", SqlDbType.VarChar).Value = ppayment_doc;
                oCommand.Parameters.Add("@ppayment_return_money", SqlDbType.Money).Value = float.Parse(ppayment_return_money);
                oCommand.Parameters.Add("@ppayment_return_ap", SqlDbType.VarChar).Value = ppayment_return_ap;
                oCommand.Parameters.Add("@pcomments_sub", SqlDbType.VarChar).Value = pcomments_sub;
                oCommand.Parameters.Add("@pc_updated_by", SqlDbType.VarChar).Value = pc_updated_by;
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

        #region SP_PAYMENT_RETURN_DETAIL_DEL
        public bool SP_PAYMENT_RETURN_DETAIL_DEL(
                string ppayment_return_detail_id,
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
                oCommand.CommandText = "SP_PAYMENT_RETURN_DETAIL_DEL";
                oCommand.Parameters.Add("@ppayment_return_detail_id", SqlDbType.Int).Value = int.Parse(ppayment_return_detail_id);
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

        #region SP_PAYMENT_RETURN_SEL
        public bool SP_PAYMENT_RETURN_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_PAYMENT_RETURN_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_PAYMENT_RETURN_SEL");
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
