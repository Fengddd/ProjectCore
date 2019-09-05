using Conference.Common.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ConferenceTest
{
    public class RedisTest
    {
        private readonly IRedisHelper _helper;
        private static IContainer Container { get; set; }
        public RedisTest()
        {
            IServiceCollection registry = new ServiceCollection();
            RedisOptions options = new RedisOptions
            {
                HostName = "127.0.0.1",
                Port = 6379,
                Password = "abcdefg123456",
                DefaultDatabase = string.Empty,
                WriteBuffer = 10240,
                Prefix = string.Empty,
                TryIt = 3,
                PoolSize = 50,
                SSL = false,
                ExpireSeconds = 60,
                EnvName = string.Empty
            };
            registry.TryAddSingleton<IRedisHelper>(new CsRedisHelper(options));
           
            var builder = new ContainerBuilder();
            builder.Populate(registry);
            Container = builder.Build();
            var serviceProvider = new AutofacServiceProvider(Container);
            _helper = serviceProvider.GetService<IRedisHelper>();
        }

        /// <summary>
        /// 保存string
        /// </summary>
        [Fact]
        public void StringSet()
        {
            var isTrue = _helper.StringSet("StringSet", "StringSet");
            var str = _helper.GetString("StringSet");
            Assert.True(isTrue);
            Assert.Equal("StringSet", str);
        }

        /// <summary>
        /// 保存一个集合
        /// </summary>
        [Fact]
        public void StringSetList()
        {
            var customerList = CustomerList();
            var isTrue = _helper.StringSet("StringSetList", customerList);
            var list = _helper.GetString<List<Customer>>("StringSetList");
            Assert.True(isTrue);
            Assert.True(list.Count > 0);
        }

        /// <summary>
        /// 保存Hash
        /// </summary>
        [Fact]
        public void RedisHashSetEntity()
        {
            var customer = new Customer() { Id = "da256fe3-9dc0-46a0-93f3-852a3dbc8dc8", CustomerName = "李四", CustomerAge = 18, CustomerPhone = "1587954569" };
            var isTrue = _helper.HashSetEntity("key", customer, e => e.Id);
            var dictionary = _helper.GetAllHashDictionary<Customer>("key");
            var customerResult = _helper.GetHash<Customer>("key", "da256fe3-9dc0-46a0-93f3-852a3dbc8dc8");
            Assert.True(isTrue);
            Assert.True(dictionary.Count > 0);
            Assert.NotNull(customerResult);
        }

        /// <summary>
        /// 保存Hash
        /// </summary>
        [Fact]
        public void RedisHashSetList()
        {
            var customerList = CustomerList();
            var isTrue = _helper.HashSetList("key", customerList, e => e.Id);
            var dictionary = _helper.GetAllHashDictionary<Customer>("key");
            var customer = _helper.GetHash<Customer>("key", "da256fe3-9dc0-46a0-93f3-852a3dbc8dc8");
            Assert.True(isTrue);
            Assert.True(dictionary.Count > 0);
            Assert.NotNull(customer);
        }

        /// <summary>
        /// 删除String
        /// </summary>
        [Fact]
        public void RedisDeleteString()
        {
            var isTrue = _helper.ExistsStringKey("StringSet");
            var i= _helper.DeleteString("StringSet");
            Assert.True(isTrue);
            Assert.True(i);
        }

        /// <summary>
        /// 删除String
        /// </summary>
        [Fact]
        public void RedisDeleteHash()
        {
            var isTrue = _helper.ExistsHashKey("key", "b4c57dbf-5db7-4d54-a1a5-d819c61bcd64");
            var i = _helper.DeleteHash("key", "b4c57dbf-5db7-4d54-a1a5-d819c61bcd64");
            Assert.True(isTrue);
            Assert.True(i);
        }

        /// <summary>
        /// 测试通配符
        /// </summary>
        [Fact]
        public void RedisLike()
        {
            var i = _helper.GetFilteredList<string>("like*");
            Assert.True(i.Count>0);
        }

        public List<Customer> CustomerList()
        {
            return new List<Customer>()
            {
                new Customer(){Id = Guid.NewGuid().ToString(),CustomerName = "李四",CustomerAge = 18,CustomerPhone="1587954569"},
                new Customer(){Id = Guid.NewGuid().ToString(),CustomerName = "王五",CustomerAge = 19,CustomerPhone="1587954889"},
                new Customer(){Id = Guid.NewGuid().ToString(),CustomerName = "李锋",CustomerAge = 20,CustomerPhone="1587954559"},
                new Customer(){Id = Guid.NewGuid().ToString(),CustomerName = "小白",CustomerAge = 21,CustomerPhone="1587954669"}
            };
        }

    }
    public class Customer
    {
        public string Id { get; set; }
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 客户年龄
        /// </summary>
        public int CustomerAge { get; set; }
        /// <summary>
        /// 客户电话
        /// </summary>
        public string CustomerPhone { get; set; }

    }

}
