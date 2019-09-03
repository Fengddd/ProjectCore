using System;

namespace Conference.Domain.DomainEvent
{
    /// <summary>
    /// 领域事件
    /// </summary>
    public class DomainEvent : IDomainEvent
    {
        public DomainEvent()
        {
            this.Id = Guid.NewGuid();
        }

        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 聚合跟
        /// </summary>
        public Guid AggregateRootId { get; set; }

        /// <summary>
        /// 聚合根类型
        /// </summary>
        public string AggregateRootType { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDateTime { get; set; }

        /// <summary>
        /// 事件版本号
        /// </summary>
        public long Version { get; set; }

        /// <summary>
        /// 事件数据
        /// </summary>
        public string EventData { get; set; }

        /// <summary>
        /// 获取路由key
        /// </summary>
        public string GetRoutingKey()
        {
            return this.GetType().Name;
        }
    }
}
