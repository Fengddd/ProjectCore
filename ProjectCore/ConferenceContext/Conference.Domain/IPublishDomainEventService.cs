using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Conference.Domain
{
    public interface IPublishDomainEventService
    {
        /// <summary>
        /// 发布事件，储存事件
        /// </summary>
        /// <typeparam name="TAggregationRoot"></typeparam>
        /// <param name="event"></param>
        void PublishEvent<TAggregationRoot>(TAggregationRoot @event) where TAggregationRoot : IAggregationRoot;

        /// <summary>
        /// 异步发布事件，储存事件
        /// </summary>
        /// <typeparam name="TAggregationRoot"></typeparam>
        /// <param name="event"></param>
        /// <returns></returns>
        Task PublishEventAsync<TAggregationRoot>(TAggregationRoot @event) where TAggregationRoot : IAggregationRoot;
    }
}
