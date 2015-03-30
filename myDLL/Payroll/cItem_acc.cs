using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace myDLL
{
    public class cItem_acc : IDisposable
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

        public cItem_acc()
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

        #region SP_ITEM_ACC_SEL
        public bool SP_ITEM_ACC_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_ITEM_ACC_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_ITEM_ACC_SEL");
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

        #region SP_ITEM_ACC_INS
        public bool SP_ITEM_ACC_INS(string pitem_acc_year, string pitem_acc_code, string pitem_acc_name, string pActive, string pC_created_by, ref string strMessage)
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
                oCommand.CommandText = "sp_ITEM_ACC_INS";
                // - - - - - - - - - - - -             
                SqlParameter oParam_item_acc_year = new SqlParameter("item_acc_year", SqlDbType.NVarChar);
                oParam_item_acc_year.Direction = ParameterDirection.Input;
                oParam_item_acc_year.Value = pitem_acc_year;
                oCommand.Parameters.Add(oParam_item_acc_year);
                // - - - - - - - - - - - -             
                SqlParameter oParam_item_acc_code = new SqlParameter("item_acc_code", SqlDbType.NVarChar);
                oParam_item_acc_code.Direction = ParameterDirection.Input;
                oParam_item_acc_code.Value = pitem_acc_code;
                oCommand.Parameters.Add(oParam_item_acc_code);
                // - - - - - - - - - - - -      
                SqlParameter oParam_item_acc_name = new SqlParameter("item_acc_name", SqlDbType.NVarChar);
                oParam_item_acc_name.Direction = ParameterDirection.Input;
                oParam_item_acc_name.Value = pitem_acc_name;
                oCommand.Parameters.Add(oParam_item_acc_name);
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

        #region SP_ITEM_ACC_UPD
        public bool SP_ITEM_ACC_UPD(string pitem_acc_id, string pitem_acc_code, string pitem_acc_year, string pitem_acc_name, string pActive, string pC_updated_by, ref string strMessage)
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
                oCommand.CommandText = "sp_ITEM_ACC_UPD";
                // - - - - - - - - - - - -             
                SqlParameter oParam_item_acc_id = new SqlParameter("item_acc_id", SqlDbType.Int);
                oParam_item_acc_id.Direction = ParameterDirection.Input;
                oParam_item_acc_id.Value = int.Parse(pitem_acc_id);
                oCommand.Parameters.Add(oParam_item_acc_id);
                // - - - - - - - - - - - -             
                SqlParameter oParam_item_acc_code = new SqlParameter("item_acc_code", SqlDbType.NVarChar);
                oParam_item_acc_code.Direction = ParameterDirection.Input;
                oParam_item_acc_code.Value = pitem_acc_code;
                oCommand.Parameters.Add(oParam_item_acc_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_item_acc_year = new SqlParameter("item_acc_year", SqlDbType.NVarChar);
                oParam_item_acc_year.Direction = ParameterDirection.Input;
                oParam_item_acc_year.Value = pitem_acc_year;
                oCommand.Parameters.Add(oParam_item_acc_year);
                // - - - - - - - - - - - -             
                SqlParameter oParam_item_acc_name = new SqlParameter("item_acc_name", SqlDbType.NVarChar);
                oParam_item_acc_name.Direction = ParameterDirection.Input;
                oParam_item_acc_name.Value = pitem_acc_name;
                oCommand.Parameters.Add(oParam_item_acc_name);
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

        #region SP_ITEM_ACC_DEL
        public bool SP_ITEM_ACC_DEL(string pitem_acc_id, string pActive, string pC_updated_by, ref string strMessage)
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
                oCommand.CommandText = "sp_ITEM_ACC_DEL";

                // - - - - - - - - - - - -             
                SqlParameter oParam_item_acc_id = new SqlParameter("item_acc_id", SqlDbType.Int);
                oParam_item_acc_id.Direction = ParameterDirection.Input;
                oParam_item_acc_id.Value = int.Parse(pitem_acc_id);
                oCommand.Parameters.Add(oParam_item_acc_id);

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



        #region SP_ITEM_ACC_HEAD_SEL
        public bool SP_ITEM_ACC_HEAD_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "SP_ITEM_ACC_HEAD_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "SP_ITEM_ACC_HEAD_SEL");
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

        #region SP_ITEM_ACC_HEAD_DEL
        public bool SP_ITEM_ACC_HEAD_DEL(string pItem_acc_doc, ref string strMessage)
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
                oCommand.CommandText = "SP_ITEM_ACC_HEAD_DEL";
                // - - - - - - - - - - - -             
                SqlParameter oParam_item_acc_doc = new SqlParameter("pitem_acc_doc", SqlDbType.NVarChar);
                oParam_item_acc_doc.Direction = ParameterDirection.Input;
                oParam_item_acc_doc.Value = pItem_acc_doc;
                oCommand.Parameters.Add(oParam_item_acc_doc);
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

        #region SP_ITEM_ACC_HEAD_INS
        public bool SP_ITEM_ACC_HEAD_INS(
                string ppayment_year,
                string ppay_month,
                string ppay_year,
                string pcomments,
                string pc_created_by,
                string pbudget_type,
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
                oCommand.CommandText = "SP_ITEM_ACC_HEAD_INS";
                oCommand.Parameters.Add("pitem_acc_year", SqlDbType.VarChar).Value = ppayment_year;
                oCommand.Parameters.Add("ppay_month", SqlDbType.VarChar).Value = ppay_month;
                oCommand.Parameters.Add("ppay_year", SqlDbType.VarChar).Value = ppay_year;
                oCommand.Parameters.Add("pitem_acc_comment", SqlDbType.VarChar).Value = pcomments;
                oCommand.Parameters.Add("c_created_by", SqlDbType.VarChar).Value = pc_created_by;
                oCommand.Parameters.Add("pbudget_type", SqlDbType.VarChar).Value = pbudget_type;                
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

        #region SP_ITEM_ACC_HEAD_UPD
        public bool SP_ITEM_ACC_HEAD_UPD(
                string pitem_acc_doc,
                string ppayment_year,
                string ppay_month,
                string ppay_year,
                string pcomments,
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
                oCommand.CommandText = "SP_ITEM_ACC_HEAD_UPD";
                oCommand.Parameters.Add("pitem_acc_doc", SqlDbType.VarChar).Value = pitem_acc_doc;
                oCommand.Parameters.Add("pitem_acc_year", SqlDbType.VarChar).Value = ppayment_year;
                oCommand.Parameters.Add("ppay_month", SqlDbType.VarChar).Value = ppay_month;
                oCommand.Parameters.Add("ppay_year", SqlDbType.VarChar).Value = ppay_year;
                oCommand.Parameters.Add("pitem_acc_comment", SqlDbType.VarChar).Value = pcomments;
                oCommand.Parameters.Add("pc_updated_by", SqlDbType.VarChar).Value = pc_created_by;
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

        #region SP_ITEM_ACC_TMP_SEL
        public bool SP_ITEM_ACC_TMP_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "SP_ITEM_ACC_TMP_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "SP_ITEM_ACC_TMP_SEL");
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


        #region sp_ITEM_ACC_INCOME_TMP_SEL
        public bool SP_ITEM_ACC_INCOME_TMP_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_ITEM_ACC_INCOME_TMP_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_ITEM_ACC_INCOME_TMP_SEL");
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

        #region SP_ITEM_ACC_DETAIL_INCOME_SEL
        public bool SP_ITEM_ACC_DETAIL_INCOME_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_ITEM_ACC_DETAIL_INCOME_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_ITEM_ACC_DETAIL_INCOME_SEL");
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

        #region SP_ITEM_ACC_DETAIL_SEL
        public bool SP_ITEM_ACC_DETAIL_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "SP_ITEM_ACC_DETAIL_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "SP_ITEM_ACC_DETAIL_SEL");
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
        
        #region SP_ITEM_ACC_DETAIL_DEL
        public bool SP_ITEM_ACC_DETAIL_DEL(string pItem_acc_detail_id, ref string strMessage)
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
                oCommand.CommandText = "SP_ITEM_ACC_DETAIL_DEL";
                // - - - - - - - - - - - -             
                SqlParameter oParam_item_acc_doc = new SqlParameter("pitem_acc_detail_id", SqlDbType.Int);
                oParam_item_acc_doc.Direction = ParameterDirection.Input;
                oParam_item_acc_doc.Value = int.Parse(pItem_acc_detail_id);
                oCommand.Parameters.Add(oParam_item_acc_doc);
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

        #region SP_ITEM_ACC_DETAIL_INS
        public bool SP_ITEM_ACC_DETAIL_INS(
                string pitem_acc_doc,
                string pitem_acc_deka,
                string pitem_acc_group,
                string pitem_acc_amount,
                string pitem_acc_tax,
                string pitem_acc_total,
                string pitem_acc_code,
                string pitem_project_code,
                string pitem_code,
                string pproduce_code,
                string pperson_group_code_acc,
                string ppayment_back_type,
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
                oCommand.CommandText = "SP_ITEM_ACC_DETAIL_INS";
                oCommand.Parameters.Add("pitem_acc_doc", SqlDbType.VarChar).Value = pitem_acc_doc;
                oCommand.Parameters.Add("pitem_acc_deka", SqlDbType.VarChar).Value = pitem_acc_deka;
                oCommand.Parameters.Add("pitem_acc_group", SqlDbType.VarChar).Value = pitem_acc_group;
                oCommand.Parameters.Add("pitem_acc_amount", SqlDbType.Money).Value = double.Parse(pitem_acc_amount);
                oCommand.Parameters.Add("pitem_acc_tax", SqlDbType.Money).Value = double.Parse(pitem_acc_tax);
                oCommand.Parameters.Add("pitem_acc_total", SqlDbType.Money).Value = double.Parse(pitem_acc_total);
                oCommand.Parameters.Add("pitem_acc_code", SqlDbType.VarChar).Value = pitem_acc_code;
                oCommand.Parameters.Add("pitem_project_code", SqlDbType.VarChar).Value = pitem_project_code;
                oCommand.Parameters.Add("pitem_code", SqlDbType.VarChar).Value = pitem_code;
                oCommand.Parameters.Add("pproduce_code", SqlDbType.VarChar).Value = pproduce_code;
                oCommand.Parameters.Add("pperson_group_code", SqlDbType.VarChar).Value = pperson_group_code_acc;
                oCommand.Parameters.Add("ppayment_back_type", SqlDbType.VarChar).Value = ppayment_back_type;

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

        #region SP_ITEM_ACC_DETAIL_UPD
        public bool SP_ITEM_ACC_DETAIL_UPD(
                string pitem_acc_detail_id,
                string pitem_acc_doc,
                string pitem_acc_deka,
                string pitem_acc_group,
                string pitem_acc_amount,
                string pitem_acc_tax,
                string pitem_acc_total,
                string pitem_acc_code,
                string pitem_project_code,
                string pitem_code,
                string pproduce_code,
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
                oCommand.CommandText = "SP_ITEM_ACC_DETAIL_UPD";
                oCommand.Parameters.Add("pitem_acc_detail_id", SqlDbType.Int).Value = int.Parse(pitem_acc_detail_id);
                oCommand.Parameters.Add("pitem_acc_doc", SqlDbType.VarChar).Value = pitem_acc_doc;
                oCommand.Parameters.Add("pitem_acc_deka", SqlDbType.VarChar).Value = pitem_acc_deka;
                oCommand.Parameters.Add("pitem_acc_group", SqlDbType.VarChar).Value = pitem_acc_group;
                oCommand.Parameters.Add("pitem_acc_amount", SqlDbType.Money).Value = double.Parse(pitem_acc_amount);
                oCommand.Parameters.Add("pitem_acc_tax", SqlDbType.Money).Value = double.Parse(pitem_acc_tax);
                oCommand.Parameters.Add("pitem_acc_total", SqlDbType.Money).Value = double.Parse(pitem_acc_total);
                oCommand.Parameters.Add("pitem_acc_code", SqlDbType.VarChar).Value = pitem_acc_code;
                oCommand.Parameters.Add("pitem_project_code", SqlDbType.VarChar).Value = pitem_project_code;
                oCommand.Parameters.Add("pitem_code", SqlDbType.VarChar).Value = pitem_code;
                oCommand.Parameters.Add("pproduce_code", SqlDbType.VarChar).Value = pproduce_code;

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
