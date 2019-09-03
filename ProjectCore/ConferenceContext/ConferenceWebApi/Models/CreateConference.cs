using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceWebApi.Models
{
    /// <summary>
    /// 创建会议
    /// </summary>
    public class CreateConference
    {
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
