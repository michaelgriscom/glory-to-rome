using System;
using GTR.Core.Model;

namespace GTR.Universal.Services
{
    public class TimestampMessage
    {
        public UserMessage Message { get; private set; }

        public DateTime Timestamp { get; private set; }

        public TimestampMessage(UserMessage userMessage)
        {
            this.Message = userMessage;
            this.Timestamp = DateTime.Now;
        }
    }
}