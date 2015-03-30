using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace myDLL
{
    public class cBudget_money : IDisposable
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

        public cBudget_money()
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

        #region SP_BUDGET_MONEY_HEAD_SEL
        public bool SP_BUDGET_MONEY_HEAD_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_BUDGET_MONEY_HEAD_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_BUDGET_MONEY_HEAD_SEL");
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

        #region SP_BUDGET_MONEY_SEL
        public bool SP_BUDGET_MONEY_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_BUDGET_MONEY_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_BUDGET_MONEY_SEL");
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

        #region SP_BUDGET_MONEY_TEMP_SEL
        public bool SP_BUDGET_MONEY_TEMP_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_BUDGET_MONEY_TEMP_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_BUDGET_MONEY_TEMP_SEL");
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

        #region SP_BUDGET_MONEY_INS
        public bool sp_BUDGET_MONEY_INS(
                    ref string budget_money_doc,
                    string budget_money_date,
                    string budget_money_year,
                    string budget_plan_code,
                    string budget_money_all,
                    string budget_money_adjust,
                    string budget_money_use,
                    string budget_money_remain,
                    string comments,
                    string c_active,
                    string c_created_by,
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
                oCommand.CommandText = "sp_BUDGET_MONEY_INS";
                SqlParameter oParamI_budget_money_doc = new SqlParameter("budget_money_doc", SqlDbType.VarChar);
                oParamI_budget_money_doc.Direction = ParameterDirection.Output;
                oParamI_budget_money_doc.Value = budget_money_doc;
                oCommand.Parameters.Add(oParamI_budget_money_doc);
                oCommand.Parameters.Add("budget_money_date", SqlDbType.DateTime).Value = cCommon.CheckDate(budget_money_date);
                oCommand.Parameters.Add("budget_money_year", SqlDbType.VarChar).Value = budget_money_year;
                oCommand.Parameters.Add("budget_plan_code", SqlDbType.VarChar).Value = budget_plan_code;
                oCommand.Parameters.Add("budget_money_all", SqlDbType.Decimal).Value = decimal.Parse(budget_money_all);
                oCommand.Parameters.Add("budget_money_adjust", SqlDbType.Decimal).Value = decimal.Parse(budget_money_adjust);
                oCommand.Parameters.Add("budget_money_use", SqlDbType.Decimal).Value = decimal.Parse(budget_money_use);
                oCommand.Parameters.Add("budget_money_remain", SqlDbType.Decimal).Value = decimal.Parse(budget_money_remain);
                oCommand.Parameters.Add("comments", SqlDbType.VarChar).Value = comments;
                oCommand.Parameters.Add("c_active", SqlDbType.VarChar).Value = c_active;
                oCommand.Parameters.Add("c_created_by", SqlDbType.VarChar).Value = c_created_by;
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

        #region SP_BUDGET_MONEY_UPD
        public bool SP_BUDGET_MONEY_UPD(string budget_money_doc,
                    string budget_money_date,
                    string budget_money_year,
                    string budget_plan_code,
                    string budget_money_all,
                    string budget_money_adjust,
                    string budget_money_use,
                    string budget_money_remain,
                    string comments,
                    string c_active,
                    string c_updated_by,
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
                oCommand.CommandText = "sp_BUDGET_MONEY_UPD";
                oCommand.Parameters.Add("budget_money_doc", SqlDbType.VarChar).Value = budget_money_doc;
                oCommand.Parameters.Add("budget_money_date", SqlDbType.DateTime).Value = cCommon.CheckDate(budget_money_date);
                oCommand.Parameters.Add("budget_money_year", SqlDbType.VarChar).Value = budget_money_year;
                oCommand.Parameters.Add("budget_plan_code", SqlDbType.VarChar).Value = budget_plan_code;
                oCommand.Parameters.Add("budget_money_all", SqlDbType.Decimal).Value = decimal.Parse(budget_money_all);
                oCommand.Parameters.Add("budget_money_adjust", SqlDbType.Decimal).Value = decimal.Parse(budget_money_adjust);
                oCommand.Parameters.Add("budget_money_use", SqlDbType.Decimal).Value = decimal.Parse(budget_money_use);
                oCommand.Parameters.Add("budget_money_remain", SqlDbType.Decimal).Value = decimal.Parse(budget_money_remain);
                oCommand.Parameters.Add("comments", SqlDbType.VarChar).Value = comments;
                oCommand.Parameters.Add("c_active", SqlDbType.VarChar).Value = c_active;
                oCommand.Parameters.Add("c_updated_by", SqlDbType.VarChar).Value = c_updated_by;
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

        #region SP_BUDGET_MONEY_DEL
        public bool SP_BUDGET_MONEY_DEL(string budget_money_doc, string active, string updated_by, ref string strMessage)
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
                oCommand.CommandText = "sp_BUDGET_MONEY_DEL";
                oCommand.Parameters.Add("@budget_money_doc", SqlDbType.VarChar).Value = budget_money_doc;
                oCommand.Parameters.Add("@c_active", SqlDbType.VarChar).Value = active;
                oCommand.Parameters.Add("@c_updated_by", SqlDbType.VarChar).Value = updated_by;
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

        #region SP_BUDGET_MONEY_DETAIL_INS
        public bool SP_BUDGET_MONEY_DETAIL_INS(string budget_money_doc,
                    string item_group_code,
                    string lot_code,
                    string budget_item_group_code ,
                    string budget_money_suball,
                    string budget_money_subadjust,
                    string budget_money_subuse,
                    string budget_money_subremain,
                    string c_active,
                    string c_created_by,
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
                oCommand.CommandText = "sp_BUDGET_MONEY_DETAIL_INS";
                oCommand.Parameters.Add("budget_money_doc", SqlDbType.VarChar).Value = budget_money_doc;
                oCommand.Parameters.Add("item_group_code", SqlDbType.VarChar).Value = item_group_code;
                oCommand.Parameters.Add("lot_code", SqlDbType.VarChar).Value = lot_code;
                oCommand.Parameters.Add("budget_item_group_code", SqlDbType.VarChar).Value = budget_item_group_code;                
                oCommand.Parameters.Add("budget_money_suball", SqlDbType.Decimal).Value = decimal.Parse(budget_money_suball);
                oCommand.Parameters.Add("budget_money_subadjust", SqlDbType.Decimal).Value = decimal.Parse(budget_money_subadjust);
                oCommand.Parameters.Add("budget_money_subuse", SqlDbType.Decimal).Value = decimal.Parse(budget_money_subuse);
                oCommand.Parameters.Add("budget_money_subremain", SqlDbType.Decimal).Value = decimal.Parse(budget_money_subremain);
                oCommand.Parameters.Add("c_active", SqlDbType.VarChar).Value = c_active;
                oCommand.Parameters.Add("c_created_by", SqlDbType.VarChar).Value = c_created_by;
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

        #region SP_BUDGET_MONEY_DETAIL_UPD
        public bool SP_BUDGET_MONEY_DETAIL_UPD(string budget_money_doc,
                    string item_group_code,
                    string lot_code,
                    string budget_item_group_code,
                    string budget_money_suball,
                    string budget_money_subadjust,
                    string budget_money_subuse,
                    string budget_money_subremain,
                    string c_active,
                    string c_updated_by,
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
                oCommand.CommandText = "sp_BUDGET_MONEY_DETAIL_UPD";
                oCommand.Parameters.Add("budget_money_doc", SqlDbType.VarChar).Value = budget_money_doc;
                oCommand.Parameters.Add("item_group_code", SqlDbType.VarChar).Value = item_group_code;
                oCommand.Parameters.Add("lot_code", SqlDbType.VarChar).Value = lot_code;
                oCommand.Parameters.Add("budget_item_group_code", SqlDbType.VarChar).Value = budget_item_group_code;                
                oCommand.Parameters.Add("budget_money_suball", SqlDbType.Decimal).Value = decimal.Parse(budget_money_suball);
                oCommand.Parameters.Add("budget_money_subadjust", SqlDbType.Decimal).Value = decimal.Parse(budget_money_subadjust);
                oCommand.Parameters.Add("budget_money_subuse", SqlDbType.Decimal).Value = decimal.Parse(budget_money_subuse);
                oCommand.Parameters.Add("budget_money_subremain", SqlDbType.Decimal).Value = decimal.Parse(budget_money_subremain);
                oCommand.Parameters.Add("c_active", SqlDbType.VarChar).Value = c_active;
                oCommand.Parameters.Add("c_updated_by", SqlDbType.VarChar).Value = c_updated_by;
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

        #region SP_BUDGET_MONEY_DETAIL_DEL
        public bool SP_BUDGET_MONEY_DETAIL_DEL(
                    string budget_money_doc,
                    string c_active,
                    string c_updated_by,
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
                oCommand.CommandText = "sp_BUDGET_MONEY_DETAIL_DEL";
                oCommand.Parameters.Add("budget_money_doc", SqlDbType.VarChar).Value = budget_money_doc;
                oCommand.Parameters.Add("c_active", SqlDbType.VarChar).Value = c_active;
                oCommand.Parameters.Add("c_updated_by", SqlDbType.VarChar).Value = c_updated_by;
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

        #region SP_BUDGET_TRANFER_INS
        public bool SP_BUDGET_TRANFER_INS(
                    string pbudget_tranfer_date,
                    string pbudget_year,
                    string pbudget_tranfer_year,
                    string pbudget_tranfer_month,
                    string pbudget_plan_code_sr,
                    string plot_code_sr,
                    string pitem_group_code_sr,
                    string pbudget_tranfer_money_sr,
                    string pbudget_plan_code_ds,
                    string plot_code_ds,
                    string pitem_group_code_ds,
                    string pbudget_tranfer_money_ds,
                    string pbudget_tranfer_money,
                    string pc_active,
                    string pc_created_by,
                    string pbudget_item_group_code_sr,
                    string pbudget_item_group_code_ds,
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
                oCommand.CommandText = "sp_BUDGET_TRANFER_INS";
                oCommand.Parameters.Add("pbudget_tranfer_date", SqlDbType.DateTime).Value = cCommon.CheckDate(pbudget_tranfer_date);
                oCommand.Parameters.Add("pbudget_year", SqlDbType.VarChar).Value = pbudget_year;
                oCommand.Parameters.Add("pbudget_tranfer_year", SqlDbType.VarChar).Value = pbudget_tranfer_year;
                oCommand.Parameters.Add("pbudget_tranfer_month", SqlDbType.VarChar).Value = pbudget_tranfer_month;
                oCommand.Parameters.Add("pbudget_plan_code_sr", SqlDbType.VarChar).Value = pbudget_plan_code_sr;
                oCommand.Parameters.Add("plot_code_sr", SqlDbType.VarChar).Value = plot_code_sr;
                oCommand.Parameters.Add("pitem_group_code_sr", SqlDbType.VarChar).Value = pitem_group_code_sr;
                oCommand.Parameters.Add("pbudget_tranfer_money_sr", SqlDbType.Decimal).Value = pbudget_tranfer_money_sr;
                oCommand.Parameters.Add("pbudget_plan_code_ds", SqlDbType.VarChar).Value = pbudget_plan_code_ds;
                oCommand.Parameters.Add("plot_code_ds", SqlDbType.VarChar).Value = plot_code_ds;
                oCommand.Parameters.Add("pitem_group_code_ds", SqlDbType.VarChar).Value = pitem_group_code_ds;
                oCommand.Parameters.Add("pbudget_tranfer_money_ds", SqlDbType.Decimal).Value = pbudget_tranfer_money_ds;
                oCommand.Parameters.Add("pbudget_tranfer_money", SqlDbType.Decimal).Value = pbudget_tranfer_money;
                oCommand.Parameters.Add("pc_active", SqlDbType.NVarChar).Value = pc_active;
                oCommand.Parameters.Add("pc_created_by", SqlDbType.NVarChar).Value = pc_created_by;

                oCommand.Parameters.Add("pbudget_item_group_code_sr", SqlDbType.VarChar).Value = pbudget_item_group_code_sr;
                oCommand.Parameters.Add("pbudget_item_group_code_ds", SqlDbType.VarChar).Value = pbudget_item_group_code_ds;

                
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

        #region SP_BUDGET_TRANFER_UPD
        public bool SP_BUDGET_TRANFER_UPD(
                    string pbudget_tranfer_doc,
                    string pbudget_tranfer_date,
                    string pbudget_year,
                    string pbudget_tranfer_year,
                    string pbudget_tranfer_month,
                    string pbudget_plan_code_sr,
                    string plot_code_sr,
                    string pitem_group_code_sr,
                    string pbudget_tranfer_money_sr,
                    string pbudget_plan_code_ds,
                    string plot_code_ds,
                    string pitem_group_code_ds,
                    string pbudget_tranfer_money_ds,
                    string pbudget_tranfer_money,
                    string pc_active,
                    string pc_updated_by,
                    string pbudget_item_group_code_sr ,
                    string pbudget_item_group_code_ds ,
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
                oCommand.CommandText = "sp_BUDGET_TRANFER_UPD";
                oCommand.Parameters.Add("pbudget_tranfer_doc", SqlDbType.VarChar).Value = pbudget_tranfer_doc;
                oCommand.Parameters.Add("pbudget_tranfer_date", SqlDbType.DateTime).Value = cCommon.CheckDate(pbudget_tranfer_date);
                oCommand.Parameters.Add("pbudget_year", SqlDbType.VarChar).Value = pbudget_year;
                oCommand.Parameters.Add("pbudget_tranfer_year", SqlDbType.VarChar).Value = pbudget_tranfer_year;
                oCommand.Parameters.Add("pbudget_tranfer_month", SqlDbType.VarChar).Value = pbudget_tranfer_month;
                oCommand.Parameters.Add("pbudget_plan_code_sr", SqlDbType.VarChar).Value = pbudget_plan_code_sr;
                oCommand.Parameters.Add("plot_code_sr", SqlDbType.VarChar).Value = plot_code_sr;
                oCommand.Parameters.Add("pitem_group_code_sr", SqlDbType.VarChar).Value = pitem_group_code_sr;
                oCommand.Parameters.Add("pbudget_tranfer_money_sr", SqlDbType.Decimal).Value = pbudget_tranfer_money_sr;
                oCommand.Parameters.Add("pbudget_plan_code_ds", SqlDbType.VarChar).Value = pbudget_plan_code_ds;
                oCommand.Parameters.Add("plot_code_ds", SqlDbType.VarChar).Value = plot_code_ds;
                oCommand.Parameters.Add("pitem_group_code_ds", SqlDbType.VarChar).Value = pitem_group_code_ds;
                oCommand.Parameters.Add("pbudget_tranfer_money_ds", SqlDbType.Decimal).Value = pbudget_tranfer_money_ds;
                oCommand.Parameters.Add("pbudget_tranfer_money", SqlDbType.Decimal).Value = pbudget_tranfer_money;
                oCommand.Parameters.Add("pc_active", SqlDbType.NVarChar).Value = pc_active;
                oCommand.Parameters.Add("pc_updated_by", SqlDbType.NVarChar).Value = pc_updated_by ;

                oCommand.Parameters.Add("pbudget_item_group_code_sr", SqlDbType.VarChar).Value = pbudget_item_group_code_sr;
                oCommand.Parameters.Add("pbudget_item_group_code_ds", SqlDbType.VarChar).Value = pbudget_item_group_code_ds;
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

        #region SP_BUDGET_TRANFER_DEL
        public bool SP_BUDGET_TRANFER_DEL(
                    string pbudget_tranfer_doc,
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
                oCommand.CommandText = "sp_BUDGET_TRANFER_DEL";
                oCommand.Parameters.Add("pbudget_tranfer_doc", SqlDbType.VarChar).Value = pbudget_tranfer_doc;
                oCommand.Parameters.Add("pc_updated_by", SqlDbType.NVarChar).Value = pc_updated_by;
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
                
        #region SP_BUDGET_TRANFER_SEL
        public bool SP_BUDGET_TRANFER_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_BUDGET_TRANFER_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_BUDGET_TRANFER_SEL");
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
