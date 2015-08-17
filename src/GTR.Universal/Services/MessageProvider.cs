using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTR.Core.Model;
using GTR.Core.Services;
using GTR.Core.Util;

namespace GTR.Universal.Services
{
    public class MessageProvider : ObservableObject, IMessageProvider
    {
        public ObservableCollection<TimestampMessage> Messages { get; }

        public MessageProvider()
        {
            Messages = new ObservableCollection<TimestampMessage>();
        }

        public void Display(UserMessage message)
        {
            TimestampMessage umt = new TimestampMessage(message);
            Messages.Add(umt);
        }
    }
}
