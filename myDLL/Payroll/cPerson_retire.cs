using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace myDLL
{
    public class cPerson_retire : IDisposable
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

        public cPerson_retire()
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

        #region SP_PERSON_RETIRE_SEL
        public bool SP_PERSON_RETIRE_SEL(string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = "sp_PERSON_RETIRE_SEL";
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "sp_PERSON_RETIRE_SEL");
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

        #region SP_PERSON_RETIRE_DEL
        public bool SP_PERSON_RETIRE_DEL(string pPr_person_code, ref string strMessage)
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
                oCommand.CommandText = "sp_PERSON_RETIRE_DEL";
                // - - - - - - - - - - - -             
                SqlParameter oParam_pr_person_code = new SqlParameter("pr_person_code", SqlDbType.NVarChar);
                oParam_pr_person_code.Direction = ParameterDirection.Input;
                oParam_pr_person_code.Value = pPr_person_code;
                oCommand.Parameters.Add(oParam_pr_person_code);
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

        #region SP_PERSON_RETIRE_INS

        public bool SP_PERSON_RETIRE_INS
            (
              string ppr_person_code
           , string ptitle_code
           , string pperson_thai_name
           , string pperson_thai_surname
           , string pperson_id
           , string pc_active
           , string pc_created_by
           , string pperson_password
           , string pperson_email
           , ref string strMessage
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
                oCommand.CommandText = "sp_PERSON_RETIRE_INS";
                oCommand.Parameters.Add("pr_person_code", SqlDbType.VarChar).Value = ppr_person_code;
                oCommand.Parameters.Add("title_code", SqlDbType.VarChar).Value = ptitle_code;
                oCommand.Parameters.Add("person_thai_name", SqlDbType.VarChar).Value = pperson_thai_name;
                oCommand.Parameters.Add("person_thai_surname", SqlDbType.VarChar).Value = pperson_thai_surname;
                oCommand.Parameters.Add("person_id", SqlDbType.VarChar).Value = pperson_id;
                oCommand.Parameters.Add("c_active", SqlDbType.VarChar).Value = pc_active;
                oCommand.Parameters.Add("c_created_by", SqlDbType.VarChar).Value = pc_created_by;
                oCommand.Parameters.Add("person_password", SqlDbType.VarChar).Value  = Cryptorengine.Encrypt(pperson_password, true); 
                oCommand.Parameters.Add("person_email", SqlDbType.VarChar).Value = pperson_email;           
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

        #region SP_PERSON_RETIRE_UPD

        public bool SP_PERSON_RETIRE_UPD
            (
             string ppr_person_code
           , string ptitle_code
           , string pperson_thai_name
           , string pperson_thai_surname
           , string pperson_id
           , string pc_active
           , string pc_updated_by
           , string pperson_password
           , string pperson_email
           , ref string strMessage
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
                oCommand.CommandText = "sp_PERSON_RETIRE_UPD";
                oCommand.Parameters.Add("pr_person_code", SqlDbType.VarChar).Value = ppr_person_code;
                oCommand.Parameters.Add("title_code", SqlDbType.VarChar).Value = ptitle_code;
                oCommand.Parameters.Add("person_thai_name", SqlDbType.VarChar).Value = pperson_thai_name;
                oCommand.Parameters.Add("person_thai_surname", SqlDbType.VarChar).Value = pperson_thai_surname;
                oCommand.Parameters.Add("person_id", SqlDbType.VarChar).Value = pperson_id;
                oCommand.Parameters.Add("c_active", SqlDbType.VarChar).Value = pc_active;
                oCommand.Parameters.Add("c_updated_by", SqlDbType.VarChar).Value = pc_updated_by;
                oCommand.Parameters.Add("person_password", SqlDbType.VarChar).Value = Cryptorengine.Encrypt(pperson_password, true); 
                oCommand.Parameters.Add("person_email", SqlDbType.VarChar).Value = pperson_email;
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
