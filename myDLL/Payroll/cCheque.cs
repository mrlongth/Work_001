using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace myDLL
{
    public class cCheque : IDisposable
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

        public cCheque()
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

        #region SP_CHEQUE_BANK_SEL
        public bool SP_CHEQUE_BANK_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_CHEQUE_BANK_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_CHEQUE_BANK_SEL");
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

        #region SP_CHEQUE_SELECT_SEL
        public bool SP_CHEQUE_SELECT_SEL(string strCriteria,string strCriteriatmp, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_CHEQUE_SELECT_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);

                SqlParameter oParamI_vc_criteria2 = new SqlParameter("vc_criteriatmp", SqlDbType.NVarChar);
                oParamI_vc_criteria2.Direction = ParameterDirection.Input;
                oParamI_vc_criteria2.Value = strCriteriatmp;
                oCommand.Parameters.Add(oParamI_vc_criteria2);
                
                
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_CHEQUE_SELECT_SEL");
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

        #region SP_CHEQUE_UNIT_SELECT_SEL
        public bool SP_CHEQUE_UNIT_SELECT_SEL(string strCriteria, string strCriteriatmp, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_CHEQUE_UNIT_SELECT_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);

                SqlParameter oParamI_vc_criteria2 = new SqlParameter("vc_criteriatmp", SqlDbType.NVarChar);
                oParamI_vc_criteria2.Direction = ParameterDirection.Input;
                oParamI_vc_criteria2.Value = strCriteriatmp;
                oCommand.Parameters.Add(oParamI_vc_criteria2);

                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_CHEQUE_UNIT_SELECT_SEL");
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


        #region SP_CHEQUE_SEL
        public bool SP_SEL_CHEQUE(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_CHEQUE_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_CHEQUE_SEL");
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

        #region SP_INS_CHEQUE
        public bool SP_INS_CHEQUE(string pcheque_code, string pcheque_name,string pcheque_desc , string pactive, string pC_created_by, ref string strMessage)
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
                oCommand.CommandText = "sp_CHEQUE_INS";
                // - - - - - - - - - - - -             
                SqlParameter oParam_cheque_code = new SqlParameter("cheque_code", SqlDbType.NVarChar);
                oParam_cheque_code.Direction = ParameterDirection.Input;
                oParam_cheque_code.Value = pcheque_code;
                oCommand.Parameters.Add(oParam_cheque_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_cheque_name = new SqlParameter("cheque_name", SqlDbType.NVarChar);
                oParam_cheque_name.Direction = ParameterDirection.Input;
                oParam_cheque_name.Value = pcheque_name;
                oCommand.Parameters.Add(oParam_cheque_name);
                // - - - - - - - - - - - -             
                SqlParameter oParam_cheque_desc = new SqlParameter("cheque_desc", SqlDbType.NVarChar);
                oParam_cheque_desc.Direction = ParameterDirection.Input;
                oParam_cheque_desc.Value = pcheque_desc;
                oCommand.Parameters.Add(oParam_cheque_desc);                
                // - - - - - - - - - - - -             
                SqlParameter oParam_Active = new SqlParameter("c_active", SqlDbType.NVarChar);
                oParam_Active.Direction = ParameterDirection.Input;
                oParam_Active.Value = pactive;
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

        #region SP_UPD_CHEQUE
        public bool SP_UPD_CHEQUE(string pcheque_code, string pcheque_name, string pcheque_desc, string pactive, string pc_updated_by, ref string strMessage)
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
                oCommand.CommandText = "sp_CHEQUE_UPD";
                // - - - - - - - - - - - -             
                SqlParameter oParam_cheque_code = new SqlParameter("cheque_code", SqlDbType.NVarChar);
                oParam_cheque_code.Direction = ParameterDirection.Input;
                oParam_cheque_code.Value = pcheque_code;
                oCommand.Parameters.Add(oParam_cheque_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_cheque_name = new SqlParameter("cheque_name", SqlDbType.NVarChar);
                oParam_cheque_name.Direction = ParameterDirection.Input;
                oParam_cheque_name.Value = pcheque_name;
                oCommand.Parameters.Add(oParam_cheque_name);
                // - - - - - - - - - - - -             
                SqlParameter oParam_cheque_desc = new SqlParameter("cheque_desc", SqlDbType.NVarChar);
                oParam_cheque_desc.Direction = ParameterDirection.Input;
                oParam_cheque_desc.Value = pcheque_desc;
                oCommand.Parameters.Add(oParam_cheque_desc);
                // - - - - - - - - - - - -             
                SqlParameter oParam_Active = new SqlParameter("c_active", SqlDbType.NVarChar);
                oParam_Active.Direction = ParameterDirection.Input;
                oParam_Active.Value = pactive;
                oCommand.Parameters.Add(oParam_Active);
                // - - - - - - - - - - - -             
                SqlParameter oParam_c_updated_by = new SqlParameter("c_updated_by", SqlDbType.NVarChar);
                oParam_c_updated_by.Direction = ParameterDirection.Input;
                oParam_c_updated_by.Value = pc_updated_by;
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

        #region SP_DEL_CHEQUE
        public bool SP_DEL_CHEQUE(string pcheque_code, string pactive, string pc_updated_by, ref string strMessage)
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
                oCommand.CommandText = "sp_CHEQUE_DEL";
                // - - - - - - - - - - - -             
                SqlParameter oParam_cheque_code = new SqlParameter("cheque_code", SqlDbType.NVarChar);
                oParam_cheque_code.Direction = ParameterDirection.Input;
                oParam_cheque_code.Value = pcheque_code;
                oCommand.Parameters.Add(oParam_cheque_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_Active = new SqlParameter("c_active", SqlDbType.NVarChar);
                oParam_Active.Direction = ParameterDirection.Input;
                oParam_Active.Value = pactive;
                oCommand.Parameters.Add(oParam_Active);
                // - - - - - - - - - - - -             
                SqlParameter oParam_c_updated_by = new SqlParameter("c_updated_by", SqlDbType.NVarChar);
                oParam_c_updated_by.Direction = ParameterDirection.Input;
                oParam_c_updated_by.Value = pc_updated_by;
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


        #region SP_CHEQUE_HEAD_SEL
        public bool SP_CHEQUE_HEAD_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_CHEQUE_HEAD_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_CHEQUE_HEAD_SEL");
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

        #region SP_CHEQUE_HEAD_INS
        public bool SP_CHEQUE_HEAD_INS(
                string pcheque_doc,
                string pcheque_date,
                string pcheque_year,
                string ppay_month,
                string ppay_year,
                string pcheque_bank_code,
                string pcheque_comment,
                string pc_created_by, 
                string pcheque_type,
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
                oCommand.CommandText = "sp_CHEQUE_HEAD_INS";
                oCommand.Parameters.Add("@pcheque_doc", SqlDbType.VarChar).Value = pcheque_doc;
                oCommand.Parameters.Add("@pcheque_date", SqlDbType.DateTime).Value = cCommon.CheckDate(pcheque_date);
                oCommand.Parameters.Add("@pcheque_year", SqlDbType.VarChar).Value = pcheque_year;
                oCommand.Parameters.Add("@ppay_month", SqlDbType.VarChar).Value = ppay_month;
                oCommand.Parameters.Add("@ppay_year", SqlDbType.VarChar).Value = ppay_year;
                oCommand.Parameters.Add("@pcheque_bank_code", SqlDbType.VarChar).Value = pcheque_bank_code;
                oCommand.Parameters.Add("@pcheque_comment", SqlDbType.VarChar).Value = pcheque_comment;
                oCommand.Parameters.Add("@c_created_by", SqlDbType.VarChar).Value = pc_created_by;
                oCommand.Parameters.Add("@pcheque_type", SqlDbType.VarChar).Value = pcheque_type;
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

        #region SP_CHEQUE_HEAD_UPD
        public bool SP_CHEQUE_HEAD_UPD(
                string pcheque_doc,
                string pcheque_date,
                string pcheque_year,
                string ppay_month,
                string ppay_year,
                string pcheque_bank_code,
                string pcheque_comment,
                string pc_updated_by,
                string pcheque_type,
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
                oCommand.CommandText = "sp_CHEQUE_HEAD_UPD";
                oCommand.Parameters.Add("@pcheque_doc", SqlDbType.VarChar).Value = pcheque_doc;
                oCommand.Parameters.Add("@pcheque_date", SqlDbType.DateTime).Value = cCommon.CheckDate(pcheque_date);
                oCommand.Parameters.Add("@pcheque_year", SqlDbType.VarChar).Value = pcheque_year;
                oCommand.Parameters.Add("@ppay_month", SqlDbType.VarChar).Value = ppay_month;
                oCommand.Parameters.Add("@ppay_year", SqlDbType.VarChar).Value = ppay_year;
                oCommand.Parameters.Add("@pcheque_bank_code", SqlDbType.VarChar).Value = pcheque_bank_code;
                oCommand.Parameters.Add("@pcheque_comment", SqlDbType.VarChar).Value = pcheque_comment;
                oCommand.Parameters.Add("@pc_updated_by", SqlDbType.VarChar).Value = pc_updated_by;
                oCommand.Parameters.Add("@pcheque_type", SqlDbType.VarChar).Value = pcheque_type;
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

        #region SP_CHEQUE_HEAD_DEL
        public bool SP_CHEQUE_HEAD_DEL(
                string pcheque_doc,
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
                oCommand.CommandText = "sp_CHEQUE_HEAD_DEL";
                oCommand.Parameters.Add("@pcheque_doc", SqlDbType.VarChar).Value = pcheque_doc;
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


        #region SP_CHEQUE_DETAIL_SEL
        public bool SP_CHEQUE_DETAIL_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_CHEQUE_DETAIL_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_CHEQUE_DETAIL_SEL");
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

        #region SP_CHEQUE_DETAIL_INS
        public bool SP_CHEQUE_DETAIL_INS(
                string pcheque_doc,
                string pcheque_code,
                string pcheque_no,
                string pcheque_pvno,
                string pcheque_money,
                string pcheque_money_thai,
                string pcheque_comment_sub,
                string pcheque_print,
                string pdirector_code,
                string pcheque_date_print,
	            string pcheque_date_pay,
	            string pcheque_date_bank,
	            string pcheque_deka,
	            string pcheque_acccode,
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
                oCommand.CommandText = "sp_CHEQUE_DETAIL_INS";
                oCommand.Parameters.Add("@pcheque_doc", SqlDbType.VarChar).Value = pcheque_doc;
                oCommand.Parameters.Add("@pcheque_code", SqlDbType.VarChar).Value = pcheque_code;
                oCommand.Parameters.Add("@pcheque_no", SqlDbType.VarChar).Value = pcheque_no;
                oCommand.Parameters.Add("@pcheque_pvno", SqlDbType.VarChar).Value = pcheque_pvno;
                oCommand.Parameters.Add("@pcheque_money", SqlDbType.Money).Value = double.Parse(pcheque_money);
                oCommand.Parameters.Add("@pcheque_money_thai", SqlDbType.VarChar).Value = pcheque_money_thai;
                oCommand.Parameters.Add("@pcheque_comment_sub", SqlDbType.VarChar).Value = pcheque_comment_sub;
                oCommand.Parameters.Add("@pcheque_print", SqlDbType.VarChar).Value = pcheque_print;
                oCommand.Parameters.Add("@pdirector_code", SqlDbType.VarChar).Value = pdirector_code;
                oCommand.Parameters.Add("@pcheque_date_print", SqlDbType.DateTime).Value = cCommon.CheckDate(pcheque_date_print);
                oCommand.Parameters.Add("@pcheque_date_pay", SqlDbType.DateTime).Value = cCommon.CheckDate(pcheque_date_pay);
                oCommand.Parameters.Add("@pcheque_date_bank", SqlDbType.DateTime).Value = cCommon.CheckDate(pcheque_date_bank);
                oCommand.Parameters.Add("@pcheque_deka", SqlDbType.VarChar).Value = pcheque_deka;
                oCommand.Parameters.Add("@pcheque_acccode", SqlDbType.VarChar).Value = pcheque_acccode;
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

        #region SP_CHEQUE_DETAIL_UPD
        public bool SP_CHEQUE_DETAIL_UPD(
                string pcheque_detail_id ,
                string pcheque_doc,
                string pcheque_code,
                string pcheque_no,
                string pcheque_pvno,
                string pcheque_money,
                string pcheque_money_thai,
                string pcheque_comment_sub,
                string pcheque_print,
                string pdirector_code,
                string pcheque_date_print,
                string pcheque_date_pay,
                string pcheque_date_bank,
                string pcheque_deka,
                string pcheque_acccode,
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
                oCommand.CommandText = "sp_CHEQUE_DETAIL_UPD";
                oCommand.Parameters.Add("@pcheque_detail_id", SqlDbType.Int).Value = int.Parse(pcheque_detail_id);
                oCommand.Parameters.Add("@pcheque_doc", SqlDbType.VarChar).Value = pcheque_doc;
                oCommand.Parameters.Add("@pcheque_code", SqlDbType.VarChar).Value = pcheque_code;
                oCommand.Parameters.Add("@pcheque_no", SqlDbType.VarChar).Value = pcheque_no;
                oCommand.Parameters.Add("@pcheque_pvno", SqlDbType.VarChar).Value = pcheque_pvno;
                oCommand.Parameters.Add("@pcheque_money", SqlDbType.Money).Value = double.Parse(pcheque_money);
                oCommand.Parameters.Add("@pcheque_money_thai", SqlDbType.VarChar).Value = pcheque_money_thai;
                oCommand.Parameters.Add("@pcheque_comment_sub", SqlDbType.VarChar).Value = pcheque_comment_sub;
                oCommand.Parameters.Add("@pcheque_print", SqlDbType.VarChar).Value = pcheque_print;
                oCommand.Parameters.Add("@pdirector_code", SqlDbType.VarChar).Value = pdirector_code;
                oCommand.Parameters.Add("@pcheque_date_print", SqlDbType.DateTime).Value = cCommon.CheckDate(pcheque_date_print);
                oCommand.Parameters.Add("@pcheque_date_pay", SqlDbType.DateTime).Value = cCommon.CheckDate(pcheque_date_pay);
                oCommand.Parameters.Add("@pcheque_date_bank", SqlDbType.DateTime).Value = DateTime.Now.Date;
                oCommand.Parameters.Add("@pcheque_deka", SqlDbType.VarChar).Value = pcheque_deka;
                oCommand.Parameters.Add("@pcheque_acccode", SqlDbType.VarChar).Value = pcheque_acccode;
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


        #region SP_CHEQUE_DETAIL_PRINT_UPD
        public bool SP_CHEQUE_DETAIL_PRINT_UPD(
                string pcheque_detail_id,
                string pcheque_print,
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
                oCommand.CommandText = "sp_CHEQUE_DETAIL_PRINT_UPD";
                oCommand.Parameters.Add("@pcheque_detail_id", SqlDbType.Int).Value = int.Parse(pcheque_detail_id);
                oCommand.Parameters.Add("@pcheque_print", SqlDbType.VarChar).Value = pcheque_print;
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

        #region SP_CHEQUE_DETAIL_DEL
        public bool SP_CHEQUE_DETAIL_DEL(
                string pcheque_detail_id ,
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
                oCommand.CommandText = "sp_CHEQUE_DETAIL_DEL";
                oCommand.Parameters.Add("@pcheque_detail_id", SqlDbType.Int).Value = int.Parse(pcheque_detail_id);
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
