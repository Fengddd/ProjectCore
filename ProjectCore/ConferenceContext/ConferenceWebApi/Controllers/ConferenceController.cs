using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Conference.Command;
using Conference.Command.Command;
using Conference.Common;
using Conference.QueryService;
using Conference.QueryService.Dto;
using ConferenceWebApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceWebApi.Controllers
{
    /// <summary>
    /// 会议
    /// </summary>
    [Route("api/[controller]/[Action]")]
    [ApiController]
    [EnableCors("any")]
    public class ConferenceController : ControllerBase
    {
        private readonly ICommandService _commandService;
        private readonly IConferenceQueryService _conferenceQueryService;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="commandService"></param>
        /// <param name="conferenceQueryService"></param>
        public ConferenceController(ICommandService commandService, IConferenceQueryService conferenceQueryService)
        {
            _commandService = commandService;
            _conferenceQueryService = conferenceQueryService;
        }

        /// <summary>
        /// 创建会议
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task CreateConference([FromBody] CreateConference input)
        {
            var createConferenceCommand = new CreateConferenceCommand(Guid.NewGuid(), input.ConferenceName, input.ConferenceContent, input.ConferenceAddress, input.ConferenceParticipantNum, input.ConferenceStartTime, input.ConferenceEndTime, Guid.NewGuid());
            await _commandService.SendCommandAsync(createConferenceCommand);
        }

        /// <summary>
        /// 添加座位类型
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task AddSeatType(Guid conferenceId)
        {
            var addSeatTypeCommand = new AddSeatTypeCommand(conferenceId);
            await _commandService.SendCommandAsync(addSeatTypeCommand);
        }

        /// <summary>
        /// 发布会议
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task PublishConference(Guid conferenceId)
        {
            var publishConferenceCommand = new PublishConferenceCommand(conferenceId);
            await _commandService.SendCommandAsync(publishConferenceCommand);
        }

        /// <summary>
        /// 获取会议列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<ConferenceDto>> GetConferenceList()
        {
            return await _conferenceQueryService.GetConferenceList();
        }

    }
}