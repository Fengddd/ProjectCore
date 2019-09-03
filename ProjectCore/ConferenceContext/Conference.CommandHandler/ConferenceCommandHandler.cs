using Conference.Command.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Conference.Common.Log;
using DotNetCore.CAP;
using Conference.Domain.Entity;
using Conference.Domain;
using Conference.RepositoryInterface;
using Exceptionless;

namespace Conference.CommandHandler
{
    /// <summary>
    /// 会议CommandHandler
    /// </summary>
    public class ConferenceCommandHandler :
        IConferenceCommandHandler,
        ICapSubscribe,
        ICommandHandler<CreateConferenceCommand>,
        ICommandHandler<AddSeatTypeCommand>,
        ICommandHandler<PublishConferenceCommand>
    {
        private readonly IPublishDomainEventService _publishDomainEvent;
        private readonly IConferenceRepository _conferenceRepository;

        public ConferenceCommandHandler(IPublishDomainEventService publishDomainEvent, IConferenceRepository conferenceRepository)
        {
            _publishDomainEvent = publishDomainEvent;
            _conferenceRepository = conferenceRepository;
        }

        /// <summary>
        /// 创建会议命令
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [CapSubscribe(nameof(CreateConferenceCommand), Group = nameof(CreateConferenceCommand))]
        public async Task HandleAsync(CreateConferenceCommand command)
        {
            try
            {
                var conference = new ConferenceInfo(Guid.NewGuid());
                conference.CreateConference(command.ConferenceName, command.ConferenceContent, command.ConferenceAddress, command.ConferenceParticipantNum, command.ConferenceStartTime, command.ConferenceEndTime, command.CustomerId);
                await _publishDomainEvent.PublishEventAsync(conference);
            }
            catch (Exception e)
            {
                e.ToExceptionless().Submit();               
                throw;
            }
        }

        /// <summary>
        /// 添加座位类型
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [CapSubscribe(nameof(AddSeatTypeCommand), Group = nameof(AddSeatTypeCommand))]
        public async Task HandleAsync(AddSeatTypeCommand command)
        {
            var conference = await _conferenceRepository.GetConference(command.AggregateRootId);
            List<SeatType> seatTypeList = new List<SeatType>();
            var seatType = new SeatType("舒适型", 60, 20, command.AggregateRootId);
            var seatType1 = new SeatType("豪华型", 80, 10, command.AggregateRootId);
            seatTypeList.Add(seatType);
            seatTypeList.Add(seatType1);
            conference.AddSeatType(seatTypeList);
            await _conferenceRepository.ModifyConference(conference);
            await _conferenceRepository.CommitAsync();
            //await _publishDomainEvent.PublishEventAsync(conference);
        }

        /// <summary>
        /// 发布会议
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [CapSubscribe(nameof(PublishConferenceCommand), Group = nameof(PublishConferenceCommand))]
        public async Task HandleAsync(PublishConferenceCommand command)
        {
            var conference = await _conferenceRepository.GetConference(command.AggregateRootId);
            conference.PublishConference();
            await _publishDomainEvent.PublishEventAsync(conference);
        }
    }
}
