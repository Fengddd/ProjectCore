using System;
using System.Collections.Generic;
using System.Text;

namespace Conference.Command
{
    /// <summary>
    /// 命令
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// 聚合根Id
        /// </summary>
        Guid AggregateRootId { get; set; }

        /// <summary>
        /// 获取路由key
        /// </summary>
        string GetRoutingKey();
    }
}
