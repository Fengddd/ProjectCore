using System;
using System.Net.Sockets;
using DotNetCore.CAP;
using System.Threading.Tasks;
using Polly;
using System.Data;
using Conference.Common.Dapper;

namespace Conference.Command
{
    /// <summary>
    /// CommandService
    /// </summary>
    public class CommandService : ICommandService
    {
        private readonly ICapPublisher _capPublisher;
        private readonly IDbConnection _connection = DapperConnection.DapperInstance();
        public CommandService(ICapPublisher capPublisher)
        {
            _capPublisher = capPublisher;
        }

        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="command"></param>
        public void SendCommand(ICommand command)
        {
            var routingKey = command.GetRoutingKey();
            TryDapperConnection();
            using (var transaction = _connection.BeginTransaction())
            {
                _capPublisher.Publish(routingKey, command);
                transaction.Commit();
                _connection.Close();
            }
        }

        /// <summary>
        /// 异步发送命令
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task SendCommandAsync(ICommand command)
        {
            var routingKey = command.GetRoutingKey();       
            TryDapperConnection();
            using (var transaction = _connection.BeginTransaction())
            {
                await _capPublisher.PublishAsync(routingKey, command);
                transaction.Commit();
                _connection.Close();
            }
        }

        /// <summary>
        /// Dapper连接异常重试
        /// </summary>
        public void TryDapperConnection()
        {
            var policy = Policy.Handle<SocketException>().Or<InvalidOperationException>()
                .WaitAndRetry(5, p => TimeSpan.FromSeconds(1), (ex, time) =>
                {
                    //记录错误日志
                });
            policy.Execute(() =>
            {
                _connection.Open();
            });
        }
    }
}
