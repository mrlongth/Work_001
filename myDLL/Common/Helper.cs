using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.IO;

namespace myDLL
{
    public class Helper
    {
        public Helper()
        {

        }

        #region CStr

        public static string CStr(object value)
        {
            try
            {
                if (!string.IsNullOrEmpty(value.ToString()))
                {
                    return Convert.ToString(value);
                }
                else
                {
                    return string.Empty;
                }
            }
            catch
            {
                return string.Empty;
            }
        }
        public static string CStr(object value, string defaultValue)
        {
            try
            {
                if (!string.IsNullOrEmpty(value.ToString()))
                {
                    return value.ToString();
                }
                else
                {
                    return defaultValue;
                }

            }
            catch
            {
                return defaultValue;
            }
        }

        #endregion

        #region CDate

        public static DateTime? CDate(object value)
        {
            try
            {
                if (value != null)
                {
                    return DateTime.Parse(value.ToString(), CultureInfo.InvariantCulture);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
        public static DateTime? CDate(object value, DateTime defaultValue)
        {
            try
            {
                if (value != null)
                {
                    return DateTime.Parse(value.ToString(), CultureInfo.InvariantCulture);
                }
                else
                {
                    return defaultValue;
                }
            }
            catch
            {
                return defaultValue;
            }
        }

        #endregion

        #region CInt

        public static int CInt(object value)
        {
            try
            {
                return int.Parse(value.ToString());
            }
            catch
            {

                return 0;
            }
        }
        public static int CInt(object value, int defaultValue)
        {
            try
            {
                return int.Parse(value.ToString());
            }
            catch
            {

                return defaultValue;
            }
        }

        #endregion

        #region CLong

        public static long CLong(object value)
        {
            try
            {
                return long.Parse(value.ToString());
            }
            catch
            {

                return 0;
            }
        }
        public static long CLong(object value, int defaultValue)
        {
            try
            {
                return long.Parse(value.ToString());
            }
            catch
            {

                return defaultValue;
            }
        }

        #endregion


        #region CDec

        public static decimal CDec(object value)
        {
            try
            {
                return decimal.Parse(value.ToString());
            }
            catch
            {

                return 0;
            }
        }
        public static decimal CDec(object value, decimal defaultValue)
        {
            try
            {
                return decimal.Parse(value.ToString());
            }
            catch
            {

                return defaultValue;
            }
        }

        #endregion

        #region CDbl

        public static double CDbl(object value)
        {
            try
            {
                return double.Parse(value.ToString());
            }
            catch
            {

                return 0;
            }
        }
        public static double CDbl(object value, double defaultValue)
        {
            try
            {
                return double.Parse(value.ToString());
            }
            catch
            {

                return defaultValue;
            }
        }

        #endregion

        #region Insert a single quote

        public static string ISql(string stringData)
        {
            try
            {
                return stringData.Replace("'", "''");
            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
            }

        }

        #endregion

        public static bool DeleteUnusedFile(string strFilePath, short sintFileAliveTime)
        {
            bool blnResult = false;

            DateTime dtNow = DateTime.Now;
            TimeSpan span = default(TimeSpan);

            DirectoryInfo dirInfo = new DirectoryInfo(strFilePath);

            if (dirInfo.Exists)
            {
                foreach (FileInfo fi in dirInfo.GetFiles())
                {
                    span = dtNow.Subtract(fi.LastWriteTime);
                    if (span.TotalMinutes > sintFileAliveTime)
                    {
                        try
                        {
                            fi.IsReadOnly = false;
                            fi.Delete();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                    }
                }
                blnResult = true;
            }
            return blnResult;
        }

        public static string ReplaceScript(string strText)
        {
            strText = strText.Replace("\n", "\\n");
            strText = strText.Replace("\r", "\\r");
            strText = strText.Replace("'", "\\'");
            return strText;
        }




    }
}
