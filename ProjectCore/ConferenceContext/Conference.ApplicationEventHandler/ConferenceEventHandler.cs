using System;
using System.Data;
using System.Threading.Tasks;
using Conference.Common;
using Conference.Common.Dapper;
using Conference.Domain.Entity;
using Conference.Domain.Event;
using Conference.RepositoryInterface;
using Dapper;
using DotNetCore.CAP;

namespace Conference.ApplicationEventHandler
{
    /// <summary>
    /// 会议EventHandler
    /// </summary>
    public class ConferenceEventHandler :
        IEventHandler<CreateConferenceEvent>,
        IEventHandler<AddSeatTypeEvent>,
        IEventHandler<PublishConferenceEvent>,
        IConferenceEventHandler,
        ICapSubscribe
    {
        private readonly IConferenceRepository _conferenceRepository;
        private readonly IDbConnection _connection = DapperConnection.DapperInstance();
        public ConferenceEventHandler(IConferenceRepository conferenceRepository)
        {
            _conferenceRepository = conferenceRepository;
        }

        /// <summary>
        /// 创建会议
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [CapSubscribe(nameof(CreateConferenceEvent), Group = nameof(CreateConferenceEvent))]
        public async Task HandleAsync(CreateConferenceEvent input)
        {
            var conferenceInfo = input.CreateConferenceInfo.MapTo<CreateConference, ConferenceInfo>();
            await _conferenceRepository.SaveConference(conferenceInfo);
            await _conferenceRepository.CommitAsync();
        }

        /// <summary>
        /// 添加座位类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [CapSubscribe(nameof(AddSeatTypeEvent), Group = nameof(AddSeatTypeEvent))]
        public async Task HandleAsync(AddSeatTypeEvent input)
        {
            await Task.CompletedTask;
        }

        /// <summary>
        /// 发布会议
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [CapSubscribe(nameof(PublishConferenceEvent), Group = nameof(PublishConferenceEvent))]
        public async Task HandleAsync(PublishConferenceEvent input)
        {
            string sql = $"UPDATE ConferenceInfo SET ConferencePublishStatus=@ConferencePublishStatus WHERE Id=@Id";
            await _connection.ExecuteAsync(sql, new { ConferencePublishStatus = input.ConferencePublishStatus, Id = input.AggregateRootId });
        }
    }
}
