#region

using GTR.Core.Model;

#endregion

namespace GTR.Core.Services
{
    public enum MessageType
    {
        Information,
        Warning,
        Error
    }

    public interface IMessageProvider
    {
        void Display(UserMessage message);
    }
}