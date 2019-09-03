using System;

namespace Conference.Domain
{
    /// <summary>
    /// 实体
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// 实体Id
        /// </summary>
        Guid Id { get; set; }

    }
}
