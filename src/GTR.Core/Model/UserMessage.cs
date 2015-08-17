#region

using GTR.Core.Services;

#endregion

namespace GTR.Core.Model
{
    public class UserMessage
    {
        public UserMessage(string text, string source, MessageType type)
        {
            Source = source;
            Text = text;
            Type = type;
        }

        public UserMessage(string text, string source) : this(text, source, MessageType.Information)
        {
        }

        public UserMessage(string text, MessageType messageType) : this(text, "System", messageType)
        {
        }

        public UserMessage(string text) : this(text, "System", MessageType.Information)
        {
        }

        public MessageType Type { get; private set; }
        public string Text { get; private set; }
        public string Source { get; private set; }
    }
}