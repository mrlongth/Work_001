using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace myDLL
{
    public class cPayment_back : IDisposable
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

        public cPayment_back()
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

        #region SP_PAYMENT_BACK_HEAD_INS
        public bool SP_PAYMENT_BACK_HEAD_INS(
                string ppayment_doc,
                string ppayment_back_type,
                string ppayment_level_old,
                string ppayment_level_new,
                string ppayment_back_comment,
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
                oCommand.CommandText = "sp_PAYMENT_BACK_HEAD_INS";
                oCommand.Parameters.Add("@ppayment_doc", SqlDbType.VarChar).Value = ppayment_doc;
                oCommand.Parameters.Add("@ppayment_back_type", SqlDbType.VarChar).Value = ppayment_back_type;                
                oCommand.Parameters.Add("@ppayment_level_old", SqlDbType.VarChar).Value = ppayment_level_old;
                oCommand.Parameters.Add("@ppayment_level_new", SqlDbType.VarChar).Value = ppayment_level_new;
                oCommand.Parameters.Add("@ppayment_back_comment", SqlDbType.VarChar).Value = ppayment_back_comment;
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

        #region SP_PAYMENT_BACK_HEAD_UPD
        public bool SP_PAYMENT_BACK_HEAD_UPD(
                string ppayment_back_id ,
                string ppayment_doc,
                string ppayment_back_type ,
                string ppayment_level_old,
                string ppayment_level_new,
                string ppayment_back_comment,
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
                oCommand.CommandText = "sp_PAYMENT_BACK_HEAD_UPD";
                oCommand.Parameters.Add("@ppayment_back_id", SqlDbType.Int).Value = int.Parse(ppayment_back_id);
                oCommand.Parameters.Add("@ppayment_doc", SqlDbType.VarChar).Value = ppayment_doc;
                oCommand.Parameters.Add("@ppayment_back_type", SqlDbType.VarChar).Value = ppayment_back_type;
                oCommand.Parameters.Add("@ppayment_level_old", SqlDbType.VarChar).Value = ppayment_level_old;
                oCommand.Parameters.Add("@ppayment_level_new", SqlDbType.VarChar).Value = ppayment_level_new;
                oCommand.Parameters.Add("@ppayment_back_comment", SqlDbType.VarChar).Value = ppayment_back_comment;
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

        #region SP_PAYMENT_BACK_HEAD_SEL
        public bool SP_PAYMENT_BACK_HEAD_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_PAYMENT_BACK_HEAD_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_PAYMENT_BACK_HEAD_SEL");
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

        #region SP_PAYMENT_BACK_HEAD_DEL
        public bool SP_PAYMENT_BACK_HEAD_DEL(string ppayment_back_id, ref string strMessage)
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
                oCommand.CommandText = "sp_PAYMENT_BACK_HEAD_DEL";
                oCommand.Parameters.Add("@ppayment_back_id", SqlDbType.Int).Value = int.Parse(ppayment_back_id);
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
        
        #region SP_PAYMENT_BACK_DETAIL_INS
        public bool SP_PAYMENT_BACK_DETAIL_INS(
                string ppayment_back_id,
                string pdate_begin,
                string pdate_end,
	            string pdate_count_day ,
	            string pdate_count_month ,
	            string pdate_count_year ,
	            string pdate_count_des ,
				string pitem_code,
                string ppayment_item_old,
                string ppayment_item_new,
                string ppayment_item_diff,
                string ppayment_item_back, 
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
                oCommand.CommandText = "sp_PAYMENT_BACK_DETAIL_INS";
                oCommand.Parameters.Add("@ppayment_back_id", SqlDbType.Int).Value = int.Parse(ppayment_back_id);
                oCommand.Parameters.Add("@pdate_begin", SqlDbType.DateTime).Value = cCommon.CheckDate(pdate_begin);
                oCommand.Parameters.Add("@pdate_end", SqlDbType.DateTime).Value = cCommon.CheckDate(pdate_end);
                oCommand.Parameters.Add("@pdate_count_day", SqlDbType.Int).Value = int.Parse(pdate_count_day);
                oCommand.Parameters.Add("@pdate_count_month", SqlDbType.Int).Value = int.Parse(pdate_count_month);
                oCommand.Parameters.Add("@pdate_count_year", SqlDbType.Int).Value = int.Parse(pdate_count_year);
                oCommand.Parameters.Add("@pdate_count_des", SqlDbType.VarChar).Value = pdate_count_des;
                oCommand.Parameters.Add("@pitem_code", SqlDbType.VarChar).Value = pitem_code;
                oCommand.Parameters.Add("@ppayment_item_old", SqlDbType.Money).Value = float.Parse(ppayment_item_old);
                oCommand.Parameters.Add("@ppayment_item_new", SqlDbType.Money).Value = float.Parse(ppayment_item_new);
                oCommand.Parameters.Add("@ppayment_item_diff", SqlDbType.Money).Value = float.Parse(ppayment_item_diff);
                oCommand.Parameters.Add("@ppayment_item_back", SqlDbType.Money).Value = float.Parse(ppayment_item_back);
                oCommand.Parameters.Add("@pcomments_sub", SqlDbType.VarChar).Value =  pcomments_sub;
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

        #region SP_PAYMENT_BACK_DETAIL_UPD
        public bool SP_PAYMENT_BACK_DETAIL_UPD(
                string ppayment_back_detail_id,
                string ppayment_back_id,
                string pdate_begin,
                string pdate_end,
                string pdate_count_day,
                string pdate_count_month,
                string pdate_count_year,
                string pdate_count_des,
                string pitem_code,
                string ppayment_item_old,
                string ppayment_item_new,
                string ppayment_item_diff,
                string ppayment_item_back,
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
                oCommand.CommandText = "SP_PAYMENT_BACK_DETAIL_UPD";
                oCommand.Parameters.Add("@ppayment_back_detail_id", SqlDbType.Int).Value = int.Parse(ppayment_back_detail_id);
                oCommand.Parameters.Add("@ppayment_back_id", SqlDbType.Int).Value = int.Parse(ppayment_back_id);
                oCommand.Parameters.Add("@pdate_begin", SqlDbType.DateTime).Value = cCommon.CheckDate(pdate_begin);
                oCommand.Parameters.Add("@pdate_end", SqlDbType.DateTime).Value = cCommon.CheckDate(pdate_end);
                oCommand.Parameters.Add("@pdate_count_day", SqlDbType.Int).Value = int.Parse(pdate_count_day);
                oCommand.Parameters.Add("@pdate_count_month", SqlDbType.Int).Value = int.Parse(pdate_count_month);
                oCommand.Parameters.Add("@pdate_count_year", SqlDbType.Int).Value = int.Parse(pdate_count_year);
                oCommand.Parameters.Add("@pdate_count_des", SqlDbType.VarChar).Value = pdate_count_des;
                oCommand.Parameters.Add("@pitem_code", SqlDbType.VarChar).Value = pitem_code;
                oCommand.Parameters.Add("@ppayment_item_old", SqlDbType.Money).Value = float.Parse(ppayment_item_old);
                oCommand.Parameters.Add("@ppayment_item_new", SqlDbType.Money).Value = float.Parse(ppayment_item_new);
                oCommand.Parameters.Add("@ppayment_item_diff", SqlDbType.Money).Value = float.Parse(ppayment_item_diff);
                oCommand.Parameters.Add("@ppayment_item_back", SqlDbType.Money).Value = float.Parse(ppayment_item_back);
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



        #region SP_PAYMENT_SOS_DETAIL_UPD
        public bool SP_PAYMENT_SOS_DETAIL_UPD(
                string ppayment_back_detail_id,
                string ppayment_back_id,
                string pdate_begin,
                string pdate_end,
                string pdate_count_day,
                string pdate_count_month,
                string pdate_count_year,
                string pdate_count_des,
                string pitem_code,
                string ppayment_item_old,
                string ppayment_item_new,
                string ppayment_item_diff,
                string ppayment_item_back,
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
                oCommand.CommandText = "sp_PAYMENT_SOS_DETAIL_UPD";
                oCommand.Parameters.Add("@ppayment_back_detail_id", SqlDbType.Int).Value = int.Parse(ppayment_back_detail_id);
                oCommand.Parameters.Add("@ppayment_back_id", SqlDbType.Int).Value = int.Parse(ppayment_back_id);
                oCommand.Parameters.Add("@pdate_begin", SqlDbType.DateTime).Value = cCommon.CheckDate(pdate_begin);
                oCommand.Parameters.Add("@pdate_end", SqlDbType.DateTime).Value = cCommon.CheckDate(pdate_end);
                oCommand.Parameters.Add("@pdate_count_day", SqlDbType.Int).Value = int.Parse(pdate_count_day);
                oCommand.Parameters.Add("@pdate_count_month", SqlDbType.Int).Value = int.Parse(pdate_count_month);
                oCommand.Parameters.Add("@pdate_count_year", SqlDbType.Int).Value = int.Parse(pdate_count_year);
                oCommand.Parameters.Add("@pdate_count_des", SqlDbType.VarChar).Value = pdate_count_des;
                oCommand.Parameters.Add("@pitem_code", SqlDbType.VarChar).Value = pitem_code;
                oCommand.Parameters.Add("@ppayment_item_old", SqlDbType.Money).Value = float.Parse(ppayment_item_old);
                oCommand.Parameters.Add("@ppayment_item_new", SqlDbType.Money).Value = float.Parse(ppayment_item_new);
                oCommand.Parameters.Add("@ppayment_item_diff", SqlDbType.Money).Value = float.Parse(ppayment_item_diff);
                oCommand.Parameters.Add("@ppayment_item_back", SqlDbType.Money).Value = float.Parse(ppayment_item_back);
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


        #region SP_PAYMENT_BACK_DETAIL_DEL
        public bool SP_PAYMENT_BACK_DETAIL_DEL(
                string ppayment_back_detail_id,
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
                oCommand.CommandText = "SP_PAYMENT_BACK_DETAIL_DEL";
                oCommand.Parameters.Add("@ppayment_back_detail_id", SqlDbType.Int).Value = int.Parse(ppayment_back_detail_id);
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

        #region SP_PAYMENT_BACK_SEL
        public bool SP_PAYMENT_BACK_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_PAYMENT_BACK_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_PAYMENT_BACK_SEL");
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

        #region SP_PAYMENT_BACK_REPORT_SEL
        public bool SP_PAYMENT_BACK_REPORT_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_PAYMENT_BACK_REPORT_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_PAYMENT_BACK_REPORT_SEL");
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
