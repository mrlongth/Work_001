using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace myDLL
{
    public class cDirector : IDisposable
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

    public cDirector()
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

    #region SP_SEL_DIRECTOR
    public bool SP_SEL_DIRECTOR(string strCriteria, ref DataSet ds, ref string strMessage)
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
            oCommand.CommandText = "sp_DIRECTOR_SEL";
            SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
            oParamI_vc_criteria.Direction = ParameterDirection.Input;
            oParamI_vc_criteria.Value = strCriteria;
            oCommand.Parameters.Add(oParamI_vc_criteria);
            oAdapter = new SqlDataAdapter(oCommand);
            ds = new DataSet();
            oAdapter.Fill(ds, "sp_DIRECTOR_SEL");
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

    #region SP_INS_DIRECTOR
    public bool SP_INS_DIRECTOR(string pdirector_year, string pdirector_name, string pdirector_sign_name, string pdirector_sign_image, string psign_position, string pDirector_order,
                                                                    string pActive, string pC_created_by,string pbudget_type, ref string strMessage)
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
            oCommand.CommandText = "sp_DIRECTOR_INS";
            // - - - - - - - - - - - -             
            SqlParameter oParam_Director_year= new SqlParameter("director_year", SqlDbType.NVarChar);
            oParam_Director_year.Direction = ParameterDirection.Input;
            oParam_Director_year.Value = pdirector_year;
            oCommand.Parameters.Add(oParam_Director_year);
            // - - - - - - - - - - - -             
            SqlParameter oParam_Director_name = new SqlParameter("director_name", SqlDbType.NVarChar);
            oParam_Director_name.Direction = ParameterDirection.Input;
            oParam_Director_name.Value = pdirector_name;
            oCommand.Parameters.Add(oParam_Director_name);

            // - - - - - - - - - - - -             
            SqlParameter oParam_director_sign_name = new SqlParameter("director_sign_name", SqlDbType.NVarChar);
            oParam_director_sign_name.Direction = ParameterDirection.Input;
            oParam_director_sign_name.Value = pdirector_sign_name;
            oCommand.Parameters.Add(oParam_director_sign_name);

            // - - - - - - - - - - - -             
            SqlParameter oParam_director_sign_image = new SqlParameter("director_sign_image", SqlDbType.Image);
            oParam_director_sign_image.Direction = ParameterDirection.Input;
            oParam_director_sign_image.Value = getImage(pdirector_sign_image);
            oCommand.Parameters.Add(oParam_director_sign_image);

            // - - - - - - - - - - - -             
            SqlParameter oParam_sign_position = new SqlParameter("sign_position", SqlDbType.NVarChar);
            oParam_sign_position.Direction = ParameterDirection.Input;
            oParam_sign_position.Value = psign_position;
            oCommand.Parameters.Add(oParam_sign_position);

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


            SqlParameter oParam_budget_type = new SqlParameter("budget_type", SqlDbType.NVarChar);
            oParam_budget_type.Direction = ParameterDirection.Input;
            oParam_budget_type.Value = pbudget_type;
            oCommand.Parameters.Add(oParam_budget_type);


            SqlParameter oParam_director_order = new SqlParameter("director_order", SqlDbType.Int);
            oParam_director_order.Direction = ParameterDirection.Input;
            oParam_director_order.Value = int.Parse(pDirector_order);
            oCommand.Parameters.Add(oParam_director_order);
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


    protected byte[] getImage(string strdirector_sign_image)
    {
        FileStream fs = new FileStream(strdirector_sign_image, FileMode.Open, FileAccess.Read);
        BinaryReader br = new BinaryReader(fs);
        byte[] photo = br.ReadBytes((int)fs.Length);
        br.Close();
        fs.Close();
        return photo;
    }



    #endregion

    #region SP_UPD_DIRECTOR
    public bool SP_UPD_DIRECTOR(string pdirector_code, string pdirector_year, string pdirector_name, string pdirector_sign_name, string pdirector_sign_image, string psign_position,
        string pDirector_order , string pActive, string pC_updated_by, string pbudget_type, ref string strMessage)
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
            oCommand.CommandText = "sp_DIRECTOR_UPD";
            // - - - - - - - - - - - -             
            SqlParameter oParam_Director_code = new SqlParameter("director_code", SqlDbType.NVarChar);
            oParam_Director_code.Direction = ParameterDirection.Input;
            oParam_Director_code.Value = pdirector_code;
            oCommand.Parameters.Add(oParam_Director_code);
            // - - - - - - - - - - - -             
            SqlParameter oParam_Director_year = new SqlParameter("director_year", SqlDbType.NVarChar);
            oParam_Director_year.Direction = ParameterDirection.Input;
            oParam_Director_year.Value = pdirector_year;
            oCommand.Parameters.Add(oParam_Director_year);
            // - - - - - - - - - - - -             
            SqlParameter oParam_Director_name = new SqlParameter("director_name", SqlDbType.NVarChar);
            oParam_Director_name.Direction = ParameterDirection.Input;
            oParam_Director_name.Value = pdirector_name;
            oCommand.Parameters.Add(oParam_Director_name);

            // - - - - - - - - - - - -             
            SqlParameter oParam_director_sign_name = new SqlParameter("director_sign_name", SqlDbType.NVarChar);
            oParam_director_sign_name.Direction = ParameterDirection.Input;
            oParam_director_sign_name.Value = pdirector_sign_name;
            oCommand.Parameters.Add(oParam_director_sign_name);

            // - - - - - - - - - - - -             
            SqlParameter oParam_director_sign_image = new SqlParameter("director_sign_image", SqlDbType.Image);
            oParam_director_sign_image.Direction = ParameterDirection.Input;
            oParam_director_sign_image.Value = getImage(pdirector_sign_image);
            oCommand.Parameters.Add(oParam_director_sign_image);

            // - - - - - - - - - - - -             
            SqlParameter oParam_sign_position = new SqlParameter("sign_position", SqlDbType.NVarChar);
            oParam_sign_position.Direction = ParameterDirection.Input;
            oParam_sign_position.Value = psign_position;
            oCommand.Parameters.Add(oParam_sign_position);

            
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

            SqlParameter oParam_budget_type = new SqlParameter("budget_type", SqlDbType.NVarChar);
            oParam_budget_type.Direction = ParameterDirection.Input;
            oParam_budget_type.Value = pbudget_type;
            oCommand.Parameters.Add(oParam_budget_type);

            SqlParameter oParam_director_order = new SqlParameter("director_order", SqlDbType.Int);
            oParam_director_order.Direction = ParameterDirection.Input;
            oParam_director_order.Value = int.Parse(pDirector_order);
            oCommand.Parameters.Add(oParam_director_order);
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

    #region SP_UPD_NOIMAGE_DIRECTOR
    public bool SP_UPD_NOIMAGE_DIRECTOR(string pdirector_code, string pdirector_year, string pdirector_name, string pdirector_sign_name, string psign_position,
        string pDirector_order ,string pActive, string pC_updated_by, string pbudget_type, ref string strMessage)
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
            oCommand.CommandText = "sp_DIRECTOR_NOIMG_UPD";
            // - - - - - - - - - - - -             
            SqlParameter oParam_Director_code = new SqlParameter("director_code", SqlDbType.NVarChar);
            oParam_Director_code.Direction = ParameterDirection.Input;
            oParam_Director_code.Value = pdirector_code;
            oCommand.Parameters.Add(oParam_Director_code);
            // - - - - - - - - - - - -             
            SqlParameter oParam_Director_year = new SqlParameter("director_year", SqlDbType.NVarChar);
            oParam_Director_year.Direction = ParameterDirection.Input;
            oParam_Director_year.Value = pdirector_year;
            oCommand.Parameters.Add(oParam_Director_year);
            // - - - - - - - - - - - -             
            SqlParameter oParam_Director_name = new SqlParameter("director_name", SqlDbType.NVarChar);
            oParam_Director_name.Direction = ParameterDirection.Input;
            oParam_Director_name.Value = pdirector_name;
            oCommand.Parameters.Add(oParam_Director_name);

            // - - - - - - - - - - - -             
            SqlParameter oParam_director_sign_name = new SqlParameter("director_sign_name", SqlDbType.NVarChar);
            oParam_director_sign_name.Direction = ParameterDirection.Input;
            oParam_director_sign_name.Value = pdirector_sign_name;
            oCommand.Parameters.Add(oParam_director_sign_name);

            // - - - - - - - - - - - -             
            SqlParameter oParam_sign_position = new SqlParameter("sign_position", SqlDbType.NVarChar);
            oParam_sign_position.Direction = ParameterDirection.Input;
            oParam_sign_position.Value = psign_position;
            oCommand.Parameters.Add(oParam_sign_position);


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

            SqlParameter oParam_budget_type = new SqlParameter("budget_type", SqlDbType.NVarChar);
            oParam_budget_type.Direction = ParameterDirection.Input;
            oParam_budget_type.Value = pbudget_type;
            oCommand.Parameters.Add(oParam_budget_type);


            SqlParameter oParam_director_order = new SqlParameter("director_order", SqlDbType.Int);
            oParam_director_order.Direction = ParameterDirection.Input;
            oParam_director_order.Value = int.Parse(pDirector_order);
            oCommand.Parameters.Add(oParam_director_order);
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

    #region SP_DEL_DIRECTOR
    public bool SP_DEL_DIRECTOR(string pdirector_code, string pActive, string pC_updated_by, ref string strMessage)
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
            oCommand.CommandText = "sp_DIRECTOR_DEL";
            // - - - - - - - - - - - -             
            SqlParameter oParam_Director_code = new SqlParameter("director_code", SqlDbType.NVarChar);
            oParam_Director_code.Direction = ParameterDirection.Input;
            oParam_Director_code.Value = pdirector_code;
            oCommand.Parameters.Add(oParam_Director_code);
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

    #region IDisposable Members

    void IDisposable.Dispose()
    {
        throw new NotImplementedException();
    }

    #endregion

    }
}
