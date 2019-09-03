namespace Conference.Domain.DomainEvent
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
