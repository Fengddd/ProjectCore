using System;
using System.Collections.Generic;
using System.Text;

namespace Conference.Command.Command
{
    public class PublishConferenceCommand : ConferenceCommand
    {
        public PublishConferenceCommand(Guid conferenceId)
        {
            this.AggregateRootId = conferenceId;
        }
    }
}
