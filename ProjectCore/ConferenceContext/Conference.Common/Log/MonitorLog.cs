using System;
using System.ComponentModel.DataAnnotations;

namespace Conference.Common.Log
{
    /// <summary>
    /// 业务日志实体对象
    /// </summary>
    public class MonitorLog
    {

        public MonitorLog()
        {

        }
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Key]
        public int Id { get; set; }
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
    }
}
