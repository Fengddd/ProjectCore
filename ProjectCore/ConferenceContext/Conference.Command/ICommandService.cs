using System.Threading.Tasks;

namespace Conference.Command
{
    /// <summary>
    /// CommandService
    /// </summary>
    public interface ICommandService
    {
        /// <summary>
        /// 发送命令
        /// </summary>
        void SendCommand(ICommand command);

        /// <summary>
        /// 异步发送命令
        /// </summary>
        /// <returns></returns>
        Task SendCommandAsync(ICommand command);
    }
}
