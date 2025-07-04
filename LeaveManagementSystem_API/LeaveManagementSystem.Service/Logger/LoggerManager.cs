﻿using NLog;

namespace LeaveManagementSystem.Service.Logger
{
    public class LoggerManager : ILoggerManager
    {
        #region Field
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructor
        public LoggerManager()
        {
        }
        #endregion

        #region Log Warn Methods
        public void Information(string message)
        {
            logger.Info(message);
        }

        public void Warning(string message)
        {
            logger.Warn(message);
        }

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Error(string message)
        {
            logger.Error(message);
        }
        #endregion
    }
}