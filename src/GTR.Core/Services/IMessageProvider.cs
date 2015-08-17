using GTR.Core.Model;

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