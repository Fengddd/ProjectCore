using System;
using System.Collections.Generic;
using System.Text;

namespace Conference.Command
{
    public class ConferenceCommand : ICommand
    {
        /// <summary>
        /// 聚合根Id
        /// </summary>
        public Guid AggregateRootId { get; set; }

        /// <summary>
        /// 获取路由key
        /// </summary>
        public string GetRoutingKey()
        {
            return this.GetType().Name;
        }
    }
}
