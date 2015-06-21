#region

using System;

#endregion

namespace GTR.Core.Services
{
    public class NullMessageProvider : IMessageProvider
    {
        public void Display(string message, MessageType messageType = MessageType.Information)
        {
            throw new NotImplementedException();
        }
    }
}