using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Globalization;

using Aware.DAL;
using System.Data;
using System.Data.SqlClient;


namespace myDLL
{

    public class UserMenuBLL : IDisposable
    {

        #region Private Variables

        private string strConnectionString = null;
        private bool disposed = false;

        #endregion

        #region Constructors & Destructor Methods

        public UserMenuBLL()
        {
            strConnectionString = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"];
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                }

                // Call the appropriate methods to clean up 
                // unmanaged resources here.
                // If disposing is false, 
                // only the following code is executed.


                // Note disposing has been done.
                disposed = true;
            }
        }

        ~UserMenuBLL()
        {
            Dispose(false);
        }

        #endregion

        #region Public Methods
           
        public DataTable SelectByID(int sintUserid, int sintMenuid)
        {
            DataSet ds = null;
            SqlParameter[] parms =
        {
			new SqlParameter("pUserID", SqlDbType.SmallInt, 2, ParameterDirection.Input, false, 5, 0,"UserID", DataRowVersion.Current,sintUserid),
			new SqlParameter("pMenuID", SqlDbType.SmallInt, 2, ParameterDirection.Input, false, 5, 0,"MenuID", DataRowVersion.Current,sintMenuid),

        };

            ds = SqlServerDAL.ExecuteDataset(strConnectionString,
                CommandType.StoredProcedure,
                "sp_UserMenuSelectByID",
                parms);

            return ds.Tables[0];
        }

        public DataTable SelectManageByCriteria(string strCriteria)
        {
            DataSet ds = null;
            SqlParameter[] parms =
            {
			    new SqlParameter("pCriteria",  strCriteria ),
            };
            ds = SqlServerDAL.ExecuteDataset(strConnectionString, CommandType.StoredProcedure, "sp_UserMenuSelectManageByCriteria", parms);
            return ds.Tables[0];
        }

        public DataTable SelectByCriteria(string strCriteria)
        {
            DataSet ds = null;
            SqlParameter[] parms =
            {
			    new SqlParameter("pCriteria",  strCriteria ),
            };
            ds = SqlServerDAL.ExecuteDataset(strConnectionString, CommandType.StoredProcedure, "sp_UserMenuSelectByCriteria", parms);
            return ds.Tables[0];
        }

        public void Insert(int sintUserid, int sintMenuid, char strCanview, char strCaninsert, char strCanedit, char strCandelete, char strCanapprove, char strCanextra, string strCreatedby, ref SqlTransaction trans)
        {
            SqlParameter[] parms =
        {
			new SqlParameter("pUserID", SqlDbType.SmallInt, 2, ParameterDirection.Input, false, 5, 0,"UserID", DataRowVersion.Current,sintUserid),
			new SqlParameter("pMenuID", SqlDbType.SmallInt, 2, ParameterDirection.Input, false, 5, 0,"MenuID", DataRowVersion.Current,sintMenuid),
			new SqlParameter("pCanView", SqlDbType.VarChar, 1, ParameterDirection.Input, false, 0, 0,"CanView", DataRowVersion.Current,strCanview),
			new SqlParameter("pCanInsert", SqlDbType.VarChar, 1, ParameterDirection.Input, false, 0, 0,"CanInsert", DataRowVersion.Current,strCaninsert),
			new SqlParameter("pCanEdit", SqlDbType.VarChar, 1, ParameterDirection.Input, false, 0, 0,"CanEdit", DataRowVersion.Current,strCanedit),
			new SqlParameter("pCanDelete", SqlDbType.VarChar, 1, ParameterDirection.Input, false, 0, 0,"CanDelete", DataRowVersion.Current,strCandelete),
			new SqlParameter("pCanApprove", SqlDbType.VarChar, 1, ParameterDirection.Input, false, 0, 0,"CanApprove", DataRowVersion.Current,strCanapprove),
			new SqlParameter("pCanExtra", SqlDbType.VarChar, 1, ParameterDirection.Input, false, 0, 0,"CanExtra", DataRowVersion.Current,strCanextra),
			new SqlParameter("pCreatedBy", SqlDbType.NVarChar, 51, ParameterDirection.Input, true, 0, 0,"CreatedBy", DataRowVersion.Current,strCreatedby)			
        };

            SqlServerDAL.ExecuteNonQuery(trans,
                CommandType.StoredProcedure,
                "sp_UserMenuInsert",
                parms);
        }

        public void Insert(int sintUserid, int sintMenuid, char strCanview, char strCaninsert, char strCanedit, char strCandelete, char strCanapprove, char strCanextra, string strCreatedby, ref string strMessage)
        {
            using (SqlConnection conn = new SqlConnection(strConnectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);

                try
                {
                    Insert(sintUserid, sintMenuid, strCanview, strCaninsert, strCanedit, strCandelete, strCanapprove, strCanextra, strCreatedby, ref trans);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    strMessage = ex.Message;
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }

        }

        public void Delete(int sintUserid, ref SqlTransaction trans)
        {
            SqlParameter[] parms =
        {
			new SqlParameter("pUserID", SqlDbType.SmallInt, 2, ParameterDirection.Input, false, 5, 0,"UserID", DataRowVersion.Current,sintUserid)
        };

            SqlServerDAL.ExecuteNonQuery(trans,
                CommandType.StoredProcedure,
                "sp_UserMenuDelete",
                parms);
        }

        public void Delete(int sintUserid, ref string strMessage)
        {

            using (SqlConnection conn = new SqlConnection(strConnectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    Delete(sintUserid, ref trans);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    strMessage = ex.Message;
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }

        }

        public void Update(int sintUserid, int sintMenuid, char strCanview, char strCaninsert, char strCanedit, char strCandelete, char strCanapprove, char strCanextra, string strCreatedby, ref SqlTransaction trans)
        {
            SqlParameter[] parms =
        {
			new SqlParameter("pUserID", SqlDbType.SmallInt, 2, ParameterDirection.Input, false, 5, 0,"UserID", DataRowVersion.Current,sintUserid),
			new SqlParameter("pMenuID", SqlDbType.SmallInt, 2, ParameterDirection.Input, false, 5, 0,"MenuID", DataRowVersion.Current,sintMenuid),
			new SqlParameter("pCanView", SqlDbType.VarChar, 1, ParameterDirection.Input, false, 0, 0,"CanView", DataRowVersion.Current,strCanview),
			new SqlParameter("pCanInsert", SqlDbType.VarChar, 1, ParameterDirection.Input, false, 0, 0,"CanInsert", DataRowVersion.Current,strCaninsert),
			new SqlParameter("pCanEdit", SqlDbType.VarChar, 1, ParameterDirection.Input, false, 0, 0,"CanEdit", DataRowVersion.Current,strCanedit),
			new SqlParameter("pCanDelete", SqlDbType.VarChar, 1, ParameterDirection.Input, false, 0, 0,"CanDelete", DataRowVersion.Current,strCandelete),
			new SqlParameter("pCanApprove", SqlDbType.VarChar, 1, ParameterDirection.Input, true, 0, 0,"CanApprove", DataRowVersion.Current,strCanapprove),
			new SqlParameter("pCanExtra", SqlDbType.VarChar, 1, ParameterDirection.Input, false, 0, 0,"CanExtra", DataRowVersion.Current,strCanextra),
			new SqlParameter("pUpdatedBy", SqlDbType.NVarChar, 51, ParameterDirection.Input, true, 0, 0,"CreatedBy", DataRowVersion.Current,strCreatedby)			
        };

            SqlServerDAL.ExecuteNonQuery(trans,
                CommandType.StoredProcedure,
                "sp_UserMenuUpdate",
                parms);
        }

        public void Update(int sintUserID, int sintMenuid, char strCanview, char strCaninsert, char strCanedit, char strCandelete, char strCanapprove, char strCanextra, string strCreatedby, ref string strMessage)
        {

            using (SqlConnection conn = new SqlConnection(strConnectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    Update(sintUserID, sintMenuid, strCanview, strCaninsert, strCanedit, strCandelete, strCanapprove, strCanextra, strCreatedby, ref trans);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    strMessage = ex.Message;
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }

        }

        #endregion

    }

}