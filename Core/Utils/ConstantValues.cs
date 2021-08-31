using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace Core.Utils
{
    public class ConstantValues
    {
        public static string WebAPI_Root_Url = "http://localhost:11959/";
        public static string Web_Root_Url = "http://localhost:2794/";
        public static string SG_Timezone = "Singapore Standard Time";
        public const string STATUS_PENDING = "Pending";
        public const string STATUS_APPROVE = "Approve";
        public const string STATUS_REJECT = "Reject";
        public const string STATUS_CANCEL = "Cancel";

        public static string RoleId = string.Empty;
        public static string CurrentUsername = string.Empty;
        public static object CurrentUser;
        public static string LoginId = string.Empty;


        public static string WebAPI_Url = string.Empty;
        public static object ReportData;
        // email setting
        public static string ReturnUrl = string.Empty;

        public static string EmailHost = string.Empty;
        public static string EmailTo = string.Empty;
        public static string EmailCc = string.Empty;
        public static string EmailCurrentUser = string.Empty;
        public static string EmailSubject = string.Empty;
        public static string ReplyEmail = string.Empty;

        private static CultureInfo provider;
        public static DateTime GetDateTime(string timeZone)
        {
            DateTime timeUtc = DateTime.UtcNow;
            TimeZoneInfo kstZone = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
            return TimeZoneInfo.ConvertTimeFromUtc(timeUtc, kstZone);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentMethod()
        {
            var st = new StackTrace();
            var sf = st.GetFrame(1);

            return sf.GetMethod().Name + "()";
        }
        public static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c is >= 'a' and <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public static DateTime StringToDateTime(string value, string format)
        {
            provider = new CultureInfo("en-SG");
            DateTime oDate = DateTime.ParseExact(value.Trim(), format, provider);
            TimeZoneInfo sgTimeZone = TimeZoneInfo.FindSystemTimeZoneById(SG_Timezone);
            DateTime formattedDateTime = TimeZoneInfo.ConvertTime(oDate, TimeZoneInfo.Local, sgTimeZone);
            return formattedDateTime;
        }
    }
}