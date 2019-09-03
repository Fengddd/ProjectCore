using System.Threading.Tasks;

namespace Conference.Common.Interface
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// 提交
        /// </summary>
        void Commit();

        /// <summary>
        /// 异步提交
        /// </summary>
        /// <returns></returns>
        Task CommitAsync();       
    }
}
