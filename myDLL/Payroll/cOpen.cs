using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace myDLL
{
    public class cOpen : IDisposable
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

        public cOpen()
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

        #region OPEN

        #region SP_OPEN_SEL
        public bool SP_OPEN_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_OPEN_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_OPEN_SEL");
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

        #region SP_OPEN_INS
        public bool SP_OPEN_INS(
              ref string popen_code,
              string popen_to,
              string popen_title,
              string popen_command_desc,
              string popen_desc,
              string popen_level,
              string popen_report_code,
              string popen_remark,
              string pitem_group_code,
              string plot_code,
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
                oCommand.CommandText = "sp_OPEN_INS";

                oCommand.Parameters.Add("@popen_code", SqlDbType.VarChar, 10);
                oCommand.Parameters["@popen_code"].Direction = ParameterDirection.Output;
                oCommand.Parameters.Add("@popen_to", SqlDbType.VarChar).Value = popen_to;
                oCommand.Parameters.Add("@popen_title", SqlDbType.VarChar).Value = popen_title;
                oCommand.Parameters.Add("@popen_command_desc", SqlDbType.VarChar).Value = popen_command_desc;
                oCommand.Parameters.Add("@popen_desc", SqlDbType.VarChar).Value = popen_desc;
                oCommand.Parameters.Add("@popen_level", SqlDbType.VarChar).Value = popen_level;
                oCommand.Parameters.Add("@popen_report_code", SqlDbType.VarChar).Value = popen_report_code;
                oCommand.Parameters.Add("@popen_remark", SqlDbType.VarChar).Value = popen_remark;
                oCommand.Parameters.Add("@pitem_group_code", SqlDbType.VarChar).Value = pitem_group_code;
                oCommand.Parameters.Add("@plot_code", SqlDbType.VarChar).Value = plot_code;
                oCommand.Parameters.Add("@pc_created_by", SqlDbType.VarChar).Value = pc_created_by;
                // - - - - - - - - - - - -             
                oCommand.ExecuteNonQuery();
                popen_code = (string)oCommand.Parameters["@popen_code"].Value;
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

        #region SP_OPEN_UPD
        public bool SP_OPEN_UPD(
              string popen_id,
              string popen_code,
              string popen_to,
              string popen_title,
              string popen_command_desc,
              string popen_desc,
              string popen_level,
              string popen_report_code,
              string popen_remark,
              string pitem_group_code,
              string plot_code,
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
                oCommand.CommandText = "sp_OPEN_UPD";
                oCommand.Parameters.Add("@popen_id", SqlDbType.Int).Value = int.Parse(popen_id);
                oCommand.Parameters.Add("@popen_code", SqlDbType.VarChar).Value = popen_code;
                oCommand.Parameters.Add("@popen_to", SqlDbType.VarChar).Value = popen_to;
                oCommand.Parameters.Add("@popen_title", SqlDbType.VarChar).Value = popen_title;
                oCommand.Parameters.Add("@popen_command_desc", SqlDbType.VarChar).Value = popen_command_desc;
                oCommand.Parameters.Add("@popen_desc", SqlDbType.VarChar).Value = popen_desc;
                oCommand.Parameters.Add("@popen_level", SqlDbType.VarChar).Value = popen_level;
                oCommand.Parameters.Add("@popen_report_code", SqlDbType.VarChar).Value = popen_report_code;
                oCommand.Parameters.Add("@popen_remark", SqlDbType.VarChar).Value = popen_remark;
                oCommand.Parameters.Add("@pitem_group_code", SqlDbType.VarChar).Value = pitem_group_code;
                oCommand.Parameters.Add("@plot_code", SqlDbType.VarChar).Value = plot_code;
                oCommand.Parameters.Add("@c_updated_by", SqlDbType.VarChar).Value = pc_created_by;
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

        #region SP_OPEN_DEL
        public bool SP_OPEN_DEL(
              string popen_id,
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
                oCommand.CommandText = "sp_OPEN_DEL";
                oCommand.Parameters.Add("@popen_id", SqlDbType.Int).Value = int.Parse(popen_id);
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

        #endregion

        #region OPEN ITEM

        #region SP_OPEN_ITEM_SEL
        public bool SP_OPEN_ITEM_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_OPEN_ITEM_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_OPEN_ITEM_SEL");
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

        #region SP_OPEN_ITEM_INS
        public bool SP_OPEN_ITEM_INS(
            string popen_item_id,
            string popen_id,
            string pmaterial_id,
            string popen_rate,
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
                oCommand.CommandText = "sp_OPEN_ITEM_INS";
                oCommand.Parameters.Add("@popen_item_id", SqlDbType.Int).Value = int.Parse(popen_item_id);
                oCommand.Parameters.Add("@popen_id", SqlDbType.Int).Value = int.Parse(popen_id);
                oCommand.Parameters.Add("@pmaterial_id", SqlDbType.Int).Value = int.Parse(pmaterial_id);
                oCommand.Parameters.Add("@popen_rate", SqlDbType.Money).Value = float.Parse(popen_rate);
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

        #region SP_OPEN_ITEM_UPD
        public bool SP_OPEN_ITEM_UPD(
            string popen_item_id,
            string popen_id,
            string pmaterial_id,
            string popen_rate,
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
                oCommand.CommandText = "sp_OPEN_ITEM_UPD";
                oCommand.Parameters.Add("@popen_item_id", SqlDbType.Int).Value = int.Parse(popen_item_id);
                oCommand.Parameters.Add("@popen_id", SqlDbType.Int).Value = int.Parse(popen_id);
                oCommand.Parameters.Add("@pmaterial_id", SqlDbType.Int).Value = int.Parse(pmaterial_id);
                oCommand.Parameters.Add("@popen_rate", SqlDbType.Money).Value = float.Parse(popen_rate);
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

        #region SP_OPEN_ITEM_DEL
        public bool SP_OPEN_ITEM_DEL(
            string popen_id,
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
                oCommand.CommandText = "sp_OPEN_ITEM_DEL";
                oCommand.Parameters.Add("@popen_id", SqlDbType.Int).Value = int.Parse(popen_id);
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

        #endregion

        #region OPEN HEAD

        #region SP_OPEN_HEAD_SEL
        public bool SP_OPEN_HEAD_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_OPEN_HEAD_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_OPEN_HEAD_SEL");
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

        #region SP_OPEN_HEAD_INS
        public bool SP_OPEN_HEAD_INS(
              ref string popen_doc,
              string popen_year,
              string popen_id,
              string popen_to,
              string popen_title,
              string popen_command_desc,
              string popen_desc,
              string popen_tel,
              string punit_code,
              string pdirector_code,
              string pbudget_plan_code,
              string pperson_open,
              string popen_level,
              string popen_date,
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
                oCommand.CommandText = "sp_OPEN_HEAD_INS";

                oCommand.Parameters.Add("@popen_doc", SqlDbType.VarChar, 10);
                oCommand.Parameters["@popen_doc"].Direction = ParameterDirection.Output;
                oCommand.Parameters.Add("@popen_year", SqlDbType.VarChar).Value = popen_year;
                oCommand.Parameters.Add("@popen_id", SqlDbType.Int).Value = int.Parse(popen_id);
                oCommand.Parameters.Add("@popen_to", SqlDbType.VarChar).Value = popen_to;
                oCommand.Parameters.Add("@popen_title", SqlDbType.VarChar).Value = popen_title;
                oCommand.Parameters.Add("@popen_desc", SqlDbType.VarChar).Value = popen_desc;
                oCommand.Parameters.Add("@punit_code", SqlDbType.VarChar).Value = punit_code;
                oCommand.Parameters.Add("@pdirector_code", SqlDbType.VarChar).Value = pdirector_code;
                oCommand.Parameters.Add("@pbudget_plan_code", SqlDbType.VarChar).Value = pbudget_plan_code;
                oCommand.Parameters.Add("@pperson_open", SqlDbType.VarChar).Value = pperson_open;
                oCommand.Parameters.Add("@popen_level", SqlDbType.VarChar).Value = popen_level;
                oCommand.Parameters.Add("@popen_command_desc", SqlDbType.VarChar).Value = popen_command_desc;
                oCommand.Parameters.Add("@popen_tel", SqlDbType.VarChar).Value = popen_tel;
                oCommand.Parameters.Add("@popen_date", SqlDbType.DateTime).Value = cCommon.CheckDate(popen_date);
                oCommand.Parameters.Add("@pc_created_by", SqlDbType.VarChar).Value = pc_created_by;
                // - - - - - - - - - - - -             
                oCommand.ExecuteNonQuery();
                popen_doc = (string)oCommand.Parameters["@popen_doc"].Value;
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

        #region SP_OPEN_HEAD_UPD
        public bool SP_OPEN_HEAD_UPD(
              string popen_head_id,
              string popen_doc,
              string popen_year,
              string popen_id,
              string popen_to,
              string popen_title,
              string popen_command_desc,
              string popen_tel,
              string popen_desc,
              string punit_code,
              string pdirector_code,
              string pbudget_plan_code,
              string pperson_open,
              string popen_level,
              string popen_date,
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
                oCommand.CommandText = "sp_OPEN_HEAD_UPD";
                oCommand.Parameters.Add("@popen_head_id", SqlDbType.Int).Value = int.Parse(popen_head_id);
                oCommand.Parameters.Add("@popen_doc", SqlDbType.VarChar).Value = popen_doc;
                oCommand.Parameters.Add("@popen_year", SqlDbType.VarChar).Value = popen_year;
                oCommand.Parameters.Add("@popen_id", SqlDbType.Int).Value = int.Parse(popen_id);
                oCommand.Parameters.Add("@popen_to", SqlDbType.VarChar).Value = popen_to;
                oCommand.Parameters.Add("@popen_title", SqlDbType.VarChar).Value = popen_title;
                oCommand.Parameters.Add("@popen_desc", SqlDbType.VarChar).Value = popen_desc;
                oCommand.Parameters.Add("@punit_code", SqlDbType.VarChar).Value = punit_code;
                oCommand.Parameters.Add("@pdirector_code", SqlDbType.VarChar).Value = pdirector_code;
                oCommand.Parameters.Add("@pbudget_plan_code", SqlDbType.VarChar).Value = pbudget_plan_code;
                oCommand.Parameters.Add("@pperson_open", SqlDbType.VarChar).Value = pperson_open;
                oCommand.Parameters.Add("@popen_level", SqlDbType.VarChar).Value = popen_level;
                oCommand.Parameters.Add("@popen_command_desc", SqlDbType.VarChar).Value = popen_command_desc;
                oCommand.Parameters.Add("@popen_tel", SqlDbType.VarChar).Value = popen_tel;
                oCommand.Parameters.Add("@popen_date", SqlDbType.DateTime).Value = cCommon.CheckDate(popen_date);
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

        #region SP_OPEN_HEAD_DEL
        public bool SP_OPEN_HEAD_DEL(
              string popen_head_id,
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
                oCommand.CommandText = "sp_OPEN_HEAD_DEL";
                oCommand.Parameters.Add("@popen_head_id", SqlDbType.Int).Value = int.Parse(popen_head_id);
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

        #endregion

        #region OPEN COMMAND

        #region SP_OPEN_COMMAND_SEL
        public bool SP_OPEN_COMMAND_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_OPEN_COMMAND_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_OPEN_COMMAND_SEL");
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

        #region SP_OPEN_COMMAND_INS
        public bool SP_OPEN_COMMAND_INS(
              string popen_head_id,
              string popen_no,
              string popen_date,
              string popen_desc,
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
                oCommand.CommandText = "sp_OPEN_COMMAND_INS";
                oCommand.Parameters.Add("@popen_head_id", SqlDbType.VarChar).Value = popen_head_id;
                oCommand.Parameters.Add("@popen_no", SqlDbType.VarChar).Value = popen_no;
                oCommand.Parameters.Add("@popen_date", SqlDbType.DateTime).Value = cCommon.CheckDate(popen_date);
                oCommand.Parameters.Add("@popen_desc", SqlDbType.VarChar).Value = popen_desc;
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

        #region SP_OPEN_COMMAND_UPD
        public bool SP_OPEN_COMMAND_UPD(
              string popen_head_id,
              string popen_command_id,
              string popen_no,
              string popen_date,
              string popen_desc,
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
                oCommand.CommandText = "sp_OPEN_COMMAND_UPD";
                oCommand.Parameters.Add("@popen_command_id", SqlDbType.Int).Value = int.Parse(popen_command_id);
                oCommand.Parameters.Add("@popen_head_id", SqlDbType.VarChar).Value = int.Parse(popen_head_id);
                oCommand.Parameters.Add("@popen_no", SqlDbType.VarChar).Value = popen_no;
                oCommand.Parameters.Add("@popen_date", SqlDbType.DateTime).Value = cCommon.CheckDate(popen_date);
                oCommand.Parameters.Add("@popen_desc", SqlDbType.VarChar).Value = popen_desc;
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

        #region SP_OPEN_COMMAND_DEL
        public bool SP_OPEN_COMMAND_DEL(
              string popen_head_id,
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
                oCommand.CommandText = "SP_OPEN_COMMAND_DEL";
                oCommand.Parameters.Add("@popen_head_id", SqlDbType.Int).Value = int.Parse(popen_head_id);
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

        #endregion

        #region OPEN DETAIL

        #region SP_OPEN_DETAIL_SEL
        public bool SP_OPEN_DETAIL_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_OPEN_DETAIL_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_OPEN_DETAIL_SEL");
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

        #region SP_OPEN_DETAIL_INS
        public bool SP_OPEN_DETAIL_INS(
            string popen_head_id,
            string pmaterial_id,
            string popen_begin_date,
            string popen_end_date,
            string popen_qty_month,
            string popen_qty_day,
            string popen_qty_person,
            string popen_rate,
            string popen_rate_all,
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
                oCommand.CommandText = "sp_OPEN_DETAIL_INS";
                oCommand.Parameters.Add("@popen_head_id", SqlDbType.Int).Value = int.Parse(popen_head_id);
                oCommand.Parameters.Add("@pmaterial_id", SqlDbType.Int).Value = int.Parse(pmaterial_id);
                oCommand.Parameters.Add("@popen_begin_date", SqlDbType.DateTime).Value = cCommon.CheckDate(popen_begin_date);
                oCommand.Parameters.Add("@popen_end_date", SqlDbType.DateTime).Value = cCommon.CheckDate(popen_end_date);
                oCommand.Parameters.Add("@popen_qty_month", SqlDbType.Int).Value = int.Parse(popen_qty_month);
                oCommand.Parameters.Add("@popen_qty_day", SqlDbType.Int).Value = int.Parse(popen_qty_day);
                oCommand.Parameters.Add("@popen_qty_person", SqlDbType.Int).Value = int.Parse(popen_qty_person);
                oCommand.Parameters.Add("@popen_rate", SqlDbType.Money).Value = float.Parse(popen_rate);
                oCommand.Parameters.Add("@popen_rate_all", SqlDbType.Money).Value = float.Parse(popen_rate_all);
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

        #region SP_OPEN_DETAIL_UPD
        public bool SP_OPEN_DETAIL_UPD(
            string popen_detail_id,
            string popen_head_id,
            string pmaterial_id,
            string popen_begin_date,
            string popen_end_date,
            string popen_qty_month,
            string popen_qty_day,
            string popen_qty_person,
            string popen_rate,
            string popen_rate_all,
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
                oCommand.CommandText = "sp_OPEN_DETAIL_UPD";
                oCommand.Parameters.Add("@popen_detail_id", SqlDbType.Int).Value = int.Parse(popen_detail_id);
                oCommand.Parameters.Add("@popen_head_id", SqlDbType.Int).Value = int.Parse(popen_head_id);
                oCommand.Parameters.Add("@pmaterial_id", SqlDbType.Int).Value = int.Parse(pmaterial_id);
                oCommand.Parameters.Add("@popen_begin_date", SqlDbType.DateTime).Value = cCommon.CheckDate(popen_begin_date);
                oCommand.Parameters.Add("@popen_end_date", SqlDbType.DateTime).Value = cCommon.CheckDate(popen_end_date);
                oCommand.Parameters.Add("@popen_qty_month", SqlDbType.Int).Value = int.Parse(popen_qty_month);
                oCommand.Parameters.Add("@popen_qty_day", SqlDbType.Int).Value = int.Parse(popen_qty_day);
                oCommand.Parameters.Add("@popen_qty_person", SqlDbType.Int).Value = int.Parse(popen_qty_person);
                oCommand.Parameters.Add("@popen_rate", SqlDbType.Money).Value = float.Parse(popen_rate);
                oCommand.Parameters.Add("@popen_rate_all", SqlDbType.Money).Value = float.Parse(popen_rate_all);
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

        #region SP_OPEN_DETAIL_DEL
        public bool SP_OPEN_DETAIL_DEL(
            string popen_head_id,
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
                oCommand.CommandText = "sp_OPEN_DETAIL_DEL";
                oCommand.Parameters.Add("@popen_head_id", SqlDbType.Int).Value = int.Parse(popen_head_id);
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

        #region sp_OPEN_DETAIL_TMP_SEL
        public bool sp_OPEN_DETAIL_TMP_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_OPEN_DETAIL_TMP_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_OPEN_DETAIL_TMP_SEL");
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

        #endregion

        #region OPEN PERSON

        #region SP_OPEN_PERSON_SEL
        public bool SP_OPEN_PERSON_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_OPEN_PERSON_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_OPEN_PERSON_SEL");
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

        #region SP_OPEN_PERSON_INS
        public bool SP_OPEN_PERSON_INS(
            string popen_head_id,
            string pperson_code,
            string popen_begin_date,
            string popen_end_date,
            string popen_qty_month,
            string popen_qty_day,
            string popen_qty_person,
            string popen_rate,
            string popen_rate_all,
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
                oCommand.CommandText = "sp_OPEN_PERSON_INS";
                oCommand.Parameters.Add("@popen_head_id", SqlDbType.Int).Value = int.Parse(popen_head_id);
                oCommand.Parameters.Add("@pperson_code", SqlDbType.VarChar).Value = pperson_code;
                oCommand.Parameters.Add("@popen_begin_date", SqlDbType.DateTime).Value = cCommon.CheckDate(popen_begin_date);
                oCommand.Parameters.Add("@popen_end_date", SqlDbType.DateTime).Value = cCommon.CheckDate(popen_end_date);
                oCommand.Parameters.Add("@popen_qty_month", SqlDbType.Int).Value = int.Parse(popen_qty_month);
                oCommand.Parameters.Add("@popen_qty_day", SqlDbType.Int).Value = int.Parse(popen_qty_day);
                oCommand.Parameters.Add("@popen_qty_person", SqlDbType.Int).Value = int.Parse(popen_qty_person);
                oCommand.Parameters.Add("@popen_rate", SqlDbType.Money).Value = float.Parse(popen_rate);
                oCommand.Parameters.Add("@popen_rate_all", SqlDbType.Money).Value = float.Parse(popen_rate_all);
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

        #region SP_OPEN_PERSON_UPD
        public bool SP_OPEN_PERSON_UPD(
            string popen_person_id,
            string popen_head_id,
            string pperson_code,
            string popen_begin_date,
            string popen_end_date,
            string popen_qty_month,
            string popen_qty_day,
            string popen_qty_person,
            string popen_rate,
            string popen_rate_all,
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
                oCommand.CommandText = "sp_OPEN_PERSON_UPD";
                oCommand.Parameters.Add("@popen_person_id", SqlDbType.Int).Value = int.Parse(popen_person_id);
                oCommand.Parameters.Add("@popen_head_id", SqlDbType.Int).Value = int.Parse(popen_head_id);
                oCommand.Parameters.Add("@pperson_code", SqlDbType.VarChar).Value = pperson_code;
                oCommand.Parameters.Add("@popen_begin_date", SqlDbType.DateTime).Value = cCommon.CheckDate(popen_begin_date);
                oCommand.Parameters.Add("@popen_end_date", SqlDbType.DateTime).Value = cCommon.CheckDate(popen_end_date);
                oCommand.Parameters.Add("@popen_qty_month", SqlDbType.Int).Value = int.Parse(popen_qty_month);
                oCommand.Parameters.Add("@popen_qty_day", SqlDbType.Int).Value = int.Parse(popen_qty_day);
                oCommand.Parameters.Add("@popen_qty_person", SqlDbType.Int).Value = int.Parse(popen_qty_person);
                oCommand.Parameters.Add("@popen_rate", SqlDbType.Money).Value = float.Parse(popen_rate);
                oCommand.Parameters.Add("@popen_rate_all", SqlDbType.Money).Value = float.Parse(popen_rate_all);
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

        #region SP_OPEN_PERSON_DEL
        public bool SP_OPEN_PERSON_DEL(
            string popen_person_id,
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
                oCommand.CommandText = "sp_OPEN_PERSON_DEL";
                oCommand.Parameters.Add("@popen_person_id", SqlDbType.Int).Value = int.Parse(popen_person_id);
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


        #region sp_OPEN_PERSON_TMP_SEL
        public bool SP_OPEN_PERSON_TMP_SEL(int pOpenID, string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_OPEN_PERSON_TMP_SEL";

                SqlParameter oParam_open_id = new SqlParameter("vopen_id", SqlDbType.Int);
                oParam_open_id.Direction = ParameterDirection.Input;
                oParam_open_id.Value = pOpenID;
                oCommand.Parameters.Add(oParam_open_id);

                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_OPEN_PERSON_TMP_SEL");
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

        #endregion

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
