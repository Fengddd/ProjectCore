using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Conference.Common;
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
        /// <summary>
        /// CAP测试
        /// </summary>
        /// <param name="capBus"></param>
        /// <param name="publishDomainEvent"></param>
        public CapTestController(ICapPublisher capBus, IPublishDomainEventService publishDomainEvent)
        {
            _capBus = capBus;
            _publishDomainEvent = publishDomainEvent;
        }

        /// <summary>
        /// ddd
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
        /// dddd
        /// </summary>
        [HttpPost]
        //[CapSubscribe("Create111", Group = "Create111")]
        public void GetSubscribeTest()
        {
           
                var a = 0;
                var c = 0;
                var p = a / c;
       
        }

    }  
}