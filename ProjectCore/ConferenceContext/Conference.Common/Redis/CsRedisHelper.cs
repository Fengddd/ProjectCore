using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSRedis;
using Newtonsoft.Json;

namespace Conference.Common.Redis
{
    /// <summary>
    /// CsRedis帮助类
    /// </summary>
    public class CsRedisHelper : IRedisHelper
    {
        private readonly CSRedisClient _redisClient;
        private readonly RedisOptions _redisOptions;

        /// <summary>
        /// 使用自定义方法序列化 -> 避免自循环
        /// </summary>
        private readonly Func<object, string> _jsonSerializer = (value) =>
        {
            var serializerSettings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind
            };
            return value != null ? JsonConvert.SerializeObject(value, serializerSettings) : null;
        };

        /// <summary>
        /// RedisClient构造方法
        /// </summary>
        /// <param name="redisOptions">redis参数</param>
        public CsRedisHelper(RedisOptions redisOptions)
        {
            _redisOptions = redisOptions;
            _redisClient = new CSRedisClient(_redisOptions.ToString());
            RedisHelper.Initialization(_redisClient);
            // 使用自定义方法序列化 -> 避免自循环
            CSRedisClient.Serialize = _jsonSerializer;
        }

        #region String

        /// <summary>
        /// 保存单个key value
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        /// <param name="expireSeconds">过期时间</param>
        /// <returns>Boolean</returns>
        public bool StringSet(string key, object value, int? expireSeconds = null)
        {
            try
            {
                return (expireSeconds == null) ?
                    RedisHelper.Set(key, value) :
                    RedisHelper.Set(key, value, expireSeconds.Value);

            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 异步保存单个key value
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        /// <param name="expireSeconds">过期时间</param>
        /// <returns>Boolean</returns>
        public async Task<bool> StringSetAsync(string key, object value, int? expireSeconds = null)
        {
            try
            {
                return (expireSeconds == null) ?
                    await RedisHelper.SetAsync(key, value) :
                    await RedisHelper.SetAsync(key, value, expireSeconds.Value);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 获取单个key的值
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <returns>String</returns>
        public string GetString(string key)
        {
            try
            {
                return RedisHelper.Get(key);
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 异步获取单个key的值
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <returns>String</returns>

        public async Task<string> GetStringAsync(string key)
        {
            try
            {
                return await RedisHelper.GetAsync(key);
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取对象或集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns>T</returns>
        public T GetString<T>(string key)
        {
            try
            {
                return RedisHelper.Get<T>(key);
            }
            catch (Exception e)
            {
                return default(T);
            }
        }

        /// <summary>
        /// 异步获取对象或集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns>T</returns>
        public async Task<T> GetStringAsync<T>(string key)
        {
            try
            {
                return await RedisHelper.GetAsync<T>(key);
            }
            catch (Exception e)
            {
                return default(T);
            }
        }

        /// <summary>
        /// 删除 String
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Boolean</returns>
        public bool DeleteString(string key)
        {
            try
            {
                return RedisHelper.Del(key) > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 异步删除 String
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Boolean</returns>
        public async Task<bool> DeleteStringAsync(string key)
        {
            try
            {
                return await RedisHelper.DelAsync(key) > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 检查是否存在 String
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Boolean</returns>
        public bool ExistsStringKey(string key)
        {
            try
            {
                return RedisHelper.Exists(key);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 检查是否存在 String
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Boolean</returns>
        public async Task<bool> ExistsStringKeyAsync(string key)
        {
            try
            {
                return await RedisHelper.ExistsAsync(key);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        #endregion

        #region Hash

        /// <summary>
        /// 保存一个对象  Hash
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">Redis Key</param>
        /// <param name="item">数据集合</param>
        /// <param name="itemKey">HashSet</param>
        public bool HashSetEntity<T>(string key, T item, Func<T, string> itemKey)
        {
            var isTrue = true;
            try
            {
                RedisHelper.HSet(key, itemKey(item), item);
            }
            catch (Exception e)
            {
                isTrue = false;
            }
            return isTrue;
        }

        /// <summary>
        /// 异步保存一个对象  Hash
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">Redis Key</param>
        /// <param name="item">数据集合</param>
        /// <param name="itemKey">HashSet</param>
        public async Task<bool> HashSetEntityAsync<T>(string key, T item, Func<T, string> itemKey)
        {
            var isTrue = true;
            try
            {
                await RedisHelper.HSetAsync(key, itemKey(item), item);
            }
            catch (Exception e)
            {
                isTrue = false;
            }
            return isTrue;
        }

        /// <summary>
        /// 保存一个集合  Hash
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">Redis Key</param>
        /// <param name="list">数据集合</param>
        /// <param name="itemKey">HashSet</param>
        public bool HashSetList<T>(string key, List<T> list, Func<T, string> itemKey)
        {
            var isTrue = true;
            try
            {
                foreach (var item in list)
                {
                    RedisHelper.HSet(key, itemKey(item), item);
                }
            }
            catch (Exception e)
            {
                isTrue = false;
            }
            return isTrue;
        }

        /// <summary>
        /// 异步保存一个集合  Hash
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">Redis Key</param>
        /// <param name="list">数据集合</param>
        /// <param name="itemKey">key</param>
        public async Task<bool> HashSetListAsync<T>(string key, List<T> list, Func<T, string> itemKey)
        {
            var isTrue = true;
            try
            {
                foreach (var item in list)
                {
                    var c = itemKey(item);
                    await RedisHelper.HSetAsync(key, itemKey(item), item);
                }
            }
            catch (Exception e)
            {
                isTrue = false;
            }
            return isTrue;
        }

        /// <summary>
        /// 获取 Hash 集合或对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns>Dictionary</returns>
        public Dictionary<string, T> GetAllHashDictionary<T>(string key)
        {
            try
            {
                return RedisHelper.HGetAll<T>(key);
            }
            catch (Exception e)
            {
                return default(Dictionary<string, T>);
            }
        }

        /// <summary>
        /// 异步获取 Hash 集合或对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns>Dictionary</returns>
        public async Task<Dictionary<string, T>> GetAllHashDictionaryAsync<T>(string key)
        {
            try
            {
                return await RedisHelper.HGetAllAsync<T>(key);
            }
            catch (Exception e)
            {
                return default(Dictionary<string, T>);
            }
        }

        /// <summary>
        /// 获取 Hash 集合或对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="itemKey"></param>
        /// <returns>T</returns>
        public T GetHash<T>(string key, string itemKey)
        {
            try
            {
                return RedisHelper.HGet<T>(key, itemKey);
            }
            catch (Exception e)
            {
                return default(T);
            }
        }

        /// <summary>
        /// 异步获取 Hash 集合或对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="itemKey"></param>
        /// <returns>T</returns>
        public async Task<T> GetHashAsync<T>(string key, string itemKey)
        {
            try
            {
                return await RedisHelper.HGetAsync<T>(key, itemKey);
            }
            catch (Exception e)
            {
                return default(T);
            }
        }

        /// <summary>
        /// 删除Hash
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool DeleteAllHash(string key)
        {
            try
            {
                return RedisHelper.HDel(key) > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 异步删除Hash
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAllHashAsync(string key)
        {
            try
            {
                return await RedisHelper.HDelAsync(key) > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 删除Hash
        /// </summary>
        /// <param name="key"></param>
        /// <param name="itemKey"></param>
        /// <returns></returns>
        public bool DeleteHash(string key, string itemKey)
        {
            try
            {
                return RedisHelper.HDel(key, itemKey) > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 异步删除Hash
        /// </summary>
        /// <param name="key"></param>
        /// <param name="itemKey"></param>
        /// <returns></returns>
        public async Task<bool> DeleteHashAsync(string key, string itemKey)
        {
            try
            {
                return await RedisHelper.HDelAsync(key, itemKey) > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 检查是否存在 Hash
        /// </summary>
        /// <param name="key"></param>
        /// <param name="itemKey"></param>
        /// <returns></returns>
        public bool ExistsHashKey(string key, string itemKey)
        {
            try
            {
                return RedisHelper.HExists(key, itemKey);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 检查是否存在 Hash
        /// </summary>
        /// <param name="key"></param>
        /// <param name="itemKey"></param>
        /// <returns></returns>
        public async Task<bool> ExistsHashKeyAsync(string key, string itemKey)
        {
            try
            {
                return await RedisHelper.HExistsAsync(key, itemKey);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        #endregion

        /// <summary>
        /// 获取过滤数据集(模糊匹配)
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="pattern">通配符</param>
        /// <returns>数据集</returns>
        public IList<T> GetFilteredList<T>(string pattern)
        {
            try
            {
                var keys = _redisClient.Keys(pattern);
                var filteredList = _redisClient.MGet<T>(keys);

                return filteredList as IList<T>;
            }
            catch (Exception e)
            {
                return default(IList<T>);
            }
        }

        /// <summary>
        /// 异步获取过滤数据集(模糊匹配)
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="pattern">通配符</param>
        /// <returns>数据集</returns>
        public async Task<IList<T>> GetFilteredListAsync<T>(string pattern)
        {
            try
            {
                var keys = await _redisClient.KeysAsync(pattern);
                var filteredList = await _redisClient.MGetAsync<T>(keys);

                return filteredList as IList<T>;
            }
            catch (Exception e)
            {
                return default(IList<T>);
            }
        }

        /// <summary>
        /// 重置缓存数据过期时间
        /// </summary>
        /// <param name="pattern">通配符</param>
        /// <param name="addSeconds">更新的过期时间</param>
        /// <returns>是否成功</returns>
        public bool ResetAllExpireTime(string pattern, int addSeconds = 3600)
        {
            try
            {
                var keys = _redisClient.Keys(pattern);
                if (keys == null || keys.Length == 0)
                {
                    return false;
                }

                foreach (var key in keys)
                {
                    _redisClient.ExpireAt(key, DateTime.Now.AddSeconds(addSeconds));
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 异步重置缓存数据过期时间
        /// </summary>
        /// <param name="pattern">通配符</param>
        /// <param name="addSeconds">更新的过期时间</param>
        /// <returns>是否成功</returns>
        public async Task<bool> ResetAllExpireTimeAsync(string pattern, int addSeconds = 3600)
        {
            try
            {
                var keys = await _redisClient.KeysAsync(pattern);
                if (keys == null || keys.Length == 0)
                {
                    return false;
                }

                foreach (var key in keys)
                {
                    await _redisClient.ExpireAtAsync(key, DateTime.Now.AddSeconds(addSeconds));
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 重置单个缓存数据过期时间
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="addSeconds">更新的过期时间</param>
        /// <returns>是否成功</returns>
        public bool ResetExpireTime(string key, int addSeconds = 3600)
        {
            try
            {
                return _redisClient.ExpireAt(key, DateTime.Now.AddSeconds(addSeconds));
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 异步单个重置缓存数据过期时间
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="addSeconds">更新的过期时间</param>
        /// <returns>是否成功</returns>
        public async Task<bool> ResetExpireTimeAsync(string key, int addSeconds = 3600)
        {
            try
            {
                return await _redisClient.ExpireAtAsync(key, DateTime.Now.AddSeconds(addSeconds));
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 管道模式
        /// 打包多条命令一起执行，从而提高性能
        /// eg. StartPipe().Set("a","111").Get("a").EndPipe();
        /// </summary>
        /// <returns>操作结果</returns>
        private CSRedisClientPipe<string> StartPipe()
        {
            return RedisHelper.StartPipe();
        }

        /// <summary>
        /// 获取Redis配置
        /// </summary>
        /// <returns></returns>
        public RedisOptions GetRedisOptions()
        {
            return _redisOptions;
        }

    }
}
