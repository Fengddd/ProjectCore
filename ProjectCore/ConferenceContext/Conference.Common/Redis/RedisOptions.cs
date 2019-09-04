using System;
using System.Collections.Generic;
using System.Text;

namespace Conference.Common.Redis
{
    /// <summary>
    /// Redis 配置选项
    /// </summary>
    public class RedisOptions
    {
        /// <summary>
        /// Redis服务主机名或IP地址
        /// </summary>
        public string HostName { get; set; }

        /// <summary>
        /// Redis服务端口号
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 默认数据库名
        /// </summary>
        public string DefaultDatabase { get; set; }

        /// <summary>
        /// 连接池大小（默认50）
        /// </summary>
        public int PoolSize { get; set; } = 50;

        /// <summary>
        /// 是否预热连接，默认为True
        /// </summary>
        public bool Preheat { get; set; } = true;

        /// <summary>
        /// 是否开启加密传输，默认为False
        /// </summary>
        public bool SSL { get; set; } = false;

        /// <summary>
        /// 异步方法写入缓冲区大小(字节)
        /// </summary>
        public int WriteBuffer { get; set; } = 10240;

        /// <summary>
        /// 统一设置缓存过期时间，默认为60S
        /// </summary>
        public int ExpireSeconds { get; set; } = 60;

        /// <summary>
        /// 执行命令出错，尝试重试的次数
        /// </summary>
        public int TryIt { get; set; } = 0;

        /// <summary>
        /// 连接名称，可以使用 Client List 命令查看
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// key前辍，所有方法都会附带此前辍，Set(prefix + "key", 111);
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// 环境名称
        /// </summary>
        public string EnvName { get; set; }

        public override string ToString()
        {
            // "127.0.0.1:6379,password=123,defaultDatabase=13,poolsize=50,preheat=true,ssl=false,writeBuffer=10240,tryit=0,name=clientName,prefix=key前辍"
            var connStr = $"{HostName}:{Port},password={Password},defaultDatabase={DefaultDatabase},poolsize={PoolSize},preheat={Preheat},ssl={SSL},writeBuffer={WriteBuffer},tryit={TryIt}";

            if (!string.IsNullOrWhiteSpace(ClientName))
            {
                connStr += $",name={ClientName}";
            }

            if (!string.IsNullOrWhiteSpace(Prefix))
            {
                connStr += $",prefix={Prefix}";
            }

            return connStr;
        }
    }

}
