using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Conference.Common;
using Conference.Common.Redis;
using Conference.Domain;
using Conference.Domain.Entity;
using DotNetCore.CAP;
using Exceptionless;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceWebApi.Controllers
{
    /// <summary>
    /// CAP测试控制器
    /// </summary>
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class CapTestController : ControllerBase
    {
        private readonly ICapPublisher _capBus;
        private readonly IPublishDomainEventService _publishDomainEvent;
        private readonly IRedisHelper _redisHelper;
        /// <summary>
        /// CAP测试
        /// </summary>
        /// <param name="capBus"></param>
        /// <param name="publishDomainEvent"></param>
        public CapTestController(ICapPublisher capBus, IPublishDomainEventService publishDomainEvent, IRedisHelper redisHelper)
        {
            _capBus = capBus;
            _redisHelper = redisHelper;
            _publishDomainEvent = publishDomainEvent;
        }

        /// <summary>
        /// CAP发布
        /// </summary>
        [HttpPost]
        public void GetPublishTest()
        {
            var conferenceId = Guid.NewGuid();
            var conference = new ConferenceInfo("测试会议", "测试", "测试地址", 40, DateTime.Now, DateTime.Now.AddDays(10), conferenceId);
            //List<SeatType> seatTypeList = new List<SeatType>();
            //var seatType = new SeatType("舒适型", 60, 30, conferenceId);
            //var seatType1 = new SeatType("豪华型", 80, 10, conferenceId);
            //seatTypeList.Add(seatType);
            //seatTypeList.Add(seatType1);
            //conference.AddSeatType(seatTypeList);

            conference.CreateConference("测试会议", "测试", "测试地址", 40, DateTime.Now, DateTime.Now.AddDays(10), conferenceId);
          
            //_publishDomainEvent.PublishEvent(conference);
          
        
        }

        /// <summary>
        /// CAP订阅
        /// </summary>
        [HttpPost]
        //[CapSubscribe("Create111", Group = "Create111")]
        public void GetSubscribeTest()
        {
           
                var a = 0;
                var c = 0;
                var p = a / c;
       
        }

        /// <summary>
        /// Redis测试
        /// </summary>
        [HttpPost]
        public void RedisTest()
        {
            var customerList = CustomerList();
            var isTrue = _redisHelper.StringSet("StringSetList", customerList);
            var list = _redisHelper.GetString<List<Customer>>("StringSetList");
        }

        /// <summary>
        /// 客户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public List<Customer> CustomerList()
        {
            return new List<Customer>()
            {
                new Customer(){Id = Guid.NewGuid().ToString(),CustomerName = "李四",CustomerAge = 18,CustomerPhone="1587954569"},
                new Customer(){Id = Guid.NewGuid().ToString(),CustomerName = "王五",CustomerAge = 19,CustomerPhone="1587954889"},
                new Customer(){Id = Guid.NewGuid().ToString(),CustomerName = "李锋",CustomerAge = 20,CustomerPhone="1587954559"},
                new Customer(){Id = Guid.NewGuid().ToString(),CustomerName = "小白",CustomerAge = 21,CustomerPhone="1587954669"}
            };
        }


    }

    /// <summary>
    /// 客户
    /// </summary>
    public class Customer
    {
        public string Id { get; set; }
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 客户年龄
        /// </summary>
        public int CustomerAge { get; set; }
        /// <summary>
        /// 客户电话
        /// </summary>
        public string CustomerPhone { get; set; }

    }

}