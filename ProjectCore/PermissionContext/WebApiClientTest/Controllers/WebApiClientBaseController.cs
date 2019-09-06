using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectCore.Application.AppUser.Dto;
using ProjectCore.Common;
using WebApiClient;

namespace WebApiClientTest.Controllers
{
    /// <summary>
    /// WebApiClientBase 基础
    /// </summary>
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class WebApiClientBaseController : ControllerBase
    {
        private readonly IWebApiClientBase _webApiClientBase;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="webApiClientBase"></param>
        public WebApiClientBaseController(IWebApiClientBase webApiClientBase)
        {
            _webApiClientBase = webApiClientBase;
        }

        /// <summary>
        /// WebApiClientBase
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<HeaderResult<List<UserDto>>> WebApiClientBaseAsync()
        {
            SearchUserDto search = new SearchUserDto()
            {
                PageIndex = 1,
                Pagesize = 10,
                SearchPwd = "",
                SearchName = ""
            };
            try
            {
                var myWebApi = HttpApi.Create<IWebApiClientBase>();
                var userList = await myWebApi.GetUserListAsync(search);
                return userList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        /// <summary>
        /// WebApiClientBaseLoc
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<HeaderResult<List<UserDto>>> WebApiClientBaseLocAsync()
        {
            SearchUserDto search = new SearchUserDto()
            {
                PageIndex = 1,
                Pagesize = 10,
                SearchPwd = "",
                SearchName = ""
            };
            try
            {
                var userList = await _webApiClientBase.GetUserListAsync(search);
                return userList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

    }
}