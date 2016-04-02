using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace myDLL
{
    public class cefLoan : IDisposable
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

        public cefLoan()
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

        #region SP_LOAN_HEAD_PASS
        public bool SP_LOAN_HEAD_PASS(int ploan_id)
        {
            bool blnResult = false;
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            try
            {
                string strSql = "Update ef_loan_head set loan_status = 'P' , d_process_date = '" + cCommon.GetDateTimeNow() + "' " +
                                "Where loan_id = " + ploan_id;
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

        #region SP_LOAN_HEAD_RESTORE
        public bool SP_LOAN_HEAD_RESTORE(int ploan_id)
        {
            bool blnResult = false;
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            try
            {
                string strSql = "Update ef_loan_head set loan_status = 'W' " +
                                "Where loan_id = " + ploan_id + ";";
                strSql += "Update ef_loan_detail_approve set approve_status = 'P' " +
                "Where loan_id = " + ploan_id + ";";

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

        #region SP_LOAN_HEAD_SEL
        public DataTable SP_LOAN_HEAD_SEL(string strCriteria)
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
                oCommand.CommandText = "Select * from view_ef_loan_head where 1=1 " + strCriteria;
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

        #region SP_LOAN_HEAD_DEL
        public bool SP_LOAN_HEAD_DEL(int ploanHeadId)
        {
            bool blnResult = false;
            var oConn = new SqlConnection();
            var oCommand = new SqlCommand();
            try
            {
                string strSql = "Update ef_loan_head set loan_status = 'C' " +
                                "Where loan_id = " + ploanHeadId;
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

        #region SP_LOAN_HEAD_INS
        public bool SP_LOAN_HEAD_INS(
            ref long ploan_id,
            ref string ploan_doc,
            string ploan_doc_no,
            string ploan_year,
            string ploan_date,
            string ploan_date_due,
            string ploan_path,
            string ploan_no,
            string pperson_code,
            string pposition_code,
            string pposition_name,
            string ploan_reason,
            string ploan_offer,
            string pbudget_type,
            string pbudget_type_text,
            string pbudget_plan_code,
            string pdirector_code,
            string punit_code,
            string pbudget_code,
            string pproduce_code,
            string pactivity_code,
            string pplan_code,
            string pwork_code,
            string pfund_code,
            string plot_code,
            string ploan_req,
            string ploan_approve,
            string ploan_status,
            string ploan_tel,
            string ploan_remark,
            double ploan_return,
            string ploan_return_remark,
            string pdoctype_code,
            string ploan_old_year,
            string pc_created_by)
        {
            bool blnResult = false;
            var oConn = new SqlConnection();
            var oCommand = new SqlCommand();
            try
            {
                var dtMaxCode = SP_LOAN_HEAD_SEL(" and loan_id = (select max(loan_id) from [ef_loan_head] where loan_year = '" + ploan_year + "')");
                int maxCode = (dtMaxCode.Rows.Count > 0) ? Helper.CInt(dtMaxCode.Rows[0]["loan_id"]) : 0;
                maxCode++;
                ploan_doc = ploan_year.Substring(2, 2) + "P" + maxCode.ToString(CultureInfo.InvariantCulture).PadLeft(7, '0');

                string strSql = "Insert into ef_loan_head ([loan_doc],[loan_doc_no],[loan_year],[loan_date],[loan_date_due],[loan_path],[loan_no],[person_code],[position_code]," +
                                "[position_name],[loan_reason],[loan_offer],[budget_type],[budget_type_text],[budget_plan_code],[director_code],[unit_code]," +
                                "[budget_code],[produce_code],[activity_code],[plan_code],[work_code],[fund_code],[lot_code],[loan_req],[loan_approve]," +
                                "[loan_status],[loan_tel],[loan_remark],[loan_return],[loan_return_remark],[ef_doctype_code],[loan_old_year],[c_created_by],[d_created_date]) values ( " +
                                "'" + ploan_doc + "'," +
                                "'" + ploan_doc_no + "'," +
                                "'" + ploan_year + "'," +
                                "'" + cCommon.SaveDate(ploan_date) + "'," +
                               (ploan_date_due.Length > 0 ? "'" + cCommon.SaveDate(ploan_date_due) + "'" : "null") + "," +
                                "'" + ploan_path + "'," +
                                "'" + ploan_no + "'," +
                                "'" + pperson_code + "'," +
                                "'" + pposition_code + "'," +
                                "'" + pposition_name + "'," +
                                "'" + ploan_reason + "'," +
                                "'" + ploan_offer + "'," +
                                "'" + pbudget_type + "'," +
                                "'" + pbudget_type_text + "'," +
                                "'" + pbudget_plan_code + "'," +
                                "'" + pdirector_code + "'," +
                                "'" + punit_code + "'," +
                                "'" + pbudget_code + "'," +
                                "'" + pproduce_code + "'," +
                                "'" + pactivity_code + "'," +
                                "'" + pplan_code + "'," +
                                "'" + pwork_code + "'," +
                                "'" + pfund_code + "'," +
                                "'" + plot_code + "'," +
                                "" + ploan_req + "," +
                                "" + ploan_approve + "," +
                                "'" + ploan_status + "'," +
                                "'" + ploan_tel + "'," +
                                "'" + ploan_remark + "'," +
                                "" + ploan_return + "," +
                                "'" + ploan_return_remark + "'," +
                                "" + pdoctype_code + "," +
                                "'" + ploan_old_year + "'," +
                                "'" + pc_created_by + "'," +
                                "'" + cCommon.GetDateTimeNow() + "')";
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = strSql;
                oCommand.ExecuteNonQuery();
                var dt = SP_LOAN_HEAD_SEL(" and loan_doc='" + ploan_doc + "'");
                ploan_id = Helper.CLong(dt.Rows[0]["loan_id"]);
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

        #region SP_LOAN_HEAD_UPD
        public bool SP_LOAN_HEAD_UPD(
            long ploan_id,
            string ploan_doc,
            string ploan_doc_no,
            string ploan_year,
            string ploan_date,
            string ploan_date_due,
            string ploan_path,
            string ploan_no,
            string pperson_code,
            string pposition_code,
            string pposition_name,
            string ploan_reason,
            string ploan_offer,
            string pbudget_type,
            string pbudget_type_text,
            string pbudget_plan_code,
            string pdirector_code,
            string punit_code,
            string pbudget_code,
            string pproduce_code,
            string pactivity_code,
            string pplan_code,
            string pwork_code,
            string pfund_code,
            string plot_code,
            string ploan_tel,
            string ploan_remark,
            double ploan_return,
            string ploan_return_remark,
            string pdoctype_code,
            string ppay_type,
            string ppay_acc_no,
            string ppay_name,
            string ppay_bank,
            string ppay_bank_branch,
            string ppay_remark,
            string ploan_old_year,
            string pc_updated_by)
        {
            bool blnResult = false;
            var oConn = new SqlConnection();
            var oCommand = new SqlCommand();
            try
            {
                string strSql = "Update ef_loan_head  Set " +
                                "loan_doc = '" + ploan_doc + "'," +
                                "loan_doc_no = '" + ploan_doc_no + "'," +
                                "loan_year = '" + ploan_year + "'," +
                                "loan_date = '" + cCommon.SaveDate(ploan_date) + "'," +
                                "loan_date_due =" + (ploan_date_due.Length > 0 ? "'" + cCommon.SaveDate(ploan_date_due) + "'" : "null") + "," +
                                "loan_path= '" + ploan_path + "'," +
                                "loan_no = '" + ploan_no + "'," +
                                "person_code = '" + pperson_code + "'," +
                                "position_code = '" + pposition_code + "'," +
                                "position_name = '" + pposition_name + "'," +
                                "loan_reason = '" + ploan_reason + "'," +
                                "loan_offer = '" + ploan_offer + "'," +
                                "budget_type = '" + pbudget_type + "'," +
                                "budget_type_text = '" + pbudget_type_text + "'," +
                                "budget_plan_code = '" + pbudget_plan_code + "'," +
                                "director_code = '" + pdirector_code + "'," +
                                "unit_code = '" + punit_code + "'," +
                                "budget_code = '" + pbudget_code + "'," +
                                "produce_code = '" + pproduce_code + "'," +
                                "activity_code = '" + pactivity_code + "'," +
                                "plan_code = '" + pplan_code + "'," +
                                "work_code = '" + pwork_code + "'," +
                                "fund_code = '" + pfund_code + "'," +
                                "lot_code = '" + plot_code + "'," +
                                "loan_tel = '" + ploan_tel + "'," +
                                "loan_remark = '" + ploan_remark + "'," +
                                "loan_return = " + ploan_return + "," +
                                "loan_return_remark = '" + ploan_return_remark + "'," +
                                "ef_doctype_code = " + pdoctype_code + "," +
                                "pay_type= '" + ppay_type + "'," +
                                "pay_acc_no= '" + ppay_acc_no + "'," +
                                "pay_name= '" + ppay_name + "'," +
                                "pay_bank= '" + ppay_bank + "'," +
                                "pay_bank_branch= '" + ppay_bank_branch + "'," +
                                "pay_remark= '" + ppay_remark + "'," +
                                "loan_old_year = '" + ploan_old_year + "'," +                                
                                "c_updated_by = '" + pc_updated_by + "'," +
                                "d_updated_date = '" + cCommon.GetDateTimeNow() + "' " +
                                " Where loan_id = " + ploan_id;
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

        #region SP_LOAN_HEAD_APPROVE_UPD
        public bool SP_LOAN_HEAD_APPROVE_UPD(
           long ploan_id,
           string pc_updated_by)
        {
            bool blnResult = false;
            var oConn = new SqlConnection();
            var oCommand = new SqlCommand();
            var oAdapter = new SqlDataAdapter();
            DataTable dtCheck;
            var strApprove_head_status = "P";
            try
            {
                string strSql = " and loan_id=" + ploan_id;
                DataTable dt = SP_LOAN_DETAIL_APPROVE_SEL(strSql);
                if (dt.Rows.Count > 0)
                {
                    dtCheck = (new DataView(dt, "approve_status='A'", "", DataViewRowState.CurrentRows)).ToTable();
                    if (dt.Rows.Count == dtCheck.Rows.Count)
                    {
                        strApprove_head_status = "A";
                        dt = SP_LOAN_HEAD_SEL(strSql);
                        if (dt.Rows.Count > 0)
                        {
                            if (Helper.CDbl(dt.Rows[0]["loan_return"]) > 0 && Helper.CDbl(dt.Rows[0]["loan_approve"]) > Helper.CDbl(dt.Rows[0]["loan_return"]))
                            {
                                strApprove_head_status = "S";
                            }
                            else if (Helper.CDbl(dt.Rows[0]["loan_return"]) > 0 && Helper.CDbl(dt.Rows[0]["loan_return"]) >= Helper.CDbl(dt.Rows[0]["loan_approve"]))
                            {
                                strApprove_head_status = "F";
                            }
                        }
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

                strSql = "Update ef_loan_head  Set " +
                                "loan_status = '" + strApprove_head_status + "', " +
                                "d_updated_date = '" + cCommon.GetDateTimeNow() + "', " +
                                "c_updated_by = '" + pc_updated_by + "' " +
                                "Where loan_id = " + ploan_id + " and loan_status not in ('W','C')";

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

        #region SP_LOAN_HEAD_SUM_UPD
        public bool SP_LOAN_HEAD_SUM_UPD(
           long ploan_id,
           string pc_updated_by)
        {
            bool blnResult = false;
            var oConn = new SqlConnection();
            var oCommand = new SqlCommand();
            try
            {
                string strSql = "Update ef_loan_head  Set " +
                                "loan_req = (select sum(loan_detail_amount) from ef_loan_detail where loan_id = " + ploan_id + "), " +
                                "loan_approve = (select sum(loan_detail_amount) from ef_loan_detail where loan_id = " + ploan_id + "), " +
                                "d_updated_date = '" + cCommon.GetDateTimeNow() + "', " +
                                "c_updated_by = '" + pc_updated_by + "' " +
                                "Where loan_id = " + ploan_id;

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


        #region SP_LOAN_DETAIL_ALL_DEL
        public bool SP_LOAN_DETAIL_ALL_DEL(int ploanId)
        {
            bool blnResult = false;
            var oConn = new SqlConnection();
            var oCommand = new SqlCommand();
            try
            {
                string strSql = "Delete from ef_loan_detail " +
                                "Where loan_id = " + ploanId;
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

        #region SP_LOAN_DETAIL_SEL
        public DataTable SP_LOAN_DETAIL_SEL(string strCriteria)
        {
            var oConn = new SqlConnection();
            var oCommand = new SqlCommand();
            var dt = new DataTable();
            strCriteria += strCriteria.ToLower().Contains("order by") ? string.Empty : " Order by loan_detail_id";
            try
            {
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = "Select * from view_ef_loan_detail where 1=1 " + strCriteria;
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

        #region SP_LOAN_DETAIL_APPROVE_SEL
        public DataTable SP_LOAN_DETAIL_APPROVE_SEL(string strCriteria)
        {
            var oConn = new SqlConnection();
            var oCommand = new SqlCommand();
            var dt = new DataTable();
            strCriteria += strCriteria.ToLower().Contains("order by") ? string.Empty : " Order by loan_detail_approve_id";
            try
            {
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = "Select * from view_ef_loan_detail_approve where 1=1 " + strCriteria;
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

        #region SP_LOAN_DETAIL_INS
        public bool SP_LOAN_DETAIL_INS(
            long ploan_id,
            int pmaterial_id,
            string pmaterial_name,
            string pmaterial_detail,
            string ploan_detail_remark,
            double ploan_detail_amount)
        {
            bool blnResult = false;
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            try
            {
                string strSql = "Insert into [ef_loan_detail]([loan_id],[material_id],[material_name]," +
                                "[material_detail],[loan_detail_remark],[loan_detail_amount]) values ( " +
                                "" + ploan_id + "," +
                                "" + pmaterial_id + "," +
                                "'" + pmaterial_name + "'," +
                                "'" + pmaterial_detail + "'," +
                                "'" + ploan_detail_remark + "'," +
                                "" + ploan_detail_amount + ")";
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

        #region SP_LOAN_DETAIL_UPD
        public bool SP_LOAN_DETAIL_UPD(
            long poloan_detail_id,
            int pmaterial_id,
            string pmaterial_name,
            string pmaterial_detail,
            string ploan_detail_remark,
            double ploan_detail_amount)
        {
            bool blnResult = false;
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            try
            {
                string strSql = "Update [ef_loan_detail] Set " +
                                "material_id = " + pmaterial_id + "," +
                                "material_name = '" + pmaterial_name + "'," +
                                "material_detail = '" + pmaterial_detail + "'," +
                                "loan_detail_remark = '" + ploan_detail_remark + "'," +
                                "loan_detail_amount = " + ploan_detail_amount +
                                " Where loan_detail_id = " + poloan_detail_id;

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

        #region SP_LOAN_DETAIL_DEL
        public bool SP_LOAN_DETAIL_DEL(long ploan_detail_id)
        {
            bool blnResult = false;
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            try
            {
                string strSql = "Delete from ef_loan_detail " +
                                "Where loan_detail_id = " + ploan_detail_id;
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


        #region SP_LOAN_APPROVE_ALL_DEL
        public bool SP_LOAN_APPROVE_ALL_DEL(int ploanId)
        {
            bool blnResult = false;
            var oConn = new SqlConnection();
            var oCommand = new SqlCommand();
            try
            {
                string strSql = "Delete from ef_loan_detail_approve " +
                                "Where loan_id = " + ploanId;
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

        #region SP_LOAN_DETAIL_APPROVE_DEL
        public bool SP_LOAN_DETAIL_APPROVE_DEL(int ploan_detail_approve_id)
        {
            bool blnResult = false;
            var oConn = new SqlConnection();
            var oCommand = new SqlCommand();
            try
            {
                string strSql = "Delete from ef_loan_detail_approve " +
                                "Where loan_detail_approve_id = " + ploan_detail_approve_id;
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

        #region SP_LOAN_DETAIL_APPROVE_INS
        public bool SP_LOAN_DETAIL_APPROVE_INS(
            long ploan_id,
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
                string strSql = "Insert into [ef_loan_detail_approve] ([loan_id],[approve_code],[approve_name],[approve_level]," +
                     "[person_code],[person_manage_code],[person_manage_name],[approve_remark],[approve_status],[c_created_by],[d_created_date]) values ( " +
                                "" + ploan_id + "," +
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

        #region SP_LOAN_DETAIL_APPROVE_UPD
        public bool SP_LOAN_DETAIL_APPROVE_UPD(
            long ploan_detail_approve_id,
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
            var oConn = new SqlConnection();
            var oCommand = new SqlCommand();
            try
            {
                string strSql = "Update ef_loan_detail_approve Set " +
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
                                "Where loan_detail_approve_id = " + ploan_detail_approve_id;
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

        #region SP_LOAN_DETAIL_APPROVE_EDIT
        public bool SP_LOAN_DETAIL_APPROVE_EDIT(
            int ploan_detail_approve_id,
            string papprove_note,
            string papprove_status,
            string pc_updated_by)
        {
            bool blnResult = false;
            var oConn = new SqlConnection();
            var oCommand = new SqlCommand();
            var oAdapter = new SqlDataAdapter();
            try
            {
                string strSql = "Update ef_loan_detail_approve Set " +
                                "approve_note ='" + papprove_note + "'," +
                                "approve_status ='" + papprove_status + "', " +
                                "c_updated_by ='" + pc_updated_by + "', " +
                                "d_approved_date ='" + cCommon.GetDateTimeNow() + "' " +
                                "Where loan_detail_approve_id = " + ploan_detail_approve_id;
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

        #region SP_LOAN_ATTACH_SEL
        public DataTable SP_LOAN_ATTACH_SEL(string strCriteria)
        {
            var oConn = new SqlConnection();
            var oCommand = new SqlCommand();
            var dt = new DataTable();
            strCriteria += strCriteria.ToLower().Contains("order by") ? string.Empty : " Order by loan_attach_id";
            try
            {
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = "Select * from view_ef_loan_attach where 1=1 " + strCriteria;
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

        #region SP_LOAN_ATTACH_INS
        public bool SP_LOAN_ATTACH_INS(
            int ploan_id,
            string ploan_attach_des,
            string loan_attach_file_name,
            string pc_created_by)
        {
            bool blnResult = false;
            var oConn = new SqlConnection();
            var oCommand = new SqlCommand();
            try
            {
                string strSql = "Insert into ef_loan_attach ([loan_id],[loan_attach_des],[loan_attach_file_name],[loan_attach_date],[c_created_by],[d_created_date]) values ( " +
                                "" + ploan_id + "," +
                                "'" + ploan_attach_des + "'," +
                                "'" + loan_attach_file_name + "'," +
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

        #region SP_LOAN_ATTACH_UPD
        public bool SP_LOAN_ATTACH_UPD(
            long ploan_attach_id,
            int ploan_id,
            string ploan_attach_des,
            string pc_updated_by)
        {
            bool blnResult = false;
            var oConn = new SqlConnection();
            var oCommand = new SqlCommand();
            try
            {
                string strSql = "Update ef_loan_attach set " +
                                "loan_id = " + ploan_id + "," +
                                "loan_attach_des = '" + ploan_attach_des + "'," +
                                "loan_attach_date = '" + cCommon.GetDateTimeNow() + "'," +
                                "c_updated_by = '" + pc_updated_by + "'," +
                                "d_created_date = '" + cCommon.GetDateTimeNow() + "'" +
                                " Where loan_attach_id = " + ploan_attach_id;
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

        #region SP_LOAN_ATTACH_DEL
        public bool SP_LOAN_ATTACH_DEL(
            long ploan_attach_id)
        {
            bool blnResult = false;
            var oConn = new SqlConnection();
            var oCommand = new SqlCommand();
            try
            {
                string strSql = "Delete from ef_loan_attach " +
                                " Where loan_attach_id = " + ploan_attach_id;
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

        #region SP_EF_LOAN_PRINT_01_SEL
        public bool SP_EF_LOAN_PRINT_01_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_EF_LOAN_PRINT_01_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_EF_LOAN_PRINT_01_SEL");
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

        #region SP_EF_LOAN_DETAIL_APPROVE_SEL
        public bool SP_EF_LOAN_DETAIL_APPROVE_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_EF_LOAN_DETAIL_APPROVE_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_EF_LOAN_DETAIL_APPROVE_SEL");
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
