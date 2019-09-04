using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Conference.Common.Redis
{
    /// <summary>
    /// CsRedis帮助类
    /// </summary>
   public interface IRedisHelper
    {
        /// <summary>
        /// 获取Redis配置信息
        /// </summary>
        /// <returns></returns>
        RedisOptions GetRedisOptions();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireSeconds"></param>
        /// <returns></returns>
        bool Set(string key, object value, int? expireSeconds = null);

        Task<bool> SetAsync(string key, object value, int? expireSeconds = null);

        string Get(string key);

        Task<string> GetAsync(string key);

        T Get<T>(string key);

        Task<T> GetAsync<T>(string key);

        IList<T> GetFilteredList<T>(string pattern);

        Task<IList<T>> GetFilteredListAsync<T>(string pattern);

        bool ResetExpireTime(string key, int addSeconds = 3600);

        Task<bool> ResetExpireTimeAsync(string key, int addSeconds = 3600);

        bool ResetAllExpireTime(string pattern, int addSeconds = 3600);

        Task<bool> ResetAllExpireTimeAsync(string pattern, int addSeconds = 3600);

        /// <summary>
        /// 原子自增
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">自增量</param>
        /// <returns>自增值</returns>
        long IncBy(string key, long value = 1L);
    }
}
