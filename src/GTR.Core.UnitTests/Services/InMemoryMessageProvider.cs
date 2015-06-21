using System.Text;
using GTR.Core.Services;

namespace GTR.Core.UnitTests.Services
{
    class InMemoryMessageProvider : IMessageProvider
    {
        StringBuilder stringBuilder = new StringBuilder();

        public void Display(string message, MessageType messageType = MessageType.Information)
        {
            stringBuilder.AppendLine(string.Format("{0}: {1}", messageType, message));
        }

        public string DumpMessages()
        {
            return stringBuilder.ToString();
        }
    }
}
