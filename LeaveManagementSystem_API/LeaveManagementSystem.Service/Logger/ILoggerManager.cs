﻿namespace LeaveManagementSystem.Service.Logger
{
    public interface ILoggerManager
    {
        void Information(string message);
        void Warning(string message);
        void Debug(string message);
        void Error(string message);
    }
}
