using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace myDLL
{
    public class cPayment : IDisposable
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

        public cPayment()
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

        #region SP_PAYMENT_ALL_SEL
        public bool SP_PAYMENT_ALL_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_PAYMENT_ALL_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_PAYMENT_ALL_SEL");
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

        #region SP_PAYMENT_SEL
        public bool SP_PAYMENT_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_PAYMENT_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_PAYMENT_SEL");
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

        #region SP_PAYMENT_HEAD_SEL
        public bool SP_PAYMENT_HEAD_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_PAYMENT_HEAD_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_PAYMENT_HEAD_SEL");
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

        #region SP_PAYMENT_DEL
        public bool SP_PAYMENT_DEL(string pPayment_doc, string pActive, string pC_updated_by, ref string strMessage)
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
                oCommand.CommandText = "sp_PAYMENT_HEAD_DEL";
                // - - - - - - - - - - - -             
                SqlParameter oParam_PERSON_code = new SqlParameter("ppayment_doc", SqlDbType.NVarChar);
                oParam_PERSON_code.Direction = ParameterDirection.Input;
                oParam_PERSON_code.Value = pPayment_doc;
                oCommand.Parameters.Add(oParam_PERSON_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_Active = new SqlParameter("pc_active", SqlDbType.NVarChar);
                oParam_Active.Direction = ParameterDirection.Input;
                oParam_Active.Value = pActive;
                oCommand.Parameters.Add(oParam_Active);
                // - - - - - - - - - - - -             
                SqlParameter oParam_c_updated_by = new SqlParameter("pc_updated_by", SqlDbType.NVarChar);
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

        #region SP_PAYMENT_HEAD_INS
        public bool SP_PAYMENT_HEAD_INS(
                string ppayment_date,
                string ppayment_year,
                string ppay_month,
                string ppay_year,
                string pperson_code,
                string pposition_code,
                string pperson_level,
                string pperson_grou_code,
                string pperson_manage_code,
                string pbudget_plan_code,
                string pperson_work_status_code,
                string ppayment_recv,
                string ppayment_pay,
                string ppayment_net,
                string pcomments,
                string pc_status,
                string pc_active,
                string ptype_position_code,
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
                oCommand.CommandText = "sp_PAYMENT_HEAD_INS";
                oCommand.Parameters.Add("ppayment_date", SqlDbType.DateTime).Value = cCommon.CheckDate(ppayment_date);
                oCommand.Parameters.Add("ppayment_year", SqlDbType.VarChar).Value = ppayment_year;
                oCommand.Parameters.Add("ppay_month", SqlDbType.VarChar).Value = ppay_month;
                oCommand.Parameters.Add("ppay_year", SqlDbType.VarChar).Value = ppay_year;
                oCommand.Parameters.Add("pperson_code", SqlDbType.VarChar).Value = pperson_code;
                oCommand.Parameters.Add("pposition_code", SqlDbType.VarChar).Value = pposition_code;
                oCommand.Parameters.Add("pperson_level", SqlDbType.VarChar).Value = pperson_level;
                oCommand.Parameters.Add("pperson_group_code", SqlDbType.VarChar).Value = pperson_grou_code;
                oCommand.Parameters.Add("pperson_manage_code", SqlDbType.VarChar).Value = pperson_manage_code;
                oCommand.Parameters.Add("pbudget_plan_code", SqlDbType.VarChar).Value = pbudget_plan_code;
                oCommand.Parameters.Add("pperson_work_status_code", SqlDbType.VarChar).Value = pperson_work_status_code;
                oCommand.Parameters.Add("ppayment_recv", SqlDbType.Money).Value = float.Parse(ppayment_recv);
                oCommand.Parameters.Add("ppayment_pay", SqlDbType.Money).Value = float.Parse(ppayment_pay);
                oCommand.Parameters.Add("ppayment_net", SqlDbType.Money).Value = float.Parse(ppayment_net);
                oCommand.Parameters.Add("pcomments", SqlDbType.VarChar).Value = pcomments;
                oCommand.Parameters.Add("pc_status", SqlDbType.VarChar).Value = pc_status;
                oCommand.Parameters.Add("pc_active", SqlDbType.VarChar).Value = pc_active;
                oCommand.Parameters.Add("pc_created_by", SqlDbType.VarChar).Value = pc_created_by;
                oCommand.Parameters.Add("ptype_position_code", SqlDbType.VarChar).Value = ptype_position_code;
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

        #region SP_PAYMENT_HEAD_UPD
        public bool SP_PAYMENT_HEAD_UPD(
                string ppayment_doc,
                string pperson_code,
                string pposition_code,
                string pperson_level,
                string pperson_grou_code,
                string pperson_manage_code,
                string pbudget_plan_code,
                string pperson_work_status_code,
                string pcomments,
                string pc_active,
                string pc_created_by,
                string ptype_position_code,
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
                oCommand.CommandText = "sp_PAYMENT_HEAD_UPD";
                oCommand.Parameters.Add("ppayment_doc", SqlDbType.VarChar).Value = ppayment_doc;
                oCommand.Parameters.Add("pperson_code", SqlDbType.VarChar).Value = pperson_code;
                oCommand.Parameters.Add("pposition_code", SqlDbType.VarChar).Value = pposition_code;
                oCommand.Parameters.Add("pperson_level", SqlDbType.VarChar).Value = pperson_level;
                oCommand.Parameters.Add("pperson_group_code", SqlDbType.VarChar).Value = pperson_grou_code;
                oCommand.Parameters.Add("pperson_manage_code", SqlDbType.VarChar).Value = pperson_manage_code;
                oCommand.Parameters.Add("pbudget_plan_code", SqlDbType.VarChar).Value = pbudget_plan_code;
                oCommand.Parameters.Add("pperson_work_status_code", SqlDbType.VarChar).Value = pperson_work_status_code;
                oCommand.Parameters.Add("pcomments", SqlDbType.VarChar).Value = pcomments;
                oCommand.Parameters.Add("pc_active", SqlDbType.VarChar).Value = pc_active;
                oCommand.Parameters.Add("pc_updated_by", SqlDbType.VarChar).Value = pc_created_by;
                oCommand.Parameters.Add("ptype_position_code", SqlDbType.VarChar).Value = ptype_position_code;

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

        #region SP_PAYMENT_SUM_UPD
        public bool SP_PAYMENT_SUM_UPD(
                string ppayment_doc,
                string ppayment_recv,
                string ppayment_pay,
                string ppayment_net,
                string pcomments,
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
                oCommand.CommandText = "sp_PAYMENT_SUM_UPD";
                oCommand.Parameters.Add("ppayment_doc", SqlDbType.VarChar).Value = ppayment_doc;
                oCommand.Parameters.Add("ppayment_recv", SqlDbType.Money).Value = float.Parse(ppayment_recv);
                oCommand.Parameters.Add("ppayment_pay", SqlDbType.Money).Value = float.Parse(ppayment_pay);
                oCommand.Parameters.Add("ppayment_net", SqlDbType.Money).Value = float.Parse(ppayment_net);
                oCommand.Parameters.Add("pcomments", SqlDbType.VarChar).Value = pcomments;
                oCommand.Parameters.Add("pc_active", SqlDbType.VarChar).Value = pc_active;
                oCommand.Parameters.Add("pc_updated_by", SqlDbType.VarChar).Value = pc_updated_by;
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

        #region SP_PAYMENT_COMM_UPD
        public bool SP_PAYMENT_COMM_UPD(
                string ppayment_doc,
                string pcomments,
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
                oCommand.CommandText = "sp_PAYMENT_COMM_UPD";
                oCommand.Parameters.Add("ppayment_doc", SqlDbType.VarChar).Value = ppayment_doc;
                oCommand.Parameters.Add("pcomments", SqlDbType.VarChar).Value = pcomments;
                oCommand.Parameters.Add("pc_active", SqlDbType.VarChar).Value = pc_active;
                oCommand.Parameters.Add("pc_updated_by", SqlDbType.VarChar).Value = pc_updated_by;
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

        #region SP_PAYMENT_DETAIL_INS
        public bool SP_PAYMENT_DETAIL_INS
            (
                string ppayment_doc,
                string pitem_code,
                string ppayment_item_recv,
                string ppayment_item_pay,
                string ppayment_item_tax,
                string ppayment_item_sos,
                string pcomments_sub,
                string pc_active,
                string pc_created_by,
                ref string strMessage
            )
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
                oCommand.CommandText = "sp_PAYMENT_DETAIL_INS";
                oCommand.Parameters.Add("ppayment_doc", SqlDbType.VarChar).Value = ppayment_doc;
                oCommand.Parameters.Add("pitem_code", SqlDbType.VarChar).Value = pitem_code;
                oCommand.Parameters.Add("ppayment_item_recv", SqlDbType.Money).Value = float.Parse(ppayment_item_recv);
                oCommand.Parameters.Add("ppayment_item_pay", SqlDbType.Money).Value = float.Parse(ppayment_item_pay);
                oCommand.Parameters.Add("ppayment_item_tax", SqlDbType.VarChar).Value = ppayment_item_tax;
                oCommand.Parameters.Add("ppayment_item_sos", SqlDbType.VarChar).Value = ppayment_item_sos;
                oCommand.Parameters.Add("pcomments_sub", SqlDbType.VarChar).Value = pcomments_sub;
                oCommand.Parameters.Add("pc_active", SqlDbType.VarChar).Value = pc_active;
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

        #region SP_PAYMENT_DETAIL_BUDGET_INS
        public bool SP_PAYMENT_DETAIL_BUDGET_INS
            (
                string ppayment_doc,
                string pitem_code,
                string ppayment_item_recv,
                string ppayment_item_pay,
                string ppayment_item_tax,
                string ppayment_item_sos,
                string pcomments_sub,
                string pc_active,
                string pc_created_by,
                string ppayment_back,
                string pbudget_plan_code,
                string pbudget_plan_type,
                string ppayment_lot_code,
                string pbudget_type,
                string pperson_group_code,
                ref string strMessage
            )
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
                oCommand.CommandText = "sp_PAYMENT_DETAIL_BUDGET_INS";
                oCommand.Parameters.Add("ppayment_doc", SqlDbType.VarChar).Value = ppayment_doc;
                oCommand.Parameters.Add("pitem_code", SqlDbType.VarChar).Value = pitem_code;
                oCommand.Parameters.Add("ppayment_item_recv", SqlDbType.Money).Value = float.Parse(ppayment_item_recv);
                oCommand.Parameters.Add("ppayment_item_pay", SqlDbType.Money).Value = float.Parse(ppayment_item_pay);
                oCommand.Parameters.Add("ppayment_item_tax", SqlDbType.VarChar).Value = ppayment_item_tax;
                oCommand.Parameters.Add("ppayment_item_sos", SqlDbType.VarChar).Value = ppayment_item_sos;
                oCommand.Parameters.Add("pcomments_sub", SqlDbType.VarChar).Value = pcomments_sub;
                oCommand.Parameters.Add("pc_active", SqlDbType.VarChar).Value = pc_active;
                oCommand.Parameters.Add("pc_created_by", SqlDbType.VarChar).Value = pc_created_by;
                oCommand.Parameters.Add("ppayment_back", SqlDbType.VarChar).Value = ppayment_back;
                oCommand.Parameters.Add("pbudget_plan_code", SqlDbType.VarChar).Value = pbudget_plan_code;
                oCommand.Parameters.Add("pbudget_plan_type", SqlDbType.VarChar).Value = pbudget_plan_type;
                oCommand.Parameters.Add("ppayment_lot_code", SqlDbType.VarChar).Value = ppayment_lot_code;
                oCommand.Parameters.Add("pperson_group_code", SqlDbType.VarChar).Value = pperson_group_code;
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



        #region SP_IMPORT_PAYMENT_ITEM_INS
        public bool SP_IMPORT_PAYMENT_ITEM_INS
            (
                string ppayment_year,
                string ppay_month,
                string ppay_year,
                string pperson_code,
                string pitem_code,
                string pitem_amt,
                string puser_guid,
                string pc_created_by,
                ref string strMessage
            )
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
                oCommand.CommandText = "sp_IMPORT_PAYMENT_ITEM_INS";
                oCommand.Parameters.Add("ppayment_year", SqlDbType.VarChar).Value = ppayment_year;
                oCommand.Parameters.Add("ppay_month", SqlDbType.VarChar).Value = ppay_month;
                oCommand.Parameters.Add("ppay_year", SqlDbType.VarChar).Value = ppay_year;
                oCommand.Parameters.Add("pperson_code", SqlDbType.VarChar).Value = pperson_code;
                oCommand.Parameters.Add("pitem_code", SqlDbType.VarChar).Value = pitem_code;
                oCommand.Parameters.Add("pitem_amt", SqlDbType.Money).Value = float.Parse(pitem_amt);
                oCommand.Parameters.Add("puser_guid", SqlDbType.VarChar).Value = puser_guid;
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

        #region SP_PAYMENT_DETAIL_UPD
        public bool SP_PAYMENT_DETAIL_UPD
            (
                string ppayment_doc,
                string pitem_code,
                string ppayment_item_recv,
                string ppayment_item_pay,
                string ppayment_item_tax,
                string ppayment_item_sos,
                string pcomments_sub,
                string pc_active,
                string pc_updated_by,
                string pbudget_type,
                string strpayment_detail_id,
                ref string strMessage
            )
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
                oCommand.CommandText = "sp_PAYMENT_DETAIL_UPD";
                oCommand.Parameters.Add("ppayment_doc", SqlDbType.VarChar).Value = ppayment_doc;
                oCommand.Parameters.Add("pitem_code", SqlDbType.VarChar).Value = pitem_code;
                oCommand.Parameters.Add("ppayment_item_recv", SqlDbType.Money).Value = float.Parse(ppayment_item_recv);
                oCommand.Parameters.Add("ppayment_item_pay", SqlDbType.Money).Value = float.Parse(ppayment_item_pay);
                oCommand.Parameters.Add("ppayment_item_tax", SqlDbType.VarChar).Value = ppayment_item_tax;
                oCommand.Parameters.Add("ppayment_item_sos", SqlDbType.VarChar).Value = ppayment_item_sos;
                oCommand.Parameters.Add("pcomments_sub", SqlDbType.VarChar).Value = pcomments_sub;
                oCommand.Parameters.Add("pc_active", SqlDbType.VarChar).Value = pc_active;
                oCommand.Parameters.Add("pc_updated_by", SqlDbType.VarChar).Value = pc_updated_by;
                oCommand.Parameters.Add("pbudget_type", SqlDbType.VarChar).Value = pbudget_type;
                oCommand.Parameters.Add("ppayment_detail_id", SqlDbType.BigInt).Value = long.Parse(strpayment_detail_id);

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

        #region SP_PAYMENT_DETAIL_BUDGET_UPD
        public bool SP_PAYMENT_DETAIL_BUDGET_UPD
            (
                string ppayment_doc,
                string pitem_code,
                string pbudget_plan_code,
                string pbudget_plan_type,
                string ppayment_lot_code,
                string pbudget_plan_code_old,
                string ppayment_lot_code_old,
                string pperson_group_code,
                string pbudget_type_new,
                string pbudget_type_old,
                string strpayment_detail_id,
                ref string strMessage
            )
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
                oCommand.CommandText = "sp_PAYMENT_DETAIL_BUDGET_UPD";
                oCommand.Parameters.Add("ppayment_doc", SqlDbType.VarChar).Value = ppayment_doc;
                oCommand.Parameters.Add("pitem_code", SqlDbType.VarChar).Value = pitem_code;
                oCommand.Parameters.Add("pbudget_plan_code", SqlDbType.VarChar).Value = pbudget_plan_code;
                oCommand.Parameters.Add("pbudget_plan_type", SqlDbType.VarChar).Value = pbudget_plan_type;
                oCommand.Parameters.Add("ppayment_lot_code", SqlDbType.VarChar).Value = ppayment_lot_code;
                oCommand.Parameters.Add("pbudget_plan_code_old", SqlDbType.VarChar).Value = pbudget_plan_code_old;
                oCommand.Parameters.Add("ppayment_lot_code_old", SqlDbType.VarChar).Value = ppayment_lot_code_old;
                oCommand.Parameters.Add("pperson_group_code", SqlDbType.VarChar).Value = pperson_group_code;
                oCommand.Parameters.Add("pbudget_type_new", SqlDbType.VarChar).Value = pbudget_type_new;
                oCommand.Parameters.Add("pbudget_type_old", SqlDbType.VarChar).Value = pbudget_type_old;
                oCommand.Parameters.Add("ppayment_detail_id", SqlDbType.BigInt).Value = long.Parse(strpayment_detail_id);
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

        #region SP_PAYMENT_DETAIL_DEL
        public bool SP_PAYMENT_DETAIL_DEL
            (
                string ppayment_doc,
                string pitem_code,
                string pc_active,
                string pc_updated_by,
                ref string strMessage
            )
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
                oCommand.CommandText = "sp_PAYMENT_DETAIL_DEL";
                oCommand.Parameters.Add("ppayment_doc", SqlDbType.VarChar).Value = ppayment_doc;
                oCommand.Parameters.Add("pitem_code", SqlDbType.VarChar).Value = pitem_code;
                oCommand.Parameters.Add("pc_active", SqlDbType.VarChar).Value = pc_active;
                oCommand.Parameters.Add("pc_updated_by", SqlDbType.VarChar).Value = pc_updated_by;
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


        #region SP_PAYMENT_DETAIL_BUDGET_DEL
        public bool SP_PAYMENT_DETAIL_BUDGET_DEL
            (
                string ppayment_doc,
                string pitem_code,
                string pc_updated_by,
                string pbudget_type,
                string ppayment_detail_id,
            ref string strMessage
            )
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
                oCommand.CommandText = "SP_PAYMENT_DETAIL_BUDGET_DEL";
                oCommand.Parameters.Add("ppayment_doc", SqlDbType.VarChar).Value = ppayment_doc;
                oCommand.Parameters.Add("pitem_code", SqlDbType.VarChar).Value = pitem_code;
                oCommand.Parameters.Add("pc_updated_by", SqlDbType.VarChar).Value = pc_updated_by;
                oCommand.Parameters.Add("pbudget_type", SqlDbType.VarChar).Value = pbudget_type;
                oCommand.Parameters.Add("ppayment_detail_id", SqlDbType.BigInt).Value = long.Parse(ppayment_detail_id);
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




        #region SP_PAYMENT_MEMBER_TEMP_SEL
        public bool SP_PAYMENT_MEMBER_TEMP_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_PAYMENT_MEMBER_TEMP_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_PAYMENT_MEMBER_TEMP_SEL");
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

        #region SP_PAYMENT_MEMBER_TYPE_TEMP_SEL
        public bool SP_PAYMENT_MEMBER_TYPE_TEMP_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_PAYMENT_MEMBER_TYPE_TEMP_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_PAYMENT_MEMBER_TYPE_TEMP_SEL");
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

        #region SP_PAYMENT_ITEM_TEMP_SEL
        public bool SP_PAYMENT_ITEM_TEMP_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_PAYMENT_ITEM_TEMP_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_PAYMENT_MEMBER_TEMP_SEL");
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

        #region SP_PAYMENT_ITEM_IMPORT
        public bool SP_PAYMENT_ITEM_IMPORT(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_PAYMENT_ITEM_IMPORT";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_PAYMENT_ITEM_IMPORT");
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

        #region SP_PAYMENT_ITEM_IMPORT_DEL
        public bool SP_PAYMENT_ITEM_IMPORT_DEL(string puser_guid, string pC_updated_by, ref string strMessage)
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
                oCommand.CommandText = "sp_IMPORT_PAYMENT_ITEM_DEL";
                // - - - - - - - - - - - -             
                SqlParameter oParam_puser_guid = new SqlParameter("puser_guid", SqlDbType.NVarChar);
                oParam_puser_guid.Direction = ParameterDirection.Input;
                oParam_puser_guid.Value = puser_guid;
                oCommand.Parameters.Add(oParam_puser_guid);
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

        #region SP_PAYMENT_ACC_INS
        public bool SP_PAYMENT_ACC_INS
            (
                string ppayment_doc,
                string pitem_code,
                string ppayment_acc,
                string pc_active,
                string pc_created_by,
                ref string strMessage
            )
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
                oCommand.CommandText = "sp_PAYMENT_ACC_INS";
                oCommand.Parameters.Add("ppayment_doc", SqlDbType.VarChar).Value = ppayment_doc;
                oCommand.Parameters.Add("pitem_code", SqlDbType.VarChar).Value = pitem_code;
                oCommand.Parameters.Add("ppayment_acc", SqlDbType.Money).Value = float.Parse(ppayment_acc);
                oCommand.Parameters.Add("pc_active", SqlDbType.VarChar).Value = pc_active;
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

        #region SP_PAYMENT_ACC_UPD
        public bool SP_PAYMENT_ACC_UPD
            (
                string ppayment_doc,
                string pitem_code,
                string ppayment_acc,
                string pc_active,
                string pc_updated_by,
                ref string strMessage
            )
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
                oCommand.CommandText = "sp_PAYMENT_ACC_UPD";
                oCommand.Parameters.Add("ppayment_doc", SqlDbType.VarChar).Value = ppayment_doc;
                oCommand.Parameters.Add("pitem_code", SqlDbType.VarChar).Value = pitem_code;
                oCommand.Parameters.Add("ppayment_acc", SqlDbType.Money).Value = float.Parse(ppayment_acc);
                oCommand.Parameters.Add("pc_active", SqlDbType.VarChar).Value = pc_active;
                oCommand.Parameters.Add("pc_updated_by", SqlDbType.VarChar).Value = pc_updated_by;
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

        #region SP_PAYMENT_ACC_TWIN_UPD
        public bool SP_PAYMENT_ACC_TWIN_UPD
            (
                string ppayment_doc,
                string pitem_code,
                string ppayment_acc,
                string pc_active,
                string pc_updated_by,
                string pbudget_plan_code,
                string pbudget_plan_type,
                string ppayment_lot_code,
                string pbudget_plan_code_old,
                string ppayment_lot_code_old,
                string pperson_group_code,
                ref string strMessage
            )
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
                oCommand.CommandText = "sp_PAYMENT_ACC_TWIN_UPD";
                oCommand.Parameters.Add("ppayment_doc", SqlDbType.VarChar).Value = ppayment_doc;
                oCommand.Parameters.Add("pitem_code", SqlDbType.VarChar).Value = pitem_code;
                oCommand.Parameters.Add("ppayment_acc", SqlDbType.Money).Value = float.Parse(ppayment_acc);
                oCommand.Parameters.Add("pc_active", SqlDbType.VarChar).Value = pc_active;
                oCommand.Parameters.Add("pc_updated_by", SqlDbType.VarChar).Value = pc_updated_by;

                oCommand.Parameters.Add("pbudget_plan_code", SqlDbType.VarChar).Value = pbudget_plan_code;
                oCommand.Parameters.Add("pbudget_plan_type", SqlDbType.VarChar).Value = pbudget_plan_type;
                oCommand.Parameters.Add("ppayment_lot_code", SqlDbType.VarChar).Value = ppayment_lot_code;
                oCommand.Parameters.Add("pbudget_plan_code_old", SqlDbType.VarChar).Value = pbudget_plan_code_old;
                oCommand.Parameters.Add("ppayment_lot_code_old", SqlDbType.VarChar).Value = ppayment_lot_code_old;
                oCommand.Parameters.Add("pperson_group_code", SqlDbType.VarChar).Value = pperson_group_code;

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

        #region SP_PAYMENT_ACC_DEL
        public bool SP_PAYMENT_ACC_DEL
            (
                string ppayment_doc,
                string pitem_code,
                ref string strMessage
            )
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
                oCommand.CommandText = "sp_PAYMENT_ACC_DEL";
                oCommand.Parameters.Add("ppayment_doc", SqlDbType.VarChar).Value = ppayment_doc;
                oCommand.Parameters.Add("pitem_code", SqlDbType.VarChar).Value = pitem_code;
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

        #region SP_PAYMENT_ACC_SEL
        public bool SP_PAYMENT_ACC_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_PAYMENT_ACC_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_PAYMENT_ACC_SEL");
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

        #region SP_PAYMENT_DEBIT_SEL
        public bool SP_PAYMENT_DEBIT_SEL(string strCriteria, string strCriteria2, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_PAYMENT_DEBIT_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);

                SqlParameter oParamI_vc_criteria2 = new SqlParameter("vc_criteria2", SqlDbType.NVarChar);
                oParamI_vc_criteria2.Direction = ParameterDirection.Input;
                oParamI_vc_criteria2.Value = strCriteria2;
                oCommand.Parameters.Add(oParamI_vc_criteria2);

                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_PAYMENT_DEBIT_SEL");
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

        #region SP_PAYMENT_DEBIT_ALL_SEL
        public bool SP_PAYMENT_DEBIT_ALL_SEL(string strCriteria, string strCriteria2, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_PAYMENT_DEBIT_ALL_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);

                SqlParameter oParamI_vc_criteria2 = new SqlParameter("vc_criteria2", SqlDbType.NVarChar);
                oParamI_vc_criteria2.Direction = ParameterDirection.Input;
                oParamI_vc_criteria2.Value = strCriteria2;
                oCommand.Parameters.Add(oParamI_vc_criteria2);

                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_PAYMENT_DEBIT_ALL_SEL");
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


        #region sp_PAYMENT_DEBIT_ALL_TMP_SEL
        public bool SP_PAYMENT_DEBIT_ALL_TMP_SEL(string strCriteria, string strCriteria2, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_PAYMENT_DEBIT_ALL_TMP_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);

                SqlParameter oParamI_vc_criteria2 = new SqlParameter("vc_criteria2", SqlDbType.NVarChar);
                oParamI_vc_criteria2.Direction = ParameterDirection.Input;
                oParamI_vc_criteria2.Value = strCriteria2;
                oCommand.Parameters.Add(oParamI_vc_criteria2);

                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_PAYMENT_DEBIT_ALL_TMP_SEL");
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


        

        #region SP_PAYMENT_REPORT_SEL
        public bool SP_PAYMENT_REPORT_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_PAYMENT_REPORT_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_PAYMENT_REPORT_SEL");
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

        #region SP_PAYMENT_SLIP_SEL
        public bool SP_PAYMENT_SLIP_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_PAYMENT_SLIP_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_PAYMENT_SLIP_SEL");
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

        #region sp_PAYMENT_SLIP_DEBIT
        public bool SP_PAYMENT_SLIP_DEBIT(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_PAYMENT_SLIP_DEBIT";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_PAYMENT_SLIP_DEBIT");
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

        #region sp_PAYMENT_SLIP_CREDIT
        public bool SP_PAYMENT_SLIP_CREDIT(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_PAYMENT_SLIP_CREDIT";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_PAYMENT_SLIP_CREDIT");
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

        #region SP_PAYMENT_SLIP_ALL_SEL
        public bool SP_PAYMENT_SLIP_ALL_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_PAYMENT_SLIP_ALL_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_PAYMENT_SLIP_ALL_SEL");
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

        #region SP_PAYMENT_RETIRE_SEL
        public bool SP_PAYMENT_RETIRE_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_PAYMENT_RETIRE_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_PAYMENT_RETIRE_SEL");
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

        #region SP_PAYMENT_RETIRE_HEAD_SEL
        public bool SP_PAYMENT_RETIRE_HEAD_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_PAYMENT_RETIRE_HEAD_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_PAYMENT_RETIRE_HEAD_SEL");
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

        #region SP_PAYMENT_RETIRE_SLIP_SEL
        public bool SP_PAYMENT_RETIRE_SLIP_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "SP_PAYMENT_RETIRE_SLIP_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "SP_PAYMENT_RETIRE_SLIP_SEL");
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

        #region SP_REP_PAYMENT_CERTIFICATE_SEL
        public bool SP_REP_PAYMENT_CERTIFICATE_SEL(string strCriteria, string strPay_month, string strPay_year, string strItem_type, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "SP_REP_PAYMENT_CERTIFICATE_SEL";
                oCommand.Parameters.Add("vc_criteria", SqlDbType.NVarChar).Value = strCriteria;
                oCommand.Parameters.Add("vc_pay_month", SqlDbType.NVarChar).Value = strPay_month;
                oCommand.Parameters.Add("vc_pay_year", SqlDbType.NVarChar).Value = strPay_year;
                oCommand.Parameters.Add("vc_item_type", SqlDbType.NVarChar).Value = strItem_type;
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "SP_REP_PAYMENT_CERTIFICATE_SEL");
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

        #region SP_REP_PAYMENT_MAIN_CERTIFICATE_SEL
        public bool SP_REP_PAYMENT_MAIN_CERTIFICATE_SEL(string strCriteria, string strPay_month, string strPay_year, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "SP_REP_PAYMENT_MAIN_CERTIFICATE_SEL";
                oCommand.Parameters.Add("vc_criteria", SqlDbType.NVarChar).Value = strCriteria;
                oCommand.Parameters.Add("vc_pay_month", SqlDbType.NVarChar).Value = strPay_month;
                oCommand.Parameters.Add("vc_pay_year", SqlDbType.NVarChar).Value = strPay_year;
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "SP_REP_PAYMENT_MAIN_CERTIFICATE_SEL");
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


        #region SP_PAYMENT_LOAN_SEL
        public bool SP_PAYMENT_LOAN_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_PAYMENT_LOAN_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_PAYMENT_LOAN_SEL");
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

        #region SP_PAYMENT_LOAN_DEL
        public bool SP_PAYMENT_LOAN_DEL(string pPayment_doc, string pLoan_code, string pLoan_acc, ref string strMessage)
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
                oCommand.CommandText = "SP_PAYMENT_LOAN_DEL";
                // - - - - - - - - - - - -             
                SqlParameter oParam_payment_doc = new SqlParameter("payment_doc", SqlDbType.NVarChar);
                oParam_payment_doc.Direction = ParameterDirection.Input;
                oParam_payment_doc.Value = pPayment_doc;
                oCommand.Parameters.Add(oParam_payment_doc);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_loan_code = new SqlParameter("loan_code", SqlDbType.NVarChar);
                oParam_person_loan_code.Direction = ParameterDirection.Input;
                oParam_person_loan_code.Value = pLoan_code;
                oCommand.Parameters.Add(oParam_person_loan_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_loan_acc = new SqlParameter("loan_acc", SqlDbType.NVarChar);
                oParam_person_loan_acc.Direction = ParameterDirection.Input;
                oParam_person_loan_acc.Value = pLoan_acc;
                oCommand.Parameters.Add(oParam_person_loan_acc);

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

        #region SP_PAYMENT_LOAN_INS
        public bool SP_PAYMENT_LOAN_INS(string pPayment_doc, string pLoan_code, string pLoan_acc, string pLoan_money, string pC_created_by, ref string strMessage)
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
                oCommand.CommandText = "sp_PAYMENT_LOAN_INS";
                // - - - - - - - - - - - -             
                SqlParameter oParam_payment_doc = new SqlParameter("payment_doc", SqlDbType.NVarChar);
                oParam_payment_doc.Direction = ParameterDirection.Input;
                oParam_payment_doc.Value = pPayment_doc;
                oCommand.Parameters.Add(oParam_payment_doc);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_loan_code = new SqlParameter("loan_code", SqlDbType.NVarChar);
                oParam_person_loan_code.Direction = ParameterDirection.Input;
                oParam_person_loan_code.Value = pLoan_code;
                oCommand.Parameters.Add(oParam_person_loan_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_loan_acc = new SqlParameter("loan_acc", SqlDbType.NVarChar);
                oParam_person_loan_acc.Direction = ParameterDirection.Input;
                oParam_person_loan_acc.Value = pLoan_acc;
                oCommand.Parameters.Add(oParam_person_loan_acc);

                // - - - - - - - - - - - -             
                SqlParameter oParam_loan_money = new SqlParameter("loan_money", SqlDbType.Money);
                oParam_loan_money.Direction = ParameterDirection.Input;
                oParam_loan_money.Value = float.Parse(pLoan_money);
                oCommand.Parameters.Add(oParam_loan_money);

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

        #region SP_PAYMENT_LOAN_UPD
        public bool SP_PAYMENT_LOAN_UPD(string pPayment_doc, string pLoan_code, string pLoan_acc, string pLoan_money, string pC_updated_by, ref string strMessage)
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
                oCommand.CommandText = "sp_PAYMENT_LOAN_UPD";

                // - - - - - - - - - - - -             
                SqlParameter oParam_payment_doc = new SqlParameter("payment_doc", SqlDbType.NVarChar);
                oParam_payment_doc.Direction = ParameterDirection.Input;
                oParam_payment_doc.Value = pPayment_doc;
                oCommand.Parameters.Add(oParam_payment_doc);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_loan_code = new SqlParameter("loan_code", SqlDbType.NVarChar);
                oParam_person_loan_code.Direction = ParameterDirection.Input;
                oParam_person_loan_code.Value = pLoan_code;
                oCommand.Parameters.Add(oParam_person_loan_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_loan_acc = new SqlParameter("loan_acc", SqlDbType.NVarChar);
                oParam_person_loan_acc.Direction = ParameterDirection.Input;
                oParam_person_loan_acc.Value = pLoan_acc;
                oCommand.Parameters.Add(oParam_person_loan_acc);

                // - - - - - - - - - - - -             
                SqlParameter oParam_loan_money = new SqlParameter("loan_money", SqlDbType.Money);
                oParam_loan_money.Direction = ParameterDirection.Input;
                oParam_loan_money.Value = float.Parse(pLoan_money);
                oCommand.Parameters.Add(oParam_loan_money);

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



        #region SP_EXPORT_DIRECT_PAY_ITEM_SEL
        public bool SP_EXPORT_DIRECT_PAY_ITEM_SEL(string strCriteria,string strCodeList, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_EXPORT_DIRECT_PAY_ITEM_SEL";
                
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);

                SqlParameter oParamI_vc_code_list = new SqlParameter("vc_code_list", SqlDbType.NVarChar);
                oParamI_vc_code_list.Direction = ParameterDirection.Input;
                oParamI_vc_code_list.Value = strCodeList;
                oCommand.Parameters.Add(oParamI_vc_code_list);

                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_EXPORT_DIRECT_PAY_ITEM_SEL");
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
