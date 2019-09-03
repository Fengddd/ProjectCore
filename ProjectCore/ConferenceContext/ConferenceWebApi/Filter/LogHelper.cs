using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Conference.Common;
using Conference.Common.Log;
using Exceptionless;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace ConferenceWebApi.Filter
{
    /// <summary>
    /// 日志帮助类
    /// </summary>
    public class LogHelper : ActionFilterAttribute, IExceptionFilter
    {
        private readonly Stopwatch _stopWatch;
        private readonly MonitorLog _monitorLog;
        private readonly ILog _logger = LogManager.GetLogger(Startup.Repository.Name, "NETCorelog4net");

        /// <summary>
        /// 构造函数
        /// </summary>
        public LogHelper()
        {
            this._stopWatch = new Stopwatch();
            if (_monitorLog == null)
            {
                this._monitorLog = new MonitorLog();
            }
        }

        /// <summary>
        /// Action 执行前
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //开始_stopWatch
            _stopWatch.Start();
            //获取控制器名称
            var controllerName = filterContext.RouteData.Values["controller"].ToString();
            //获取Action名称
            var actionName = filterContext.RouteData.Values["action"].ToString();
            //获取控制器参数
            var actionParameters = filterContext.ActionArguments;
            var paraStr = "";
            if (actionParameters.Count > 0)
            {
                var actionParameKey = actionParameters.Keys;
                var actionParaList = actionParameters.Values.ToList();
                foreach (var item in actionParaList)
                {
                    paraStr += JsonConvert.SerializeObject(item) + " ";
                }
            }
            _monitorLog.MonitorLogId = Guid.NewGuid();
            _monitorLog.ControllerName = controllerName;
            _monitorLog.ActionName = actionName;
            _monitorLog.RequestParameters = paraStr;
            _monitorLog.ExecuteStartTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff",
            (IFormatProvider)DateTimeFormatInfo.InvariantInfo));
            _monitorLog.AddressIp = GetAddressIp();
            _monitorLog.UserId = Guid.NewGuid();
            _monitorLog.UserName = "Admin";
        }

        /// <summary>
        /// Action 执行后
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _stopWatch.Stop();
            var logType = (int)LogType.OperationLog;
            var logLevel = (int)LogLevel.Write;
            _monitorLog.ExecuteEndTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff",
                    (IFormatProvider)DateTimeFormatInfo.InvariantInfo));
            _monitorLog.ExecutionTime = _stopWatch.ElapsedMilliseconds;
            if (filterContext.Exception != null)
            {
                _monitorLog.ErrorMsg = filterContext.Exception.Message;
                logType = (int)LogType.ExceptionLog;
                logLevel = (int)LogLevel.Error;
            }
            _monitorLog.LogType = logType;
            _monitorLog.LogLevel = logLevel;
            //记录日志数据到数据库
            InsertBusLogs(_monitorLog);
        }

        /// <summary>
        /// 异常过滤器
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
                return;
            string errorFormat = string.Format("\r\n 控制器:{0}  \r\n  视图:{1}    \r\n    开始时间：{2}   \r\n      结束时间：{3}\r\n      总 时 间：{4}秒", _monitorLog.ControllerName, _monitorLog.ActionName, _monitorLog.ExecuteStartTime, _monitorLog.ExecuteEndTime, (_monitorLog.ExecuteEndTime - _monitorLog.ExecuteStartTime));
            this.Error(errorFormat, filterContext.Exception);
            var message = "";
            Exception exception = filterContext.Exception;
            var exceptionType = exception.GetType().ToString();
            if (exceptionType == "ProjectCore.Common.DomainException")
            {
                //用来分类处理业务逻辑
            }
            if (exceptionType == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
            {
                //并发异常
                message += "同一条数据不能进行同时修改，删除！";
            }

            //获取controller的名称
            var controller = filterContext.RouteData.Values["controller"].ToString();
            //获取Action的名称
            var action = filterContext.RouteData.Values["Action"].ToString();
            
            var errorPath = controller + "/" + action;
            //返回错误码
            var statusCode = filterContext.HttpContext.Response.StatusCode;
            message += exception.Message;
            filterContext.Result = new JsonResult(new ResponseResult<string>
            {
                Message = "错误路径:" + errorPath + ":错误信息" + message + "",
                IsSucceed = false
            });
            //提交日志到ExceptionLess
            exception.ToExceptionless().Submit();
            //异常已处理了
            filterContext.ExceptionHandled = true;
        }

        /// <summary>
        /// 获取本地IP地址信息
        /// </summary>
        public static string GetAddressIp()
        {
            //获取本地的IP地址
            string addressIp = string.Empty;
            foreach (IPAddress ipAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (ipAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    addressIp = ipAddress.ToString();
                }
            }
            return addressIp;
        }

        #region 日志类型

        /// <summary>记录Error日志</summary>
        /// <param name="errorMsg"></param>
        /// <param name="ex"></param>
        public void Error(string errorMsg, Exception ex = null)
        {
            if (ex != null)
                _logger.Error((object)errorMsg, ex);
            else
                _logger.Error((object)errorMsg);
        }

        /// <summary>记录Info日志</summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public void Info(string msg, Exception ex = null)
        {
            if (ex != null)
                _logger.Info((object)msg, ex);
            else
                _logger.Info((object)msg);
        }

        /// <summary>记录Debug日志</summary>
        /// <param name="msg"></param>
        public void Debug(string msg)
        {
            _logger.Debug((object)msg);
        }

        /// <summary>
        /// 操作日志进入数据库(异步写入消息队列)
        /// </summary>
        /// <param name="entity"></param>
        public void InsertBusLogs(MonitorLog entity)
        {
            //await Task.Run(() =>
            //{
            //    _applicationEventBus.Publisher(new MonitorLogEvent(entity));               
            //});
        }

        #endregion

    }
}
