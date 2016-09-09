using System.Diagnostics;
using GTR.Core.Model;
using GTR.Core.Services;

namespace GTR.Core.UnitTests.Services
{
    public class DebugMessageProvider : IMessageProvider
    {
        public void Display(UserMessage message)
        {
            Debug.WriteLine("{0}: {1}", message.Type, message.Text);
        }
    }
}
