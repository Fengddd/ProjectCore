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
    public class CsRedisHelper
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
        /// <returns></returns>
        public static bool StringSet(string key, object value, int? expireSeconds = null)
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
        /// <returns></returns>
        public static async Task<bool> StringSetAsync(string key, object value, int? expireSeconds = null)
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
        /// <returns></returns>
        public static string GetString(string key)
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
        /// <returns></returns>

        public static async Task<string> GetStringAsync(string key)
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
        /// 获取一个key的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetString<T>(string key)
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
        /// 异步获取一个key的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task<T> GetStringAsync<T>(string key)
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


        #endregion

        #region Hash

        /// <summary>
        /// 保存一个集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">Redis Key</param>
        /// <param name="list">数据集合</param>
        /// <param name="getModelId">HashSet</param>
        public static bool HashSet<T>(string key, List<T> list, Func<T, string> getModelId)
        {
            var isTrue = true;
            try
            {
                foreach (var item in list)
                {
                    RedisHelper.HSet(key, getModelId(item), item);
                }
            }
            catch (Exception)
            {
                isTrue = false;
            }
            return isTrue;
        }

        #endregion


        /// <summary>
        /// 获取过滤数据集
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
        /// 异步获取过滤数据集
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

        public RedisOptions GetRedisOptions()
        {
            return _redisOptions;
        }

    }
}
