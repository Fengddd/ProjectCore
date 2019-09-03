using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Conference.Domain.Impl
{
    /// <summary>
    /// 聚合根
    /// </summary>
    public class AggregationRoot<TAggregateRootIdType> : IAggregationRoot
    {
        private readonly Lazy<Dictionary<string, MethodInfo>> _registeredHandlers;
        private readonly Queue<IDomainEvent> _uncommittedEvents = new Queue<IDomainEvent>();
        private readonly object _sync = new object();
        protected long _version;
        /// <summary>
        /// 有参构造函数
        /// </summary>
        public AggregationRoot(Guid aggregateRootId)
        {
            this.Id = aggregateRootId;
            _registeredHandlers = new Lazy<Dictionary<string, MethodInfo>>(() =>
            {
                var registry = new Dictionary<string, MethodInfo>();
                var methodInfoList = from mi in this.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                     let returnType = mi.ReturnType
                                     let parameters = mi.GetParameters()
                                     where mi.Name == "Handle" &&
                                           returnType == typeof(void) &&
                                           parameters.Length == 1 &&
                                           typeof(IDomainEvent).IsAssignableFrom(parameters[0].ParameterType)
                                     select new { EventName = parameters[0].ParameterType.FullName, MethodInfo = mi };

                foreach (var methodInfo in methodInfoList)
                {
                    registry.Add(methodInfo.EventName, methodInfo.MethodInfo);
                }
                return registry;
            });
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public AggregationRoot() : this(Guid.NewGuid())
        {

        }
       

        /// <summary>
        /// 聚合根Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 聚合根版本号
        /// </summary>

        public long Version
        {
            get => this._version + 1;
            set { }
        }


        /// <summary>
        /// 未提交的事件
        /// </summary>
        public IEnumerable<IDomainEvent> UncommittedEvents => this._uncommittedEvents;

        /// <summary>
        /// 清空聚合跟中的事件
        /// </summary>
        public void ClearEvents()
        {
            lock (_sync)
            {
                _uncommittedEvents.Clear();
            }
        }

        /// <summary>
        /// 重播聚合跟中的事件
        /// </summary>
        /// <param name="events"></param>
        public void ReplayEvent(IEnumerable<IDomainEvent> events)
        {
            ClearEvents();
            events.OrderBy(e => e.CreateDateTime)
                .ToList()
                .ForEach(e =>
                {
                    HandleEvent(e);
                    this.Version = e.Version;
                });
        }

        /// <summary>
        /// 使用领域事件
        /// </summary>
        /// <typeparam name="TDomainEvent"></typeparam>
        /// <param name="domainEvent"></param>
        protected void ApplyEvent<TDomainEvent>(TDomainEvent domainEvent)
            where TDomainEvent : IDomainEvent
        {
            lock (_sync)
            {
                // 然后设置事件的元数据，包括当前事件所对应的聚合根类型以及聚合的ID值。
                domainEvent.AggregateRootId = this.Id;
                domainEvent.AggregateRootType = this.GetType().FullName;
                domainEvent.Version = this.Version;
                domainEvent.CreateDateTime = DateTime.Now;
                // 首先处理事件数据。
                this.HandleEvent(domainEvent);
                // 最后将事件缓存在“未提交事件”列表中。
                this._uncommittedEvents.Enqueue(domainEvent);
            }
        }

        /// <summary>
        /// 反射调用聚合上的Handle方法
        /// </summary>
        /// <typeparam name="TDomainEvent"></typeparam>
        /// <param name="domainEvent"></param>
        private void HandleEvent<TDomainEvent>(TDomainEvent domainEvent)
            where TDomainEvent : IDomainEvent
        {
            var key = domainEvent.GetType().FullName;
            if (_registeredHandlers.Value.ContainsKey(key))
            {
                _registeredHandlers.Value[key].Invoke(this, new object[] { domainEvent });
            }
        }


    }
}
