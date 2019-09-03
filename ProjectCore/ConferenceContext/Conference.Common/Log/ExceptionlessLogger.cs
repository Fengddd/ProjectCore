using System;
using System.Collections.Generic;
using System.Text;
using Exceptionless;

namespace Conference.Common.Log
{
    /// <summary>
    /// ExceptionLess日志实现
    /// </summary>
    public class ExceptionLessLogger : ILoggerHelper
    {
        /// <summary>
        /// 记录trace日志
        /// </summary>
        /// <param name="source">信息来源</param>
        /// <param name="message">日志内容</param>
        /// <param name="args">添加标记</param>
        public void Trace(string source, string message, params string[] args)
        {
            if (args != null && args.Length > 0)
            {
                ExceptionlessClient.Default.CreateLog(source, message, "Debug").AddTags(args).Submit();

            }
            else
            {
                ExceptionlessClient.Default.SubmitLog(source, message, "Trace");
            }
        }
        /// <summary>
        /// 记录debug信息
        /// </summary>
        /// <param name="source">信息来源</param>
        /// <param name="message">日志内容</param>
        /// <param name="args">标记</param>
        public void Debug(string source, string message, params string[] args)
        {
            if (args != null && args.Length > 0)
            {
                ExceptionlessClient.Default.CreateLog(source, message, "Debug").AddTags(args).Submit();
            }
            else
            {
                ExceptionlessClient.Default.SubmitLog(source, message, "Debug");
            }
        }
        /// <summary>
        /// 记录信息
        /// </summary>
        /// <param name="source">信息来源</param>
        /// <param name="message">日志内容</param>
        /// <param name="args">标记</param>
        public void Info(string source, string message, params string[] args)
        {
            if (args != null && args.Length > 0)
            {
                ExceptionlessClient.Default.CreateLog(source, message, "Info").AddTags(args).Submit();
            }
            else
            {
                ExceptionlessClient.Default.SubmitLog(source, message, "Info");
            }
        }
        /// <summary>
        /// 记录警告日志
        /// </summary>
        /// <param name="source">信息来源</param>
        /// <param name="message">日志内容</param>
        /// <param name="args">标记</param>
        public void Warn(string source, string message, params string[] args)
        {
            if (args != null && args.Length > 0)
            {
                ExceptionlessClient.Default.CreateLog(source, message, "Warn").AddTags(args).Submit();
            }
            else
            {
                ExceptionlessClient.Default.SubmitLog(source, message, "Warn");
            }
        }
        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="source">信息来源</param>
        /// <param name="message">日志内容</param>
        /// <param name="args">标记</param>
        public void Error(string source, string message, params string[] args)
        {
            if (args != null && args.Length > 0)
            {
                ExceptionlessClient.Default.CreateLog(source, message, "Error").AddTags(args).Submit();
            }
            else
            {
                ExceptionlessClient.Default.SubmitLog(source, message, "Error");
            }
        }
    }
}
