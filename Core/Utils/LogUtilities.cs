using log4net;
using System;

namespace Core.Utils
{
    public class LogUtilities
    {
        #region Variables
        private static readonly ILog mLog = LogManager.GetLogger("MessageLogger");
        private static readonly ILog eLog = LogManager.GetLogger("ErrorLogger");
        #endregion

        #region log4net properties

        public static void LogErrorMessage(string message, Exception ex)
        {
            SetLogProperties();
            eLog.Error(message, ex);
        }
        public static void LogErrorMessage(string message)
        {
            SetLogProperties();
            eLog.Error(message);
        }

        public static void LogInfoMessage(string message, string formName)
        {
            SetLogProperties();
            mLog.Info(GetLogInfo(message, formName));
        }

        private static void SetLogProperties()
        {
            if (!string.IsNullOrEmpty(ConstantValues.CurrentUsername))
            {
                log4net.LogicalThreadContext.Properties["systemUsername"] = ConstantValues.CurrentUsername;
            }
        }

        private static string GetLogInfo(string message, string formName)
        {
            return message + " [" + formName + "]";
        }
        #endregion
    }
}
