using System;
using System.Threading.Tasks;

namespace ProjectCore.Common.Log
{
    /// <summary>
    /// 日志接口
    /// </summary>
    public interface ILoggerHelper
    {
        /// <summary>
        /// 记录trace日志
        /// </summary>
        /// <param name="source">信息来源</param>
        /// <param name="message">日志内容</param>
        /// <param name="args">标记</param>
        void LogTrace(string source, string message, params string[] args);

        /// <summary>
        /// 异步记录trace日志
        /// </summary>
        /// <param name="source">信息来源</param>
        /// <param name="message">日志内容</param>
        /// <param name="args">标记</param>
        /// <returns>Task</returns>
        Task LogTraceAsync(string source, string message, params string[] args);

        /// <summary>
        /// 记录debug信息
        /// </summary>
        /// <param name="source">信息来源</param>
        /// <param name="message">日志内容</param>
        /// <param name="args">标记</param>
        void LogDebug(string source, string message, params string[] args);

        /// <summary>
        /// 异步记录debug信息
        /// </summary>
        /// <param name="source">信息来源</param>
        /// <param name="message">日志内容</param>
        /// <param name="args">标记</param>
        /// <returns>Task</returns>
        Task LogDebugAsync(string source, string message, params string[] args);

        /// <summary>
        /// 记录信息
        /// </summary>
        /// <param name="source">信息来源</param>
        /// <param name="message">日志内容</param>
        /// <param name="args">标记</param>
        void LogInfo(string source, string message, params string[] args);

        /// <summary>
        /// 异步记录信息
        /// </summary>
        /// <param name="source">信息来源</param>
        /// <param name="message">日志内容</param>
        /// <param name="args">标记</param>
        /// <returns>Task</returns>
        Task LogInfoAsync(string source, string message, params string[] args);

        /// <summary>
        /// 记录警告日志
        /// </summary>
        /// <param name="source">信息来源</param>
        /// <param name="message">日志内容</param>
        /// <param name="args">标记</param>
        void LogWarn(string source, string message, params string[] args);

        /// <summary>
        /// 异步记录警告日志
        /// </summary>
        /// <param name="source">信息来源</param>
        /// <param name="message">日志内容</param>
        /// <param name="args">标记</param>
        /// <returns>Task</returns>
        Task LogWarnAsync(string source, string message, params string[] args);

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="source">信息来源</param>
        /// <param name="message">日志内容</param>
        /// <param name="args">标记</param>
        void LogError(string source, string message, params string[] args);

        /// <summary>
        /// 异步记录错误日志
        /// </summary>
        /// <param name="source">信息来源</param>
        /// <param name="message">日志内容</param>
        /// <param name="args">标记</param>
        /// <returns>Task</returns>
        Task LogErrorAsync(string source, string message, params string[] args);

        /// <summary>
        /// 提交异常日志
        /// </summary>
        /// <param name="ex">异常对象</param>
        void LogException(Exception ex);

        /// <summary>
        /// 异步提交异常日志
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <returns>Task</returns>
        Task LogExceptionAsync(Exception ex);
    }
  
}
