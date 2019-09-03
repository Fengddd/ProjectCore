using System;
using System.Collections.Generic;
using System.Text;

namespace Conference.Command.Command
{
    /// <summary>
    /// 添加座位类型
    /// </summary>
    public class AddSeatTypeCommand : ConferenceCommand
    {
        public AddSeatTypeCommand(Guid conferenceId)
        {
            this.AggregateRootId = conferenceId;
        }
    }
}
