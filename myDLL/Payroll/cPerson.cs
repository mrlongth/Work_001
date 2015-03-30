using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace myDLL
{
    public class cPerson : IDisposable
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

        public cPerson()
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
        #region SP_PERSON_ALL_SEL
        public bool SP_PERSON_ALL_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_PERSON_ALL_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_PERSON_ALL_SEL");
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

        #region SP_PERSON_LIST_SEL
        public bool SP_PERSON_LIST_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_PERSON_LIST_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_PERSON_LIST_SEL");
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

        #region SP_PERSON_HIS_INS
        public bool SP_PERSON_HIS_INS(string ptitle_code, string pperson_thai_name, string pperson_thai_surname, string pperson_eng_name,
                                                                                string pperson_eng_surname, string pperson_nickname, string pperson_id, string pperson_pic,
                                                                                string pActive, string pC_created_by, ref string strMessage)
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
                oCommand.CommandText = "sp_PERSON_HIS_INS";
                // - - - - - - - - - - - -             
                SqlParameter oParam_title_code = new SqlParameter("title_code", SqlDbType.NVarChar);
                oParam_title_code.Direction = ParameterDirection.Input;
                oParam_title_code.Value = ptitle_code;
                oCommand.Parameters.Add(oParam_title_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_thai_name = new SqlParameter("person_thai_name", SqlDbType.NVarChar);
                oParam_person_thai_name.Direction = ParameterDirection.Input;
                oParam_person_thai_name.Value = pperson_thai_name;
                oCommand.Parameters.Add(oParam_person_thai_name);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_thai_surname = new SqlParameter("person_thai_surname", SqlDbType.NVarChar);
                oParam_person_thai_surname.Direction = ParameterDirection.Input;
                oParam_person_thai_surname.Value = pperson_thai_surname;
                oCommand.Parameters.Add(oParam_person_thai_surname);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_eng_name = new SqlParameter("person_eng_name", SqlDbType.NVarChar);
                oParam_person_eng_name.Direction = ParameterDirection.Input;
                oParam_person_eng_name.Value = pperson_eng_name;
                oCommand.Parameters.Add(oParam_person_eng_name);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_eng_surname = new SqlParameter("person_eng_surname", SqlDbType.NVarChar);
                oParam_person_eng_surname.Direction = ParameterDirection.Input;
                oParam_person_eng_surname.Value = pperson_eng_surname;
                oCommand.Parameters.Add(oParam_person_eng_surname);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_nickname = new SqlParameter("person_nickname", SqlDbType.NVarChar);
                oParam_person_nickname.Direction = ParameterDirection.Input;
                oParam_person_nickname.Value = pperson_nickname;
                oCommand.Parameters.Add(oParam_person_nickname);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_id = new SqlParameter("person_id", SqlDbType.NVarChar);
                oParam_person_id.Direction = ParameterDirection.Input;
                oParam_person_id.Value = pperson_id;
                oCommand.Parameters.Add(oParam_person_id);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_pic = new SqlParameter("person_pic", SqlDbType.NVarChar);
                oParam_person_pic.Direction = ParameterDirection.Input;
                oParam_person_pic.Value = pperson_pic;
                oCommand.Parameters.Add(oParam_person_pic);
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

        #region SP_PERSON_HIS_UPD
        public bool SP_PERSON_HIS_UPD(string pperson_code, string ptitle_code, string pperson_thai_name, string pperson_thai_surname, string pperson_eng_name,
                                                                                string pperson_eng_surname, string pperson_nickname, string pperson_id, string pperson_pic,
                                                                                string pActive, string pC_updated_by, ref string strMessage)
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
                oCommand.CommandText = "sp_PERSON_HIS_UPD";
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_code = new SqlParameter("person_code", SqlDbType.NVarChar);
                oParam_person_code.Direction = ParameterDirection.Input;
                oParam_person_code.Value = pperson_code;
                oCommand.Parameters.Add(oParam_person_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_title_code = new SqlParameter("title_code", SqlDbType.NVarChar);
                oParam_title_code.Direction = ParameterDirection.Input;
                oParam_title_code.Value = ptitle_code;
                oCommand.Parameters.Add(oParam_title_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_thai_name = new SqlParameter("person_thai_name", SqlDbType.NVarChar);
                oParam_person_thai_name.Direction = ParameterDirection.Input;
                oParam_person_thai_name.Value = pperson_thai_name;
                oCommand.Parameters.Add(oParam_person_thai_name);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_thai_surname = new SqlParameter("person_thai_surname", SqlDbType.NVarChar);
                oParam_person_thai_surname.Direction = ParameterDirection.Input;
                oParam_person_thai_surname.Value = pperson_thai_surname;
                oCommand.Parameters.Add(oParam_person_thai_surname);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_eng_name = new SqlParameter("person_eng_name", SqlDbType.NVarChar);
                oParam_person_eng_name.Direction = ParameterDirection.Input;
                oParam_person_eng_name.Value = pperson_eng_name;
                oCommand.Parameters.Add(oParam_person_eng_name);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_eng_surname = new SqlParameter("person_eng_surname", SqlDbType.NVarChar);
                oParam_person_eng_surname.Direction = ParameterDirection.Input;
                oParam_person_eng_surname.Value = pperson_eng_surname;
                oCommand.Parameters.Add(oParam_person_eng_surname);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_nickname = new SqlParameter("person_nickname", SqlDbType.NVarChar);
                oParam_person_nickname.Direction = ParameterDirection.Input;
                oParam_person_nickname.Value = pperson_nickname;
                oCommand.Parameters.Add(oParam_person_nickname);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_id = new SqlParameter("person_id", SqlDbType.NVarChar);
                oParam_person_id.Direction = ParameterDirection.Input;
                oParam_person_id.Value = pperson_id;
                oCommand.Parameters.Add(oParam_person_id);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_pic = new SqlParameter("person_pic", SqlDbType.NVarChar);
                oParam_person_pic.Direction = ParameterDirection.Input;
                oParam_person_pic.Value = pperson_pic;
                oCommand.Parameters.Add(oParam_person_pic);
                // - - - - - - - - - - - -             
                SqlParameter oParam_Active = new SqlParameter("c_active", SqlDbType.NVarChar);
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

        #region SP_PERSON_PIC_UPD
        public bool SP_PERSON_PIC_UPD(string pperson_code, string pperson_pic, ref string strMessage)
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
                oCommand.CommandText = "sp_PERSON_PIC_UPD";
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_code = new SqlParameter("person_code", SqlDbType.NVarChar);
                oParam_person_code.Direction = ParameterDirection.Input;
                oParam_person_code.Value = pperson_code;
                oCommand.Parameters.Add(oParam_person_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_pic = new SqlParameter("person_pic", SqlDbType.NVarChar);
                oParam_person_pic.Direction = ParameterDirection.Input;
                oParam_person_pic.Value = pperson_pic;
                oCommand.Parameters.Add(oParam_person_pic);
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

        #region SP_PERSON_WORK_UPD
        public bool SP_PERSON_WORK_UPD(string pperson_code, string pposition_code, string pperson_level,
                                        string pperson_postionno, string pbranch_code, string pbank_no,
                                        string pperson_salaly, string pperson_start, string pperson_end, string pperson_group_code, string pmember_type_code,
                                        string pmember_type_add, string pperson_manage_code, string pbudget_plan_code, string pperson_work_status_code,
                                        string pc_updated_by, string ptype_position_code, string pbranch_code_2, string pbank_no_2, ref string strMessage)
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
                oCommand.CommandText = "sp_PERSON_WORK_UPD";
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_code = new SqlParameter("person_code", SqlDbType.NVarChar);
                oParam_person_code.Direction = ParameterDirection.Input;
                oParam_person_code.Value = pperson_code;
                oCommand.Parameters.Add(oParam_person_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_position_code = new SqlParameter("position_code", SqlDbType.NVarChar);
                oParam_position_code.Direction = ParameterDirection.Input;
                oParam_position_code.Value = pposition_code;
                oCommand.Parameters.Add(oParam_position_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_level = new SqlParameter("person_level", SqlDbType.NVarChar);
                oParam_person_level.Direction = ParameterDirection.Input;
                oParam_person_level.Value = pperson_level;
                oCommand.Parameters.Add(oParam_person_level);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_postionno = new SqlParameter("person_postionno", SqlDbType.NVarChar);
                oParam_person_postionno.Direction = ParameterDirection.Input;
                oParam_person_postionno.Value = pperson_postionno;
                oCommand.Parameters.Add(oParam_person_postionno);
                // - - - - - - - - - - - -             
                SqlParameter oParam_branch_code = new SqlParameter("branch_code", SqlDbType.NVarChar);
                oParam_branch_code.Direction = ParameterDirection.Input;
                oParam_branch_code.Value = pbranch_code;
                oCommand.Parameters.Add(oParam_branch_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_bank_no = new SqlParameter("bank_no", SqlDbType.NVarChar);
                oParam_bank_no.Direction = ParameterDirection.Input;
                oParam_bank_no.Value = pbank_no;
                oCommand.Parameters.Add(oParam_bank_no);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_salaly = new SqlParameter("person_salaly", SqlDbType.Money);
                oParam_person_salaly.Direction = ParameterDirection.Input;
                oParam_person_salaly.Value = float.Parse(pperson_salaly);
                oCommand.Parameters.Add(oParam_person_salaly);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_start = new SqlParameter("person_start", SqlDbType.DateTime);
                oParam_person_start.Direction = ParameterDirection.Input;
                oParam_person_start.Value = cCommon.CheckDate(pperson_start);
                oCommand.Parameters.Add(oParam_person_start);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_end = new SqlParameter("person_end", SqlDbType.DateTime);
                oParam_person_end.Direction = ParameterDirection.Input;
                oParam_person_end.Value = cCommon.CheckDate(pperson_end);
                oCommand.Parameters.Add(oParam_person_end);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_group_code = new SqlParameter("person_group_code", SqlDbType.NVarChar);
                oParam_person_group_code.Direction = ParameterDirection.Input;
                oParam_person_group_code.Value = pperson_group_code;
                oCommand.Parameters.Add(oParam_person_group_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_member_type_code = new SqlParameter("member_type_code", SqlDbType.NVarChar);
                oParam_member_type_code.Direction = ParameterDirection.Input;
                oParam_member_type_code.Value = pmember_type_code;
                oCommand.Parameters.Add(oParam_member_type_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_member_type_add = new SqlParameter("member_type_add", SqlDbType.Money);
                oParam_member_type_add.Direction = ParameterDirection.Input;
                oParam_member_type_add.Value = float.Parse(pmember_type_add);
                oCommand.Parameters.Add(oParam_member_type_add);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_manage_code = new SqlParameter("person_manage_code", SqlDbType.NVarChar);
                oParam_person_manage_code.Direction = ParameterDirection.Input;
                oParam_person_manage_code.Value = pperson_manage_code;
                oCommand.Parameters.Add(oParam_person_manage_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_budget_plan_code = new SqlParameter("budget_plan_code", SqlDbType.NVarChar);
                oParam_budget_plan_code.Direction = ParameterDirection.Input;
                oParam_budget_plan_code.Value = pbudget_plan_code;
                oCommand.Parameters.Add(oParam_budget_plan_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_work_status_code = new SqlParameter("person_work_status_code", SqlDbType.NVarChar);
                oParam_person_work_status_code.Direction = ParameterDirection.Input;
                oParam_person_work_status_code.Value = pperson_work_status_code;
                oCommand.Parameters.Add(oParam_person_work_status_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_c_updated_by = new SqlParameter("c_updated_by", SqlDbType.NVarChar);
                oParam_c_updated_by.Direction = ParameterDirection.Input;
                oParam_c_updated_by.Value = pc_updated_by;
                oCommand.Parameters.Add(oParam_c_updated_by);

                // - - - - - - - - - - - -             
                SqlParameter oParam_type_position_code = new SqlParameter("type_position_code", SqlDbType.NVarChar);
                oParam_type_position_code.Direction = ParameterDirection.Input;
                oParam_type_position_code.Value = ptype_position_code;
                oCommand.Parameters.Add(oParam_type_position_code);


                // - - - - - - - - - - - -             
                SqlParameter oParam_branch_code_2 = new SqlParameter("branch_code_2", SqlDbType.NVarChar);
                oParam_branch_code_2.Direction = ParameterDirection.Input;
                oParam_branch_code_2.Value = pbranch_code_2;
                oCommand.Parameters.Add(oParam_branch_code_2);
                // - - - - - - - - - - - -             
                SqlParameter oParam_bank_no_2 = new SqlParameter("bank_no_2", SqlDbType.NVarChar);
                oParam_bank_no_2.Direction = ParameterDirection.Input;
                oParam_bank_no_2.Value = pbank_no_2;
                oCommand.Parameters.Add(oParam_bank_no_2);

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

        #region SP_PERSON_STATUS_UPD
        public bool SP_PERSON_STATUS_UPD(string pperson_code, string pperson_sex, string pperson_high, string pperson_width, string pperson_origin, string pperson_nation,
                                                                                        string pperson_religion, string pperson_birth, string pperson_marry, string pc_updated_by, ref string strMessage)
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
                oCommand.CommandText = "sp_PERSON_STATUS_UPD";
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_code = new SqlParameter("person_code", SqlDbType.NVarChar);
                oParam_person_code.Direction = ParameterDirection.Input;
                oParam_person_code.Value = pperson_code;
                oCommand.Parameters.Add(oParam_person_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_sex = new SqlParameter("person_sex", SqlDbType.NVarChar);
                oParam_person_sex.Direction = ParameterDirection.Input;
                oParam_person_sex.Value = pperson_sex;
                oCommand.Parameters.Add(oParam_person_sex);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_high = new SqlParameter("person_high", SqlDbType.NVarChar);
                oParam_person_high.Direction = ParameterDirection.Input;
                oParam_person_high.Value = pperson_high;
                oCommand.Parameters.Add(oParam_person_high);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_width = new SqlParameter("person_width", SqlDbType.NVarChar);
                oParam_person_width.Direction = ParameterDirection.Input;
                oParam_person_width.Value = pperson_width;
                oCommand.Parameters.Add(oParam_person_width);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_origin = new SqlParameter("person_origin", SqlDbType.NVarChar);
                oParam_person_origin.Direction = ParameterDirection.Input;
                oParam_person_origin.Value = pperson_origin;
                oCommand.Parameters.Add(oParam_person_origin);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_nation = new SqlParameter("person_nation", SqlDbType.NVarChar);
                oParam_person_nation.Direction = ParameterDirection.Input;
                oParam_person_nation.Value = pperson_nation;
                oCommand.Parameters.Add(oParam_person_nation);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_religion = new SqlParameter("person_religion", SqlDbType.NVarChar);
                oParam_person_religion.Direction = ParameterDirection.Input;
                oParam_person_religion.Value = pperson_religion;
                oCommand.Parameters.Add(oParam_person_religion);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_birth = new SqlParameter("person_birth", SqlDbType.DateTime);
                oParam_person_birth.Direction = ParameterDirection.Input;
                oParam_person_birth.Value = cCommon.CheckDate(pperson_birth);
                oCommand.Parameters.Add(oParam_person_birth);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_marry = new SqlParameter("person_marry", SqlDbType.NVarChar);
                oParam_person_marry.Direction = ParameterDirection.Input;
                oParam_person_marry.Value = pperson_marry;
                oCommand.Parameters.Add(oParam_person_marry);
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

        #region SP_PERSON_ADDRESS_UPD
        public bool SP_PERSON_ADDRESS_UPD(string pperson_code, string pperson_room, string pperson_floor, string pperson_village, string pperson_homeno, string pperson_soi,
                                                                                                string pperson_moo, string pperson_road, string pperson_tambol, string pperson_aumphur, string pperson_province, string pperson_postno,
                                                                                                string pperson_tel, string pperson_contact, string pperson_ralation, string pperson_contact_tel, string pc_updated_by, ref string strMessage)
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
                oCommand.CommandText = "sp_PERSON_ADDRESS_UPD";
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_code = new SqlParameter("person_code", SqlDbType.NVarChar);
                oParam_person_code.Direction = ParameterDirection.Input;
                oParam_person_code.Value = pperson_code;
                oCommand.Parameters.Add(oParam_person_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_room = new SqlParameter("person_room", SqlDbType.NVarChar);
                oParam_person_room.Direction = ParameterDirection.Input;
                oParam_person_room.Value = pperson_room;
                oCommand.Parameters.Add(oParam_person_room);

                // - - - - - - - - - - - -             
                SqlParameter oParam_person_floor = new SqlParameter("person_floor", SqlDbType.NVarChar);
                oParam_person_floor.Direction = ParameterDirection.Input;
                oParam_person_floor.Value = pperson_floor;
                oCommand.Parameters.Add(oParam_person_floor);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_village = new SqlParameter("person_village", SqlDbType.NVarChar);
                oParam_person_village.Direction = ParameterDirection.Input;
                oParam_person_village.Value = pperson_village;
                oCommand.Parameters.Add(oParam_person_village);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_homeno = new SqlParameter("person_homeno", SqlDbType.NVarChar);
                oParam_person_homeno.Direction = ParameterDirection.Input;
                oParam_person_homeno.Value = pperson_homeno;
                oCommand.Parameters.Add(oParam_person_homeno);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_soi = new SqlParameter("person_soi", SqlDbType.NVarChar);
                oParam_person_soi.Direction = ParameterDirection.Input;
                oParam_person_soi.Value = pperson_soi;
                oCommand.Parameters.Add(oParam_person_soi);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_moo = new SqlParameter("person_moo", SqlDbType.NVarChar);
                oParam_person_moo.Direction = ParameterDirection.Input;
                oParam_person_moo.Value = pperson_moo;
                oCommand.Parameters.Add(oParam_person_moo);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_road = new SqlParameter("person_road", SqlDbType.NVarChar);
                oParam_person_road.Direction = ParameterDirection.Input;
                oParam_person_road.Value = pperson_road;
                oCommand.Parameters.Add(oParam_person_road);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_tambol = new SqlParameter("person_tambol", SqlDbType.NVarChar);
                oParam_person_tambol.Direction = ParameterDirection.Input;
                oParam_person_tambol.Value = pperson_tambol;
                oCommand.Parameters.Add(oParam_person_tambol);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_aumphur = new SqlParameter("person_aumphur", SqlDbType.NVarChar);
                oParam_person_aumphur.Direction = ParameterDirection.Input;
                oParam_person_aumphur.Value = pperson_aumphur;
                oCommand.Parameters.Add(oParam_person_aumphur);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_province = new SqlParameter("person_province", SqlDbType.NVarChar);
                oParam_person_province.Direction = ParameterDirection.Input;
                oParam_person_province.Value = pperson_province;
                oCommand.Parameters.Add(oParam_person_province);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_postno = new SqlParameter("person_postno", SqlDbType.NVarChar);
                oParam_person_postno.Direction = ParameterDirection.Input;
                oParam_person_postno.Value = pperson_postno;
                oCommand.Parameters.Add(oParam_person_postno);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_tel = new SqlParameter("person_tel", SqlDbType.NVarChar);
                oParam_person_tel.Direction = ParameterDirection.Input;
                oParam_person_tel.Value = pperson_tel;
                oCommand.Parameters.Add(oParam_person_tel);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_contact = new SqlParameter("person_contact", SqlDbType.NVarChar);
                oParam_person_contact.Direction = ParameterDirection.Input;
                oParam_person_contact.Value = pperson_contact;
                oCommand.Parameters.Add(oParam_person_contact);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_ralation = new SqlParameter("person_ralation", SqlDbType.NVarChar);
                oParam_person_ralation.Direction = ParameterDirection.Input;
                oParam_person_ralation.Value = pperson_ralation;
                oCommand.Parameters.Add(oParam_person_ralation);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_contact_tel = new SqlParameter("person_contact_tel", SqlDbType.NVarChar);
                oParam_person_contact_tel.Direction = ParameterDirection.Input;
                oParam_person_contact_tel.Value = pperson_contact_tel;
                oCommand.Parameters.Add(oParam_person_contact_tel);
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

        #region SP_PERSON_DEL
        public bool SP_PERSON_DEL(string pPerson_code, string pActive, string pC_updated_by, ref string strMessage)
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
                oCommand.CommandText = "sp_PERSON_DEL";
                // - - - - - - - - - - - -             
                SqlParameter oParam_PERSON_code = new SqlParameter("Person_code", SqlDbType.NVarChar);
                oParam_PERSON_code.Direction = ParameterDirection.Input;
                oParam_PERSON_code.Value = pPerson_code;
                oCommand.Parameters.Add(oParam_PERSON_code);
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

        #region SP_PERSON_ITEM_SEL
        public bool SP_PERSON_ITEM_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_PERSON_ITEM_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_PERSON_ITEM_SEL");
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

        #region SP_PERSON_ITEM_DEL
        public bool SP_PERSON_ITEM_DEL(string pPerson_code, string pPerson_item_year, string pPerson_item_code,
                                                                                                        string pActive, string pC_updated_by, ref string strMessage)
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
                oCommand.CommandText = "sp_PERSON_ITEM_DEL";
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_code = new SqlParameter("person_code", SqlDbType.NVarChar);
                oParam_person_code.Direction = ParameterDirection.Input;
                oParam_person_code.Value = pPerson_code;
                oCommand.Parameters.Add(oParam_person_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_item_year = new SqlParameter("person_item_year", SqlDbType.NVarChar);
                oParam_person_item_year.Direction = ParameterDirection.Input;
                oParam_person_item_year.Value = pPerson_item_year;
                oCommand.Parameters.Add(oParam_person_item_year);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_item_code = new SqlParameter("item_code", SqlDbType.NVarChar);
                oParam_person_item_code.Direction = ParameterDirection.Input;
                oParam_person_item_code.Value = pPerson_item_code;
                oCommand.Parameters.Add(oParam_person_item_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_Active = new SqlParameter("c_active", SqlDbType.NVarChar);
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

        #region SP_PERSON_ITEM_INS
        public bool SP_PERSON_ITEM_INS(string pPerson_code, string pPerson_item_year, string pPerson_item_code,
                      string pitem_debit, string pitem_credit, string pperson_item_tax, string pperson_item_sos,
                      string pActive, string pC_created_by, string pbudget_plan_code, string pperson_item_lot_code,
                      string pbudget_type, ref string strMessage)
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
                oCommand.CommandText = "sp_PERSON_ITEM_INS";
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_code = new SqlParameter("person_code", SqlDbType.NVarChar);
                oParam_person_code.Direction = ParameterDirection.Input;
                oParam_person_code.Value = pPerson_code;
                oCommand.Parameters.Add(oParam_person_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_item_year = new SqlParameter("person_item_year", SqlDbType.NVarChar);
                oParam_person_item_year.Direction = ParameterDirection.Input;
                oParam_person_item_year.Value = pPerson_item_year;
                oCommand.Parameters.Add(oParam_person_item_year);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_item_code = new SqlParameter("item_code", SqlDbType.NVarChar);
                oParam_person_item_code.Direction = ParameterDirection.Input;
                oParam_person_item_code.Value = pPerson_item_code;
                oCommand.Parameters.Add(oParam_person_item_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_item_debit = new SqlParameter("item_debit", SqlDbType.Float);
                oParam_item_debit.Direction = ParameterDirection.Input;
                oParam_item_debit.Value = float.Parse(pitem_debit);
                oCommand.Parameters.Add(oParam_item_debit);
                // - - - - - - - - - - - -             
                SqlParameter oParam_item_credit = new SqlParameter("item_credit", SqlDbType.Float);
                oParam_item_credit.Direction = ParameterDirection.Input;
                oParam_item_credit.Value = float.Parse(pitem_credit);
                oCommand.Parameters.Add(oParam_item_credit);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_item_tax = new SqlParameter("person_item_tax", SqlDbType.NVarChar);
                oParam_person_item_tax.Direction = ParameterDirection.Input;
                oParam_person_item_tax.Value = pperson_item_tax;
                oCommand.Parameters.Add(oParam_person_item_tax);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_item_sos = new SqlParameter("person_item_sos", SqlDbType.NVarChar);
                oParam_person_item_sos.Direction = ParameterDirection.Input;
                oParam_person_item_sos.Value = pperson_item_sos;
                oCommand.Parameters.Add(oParam_person_item_sos);
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


                SqlParameter oParam_c_budget_type = new SqlParameter("budget_plan_code", SqlDbType.NVarChar);
                oParam_c_budget_type.Direction = ParameterDirection.Input;
                oParam_c_budget_type.Value = pbudget_plan_code;
                oCommand.Parameters.Add(oParam_c_budget_type);

                SqlParameter oParam_person_item_lot_code = new SqlParameter("person_item_lot_code", SqlDbType.NVarChar);
                oParam_person_item_lot_code.Direction = ParameterDirection.Input;
                oParam_person_item_lot_code.Value = pperson_item_lot_code;
                oCommand.Parameters.Add(oParam_person_item_lot_code);

                SqlParameter oParam_pbudget_type = new SqlParameter("budget_type", SqlDbType.NVarChar);
                oParam_pbudget_type.Direction = ParameterDirection.Input;
                oParam_pbudget_type.Value = pbudget_type;
                oCommand.Parameters.Add(oParam_pbudget_type);


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

        #region SP_PERSON_ITEM_UPD
        public bool SP_PERSON_ITEM_UPD(string pPerson_code, string pPerson_item_year, string pPerson_item_code,
                      string pitem_debit, string pitem_credit, string pperson_item_tax, string pperson_item_sos,
                      string pActive, string pC_updated_by, string pbudget_plan_code, string pperson_item_lot_code,
                     string pbudget_type, ref string strMessage)
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
                oCommand.CommandText = "sp_PERSON_ITEM_UPD";
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_code = new SqlParameter("person_code", SqlDbType.NVarChar);
                oParam_person_code.Direction = ParameterDirection.Input;
                oParam_person_code.Value = pPerson_code;
                oCommand.Parameters.Add(oParam_person_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_item_year = new SqlParameter("person_item_year", SqlDbType.NVarChar);
                oParam_person_item_year.Direction = ParameterDirection.Input;
                oParam_person_item_year.Value = pPerson_item_year;
                oCommand.Parameters.Add(oParam_person_item_year);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_item_code = new SqlParameter("item_code", SqlDbType.NVarChar);
                oParam_person_item_code.Direction = ParameterDirection.Input;
                oParam_person_item_code.Value = pPerson_item_code;
                oCommand.Parameters.Add(oParam_person_item_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_item_debit = new SqlParameter("item_debit", SqlDbType.Float);
                oParam_item_debit.Direction = ParameterDirection.Input;
                oParam_item_debit.Value = float.Parse(pitem_debit);
                oCommand.Parameters.Add(oParam_item_debit);
                // - - - - - - - - - - - -             
                SqlParameter oParam_item_credit = new SqlParameter("item_credit", SqlDbType.Float);
                oParam_item_credit.Direction = ParameterDirection.Input;
                oParam_item_credit.Value = float.Parse(pitem_credit);
                oCommand.Parameters.Add(oParam_item_credit);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_item_tax = new SqlParameter("person_item_tax", SqlDbType.NVarChar);
                oParam_person_item_tax.Direction = ParameterDirection.Input;
                oParam_person_item_tax.Value = pperson_item_tax;
                oCommand.Parameters.Add(oParam_person_item_tax);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_item_sos = new SqlParameter("person_item_sos", SqlDbType.NVarChar);
                oParam_person_item_sos.Direction = ParameterDirection.Input;
                oParam_person_item_sos.Value = pperson_item_sos;
                oCommand.Parameters.Add(oParam_person_item_sos);
                // - - - - - - - - - - - -             
                SqlParameter oParam_Active = new SqlParameter("c_active", SqlDbType.NVarChar);
                oParam_Active.Direction = ParameterDirection.Input;
                oParam_Active.Value = pActive;
                oCommand.Parameters.Add(oParam_Active);
                // - - - - - - - - - - - -             
                SqlParameter oParam_c_updated_by = new SqlParameter("c_updated_by", SqlDbType.NVarChar);
                oParam_c_updated_by.Direction = ParameterDirection.Input;
                oParam_c_updated_by.Value = pC_updated_by;
                oCommand.Parameters.Add(oParam_c_updated_by);
                // - - - - - - - - - - - -      

                SqlParameter oParam_c_budget_type = new SqlParameter("budget_plan_code", SqlDbType.NVarChar);
                oParam_c_budget_type.Direction = ParameterDirection.Input;
                oParam_c_budget_type.Value = pbudget_plan_code;
                oCommand.Parameters.Add(oParam_c_budget_type);

                SqlParameter oParam_person_item_lot_code = new SqlParameter("person_item_lot_code", SqlDbType.NVarChar);
                oParam_person_item_lot_code.Direction = ParameterDirection.Input;
                oParam_person_item_lot_code.Value = pperson_item_lot_code;
                oCommand.Parameters.Add(oParam_person_item_lot_code);

                SqlParameter oParam_pbudget_type = new SqlParameter("budget_type", SqlDbType.NVarChar);
                oParam_pbudget_type.Direction = ParameterDirection.Input;
                oParam_pbudget_type.Value = pbudget_type;
                oCommand.Parameters.Add(oParam_pbudget_type);

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

        #region SP_PERSON_MEMBER_SEL
        public bool SP_PERSON_MEMBER_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_PERSON_MEMBER_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_PERSON_MEMBER_SEL");
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

        #region SP_PERSON_MEMBER_DEL
        public bool SP_PERSON_MEMBER_DEL(string pPerson_code, string pPerson_member_code,
                                                                                                        string pActive, string pC_updated_by, ref string strMessage)
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
                oCommand.CommandText = "sp_PERSON_MEMBER_DEL";
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_code = new SqlParameter("person_code", SqlDbType.NVarChar);
                oParam_person_code.Direction = ParameterDirection.Input;
                oParam_person_code.Value = pPerson_code;
                oCommand.Parameters.Add(oParam_person_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_member_code = new SqlParameter("member_code", SqlDbType.NVarChar);
                oParam_person_member_code.Direction = ParameterDirection.Input;
                oParam_person_member_code.Value = pPerson_member_code;
                oCommand.Parameters.Add(oParam_person_member_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_Active = new SqlParameter("c_active", SqlDbType.NVarChar);
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

        #region SP_PERSON_MEMBER_INS
        public bool SP_PERSON_MEMBER_INS(string pPerson_code, string pMember_code,
                                                                                   string pMember_quan, string pActive, string pC_created_by, ref string strMessage)
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
                oCommand.CommandText = "sp_PERSON_MEMBER_INS";
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_code = new SqlParameter("person_code", SqlDbType.NVarChar);
                oParam_person_code.Direction = ParameterDirection.Input;
                oParam_person_code.Value = pPerson_code;
                oCommand.Parameters.Add(oParam_person_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_member_code = new SqlParameter("member_code", SqlDbType.NVarChar);
                oParam_member_code.Direction = ParameterDirection.Input;
                oParam_member_code.Value = pMember_code;
                oCommand.Parameters.Add(oParam_member_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_member_quan = new SqlParameter("member_quan", SqlDbType.Int);
                oParam_member_quan.Direction = ParameterDirection.Input;
                oParam_member_quan.Value = pMember_quan;
                oCommand.Parameters.Add(oParam_member_quan);
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

        #region SP_PERSON_MEMBER_UPD
        public bool SP_PERSON_MEMBER_UPD(string pPerson_code, string pMember_code,
                                                                                   string pMember_quan, string pActive, string pC_update_by, ref string strMessage)
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
                oCommand.CommandText = "sp_PERSON_MEMBER_UPD";
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_code = new SqlParameter("person_code", SqlDbType.NVarChar);
                oParam_person_code.Direction = ParameterDirection.Input;
                oParam_person_code.Value = pPerson_code;
                oCommand.Parameters.Add(oParam_person_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_member_code = new SqlParameter("member_code", SqlDbType.NVarChar);
                oParam_member_code.Direction = ParameterDirection.Input;
                oParam_member_code.Value = pMember_code;
                oCommand.Parameters.Add(oParam_member_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_member_quan = new SqlParameter("member_quan", SqlDbType.Int);
                oParam_member_quan.Direction = ParameterDirection.Input;
                oParam_member_quan.Value = pMember_quan;
                oCommand.Parameters.Add(oParam_member_quan);
                // - - - - - - - - - - - -             
                SqlParameter oParam_Active = new SqlParameter("c_active", SqlDbType.NVarChar);
                oParam_Active.Direction = ParameterDirection.Input;
                oParam_Active.Value = pActive;
                oCommand.Parameters.Add(oParam_Active);
                // - - - - - - - - - - - -             
                SqlParameter oParam_c_updated_by = new SqlParameter("c_updated_by", SqlDbType.NVarChar);
                oParam_c_updated_by.Direction = ParameterDirection.Input;
                oParam_c_updated_by.Value = pC_update_by;
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

        #region SP_PERSON_POSITION_SEL
        public bool SP_PERSON_POSITION_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_PERSON_POSITION_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_PERSON_POSITION_SEL");
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

        #region SP_PERSON_POSITION_DEL
        public bool SP_PERSON_POSITION_DEL(string pPerson_code, string pChange_date, string pActive, string pC_updated_by, ref string strMessage)
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
                oCommand.CommandText = "sp_PERSON_POSITION_DEL";
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_code = new SqlParameter("person_code", SqlDbType.NVarChar);
                oParam_person_code.Direction = ParameterDirection.Input;
                oParam_person_code.Value = pPerson_code;
                oCommand.Parameters.Add(oParam_person_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_change_date = new SqlParameter("change_date", SqlDbType.DateTime);
                oParam_change_date.Direction = ParameterDirection.Input;
                oParam_change_date.Value = cCommon.CheckDate(pChange_date);
                oCommand.Parameters.Add(oParam_change_date);
                // - - - - - - - - - - - -             
                SqlParameter oParam_Active = new SqlParameter("c_active", SqlDbType.NVarChar);
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

        #region SP_PERSON_POSITION_INT
        public bool SP_PERSON_POSITION_INS(string pPerson_code, string pChange_date, string pSalary_old,
                                                                                            string pSalary_new, string pPosition_old, string pPosition_new,
                                                                                            string pLevel_old, string pLevel_new,
                                                                                            string pActive, string pC_created_by, ref string strMessage)
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
                oCommand.CommandText = "sp_PERSON_POSITION_INS";
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_code = new SqlParameter("person_code", SqlDbType.NVarChar);
                oParam_person_code.Direction = ParameterDirection.Input;
                oParam_person_code.Value = pPerson_code;
                oCommand.Parameters.Add(oParam_person_code);

                // - - - - - - - - - - - -             
                SqlParameter oParam_change_date = new SqlParameter("change_date", SqlDbType.DateTime);
                oParam_change_date.Direction = ParameterDirection.Input;
                oParam_change_date.Value = cCommon.CheckDate(pChange_date);
                oCommand.Parameters.Add(oParam_change_date);
                // - - - - - - - - - - - -             
                SqlParameter oParam_salary_old = new SqlParameter("salary_old", SqlDbType.Float);
                oParam_salary_old.Direction = ParameterDirection.Input;
                oParam_salary_old.Value = float.Parse(pSalary_old);
                oCommand.Parameters.Add(oParam_salary_old);
                // - - - - - - - - - - - -             
                SqlParameter oParam_salary_new = new SqlParameter("salary_new", SqlDbType.Float);
                oParam_salary_new.Direction = ParameterDirection.Input;
                oParam_salary_new.Value = float.Parse(pSalary_new);
                oCommand.Parameters.Add(oParam_salary_new);
                // - - - - - - - - - - - -  
                SqlParameter oParam_position_old = new SqlParameter("position_old", SqlDbType.VarChar);
                oParam_position_old.Direction = ParameterDirection.Input;
                oParam_position_old.Value = pPosition_old;
                oCommand.Parameters.Add(oParam_position_old);
                // - - - - - - - - - - - -             
                SqlParameter oParam_position_new = new SqlParameter("position_new", SqlDbType.VarChar);
                oParam_position_new.Direction = ParameterDirection.Input;
                oParam_position_new.Value = pPosition_new;
                oCommand.Parameters.Add(oParam_position_new);
                // - - - - - - - - - - - -  
                SqlParameter oParam_level_old = new SqlParameter("level_old", SqlDbType.VarChar);
                oParam_level_old.Direction = ParameterDirection.Input;
                oParam_level_old.Value = pLevel_old;
                oCommand.Parameters.Add(oParam_level_old);
                // - - - - - - - - - - - -             
                SqlParameter oParam_level_new = new SqlParameter("level_new", SqlDbType.VarChar);
                oParam_level_new.Direction = ParameterDirection.Input;
                oParam_level_new.Value = pLevel_new;
                oCommand.Parameters.Add(oParam_level_new);

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

        #region SP_PERSON_POSITION_UPD
        public bool SP_PERSON_POSITION_UPD(string pPerson_code, string pChange_date, string pSalary_old,
                                                                                            string pSalary_new, string pPosition_old, string pPosition_new,
                                                                                            string pLevel_old, string pLevel_new,
                                                                                            string pActive, string pC_updated_by, ref string strMessage)
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
                oCommand.CommandText = "sp_PERSON_POSITION_UPD";
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_code = new SqlParameter("person_code", SqlDbType.NVarChar);
                oParam_person_code.Direction = ParameterDirection.Input;
                oParam_person_code.Value = pPerson_code;
                oCommand.Parameters.Add(oParam_person_code);

                // - - - - - - - - - - - -             
                SqlParameter oParam_change_date = new SqlParameter("change_date", SqlDbType.DateTime);
                oParam_change_date.Direction = ParameterDirection.Input;
                oParam_change_date.Value = cCommon.CheckDate(pChange_date); ;
                oCommand.Parameters.Add(oParam_change_date);
                // - - - - - - - - - - - -             
                SqlParameter oParam_salary_old = new SqlParameter("salary_old", SqlDbType.Float);
                oParam_salary_old.Direction = ParameterDirection.Input;
                oParam_salary_old.Value = float.Parse(pSalary_old);
                oCommand.Parameters.Add(oParam_salary_old);
                // - - - - - - - - - - - -             
                SqlParameter oParam_salary_new = new SqlParameter("salary_new", SqlDbType.Float);
                oParam_salary_new.Direction = ParameterDirection.Input;
                oParam_salary_new.Value = float.Parse(pSalary_new);
                oCommand.Parameters.Add(oParam_salary_new);
                // - - - - - - - - - - - -  
                SqlParameter oParam_position_old = new SqlParameter("position_old", SqlDbType.VarChar);
                oParam_position_old.Direction = ParameterDirection.Input;
                oParam_position_old.Value = pPosition_old;
                oCommand.Parameters.Add(oParam_position_old);
                // - - - - - - - - - - - -             
                SqlParameter oParam_position_new = new SqlParameter("position_new", SqlDbType.VarChar);
                oParam_position_new.Direction = ParameterDirection.Input;
                oParam_position_new.Value = pPosition_new;
                oCommand.Parameters.Add(oParam_position_new);
                // - - - - - - - - - - - -  
                SqlParameter oParam_level_old = new SqlParameter("level_old", SqlDbType.VarChar);
                oParam_level_old.Direction = ParameterDirection.Input;
                oParam_level_old.Value = pLevel_old;
                oCommand.Parameters.Add(oParam_level_old);
                // - - - - - - - - - - - -             
                SqlParameter oParam_level_new = new SqlParameter("level_new", SqlDbType.VarChar);
                oParam_level_new.Direction = ParameterDirection.Input;
                oParam_level_new.Value = pLevel_new;
                oCommand.Parameters.Add(oParam_level_new);
                // - - - - - - - - - - - -             
                SqlParameter oParam_Active = new SqlParameter("c_active", SqlDbType.NVarChar);
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

        #region SP_PERSON_BUDGET_UPD
        public bool SP_PERSON_BUDGET_UPD(string pperson_code, string pbudget_plan_code, ref string strMessage)
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
                oCommand.CommandText = "sp_PERSON_BUDGET_UPD";
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_code = new SqlParameter("person_code", SqlDbType.NVarChar);
                oParam_person_code.Direction = ParameterDirection.Input;
                oParam_person_code.Value = pperson_code;
                oCommand.Parameters.Add(oParam_person_code);
                // - - - - - - - - - - - -              
                SqlParameter oParam_budget_plan_code = new SqlParameter("budget_plan_code", SqlDbType.NVarChar);
                oParam_budget_plan_code.Direction = ParameterDirection.Input;
                oParam_budget_plan_code.Value = pbudget_plan_code;
                oCommand.Parameters.Add(oParam_budget_plan_code);

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

        #region SP_PERSON_ITEM_IMPORT
        public bool SP_PERSON_ITEM_IMPORT(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_PERSON_ITEM_IMPORT";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_PERSON_ITEM_IMPORT");
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

        #region SP_PERSON_CUMULATIVE_UPD
        public bool SP_PERSON_CUMULATIVE_UPD(string pperson_code, string pcumulative_acc, string pcumulative_money, ref string strMessage)
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
                oCommand.CommandText = "sp_PERSON_CUMULATIVE_UPD";
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_code = new SqlParameter("person_code", SqlDbType.NVarChar);
                oParam_person_code.Direction = ParameterDirection.Input;
                oParam_person_code.Value = pperson_code;
                oCommand.Parameters.Add(oParam_person_code);
                // - - - - - - - - - - - -             
                SqlParameter oParam_cumulative_acc = new SqlParameter("cumulative_acc", SqlDbType.NVarChar);
                oParam_cumulative_acc.Direction = ParameterDirection.Input;
                oParam_cumulative_acc.Value = pcumulative_acc;
                oCommand.Parameters.Add(oParam_cumulative_acc);
              
                // - - - - - - - - - - - -             
                SqlParameter oParam_cumulative_money = new SqlParameter("cumulative_money", SqlDbType.Money);
                oParam_cumulative_money.Direction = ParameterDirection.Input;
                oParam_cumulative_money.Value = float.Parse(pcumulative_money);
                oCommand.Parameters.Add(oParam_cumulative_money);
              
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

        #region SP_PERSON_CUMULATIVE_SEL
        public bool SP_PERSON_CUMULATIVE_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_PERSON_CUMULATIVE_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_PERSON_CUMULATIVE_SEL");
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


        #region SP_PERSON_LOAN_SEL
        public bool SP_PERSON_LOAN_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_PERSON_LOAN_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_PERSON_LOAN_SEL");
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

        #region SP_PERSON_LOAN_DEL
        public bool SP_PERSON_LOAN_DEL(string pPerson_code, string pLoan_code, string pLoan_acc , ref string strMessage)
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
                oCommand.CommandText = "sp_PERSON_LOAN_DEL";
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_code = new SqlParameter("person_code", SqlDbType.NVarChar);
                oParam_person_code.Direction = ParameterDirection.Input;
                oParam_person_code.Value = pPerson_code;
                oCommand.Parameters.Add(oParam_person_code);
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

        #region SP_PERSON_LOAN_INS
        public bool SP_PERSON_LOAN_INS(string pPerson_code, string pLoan_code, string pLoan_acc,string pLoan_acc_name , string pLoan_remark, string pC_created_by, ref string strMessage)
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
                oCommand.CommandText = "sp_PERSON_LOAN_INS";
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_code = new SqlParameter("person_code", SqlDbType.NVarChar);
                oParam_person_code.Direction = ParameterDirection.Input;
                oParam_person_code.Value = pPerson_code;
                oCommand.Parameters.Add(oParam_person_code);
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
                SqlParameter oParam_loan_acc_name = new SqlParameter("loan_acc_name", SqlDbType.NVarChar);
                oParam_loan_acc_name.Direction = ParameterDirection.Input;
                oParam_loan_acc_name.Value = pLoan_acc_name;
                oCommand.Parameters.Add(oParam_loan_acc_name);

                // - - - - - - - - - - - -             
                SqlParameter oParam_loan_remark = new SqlParameter("loan_remark", SqlDbType.NVarChar);
                oParam_loan_remark.Direction = ParameterDirection.Input;
                oParam_loan_remark.Value = pLoan_remark;
                oCommand.Parameters.Add(oParam_loan_remark);

   
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

        #region SP_PERSON_LOAN_UPD
        public bool SP_PERSON_LOAN_UPD(string pPerson_code, string pLoan_code, string pLoan_acc, string pLoan_acc_name , string pLoan_remark, string pC_updated_by, ref string strMessage)
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
                oCommand.CommandText = "sp_PERSON_LOAN_UPD";
                // - - - - - - - - - - - -             
                SqlParameter oParam_person_code = new SqlParameter("person_code", SqlDbType.NVarChar);
                oParam_person_code.Direction = ParameterDirection.Input;
                oParam_person_code.Value = pPerson_code;
                oCommand.Parameters.Add(oParam_person_code);
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
                SqlParameter oParam_loan_acc_name = new SqlParameter("loan_acc_name", SqlDbType.NVarChar);
                oParam_loan_acc_name.Direction = ParameterDirection.Input;
                oParam_loan_acc_name.Value = pLoan_acc_name;
                oCommand.Parameters.Add(oParam_loan_acc_name);

                // - - - - - - - - - - - -             
                SqlParameter oParam_loan_remark = new SqlParameter("loan_remark", SqlDbType.NVarChar);
                oParam_loan_remark.Direction = ParameterDirection.Input;
                oParam_loan_remark.Value = pLoan_remark;
                oCommand.Parameters.Add(oParam_loan_remark);

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


        #region IDisposable Members

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
