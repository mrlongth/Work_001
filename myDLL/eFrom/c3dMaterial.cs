using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace myDLL
{
    public class c3dMaterial : IDisposable
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

        public c3dMaterial()
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

        #region SP_MATERIAL_SEL
        public DataTable SP_MATERIAL_SEL(string strCriteria)
        {
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            strCriteria += strCriteria.ToLower().Contains("order by") ? string.Empty : " Order by material_name";
            try
            {
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = "Select * from view_ef_material where 1=1 " + strCriteria;
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

        #region SP_PAYMENT_MATERIAL_SEL
        public DataTable SP_PAYMENT_MATERIAL_SEL(string strCriteria)
        {
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            strCriteria += strCriteria.ToLower().Contains("order by") ? string.Empty : " Order by material_name";
            try
            {
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = "Select * from [3d_view_material] where 1=1 " + strCriteria;
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



        #region SP_MATERIAL_INS
        public bool SP_MATERIAL_INS(
             ref int pmaterial_id,
             ref string pmaterial_code,
             string pmaterial_name,
             int pcount_id,
             double pstandard_price,
             double plast_price,
             string pmaterial_type,
             string pc_created_by)
        {
            bool blnResult = false;
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            try
            {
                var dtMaxCode = SP_MATERIAL_SEL(" and material_id = (select max(material_id) from [3d_material])");
                int maxCode = (dtMaxCode.Rows.Count > 0) ? Helper.CInt(dtMaxCode.Rows[0]["material_id"]) : 0;
                maxCode++;
                pmaterial_code = maxCode.ToString().PadLeft(6, '0');
                string strSql = "Insert into [3d_material] ([count_id],[material_code],[material_name],[standard_price],[last_price]," +
                                "[material_type],[c_created_by],[d_created_date]) values ( " +
                                "" + pcount_id + "," +
                                "'" + pmaterial_code + "'," +
                                "'" + pmaterial_name + "'," +
                                "" + pstandard_price + "," +
                                "" + plast_price + "," +
                                "'" + pmaterial_type + "'," +
                                "'" + pc_created_by + "'," +
                                "'" +  cCommon.GetDateTimeNow() + "')";
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = strSql;
                oCommand.ExecuteNonQuery();
                var dt = SP_MATERIAL_SEL(" and material_code='" + pmaterial_code + "'");
                pmaterial_id = Helper.CInt(dt.Rows[0]["material_id"]);
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

        #region SP_MATERIAL_UPD
        public bool SP_MATERIAL_UPD(
             int pmaterial_id,
             string pmaterial_code,
             string pmaterial_name,
             int pcount_id,
             double pstandard_price,
             double plast_price,
             string pmaterial_type,
             string pc_updated_by)
        {
            bool blnResult = false;
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            try
            {
                string strSql = "Update [3d_material] Set " +
                                "count_id = '" + pcount_id + "'," +
                                "material_code = '" + pmaterial_code + "'," +
                                "material_name = '" + pmaterial_name + "'," +
                                "standard_price= " + pstandard_price + "," +
                                "last_price = " + plast_price + "," +
                                "material_type = '" + pmaterial_type + "'," +
                                "c_updated_by = '" + pc_updated_by + "'," +
                                "d_created_date = '" +  cCommon.GetDateTimeNow() + "' " +
                                "Where material_id = " + pmaterial_id;
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

        #region SP_MATERIAL_DEL
        public bool SP_MATERIAL_DEL(int pmaterial_id)
        {
            bool blnResult = false;
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            try
            {
                string strSql = "Delete from [3d_material] " +
                                "Where material_id = " + pmaterial_id;
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


        #region SP_MATERIAL_INS
        public bool SP_MATERIAL_INS(
             ref int pmaterial_id,
             ref string pmaterial_code,
             string pmaterial_name,
             string pitem_code,
             double pstandard_price,
             double plast_price,
             string pmaterial_type,
             string pc_created_by)
        {
            bool blnResult = false;
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            try
            {
                var dtMaxCode = SP_MATERIAL_SEL(" and material_id = (select max(material_id) from [3d_material])");
                int maxCode = (dtMaxCode.Rows.Count > 0) ? Helper.CInt(dtMaxCode.Rows[0]["material_id"]) : 0;
                maxCode++;
                pmaterial_code = maxCode.ToString().PadLeft(6, '0');
                string strSql = "Insert into [3d_material] ([item_code],[material_code],[material_name],[standard_price],[last_price]," +
                                "[material_type],[c_created_by],[d_created_date]) values ( " +
                                "'" + pitem_code + "'," +
                                "'" + pmaterial_code + "'," +
                                "'" + pmaterial_name + "'," +
                                "" + pstandard_price + "," +
                                "" + plast_price + "," +
                                "'" + pmaterial_type + "'," +
                                "'" + pc_created_by + "'," +
                                "'" + cCommon.GetDateTimeNow() + "')";
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = strSql;
                oCommand.ExecuteNonQuery();
                var dt = SP_MATERIAL_SEL(" and material_code='" + pmaterial_code + "'");
                pmaterial_id = Helper.CInt(dt.Rows[0]["material_id"]);
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

        #region SP_MATERIAL_UPD
        public bool SP_MATERIAL_UPD(
             int pmaterial_id,
             string pmaterial_code,
             string pmaterial_name,
             string pitem_code,
             double pstandard_price,
             double plast_price,
             string pmaterial_type,
             string pc_updated_by)
        {
            bool blnResult = false;
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            try
            {
                string strSql = "Update [3d_material] Set " +
                                "item_code = '" + pitem_code + "'," +
                                "material_code = '" + pmaterial_code + "'," +
                                "material_name = '" + pmaterial_name + "'," +
                                "standard_price= " + pstandard_price + "," +
                                "last_price = " + plast_price + "," +
                                "material_type = '" + pmaterial_type + "'," +
                                "c_updated_by = '" + pc_updated_by + "'," +
                                "d_created_date = '" + cCommon.GetDateTimeNow() + "' " +
                                "Where material_id = " + pmaterial_id;
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

 

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
