using System.Text;
using GTR.Core.Model;
using GTR.Core.Services;

namespace GTR.Core.UnitTests.Services
{
    class InMemoryMessageProvider : IMessageProvider
    {
        StringBuilder stringBuilder = new StringBuilder();

        public void Display(UserMessage message)
        {
            stringBuilder.AppendLine(string.Format("{0}: {1}", message.Type, message.Text));
        }

        public string DumpMessages()
        {
            return stringBuilder.ToString();
        }
    }
}
