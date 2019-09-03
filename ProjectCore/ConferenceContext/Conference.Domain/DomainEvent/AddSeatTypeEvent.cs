using System.Collections.Generic;
using Conference.Domain.Entity;

namespace Conference.Domain.DomainEvent
{
    public class AddSeatTypeEvent : DomainEvent
    {
        public List<SeatType> SeatTypeList { get; private set; }
        public AddSeatTypeEvent(List<SeatType> seatTypeList)
        {
            SeatTypeList = seatTypeList;
        }
    }
}
