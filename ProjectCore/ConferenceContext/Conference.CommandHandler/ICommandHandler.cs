using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace Conference.CommandHandler
{
    /// <summary>
    /// 事件处理器
    /// </summary>
    public interface ICommandHandler<in TCommand> where TCommand : class
    {
        /// <summary>
        /// 异步处理事件
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task HandleAsync(TCommand command);
    }
}
