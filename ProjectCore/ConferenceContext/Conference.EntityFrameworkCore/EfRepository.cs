using System;
using System.Threading;
using System.Threading.Tasks;
using Conference.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Conference.EntityFrameworkCore
{
    public class EfRepository : DbContext, IRepository
    {
        public readonly DbContext _dbContext;
        public EfRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// 提交
        /// </summary>
        public void Commit()
        {   
            
           _dbContext.SaveChanges();        
        }
        /// <summary>
        /// 异步提交
        /// </summary>
        /// <returns></returns>
        public async Task CommitAsync()
        {          
             await _dbContext.SaveChangesAsync(CancellationToken.None);           
        }
        /// <summary>
        /// 内存回收
        /// </summary>
        public void Dispose()
        {
            _dbContext.Dispose();
            GC.Collect();
        }

        /// <summary>
        /// 保存更改前操作
        /// </summary>
        public virtual void SaveChangesBefore()
        {
            
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:

                        break;
                    case EntityState.Modified:

                        break;
                    case EntityState.Deleted:

                        break;
                }
            }
        }
    }
}
