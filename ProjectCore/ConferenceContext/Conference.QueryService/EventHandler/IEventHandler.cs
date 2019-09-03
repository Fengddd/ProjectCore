using System.Threading.Tasks;

namespace Conference.QueryService.EventHandler
{
    /// <summary>
    /// 处理事件
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    public interface IEventHandler<in TEvent> where TEvent : class
    {
        /// <summary>
        /// 异步处理事件
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task HandleAsync(TEvent command);
    }
}
