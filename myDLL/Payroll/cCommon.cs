using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace myDLL
{
    public class cCommon : IDisposable
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

        public cCommon()
        {
            //
            // TODO: Add constructor logic here
            //
            _strConn = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"];
        }

        #region SP_SEL_OBJECT
        public bool SP_SEL_OBJECT(string strSp_name, string strCriteria, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandText = strSp_name;
                SqlParameter oParamI_vc_criteria = new SqlParameter("vc_criteria", SqlDbType.NVarChar);
                oParamI_vc_criteria.Direction = ParameterDirection.Input;
                oParamI_vc_criteria.Value = strCriteria;
                oCommand.Parameters.Add(oParamI_vc_criteria);
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, strSp_name);
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

        #region SEL_SQL
        public bool SEL_SQL(string strSQL, ref DataSet ds, ref string strMessage)
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
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = strSQL;
                oAdapter = new SqlDataAdapter(oCommand);
                ds = new DataSet();
                oAdapter.Fill(ds, "data");
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


        #region EXE_SQL
        public bool EXE_SQL(string strSQL, ref string strMessage)
        {
            bool blnResult = false;
            SqlConnection oConn = new SqlConnection();
            SqlCommand oCommand = new SqlCommand();
            try
            {
                oConn.ConnectionString = _strConn;
                oConn.Open();
                oCommand.Connection = oConn;
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = strSQL;
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

        public static string CheckDate(string strinputDate)
        {
            DateTime inputDate;
            inputDate = DateTime.Parse(strinputDate);
            // ----- Use For Add And Update Date Data Type-----
            string inDay = inputDate.Day.ToString();
            string inMonth = inputDate.Month.ToString();
            string inYear = inputDate.Year.ToString();
            int mChkyear = 0;
            if (inDay.Length == 1)
            {
                inDay = "0" + inDay;
            }
            if (inMonth.Length == 1)
            {
                inMonth = "0" + inMonth;
            }
            mChkyear = int.Parse(inYear);
            if (mChkyear < 2200)
            {
                mChkyear = mChkyear + 543;
            }
            return inDay + "/" + inMonth + "/" + mChkyear.ToString();
        }


        public static string CheckDateTime(string strinputDate)
        {
            DateTime inputDate;
            inputDate = DateTime.Parse(strinputDate);
            // ----- Use For Add And Update Date Data Type-----
            string inDay = inputDate.Day.ToString();
            string inMonth = inputDate.Month.ToString();
            string inYear = inputDate.Year.ToString();
            string inHour = inputDate.Hour.ToString().PadLeft(2, '0');
            string inMin = inputDate.Minute.ToString().PadLeft(2, '0');
            int mChkyear = 0;
            if (inDay.Length == 1)
            {
                inDay = "0" + inDay;
            }
            if (inMonth.Length == 1)
            {
                inMonth = "0" + inMonth;
            }
            mChkyear = int.Parse(inYear);
            if (mChkyear < 2200)
            {
                mChkyear = mChkyear + 543;
            }
            return inDay + "/" + inMonth + "/" + mChkyear.ToString() + " " + inHour + ":" + inMin;
        }

        public static string SaveDate(string strinputDate)
        {
            DateTime inputDate;
            inputDate = DateTime.Parse(strinputDate);
            // ----- Use For Add And Update Date Data Type-----
            string inDay = inputDate.Day.ToString();
            string inMonth = inputDate.Month.ToString();
            string inYear = inputDate.Year.ToString();
            int mChkyear = 0;
            if (inDay.Length == 1)
            {
                inDay = "0" + inDay;
            }
            if (inMonth.Length == 1)
            {
                inMonth = "0" + inMonth;
            }
            mChkyear = int.Parse(inYear);
            if (mChkyear > 2200)
            {
                mChkyear = mChkyear - 543;
            }
            return (inMonth + "/" + inDay + "/" + mChkyear.ToString());
        }

        public static string GetDateTimeNow()
        {
            DateTime inputDate = DateTime.Now;
            // ----- Use For Add And Update Date Data Type-----
            string inDay = inputDate.Day.ToString();
            string inMonth = inputDate.Month.ToString();
            string inYear = inputDate.Year.ToString();
            int mChkyear = 0;
            if (inDay.Length == 1)
            {
                inDay = "0" + inDay;
            }
            if (inMonth.Length == 1)
            {
                inMonth = "0" + inMonth;
            }
            mChkyear = int.Parse(inYear);
            if (mChkyear > 2200)
            {
                mChkyear = mChkyear - 543;
            }
            return (inMonth + "/" + inDay + "/" + mChkyear.ToString() + " " + DateTime.Now.ToString("HH:mm:ss"));
        }

        public static string SeekDate(string strinputDate)
        {
            DateTime inputDate;
            inputDate = DateTime.Parse(strinputDate);
            // ----- Use For Add And Update Date Data Type-----
            string inDay = inputDate.Day.ToString();
            string inMonth = inputDate.Month.ToString();
            string inYear = inputDate.Year.ToString();
            int mChkyear = 0;
            if (inDay.Length == 1)
            {
                inDay = "0" + inDay;
            }
            if (inMonth.Length == 1)
            {
                inMonth = "0" + inMonth;
            }
            mChkyear = int.Parse(inYear);
            //if (CultureInfo.CurrentCulture.Name == "th-TH")
            //{
            //    if (mChkyear < 2200)
            //    {
            //        mChkyear = mChkyear + 543;
            //    }
            //}
            //else
            {
                if (mChkyear > 2200)
                {
                    mChkyear = mChkyear - 543;
                }
            }

            return mChkyear.ToString() + "-" + inMonth + "-" + inDay;
        }

        public static string getMonthName(object pMonth)
        {
            string strMonth = string.Empty;
            if (pMonth.Equals("01")) strMonth = "มกราคม";
            else if (pMonth.Equals("02")) strMonth = "กุมภาพันธ์";
            else if (pMonth.Equals("03")) strMonth = "มีนาคม";
            else if (pMonth.Equals("04")) strMonth = "เมษายน";
            else if (pMonth.Equals("05")) strMonth = "พฤษภาคม";
            else if (pMonth.Equals("06")) strMonth = "มิถุนายน";
            else if (pMonth.Equals("07")) strMonth = "กรกฏาคม";
            else if (pMonth.Equals("08")) strMonth = "สิงหาคม";
            else if (pMonth.Equals("09")) strMonth = "กันยายน";
            else if (pMonth.Equals("10")) strMonth = "ตุลาคม";
            else if (pMonth.Equals("11")) strMonth = "พฤศจิกายน";
            else if (pMonth.Equals("12")) strMonth = "ธันวาคม";
            return strMonth;
        }

        public enum DateInterval
        {
            Year,
            Month,
            Weekday,
            Day,
            Hour,
            Minute,
            Second
        }

        public class DateTimeUtil
        {

            public static long DateDiff(DateInterval interval, DateTime date1, DateTime date2)
            {

                TimeSpan ts = date2 - date1;

                switch (interval)
                {
                    case DateInterval.Year:
                        return date2.Year - date1.Year;
                    case DateInterval.Month:
                        return (date2.Month - date1.Month) + (12 * (date2.Year - date1.Year));
                    case DateInterval.Weekday:
                        return Fix(ts.TotalDays) / 7;
                    case DateInterval.Day:
                        return Fix(ts.TotalDays);
                    case DateInterval.Hour:
                        return Fix(ts.TotalHours);
                    case DateInterval.Minute:
                        return Fix(ts.TotalMinutes);
                    default:
                        return Fix(ts.TotalSeconds);
                }
            }

            private static long Fix(double Number)
            {
                if (Number >= 0)
                {
                    return (long)Math.Floor(Number);
                }
                return (long)Math.Ceiling(Number);
            }

        }

        public static string ThaiBaht(decimal BahtStr)
        {
            // ---- แปลงค่าตัวเลขเป็นจำนวนเงินบาท ใช้ได้ 99,999,999.99 ----
            // ---- รับค่าเป็น Decimal Return เป็น String ---
            if (BahtStr == 0)
            {
                return "";
            }
            string A = Convert.ToString(BahtStr);
            string B = A.Trim();
            int Mainloop = B.Length;
            int Subloop = B.Length;
            string rvalue = "";
            string dvalue = "";
            int max = 0;
            string chk = null;
            int Number = 0;
            int Digits = 0;

            while (Mainloop > 0)
            {
                Mainloop = Mainloop - 1;
                max = max + 1;
                chk = B.Substring(max, 1);

                if (chk != ".")
                {
                    rvalue = rvalue + B.Substring(max, 1);
                }
                else
                {
                    Subloop = Mainloop;
                    Mainloop = 0;
                }
            }

            while (Subloop > 0)
            {
                Subloop = Subloop - 1;
                max = max + 1;
                dvalue = dvalue + B.Substring(max, 1);
            }

            if (rvalue.Length == 0)
            {
                Number = 0;
            }
            else
            {
                Number = Convert.ToInt32(rvalue);
            }

            if (dvalue.Length == 0)
            {
                Digits = 0;
            }
            else if (dvalue.Length == 1)
            {
                Digits = Convert.ToInt32(dvalue + "0");
            }
            else if (dvalue.Length == 2)
            {
                Digits = Convert.ToInt32(dvalue);
            }
            else if (dvalue.Length > 2)
            {
                Digits = Convert.ToInt32(dvalue.Substring(1, 2));
            }

            string St = "";
            string NumStr = null;
            NumStr = Convert.ToString(Number.ToString().Substring(1, 8));
            int LenNumberStr = NumStr.Length;

            if (NumStr.Length == 7)
            {
                NumStr = "X" + Convert.ToString(Number);
            }
            if (NumStr.Length == 6)
            {
                NumStr = "XX" + Convert.ToString(Number);
            }
            if (NumStr.Length == 5)
            {
                NumStr = "XXX" + Convert.ToString(Number);
            }
            if (NumStr.Length == 4)
            {
                NumStr = "XXXX" + Convert.ToString(Number);
            }
            if (NumStr.Length == 3)
            {
                NumStr = "XXXXX" + Convert.ToString(Number);
            }
            if (NumStr.Length == 2)
            {
                NumStr = "XXXXXX" + Convert.ToString(Number);
            }
            if (NumStr.Length == 1)
            {
                NumStr = "XXXXXXX" + Convert.ToString(Number);
            }

            int mCount = 0;
            for (mCount = 1; mCount <= 8; mCount++)
            {
                if (mCount == 1)
                {
                    if (NumStr.Substring(mCount, 1) != "X")
                    {
                        if (Convert.ToInt32(NumStr.Substring(mCount, 1)) == 1)
                        {
                            St = St + "สิบ";
                        }
                        if (Convert.ToInt32(NumStr.Substring(mCount, 1)) == 2)
                        {
                            St = St + "ยี่สิบ";
                        }
                        if (Convert.ToInt32(NumStr.Substring(mCount, 1)) > 2)
                        {
                            St = St + CheckNum(NumStr.Substring(mCount, 1));
                            St = St + "สิบ";
                        }
                    }
                }
                if (mCount == 2)
                {
                    if (NumStr.Substring(mCount, 1) != "X")
                    {
                        if (Convert.ToInt32(NumStr.Substring(mCount, 1)) == 1)
                        {
                            if (BahtStr < 9999999)
                            {
                                St = St + "หนึ่ง";
                            }
                            else
                            {
                                St = St + "เอ็ด";
                            }
                        }
                        else
                        {
                            St = St + CheckNum(NumStr.Substring(mCount, 1));
                        }
                        St = St + "ล้าน";
                    }
                }
                if (mCount == 3)
                {
                    if (NumStr.Substring(mCount, 1) != "X")
                    {
                        if (NumStr.Substring(mCount, 1) != "0")
                        {
                            St = St + CheckNum(NumStr.Substring(mCount, 1));
                            St = St + "แสน";
                        }
                    }
                }
                if (mCount == 4)
                {
                    if (NumStr.Substring(mCount, 1) != "X")
                    {
                        if (NumStr.Substring(mCount, 1) != "0")
                        {
                            St = St + CheckNum(NumStr.Substring(mCount, 1));
                            St = St + "หมื่น";
                        }
                    }
                }
                if (mCount == 5)
                {
                    if (NumStr.Substring(mCount, 1) != "X")
                    {
                        if (NumStr.Substring(mCount, 1) != "0")
                        {
                            St = St + CheckNum(NumStr.Substring(mCount, 1));
                            St = St + "พัน";
                        }
                    }
                }
                if (mCount == 6)
                {
                    if (NumStr.Substring(mCount, 1) != "X")
                    {
                        if (NumStr.Substring(mCount, 1) != "0")
                        {
                            St = St + CheckNum(NumStr.Substring(mCount, 1));
                            St = St + "ร้อย";
                        }
                    }
                }
                if (mCount == 7)
                {
                    if (NumStr.Substring(mCount, 1) != "X")
                    {
                        if (NumStr.Substring(mCount, 1) != "0")
                        {
                            if (Convert.ToInt32(NumStr.Substring(mCount, 1)) == 1)
                            {
                                St = St + "สิบ";
                            }
                            if (Convert.ToInt32(NumStr.Substring(mCount, 1)) == 2)
                            {
                                St = St + "ยี่สิบ";
                            }
                            if (Convert.ToInt32(NumStr.Substring(mCount, 1)) > 2)
                            {
                                St = St + CheckNum(NumStr.Substring(mCount, 1));
                                St = St + "สิบ";
                            }
                        }
                    }
                }
                if (mCount == 8)
                {
                    if (NumStr.Substring(mCount, 1) != "X")
                    {
                        if (NumStr.Substring(mCount, 1) != "0")
                        {
                            if (Convert.ToInt32(NumStr.Substring(mCount, 1)) == 1)
                            {
                                St = St + "เอ็ด";
                            }
                            else
                            {
                                St = St + CheckNum(NumStr.Substring(mCount, 1));
                            }
                        }
                    }
                }
            }

            if (Digits == 0)
            {
                St = St + "บาทถ้วน";
                if (St == "เอ็ดบาทถ้วน")
                {
                    St = "หนึ่งบาทถ้วน";
                }
                return St;
            }
            else
            {
                St = St + "บาท";
                if (St == "เอ็ดบาท")
                {
                    St = "หนึ่งบาท";
                }
                if (Number == 0)
                {
                    St = "";
                }
            }

            string DigitsStr = Convert.ToString(dvalue);
            int LenDigitsStr = DigitsStr.Length;
            for (mCount = 1; mCount <= LenDigitsStr; mCount++)
            {
                if (mCount == 1)
                {
                    if (Convert.ToInt32(DigitsStr.Substring(mCount, 1)) == 0)
                    {
                        St = St;
                    }
                    if (Convert.ToInt32(DigitsStr.Substring(mCount, 1)) == 1)
                    {
                        St = St + "สิบ";
                    }
                    if (Convert.ToInt32(DigitsStr.Substring(mCount, 1)) == 2)
                    {
                        St = St + "ยี่สิบ";
                    }
                    if (Convert.ToInt32(DigitsStr.Substring(mCount, 1)) > 2)
                    {
                        St = St + CheckNum(DigitsStr.Substring(mCount, 1));
                        St = St + "สิบ";
                    }
                }
                if (mCount == 2)
                {
                    if (Convert.ToInt32(DigitsStr.Substring(mCount, 1)) == 1)
                    {
                        if (Digits > 9)
                        {
                            St = St + "เอ็ด";
                        }
                        else
                        {
                            St = St + "หนึ่ง";
                        }
                    }
                    else
                    {
                        St = St + CheckNum(DigitsStr.Substring(mCount, 1));
                    }
                }
            }
            St = St + "สตางค์";
            return St;
        }

        public static string CheckNum(string CurrStr)
        {
            if (CurrStr == "1")
            {
                return "หนึ่ง";
            }
            if (CurrStr == "2")
            {
                return "สอง";
            }
            if (CurrStr == "3")
            {
                return "สาม";
            }
            if (CurrStr == "4")
            {
                return "สี่";
            }
            if (CurrStr == "5")
            {
                return "ห้า";
            }
            if (CurrStr == "6")
            {
                return "หก";
            }
            if (CurrStr == "7")
            {
                return "เจ็ด";
            }
            if (CurrStr == "8")
            {
                return "แปด";
            }
            if (CurrStr == "9")
            {
                return "เก้า";
            }
            if (CurrStr == "0")
            {
                return "";
            }
            return "";
        }

        #region IDisposable Members
        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }
        #endregion

        public static string AbsoluteWebRoot
        {
            get
            {
                HttpContext context;
                Uri uriAbsoluteURL;
                string strAbsoluteUrl;
                context = HttpContext.Current;
                if ((context == null))
                {
                    throw new System.Net.WebException("The current HttpContext is null");
                }
                if ((context.Items["absoluteurl"] == null))
                {
                    context.Items["absoluteurl"] = new Uri((context.Request.Url.GetLeftPart(UriPartial.Authority) + RelativeWebRoot));
                }
                uriAbsoluteURL = ((Uri)(context.Items["absoluteurl"]));
                strAbsoluteUrl = uriAbsoluteURL.ToString();
                return strAbsoluteUrl;
            }
        }

        public static string RelativeWebRoot
        {
            get
            {
                return VirtualPathUtility.ToAbsolute("~/");
            }
        }




    }
}
