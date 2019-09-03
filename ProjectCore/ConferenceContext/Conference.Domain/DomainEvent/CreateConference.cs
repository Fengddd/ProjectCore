using System;
using System.Collections.Generic;
using Conference.Domain.Entity;

namespace Conference.Domain.DomainEvent
{
    public class CreateConference
    {
        #region 属性

        public CreateConference(Guid id, string conferenceName, string conferenceContent, string conferenceAddress, int conferenceParticipantNum, DateTime conferenceStartTime, DateTime conferenceEndTime, Guid customerId)
        {
            Id = id;
            ConferenceName = conferenceName;
            ConferenceContent = conferenceContent;
            ConferenceAddress = conferenceAddress;
            ConferenceParticipantNum = conferenceParticipantNum;
            ConferencePublishStatus = false;
            ConferenceStartTime = conferenceStartTime;
            ConferenceEndTime = conferenceEndTime;
            CustomerId = customerId;
            CreateTime = DateTime.Now;
            SeatTypeList = new List<SeatType>();
        }

        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

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
        public string ConferenceAddress { get;  set; }

        /// <summary>
        /// 会议参与人数
        /// </summary>
        public int ConferenceParticipantNum { get;  set; }

        /// <summary>
        /// 会议状态
        /// </summary>
        public bool ConferencePublishStatus { get;  set; }

        /// <summary>
        /// 会议开始时间
        /// </summary>
        public DateTime ConferenceStartTime { get;  set; }

        /// <summary>
        /// 会议结束时间
        /// </summary>
        public DateTime ConferenceEndTime { get;  set; }

        /// <summary>
        /// 客户Id
        /// </summary>
        public Guid CustomerId { get;  set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get;  set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public long Version { get; set; }

        /// <summary>
        /// 会议座位类型
        /// </summary>
        public List<SeatType> SeatTypeList { get; set; }

        #endregion

    }
}
