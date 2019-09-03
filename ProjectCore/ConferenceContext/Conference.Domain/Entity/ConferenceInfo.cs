using System;
using System.Collections.Generic;
using System.Linq;
using Conference.Domain.Event;
using Conference.Domain.Impl;

namespace Conference.Domain.Entity
{
    /// <summary>
    /// 会议
    /// </summary>
    public class ConferenceInfo : AggregationRoot<Guid>
    {

        public ConferenceInfo(string conferenceName, string conferenceContent, string conferenceAddress, int conferenceParticipantNum, DateTime conferenceStartTime, DateTime conferenceEndTime, Guid customerId):base(Guid.NewGuid())
        {
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

        public ConferenceInfo(Guid id) :base(id)
        {

        }

        public ConferenceInfo()
        {

        }

        #region 属性

        /// <summary>
        /// 会议名称
        /// </summary>
        public string ConferenceName { get; private set; }

        /// <summary>
        /// 会议内容
        /// </summary>
        public string ConferenceContent { get; private set; }

        /// <summary>
        /// 会议地址
        /// </summary>
        public string ConferenceAddress { get; private set; }

        /// <summary>
        /// 会议参与人数
        /// </summary>
        public int ConferenceParticipantNum { get; private set; }

        /// <summary>
        /// 会议状态
        /// </summary>
        public bool ConferencePublishStatus { get; private set; }

        /// <summary>
        /// 会议开始时间
        /// </summary>
        public DateTime ConferenceStartTime { get; private set; }

        /// <summary>
        /// 会议结束时间
        /// </summary>
        public DateTime ConferenceEndTime { get; private set; }

        /// <summary>
        /// 客户Id
        /// </summary>
        public Guid CustomerId { get; private set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; private set; }

        /// <summary>
        /// 会议座位类型
        /// </summary>
        public List<SeatType> SeatTypeList { get; set; }

        #endregion
     
        /// <summary>
        /// 创建会议
        /// </summary>
        /// <returns></returns>
        public void CreateConference(string conferenceName, string conferenceContent, string conferenceAddress, int conferenceParticipantNum, DateTime conferenceStartTime, DateTime conferenceEndTime, Guid customerId)
        {
            var createConference = new CreateConference(this.Id, conferenceName, conferenceContent, conferenceAddress, conferenceParticipantNum, conferenceStartTime, conferenceEndTime, customerId);
            ApplyEvent(new CreateConferenceEvent(createConference));
        }

        /// <summary>
        /// 发布会议
        /// </summary>
        public void PublishConference()
        {
            if (this.ConferencePublishStatus)
            {
                throw new DomainException("只有未发布的会议才能进行发布会议！");
            }
            ApplyEvent(new PublishConferenceEvent(true));
        }

        /// <summary>
        /// 取消发布会议
        /// </summary>
        public void UnPublishConference()
        {
            if (!this.ConferencePublishStatus)
            {
                throw new DomainException("只有已发布的会议才能进行取消会议！");
            }
            ApplyEvent(new UnPublishConferenceEvent(false));
        }

        /// <summary>
        /// 添加座位类型
        /// </summary>
        public void AddSeatType(List<SeatType> seatTypeList)
        {
            var seatTypeTotalNum = 0;
            List<SeatType> list = new List<SeatType>();
            foreach (var item in seatTypeList)
            {
                seatTypeTotalNum += item.SeatTypeNum;
                var number = list.Sum(e => e.SeatList.Count);
                var seatType = new SeatType(item.SeatTypeName, item.SeatTypePrice, item.SeatTypeNum, this.CustomerId, number);
                list.Add(seatType);
            }
            if (this.ConferenceParticipantNum < seatTypeTotalNum)
            {
                throw new DomainException("会议的座位类型数量不能超过会议的参与人数量！");
            }
            ApplyEvent(new AddSeatTypeEvent(list));
        }

        /// <summary>
        /// 创建会议
        /// </summary>
        /// <param name="createConferenceEvent"></param>
        public void Handle(CreateConferenceEvent createConferenceEvent)
        {
            this.Id = createConferenceEvent.AggregateRootId;
            this.ConferenceAddress = createConferenceEvent.CreateConferenceInfo.ConferenceAddress;
            this.ConferenceContent = createConferenceEvent.CreateConferenceInfo.ConferenceContent;
            this.ConferenceEndTime = createConferenceEvent.CreateConferenceInfo.ConferenceEndTime;
            this.ConferenceName = createConferenceEvent.CreateConferenceInfo.ConferenceName;
            this.ConferenceParticipantNum = createConferenceEvent.CreateConferenceInfo.ConferenceParticipantNum;
            this.ConferencePublishStatus= createConferenceEvent.CreateConferenceInfo.ConferencePublishStatus;
            this.ConferenceStartTime= createConferenceEvent.CreateConferenceInfo.ConferenceStartTime;
            this.CreateTime= createConferenceEvent.CreateConferenceInfo.CreateTime;
            this.CustomerId= createConferenceEvent.CreateConferenceInfo.CustomerId;
            this.SeatTypeList=new List<SeatType>();
        }

        /// <summary>
        /// 发布会议
        /// </summary>
        /// <param name="publishConferenceEvent"></param>
        public void Handle(PublishConferenceEvent publishConferenceEvent)
        {
            this.ConferencePublishStatus = publishConferenceEvent.ConferencePublishStatus;
        }

        /// <summary>
        /// 取消会议
        /// </summary>
        /// <param name="publishConferenceEvent"></param>
        public void Handle(UnPublishConferenceEvent publishConferenceEvent)
        {
            this.ConferencePublishStatus = publishConferenceEvent.ConferencePublishStatus;
        }

        /// <summary>
        /// 添加座位类型
        /// </summary>
        /// <param name="addSeatTypeEvent"></param>
        public void Handle(AddSeatTypeEvent addSeatTypeEvent)
        {
            this.SeatTypeList = addSeatTypeEvent.SeatTypeList;
        }

    }

}
