using System;

namespace Conference.Common.Log
{
   public class MonitorLogEvent
    {

        public MonitorLogEvent()
        {

        }
        public MonitorLogEvent(MonitorLog input)
        {
            this.LogLevel = input.LogLevel;
            this.ActionName = input.ActionName;
            this.AddressIp = input.AddressIp;
            this.ControllerName = input.ControllerName;
            this.ErrorMsg = input.ErrorMsg;
            this.ExecuteEndTime = input.ExecuteEndTime;
            this.ExecuteStartTime = input.ExecuteStartTime;
            this.ExecutionTime = input.ExecutionTime;
            this.LogType = input.LogType;
            this.MonitorLogId = input.MonitorLogId;
            this.RequestParameters = input.RequestParameters;
            this.ActionName = input.ActionName;
            this.UserId = input.UserId;
            this.UserName = input.UserName;
        }
        #region 属性
        /// <summary>
        /// 业务关联Id
        /// </summary>
        public Guid MonitorLogId { get; set; }

        /// <summary>
        /// 控制器名称
        /// </summary>
        public string ControllerName { get; set; }

        /// <summary>
        /// 视图名称
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 请求参数
        /// </summary>
        public string RequestParameters { get; set; }

        /// <summary>
        /// 记录开始时间
        /// </summary>
        public DateTime ExecuteStartTime { get; set; }

        /// <summary>
        /// 记录结束时间
        /// </summary>
        public DateTime ExecuteEndTime { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public long ExecutionTime { get; set; }

        /// <summary>
        /// 异常错误信息
        /// </summary>
        public string ErrorMsg { get; set; }

        /// <summary>
        /// 日志类型
        /// </summary>
        public int LogType { get; set; }

        /// <summary>
        /// 日志级别
        /// </summary>
        public int LogLevel { get; set; }

        /// <summary>
        /// 访问者Ip
        /// </summary>
        public string AddressIp { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        #endregion

    }
}
