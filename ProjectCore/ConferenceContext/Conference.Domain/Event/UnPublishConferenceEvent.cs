using Conference.Domain.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conference.Domain.Event
{
    public class UnPublishConferenceEvent : DomainEvent
    {
        public bool ConferencePublishStatus { get; private set; }
        public UnPublishConferenceEvent(bool conferencePublishStatus)
        {
            ConferencePublishStatus = conferencePublishStatus;
        }
    }
}
