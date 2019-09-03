namespace Conference.Domain.DomainEvent
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
