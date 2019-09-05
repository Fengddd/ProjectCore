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
        #region String

        /// <summary>
        /// 保存单个key value
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        /// <param name="expireSeconds">过期时间</param>
        /// <returns>Boolean</returns>
        bool StringSet(string key, object value, int? expireSeconds = null);

        /// <summary>
        /// 异步保存单个key value
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        /// <param name="expireSeconds">过期时间</param>
        /// <returns>Boolean</returns>
        Task<bool> StringSetAsync(string key, object value, int? expireSeconds = null);

        /// <summary>
        /// 获取单个key的值
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <returns>String</returns>
        string GetString(string key);


        /// <summary>
        /// 异步获取单个key的值
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <returns>String</returns>
        Task<string> GetStringAsync(string key);

        /// <summary>
        /// 获取对象或集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns>T</returns>
        T GetString<T>(string key);

        /// <summary>
        /// 异步获取对象或集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns>T</returns>
        Task<T> GetStringAsync<T>(string key);

        /// <summary>
        /// 删除所有 String
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Boolean</returns>
        bool DeleteString(string key);

        /// <summary>
        /// 异步删除 String
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Boolean</returns>
        Task<bool> DeleteStringAsync(string key);

        /// <summary>
        /// 检查是否存在 String
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Boolean</returns>
        bool ExistsStringKey(string key);

        /// <summary>
        /// 检查是否存在 String
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Boolean</returns>
        Task<bool> ExistsStringKeyAsync(string key);


        #endregion

        /// <summary>
        /// 保存一个对象  Hash
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">Redis Key</param>
        /// <param name="item">数据集合</param>
        /// <param name="itemKey">HashSet</param>
        bool HashSetEntity<T>(string key, T item, Func<T, string> itemKey);

        /// <summary>
        /// 异步保存一个对象  Hash
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">Redis Key</param>
        /// <param name="item">数据集合</param>
        /// <param name="itemKey">HashSet</param>
        Task<bool> HashSetEntityAsync<T>(string key, T item, Func<T, string> itemKey);

        /// <summary>
        /// 保存一个集合  Hash
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">Redis Key</param>
        /// <param name="list">数据集合</param>
        /// <param name="itemKey">HashSet</param>
        bool HashSetList<T>(string key, List<T> list, Func<T, string> itemKey);

        /// <summary>
        /// 异步保存一个集合  Hash
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">Redis Key</param>
        /// <param name="list">数据集合</param>
        /// <param name="itemKey">key</param>
        Task<bool> HashSetListAsync<T>(string key, List<T> list, Func<T, string> itemKey);

        /// <summary>
        /// 获取 Hash 集合或对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns>Dictionary</returns>
        Dictionary<string, T> GetAllHashDictionary<T>(string key);

        /// <summary>
        /// 异步获取 Hash 集合或对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns>Dictionary</returns>
        Task<Dictionary<string, T>> GetAllHashDictionaryAsync<T>(string key);


        /// <summary>
        /// 获取 Hash 集合或对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="itemKey"></param>
        /// <returns>T</returns>
        T GetHash<T>(string key, string itemKey);


        /// <summary>
        /// 异步获取 Hash 集合或对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="itemKey"></param>
        /// <returns>T</returns>
        Task<T> GetHashAsync<T>(string key, string itemKey);

        /// <summary>
        /// 删除Hash
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool DeleteAllHash(string key);

        /// <summary>
        /// 异步删除Hash
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> DeleteAllHashAsync(string key);

        /// <summary>
        /// 删除Hash
        /// </summary>
        /// <param name="key"></param>
        /// <param name="itemKey"></param>
        /// <returns></returns>
        bool DeleteHash(string key, string itemKey);

        /// <summary>
        /// 异步删除Hash
        /// </summary>
        /// <param name="key"></param>
        /// <param name="itemKey"></param>
        /// <returns></returns>
        Task<bool> DeleteHashAsync(string key, string itemKey);

        /// <summary>
        /// 检查是否存在 Hash
        /// </summary>
        /// <param name="key"></param>
        /// <param name="itemKey"></param>
        /// <returns></returns>
        bool ExistsHashKey(string key, string itemKey);

        /// <summary>
        /// 检查是否存在 Hash
        /// </summary>
        /// <param name="key"></param>
        /// <param name="itemKey"></param>
        /// <returns></returns>
        Task<bool> ExistsHashKeyAsync(string key, string itemKey);

        /// <summary>
        /// 获取Redis配置信息
        /// </summary>
        /// <returns></returns>
        RedisOptions GetRedisOptions();

        /// <summary>
        /// 模糊匹配
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pattern"></param>
        /// <returns></returns>
        IList<T> GetFilteredList<T>(string pattern);

        /// <summary>
        /// 异步模糊匹配
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pattern"></param>
        /// <returns></returns>
        Task<IList<T>> GetFilteredListAsync<T>(string pattern);

        bool ResetExpireTime(string key, int addSeconds = 3600);

        Task<bool> ResetExpireTimeAsync(string key, int addSeconds = 3600);

        bool ResetAllExpireTime(string pattern, int addSeconds = 3600);

        Task<bool> ResetAllExpireTimeAsync(string pattern, int addSeconds = 3600);

    }
}
