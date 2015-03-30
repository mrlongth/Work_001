using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace myDLL
{
    public class cefUser : IDisposable
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

        public cefUser()
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

        #region SP_PERSON_USER_GROUP_UPD
        public bool SP_PERSON_USER_GROUP_UPD(
             string pperson_code,
             string pef_user_group_list,
             string papprove_for,
             string pc_updated_by)
        {
            bool blnResult = false;
            var oConn = new SqlConnection();
            var oCommand = new SqlCommand();
            try
            {
                string strSql = "Update person_work Set " +
                                " ef_user_group_list = '" + pef_user_group_list + "'," +
                                " ef_approve_for = '" + papprove_for + "'," +
                                " c_updated_by = '" + pc_updated_by + "'," +
                                " d_updated_date = '" +  cCommon.GetDateTimeNow() + "' " +
                                " Where person_code = '" + pperson_code + "'";
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

        #region SP_USER_GROUP_MENU_SEL
        public DataTable SP_USER_GROUP_MENU_GROUP(string puser_group_code)
        {
            var oConn = new SqlConnection();
            var oCommand = new SqlCommand();
            var oAdapter = new SqlDataAdapter();
            var dt = new DataTable();
            try
            {
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText =
                    @"SELECT [UserGroupCode],[MenuID],[CanView],[CanInsert],[CanEdit],[CanDelete],[CanApprove]," +
                    " [CanExtra],[g_name],[MenuName],[MenuNavigationUrl],[MenuOrder],[IsCanView],[IsCanInsert]," +
                    " [IsCanEdit],[IsCanDelete],[IsCanApprove],[IsCanExtra] " +
                    " FROM [view_ef_user_group_menu] Where [UserGroupCode] = '" + puser_group_code + "'" +
                    " Union " +
                    " SELECT '',[MenuID],'N','N','N','N','N','N','',[MenuName],[MenuNavigationUrl],[MenuOrder],[CanView]," +
                    " [CanInsert],[CanEdit],[CanDelete],[CanApprove],[CanExtra] From ef_menu " +
                    " Where MenuID not in (select MenuID from ef_user_group_menu where UserGroupCode = '" + puser_group_code + "') " +
                    " Order by MenuOrder ";
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

        #region SP_USER_GROUP_MENU_SEL
        public DataTable SP_USER_GROUP_MENU_SEL(string strCriteria)
        {
            var oConn = new SqlConnection();
            var oCommand = new SqlCommand();
            var oAdapter = new SqlDataAdapter();
            var dt = new DataTable();
            strCriteria += strCriteria.ToLower().Contains("order by") ? string.Empty : " Order by MenuOrder";
            try
            {
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = "Select * from view_ef_user_group_menu where 1=1 " + strCriteria;
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

        #region SP_USER_GROUP_MENU_DEL
        public bool SP_USER_GROUP_MENU_DEL(string pUserGroupCode)
        {
            bool blnResult = false;
            var oConn = new SqlConnection();
            var oCommand = new SqlCommand();
            try
            {
                string strSql = "Delete from ef_user_group_menu " +
                                " Where UserGroupCode = '" + pUserGroupCode + "'";
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

        #region SP_USER_GROUP_MENU_INS
        public bool SP_USER_GROUP_MENU_INS(
            string pUserGroupCode,
            int pMenuId,
            string pCanView,
            string pCanInsert,
            string pCanEdit,
            string pCanDelete,
            string pCanApprove,
            string pCanExtra)
        {
            bool blnResult = false;
            var oConn = new SqlConnection();
            var oCommand = new SqlCommand();
            try
            {
                string strSql =
                    "Insert into ef_user_group_menu ([UserGroupCode],[MenuID],[CanView],[CanInsert],[CanEdit],[CanDelete],[CanApprove],[CanExtra]) values (" +
                    "'" + pUserGroupCode + "'," +
                    "" + pMenuId + "," +
                    "'" + pCanView + "'," +
                    "'" + pCanInsert + "'," +
                    "'" + pCanEdit + "'," +
                    "'" + pCanDelete + "'," +
                    "'" + pCanApprove + "'," +
                    "'" + pCanExtra + "')";
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

        #region SP_USER_SIGN_SEL
        public DataTable SP_USER_SIGN_SEL(string strCriteria)
        {
            var oConn = new SqlConnection();
            var oCommand = new SqlCommand();
            var oAdapter = new SqlDataAdapter();
            var dt = new DataTable();
            strCriteria += strCriteria.ToLower().Contains("order by") ? string.Empty : " Order by person_code";
            try
            {
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = "Select * from view_ef_person_sign where 1=1 " + strCriteria;
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

        #region SP_USER_SIGN_UPDATE
        public bool SP_USER_SIGN_UPDATE(
            string pperson_code,
            byte[] pperson_sign_image)
        {
            bool blnResult = false;
            var oConn = new SqlConnection();
            var oCommand = new SqlCommand();
            try
            {
                string strSql = string.Empty;
                var dt = SP_USER_SIGN_SEL(" and person_code='" + pperson_code + "'");
                if (dt.Rows.Count > 0)
                {
                    strSql = "Update ef_person_sign set person_sign_image = @person_sign_image " +
                    "Where person_code = '" + pperson_code + "'";
                }
                else
                {
                    strSql = "Insert into ef_person_sign (person_code,person_sign_image) values ('" + pperson_code + "',@person_sign_image)";
                }
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.Parameters.Add("@person_sign_image", SqlDbType.Binary).Value = pperson_sign_image;
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


        #region IDisposable Members

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
