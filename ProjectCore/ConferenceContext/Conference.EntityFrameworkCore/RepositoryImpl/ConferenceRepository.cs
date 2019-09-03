using System;
using System.Threading.Tasks;
using Conference.Domain.Entity;
using Conference.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Conference.EntityFrameworkCore.RepositoryImpl
{
    /// <summary>
    /// 会议仓储
    /// </summary>
    public class ConferenceRepository : BaseRepository<ConferenceInfo>, IConferenceRepository
    {
        private readonly ConferenceContext _conferenceContext;
        public ConferenceRepository(ConferenceContext conferenceContext) : base(conferenceContext)
        {
            _conferenceContext = conferenceContext;
        }

        /// <summary>
        /// 保存会议
        /// </summary>
        /// <returns></returns>
        public async Task SaveConference(ConferenceInfo input)
        {           
            await _conferenceContext.ConferenceInfo.AddAsync(input);
        }

        /// <summary>
        /// 修改会议
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task ModifyConference(ConferenceInfo input)
        {
            _conferenceContext.ConferenceInfo.Update(input);
            await Task.CompletedTask;
        }

        /// <summary>
        /// 获取会议聚合根
        /// </summary>
        /// <param name="conferenceId"></param>
        /// <returns></returns>
        public async Task<ConferenceInfo> GetConference(Guid conferenceId)
        {
            var conferenceInfo = await _conferenceContext.ConferenceInfo.Include(e => e.SeatTypeList).ThenInclude(e => e.SeatList)
                 .FirstOrDefaultAsync(e => e.Id == conferenceId);
            return conferenceInfo;
        }
    }
}
