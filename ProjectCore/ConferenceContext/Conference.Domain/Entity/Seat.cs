using System;
using System.Collections.Generic;
using System.Text;

namespace Conference.Domain.Entity
{
    /// <summary>
    /// 座位
    /// </summary>
    public class Seat
    {
        public Seat()
        {

        }

        public Seat(int seatNumber, Guid seatTypeId)
        {
            Id = Guid.NewGuid();
            SeatNumber = seatNumber;
            SeatTypeId = seatTypeId;
        }

        /// <summary>
        /// 座位Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 座位编号
        /// </summary>
        public int SeatNumber { get; private set; }

        /// <summary>
        /// 座位类型Id
        /// </summary>
        public Guid SeatTypeId { get; private set; }

        /// <summary>
        /// 座位类型
        /// </summary>
        public SeatType SeatTypeInfo { get; set; }
    }
}
