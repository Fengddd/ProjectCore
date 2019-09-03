namespace Conference.Domain.DomainEvent
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
