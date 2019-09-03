using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Conference.QueryService.Dto;

namespace Conference.QueryService
{
    /// <summary>
    /// 会议接口
    /// </summary>
    public interface IConferenceQueryService
    {
        /// <summary>
        /// 获取会议列表
        /// </summary>
        /// <returns></returns>
        Task<List<ConferenceDto>> GetConferenceList();
    }
}
