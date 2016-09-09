#region

using System.Collections.ObjectModel;
using GTR.Core.Model;
using GTR.Core.Services;
using GTR.Core.Util;

#endregion

namespace GTR.Universal.Services
{
    public class MessageProvider : ObservableObject, IMessageProvider
    {
        public MessageProvider()
        {
            Messages = new ObservableCollection<TimestampMessage>();
        }

        public ObservableCollection<TimestampMessage> Messages { get; }

        public void Display(UserMessage message)
        {
            TimestampMessage umt = new TimestampMessage(message);
            Messages.Insert(0, umt);
        }
    }
}