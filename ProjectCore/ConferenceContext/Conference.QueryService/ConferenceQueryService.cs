using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Conference.Common.Dapper;
using Conference.QueryService.Dto;
using Dapper;
namespace Conference.QueryService
{
    public class ConferenceQueryService : IConferenceQueryService
    {
        private readonly IDbConnection _connection = DapperConnection.DapperInstance();

        /// <summary>
        /// 获取会议列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<ConferenceDto>> GetConferenceList()
        {
            string sql = $"SELECT Id, ConferenceName, ConferenceContent, ConferenceAddress, ConferenceParticipantNum, ConferencePublishStatus, ConferenceStartTime, ConferenceEndTime, CustomerId,CreateTime FROM ConferenceInfo";
            var conferenceList=await _connection.QueryAsync<ConferenceDto>(sql);
            return conferenceList.ToList();
        }
    }
}
