#region

using System;
using GTR.Core.Model;

#endregion

namespace GTR.Universal.Services
{
    public class TimestampMessage
    {
        public TimestampMessage(UserMessage userMessage)
        {
            Message = userMessage;
            Timestamp = DateTime.Now;
        }

        public UserMessage Message { get; private set; }
        public DateTime Timestamp { get; private set; }
    }
}