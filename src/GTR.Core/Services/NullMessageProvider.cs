#region

using System;
using GTR.Core.Model;

#endregion

namespace GTR.Core.Services
{
    public class NullMessageProvider : IMessageProvider
    {
        public void Display(UserMessage message)
        {
        }
    }
}