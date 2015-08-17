using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTR.Core.Services;
using GTR.Core.Util;

namespace GTR.Core.Model
{
    public class UserMessage
    {
        public MessageType Type { get; private set; }
        public string Text { get; private set; }
        public string Source { get; private set; }

        public UserMessage(string text, string source, MessageType type)
        {
            this.Source = source;
            this.Text = text;
            this.Type = type;
        }

        public UserMessage(string text, string source) : this (text, source, MessageType.Information)
        {
            
        }

        public UserMessage(string text, MessageType messageType) : this(text, "System", messageType)
        {

        }

        public UserMessage(string text) : this(text, "System", MessageType.Information)
        {

        }
    }
}
