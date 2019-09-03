using System;
using System.Collections.Generic;
using System.Text;

namespace Conference.Command.Command
{
    /// <summary>
    /// 创建会议Command
    /// </summary>
    public class CreateConferenceCommand : ConferenceCommand
    {
        public CreateConferenceCommand(Guid aggregateRootId, string conferenceName, string conferenceContent, string conferenceAddress, int conferenceParticipantNum, DateTime conferenceStartTime, DateTime conferenceEndTime, Guid customerId)
        {
            AggregateRootId = aggregateRootId;
            ConferenceName = conferenceName;
            ConferenceContent = conferenceContent;
            ConferenceAddress = conferenceAddress;
            ConferenceParticipantNum = conferenceParticipantNum;
            ConferenceStartTime = conferenceStartTime;
            ConferenceEndTime = conferenceEndTime;
            CustomerId = customerId;
        }

        /// <summary>
        /// 会议名称
        /// </summary>
        public string ConferenceName { get; set; }

        /// <summary>
        /// 会议内容
        /// </summary>
        public string ConferenceContent { get; set; }

        /// <summary>
        /// 会议地址
        /// </summary>
        public string ConferenceAddress { get; set; }

        /// <summary>
        /// 会议参与人数
        /// </summary>
        public int ConferenceParticipantNum { get; set; }


        /// <summary>
        /// 会议开始时间
        /// </summary>
        public DateTime ConferenceStartTime { get; set; }

        /// <summary>
        /// 会议结束时间
        /// </summary>
        public DateTime ConferenceEndTime { get; set; }

        /// <summary>
        /// 客户Id
        /// </summary>
        public Guid CustomerId { get; set; }
    }
}
