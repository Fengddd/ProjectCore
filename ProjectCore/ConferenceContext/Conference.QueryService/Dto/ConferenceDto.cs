using System;
using System.Collections.Generic;
using System.Text;
using Conference.Domain.Entity;

namespace Conference.QueryService.Dto
{
    /// <summary>
    /// 会议Dto
    /// </summary>
    public class ConferenceDto
    {
        #region 属性

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
        /// 会议状态
        /// </summary>
        public bool ConferencePublishStatus { get; set; }

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

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 会议座位类型
        /// </summary>
        public List<SeatType> SeatTypeList { get; set; }

        #endregion
    }
}
