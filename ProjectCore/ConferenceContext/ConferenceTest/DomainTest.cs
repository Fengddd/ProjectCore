using Conference.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ConferenceTest
{
    public class DomainTest
    {
        [Fact]
        public void CreateConferenceTest()
        {
            var conferenceId = Guid.NewGuid();
            var conference = new ConferenceInfo("测试会议", "测试", "测试地址", 40, DateTime.Now, DateTime.Now.AddDays(10), conferenceId);
            conference.CreateConference("测试会议", "测试", "测试地址", 40, DateTime.Now, DateTime.Now.AddDays(10), conferenceId);
            Assert.NotNull(conference);
        }
    }
}
