using Conference.Domain.Impl;
using System;
using System.Collections.Generic;
using System.Text;
using Conference.Domain.Entity;

namespace Conference.Domain.Event
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
