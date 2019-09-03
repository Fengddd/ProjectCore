using System;
using System.Collections.Generic;
using System.Text;

namespace Conference.Domain.Entity
{
    /// <summary>
    /// 座位类型
    /// </summary>
    public class SeatType : IEntity
    {
        public SeatType()
        {

        }

        public SeatType(string seatTypeName, decimal seatTypePrice, int seatTypeNum, Guid conferenceId, int number)
        {
            //客户定义座位类型的数量，不能超过会议的参会人数
            //座位编号，随机生成编号，不允许出现重复编号，最大编号不能超过会议参会人数            
            Id = Guid.NewGuid();
            SeatTypeName = seatTypeName;
            SeatTypePrice = seatTypePrice;
            SeatTypeNum = seatTypeNum;
            ConferenceId = conferenceId;
            var seatList = new List<Seat>();
            for (var i = 1; i <= seatTypeNum; i++)
            {
                var seat = new Seat(number + i, this.Id);
                seatList.Add(seat);
            }
            SeatList = seatList;
        }

        public SeatType(string seatTypeName, decimal seatTypePrice, int seatTypeNum, Guid conferenceId)
        {
            //客户定义座位类型的数量，不能超过会议的参会人数
            //座位编号，随机生成编号，不允许出现重复编号，最大编号不能超过会议参会人数            
            Id = Guid.NewGuid();
            SeatTypeName = seatTypeName;
            SeatTypePrice = seatTypePrice;
            SeatTypeNum = seatTypeNum;
            ConferenceId = conferenceId;
            var seatList = new List<Seat>();
            SeatList = seatList;
        }

        /// <summary>
        /// 座位类型Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 座位类型名称
        /// </summary>
        public string SeatTypeName { get; private set; }

        /// <summary>
        /// 座位类型价格
        /// </summary>
        public decimal SeatTypePrice { get; private set; }

        /// <summary>
        /// 座位类型数量
        /// </summary>
        public int SeatTypeNum { get; private set; }

        /// <summary>
        /// 会议Id
        /// </summary>
        public Guid ConferenceId { get; private set; }

        /// <summary>
        /// 会议
        /// </summary>
        public ConferenceInfo ConferenceInfo { get; set; }

        /// <summary>
        /// 座位
        /// </summary>
        public List<Seat> SeatList { get;set; }
    }

}
