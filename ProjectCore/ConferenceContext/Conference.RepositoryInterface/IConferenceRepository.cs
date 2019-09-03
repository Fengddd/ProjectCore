using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Conference.Domain.Entity;

namespace Conference.RepositoryInterface
{
    /// <summary>
    /// 会议仓储
    /// </summary>
    public interface IConferenceRepository : IBaseRepository<ConferenceInfo>
    {
        /// <summary>
        /// 保存会议
        /// </summary>
        /// <returns></returns>
        Task SaveConference(ConferenceInfo input);

        /// <summary>
        /// 修改会议
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task ModifyConference(ConferenceInfo input);

        /// <summary>
        ///  获取会议聚合根
        /// </summary>
        /// <param name="conferenceId"></param>
        /// <returns></returns>
        Task<ConferenceInfo> GetConference(Guid conferenceId);
    }
}
