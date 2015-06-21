using System.Diagnostics;
using GTR.Core.Services;

namespace GTR.Core.UnitTests.Services
{
    public class DebugMessageProvider : IMessageProvider
    {
        public void Display(string message, MessageType messageType = MessageType.Information)
        {
            Debug.WriteLine("{0}: {1}", messageType, message);
        }
    }
}
