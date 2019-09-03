using System;

namespace Conference.Domain
{
    /// <summary>
    /// 领域事件
    /// </summary>
    public interface IDomainEvent
    {
        /// <summary>
        /// Id
        /// </summary>
        Guid Id { get; set; }

        /// <summary>
        /// 聚合根Id
        /// </summary>
        Guid AggregateRootId { get; set; }
        /// <summary>
        /// 聚合根类型
        /// </summary>
        string AggregateRootType { get; set; }

        /// <summary>
        /// 聚合根序列号，版本号
        /// </summary>
        long Version { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreateDateTime { get; set; }

        /// <summary>
        /// 事件数据
        /// </summary>
        string EventData { get; set; }

        /// <summary>
        /// 获取路由key
        /// </summary>
        string GetRoutingKey();
    }
}
