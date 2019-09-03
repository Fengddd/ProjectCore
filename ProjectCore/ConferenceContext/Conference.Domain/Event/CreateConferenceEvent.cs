using System;
using System.Collections.Generic;
using System.Text;
using Conference.Domain.Impl;
using Conference.Domain.Entity;
namespace Conference.Domain.Event
{
    public class CreateConferenceEvent : DomainEvent
    {
        public CreateConference CreateConferenceInfo { get; set; }

        public CreateConferenceEvent(CreateConference createConference)
        {
            this.CreateConferenceInfo = createConference;
        }

    }

}
