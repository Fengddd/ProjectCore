using System;
using System.Collections.Generic;
using System.Text;
using Conference.Domain.Impl;

namespace Conference.Domain.Event
{
    public class PublishConferenceEvent : DomainEvent
    {
        public bool ConferencePublishStatus { get; private set; }
        public PublishConferenceEvent(bool conferencePublishStatus)
        {
            ConferencePublishStatus = conferencePublishStatus;
        }

    }
}
