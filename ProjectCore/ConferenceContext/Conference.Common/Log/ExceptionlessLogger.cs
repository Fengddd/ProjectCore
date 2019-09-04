using System;
using System.Threading.Tasks;
using Exceptionless;
using Exceptionless.Logging;

namespace ProjectCore.Common.Log
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
        public void LogTrace(string source, string message, params string[] args)
        {
            if (args != null && args.Length > 0)
            {
                ExceptionlessClient.Default.CreateLog(source, message, LogLevel.Trace).AddTags(args).Submit();
            }
            else
            {
                ExceptionlessClient.Default.SubmitLog(source, message, LogLevel.Trace);
            }
        }

        /// <summary>
        /// 异步记录trace日志
        /// </summary>
        /// <param name="source">信息来源</param>
        /// <param name="message">日志内容</param>
        /// <param name="args">添加标记</param>
        /// <returns>Task</returns>
        public async Task LogTraceAsync(string source, string message, params string[] args)
        {
            if (args != null && args.Length > 0)
            {
                ExceptionlessClient.Default.CreateLog(source, message, LogLevel.Trace).AddTags(args).Submit();
            }
            else
            {
                ExceptionlessClient.Default.SubmitLog(source, message, LogLevel.Trace);
            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// 记录debug信息
        /// </summary>
        /// <param name="source">信息来源</param>
        /// <param name="message">日志内容</param>
        /// <param name="args">标记</param>
        public void LogDebug(string source, string message, params string[] args)
        {
            if (args != null && args.Length > 0)
            {
                ExceptionlessClient.Default.CreateLog(source, message, LogLevel.Debug).AddTags(args).Submit();
            }
            else
            {
                ExceptionlessClient.Default.SubmitLog(source, message, LogLevel.Debug);
            }
        }

        /// <summary>
        /// 记录debug信息
        /// </summary>
        /// <param name="source">信息来源</param>
        /// <param name="message">日志内容</param>
        /// <param name="args">标记</param>
        /// <returns>Task</returns>
        public async Task LogDebugAsync(string source, string message, params string[] args)
        {
            if (args != null && args.Length > 0)
            {
                ExceptionlessClient.Default.CreateLog(source, message, LogLevel.Debug).AddTags(args).Submit();
            }
            else
            {
                ExceptionlessClient.Default.SubmitLog(source, message, LogLevel.Debug);
            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// 记录Info日志
        /// </summary>
        /// <param name="source">信息来源</param>
        /// <param name="message">日志内容</param>
        /// <param name="args">标记</param>
        public void LogInfo(string source, string message, params string[] args)
        {
            if (args != null && args.Length > 0)
            {
                ExceptionlessClient.Default.CreateLog(source, message, LogLevel.Info).AddTags(args).Submit();
            }
            else
            {
                ExceptionlessClient.Default.SubmitLog(source, message, LogLevel.Info);
            }
        }

        /// <summary>
        /// 异步记录Info日志
        /// </summary>
        /// <param name="source">信息来源</param>
        /// <param name="message">日志内容</param>
        /// <param name="args">标记</param>
        /// <returns>Task</returns>
        public async Task LogInfoAsync(string source, string message, params string[] args)
        {
            if (args != null && args.Length > 0)
            {
                ExceptionlessClient.Default.CreateLog(source, message, LogLevel.Info).AddTags(args).Submit();
            }
            else
            {
                ExceptionlessClient.Default.SubmitLog(source, message, LogLevel.Info);
            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// 记录警告日志
        /// </summary>
        /// <param name="source">信息来源</param>
        /// <param name="message">日志内容</param>
        /// <param name="args">标记</param>
        public void LogWarn(string source, string message, params string[] args)
        {
            if (args != null && args.Length > 0)
            {
                ExceptionlessClient.Default.CreateLog(source, message, LogLevel.Warn).AddTags(args).Submit();
            }
            else
            {
                ExceptionlessClient.Default.SubmitLog(source, message, LogLevel.Warn);
            }
        }

        /// <summary>
        /// 异步记录警告日志
        /// </summary>
        /// <param name="source">信息来源</param>
        /// <param name="message">日志内容</param>
        /// <param name="args">标记</param>
        /// <returns>Task</returns>
        public async Task LogWarnAsync(string source, string message, params string[] args)
        {
            if (args != null && args.Length > 0)
            {
                ExceptionlessClient.Default.CreateLog(source, message, LogLevel.Warn).AddTags(args).Submit();
            }
            else
            {
                ExceptionlessClient.Default.SubmitLog(source, message, LogLevel.Warn);
            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="source">信息来源</param>
        /// <param name="message">日志内容</param>
        /// <param name="args">标记</param>
        public void LogError(string source, string message, params string[] args)
        {
            if (args != null && args.Length > 0)
            {
                ExceptionlessClient.Default.CreateLog(source, message, LogLevel.Error).AddTags(args).Submit();
            }
            else
            {
                ExceptionlessClient.Default.SubmitLog(source, message, LogLevel.Error);
            }
        }

        /// <summary>
        /// 异步记录错误日志
        /// </summary>
        /// <param name="source">信息来源</param>
        /// <param name="message">日志内容</param>
        /// <param name="args">标记</param>
        /// <returns>Task</returns>
        public async Task LogErrorAsync(string source, string message, params string[] args)
        {
            if (args != null && args.Length > 0)
            {
                ExceptionlessClient.Default.CreateLog(source, message, LogLevel.Error).AddTags(args).Submit();
            }
            else
            {
                ExceptionlessClient.Default.SubmitLog(source, message, LogLevel.Error);
            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// 提交异常日志
        /// </summary>
        /// <param name="ex">异常对象</param>
        public void LogException(Exception ex)
        {
            ex.ToExceptionless().Submit();
        }

        /// <summary>
        /// 异步提交异常日志
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <returns>Task</returns>
        public async Task LogExceptionAsync(Exception ex)
        {
            ex.ToExceptionless().Submit();

            await Task.CompletedTask;
        }
    }
}
