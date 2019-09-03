using System.Collections.Generic;

namespace Conference.Domain
{
    /// <summary>
    /// 聚合根
    /// </summary>
    public interface IAggregationRoot : IEntity
    {
        /// <summary>
        /// 版本号
        /// </summary>
        long Version { get; }

        /// <summary>
        /// 未提交的事件
        /// </summary>
        IEnumerable<IDomainEvent> UncommittedEvents { get; }

        /// <summary>
        /// 重播事件
        /// </summary>
        /// <param name="events"></param>
        void ReplayEvent(IEnumerable<IDomainEvent> events);

        /// <summary>
        /// 清空事件
        /// </summary>
        void ClearEvents();

    }
}
