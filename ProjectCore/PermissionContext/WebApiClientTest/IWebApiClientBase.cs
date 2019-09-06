using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ProjectCore.Application.AppUser.Dto;
using ProjectCore.Common;
using WebApiClient;
using WebApiClient.Attributes;

namespace WebApiClientTest
{
    //[HttpHost("https://localhost:44376/")]
   public interface IWebApiClientBase: IHttpApi
    {
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("api/User/GetUserList")]
        [JsonReturn]
        Task<HeaderResult<List<UserDto>>> GetUserListAsync([JsonContent] SearchUserDto input);
    }
}
