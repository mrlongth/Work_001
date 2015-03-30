using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace myDLL
{
    public class cItem : IDisposable
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

        public cItem()
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

        #region SP_ITEM_SEL
        public bool SP_ITEM_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_ITEM_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_ITEM_SEL");
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

        #region SP_DIRECT_PAY_ITEM_SEL
        public bool SP_DIRECT_PAY_ITEM_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_DIRECT_PAY_ITEM_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_DIRECT_PAY_ITEM_SEL");
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

        #region SP_ITEM_LOT_GROUP_SEL
        public bool SP_ITEM_LOT_GROUP_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_ITEM_LOT_GROUP_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_ITEM_LOT_GROUP_SEL");
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

        #region SP_ITEM_INS
        public bool SP_ITEM_INS(string pitem_year, string pitem_name, string pitem_type, string pitem_group_code,
                                string plot_code, string pperson_group_code, string pactive,
                                string pc_created_by, string pcheque_code, string pcheque_type,
                                string pitem_acc_code, string pitem_project_code1, string pitem_project_code2, string pbudget_type, string pdirect_pay_code, ref string strMessage)
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
                oCommand.CommandText = "sp_ITEM_INS";
                // - - - - - - - - - - - -             
                SqlParameter oParam_item_year = new SqlParameter("item_year", SqlDbType.NVarChar);
                oParam_item_year.Direction = ParameterDirection.Input;
                oParam_item_year.Value = pitem_year;
                oCommand.Parameters.Add(oParam_item_year);
                // - - - - - - - - - - - -             
                SqlParameter oParam_item_name = new SqlParameter("item_name", SqlDbType.NVarChar);
                oParam_item_name.Direction = ParameterDirection.Input;
                oParam_item_name.Value = pitem_name;
                oCommand.Parameters.Add(oParam_item_name);
                // - - - - - - - - - - - -             
                SqlParameter oParam_item_type = new SqlParameter("item_type", SqlDbType.NVarChar);
                oParam_item_type.Direction = ParameterDirection.Input;
                oParam_item_type.Value = pitem_type;
                oCommand.Parameters.Add(oParam_item_type);
                // - - - - - - - - - - - -             
                SqlParameter oParam_item_group_code = new SqlParameter("item_group_code", SqlDbType.NVarChar);
                oParam_item_group_code.Direction = ParameterDirection.Input;
                oParam_item_group_code.Value = pitem_group_code;
                oCommand.Parameters.Add(oParam_item_group_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_lot_code = new SqlParameter("lot_code", SqlDbType.NVarChar);
                oParam_lot_code.Direction = ParameterDirection.Input;
                oParam_lot_code.Value = plot_code;
                oCommand.Parameters.Add(oParam_lot_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_group_code = new SqlParameter("person_group_code", SqlDbType.NVarChar);
                oParam_person_group_code.Direction = ParameterDirection.Input;
                oParam_person_group_code.Value = pperson_group_code;
                oCommand.Parameters.Add(oParam_person_group_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_active = new SqlParameter("c_active", SqlDbType.NVarChar);
                oParam_active.Direction = ParameterDirection.Input;
                oParam_active.Value = pactive;
                oCommand.Parameters.Add(oParam_active);
                // - - - - - - - - - - - -             
                SqlParameter oParam_c_created_by = new SqlParameter("c_created_by", SqlDbType.NVarChar);
                oParam_c_created_by.Direction = ParameterDirection.Input;
                oParam_c_created_by.Value = pc_created_by;
                oCommand.Parameters.Add(oParam_c_created_by);
                // - - - - - - - - - - - -             
                SqlParameter oParam_cheque_code = new SqlParameter("cheque_code", SqlDbType.NVarChar);
                oParam_cheque_code.Direction = ParameterDirection.Input;
                oParam_cheque_code.Value = pcheque_code;
                oCommand.Parameters.Add(oParam_cheque_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_cheque_type = new SqlParameter("cheque_type", SqlDbType.NVarChar);
                oParam_cheque_type.Direction = ParameterDirection.Input;
                oParam_cheque_type.Value = pcheque_type;
                oCommand.Parameters.Add(oParam_cheque_type);
                // - - - - - - - - - - - -           
                if (pitem_acc_code == "") pitem_acc_code = "0";
                SqlParameter oParam_item_acc_code = new SqlParameter("item_acc_id", SqlDbType.Int);
                oParam_item_acc_code.Direction = ParameterDirection.Input;
                oParam_item_acc_code.Value = int.Parse(pitem_acc_code);
                oCommand.Parameters.Add(oParam_item_acc_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_item_project_code1 = new SqlParameter("item_project_code1", SqlDbType.NVarChar);
                oParam_item_project_code1.Direction = ParameterDirection.Input;
                oParam_item_project_code1.Value = pitem_project_code1;
                oCommand.Parameters.Add(oParam_item_project_code1);
                // - - - - - - - - - - - -             
                SqlParameter oParam_item_project_code2 = new SqlParameter("item_project_code2", SqlDbType.NVarChar);
                oParam_item_project_code2.Direction = ParameterDirection.Input;
                oParam_item_project_code2.Value = pitem_project_code2;
                oCommand.Parameters.Add(oParam_item_project_code2);
                // - - - - - - - - - - - -            

                // - - - - - - - - - - - -             
                SqlParameter oParam_budget_type = new SqlParameter("budget_type", SqlDbType.NVarChar);
                oParam_budget_type.Direction = ParameterDirection.Input;
                oParam_budget_type.Value = pbudget_type;
                oCommand.Parameters.Add(oParam_budget_type);

                // - - - - - - - - - - - -             
                SqlParameter oParam_direct_pay_code = new SqlParameter("direct_pay_code", SqlDbType.NVarChar);
                oParam_direct_pay_code.Direction = ParameterDirection.Input;
                oParam_direct_pay_code.Value = pdirect_pay_code;
                oCommand.Parameters.Add(oParam_direct_pay_code);


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

        #region SP_ITEM_UPD
        public bool SP_ITEM_UPD(string pitem_code, string pitem_year, string pitem_name, string pitem_type, string pitem_group_code,
                                string plot_code, string pperson_group_code, string pactive,
                                string pc_updated_by, string pcheque_code, string pcheque_type,
                                string pitem_acc_code, string pitem_project_code1, string pitem_project_code2, string pbudget_type, string pdirect_pay_code, ref string strMessage)
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
                oCommand.CommandText = "sp_ITEM_UPD";
                // - - - - - - - - - - - -             
                SqlParameter oParam_item_code = new SqlParameter("item_code", SqlDbType.NVarChar);
                oParam_item_code.Direction = ParameterDirection.Input;
                oParam_item_code.Value = pitem_code;
                oCommand.Parameters.Add(oParam_item_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_item_year = new SqlParameter("item_year", SqlDbType.NVarChar);
                oParam_item_year.Direction = ParameterDirection.Input;
                oParam_item_year.Value = pitem_year;
                oCommand.Parameters.Add(oParam_item_year);
                // - - - - - - - - - - - -             
                SqlParameter oParam_item_name = new SqlParameter("item_name", SqlDbType.NVarChar);
                oParam_item_name.Direction = ParameterDirection.Input;
                oParam_item_name.Value = pitem_name;
                oCommand.Parameters.Add(oParam_item_name);
                // - - - - - - - - - - - -             
                SqlParameter oParam_item_type = new SqlParameter("item_type", SqlDbType.NVarChar);
                oParam_item_type.Direction = ParameterDirection.Input;
                oParam_item_type.Value = pitem_type;
                oCommand.Parameters.Add(oParam_item_type);
                // - - - - - - - - - - - -             
                SqlParameter oParam_item_group_code = new SqlParameter("item_group_code", SqlDbType.NVarChar);
                oParam_item_group_code.Direction = ParameterDirection.Input;
                oParam_item_group_code.Value = pitem_group_code;
                oCommand.Parameters.Add(oParam_item_group_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_lot_code = new SqlParameter("lot_code", SqlDbType.NVarChar);
                oParam_lot_code.Direction = ParameterDirection.Input;
                oParam_lot_code.Value = plot_code;
                oCommand.Parameters.Add(oParam_lot_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_group_code = new SqlParameter("person_group_code", SqlDbType.NVarChar);
                oParam_person_group_code.Direction = ParameterDirection.Input;
                oParam_person_group_code.Value = pperson_group_code;
                oCommand.Parameters.Add(oParam_person_group_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_active = new SqlParameter("c_active", SqlDbType.NVarChar);
                oParam_active.Direction = ParameterDirection.Input;
                oParam_active.Value = pactive;
                oCommand.Parameters.Add(oParam_active);
                // - - - - - - - - - - - -             
                SqlParameter oParam_c_updated_by = new SqlParameter("c_updated_by", SqlDbType.NVarChar);
                oParam_c_updated_by.Direction = ParameterDirection.Input;
                oParam_c_updated_by.Value = pc_updated_by;
                oCommand.Parameters.Add(oParam_c_updated_by);
                // - - - - - - - - - - - -             
                SqlParameter oParam_cheque_code = new SqlParameter("cheque_code", SqlDbType.NVarChar);
                oParam_cheque_code.Direction = ParameterDirection.Input;
                oParam_cheque_code.Value = pcheque_code;
                oCommand.Parameters.Add(oParam_cheque_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_cheque_type = new SqlParameter("cheque_type", SqlDbType.NVarChar);
                oParam_cheque_type.Direction = ParameterDirection.Input;
                oParam_cheque_type.Value = pcheque_type;
                oCommand.Parameters.Add(oParam_cheque_type);
                // - - - - - - - - - - - -     
                if (pitem_acc_code == "") pitem_acc_code = "0";
                SqlParameter oParam_item_acc_code = new SqlParameter("item_acc_id", SqlDbType.Int);
                oParam_item_acc_code.Direction = ParameterDirection.Input;
                oParam_item_acc_code.Value = int.Parse(pitem_acc_code);
                oCommand.Parameters.Add(oParam_item_acc_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_item_project_code1 = new SqlParameter("item_project_code1", SqlDbType.NVarChar);
                oParam_item_project_code1.Direction = ParameterDirection.Input;
                oParam_item_project_code1.Value = pitem_project_code1;
                oCommand.Parameters.Add(oParam_item_project_code1);
                // - - - - - - - - - - - -             
                SqlParameter oParam_item_project_code2 = new SqlParameter("item_project_code2", SqlDbType.NVarChar);
                oParam_item_project_code2.Direction = ParameterDirection.Input;
                oParam_item_project_code2.Value = pitem_project_code2;
                oCommand.Parameters.Add(oParam_item_project_code2);

                // - - - - - - - - - - - -             
                SqlParameter oParam_budget_type = new SqlParameter("budget_type", SqlDbType.NVarChar);
                oParam_budget_type.Direction = ParameterDirection.Input;
                oParam_budget_type.Value = pbudget_type;
                oCommand.Parameters.Add(oParam_budget_type);

                // - - - - - - - - - - - -             
                SqlParameter oParam_direct_pay_code = new SqlParameter("direct_pay_code", SqlDbType.NVarChar);
                oParam_direct_pay_code.Direction = ParameterDirection.Input;
                oParam_direct_pay_code.Value = pdirect_pay_code;
                oCommand.Parameters.Add(oParam_direct_pay_code);


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

        #region SP_ITEM_DEL
        public bool SP_ITEM_DEL(string pitem_code, string pactive, string pc_updated_by, ref string strMessage)
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
                oCommand.CommandText = "sp_ITEM_DEL";
                // - - - - - - - - - - - -             
                SqlParameter oParam_item_code = new SqlParameter("item_code", SqlDbType.NVarChar);
                oParam_item_code.Direction = ParameterDirection.Input;
                oParam_item_code.Value = pitem_code;
                oCommand.Parameters.Add(oParam_item_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_active = new SqlParameter("c_active", SqlDbType.NVarChar);
                oParam_active.Direction = ParameterDirection.Input;
                oParam_active.Value = pactive;
                oCommand.Parameters.Add(oParam_active);
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

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
