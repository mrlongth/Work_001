using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace myDLL
{
    public class cefOpen : IDisposable
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

        public cefOpen()
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

        #region SP_OPEN_SEL
        public DataTable SP_OPEN_SEL(string strCriteria)
        {
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            strCriteria += strCriteria.ToLower().Contains("order by") ? string.Empty : " Order by open_title";
            try
            {
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = "Select * from ef_open where 1=1 " + strCriteria;
                oAdapter = new SqlDataAdapter(oCommand);
                oAdapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oConn.Close();
                oCommand.Dispose();
                oConn.Dispose();
            }
            return dt;
        }
        #endregion

        #region SP_OPEN_INS
        public bool SP_OPEN_INS(
             ref int popen_code,
             string popen_to,
             string popen_title,
             string popen_command_desc,
             string popen_desc,
             string popen_report_code,
             string popen_remark,
             string pc_created_by)
        {
            bool blnResult = false;
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            try
            {
                string strSql = "Insert into ef_OPEN ([open_to],[open_title],[open_command_desc],[open_desc]," +
                                "[open_report_code],[open_remark],[c_created_by],[d_created_date]) values ( " +
                                "'" + popen_to + "'," +
                                "'" + popen_title + "'," +
                                "'" + popen_command_desc + "'," +
                                "'" + popen_desc + "'," +
                                "'" + popen_report_code + "'," +
                                "'" + popen_remark + "'," +
                                "'" + pc_created_by + "'," +
                                "'" + cCommon.GetDateTimeNow() + "')";
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = strSql;
                oCommand.ExecuteNonQuery();
                var dt = SP_OPEN_SEL(" and open_title='" + popen_title + "'");
                popen_code = Helper.CInt(dt.Rows[0]["open_code"]);
                blnResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
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
             int popen_code,
             string popen_to,
             string popen_title,
             string popen_command_desc,
             string popen_desc,
             string popen_report_code,
             string popen_remark,
             string pc_updated_by)
        {
            bool blnResult = false;
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            try
            {
                string strSql = "Update ef_OPEN Set " +
                                "open_to = '" + popen_to + "'," +
                                "open_title= '" + popen_title + "'," +
                                "open_command_desc= '" + popen_command_desc + "'," +
                                "open_desc= '" + popen_desc + "'," +
                                "open_report_code = '" + popen_report_code + "'," +
                                "open_remark = '" + popen_remark + "'," +
                                "c_updated_by = '" + pc_updated_by + "'," +
                                "d_updated_date = '" + cCommon.GetDateTimeNow() + "' " +
                                "Where open_code = " + popen_code;
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = strSql;
                oCommand.ExecuteNonQuery();
                blnResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
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
        public bool SP_OPEN_DEL(int popen_code)
        {
            bool blnResult = false;
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            try
            {
                string strSql = "Delete from ef_open " +
                                "Where open_code = " + popen_code;
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = strSql;
                oCommand.ExecuteNonQuery();
                blnResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
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

        #region SP_OPEN_HEAD_SUM_UPD
        public bool SP_OPEN_HEAD_SUM_UPD(
           long popen_head_id,
           string pc_updated_by)
        {
            bool blnResult = false;
            var oConn = new SqlConnection();
            var oCommand = new SqlCommand();
            try
            {
                string strSql = "Update ef_open_head  Set " +
                                "open_amount = (select sum(open_detail_amount) from ef_open_detail where open_head_id = " + popen_head_id + "), " +
                                "d_updated_date = '" + cCommon.GetDateTimeNow() + "', " +
                                "c_updated_by = '" + pc_updated_by + "' " +
                                "Where open_head_id = " + popen_head_id;

                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = strSql;
                oCommand.ExecuteNonQuery();
                blnResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
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


        #region SP_OPEN_HEAD_SEL
        public DataTable SP_OPEN_HEAD_SEL(string strCriteria)
        {
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            strCriteria += strCriteria.ToLower().Contains("order by") ? string.Empty : " Order by open_doc";
            try
            {
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = "Select * from view_ef_open_head where 1=1 " + strCriteria;
                oAdapter = new SqlDataAdapter(oCommand);
                oAdapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oConn.Close();
                oCommand.Dispose();
                oConn.Dispose();
            }
            return dt;
        }
        #endregion


        #region SP_OPEN_COUNT_HEAD_SEL
        public DataTable SP_OPEN_COUNT_HEAD_SEL(string strCriteria)
        {
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            strCriteria += strCriteria.ToLower().Contains("order by") ? string.Empty : " Order by open_doc";
            try
            {
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = "Select * from view_ef_open_count_head where 1=1 " + strCriteria;
                oAdapter = new SqlDataAdapter(oCommand);
                oAdapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oConn.Close();
                oCommand.Dispose();
                oConn.Dispose();
            }
            return dt;
        }
        #endregion


        #region SP_OPEN_HEAD_DEL
        public bool SP_OPEN_HEAD_DEL(int popen_head_id)
        {
            bool blnResult = false;
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            try
            {
                string strSql = "Update ef_open_head set approve_head_status = 'C' " +
                                "Where open_head_id = " + popen_head_id;
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = strSql;
                oCommand.ExecuteNonQuery();
                blnResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
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
           ref int popen_head_id,
           ref string popen_doc,
           string popen_year,
           string popen_date,
           string popen_path,
           string popen_no,
           int popen_code,
           string popen_to,
           string popen_title,
           string popen_command_desc,
           string popen_desc,
           string pbudget_type,
           string pbudget_type_text,
           string pbudget_plan_code,
           string pdirector_code,
           string punit_code,
           string pbutget_code,
           string pproduce_code,
           string pactivity_code,
           string pplan_code,
           string pwork_code,
           string pfund_code,
           string plot_code,
           string pperson_open,
           string popen_tel,
           string popen_remark,
           double popen_amount,
           string ploan_doc,
           string pdoctype_code,
           string pc_created_by)
        {
            bool blnResult = false;
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            try
            {
                var dtMaxCode = SP_OPEN_HEAD_SEL(" and open_head_id = (select max(open_head_id) from [ef_open_head])");
                int maxCode = (dtMaxCode.Rows.Count > 0) ? Helper.CInt(dtMaxCode.Rows[0]["open_head_id"]) : 0;
                maxCode++;
                popen_doc = maxCode.ToString().PadLeft(10, '0');
                popen_doc = popen_year.Substring(2, 2) + "O" + maxCode.ToString().PadLeft(7, '0');

                string strSql = "Insert into ef_open_head ([open_doc],[open_year],[open_date],[open_path],[open_no],[open_code],[open_to]," +
                                "[open_title],[open_command_desc],[open_desc],[budget_type],[budget_type_text],[budget_plan_code],[director_code],[unit_code],[budget_code]," +
                                "[produce_code],[activity_code],[plan_code],[work_code],[fund_code],[lot_code],[person_open],[open_tel],[open_remark]," +
                                "[open_amount],[loan_doc],[approve_head_status],[ef_doctype_code],[c_created_by],[d_created_date]) values ( " +
                                "'" + popen_doc + "'," +
                                "'" + popen_year + "'," +
                                "'" + cCommon.SaveDate(popen_date) + "'," +
                                "'" + popen_path + "'," +
                                "'" + popen_no + "'," +
                                "" + popen_code + "," +
                                "'" + popen_to + "'," +
                                "'" + popen_title + "'," +
                                "'" + popen_command_desc + "'," +
                                "'" + popen_desc + "'," +
                                "'" + pbudget_type + "'," +
                                "'" + pbudget_type_text + "'," +
                                "'" + pbudget_plan_code + "'," +
                                "'" + pdirector_code + "'," +
                                "'" + punit_code + "'," +
                                "'" + pbutget_code + "'," +
                                "'" + pproduce_code + "'," +
                                "'" + pactivity_code + "'," +
                                "'" + pplan_code + "'," +
                                "'" + pwork_code + "'," +
                                "'" + pfund_code + "'," +
                                "'" + plot_code + "'," +
                                "'" + pperson_open + "'," +
                                "'" + popen_tel + "'," +
                                "'" + popen_remark + "'," +
                                "" + popen_amount + "," +
                                "'" + ploan_doc + "'," +
                                "'W'," +
                                "" + pdoctype_code + "," +
                                "'" + pc_created_by + "'," +
                                "'" + cCommon.GetDateTimeNow() + "')";
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = strSql;
                oCommand.ExecuteNonQuery();
                var dt = SP_OPEN_HEAD_SEL(" and open_doc='" + popen_doc + "'");
                popen_head_id = Helper.CInt(dt.Rows[0]["open_head_id"]);
                blnResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
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
           int popen_head_id,
           string popen_year,
           string popen_date,
           string popen_path,
           string popen_no,
           int popen_code,
           string popen_to,
           string popen_title,
           string popen_command_desc,
           string popen_desc,
           string pbudget_type,
           string pbudget_type_text,
           string pbudget_plan_code,
           string pdirector_code,
           string punit_code,
           string pbutget_code,
           string pproduce_code,
           string pactivity_code,
           string pplan_code,
           string pwork_code,
           string pfund_code,
           string plot_code,
           string pperson_open,
           string popen_tel,
           string popen_remark,
           double popen_amount,
           string ploan_doc,
           string pdoctype_code,
           string pc_updated_by)
        {
            bool blnResult = false;
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            try
            {
                string strSql = "Update ef_open_head  Set " +
                                "open_year = '" + popen_year + "'," +
                                "open_date = '" + cCommon.SaveDate(popen_date) + "'," +
                                "open_path = '" + popen_path + "'," +
                                "open_no = '" + popen_no + "'," +
                                "open_code = " + popen_code + "," +
                                "open_to = '" + popen_to + "'," +
                                "open_title = '" + popen_title + "'," +
                                "open_command_desc = '" + popen_command_desc + "'," +
                                "open_desc = '" + popen_desc + "'," +
                                "budget_type = '" + pbudget_type + "'," +
                                "budget_type_text = '" + pbudget_type_text + "'," +
                                "budget_plan_code = '" + pbudget_plan_code + "'," +
                                "director_code = '" + pdirector_code + "'," +
                                "unit_code = '" + punit_code + "'," +
                                "budget_code = '" + pbutget_code + "'," +
                                "produce_code = '" + pproduce_code + "'," +
                                "activity_code = '" + pactivity_code + "'," +
                                "plan_code = '" + pplan_code + "'," +
                                "work_code = '" + pwork_code + "'," +
                                "fund_code = '" + pfund_code + "'," +
                                "lot_code = '" + plot_code + "'," +
                                "person_open = '" + pperson_open + "'," +
                                "open_tel = '" + popen_tel + "'," +
                                "open_remark = '" + popen_remark + "'," +
                                "open_amount = " + popen_amount + "," +
                                "loan_doc = '" + ploan_doc + "'," +
                                "ef_doctype_code = " + pdoctype_code + "," +
                                "c_updated_by = '" + pc_updated_by + "'," +
                                "d_updated_date = '" + cCommon.GetDateTimeNow() + "' " +
                                "Where open_head_id = " + popen_head_id;
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = strSql;
                oCommand.ExecuteNonQuery();
                blnResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
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

        #region SP_OPEN_HEAD_APPROVE_UPD
        public bool SP_OPEN_HEAD_APPROVE_UPD(
           int popen_head_id,
           string pc_updated_by)
        {
            bool blnResult = false;
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            DataTable dtCheck = new DataTable();
            string strApprove_head_status = "P";
            try
            {
                string strSql = " and open_head_id=" + popen_head_id;
                DataTable dt = SP_OPEN_DETAIL_APPROVE_SEL(strSql);
                if (dt.Rows.Count > 0)
                {
                    dtCheck = (new DataView(dt, "approve_status='A'", "", DataViewRowState.CurrentRows)).ToTable();
                    if (dt.Rows.Count == dtCheck.Rows.Count)
                    {
                        strApprove_head_status = "A";
                    }
                    else
                    {
                        dtCheck = (new DataView(dt, "approve_status='P'", "", DataViewRowState.CurrentRows)).ToTable();
                        if (dt.Rows.Count == dtCheck.Rows.Count)
                        {
                            strApprove_head_status = "P";
                        }
                        else
                        {
                            dtCheck = (new DataView(dt, "approve_status='N'", "", DataViewRowState.CurrentRows)).ToTable();
                            if (dtCheck.Rows.Count > 0)
                            {
                                strApprove_head_status = "N";
                            }
                            else
                            {
                                strApprove_head_status = "X";
                            }
                        }
                    }
                }

                strSql = "Update ef_open_head  Set " +
                                "approve_head_status = '" + strApprove_head_status + "', " +
                                "d_updated_date = '" + cCommon.GetDateTimeNow() + "', " +
                                "c_updated_by = '" + pc_updated_by + "' " +
                                "Where open_head_id = " + popen_head_id + " and approve_head_status not in ('W','C')";

                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = strSql;
                oCommand.ExecuteNonQuery();
                blnResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
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


        #region SP_OPEN_HEAD_PASS
        public bool SP_OPEN_HEAD_PASS(int popen_head_id)
        {
            bool blnResult = false;
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            try
            {
                string strSql = "Update ef_open_head set approve_head_status = 'P' , d_process_date = '" + cCommon.GetDateTimeNow() + "' " +
                                "Where open_head_id = " + popen_head_id;
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = strSql;
                oCommand.ExecuteNonQuery();
                blnResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
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

        #region SP_OPEN_HEAD_RESTORE
        public bool SP_OPEN_HEAD_RESTORE(int popen_head_id)
        {
            bool blnResult = false;
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            try
            {
                string strSql = "Update ef_open_head set approve_head_status = 'W' " +
                                "Where open_head_id = " + popen_head_id + ";";

                strSql += "Update ef_open_detail_approve set approve_status = 'P' " +
                                "Where open_head_id = " + popen_head_id + ";";

                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = strSql;
                oCommand.ExecuteNonQuery();
                blnResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
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



        #region SP_OPEN_DETAIL_SEL
        public DataTable SP_OPEN_DETAIL_SEL(string strCriteria)
        {
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            strCriteria += strCriteria.ToLower().Contains("order by") ? string.Empty : " Order by open_detail_id";
            try
            {
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = "Select * from view_ef_open_detail where 1=1 " + strCriteria;
                oAdapter = new SqlDataAdapter(oCommand);
                oAdapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oConn.Close();
                oCommand.Dispose();
                oConn.Dispose();
            }
            return dt;
        }
        #endregion

        #region SP_OPEN_DETAIL_INS
        public bool SP_OPEN_DETAIL_INS(
            int popen_head_id,
            int pmaterial_id,
            string pmaterial_name,
            string pmaterial_detail,
            string popen_detail_remark,
            double popen_detail_amount)
        {
            bool blnResult = false;
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            try
            {
                string strSql = "Insert into [ef_open_detail]([open_head_id],[material_id],[material_name]," +
                                "[material_detail],[open_detail_remark],[open_detail_amount]) values ( " +
                                "" + popen_head_id + "," +
                                "" + pmaterial_id + "," +
                                "'" + pmaterial_name + "'," +
                                "'" + pmaterial_detail + "'," +
                                "'" + popen_detail_remark + "'," +
                                "" + popen_detail_amount + ")";
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = strSql;
                oCommand.ExecuteNonQuery();
                blnResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
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
            long popen_detail_id,
            int popen_head_id,
            int pmaterial_id,
            string pmaterial_name,
            string pmaterial_detail,
            string popen_detail_remark,
            double popen_detail_amount)
        {
            bool blnResult = false;
            var oConn = new SqlConnection();
            var oCommand = new SqlCommand();
            try
            {
                string strSql = "Update [ef_open_detail] Set " +
                                "open_head_id = " + popen_head_id + "," +
                                "material_id = " + pmaterial_id + "," +
                                "material_name = '" + pmaterial_name + "'," +
                                "material_detail = '" + pmaterial_detail + "'," +
                                "open_detail_remark = '" + popen_detail_remark + "'," +
                                "open_detail_amount = " + popen_detail_amount + " " +
                                " Where open_detail_id = " + popen_detail_id;
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = strSql;
                oCommand.ExecuteNonQuery();
                blnResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
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
        public bool SP_OPEN_DETAIL_DEL(int popen_detail_id)
        {
            bool blnResult = false;
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            try
            {
                string strSql = "Delete from ef_open_detail " +
                                "Where open_detail_id = " + popen_detail_id;
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = strSql;
                oCommand.ExecuteNonQuery();
                blnResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
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


        #region SP_OPEN_DETAIL_APPROVE_COUNT_SEL
        public DataTable SP_OPEN_DETAIL_APPROVE_COUNT_SEL(string strCriteria)
        {
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            strCriteria += strCriteria.ToLower().Contains("order by") ? string.Empty : " Order by approve_level";
            try
            {
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = "Select * from view_ef_open_detail_approve_count where 1=1 " + strCriteria;
                oAdapter = new SqlDataAdapter(oCommand);
                oAdapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oConn.Close();
                oCommand.Dispose();
                oConn.Dispose();
            }
            return dt;
        }
        #endregion


        #region SP_OPEN_DETAIL_APPROVE_SEL
        public DataTable SP_OPEN_DETAIL_APPROVE_SEL(string strCriteria)
        {
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            strCriteria += strCriteria.ToLower().Contains("order by") ? string.Empty : " Order by approve_level";
            try
            {
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = "Select * from view_ef_open_detail_approve where 1=1 " + strCriteria;
                oAdapter = new SqlDataAdapter(oCommand);
                oAdapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oConn.Close();
                oCommand.Dispose();
                oConn.Dispose();
            }
            return dt;
        }
        #endregion

        #region SP_OPEN_DETAIL_APPROVE_DEL
        public bool SP_OPEN_DETAIL_APPROVE_DEL(int popen_detail_approve_id)
        {
            bool blnResult = false;
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            try
            {
                string strSql = "Delete from ef_open_detail_approve " +
                                "Where open_detail_approve_id = " + popen_detail_approve_id;
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = strSql;
                oCommand.ExecuteNonQuery();
                blnResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
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

        #region SP_OPEN_DETAIL_APPROVE_ALL_DEL
        public bool SP_OPEN_DETAIL_APPROVE_ALL_DEL(int popen_head_id)
        {
            bool blnResult = false;
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            try
            {
                string strSql = "Delete from ef_open_detail_approve " +
                                "Where open_head_id = " + popen_head_id;
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = strSql;
                oCommand.ExecuteNonQuery();
                blnResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
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

        #region SP_OPEN_DETAIL_APPROVE_INS
        public bool SP_OPEN_DETAIL_APPROVE_INS(
            int popen_head_id,
            int papprove_code,
            string papprove_name,
            int papprove_level,
            string pperson_code,
            string pperson_manage_code,
            string pperson_manage_name,
            string papprove_remark,
            string papprove_status,
            string pc_created_by)
        {
            bool blnResult = false;
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            try
            {
                string strSql = "Insert into [ef_open_detail_approve] ([open_head_id],[approve_code],[approve_name],[approve_level]," +
                     "[person_code],[person_manage_code],[person_manage_name],[approve_remark],[approve_status],[c_created_by],[d_created_date]) values ( " +
                                "" + popen_head_id + "," +
                                "" + papprove_code + "," +
                                "'" + papprove_name + "'," +
                                "" + papprove_level + "," +
                                "'" + pperson_code + "'," +
                                "'" + pperson_manage_code + "'," +
                                "'" + pperson_manage_name + "'," +
                                "'" + papprove_remark + "'," +
                                "'" + papprove_status + "'," +
                                "'" + pc_created_by + "'," +
                                "'" + cCommon.GetDateTimeNow() + "')";
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = strSql;
                oCommand.ExecuteNonQuery();
                blnResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
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

        #region SP_OPEN_DETAIL_APPROVE_UPD
        public bool SP_OPEN_DETAIL_APPROVE_UPD(
            int popen_detail_approve_id,
            int papprove_code,
            string papprove_name,
            int papprove_level,
            string pperson_code,
            string pperson_manage_code,
            string pperson_manage_name,
            string papprove_remark,
            string papprove_status,
            string pc_updated_by)
        {
            bool blnResult = false;
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            try
            {
                string strSql = "Update ef_open_detail_approve Set " +
                                "approve_code=" + papprove_code + "," +
                                "approve_name='" + papprove_name + "'," +
                                "approve_level=" + papprove_level + "," +
                                "person_code='" + pperson_code + "'," +
                                "person_manage_code='" + pperson_manage_code + "'," +
                                "person_manage_name='" + pperson_manage_name + "'," +
                                "approve_remark ='" + papprove_remark + "'," +
                                "approve_status ='" + papprove_status + "', " +
                                "c_updated_by ='" + pc_updated_by + "', " +
                                "d_updated_date ='" + cCommon.GetDateTimeNow() + "' " +
                                "Where open_detail_approve_id = " + popen_detail_approve_id;
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = strSql;
                oCommand.ExecuteNonQuery();
                blnResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
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

        #region SP_OPEN_DETAIL_APPROVE_EDIT
        public bool SP_OPEN_DETAIL_APPROVE_EDIT(
            int popen_detail_approve_id,
            string papprove_note,
            string papprove_status,
            string pc_updated_by)
        {
            bool blnResult = false;
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            try
            {
                string strSql = "Update ef_open_detail_approve Set " +
                                "approve_note ='" + papprove_note + "'," +
                                "approve_status ='" + papprove_status + "', " +
                                "c_updated_by ='" + pc_updated_by + "', " +
                                "d_approved_date ='" + cCommon.GetDateTimeNow() + "' " +
                                "Where open_detail_approve_id = " + popen_detail_approve_id;
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = strSql;
                oCommand.ExecuteNonQuery();
                blnResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
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


        #region SP_OPEN_LOAN_SEL
        public DataTable SP_OPEN_LOAN_SEL(string strCriteria)
        {
            var oConn = new SqlConnection();
            var oCommand = new SqlCommand();
            var dt = new DataTable();
            strCriteria += strCriteria.ToLower().Contains("order by") ? string.Empty : " Order by loan_doc";
            try
            {
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = "Select * from view_ef_open_loan where 1=1 " + strCriteria;
                var oAdapter = new SqlDataAdapter(oCommand);
                oAdapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oConn.Close();
                oCommand.Dispose();
                oConn.Dispose();
            }
            return dt;
        }
        #endregion

        #region SP_OPEN_LOAN_INS
        public bool SP_OPEN_LOAN_INS(
            int popen_head_id,
            long loan_id,
            string pc_created_by)
        {
            bool blnResult = false;
            var oConn = new SqlConnection();
            var oCommand = new SqlCommand();
            try
            {
                string strSql = "Insert into [ef_open_loan] ([open_head_id],[loan_id],[c_created_by],[d_created_date]) values ( " +
                                "" + popen_head_id + "," +
                                "" + loan_id + "," +
                                "'" + pc_created_by + "'," +
                                "'" + cCommon.GetDateTimeNow() + "')";
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = strSql;
                oCommand.ExecuteNonQuery();
                blnResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
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

        #region SP_OPEN_LOAN_UPD
        public bool SP_OPEN_LOAN_UPD(
            long popen_loan_id,
            int popen_head_id,
            long loan_id,
            string pc_updated_by)
        {
            bool blnResult = false;
            var oConn = new SqlConnection();
            var oCommand = new SqlCommand();
            try
            {
                string strSql = "Update [ef_open_loan] Set " +
                    "open_head_id = " + popen_head_id + "," +
                    "loan_id = " + loan_id + "," +
                    "c_updated_by = '" + pc_updated_by + "'," +
                    "d_updated_date = '" + cCommon.GetDateTimeNow() + "' " +
                    " Where open_loan_id = " + popen_loan_id;
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = strSql;
                oCommand.ExecuteNonQuery();
                blnResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
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

        #region SP_OPEN_LOAN_DEL
        public bool SP_OPEN_LOAN_DEL(long popen_loan_id)
        {
            bool blnResult = false;
            var oConn = new SqlConnection();
            var oCommand = new SqlCommand();
            try
            {
                string strSql = "Delete from [ef_open_loan] Where open_loan_id = " + popen_loan_id;
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = strSql;
                oCommand.ExecuteNonQuery();
                blnResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
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


        #region SP_OPEN_ATTACH_SEL
        public DataTable SP_OPEN_ATTACH_SEL(string strCriteria)
        {
            var oConn = new SqlConnection();
            var oCommand = new SqlCommand();
            var dt = new DataTable();
            strCriteria += strCriteria.ToLower().Contains("order by") ? string.Empty : " Order by open_attach_id";
            try
            {
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = "Select * from view_ef_open_attach where 1=1 " + strCriteria;
                var oAdapter = new SqlDataAdapter(oCommand);
                oAdapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oConn.Close();
                oCommand.Dispose();
                oConn.Dispose();
            }
            return dt;
        }
        #endregion

        #region SP_OPEN_ATTACH_INS
        public bool SP_OPEN_ATTACH_INS(
            int popen_head_id,
            string popen_attach_des,
            string open_attach_file_name,
            string pc_created_by)
        {
            bool blnResult = false;
            var oConn = new SqlConnection();
            var oCommand = new SqlCommand();
            try
            {
                string strSql = "Insert into ef_open_attach ([open_head_id],[open_attach_des],[open_attach_file_name],[open_attach_date],[c_created_by],[d_created_date]) values ( " +
                                "" + popen_head_id + "," +
                                "'" + popen_attach_des + "'," +
                                "'" + open_attach_file_name + "'," +
                                "'" + cCommon.GetDateTimeNow() + "'," +
                                "'" + pc_created_by + "'," +
                                "'" + cCommon.GetDateTimeNow() + "')";
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = strSql;
                oCommand.ExecuteNonQuery();
                blnResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
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

        #region SP_OPEN_ATTACH_UPD
        public bool SP_OPEN_ATTACH_UPD(
            long popen_attach_id,
            int popen_head_id,
            string popen_attach_des,
            string pc_updated_by)
        {
            bool blnResult = false;
            var oConn = new SqlConnection();
            var oCommand = new SqlCommand();
            try
            {
                string strSql = "Update ef_open_attach set " +
                                "open_head_id = " + popen_head_id + "," +
                                "open_attach_des = '" + popen_attach_des + "'," +
                                "open_attach_date = '" + cCommon.GetDateTimeNow() + "'," +
                                "c_updated_by = '" + pc_updated_by + "'," +
                                "d_created_date = '" + cCommon.GetDateTimeNow() + "'" +
                                " Where open_attach_id = " + popen_attach_id;
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = strSql;
                oCommand.ExecuteNonQuery();
                blnResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
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

        #region SP_OPEN_ATTACH_DEL
        public bool SP_OPEN_ATTACH_DEL(
            long popen_attach_id)
        {
            bool blnResult = false;
            var oConn = new SqlConnection();
            var oCommand = new SqlCommand();
            try
            {
                string strSql = "Delete from ef_open_attach " +
                                " Where open_attach_id = " + popen_attach_id;
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = strSql;
                oCommand.ExecuteNonQuery();
                blnResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
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

        #region GetOpenDesc
        public string GetOpenDesc(string strOpenDesc, string person_code)
        {
            cPerson oPerson = new cPerson();
            DataSet ds = null;
            string strMessage = string.Empty ;
            string strCriteria = " and person_code = '" + person_code + "' ";
            List<string> defineName = new List<string>()
	        {
	           "title_name", "person_thai_name", "person_thai_surname", "position_name", "director_name", "unit_name", "work_name"
	        };

            try
            {
                if (oPerson.SP_PERSON_ALL_SEL(strCriteria, ref ds, ref strMessage))
                {
                    foreach (var name in defineName)
                    {
                        if (strOpenDesc.Contains(name))
                        {
                            strOpenDesc = strOpenDesc.Replace("@" + name, Helper.CStr(ds.Tables[0].Rows[0][name]));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oPerson.Dispose(); 
            }
            return strOpenDesc;
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
