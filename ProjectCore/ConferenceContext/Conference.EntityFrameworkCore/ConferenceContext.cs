using System;
using System.Linq.Expressions;
using Conference.Common;
using Conference.Common.Interface;
using Conference.Common.Log;
using Conference.Domain;
using Conference.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Conference.EntityFrameworkCore
{
    /// <summary>
    /// EF Core 上下文
    /// </summary>
    public class ConferenceContext : DbContext, IConferenceContext
    {
        public ConferenceContext(DbContextOptions<ConferenceContext> options)
        : base(options)
        {
            // Database.EnsureCreated();
        }

        /// <summary>
        /// 审计日志
        /// </summary>
        public DbSet<MonitorLog> MonitorLogInfo { get; set; }

        /// <summary>
        /// 事件存储
        /// </summary>
        public DbSet<EventStorage> EventStorageInfo { get; set; }

        /// <summary>
        /// 客户
        /// </summary>
        public DbSet<Customer> CustomerInfo { get; set; }

        /// <summary>
        /// 会议
        /// </summary>
        public DbSet<ConferenceInfo> ConferenceInfo { get; set; }

        /// <summary>
        /// 座位类型
        /// </summary>
        public DbSet<SeatType> SeatTypeInfo { get; set; }

        /// <summary>
        /// 座位
        /// </summary>
        public DbSet<Seat> SeatInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MonitorLog>().ToTable("MonitorLogInfo").HasKey(e => e.Id);
            modelBuilder.Entity<MonitorLog>().HasIndex(e => new { e.ControllerName, e.ActionName, e.ExecuteStartTime, e.ExecuteEndTime });

            modelBuilder.Entity<EventStorage>().ToTable("EventStorageInfo").HasKey(e => e.Id);
            modelBuilder.Entity<EventStorage>().HasIndex(e => new { e.AggregateRootId, e.AggregateRootType });

            modelBuilder.Entity<Customer>().ToTable("CustomerInfo").HasKey(e => e.Id);
            modelBuilder.Entity<Customer>().OwnsOne(e => e.CustomerAddress);

            modelBuilder.Entity<ConferenceInfo>().ToTable("ConferenceInfo").HasKey(e => e.Id);
            modelBuilder.Entity<ConferenceInfo>().HasMany(e => e.SeatTypeList).WithOne(e => e.ConferenceInfo)
                .HasForeignKey(e => e.ConferenceId);


            modelBuilder.Entity<SeatType>().ToTable("SeatTypeInfo").HasKey(e => e.Id);
            modelBuilder.Entity<SeatType>().HasMany(e => e.SeatList).WithOne(e => e.SeatTypeInfo)
                .HasForeignKey(e => e.SeatTypeId);

            modelBuilder.Entity<Seat>().ToTable("SeatInfo").HasKey(e => e.Id);

            #region 设置软删除
            //modelBuilder.ApplyConfiguration<UserInfo>(new UserMap());   
            //设置软删除
            //foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            //{           
            //    //// 1. Add the IsDeleted property
            //    //entityType.GetOrAddProperty("IsDeleted", typeof(bool));
            //    // 2. Create the query filter
            //    var parameter = Expression.Parameter(entityType.ClrType);
            //    //查询类上面是否有SoftDelete（值对象）的特性
            //    var ownedModelType = parameter.Type;
            //    var ownedAttribute = Attribute.GetCustomAttribute(ownedModelType, typeof(SoftDeleteAttribute));
            //    if (ownedAttribute == null)
            //    {
            //        // 3. EF.Property<bool>(post, "IsDeleted")
            //        var propertyMethodInfo = typeof(EF).GetMethod("Property").MakeGenericMethod(typeof(bool));
            //        var isDeletedProperty =
            //            Expression.Call(propertyMethodInfo, parameter, Expression.Constant("IsDeleted"));

            //        // 4. EF.Property<bool>(post, "IsDeleted") == false
            //        BinaryExpression compareExpression = Expression.MakeBinary(ExpressionType.Equal, isDeletedProperty,
            //            Expression.Constant(false));

            //        // 5. post => EF.Property<bool>(post, "IsDeleted") == false
            //        var lambda = Expression.Lambda(compareExpression, parameter);

            //        modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            //    }             
            //}


            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
