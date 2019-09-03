using System;
using Conference.Domain;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Conference.Common.Dapper;
using Dapper;
using DotNetCore.CAP;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Polly;

namespace Conference.Common
{
    public class PublishDomainEventService : IPublishDomainEventService
    {
        private readonly ICapPublisher _capPublisher;
        private readonly IDbConnection _connection = DapperConnection.DapperInstance();
        public PublishDomainEventService(ICapPublisher capPublisher)
        {
            _capPublisher = capPublisher;
        }

        /// <summary>
        /// 发布事件，储存事件
        /// </summary>
        /// <typeparam name="TAggregationRoot"></typeparam>
        /// <param name="event"></param>
        public void PublishEvent<TAggregationRoot>(TAggregationRoot @event) where TAggregationRoot : IAggregationRoot
        {
            var domainEventList = @event.UncommittedEvents.ToList();
            TryDapperConnection();
            using (var transaction = _connection.BeginTransaction())
            {
                if (domainEventList.Count > 0)
                {
                    foreach (var domainEvent in domainEventList)
                    {
                        EventStorage eventStorage = new EventStorage
                        {
                            Id = domainEvent.Id,
                            AggregateRootId = domainEvent.AggregateRootId,
                            AggregateRootType = domainEvent.AggregateRootType,
                            CreateDateTime = domainEvent.CreateDateTime,
                            Version = domainEvent.Version,
                            EventData = JsonConvert.SerializeObject(domainEvent)
                        };
                        var eventStorageSql = $"INSERT INTO EventStorageInfo(Id,AggregateRootId,AggregateRootType,CreateDateTime,Version,EventData) VALUES (@Id,@AggregateRootId,@AggregateRootType,@CreateDateTime,@Version,@EventData)";
                        _connection.Execute(eventStorageSql, eventStorage, transaction);
                        _capPublisher.Publish(domainEvent.GetRoutingKey(), domainEvent);
                    }
                }
                transaction.Commit();
                _connection.Close();
                @event.ClearEvents();
            }
        }

        /// <summary>
        /// 异步发布事件，储存事件
        /// </summary>
        /// <typeparam name="TAggregationRoot"></typeparam>
        /// <param name="event"></param>
        /// <returns></returns>
        public async Task PublishEventAsync<TAggregationRoot>(TAggregationRoot @event) where TAggregationRoot : IAggregationRoot
        {
            var domainEventList = @event.UncommittedEvents.ToList();
            TryDapperConnection();
            using (var transaction = _connection.BeginTransaction())
            {
                if (domainEventList.Count > 0)
                {
                    foreach (var domainEvent in domainEventList)
                    {
                        EventStorage eventStorage = new EventStorage
                        {
                            Id = domainEvent.Id,
                            AggregateRootId = domainEvent.AggregateRootId,
                            AggregateRootType = domainEvent.AggregateRootType,
                            CreateDateTime = domainEvent.CreateDateTime,
                            Version = domainEvent.Version,
                            EventData = JsonConvert.SerializeObject(domainEvent)
                        };
                        var eventStorageSql = $"INSERT INTO EventStorageInfo(Id,AggregateRootId,AggregateRootType,CreateDateTime,Version,EventData) VALUES (@Id,@AggregateRootId,@AggregateRootType,@CreateDateTime,@Version,@EventData)";
                        await _connection.ExecuteAsync(eventStorageSql, eventStorage, transaction);
                        await _capPublisher.PublishAsync(domainEvent.GetRoutingKey(), domainEvent);
                    }
                }
                transaction.Commit();
                _connection.Close();
                @event.ClearEvents();
            }
        }


        /// <summary>
        /// Dapper连接异常重试
        /// </summary>
        public void TryDapperConnection()
        {
            var policy = Policy.Handle<SocketException>().Or<InvalidOperationException>()
                .WaitAndRetry(5, p => TimeSpan.FromSeconds(1), (ex, time) =>
                {
                    //记录错误日志
                });
            policy.Execute(() =>
            {
                _connection.Open();
            });
        }

    }
}
